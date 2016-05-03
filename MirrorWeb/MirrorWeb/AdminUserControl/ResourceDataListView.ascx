<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResourceDataListView.ascx.cs" Inherits="DRMS.MirrorWeb.AdminUserControl.ResourceDataListView" %>
<%@ Register Src="~/AdminUserControl/NotificationBoxView.ascx" TagName="NotificationView" TagPrefix="DRMS" %>
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
                名称
            </th>
            <th> 
                父资源名称
            </th>
            <th>
                创建时间
            </th>
            <th>
                操作
            </th>
        </tr>
    </thead>
    <tfoot>
        <tr>
            <td colspan="6">
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
                        <input type="checkbox" value='<%#RemoveRed(Eval("SYS_FLD_DOI")) %>' />
                    </td>
                    <td class="record-no"></td>

                    <td><%#ReplaceRed(Eval("NAME"))%></td>
                    <td>
                        <%#ReplaceRed(Eval("PARENTID"))%>
                    </td>
                    <td>
                        <%#ReplaceRed(Eval("CREATETIME"))%>
                    </td>
                    <td>
                        <a href="OwnerBookList.aspx?baseid=<%#RemoveRed(Eval("SYS_FLD_DOI")) %>">
                            <img src="/images/icons/document-preview.png" alt="查看书籍" />
                        </a>
                        <a href="ResourceTypeItem.aspx?doi=<%#RemoveRed(Eval("SYS_FLD_DOI")) %>">
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
<asp:HiddenField ID="hid_SqlQueryCondition" runat="server" EnableViewState="true"/>