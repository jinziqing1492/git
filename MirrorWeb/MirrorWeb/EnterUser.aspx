﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterUser.aspx.cs" Inherits="DRMS.MirrorWeb.EnterUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        文件地址：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        
        <br />
        <asp:Button ID="Button1" runat="server" Text="导入数据" OnClick="Button1_Click"/>
    </div>
    </form>
</body>
</html>
