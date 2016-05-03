//  工具类, 需要在引用该js前，引用jquery

// 格式化文件大小，显示KB/MB/GB
function FormatSize(size) {
    var SIZE_KB = 1024;
    var SIZE_MB = SIZE_KB * 1024;
    var SIZE_GB = SIZE_MB * 1024;
    //正则表达式取小数点后两位
    //var re = /([0-9]+\.[0-9]{2})[0-9]*/;
    //aNew = a.replace(re,"$1");
    if (size < SIZE_KB) {
        return size + "B";
    }
    else if (size < SIZE_MB) {
        return Math.round((parseFloat(size) / SIZE_KB) * 100) / 100 + "KB";
    }
    else if (size < SIZE_GB) {
        return Math.round((parseFloat(size) / SIZE_MB) * 100) / 100 + "MB";
    }
    else {
        return Math.round((parseFloat(size) / SIZE_GB) * 100) / 100 + "GB";
    }
}

//将json对象转换成字符串
function Obj2str(o) {
    if (o == undefined) {
        return "";
    }
    var r = [];
    if (typeof o == "string")
        return "\"" + o.replace(/([\"\\])/g, "\\$1").replace(/(\n)/g, "\\n").replace(/(\r)/g, "\\r").replace(/(\t)/g, "\\t") + "\"";
    if (typeof o == "object") {
        if (!o.sort) {
            for (var i in o) r.push("\"" + i + "\":" + Obj2str(o[i]));
            if (!!document.all && !/^\n?function\s*toString\(\)\s*\{\n?\s*\[native code\]\n?\s*\}\n?\s*$/.test(o.toString)) {
                r.push("toString:" + o.toString.toString());
            }
            r = "{" + r.join() + "}"
        } else {
            for (var i = 0; i < o.length; i++)
                r.push(Obj2str(o[i]));
            r = "[" + r.join() + "]";
        }
        return r;
    }
    return o.toString().replace(/\"\:/g, '":""');
}

//在查询字符串中获取参数
function QueryString(paramName, url) {
    ///<summary>获取链接queryString值，默认查询当前url（获取解码后的值）
    /// <para>queryString键</para>
    /// <para>链接地址，缺省时使用当前url</para>
    ///</summary>
    if (url == null) {
        url = window.location.search;
    }
    var oRegex = new RegExp('[\?&]' + paramName + '=([^&]+)', 'i');
    var oMatch = oRegex.exec(url);
    if (oMatch && oMatch.length > 1) {
        return decodeURIComponent(oMatch[1]);
    }
    else {
        return "";
    }
}

////  trim() 方法
//String.prototype.trim = function () {
//    return $.trim(this);
//}

//Trim方法
function trimchar(sString, cChar) {
    while (sString.substring(0, 1) == cChar) {
        sString = sString.substring(1, sString.length);
    }
    while (sString.substring(sString.length - 1, sString.length) == cChar) {
        sString = sString.substring(0, sString.length - 1);
    }
    return sString;
}

String.prototype.startsWith = function (str) {
    if (str == null || str == "" || this.length == 0 || str.length > this.length)
        return false;
    if (this.substr(0, str.length) == str)
        return true;
    else
        return false;
    return true;
}
String.prototype.endsWith = function (str) {
    if (str == null || str == "" || this.length == 0 || str.length > this.length)
        return false;
    var startIndex = this.length - str.length;
    if (this.substr(startIndex, str.length) == str)
        return true;
    else
        return false;
    return true;
}

String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
String.prototype.LTrim = function () {
    return this.replace(/(^\s*)/g, "");
}
String.prototype.RTrim = function () {
    return this.replace(/(\s*$)/g, "");
}

// trimEnd
String.prototype.trimEnd = function (trimStr) {
    if (!trimStr) { return this; }
    var temp = this;
    while (true) {
        if (temp.substr(temp.length - trimStr.length, trimStr.length) != trimStr) {
            break;
        }
        temp = temp.substr(0, temp.length - trimStr.length);
    }
    return temp;
}

// replaceAll
String.prototype.replaceAll = function (oldStr, newStr) {
    if (!oldStr) { return this; }
    var temp = this;
    var regexp = new RegExp(oldStr, "g");
    temp = temp.replace(regexp, newStr);
    return temp;
}

// RestructuringStrByChar
String.prototype.restructuringStr = function (baseChar) {
    if (!baseChar) { return this; }
    var temp = this;
    if (temp.indexOf(baseChar) == -1)
        return temp;
    var tempArr = temp.split(baseChar);
    var tempResult = [];
    for (var i = 0, j = 0; i < temp.length; i++) {
        if (tempArr[i])
            tempResult[j++] = tempArr[i];
    }
    temp = tempResult.join(",");
    return temp;
}

//  判断字符串是否是数字
String.prototype.isNumber = function () {
    return $.isNumeric(this);
}
function GetRandomFlag() {
    ///<summary>根据时刻，获取随机标识</summary>
    var date = new Date();
    return date.getTime();
}

//  String.Format方法（注意大小写一致）
String.Format = function (src) {
    if (arguments.length == 0) return null;
    var args = Array.prototype.slice.call(arguments, 1);
    return src.replace(/\{(\d+)\}/g, function (m, i) {
        return args[i];
    });
};
//浏览器类型侦测
var isIE = function () {
    try {
        var isietemp = (document.all) ? true : false; //这里仅仅简单的对是否是IE进行判断，详细浏览器判断：请参考浏览器类型侦测
        var ua = navigator.userAgent.toLowerCase().match(/msie ([\d.]+)/)[1];
        if (ua == "9.0") {
            isietemp = false;
        }
        return isietemp;
    }
    catch (e)
    { return false; }
}

function IsNumber(obj) {
    ///<summary>判断是否是数字
    ///<para>输入</para>
    ///</summary>
    return $.isNumeric(obj);
}
function IsInt(obj) {
    ///<summary>判断是否是整形
    ///<para>输入</para>
    ///</summary>
    return $.isNumeric(obj) ? parseInt(obj) == obj : false;
}
function IsFloat(obj) {
    ///<summary>判断是否是浮点型
    ///<para>输入</para>
    ///</summary>
    return $.isNumeric(obj) ? parseFloat(obj) == obj : false;
}

function SetCookie(name, value, timeSpan) {
    ///<summary>将字符串保存到cookie(需要引入jquery.cookie插件)
    ///<para>名称</para>
    ///<para>值</para>
    ///<para>时间（单位：天）</para>
    ///</summary>
    if ($.isNumeric(timeSpan) == false || timeSpan <= 0) {
        return;
    }
    $.cookie(name, encodeURIComponent(value), {
        expires: timeSpan,
        path: '/'
    });
}
function GetCookie(name) {
    ///<summary从cookie中读取值(需要引入jquery.cookie插件)
    ///<para>cookie名称</para>
    ///</summary>
    var cookieValue = $.cookie(name);
    if (cookieValue == undefined || cookieValue == null) {
        cookieValue = "";
    }
    return cookieValue;
}

function GetUrlFileName(url) {
    ///<summary>获取url中文件名称
    ///<para>url地址</para>
    ///<summary>
    if (!url || url == "") {
        return "";
    }
    url = url.trim("#");
    if (url.indexOf("?") > 0) {
        url = url.substring(0, url.indexOf("?"));
    }
    if (url.indexOf("/") > 0) {
        url = url.substring(url.lastIndexOf("/") + 1);
    }
    return url;
}

function PadLeft(input, pad, length) {
    ///<summary>字符串左侧填充
    ///<para>原始字符串</para>
    ///<para>占位符</para>
    ///<para>总长度</para>
    ///</summary>
    if (pad == "") {
        return input;
    }
    while (input.length < length) {
        input = pad + input;
    }
    return input;
}

function PadRight(input, pad, length) {
    ///<summary>字符串右侧填充
    ///<para>原始字符串</para>
    ///<para>占位符</para>
    ///<para>总长度</para>
    ///</summary>
    if (pad == "") {
        return input;
    }
    while (input.length < length) {
        input += pad;
    }
    return input;
}

///** 
// * 从当前日期算起，获取N天前的日期（当前日不算在内），日期格式为yyyy-MM-dd 
// *  
// * @param daily 天数 
// * @return  
// */
function getDateByDay(daily) {
    var date = new Date();
    var year = date.getYear();
    var month = date.getMonth();
    var day = date.getDay() - daily;
    if (day < 1) {
        month -= 1;
        if (month == 0) {
            year -= 1;
            month = 12;
        }
        if (month == 4 || month == 6 || month == 9 || month == 11) {
            day = 30 + day;
        } else if (month == 1 || month == 3 || month == 5 || month == 7
                || month == 8 || month == 10 || month == 12) {
            day = 31 + day;
        } else if (month == 2) {
            if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0)) {
                day = 29 + day;
            }
            else {
                day = 28 + day;
            }
        }

    }
    var y = year + "";
    var m = "";
    var d = "";
    if (month < 10) {
        m = "0" + month;
    } else {
        m = month + "";
    }
    if (day < 10) {
        d = "0" + day;
    } else {
        d = day + "";
    }
    return y + "-" + m + "-" + d;
}

/** 
* 从当前日期算起，获取N个月前的日期，日期格式为yyyy-MM-dd 
*  
* @param mon 月份 
* @return 
*/
function getDateByMonth(mon) {
    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth() + 1 - mon;
    var day = date.getDate();
    if (month <= 0) {
        year -= 1;
        month += 12;
    } else if (day > 28) {
        if (month == 2) {
            if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0)) {
                day = 29;
            } else {
                day = 28;
            }
        } else if ((month == 4 || month == 6 || month == 9 || month == 11)
                && day == 31) {
            day = 30;
        }
    }
    return year + "-" + month + "-" + day;
}

function GetCurrentDate(includeLunarCalendar) {
    ///<summary>获取当前日期（包含星期信息）
    /// <para>是否包含农历信息</para>
    ///<summary>
    var CalendarData = new Array(20);
    var madd = new Array(12);
    var TheDate = new Date();
    var tgString = "甲乙丙丁戊己庚辛壬癸";
    var dzString = "子丑寅卯辰巳午未申酉戌亥";
    var numString = "一二三四五六七八九十";
    var monString = "正二三四五六七八九十冬腊";
    var weekString = "日一二三四五六";
    var sx = "鼠牛虎兔龙蛇马羊猴鸡狗猪";
    var cYear;
    var cMonth;
    var cDay;
    var cHour;
    var cDateString;
    var DateString;
    var Browser = navigator.appName;
    function init() {
        CalendarData[0] = 0x41A95;
        CalendarData[1] = 0xD4A;
        CalendarData[2] = 0xDA5;
        CalendarData[3] = 0x20B55;
        CalendarData[4] = 0x56A;
        CalendarData[5] = 0x7155B;
        CalendarData[6] = 0x25D;
        CalendarData[7] = 0x92D;
        CalendarData[8] = 0x5192B;
        CalendarData[9] = 0xA95;
        CalendarData[10] = 0xB4A;
        CalendarData[11] = 0x416AA;
        CalendarData[12] = 0xAD5;
        CalendarData[13] = 0x90AB5;
        CalendarData[14] = 0x4BA;
        CalendarData[15] = 0xA5B;
        CalendarData[16] = 0x60A57;
        CalendarData[17] = 0x52B;
        CalendarData[18] = 0xA93;
        CalendarData[19] = 0x40E95;
        madd[0] = 0;
        madd[1] = 31;
        madd[2] = 59;
        madd[3] = 90;
        madd[4] = 120;
        madd[5] = 151;
        madd[6] = 181;
        madd[7] = 212;
        madd[8] = 243;
        madd[9] = 273;
        madd[10] = 304;
        madd[11] = 334;
    }
    function GetBit(m, n) {
        return (m >> n) & 1;
    }
    function e2c() {
        var total, m, n, k;
        var isEnd = false;
        var tmp = TheDate.getYear();
        if (tmp < 1900) tmp += 1900;

        total = (tmp - 2001) * 365
    + Math.floor((tmp - 2001) / 4)
    + madd[TheDate.getMonth()]
    + TheDate.getDate()
    - 23;
        if (TheDate.getYear() % 4 == 0 && TheDate.getMonth() > 1)
            total++;
        for (m = 0; ; m++) {
            k = (CalendarData[m] < 0xfff) ? 11 : 12;
            for (n = k; n >= 0; n--) {
                if (total <= 29 + GetBit(CalendarData[m], n)) {
                    isEnd = true;
                    break;
                }
                total = total - 29 - GetBit(CalendarData[m], n);
            }
            if (isEnd) break;
        }
        cYear = 2001 + m;
        cMonth = k - n + 1;
        cDay = total;
        if (k == 12) {
            if (cMonth == Math.floor(CalendarData[m] / 0x10000) + 1)
                cMonth = 1 - cMonth;
            if (cMonth > Math.floor(CalendarData[m] / 0x10000) + 1)
                cMonth--;
        }
        cHour = Math.floor((TheDate.getHours() + 3) / 2);
    }
    function GetcDateString() {
        var tmp = "";
        tmp += tgString.charAt((cYear - 4) % 10); //年干
        tmp += dzString.charAt((cYear - 4) % 12); //年支
        tmp += "年（";
        tmp += sx.charAt((cYear - 4) % 12);
        tmp += "）";
        if (cMonth < 1) {
            tmp += "闰";
            tmp += monString.charAt(-cMonth - 1);
        }
        else
            tmp += monString.charAt(cMonth - 1);
        tmp += "月";
        tmp += (cDay < 11) ? "初" : ((cDay < 20) ? "十" : ((cDay < 30) ? "廿" : "卅"));
        if (cDay % 10 != 0 || cDay == 10)
            tmp += numString.charAt((cDay - 1) % 10);
        //            if (cHour == 13) tmp += "夜";
        //            tmp += dzString.charAt((cHour - 1) % 12);
        //            tmp += "时";
        cDateString = tmp;
        return tmp;
    }
    function GetDateString() {
        var tmp = "";
        var t1 = TheDate.getYear();
        if (t1 < 1900) t1 += 1900;
        tmp += t1
    + "年"
    + (TheDate.getMonth() + 1) + "月"
    + TheDate.getDate() + "日 "
        //    + TheDate.getHours() + ":"
        //    + ((TheDate.getMinutes() < 10) ? "0" : "")
        //    + TheDate.getMinutes()
    + " 星期" + weekString.charAt(TheDate.getDay());
        DateString = tmp;
        return tmp;
    }
    init();
    e2c();
    return includeLunarCalendar ? GetDateString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + GetcDateString() : GetDateString();
}


/*
* 字符串操作类，主要用于字符串的添加和删除。例如：在字符串 "a,b,c"中添加字符串"d",或者直接删除某个字符串。
* 主要提供将给定字符串添加到某个字符串，中间用逗号分隔
*/
var StringOperate = {
    separator: ",", //字符串分隔符
    baforeInsert: false, //字符串追加方式，默认false是在后面添加，true在追加到前面
    isRepeat: false, //追加的字符串是否可重复添加
    isDeleteAll: true, //删除所有匹配的字符串

    //左边添加分隔符
    lInsertSeparator: function (operateString) {
        if (operateString.indexOf(this.separator) == 0)
            return operateString;
        return this.separator + operateString;
    },
    //右边添加分隔符
    rInsertSeparator: function (operateString) {
        if (operateString.lastIndexOf(this.separator) == (operateString.length - this.separator.length))
            return operateString;
        return operateString + this.separator;
    },
    //去除左边分隔符
    lSeparatorTrim: function (operateString) {
        if (operateString.indexOf(this.separator) == 0)
            return operateString.substring(1);
        return operateString;
    },
    //去除右边的分隔符
    rSeparatorTrim: function (operateString) {
        if (operateString.lastIndexOf(this.separator) == (operateString.length - this.separator.length))
            return operateString.substring(0, operateString.length - 1);
        return operateString;
    },
    //追加字符串，将str字符串 追加到operateString中
    add: function (operateString, str) {
        if (str && str != "") {
            if (this.isRepeat) {//重复追加
                if (this.baforeInsert) {//追加在开头
                    return this.rSeparatorTrim(this.lSeparatorTrim(str + this.separator + operateString));
                }
                return this.rSeparatorTrim(this.lSeparatorTrim(operateString + this.separator + str));
            } else {
                //开头和结尾都添加分隔符
                operateString = this.lInsertSeparator(this.rInsertSeparator(operateString));
                if (operateString.indexOf(this.separator + str + this.separator) == -1) {
                    if (this.baforeInsert) {
                        return this.rSeparatorTrim(this.lSeparatorTrim(str + operateString));
                    } else {
                        return this.rSeparatorTrim(this.lSeparatorTrim(operateString + str));
                    }
                }
                return this.rSeparatorTrim(this.lSeparatorTrim(operateString));
            }
        }
    },
    //删除指定字符串
    remove: function (operateString, str) {
        if (operateString && str && operateString != "" && str != "") {
            //开头和结尾都添加分隔符
            operateString = this.lInsertSeparator(this.rInsertSeparator(operateString));
            if (this.isDeleteAll) {
                operateString = operateString.replace(new RegExp(this.separator, "g"), this.separator + this.separator);
                //删除所有匹配的字符串
                operateString = operateString.replace(new RegExp(this.separator + str + this.separator, "g"), this.separator);
                operateString = operateString.replace(new RegExp(this.separator + "{2,}", "g"), this.separator);
            } else {
                operateString = operateString.replace(new RegExp(this.separator + str + this.separator), this.separator);
            }
            return this.rSeparatorTrim(this.lSeparatorTrim(operateString));
        }
    }


};