<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginNew.aspx.cs" Inherits="DRMS.MirrorWeb.LoginNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>航空科技知识数字资源库</title>
    <link type="text/css" rel="stylesheet" href="css/ZHnew_Login.css" />
    <script type="text/javascript">
        if (window != top)
            top.location.href = location.href;
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="zhNewContent">
        <div class="zhNewContent_Logo">
            <img src="images/zhNewContent_Logo.png" alt="" />
        </div>
        <div class="zhNewLogin">
            <asp:TextBox ID="tbxusername" runat="server" placeholder="用户名" CssClass="zhNew_User"></asp:TextBox>
            <asp:TextBox ID="tbxpwd" runat="server"  placeholder="密码" CssClass="zhNew_PassWord" TextMode="Password" style="margin-bottom:10px;"></asp:TextBox>
            <div style="text-align:left;">
                <asp:Label ID="message"  Text=""  runat="server" style="color:red;"></asp:Label>
            </div>
            <div class="zhNew_Opera" style=" margin-top:12px;">
                
                <div class="zhNew_BTN">
                    <a href="#">
                        <asp:ImageButton ID="imglogin" runat="server" ImageUrl="~/images/zhNew_BTN.png"
                            OnClick="imglogin_Click" />
                    </a>
                </div>
                <div>
                    <a href="#">
                        <asp:ImageButton ID="imgIplogin" runat="server" ImageUrl="~/images/zhNewIp_BTN.png" OnClick="imgIplogin_Click" />
                    </a>
                </div>
            </div>
        </div>
        <div class="zhFootLogo"><img src="images/zhFootLogo.png" alt="" /></div>
    </div>
    </form>
</body>
</html>
