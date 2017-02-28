<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExpWelcomeMail.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.ExpWelcomeMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Export Welcome Mail</title>
    <script language="javascript" type="text/jscript" src="include/MyCalendar.js"></script> 
    <script type="text/javascript">
    function ShowProgress()
    {
        var progress = document.getElementById("ProgressBar");
        progress.style.display = "";
    }
    function StopProgress()
    {
        var progress = document.getElementById("ProgressBar");
        progress.style.display = "none";
    }
    function UpdatePage(results, context)
    {
        StopProgress();
        var label = document.getElementById("lblStep3");
        label.innerHTML = results;
        
        var button1 = document.getElementById("btnBuildOutput");
        button1.disabled = false;
    }
    
        // Clear
    // var results = document.getElementById("Results");
    // results.innerHTML = "";
function DisableButtons()
{
    var button1 = document.getElementById("btnBuildOutput");
    
    if (button1 != null)
    {
		button1.disabled = true;
    }
    //var textbox1 = document.getElementById("TextBox1");
   // textbox1.disabled = true;
    
    var progress = document.getElementById("ProgressBar");
    progress.style.display = "";
    return false;
}
    
    </script>
</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom : 0px; margin-left : 0px; margin-right : 0px; margin-top : 0px;" >
    <form id="form1" runat="server" onsubmit="DisableButtons()" submitdisabledcontrols="true">
    <div>
 <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="AutoNumber1">
  <tr>
    <td width="34%" height="100" bgcolor="#000000">
        &nbsp;&nbsp;<img src="../../images/bwLogo.bmp" /></td>
    <td width="33%" height="100" bgcolor="#000000">&nbsp;</td>
    <td width="33%" height="100" bgcolor="#000000" align="right" valign="bottom"><a href="../../SignOut.aspx"><font color="#FFFFFF">Sign Out</font></a>&nbsp;&nbsp;</td>
  </tr>
      <tr>
    <td width="100%" colspan="3" bgcolor="#000000" height="25" align=left valign=middle >&nbsp;
    <a href="../../Default.aspx"><font color="#FFFFFF">Main Menu</font></a> <a href="ReminderDrop.aspx"><font color="#FFFFFF">Reminder Drop</font></a> <a href="AddNewReminder.aspx"><font color="#FFFFFF">Add New Reminder Drop</font></a>&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" colspan="3" bgcolor="#FFFFFF" height="2"><img src="../../images/pixel.gif" /></td>
  </tr>
  <tr>
    <td width="34%" bgcolor="#006AB6" height="25" align="left">&nbsp;<font color="#FFFFFF">User 
    login:&nbsp;<asp:Label ID="lblUserName" runat="server" Font-Bold="True" Text="No User"></asp:Label></font></td>
    <td width="66%" colspan="2" bgcolor="#006AB6" height="25" align="right"><a href="ExpWmSend.aspx"><font color="#FFFFFF">Already Send</font></a>  
    &nbsp;
    </td>
    
  </tr>
  <tr>
    <td width="100%" colspan="3" align="center" valign="top" style="height: 16px">
    <br />
    <table style="width: 600px;" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td style="width: 600px; height: 24px; text-align: left; font-family: Arial; font-size: 16px; color: #006AB6; font-weight: bold;" colspan="2">
                    Import - Exporting Welcome Mail, <asp:Label ID="lblCurrentDate" Font-Names="Arial" Font-Size="10px" runat="server" EnableViewState="False"></asp:Label><br />
                    <asp:Label ID="lblMessageDisplay" Font-Names="Verdana" Font-Bold="false" Font-Size="12px" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left;">&nbsp;</td>
				<td style="width: 500px; text-align: left;">
                    <asp:Label ID="lblMessage" runat="server" Font-Names="Arial" Font-Size="12px"></asp:Label><br />
                    <asp:Button ID="btnImportFile" runat="server" Text="Import File(s)" Width="138px" /></td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left; height: 16px;">&nbsp;<asp:Label ID="lblStatus" runat="server"></asp:Label></td>
				<td style="width: 500px; text-align: left; height: 16px;">
                    <asp:Button ID="btnProcessMaster" runat="server" Text="Process Master" Enabled="False" Visible="False" />
                    <asp:Button ID="btnBuildOutput" Enabled="false" UseSubmitBehavior="false" runat="server" Text="Build Output File" Visible="False" /><br />
                    <div id="ProgressBar" style="display:none; font-weight: bold; font-size: 12px; color: navy; font-family: Verdana; background-color: #ffff99;">
                    <img alt="" src="images/indicator.gif"  />
                    <span id="Msg">Please, wait ... </span>
                    </div>
                    </td>
			</tr>			
			<tr>
				<td style="width: 100px; text-align: left; height: 24px;">&nbsp;</td>
				<td style="width: 500px; text-align: left; height: 24px;">
                    <asp:Label ID="lblFiles" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left; vertical-align: top; height: 24px; background-color: #FFFFC1;">Print Date
                    <asp:RequiredFieldValidator ID="rfvPrintDate" runat="server" ControlToValidate="txtPrintDate"
                        Display="Dynamic" ErrorMessage="* " SetFocusOnError="True" ValidationGroup="PrintDate"></asp:RequiredFieldValidator></td>
				<td style="width: 500px; text-align: left; height: 24px; background-color: #FFFFC1;">
                    <asp:TextBox ID="txtPrintDate" runat="server" Width="146px" ValidationGroup="PrintDate"></asp:TextBox>
                    <a href="javascript:OpenCalendar('txtPrintDate')"><img src="images/sCalendar.gif" border="0" alt="Pick Up Calendar" /></a>&nbsp;
                    Batch Id:<asp:DropDownList ID="drpBoxBatchID" runat="server" ValidationGroup="PrintDate" Width="185px">
                    </asp:DropDownList><br />
                    <asp:Button
                            ID="btnPrintDate" BackColor="#FFFFC1" runat="server" Height="22px" Text="Set Print Date" Width="117px" ValidationGroup="PrintDate" /><br />
                    &nbsp;</td>
			</tr>
			<tr>
				<td style="width: 100px; vertical-align: top; text-align: left; height: 24px; background-color: #C0FFFF;">
                    Bad Address<asp:RequiredFieldValidator ID="rfvMicrochip" runat="server"
                        ControlToValidate="txtMicroChipId" Display="Dynamic" ErrorMessage="* " SetFocusOnError="True"
                        ValidationGroup="MicroChip"></asp:RequiredFieldValidator></td>
				<td style="width: 500px; text-align: left; height: 24px; background-color: #C0FFFF;">
                    <asp:TextBox ID="txtMicroChipId" runat="server" ValidationGroup="MicroChip"></asp:TextBox>
                    Batch Id:
                    <asp:DropDownList ID="drpBadAddress" runat="server" ValidationGroup="MicroChip" Width="185px">
                    </asp:DropDownList><br /><asp:Button
                            ID="btnMicroChip" BackColor="#C0FFFF" runat="server" Height="22px" Text="Update" Width="91px" ValidationGroup="MicroChip" /><br />
                    &nbsp;</td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left; height: 24px;">&nbsp;</td>
				<td style="width: 500px; text-align: left; height: 24px;">
                    </td>
			</tr>			
			<tr>
				<td style="width: 100px; text-align: left; height: 24px;">&nbsp;</td>
				<td style="width: 500px; text-align: left; height: 24px;">
                    </td>
			</tr>
		</table>

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
