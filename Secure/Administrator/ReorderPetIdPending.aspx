<%@ Page StylesheetTheme="DefaultPages" Language="VB" AutoEventWireup="false" CodeFile="ReorderPetIdPending.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.ReorderPetIdPending" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Re-Order PetIds - Pending.</title>
</head>
<body>
    <form id="frmReorderPetIdPending" runat="server">
    <div>
     <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                width="100%" >
                <tr>
                    <td width="34%" height="100" bgcolor="#000000">
                        &nbsp;&nbsp;<img src="../../images/bwLogo.bmp" /></td>
                    <td width="33%" height="100" bgcolor="#000000">
                        &nbsp;</td>
                <td width="33%" height="100" bgcolor="#000000" align="right" valign="bottom"><a href="../../SignOut.aspx"><font color="#FFFFFF">Sign Out</font></a>&nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" bgcolor="#000000" height="25" align="left" valign="middle">
                        &nbsp;<a href="../../Default.aspx"><font color="#FFFFFF">Main Menu</font></a> <font color="#FFFFFF">|</font> 
                        <a href="ReOrderPetId.aspx"><font color="#FFFFFF">Re-Order PetId</font></a>
                    </td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" bgcolor="#FFFFFF" height="2">
                        <img src="../../images/pixel.gif" /></td>
                </tr>
                <tr>
                    <td width="34%" bgcolor="#006AB6" height="25" align="left">
                        &nbsp;<font color="#FFFFFF">User login:&nbsp;<asp:Label ID="lblUserName" runat="server"
                            Font-Bold="True" Text="No User"></asp:Label></font></td>
                    <td width="66%" colspan="2" bgcolor="#006AB6" height="25" align="right">
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" align="center" valign="top" style="height: 16px">
                        <br />
                        <table style="width: 600px;" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="width: 600px; height: 25px; text-align: left; background-color: #6baaf5;" colspan="3">
                                    <b>&nbsp;Pending PetIds:</b>&nbsp;
                                    <asp:Label ID="lblPetIdTotal" runat="server" EnableViewState="false"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 600px; text-align: left;"  colspan="3">
                                    <asp:GridView ID="grvPending" GridLines="Both" EmptyDataText="No Records ..." BorderColor="black" CellSpacing="1" BorderWidth="1px" runat="server">
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" bgcolor="#000000" height="3">
                        <img src="../../images/pixel.gif" /></td>
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
