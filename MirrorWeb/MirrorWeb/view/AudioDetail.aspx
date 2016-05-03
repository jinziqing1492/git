<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true"
    CodeBehind="AudioDetail.aspx.cs" Inherits="DRMS.MirrorWeb.view.AudioDetail" %>

<%@ Register Src="~/UserControl/ZoomImages.ascx" TagName="zoom" TagPrefix="drms" %>
<%@ Register Src="~/UserControl/DataBaseListNavView.ascx" TagName="databaseNav" TagPrefix="drms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="../css/BookDetail.css" />
    <link href="../css/fancybox/source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <script src="../css/fancybox/source/jquery.fancybox.js" type="text/javascript"></script>
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
            $("#DataBase").parent().addClass("TYnav_active");
            $("#11").addClass("TYSubnav_actives");
            $("#basedatabase_a").siblings(".DBSubnavL_subordinate").show();
            $("#basedatabase_a").addClass("TYSubnav_actives");

            //注册查看大图的click事件
            function seeBigPic(src, title) {
                $.fancybox.open({
                    href: src,
                    title: title
                });
            }


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
    <script src="../js/jwplayer-6.6/jwplayer/jwplayer.js" type="text/javascript"></script>
    <script type="text/javascript">
        var thePlayer;  //保存当前播放器以便操作  
        $(function () {
            thePlayer = jwplayer('container').setup({
                flashplayer: "../js/jwplayer-6.6/jwplayer/jwplayer.flash.swf",
                file: "<%=AudioFileVirPath %>", //"http://content.bitsontherun.com/videos/lWMJeVvV-364767.mp4", 
                image: "<%=AudioCoverVirPath %>", //视频预览图片
                width: "100%",
                height: "200"
            });
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
                                    <span class="fr"></span><span class="TYcurrent">当前位置：数据库 >
                                        <%=DataBaseName %></span></p>
                            </div>
                            <div class="TYperiodical_List">
                                <asp:HiddenField ID="hdnQueryCon" runat="server" />
                                <!-- 加载 iframe 页面 -->
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
                                        <div style=' padding-left:150px; padding-right:150px;'>
                                            <div id="container" >
                                            </div>
                                        </div> 
                                        <div class="sgbMain">                                             
                                            <div class="sgbmr">
                                                <ul class="summary">
                                                    <asp:Literal ID="lt_summary" runat="server"></asp:Literal>
                                                </ul>                                                
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <div class="singleDetail">
                                        <h2>
                                            音频简介</h2>
                                        <div class="sm">
                                            <asp:Literal ID="lt_Digest" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                    </div>
                                    <asp:Literal ID="ltl_attachment" runat="server" EnableViewState="false"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hidDoi" runat="server" />
    <asp:HiddenField ID="hidPicDoi" runat="server" />
</asp:Content>
