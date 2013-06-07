<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MyFriend.aspx.cs" Inherits="Account_MyFriend" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="../Scripts/jquery-1.8.2.js"></script>
    <link href="../Content/SimpleTree.css" rel="stylesheet" />
    <script type="text/javascript" src="js/SimpleTree.js"></script>
    <script src="../Scripts/SimpleTree.js"></script>
<script type="text/javascript">
    (function ($) {
        drawInvites = function () {
            var pstring = "{'userid':'" + <%=this.CurrentUser.UserID%> + "'}";
            $.ajax({
                type: "Post",
                url: "MyFriend.aspx/GetInvite",
                data: pstring,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d[0] != undefined) {
                        if (data.d[0]['MyID'] != undefined) {
                            var chtmlobj = "<table>";
                            $.each(data.d, function () {
                                chtmlobj = chtmlobj + "<tr><td width=\"200px\"><a href=\"#\" ref=\"" + this['MyID'] + "\">" + this['MyName'] + "</a></td><td><input type=\"button\" id=" + this['GroupMemberID'] + " class=\"sendBtn\" ref=\"ok\" value=\"接受邀请\"></input></td><td><input type=\"button\" id=" + this['GroupMemberID'] + " class=\"sendBtn\" ref=\"deny\" value=\"拒绝邀请\"></input></td></tr>"
                            });
                            chtmlobj = chtmlobj + "</table>";
                            $('.invite').html("");
                            $('.invite').append(chtmlobj);
                            $("input.sendBtn").each(function () {
                                $(this).bind('click', function () {
                                    var pstring = "{'GroupMemberID':'" + $(this).attr("ID") + "','type':'" + $(this).attr("ref") + "'}";
                                    $.ajax({
                                        type: "Post",
                                        url: "MyFriend.aspx/DoInvite",
                                        data: pstring,
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        success: function (data) {
                                            drawInvites();
                                            drawFriends();
                                        },
                                        error: function (err) {
                                            alert(err.responseText);
                                        }
                                    })
                                })
                            })
                        } }
                    else {
                            $('.invite').html("");
                            $('.invite').append("您还没有收到邀请。");
                        }
                }, error: function (err) {
                    alert(err.responseText);
                }
            })
        }
        drawFriends = function () {
            var pstring = "{'userid':'" + <%=this.CurrentUser.UserID%> + "'}";
            $.ajax({
                type: "Post",
                url: "MyFriend.aspx/GetGroups",
                data: pstring,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $.each(data.d, function () {
                        var parameters = "{'GroupID':'" + this['GroupID'] + "'}";
                        $.ajax({
                            type: "Post",
                            url: "MyFriend.aspx/GetGroupMembers",
                            data: parameters,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var chtmlobj = "<ul show=\"true\">";
                                $.each(data.d, function () {
                                    chtmlobj = chtmlobj + "<li><a href=\"#\" ref=\"" + this['UserID'] + "\">" + this['UserName'] + "</a></li>"
                                });
                                chtmlobj = chtmlobj + "</ul>";
                                $('.st_tree').html("");
                                $('.st_tree').append(chtmlobj);
                            },
                            error: function (err) {
                                alert(err.responseText);
                            }
                        })
                    })
                }, error: function (err) {
                    alert(err.responseText);
                }
            })
        }
        $(document).ready(function () {
            drawFriends();
            drawInvites();
            })
    })(this.jQuery)
</script>
    <div id="Div1" class="formleft"> 
        <h2>添加好友</h2>
        <div class="form-row">
	    <div class="label">用户名</div>
	    <div class="input-container">
            <asp:TextBox ID="txtUserName" runat="server" Name="UserName" class="input" minlength="3" maxlength="40"></asp:TextBox>&nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" CssClass="tip_right" ErrorMessage="请输入用户名。" Display="Dynamic" SetFocusOnError="True" />
</div></div>
            <div class="form-row">
	    <div class="label"></div> <div class="input-container">
        <asp:Button ID="Button1" runat="server" class="sendBtn" Text="添 加" OnClick="addBtn_Click" />
            </div>
	</div>
        <h2>我的邀请</h2>
        <div class="invite">
        </div>
        <h2>我的好友</h2>
        <div class="st_tree">
        </div>
    </div>
     <div class="formright">
  	 <h2>好友动态</h2>
     </div>
</asp:Content>

