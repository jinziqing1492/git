<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OwnerItem.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.OwnerItem" %>
<%@ Register Src="../AdminUserControl/NotificationBoxView.ascx" TagName="NotificationView"
    TagPrefix="ACM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //js验证
        function checkcodenull() {
            if (!$("#<%=tbxCodeName.ClientID%>").val()) {
                alert("资源标示不能为空");
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="shortcut-buttons-set">
        <li><a class="shortcut-button" href="OwnerItem.aspx"><span>
            <img src="../images/icons/accessories-text-editor.png" alt="icon" /><br />
            添加自有资源</span> </a></li>
        <li><a class="shortcut-button clear-input" href="#"><span>
            <img src="../images/icons/edit-clear.png" alt="icon" /><br />
            清空输入</span> </a></li>
        <li style="float:right;"><a class="shortcut-button" href="OwnerList.aspx"><span>
            <img src="../images/icons/folder-documents.png" alt="icon" /><br />
            返回自有资源列表</span> </a></li>
    </ul>
    <!-- End .shortcut-buttons-set -->
    <div class="clear">
    </div>
    <!-- End .clear -->
    <div class="content-box">
        <div class="content-box-header">
            <h3>
                管理自有资源</h3>
            <ul class="content-box-tabs">
                <li><a class="default-tab" href="#tab1">编辑自有资源</a></li>
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
                                <label>资源标示：</label></dt>
                        <dd>
                            <asp:TextBox ID="tbxCodeName" runat="server" CssClass="text-input medium-input"></asp:TextBox>
                            <asp:LinkButton ID="lbtnCheckCodeName" runat="server" OnClientClick="return checkcodenull()" OnClick="lbtnCheckCode_Click" TabIndex="-1">检查资源标示是否可用</asp:LinkButton>
                            <span class="input-notification information png_bg">必填项</span>
                        </dd>
                        <dt>
                                <label>资源名称：</label></dt>
                        <dd>
                            <asp:TextBox ID="tbxName" runat="server" class="text-input medium-input"></asp:TextBox>
                            <span class="input-notification information png_bg">必填项</span>
                        </dd>
                        <dt>
                                <label>选择分类：</label></dt>
                        <dd>
                            <asp:DropDownList CssClass="small-input" ID="ddl_Type" runat="server">
                                <asp:ListItem Value="0" >--请选择--</asp:ListItem>
                            </asp:DropDownList>
                            <span class="input-notification information png_bg">必填项</span>
                        </dd>
                        <dt>
                                <label>描述：</label></dt>
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
