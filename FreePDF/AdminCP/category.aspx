<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminCP.Master" AutoEventWireup="true" CodeBehind="category.aspx.cs" Inherits="FreePDF.AdminCP.category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="server">
    <asp:ListView ID="lstCategory" runat="server">
        <LayoutTemplate>
            <table>
                <thead>
                    <tr>
                        <th>STT</th>
                        <th>Tên</th>
                        <th>Alias</th>
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
                <td><%# Eval("Name") %></td>
                <td><%# Eval("Alias") %></td>                
                <td Width="16">
                    <%--<asp:ImageButton ID="btnEdit" ImageUrl="~/theme/admin/edit.png" ToolTip="Sửa bản ghi này" CommandName="EditRow" CommandArgument="0" runat="server" />--%>
                    <input type="button" class="btnEdit" title="Sửa bản ghi này" onclick="window.location.href='edit.aspx?type=3&id=<%# Eval("CategoryID") %>'" />
                </td>
                <td Width="16">
                    <%--<asp:ImageButton ID="btnDelete" ImageUrl="~/theme/admin/remove.png" ToolTip="Xóa bản ghi này" CommandName="DeleteRow" OnClientClick="return confirm('Bạn có chắc muốn xóa bản ghi này?')" runat="server" />--%>
                    <input type="button" class="btnDelete" title="Xóa bản ghi này" onclick="deleteConfirm('category.aspx?removeid=<%# Eval("CategoryID") %>')" />
                </td>
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