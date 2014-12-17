<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminCP.Master" AutoEventWireup="true" CodeBehind="document.aspx.cs" Inherits="FreePDF.AdminCP.document" %>
<asp:Content ID="Content1" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="server">    
    <asp:PlaceHolder ID="pnlView" Visible="false" runat="server">    
        <asp:TextBox ID="txtSearch" CssClass="outerglow_textbox" runat="server"></asp:TextBox>
        Tìm kiếm theo
        <asp:DropDownList ID="ddlSearchType" CssClass="outerglow_textbox" runat="server">
            <asp:ListItem>Tên</asp:ListItem>
            <asp:ListItem>Người Gửi</asp:ListItem> 
            <asp:ListItem>BST</asp:ListItem>       
        </asp:DropDownList>
        <asp:Button ID="btnSearch" CssClass="red_button" runat="server" Text="Tìm" onclick="btnSearch_Click" />
        <b>HOẶC</b> Lọc theo thể loại
        <asp:DropDownList ID="ddlFilter" CssClass="outerglow_textbox" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnFilter" CssClass="red_button" runat="server" Text="Lọc" onclick="btnFilter_Click" />
        <div class="space"><!-- --></div>
        <asp:ListView ID="lstDocument" runat="server">
            <LayoutTemplate>
                <table>
                    <thead>
                        <tr>
                        <th>STT</th>
                        <th>Tên</th>
                        <th>Link</th>
                        <th>Ngày Ðang</th>
                        <th>Gửi Bởi</th>
                        <th>Thể Loại</th>
                        <th>BST</th>
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
                <td><%# ++OrderNo %></td>
                <td><%# Eval("Name") %></td>
                <td><%# Eval("Link") %></td>
                <td><%# Eval("UploadDate", "{0:dd/MM/yyyy}") %></td>
                <td><%# GetUsername(Convert.ToInt32(Eval("UserID"))) %></td>
                <td><%# GetCategoryName(Convert.ToInt32(Eval("CategoryID")))%></td>
                <td><%# Eval("CollectionID") %></td>
                <td width="16"><input type="button" class="btnDetails" title="Xem chi tiết" onclick="window.location.href='document.aspx?filter=details&id=<%# Eval("DocumentID") %>'" /></td>
                <td width="16"><input type="button" class="btnEdit" title="Sửa bản ghi này" onclick="window.location.href='edit.aspx?type=4&id=<%# Eval("DocumentID") %>'" /></td>
                <td width="16"><input type="button" class="btnDelete" title="Xóa bản ghi này" onclick="deleteConfirm('document.aspx?removeid=<%# Eval("DocumentID") %>')" /></td>
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
        <asp:DetailsView ID="dvDocument" runat="server" AutoGenerateRows="false" Width="600px">
            <HeaderTemplate>
                <h4 style="text-align: center;">
                    Thông Tin Chi Tiết</h4>
            </HeaderTemplate>
            <Fields>
                <asp:BoundField DataField="Name" HeaderText="Tên" />
                <asp:BoundField DataField="Link" HeaderText="Link" />
                <asp:BoundField DataField="FileSize" HeaderText="Dung Lượng" />
                <asp:TemplateField HeaderText="Người Gửi">
                    <ItemTemplate>
                        <%# GetUsername(Convert.ToInt32(Eval("UserID")))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Thê Loại">
                    <ItemTemplate>
                        <%# GetCategoryName(Convert.ToInt32(Eval("CategoryID")))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BST">
                    <ItemTemplate>
                        <%# Convert.ToInt32(Eval("CollectionID")) == 0 ? "N/A" : GetCollectionName(Convert.ToInt32(Eval("CollectionID")))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TotalView" HeaderText="Lượt Xem" />
                <asp:BoundField DataField="TotalDownload" HeaderText="Lượt Tải" />
                <asp:BoundField DataField="UploadDate" DataFormatString="{0:dd/MM/yyyy}" 
                    HeaderText="Ngày Ðăng" />
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
                <td><b>Link</b></td>
                <td><asp:Label ID="lblLink" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Dung Lượng</b></td>
                <td><asp:Label ID="lblFileSize" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Người Gửi</b></td>
                <td><asp:Label ID="lblSender" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Thể Loại</b></td>
                <td><asp:Label ID="lblCategory" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>BST</b></td>
                <td><asp:Label ID="lblCollection" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Lượt Xem</b></td>
                <td><asp:Label ID="lblTotalView" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Lượt Tải</b></td>
                <td><asp:Label ID="lblTotalDownload" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Ngày Ðăng</b></td>
                <td><asp:Label ID="lblUploadDate" runat="server"></asp:Label></td>
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