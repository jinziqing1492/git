<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataBaseListNavView.ascx.cs" Inherits="DRMS.MirrorWeb.UserControl.DataBaseListNavView" %>
<div class="TYSubnavL  clearfix"">
    <h3 class="TYSubnavL_title">
        <span>
            <img src="../images/TYsubnav_datebase.gif" /></span>数据库</h3>
    <ul class="TYSubnavL_list TYSubnavL_Mytask">
        <li><a id="basedatabase_a" href="/view/UserMoreBaseDBList.aspx"><span>
            <img src="../images/Img_spirit20.gif" /></span>基础数据库</a>
            <div class="DBSubnavL_subordinate" style="display: none;">
                <ul>
                    <asp:Literal Text="" ID="ltlbasedatabaseview" runat="server" />
                </ul>
            </div>
        </li>
    </ul>
</div>
