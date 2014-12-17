<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminCP.Master" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="FreePDF.AdminCP.add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="server">
    <div id="inside_form">
        <asp:Panel ID="pnlAddUser" DefaultButton="btnAddpUser" Visible="false" runat="server">
            <h3 class="formtitle">Thêm Người Dùng Mới</h3>            
            <p><asp:Label ID="Label1" runat="server" Text="Tài Khoản"></asp:Label><myControl:RequiredTextBox ID="txtUsername" ErrorMessage="Chưa nhập tên tài khoản" runat="server" /></p>
            <p><asp:Label ID="Label4" runat="server" Text="Email"></asp:Label><myControl:EmailBoxValidate ID="txtEmail" runat="server" /></p>
            <p><asp:Label ID="Label5" runat="server" Text="Mật Khẩu"></asp:Label><myControl:RequiredTextBox ID="txtPassword" ErrorMessage="Chưa nhập mật khẩu" TextMode="Password" runat="server" /></p>
            <p><asp:Label ID="Label7" runat="server" Text="Nhóm"></asp:Label><asp:DropDownList ID="ddlGroup" CssClass="outerglow_textbox" runat="server"></asp:DropDownList></p>
            <p class="button">
                <asp:Button ID="btnCancelAddUser" CssClass="red_button" runat="server" Text="Hủy" ValidationGroup="none" onclick="btnCancelAddUser_Click"/>
                <asp:Button ID="btnAddpUser" CssClass="red_button" runat="server" Text="Thêm" onclick="btnAddpUser_Click" />
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlAddGroup" DefaultButton="btnAddGroup" Visible="false" runat="server">
            <h3 class="formtitle">Thêm Nhóm Người Dùng Mới</h3>
            <p><asp:Label ID="Label2" runat="server" Text="Tên"></asp:Label><myControl:RequiredTextBox ID="txtName" ErrorMessage="Chưa nhập tên nhóm" runat="server" /></p>
            <p><asp:Label ID="Label3" runat="server" Text="Mô Tả"></asp:Label><myControl:RequiredTextBox ID="txtDescription" TextMode="MultiLine" ErrorMessage="Chưa nhập mô tả" runat="server" /></p>
            <p><asp:Label ID="Label6" runat="server" Text="Giới Hạn Chức Năng"></asp:Label><asp:CheckBox ID="cbIsLimit" Checked="true" runat="server" /></p>
            <p><asp:Label ID="Label8" runat="server" Text="Giới Hạn Lượt Tải Trong Ngày (0 - Không Giới Hạn)"></asp:Label><myControl:RequiredTextBox ID="txtDownLimit" ErrorMessage="Chưa nhập giới hạn lượt tải" Text="3" runat="server" /></p>
            <p class="button">
                <asp:Button ID="btnCancelAddGroup" CssClass="red_button" runat="server" Text="Hủy" ValidationGroup="none" onclick="btnCancelAddGroup_Click" />
                <asp:Button ID="btnAddGroup" CssClass="red_button" runat="server" Text="Thêm" onclick="btnAddGroup_Click" />
            </p>
        </asp:Panel>
        <asp:Panel ID="pnlAddCategory" DefaultButton="btnAddCategory" Visible="false" runat="server">
            <h3 class="formtitle">Thêm Thể Loại Mới</h3>
            <p><asp:Label ID="Tên" runat="server" Text="Tên"></asp:Label><myControl:RequiredTextBox ID="txtCategoryName" ErrorMessage="Chưa nhập tên thể loại" runat="server" /></p>            
            <p class="button">
                <asp:Button ID="btnCancelAddCategory" CssClass="red_button" runat="server" Text="Hủy" ValidationGroup="none" onclick="btnCancelAddCategory_Click" />
                <asp:Button ID="btnAddCategory" CssClass="red_button" runat="server" Text="Thêm" onclick="btnAddCategory_Click" />
            </p>
        </asp:Panel>
        <asp:BulletedList ID="blInfo" CssClass="infomsg" ViewStateMode="Disabled" runat="server">
        </asp:BulletedList>
        <asp:ValidationSummary ID="validtSum_add" CssClass="errormsg" runat="server" />
    </div>
</asp:Content>
