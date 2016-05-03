<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookListView.ascx.cs" Inherits="DRMS.MirrorWeb.AdminUserControl.BookListView" %>
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
                图书名称
            </th>
            <th> 
                作者
            </th>
            <th>
                ISBN
            </th>
            <th>
                出版单位
            </th>
            <th>
                工作时间阅读
            </th>
        </tr>
    </thead>
    <tfoot>
        <tr>
            <td colspan="8">
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
                        <%#ReplaceRed(Eval("Author"))%>
                    </td>
                    <td>
                        <%#ReplaceRed(Eval("ISBN"))%>
                    </td>
                    <td>
                        <%#ReplaceRed(Eval("IssueDep"))%>
                    </td>
                    <td>
                        <%#GetOpeateLink(Eval("ReadType")) %>
                        <asp:Button ID="openRead" runat="server" Text="禁止" CommandName="openRead" CommandArgument='<%#RemoveRed(Eval("SYS_FLD_DOI")) %>' CssClass='<%#Eval("ReadType").ToString()=="2"?"hide":"" %>' />
                        <asp:Button ID="closeRead" runat="server" Text="允许" CommandName="closeRead" CommandArgument='<%#RemoveRed(Eval("SYS_FLD_DOI")) %>' CssClass='<%#Eval("ReadType").ToString()=="2"?"":"hide" %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
<asp:HiddenField ID="hid_SqlQueryCondition" runat="server" EnableViewState="true"/>