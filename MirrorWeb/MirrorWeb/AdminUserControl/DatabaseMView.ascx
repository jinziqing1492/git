<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatabaseMView.ascx.cs" Inherits="DRMS.MirrorWeb.AdminUserControl.DatabaseMView" %>
<div class="TYdata_base MT">
    <div class="TYDataBase_Title">
        <h2>
            数据库</h2>
    </div>
    <div class="TYbaseDataBase">
        <div class="TYbaseDataBase_title">
            <a href="#" class="TYbaseMore">+ 更多</a>
            <h4>
                基础数据库</h4>
        </div>
        <div class="TYbaseDataBase_file clearfix">
            <ul>
                <asp:Literal Text="" ID="ltlbasedatabase" runat="server" />
            </ul>
        </div>
    </div>
    <div class="TYbaseDataBase">
        <div class="TYbaseDataBase_title">
            <a href="#" class="TYbaseMore">+ 更多</a>
            <h4>
                业务应用库</h4>
        </div>
        <div class="TYbaseDataBase_file  clearfix">
            <ul>
                <asp:Literal ID="ltllogdb" runat="server" EnableViewState="false"></asp:Literal>
            </ul>
        </div>
    </div>
    
</div>
