<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NotificationBoxView.ascx.cs" Inherits="DRMS.MirrorWeb.AdminUserControl.NotificationBoxView" %>
<asp:Panel ID="boxContainer" runat="server">
    <div class="notification <%=GetClassName() %> png_bg"> 
        <a href="#" class="close"><img src="../images/icons/cross_grey_small.png" title="关闭该通知" alt="close" /></a>
      <div><%=GetContent() %></div>  
    </div>
</asp:Panel>