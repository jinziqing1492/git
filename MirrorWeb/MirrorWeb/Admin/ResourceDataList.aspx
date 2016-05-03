<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ResourceDataList.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.ResourceDataList" %>
<%@ Register src="../AdminUserControl/ResourceDataListView.ascx" tagname="ResourceDataListView" tagprefix="DRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript" src="../js/jquery-ui-1.8.18.custom.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="shortcut-buttons-set">
        <li><a class="shortcut-button" href="ResourceTypeItem.aspx"><span>
            <img src="../images/icons/accessories-text-editor.png" alt="icon" /><br />
            添加分类</span> </a></li>
        <li id="record-filter" class="action">
        <a class="shortcut-button" href="#">
            <span> <img src="../images/icons/edit-find-project.png" alt="icon" /><br />检索资源</span>
        </a>
        </li>
    </ul>
    <!-- End .shortcut-buttons-set -->
    <div class="clear"></div>
    <div class="content-box">
        <div class="content-box-header">
            <h3>
                管理资源</h3>
            <ul class="content-box-tabs">
                <li><a class="default-tab" href="#tab1">资源列表</a></li>
            </ul>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">            
            <DRMS:ResourceDataListView  id="DataList" runat="server" />
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
