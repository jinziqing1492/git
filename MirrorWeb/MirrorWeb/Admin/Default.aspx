<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p id="page-intro">快捷导航</p>
    <ul class="shortcut-buttons-set">
      <li><a class="shortcut-button" href="/Admin/UserList.aspx" rel="modal"><span> <img src="../images/icons/user-properties.png" alt="icon" /><br />
        用户管理</span></a></li>
      <li><a class="shortcut-button" href="/Admin/LogList.aspx"><span> <img src="../images/icons/view-calendar-whatsnext.png" alt="icon" /><br />
        管理日志</span></a></li>
    </ul>
    <!-- End .shortcut-buttons-set -->
    <div class="clear"></div>
    <!-- End .clear -->
</asp:Content>

