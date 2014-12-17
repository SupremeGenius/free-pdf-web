<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminCP.Master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="FreePDF.AdminCP.users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="server">    
    <asp:TextBox ID="txtSearch" CssClass="outerglow_textbox" runat="server"></asp:TextBox>
    Tìm kiếm theo
    <asp:DropDownList ID="ddlSearchType" CssClass="outerglow_textbox" runat="server">
        <asp:ListItem>Tài Khoản</asp:ListItem>
        <asp:ListItem>Email</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="btnSearch" CssClass="red_button" runat="server" Text="Search" onclick="btnSearch_Click" />
    <div class="space"></div>
    <asp:ListView ID="lstUser" runat="server">
        <LayoutTemplate>
            <table>
                <thead>
                    <tr>
                        <th>STT</th>
                        <th>Username</th>
                        <th>Email</th>
                        <th>Nhóm</th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td><%# ++OrderNo %></td>
                <td><%# Eval("Username") %></td>
                <td><%# Eval("Email") %></td>
                <td><%# GetGroupName(Int32.Parse(Eval("GroupID").ToString()))%></td>
                <td Width="16"><input type="button" class="btnEdit" title="Sửa bản ghi này" onclick="window.location.href='edit.aspx?type=1&id=<%# Eval("UserID") %>'" /></td>
                <td Width="16"><input type="button" class="btnDelete" title="Xóa bản ghi này" onclick="deleteConfirm('category.aspx?removeid=<%# Eval("UserID") %>')" /></td>
                <%--<td Width="16"><asp:ImageButton ID="btnEdit" ImageUrl="~/theme/admin/edit.png" ToolTip="Sửa bản ghi này" CommandName="EditRow" CommandArgument="0" runat="server" /></td>
                <td Width="16"><asp:ImageButton ID="btnDelete" ImageUrl="~/theme/admin/remove.png" ToolTip="Xóa bản ghi này" CommandName="DeleteRow" OnClientClick="return confirm('Bạn có chắc muốn xóa bản ghi này?')" runat="server" /></td>--%>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            Không Tìm Thấy Bản Ghi Nào
        </EmptyDataTemplate>
    </asp:ListView>
    <div id="pagination" class="space">
        <asp:PlaceHolder ID="pageHolder" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>