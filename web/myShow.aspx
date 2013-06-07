<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myShow.aspx.cs" Inherits="myShow" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.7.1.js"></script>
     <script type="text/javascript">
    (function ($) {
        $(document).ready(function () {
            $("#Close").click(function (e) { window.close(); });
            $("#Favorite").click(function (e) {
                if (<%=this.CurrentUser.UserID%>+"" == "-1")
                {
                    alert("请您先登录");
                }
                else
                {
                    var pstring = "{'URLID':'" + <%=this.URLID%>  + "','KnowledgeID':'" + <%=this.KnowledgeID%> + "','UserID':'" + <%=this.CurrentUser.UserID%> + "'}";
                    $.ajax({
                        type: "Post",
                        url: "myShow.aspx/SaveFavorite",
                        data: pstring,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data.d)
                            {
                                alert("收藏成功！");
                            }
                        },
                        error: function (err) {
                            alert(err.responseText);
                        } 
                    });
                }
            });

            function fnOnError() {
                return true;
            }
            window.onerror = fnOnError;

        var urlstren = "a.pt?action=Enter&KnowledgeID=<%=this.KnowledgeID%>&urlid=<%=this.URLID%>&userid=<%=this.UserID%>&url=<%=this.URL%>";
        var urlstrex = "a.pt?action=Exit&KnowledgeID=<%=this.KnowledgeID%>&urlid=<%=this.URLID%>&userid=<%=this.UserID%>&url=<%=this.URL%>";
        //window.onload = function () {
        //    $.ajax({
        //        type: "Post",
        //        url: urlstren,
        //        data: "{}",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //        },
        //        error: function (err) {
        //        }
        //    });
        //};
        //window.onbeforeunload = function () {
        //    alert("onbeforeunload");
        //    $.ajax({
        //        type: "Post",
        //        url: urlstrex,
        //        data: "{}",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (data) {
        //        },
        //        error: function (err) {
        //        }
        //    });
        //};
        window.onfocus = function () {
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
        };

        window.onblur = function () {
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
    })
    })(this.jQuery)


         

       </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
    <div class="floatingbar">
         <ul class="navigationleft"> 
             <li><a href="#" id="Close">关闭</a></li>
             <li><a href="#" id="Share">分享</a></li>
             <li><a href="#" id="Favorite">收藏</a></li>
             <li><a href="<%=this.BasePath%>Default.aspx">首页</a></li>
		 </ul>
	</div>
    <div>
        <iframe src="#" width="100%"  height="600px" frameBorder=0 scrolling=yes id="ifm" runat="server"></iframe>
    </div>
            </div>
    </form>
</body>
</html>
