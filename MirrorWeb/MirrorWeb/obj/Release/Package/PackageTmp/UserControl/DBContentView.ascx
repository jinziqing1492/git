<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DBContentView.ascx.cs" Inherits="DRMS.MirrorWeb.UserControl.DBContentView" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<link href="/css/magazineSearch.css" rel="stylesheet" type="text/css" />
<link href="/css/base.css" rel="stylesheet" type="text/css" />
<link href="/css/mykvideo.css" rel="stylesheet" type="text/css" />
<script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
<script src="../js/base.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        showNote();
    });
    function SetImgAutoSize(obj) {
        var img = obj;
        var MaxWidth = $(img).parent().width();  //设置图片宽度界限 
        var MaxHeight = $(img).parent().height(); //设置图片高度界限 
        var HeightWidth = img.offsetHeight / img.offsetWidth; //设置高宽比
        var MaxHeightWidth = MaxHeight / MaxWidth;

        if ($.browser.msie) {
            if (img.readyState != "complete") return false; //确保图片完全加载 
        }
        if ($.browser.mozilla) {
            if (img.complete == false) return false;
        }

        if (HeightWidth < MaxHeightWidth && img.offsetWidth > MaxWidth) {
            img.width = MaxWidth;
        }
        else if (HeightWidth > MaxHeightWidth && img.offsetHeight > MaxHeight) {
            img.height = MaxHeight;
        }
        //设置图片居中
        var marginTop = ($(img).parent().height() - img.height) / 2;
        $(img).css("margin-top", marginTop);
    }
</script>
<div class="pagin" style="margin-bottom: 0px;">
    <asp:Literal ID="ltlmessage" runat="server"></asp:Literal>

</div>
<asp:Literal ID="lt_list" runat="server" EnableViewState="false"></asp:Literal>
<div class="clear">
</div>
<div class="pagin">
    <div>
        <webdiyer:aspnetpager id="aspNetPager" runat="server" cssclass="aspNetPager" urlpaging="false"
            alwaysshow="false" pagesize="4" custominfohtml="第<b>%CurrentPageIndex%</b></font> 页 共 %PageCount% 页 显示 %StartRecordIndex%-%EndRecordIndex% 条"
            firstpagetext="首页" lastpagetext="末页" nextpagetext="后一页" prevpagetext="前一页" showinputbox="Auto"
            submitbuttontext="跳转到" onpagechanging="aspNetPager_PageChanging">
        </webdiyer:aspnetpager>
    </div>
</div>
<asp:HiddenField ID="hid_type" runat="server" />
<asp:HiddenField ID="hid_keyWord" runat="server" />
<asp:HiddenField ID="hid_sql" runat="server" />
<asp:HiddenField ID="hid_search" runat="server" />
<asp:HiddenField ID="Hid_bookid" runat="server" />
<asp:HiddenField ID="Hid_powerid" runat="server" />
<asp:HiddenField ID="hid_selectValue" runat="server" />
<asp:HiddenField ID="hid_orderField" runat="server" />
<asp:HiddenField ID="hid_where" runat="server" />
<asp:HiddenField ID="hid_second" runat="server" />
<asp:HiddenField ID="hid_owner" runat="server" />