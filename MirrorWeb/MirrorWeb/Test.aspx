<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="DRMS.MirrorWeb.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/jquery-1.7.2.min.js"></script>
    <script src="js/layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            layer.tips('Hi，我是tips', 'img');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img src="/images/notepic.png" />
        <iframe src="Test.aspx" width="100%" height="100%">

        </iframe>
    </div>
    </form>
</body>
</html>
