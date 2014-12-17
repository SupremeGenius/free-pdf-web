<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/General.Master" AutoEventWireup="true" CodeBehind="document.aspx.cs" Inherits="FreePDF.document" %>
<asp:Content ID="Content1" ContentPlaceHolderID="style_embed" runat="server">
    <link href="/js/rating/jquery.rating.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .doc_title {
            text-transform: capitalize;
            font-style: italic;            
        }
        
            .doc_title a {
                color: #666;
            }
        
            .doc_title a:hover {
                color: #999;
                text-decoration: none;
            }
        
        .doc_info .doc_thumb {
            text-align: center;
			background: transparent url('/theme/doc_thumb.png') center center no-repeat;
			padding: 7px;
			width: 155px; height: 185px;
			float: left;
        }
        
        .doc_info .doc_thumb img {
            width: 141px; height: 171px;
        }
        
        .doc_info .details_info {
            margin-left: 160px;            
        }
        
        #doc_toolbar li {
            list-style: none inside;
            float: left;
        }
        
        #comment_list li {
            list-style: none;                       
        }
        
        #comment_list li .comment {
			min-height:  50px;
			margin: 5px 0;
			border-color: #FF9C00 #FF9C00 #FF9C00 #FEAA4B;
            border-style: dashed dashed dashed solid;
            border-width: 1px 1px 1px 5px;
        }
        
        #comment_list li .comment_content {
            padding: 10px;
            padding-left: 35px;
            background: transparent url('/theme/quotemark.png') 5px 5px no-repeat;
        }
        
        #comment_list li .author_name {
            background: #FFFBC3;
            padding: 5px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="js_embed" runat="server">
    <script src="/js/rating/jquery.form.js" type="text/javascript"></script>
    <script src="/js/rating/jquery.MetaData.js" type="text/javascript"></script>
    <script src="/js/rating/jquery.rating.js" type="text/javascript"></script>
    <script type="text/javascript">
        function getRateValue(rateValue) {
            $('#mainContentHolder_hidden_rateValue').val(rateValue);
            //var rate = $('#mainContentHolder_hidden_rateValue').val();
            var userID = $('#hidden_UserID').val();
            var docID = $('#hidden_DocID').val();
            var param = "{DocumentID:" + docID + ",UserID:" + userID + ",RateValue:" + rateValue + "}";

            $.ajax({
                type: 'POST',
                url: '/WebServices/RatingServices.asmx/SetRateValue',
                contentType: 'application/json; charset=utf-8',
                data: param,
                dataType: 'json',
                success: function () {
                }
            });
        }
    </script>
    <%--<script src="js/rating/jquery.rating.pack.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentHolder" runat="server">
    <asp:ListView ID="DocInfo" runat="server">
        <ItemTemplate>
            <h2 class="doc_title"><a href="#"><%# Eval("Name") %></a></h2>
            <div class="space"></div>
            <div class="doc_info">
                <div class="doc_thumb">
                    <img src="<%= GetThumbnailsPath() %>/<%# Eval("Thumbnails") %>" alt="<%# Eval("Name") %>" />
                </div>
                <ul class="details_info">
                    <li><b>Người Gửi:</b> <a href="#"><%# GetUsername(Convert.ToInt32(Eval("UserID"))) %></a></li>
                    <li><b>Lượt Xem:</b> <%# Eval("TotalView") %></li>
                    <li><b>Lượt Tải:</b> <%# Eval("TotalDownload") %></li>
                    <li><b>Ngày Upload:</b> <%# Eval("UploadDate", "{0:dd/MM/yyyy}") %></li>
                    <li><b>Dung Lượng:</b> <%# Math.Round(Convert.ToDouble(Eval("FileSize")) / (1024 * 1024), 2) %> MB</li>
                    <li><b>Thuộc BST:</b> <%# GetCollectionName(Convert.ToInt32(Eval("CollectionID"))) %></li>
                    <li><b>Thể Loại:</b> <%# GetCategoryName(Convert.ToInt32(Eval("CategoryID"))) %></li>
                    <li><b>Đánh Giá</b><br />
                        <input name="star1" type="radio" class="star"/>
                        <input name="star1" type="radio" class="star"/>
                        <input name="star1" type="radio" class="star"/>
                        <input name="star1" type="radio" class="star"/>
                        <input name="star1" type="radio" class="star"/>
                        <asp:HiddenField ID="hidden_rateValue" runat="server" />             
                    </li>
                    <input id="hidden_UserID" type="hidden" value="<%# Eval("UserID") %>" />
                    <input id="hidden_DocID" type="hidden" value="<%# Eval("DocumentID") %>" />
                </ul>
            </div>
            <div class="space clr"></div>
            <p>Link Chia Sẻ: <asp:TextBox ID="txtURL" CssClass="outerglow_textbox" Width="50%" Text='<%# Context.Request.Url.OriginalString %>' runat="server" /></p>            
        </ItemTemplate>
    </asp:ListView>
    <div class="space"></div>
    <ul id="doc_toolbar">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li><asp:Button ID="btnDownload" CssClass="red_button" Text="Tải Về" runat="server" onclick="btnDownload_Click" /></li>
                <li><asp:Button ID="btnReportError" CssClass="red_button" Text="Báo Lỗi" runat="server" onclick="btnReportError_Click" /></li>        
                <li><asp:Button ID="btnAddToCollection" CssClass="red_button" Text="Thêm Vào BST" runat="server" /></li>
                <asp:BulletedList ID="blMessage" style="padding: 2px; border: none; clear: both;" CssClass="infomsg" ViewStateMode="Disabled" runat="server">
                </asp:BulletedList>
            </ContentTemplate>
        </asp:UpdatePanel>                
    </ul>
    <div class="space clr"></div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <h4>Bình Luận</h4>
            <% if (Session["Username"] != null)
               { %>
                <p><asp:TextBox ID="txtCommentContent" CssClass="outerglow_textbox" style="width: 70%; height: 110px;" runat="server" TextMode="MultiLine" /></p>
                <p><asp:Button ID="btnAddComment" CssClass="red_button" Text="Thêm Bình Luận" runat="server" onclick="btnAddComment_Click" /></p>
            <% }
               else
               { %>
               <p style="color:red">Bạn phải <a href="/Member/Login?redirect=<%= Request.Url.OriginalString %>">Đăng Nhập</a> vào tài khoản để viết bình luận.</p>
            <% } %>
            <asp:BulletedList ID="blInfo" style="padding: 2px; border: none; clear: both;" CssClass="infomsg" ViewStateMode="Disabled" runat="server">
            </asp:BulletedList>
            <div class="space_double"></div>
            <h4>Các Bình Luận (<%= CommentCount() %>)</h4>
            <ul id="comment_list">
                <asp:Repeater ID="rpComment" runat="server">
                    <ItemTemplate>
                        <li>
                            <div class="comment">
                                <p class="author_name"><a href="#"><%# GetUsername(Convert.ToInt32(Eval("UserID"))) %></a></p>
                                <p class="comment_content">
                                    <%# Eval("CommentContent") %>
                                </p>                            
                            </div>                       
                        </li> 
                    </ItemTemplate>
                </asp:Repeater>                               
            </ul>   
        </ContentTemplate>
    </asp:UpdatePanel>     
</asp:Content>