<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true" CodeBehind="SupportDetail.aspx.cs" Inherits="DRMS.MirrorWeb.view.SupportDetail" %>
<%@ Register Src="~/UserControl/DataBaseListNavView.ascx" TagName="databaseNav"  TagPrefix="drms"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link type="text/css" rel="stylesheet" href="../css/BookDetail.css" />
<link href="../css/fancybox/source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
<script src="../js/TYcommon.js" type="text/javascript"></script>
<style type="text/css">
    .sTitle
    {
        line-height: 18px;
        padding-top: 5px;
        padding: 4px 0 4px 20px;
        color: #57A000;
        display: inline-block;
        font-size: 14px;
        width: 120px;
        vertical-align:middle;
    }
</style>
<script type="text/javascript">
    $(function () {
        $("#DataBase").parent().addClass("TYnav_active");
        $("#supportdatabase_a").parent().find("a[id='" + QueryString("dbtype") + "']").addClass("TYSubnav_actives");
        $("#supportdatabase_a").siblings(".DBSubnavL_subordinate").show();
        $("#supportdatabase_a").addClass("TYSubnav_actives");
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
                                    <span class="fr"></span><span class="TYcurrent">当前位置：数据库 > <%=DataBaseName %> >细览</span></p>
                            </div>
                            <div class="TYperiodical_List">
                                <asp:HiddenField ID="hdnResDoi" runat="server" />
                                <asp:Panel ID="PanelUCContent" runat="server">
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
