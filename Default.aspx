<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Welcome to Veterinary Metrics - Secure Web Site.</title>
</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom: 0; margin-left: 0; margin-top: 0; margin-right: 0;">
    <form id="frmMainMenu" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="AutoNumber1">
  <tr>
    <td style="width: 34%; height: 100px; background-color: #000000;">
        &nbsp;&nbsp;<img alt="Logo" src="images/bwLogo.bmp" /></td>
    <td style="width: 33%; height: 100px; background-color: #000000;">&nbsp;</td>
    <td style="width: 34%; height: 100px; background-color: #000000; text-align: right; vertical-align: bottom; color: #FFFFFF;"><a href="SignOut.aspx">Sign Out</a>&nbsp;&nbsp;</td>
  </tr>
  <tr>
    <td style="width: 100%; height: 25px; background-color: #006AB6; text-align: right; vertical-align: bottom; color: #FFFFFF;" colspan="3">&nbsp;User 
    login:&nbsp;<asp:Label ID="lblUserName" runat="server" Font-Bold="True" Text="No User"></asp:Label></td>
  </tr>
  <tr>
    <td style="width: 100%; height: 25px; background-color: #FFFFFF; text-align: center; vertical-align: top;" colspan="3">
        &nbsp;
        <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse; border-color: #006AB6;" width="69%" id="mainTableSub">
      <tr>
        <td style="width: 100%; height: 25px; text-align: left; vertical-align: middle; background-color: #006AB6; color: #FFFFFF; font-weight: bold;" colspan="3">&nbsp;&nbsp;Main Menu</td>
      </tr>
      <tr>
        <td style="width: 33%; text-align: left; vertical-align: top;">
          <br />
          <p>&nbsp;<b>VMDB-01</b></p>
        <ul>
            <li><a href="Secure/Administrator/AdminMenu/Default.aspx">Admin Menu 3.00</a></li>
            <li><a href="Secure/Administrator/DashBoardMenu/Default.aspx">Dash-Board 3.00</a></li>
            <li><a href="Secure/Administrator/ImportMatrix.aspx">Import Position Matrix 3.00</a></li>
            <li><a href="Secure/Administrator/ImportTargetMX.aspx">Import Target Matrix 3.00</a></li>
            <li><a href="Secure/Administrator/ImportMatrixLog.aspx">View Matrix Log file 1.00</a></li>
            <li><a href="Secure/Administrator/ReminderDrop.aspx">Reminder Drop 3.00</a> </li>
            <li><a href="Secure/Administrator/AddNewReminder.aspx">Add new Reminder Drop</a></li>
            <li><a href="Secure/Administrator/ProcessingMenu/Default.aspx">Processing 1.00</a></li>
            <li><a href="Secure/Administrator/ReportsMenu/Default.aspx">Reports 1.00</a></li>
        </ul>
        </td>
        <td style="width: 33%; text-align: left; vertical-align: top;">
            <br />
          <p>&nbsp;<b>HOMEAGAIN</b></p>
          <ul>
<li><a href="Secure/Administrator/ActivationMail/Default.aspx">Welcome Letter Mail 5.00</a></li>
            <li>Direct Mail</li>
            <li>Pet ID Process</li>
          </ul>
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
            &nbsp;</td>
        <td style="width: 34%; text-align: left; vertical-align: top;">&nbsp;<br />
            &nbsp;<b>PET ID</b>
            <ul>
            <li>ReOrder Pet ID v1.0</li>
            </ul> 
            </td>
      </tr>
    </table>
    &nbsp;
    </td>
  </tr>
  <tr>
    <td style="width: 100%; height: 3px; background-color: #000000;" colspan="3"><img src="images/pixel.gif" alt="" /></td>
  </tr>
  <tr>
    <td colspan="3" style="width: 100%; background-color: #006AB6; height: 25px; text-align: center; color: #FFFFFF; font-size: small;">
    &copy;2007-2009, Vet Metrics Inc. All rights reserved. Last
        Updated: Feb 26, 2009
        02:49 PM</td>
  </tr>
</table>
    
    </div>
    </form>
</body>
</html>
