<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true" CodeBehind="ArticleDetail.aspx.cs" Inherits="DRMS.MirrorWeb.view.ArticleDetail" %>
<%@ Register Src="~/UserControl/ArticleTree.ascx" TagName="tree" TagPrefix="drms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/base.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //点击章节开始阅读
        function ReadChapter(treeID, pID, pName) {
            if (pName && pID) {
                var frmsrc = "ArticleContent.aspx?doi=" + treeID + "&pID=" + pID + "&parentName=" + encodeURIComponent(pName) + "&dbtype=" + "<%=dbtybe %>";
            }
            else {
                var frmsrc = "ArticleContent.aspx?doi=" + treeID + "&dbtype=" + "<%=dbtybe %>";
            }
            $("#frmContent").attr("src", frmsrc);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TYContainer_page">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="container_div_minHeight">
            <tr>
                <td class="TYSubnavL">
                    <div class="ChapterTitle">
                        <span>文章列表</span>
                    </div>
                    <drms:tree ID="ctrl_tree" runat="server" />
                </td>
                <td class="TYRconter">
                    <div class="TYContainerRBG">
                        <div class="TYContainerR">
                            <div class="TYcurrentDate">
                                <p>
                                    <span class="fr"></span><span class="TYcurrent">当前位置：文章浏览 </span></p>
                            </div>
                            <div class="TYperiodical_List">
                                <asp:HiddenField ID="hdnQueryCon" runat="server" ClientIDMode="Static"/>
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
