<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="FreePDF.test" Trace="True" TraceMode="SortByCategory" %>
<%@ Register TagName="DataPaging" TagPrefix="myCtr" Src="~/UserControl/DataPaging.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        @import "theme/css/fonts.css";
        @import "theme/css/custom.css";
        @import "theme/css/home.css";
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="Button" />
        <%--<asp:GridView ID="GridView1" DataSourceID="srcData" runat="server" PageSize="5">           
        </asp:GridView>--%>
        <asp:ListView ID="lstTest" runat="server">
            <LayoutTemplate>
                <ol>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                </ol>                
            </LayoutTemplate>
            <ItemTemplate>
                <li><%# Eval("SalesOrderID") %> - <%# Eval("UnitPrice") %></li>
            </ItemTemplate>
        </asp:ListView>
<%--        <div id="pagination" class="space">
            <ul>
                <li><a href="?page=1">First Page</a></li>
                <li><a href="?page=<%= PageNumber - 1 %>">Previous</a></li>            
                <%
                    foreach (int i in PageList(PageNumber))
                    {
                        if(i == PageNumber)
                            Response.Write(String.Format("<li><a class='activepage' href='?page={0}'>{0}</a></li>", i));
                        else                            
                            Response.Write(String.Format("<li><a href='?page={0}'>{0}</a></li>", i));
                    }
                %>
                <li><a href="?page=<%= PageNumber + 1 %>">Next</a></li>
                <li><a href="?page=<%= LastPageNumber %>">Last Page</a></li>
            </ul>
        </div>--%>    
        <div id="pagination" class="space">
            <myCtr:DataPaging ID="dataPaging" PageSize="5" runat="server"></myCtr:DataPaging>
        </div>
    
        <%--<asp:DataPager ID="DataPager1" PagedControlID="ListView1" PageSize="3" runat="server">
            <Fields>
                <asp:NextPreviousPagerField ShowFirstPageButton="false" ShowPreviousPageButton="true" ShowNextPageButton="false" ShowLastPageButton="false" FirstPageText="Trang Đầu" LastPageText="Trang Cuối" NextPageText="Trang Kế »" PreviousPageText="« Trang Trước" />
                <asp:NumericPagerField CurrentPageLabelCssClass="activepage" />
                <asp:NextPreviousPagerField ShowFirstPageButton="false" ShowPreviousPageButton="false" ShowNextPageButton="true" ShowLastPageButton="false" FirstPageText="Trang Đầu" LastPageText="Trang Cuối" NextPageText="Trang Kế »" PreviousPageText="« Trang Trước" />
            </Fields>
        </asp:DataPager>
        <asp:ObjectDataSource ID="srcData" TypeName="BusinessLogicLayer.DocumentLINQ" SelectCountMethod="DocumentCount" SelectMethod="GetDocumentList" EnablePaging="true" EnableCaching="true" runat="server">
        </asp:ObjectDataSource>--%>
    </form>
</body>
</html>