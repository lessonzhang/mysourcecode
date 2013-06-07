(function ($) {
    $(document).ready(function () {
        var urlstren = "a.pt?action=Enter&KnowledgeID=<%=this.KnowledgeID%>&urlid=<%=this.URLID%>&userid=<%=this.UserID%>&url=<%=this.URL%>";
        var urlstrex = "a.pt?action=Exit&KnowledgeID=<%=this.KnowledgeID%>&urlid=<%=this.URLID%>&userid=<%=this.UserID%>&url=<%=this.URL%>";
        alert("fdsa");
        window.onfocus = function () {
            alert("onfocus");
            $.ajax({
                type: "Post",
                url: urlstren,
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                },
                error: function (err) {
                }
            });
        

            window.onblur = function () {
                alert("onblur");
            $.ajax({
                type: "Post",
                url: urlstrex,
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                },
                error: function (err) {
                }
            });
        };
};
        window.onload = function () {
            alert("onload");
            $.ajax({
                type: "Post",
                url: urlstren,
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                },
                error: function (err) {
                }
            });
        

            window.onbeforeunload = function () {
                alert("onbeforeunload");
            $.ajax({
                type: "Post",
                url: urlstrex,
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                },
                error: function (err) {
                }
            });
        };
        };
    })
})(this.jQuery)