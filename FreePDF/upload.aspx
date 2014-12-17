<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/General.Master" AutoEventWireup="true" CodeBehind="upload.aspx.cs" Inherits="FreePDF.upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="style_embed" runat="server">
    <link rel="Stylesheet" type="text/css" href="/theme/css/upload.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentHolder" runat="server">
    <div class="space_double"><!-- --></div>
    <center>    
        <div id="fUpload">                
            <p><span>Tên </span><myControl:RequiredTextBox ID="txtDocumentName" ErrorMessage="Chưa nhập tên tài liệu" runat="server" /></p>						
			<p><span>Thể Loại </span>
                <asp:DropDownList ID="ddlCategory" CssClass="outerglow_textbox" runat="server">                        
                </asp:DropDownList>
			</p>
			<p><span>Thêm Vào Bộ Sưu Tập (BST) </span>
                <asp:DropDownList ID="ddlCollection" CssClass="outerglow_textbox" runat="server">                        
                </asp:DropDownList>
			</p>
            <p><span>Tạo Nhanh BST (Bỏ Trống Mục Này Nếu Thêm Vào BST Có sẵn)</span></p>
            <p><asp:TextBox id="txtCollectionName" CssClass="outerglow_textbox" runat="server"/></p>
            <p><span>Tags (Từ Khóa Dùng Cho Tìm Kiếm)</span></p>
            <p><asp:TextBox ID="txtTags" CssClass="outerglow_textbox" runat="server" /></p>
			<p><span>Mô Tả </span><myControl:RequiredTextBox ID="txtDescription" ErrorMessage="Chưa nhập nội dung mô tả" TextMode="MultiLine" runat="server" /></p>
            <p><span>Ảnh Đại Diện </span><asp:FileUpload ID="thumbUpload" CssClass="outerglow_textbox" runat="server" /></p>
            <p><span>File Tải Lên </span><asp:FileUpload ID="fileUpload" CssClass="outerglow_textbox" runat="server" /></p>				
	        <p class="button_wrapper">
		        <input class="red_button" type="button" id="btnClear" value="Xóa" onclick="clearForm('#fUpload');" />				
		        <asp:Button CssClass="red_button" id="btnUpload" Text="Tải Lên" runat="server" onclick="btnUpload_Click" />								
	        </p>
            <asp:BulletedList ID="blInfo" CssClass="infomsg" ViewStateMode="Disabled" runat="server">
            </asp:BulletedList>                
            <asp:RequiredFieldValidator ID="validtFile" runat="server" ErrorMessage="Chưa chọn file để tải lên" Display="None" ControlToValidate="fileUpload"></asp:RequiredFieldValidator>                                
            <asp:ValidationSummary ID="validtSum_upload" CssClass="errormsg" runat="server" />
        </div>
    </center>
</asp:Content>
