<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DRMS.MirrorWeb.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>航空科技知识数字资源库</title>
    <script src="../js/jquery-1.7.2.min.js"></script>
    <link href="../css/search.css" rel="stylesheet" />
    <script src="../js/jquery.ztree.all-3.1.js"></script>
    <link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <script src="js/jquery.cookie.js"></script>
    <script src="js/Util.js"></script>
    <script src="js/singleLogin.js"></script>
    <script type="text/javascript">
        $(function () {
            //右边的高度
            $(".Nright").height($(window).height());

            //窗口大小变化时更新高度
            $(window).resize(function() {
                $(".Nright").height($(window).height());
            });
            //搜索tab
            $(".Nsearch_Tab div").click(function () {
                //更改搜索框里的选项
                var clickField = $.trim($(this).attr("field"));
                $(".Ninput_Select ul").empty();
                switch(clickField)
                {
                    case "16":
                    case "18":
                        $(".Ninput_Select ul").append(
                            "<li>全部</li>"+
                            "<li>标题</li>"+
                            "<li>全文</li>");
                        break;
                    case "1":
                        $(".Ninput_Select ul").append(
                            "<li>全部</li>"+
                            "<li>标题</li>"+
                            "<li>作者</li>");
                        break;
                    default:
                        $(".Ninput_Select ul").append(
                            "<li>全部</li>"+
                            "<li>标题</li>");
                        break;
                }
                //由于元素重新加载所以要重新注册事件
                $(".Ninput_Select li").click(function(){
                    $(".Ninput_Select div").text($(this).text());
                    $(".Ninput_Select ul").hide();
                });

                $(this).addClass("Actived").siblings().removeClass("Actived");
            });

            //如果是ip登录则记录一个cookie是单点登录系统不对其产生影响
            if(QueryString("issingle")=="false"){
                $.cookie("issingle", "false", {
                    path: '/'
                });
            }
            $(".Ninput_Select div").click(function(){
                if($(".Ninput_Select ul").is(":visible")){
                    $(".Ninput_Select ul").hide();
                }
                else{
                    $(".Ninput_Select ul").show();
                }
            });
            $(".Ninput_Select li").click(function(){
                $(".Ninput_Select div").text($(this).text());
                $(".Ninput_Select ul").hide();
            });
        })
        function searchData() {
            var txt = encodeURIComponent($("#searchText").val());
            var activeId = $(".Nsearch_Tab div.Actived").attr("field");
            var selectName = $(".Ninput_Select div").text();
            var selectValue="";
            switch(selectName)
            {
                case "全部":selectValue="all";break;
                case "标题":selectValue="title";break;
                case "全文":selectValue="content";break;
                case "作者":selectValue="author";break;
                default:selectValue="all";break;
            }
            
            window.location.href = "/view/DBThemeNav.aspx?txt=" + txt + "&resource="+activeId+"&selectValue="+selectValue;
        }
        function keyDownSearch() {
            if (event.keyCode == 13) {
                searchData();
            }
        }
    </script>
    <script type="text/javascript">
        var zTree;
        var demoIframe;

        var setting = {
            view: {
                dblClickExpand: false,
                showLine: false,
                selectedMulti: false,
                showIcon: false,
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
                beforeClick: this.beforeClick
            }
        };
        function beforeClick(treeId, node) {
            if (node.level == 0) {
                if(node.isParent){
                    if (!node.open) {
                        zTree.expandAll(false);
                        zTree.expandNode(node);
                    }
                    else {
                        zTree.expandAll(false);
                    }
                    $(".curSelectedNode").removeClass("curSelectedNode");
                }
                else{
                    window.location.href="/view/DBThemeNav.aspx?classid="+node.id;
                }
            }
        }
        $(document).ready(function () {
            var t = $("#tree");
            zTree = $.fn.zTree.init(t, setting, <%=Nodes %>);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Nleft">
	    <div class="Nleft_Title">
		    <h2>技术体系</h2>
		    <h3>Technical System</h3>
	    </div>
	    <div class="Nleft_SubNav">
            <ul id="tree" class="ztree" style="height:500px; overflow:visible; padding:0px !important;">
                
            </ul>
	    </div>
    </div>
    <div class="Nright">
	    <div class="Rcontent">
		    <div class="Nlogo"><img src="images/Nlogo.png"></div>
		    <div class="Nsearch">
			    <div class="Nsearch_Tab">
                    <div class="first Actived" style="left: 0px; z-index: 100;" field="16">条目</div> 
                    <div style="left: 94px; z-index: 99;" field="1">图书</div> 
                    <div style="left: 188px; z-index: 98;" field="18">文章</div> 
                    <div style="left: 282px; z-index: 97;" field="12">图片</div>
                    <div style="left: 376px; z-index: 96;" field="22">手册词条</div> 
                    <div style="left: 470px; z-index: 95;" field="70">英文资源</div> 
                    <%if(IsDisplayOwner){ %>
                        <div style="left: 564px; z-index: 94;" field="64">泛华资源</div> 
                    <%} %>
			    </div>
			    <div class="Nsearch_Content">
				    <div class="Nsearch_Input clearfix">				
					    <input type="text" class="Ninput_Text" id="searchText" onkeydown="keyDownSearch()"/>
				    </div>
                    <div class="Ninput_Select">
                        <div>全部</div>
                        <ul>
                            <li>全部</li>
                            <li>标题</li>
                            <li>全文</li>
                        </ul>
                    </div>
                    <div style="float:right;padding-top:1px;">
					    <input type="button" class="Ninput_Btn" onclick="searchData()"/>
                    </div>
                    <div class="clear"></div>
			    </div>
		    </div>
		    <div class="Nhot_Word">
                <a href="/view/DBThemeNav.aspx?txt=&resource=4&selectValue=all">中文期刊全文浏览</a>
                <a href="/view/DBThemeNav.aspx?txt=&resource=60&selectValue=all">英文期刊全文浏览</a>
                <asp:Literal ID="lt_words" runat="server" Visible="false"></asp:Literal>
		    </div>
	    </div>
	    <div class="Nfooter"><p>中航出版传媒有限责任公司 版权所有 Copyright 2010 All rights</p></div>
    </div>
    <div class="clear"></div>
    <!--页面中只有一个text时，其回车事件会提交到后台-->
    <input type="text" style=" display:none;" />
    </form>
</body>
</html>
