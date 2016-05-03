
//加载作者组信息
function loadAuthorGroup(data) {
    if (data) {
        var dataObj = data; // $.parseJSON(data);
        if (dataObj == null) return;
        var authorgroup = "";
        for (var i = 0; i < dataObj.length; i++) {

            var myDate = new Date();
            var time = myDate.getMilliseconds();
            
            if (i == 0) {
                $("#LIABILITYFORM_1").val(decodeURIComponent(dataObj[i].zzfs));
                $("#AUTHORTYPE_1").val(decodeURIComponent(dataObj[i].auttype));
                $("#AUTHOR_1").val(decodeURIComponent(dataObj[i].name));
                $("#AUTHORDEPT_1").val(decodeURIComponent(dataObj[i].deptname));
                $("#EMAIL_1").val(decodeURIComponent(dataObj[i].email));
                $("#ADDRESS_1").val(decodeURIComponent(dataObj[i].address));
                    $("#AUTHORDESC_1").val(decodeURIComponent(dataObj[i].blurbEdit));
                continue;
            }
            authorgroup += "<tr>"
                        + "<td class=\"TYBookText_title\">著作方式：</td>"
                        + "<td><input type=\"text\" id=\"LIABILITYFORM_" + time + "\" name=\"LIABILITYFORM\" value=\"" + decodeURIComponent(dataObj[i].zzfs) + "\"/></td>"
                        + "<td class=\"TYBookText_title\">作者类型：</td>"
                        + "<td>"
                            + "<select id=\"AUTHORTYPE_" + time + "\" class=\"infoSelect\" name=\"AUTHORTYPE\">"
                                + "<option value=\"0\">个人</option><option value=\"1\" " + (decodeURIComponent(dataObj[i].auttype) == "1" ? "selected=\"selected\"" : "") + ">机构</option>"
                            + "</select>"
                        + "</td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\">作者名称：</td>"
                        + "<td><input type=\"text\" id=\"AUTHOR_" + time + "\" name=\"AUTHOR\" value=\"" + decodeURIComponent(dataObj[i].name) + "\" /></td>"
                        + "<td class=\"TYBookText_title\">隶属机构：</td>"
                        + "<td><input type=\"text\" id=\"AUTHORDEPT_" + time + "\" name=\"AUTHORDEPT\" value=\"" + decodeURIComponent(dataObj[i].deptname) + "\" /></td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\">作者Email：</td>"
                        + "<td><input type=\"text\" id=\"EMAIL_" + time + "\" name=\"EMAIL\" value=\"" + decodeURIComponent(dataObj[i].email) + "\" /></td>"
                        + "<td class=\"TYBookText_title\">作者住址：</td>"
                        + "<td><input type=\"text\" id=\"ADDRESS_" + time + "\" name=\"ADDRESS\" value=\"" + decodeURIComponent(dataObj[i].address) + "\" /></td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\">作者简介：</td>"
                        + "<td colspan=\"3\">"
                            + "<textarea rows=\"10\" id=\"AUTHORDESC_" + time + "\" class=\"TYImgTa\" name=\"AUTHORDESC\">" + decodeURIComponent(dataObj[i].blurbEdit) + "</textarea><img"
                                + " class=\"TYAddElem\" title=\"添加作者\" src=\"../images/Add.png\" onclick=\"addAuthor(this)\" /><img"
                                    + " class=\"TYDeleteElem\" title=\"删除作者\" src=\"../images/Delete.png\" onclick=\"removeAuthor(this)\" />"
                        + "</td>"
                    + "</tr>";
        }
        $("#AUTHORDESC_1").parent().parent().after($(authorgroup));
    }
}

//加载关键词信息
function loadKeywords(data) {
    var myDate = new Date();
    if (data) {
        var dataObj = data; // $.parseJSON(data);
        if (dataObj == null) return;
        var keywords = "";
        if (dataObj.zhHans) {
            $("#KEYWORDS_1").val(decodeURIComponent(dataObj.zhHans));
        }
        if (dataObj.zhHant) {
            var time = myDate.getMilliseconds();
            keywords += "<tr>"
                + "<td class=\"TYBookText_title\">关键词：</td>"
                + "<td>"
                    + "<select class=\"infoSelect\" id=\"KEYWORDSLanguage_" + time + "\" name=\"KEYWORDSLanguage\">"
                        + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\" selected=\"selected\">中文(繁体)</option>"
                        + "<option value=\"en\">英语</option><option value=\"ja\">日语</option>"
                        + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                        + "<option value=\"fr\">法语</option><option value=\"de\">德语</option>"
                    + "</select>"
                + "</td>"
                + "<td colspan=\"2\">"
                    + "<input type=\"text\" id=\"KEYWORDS_" + time + "\" name=\"KEYWORDS\" class=\"TYkeywordInput\" value=\"" + decodeURIComponent(dataObj.zhHant) + "\" /><img class=\"TYAddElem\""
                        + " title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addKeyword(this)\" /><img class=\"TYDeleteElem\""
                            + " title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeKeyword(this)\" />"
                + "</td>"
            + "</tr>";
        }
        if (dataObj.en) {
            var time = myDate.getMilliseconds();
            keywords += "<tr>"
                + "<td class=\"TYBookText_title\">关键词：</td>"
                + "<td>"
                    + "<select class=\"infoSelect\" id=\"KEYWORDSLanguage_" + time + "\" name=\"KEYWORDSLanguage\">"
                        + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                        + "<option value=\"en\" selected=\"selected\">英语</option><option value=\"ja\">日语</option>"
                        + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                        + "<option value=\"fr\">法语</option><option value=\"de\">德语</option>"
                    + "</select>"
                + "</td>"
                + "<td colspan=\"2\">"
                    + "<input type=\"text\" id=\"KEYWORDS_" + time + "\" name=\"KEYWORDS\" class=\"TYkeywordInput\" value=\"" + decodeURIComponent(dataObj.en) + "\" /><img class=\"TYAddElem\""
                        + " title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addKeyword(this)\" /><img class=\"TYDeleteElem\""
                            + " title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeKeyword(this)\" />"
                + "</td>"
            + "</tr>";
        }
        if (dataObj.ja) {
            var time = myDate.getMilliseconds();
            keywords += "<tr>"
                + "<td class=\"TYBookText_title\">关键词：</td>"
                + "<td>"
                    + "<select class=\"infoSelect\" id=\"KEYWORDSLanguage_" + time + "\" name=\"KEYWORDSLanguage\">"
                        + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                        + "<option value=\"en\">英语</option><option value=\"ja\" selected=\"selected\">日语</option>"
                        + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                        + "<option value=\"fr\">法语</option><option value=\"de\">德语</option>"
                    + "</select>"
                + "</td>"
                + "<td colspan=\"2\">"
                    + "<input type=\"text\" id=\"KEYWORDS_" + time + "\" name=\"KEYWORDS\" class=\"TYkeywordInput\" value=\"" + decodeURIComponent(dataObj.ja) + "\" /><img class=\"TYAddElem\""
                        + " title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addKeyword(this)\" /><img class=\"TYDeleteElem\""
                            + " title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeKeyword(this)\" />"
                + "</td>"
            + "</tr>";
        }
        if (dataObj.koKR) {
            var time = myDate.getMilliseconds();
            keywords += "<tr>"
                + "<td class=\"TYBookText_title\">关键词：</td>"
                + "<td>"
                    + "<select class=\"infoSelect\" id=\"KEYWORDSLanguage_" + time + "\" name=\"KEYWORDSLanguage\">"
                        + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                        + "<option value=\"en\">英语</option><option value=\"ja\">日语</option>"
                        + "<option value=\"ko-KR\" selected=\"selected\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                        + "<option value=\"fr\">法语</option><option value=\"de\">德语</option>"
                    + "</select>"
                + "</td>"
                + "<td colspan=\"2\">"
                    + "<input type=\"text\" id=\"KEYWORDS_" + time + "\" name=\"KEYWORDS\" class=\"TYkeywordInput\" value=\"" + decodeURIComponent(dataObj.koKR) + "\" /><img class=\"TYAddElem\""
                        + " title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addKeyword(this)\" /><img class=\"TYDeleteElem\""
                            + " title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeKeyword(this)\" />"
                + "</td>"
            + "</tr>";
        }
        if (dataObj.ru) {
            var time = myDate.getMilliseconds();
            keywords += "<tr>"
                + "<td class=\"TYBookText_title\">关键词：</td>"
                + "<td>"
                    + "<select class=\"infoSelect\" id=\"KEYWORDSLanguage_" + time + "\" name=\"KEYWORDSLanguage\">"
                        + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                        + "<option value=\"en\">英语</option><option value=\"ja\">日语</option>"
                        + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\" selected=\"selected\">俄语</option>"
                        + "<option value=\"fr\">法语</option><option value=\"de\">德语</option>"
                    + "</select>"
                + "</td>"
                + "<td colspan=\"2\">"
                    + "<input type=\"text\" id=\"KEYWORDS_" + time + "\" name=\"KEYWORDS\" class=\"TYkeywordInput\" value=\"" + decodeURIComponent(dataObj.ru) + "\" /><img class=\"TYAddElem\""
                        + " title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addKeyword(this)\" /><img class=\"TYDeleteElem\""
                            + " title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeKeyword(this)\" />"
                + "</td>"
            + "</tr>";
        }
        if (dataObj.fr) {
            var time = myDate.getMilliseconds();
            keywords += "<tr>"
                + "<td class=\"TYBookText_title\">关键词：</td>"
                + "<td>"
                    + "<select class=\"infoSelect\" id=\"KEYWORDSLanguage_" + time + "\" name=\"KEYWORDSLanguage\">"
                        + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                        + "<option value=\"en\">英语</option><option value=\"ja\">日语</option>"
                        + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                        + "<option value=\"fr\" selected=\"selected\">法语</option><option value=\"de\">德语</option>"
                    + "</select>"
                + "</td>"
                + "<td colspan=\"2\">"
                    + "<input type=\"text\" id=\"KEYWORDS_" + time + "\" name=\"KEYWORDS\" class=\"TYkeywordInput\" value=\"" + decodeURIComponent(dataObj.fr) + "\" /><img class=\"TYAddElem\""
                        + " title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addKeyword(this)\" /><img class=\"TYDeleteElem\""
                            + " title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeKeyword(this)\" />"
                + "</td>"
            + "</tr>";
        }
        if (dataObj.de) {
            var time = myDate.getMilliseconds();
            keywords += "<tr>"
                + "<td class=\"TYBookText_title\">关键词：</td>"
                + "<td>"
                    + "<select class=\"infoSelect\" id=\"KEYWORDSLanguage_" + time + "\" name=\"KEYWORDSLanguage\">"
                        + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                        + "<option value=\"en\">英语</option><option value=\"ja\">日语</option>"
                        + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                        + "<option value=\"fr\">法语</option><option value=\"de\" selected=\"selected\">德语</option>"
                    + "</select>"
                + "</td>"
                + "<td colspan=\"2\">"
                    + "<input type=\"text\" id=\"KEYWORDS_" + time + "\" name=\"KEYWORDS\" class=\"TYkeywordInput\" value=\"" + decodeURIComponent(dataObj.de) + "\" /><img class=\"TYAddElem\""
                        + " title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addKeyword(this)\" /><img class=\"TYDeleteElem\""
                            + " title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeKeyword(this)\" />"
                + "</td>"
            + "</tr>";
        }
        $("#KEYWORDS_1").parent().parent().after($(keywords));
    }
}

//加载内容简介信息
function loadDegists(data) {
    var myDate = new Date();
    if (data) {
        var dataObj = data; // $.parseJSON(data);
        if (dataObj == null) return;
        var degists = "";
        if (dataObj.zhHans) {
            $("#DIGEST_1").val(decodeURIComponent(dataObj.zhHans));
        }
        if (dataObj.zhHant) {
            var time = myDate.getMilliseconds();
            degists += "<tr>"
                        + "<td class=\"TYBookText_title\">内容提要：</td>"
                        + "<td colspan=\"3\">"
                            + "<select class=\"infoSelect\" id=\"DIGESTLanguage_" + time + "\" name=\"DIGESTLanguage\">"
                                + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\" selected=\"selected\">中文(繁体)</option>"
                                + "<option value=\"en\">英语</option><option value=\"ja\">日语</option>"
                                + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                                + "<option value=\"fr\">法语</option><option value=\"de\">德语</option>"
                            + "</select>"
                            + "<img class=\"TYAddElem\" title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addDegist(this)\" />"
                            + "<img class=\"TYDeleteElem\" title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeDegist(this)\" />"
                        + "</td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\"></td>"
                        + "<td colspan=\"3\"><textarea rows=\"10\" id=\"DIGEST_" + time + "\" name=\"DIGEST\">" + decodeURIComponent(dataObj.zhHant) + "</textarea></td>"
                    + "</tr>";
        }
        if (dataObj.en) {
            var time = myDate.getMilliseconds();
            degists += "<tr>"
                        + "<td class=\"TYBookText_title\">内容提要：</td>"
                        + "<td colspan=\"3\">"
                            + "<select class=\"infoSelect\" id=\"DIGESTLanguage_" + time + "\" name=\"DIGESTLanguage\">"
                                + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                                + "<option value=\"en\" selected=\"selected\">英语</option><option value=\"ja\">日语</option>"
                                + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                                + "<option value=\"fr\">法语</option><option value=\"de\">德语</option>"
                            + "</select>"
                            + "<img class=\"TYAddElem\" title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addDegist(this)\" />"
                            + "<img class=\"TYDeleteElem\" title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeDegist(this)\" />"
                        + "</td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\"></td>"
                        + "<td colspan=\"3\"><textarea rows=\"10\" id=\"DIGEST_" + time + "\" name=\"DIGEST\">" + decodeURIComponent(dataObj.en) + "</textarea></td>"
                    + "</tr>";
        }
        if (dataObj.ja) {
            var time = myDate.getMilliseconds();
            degists += "<tr>"
                        + "<td class=\"TYBookText_title\">内容提要：</td>"
                        + "<td colspan=\"3\">"
                            + "<select class=\"infoSelect\" id=\"DIGESTLanguage_" + time + "\" name=\"DIGESTLanguage\">"
                                + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                                + "<option value=\"en\">英语</option><option value=\"ja\" selected=\"selected\">日语</option>"
                                + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                                + "<option value=\"fr\">法语</option><option value=\"de\">德语</option>"
                            + "</select>"
                            + "<img class=\"TYAddElem\" title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addDegist(this)\" />"
                            + "<img class=\"TYDeleteElem\" title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeDegist(this)\" />"
                        + "</td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\"></td>"
                        + "<td colspan=\"3\"><textarea rows=\"10\" id=\"DIGEST_" + time + "\" name=\"DIGEST\">" + decodeURIComponent(dataObj.ja) + "</textarea></td>"
                    + "</tr>";
        }
        if (dataObj.koKR) {
            var time = myDate.getMilliseconds();
            degists += "<tr>"
                        + "<td class=\"TYBookText_title\">内容提要：</td>"
                        + "<td colspan=\"3\">"
                            + "<select class=\"infoSelect\" id=\"DIGESTLanguage_" + time + "\" name=\"DIGESTLanguage\">"
                                + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                                + "<option value=\"en\">英语</option><option value=\"ja\">日语</option>"
                                + "<option value=\"ko-KR\" selected=\"selected\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                                + "<option value=\"fr\">法语</option><option value=\"de\">德语</option>"
                            + "</select>"
                            + "<img class=\"TYAddElem\" title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addDegist(this)\" />"
                            + "<img class=\"TYDeleteElem\" title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeDegist(this)\" />"
                        + "</td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\"></td>"
                        + "<td colspan=\"3\"><textarea rows=\"10\" id=\"DIGEST_" + time + "\" name=\"DIGEST\">" + decodeURIComponent(dataObj.koKR) + "</textarea></td>"
                    + "</tr>";
        }
        if (dataObj.ru) {
            var time = myDate.getMilliseconds();
            degists += "<tr>"
                        + "<td class=\"TYBookText_title\">内容提要：</td>"
                        + "<td colspan=\"3\">"
                            + "<select class=\"infoSelect\" id=\"DIGESTLanguage_" + time + "\" name=\"DIGESTLanguage\">"
                                + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                                + "<option value=\"en\">英语</option><option value=\"ja\">日语</option>"
                                + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\" selected=\"selected\">俄语</option>"
                                + "<option value=\"fr\">法语</option><option value=\"de\">德语</option>"
                            + "</select>"
                            + "<img class=\"TYAddElem\" title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addDegist(this)\" />"
                            + "<img class=\"TYDeleteElem\" title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeDegist(this)\" />"
                        + "</td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\"></td>"
                        + "<td colspan=\"3\"><textarea rows=\"10\" id=\"DIGEST_" + time + "\" name=\"DIGEST\">" + decodeURIComponent(dataObj.ru) + "</textarea></td>"
                    + "</tr>";
        }
        if (dataObj.fr) {
            var time = myDate.getMilliseconds();
            degists += "<tr>"
                        + "<td class=\"TYBookText_title\">内容提要：</td>"
                        + "<td colspan=\"3\">"
                            + "<select class=\"infoSelect\" id=\"DIGESTLanguage_" + time + "\" name=\"DIGESTLanguage\">"
                                + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                                + "<option value=\"en\">英语</option><option value=\"ja\">日语</option>"
                                + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                                + "<option value=\"fr\" selected=\"selected\">法语</option><option value=\"de\">德语</option>"
                            + "</select>"
                            + "<img class=\"TYAddElem\" title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addDegist(this)\" />"
                            + "<img class=\"TYDeleteElem\" title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeDegist(this)\" />"
                        + "</td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\"></td>"
                        + "<td colspan=\"3\"><textarea rows=\"10\" id=\"DIGEST_" + time + "\" name=\"DIGEST\">" + decodeURIComponent(dataObj.fr) + "</textarea></td>"
                    + "</tr>";
        }
        if (dataObj.de) {
            var time = myDate.getMilliseconds();
            degists += "<tr>"
                        + "<td class=\"TYBookText_title\">内容提要：</td>"
                        + "<td colspan=\"3\">"
                            + "<select class=\"infoSelect\" id=\"DIGESTLanguage_" + time + "\" name=\"DIGESTLanguage\">"
                                + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                                + "<option value=\"en\">英语</option><option value=\"ja\">日语</option>"
                                + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                                + "<option value=\"fr\">法语</option><option value=\"de\" selected=\"selected\">德语</option>"
                            + "</select>"
                            + "<img class=\"TYAddElem\" title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addDegist(this)\" />"
                            + "<img class=\"TYDeleteElem\" title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeDegist(this)\" />"
                        + "</td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\"></td>"
                        + "<td colspan=\"3\"><textarea rows=\"10\" id=\"DIGEST_" + time + "\" name=\"DIGEST\">" + decodeURIComponent(dataObj.de) + "</textarea></td>"
                    + "</tr>";
        }
        $("#DIGEST_1").parent().parent().after($(degists));
    }
}

//获取作者组信息xml
function getAuthorGroup(authorhdn, authordeschdn) {
    //拼写作者组xml
    var LIABILITYFORMs = $("input[name='LIABILITYFORM']");
    var AUTHORTYPEs = $("select[name='AUTHORTYPE']");
    var AUTHORs = $("input[name='AUTHOR']");
    var AUTHORDEPTs = $("input[name='AUTHORDEPT']");
    var EMAILs = $("input[name='EMAIL']");
    var ADDRESSs = $("input[name='ADDRESS']");
    var AUTHORDESCs = $("textarea[name='AUTHORDESC']");
    var authorGroups = "<authorgroup>";
    for (var i = 0; i < LIABILITYFORMs.length; i++) {
        //处理存入数据库的作者信息
        $(authorhdn).val($(authorhdn).val() + ";" + $(AUTHORs[i]).val());
        $(authordeschdn).val($(authorhdn).val() + ";{" + $(AUTHORDESCs[i]).val() + "}");

        //处理简介中的换行符
        var AUTHORDESC = "<para><![CDATA[" + $(AUTHORDESCs[i]).val() + "]]></para>";
        AUTHORDESC = AUTHORDESC.replaceAll("\n", "]]></para><para><![CDATA[").replaceAll("<para><![CDATA[]]></para>", "");
        if (!$(AUTHORTYPEs[i]).val() || $(AUTHORTYPEs[i]).val() == "0") {
            authorGroups += "<author role=\"" + $(LIABILITYFORMs[i]).val() + "\">"
                + "<personname><![CDATA[" + $(AUTHORs[i]).val() + "]]></personname>"
                + "<affiliation><![CDATA[" + $(AUTHORDEPTs[i]).val() + "]]></affiliation>"
                + "<email><![CDATA[" + $(EMAILs[i]).val() + "]]></email>"
                + "<address><![CDATA[" + $(ADDRESSs[i]).val() + "]]></address>"
                + "<personblurb>" + AUTHORDESC + "</personblurb>"
                + "</author>";
        }
        else if ($(AUTHORTYPEs[i]).val() == "1") {
            authorGroups += "<author role=\"" + $(LIABILITYFORMs[i]).val() + "\">"
                + "<orgname><![CDATA[" + $(AUTHORs[i]).val() + "]]></orgname>"
                + "<affiliation><![CDATA[" + $(AUTHORDEPTs[i]).val() + "]]></affiliation>"
                + "<email><![CDATA[" + $(EMAILs[i]).val() + "]]></email>"
                + "<address><![CDATA[" + $(ADDRESSs[i]).val() + "]]></address>"
                + "<orgblurb>" + AUTHORDESC + "</orgblurb>"
                + "</author>";
        }
    }
    authorGroups += "</authorgroup>";
    //返回作者组信息
    return authorGroups;
}
//动态添加删除作者
function addAuthor(obj) {
    var myDate = new Date();
    var time = myDate.getMilliseconds();
    var newAuthor = "<tr>"
                        + "<td class=\"TYBookText_title\">著作方式：</td>"
                        + "<td><input type=\"text\" id=\"LIABILITYFORM_" + time + "\" name=\"LIABILITYFORM\" /></td>"
                        + "<td class=\"TYBookText_title\">作者类型：</td>"
                        + "<td>"
                            + "<select id=\"AUTHORTYPE_" + time + "\" class=\"infoSelect\" name=\"AUTHORTYPE\">"
                                + "<option value=\"0\">个人</option><option value=\"1\">机构</option>"
                            + "</select>"
                        + "</td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\">作者名称：</td>"
                        + "<td><input type=\"text\" id=\"AUTHOR_" + time + "\" name=\"AUTHOR\" /></td>"
                        + "<td class=\"TYBookText_title\">隶属机构：</td>"
                        + "<td><input type=\"text\" id=\"AUTHORDEPT_" + time + "\" name=\"AUTHORDEPT\" /></td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\">作者Email：</td>"
                        + "<td><input type=\"text\" id=\"EMAIL_" + time + "\" name=\"EMAIL\" /></td>"
                        + "<td class=\"TYBookText_title\">作者住址：</td>"
                        + "<td><input type=\"text\" id=\"ADDRESS_" + time + "\" name=\"ADDRESS\" /></td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\">作者简介：</td>"
                        + "<td colspan=\"3\">"
                            + "<textarea rows=\"10\" id=\"AUTHORDESC_" + time + "\" class=\"TYImgTa\" name=\"AUTHORDESC\"></textarea><img"
                                + " class=\"TYAddElem\" title=\"添加作者\" src=\"../images/Add.png\" onclick=\"addAuthor(this)\" /><img"
                                    + " class=\"TYDeleteElem\" title=\"删除作者\" src=\"../images/Delete.png\" onclick=\"removeAuthor(this)\" />"
                        + "</td>"
                    + "</tr>";
    $(obj).parent().parent().after($(newAuthor));
}
//移除作者
function removeAuthor(obj) {
    var result = confirm("确定要删除此作者？");
    if (!result) return;
    $(obj).parent().parent().prev().prev().prev().remove();
    $(obj).parent().parent().prev().prev().remove();
    $(obj).parent().parent().prev().remove();
    $(obj).parent().parent().remove();
}

//获取关键词信息xml
function getKeywords(keywordhdn) {
    //拼写关键词xml
    var keywords = "";
    var KEYWORDSLanguages = $("select[name='KEYWORDSLanguage']");
    var KEYWORDSs = $("input[name='KEYWORDS']");
    //合并相同语种的关键词
    var kls = [];
    var ks = [];
    for (var i = 0; i < KEYWORDSLanguages.length; i++) {
        if ($.inArray($(KEYWORDSLanguages[i]).val(), kls) == -1) {
            kls.push($(KEYWORDSLanguages[i]).val());
            ks[i] = $(KEYWORDSs[i]).val();
        }
        else {
            var index = $.inArray($(KEYWORDSLanguages[i]).val(), kls);
            ks[index] = ks[index] + ";" + $(KEYWORDSs[i]).val();
        }
    }

    for (var i = 0; i < kls.length; i++) {
        //处理入库的关键词
        $(keywordhdn).val($(keywordhdn).val() + ";" + ks[i]);
        //使用英文分号拆分关键词
        var tempKw = ks[i];
        var tempKwArr = tempKw.split(";");
        keywords += "<keywordset lang=\"" + kls[i] + "\">";
        for (var j = 0; j < tempKwArr.length; j++) {
            if (tempKwArr[j])
                keywords += "<keyword><![CDATA[" + tempKwArr[j] + "]]></keyword>";
        }
        keywords += "</keywordset>";
    }
    //返回关键词信息
    return keywords;
}
//动态添加删除关键词
function addKeyword(obj) {
    var myDate = new Date();
    var time = myDate.getMilliseconds();
    var newAuthor = "<tr>"
                        + "<td class=\"TYBookText_title\">关键词：</td>"
                        + "<td>"
                            + "<select class=\"infoSelect\" id=\"KEYWORDSLanguage_" + time + "\" name=\"KEYWORDSLanguage\">"
                                + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                                + "<option value=\"en\">英语</option><option value=\"ja\">日语</option>"
                                + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                                + "<option value=\"fr\">法语</option><option value=\"de\">德语</option>"
                            + "</select>"
                        + "</td>"
                        + "<td colspan=\"2\">"
                            + "<input type=\"text\" id=\"KEYWORDS_" + time + "\" name=\"KEYWORDS\" class=\"TYkeywordInput\" /><img class=\"TYAddElem\""
                               + " title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addKeyword(this)\" /><img class=\"TYDeleteElem\""
                                   + " title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeKeyword(this)\" />"
                        + "</td>"
                    + "</tr>";
    $(obj).parent().parent().after($(newAuthor));
}
//移除关键词
function removeKeyword(obj) {
    var result = confirm("确定要删除此关键词？");
    if (!result) return;
    $(obj).parent().parent().remove();
}

//获取内容简介信息xml
function getDegist(degistshdn) {
    //拼写内容简介xml
    var degists = "";
    var DIGESTLanguages = $("select[name='DIGESTLanguage']");
    var DIGESTs = $("textarea[name='DIGEST']");

    //合并相同语种的内容简介
    var kls = [];
    var ks = [];
    for (var i = 0; i < DIGESTLanguages.length; i++) {
        if ($.inArray($(DIGESTLanguages[i]).val(), kls) == -1) {
            kls.push($(DIGESTLanguages[i]).val());
            ks[i] = $(DIGESTs[i]).val();
        }
        else {
            var index = $.inArray($(DIGESTLanguages[i]).val(), kls);
            ks[index] = ks[index] + ";" + $(DIGESTs[i]).val();
        }
    }

    for (var i = 0; i < kls.length; i++) {
        //处理入库的内容简介
        $(degistshdn).val($(degistshdn).val() + ";" + ks[i]);
        //处理简介中的换行符
        var DIGEST = "<para><![CDATA[" + ks[i] + "]]></para>";
        DIGEST = DIGEST.replaceAll("\n", "]]></para><para><![CDATA[").replaceAll("<para><![CDATA[]]></para>", "");
        degists += "<abstract lang=\"" + kls[i] + "\">" + DIGEST + "</abstract>";
    }
    //返回内容简介信息
    return degists;
}
//动态添加删除内容简介
function addDegist(obj) {
    var myDate = new Date();
    var time = myDate.getMilliseconds();
    var newAuthor = "<tr>"
                        + "<td class=\"TYBookText_title\">内容提要：</td>"
                        + "<td colspan=\"3\">"
                            + "<select class=\"infoSelect\" id=\"DIGESTLanguage_" + time + "\" name=\"DIGESTLanguage\">"
                                + "<option value=\"zh-Hans\">中文(简体)</option><option value=\"zh-Hant\">中文(繁体)</option>"
                                + "<option value=\"en\">英语</option><option value=\"ja\">日语</option>"
                                + "<option value=\"ko-KR\">朝鲜语(韩国)</option><option value=\"ru\">俄语</option>"
                                + "<option value=\"fr\">法语</option><option value=\"de\">德语</option>"
                            + "</select>"
                            + "<img class=\"TYAddElem\" title=\"添加关键词\" src=\"../images/Add.png\" onclick=\"addDegist(this)\" />"
                            + "<img class=\"TYDeleteElem\" title=\"删除关键词\" src=\"../images/Delete.png\" onclick=\"removeDegist(this)\" />"
                        + "</td>"
                    + "</tr>"
                    + "<tr>"
                        + "<td class=\"TYBookText_title\"></td>"
                        + "<td colspan=\"3\"><textarea rows=\"10\" id=\"DIGEST_" + time + "\" name=\"DIGEST\"></textarea></td>"
                    + "</tr>";
    $(obj).parent().parent().next().after($(newAuthor));
}
//移除内容简介
function removeDegist(obj) {
    var result = confirm("确定要删除此内容简介？");
    if (!result) return;
    $(obj).parent().parent().next().remove();
    $(obj).parent().parent().remove();
}