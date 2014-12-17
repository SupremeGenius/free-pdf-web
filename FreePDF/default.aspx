<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/General.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FreePDF._default" %>
<asp:Content ID="Content2" ContentPlaceHolderID="style_embed" runat="server">    
    <link rel="Stylesheet" type="text/css" href="theme/css/home.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="js_embed" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentHolder" runat="server">
    <asp:UpdatePanel ID="pnlUpdatePage" runat="server">
        <ContentTemplate>
            <div id="lastest_doc">            
            <asp:ListView ID="lstDocument" runat="server">
                <ItemTemplate>
                    <div class="lastest_doc-item">
                        <div class="doc_thumb"><img src="<%= GetThumbnailsPath() %>/<%# Eval("Thumbnails") %>" alt="<%# Eval("Name") %>"/></div>
	                    <div class="doc_info">
		                    <h2 class="doc_title"><a href="/TaiLieu/<%# Eval("DocumentID") %>-<%# Eval("Alias") %>"><%# Eval("Name") %></a></h2>
		                    <%--<p class="doc_pagenumber"><b>Số Trang: </b>35</p>--%>								
		                    <p class="doc_uploadby"><b>Người Gửi: </b><a href=""><%# GetUsername(Convert.ToInt32(Eval("UserID"))) %></a></p>
		                    <p class="doc_uploaddate"><b>Ngày Đăng: </b><%# Eval("UploadDate", "{0:dd/MM/yyyy}") %></p>
                            <p class="doc_category"><b>Thể Loại: </b><%# GetCategoryName(Convert.ToInt32(Eval("CategoryID"))) %></p>
		                    <%--<p class="doc_tags">                                
                            </p>--%>
		                    <p class="doc_details">
			                    <%# Eval("Description") %>
		                    </p>
	                    </div>
                    </div> <!-- END: DIV#LASTEST_DOC-ITEM -->
                    <div class="breakline"><!-- --></div>
                </ItemTemplate>
                <EmptyItemTemplate>
                    Dữ Liệu Trống
                </EmptyItemTemplate>
            </asp:ListView>
            </div> <!-- END: DIV#LASTEST_DOC -->
            <div id="pagination" class="space">
                <myControl:DataPaging ID="dataPaging" PageSize="5" runat="server"></myControl:DataPaging>
            </div> <!-- END: DIV#PAGINATION -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>