<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true" CodeBehind="ApplyDownLoadList.aspx.cs" Inherits="DRMS.MirrorWeb.view.ApplyDownLoadList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            var q = "0";
            switch (QueryString("q")) {
                case "0":
                    {
                        q = "0";
                        $(".TYcurrent").html("个人中心 > 待下载的资源");
                    }
                    break;
                case "1":
                    {
                        q = "1";
                        $(".TYcurrent").html("个人中心 > 我申请下载的资源");
                    }
                    break;
                default: break;
            }
            $("#ifrContent_book").attr("src", "../view/MyApplyDownloadList.aspx?q="+q);
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TYContainerRBG">
        <div class="TYContainerR">
            <div class="TYcurrentDate">
                <p>
                    <span class="fr"></span><span class="TYcurrent">当前位置：个人中心 > 待下载的资源</span></p>
            </div>
            <div class="TYperiodical_List">
                <div class="TYContainerRText_title MT clearfix">
                    <div class="TYCRTextTitle_arTab fl">
                        <div class="TYCRTextTitle_left fl">
                        </div>
                        <div class="TYCRTextTitle_center fl">
                            资源列表</div>
                        <div class="TYCRTextTitle_right fl">
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hdnQueryCon" runat="server" />
                <!-- 加载 iframe 页面 -->
                <iframe id="ifrContent_book" marginwidth="0" onload="Javascript:$('#ifrContent_book').height($('#ifrContent_book').contents().find('body').height()+15)"
                    width="100%" marginheight="0" frameborder="0" scrolling="no"></iframe>
            </div>
        </div>
    </div>
</asp:Content>
