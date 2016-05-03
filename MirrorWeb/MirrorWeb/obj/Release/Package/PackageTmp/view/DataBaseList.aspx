<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true" CodeBehind="DataBaseList.aspx.cs" Inherits="DRMS.MirrorWeb.view.DataBaseList" %>

<%@ Register Src="~/UserControl/DataBaseListNavView.ascx" TagName="databaseNav"  TagPrefix="drms"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/fancybox/source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <script src="../css/fancybox/source/jquery.fancybox.js" type="text/javascript"></script>
    <script src="../js/TYcommon.js" type="text/javascript"></script>
    <style type="text/css">
        .adv-search
        {
            color: #555555;
            line-height: 18px;
            padding: 0 3px;
            line-height: 26px;
            margin-left: 10px;
            text-decoration: underline !important;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        var type = QueryString("dbtype");
        $(function () {
            $("#ifrContent").attr("src", "../view/DatabaseContent.aspx?type=" + $("#<%=hdnQueryCon.ClientID %>").val() + "&searchword=" + $("<%=hdnSearchWord.ClientID %>").val());
            $("#DataBase").parent().addClass("TYnav_active");

            var type = QueryString("dbtype");

            if (type == 13 || type == 14 || type == 15 || type == 28) {
                $("#supportdatabase_a").parent().find("a[id='" + QueryString("dbtype") + "']").addClass("TYSubnav_actives");
                $("#supportdatabase_a").siblings(".DBSubnavL_subordinate").show();
                $("#supportdatabase_a").addClass("TYSubnav_actives");
                $("#advSearch_DB").hide();
            }
            else {
                $("#basedatabase_a").addClass("TYSubnav_actives");
                $("#basedatabase_a").parent().find("a[id='" + QueryString("dbtype") + "']").addClass("TYSubnav_actives");
                $("#basedatabase_a").siblings(".DBSubnavL_subordinate").show();
            }
            if (type == 16) {
                $(".big-searchbox").show();
                $("#advSearch_DB").hide();
            }
            else if (type == 17 || type == 18 || type == 19 || type == 22) {
                //$(".big-searchbox").show();
                $("#advSearch_DB").hide();
                $("#btn_advSearch").show();
            }
//            if (type == 16) {
//                $(":radio['book_radio_search'][value=1]").prop("checked", false);
//                $(":radio['book_radio_search'][value=16]").prop("checked", true);
//                //$(":radio[.book][value=1]").prop("checked", false);//这3种写法都可以
//                //$(":radio[.book][value=16]").prop("checked", true);//这3种写法都可以
//                //$(":radio[name='ctl00$ContentPlaceHolder1$book_radio_search'][value=1]").prop("checked", false);
//                //$(":radio[name='ctl00$ContentPlaceHolder1$book_radio_search'][value=16]").prop("checked", true);                
//            }
            if (type == 18) {
                $(":radio['journal_radio_search'][value=4]").prop("checked", false);
                $(":radio['journal_radio_search'][value=18]").prop("checked", true);
            }
            else if (type == 19) {
                $(":radio['Conference_radio_search'][value=5]").prop("checked", false);
                $(":radio['Conference_radio_search'][value=19]").prop("checked", true);
            }
            else if (type == 22) {
                $(":radio['toolbook_radio_search'][value=3]").prop("checked", false);
                $(":radio['toolbook_radio_search'][value=22]").prop("checked", true);
            }

            $("#advSearch_DB").attr("href", $("#advSearch_DB").attr("href") + type);


        });

        function showBigBtn(args) {
            if (args == 1) {
                $("#btn_advSearch").show();
            }
            else {
                $("#btn_advSearch").hide();
            }
        }
        function showBigDiv() {
            $.fancybox.open({
                href: "#text_searchbox",
                title: ""
            });
        }

        function searchData() { 
            var type = $("input[type=radio]:checked").val();
            var doctype = "";
            if (type == "16")
                doctype = 0;
            else if (type == "17")
                doctype = 1;
            if (type == undefined)
                type = $("#<%=hdnQueryCon.ClientID %>").val();
            var keyword = decodeURIComponent($("#tbxTitle").val());
            $("#ifrContent").attr("src", "../view/DatabaseContent.aspx?type=" + type + "&keyword=" + keyword + "&searchword=" + $("<%=hdnSearchWord.ClientID %>").val() + "&doctype=" + doctype);
        }
        // 回车搜索
        function Search_keyDown() {
            var lKeyCode = (navigator.appname == "Netscape") ? event.which : event.keyCode;
            if (lKeyCode == "13") {
                searchData();
                return false;
            }
        }

        function searchBigData() {
            var sql = $("#<%=hdnQueryCon.ClientID %>").val();
            var tbxbigtext = $("#tbxBig").val();
            if (tbxbigtext && tbxbigtext.length < 200) {
                var keyword = decodeURIComponent("@big@" + tbxbigtext);

                $.fancybox.close({
                    href: "#text_searchbox"
                });
                var istype = $("input[type=radio]:checked").val();
                var doctype = "";
                if (istype == "16")
                    doctype = 0;
                else if (istype == "17")
                    doctype = 1;
                var type = $("#<%=hdnQueryCon.ClientID %>").val();
                if (type == "1")
                { type = "16"; }
                if (type == "2")
                { type = "17"; }
                if (type == "3")
                { type = "22"; }
                if (type == "4")
                { type = "18"; }
                if (type == "5")
                { type = "19"; }
                $("#ifrContent").attr("src", "../view/DatabaseContent.aspx?type=" + type + "&keyword=" + keyword + "&searchword=" + $("<%=hdnSearchWord.ClientID %>").val() + "&doctype=" + doctype);
            }
            else {
                alert("最大长度为200字符，谢谢！");
            }
        }
        // 回车搜索
        function BigSearch_keyDown() {
            var lKeyCode = (navigator.appname == "Netscape") ? event.which : event.keyCode;
            if (lKeyCode == "13") {
                searchBigData();
                return false;
            }
        }

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
                                        <%=DataBaseName %></span>
                                </p>
                            </div>
                            <div class="TYchoiceProcess MT">
                                <div class="bc-search" style="margin-bottom: 10px;">
                                    <div class="bcs-left">
                                        <div class="bcs-right">
                                            <div class="bcs-main" style="padding-left: 20px;">
                                                <div class="bcs-radio-div">
                                                    <asp:RadioButton ID="book_radio" value="1" Text="图书" GroupName="book_radio_search" Checked="true"
                                                        runat="server" onclick="showBigBtn(0)" />
                                                    <asp:RadioButton ID="chapter_radio" value="16" Text="章节" GroupName="book_radio_search" runat="server"
                                                        onclick="showBigBtn(1)" />
                                                    <asp:RadioButton ID="std_radio" value="2" Text="标准" GroupName="book_radio_search" Checked="true" class="book"
                                                        runat="server" onclick="showBigBtn(0)" />
                                                    <asp:RadioButton ID="stdchapter_radio" value="17" Text="章节" GroupName="book_radio_search" runat="server"  class="book"
                                                        onclick="showBigBtn(1)" />
                                                    <asp:RadioButton ID="Conference_radio" value="5" Text="论文集" GroupName="Conference_radio_search"
                                                        Checked="true" runat="server" onclick="showBigBtn(0)" />
                                                    <asp:RadioButton ID="article_radio" value="19" Text="文章" GroupName="Conference_radio_search"
                                                        runat="server" onclick="showBigBtn(1)" />
                                                    <asp:RadioButton ID="toolbook_radio" value="3" Text="工具书" GroupName="toolbook_radio_search"
                                                        Checked="true" runat="server" onclick="showBigBtn(0)" />
                                                    <asp:RadioButton ID="entry_radio" value="22" Text="词条" GroupName="toolbook_radio_search" runat="server"
                                                        onclick="showBigBtn(1)" />
                                                    <asp:RadioButton ID="journal_radio" value="4" Text="期刊" GroupName="journal_radio_search"
                                                        Checked="true" runat="server" onclick="showBigBtn(0)" />
                                                    <asp:RadioButton ID="journal_article_radio" value="18" Text="文章" GroupName="journal_radio_search" runat="server"
                                                        onclick="showBigBtn(1)" />
                                                </div>
                                                <div class="bcs-box">
                                                    <input type="text" id="tbxTitle" onkeydown="Search_keyDown();" />
                                                </div>
                                                <div style="float:left;margin-right:20px;">
                                                <input type="button" value="检 索" class="btn-news" onclick="searchData()" />
                                                <input type="button" value="大文本检索" class="btn-news" style="display: none;" onclick="showBigDiv()"
                                                    id="btn_advSearch" />
                                                <%--<a class="adv-search" id="advSearch_DB" href="AdvancedDBThemeNav.aspx?dbtype=" target="_self">高级检索</a>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="big-searchbox clearfix" id="text_searchbox" style="display: none;">
                                    <span></span>
                                    <textarea id="tbxBig" rows="5" cols="5" onkeydown="BigSearch_keyDown();"></textarea>
                                    <input type="button" value="大文本检索" class="btn-news" onclick="searchBigData()" />
                                </div>
                            </div>
                            <div class="TYperiodical_List">
                                <div class="TYContainerRText_title MT clearfix">
                                    <div class="TYCRTextTitle_arTab fl">
                                        <div class="TYCRTextTitle_left fl">
                                        </div>
                                        <div class="TYCRTextTitle_center fl">
                                            资源列表
                                        </div>
                                        <div class="TYCRTextTitle_right fl">
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdnQueryCon" runat="server" />
                                <asp:HiddenField ID="hdnSearchWord" runat="server" />
                                <!-- 加载 iframe 页面 -->
                                <iframe id="ifrContent" marginwidth="0" onload="Javascript:$('#ifrContent').height($('#ifrContent').contents().find('body').height()+15)"
                                    width="100%" height="500px" marginheight="0" frameborder="0" scrolling="no"></iframe>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
