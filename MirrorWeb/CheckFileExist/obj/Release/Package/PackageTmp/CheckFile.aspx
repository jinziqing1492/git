<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckFile.aspx.cs" Inherits="CheckFileExist.CheckFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        table{ font-size:12px; border:1px solid gray;}
        table td,table th{ border-bottom:1px solid gray; padding:0px 10px; line-height:25px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btn_Check" runat="server" Text="开始检测" OnClick="btn_Check_Click"/>
    <div>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <th>序号</th>
                <th>唯一标示</th>
                <th>标题</th>
                <th>是否有封面</th>
                <th>是否有文件</th>
            </tr>
            <asp:Literal ID="lt_list" runat="server"></asp:Literal>
        </table>
    </div>
    </form>
</body>
</html>
