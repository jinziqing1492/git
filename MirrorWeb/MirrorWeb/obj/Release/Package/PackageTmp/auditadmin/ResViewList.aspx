<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResViewList.aspx.cs"
    Inherits="DRMS.MirrorWeb.auditadmin.ResViewList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="../css/mytasklist.css" />
    <script src="../js/Util.js" type="text/javascript"></script>
    <script src="../js/TYcommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        //页面跳转
        function SwitchOne() {
            document.getElementById('<%=GoTo.ClientID %>').click();
        }

        //下一页
        function NextOne() {
            document.getElementById('<%=NextOne.ClientID %>').click();
        }

        //前一页
        function PreviewOne() {
            document.getElementById('<%=PreviewOne.ClientID %>').click();
        }

        //第一页
        function FirstOne() {
            document.getElementById('<%=FirstOne.ClientID %>').click();
        }

        //末一页
        function LastOne() {
            document.getElementById('<%=LastOne.ClientID %>').click();
        }

        //存储选中任务记录的id
        $(function () {
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="TYContainer_Table">
        <input type="hidden" id="hdnLogicID" runat="server" />
        <input type="hidden" id="hdnResTypeFlag" runat="server" />
        <input type="hidden" id="hdnAddResDois" runat="server" />
        <input type="hidden" id="hdnDeleteResDois" runat="server" />
        <table cellpadding="0" cellspacing="0" border="0" width="100%" pageno="<%=PageNo %>"
            pagesize="<%=PageSize %>">
            <thead>
                <asp:Literal ID="LiteralTHead" runat="server"></asp:Literal>
            </thead>
            <tbody>
                <asp:Literal ID="LiteralTBody" runat="server"></asp:Literal>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="8">
                        <div class="pagination">
                            <webdiyer:AspNetPager ID="aspNetPager" runat="server" CssClass="aspNetPager" UrlPaging="false"
                                PageSize="10" FirstPageText="首页" LastPageText="尾页" NextPageText="后一页" PrevPageText="前一页"
                                InputBoxClass="jump-input" ShowBoxThreshold="10" ShowInputBox="Always" NumericButtonCount="10"
                                Visible="false">
                            </webdiyer:AspNetPager>
                        </div>
                        <div class="TYtabkePage_turning fr">
                            <a onclick="FirstOne()" href="#">首页<asp:Button ID="FirstOne" runat="server" OnClick="FirstOne_Click"
                                BorderStyle="None" Width="0px" Height="0px" /></a> <a onclick="PreviewOne()" href="#">
                                    <asp:ImageButton ID="PreviewOne" ImageUrl="../images/TYpub1.png" runat="server" Height="12px"
                                        Width="12px" OnClick="PreviewOne_Click" /></a>
                            <asp:Literal ID="ltlCBookCurrentpage" runat="server"></asp:Literal><span>/</span>
                            <asp:Literal ID="ltlCBookPagecount" runat="server"></asp:Literal><span>页</span>
                            <a onclick="NextOne()" href="#">
                                <asp:ImageButton ID="NextOne" ImageUrl="../images/TYpubNext2.png" runat="server"
                                    OnClick="NextOne_Click" Height="12px" Width="12px" /></a> <a onclick="LastOne()"
                                        href="#">尾页<asp:Button ID="LastOne" runat="server" OnClick="LastOne_Click" Height="0px"
                                            Width="0px" BorderStyle="None" /></a> <span>转到第<asp:TextBox ID="PageNum" runat="server"></asp:TextBox>页</span>
                            <a href="#" class="TYpubGo">
                                <asp:ImageButton ID="GoTo" ImageUrl="../images/TYpubGo.png" runat="server" OnClick="GoTo_Click"
                                    Height="17px" Width="16px" />转</a>
                        </div>
                        <div class="TYtabkePage_information">
                            共<asp:Literal ID="ltlCBookItemcount" runat="server"></asp:Literal>条记录，每页10条</div>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    </form>
</body>
</html>
