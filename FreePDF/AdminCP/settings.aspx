<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminCP.Master" AutoEventWireup="true" CodeBehind="settings.aspx.cs" Inherits="FreePDF.AdminCP.settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="js_embed" runat="server">
    <style type="text/css">
        table tbody tr td input, table tbody tr td select
        {
            width: 95%;
        }
        
        table tbody tr td input[type=checkbox]
        {
            width: auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentHolder" runat="server">
    <table>
        <tbody>
            <tr>
                <th colspan="2">Thiết Lập Nhóm</th>
                <th></th>
            </tr>
            <tr>
                <td>Nhóm Mặc Định Khi Đăng Kí Thành Viên</td>
                <td><asp:DropDownList ID="ddlDefaultGroup" CssClass="outerglow_textbox" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Nhóm Quản Trị</td>
                <td><asp:DropDownList ID="ddlAdminGroup" CssClass="outerglow_textbox" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <th colspan="2">Thiết Lập Hệ Thống</th>
                <th></th>
            </tr>
            <tr>
                <td>File Server</td>
                <td><asp:TextBox ID="txtFileServer" CssClass="outerglow_textbox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Nơi Chứa Ảnh Đại Diện</td>
                <td><asp:TextBox ID="txtThumbPath" CssClass="outerglow_textbox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Webmaster Email</td>
                <td><asp:TextBox ID="txtWebmasterEmail" CssClass="outerglow_textbox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <th colspan="2">Thiết Lập Các Module (Trang Chủ)</th>
                <th></th>
            </tr>
            <tr>
                <td>Module Tìm Kiếm</td>
                <td><asp:CheckBox ID="cbSearchBoxModule" runat="server" /></td>
            </tr>
            <tr>
                <td>Module Đăng Nhập</td>
                <td><asp:CheckBox ID="cbLoginBoxModule" runat="server" /></td>
            </tr>
            <tr>
                <td>Module Thống Kế Xem Nhiều - Đánh Giá Cao</td>
                <td>Hiệu Lực&nbsp;<asp:CheckBox ID="cbMostViewRateBoxModule" runat="server" />&nbsp;Số Lượng Tài Liệu Hiển Thị&nbsp;<asp:TextBox Width="10%" ID="txtMostViewRateBoxModuleAmount" CssClass="outerglow_textbox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Module Tài Liệu Ngẫu Nhiên - Tải Nhiều</td>
                <td>Hiệu Lực&nbsp;<asp:CheckBox ID="cbRandomDownloadBoxModule" runat="server" />&nbsp;Số Lượng Tài Liệu Hiển Thị&nbsp;<asp:TextBox Width="10%" ID="txtRandomDownloadBoxModuleAmount" CssClass="outerglow_textbox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Module Thống Kê Thể Loại</td>
                <td><asp:CheckBox ID="cbCategoryModule" runat="server" /></td>
            </tr>            
            <tr>
                <td></td>
                <td></td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="2"><asp:Button ID="btnSave" CssClass="red_button" runat="server" Text="Lưu Thiết Lập" onclick="btnSave_Click" /></td>
                <td></td>
            </tr>
        </tfoot>        
    </table>
</asp:Content>
