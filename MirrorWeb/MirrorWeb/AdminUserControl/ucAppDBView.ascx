<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAppDBView.ascx.cs"
    Inherits="DRMS.MirrorWeb.AdminUserControl.ucAppDBView" %>
<script type="text/javascript">
    $(function () {
        $(".EditDBInfoDiv").click(function () {
            window.location.href = "AddAppDataBase.aspx?id=" + $("#<%=hdnLogicDBID.ClientID %>").val();
        });

        $(".TYCRTextTitle_arTab").mouseenter(function () {
            $(this).children(".TYCRTextTitle_left").removeClass("TYCRTextTitle_left").addClass("TYCRTextTitle_left_hover");
            $(this).children(".TYCRTextTitle_center").removeClass("TYCRTextTitle_center").addClass("TYCRTextTitle_center_hover");
            $(this).children(".TYCRTextTitle_right").removeClass("TYCRTextTitle_right").addClass("TYCRTextTitle_right_hover");
        });
        $(".TYCRTextTitle_arTab").mouseleave(function () {
            $(this).children(".TYCRTextTitle_left_hover").removeClass("TYCRTextTitle_left_hover").addClass("TYCRTextTitle_left");
            $(this).children(".TYCRTextTitle_center_hover").removeClass("TYCRTextTitle_center_hover").addClass("TYCRTextTitle_center");
            $(this).children(".TYCRTextTitle_right_hover").removeClass("TYCRTextTitle_right_hover").addClass("TYCRTextTitle_right");
        });

        $(".TYCRTextTitle_arTab").click(function () {
            if (!$(this).attr("field")) return;

            //切换tab样式
            $(".TYCRTextTitle_arTab").children(".TYCRTextTitle_left_click").removeClass("TYCRTextTitle_left_click").addClass("TYCRTextTitle_left");
            $(".TYCRTextTitle_arTab").children(".TYCRTextTitle_center_click").removeClass("TYCRTextTitle_center_click").addClass("TYCRTextTitle_center");
            $(".TYCRTextTitle_arTab").children(".TYCRTextTitle_right_click").removeClass("TYCRTextTitle_right_click").addClass("TYCRTextTitle_right");
            $(this).children(".TYCRTextTitle_left").removeClass("TYCRTextTitle_left").addClass("TYCRTextTitle_left_click");
            $(this).children(".TYCRTextTitle_center").removeClass("TYCRTextTitle_center").addClass("TYCRTextTitle_center_click");
            $(this).children(".TYCRTextTitle_right").removeClass("TYCRTextTitle_right").addClass("TYCRTextTitle_right_click");
            $(this).children(".TYCRTextTitle_left_hover").removeClass("TYCRTextTitle_left_hover").addClass("TYCRTextTitle_left_click");
            $(this).children(".TYCRTextTitle_center_hover").removeClass("TYCRTextTitle_center_hover").addClass("TYCRTextTitle_center_click");
            $(this).children(".TYCRTextTitle_right_hover").removeClass("TYCRTextTitle_right_hover").addClass("TYCRTextTitle_right_click");

            $("#ifrContent").attr("src", "../auditadmin/ResViewList.aspx?lid=" + QueryString("id") + "&rtf="
                    + $(this).attr("field"));
        });
        $(".TYCRTextTitle_arTab[field='1']").click();
        SetImgAutoSize();
        $(window).resize(SetImgAutoSize);
    });
    function SetImgAutoSize() {
        var img = $("#<%=bookcover_img.ClientID %>"); // obj;
        var MaxWidth = $(img).parent().parent().parent().parent().parent().width() * 0.3 - 50;  //设置图片宽度界限
        var MaxHeight = MaxWidth; //设置图片高度界限

        var HeightWidth = img.height() / img.width(); //设置高宽比
        var MaxHeightWidth = MaxHeight / MaxWidth;

        if (HeightWidth < MaxHeightWidth) {// && img.width() > MaxWidth) {
            img.css("width", MaxWidth);
            img.css("height", MaxWidth * HeightWidth);
        }
        else if (HeightWidth > MaxHeightWidth) {// && img.height() > MaxHeight) {
            img.css("height", MaxHeight);
            img.css("width", MaxHeight / HeightWidth);
        }
    }
</script>
<div class="TYperiodical_List">
    <div class="TYContainerRText_title MT clearfix">
        <div class="TYCRTextTitle_arTabTitle">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                库基本信息</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
        <asp:Literal ID="LiteralEditDB" runat="server"></asp:Literal>
    </div>
    <!--图书基本信息-->
    <div class="book-main">
        <div class="bookBase-dalite">
            <table>
                <tbody>
                    <tr>
                        <td rowspan="3">
                            <div class="bookBase-intro">
                                <div class="picarea">
                                    <img src="../images/zanwu.jpg" id="bookcover_img" alt="" runat="server" /></div>
                                <div class="clear">
                                </div>
                            </div>
                        </td>
                        <td>
                            <strong>数据库名：</strong><span id="DBNAME" runat="server"></span>
                        </td>
                        <td>
                            <strong>逻辑库类型：</strong><span id="DBTYPE" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <strong>数据库描述：</strong><span id="DBDESCRIPTION" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <strong>备注：</strong><span id="REMARK" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="TYContainerRText_title MT clearfix">
        <div class="TYCRTextTitle_arTab" field="1">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                图书</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
        <div class="TYCRTextTitle_arTab" field="2">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                标准</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
        <div class="TYCRTextTitle_arTab" field="3">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                工具书</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
        <div class="TYCRTextTitle_arTab" field="4">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                期刊</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
        <div class="TYCRTextTitle_arTab" field="5">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                会议论文</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
        <div class="TYCRTextTitle_arTab" field="6">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                年鉴</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
        <div class="TYCRTextTitle_arTab" field="7">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                杂志</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
        <div class="TYCRTextTitle_arTab" field="8">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                报纸</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
        <div class="TYCRTextTitle_arTab" field="9">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                学位论文</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
        <div class="TYCRTextTitle_arTab" field="10">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                图片</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
        <div class="TYCRTextTitle_arTab" field="11">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                音频</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
        <div class="TYCRTextTitle_arTab" field="12">
            <div class="TYCRTextTitle_left fl">
            </div>
            <div class="TYCRTextTitle_center fl">
                视频</div>
            <div class="TYCRTextTitle_right fl">
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnLogicDBID" runat="server" Value="-1" />
    <!-- 加载 iframe 页面 -->
    <iframe id="ifrContent" name="ifrContentName" marginwidth="0" onload="Javascript:$('#ifrContent').height($('#ifrContent').contents().find('body').height()+15)"
        width="100%" marginheight="0" frameborder="0" scrolling="no"></iframe>
    <div class="TYContainer_Btn MT">
        <a href="javascript:window.history.go(-1)">返&nbsp;回</a>
    </div>
</div>
