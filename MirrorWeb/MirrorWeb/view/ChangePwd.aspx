<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true" CodeBehind="ChangePwd.aspx.cs" Inherits="DRMS.MirrorWeb.view.ChangePwd" %>
<%@ Register Src="~/UserControl/ucChangePwd.ascx" TagName="ucCgPwd" TagPrefix="drms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/TYcommon.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TYContainerRBG">
        <div class="TYContainerR">
            <div class="TYcurrentDate">
                <p>
                    <span class="fr"></span><span class="TYcurrent">当前位置：个人中心 > 修改密码</span></p>
            </div>
            <div class="TYperiodical_List">
                <div class="TYContainerRText_title MT clearfix">
                    <div class="TYCRTextTitle_arTab fl">
                        <div class="TYCRTextTitle_left fl">
                        </div>
                        <div class="TYCRTextTitle_center fl">
                            修改密码</div>
                        <div class="TYCRTextTitle_right fl">
                        </div>
                    </div>
                </div>
                <drms:ucCgPwd ID="myChangePwd" runat="server" />
            </div>             
        </div>
    </div>
</asp:Content>
