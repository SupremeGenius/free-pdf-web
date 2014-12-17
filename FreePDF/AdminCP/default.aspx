<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FreePDF.AdminCP._default" %>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
	<meta name="author" content="Tran Phuc Tho" />
	<title>FreePDF - Administrator Login</title>
	<link rel="shortcut icon" href="../farvicon.ico"/>
	<link rel="stylesheet" type="text/css" href="../theme/admin/css/login.css"/>
	<!-- LOAD JS -->
	<script type="text/javascript" src="../js/cssbrowserselector.js"></script>
	<script type="text/javascript" src="../js/jquery.js"></script>
	<script type="text/javascript" src="../js/functions.js"></script>
	<!--[if lt IE 7]>
        	<script type="text/javascript" src="js/unitpngfix.js"></script>
	<![endif]-->
	<!--[if lt IE 7]>
		<script type="text/javascript">
			$(document).ready(function(){
				$('.red_button').hover(function(){
					$(this).addClass("red_buttonhover");
				}, function(){
					$(this).removeClass("red_buttonhover");
				});
			});
		</script>
	<![endif]-->
</head>
<body>
    <div id="wrapper">
		<div id="navbar"><a href="../default.aspx">Trở về Trang Chủ</a></div>
		<h2 id="header_title"><span class="icon"></span><span>Administrator Login</span></h2>
		<div class="space_double clr"><!-- --></div>
		<center>
		<form id="fAdminLogin" runat="server">
			<p><span>Username</span><asp:TextBox ID="txtUsername" CssClass="outerglow_textbox" runat="server"/></p>
			<p><span>Password</span><asp:TextBox ID="txtPassword" CssClass="outerglow_textbox" TextMode="Password" runat="server" /></p>
			<p class="button_wrapper">
				<input class="red_button" type="button" name="btnClear" id="btnClear" value="Xóa" onclick="clearForm('#fAdminLogin');" />
				<asp:Button CssClass="red_button" id="btnLogin" Text="Đăng Nhập" runat="server" onclick="btnLogin_Click" />
			</p>
            <asp:BulletedList ID="blInfo" CssClass="infomsg" runat="server">
            </asp:BulletedList>
            <asp:RequiredFieldValidator ID="validtUsername" runat="server" ErrorMessage="Chưa nhập tài khoản" ControlToValidate="txtUsername" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="validtPassword" runat="server" ErrorMessage="Chưa nhập mật khẩu" ControlToValidate="txtPassword" Display="None"></asp:RequiredFieldValidator>                
            <asp:ValidationSummary ID="validtSum_lg" CssClass="errormsg" runat="server" />
		</form>
        
		</center>
	</div> <!-- END: DIV#WRAPPER -->
</body>
</html>
