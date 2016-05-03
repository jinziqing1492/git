<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true" CodeBehind="MyFavorite.aspx.cs" Inherits="DRMS.MirrorWeb.UserCenter.MyFavorite" %>
<%@ Register Src="~/UserControl/MyFavoriteView.ascx" TagName="favoriteView" TagPrefix="DRMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="TYContainerRBG">
        <div class="TYContainerR">
            <div class="TYcurrentDate">
                <p>
                    <span class="fr"></span><span class="TYcurrent">当前位置：个人中心 > 我的收藏</span></p>
            </div>
            <div class="TYperiodical_List">
                <div class="TYContainerRText_title MT clearfix">
                    <div class="TYCRTextTitle_arTab fl">
                        <div class="TYCRTextTitle_left fl">
                        </div>
                        <div class="TYCRTextTitle_center fl">
                            我的收藏</div>
                        <div class="TYCRTextTitle_right fl">
                        </div>
                    </div>
                </div>
                <DRMS:favoriteView ID="ctrl_view" runat="server" />
            </div>             
        </div>
    </div>
</asp:Content>
