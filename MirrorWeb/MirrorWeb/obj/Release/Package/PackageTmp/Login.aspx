<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DRMS.MirrorWeb.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>腾云数字资源管理平台</title>
    <link type="text/css" rel="stylesheet" href="/css/login.css" />
        <script  type="text/javascript">
            if (window != top)
                top.location.href = location.href;
    </script>
</head>
<body class="TYloginBG">
    <form id="form1" runat="server">
    <div class="TYCompany_logo">
    </div>
    <div class="TYloginContainer">
        <div class="clearfix">
            <div class="TYloginL">
                <div class="TYlogin">
                </div>
                <div class="TYlogin-pic">
                </div>
            </div>
            <div class="TYloginR">
                <h2 class="TYloginRTitle">用户登陆</h2>
                <asp:Panel ID="pan" runat="server" DefaultButton="imglogin">
                    <table cellpadding="0" border="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td>
                                    用户名：
                                </td>
                                <td>
                                    <%--  <input class="TYinputWidth" type="text" />--%><asp:TextBox ID="tbxusername"
                                        runat="server" CssClass="TYinputWidth"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    密&nbsp;&nbsp;&nbsp;码：
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxpwd" runat="server" CssClass="TYinputWidth" 
                                        TextMode="Password"></asp:TextBox>
                                    <%--<input class="TYinputWidth" type="text" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <input class="TYinputChexkbox" type="checkbox" />记住我的密码<span>（？）</span>
                                </td>
                            </tr>
                            <asp:Literal ID="message" runat="server"></asp:Literal>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <a href="#">
                                        <asp:ImageButton ID="imglogin" runat="server" 
                                        ImageUrl="~/images/TYloginbutn.gif" onclick="imglogin_Click" /></a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </div>
            <div class="TYloginFooter">
                <p>
                    同方知网出版集团—数字出版技术研发中心</p>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
