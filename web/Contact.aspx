<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="formContact" class="form">

  	<h2>联系我们</h2>
  	<div class="form-row">
	    <div class="label">电话</div>
	    <div class="input-container">13************
            </div>
	</div>
    <div class="form-row">
	    <div class="label">Email</div>
	    <div class="input-container">
            <span><a href="mailto:l********@hotmail.com">********@hotmail.com</a></span><BR/>
            <span><a href="mailto:********@gmail.com">********@gmail.com</a></span>
            </div>
	</div>
	<div class="form-row">
	    <div class="label">QQ</div>
	    <div class="input-container">
            ***********</div>
	</div>
</div>
</asp:Content>