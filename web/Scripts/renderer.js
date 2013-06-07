(function(){
  
    var Renderer = function (view) {
        var dom = $(view);
        var canvas = dom.get(0);

        //大小
        var cWidth = canvas.width = 600;
        var cHeight = canvas.height = 500;
        var ctx = canvas.getContext("2d");
        var particleSystem = null;
        var gfx = arbor.Graphics(canvas);

        // helpers for figuring out where to draw arrows (thanks springy.js)
        var intersect_line_line = function (p1, p2, p3, p4) {
            var denom = ((p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y));
            if (denom === 0) return false // lines are parallel
            var ua = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denom;
            var ub = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denom;

            if (ua < 0 || ua > 1 || ub < 0 || ub > 1) return false;
            return arbor.Point(p1.x + ua * (p2.x - p1.x), p1.y + ua * (p2.y - p1.y));
        }

        var intersect_line_box = function (p1, p2, boxTuple) {
            var p3 = { x: boxTuple[0], y: boxTuple[1] },
                w = boxTuple[2],
                h = boxTuple[3];

            var tl = { x: p3.x, y: p3.y };
            var tr = { x: p3.x + w, y: p3.y };
            var bl = { x: p3.x, y: p3.y + h };
            var br = { x: p3.x + w, y: p3.y + h };

            return intersect_line_line(p1, p2, tl, tr) ||
                   intersect_line_line(p1, p2, tr, br) ||
                   intersect_line_line(p1, p2, br, bl) ||
                   intersect_line_line(p1, p2, bl, tl) ||
                   false;
        }

        var that = {

            init: function (system) {
                particleSystem = system;
                particleSystem.screenSize(cWidth, cHeight);
                //particleSystem.screenPadding(20);

                //鼠标事件
                this.initMouseHandling();
            },

            resize: function (width, height) {
                cWidth = width;
                cHeight = height;
                particleSystem.screenSize(cWidth, cHeight);
            },

            redraw: function () {
                if (!particleSystem) return;

                ctx.clearRect(0, 0, canvas.width, canvas.height);
                //ctx.fillStyle = "white";
                //ctx.fillRect(0, 0, cWidth, cHeight);

                var nodeBoxes = {};
                particleSystem.eachNode(function (node, pt) {
                    var label = node.data.label || "";
                    var w = ctx.measureText("" + label).width + 10;
                    if (!("" + label).match(/^[ \t]*$/)) {
                        pt.x = Math.floor(pt.x);
                        pt.y = Math.floor(pt.y);
                    } else {
                        label = null;
                    }

                    // draw a rectangle centered at pt
                    if (node.data.color) ctx.fillStyle = node.data.color;
                        // else ctx.fillStyle = "#d0d0d0"
                    else ctx.fillStyle = "rgba(0,0,0,.2)";
                    if (node.data.color == 'none') ctx.fillStyle = "white";

                    // ctx.fillRect(pt.x-w/2, pt.y-10, w,20)
                    if (node.data.shape == 'dot') {
                        gfx.oval(pt.x - w / 2, pt.y - w / 2, w, w, { fill: ctx.fillStyle });
                        nodeBoxes[node.name] = [pt.x - w / 2, pt.y - w / 2, w, w];
                    } else {
                        gfx.rect(pt.x - w / 2, pt.y - 14, w, 30, 4, { fill: ctx.fillStyle });
                        nodeBoxes[node.name] = [pt.x - w / 2, pt.y - 14, w, 30];
                    }

                    // draw the text
                    if (label) {
                        ctx.font = "13px Verdana";
                        ctx.textAlign = "center";
                        ctx.fillStyle = "white";
                        if (node.data.color == 'none') ctx.fillStyle = '#333333';
                        ctx.fillText(label || "", pt.x, pt.y + 4);
                        ///ctx.fillText(label||"", pt.x, pt.y+4);
                    }
                });

                ctx.strokeStyle = "#cccccc";
                ctx.lineWidth = 2;
                ctx.beginPath();
                particleSystem.eachEdge(function (edge, pt1, pt2) {
                    // edge: {source:Node, target:Node, length:#, data:{}}
                    // pt1:  {x:#, y:#}  source position in screen coords
                    // pt2:  {x:#, y:#}  target position in screen coords

                    var weight = edge.data.weight;
                    var color = edge.data.color;

                    // trace(color)
                    if (!color || ("" + color).match(/^[ \t]*$/)) color = null;

                    // find the start point
                    var tail = intersect_line_box(pt1, pt2, nodeBoxes[edge.source.name]);
                    var head = intersect_line_box(tail, pt2, nodeBoxes[edge.target.name]);

                    ctx.save();
                    ctx.beginPath();

                    if (!isNaN(weight)) ctx.lineWidth = weight;
                    if (color) ctx.strokeStyle = color;
                    // if (color) trace(color)
                    ctx.fillStyle = null;

                    ctx.moveTo(tail.x, tail.y);
                    ctx.lineTo(head.x, head.y);
                    ctx.stroke();
                    ctx.restore();

                    // draw an arrowhead if this is a -> style edge
                    if (edge.data.directed) {
                        ctx.save();
                        // move to the head position of the edge we just drew
                        var wt = !isNaN(weight) ? parseFloat(weight) : ctx.lineWidth;
                        var arrowLength = 10 + wt;
                        var arrowWidth = 6 + wt;
                        ctx.fillStyle = (color) ? color : ctx.strokeStyle;
                        ctx.translate(head.x, head.y);
                        ctx.rotate(Math.atan2(head.y - tail.y, head.x - tail.x));

                        // delete some of the edge that's already there (so the point isn't hidden)
                        ctx.clearRect(-arrowLength / 2, -wt / 2, arrowLength / 2, wt);

                        // draw the chevron
                        ctx.beginPath();
                        ctx.moveTo(-arrowLength, arrowWidth);
                        ctx.lineTo(0, 0);
                        ctx.lineTo(-arrowLength, -arrowWidth);
                        ctx.lineTo(-arrowLength * 0.8, -0);
                        ctx.closePath();
                        ctx.fill();
                        ctx.restore();
                    }
                });

            },

            initMouseHandling: function () {
                var _mouseP = null;
                var selected = null;
                var dragged = null;
                var nearest = null;
                var oldmass = 1;

                var handler = {

                    moved: function (e) {
                        if (dragged != undefined && dragged != null && dragged.node !== null) {
                            dragged.node.fixed = true;
                            return false;
                        }

                        var pos = $(canvas).offset();
                        _mouseP = arbor.Point(e.pageX - pos.left, e.pageY - pos.top);
                        nearest = particleSystem.nearest(_mouseP);
                        if (nearest != undefined && nearest != null && nearest.node !== null) {
                            //selected = (nearest.distance < 50) ? nearest : null;
                            if (nearest.distance < 50) {
                                dom.addClass('domhover');
                                nearest.node.fixed = true;
                            } else {
                                dom.removeClass('domhover');
                            }
                        }

                        return false;
                    },

                    clicked: function (e) {
                        var pos = $(canvas).offset();
                        _mouseP = arbor.Point(e.pageX - pos.left, e.pageY - pos.top);
                        nearest = particleSystem.nearest(_mouseP);

                        if (nearest != undefined && nearest != null && nearest.node !== null) {
                            dragged = selected = (nearest.distance < 50) ? nearest : null;
                            if (selected) {
                                selected.node.fixed = true;

                                //显示信息
                                traceObj.tracemessage(selected.node.name);
                            }
                        }

                        $(canvas).bind("mousemove", handler.dragged);
                        $(window).bind("mouseup", handler.dropped);
                        return false;
                    },

                    dragged: function (e) {
                        var pos = $(canvas).offset();
                        var s = arbor.Point(e.pageX - pos.left, e.pageY - pos.top);

                        if (dragged != undefined && dragged != null && dragged.node !== null) {
                            var p = particleSystem.fromScreen(s);
                            dragged.node.p = p; //{x:p.x, y:p.y}
                        }

                        return false;
                    },

                    dropped: function (e) {
                        if (dragged == null || dragged.node == undefined || dragged.node == null) {
                            return;
                        }
                        if (dragged.node !== null) {
                            dragged.node.fixed = false;
                        }
                        dragged.node.tempMass = 1000;
                        dragged = null;
                        selected = null;
                        $(canvas).unbind("mousemove", handler.dragged);
                        $(window).unbind("mouseup", handler.dropped);
                        _mouseP = null;
                        return false;
                    }
                };

                // start listening
                $(canvas).mousedown(handler.clicked);
                $(canvas).mousemove(handler.moved);
            }
        };

        return that;
    }
  
})()