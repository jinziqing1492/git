<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvancedSearch.aspx.cs" Inherits="DRMS.MirrorWeb.view.AdvancedSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>电力出版社图书馆</title>
    <link type="text/css" rel="stylesheet" href="../css/index_commer.css" />
    <link type="text/css" rel="stylesheet" href="../css/index.css" />
    <link type="text/css" rel="stylesheet" href="../css/index_mirror.css" />
    <link type="text/css" rel="stylesheet" href="../css/Add-resources.css" />
    <script src="/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/js/jquery.focusEnd.js" type="text/javascript"></script>
    <script src="/js/Util.js" type="text/javascript"></script>
    <script src="/js/TYcommon.js" type="text/javascript"></script>
    <link href="/css/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
    <script src="/js/jquery.ztree.all-3.1.js" type="text/javascript"></script>
    <link href="/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            var key =  $("#<%=hdnQueryCon.ClientID %>").val();
            var cid = $("#<%=hid_theme.ClientID %>").val();
            if (cid) {
                if (cid == "<%=ClassofElectricalfence %>") {
                    ElectricalClick();
                }
                else if (cid == "<%=ClassofElectronics %>") {
                    ElectronicsClick();
                }
                else if (cid == "<%=ClassofStd %>") {
                    StdClick();
                }
                else {
                    RemoveStyleClick();
                    searchByTheme(cid, key);
                }
            }
            else {
                RemoveStyleClick();
                searchByTheme("", key);
            }

            $("#tbxTitle").keydown(function (e) {
                if (e.which == 13) {
                    SearchByKeyword();
                }
            }).click(function () {
                $(this).focusEnd();
            });
            var keyword = QueryString("keyword");
            $("#tbxTitle").val(keyword);
            
        });
        function SearchByKeyword() {
            var keyword = $("#tbxTitle").val();
            window.location.href = "../Default.aspx?keyword=" + encodeURIComponent(keyword);
        }
        function searchData() {
            var keyword = encodeURIComponent($("#tbxTitle").val());
            window.location.href = "../Default.aspx?keyword=" + keyword;
            }
        function searchByTheme(classid, keyword) {

            $("#<%=hid_theme.ClientID %>").val(classid);
            $("#ifrContent").attr("src", "/view/DatabaseContent.aspx?power=1&type=1" + "&classid=" + classid + "&queryConn=" + keyword);
        }
        function RemoveStyleClick() {
            $("#powerindex").removeClass("DLnav_Activer");            
            $("#Electrical").removeClass("DLnav_Activer");
            $("#Electronics").removeClass("DLnav_Activer");
            $("#Std").removeClass("DLnav_Activer");
        }
        function ElectricalClick() {
            $("#powerindex").removeClass("DLnav_Activer");
            $("#Electronics").removeClass("DLnav_Activer");
            $("#Std").removeClass("DLnav_Activer");
            $("#Electrical").addClass("DLnav_Activer");
            searchByTheme("<%=ClassofElectricalfence %>", $("#<%=hdnQueryCon.ClientID %>").val());
        }
        function ElectronicsClick() {
            $("#powerindex").removeClass("DLnav_Activer");
            $("#Electrical").removeClass("DLnav_Activer");
            $("#Std").removeClass("DLnav_Activer");
            $("#Electronics").addClass("DLnav_Activer");
            searchByTheme("<%=ClassofElectronics %>", $("#<%=hdnQueryCon.ClientID %>").val());
        }
        function StdClick() {
            $("#powerindex").removeClass("DLnav_Activer");
            $("#Electrical").removeClass("DLnav_Activer");
            $("#Electronics").removeClass("DLnav_Activer");
            $("#Std").addClass("DLnav_Activer");
            searchByTheme("<%=ClassofStd %>", $("#<%=hdnQueryCon.ClientID %>").val());
        }
        
    </script>
    <script type="text/javascript">
        <!--
        var zTree;
        var demoIframe;

        var setting = {
            view: {
                dblClickExpand: false,
                showLine: false,
                selectedMulti: false,
                expandSpeed: ($.browser.msie && parseInt($.browser.version) <= 6) ? "" : "fast"
            },
            data: {
                simpleData: {
                    enable: true,
                    idKey: "id",
                    pIdKey: "pId",
                    rootPId: ""
                }
            },
            callback: {
                beforeClick: function (treeId, treeNode) {
                    if (treeNode.id == "<%=ClassofElectricalfence %>") {
                        ElectricalClick();
                    }
                    else if (treeNode.id == "<%=ClassofElectronics %>") {
                        ElectronicsClick();
                    }
                    else if (treeNode.id == "<%=ClassofStd %>") {
                        StdClick();
                    }
                    else {
                    RemoveStyleClick();
                    $("#<%=hid_theme.ClientID %>").val(treeNode.id);
                    searchByTheme(treeNode.id, $("#<%=hdnQueryCon.ClientID %>").val());
                    }
                }
            }
        };
		$(document).ready(function () {
		    var t = $("#tree");
		    t = $.fn.zTree.init(t, setting, <%=Nodes %>);
		});
      //-->
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="warpper">
	<div class="DLlogo"></div>
    <div class="DLnav">
    	<div class="DZsearch fr">
        	<input type="text" id="tbxTitle"  />
            <a class="DZsearch_btn"  onclick="searchData()"></a>
            <a href="#" class="DZadvanced_search">高级搜索</a>
        </div>
        <div class="DLnav_container clearfix">
        	<a href="../Default.aspx" >首&nbsp;&nbsp;页</a>
            <a id="Electrical" href="#" onclick="ElectricalClick()">电网技术</a>
            <a id="Electronics" href="#" onclick="ElectronicsClick()">电工电子</a>
            <a id="Std" href="#" onclick="StdClick()">标&nbsp;&nbsp;准</a>
            <a href="/view/PowerJournallist.aspx" >期&nbsp;&nbsp;刊</a>            
        </div>        
    </div>
    <div class="DLcontainer">
    <div class="DLbanner MT"><img src="../images/DLbanner.jpg" /></div>
    <div class="TYContainer_page">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="container_div_minHeight">
            <tr>
                <td class="TYSubnavL">
                    <div style="width: 245px; float: left; border:none;min-height: 500px;">
                        <div class="ChapterTitle">
                            <span>分类体系</span>
                        </div>
                        <ul id="tree" class="ztree" style="overflow: visible;">
                        </ul>
                    </div>
                </td>
                <td class="TYRconter">
                    <div class="TYContainerRBG">
                        <div class="TYContainerR">
                            <div class="TYperiodical_List">
                            <table style='margin: 0px auto; width: 100%; height: 150px;  text-align:center;
                                      padding-top: 1px; border: 1px solid #aed0ea;'>
                                <tr>
                                    
                                    <td class="TYBookText_title" style="width: 10%">
                                        书名：                                        
                                    </td>
                                    <td >
                                        <asp:TextBox ID="bTitle" runat="server" Width="200px" ></asp:TextBox>
                                    </td>                                    
                                    <td class="TYBookText_title" style="width: 10%">
                                        ISBN：                                        
                                    </td>
                                    <td >
                                        <asp:TextBox ID="bISBN" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                                                   
                                </tr>
                                <tr>                                     
                                    <td class="TYBookText_title" style="width: 10%">
                                        作者：                                        
                                    </td>
                                    <td >
                                        <asp:TextBox ID="bAuthor" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td class="TYBookText_title" style="width: 10%">
                                        关键字：                                        
                                    </td>
                                    <td >
                                        <asp:TextBox ID="bKeyWord" runat="server" Width="200px" ></asp:TextBox>
                                    </td>                                    
                                </tr>
                                <tr>                                    
                                    <td class="TYBookText_title" style="width: 10%">
                                        出版社：                                        
                                    </td>
                                    <td >
                                        <asp:TextBox ID="bPubDep" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                    <td >                                        
                                    </td>                                    
                                    <td >
                                        <a id="queryUser"  class="DZsearch_btn" runat="server" onserverclick="QueryBtn_Click">
                                        </a>
                                    </td>
                                </tr>
                            </table>                                 
                                <iframe id="ifrContent" marginwidth="0" onload="Javascript:$('#ifrContent').height($('#ifrContent').contents().find('body').height()+15)"
                                    width="100%" height="500px" marginheight="0" frameborder="0" scrolling="no" allowtransparency="true" ></iframe>
                                
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnQueryCon" runat="server" />
    <asp:HiddenField ID="hdnSearchWord" runat="server" />
    <asp:HiddenField id="hid_theme"  runat="server"/>
    </div>
    <div class="DLfooter MT">
    	<p>版权所有：英大传媒投资集团有限公司</p>
        <p>中国电力出版社有限公司</p>
        <a href="../soft/CAJViewer 7.2.self.exe" ><font color="white">CAJViewer 7.2 阅读器下载</font></a>
    </div>    
    </div>
    </form>
</body>
</html>
