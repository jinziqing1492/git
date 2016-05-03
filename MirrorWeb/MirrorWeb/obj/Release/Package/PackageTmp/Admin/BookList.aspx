<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="BookList.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.BookList" %>
<%@ Register src="../AdminUserControl/BookListView.ascx" tagname="BookListView" tagprefix="DRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript" src="../js/jquery-ui-1.8.18.custom.min.js"></script>
    <script type="text/javascript" language="javascript">
        function GetCustomisedQueryCondition() {
            ///<summary>生成自定义查询条件
            ///</summary>
            var queryCondition = [];
            var name = $.trim($("#tbxName").val());
            var author = $.trim($("#tbxAuthor").val());
            var isbn = $.trim($("#tbxIsbn").val());
            var issueDep = $.trim($("#tbxIssueDep").val());
            var readType = document.getElementById("selReadType").selectedIndex;
            if (name != "") {
                queryCondition.push(String.Format("NAME = '{0}'", name));
            }
            if (author != "") {
                queryCondition.push(String.Format("AUTHOR = '?{0}'", author));
            }
            if (isbn != "") {
                queryCondition.push(String.Format("ISBN = '?{0}'", isbn));
            }
            if (issueDep != "") {
                queryCondition.push(String.Format("ISSUEDEP = '?{0}'", issueDep));
            }
            if (readType != 0) {
                if (readType == 2) {
                    queryCondition.push("ReadType=2");
                }
                else {
                    queryCondition.push("(ReadType=* not ReadType=2)");
                }
            }
            if (queryCondition.length != 1) {
                queryCondition = queryCondition.join(" and ");
            }
            $("#<%=hidQueryCondition.ClientID %>").val(queryCondition);

            return queryCondition;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="shortcut-buttons-set">
        <li id="record-filter" class="action">
        <a class="shortcut-button" href="#">
            <span> <img src="../images/icons/edit-find-project.png" alt="icon" /><br />检索图书</span>
        </a>
        </li>
    </ul>
    <!-- End .shortcut-buttons-set -->
    <div class="clear"></div>
    <div class="content-box">
        <div class="content-box-header">
            <h3>
                管理图书</h3>
            <ul class="content-box-tabs">
                <li><a class="default-tab" href="#tab1">图书列表</a></li>
            </ul>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">            
            <DRMS:BookListView  id="BooksList" runat="server" />
        </div>
        <!-- End .content-box-content -->
    </div>
    <!-- End .content-box -->
    <!-- ui-dialog -->
    <div id="dialog-condition-box" class="hide" title="自定义查询">
        <div class="dialogContainer form-container">
            <dl>
                <dt><label>图书名称：</label></dt>
                <dd id="textInputDD"><input id="tbxName" class="text-input long-input" type="text" />
                </dd>
                <dt><label>作者：</label></dt>
                <dd id="textInputRealNameDD"><input id="tbxAuthor" class="text-input long-input" type="text" />
                </dd>
                <dt><label>ISBN：</label></dt>
                <dd id="Dd1"><input id="tbxIsbn" class="text-input long-input" type="text" />
                </dd>
                <dt><label>出版单位：</label></dt>
                <dd id="Dd2"><input id="tbxIssueDep" class="text-input long-input" type="text" />
                </dd>
                <dt><label>工作时间阅读：</label></dt>
                <dd id="Dd3"><select id="selReadType">
                    <option value="-1">--请选择--</option>
                    <option value="0">允许</option>
                    <option value="1">禁止</option>
                </select>
                </dd>
            </dl>
        </div>
    </div>
    <input id="hidQueryCondition" runat="server" type="hidden" />    
    <asp:Button ID="btnSearch" CssClass="hide customised-search-trigger" runat="server" Text="检索" onclick="btnSearch_Click" />  
</asp:Content>
