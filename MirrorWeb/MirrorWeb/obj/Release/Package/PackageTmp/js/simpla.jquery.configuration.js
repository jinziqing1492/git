$(document).ready(function () {
    ReactivateMenuNav();

    //Sidebar Accordion Menu:

    $("#main-nav li ul").hide(); // Hide all sub menus
    $("#main-nav li a.current").parent().find("ul").slideToggle("slow"); // Slide down the current menu item's sub menu

    $("#main-nav li a.nav-top-item").click( // When a top menu item is clicked...
			function () {
			    $(this).parent().siblings().find("ul").slideUp("normal"); // Slide up all sub menus except the one clicked
			    $(this).next().slideToggle("normal"); // Slide down the clicked sub menu
			    return false;
			}
		);

    $("#main-nav li a.no-submenu").click( // When a menu item with no sub menu is clicked...
			function () {
			    window.location.href = (this.href); // Just open the link instead of a sub menu
			    return false;
			}
		);

    // Sidebar Accordion Menu Hover Effect:

    $("#main-nav li .nav-top-item").hover(
			function () {
			    $(this).stop().animate({ paddingRight: "25px" }, 200);
			},
			function () {
			    $(this).stop().animate({ paddingRight: "15px" });
			}
		);

    //Minimize Content Box

    $(".content-box-header h3").css({ "cursor": "s-resize" }); // Give the h3 in Content Box Header a different cursor
    $(".closed-box .content-box-content").hide(); // Hide the content of the header if it has the class "closed"
    $(".closed-box .content-box-tabs").hide(); // Hide the tabs in the header if it has the class "closed"

    $(".content-box-header h3").click( // When the h3 is clicked...
			function () {
			    $(this).parent().next().toggle(); // Toggle the Content Box
			    $(this).parent().parent().toggleClass("closed-box"); // Toggle the class "closed-box" on the content box
			    $(this).parent().find(".content-box-tabs").toggle(); // Toggle the tabs
			}
		);

    // Content box tabs:

    $('.content-box .content-box-content div.tab-content').hide(); // Hide the content divs
    $('ul.content-box-tabs li a.default-tab').addClass('current'); // Add the class "current" to the default tab
    $('.content-box-content div.default-tab').show(); // Show the div with class "default-tab"

    $('.content-box ul.content-box-tabs li a').click( // When a tab is clicked...
			function () {
			    $(this).parent().siblings().find("a").removeClass('current'); // Remove "current" class from all tabs
			    $(this).addClass('current'); // Add class "current" to clicked tab
			    var currentTab = $(this).attr('href'); // Set variable "currentTab" to the value of href of clicked tab
			    $(currentTab).siblings().hide(); // Hide all content divs
			    $(currentTab).show(); // Show the content div with the id equal to the id of clicked tab
			    return false;
			}
		);

    //Close button:

    $(".close").click(
			function () {
			    $(this).parent().fadeTo(400, 0, function () { // Links with the class "close" will close parent
			        $(this).slideUp(400);
			    });
			    return false;
			}
		);

    // Alternating table rows:

    $('tbody tr:even').addClass("alt-row"); // Add class "alt-row" to even table rows

    $("table[pageno][pagesize]").each(function () {
        var pageNo = $(this).attr("pageno");
        var pageSize = $(this).attr("pagesize");
        if ($.isNumeric(pageNo) == false || $.isNumeric(pageSize) == false) {
            return;
        }
        pageNo = parseInt(pageNo);
        if (pageNo <= 0) {
            pageNo = 1;
        }
        pageSize = parseInt(pageSize);
        $(this).find("td.record-no").each(function (index) {
            $(this).html(index + 1 + (pageNo - 1) * pageSize);
        });
    });

    $(".pagination > .aspNetPager > a").removeAttr("style").addClass("number");

    // Check all checkboxes when the one in a table head is checked:

    $('.check-all').click(
			function () {
			    $(this).parent().parent().parent().parent().find("input[type='checkbox']").attr('checked', $(this).is(':checked'));
			}
		);
    $("#loading").bind("ajaxStart", function () {
        $(this).removeClass("hide");
    }).bind("ajaxComplete", function () {
        $(this).addClass("hide");
    });

    $(".position .nav-date").html(GetCurrentDate(true));
    //  点击导航链接，保存当前活动项信息
    $("#main-nav > li > a.nav-top-item").click(function () {
        var index = $("#main-nav > li > a.nav-top-item").index($(this));
        SetCookie("mainNav", index, 1);
        SetCookie("targetPage", "", 1);
    });
    $("#main-nav > li > ul > li > a").not("a.action").click(function () {
        var index = $(this).parent().parent().find("a").index($(this));
        SetCookie("subNav", index, 1);
        SetCookie("targetPage", "", 1);
    });

    //  管理页点击顶部导航链接，保存目的地址作为活动项
    $(".shortcut-buttons-set > li > a.shortcut-button").not(".shortcut-buttons-set > li.action > a.shortcut-button").click(function () {
        var href = $(this).attr("href");
        var fileName = GetUrlFileName(href);
        if (fileName == "") {
            return;
        }
        SetCookie("targetPage", fileName, 1);
    });

    var deleteConfirm = false;
    //  删除前提示
    $("input.confirm-delete").click(function () {
        //        if (confirm("您确定要删除吗？")) {
        //            return true;
        //        }        
        if ($(this).attr("confirm")) {
            deleteConfirm = $(this).attr("confirm");
        }
        if (deleteConfirm == "true") {
            return true;
        }
        var id = $(this).attr("id");
        $(this).removeAttr("confirm").attr("confirm", deleteConfirm);
        if ($("#dialog-delete-confirm").length == 0) {
            var html = "<div id=\"dialog-delete-confirm\" class=\"hide\" title=\"您确认要删除吗？\">";
            html += "<p>";
            html += "<span class=\"ui-icon ui-icon-alert\" style=\"float:left; margin:0 7px 20px 0;\"></span>";
            html += "当前项将被删除并且无法恢复。您确定吗？";
            html += "</p>";
            html += "</div>";
            $(html).appendTo($("body"));
        }
        $("#dialog:ui-dialog").dialog("destroy");
        $("#dialog-delete-confirm").dialog({
            resizable: false,
            height: 140,
            width: 400,
            modal: true,
            buttons: {
                "确定": function () {
                    $(this).dialog("close");
                    $("#" + id).removeAttr("confirm").attr("confirm", true).trigger("click");
                },
                "取消": function () {
                    $(this).dialog("close");
                }
            }
        });
        return false;
    });

    var batchActionConfirm = false;
    //  批量操作提示
    $("input.confirm-batch-action").click(function () {
        var $targetHidden = $(this).next(":hidden.target-list");
        if ($targetHidden.length == 0) {
            return false;
        }
        $tbody = jQuery(this).parents("tfoot").next("tbody");
        var targetList = GetSelectedCheckoxValue($tbody);
        if (targetList == "") {
            return false;
        }
        $targetHidden.val(targetList);
        var $actionSelect = $(this).prev("select.action-list");
        if ($actionSelect.length == 0) {
            return false;
        }
        var actionOption = $actionSelect.find("option:selected");
        var actionText = actionOption.text();
        var actionValue = actionOption.val();
        if (actionValue == "" || actionText == "") {
            return false;
        }
        if ($(this).attr("confirm")) {
            batchActionConfirm = $(this).attr("confirm");
        }
        if (batchActionConfirm == "true") {
            return true;
        }
        var id = $(this).attr("id");
        $(this).removeAttr("confirm").attr("confirm", batchActionConfirm);
        if ($("#dialog-batch-action-confirm").length == 0) {
            var html = "<div id=\"dialog-batch-action-confirm\" class=\"hide\" title=\"您确认要" + actionText + "当前页选择项吗？\">";
            html += "<p>";
            html += "<span class=\"ui-icon ui-icon-alert\" style=\"float:left; margin:0 7px 20px 0;\"></span>";
            html += "当前页选择项将被" + actionText + "。您确定吗？";
            html += "</p>";
            html += "</div>";
            $(html).appendTo($("body"));
        }
        $("#dialog:ui-dialog").dialog("destroy");
        $("#dialog-batch-action-confirm").dialog({
            resizable: false,
            height: 140,
            width: 400,
            modal: true,
            buttons: {
                "确定": function () {
                    $(this).dialog("close");
                    $("#" + id).removeAttr("confirm").attr("confirm", true).trigger("click");
                },
                "取消": function () {
                    $(this).dialog("close");
                }
            }
        });
        return false;
    });

    //  级联选择
    $(".form-container span.radio-box > label").click(function () {
        $(this).parent().children(":radio").trigger("click");
    });

    //  清空输入
    $(".shortcut-buttons-set > li > a.clear-input").click(function () {
        $(".form-container :text:visible, .form-container textarea:visible").each(function () {
            if ($(this).prop("readonly")) {
                return;
            }
            $(this).val("");
            if ($(this).is("textarea")) {
                $(this).empty();
            }
        });
        return false;
    });

    //  筛选选记录
    $(".shortcut-buttons-set > li#record-filter > a").click(function () {
        $("#dialog-condition-box").dialog({
            autoOpen: false,
            //    modal: true,
            width: 600,
            buttons: {
                "提交": function () {
                    $(this).dialog("close");
                    SubmitCustomisedSearch();
                },
                "取消": function () {
                    $(this).dialog("close");
                }
            }
        });
        $("#dialog:ui-dialog").dialog("destroy");
        $("#dialog-condition-box").dialog('open');
        return false;
    });
});
function GetSelectedCheckoxValue($tbody) {
    ///<summary>获取tbody中选择的checkbox的值
    /// <para>tbody的jquery对象</para>
    ///</summary>
    if (!$tbody || $tbody.lengt == 0) {
        return "";
    }
    var selectedValue = "";
    $tbody.find("tr td:first-child :checkbox:checked").each(function () {
        selectedValue += $(this).val();
        selectedValue += ";";
    });
    return selectedValue.trimEnd(";");
}
function SubmitCustomisedSearch() {
    ///<summary>提交自定义查询
    ///</summary>
    var $searchBtn = $(".customised-search-trigger");

    if ($searchBtn.length == 0) {
        return;
    }
    var $queryConditionContainer = $searchBtn.prev(":hidden");
    if ($queryConditionContainer.length == 0) {
        return;
    }
    GetCustomisedQueryCondition();
    $searchBtn.trigger("click");
}
function GetCustomisedQueryCondition() {
    ///<summary>生成自定义查询条件(页面需要自定义内容)
    ///</summary>
}
function GetTargetPageNavActiveIndex(pageName){
	///<summary>获取目标页面在导航菜单中的序号信息
	/// <para>页面</para>
	///</summary>
	if (!pageName || pageName == "") {
		return null;
}

	var mainNavIndex = null, subNavIndex = null;
	$("#main-nav > li").each(function (index) {
		var currentLink = $(this).children("a");
		var currentPageName = GetUrlFileName(currentLink.attr("href"));
		if (pageName.toLowerCase() == currentPageName.toLowerCase()) {
			mainNavIndex = index;
			subNavIndex = null;
			return false;
		}
		$(this).find("ul li a").each(function (i) {
			currentPageName = GetUrlFileName($(this).attr("href"));
			if (pageName.toLowerCase() == currentPageName.toLowerCase()) {
				mainNavIndex = index;
				subNavIndex = i;
				return false;
			}
		});
	});
//    var tempArray = new Array();
//    if (mainNavIndex) {
//        tempArray.push(mainNavIndex);
//    }
//    if (subNavIndex) {
//        tempArray.push(subNavIndex);
//    }
	var result = {
		mainNavActiveIndex: mainNavIndex, 
		subNavActiveIndex: subNavIndex
	};
	return result;
}
function ReactivateMenuNav() {
	///<summary>读取cookie，恢复导航菜单活动项
	///</summary>
	var targetPage = "";
	var mainNavActiveIndex = "";
	var subNavActiveIndex = "";

	//  读取优先级高的目的页面
	targetPage = GetCookie("targetPage");
	if (QueryString("flag") == "active") {
		targetPage = GetUrlFileName(window.location.href);
	}
	if (targetPage != "") {
		var indexObj = GetTargetPageNavActiveIndex(targetPage);
		mainNavActiveIndex = indexObj.mainNavActiveIndex;
		subNavActiveIndex = indexObj.subNavActiveIndex;
	} else {
		//  从cookie中读取当前导航的活动项
		mainNavActiveIndex = GetCookie("mainNav");
		subNavActiveIndex = GetCookie("subNav");
	}
	if ($.isNumeric(mainNavActiveIndex) == false) {
		mainNavActiveIndex = 0;
	}
	else {
		mainNavActiveIndex = parseInt(mainNavActiveIndex);
	}
	$("#main-nav > li > a.nav-top-item").removeClass("current");
	var currentMainNav = $("#main-nav > li > a.nav-top-item").eq(mainNavActiveIndex).addClass("current");
	var mainLabel = $.trim(currentMainNav.html());
	$(".position").find("span.main-path").html(mainLabel);
	if (subNavActiveIndex == "" && currentMainNav.next().children("li").length == 0) {        
		return;
	}
	if ($.isNumeric(subNavActiveIndex) == false) {
		subNavActiveIndex = 0;
	}
	else{
		subNavActiveIndex = parseInt(subNavActiveIndex);
	}
	var activeLabel = $.trim(currentMainNav.next().find("a").eq(subNavActiveIndex).addClass("current").html());
	$(".position").find("span.sub-path").html(activeLabel);
}
  
  
  