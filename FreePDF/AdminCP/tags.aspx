<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminCP.Master" AutoEventWireup="true" CodeBehind="tags.aspx.cs" Inherits="FreePDF.AdminCP.tags" %>
<asp:Content ID="Content1" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="server">
    <asp:ListView ID="lstTags" runat="server">
        <LayoutTemplate>
            <table>
                <thead>
                    <tr>
                        <th>STT</th>
                        <th>Tên Thẻ</th>
                        <th>Alias</th>                    
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
                <td><%# Eval("TagName") %></td>
                <td><%# Eval("Alias") %></td>              
                <td Width="16"><input type="button" class="btnDelete" title="Xóa bản ghi này" onclick="deleteConfirm('tags.aspx?removeid=<%# Eval("TagID") %>')" /></td>
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
