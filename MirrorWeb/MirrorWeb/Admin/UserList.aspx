<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.UserList" %>
<%@ Register src="../AdminUserControl/UserListView.ascx" tagname="UserListView" tagprefix="DRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" language="javascript" src="../js/jquery-ui-1.8.18.custom.min.js"></script>
    <script type="text/javascript" language="javascript">
        function GetCustomisedQueryCondition() {
            ///<summary>生成自定义查询条件
            ///</summary>
            var queryCondition = [];
            var roleTypeIndex = document.getElementById("selRoleType").selectedIndex;
            var titile = $.trim($("#tbxTitle").val());
            var realname = $.trim($("#tbxRealName").val());
            if (titile !== "") {
                queryCondition.push(String.Format("USERNAME = '?{0}'", titile));
            }
            if (realname !== "") {
                queryCondition.push(String.Format("REALNAME = '?{0}'", realname));
            }
            if (roleTypeIndex != 0) {
                queryCondition.push(String.Format("ROLE = {0}", roleTypeIndex - 1));
            }
            if (queryCondition.length !== 1) {
                queryCondition = queryCondition.join(" and ");
            }
            $("#<%=hidQueryCondition.ClientID %>").val(queryCondition);

            return queryCondition;
        }
        function SetQuery(type) {
            var query = "";
            switch (type) {
                case 1: query = "ISORG=0";
                    break;
                case 2: query = "ISORG=1";
                    break;
            }
            $("#<%=hidQueryCondition.ClientID %>").val(query);
            $("#<%=btnSearch.ClientID %>").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="shortcut-buttons-set">
        <li>
        <a class="shortcut-button" href="UserItem.aspx">
            <span> <img src="../images/icons/accessories-text-editor.png" alt="icon" /><br />添加用户</span>
        </a>
        </li>
        <li id="record-filter" class="action">
        <a class="shortcut-button" href="#">
            <span> <img src="../images/icons/edit-find-project.png" alt="icon" /><br />检索用户</span>
        </a>
        </li>
        <li style="float:right;">
        <a class="shortcut-button" href="javascript:void(0)" onclick="SetQuery(1)">
            <span> <img src="../images/icons/entry_all.png" alt="icon" /><br />普通用户</span>
        </a>
        </li>
        <li style="float:right;">
        <a class="shortcut-button" href="javascript:void(0)" onclick="SetQuery(2)">
            <span> <img src="../images/icons/entry_all.png" alt="icon" /><br />机构用户</span>
        </a>
        </li>
        <li style="float:right;">
        <a class="shortcut-button" href="javascript:void(0)" onclick="SetQuery(2)">
            <span> <img src="../images/icons/entry_all.png" alt="icon" /><br />导入用户</span>
        </a>
        </li>
    </ul>
    <!-- End .shortcut-buttons-set -->
    <div class="clear"></div>
    <div class="content-box">
        <div class="content-box-header">
            <h3>
                管理用户</h3>
            <ul class="content-box-tabs">
                <li><a class="default-tab" href="#tab1">用户列表</a></li>
            </ul>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">            
            <DRMS:UserListView  id="UsersList" runat="server" />
        </div>
        <!-- End .content-box-content -->
    </div>
    <!-- End .content-box -->
    <!-- ui-dialog -->
    <div id="dialog-condition-box" class="hide" title="自定义查询">
        <div class="dialogContainer form-container">
            <dl>
                <dt><label>用户名：</label></dt>
                <dd id="textInputDD"><input id="tbxTitle" class="text-input long-input" type="text" />
                </dd>
                <dt><label>真实姓名：</label></dt>
                <dd id="textInputRealNameDD"><input id="tbxRealName" class="text-input long-input" type="text" />
                </dd>
                <dt><label>角色：</label></dt>
                <dd id="roleSelDD"><select id="selRoleType">
                    <option value="-1">--请选择--</option>
                    <option value="0">普通用户</option>
                    <option value="1">资源管理员</option>
                    <option  value="2">系统管理员</option>
                </select>
                </dd> 
            </dl>
        </div>
    </div>
    <input id="hidQueryCondition" runat="server" type="hidden" />    
    <asp:Button ID="btnSearch" CssClass="hide customised-search-trigger" runat="server" Text="检索" onclick="btnSearch_Click" />  
</asp:Content>
