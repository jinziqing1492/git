<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ResAdmin.Master" AutoEventWireup="true"
    CodeBehind="AdminDefault.aspx.cs" Inherits="DRMS.MirrorWeb.AdminDefault" %>

<%@ Register Src="~/AdminUserControl/TaskView.ascx" TagName="task" TagPrefix="drms" %>
<%@ Register Src="~/AdminUserControl/DatabaseMView.ascx" TagName="dblist" TagPrefix="drms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //页面加载中head的按钮特效
        $(function () { $("#Head li").first().addClass("TYnav_active"); });
        $(document).ready(function () {
            $("#FirstPage").parent().addClass("TYnav_active");
            var taskcount = 0;
            // 获取所有的任务数
            $(".TaskNum").each(function (index) {
                // alert(index); //循环的下标值，从0开始 
                taskcount = taskcount + parseInt($(this).text());
            });
            if (taskcount == 0 || isNaN(taskcount)) {
                $(".TYtaskDo-title").html("无待办任务");
            }
            else {
                $(".TYtaskDo-title").html("待办任务<span>（总共<em>" + taskcount + "</em>条任务）</span>");
            }

            $.ajax({
                type: "GET",
                url: "../Ajax/GetDBResCount.ashx",
                data: "dbid=",
                dataType: "json",
                success: function (data) {
                    if (data) {
                        for (var i = 0; i < data.length; i++) {

                            $("#" + data[i].DBtype + " p").html("共有<em>" + data[i].Count + "</em>条数据");
                        }

                    }
                }
            });


        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="TYcontainer">
        <div class="TYcurrentDate">
            <p>
                <span class="TYcurrent">当前位置：系统首页</span> <span class="fr">2013年7月16日 星期二</span>
            </p>
        </div>
        <%--<drms:task ID="tasklist" runat="server" />--%>
        <drms:dblist ID="dblistview" runat="server" />
    </div>
</asp:Content>
