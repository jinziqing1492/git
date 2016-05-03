<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OwnerTypeItem.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.OwnerTypeItem" %>
<%@ Register Src="../AdminUserControl/NotificationBoxView.ascx" TagName="NotificationView"
    TagPrefix="ACM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function setImgCenter(obj) {
            //设置图片居中
            var marginTop = ($(obj).parent().height() - $(obj).height()) / 2;
            $(obj).css("margin-top", marginTop);
        }
    </script>
    <style type="text/css">
        .cover{ display:inline-block; width:100px; height:100px; border:1px solid #d3d3d3; text-align:center; line-height:100px;}
        .cover img{ max-height:100px; _height:100px; max-width:100px; _width:100px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="shortcut-buttons-set">
        <li><a class="shortcut-button clear-input" href="#"><span>
            <img src="../images/icons/edit-clear.png" alt="icon" /><br />
            清空输入</span> </a></li>
        <li style="float:right;"><a class="shortcut-button" href="OwnerTypeList.aspx"><span>
            <img src="../images/icons/folder-documents.png" alt="icon" /><br />
            返回自有资源类别列表</span> </a></li>
    </ul>
    <!-- End .shortcut-buttons-set -->
    <div class="clear">
    </div>
    <!-- End .clear -->
    <div class="content-box">
        <div class="content-box-header">
            <h3>
                管理自有资源类别</h3>
            <ul class="content-box-tabs">
                <li><a class="default-tab" href="#tab1">编辑自有资源类别</a></li>
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
                                <label>名称：</label></dt>
                        <dd>
                            <asp:TextBox ID="tbxName" runat="server" CssClass="text-input medium-input"></asp:TextBox>
                            <span class="input-notification information png_bg">必填项</span>
                        </dd>
                        <dt>
                            <label>
                                封面：</label></dt>
                        <dd>
                            <a href="javascript:void(0)" class="cover">
                                <%--<img src="../Page/ShowPic.aspx?path=\Manufacturer\OneCompanyCR_Index_Products1.jpg&vpath=1" onload="setImgCenter(this)"/>--%>
                                <asp:Literal ID="lt_cover" runat="server">暂无封面</asp:Literal>
                            </a>
                        </dd>
                        <dt>
                            <label>
                                修改封面：</label></dt>
                        <dd>
                            <asp:FileUpload ID="file_cover" runat="server" CssClass="file"/>
                            <span class="input-notification information png_bg">
                                限制格式：.jpg .png .gif  &nbsp;&nbsp;文件大小：小于1M
                            </span>
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

