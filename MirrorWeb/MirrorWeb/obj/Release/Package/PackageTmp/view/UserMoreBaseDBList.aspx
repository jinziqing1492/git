<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserDBNav.Master" AutoEventWireup="true" CodeBehind="UserMoreBaseDBList.aspx.cs" Inherits="DRMS.MirrorWeb.view.UserMoreBaseDBList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/TYcommon.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //页面加载中head的按钮特效
            $("#DataBase").parent().addClass("TYnav_active");
            $("#basedatabase_a").addClass("TYSubnav_actives");
            setSlideDownMenu("basedatabase_a");

            $("#determined").click(function () {
                linkto();
            });
            //点击时效果切换
            $("#test li").click(function () {
                var selected = $(this).attr("id");
                $("#" + selected).addClass("TYCchoice_pitchOn").siblings().removeClass("TYCchoice_pitchOn");
                linkto();
            });

            $.ajax({
                type: "GET",
                url: "../Ajax/GetDBResCount.ashx",
                data: "dbid=",
                dataType: "json",
                success: function (data) {
                    if (data) {
                        for (var i = 0; i < data.length; i++) {

                            $("#" + data[i].DBtype + "-m p").html("共有<em>" + data[i].Count + "</em>条数据");
                        }

                    }
                }
            });

        });

        
        function linkto() {

            var selected = $(".TYCchoice_pitchOn").attr("id");
            if (selected.indexOf("-m")) {
                selected = selected.replace("-m", "");
            }
            location.href = "../view/DataBaseList.aspx?dbtype=" + selected;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TYContainerR">
        <div class="TYcurrentDate">
            <p>
                <span class="fr">2013年7月16日 星期二</span> <span class="TYcurrent">当前位置：数据库管理 > 基础数据库</span>
            </p>
        </div>
        <div class="TYCchoice_database">
            <div class="TYContainerRText_title MT clearfix">
                <div class="TYCRTextTitle_left fl">
                </div>
                <div class="TYCRTextTitle_center fl">
                    请选择数据库</div>
                <div class="TYCRTextTitle_right fl">
                </div>
            </div>
            <div class="TYbaseDataBase_file  clearfix">
                <ul class="clearfix" id="test">
                    <asp:Literal Text="" ID="ltlbasedatabase" runat="server" />
                    <%--<li id="bookLib" class="TYCchoice_pitchOn">
                        <img src="../images/TYdatabaseIcon.png" />
                        <h5>
                            图书库</h5><p></p>
                    </li>
                    <li id="journalLib">
                        <img src="../images/TYdatabaseIcon.png" />
                        <h5>
                            期刊库</h5><p></p>
                    </li>
                    <li id="thesisLib">
                        <img src="../images/TYdatabaseIcon.png" />
                        <h5>
                            论文库</h5><p></p>
                    </li>
                    <li id="conferenceLib">
                        <img src="../images/TYdatabaseIcon.png" />
                        <h5>
                            论文集库</h5><p></p>
                    </li>
                    <li id="toolbookLib">
                        <img src="../images/TYdatabaseIcon.png" />
                        <h5>
                            工具书库</h5><p></p>
                    </li>
                    <li id="stdLib">
                        <img src="../images/TYdatabaseIcon.png" />
                        <h5>
                            标准库</h5><p></p>
                    </li>
                    <li id="yearLib">
                        <img src="../images/TYdatabaseIcon.png" />
                        <h5>
                            年鉴库</h5><p></p>
                    </li>
                    <li id="picLib">
                        <img src="../images/TYdatabaseIcon.png" />
                        <h5>
                            图片库</h5><p></p>
                    </li>
                    <li id="audioLib">
                        <img src="../images/TYdatabaseIcon.png" />
                        <h5>
                            音频库</h5><p></p>
                    </li>
                    <li id="videoLib">
                        <img src="../images/TYdatabaseIcon.png" />
                        <h5>
                            视频库</h5><p></p>
                    </li>
                     <li id="chapterLib">
                        <img src="../images/TYdatabaseIcon.png" />
                        <h5>
                            章节库</h5><p></p>
                    </li>--%>
                </ul>
            </div>
            <div class="TYContainer_Btn MT">
                <a href="javascript:void(0);" id="determined">确&nbsp;定</a>
            </div>
        </div>
    </div>
</asp:Content>
