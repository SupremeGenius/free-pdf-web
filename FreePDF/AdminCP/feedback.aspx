<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminCP.Master" AutoEventWireup="true" CodeBehind="feedback.aspx.cs" Inherits="FreePDF.AdminCP.feedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="server">
    <asp:PlaceHolder ID="pnlView" Visible="false" runat="server">    
        <asp:ListView ID="lstFeedback" runat="server">
            <LayoutTemplate>
                <table>
                    <thead>
                        <tr>
                            <th>STT</th>
                            <th>Người Gửi</th>
                            <th>Email</th>
                            <th>Ngày Gửi</th>
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
                    <td><%# Eval("Sender") %></td>
                    <td><%# Eval("Email") %></td>                 
                    <td><%# Eval("FeedbackDate", "{0:dd/MM/yyyy}") %></td>
                    <%--<td Width="16"><asp:ImageButton ID="btnDetails" ImageUrl="~/theme/admin/details.png" ToolTip="Xem chi tiết" CommandName="RowDetails" runat="server" /></td>
                    <td Width="16"><asp:ImageButton ID="btnDelete" ImageUrl="~/theme/admin/remove.png" ToolTip="Xóa bản ghi này" CommandName="DeleteRow" OnClientClick="return confirm('Bạn có chắc muốn xóa bản ghi này?')" runat="server" /></td>--%>
                    <td width="16"><input type="button" class="btnDetails" title="Xem chi tiết" onclick="window.location.href='feedback.aspx?filter=details&id=<%# Eval("FeedbackID") %>'" /></td>
                    <td Width="16"><input type="button" class="btnDelete" title="Xóa bản ghi này" onclick="deleteConfirm('feedback.aspx?removeid=<%# Eval("FeedbackID") %>')" /></td>
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
        <asp:DetailsView ID="dvFeedback" runat="server" AutoGenerateRows="false" Width="600px">        
            <HeaderTemplate>
                <h4 style="text-align: center;">Thông Tin Chi Tiết</h4>
            </HeaderTemplate>
            <Fields>
                <asp:BoundField DataField="Sender" HeaderText="Người Gửi" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="FeedbackDate" HeaderText="Ngày Gửi" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="FeedbackContent" HeaderText="Nội Dung" />
            </Fields>
        </asp:DetailsView>
       <%-- <table style="width: 600px !important">
            <tr>
                <th colspan="2" style="text-align: center">Thông Tin Chi Tiết</th>
            </tr>
            <tr>
                <td><b>Người Gửi</b></td>
                <td><asp:Label ID="lblSender" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Email</b></td>
                <td><asp:Label ID="lblEmail" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Ngày Gửi</b></td>
                <td><asp:Label ID="lblFeedbackDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><b>Nội Dung</b></td>
                <td><asp:Label ID="lblFeedbackContent" runat="server"></asp:Label></td>
            </tr>
        </table>--%>
    </asp:PlaceHolder>
</asp:Content>