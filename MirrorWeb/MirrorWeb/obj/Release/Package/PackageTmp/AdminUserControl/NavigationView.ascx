<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationView.ascx.cs" Inherits="DRMS.MirrorWeb.AdminUserControl.NavigationView" %>
<ul id="main-nav">
    <!-- Accordion Menu -->
    <li><a href="../Admin/Default.aspx" class="nav-top-item no-submenu current">系统首页</a>
        <!-- Add the class "no-submenu" to menu items with no sub menu -->
    </li>
    <li id="Li3" runat="server"><a href="#" class="nav-top-item">管理账户</a>
        <ul>
            <li><a href="../admin/UserItem.aspx">添加用户</a></li>
            <li><a href="../admin/UserList.aspx">管理用户</a></li>
        </ul>
    </li>
    <li id="Li1" runat="server"><a href="#" class="nav-top-item">资源管理</a>
        <ul>
            <li><a href="../admin/BookList.aspx">管理图书</a></li>
        </ul>
    </li>
    <li><a href="#" class="nav-top-item">自有资源入库</a>
        <ul>
            <li><a href="../Admin/OwnerItem.aspx">添加自有资源</a></li>
            <li><a href="../Admin/OwnerList.aspx">自有资源管理</a></li>
            <li><a href="../Admin/OwnerTypeList.aspx">自有资源分类管理</a></li>
        </ul>
    </li>
    <li id="Li11" runat="server"><a href="#" class="nav-top-item">系统日志</a>
        <ul>
            <li><a href="../Admin/LogList.aspx">查看日志</a></li>
        </ul>
    </li>
    <li><a href="#" class="nav-top-item">设置</a>
        <ul>
            <li><a href="../Admin/ModifyPwd.aspx">修改密码</a></li>
            <li><a class="action" href="../Index.aspx">访问首页</a></li>
            <li><a href="../Logout.aspx?url=admin">退出登录</a></li>
        </ul>
    </li>
</ul>
<!-- End #main-nav -->
