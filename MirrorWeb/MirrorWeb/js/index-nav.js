//全局变量
var zTree;

//zTree的设置
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

//zTree的click事件
function beforeClick(treeId, node) {
    if (node.level == 0) {
        if (node.isParent) {
            if (!node.open) {
                zTree.expandAll(false);
                zTree.expandNode(node);
            }
            else {
                zTree.expandAll(false);
            }
            $(".curSelectedNode").removeClass("curSelectedNode");
        }
        else {
            window.location.href = "/view/DBThemeNav.aspx?classid=" + node.id;
        }
    }
}

//绑定技术体系
function bindNav() {
    $.ajax({
        type: "get",
        url: "../ajax/GetNavList.ashx",
        success: function (data) {
            var nodes = JSON.parse(data);
            var t = $("#tree");
            zTree = $.fn.zTree.init(t, setting, nodes);
        }
    });
}