<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserDBNav.Master" AutoEventWireup="true" CodeBehind="UserMoreAppDBList.aspx.cs" Inherits="DRMS.MirrorWeb.view.UserMoreAppDBList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/TYcommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //页面加载中head的按钮特效
            $("#DataBase").parent().addClass("TYnav_active");
            $("#appdatabase_a").addClass("TYSubnav_actives");
            setSlideDownMenu("appdatabase_a");

            $(".TYbaseDataBase_file ul li").mouseenter(function () {
                $(this).children(".logicDBOpe").show();
            });
            $(".TYbaseDataBase_file ul li").mouseleave(function () {
                $(".logicDBOpe").hide();
            });
        });

         
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TYContainerR">
        <div class="TYcurrentDate">
            <p>
                <span class="fr">2013年7月16日 星期二</span> <span class="TYcurrent">当前位置：数据库 > 业务应用数据库</span>
            </p>
        </div>
        <asp:HiddenField ID="HiddenFieldDeleteID" runat="server" />
        <div class="TYCchoice_database">
            <div class="TYContainerRText_title MT clearfix">
                <div class="TYCRTextTitle_left fl">
                </div>
                <div class="TYCRTextTitle_center fl">
                    业务应用库</div>
                <div class="TYCRTextTitle_right fl">
                </div>
            </div>
            <div class="TYbaseDataBase_file  clearfix">
                <ul>
                    <asp:Literal ID="ltlldlist" runat="server" EnableViewState="false"></asp:Literal>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
