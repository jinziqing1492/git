
function loadFileMngr(flLoadJson, auth, AspSessID) {
    //获取上传文件类型，而制定规则
    var multiparam = false;
    var fileExtparam = "*.*";
    var fileDescparam = "请选择文件";
    switch ($("#fileSel").val()) {
        case "mainf":
            $("#attachSel").hide();
            break;
        case "cover":
            fileExtparam = "*.jpg;*.jpeg*.png;*.gif;*.bmp;*.png";
            fileDescparam = "请选择 jpg jpeg png gif bmp png 文件";
            $("#attachSel").hide();
            break;
        case "attach":
            multiparam = true;
            fileExtparam = "*.*";
            fileDescparam = "请选择 任何类型 文件";
            $("#attachSel").show();
            break;
    }

    $("#uploadify").uploadify({
        'uploader': '../js/jquery.uploadify-v2.1.0/uploadify.swf',
        'script': '../ajax/UploadHandler.ashx?otype=uh',
        'checkScript': '../ajax/UploadHandler.ashx?otype=cfh',
        'cancelImg': '../js/jquery.uploadify-v2.1.0/cancel.png',
        'folder': '../UploadFile',
        'buttonImg': '../images/TYenterTarget.png',
        'wmode': 'transparent',
        'width': '101',
        'height': '116',
        'auto': true,
        'multi': multiparam,
        'fileExt': fileExtparam,
        'fileDesc': fileDescparam,
        'scriptData': { 'ftype': $("#fileSel").val(), ASPSESSID: AspSessID, AUTHID: auth },
        'onComplete': function (event, queueId, fileObj, response, data) {
            var fileHtml = "<li><input type='hidden' value='" + $("#attachSel").val() + "' /><p class='TYencTitle' title='" + fileObj.name + "'>"
                    + fileObj.name.substr(0, 9) + "...</p><p><span>"
                    + FormatSize(fileObj.size)
                    + "</span><a href='javascript:void(0);' class='cancelUF'>取消</a></p></li>";
            switch (response) {
                case "mainf":
                    $("#mainf").html(fileHtml);
                    break;
                case "cover":
                    $("#cover").html(fileHtml);
                    break;
                case "attach":
                    if ($("#attach").html().indexOf(fileObj.name) == -1)
                        $("#attach").html($("#attach").html() + fileHtml);
                    break;
            }
            //绑定取消按钮点击事件
            $(".cancelUF").click(cancelUploadFile);
        },
        'onError': function (event, ID, fileObj, errorObj) {
            alert("文件" + fileObj.name + '上传失败！');
        },
        'onCheck': function (event, checkScript, fileQueueObj, folder, single) {
        },
        'onOpen': function () {
        }
    });
    //切换上传文件类型时，更改uploadify参数
    $("#fileSel").change(function () {
        var multiparam = false;
        var fileExtparam = "*.*";
        var fileDescparam = "请选择文件";
        switch ($(this).val()) {
            case "mainf":
                $("#attachSel").hide();
                break;
            case "cover":
                fileExtparam = "*.jpg;*.jpeg*.png;*.gif;*.bmp;*.png";
                fileDescparam = "请选择 jpg jpeg png gif bmp png 文件";
                $("#attachSel").hide();
                break;
            case "attach":
                multiparam = true;
                fileExtparam = "*.*";
                fileDescparam = "请选择 任何类型 文件";
                $("#attachSel").show();
                break;
        }
        var upload = $('#uploadify');
        upload.uploadifySettings('multi', multiparam);
        upload.uploadifySettings('fileExt', fileExtparam);
        upload.uploadifySettings('fileDesc', fileDescparam);
        upload.uploadifySettings('scriptData', { 'ftype': $("#fileSel").val() });
    });
    //加载已经存在的文件列表内容 "{\"mainf\":\"['1.pdf','2.44MB']\",\"cover\":\"['1.jpg','589.3KB']\",\"attach\":\"['1.jpg','34.23KB','2.txt','3.2KB']\"}"
    //var flJson = "";// $("#<%=hdnFileJsonStr.ClientID %>").val(); // "<%=FileJsonStr %>";
    //var flJson = jQuery.parseJSON(flJsonStr);
    if (flLoadJson) {
        if (flLoadJson.mainf) {
            var mainInfo = eval("(" + flLoadJson.mainf + ")");
            var fileHtml = "<li><p class='TYencTitle' title='" + mainInfo[0] + "'>"
            + mainInfo[0].substr(0, 9) + "...</p><p><span>"
            + mainInfo[1]
            + "</span><a href='javascript:void(0);' class='deleteUF'>删除</a></p></li>";
            $("#mainf").html(fileHtml);
        }
        if (flLoadJson.cover) {
            var coverInfo = eval("(" + flLoadJson.cover + ")");
            var fileHtml = "<li><p class='TYencTitle' title='" + coverInfo[0] + "'>"
            + coverInfo[0].substr(0, 9) + "...</p><p><span>"
            + coverInfo[1]
            + "</span><a href='javascript:void(0);' class='deleteUF'>删除</a></p></li>";
            $("#cover").html(fileHtml);
        }
        if (flLoadJson.attach) {
            var attachInfo = eval("(" + flLoadJson.attach + ")");
            var fileHtml = "";
            for (var i = 0; i < attachInfo.length; i += 3) {
                var itemHtml = "<li><p class='TYencTitle' title='" + attachInfo[i]
                + "' id='" + attachInfo[i + 1] + "'>"
                + attachInfo[i].substr(0, 9) + "...</p><p><span>"
                + attachInfo[i + 2]
                + "</span><a href='javascript:void(0);' class='deleteUF'>删除</a></p></li>";
                fileHtml += itemHtml;
            }
            $("#attach").html(fileHtml);
        }
        //绑定删除按钮点击事件
        $(".deleteUF").click(deleteUploadFile);
    }
}

//删除临时文件夹中的对应文件
function cancelUploadFile() {
    var $curObj = $(this);
    var filename = $curObj.parent().prev().attr("title");
    //删除临时文件夹中的对应文件
    $.ajax({
        type: "GET",
        url: "../ajax/UploadHandler.ashx",
        data: { 'otype': 'cuf', 'fname': filename, 'folder': '../UploadFile' },
        async: false,
        cache: false,
        complete: function () {
            //移除界面对应文件的 li 元素
            $curObj.parent().parent().remove();
        }
    });
}
//存储待真正删除的文件列表
var readyDelFL = "";
//删除记录已存在文件显示并记下，附件记录文件名，其它记录类型即可
function deleteUploadFile() {
    var $curObj = $(this);
    var filedoi = $curObj.parent().prev().attr("id");
    var ulId = $curObj.parent().parent().parent().attr("id");
    if (ulId != "attach" && readyDelFL.indexOf(ulId) == -1)
        readyDelFL += ulId + "|";
    else if (readyDelFL.indexOf(filedoi) == -1)
        readyDelFL += filedoi + "|";
    //移除界面对应文件的 li 元素
    $curObj.parent().parent().remove();
}

//获取当前待上传的文件的Json对象  {'mainf':'1.pdf','cover':'1.jpg','attach':'1.jpg|2.txt|3.docx'}
function getPreUploadFileJson() {
    //文件列表Json
    var flJson = "{";
    var mainFN = $(".TYenclosure table ul[id='mainf'] a:contains('取消')").parent().prev().attr("title");
    if (mainFN)
        flJson += "\"mainf\":\"" + mainFN + "\",";
    var coverFN = $(".TYenclosure table ul[id='cover'] a:contains('取消')").parent().prev().attr("title");
    if (coverFN)
        flJson += "\"cover\":\"" + coverFN + "\",";
    var attachArr = $(".TYenclosure table ul[id='attach'] a:contains('取消')");
    var attachFN = "";
    for (var i = 0; i < attachArr.length; i++) {
        var attachItem = $(attachArr[i]).parent().prev().attr("title");
        var attachType = $(attachArr[i]).parent().prev().prev().val();
        if (attachItem && attachType)
            attachFN += attachItem + ":" + attachType + "|";
    }
    if (attachFN)
        flJson += "\"attach\":\"" + attachFN.trimEnd(',') + "\"";
    flJson = flJson.trimEnd(',') + "}";
    return flJson;
}