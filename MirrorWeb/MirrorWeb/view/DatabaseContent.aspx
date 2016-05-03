<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseContent.aspx.cs" Inherits="DRMS.MirrorWeb.view.DatabaseContent" %>
<%@ Register Src="~/UserControl/DBContentView.ascx" TagName="dbcontent" TagPrefix="drms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <drms:dbcontent id="ctrl_Search" runat="server"></drms:dbcontent>
    </div>
    </form>
</body>
</html>
