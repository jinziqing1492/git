<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucChangePwd.ascx.cs" Inherits="DRMS.MirrorWeb.UserControl.ucChangePwd" %>

<div > 
       <table cellpadding="30" cellspacing="0"  style="width: 620px;height:240px;">
          <tr>
              <td>
                 <table cellpadding="4" class="TYTable_UserPwd" cellspacing="0" width="100%">
                     <tbody>
                         <tr>
                             <td class="TYTabletd_UserPwd" >
                                <asp:Label ID="CurrentPasswordLabel" runat="server"  Height="24px"
                                           AssociatedControlID="CurrentPassword">原密码：</asp:Label>
                             </td>                                   
                             <td style="color:Red;">
                                 <asp:TextBox ID="CurrentPassword" Height="24px" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" Height="24px"
                                        ControlToValidate="CurrentPassword" ErrorMessage="必须填写“原密码”！" 
                                        ToolTip="必须填写“原密码”！" ValidationGroup="ctl00$ChangePassword1">必须填写“原密码”！</asp:RequiredFieldValidator>
                             </td>
                         </tr>                                   
                         <tr>
                             <td class="TYTabletd_UserPwd">
                                <asp:Label ID="NewPasswordLabel" runat="server"  Height="24px"
                                           AssociatedControlID="NewPassword">新密码：</asp:Label>
                             </td>
                             <td style="color:Red;">
                                 <asp:TextBox ID="NewPassword" Height="24px" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" Height="24px"
                                        ControlToValidate="NewPassword" ErrorMessage="必须填写“新密码”！" ToolTip="必须填写“新密码”！" 
                                        ValidationGroup="ctl00$ChangePassword1">必须填写“新密码”！</asp:RequiredFieldValidator>
                             </td>
                         </tr>                                   
                         <tr>
                             <td class="TYTabletd_UserPwd">
                                 <asp:Label ID="ConfirmNewPasswordLabel" runat="server"  Height="24px"
                                           AssociatedControlID="ConfirmNewPassword">确认新密码：</asp:Label>
                             </td>
                             <td style="color:Red;">
                                 <asp:TextBox ID="ConfirmNewPassword" Height="24px" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" Height="24px"
                                        ControlToValidate="ConfirmNewPassword" ErrorMessage="必须填写“确认新密码”！" 
                                        ToolTip="必须填写“确认新密码”！" ValidationGroup="ctl00$ChangePassword1">必须填写“确认新密码”</asp:RequiredFieldValidator>
                             </td>
                         </tr>
                         <tr>
                             <td align="center" colspan="2" style="color:Red;">
                                 <asp:CompareValidator ID="NewPasswordCompare" runat="server" 
                                        ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                                        Display="Dynamic" ErrorMessage="“确认新密码”与“新密码”不一致！" 
                                        ValidationGroup="ctl00$ChangePassword1"></asp:CompareValidator>
                             </td>
                         </tr>                                   
                         <tr>
                             <td colspan="2">
                                 <div class="TYContainer_Btn input">
                                    <asp:Button ID="ChangePasswordPushButton" runat="server" OnClick="ChangePasswordPushButton_Click"  
                                        CommandName="ChangePassword" Text="更改密码" 
                                        ValidationGroup="ctl00$ChangePassword1" />
                                    <asp:Button ID="CancelPushButton"  runat="server" CausesValidation="False" OnClick="CancelPushButton_Click"
                                        CommandName="Cancel" Text="重  置" />
                                    </div>                                                                    
                             </td>
                         </tr>                                   
                     </tbody>                                   
                 </table>                              
                                                    
              </td>                                  
                                                
          </tr>                              
       </table>                              
</div>                                        