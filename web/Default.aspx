<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <script src="Scripts/jquery-1.7.1.js"></script>
    <script src="Scripts/jquery.showLoading.js"></script>
    <script src="Scripts/jquery.tipTip.minified.js"></script>
      <script type="text/javascript">
          (function ($) {
              myShow = function (course) {
                  var filename = "";
                  var x = ($("#viewport").get(0).clientWidth / 72).toString();
                  var y = ($("#viewport").get(0).clientHeight / 72).toString();
                  var sizex = x.split(".")[0] +"."+x.split(".")[1].substring(0,1);
                  var sizey = y.split(".")[0] +"."+y.split(".")[1].substring(0,1);
                  var parameters = "{ 'course':'" + course + "', 'sizex':'" + sizex + "', 'sizey':'" + sizey + "'}";
                  $.ajax({
                      type: "Post",
                      url: "Default.aspx/getFileName",
                      data: parameters,
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (data) {
                          htmlobj = $.ajax({ url: "TempFile/Graphviz/" + data.d + ".svg", async: false }).responseText;
                          $("#viewport").html("");
                          $("#knowledgeVideo").html("");
                          $("#knowledgeSR").html("");
                          $("#viewport").append(htmlobj + "");
                          redrawknowledgemap(course);
                          $('#viewport').hideLoading();
                          $('#myknowledge').hideLoading();
                      },
                      error: function (err) {
                          alert(err.responseText);
                      }
                  });
              }

              redrawknowledgemap = function (course)
              {
                  $("g").each(function () {
                      $(this).find("path").attr("id", $(this.childNodes).filter("title")[0].textContent.trim());
                      $(this).find("text").attr("fill", "#fff");
                      $(this).find("text").attr("font-size", "13");
                      $(this.childNodes).filter("title").remove();
                  })
                  $("g.node").mouseover(
                      function () { $(this).find("path").attr("fill", "#4eb3d3"); }).mouseout(
                      function () { $(this).find("path").attr("fill", $(this).find("path").attr("stroke")); }).bind('click dbclick',                              function () {
                      $('#myknowledge').showLoading();
                      GetSearchResult($(this.childNodes).filter("text")[0].textContent.trim(), $(this).find("path").attr("id"), course);
                      GetVideo($(this).find("path").attr("id"));
                  })
              }

              redrawSearchResult = function () {
                  $("table.srdataTable td a").each(function () {
                      $(this).bind('click dbclick', function () {
                          var url = $(this).attr("url") + "";
                          var Description = $(this).attr("description") + "";
                          var mytitle = $(this).attr("mytitle") + "";
                          var str = url.split(",");
                          var pstring = "{'URL':'" + str[2] + "','Title':'" + mytitle + "','Description':'" + Description + "','KnowledgeID':'" + str[0] + "'}";
                          $.ajax({
                              type: "Post",
                              url: "Default.aspx/SaveClickURL",
                              data: pstring,
                              contentType: "application/json; charset=utf-8",
                              dataType: "json",
                              success: function (data) {
                                  str[1] = data.d;
                              },
                              error: function (err) {
                                  alert(err.responseText);
                              }})
                          var w = window.open();
                           setTimeout(function () {
                               w.location = "myShow.aspx?knowledgeid=" + str[0] + "&urlid=" + str[1] + "&url=" + str[2];
                           }, 1000);
                           return false;
                      })
                  })
              }

              redrawSearchVideo = function () {
                  $("table.svdataTable td a").each(function () {
                      $(this).bind('click dbclick', function () {
                          var url = $(this).attr("url") + "";
                          var Description = $(this).attr("description") + "";
                          var mytitle = $(this).attr("mytitle") + "";
                          var str = url.split(",");
                          var pstring = "{'VideoID':'" + str[1] + "'}";
                          $.ajax({
                              type: "Post",
                              url: "Default.aspx/SaveClickVideo",
                              data: pstring,
                              contentType: "application/json; charset=utf-8",
                              dataType: "json",
                              success: function (data) {
                              },
                              error: function (err) {
                                  alert(err.responseText);
                              }
                          })
                          var w = window.open();
                          setTimeout(function () {
                              w.location = "myVideo.aspx?knowledgeid=" + str[0] + "&videoid=" + str[1] + "&url=" + str[2];
                          }, 1000);
                          return false;
                      })
                  })
              }
              GetSearchResult = function (keyword, knowledgeid, course) {
                  $("#knowledgeSR").html("");
                  $.ajax({
                      type: "Post",
                      url: "Default.aspx/GetSearchResult",
                      data: "{'keyword':'" + keyword + "','knowledgeid':'" + knowledgeid + "','course':'" + course + "'}",
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (data) {
                          var htmlobj = "<table border='0' cellspacing='0' cellpadding='0' class='srdataTable'><thead><tr ><th class='dataTableHeader'>相关资源：</th></tr></thead> <tbody>"
                          $.each(data.d, function () {
                              htmlobj = htmlobj + "<tr class=\"row\"><td><a href=\"#\" url=\"" + knowledgeid + "," + this['URLID'] + "," + this['URL'] + "\" description=\"" + this['Description'] + "\" mytitle=\"" + this['Title'] + "\" title=\"" + this['Description'] + "\">" + this['Title'] + "</a></td></tr>"
                          })
                          var htmlobj = htmlobj + "</tbody></table>";
                          $('#knowledgeSR').append(htmlobj);
                          redrawSearchResult();
                          $("table.srdataTable td a").tipTip({ maxWidth: "500px", edgeOffset: 10 });
                          $('#myknowledge').hideLoading();
                      },
                      error: function (err) {
                          alert(err.responseText);
                      }
                  });
              }

              GetVideo = function (knowledgeid) {
                  $('#knowledgeVideo').html("");
                  $.ajax({
                      type: "Post",
                      url: "Default.aspx/GetVideo",
                      data: "{'knowledgeid':'" + knowledgeid + "'}",
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (data) {
                          var htmlobj = "<table border='0' cellspacing='0' cellpadding='0' class='svdataTable'><thead><tr ><th class='dataTableHeader'>相关视频：</th></tr></thead> <tbody>"
                          $.each(data.d, function () {
                              htmlobj = htmlobj + "<tr class=\"row\"><td><a href=\"#\" url=\"" + knowledgeid + "," + this['VideoID'] + "," + this['URL'] + "\" description=\"" + this['Description'] + "\" mytitle=\"" + this['Title'] + "\" title=\"" + this['Description'] + "\">" + this['Title'] + "</a></td></tr>"
                          })
                          var htmlobj = htmlobj + "</tbody></table>";
                          $('#knowledgeVideo').append(htmlobj);
                          redrawSearchVideo();
                          $("table.svdataTable td a").tipTip({ maxWidth: "500px", edgeOffset: 10 });;
                      },
                      error: function (err) {
                          alert(err.responseText);
                      }
                  });
              }

              $(document).ready(function () {
                  $('#viewport').showLoading();
                  $('#myknowledge').showLoading();
                  myShow("math1");
                  $('ul#navigation li a').click(function (e) {
                    $("ul#navigation li").removeClass("selected");
                    $(this).parents().addClass("selected");
                    $('#viewport').showLoading();
                    $('#myknowledge').showLoading();
                    if ($(this).attr('name') + "" == "morecourse")
                    {
                        $("#viewport").html("");
                        $("#viewport").append("<br/>&nbsp;&nbsp;&nbsp;&nbsp;敬请等待！谢谢");
                        $('#viewport').hideLoading();
                        $('#myknowledge').hideLoading();
                    }
                    else {
                        myShow($(this).attr('name'));
                    }
                  });
              })
          })(this.jQuery)
      </script>
    <ul id="navigation">
	<li class="selected"><a  name="math1" href="#" id="math1">小学数学</a></li>
	<li><a href="#" name="math2" id="math2">初中数学</a></li>
	<li><a name="morecourse" href="#">更多课程</a></li>
    </ul>
    <div id="container">
        <div id="viewport" class="leftdiv">

        </div>
        <div id="myknowledge" class="rightdiv">
            <div class="righttopbar">
                 <ul class="navigationleft">
                     <li><a href="<%=this.BasePath%>myWrongCollection.aspx">我的错题</a></li>
                     <li><a href="<%=this.BasePath%>myFavorite.aspx">我的收藏</a></li>
                     <li><a href="<%=this.BasePath%>myNote.aspx">我的笔记</a></li>
                     <li><a href="<%=this.BasePath%>myExercise.aspx">课后习题</a></li>
		         </ul>
            </div>
            <div id="knowledgeVideo"></div>
            <div id="knowledgeSR"></div>
           
        </div>
    </div>
</asp:Content>