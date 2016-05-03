<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserListView.ascx.cs" Inherits="DRMS.MirrorWeb.AdminUserControl.UserListView" %>
<%@ Register Src="~/AdminUserControl/NotificationBoxView.ascx" TagName="NotificationView"
    TagPrefix="DRMS" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<DRMS:NotificationView ID="message" runat="server" MessageType="Information" Visible="true" />
<DRMS:NotificationView ID="result" runat="server" Visible="false" />
<table class="table-list" pageno="<%=PageNo %>" pagesize="<%=PageSize %>">
    <thead>
        <tr>
            <th>
                <input class="check-all" type="checkbox" />
            </th>
            <th>编号</th>
            <th>
                用户名
            </th>
            <th> 
                真实姓名
            </th>
            <th>
                角色
            </th>
            <th>
                是否是机构用户
            </th>
            <th>
                添加时间
            </th>
            <th>
                操作
            </th>
        </tr>
    </thead>
    <tfoot>
        <tr>
            <td colspan="8">
                <div class="bulk-actions align-left">
                    <asp:DropDownList ID="ddlAction" CssClass="action-list" runat="server">
                        <asp:ListItem Value="">选择一个操作...</asp:ListItem>
                        <asp:ListItem Value="batchDelete">批量删除</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button confirm-batch-action"
                        Text="应用到当前页选择项" OnClick="btnSubmit_Click" />
                    <input id="hidIDList" class="target-list" runat="server" type="hidden" />
                </div>
                <div class="pagination">
                    <webdiyer:AspNetPager ID="aspNetPager" runat="server" CssClass="aspNetPager" UrlPaging="false"
                        PageSize="10" FirstPageText="第一页" LastPageText="最后一页" NextPageText="后一页" PrevPageText="前一页"
                        InputBoxClass="jump-input" ShowBoxThreshold="10" ShowInputBox="Always" SubmitButtonClass="jump-button"
                        SubmitButtonText="跳转" onpagechanging="aspNetPager_PageChanging">
                    </webdiyer:AspNetPager>
                </div>
                <!-- End .pagination -->
                <div class="clear">
                </div>
            </td>
        </tr>
    </tfoot>
    <tbody>
        <asp:Repeater ID="repEntryList" runat="server" OnItemCommand="RepeaterItemCommand">
            <ItemTemplate>
                <tr>
                    <td>
                        <input type="checkbox" value='<%#RemoveRed(Eval("UserName")) %>' />
                    </td>
                    <td class="record-no"></td>

                    <td><a href='#' ><%#ReplaceRed(Eval("UserName"))%></a></td>
                    <td>
                        <%#ReplaceRed(Eval("RealName"))%>
                    </td>
                    <td>
                        <%#ReplaceRed(GetRoleName(Eval("Role").ToString()))%>
                    </td>
                    <td>
                        <%#Eval("IsOrg").ToString()=="0"?"不是":"是"%>
                    </td>
                    <td>
                        <%#FormatDate(Eval("ADDDATE")) %>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgBtnResetPwd" CommandName="ResetPwd" CommandArgument='<%#RemoveRed(Eval("UserName"))%>'
                            runat="server" ToolTip="密码重置" ImageUrl="~/images/icons/restpwd.png" Width="16px"/> 
                        <asp:ImageButton ID="imgBtnEdit" CommandName="Edit" CommandArgument='<%#RemoveRed(Eval("UserName"))%>'
                            runat="server" ToolTip="编辑" ImageUrl="~/images/icons/hammer_screwdriver.png" /> 
                        <asp:ImageButton ID="imgBtnDelete" CommandName="Delete" CssClass="confirm-delete"
                            CommandArgument='<%#RemoveRed(Eval("UserName"))%>' runat="server" ToolTip="删除" ImageUrl="~/images/icons/cross.png" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>