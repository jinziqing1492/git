<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true" CodeBehind="DBThemeNav.aspx.cs" Inherits="DRMS.MirrorWeb.view.DBThemeNav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
    <script src="../js/jquery.ztree.all-3.1.js" type="text/javascript"></script>
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .adv-search {
            color: #555555;
            line-height: 18px;
            padding: 0 3px;
            line-height: 26px;
            margin-left: 10px;
            text-decoration: underline !important;
            cursor: pointer;
        }
        .ownerres{ text-align:left !important; padding-left:20px; display:none;}
    </style>
    <script type="text/javascript">
        $(function () {
            $("#ThemeView").parent().addClass("TYnav_active");
            $("#ifrContent").attr("src", "../view/DatabaseContent.aspx?type=" + $("#<%=hdnQueryCon.ClientID %>").val() + "&searchword=" + $("<%=hdnSearchWord.ClientID %>").val());
            var txt = QueryString("txt");
            if (txt != null && txt != "") {
                $("#tbxTitle").val(decodeURIComponent(txt));
            }
            var key = QueryString("searchword");
            if (key != null && key != "") {
                $("#tbxTitle").val(decodeURIComponent(key));
            }
            $("#<%=hdnSearchWord.ClientID %>").val(key);


        });

        function searchData() {
            var sql = $("#<%=hdnQueryCon.ClientID %>").val();
            var keyword = $("#tbxTitle").val();
            var tabs_select;
            $("#tabs div").each(function () {
                if (!$(this).hasClass("ui-tabs-hide")) {
                    tabs_select = $(this);
                }
            });
            $("#ifrContent").attr("src", "../view/DatabaseContent.aspx?type=" + $("#<%=hdnQueryCon.ClientID %>").val() + "&keyword=" + keyword);
        }
        // 回车搜索
        function Search_keyDown() {
            var lKeyCode = (navigator.appname == "Netscape") ? event.which : event.keyCode;
            if (lKeyCode == "13") {
                searchData();
                
            }
        }
         
    </script>
    <script type="text/javascript">
        var activeIndex = 0;
        var iframeHeight = 860;
        var iframeWidth = 1005;
        var contentUrlFormat = "DatabaseContent.aspx?type={0}&keyword={1}&searchword={2}";
        var contentUrlQueryFormat = "DatabaseContent.aspx?type={0}&keyword={1}&queryConn={2}&searchword={3}&selectValue={4}&orderField={5}&second={6}&owner={7}";
        $(function () {
            var sql = $("#hid_theme").val();
            if(!sql){//读取url中的参数
                var classid=QueryString("classid");
                if(classid)
                {
                    sql="SYS_FLD_CLASSFICATION='" + classid + "?'";
                }
            }
            var selectValue=QueryString("selectValue");
            var search = QueryString("searchword");
            var keyword="";
            if (search == null || search == "undefined"||search=="") {
                keyword = $("#tbxTitle").val();
            }
            else {
                search = decodeURIComponent($("#tbxTitle").val());
            }
            var activeItem=$("#tabs-1");
            var resource=QueryString("resource");
            var index=0;

            //自有资源搜索
            //判断检索词是否不为空 如果不为空则搜索文章
            var isowner = "0";
            if(resource=="64" && keyword)
            {
                resource = "18";
                showOwner();
                selectOwner();
                isowner = "1";
            }

            $("#tabs ul a").each(function(){
                if($(this).attr("field")==resource||index==0){
                    activeIndex=index;
                    activeItem=$($(this).attr("href"));
                }
                index++;
            });
            //  tab选项卡
            $("#tabs").tabs({
                selected: activeIndex,
                select: function (event, ui) {
                    $("#sel_orderField").val("");
                    AppendIframeContent($(ui.tab), $(ui.panel), sql, "", search, "all");
                }
            });
            //页面初始化加载数据
            AppendIframeContent($("li.ui-tabs-selected a"), activeItem, sql, keyword, search,selectValue,0,isowner);
        });

        //添加内容iframe
        function AppendIframeContent($tab, $panel, sqlConn, keyword, search,selectValue,second,owner) {
            if ($tab.length == 0 || $panel.length == 0) {
                return;
            }
            var type = $tab.attr("field");
            if(!keyword&&keyword!=undefined){
                keyword=$("#tbxTitle").val();
            }
            var orderField = $("#sel_orderField").val();
            var url = String.Format(contentUrlQueryFormat, type, encodeURIComponent(keyword || ""), sqlConn, encodeURIComponent(search || ""),selectValue,orderField,second,owner);
            if ($panel.children("iframe").length == 0) {
                $panel.empty();
                var iframeContent = "<iframe id=\"ifr\" onload=\"Javascript:refreshHeiht();\" allowtransparency=\"true\" marginwidth=\"0\" field=\"" + type + "\" width=\"100%\" height=\"" + iframeHeight + "\" framespacing=\"0\" marginheight=\"0\" frameborder=\"0\" src=\"";
                iframeContent += url;
                iframeContent += "\" ></iframe>";
                $panel.append(iframeContent);
            }
            else if (keyword!=undefined) {
                var secondwhere =$panel.children("iframe").contents().find("input[id$=hid_where]").val();
                if(secondwhere)
                {
                    url += "&secondwhere=" + encodeURIComponent(secondwhere);
                }
                $panel.children("iframe").attr("src", url);
            }
            loadSelect(type,selectValue);
            if(orderField)
            {
                loadOrder(type,orderField);
            }
            showOwner(type);
        }
        function searchData() {
            var sql = $("#hid_theme").val();
            var keyword = decodeURIComponent($("#tbxTitle").val());
            var tabs_select;
            $("#tabs div").each(function () {
                if (!$(this).hasClass("ui-tabs-hide")) {
                    tabs_select = $(this);
                }
            });
            var selectValue=$("#sel_selectValue").val();
            var owner=$("#ck_owner").is(':checked')==true?"1":"0";
            AppendIframeContent($("li.ui-tabs-selected a"), tabs_select, sql, keyword,"", selectValue,0,owner);
        }
        function searchDataCondition() {
            var sql = $("#hid_theme").val();
            var keyword = decodeURIComponent($("#tbxTitle").val());
            var tabs_select;
            $("#tabs div").each(function () {
                if (!$(this).hasClass("ui-tabs-hide")) {
                    tabs_select = $(this);
                }
            });
            var selectValue=$("#sel_selectValue").val();
            var owner=$("#ck_owner").is(':checked')==true?"1":"0";
            AppendIframeContent($("li.ui-tabs-selected a"), tabs_select, sql, keyword,"", selectValue,1,owner);
        }
        function searchByTheme(classid) {
            //执行事件
            if (classid) {
                var sql = "SYS_FLD_CLASSFICATION='" + classid + "?'";
            }
            else {
                sql = "";
            }
            $("#hid_theme").val(sql);
            var tabs_select;
            $("#tabs div").each(function () {
                if (!$(this).hasClass("ui-tabs-hide")) {
                    tabs_select = $(this);
                }
            });
            AppendIframeContent($("li.ui-tabs-selected a"), tabs_select, sql, "");
        }

        function refreshHeiht() {

            var fch = $('#ifr').contents().find('body').height();
            if (fch == 0) {
                $('#ifr').height(855 + 25);
            }
            else {
                $('#ifr').height($('#ifr').contents().find('body').height() + 25)
            }
        }

        function loadSelect(field,selectValue)
        {
            $("#sel_selectValue").empty();
            switch(field)
            {
                case "16":
                case "18":
                    $("#sel_selectValue").append(
                        "<option value='title'>标题</option>"+
                        "<option value='content'>全文</option>");
                    break;
                case "1":
                    $("#sel_selectValue").append(
                        "<option value='all'>全部</option>"+
                        "<option value='title'>标题</option>"+
                        "<option value='author'>作者</option>");
                    break;
                default:
                    $("#sel_selectValue").append(
                        "<option value='all'>全部</option>"+
                        "<option value='title'>标题</option>");
                    break;
            }
            $("#sel_selectValue").val(selectValue);
        }
        function loadOrder(field,selectValue)
        {
            $("#sel_orderField").empty();
            switch(field)
            {
                case "16":
                    $("#sel_orderField").append(
                        "<option value='TITLE'>标题</option>"+
                        "<option value='CONTENT'>全文</option>"+
                        "<option value='KEYWORDS'>关键字</option>");
                    break;
                case "22":
                    $("#sel_orderField").append(
                        "<option value='NAME'>标题</option>"+
                        "<option value='CONTENT'>全文</option>"+
                        "<option value='FINDDATE DESC'>入库时间倒序</option>"+
                        "<option value='FINDDATE'>入库时间正序</option>");
                    break;
                case "70":
                case "18":
                    $("#sel_orderField").append(
                        "<option value='SYS_SYSID DESC'>入库时间倒叙</option>"+
                        "<option value='SYS_SYSID'>入库时间正序</option>"+
                        "<option value='TITLE'>标题</option>"+
                        "<option value='CONTENT'>全文</option>"+
                        "<option value='KEYWORDS'>关键字</option>"+
                        "<option value='YEARISSUE DESC'>年期倒序</option>"+
                        "<option value='YEARISSUE'>年期正序</option>");
                    break;
                case "1":
                    $("#sel_orderField").append(
                        "<option value='NAME'>标题</option>"+
                        "<option value='AUTHOR'>作者</option>"+
                        "<option value='ISSUEDEP'>出版单位</option>"+
                        "<option value='ISSUEDATE DESC'>出版时间倒序</option>"+
                        "<option value='ISSUEDATE'>出版时间正序</option>"+
                        "<option value='SYS_FLD_ADDDATE DESC'>入库时间倒序</option>"+
                        "<option value='SYS_FLD_ADDDATE'>入库时间正序</option>");
                    break;
                case "4":
                case "60":
                case "64":
                    $("#sel_orderField").append(
                        "<option value='SYS_SYSID DESC'>入库时间倒叙</option>"+
                        "<option value='SYS_SYSID'>入库时间正序</option>"+
                        "<option value='NAME'>标题</option>"+
                        "<option value='FOUNDDATE DESC'>建刊时间倒序</option>"+
                        "<option value='FOUNDDATE'>建刊时间正序</option>");
                    break;
                case "12":
                    $("#sel_orderField").append(
                        "<option value='NAME'>标题</option>"+
                        "<option value='PICSIZE'>图片大小正序</option>"+
                        "<option value='PICSIZE DESC'>图片大小倒序</option>"+
                        "<option value='SYS_FLD_ADDDATE DESC'>入库时间倒序</option>"+
                        "<option value='SYS_FLD_ADDDATE'>入库时间正序</option>");
                    break;
                default:
                    $("#sel_orderField").append(
                        "<option value='title'>标题</option>");
                    break;
            }
            $("#sel_orderField").val(selectValue);
        }

        function showOwner(field)
        {
            <%if(IsDisplayOwner){%>
                if(field=="18")
                {
                    $(".ownerres").show();
                }
                else{
                    $(".ownerres").hide();
                }
            <%}%>
        }
        function selectOwner()
        {
            $("#ck_owner").attr("checked","checked");
        }
        function changeOrder(){
            searchData();
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
                searchByTheme(treeNode.id);
            }
        }
    };
    $(document).ready(function () {
        var t = $("#tree");
        t = $.fn.zTree.init(t, setting, <%=Nodes %>);
        var classid=QueryString("classid");
        if(classid)
        {
            var treeObj = $.fn.zTree.getZTreeObj("tree");
            var node = treeObj.getNodeByParam("id", classid, null);
            treeObj.selectNode(node);
        }
    });
    //-->
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TYContainer_page">
        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="container_div_minHeight">
            <tr>
                <td class="TYSubnavL">
                    <div style="width: 245px; float: left; border: none; min-height: 500px;">
                        <div class="ChapterTitle">
                            <span>分类体系</span>
                        </div>
                        <ul id="tree" class="ztree" style="overflow: scroll;">
                        </ul>
                    </div>
                </td>
                <td class="TYRconter">
                    <div class="TYContainerRBG">
                        <div class="TYContainerR">
                            <div class="TYcurrentDate">
                                <p>
                                    <span class="fr"></span><span class="TYcurrent">当前位置：体系导航
                                    </span>
                                </p>
                            </div>
                            <div class="TYchoiceProcess MT">
                                <div class="bc-search">
                                    <div class="bcs-left">
                                        <div class="bcs-right">
                                            <div class="bcs-main" style="padding-left: 20px;">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 700px;">
                                                            <select style="width: 60px; margin-left: 10px;" id="sel_selectValue">
                                                                <option value="all">全部</option>
                                                                <option value="title">标题</option>
                                                                <option value="content">内容</option>
                                                                <option value="author">作者</option>
                                                            </select>
                                                            <div class="bcs-box">
                                                                <input type="text" id="tbxTitle" onkeydown="Search_keyDown();" />
                                                            </div>
                                                            <input type="button" value="检 索" class="btn-news" onclick="searchData()" />
                                                            <a class="adv-search" href="javascript:void(0)" onclick="searchDataCondition()">在结果内搜索</a>
                                                            <a class="adv-search" href="#" onclick="javascript:self.open('AdvancedDBThemeNav.aspx','_self','');">高级检索</a>
                                                        </td>
                                                        <td class="ownerres">
                                                            <input type="checkbox" id="ck_owner" value="true"/>
                                                            <span>只搜索泛华资源</span>
                                                        </td>
                                                        <td>
                                                            <div style="float: right; width: 200px;">
                                                                <select id="sel_orderField" style="float: right; margin-left: 10px;" onchange="changeOrder()">
                                                                    <option value="">标题</option>
                                                                    <option value="">作者</option>
                                                                    <option value="">全文</option>
                                                                </select>
                                                                排序方式：   
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="TYperiodical_List">
                                <asp:HiddenField ID="hdnQueryCon" runat="server" />
                                <asp:HiddenField ID="hdnSearchWord" runat="server" />
                                <div id="tabs" style="width: 100%; float: left;" class="noBackg">
                                    <ul style="padding: 0.2em 0.2em 0px 0.2em;">
                                        <li style="margin: 8px 20px 0px 20px; padding: 0px;"><span style="color: #6a6a6a; font-weight: normal;">资源列表</span></li>
                                        <asp:Literal ID="ltlbasedatabaseview" runat="server" />
                                        <%--<li style="padding: 0px;"><a href="#tabs-1" field="1">图书</a></li>
                                        <li style="padding: 0px;"><a href="#tabs-2" field="2">标准</a></li>
                                        <li style="padding: 0px;"><a href="#tabs-3" field="3">工具书</a></li>
                                        <li style="padding: 0px;"><a href="#tabs-4" field="4">期刊</a></li>
                                        <li style="padding: 0px;"><a href="#tabs-5" field="5">会议论文</a></li>
                                        <li style="padding: 0px;"><a href="#tabs-6" field="6">年鉴</a></li>
                                        <li style="padding: 0px;"><a href="#tabs-9" field="9">学位论文</a></li>
                                        <li style="padding: 0px;"><a href="#tabs-10" field="10">视频</a></li>
                                        <li style="padding: 0px;"><a href="#tabs-11" field="11">音频</a></li>
                                        <li style="padding: 0px;"><a href="#tabs-12" field="12">图片</a></li>--%>
                                    </ul>
                                    <asp:Literal ID="ltlDivTabs" runat="server" />
                                    <%--<div id="tabs-1" class="search-result-content">
                                    </div>
                                    <div id="tabs-2" class="search-result-content">
                                    </div>
                                    <div id="tabs-3" class="search-result-content">
                                    </div>
                                    <div id="tabs-4" class="search-result-content">
                                    </div>
                                    <div id="tabs-5" class="search-result-content">
                                    </div>
                                    <div id="tabs-6" class="search-result-content">
                                    </div>
                                    <div id="tabs-9" class="search-result-content">
                                    </div>
                                    <div id="tabs-10" class="search-result-content">
                                    </div>
                                    <div id="tabs-11" class="search-result-content">
                                    </div>
                                    <div id="tabs-12" class="search-result-content">
                                    </div>--%>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="hid_theme" />
</asp:Content>
