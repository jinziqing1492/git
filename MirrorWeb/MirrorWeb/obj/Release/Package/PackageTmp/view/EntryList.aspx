<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true" CodeBehind="EntryList.aspx.cs" Inherits="DRMS.MirrorWeb.view.EntryList" %>
<%@ Register Src="~/UserControl/DataBaseListNavView.ascx" TagName="databaseNav" TagPrefix="drms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/TYcommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#ifrContent").attr("src", "../view/DatabaseContent.aspx?type=" + $("#<%=hdnQueryCon.ClientID %>").val() + "&searchword=" + $("<%=hdnSearchWord.ClientID %>").val()+"&bookid="+"<%=BookID %>");
            $("#DataBase").parent().addClass("TYnav_active");

            var type = QueryString("dbtype");
            if (type == 13 || type == 14 || type == 15 || type == 28) {
                $("#supportdatabase_a").parent().find("a[id='" + QueryString("dbtype") + "']").addClass("TYSubnav_actives");
                $("#supportdatabase_a").siblings(".DBSubnavL_subordinate").show();
                $("#supportdatabase_a").addClass("TYSubnav_actives");
            }
            else {
                $("#basedatabase_a").addClass("TYSubnav_actives");
                $("#basedatabase_a").parent().find("a[id='" + QueryString("dbtype") + "']").addClass("TYSubnav_actives");
                $("#basedatabase_a").siblings(".DBSubnavL_subordinate").show();
            }
        });

        function searchData() {
            var sql = $("#<%=hdnQueryCon.ClientID %>").val();
            var keyword = decodeURIComponent($("#tbxTitle").val());
            //            var tabs_select;
            //            $("#tabs div").each(function () {
            //                if (!$(this).hasClass("ui-tabs-hide")) {
            //                    tabs_select = $(this);
            //                }
            //            });
            $("#ifrContent").attr("src", "../view/DatabaseContent.aspx?type=" + $("#<%=hdnQueryCon.ClientID %>").val() + "&keyword=" + keyword + "&searchword=" + $("<%=hdnSearchWord.ClientID %>").val());
        }
        // 回车搜索
        function Search_keyDown() {
            var lKeyCode = (navigator.appname == "Netscape") ? event.which : event.keyCode;
            if (lKeyCode == "13") {
                searchData();
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="TYContainer_page">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="container_div_minHeight">
            <tr>
                <td class="TYSubnavL">
                   <drms:databaseNav ID="mydatabasenav" runat="server" />
                </td>
                <td class="TYRconter">
                    <div class="TYContainerRBG">
                        <div class="TYContainerR">
                            <div class="TYcurrentDate">
                                <p>
                                    <span class="fr"></span><span class="TYcurrent">当前位置：数据库 > 工具书 > 词条列表</span></p>
                            </div>
                            <div class="TYchoiceProcess MT">
                                <div class="bc-search" style="margin-bottom: 10px;">
                                    <div class="bcs-left">
                                        <div class="bcs-right">
                                            <div class="bcs-main" style="padding-left: 20px;">
                                                <div class="bcs-box">
                                                    <input type="text" id="tbxTitle" onkeydown="Search_keyDown();" />
                                                </div>
                                                <input type="button" value="检 索" class="btn-news" onclick="searchData()" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="TYperiodical_List">
                                <div class="TYContainerRText_title MT clearfix">
                                    <div class="TYCRTextTitle_arTab fl">
                                        <div class="TYCRTextTitle_left fl">
                                        </div>
                                        <div class="TYCRTextTitle_center fl">
                                           词条列表</div>
                                        <div class="TYCRTextTitle_right fl">
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdnQueryCon" runat="server" />
                                <asp:HiddenField ID="hdnSearchWord" runat="server" />
                                <!-- 加载 iframe 页面 -->
                                <iframe id="ifrContent" marginwidth="0" onload="Javascript:$('#ifrContent').height($('#ifrContent').contents().find('body').height()+15)"
                                    width="90%" height="500px" marginheight="0" frameborder="0" scrolling="no"></iframe>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
