<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="FreePDF.error" %>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
	<meta name="author" content="Tran Phuc Tho" />
    <title>Free PDF - Download PDF Free</title>
    <link rel="shortcut icon" href="../farvicon.ico"/>
    <style type="text/css">
        * { padding: 0; margin: 0; }
        
        body 
        {
            padding: 50px;
            font-family: "Times New Roman", Arial;
            font-size: 100%;
        }
    </style>
</head>
<body>
    <h2 style="color: Red;">Lỗi Không Xác Định</h2>
    <p>Có lỗi xảy ra trong quá trình xử lí. Vui lòng thử lại sau hoặc gửi email cho Webmaster theo địa chỉ <a href="mailto:<%= new BusinessLogicLayer.PreferencesModel().GetPreferencesByName("WebmasterEmail").Value %>"><%= new BusinessLogicLayer.PreferencesModel().GetPreferencesByName("WebmasterEmail").Value %></a></p>
</body>
</html>
