<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportMatrix.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.ImportMatrix" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Import Position Metrix.</title>
</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom: 0; margin-left: 0; margin-right: 0; margin-top: 0;">
<form id="frmPositionMx" runat="server">
 <div>
 <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="MainTable">
  <tr>
    <td style="width: 34%; height: 100px; background-color: #000000;">
        &nbsp;&nbsp;<img src="../../images/bwLogo.bmp" alt="Logo" /></td>
    <td style="width: 32%; height: 100px; background-color: #000000;">&nbsp;</td>
    <td style="width: 33%; height: 100px; background-color: #000000; text-align: right; vertical-align: bottom;"><a href="../../SignOut.aspx"><font color="#FFFFFF">Sign Out</font></a>&nbsp;&nbsp;</td>
  </tr>
      <tr>
    <td colspan="3" style="width: 100%; height: 25; background-color: #000000; text-align: left; vertical-align: middle;">&nbsp;
        <a href="../../Default.aspx"><font color="#FFFFFF">Main Menu</font></a> <a href="ReminderDrop.aspx"><font color="#FFFFFF">Reminder Drop</font></a> &nbsp;
        <a href="ImportTargetMX.aspx"><font color="#FFFFFF"> Target Matrix</font></a>&nbsp;
        <a href="ImportMatrixLog.aspx"><font color="#FFFFFF">Matrix Log file</font></a>&nbsp;
    </td>
  </tr>
  <tr>
    <td colspan="3" style="width: 100%; height: 2px; background-color: #FFFFFF;"><img alt="" src="../../images/pixel.gif" /></td>
  </tr>
  <tr>
    <td style="width: 34%; height: 25px; background-color: #006AB6; text-align: left;">&nbsp;<font color="#FFFFFF">User 
    login:&nbsp;<asp:Label ID="lblUserName" runat="server" Font-Bold="True" Text="No User"></asp:Label></font></td>
    <td colspan="2" style="width: 66%; height: 25px; background-color: #006AB6; text-align: right;">
    &nbsp;
    </td>
    
  </tr>
  <tr>
    <td style="width: 100%; text-align: center; vertical-align: top;" colspan="3">
    <asp:ScriptManager ID="smImportMatrix" runat="server" />
    <asp:UpdateProgress AssociatedUpdatePanelID="myUpdatePanel" ID="myUpdProgress" runat="server">
    <ProgressTemplate>
    <div style="position: absolute; width: 350px; left: 200px; top: 150px; border: solid 2px blue; padding: 4px; background-color: Yellow;">
         <img src="../../Secure/Administrator/images/indicator.gif" alt="Update" />
                    Matrix is Importing now please wait ...
                    </div>
    </ProgressTemplate> 
    </asp:UpdateProgress> 
    <asp:UpdatePanel ID="myUpdatePanel" runat="server">
    <ContentTemplate>
    <table style="width: 600px;" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td style="width: 600px; height: 25px; text-align: left;" colspan="2"><h3>&nbsp;Importing Position Matrix 3.00</h3></td>
			</tr>
			<tr>
				<td style="width: 200px; text-align: left; height: 24px;">&nbsp; &nbsp; Selected File is:</td>
				<td style="width: 400px; text-align: left; height: 24px;">
                    File format: &lt;Drop_Id&gt;_&lt;Year&gt;_&lt;Matrix&gt;_&lt;Type&gt;<br />
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label><br />
                    <asp:Label ID="lblSelection" runat="server"></asp:Label><br />
                    <asp:TextBox ID="txtSelFileName" runat="server" BackColor="LightGray" ReadOnly="True"
                        Width="386px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="width: 200px; text-align: right; height: 24px; vertical-align: top;">
                    Please select a file: &nbsp;</td>
				<td style="width: 400px; text-align: left; height: 24px; background-color: LightBlue;">
				<asp:RadioButtonList ID="rdoListButton" CellPadding="0" CellSpacing="0"
				 BorderWidth="0" 
				 Font-Names="Arial" Font-Size="13px" runat="server" OnSelectedIndexChanged="rdoListButton_SelectedIndexChanged" AutoPostBack="True"></asp:RadioButtonList></td>
			</tr>
			<tr>
				<td style="width: 200px; text-align: left;">&nbsp;</td>
				<td style="width: 400px; text-align: left;">
                    <asp:Button ID="btnMtxPosition" runat="server" Text="Import Now" Enabled="False" OnClick="btnMtxPosition_Click" Width="112px" /></td>
			</tr>
		</table>
		</ContentTemplate> 
        </asp:UpdatePanel> 
		<br />
    </td>
  </tr>
  <tr>
    <td style="width: 100%; background-color: #000000; height: 3px;" colspan="3"><img src="../../images/pixel.gif" alt="" /></td>
  </tr>
  <tr>
    <td style="width: 100%; background-color: #006AB6; height: 25px; text-align: center;" colspan="3"><font color="#FFFFFF" size="1">&copy;2007-2009, Vet Metrics Inc. All rights reserved.
    </font></td>
  </tr>
</table>   
</div>
</form>
</body>
</html>
