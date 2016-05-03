<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="LogList.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.LogList" %>
<%@ Register Src="../AdminUserControl/LogListView.ascx" TagName="LogListView" TagPrefix="DRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript" src="../js/jquery-ui-1.8.18.custom.min.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function GetCustomisedQueryCondition() {
            ///<summary>生成自定义查询条件
            ///</summary>
            var queryCondition = [];
            var logName = $("#tbxLogName").val();
            var resTypeIndex = $("#<%=selResType.ClientID %>").val();
            var logTypeIndex = $("#<%=selLogType.ClientID %>").val();
            var sdate = $.trim($("#stdsdate").val());
            var edate = $.trim($("#stdedate").val());
            var userName = $.trim($("#tbxUserName").val());
            if (logName) {
                queryCondition.push(String.Format("NAME = '?{0}'", logName));
            }
            if (userName !== "") {
                queryCondition.push(String.Format("USERNAME = '?{0}'", userName));
            }
            if (resTypeIndex != -1) {
                queryCondition.push(String.Format("RESTYPE = {0}", resTypeIndex));
            }
            if (logTypeIndex != -1) {
                queryCondition.push(String.Format("LOGTYPE = {0}", logTypeIndex));
            }
            if (sdate !== "" && edate !== "") {
                if (sdate <= edate) {
                    queryCondition.push(String.Format("ADDDATE>='{0}'", sdate));
                    queryCondition.push(String.Format("ADDDATE<='{0}'", edate));
                }
            }
            if (queryCondition.length !== 1) {
                queryCondition = queryCondition.join(" and ");
            }
            $("#<%=hidQueryCondition.ClientID %>").val(queryCondition);
            return queryCondition;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="shortcut-buttons-set">
        <%-- <li>
        <a class="shortcut-button" href="UserItem.aspx">
            <span> <img src="../images/icons/accessories-text-editor.png" alt="icon" /><br />添加用户</span>
        </a>
        </li>--%>
        <li id="record-filter" class="action"><a class="shortcut-button" href="#"><span>
            <img src="../images/icons/edit-find-project.png" alt="icon" /><br />
            检索日志</span> </a></li>
    </ul>
    <!-- End .shortcut-buttons-set -->
    <div class="clear">
    </div>
    <div class="content-box">
        <div class="content-box-header">
            <h3>
                管理日志</h3>
            <ul class="content-box-tabs">
                <li><a class="default-tab" href="#tab1">日志列表</a></li>
            </ul>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">
            <DRMS:LogListView ID="logList" runat="server" />
        </div>
        <!-- End .content-box-content -->
    </div>
    <!-- End .content-box -->
    <!-- ui-dialog -->
    <div id="dialog-condition-box" class="hide" title="自定义查询">
        <div class="dialogContainer form-container">
            <dl>
                <dt>
                    <label>
                        日志名称：</label></dt>
                <dd>
                    <input id="tbxLogName" class="text-input long-input" type="text" />
                </dd>
                <dt>
                    <label>
                        用户名：</label></dt>
                <dd id="textInputDD">
                    <input id="tbxUserName" class="text-input long-input" type="text" />
                </dd>
                <dt>
                    <label>
                        资源类型：</label></dt>
                <dd id="roleSelDD">
                    <select id="selResType" runat="server" style="width:100px;">
                        <option value="-1">--请选择--</option>
                    </select>
                </dd>
                <dt>
                    <label>
                        日志类型：</label></dt>
                <dd id="userTypeSelDD">
                    <select id="selLogType" runat="server" style="width:100px;">
                        <option value="-1">--请选择--</option>
                    </select>
                </dd>
                <dt>
                    <label>
                        开始时间：</label></dt>
                <dd id="Dd2">
                    <input id="stdsdate" onclick="WdatePicker()" name="stdsdate" />
                </dd>
                <dt>
                    <label>
                        结束时间：</label></dt>
                <dd id="Dd1">
                    <input id="stdedate" onclick="WdatePicker()" name="stdedate" />
                </dd>
            </dl>
        </div>
    </div>
    <input id="hidQueryCondition" runat="server" type="hidden" />
    <asp:Button ID="btnSearch" CssClass="hide customised-search-trigger" runat="server"
        Text="检索" OnClick="btnSearch_Click" />
</asp:Content>
