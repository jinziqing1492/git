<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true" CodeBehind="ChapterRead.aspx.cs" Inherits="DRMS.MirrorWeb.view.ChapterRead" %>
<%@ Register Src="~/UserControl/CharpterTree.ascx" TagName="tree" TagPrefix="drms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/base.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //点击章节开始阅读
        function ReadChapter(treeID, pID, pName) {
            if (pName && pID) {
                var frmsrc = "ChapterReadContent.aspx?doi=" + treeID + "&pID=" + pID + "&parentName=" + encodeURIComponent(pName);
            }
            else {
                var frmsrc = "ChapterReadContent.aspx?doi=" + treeID;
            }
            $("#frmContent").attr("src", frmsrc);
            $("html,body").animate({ scrollTop: $("#allcontent").offset().top }, 1000)
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TYContainer_page">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="container_div_minHeight">
            <tr>
                <td class="TYSubnavL">
                    <div class="ChapterTitle">
                        <span>章节列表</span>
                    </div>
                    <drms:tree ID="ctrl_tree" runat="server" />
                </td>
                <td class="TYRconter">
                    <div class="TYContainerRBG">
                        <div class="TYContainerR">
                            <div class="TYcurrentDate">
                                <p>
                                    <span class="fr"></span><span class="TYcurrent">当前位置：章节浏览 </span></p>
                            </div>
                            <div class="TYperiodical_List" id="allcontent">
                                <asp:HiddenField ID="hdnQueryCon" runat="server" />
                                <div class="readeRight">
                                <div>
                                    <span>资源名称：<asp:Literal ID="lt_title" runat="server"></asp:Literal></span>
                                </div></div>
                                <!-- 加载 iframe 页面 -->
                          <iframe id="frmContent" onload="Javascript:$('#frmContent').height($('#frmContent').contents().find('body').height()+500)"
                                    width="100%" height="500px" frameborder="0" scrolling="no"></iframe>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
