<%@ Page Title="Free PDF - Liên Hệ" Language="C#" MasterPageFile="~/MasterPage/General.Master" AutoEventWireup="true" CodeBehind="contactus.aspx.cs" Inherits="FreePDF.contactus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="style_embed" runat="server">
    <link rel="Stylesheet" type="text/css" href="theme/css/contactus.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentHolder" runat="server">
    <div class="space_double"><!-- --></div>
    <center>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="fContactUs">
	            <p><span>Tên </span><myControl:RequiredTextBox ID="txtFullName" ErrorMessage="Chưa nhập tên" runat="server" /></p>                
	            <p><span>Email </span><myControl:EmailBoxValidate ID="txtEmail" runat="server" /></p>
	            <p><myControl:RequiredTextBox ID="txtMessage" TextMode="MultiLine" ErrorMessage="Chưa nhập nội dung" runat="server" /></p>                
	            <p class="button_wrapper">
		            <input class="red_button" type="button" name="btnClear" id="btnClear" value="Xóa" onclick="clearForm('#fContactUs');" />				
		            <asp:Button CssClass="red_button" id="btnSend" Text="Gửi Đi" runat="server" onclick="btnSend_Click" />								
	            </p>
                <asp:BulletedList ID="blInfo" CssClass="infomsg" ViewStateMode="Disabled" runat="server">
                </asp:BulletedList>                
                <asp:ValidationSummary ID="validtSum_contact" CssClass="errormsg" runat="server" />        
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>    
    </center>
</asp:Content>
