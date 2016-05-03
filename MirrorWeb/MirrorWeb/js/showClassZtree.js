
var setting = {
    data: {
        simpleData: {
            enable: true,
            idKey: "id",
            pIdKey: "pId",
            rootPId: ""
        },
        key: {
            name: "name",
            title: "fullName"//,
            //url: "url"
        }
    },
    view: {
        dblClickExpand: true,
        //fontCss: { color: "#ff0011", background: "blue" },
        selectedMulti: true,
        showIcon: true,
        showTitle: true
    },
    check: {
        enable: true,
        //autoCheckTrigger: true,
        chkboxType: { "Y": "", "N": "" },
        chkStyle: "checkbox"
    },
    callback: {
        beforeExpand: function zTreeBeforeExpand(treeId, treeNode) {
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            if (treeNode.children && treeNode.children.length > 0)
                return;
            $.ajax({
                url: "../ajax/ClassMngrHandler.ashx",
                type: "POST",
                async: true,
                data: { otype: "gsc", pId: treeNode.id, resdoi: _resDoi, dbtnum: _dbtNum, cSelCls: _curSelectClass },
                complete: function (XMLHttpRequest, textStatus) {
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    // 通常情况下textStatus和errorThown只有其中一个有值 
                },
                success: function (data, textStatus) {
                    var nodes = $.parseJSON(data);
                    treeObj.addNodes(treeNode, nodes);
                }
            });
        }
    }
};
//存储当前选中的分类
var _curSelectClass = "";
//存储当前页内的资源类型编号
var _dbtNum = "0";
//存储当前页内的资源doi
var _resDoi = "0";
//显示 ztree div
function showClassZtree(event, resdoi, dbtnum, cSelClass) {
    //存在此div，不创建
    if ($("#new_div_ztree_id").length > 0)
        return;
    //存储全局变量
    _curSelectClass = cSelClass;
    _dbtNum = dbtnum;
    _resDoi = resdoi;
    //进入div
    var enterdiv = false;
    //创建div
    var classTbx = $(event);
    classTbx.unbind("blur");
    var classOffset = classTbx.offset();
    var width = classTbx.width();
    var top = classOffset.top + classTbx.height() + 7;
    var bottom = classOffset.top;
    var left = classOffset.left;
    var newdiv = $('<div></div>');        //创建一个父div
    newdiv.attr('id', 'new_div_ztree_id');        //给父div设置id
    newdiv.addClass('new_div_ztree_cls');    //添加class
    //设置div样式、显示位置
    newdiv.css("width", width + "px");
    newdiv.css("top", top + "px");
    newdiv.css("left", left + "px");
    newdiv.mouseenter(function () {
        enterdiv = true;
    });
    newdiv.mouseleave(function () {
        enterdiv = false;
        //当鼠标移除div时，焦点回归到event
        classTbx.focus();
    });
    newdiv.appendTo('body');            //将父div添加到body中
    //    $("body", window.top.document).append(newdiv);
    classTbx.blur(function () {
        if (!enterdiv)
            newdiv.remove();
    });
    //创建 iframe   
    var newif = $('<ul></ul>');        //创建一个父ul
    newif.attr('id', 'treeDemo');        //给父ul设置id
    newif.addClass('ztree');    //添加class
    newif.appendTo(newdiv);            //将父table添加到div中

    //加载分类树
    $.ajax({
        url: "../ajax/ClassMngrHandler.ashx",
        type: "POST",
        async: true,
        data: { otype: "gc", resDoi: _resDoi, dbtNum: _dbtNum, cSelCls: _curSelectClass },
        complete: function (XMLHttpRequest, textStatus) {
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            // 通常情况下textStatus和errorThown只有其中一个有值 
        },
        success: function (data, textStatus) {
            var nodes = $.parseJSON(data);
            //加载分类树
            $.fn.zTree.init($("#treeDemo"), setting, nodes);
        }
    });

    //创建 a“确定”按钮
    var newa = $('<input value=\"确定\" />');        //创建一个a
    newa.attr('id', 'ok_input_id');        //给a设置id
    newa.addClass('ok_input_cls');    //添加class
    newa.click(function () {
        //获取选中的分类
        var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
        var nodes = treeObj.getCheckedNodes(true);
        var classSelected = "";
        var classSelectedText = "";
        if (nodes && nodes.length > 0) {
            for (var i = 0; i < nodes.length; i++) {
                classSelected += nodes[i].id + ";";
                classSelectedText += nodes[i].name + ";";
            }
        }
        //把结果存储到变量中
        _curSelectClass = classSelected.trimEnd(";");
        $(event).next().val(_curSelectClass);
        $(event).val(classSelectedText.trimEnd(";"));
        newdiv.remove();
    });
    newa.appendTo(newdiv);            //将父a添加到div中

    //创建 a“清空”按钮
    var newa = $('<input value=\"清空\" />');        //创建一个a
    newa.attr('id', 'clear_input_id');        //给a设置id
    newa.addClass('ok_input_cls');    //添加class
    newa.click(function () {
        //把结果存储到变量中
        _curSelectClass = "";
        $(event).next().val(_curSelectClass);
        $(event).val("");
        newdiv.remove();
    });
    newa.appendTo(newdiv);            //将父a添加到div中

    //计算div应显示的位置
    //1、获取窗口的高度(在Netscape下需要使用Window的属性；在IE下需要深入Document内部对body进行检测)
    var myScrollTop = document.documentElement.scrollTop;
    var myBdHeight = 0;
    //获取窗口高度
    if (window.innerHeight) {
        myBdHeight = window.innerHeight;
    }
    //通过深入Document内部对body进行检测，获取窗口大小
    if (document.documentElement && document.documentElement.clientHeight) {
        myBdHeight = document.documentElement.clientHeight;
    }
    //2、判断 event 下方是否足够显示
    if ((myScrollTop + myBdHeight - classOffset.top) < newdiv.height()) {

        if (classOffset.top - myScrollTop > newdiv.height()) {
            //上部放得下，放上面
            newdiv.css("top", (classOffset.top - newdiv.height()) + "px");
        } else {
            //上面放不下，底部与页面齐
            newdiv.css("top", (myScrollTop + myBdHeight - newdiv.height() - 5) + "px");
        }
    }
}

