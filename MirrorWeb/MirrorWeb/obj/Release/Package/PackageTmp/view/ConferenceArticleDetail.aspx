<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true" CodeBehind="ConferenceArticleDetail.aspx.cs" Inherits="DRMS.MirrorWeb.view.ConferenceArticleDetail" %>
<%@ Register Src="~/UserControl/DataBaseListNavView.ascx" TagName="databaseNav" TagPrefix="drms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/base.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function SetImgAutoSize(obj, width, height) {
            //var img=document.all.img1;//获取图片 
            var img = obj;
            var MaxWidth = width; //设置图片宽度界限 
            var MaxHeight = height; //设置图片高度界限 
            var HeightWidth = img.offsetHeight / img.offsetWidth; //设置高宽比 
            var WidthHeight = img.offsetWidth / img.offsetHeight; //设置宽高比 
            //if (img.readyState != "complete") return false; //确保图片完全加载  
            if ($.browser.msie) {
                if (img.readyState != "complete") return false; //确保图片完全加载 
            }
            if ($.browser.mozilla) {
                if (img.complete == false) return false;
            }
            if (img.offsetWidth > MaxWidth) {
                img.width = MaxWidth;
            }
        }
        $(document).ready(function () {
            //页面加载中head的按钮特效              
            $("#DataBase").parent().addClass("TYnav_active");
            $("#basedatabase_a").addClass("TYSubnav_actives");
            $("#basedatabase_a").siblings(".DBSubnavL_subordinate").show();

            $(".frmContent p > img").each(function () {
                $(this).parent().addClass("center");
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
                                    <span class="fr"></span><span class="TYcurrent">当前位置：会议论文集文章浏览 </span></p>
                            </div>
                            <div class="TYperiodical_List">
                                <asp:HiddenField ID="hdnQueryCon" runat="server" ClientIDMode="Static"/>
                                <div class="readeRight">
                                    <div>
                                        <span>文章来源：<asp:Literal ID="lt_title" runat="server"></asp:Literal></span>
                                    </div>
                                </div>
                                <div class="frmContent">
                                    <asp:Literal ID="lt_content" runat="server" EnableViewState="false"></asp:Literal>
                                </div>
                                <div class="TYContainer_Btn MT">
                                    <a href="javascript:window.history.go(-1)">返&nbsp;回</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
