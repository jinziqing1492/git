<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="DRMS.MirrorWeb.AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>中航传媒-产品管理系统</title>
    <link href="../css/Reset.css" rel="Stylesheet" type="text/css" />
    <!-- Main Stylesheet -->
    <link rel="stylesheet" href="../css/adminlogin.css" type="text/css" media="screen" />
     <script type="text/javascript">
         if (window != top)
             top.location.href = "AdminLogin.aspx";
    </script>
</head>

<body class="SLlongin">
 <form id="form2" runat="server">
<div class="wrapper">
	<div class="sllogining">
	<div class="sllogo">
    	<div class="notification">
        	<p><asp:Literal ID="message" runat="server" Text="请输入正确的用户名、密码，登录系统"></asp:Literal></p>
        </div>
    	<table cellpadding="0" border="0" cellspacing="0">
        	<tbody>
            	<tr><td>用户名：</td><td><input id="tbxAccount" class="text-input" type="text" runat="server" /></td></tr>
                <tr><td>密&nbsp;&nbsp;码：</td><td><input id="tbxPassword" class="text-input" type="password" runat="server" /></td></tr>
                <tr><td colspan="2"><asp:ImageButton ID="btnLogon"  runat="server" Text="登 录" Width="72px"  onclick="btnLogon_Click" ImageUrl="~/images/login/slbutn.png" /></td></tr>
            </tbody>
        </table>
    </div>
    </div>
</div>
</form>
</body>
</html>

