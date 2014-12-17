<%@ Page Title="Free PDF - Đăng Kí Tài Khoản" Language="C#" MasterPageFile="~/MasterPage/General.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="FreePDF.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="style_embed" runat="server">
    <link rel="Stylesheet" type="text/css" href="/theme/css/register.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentHolder" runat="server">
    <div class="space_double"><!-- --></div>					
    <center>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>           
            <div id="fRegister">                
                <p><span>Tài Khoản </span><myControl:RequiredTextBox ID="txtUsername_reg" ErrorMessage="Chưa nhập tài khoản" runat="server" /></p>
	            <p><span>Email </span><myControl:EmailBoxValidate ID="txtEmail_reg" runat="server" /></p>
	            <p><span>Mật Khẩu </span><asp:TextBox id="txtPassword_reg" CssClass="outerglow_textbox" TextMode="Password" runat="server"/></p>
	            <p><span>Xác Nhận Lại </span><asp:TextBox id="txtConfirmPassword_reg" CssClass="outerglow_textbox" TextMode="Password" runat="server"/></p>
	            <p class="button_wrapper">
		            <input class="red_button" type="button" id="btnClear" value="Xóa" onclick="clearForm('#fRegister');" />				
		            <asp:Button CssClass="red_button" id="btnRegister" Text="Đăng Kí" runat="server" onclick="btnRegister_Click" />
	            </p>
                <asp:BulletedList ID="blInfo" CssClass="infomsg" ViewStateMode="Disabled" runat="server">
                </asp:BulletedList>                
                <asp:RequiredFieldValidator ID="validtPassword_reg" runat="server" ErrorMessage="Chưa nhập mật khẩu"  ControlToValidate="txtPassword_reg" Display="None"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="validtConfirmPassword" runat="server" ErrorMessage="Chưa nhập mật khẩu xác nhận"  ControlToValidate="txtConfirmPassword_reg" Display="None"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="validtComparePass" runat="server" ErrorMessage="Mật khẩu không khớp nhau"  ControlToValidate="txtConfirmPassword_reg" ControlToCompare="txtPassword_reg" Display="None"></asp:CompareValidator>
                <asp:ValidationSummary ID="validtSum_reg" CssClass="errormsg" runat="server" />

	            <%--<p><span>Tài Khoản </span><asp:TextBox id="txtUsername_reg" CssClass="outerglow_textbox" runat="server"/></p>
	            <p><span>Email </span><asp:TextBox id="txtEmail_reg" CssClass="outerglow_textbox" runat="server"/></p>
	            <p><span>Mật Khẩu </span><asp:TextBox id="txtPassword_reg" CssClass="outerglow_textbox" TextMode="Password" runat="server"/></p>
	            <p><span>Xác Nhận Lại </span><asp:TextBox id="txtConfirmPassword_reg" CssClass="outerglow_textbox" TextMode="Password" runat="server"/></p>
	            <p class="button_wrapper">
		            <input class="red_button" type="button" id="btnClear" value="Xóa" onclick="clearForm('#fRegister');" />				
		            <asp:Button CssClass="red_button" id="btnRegister" Text="Đăng Kí" runat="server" onclick="btnRegister_Click" />
	            </p>
                <asp:BulletedList ID="blInfo" CssClass="infomsg" runat="server">
                </asp:BulletedList>
                <asp:RequiredFieldValidator ID="validtUsername_reg" runat="server" ErrorMessage="Chưa nhập tài khoản" ControlToValidate="txtUsername_reg" Display="None"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="validtEmail" runat="server" ErrorMessage="Chưa nhập Email"  ControlToValidate="txtEmail_reg" Display="None"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="validEmailFormat" runat="server" 
                    ErrorMessage="Email sai định dạng" ControlToValidate="txtEmail_reg" Display="None" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="validtPassword_reg" runat="server" ErrorMessage="Chưa nhập mật khẩu"  ControlToValidate="txtPassword_reg" Display="None"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="validtConfirmPassword" runat="server" ErrorMessage="Chưa nhập mật khẩu xác nhận"  ControlToValidate="txtConfirmPassword_reg" Display="None"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="validtComparePass" runat="server" ErrorMessage="Mật khẩu không khớp nhau"  ControlToValidate="txtConfirmPassword_reg" ControlToCompare="txtPassword_reg" Display="None"></asp:CompareValidator>
                <asp:ValidationSummary ID="validtSum_reg" CssClass="errormsg" runat="server" />--%>
            </div>                      
        </ContentTemplate>
    </asp:UpdatePanel>    					
    </center>		
</asp:Content>
