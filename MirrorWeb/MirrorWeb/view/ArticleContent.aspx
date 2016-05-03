<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleContent.aspx.cs" Inherits="DRMS.MirrorWeb.view.ArticleContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/base.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../js/base.js" type="text/javascript"></script>
    <script src="../js/layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //显示注释
            //showNote();
            //用layer
            showNoteLayer();
        });
        function SetImgAutoSize(obj, width, height) {
            //var img=document.all.img1;//获取图片 
            var img = obj;
            var MaxWidth = width; //设置图片宽度界限 
            var MaxHeight = height; //设置图片高度界限 
            var HeightWidth = img.offsetHeight / img.offsetWidth; //设置高宽比 
            var WidthHeight = img.offsetWidth / img.offsetHeight; //设置宽高比 
            //if (img.readyState != "complete") return false; //确保图片完全加载  
            if ($.browser.msie) {
                if (img.readyState != "complete") return false; //确保图片完全加载 
            }
            if ($.browser.mozilla) {
                if (img.complete == false) return false;
            }
            if (img.offsetWidth > MaxWidth) {
                img.width = MaxWidth;
            }
        }
        $(document).ready(function () {
            $(".frmContent p > img").each(function () {
                $(this).parent().addClass("center");
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="frmContent">
        <asp:Literal ID="lt_content" runat="server" EnableViewState="false"></asp:Literal>
        <div class="cpt-gotoDiv">
            <a href="#" onclick="javascript:parent.preChapter()">上一节</a> <a href="#" onclick="javascript:parent.nextChapter()">
                下一节</a>
        </div>
    </div>
    </form>
</body>
</html>
