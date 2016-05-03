<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyFavoriteView.ascx.cs" Inherits="DRMS.MirrorWeb.UserControl.MyFavoriteView" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
<link type="text/css" rel="stylesheet" href="../css/Add-resources.css" />
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
        function FirstPage() {
            document.getElementById('<%=FirstOne.ClientID %>').click();
        }

        //末一页
        function LastPage() {
            document.getElementById('<%=LastOne.ClientID %>').click();
        }

        //数据加载完成后，选中之前选中的任务记录
        function loadCheckedMission() {
            var tempHdnCheckedMIDs = "|" + $("#hdnCheckedMIDs").val() + "|";
            $("input[type='checkbox']").each(function () {
                if (tempHdnCheckedMIDs.indexOf("|" + $(this).val() + "|") != -1)
                    $(this).attr("checked", "checked");
            });
        }

        //存储选中任务记录的id
        $(function () {
            $("input[type='checkbox']").click(function () {
                if ($(this).attr("checked") == "checked") {

                    $("#hdnCheckedMIDs").val($("#hdnCheckedMIDs").val() + "|" + $(this).val());
                }
                else {
                    $("#hdnCheckedMIDs").val($("#hdnCheckedMIDs").val().replace($(this).val(), ""));
                    $("input[type='checkbox'].check-all").attr("checked", false);
                }
            });
            //多选按钮
            $("input[type='checkbox'].check-all").click(function () {
                if ($(this).attr("checked") == "checked") {
                    $("input[type='checkbox'].testRepeater").attr("checked", true);
                    $(".testRepeater").each(function (i) {
                        $("#hdnCheckedMIDs").val($("#hdnCheckedMIDs").val() + "|" + $(this).val());
                    });
                    $("#hdnCheckedMIDs").val($("#hdnCheckedMIDs").val().replace("on", ""));
                }
                else {
                    $("input[type='checkbox'].testRepeater").attr("checked", false);
                    $(".testRepeater").each(function (i) {
                        $("#hdnCheckedMIDs").val($("#hdnCheckedMIDs").val().replace($(this).val(), ""));
                    });
                }
            });
        });

</script>
<div class="TYContainer_Table MT">
    <%--<input type="hidden" id="hdnCheckedMIDs" name="hdnCheckedMIDs" />--%>
    <asp:HiddenField ID="hdnCheckedMIDs" runat="server" ClientIDMode="Static" />
    <table cellpadding="0" cellspacing="0" border="0" width="100%" pageno="<%=PageNo %>" pagesize="<%=PageSize %>">
        <thead>
            <tr>
                <th>序号
                </th>
                <th>资源名称
                </th>
                <th>资源类型
                </th>
                <th>添加时间
                </th>
                <th>操作
                </th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repEntryList" runat="server" OnItemCommand="RepeaterItemCommand">
                <ItemTemplate>
                    <tr>
                        <%--                            <td>
                                <input class="testRepeater"  value="<%#Eval("id") %>" type="checkbox"" />
                            </td>--%>
                        <td class="record-no">
                            <%# Container.ItemIndex + 1 %>
                        </td>
                        <td>
                            <a href="<%# GetDetailPage(Eval("BOOKTYPE"),Eval("DOI")) %>" target="_blank"><%#Eval("NAME")%></a>
                        </td>
                        <td>
                            <%# GetResourceName(Eval("BOOKTYPE"))%>
                        </td>
                        <td>
                            <%# FormatDate(Eval("OPERATORDATE"))%>
                        </td>
                        <td>
                            <asp:LinkButton ID="btn_del" CommandName="Delete" CommandArgument='<%#Eval("ID") %>' runat="server">删除</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
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
                        <a onclick="FirstPage()" href="#">首页<asp:Button ID="FirstOne" runat="server" OnClick="FirstOne_Click"
                            BorderStyle="None" Width="0px" Height="0px" /></a> <a onclick="PreviewOne()" href="#">
                                <asp:ImageButton ID="PreviewOne" ImageUrl="../images/TYpub1.png" runat="server" Height="12px"
                                    Width="12px" OnClick="PreviewOne_Click" /></a>
                        <asp:Literal ID="ltlCBookCurrentpage" runat="server"></asp:Literal><span>/</span>
                        <asp:Literal ID="ltlCBookPagecount" runat="server"></asp:Literal><span>页</span>
                        <a onclick="NextOne()" href="#">
                            <asp:ImageButton ID="NextOne" ImageUrl="../images/TYpubNext2.png" runat="server"
                                OnClick="NextOne_Click" Height="12px" Width="12px" /></a> <a onclick="LastPage()"
                                    href="#">尾页<asp:Button ID="LastOne" runat="server" OnClick="LastOne_Click" Height="0px"
                                        Width="0px" BorderStyle="None" /></a> <span>转到第<asp:TextBox ID="PageNum" runat="server"></asp:TextBox>页</span>
                        <a href="#" class="TYpubGo">
                            <asp:ImageButton ID="GoTo" ImageUrl="../images/TYpubGo.png" runat="server" OnClick="GoTo_Click"
                                Height="17px" Width="16px" />转</a>
                    </div>
                    <div class="TYtabkePage_information">共<asp:Literal ID="ltlCBookItemcount" runat="server"></asp:Literal>条记录，每页10条</div>
                </td>
            </tr>
        </tfoot>
    </table>
    <asp:HiddenField ID="hid_sql" runat="server" />
</div>
