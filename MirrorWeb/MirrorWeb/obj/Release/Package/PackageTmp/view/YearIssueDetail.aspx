<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true" CodeBehind="YearIssueDetail.aspx.cs" Inherits="DRMS.MirrorWeb.view.YearIssueDetail" %>
<%@ Register Src="~/UserControl/DataBaseListNavView.ascx" TagName="databaseNav" TagPrefix="drms" %>
<%@ Register Src="~/UserControl/ZoomImages.ascx" TagName="zoom" TagPrefix="drms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="../css/BookDetail.css" />
    <link href="/css/magazineSearch.css" rel="stylesheet" type="text/css" />
    <link href="/css/base.css" rel="stylesheet" type="text/css" />
    <link href="../css/fancybox/source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <script src="../css/fancybox/source/jquery.fancybox.js" type="text/javascript"></script>
    <script src="../js/base.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //页面加载中head的按钮特效              
            $("#DataBase").parent().addClass("TYnav_active");
            $("#basedatabase_a").addClass("TYSubnav_actives");
            $("#basedatabase_a").parent().find("a[id='" + QueryString("type") + "']").addClass("TYSubnav_actives");
            $("#basedatabase_a").siblings(".DBSubnavL_subordinate").show();
            //注册查看大图的click事件
            $("#zoom_images_click_a").click(function () {
                $.fancybox.open({
                    href: "<%=this.myctrl_Zoom.BigImgSrc %>",
                    title: ""
                });
            });
        });
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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TYContainer_page">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="container_div_minHeight">
            <tr>
                <td class="TYSubnavL">
                    <drms:databasenav ID="mydatabasenav" runat="server" />
                </td>
                <td class="TYRconter">
                    <div class="TYContainerRBG">
                        <div class="TYContainerR">
                            <div class="TYcurrentDate">
                                <p>
                                    <span class="fr"></span><span class="TYcurrent">当前位置：数据库 > 基础数据库 >
                                        <%=DataBaseName %></span></p>
                            </div>
                            <div class="TYperiodical_List">
                                <div id="noResult" runat="server" style='margin: 15px auto; width: 300px; height: 50px;
                                    text-align: center; color: Red; padding-top: 20px; border: 1px solid #aed0ea;' visible="false">
                                    <p style='margin: auto;'>
                                        该刊物不存在具体资源，请浏览其它刊物</p>
                                </div>
                                <div id="haveResult" runat="server" visible="true">
                                    <div class="singleBook">
                                        <div class="sgbName">
                                            <asp:Literal ID="lt_Title" runat="server"></asp:Literal>
                                        </div>                                        
                                        <div class="sgbMain">
                                            <div class="sgbml">
                                                <drms:zoom ID="myctrl_Zoom" runat="server" />
                                            </div>                                         
                                            <div class="sgbmr">
                                                <ul class="summary">
                                                    <asp:Literal ID="lt_summary" runat="server"></asp:Literal>
                                                </ul>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="singleDetail">
                                        <h2><%=filename%>——内容简介</h2>
                                        <div class="sm">
                                            <asp:Literal ID="lt_Digest" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                    </div>
                                        <div class="singleDetail">
                                        <h2><%=filename%>——按期阅读</h2>
                                            <div class="sm">
                                                <asp:Literal ID="lt_catalog" runat="server" EnableViewState="false"></asp:Literal>
                                            </div>
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
</asp:Content>
