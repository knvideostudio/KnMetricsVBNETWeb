<%@ Page StylesheetTheme="Default" Language="VB" AutoEventWireup="false" CodeFile="DefaultLogin.aspx.vb" Inherits="DefaultLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Secure Login at Veterinary Metrics, Inc.</title>
</head>
<body>
    <form id="frmLogin" runat="server">
    <div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Login ID="loginTemplate" BorderColor="DarkBlue" BorderWidth="1px" runat="server">
            <LayoutTemplate>
                <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="height: 202px">
                            <table border="0" cellpadding="0">
                                <tr>
                                    <td align="center" colspan="2" style="height: 75px">
                                    <img src="images/vetmetlogo.gif" alt="Veterinary Metrics" /></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="UserName" runat="server" Width="136px" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" Font-Names="Verdana" Font-Size="11px" Width="136px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" style="color: red">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Button ID="btnLogin" runat="server" CommandName="Login" Text="Log In" Width="100px" Height="25px" ValidationGroup="Login1" OnClick="LoginButton_Click" BackColor="Desktop" BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:Login>
        <asp:Label ID="lblTextMessage" runat="server"></asp:Label>
        <br />
        Veterinary Metrics - Secure Web Site<br />
        Release Version: 2.0.1.7<br />
        Date: Dec 31, 2008</div>
    </form>
</body>
</html>
