<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true" CodeBehind="EntryDetail.aspx.cs" Inherits="DRMS.MirrorWeb.view.EntryDetail" %>
<%@ Register Src="~/UserControl/ArticleTree.ascx" TagName="tree" TagPrefix="drms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/css/base.css" rel="stylesheet" type="text/css" />
    <script src="../js/base.js" type="text/javascript"></script>
    <script type="text/javascript">
        //点击章节开始阅读
        function ReadChapter(treeID, pID, pName) {
//            if (pName && pID) {
//                var frmsrc = "EntryContent.aspx?doi=" + treeID ;
//            }
//            else {
                var frmsrc = "EntryContent.aspx?doi=" + treeID ;
//            }
            $("#ifrmContent").attr("src", frmsrc);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="TYContainer_page">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="container_div_minHeight">
            <tr>
                <td class="TYSubnavL">
                   <div class="ChapterTitle">
                        <span>词条导航</span>
                    </div>                    
                    <drms:tree ID="ctrl_tree" runat="server" />                     
                </td>
                <td class="TYRconter">
                    <div class="TYContainerRBG">
                        <div class="TYContainerR">
                            <div class="TYcurrentDate">
                                <p>
                                    <span class="fr"></span><span class="TYcurrent">当前位置：数据库 > 工具书 > 词条细览</span></p>
                            </div>                               
                            <!-- 加载 iframe 页面 -->
                                <iframe id="ifrmContent" onload="Javascript:$('#frmContent').height($('#frmContent').contents().find('body').height()+500)"
                                    width="100%" height="800px" frameborder="0" scrolling="auto"></iframe>                        
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
