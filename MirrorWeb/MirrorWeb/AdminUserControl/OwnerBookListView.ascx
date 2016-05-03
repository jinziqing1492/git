<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OwnerBookListView.ascx.cs" Inherits="DRMS.MirrorWeb.AdminUserControl.OwnerBookListView" %>
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
                所属资源
            </th>
            <th>
                书籍名称
            </th>
            <th>
                添加时间
            </th>
            <th style="width:70px;">
                操作
            </th>
        </tr>
    </thead>
    <tfoot>
        <tr>
            <td colspan="7">
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
                        <input value="<%#Eval("SYS_FLD_DOI") %>" type="checkbox" />
                    </td>
                    <td class="record-no">
                    </td>
                    <td>
                        <%# ReplaceRed(Eval("CNAME")) %>
                    </td>
                    <td>
                        <%#ReplaceRed( Eval("ISSUE")) %>
                    </td>
                    <td>
                        <%#Eval("SYS_FLD_ADDDATE") %>
                    </td>
                    <td>
                        <a href="OwnerBookItem.aspx?doi=<%#RemoveRed(Eval("SYS_FLD_DOI")) %>&baseid=<%#RemoveRed(Eval("BASEID")) %>">
                            <img src="/images/icons/hammer_screwdriver.png" alt="编辑" />
                        </a>
                        <asp:ImageButton ID="imgBtnDelete" CommandName="Delete" CssClass="confirm-delete"
                            CommandArgument='<%#Eval("SYS_FLD_DOI")%>' runat="server" ToolTip="删除" ImageUrl="~/images/icons/cross.png" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
<asp:HiddenField ID="hid_sql" runat="server" />