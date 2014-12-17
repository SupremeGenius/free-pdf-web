<%@ Page Title="Free PDF - Đăng Nhập" Language="C#" MasterPageFile="~/MasterPage/General.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="FreePDF.login" %>
<asp:Content ID="cntStyle" ContentPlaceHolderID="style_embed" runat="server">
    <link rel="Stylesheet" type="text/css" href="/theme/css/login.css" />
</asp:Content>
<asp:Content ID="cntScript" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="cntMain" ContentPlaceHolderID="mainContentHolder" runat="server">
    <div class="space_double"><!-- --></div>
	<center>
    
            <div id="fLogin">                
                <p><span>Tài Khoản </span><mycontrol:RequiredTextBox ID="txtUsername_lg" runat="server" ErrorMessage="Chưa nhập tài khoản" /></p>						
		        <p><span>Mật Khẩu </span><mycontrol:RequiredTextBox ID="txtPassword_lg" runat="server" ErrorMessage="Chưa nhập mật khẩu" TextMode="Password" /></p>		        
		        <p class="button_wrapper">
			        <input class="red_button" type="button" name="btnClear" id="btnClear" value="Xóa" onclick="clearForm('#fLogin');" />				
			        <asp:Button CssClass="red_button" id="btnLogin" Text="Đăng Nhập" runat="server" onclick="btnLogin_Click" />								
		        </p>
                <asp:BulletedList ID="blInfo" CssClass="infomsg" ViewStateMode="Disabled" runat="server">
                </asp:BulletedList>               
                <asp:ValidationSummary ID="validtSum_lg" CssClass="errormsg" runat="server" />

		        <%--<p><span>Tài Khoản </span><asp:TextBox id="txtUsername_lg" CssClass="outerglow_textbox" runat="server" /></p>						
		        <p><span>Mật Khẩu </span><asp:TextBox id="txtPassword_lg" CssClass="outerglow_textbox" TextMode="Password" runat="server" /></p>		        
		        <p class="button_wrapper">
			        <input class="red_button" type="button" name="btnClear" id="btnClear" value="Xóa" onclick="clearForm('#fLogin');" />				
			        <asp:Button CssClass="red_button" id="btnLogin" Text="Đăng Nhập" runat="server" onclick="btnLogin_Click" />								
		        </p>
                <asp:BulletedList ID="blInfo" CssClass="infomsg" runat="server">
                </asp:BulletedList>
                <asp:RequiredFieldValidator ID="validtUsername_lg" runat="server" ErrorMessage="Chưa nhập tài khoản" ControlToValidate="txtUsername_lg" Display="None"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="validtPassword_lg" runat="server" ErrorMessage="Chưa nhập mật khẩu" ControlToValidate="txtPassword_lg" Display="None"></asp:RequiredFieldValidator>                
                <asp:ValidationSummary ID="validtSum_lg" CssClass="errormsg" runat="server" />--%>
	        </div>
        
	</center>
</asp:Content>
