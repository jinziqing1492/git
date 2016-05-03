/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    //rabtor 2012-10 修改
    config.language = 'zh-cn'; //中文
    config.uiColor = '#eef5fd'; //'#CCEAFE';  //编辑器颜色
    config.font_names = '宋体;楷体_GB2312;新宋体;黑体;隶书;幼圆;微软雅黑;Arial;Comic Sans MS;Courier New;Tahoma;Times New Roman;Verdana';

    var ckpath = CKEDITOR.basePath + '..';  //上级目录,可根据ckfinder的实际目录作相应修改
    config.filebrowserBrowseUrl = ckpath + '/ckfinder/ckfinder.html';                  //上传文件的查看路径    
    config.filebrowserImageBrowseUrl = ckpath + '/ckfinder/ckfinder.html?Type=Images'; //上传图片的查看路径    
    config.filebrowserFlashBrowseUrl = ckpath + '/ckfinder/ckfinder.html?Type=Flash';  //上传Flash的查看路径    
    config.filebrowserUploadUrl = ckpath + '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';        //上传文件的保存路径    
    config.filebrowserImageUploadUrl = ckpath + '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images'; //上传图片的保存路径    
    config.filebrowserFlashUploadUrl = ckpath + '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';  //上传Flash的保存路径

    //config.skin = 'office2003';
    //config.enterMode = CKEDITOR.ENTER_BR;
    config.extraPlugins = 'jwplayer';
    //自定义工具栏
    config.toolbar_MyToolbar =
    [
	    ['Source', 'Preview'],
	    ['Paste', 'PasteText', 'PasteFromWord'],
	    ['Undo', 'Redo', '-', 'Find', 'Replace'],
	    ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'],
	    ['Outdent', 'Indent'],
	    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
	    ['Link', 'Unlink', 'Anchor'], ['SelectAll'],
	    '/',
	    ['Styles', 'Format', 'Font', 'FontSize'],
	    ['Image', 'Flash', 'jwplayer', 'Table', 'HorizontalRule', 'Iframe'],
	    ['TextColor', 'BGColor'],
	    ['Maximize', 'ShowBlocks']		// No comma for the last row.
    ];

    config.toolbar_Full =
    [
	    { name: 'document', items: ['Source', '-', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', '-', 'Templates'] },
	    { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
	    { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker', 'Scayt'] },
	    { name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'] },
	    '/',
	    { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
	    { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl'] },
	    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
	    { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
	    '/',
	    { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
	    { name: 'colors', items: ['TextColor', 'BGColor'] },
	    { name: 'tools', items: ['Maximize', 'ShowBlocks', '-', 'About'] }
    ];
    config.toolbar_Basic =
     [
         ['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', '-', 'About']
     ];
    config.toolbar_MyCustom =
     [
          ['Find', 'Replace', 'Image', '-', 'Preview']//, 'Source'
     ];

    config.toolbar = "MyCustom"; //工具栏
    config.skin = "kama"//皮肤
    //config.width = '515'; //宽度
    config.height = '200'; //高度

    // 当提交包含有此编辑器的表单时，是否自动更新元素内的数据 
    config.autoUpdateElement = true;

    //是否开启 图片和表格 的改变大小的功能 config.disableObjectResizing = true; //默认为开启

//    // 取消 “拖拽以改变尺寸”功能
    //    config.resize_enabled = true; 
    //工具栏是否可以被收缩 
    //config.toolbarCanCollapse = true; 

    //    //改变大小的最大高度 
    //    config.resize_maxHeight = 300;
    //改变大小的最大宽度
    config.resize_maxWidth = 850;
    //    //改变大小的最小高度
    //    config.resize_minHeight = 301;
    //改变大小的最小宽度
    config.resize_minWidth = 850;
};

////rabtor 2012-11 添加，禁止相关标记内（如：<p></p>）自动添加Tab、空格、换行标记
//CKEDITOR.on('instanceReady', function (ev) {
//    with (ev.editor.dataProcessor.writer) {
//        setRules("p", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("h1", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("h2", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("h3", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("h4", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("h5", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("div", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("table", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("tr", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("td", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("iframe", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("li", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("ul", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//        setRules("ol", { indent: false, breakAfterOpen: false, breakBeforeClose: false });
//    }
//}); 