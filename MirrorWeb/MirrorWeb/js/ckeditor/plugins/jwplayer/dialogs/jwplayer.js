CKEDITOR.dialog.add('jwplayer', function (editor) {
    var JWplayer = CKEDITOR.plugins.get('jwplayer').path + 'jwplayer/player.swf';
    function UpdatePreview() {
        document.getElementById("_video_preview").innerHTML = ReturnPlayer()
    }
    function ReturnPlayer() {
        var fileUrl = CKEDITOR.dialog.getCurrent().getContentElement('info', 'video_url').getValue();
        var filePreUrl = CKEDITOR.dialog.getCurrent().getContentElement('info', 'video_pre_url').getValue();
        var width = CKEDITOR.dialog.getCurrent().getContentElement('info', 'width').getValue();
        var height = CKEDITOR.dialog.getCurrent().getContentElement('info', 'height').getValue();
        var auto = CKEDITOR.dialog.getCurrent().getContentElement('info', 'auto').getValue();
        var skin = '';
        var selectskin = CKEDITOR.dialog.getCurrent().getContentElement('info', 'skin').getValue();
        if (selectskin != 'default') {
            skin = "&skin=" + CKEDITOR.plugins.get('jwplayer').path + "jwplayer/skin/" + selectskin + ".zip"
        }
        var image = '';
        if (filePreUrl && filePreUrl != '') {
            image = "&image=" + filePreUrl;
        }
        var JWEmbem = "<embed id='player1' name='player1' width='" + width + "' height='" + height + "' allowfullscreen='true' allowscriptaccess='always' src='" + JWplayer + "' flashvars='file=" + fileUrl + "&amp;autostart=" + auto + skin + image + "'></embed>";

        return JWEmbem
    }
    return {
        title: '添加视频文件',
        minWidth: 460,
        minHeight: 300,
        contents: [{
            id: 'info',
            label: '',
            title: '',
            expand: true,
            padding: 0,
            elements: [{
                type: 'vbox',
                widths: ['280px', '30px'],
                align: 'left',
                children: [{
                    type: 'hbox',
                    widths: ['280px', '30px'],
                    align: 'left',
                    children: [{
                        type: 'text',
                        id: 'video_url',
                        label: '选择视频文件或者视频文件的URL',
                        onChange: UpdatePreview
                    }, {
                        type: 'button',
                        id: 'browse',
                        filebrowser: 'info:video_url',
                        label: editor.lang.common.browseServer,
                        style: 'display:inline-block;margin-top:8px;'
                    }]
                }, {
                    type: 'hbox',
                    widths: ['280px', '30px'],
                    align: 'left',
                    children: [{
                        type: 'text',
                        id: 'video_pre_url',
                        label: '选择视频预览图片或者图片的URL',
                        onChange: UpdatePreview
                    }, {
                        type: 'button',
                        id: 'browse_pre',
                        filebrowser: 'info:video_pre_url',
                        label: editor.lang.common.browseServer,
                        style: 'display:inline-block;margin-top:8px;'
                    }]
                }, {
                    type: 'hbox',
                    widths: ['280px', '10px'],
                    align: 'left',
                    children: [{
                        type: 'vbox',
                        widths: ['280px', '10px'],
                        align: 'left',
                        children: [{
                            type: 'select',
                            id: 'skin',
                            'default': 'default',
                            label: '选择播放器皮肤',
                            items: [['default', 'default'], ['simple', 'simple'], ['glow', 'glow'], ['snel', 'snel'],
								['modieus', 'modieus'], ['stormtrooper', 'stormtrooper'], ['beelden', 'beelden'],
								['stijl', 'stijl']],
                            onChange: UpdatePreview
                        }, {
                            type: 'text',
                            id: 'width',
                            style: 'width:95px',
                            label: '宽度',
                            onChange: UpdatePreview
                        }, {
                            type: 'text',
                            id: 'height',
                            style: 'width:95px',
                            label: '高度',
                            onChange: UpdatePreview
                        }, {
                            type: 'checkbox',
                            id: 'auto',
                            'default': true,
                            label: editor.lang.flash.chkPlay,
                            onChange: UpdatePreview
                        }]
                    }, {
                        type: 'vbox',
                        widths: ['280px', '10px'],
                        align: 'left',
                        children: [{
                            type: 'html',
                            id: 'preview',
                            html: '<div id="_video_preview" ><object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" height="200px" width="200px"> <param name="movie" value="' + JWplayer + '" /> <param name="allowfullscreen" value="true" /> <param name="allowscriptaccess" value="always" /> <param name="flashvars" value="autostart=false" /> <embed width="200px" height="200px" id="player1" name="player1" allowfullscreen="true" allowscriptaccess="always" flashvars="autostart=false" src="' + JWplayer + '"></embed></object></div>'
                        }]
                    }]
                }]
            }]
        }],
        buttons: [CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton],
        onOk: function () {
            editor.setData(editor.getData() + ReturnPlayer())
        }
    }
});