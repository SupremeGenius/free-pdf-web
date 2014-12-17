<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminCP.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="FreePDF.AdminCP.edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="server">
    <div id="inside_form">
    <asp:Panel ID="pnlEditUser" Visible="false" DefaultButton="btnEditUser" runat="server">
        <h3 class="formtitle">Sửa Thông Tin Người Dùng</h3>        
        <p><asp:Label ID="Label1" runat="server" Text="Tài Khoản"></asp:Label><myControl:RequiredTextBox ID="txtUsername" ErrorMessage="Tên người dùng không được bỏ trống" runat="server" /></p>
        <p><asp:Label ID="Label4" runat="server" Text="Email"></asp:Label><myControl:EmailBoxValidate ID="txtEmail" runat="server" /></p>
        <p><asp:Label ID="Label5" runat="server" Text="Mật Khẩu"></asp:Label><myControl:RequiredTextBox ID="txtPassword" ErrorMessage="Mật khẩu không được bỏ trống" TextMode="Password" runat="server" /></p>
        <p><asp:Label ID="Label6" runat="server" Text="Point"></asp:Label><asp:TextBox ID="txtPoint" CssClass="outerglow_textbox" runat="server"></asp:TextBox></p>
        <p><asp:Label ID="Label7" runat="server" Text="Nhóm"></asp:Label><asp:DropDownList ID="ddlGroup" CssClass="outerglow_textbox" runat="server"></asp:DropDownList></p>
        <p class="button">
            <asp:Button ID="btnCancelEditUser" CssClass="red_button" runat="server" Text="Hủy" ValidationGroup="none" onclick="btnCancelEditUser_Click" />
            <asp:Button ID="btnEditUser" CssClass="red_button" runat="server" Text="Lưu" onclick="btnEditUser_Click" />            
        </p>      
    </asp:Panel>
    <asp:Panel ID="pnlEditGroup" Visible="false" DefaultButton="btnEditGroup" runat="server">
        <h3 class="formtitle">Sửa Thông Tin Nhóm</h3>
        <p><asp:Label ID="Label2" runat="server" Text="Tên"></asp:Label><myControl:RequiredTextBox ID="txtName" ErrorMessage="Tên nhóm không được bỏ trống" runat="server" /></p>
        <p><asp:Label ID="Label3" runat="server" Text="Mô Tả"></asp:Label><myControl:RequiredTextBox ID="txtDescription" TextMode="MultiLine" ErrorMessage="Mô tả không được bỏ trống" runat="server" /></p>
        <p><asp:Label ID="Label17" runat="server" Text="Giới Hạn Chức Năng"></asp:Label><asp:CheckBox ID="cbIsLimit" runat="server" /></p>
            <p><asp:Label ID="Label18" runat="server" Text="Giới Hạn Lượt Tải Trong Ngày (0 - Không Giới Hạn)"></asp:Label><myControl:RequiredTextBox ID="txtDownLimit" ErrorMessage="Chưa nhập giới hạn lượt tải" runat="server" /></p>
        <p class="button">
            <asp:Button ID="btnCancelEditGroup" CssClass="red_button" runat="server" Text="Hủy" ValidationGroup="none" onclick="btnCancelEditGroup_Click" />
            <asp:Button ID="btnEditGroup" CssClass="red_button" runat="server" Text="Lưu" onclick="btnEditGroup_Click" />            
        </p>
    </asp:Panel>
    <asp:Panel ID="pnlEditCategory" Visible="false" DefaultButton="btnEditCategory" runat="server">
        <h3 class="formtitle">Sửa Thông Tin Thể Loại</h3>
        <p><asp:Label ID="Label8" runat="server" Text="Tên"></asp:Label><myControl:RequiredTextBox ID="txtCategoryName" ErrorMessage="Tên thể loại không được bỏ trống" runat="server" /></p>        
        <p class="button">
            <asp:Button ID="btnCancelEditCategory" CssClass="red_button" runat="server" Text="Hủy"  ValidationGroup="none" onclick="btnCancelEditCategory_Click" />
            <asp:Button ID="btnEditCategory" CssClass="red_button" runat="server" Text="Lưu" onclick="btnEditCategory_Click" />            
        </p>
    </asp:Panel>
        <asp:Panel ID="pnlEditDocument" Visible="false" DefaultButton="btnEditDocument" runat="server">
        <h3 class="formtitle">Sửa Thông Tin Tài Liệu</h3>
        <p><asp:Label ID="Label9" runat="server" Text="Tên"></asp:Label><myControl:RequiredTextBox ID="txtDocName" ErrorMessage="Tên tài liệu không được bỏ trống" runat="server" /></p>						
		<p><asp:Label ID="Label10" runat="server" Text="Thể Loại"></asp:Label>
            <asp:DropDownList ID="ddlDocCategory" CssClass="outerglow_textbox" runat="server">                        
            </asp:DropDownList>
		</p>
        <p><asp:Label ID="Label11" runat="server" Text="Link"></asp:Label><myControl:RequiredTextBox ID="txtDocLink" ErrorMessage="Link không được bỏ trống" runat="server" /></p>
        <p><asp:Label ID="Label13" runat="server" Text="Lỗi"></asp:Label><asp:CheckBox ID="cbIsError" runat="server" /></p>
        <p><asp:Label ID="Label12" runat="server" Text="Mô Tả"></asp:Label><myControl:RequiredTextBox id="txtDocDescription" TextMode="MultiLine" ErrorMessage="Mô tả không được bỏ trống" runat="server" /></p>      
        <p class="button">
            <asp:Button ID="btnCancelEditDocument" CssClass="red_button" runat="server" Text="Hủy"  ValidationGroup="none" onclick="btnCancelEditDocument_Click"/>
            <asp:Button ID="btnEditDocument" CssClass="red_button" runat="server" Text="Lưu" onclick="btnEditDocument_Click" />            
        </p>
    </asp:Panel>
    <asp:Panel ID="pnlEditCollection" Visible="false" DefaultButton="btnEditCollection" runat="server">
        <h3 class="formtitle">Sửa Thông Tin BST</h3>
        <p><asp:Label ID="Label14" runat="server" Text="Tên"></asp:Label><myControl:RequiredTextBox ID="txtCollectionName" ErrorMessage="Tên BST không được bỏ trống" runat="server" /></p>
        <p><asp:Label ID="Label16" runat="server" Text="Lỗi"></asp:Label><asp:CheckBox ID="cbIsCollectionError" runat="server" /></p>
        <p><asp:Label ID="Label15" runat="server" Text="Mô Tả"></asp:Label><myControl:RequiredTextBox ID="txtCollectionDesc" TextMode="MultiLine" ErrorMessage="Mô tả không được bỏ trống" runat="server" /></p>
        <p class="button">
            <asp:Button ID="btnCancelEditCollection" CssClass="red_button" runat="server" Text="Hủy"  ValidationGroup="none" onclick="btnCancelEditCollection_Click" />
            <asp:Button ID="btnEditCollection" CssClass="red_button" runat="server" Text="Lưu" onclick="btnEditCollection_Click" />
        </p>
    </asp:Panel>
</div>
</asp:Content>