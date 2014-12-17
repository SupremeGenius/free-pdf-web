<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminCP.Master" AutoEventWireup="true" CodeBehind="usergroup.aspx.cs" Inherits="FreePDF.AdminCP.usergroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="server">
    <asp:GridView ID="grvGroupList" runat="server" AutoGenerateColumns="False" DataKeyNames="GroupID" onrowcommand="grvGroupList_RowCommand">
        <Columns>
            <asp:BoundField DataField="GroupID" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Tên" />
            <asp:BoundField DataField="Description" HeaderText="Mô Tả" />
            <asp:CheckBoxField DataField="IsDefault" HeaderText="Nhóm Mặc Định" />
            <asp:CheckBoxField DataField="IsAdmin" HeaderText="Nhóm Quản Trị" />
            <asp:CheckBoxField DataField="IsLimit" HeaderText="Bị Giới Hạn" />
            <asp:BoundField DataField="DownloadLimit" HeaderText="Lượt Tải Giới Hạn" />
            <asp:ButtonField ButtonType="Image" CommandName="EditRow" Text="Edit" ImageUrl="~/theme/admin/edit.png" >
            <ControlStyle CssClass="btnEdit" />
            <HeaderStyle Width="16px" />
            </asp:ButtonField>
            <asp:ButtonField CommandName="DeleteRow" Text="Delete" ButtonType="Image" ImageUrl="~/theme/admin/remove.png" >
            <ControlStyle CssClass="btnDelete" />
            <HeaderStyle Width="16px" />
            </asp:ButtonField>
        </Columns>
    </asp:GridView>
</asp:Content>