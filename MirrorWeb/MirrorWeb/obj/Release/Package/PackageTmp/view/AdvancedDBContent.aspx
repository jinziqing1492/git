<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvancedDBContent.aspx.cs" Inherits="DRMS.MirrorWeb.view.AdvancedDBContent" %>

<%@ Register Src="../UserControl/AdvancedDBControl.ascx" TagName="AdvancedDBControl" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/index_commer.css" rel="stylesheet" />
    <link href="../css/index.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../js/Util.js"></script>

    <script type="text/javascript">
        var TYPE = (QueryString("type") == "") ? "1" : QueryString("type");
        var ZtreeSql = QueryString("queryConn");
        var contentUrlFormat = "DatabaseContent.aspx?type={0}&queryConn={1}";
        $(function () {
            //初始化加载
            $("#trClassfication").hide();
            $("#ifr").attr("src", String.Format(contentUrlFormat, TYPE, ZtreeSql));
            //alert(ZtreeSql);
            $("#btnSearch").click(function () {
                var sqlConn = getSqlConn();
                var sqlTime = getSqlTime();
                var sql = "";
                if (sqlConn == "" && sqlTime == "") {
                    sql = "";
                }
                if (sqlConn != "" && sqlTime != "") {
                    sql = sqlConn + " AND " + sqlTime;
                }
                if (sqlConn != "" && sqlTime == "") {
                    sql = sqlConn;
                }
                if (sqlConn == "" && sqlTime != "") {
                    sql = sqlTime;
                }
                sql = encodeURIComponent(sql);
                var url = String.Format(contentUrlFormat, TYPE, sql);
                $("#ifr").attr("src", url);

            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <span style="color: #6a6a6a; font-weight: normal;">
            <uc1:AdvancedDBControl ID="AdvancedDBControl1" runat="server" />
            <div class="adv-search-ok">
                <input type="button" id="btnSearch" value="提交查询" style="cursor: pointer" /><input type="button" id="btnCancel" value="返&nbsp;&nbsp;回" style="cursor: pointer" onclick="window.history.back();" />
            </div>
        </span>
        <div class="TYperiodical_List">
            <div class="TYContainerRText_title MT clearfix">
                <div class="TYCRTextTitle_arTab fl">
                    <div class="TYCRTextTitle_left fl">
                    </div>
                    <div class="TYCRTextTitle_center fl">
                        资源列表
                    </div>
                    <div class="TYCRTextTitle_right fl">
                    </div>
                </div>
            </div>
            <iframe id="ifr" allowtransparency="true" marginwidth="0" width="100%" height="1150px" framespacing="0" marginheight="0" frameborder="0" scrolling="no"></iframe>
        </div>
    </form>
</body>
</html>
