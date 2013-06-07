<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Manage.aspx.cs" Inherits="Account_Manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
       <script src="../Scripts/jquery-1.7.1.min.js"></script>
    <script src="../Scripts/formValidator-4.1.1.min.js"></script>
    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                $('#MainContent_ChangPassword').click(function () { })
                $('#MainContent_txtEmail').blur(function (e) {
                    var parameters = "{ 'email':'" + this.value + "','userid':'"+<%=this.CurrentUser.UserID%>+"'}";
                    $.ajax({
                        type: "Post",
                        url: "Manage.aspx/EmailisExist",
                        data: parameters,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data.d) {
                                $("#EmailisExist").attr("style", "");
                            }
                            else {
                                $("#EmailisExist").attr("style", "display:none;");
                            }
                        },
                        error: function (err) {
                            alert(err.responseText);
                        }
                    });
                }).focus(function (e) { $("#EmailisExist").attr("style", "display:none;"); });
            })
        })(this.jQuery)
	</script>
    <div id="formRegister" class="form">
  	<h2>修改账号信息</h2>
  	<div class="form-row">
	    <div class="label">用户名</div>
	    <div class="input-container"><asp:Label ID="lblUserName" runat="server" Text="Label"></asp:Label>
</div>
	</div>
<div class="form-row">
	    <div class="label">姓名</div>
	    <div class="input-container"><asp:TextBox ID="txtName" runat="server" class="input" minlength="3" maxlength="40"></asp:TextBox></div>
	</div>
        <div class="form-row">
	    <div class="label">E-mail</div>
	    <div class="input-container"><asp:TextBox ID="txtEmail" runat="server" Name="Email" class="input" minlength="3" maxlength="80" TextMode="Email" ValidateRequestMode="Enabled"></asp:TextBox>&nbsp;&nbsp;
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email格式不正确。" ControlToValidate="txtEmail" CssClass="tip_right" Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><span id="EmailisExist" class="tip_right" style="display:none;">此Email已经被注册，请您换一个。</span>
            </div>
	</div>
    <div class="form-row">
	    <div class="label">年级</div>
	    <div class="input-container">
            <asp:DropDownList ID="ddlGrade" runat="server"  class="input">
                <asp:ListItem Value="3">小学三年级</asp:ListItem>
                <asp:ListItem Value="4">小学四年级</asp:ListItem>
                <asp:ListItem Value="5">小学五年级</asp:ListItem>
                <asp:ListItem Value="6">小学六年级</asp:ListItem>
                <asp:ListItem Value="7">初中一年级</asp:ListItem>
                <asp:ListItem Value="8">初中二年级</asp:ListItem>
                <asp:ListItem Value="9">初中三年级</asp:ListItem>
            </asp:DropDownList></div>
	</div>
    <div class="form-row">
	    <div class="label">自我介绍</div>
	    <div class="input-container"><asp:TextBox ID="txtNote" runat="server" class="textarea" TextMode="MultiLine" ValidateRequestMode="Enabled"/></div>
	</div>
        <div class="form-row">
	    <div class="label"></div> <div class="input-container">
        <asp:Button ID="SubmitBtn" runat="server" class="sendBtn" Text="提 交" OnClick="SubmitBtn_Click" />
        
            </div>
	</div>
        <div class="form-row">
	    <div class="label"></div> <div class="input-container">
       <asp:Button ID="ChangPassword" runat="server" class="sendBtn" Text="修改密码"/>
            </div>
	</div>
	<div id="errorDiv1" class="error-div">
        <label id="errorMessage" runat="server" ></label>
	</div>
</div>
</asp:Content>

