<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OwnerTypeList.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.OwnerTypeList" %>
<%@ Register src="../AdminUserControl/OwnerTypeListView.ascx" tagname="OwnerTypeListView" tagprefix="DRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript" src="../js/jquery-ui-1.8.18.custom.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <ul class="shortcut-buttons-set">
        <li>
        <a class="shortcut-button" href="OwnerTypeItem.aspx">
            <span> <img src="../images/icons/accessories-text-editor.png" alt="icon" /><br />添加自有资源分类</span>
        </a>
        </li>
    </ul>
    <!-- End .shortcut-buttons-set -->
    <div class="clear"></div>
    <div class="content-box">
        <div class="content-box-header">
            <h3>
                管理自有资源</h3>
            <ul class="content-box-tabs">
                <li><a class="default-tab" href="#tab1">自有资源列表</a></li>
            </ul>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">            
             <DRMS:OwnerTypeListView  id="OwnList" runat="server" />
        </div>
        <!-- End .content-box-content -->
    </div>
    <!-- End .content-box -->
</asp:Content>

