<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ResourceTypeList.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.ResourceTypeList" %>
<%@ Register src="../AdminUserControl/ResourceTypeListView.ascx" tagname="ResourceTypeListView" tagprefix="DRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript" src="../js/jquery-ui-1.8.18.custom.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="shortcut-buttons-set">
        <li><a class="shortcut-button" href="ResourceTypeItem.aspx"><span>
            <img src="../images/icons/accessories-text-editor.png" alt="icon" /><br />
            添加分类</span> </a></li>
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
            <DRMS:ResourceTypeListView  id="TypeList" runat="server" />
        </div>
        <!-- End .content-box-content -->
    </div>
    <!-- End .content-box -->
    <input id="hidQueryCondition" runat="server" type="hidden" />    
    <asp:Button ID="btnSearch" CssClass="hide customised-search-trigger" runat="server" Text="检索" onclick="btnSearch_Click" /> 
</asp:Content>
