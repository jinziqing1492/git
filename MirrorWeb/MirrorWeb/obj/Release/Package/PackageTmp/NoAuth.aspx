<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoAuth.aspx.cs" Inherits="DRMS.MirrorWeb.NoAuth" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .error{ width:650px; height:380px; margin:50px auto; background-image:url(../images/error.jpg);}
        .error span{ display:inline-block; font-size:20px; font-weight:bold; color:#FF994E; margin:185px 0px 0px 243px; line-height:1.5}
        .error a{ color:#35AA66; text-decoration:none;}
        .error a:hover{ text-decoration:underline;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="error">
        <span>您没有权限访问本系统</span>
    </div>
    </form>
</body>
</html>
