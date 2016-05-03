$(function () {
    if (GetCookie("issingle") != "false") {
        setInterval(setUserCache, 29000);//设置每隔29秒，向后台发送一次信息，设为30秒容易使ajax的参数相等，从而出现缓存
    }
});
function setUserCache() {
    var cookieTimeout = 25;//cookie的过期时间

    //因为用户可能会打开多个页面，每个页面都向后台发送数据的话会对服务器造成压力。所以，设定一个cookie，如果某个页面向后台回发了数据，则记录，之后的页面便不会再向后台发送数据
    if (GetCookie("singleuser") != "1") {
        SetCookie("singleuser", "1", cookieTimeout * (1 / 24 / 60 / 60)); //记录cookie 过期时间为25秒
        var date = new Date();
        $.get("../Ajax/SetUserCache.ashx?time=" + date.getMilliseconds() + date.getMinutes() + date.getSeconds());
    }
}