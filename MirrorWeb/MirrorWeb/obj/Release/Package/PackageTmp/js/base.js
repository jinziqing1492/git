$(function () {
    $(".hot-title a").hover(function () {
        var idxNum = $(".hot-title a").index($(this));
        $(this).addClass("select").siblings("a").removeClass("select");
        $(".hot-list").eq(idxNum).show().siblings(".hot-list").hide();
    })

    $(".res-title a").hover(function () {
        var idxNum = $(".res-title a").index($(this));
        $(this).addClass("select").siblings("a").removeClass("select");
        $(".res-list").eq(idxNum).show().siblings(".res-list").hide();
    })





    //    $(".mod-btn a").hover(function () {
    //        var idxNum = $(this).parent(".mod-btn").children("a").index($(this));
    //        $(this).addClass("moda").siblings("a").removeClass("moda");
    //        $(this).parents(".module").children(".mod-main").eq(idxNum).show().siblings(".mod-main").hide();
    //    })
    /*----------------retrievalPage---------------------*/
    $(".btn-retrieval").hover(function () {
        $(this).addClass("btn-rtl").siblings("em").removeClass("btn-rtl");
    })
    /*----------------picscroll---------------------*/
    var length = $(".ps-small ul li").length;
    $(".ps-small ul li").mouseover(function () {
        index = $(".ps-small ul li").index(this);
        showPic(index);
    }).eq(0).mouseover();

    $(".pic-scroll").hover(function () {
        clearInterval(show_adTimer);
    }, function () {
        show_adTimer = setInterval(function () {
            showPic(index);
            index++;
            if (index == length) { index = 0; }
        }, 3000);
    }).trigger("mouseleave");

    function showPic(index) {
        $(".pic-scroll p").hide();
        $(".pic-scroll p").eq(index).show();
        $(".pic-scroll ul li").removeClass("select").eq(index).addClass("select");
    }
    /*----------------book city retrieval---------------------*/
    $(".shopHeader a").hover(function () {
        var idxNum = $(".shopHeader a").index($(this));
        $(this).addClass("select").siblings("a").removeClass("select");
        $(".shopMain").eq(idxNum).show().siblings(".shopMain").hide();
    })

    $(".btnShop").hover(function () {
        var idxNum = $(this).parent(".spbBtn").children("a").index($(this));
        $(this).addClass("btnShopSelect").siblings("a").removeClass("btnShopSelect");
    })

    //$(".kbdTab a").click(function () {
    //var idxNum = $(".kbdTab a").index($(this));
    //$("#ObserverType").val(idxNum);
    //$(this).addClass("select").siblings("a").removeClass("select");
    //$(".kbdt").eq(idxNum).show().siblings(".kbdt").hide();
    //__doPostBack("","");
    //var path = window.location.href;
    //path += "&type=" + idxNum;
    //window.location.href = path;
    //})

    //switch-pic
    var len = $(".pic-num a").length;
    var index = 0;
    var adTimer;
    $(".pic-show").width($(".pic-show").width() * len);
    $(".pic-num a").mouseover(function () {
        index = $(".pic-num a").index(this);
        showImg(index);
    }).eq(0).mouseover();

    $(".pic-switch").hover(function () {
        clearInterval(adTimer);
    }, function () {
        adTimer = setInterval(function () {
            showImg(index);
            index++;
            if (index == len) { index = 0; }
        }, 3000);
    }).trigger("mouseleave");

    function showImg(index) {
        var adWidth = $(".pic-switch").width();
        $(".pic-show").stop(true, false).animate({ left: -adWidth * index }, 1000);
        $(".pic-num a").removeClass("select").eq(index).addClass("select");
    }


});
///主题库切换
function limouse(obj) {
    $(obj).addClass("active");
    $(obj).siblings().removeClass("active");
    var index = $(obj).index();
    $(obj).parents(".news-mod").children("ul").eq(index).show().siblings("ul").hide();

    if ($(obj).parents(".news-mod").children("ul").eq(index).html() == "" && index != 0) {
        var dbdoi = $(obj).parents(".newstitle").children("a").attr("field");
        if (dbdoi) {
            $.ajax({
                type: "GET",
                cache: false,
                url: "/Ajax/KnowledgeIndex.ashx",
                data: "dbdoi=" + dbdoi + "&type=" + index,
                success: function (data) {
                    $(obj).parents(".news-mod").children("ul").eq(index).html(data);
                }
            });
        }
    }
}
//type  0:图书 1：条目 2：标准  3：文章  4：图片  5：音频  6：视频
function Sorthover(obj, dbID, type) {
    var idxNum = $(obj).parent(".mod-btn").children("a").index($(obj));
    $(obj).addClass("moda").siblings("a").removeClass("moda");

    $(obj).parents(".module").children(".mod-main").eq(idxNum).show().siblings(".mod-main").hide();

    var book = $(obj).parents(".module").children(".mod-main").eq(idxNum);

    if ($(obj).parents(".module").children(".mod-main").eq(idxNum).html() == "") {
        $.ajax({
            type: "GET",
            url: "../Ajax/SortDBHandler.ashx",
            cache: false,
            data: "dbid=" + dbID + "&type=" + type,
            dataType: "json",
            success: function (data) {
                var main = $(obj).parents(".module").children(".mod-main").eq(idxNum);

                //再次判断主div是否为空否则有可能出现两次加载
                if (main.html() == "") {
                    if (data) {
                        //如果type=list 则表明数据为条目或者文章，需要显示成列表的方式
                        if (data[0].Type != "list") {
                            for (var i = 0; i < data.length; i++) {

                                var bookTitle = subStr(data[i].Title, 20);
                                var bookAuthor = subStr(data[i].Author, 12);
                                var doi = data[i].Doi;
                                var hrefUrl = "";
                                if (data[i].Type == "0") {
                                    hrefUrl = "/Page/BookDetail.aspx?doi=" + doi;
                                }
                                else {
                                    hrefUrl = "/Page/BookDetail.aspx?doi=" + doi + "&type=1";
                                }

                                var mainDiv = $("<div  class='book'></div>");
                                $(mainDiv).append("<div class='book-pic'><a href='" + hrefUrl + "'>" + data[i].Img + "</a></div>");
                                var bookDiv = $('<div class="book-main"></div>');
                                $(bookDiv).append("<p class='book-title' title='" + data[i].Title + "'><a href='" + hrefUrl + "' target='_blank'>" + bookTitle + "</a></p>");
                                $(bookDiv).append("<p class='book-author'>作 译 者：<span title='" + data[i].Author + "'>" + bookAuthor + "&nbsp;编</span></p>");
                                $(mainDiv).append(bookDiv);
                                $(mainDiv).appendTo(main);
                            }
                        }
                        else {
                            //设置主div css属性
                            main.css({ padding: "0px", height: "153px" });
                            //建立左侧子div
                            var leftDiv = $("<div class='infor-main' style='width:46%; float:left; border:none;'></div>");
                            var rightDiv = $("<div class='infor-main' style='width:43%; float:left; border:none;'></div>");
                            var leftul = $("<ul></ul>");
                            var rightul = $("<ul></ul>");
                            for (var i = 0; i < data.length; i++) {
                                var bookTitle = subStr(data[i].Title, 26);
                                var newurl = data[i].Url;
                                
                                if (i < 4) {
                                    if (i == 0) {
                                        leftul.append("<li class='noBorder'><a href='" + newurl + "' target='_blank' title='" + data[i].Title + "'>" + bookTitle + "</a></li>");
                                    }
                                    else {
                                        leftul.append("<li><a href='" + newurl + "' target='_blank' title='" + data[i].Title + "'>" + bookTitle + "</a></li>");
                                    }
                                }
                                else {
                                    if (i == 4) {
                                        rightul.append("<li class='noBorder'><a href='" + newurl + "' target='_blank' title='" + data[i].Title + "'>" + bookTitle + "</a></li>");
                                    }
                                    else {
                                        rightul.append("<li><a href='" + newurl + "' target='_blank' title='" + data[i].Title + "'>" + bookTitle + "</a></li>");
                                    }
                                }
                            }
                            leftul.appendTo(leftDiv);
                            rightul.appendTo(rightDiv);
                            leftDiv.appendTo(main);
                            main.append("<img src='images/v_line.jpg' alt='' style=' float:left;'/>");
                            $(rightDiv).appendTo(main);
                        }
                    }
                }
            }
        });
    }
}
//截取字符串  
function subStr(str_sub, count) {
    if (str_sub.length > count) {
        return str_sub.substr(0, count) + "...";
    }
    else {
        return str_sub;
    }
}
//添加收藏
function addFavorite(doi,type,name,remark) {
    $.ajax({
        type: "GET",
        url: "/Ajax/CreateFavoriteData.ashx",
        cache: false,
        data: "doi=" + doi + "&type=" + type + "&name=" + name + "&remark=" + remark,
        success: function (data) {
            if (data == "-1") {
                alert("请先登陆!");
            }
            else if (data == "0") {
                alert("收藏失败!");
            }
            else if (data == "1") {
                alert("收藏成功!");
            }
            else if (data == "2") {
                alert("已收藏过！不能重复收藏！");
            }
        }
    });
}

function showNote() {
    $(".notetitle").mouseover(function (e) {
        var noteLeft = e.pageX;
        var noteWidth = $(".divnote").width();
        if (e.pageX + noteWidth > $("body").width()) {
            noteLeft = $("body").width() - noteWidth - 10;
        }
        $(this).next(".divnote").css({ top: e.pageY + 20, left: noteLeft });
        $(this).next(".divnote").show();
    });
    $(".notetitle").mouseout(function (e) {
        $(this).siblings(".divnote").hide();
    });
}



