// JavaScript Document
$(function () {
    //绑定元素事件
    //目录
    $(".xlcatalogue img").click(function () {
        if ($(this).attr("src") == "../images/fjzhankai.gif") {
            $(this).attr("src", "../images/fjshrink.gif");

            $(this).parent().parent().children("ul").children("li").each(function () {
                var childullength = $(this).children("ul").children("li").length;
                if (childullength <= 0) {
                    $(this).children("p").children("img").remove();
                    $(this).children("p").children("a").css("margin-left", "16px");
                }
            });

            $(this).parent().parent().children("ul").show();
        }
        else {
            $(this).attr("src", "../images/fjzhankai.gif");
            $(this).parent().parent().children("ul").hide();
        }
    });
    $(".xlTiltzoom a").click(function () {
        var discss = $(this).parents().parents().siblings(".XLIntroduction-text").css("height");
        if (discss) {
            if (parseInt(discss.trimEnd("px")) > 67) {
                $(this).parents().parents().siblings(".XLIntroduction-text").css("height", "67px");
                $(this).addClass("xlshow");
            }
            else {
                $(this).parents().parents().siblings(".XLIntroduction-text").css("height", "auto");
                $(this).removeClass("xlshow");
            }
            return;
        }

        var discss = $(this).parents().parents().siblings(".XLContent-text").css("height");
        if (discss) {
            if (parseInt(discss.trimEnd("px")) > 120) {
                $(this).parents().parents().siblings(".XLContent-text").css("height", "120px");
                $(this).addClass("xlshow");
            }
            else {
                $(this).parents().parents().siblings(".XLContent-text").css("height", "auto");
                $(this).removeClass("xlshow");
            }
        }
    });

    //文件
    $(".fjdocument p").mouseover(function () {
        $(this).addClass("fjact");
        $(this).children(".fjoperate").show();
    }).mouseout(function () {
        $(this).removeClass("fjact");
        $(this).children(".fjoperate").hide();
    });
    $(".fjspread a").click(function () {
        if ($(this).parents("p").siblings(".fjsecondary").css("display") == "none") {
            $(this).parents("p").siblings(".fjsecondary").show();
            $(this).addClass("active");
        }
        else {
            if ($(this).attr("class") == "active") {
                $(this).parents("p").siblings(".fjsecondary").hide()
                $(this).removeClass("active");
            }
            else {
                $(this).addClass("active");
            }
        }
    });
});
