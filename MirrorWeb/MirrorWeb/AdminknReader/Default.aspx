<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminKNReader._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="Stylesheet" href="css/read.css" />
        <script language="javascript" type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
    <script language="javascript" type="text/javascript" src="js/Reader.js"></script>
</head>
<body onload="PopReader('<%=mType%>','<%=mBookid %>','<%=page %>');">
    <form id="form1" runat="server">
<%--    <div>
    <input type="button" value="阅读" visible="false" onclick="PopReader('11','B000010000002','1');" />
    <input type="button" value="返回" visible="false" onclick="window.location.href('../TMaterial/SourceCollect.aspx')" />
    </div>--%>
    <div id="divReader">
    </div>
    <input type="hidden" id="hidHeight" value="0" />
    <input type="hidden" id="hidWidth" value="0" />
    </form>
   
</body>
</html>
