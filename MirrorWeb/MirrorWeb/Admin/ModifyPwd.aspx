<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ModifyPwd.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.ModifyPwd" %>
<%@ Register Src="../AdminUserControl/NotificationBoxView.ascx" TagName="NotificationView"
    TagPrefix="GPH" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validatePassword() {
            var origpwd = $.trim($("#<%=txtOrigPwd.ClientID %>").val());
            var pwd = $.trim($("#<%=txtPwd.ClientID %>").val());
            var repwd = $.trim($("#<%=txtRePwd.ClientID %>").val());
            var sp = $("#error_notification");
            var orig_sp = $("#orig_error_notification");
            if (origpwd === "") {
                orig_sp.show();
                orig_sp.text("请输入旧密码");
                return false;
            }
            if (pwd === "") {
                sp.show();
                sp.text("请输入新密码");
                return false;
            }
            if (repwd === "") {
                sp.show();
                sp.text("请重复输入新密码");
                return false;
            }
            if (pwd !== repwd) {
                sp.show();
                sp.text("两次输入的密码不相同");
                return false;
            }
            if (repwd.length > 10 || pwd.length < 6) {
                sp.show();
                sp.text("密码的长度应为6-10个字符");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-box">
        <div class="content-box-header">
            <h3>
                修改密码</h3>
            <ul class="content-box-tabs">
                <li><a class="default-tab" href="#tab1">修改密码</a></li>
            </ul>
            <div class="clear">
            </div>
        </div>
        <!-- End .content-box-header -->
        <div class="content-box-content">
            <div class="tab-content default-tab">
                <GPH:NotificationView ID="message" runat="server" MessageType="Information" 
                    Content="提示信息" Visible="false" />             
                <div class="form-container">
                    <dl>
                        <dt>
                            <label>旧密码：</label></dt>
                        <dd>
                            <asp:TextBox ID="txtOrigPwd" runat="server" class="text-input medium-input" 
                                TextMode="Password"></asp:TextBox>
                            <span id="orig_error_notification" class="input-notification hide information png_bg"></span>
                        </dd>
                        <dt>
                            <label>新密码：</label></dt>
                        <dd>
                            <asp:TextBox ID="txtPwd" runat="server" class="text-input medium-input" 
                                TextMode="Password"></asp:TextBox>
                            <span class="input-notification information png_bg">6-10个字符</span>
                        </dd>
                        <dt>
                            <label>重复新密码：</label></dt>
                        <dd><asp:TextBox ID="txtRePwd"  runat="server" class="text-input medium-input" 
                                TextMode="Password"></asp:TextBox>
                            <span id="error_notification" class="input-notification hide information png_bg"></span>
                        </dd>
                    </dl>          
                    <div class="clear"></div>          
                    <p class="submit" style="width:630px;">
                        <asp:Button ID="btnOk" runat="server" class="button" Text="提交" 
                            onclick="btnOk_Click" OnClientClick="return validatePassword();"/>
                        <input type="submit" value="取消" class="button" />
                    </p>
                </div>
            </div>
            <!-- End #tab3 -->
        </div>
    </div>
</asp:Content>