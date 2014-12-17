<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminCP.Master" AutoEventWireup="true" CodeBehind="collection.aspx.cs" Inherits="FreePDF.AdminCP.collection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="server">
    <asp:PlaceHolder ID="pnlView" Visible="false" runat="server">    
        <asp:TextBox ID="txtSearch" CssClass="outerglow_textbox" runat="server"></asp:TextBox>
        Tìm kiếm theo
        <asp:DropDownList ID="ddlSearchType" CssClass="outerglow_textbox" runat="server">
            <asp:ListItem>Tên</asp:ListItem>
            <asp:ListItem>Người Tạo</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnSearch" CssClass="red_button" runat="server" Text="Search" onclick="btnSearch_Click" />
        <div class="space"></div>
        <asp:ListView ID="lstCollection" runat="server">
            <LayoutTemplate>
                <table>
                    <thead>
                        <tr>
                            <th>STT</th>
                            <th>Tên</th>                        
                            <th>Tạo Bởi</th>
                            <th></th>
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
                    <td><%# GetUsername(Convert.ToInt32(Eval("UserID")))%></td>
                    <%--<td Width="16"><asp:ImageButton ID="btnDetails" ImageUrl="~/theme/admin/details.png" ToolTip="Xem chi tiết" CommandName="RowDetails" runat="server" /></td>
                    <td Width="16"><asp:ImageButton ID="btnEdit" ImageUrl="~/theme/admin/edit.png" ToolTip="Sửa bản ghi này" CommandName="EditRow" runat="server" /></td>
                    <td Width="16"><asp:ImageButton ID="btnDelete" ImageUrl="~/theme/admin/remove.png" ToolTip="Xóa bản ghi này" CommandName="DeleteRow" OnClientClick="return confirm('Bạn có chắc muốn xóa bản ghi này?')" runat="server" /></td>--%>
                    <td width="16"><input type="button" class="btnDetails" title="Xem chi tiết" onclick="window.location.href='collection.aspx?filter=details&id=<%# Eval("CollectionID") %>'" /></td>
                    <td width="16"><input type="button" class="btnEdit" title="Sửa bản ghi này" onclick="window.location.href='edit.aspx?type=5&id=<%# Eval("CollectionID") %>'" /></td>
                    <td width="16"><input type="button" class="btnDelete" title="Xóa bản ghi này" onclick="deleteConfirm('collection.aspx?removeid=<%# Eval("CollectionID") %>')" /></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                Không Tìm Thấy Bản Ghi Nào
            </EmptyDataTemplate>
        </asp:ListView>
        <div id="pagination" class="space">
            <asp:PlaceHolder ID="pageHolder" runat="server"></asp:PlaceHolder>
        </div>  
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="pnlDetails" Visible="false" runat="server"> 
        <asp:DetailsView ID="dvCollection" runat="server" AutoGenerateRows="false" Width="600px">
            <HeaderTemplate>
                <h4 style="text-align: center;">Thông Tin Chi Tiết</h4>
            </HeaderTemplate>
            <Fields>
                <asp:BoundField DataField="Name" HeaderText="Tên" />
                <asp:TemplateField HeaderText="Người Tạo">                    
                    <ItemTemplate>
                        <%# GetUsername(Convert.ToInt32(Eval("UserID"))) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TotalView" HeaderText="Lượt Xem" />
                <asp:BoundField DataField="CreatedDate" HeaderText="Ngày Tạo" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:CheckBoxField DataField="IsError" HeaderText="Lỗi" />
                <asp:BoundField DataField="Description" HeaderText="Mô Tả" />
            </Fields>
        </asp:DetailsView>
        <%--<table style="width: 600px !important">
            <tr>
                <th colspan="2" style="text-align: center">Thông Tin Chi Tiết</th>
            </tr>
            <tr>
                <td><b>Tên</b></td>
                <td><asp:Label ID="lblName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Người Tạo</b></td>
                <td><asp:Label ID="lblSender" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Lượt Xem</b></td>
                <td><asp:Label ID="lblTotalView" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Ngày Tạo</b></td>
                <td><asp:Label ID="lblCreatedDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Lỗi</b></td>
                <td><asp:CheckBox ID="cbIsError" Enabled="false" runat="server" /></td>
            </tr>
            <tr>
                <td><b>Mô Tả</b></td>
                <td><asp:Label ID="lblDescription" runat="server"></asp:Label></td>
            </tr>
        </table>--%>
    </asp:PlaceHolder>
</asp:Content>