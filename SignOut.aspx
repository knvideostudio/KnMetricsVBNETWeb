<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SignOut.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.SignOut" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sign Out</title>
</head>
<body style="font-family: Verdana; font-size: 10pt;" bottommargin="0" leftmargin="0"
    topmargin="0" rightmargin="0">
    <form id="frmSignOut" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                width="100%" id="AutoNumber1">
                <tr>
                    <td width="34%" height="100" bgcolor="#000000">
                        &nbsp;&nbsp;<img src="images/bwLogo.bmp" /></td>
                    <td width="33%" height="100" bgcolor="#000000">
                        &nbsp;</td>
                    <td width="33%" height="100" bgcolor="#000000">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" bgcolor="#000000" height="25" align="left" valign="middle">
                        &nbsp;<font color="#FFFFFF">Main Menu | Remainder Drop | Add New Remainder Drop</font>&nbsp;</td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" bgcolor="#FFFFFF" height="2">
                        <img src="images/pixel.gif" /></td>
                </tr>
                <tr>
                    <td width="34%" bgcolor="#006AB6" height="25" align="left">
                        &nbsp;<font color="#FFFFFF">User login:&nbsp;<asp:Label ID="lblUserName" runat="server"
                            Font-Bold="True" Text="No User"></asp:Label></font></td>
                    <td width="66%" colspan="2" bgcolor="#006AB6" height="25" align="right">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" align="center" valign="top" style="height: 16px">
                        <br />
                        <table style="width: 600px;" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="width: 600px; height: 25px; text-align: left;" colspan="2">
                                    &nbsp;Sign Out</td>
                            </tr>
                            <tr>
                                <td style="width: 200px; text-align: left; height: 24px;">
                                    &nbsp;</td>
                                <td style="width: 400px; text-align: left; height: 24px;">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 200px; text-align: left; height: 24px;">
                                    &nbsp;</td>
                                <td style="width: 400px; text-align: left; height: 24px;">
                                    You have been successfuly sign out.<br />
                                    <a href="DefaultLogin.aspx">Click here to login</a>.</td>
                            </tr>
                            <tr>
                                <td style="width: 200px; text-align: left; height: 24px;">
                                    &nbsp;</td>
                                <td style="width: 400px; text-align: left; height: 24px;">
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" bgcolor="#000000" height="3">
                        <img src="images/pixel.gif" /></td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" bgcolor="#006AB6" align="center" height="25">
                        <font color="#FFFFFF" size="1">&copy;2007-2009, Vet Metrics Inc. All rights reserved. </font>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
