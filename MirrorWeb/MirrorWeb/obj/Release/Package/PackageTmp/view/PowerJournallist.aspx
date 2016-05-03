<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PowerJournallist.aspx.cs" Inherits="DRMS.MirrorWeb.view.PowerJournallist" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>电力出版社图书馆</title>
    <link type="text/css" rel="stylesheet" href="../css/index_commer.css" />
    <link type="text/css" rel="stylesheet" href="../css/index.css" />
    <link type="text/css" rel="stylesheet" href="../css/index_mirror.css" />
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../js/jquery.focusEnd.js" type="text/javascript"></script>
    <script src="/js/Util.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#tbxTitle").keydown(function (e) {
                if (e.which == 13) {
                    SearchByKeyword();
                }
            }).click(function () {
                $(this).focusEnd();
            });
            
        });

        function SearchByKeyword() {
            var keyword = $("#tbxTitle").val();
            window.location.href = "../Default.aspx?keyword=" + encodeURIComponent(keyword);
        }

        function searchData() {

            var keyword = encodeURIComponent($("#tbxTitle").val());
            window.location.href = "../Default.aspx?keyword=" + keyword;
        }    
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="warpper">
	<div class="DLlogo"></div>
    <div class="DLnav">
    	<div class="DZsearch fr">
        	<input class="hide" style="display:none" />
            <input type="text" id="tbxTitle"  />
            <a class="DZsearch_btn"  onclick="searchData()"></a>
            <a href="/view/AdvancedSearch.aspx" class="DZadvanced_search">高级搜索</a>
        </div>
        <div class="DLnav_container clearfix">
        	<a href="../Default.aspx" >首&nbsp;&nbsp;页</a>
            <a href="../Default.aspx?powerclassid=<%=ClassofElectricalfence %>" >电网技术</a>
            <a href="../Default.aspx?powerclassid=<%=ClassofElectronics %>" >电工电子</a>
            <a href="../Default.aspx?powerclassid=<%=ClassofStd %>" >标&nbsp;&nbsp;准</a>
            <a href="#" class="DLnav_Activer">期&nbsp;&nbsp;刊</a>            
        </div>        
    </div>
    <div class="DLcontainer">
    <div class="DLbanner MT"><img src="../images/DLbanner.jpg"  alt=""/></div>
    <div class="TYContainer_page">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="container_div_minHeight">
            <tr>
                <td class="TYRconter">
                    <div class="TYContainerRBG">
                        <div class="TYContainerR">
                            <div class="TYperiodical_List">
                                <div id="noResult" runat="server" style='margin: 15px auto; width: 300px; height: 50px;
                                    text-align: center; color: Red; padding-top: 20px; border: 1px solid #aed0ea;'>
                                    <p style='margin: auto;'>
                                        该资源不存在，请浏览其它资源</p>
                                </div>
                                <div id="haveResult" runat="server" visible="false">
                                     <asp:Literal ID="lt_list" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hidDoi" runat="server" />
    </div>
    <div class="clear" ></div>
    <div class="DLfooter MT">
    	<p>版权所有：英大传媒投资集团有限公司</p>
        <p>中国电力出版社有限公司</p>
        <a href="../soft/CAJViewer 7.2.self.exe" ><font color="white">CAJViewer 7.2 阅读器下载</font></a>
    </div>    
    </div>
    </form>
</body>
</html>
