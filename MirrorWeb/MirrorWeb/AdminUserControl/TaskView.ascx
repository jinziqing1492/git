<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaskView.ascx.cs" Inherits="DRMS.MirrorWeb.AdminUserControl.TaskView" %>
<div class="TYtaskDo MT">
    <h2 class="TYtaskDo-title">
        待办任务<span>（总共<em>9</em>条任务）</span></h2>
    <div class="TaskOne clearfix">
        <ul>
<%--            <li>
                <a href="../auditadmin/ResourceType.aspx">
                    <img src="../images/addrec.png" />
                    <p>
                        添加资源</p>
                </a>
            </li>
            <li>
                <a href="#">
                    <img src="../images/editrec.png" />
                    <p>
                        待完善资源</p>
               </a>
            </li>
            <li>
                <a href="#">
                    <img src="../images/TYpic1.png"/>
                    <p>
                        待审核资源</p>
                    <span class="TaskNum">3</span> 
                </a>
            </li>
            <li>
                <a href="#">
                    <img src="../images/TYpic2.png"/>
                    <p>
                        待加工资源</p>
                    <span class="TaskNum">2</span> 
                </a>
            </li>
            <li>
                <a href="#">
                    <img src="../images/TYpic3.png"/>
                    <p>
                        待审核的资源（加工后）</p>
                    <span class="TaskNum">1</span> 
                </a>
            </li>
            <li>
                <a href="#">
                    <img src="../images/TYpic4.png" />
                    <p>
                        待分配的任务</p>
                    <span class="TaskNum">1</span>
                </a> 
            </li>
            <li>
                <a href="#">
                    <img src="../images/TYpic5.png"/>
                    <p>
                        待二次编辑的任务</p>
                    <span class="TaskNum">20000</span>
                </a> 
            </li>--%>
            <asp:Literal ID="ltlTask" runat="server"></asp:Literal>
        </ul>
        <div class="clear">
        </div>
    </div>
</div>
