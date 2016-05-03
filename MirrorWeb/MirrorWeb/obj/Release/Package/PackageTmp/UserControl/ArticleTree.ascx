<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleTree.ascx.cs" Inherits="DRMS.MirrorWeb.UserControl.ArticleTree" %>
<script src="../js/jquery.ztree.all-3.1.js" type="text/javascript"></script>
<link href="../css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
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
            },
            key:{
                title:"fullName"
            }
        },
        callback: {
            beforeClick: function (treeId, treeNode) {
                var pTree=treeNode.getParentNode();
                if(pTree){
                    ReadChapter(treeNode.id,pTree.id,pTree.fullName);
                }
                else{
                    ReadChapter(treeNode.id);
                }
            }
        }
    };
		$(document).ready(function () {
		    var t = $("#tree");
		    t = $.fn.zTree.init(t, setting, <%=Nodes %>);

            //默认选中某一项
            var selectID="<%=SelectID %>";
            if(selectID){
		        var zTree = $.fn.zTree.getZTreeObj("tree");
                var treeNode = zTree.getNodeByParam("id", selectID.toUpperCase());
                zTree.selectNode(treeNode, false);
                clickNode(treeNode);
            }
		});

  //-->
</script>
<script type="text/javascript">
    function clickNode(treeNode) {
        if (treeNode) {
            var pTree = treeNode.getParentNode();
            if (pTree) {
                ReadChapter(treeNode.id, pTree.id, pTree.fullName);
            }
            else {
                ReadChapter(treeNode.id);
            }
        }
    }
    //上一节
    function preChapter() {
        //获取当前选中的节点
        var zTree = $.fn.zTree.getZTreeObj("tree");
        var selectNodes = zTree.getSelectedNodes();
        var currentNode = getMinNode(selectNodes[0]);
        var nodes = zTree.getNodesByParam("children", null);
        var preNode = null;
        for (var i = 0; i < nodes.length; i++) {
            if (currentNode == nodes[i] && i > 0) {
                preNode = nodes[i - 1];
                break;
            }
        }
        if (preNode) {
            zTree.selectNode(preNode, false);
            clickNode(preNode);
        }
    }
    //下一节
    function nextChapter() {
        //获取当前选中的节点
        var zTree = $.fn.zTree.getZTreeObj("tree");
        var selectNodes = zTree.getSelectedNodes();
        var nextNode = null;
        if (selectNodes[0].isParent) {
            nextNode = getMinNode(selectNodes[0]);
        }
        else {
            var nodes = zTree.getNodesByParam("children", null);
            for (var i = 0; i < nodes.length; i++) {
                if (selectNodes[0] == nodes[i] && i < nodes.length - 1) {
                    nextNode = nodes[i + 1];
                    break;
                }
            }
        }
        if (nextNode) {
            zTree.selectNode(nextNode, false);
            clickNode(nextNode);
        }
    }
    //递归获取最里层的节点
    function getMinNode(TreeNode) {
        var childrens = TreeNode.children;
        if (childrens && childrens[0]) {
            return getMinNode(childrens[0]);
        }
        else {
            return TreeNode;
        }
    }
</script>
<ul id="tree" class="ztree" style="width: 260px; overflow: auto;">
</ul>