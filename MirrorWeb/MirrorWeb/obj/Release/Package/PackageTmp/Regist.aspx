<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regist.aspx.cs" Inherits="DRMS.MirrorWeb.Regist" %>
<%@ Register Src="~/AdminUserControl/FootView.ascx" TagName="foot" TagPrefix="drms" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link type="text/css" rel="stylesheet" href="css/index_commer.css" />
    <link type="text/css" rel="stylesheet" href="css/index.css" />
    <style type="text/css">
        .registContainer{ height:600px; background:url(../images/TYcontainerBG.png); BORDER-TOP: #1360a2 1px solid}
    </style>
</head>
<body class="TYBodybg">
    <div class="container_div_minWidth">
        <form id="form1" runat="server">
        <div class="TYLogoNav">
            <div class="TYlogoSearch">
                <div class="TYlogo">
                    <img alt="" src="../images/TYlogo.png" /></div>
                <div class="TYSearch">
                    <div class="TYSearchI-left fl">
                    </div>
                    <div class="TYSearchI-certen fl">
                        <input id="headsearch" type="text" autocomplete="off" onkeydown="ctrl_keyDown();" /></div>
                    <div class="TYSearchI-right fl">
                    </div>
                </div>
            </div>
        </div>
        <div class="registContainer">
            <div>
                <table>
                    <tr>
                        <td>
                            用户名：
                        </td>
                        <td>
                            <input type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                        <td>

                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <drms:foot ID="footview" runat="server" />
        </form>
    </div>
</body>
</html>
