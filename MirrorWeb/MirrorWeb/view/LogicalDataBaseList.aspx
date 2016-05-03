<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true"
    CodeBehind="LogicalDataBaseList.aspx.cs" Inherits="DRMS.MirrorWeb.view.LogicalDataBaseList" %>

<%@ Register Src="~/UserControl/DataBaseListNavView.ascx" TagName="databaseNav" TagPrefix="drms" %>
<%@ Register Src="~/AdminUserControl/ucAppDBView.ascx" TagName="ucadbv" TagPrefix="drms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/dataitem.css" rel="stylesheet" type="text/css" />
    <script src="../js/TYcommon.js" type="text/javascript"></script>
    <link href="../css/BookDetail.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("#DataBase").parent().addClass("TYnav_active");
            $("#appdatabase_a").parent().find("a[field='" + QueryString("id") + "']").addClass("TYSubnav_actives");
            $("#appdatabase_a").siblings(".DBSubnavL_subordinate").show();
        });
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
                                    <span class="fr"></span><span class="TYcurrent">当前位置：数据库 > 业务应用库 >
                                        <%=DataBaseName %></span></p>
                            </div>
                            <drms:ucadbv ID="mtnavi" runat="server" />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
