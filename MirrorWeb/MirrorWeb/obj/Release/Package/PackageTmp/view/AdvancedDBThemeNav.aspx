<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true" CodeBehind="AdvancedDBThemeNav.aspx.cs" Inherits="DRMS.MirrorWeb.view.AdvancedDBThemeNav" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/cupertino/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
    <script src="../js/jquery.ztree.all-3.1.js" type="text/javascript"></script>
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .class-all
        {
            margin: 5px auto 5px 5px;
            z-index: 999;
            position: relative;
            float: left;
            height: 35px;
            line-height: 35px;
            width: 110px;
            border-top: 1px solid #cbcccc;
            border-bottom: 1px solid #cbcccc;
            border-left: 1px solid #cbcccc;
            border-right: 1px solid #cbcccc;
            color: #0f86d2;
            cursor: pointer;
        }

            .class-all input.btn-class-select
            {
                margin: 9px 0 0 5px;
                height: 19px;
                width: 19px;
                border: 0px;
                padding: 0px;
                background-image: url(../images/btn-select.png);
                cursor: pointer;
            }

            .class-all ul.class-list-nav
            {
                display: none;
                top: 35px;
                left: -1px;
                position: absolute;
                list-style-type: none;
                width: 110px;
                float: left;
                overflow: hidden;
                border-bottom: 1px solid #CBCCCC;
                border-left: 1px solid #CBCCCC;
                border-right: 1px solid #CBCCCC;
                background-color: #F5F5F5;
            }

                .class-all ul.class-list-nav li
                {
                    width: 90px;
                    float: left;
                    color: #045faa;
                    padding-left: 10px;
                }

                    .class-all ul.class-list-nav li a
                    {
                        color: #045faa;
                    }

                        .class-all ul.class-list-nav li a:hover
                        {
                            color: #ec4d44;
                        }
    </style>
    <script type="text/javascript">
        var contentUrlQueryFormat = "AdvancedDBContent.aspx?type={0}&queryConn={1}";
        var sql = ($("#hid_theme").val() == undefined) ? "" : $("#hid_theme").val();

        $(function () {
            $("#ThemeView").parent().addClass("TYnav_active");
            //ztree中的url参数

            var dbtype = QueryString("dbtype");
            if (!dbtype) { dbtype = "1"; }

            //$("#ifr").attr("src", String.Format(contentUrlQueryFormat, dbtype, sql));

            $(".class-list-nav").hide();
            $(".btn-class-select").click(function () {
                $(".class-list-nav").toggle();
            });
            $(".class-list-nav li").click(function () {
                var nav = $(this).attr("id");
                var txt = $(this).text();
                $("#<%=hdnTypeId.ClientID %>").val(nav);
                $("#spanSelect").text(txt);
                $(".class-list-nav").hide();
                //alert(sql);
                var url = String.Format(contentUrlQueryFormat, nav, sql);

                $("#ifr").attr("src", url);

            });
            $(".class-list-nav li[id='" + dbtype + "']").click();
        })

        function searchByTheme(classid) {
            //执行事件
            if (classid) {
                sql = "SYS_FLD_CLASSFICATION='" + classid + "?'";
            }
            else {
                sql = "";
            }
            $("#hid_theme").val(sql);
            var url = String.Format(contentUrlQueryFormat, $("#<%=hdnTypeId.ClientID %>").val(), sql)
            $('#ifr').attr('src', url);
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
                        <ul id="tree" class="ztree" style="overflow: visible;">
                        </ul>
                    </div>
                </td>
                <td class="TYRconter">
                    <div class="TYContainerRBG">
                        <div class="TYContainerR">
                            <div class="TYcurrentDate">
                                <p>
                                    <span class="fr"></span><span class="TYcurrent">当前位置：体系导航：高级导航
                                    </span>
                                </p>
                            </div>

                            <div class="TYchoiceProcess MT">
                                <div class="class-all">
                                    <span style="float: left; padding-left: 10px; width: 70px;" id="spanSelect">图书</span>
                                    <input type="button" value="" class="btn-class-select" />
                                    <ul class="class-list-nav">
                                        <%--<li id="1"><a href="#">图书</a></li>
                                        <li id="2"><a href="#">标准</a></li>
                                        <li id="3"><a href="#">工具书</a></li>
                                        <li id="4"><a href="#">期刊</a></li>
                                        <li id="5"><a href="#">会议论文</a></li>
                                        <li id="6"><a href="#">年鉴</a> </li>
                                        <li id="7"><a href="#">杂志</a></li>
                                        <li id="8"><a href="#">报纸</a></li>
                                        <li id="9"><a href="#">学位论文</a></li>
                                        <li id="10"><a href="#">视频</a></li>
                                        <li id="11"><a href="#">音频</a></li>
                                        <li id="12"><a href="#">图片</a></li>--%>
                                        <asp:Literal ID="ltlbasedatabase" runat="server" />
                                    </ul>
                                </div>

                            </div>
                            <div class="TYperiodical_List">
                                <asp:HiddenField ID="hdnTypeId" runat="server" />
                                <div style="width: 100%; float: left;" class="noBackg">
                                    <div class="search-result-content">
                                        <iframe id="ifr" allowtransparency="true" marginwidth="0" width="100%" height="1200px" framespacing="0" marginheight="0" frameborder="0" scrolling="no"></iframe>
                                    </div>
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
