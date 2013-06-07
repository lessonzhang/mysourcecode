<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="formlogin" class="form">

  	<h2>登 录</h2>
  	<div class="form-row">
	    <div class="label">用户名</div>
	    <div class="input-container"><asp:TextBox runat="server" ID="UserName" Class="input" />                            &nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="请输入用户名." />
            </div>
	</div>
    <div class="form-row">
	    <div class="label">密码</div>
	    <div class="input-container"><asp:TextBox runat="server" ID="Password" TextMode="Password" Class="input"/>
                           &nbsp;&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="请输入密码." />
            </div>
	</div>
	<div class="form-row">
	    <div class="label"><asp:CheckBox runat="server" ID="CheckBox1" />&nbsp;&nbsp;
                            <asp:Label ID="Label2" runat="server" CssClass="checkbox">记住密码？</asp:Label></div>
	    <div class="input-container"><asp:Button ID="SubmitBtn" runat="server" class="sendBtn" Text="登 录" OnClick="Unnamed6_Click" />
            </div>
	</div>
	<div id="errorDiv1" class="error-div">
        <label id="errorMessage" runat="server" visible="false">用户名或密码错误，请重新输入。</label>
	</div>
        <div class="form-row">
	    <div class="label"></div> <div class="input-container">
        <a ID="RegisterHyperLink"  href="<%=this.BasePath%>Account/Register.aspx">注册</a>&nbsp;&nbsp;
            如果您还没有注册.
            </div>
	</div>
        <div class="form-row">
	    <div class="label"></div> <div class="input-container">
        <a ID="GetPasswordHyperLink"  href="<%=this.BasePath%>Account/GetPassword.aspx">忘记密码</a>&nbsp;&nbsp;
            如果您忘记密码了.
            </div>
	</div>
</div>
</asp:Content>