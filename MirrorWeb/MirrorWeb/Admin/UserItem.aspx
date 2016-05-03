<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserItem.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.UserItem" %>
<%@ Register Src="../AdminUserControl/NotificationBoxView.ascx" TagName="NotificationView"
    TagPrefix="ACM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <link href="../js/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">
    $(function () {
        var seletidfrist = $("#<%=ddlOrg.ClientID %>").val();
        if (seletidfrist == '1') {
            $("#dtIpStart,#ddIpStart,#dtIpEnd,#ddIpEnd,#dtMaxOnlie,#ddMaxOnlie").show();
            $("#dtUnit,#ddUnit,#dtUserRole,#ddUserRole,#dtUserGrade,#ddUserGrade").hide();
            $("#lbl_realName").text("机构名称：");
        }
        else {
            $("#dtIpStart,#ddIpStart,#dtIpEnd,#ddIpEnd,#dtMaxOnlie,#ddMaxOnlie").hide();
            $("#dtUnit,#ddUnit,#dtUserRole,#ddUserRole,#dtUserGrade,#ddUserGrade").show();
            $("#lbl_realName").text("真实姓名：");
        }
        $("#<%=ddlOrg.ClientID %>").change(function () {
            var seletid = $("#<%=ddlOrg.ClientID %>").val();
            if (seletid == '1') {
                $("#dtIpStart,#ddIpStart,#dtIpEnd,#ddIpEnd,#dtMaxOnlie,#ddMaxOnlie").show();
                $("#dtUnit,#ddUnit,#dtUserRole,#ddUserRole,#dtUserGrade,#ddUserGrade").hide();
                $("#lbl_realName").text("机构名称：");
            }
            else {
                $("#dtIpStart,#ddIpStart,#dtIpEnd,#ddIpEnd,#dtMaxOnlie,#ddMaxOnlie").hide();
                $("#dtUnit,#ddUnit,#dtUserRole,#ddUserRole,#dtUserGrade,#ddUserGrade").show();
                $("#lbl_realName").text("真实姓名：");
            }
        });
    });

    function showDateTimePicker(format) {
        format = format || "yyyy-MM-dd";
        WdatePicker({ dateFmt: format, readOnly: 'true' });
    }
</script>
<style type="text/css">
    dt p{ font-weight:bold; margin:0px; padding:0px; line-height:25px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="shortcut-buttons-set">
        <li><a class="shortcut-button" href="UserItem.aspx"><span>
            <img src="../images/icons/accessories-text-editor.png" alt="icon" /><br />
            添加用户</span> </a></li>
        <li><a class="shortcut-button clear-input" href="#"><span>
            <img src="../images/icons/edit-clear.png" alt="icon" /><br />
            清空输入</span> </a></li>
        <li><a class="shortcut-button" href="UserList.aspx"><span>
            <img src="../images/icons/folder-documents.png" alt="icon" /><br />
            返回用户列表</span> </a></li>
    </ul>
    <!-- End .shortcut-buttons-set -->
    <div class="clear">
    </div>
    <!-- End .clear -->
    <div class="content-box">
        <div class="content-box-header">
            <h3>
                管理用户</h3>
            <ul class="content-box-tabs">
                <li><a class="default-tab" href="#tab1"><%=OperateLabel%></a></li>
            </ul>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">
            <div class="tab-content default-tab">
                <ACM:NotificationView ID="message" runat="server" MessageType="Information"
                    Visible="false" />
                <div class="form-container">
                    <dl>
                        <dt>
                            <p>
                                用户名：</p></dt>
                        <dd>
                            <asp:TextBox ID="tbxUserName" runat="server" class="text-input medium-input"></asp:TextBox>
                            <asp:LinkButton ID="lbtnCheckUserName" runat="server" OnClick="lbtnCheckUserName_Click">检查用户名是否可用</asp:LinkButton>
                            <span class="input-notification information png_bg">必填项</span>
                        </dd>
                        <dt>
                            <p>
                                联系电话：</p></dt>
                        <dd>
                            <asp:TextBox ID="tbxPhone1" runat="server" class="text-input medium-input"></asp:TextBox>
                        </dd>
                         <dt>
                            <p>
                                EMAIL：</p></dt>
                        <dd>
                            <asp:TextBox ID="tbxEmail" runat="server" class="text-input medium-input"></asp:TextBox>
                        </dd>
                        
                        <dt>
                            <p>
                                是否机构用户：</p></dt>
                        <dd>
                            <asp:DropDownList CssClass="small-input" ID="ddlOrg" runat="server">
                                <asp:ListItem Value="0" >非机构用户</asp:ListItem>
                                <asp:ListItem Value="1">机构用户</asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                        <dt>
                            <p id="lbl_realName">
                                真实姓名：</p></dt>
                        <dd>
                            <asp:TextBox ID="tbxRealName" runat="server" class="text-input medium-input"></asp:TextBox>
                        </dd>
                        <dt id="dtUnit">
                            <p>
                                单位名称：</p></dt>
                        <dd id="ddUnit">
                            <asp:TextBox ID="tbxUnit" runat="server" class="text-input medium-input"></asp:TextBox>
                        </dd>
                         <dt id="dtIpStart">
                            <p>
                                绑定的IP开始：</p></dt>
                        <dd id="ddIpStart">
                            <asp:TextBox ID="tbxIpStart" runat="server" class="text-input medium-input"></asp:TextBox>
                        </dd>
                         <dt id="dtIpEnd">
                            <p>
                                绑定的IP结束：</p></dt>
                        <dd id="ddIpEnd">
                            <asp:TextBox ID="tbxIpEnd" runat="server" class="text-input medium-input"></asp:TextBox>
                        </dd>
                         <dt id="dt1">
                            <p>
                                开始日期：</p></dt>
                        <dd id="dd1">
                            <asp:TextBox ID="tbxStartDate" runat="server" class="text-input medium-input Wdate"
                                onfocus="showDateTimePicker();"></asp:TextBox>
                        </dd>
                         <dt id="dt2">
                            <p>
                                结束日期：</p></dt>
                        <dd id="dd2">
                            <asp:TextBox ID="tbxEndDate" runat="server" class="text-input medium-input Wdate"
                                onfocus="showDateTimePicker();"></asp:TextBox>
                        </dd>
                        <dt id="dtMaxOnlie">
                            <p>
                                最大在线数：</p></dt>
                        <dd id="ddMaxOnlie">
                            <asp:TextBox ID="tbxMaxOnline" runat="server" class="text-input medium-input"></asp:TextBox>
                        </dd>
                        <dt>
                            <p>
                                用户状态：</p></dt>
                        <dd>
                            <asp:DropDownList CssClass="small-input" ID="ddlStatus" runat="server">
                                <asp:ListItem Value="0">正常状态</asp:ListItem>
                                <asp:ListItem Value="1">锁定状态</asp:ListItem>
                                <asp:ListItem Value="2">禁用状态</asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                        <dt id="dtUserRole">
                            <p>
                                用户角色：</p></dt>
                        <dd id="ddUserRole">
                            <asp:DropDownList CssClass="small-input" ID="ddlRole" runat="server">
                                <asp:ListItem Value="0">普通用户</asp:ListItem>
                                <asp:ListItem Value="1">系统管理员</asp:ListItem>
                            </asp:DropDownList>
                        </dd>
                        <dt>
                            <p>
                                备注：</p></dt>
                        <dd>
                            <asp:TextBox ID="tbxRemark" runat="server" TextMode="MultiLine" class="text-input textarea"
                                Height="90px"></asp:TextBox>
                        </dd>
                    </dl>
                    <div class="clear">
                    </div>
                    <p class="submit">
                        <asp:Button ID="btnOk" runat="server" class="button" Text="提交" OnClick="btnOk_Click" />
                        <input type="submit" value="取消" class="button" />
                    </p>
                </div>
            </div>
            <!-- End #tab3 -->
        </div>
        <!-- End .content-box-content -->
    </div>
    <!-- End .content-box -->
</asp:Content>
