<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OwnerBookList.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.OwnerBookList" %>
<%@ Register src="../AdminUserControl/OwnerBookListView.ascx" tagname="OwnerBookListView" tagprefix="DRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript" src="../js/jquery-ui-1.8.18.custom.min.js"></script>
    <script type="text/javascript" language="javascript">
        function GetCustomisedQueryCondition() {
            ///<summary>生成自定义查询条件
            ///</summary>
            var queryCondition = [];
            var name = $.trim($("#tbxName").val());
            if (name !== "") {
                queryCondition.push(String.Format("ISSUE = '?{0}'", name));
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
        <li>
        <a class="shortcut-button" href="OwnerBookItem.aspx?baseid=<%=Request.QueryString["baseid"] %>">
            <span> <img src="../images/icons/accessories-text-editor.png" alt="icon" /><br />添加书籍</span>
        </a>
        </li>
        <li id="record-filter" class="action">
        <a class="shortcut-button" href="#">
            <span> <img src="../images/icons/edit-find-project.png" alt="icon" /><br />检索书籍</span>
        </a>
        </li>
    </ul>
    <!-- End .shortcut-buttons-set -->
    <div class="clear"></div>
    <div class="content-box">
        <div class="content-box-header">
            <h3>
                管理书籍</h3>
            <ul class="content-box-tabs">
                <li><a class="default-tab" href="#tab1">书籍列表</a></li>
            </ul>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">            
            <DRMS:OwnerBookListView  id="OwnBookList" runat="server" />
        </div>
        <!-- End .content-box-content -->
    </div>
    <!-- End .content-box -->
    <!-- ui-dialog -->
    <div id="dialog-condition-box" class="hide" title="自定义查询">
        <div class="dialogContainer form-container">
            <dl>
                <dt><label>书籍名称：</label></dt>
                <dd id="bookName"><input id="tbxName" class="text-input long-input" type="text" />
                </dd>
            </dl>
        </div>
    </div>
    <input id="hidQueryCondition" runat="server" type="hidden" />    
    <asp:Button ID="btnSearch" CssClass="hide customised-search-trigger" runat="server" Text="检索" onclick="btnSearch_Click" />  
</asp:Content>

