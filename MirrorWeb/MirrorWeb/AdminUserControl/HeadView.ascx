<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeadView.ascx.cs" Inherits="DRMS.MirrorWeb.AdminUserControl.HeadView" %>
<link type="text/css" rel="stylesheet" href="../css/Add-resources.css" />
<script src="../js/singleLogin.js"></script>
<script src="../js/jquery.cookie.js"></script>
<script type="text/javascript">
    $(function () {
        $(".TYSearchI-right").click(function () {
            var txt = encodeURIComponent($("#headsearch").val());
            window.location.href = "/view/DBThemeNav.aspx?txt=" + txt ;
        });
    });
    function ctrl_keyDown() {
        if (event.keyCode == "13") {
            $(".TYSearchI-right").click();
        }
    }
    //退出按钮
    function esc() {
        location.href = "/Logout.aspx";
    }
</script>
<div class="TYLogoNav">
    <div class="TYlogoSearch">
        <div class="TYlogo">
            <img alt="" src="../images/TYlogo.png" /></div>
        <div class="TYSearch">
            <div class="TYSearchI-left fl">
            </div>
            <div class="TYSearchI-certen fl">
                <input id="headsearch" type="text" autocomplete="off" onkeydown="ctrl_keyDown();" /></div>
            <div class="TYSearchI-right fl">
            </div>
     <%--       <div class="TYadvanced_search fl">
                <a href="#"></a>
            </div>--%>
        </div>
    </div>
    <div class="TYnavInformation fr">
        <div class="TYInformation">
            <p>
               <%--您好:admin；当前角色：系统管理员 <a href="#">新信息<span>3</span></a> <a href="#">发站内信</a>--%>
                <%=RoleName %>
               <%--<a href="javascript:void(0)">您好:jnb</a><a href="javascript:void(0)" style="color:#fea500">进入用户中心</a><a href="javascript:void(0)">[退出]</a>--%>
            </p>
        </div>
        <ul class="TYnav fr" id="Head">
            <li>
                <a id="FirstPage" href="../Index.aspx">
                    <img alt="" src="../images/NAV_index.png" />
                    <h3>
                        首页</h3>
                </a>
            </li>

            <asp:Literal ID="ltlmytask" runat="server" EnableViewState="false"></asp:Literal>
            <li>
                <a id="DataBase" href="/view/UserMoreBaseDBList.aspx">
                    <img alt="" src="../images/NAV_database.png" />
                    <h3>
                        数据库</h3>
                </a>
            </li>
            <li><a id="ThemeView" href="../view/DBThemeNav.aspx">
                <img alt="" src="../images/NAV_theme.png" />
                <h3>
                    体系导航</h3>
            </a>
            </li>
            <asp:Literal ID="ltldataM" runat="server" EnableViewState="false"></asp:Literal>

            <li onclick="esc()">
                <img alt="" src="../images/NAV_out.png" />
                <h3>
                    退出</h3>
            </li>
        </ul>
    </div>
</div>
