<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true" CodeBehind="PicDetail.aspx.cs" Inherits="DRMS.MirrorWeb.view.PicDetail" %>
<%@ Register Src="~/UserControl/ZoomImages.ascx" TagName="zoom" TagPrefix="drms" %>
<%@ Register Src="~/UserControl/DataBaseListNavView.ascx" TagName="databaseNav" TagPrefix="drms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="../css/BookDetail.css" />
    <link href="../css/fancybox/source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <script src="../css/fancybox/source/jquery.fancybox.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SetImgAutoSize(obj) {
            var img = obj;
            var MaxWidth = $(img).parent().width();  //设置图片宽度界限 
            var MaxHeight = $(img).parent().height(); //设置图片高度界限 
            var HeightWidth = img.offsetHeight / img.offsetWidth; //设置高宽比
            var MaxHeightWidth = MaxHeight / MaxWidth;

            if ($.browser.msie) {
                if (img.readyState != "complete") return false; //确保图片完全加载 
            }
            if ($.browser.mozilla) {
                if (img.complete == false) return false;
            }

            if (HeightWidth < MaxHeightWidth && img.offsetWidth > MaxWidth) {
                img.width = MaxWidth;
            }
            else if (HeightWidth > MaxHeightWidth && img.offsetHeight > MaxHeight) {
                img.height = MaxHeight;
            }
            //设置图片居中
            var marginTop = ($(img).parent().height() - img.height) / 2;
            $(img).css("margin-top", marginTop);
        }

        $(function () {
            $("#DataBase").parent().addClass("TYnav_active");
            $("#12").addClass("TYSubnav_actives");
            $("#basedatabase_a").siblings(".DBSubnavL_subordinate").show();
            $("#basedatabase_a").addClass("TYSubnav_actives");

            //注册查看大图的click事件
            $("#zoom_images_click_a").click(function () {
                $.fancybox.open({
                    href: "<%=this.ctrl_Zoom.BigImgSrc %>",
                    title: ""
                });
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
                                        <div class="sgbMain">
                                            <div class="sgbml">
                                                <drms:zoom ID="ctrl_Zoom" runat="server" />
                                            </div>
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
                                        <h2>图片简介</h2>
                                        <div class="sm">
                                            <asp:Literal ID="lt_Digest" runat="server" EnableViewState="false"></asp:Literal>
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
    <asp:HiddenField ID="hidPicDoi" runat="server" />
</asp:Content>
