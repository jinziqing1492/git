<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogListView.ascx.cs" Inherits="DRMS.MirrorWeb.AdminUserControl.LogListView" %>
<%@ Register Src="~/AdminUserControl/NotificationBoxView.ascx" TagName="NotificationView"
    TagPrefix="ACM" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<ACM:NotificationView ID="message" runat="server" MessageType="Information" Visible="true" />
<table class="table-list" pageno="<%=PageNo %>" pagesize="<%=PageSize %>">
    <thead>
        <tr>
            <th>
                <input class="check-all" type="checkbox" />
            </th>
            <th>
                编号
            </th>
            <th>
                用户
            </th>
            <th>
                日志名称
            </th>
            <th>
                资源类型
            </th>
            <th>
                日志类型
            </th>
            <th>
                操作时间
            </th>
            <th>
                登录IP
            </th>
            <th style="width:35px;">
                操作
            </th>
        </tr>
    </thead>
    <tfoot>
        <tr>
            <td colspan="9">
                <div class="bulk-actions align-left">
                     <asp:DropDownList ID="ddlAction" CssClass="action-list" runat="server">
                        <asp:ListItem Value="">选择一个操作...</asp:ListItem>
                        <asp:ListItem Value="batchDelete">批量删除</asp:ListItem>
                    </asp:DropDownList>
                     <asp:Button ID="btnSubmit" runat="server" CssClass="button confirm-batch-action"
                        Text="应用到当前页选择项" OnClick="btnSubmit_Click" />
                    <input id="hidIDList" class="target-list" runat="server" type="hidden" />
                    <asp:DropDownList ID="ddlPageSize" CssClass="action-list" runat="server" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                        AutoPostBack="True">
                        <asp:ListItem Value="">选择每页显示数</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="pagination">
                    <webdiyer:AspNetPager ID="aspNetPager" runat="server" CssClass="aspNetPager" UrlPaging="false"
                        PageSize="10" OnPageChanging="aspNetPager_PageChanging" FirstPageText="第一页" LastPageText="最后一页"
                        NextPageText="后一页" PrevPageText="前一页" InputBoxClass="jump-input" ShowBoxThreshold="10"
                        ShowInputBox="Auto" SubmitButtonClass="jump-button" SubmitButtonText="跳转" NumericButtonCount="5">
                    </webdiyer:AspNetPager>
                </div>
                <!-- End .pagination -->
                <div class="clear">
                </div>
            </td>
        </tr>
    </tfoot>
    <tbody>
        <asp:Repeater ID="repEntryList" OnItemCommand="RepeaterItemCommand" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <input value="<%#Eval("ID") %>" type="checkbox" />
                    </td>
                    <td class="record-no">
                    </td>
                    <td>
                        <%#ReplaceRed(Eval("USERNAME"))%>
                    </td>
                    <td>
                        <%#ReplaceRed(Eval("NAME"))%>
                    </td>
                    <td>
                        <%#ReplaceRed(ResType(Eval("RESTYPE")).ToString())%>
                    </td>
                    <td>
                        <%#ReplaceRed(LogType(Eval("LOGTYPE")).ToString())%>
                    </td>
                    <td>
                        <%#ReplaceRed(Eval("ADDDATE"))%>
                    </td>
                    <td>
                        <%#ReplaceRed(Eval("IP"))%>
                    </td>
                    <th>
                        <asp:ImageButton ID="imgBtnDelete" CommandName="Delete" CssClass="confirm-delete"
                            CommandArgument='<%#Eval("ID")%>' runat="server" ToolTip="删除" ImageUrl="~/images/icons/cross.png" />
                    </th>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
<asp:HiddenField ID="hid_sql" runat="server" />
