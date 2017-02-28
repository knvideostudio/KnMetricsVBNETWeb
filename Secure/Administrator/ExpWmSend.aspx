<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExpWmSend.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.ExpWmSend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Welcome letter - Records already send to the printer.</title>
        <style type="text/css">
        a 
        {
        }
                
        .grid
        {
           font : 10px Verdana, Arial, Sans-Serif;
           
        }
            
        .grid td, .grid th
        {
            padding :  2px;
        }
                
        .header
        {
           text-align : left;
           color : white;
           background : #B3B3FF;
        }
            
        .row td
        {
           border-bottom : solid 1px blue;
        }
            
        .alternating
        {
           background-color : #eeeeee;
        }

        .alternating td
        {
            border-bottom : solid 1px blue;
        }
    </style> 
</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom : 0px; margin-left : 0px; margin-right : 0px; margin-top : 0px;" >
    <form id="frmWelLetterSend" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="AutoNumber1">
  <tr> 
    <td width="34%" height="100" bgcolor="#000000">
        &nbsp;&nbsp;<img src="../../images/bwLogo.bmp" /></td>
    <td width="33%" height="100" bgcolor="#000000">&nbsp;</td>
    <td width="33%" height="100" bgcolor="#000000" align="right" valign="bottom"><a href="../../SignOut.aspx"><font color="#FFFFFF">Sign Out</font></a>&nbsp;&nbsp;</td>
  </tr>
      <tr>
    <td width="100%" colspan="3" bgcolor="#000000" height="25" align=left valign=middle >&nbsp;<a href="../../Default.aspx"><font color="#FFFFFF">Main Menu</font></a> <a href="ReminderDrop.aspx"><font color="#FFFFFF">Remainder Drop</font></a> <a href="AddNewReminder.aspx"><font color="#FFFFFF">Add New Remainder Drop</font></a>&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" colspan="3" bgcolor="#FFFFFF" height="2"><img src="../../images/pixel.gif" /></td>
  </tr>
  <tr>
    <td width="34%" bgcolor="#006AB6" height="25" align="left">&nbsp;<font color="#FFFFFF">User 
    login:&nbsp;<asp:Label ID="lblUserName" runat="server" Font-Bold="True" Text="No User"></asp:Label></font></td>
    <td width="66%" colspan="2" bgcolor="#006AB6" height="25" align="right"><a href="ExpWelcomeMail.aspx"><font color="#FFFFFF">Letter Main Page</font></a>    
    &nbsp;
    </td>
    
  </tr>
  <tr>
    <td width="100%" colspan="3" align="center" valign="top" style="height: 16px">
    <br />
    <table style="width: 1800px;" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td style="width: 1800px; height: 24px; text-align: left;">&nbsp;Welcome Mail - Send to printer</td>
			</tr>
			<tr>
				<td style="width: 1800px; text-align: left;">
				<asp:GridView ID="grvTotalRecords" GridLines="Vertical" BorderWidth="1px" CssClass="grid" 
                HeaderStyle-CssClass="header" 
                RowStyle-CssClass="row" 
                RowStyle-Wrap="true" 
                AlternatingRowStyle-CssClass="alternating" 
				runat="server" />
                </td>
				
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
       &nbsp;
    </td>
  </tr>
  <tr>
    <td width="100%" colspan="3" bgcolor="#000000" height="3"><img src="../../images/pixel.gif" /></td>
  </tr>
  <tr>
    <td width="100%" colspan="3" bgcolor="#006AB6" align="center" height="25"><font color="#FFFFFF" size="1">&copy;2007-2009, Vet Metrics Inc. All rights reserved.
    </font></td>
  </tr>
</table>    
    </div>
    </form>
</body>
</html>
