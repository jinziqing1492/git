// JavaScript Document
$(function () {
    //刷新页面
    //setInterval("refreshBodyHeight()", 1000);
    refreshBodyHeight();
});
function refreshBodyHeight() {
    $(".TYContainer_Table table>tbody>tr:odd,.TYTabTerrace_Table table>tbody>tr:odd").css("background", "#eef3f4");
    $(".TYSubnavL_list li a").click(function () {
        $(this).siblings(".DBSubnavL_subordinate").slideDown();
        $(this).parent("li").siblings().children(".DBSubnavL_subordinate").slideUp();
    });
    $(".TYSubnavL_Mytask li a").click(function () {
      //  $(this).parent("li").addClass("TYSubnav_actives").siblings().removeClass("TYSubnav_actives");
    });
}
function setSlideDownMenu(liId) {
    $("#" + liId).siblings(".DBSubnavL_subordinate").slideDown();
    $("#" + liId).parent("li").siblings().children(".DBSubnavL_subordinate").slideUp();
   // $("#" + liId).parent("li").addClass("TYSubnav_actives").siblings().removeClass("TYSubnav_actives");
}