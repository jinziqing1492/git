<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PowerBookDetail.aspx.cs" Inherits="DRMS.MirrorWeb.view.PowerBookDetail" %>
<%@ Register Src="~/UserControl/ZoomImages.ascx" TagName="zoom" TagPrefix="drms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>电力出版社图书馆</title>
    <link type="text/css" rel="stylesheet" href="../css/index_commer.css" />
    <link type="text/css" rel="stylesheet" href="../css/index.css" />
    <link type="text/css" rel="stylesheet" href="../css/index_mirror.css" />
    <link type="text/css" rel="stylesheet" href="../css/BookDetail.css" />
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="../css/fancybox/source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <script src="../css/fancybox/source/jquery.fancybox.js" type="text/javascript"></script> 
    <script src="../js/Util.js" type="text/javascript"></script>
    <script src="../js/TYcommon.js" type="text/javascript"></script>
    <script src="../js/jquery.focusEnd.js" type="text/javascript"></script>
    <script type="text/javascript">
        function catalogClick(doi, mType, filename, vpath) {
            //这个目录跳转到的页面还需要弄一下
            $(".sm a").click(function () {
                var page = $(this).next("span").html();
                var filename = encodeURIComponent(filename);
                var url = "/AdminknReader/Default.aspx?doi=" + doi + "&type=" + mType + "&page=" + page;
                window.location.href = url;
            });
        }
        $(function () {
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

        $(function () {

            //注册查看大图的click事件
            $("#zoom_images_click_a").click(function () {
                $.fancybox.open({
                    href: "<%=this.ctrl_Zoom.BigImgSrc %>",
                    title: ""
                });
            });


            //为目录注册点击事件
            var mType = "<%=mType %>";
            var filename = "<%=filename %>";
            var vpath = "<%=vpath %>";
            var doi = $("#<%=hidDoi.ClientID %>").val();
            catalogClick(doi, mType, filename, vpath);

            $(".attopt").click(function () {
                var attaid = $(this).attr("doi");
                var attaname = $(this).attr("attname");
                var type = $(this).attr("type");
                var bookname = $(this).attr("bookname");

                $.ajax({
                    type: "GET",
                    url: "../Ajax/ApplyDownLoadHandler.ashx",
                    data: "doi=" + attaid + "&type=" + type + "&name=" + attaname + "&bookname=" + bookname,
                    catche: false,
                    dataType: "json",
                    success: function (data) {
                        if (data) {
                            alert(data.Message);
                        }
                    }
                });

            });

        });
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
            <a href="/view/PowerJournallist.aspx">期&nbsp;&nbsp;刊</a>            
        </div>        
    </div>
    <div class="DLcontainer">
    <div class="DLbanner MT"><img src="../images/DLbanner.jpg" /></div>
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
                                    <div class="singleBook">
                                        <div class="sgbName">
                                            <asp:Literal ID="lt_Title" runat="server"></asp:Literal>
                                        </div>
                                        <div class="sgbMain">
                                            <div class="sgbml">
                                                <drms:zoom ID="ctrl_Zoom" runat="server" />
                                            </div>
                                            <div class="sgbmr">
                                                <ul class="summary">
                                                    <asp:Literal ID="lt_summary" runat="server"></asp:Literal>
                                                </ul>
                                                <div class="book-price">
                                                    <asp:Literal ID="lt_price" runat="server"></asp:Literal>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="singleDetail">
                                        <h2>
                                            内容简介</h2>
                                        <div class="sm">
                                            <asp:Literal ID="lt_Digest" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="singleDetail">
                                        <h2>
                                            目录</h2>
                                        <div class="sm">
                                            <asp:Literal ID="lt_catalog" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                    </div>
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
    <div class="DLfooter MT">
    	<p>版权所有：英大传媒投资集团有限公司</p>
        <p>中国电力出版社有限公司</p>
        <a href="../soft/CAJViewer 7.2.self.exe" ><font color="white">CAJViewer 7.2 阅读器下载</font></a>
    </div>    
    </div>
    </form>
</body>
</html>
