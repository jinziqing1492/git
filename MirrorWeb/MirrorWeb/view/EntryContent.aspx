<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EntryContent.aspx.cs" Inherits="DRMS.MirrorWeb.view.EntryContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../css/BookDetail.css" />
    <link href="/css/base.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="../css/fancybox/source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <script src="../css/fancybox/source/jquery.fancybox.js" type="text/javascript"></script>

        <script type="text/javascript">
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
            <div class="TYperiodical_List">
                                <div id="noResult" runat="server" style='margin: 15px auto; width: 300px; height: 50px;
                                    text-align: center; color: Red; padding-top: 20px; border: 1px solid #aed0ea;' visible="false">
                                    <p style='margin: auto;'>
                                        该资源不存在，请浏览其它资源</p>
                                </div>
                                <div id="haveResult" runat="server" visible="true">
                                    <div class="singleBook">
                                        <div class="sgbName">
                                            <asp:Literal ID="lt_Title" runat="server"></asp:Literal>
                                        </div>                                        
                                        <div class="sgbMain">                                                                                      
                                            <div class="sgbmr">
                                                <ul class="summary">
                                                    <asp:Literal ID="lt_summary" runat="server"></asp:Literal>
                                                </ul>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                        </div>
                                        <div class="singleDetail">
                                        <h2>定义内容</h2>
                                        <div class="sm">
                                            <asp:Literal ID="lt_Digest" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                        <asp:Literal ID="lt_Pic" runat="server"></asp:Literal>
                                    </div>                                        
                                    </div>
                                </div>
                            </div>
         </div>
    </form>
</body>
</html>
