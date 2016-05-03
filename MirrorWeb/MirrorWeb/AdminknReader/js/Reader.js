var LastScroll = 0;
var PageHeight = 892;
var MouseState = false;
var ScrollFlg = true;
function InitPage(height, width) {
    var midWidth = 412;
    if ($("#read_lef").css("display") == "none") {
        midWidth = midWidth - 200;
    }

    if ($("#read_rig").css("display") == "none") {
        midWidth = midWidth - 200;
    }
    
    $("#read_main").css("height",height - 2);
    $("#read_main").css("width", width - 2);
    $("#read_content").css("height", height - 46);
    $("#read_mid").css("width", width - midWidth);
    //ResizeFrm();
    InitLeft();
   // InitRight();
}

function Close() {
    $(window.parent.document).find("#divReader").hide();
    $(window.parent.document.body).css("overflow", "auto");
}

function Expand() {
    $("#aExpand").hide();
    $("#aRestore").show();
    
    var wHeight = $(window.parent.document).find("#hidHeight").val();
    var wWidth = $(window.parent.document).find("#hidWidth").val();
    $(window.parent.document).find("#frmReader").css("height", wHeight + "px");
    $(window.parent.document).find("#frmReader").css("width", wWidth + "px");
    $(window.parent.document).find("#frmReader").css("top", parseInt($(window.parent.document).find("#frmReader").css("top"), 10) - 40);
    $(window.parent.document).find("#frmReader").css("left", 0);
    InitPage(wHeight, wWidth);
    $("#divInfo").height(300)
    $("#divcon_text").height($("#read_main").height() - $("#divInfo").height() - 100);
   // InitRight();
}

function Restore() {
    $("#aExpand").show();
    $("#aRestore").hide();
    
    var wHeight = $(window.parent.document).find("#hidHeight").val();
    var wWidth = $(window.parent.document).find("#hidWidth").val();
    wHeight = parseInt(wHeight, 10) - 80;
    wWidth = parseInt(wWidth, 10) - 80;

    $(window.parent.document).find("#frmReader").css("height", wHeight + "px");
    $(window.parent.document).find("#frmReader").css("width", wWidth + "px");
    $(window.parent.document).find("#frmReader").css("top", parseInt($(window.parent.document).find("#frmReader").css("top"), 10) + 40);
    $(window.parent.document).find("#frmReader").css("left", 40);
    InitPage(wHeight, wWidth);
    $("#divInfo").height(300)
    $("#divcon_text").height($("#read_main").height() - $("#divInfo").height() - 100);
    InitRight();
}

function SideClose(id, width, direct) {
    //debugger;
    $("#" + id).animate({ width: "0px" }, "fast", function() { $("#" + id).hide(); ResetSide(id,width,direct,0); });
    $("#read_mid").animate({ width: parseInt($("#read_mid").css("width").replace("px", ""), 10) + (width + 2) }, "fast");
}

function SideOpen(id, width, direct) {
    //debugger;
    $("#read_mid").animate({ width: parseInt($("#read_mid").css("width").replace("px", ""), 10) - (width + 2) }, "fast");
    $("#" + id).animate({ width: width }, "fast", function() { $("#" + id).show(); ResetSide(id,width,direct,1); });
}

function ResetSide(id,width,direct,flg) {
    if (direct == 0) {
        $("#aread_closelef").attr("onclick", "");
        $("#aread_closelef").unbind("click");
        if (flg == 0) {
            $("#aread_closelef").click(function() { SideOpen(id, width, direct); });
            $("#read_closelef").removeClass("read_closelef");
            $("#read_closelef").addClass("read_closerig");
        } else {
            $("#aread_closelef").click(function() { SideClose(id, width, direct); });
            $("#read_closelef").removeClass("read_closerig");
            $("#read_closelef").addClass("read_closelef");
        }
    }
    else {
        $("#aread_closerig").attr("onclick", "");
        $("#aread_closerig").unbind("click");
        if (flg == 0) {
            $("#aread_closerig").click(function() { SideOpen(id, width, direct); });
            $("#read_closerig").removeClass("read_closerig");
            $("#read_closerig").addClass("read_closelef");
        }
        else {
            $("#aread_closerig").click(function() { SideClose(id, width, direct); });
            $("#read_closerig").removeClass("read_closelef");
            $("#read_closerig").addClass("read_closerig");
        }
    }
}

function SetPageDiv(start, end, page) {
    count = end - start + 1;
    $("#read_midbox").css("height", PageHeight * count);
    $("#hidStart").val(start);
    $("#hidEnd").val(end);
    $("#labPage").text(start);

    $("#divImg1").css("top", 0);
    $("#divImg2").css("top", PageHeight);
    $("#divImg3").css("top", 2 * PageHeight);

    $("#read_mid_srollbar").scroll(function() { ShowPage(); });
    $("#read_over").mousedown(MDown);
    $("#read_over").mouseup(MUp);
    $("#read_over").mousemove(MMove);
    
    //翻页状态
    $("#aPrePage").addClass("pre_nopage");
    if (count < 2) {
        $("#aNextPage").addClass("next_nopage");
    }
    if(page != "")
    {
        SetPage(page);
    }
}

function ShowPage() {
    if (!ScrollFlg) {
        return false;
    }
    
    var Height = $("#read_mid_srollbar").scrollTop();
    var Direction = 0;
    if (Height == LastScroll) {
        return false;
    }
    if (Height > LastScroll) {
        Direction = 1;
    }
    else {
        Direction = 0;
    }
    if (Math.abs(Height - LastScroll) >= PageHeight) {
        //alert(Height - LastScroll);
        ShowPage1();
        LastScroll = Height;
        return false;
    }
    LastScroll = Height;

    var OffHeight = Height % PageHeight;
    var PageIndex = parseInt(Height / PageHeight, 10);

    var page = parseInt($("#hidStart").val(), 10) + PageIndex;
    $("#labPage").text(page);
    if (page == parseInt($("#hidStart").val(), 10)) {
        $("#aPrePage").addClass("pre_nopage");
    } else if (page == parseInt($("#hidEnd").val(), 10)) {
        $("#aNextPage").addClass("next_nopage");
    } else {
        $("#aNextPage").removeClass("next_nopage");
        $("#aPrePage").removeClass("pre_nopage");
    }

    
//    if (Direction == 1 && OffHeight < (PageHeight / 3) * 2) {
//        return false;
//    }

//    if (Direction == 0 && OffHeight > PageHeight / 3) {
//        return false;
//    }
    
    if (PageIndex < 1) {
        return;
    }
    if (parseInt($("#hidEnd").val(), 10) - parseInt($("#hidStart").val(), 10) <= PageIndex ) {
        return;
    }
    //alert(page);
    //ClearHighLight();
    if (Direction == 1) {
        // alert($("#read_midbox #Img" + (PageIndex)).length);
        if ($("#read_midbox #Img" + (PageIndex)).length == 1) {
            PageIndex = PageIndex - 1;
            $("#read_midbox #Img" + (PageIndex)).removeAttr("src");
            $("#read_midbox #Img" + (PageIndex)).attr("src", "request/TebReadHandler.ashx?b=" + $("#hidBookID").val() + "&p=" + $("#hidPressID").val() + "&page=" + (parseInt($("#hidStart").val(), 10) + PageIndex + 2));
            $("#read_midbox #Img" + (PageIndex)).parent().parent().css("top", PageHeight * (PageIndex + 2));
            $("#read_midbox #Img" + (PageIndex)).attr("id", "Img" + (PageIndex + 3));
            //SetHighLight(parseInt($("#hidStart").val(), 10) + PageIndex + 2);
            //SearchText1(parseInt($("#hidStart").val(), 10) + PageIndex + 2);
            ClearHighLight1(PageIndex + 3);
        }
    } else {
        if ($("#read_midbox #Img" + (PageIndex + 3)).length == 1) {
            $("#read_midbox #Img" + (PageIndex + 3)).removeAttr("src");
            $("#read_midbox #Img" + (PageIndex + 3)).attr("src", "request/TebReadHandler.ashx?b=" + $("#hidBookID").val() + "&p=" + $("#hidPressID").val() + "&page=" + (parseInt($("#hidStart").val(), 10) + PageIndex - 1));
            $("#read_midbox #Img" + (PageIndex + 3)).parent().parent().css("top", PageHeight * (PageIndex - 1));
            $("#read_midbox #Img" + (PageIndex + 3)).attr("id", "Img" + (PageIndex));
            //SetHighLight(parseInt($("#hidStart").val(), 10) + PageIndex - 1);
            //SearchText1(parseInt($("#hidStart").val(), 10) + PageIndex - 1);
            ClearHighLight1(PageIndex);
        }
    }
    SearchText();

}


function ShowPage1() {
    $("#read_midbox img").removeAttr("src");
    //ClearHighLight();
    //alert($("#read_midbox img").length);
    var Height = $("#read_mid_srollbar").scrollTop();

    var PageIndex = parseInt(Height / PageHeight, 10);
    
    
    var page = parseInt($("#hidStart").val(), 10) + PageIndex;
    $("#labPage").text(page);
    if (parseInt($("#hidStart").val(), 10) == parseInt($("#hidEnd").val(), 10)) {
        $("#aNextPage").addClass("next_nopage");
        $("#aPrePage").addClass("pre_nopage");
    }
    else if (page == parseInt($("#hidStart").val(), 10)) {
        $("#aPrePage").addClass("pre_nopage");
        $("#aNextPage").removeClass("next_nopage");
    } else if (page == parseInt($("#hidEnd").val(), 10)) {
        $("#aNextPage").addClass("next_nopage");
        $("#aPrePage").removeClass("pre_nopage");
    } else {
        $("#aNextPage").removeClass("next_nopage");
        $("#aPrePage").removeClass("pre_nopage");
    }
    
    if (parseInt($("#hidEnd").val(), 10) - parseInt($("#hidStart").val(), 10) + 1 <= PageIndex) {
        return;
    }

    if (PageIndex == 0) {
        PageIndex = PageIndex + 1;
        page = page + 1;
    }
    if (PageIndex == parseInt($("#hidEnd").val(), 10) - 1) {
        PageIndex = PageIndex - 1;
        page = page - 1;
    }
    //alert(PageIndex);
    $("#divImg1").find("img").attr("src", "request/TebReadHandler.ashx?b=" + $("#hidBookID").val() + "&p=" + $("#hidPressID").val() + "&page=" + (page - 1));
    $("#divImg1").css("top", PageHeight * (PageIndex - 1));
    $("#divImg1").find("img").attr("id", "Img" + (PageIndex));

    $("#divImg2").find("img").attr("src", "request/TebReadHandler.ashx?b=" + $("#hidBookID").val() + "&p=" + $("#hidPressID").val() + "&page=" + page);
    $("#divImg2").css("top", PageHeight * PageIndex);
    $("#divImg2").find("img").attr("id", "Img" + (PageIndex + 1));

    $("#divImg3").find("img").attr("src", "request/TebReadHandler.ashx?b=" + $("#hidBookID").val() + "&p=" + $("#hidPressID").val() + "&page=" + (page + 1));
    $("#divImg3").css("top", PageHeight * (PageIndex + 1));
    $("#divImg3").find("img").attr("id", "Img" + (PageIndex + 2));
    SearchText();
 }

 function SetPage(page) {
     ClearHighLight();
     //$("#read_mid_srollbar").scroll(function() { ShowPage(); });
    page = parseInt(page, 10);
    $("#labPage").text(page);
    var index = page - parseInt($("#hidStart").val(), 10);

    if (page == parseInt($("#hidStart").val(), 10)) {
        index = index + 1;
        page = page + 1;
        $("#read_mid_srollbar").scrollTop(PageHeight * (index - 1));
        $("#aPrePage").addClass("pre_nopage");
    } else if (page == parseInt($("#hidEnd").val(), 10)) {
        index = index - 1;
        page = page - 1;
        $("#read_mid_srollbar").scrollTop(PageHeight * (index + 1));
        $("#aNextPage").addClass("next_nopage");
    } else {
        $("#aNextPage").removeClass("next_nopage");
        $("#aPrePage").removeClass("pre_nopage");
        $("#read_mid_srollbar").scrollTop(PageHeight * index);
    }

    //SearchText1(page);
    //alert(index);
    //alert(page);
//    $("#divImg1").find("img").attr("src", "../request/TebReadHandler.ashx?b=" + $("#hidBookID").val() + "&p=" + $("#hidPressID").val() + "&page=" + (page - 1));
//    $("#divImg1").css("top", PageHeight * (index - 1));
//    $("#divImg1").find("img").attr("id", "Img" + (index));

//    $("#divImg2").find("img").attr("src", "../request/TebReadHandler.ashx?b=" + $("#hidBookID").val() + "&p=" + $("#hidPressID").val() + "&page=" + page);
//    $("#divImg2").css("top", PageHeight * index);
//    $("#divImg2").find("img").attr("id", "Img" + index + 1);

//    $("#divImg3").find("img").attr("src", "../request/TebReadHandler.ashx?b=" + $("#hidBookID").val() + "&p=" + $("#hidPressID").val() + "&page=" + (page + 1));
//    $("#divImg3").css("top", PageHeight * (index + 1));
//    $("#divImg3").find("img").attr("id", "Img" + (index + 2));
}

function NextPage() {
    if ($("#labPage").text() == $("#hidEnd").val()) {
        return false;
    }
    SetPage(parseInt($("#labPage").text(), 10) + 1);
}

function PrePage() {
    if ($("#labPage").text() == $("#hidStart").val()) {
        return false;
    }
    SetPage(parseInt($("#labPage").text(), 10) - 1);
}

function MDown() {
    MouseState = true;
    //$("#read_over").css("cursor", "url(images/reader/hand2.cur);");
    document.getElementById("read_over").style.cursor = "url(images/reader/hand2.cur),auto;";
}

function MUp() {
    MouseState = false;
    //$("#read_over").css("cursor", "url(images/reader/hand1.cur);");
    document.getElementById("read_over").style.cursor = "url(images/reader/hand1.cur),auto;";
 }

 var MouseH = 0;
 function MMove(obj) {
     if (MouseH != 0 && MouseState) {
         $("#read_mid_srollbar").scrollTop($("#read_mid_srollbar").scrollTop() + (MouseH - obj.clientY));
     }
     MouseH = obj.clientY;
 }

 var ZoomSmallCount = 0;
 function ZoomSmall() {
     if (ZoomSmallCount == 4) {
         return false;
     }
     ZoomSmallCount = ZoomSmallCount + 1;
     ZoomBigCount = ZoomBigCount - 1;
     if (ZoomSmallCount == 4) {
         $("#azoom_small").addClass("zoom_nosmall");
         $("#azoom_small").removeClass("zoom_small");
     }
     $("#azoom_big").addClass("zoom_big");
     $("#azoom_big").removeClass("zoom_nobig");
     var top1 = parseInt($("#divImg1").css("top").replace("px", ""), 10);
     var top2 = parseInt($("#divImg2").css("top").replace("px", ""), 10);
     var top3 = parseInt($("#divImg3").css("top").replace("px", ""), 10);
     var num1 = top1 / PageHeight;
     var num2 = top2 / PageHeight;
     var num3 = top3 / PageHeight;
     var num = $("#read_mid_srollbar").scrollTop() / PageHeight;
     
     PageHeight = PageHeight - 50;

     $("#divImg1").css("height", PageHeight);
     $("#divImg1").css("top", parseInt($("#divImg1").css("top").replace("px", ""), 10) - 50 * num1);
     $("#divImg1").find("img").css("height", PageHeight - 12);
     $("#divImg2").css("height", PageHeight);
     $("#divImg2").css("top", parseInt($("#divImg2").css("top").replace("px", ""), 10) - 50 * num2);
     $("#divImg2").find("img").css("height", PageHeight - 12);
     $("#divImg3").css("height", PageHeight);
     $("#divImg3").css("top", parseInt($("#divImg3").css("top").replace("px", ""), 10) - 50 * num3);
     $("#divImg3").find("img").css("height", PageHeight - 12);

     $("#read_mid_srollbar").scrollTop($("#read_mid_srollbar").scrollTop() - 50 * num);

     var count = parseInt($("#hidEnd").val(), 10) - parseInt($("#hidStart").val(), 10) + 1;
     $("#read_midbox").css("height", PageHeight * count);
  }

 var ZoomBigCount = 0;
 function ZoomBig() {
     if (ZoomBigCount == 5) {
         return false;
     }
     ZoomBigCount = ZoomBigCount + 1;
     ZoomSmallCount = ZoomSmallCount - 1;
     if (ZoomBigCount == 5) {
         $("#azoom_big").removeClass("zoom_big");
         $("#azoom_big").addClass("zoom_nobig");
     }
     $("#azoom_small").removeClass("zoom_nosmall");
     $("#azoom_small").addClass("zoom_small");
     var top1 = parseInt($("#divImg1").css("top").replace("px", ""), 10);
     var top2 = parseInt($("#divImg2").css("top").replace("px", ""), 10);
     var top3 = parseInt($("#divImg3").css("top").replace("px", ""), 10);
     var num1 = top1 / PageHeight;
     var num2 = top2 / PageHeight;
     var num3 = top3 / PageHeight;
     var num = $("#read_mid_srollbar").scrollTop() / PageHeight;
     
     PageHeight = PageHeight + 50;
     
     $("#divImg1").css("height", PageHeight);
     $("#divImg1").css("top", parseInt($("#divImg1").css("top").replace("px", ""), 10) + 50 * num1);
     $("#divImg1").find("img").css("height", PageHeight - 12);
     $("#divImg2").css("height", PageHeight);
     $("#divImg2").css("top", parseInt($("#divImg2").css("top").replace("px", ""), 10) + 50 * num2);
     $("#divImg2").find("img").css("height", PageHeight - 12);
     $("#divImg3").css("height", PageHeight);
     $("#divImg3").css("top", parseInt($("#divImg3").css("top").replace("px", ""), 10) + 50 * num3);
     $("#divImg3").find("img").css("height", PageHeight - 12);

     $("#read_mid_srollbar").scrollTop($("#read_mid_srollbar").scrollTop() + 50 * num);

     var count = parseInt($("#hidEnd").val(), 10) - parseInt($("#hidStart").val(), 10) + 1;
     $("#read_midbox").css("height", PageHeight * count);
 }

 function GetDivSort() {
     var top1 = parseInt($("#divImg1").css("top").replace("px", ""), 10);
     var top2 = parseInt($("#divImg2").css("top").replace("px", ""), 10);
     var top3 = parseInt($("#divImg3").css("top").replace("px", ""), 10);
     var divID;
     var divID = "divImg1";
     if (top2 > top1) {
         if (top3 > top2) {
             divID = "divImg1,divImg2,divImg3";
         }
         else {
             if (top3 > top1) {
                 divID = "divImg1,divImg3,divImg2";
             } else {
                 divID = "divImg3,divImg1,divImg2";
             }
         }
     }
     else {
         if (top3 < top2) {
             divID = "divImg3,divImg2,divImg1";
         }
         else {
             if (top3 > top1) {
                 divID = "divImg2,divImg1,divImg3";
             } else {
                 divID = "divImg2,divImg3,divImg1";
             }
         }
     }

     return divID;
 }

 function InitLeft() {
     $("#divInfo").height(300)
     $("#divcon_text").height($("#read_main").height() - $("#divInfo").height() - 100);
     $.ajax({
         type: "GET",
         url: "/AdminknReader/request/ReaderSideHandler.ashx",
         cache: true,
         data: "op=0" + "&b=" + $("#hidBookID").val() + "&p=" + $("#hidPressID").val(),
         success: function (msg) {
             $("#divInfo").html(msg);
         }
     });

    $.ajax({
        type: "GET",
        url: "/AdminknReader/request/ReaderSideHandler.ashx",
        cache: true,
        data: "op=1" + "&b=" + $("#hidBookID").val() + "&p=" + $("#hidPressID").val(),
        success: function (msg) {
            $("#divcon_text").html(msg);
            BindCatalogClick();
        }
    });      
 }

 function InitRight() {
    var height = $("#read_main").height() - 95;
    $.ajax({
        type: "GET",
        url: "request/ReaderSideHandler.ashx",
        cache: true,
        data: "op=2" + "&b="+$("#hidBookID").val()+"&h="+height,
        success: function(msg)
        {
            $("#divComment_book").html(msg);            
        }
    }); 
    
    $.ajax({
        type: "GET",
        url: "request/ReaderSideHandler.ashx",
        cache: true,
        data: "op=3" + "&b="+$("#hidBookID").val()+"&h="+height,
        success: function(msg)
        {
            $("#divBrowse_book").html(msg);            
        }
    }); 
 }

 function CatalogClick() {
     if ($("#divcon_text").css("display") == "block") {
         $("#divcon_text").animate({ height: 0 }, "normal", function() { $("#divcon_text").hide();$("#h4merge").addClass("merge"); });
     } else {
         var height = $("#read_main").height() - $("#divInfo").height() - 100;
         $("#divcon_text").show();
         $("#divcon_text").animate({ height: height }, "normal", function() { $("#h4merge").removeClass("merge"); });
     }
 }

 function InfoClose() {
     $("#read_book").hide();
     $("#readbook_name").show();
     $("#aInfoClose").hide();
     $("#aInfoOpen").show();
     $("#divInfo").height(60);
     $("#divcon_text").height($("#read_main").height() - $("#divInfo").height() - 100);
 }

 function InfoOpen() {
     $("#readbook_name").hide();
     $("#read_book").show();
     $("#aInfoOpen").hide();
     $("#aInfoClose").show();
     $("#divInfo").height(300);
     $("#divcon_text").height($("#read_main").height() - $("#divInfo").height() - 100);
 }

 function ShowHistory(obj) {
     $(obj).parent().find("li").each(function() {
         $(this).removeClass("showtext");
     });
     $(obj).addClass("showtext");
     $("#divComment_book").hide();
     $("#divBrowse_book").show();
     $("#h4Comment").hide();
     $("#h4History").show()
 }

 function ShowComment(obj) {
     $(obj).parent().find("li").each(function() {
         $(this).removeClass("showtext");
     });
     $(obj).addClass("showtext");
     $("#divBrowse_book").hide();
     $("#divComment_book").show();
     $("#h4History").hide();
     $("#h4Comment").show()
 }
 
 function Ins_click(obj,parentId)
 { 
    if(obj.title=="合并")
    {
        //合并
        obj.title = "展开";
        $(obj).children("img").eq(0).hide();
        $(obj).children("img").eq(1).show();
        //只合并紧邻的子目录
        $("#divcon_text dd[id^='bCatalog"+parentId+"-']").each(function() {
            var subId = $(this).attr("id").substr(("bCatalog"+parentId+"-").length);
            if(subId.search("-") == -1)
            {
                $(this).hide();
            }     
        });   
    }
    else
    {
        //展开
        obj.title = "合并";
        $(obj).children("img").eq(1).hide();
        $(obj).children("img").eq(0).show();
        //只展开紧邻的子目录
        $("#divcon_text dd[id^='bCatalog"+parentId+"-']").each(function() {
            var subId = $(this).attr("id").substr(("bCatalog"+parentId+"-").length);
            if(subId.search("-") == -1)
            {
                $(this).show();
            }     
        }); 
    }
 }
 
 //购买方式选择
function displayBuySelect(typeId,typeValue) {
        $("#borrowRead,#downRead,#onlineRead").removeClass("select");
        $("#borrowRead,#downRead,#onlineRead").addClass("buy");
        $("#"+typeId).removeClass("buy");
        $("#" + typeId).addClass("select");
        $("#hidBuyStyle").val(typeValue);
}

//购买
function Buy(bookid) {
    AddToCart(bookid,$("#hidBuyStyle").val());
}

//收藏
function AddCollect(bookid) {
    $.ajax({
        type: "GET",
        url: "request/CollectHandler.ashx",
        cache: false,
        async: false,
        data: "bookid=" + bookid,
        success: function(msg) {
            alert(msg);
        }
    });
}

function AddToCart(ids,types)
{
    $.ajax({
    type: "GET",
    url: "request/ShoppingCartHandler.ashx",
    cache: false,
    async: false,
    data: "t=1&id=" + ids+"&type="+types,
    success: function(msg) { 
        }
    }); 
    //放在success中会被ie拦截
    window.open("../bookshelf/ShoppingCart.aspx","_blank");  
}

function Catalog_Click(obj,page)
{
   // $("#divcon_text a").removeClass("current");
   // $(obj).addClass("current");
    $("#divcon_text a").click(function () {
        var page = $(this).next("span").html();
        $(this).removeClass("current");
        $(this).addClass("current");
       SetPage(page);
    });
}

function BindCatalogClick() {
    $("#divcon_text a").click(function () {
        var page = $(this).next("span").html();
        $(this).removeClass("current");
        $(this).addClass("current");
        SetPage(page);
    });
}

function PopReader(pressid, bookid, page) {
    if (!page) {
        page = "";
    }
    $(window).resize(function() {
        SetSize();
        ResizeFrm();
    });
    SetSize();
    $(document.body).css("overflow", "hidden");
    var wHeight = $(window).height();
    var wWidth = $(window).width();
    var dHeight = $(document).height();
    var sHeight = $(window).scrollTop();
    //alert(wHeight);
    $("#divReader").addClass("divReader");
    $("#divReader").show();
    $("#divReader").html("<div class='readbg' style='height:" + dHeight + "px;'></div><div class='readbgload'></div>" +
            "<iframe id='frmReader' allowtransparency='ture' name='frmReader' src='reader.aspx?h=" + (wHeight - 80) + "&w=" + (wWidth - 80) + "&p=" + pressid + "&b=" + bookid + "&pg=" + page + "' class='readiframe'" +
            "frameborder='0' height='" + (wHeight - 80) + "' width='" + (wWidth - 80) + "' style='left:40px;top:" + (sHeight + 40) + "px;'></iframe>");
}

function SetSize() {
    $("#hidHeight").val($(window).height());
    $("#hidWidth").val($(window).width());
}

function ResizeFrm() {
    var height = $(window).height();
    var width = $(window).width();
    if (parseInt($("#frmReader").css("left"), 10) > 0) {
        height = height - 80;
        width = width - 80;
    }
    $("#frmReader").css("height", height);
    $("#frmReader").css("width", width);

    var midWidth = 412;
    if ($("#frmReader").contents().find("#read_lef").css("display") == "none") {
        midWidth = midWidth - 180;
    }

    if ($("#frmReader").contents().find("#read_rig").css("display") == "none") {
        midWidth = midWidth - 200;
    }

    $("#frmReader").contents().find("#read_main").css("height", height - 2);
    $("#frmReader").contents().find("#read_main").css("width", width - 2);
    $("#frmReader").contents().find("#read_content").css("height", height - 46);
    $("#frmReader").contents().find("#read_mid").css("width", width - midWidth);
}

function SetHighLight(msg) {
    //alert(page);
    var content = msg.split('$');
    if (Compare(content[0], content[1]))
        return;
    $("#read_midbox #Img" + (content[0])).parent().children("div").remove();
    //document.getElementById("Img" + content[0]).parentNode.innerHTML = content[1] + document.getElementById("Img" + content[0]).outerHTML;
    $("#read_midbox #Img" + (content[0])).before(content[1]);
}

function ClearHighLight() {
    //alert($("#read_midbox div span div").length);
    $("#read_midbox div span div").remove();
}

function ClearHighLight1(page) {
    //alert($("#read_midbox div span div").length);
    //$("#read_midbox div span div").remove();
    $("#read_midbox #Img" + (page)).parent().children("div").remove();
}

function Compare(page, html) {
    var iCount = $("#read_midbox #Img" + (page)).parent().children("div").length;
    if (iCount == 0) {
        return false;
    }
    
    var arrDiv = html.split("</div>");

    if (iCount != arrDiv.length - 1) {
        return false;
    }

    for (var i = 0; i < iCount; i++) {
        if (GetPosition("left", arrDiv[i]) != $("#read_midbox #Img" + (page)).parent().children("div")[i].style.left.replace("px", "")) {
            return false;
        }

        if (GetPosition("top", arrDiv[i]) != $("#read_midbox #Img" + (page)).parent().children("div")[i].style.top.replace("px", "")) {
            return false;
        }
        if (GetPosition("height", arrDiv[i]) != $("#read_midbox #Img" + (page)).parent().children("div")[i].style.height.replace("px", "")) {
            return false;
        }
        if (GetPosition("width", arrDiv[i]) != $("#read_midbox #Img" + (page)).parent().children("div")[i].style.width.replace("px", "")) {
            return false;
        }
    }

    return true;  

}

function GetPosition(name, str) {
    var iStart = str.indexOf(name + ":");
    var iEnd = str.indexOf("px", iStart);

    var Ret = str.substring(iStart + name.length + 1, iEnd);
    return Ret;
    //var strTemp=str.sub
}

function EnterClick() {
    if (event.keyCode == 13) {
        SearchClick();
        return false;
    }
}
function SearchClick() {
    if ($("#txtSearch").val() == "") {
        alert("请输入检索词！");
        return false;
    }
    SearchText();
}
function SearchText() {
    var text = $("#txtSearch").val();
    if (text == "") {
        return false;
    }
    var page = 1;
    $("#read_midbox img").each(function() {
        page = $(this).attr("id").replace("Img", "");

        $.ajax({
            type: "GET",
            url: "request/TebSearchHandler.ashx",
            cache: false,
            data: "b=" + $("#hidBookID").val() + "&press=" + $("#hidPressID").val() + "&p=" + page + "&t=" + escape(text) + "&w=" + $(this).width() + "&h=" + $(this).height(),
            success: function(msg) {
                var content = msg.split('$');
                if (content[1] != "") {
                    //alert(msg);
                    SetHighLight(msg);
                }
                else {
                    ClearHighLight1(content[0]);
                }
            }
        });
    })
}

function SearchText1(page) {
    var text = $("#txtSearch").val();
    if (text == "") {
        return false;
    }
    $.ajax({
        type: "GET",
        url: "request/TebSearchHandler.ashx",
        cache: false,
        data: "b=" + $("#hidBookID").val() + "&press=" + $("#hidPressID").val() + "&p=" + page + "&t=" + escape(text) + "&w=" + $(this).width() + "&h=" + $(this).height(),
        success: function(msg) {
        var content = msg.split('$');
        if (content[1] != "") {
            //alert(msg);
            SetHighLight(msg);
        }
        else {
            ClearHighLight1(content[0]);
        }
        }
    });
}