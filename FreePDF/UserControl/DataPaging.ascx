<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataPaging.ascx.cs" Inherits="FreePDF.UserControl.DataPaging" %>
<div>    
    <asp:LinkButton ID="lnkFirstPage" runat="server" onclick="lnkFirstPage_Click">Trang Đầu</asp:LinkButton>    
    <asp:LinkButton ID="lnkPreviousPage" runat="server" onclick="lnkPreviousPage_Click">« Trang Trước</asp:LinkButton>
    <!-- Page List -->
    <asp:PlaceHolder ID="pageNumberHolder" runat="server"></asp:PlaceHolder>
    <!-- END: Page List -->
    <asp:LinkButton ID="lnkNextPage" runat="server" onclick="lnkNextPage_Click">Trang Kế »</asp:LinkButton>
    <asp:LinkButton ID="lnkLastPage" runat="server" onclick="lnkLastPage_Click">Trang Cuối</asp:LinkButton>
</div>