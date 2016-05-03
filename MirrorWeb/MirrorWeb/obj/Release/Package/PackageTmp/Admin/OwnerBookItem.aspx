<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OwnerBookItem.aspx.cs" Inherits="DRMS.MirrorWeb.Admin.OwnerBookItem" %>
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
        .downfile{ display:block; height:30px;}
        .file{ margin-bottom:10px; margin-top:3px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="shortcut-buttons-set">
        <li><a class="shortcut-button" href="OwnerItem.aspx"><span>
            <img src="../images/icons/accessories-text-editor.png" alt="icon" /><br />
            添加书籍</span> </a></li>
        <li><a class="shortcut-button clear-input" href="#"><span>
            <img src="../images/icons/edit-clear.png" alt="icon" /><br />
            清空输入</span> </a></li>
        <li style="float:right;"><a class="shortcut-button" href="OwnerList.aspx"><span>
            <img src="../images/icons/folder-documents.png" alt="icon" /><br />
            返回书籍列表</span> </a></li>
    </ul>
    <!-- End .shortcut-buttons-set -->
    <div class="clear">
    </div>
    <!-- End .clear -->
    <div class="content-box">
        <div class="content-box-header">
            <h3>
                管理书籍</h3>
            <ul class="content-box-tabs">
                <li><a class="default-tab" href="#tab1">编辑书籍</a></li>
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
                               <label>书籍名称：</label> </dt>
                        <dd>
                            <asp:TextBox ID="tbxName" runat="server" class="text-input medium-input"></asp:TextBox>
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
                            <label>
                                Pdf文件：
                            </label>
                        </dt>
                        <dd>
                            <asp:Literal ID="lt_pdf" runat="server">
                                <a href="javascript:void(0)" class="downfile">暂无文件</a>
                            </asp:Literal>
                        </dd>
                        <dt>
                            <label>
                                修改Pdf：</label></dt>
                        <dd>
                            <asp:FileUpload ID="file_pdf" runat="server" CssClass="file"/>
                            <span class="input-notification information png_bg">
                                限制格式：.pdf  &nbsp;&nbsp;文件大小：小于200M
                            </span>
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
        </div>
        <!-- End .content-box-content -->
    </div>
    <!-- End .content-box -->
</asp:Content>
