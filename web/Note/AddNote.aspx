<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddNote.aspx.cs" Inherits="Note_AddNote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
  
    <link href="../Content/colorpicker.css" rel="stylesheet" />
    <link href="../Content/literally.css" rel="stylesheet" />
    <link href="../Content/Site.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.8.2.js"></script>
    <script src="../Scripts/underscore-1.4.2.js"></script>
    <script src="../Scripts/Canvas/literallycanvas.fat.js"></script>
    
     <style type="text/css">
      body {
        font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
        margin: 0;
      }

      .fs-container {
        /*position: fixed;*/
        width: 800px;
        height: 500px;
      }

      .literally {
        /*position: absolute;*/
        bottom: 0;
        width: 800px;
        height: 500px;
      }

      /* let's make things a bit bigger */

      .literally .button, .literally .zoom-display {
        line-height: 28px;
        font-size: 18px;
      }

      .literally .toolbar-row-right {
        width: 13em;
      }

      .literally .button {
        padding: 5px 15px 5px 15px;
      }

      .literally .button.color-square {
        padding-top: 10px;
        height: 23px;
      }

      .literally .tools .button {
        padding: 6px 13px 2px 15px;
        line-height: 24px;
      }

      .literally .tools .button img {
        width: 24px;
        height: 24px;
      }

      .literally input[type=range] {
        width: 100px;
        height: 15px;
      }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var oCanvas = document.getElementById("myCanvas");
            $(document).bind('touchmove', function (e) {
                if (e.target === document.documentElement) {
                    return e.preventDefault();
                }
            });

            $('.literally').literallycanvas();

            $('#MainContent_SubmitBtn').bind('click dbclick', function () {
                var shapes = JSON.stringify($('.literally').canvasForExport());
                var pstring = "{'Note':'" + shapes + "'}";
                $.ajax({
                    type: "Post",
                    url: "AddNote.aspx/SaveNote",
                    data: pstring,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d) {
                            alert("保存成功！");
                        }
                    },
                    error: function (err) {
                        alert(err.responseText);
                    }
                });
            });
            $('#MainContent_readBtn').bind('click dbclick', function () {
             
                var pstring = "{}";
                $.ajax({
                    type: "Post",
                    url: "AddNote.aspx/ReadNote",
                    data: pstring,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $('.literally').literallycanvas({ shapes: $.parseJSON(data.d) });
                        //var oCanvasdd = document.getElementById("myCanvas");
                        //var ctx = oCanvasdd.getContext("2d");
                        //document.getElementById("img").src = data.d;
                        //$("#myCanvas").attr("","");
                        //var imga = document.getElementById("img");
                        //ctx.drawImage(imga,0,0);
                    },
                    error: function (err) {
                        alert(err.responseText);
                    }
                });
            });
        });
    </script>
    
    <div id="formRegister" class="form">
  	<h2>我的笔记</h2>
  	<div class="form-row">
	    <div class="label">标 题</div>
	    <div class="input-container">
            <asp:TextBox ID="txtTitle" runat="server" Name="UserName" class="longinput" minlength="3" maxlength="40"></asp:TextBox>&nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" CssClass="tip_right" ErrorMessage="请填写标题。" Display="Dynamic" SetFocusOnError="True" />
</div>
	</div>
        <div class="form-row">
            <div class="fs-container">
   <div class="literally"><canvas id="myCanvas"></canvas></div></div></div>
        <div class="form-row">
	    <div class="label"> <asp:Button ID="readBtn" runat="server" class="sendBtn" Text="读 取"/></div> <div class="input-container">
        <asp:Button ID="SubmitBtn" runat="server" class="sendBtn" Text="提 交"/>
           
            </div>
	</div>
	<div id="errorDiv1" class="error-div">
        <label id="errorMessage" runat="server" ></label>
	</div>
         <br />
    <img id="img" alt="" src="" />
</div>
</asp:Content>

