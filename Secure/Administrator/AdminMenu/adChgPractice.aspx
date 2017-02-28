<%@ Page StylesheetTheme="AdminPart" Language="VB" AutoEventWireup="false" CodeFile="adChgPractice.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.AdminChangePractice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Admin Menu - Change Practice ID.</title>
<script language="javascript" type="text/javascript">
function DisableButtons()
{
        var button1 = document.getElementById("btnStartNow");
        
        if (button1 != null)
        {
		    button1.disabled = true;
        }
}

function ConfirmExec()
{
    var confirmed = window.confirm("Are you sure?");
    return (confirmed);
}

</script> 
</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom : 0px; margin-left : 0px; margin-right : 0px; margin-top : 0px;" >
    <form id="frmChgPrc" 
    runat="server" 
    defaultbutton="btnStartNow"
    > 
    <div>
    <asp:ScriptManager ID="smChgPrt" runat="server" />
 <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="MainTable">
  <tr>
    <td colspan="3">
        <table cellpadding="2" cellspacing="0" border="0" style="background-color: #cecece;" width="100%">
            <tr>
                <td style="height: 81px; vertical-align: middle;">
                    <table style="background-color: #1c5280;" width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td class="Banner" />
                        </tr>
                        <tr>
                            <td class="Banner">
                                <span class="BannerTextCompany">&nbsp;&nbsp;<img src="../../../images/LogoTrp.gif" width="275" height="51" id="Logo" alt="Logo" /></span></td>
                        </tr>
                        <tr>
                            <td class="Banner" align="RIGHT" style="height: 8px" />
                        </tr>
                    </table>
                    &nbsp;<a href="../../../Default.aspx">Main Menu</a> | 
                    <a href="../AdminMenu/Default.aspx">Admin Menu</a> | 
                    <a href="../DashBoardMenu/Default.aspx">DashBoard Menu</a> | 
                    <a href="../../../SignOut.aspx">Sign Out</a>
                       </td>
            </tr>
        </table>
    </td>
  </tr>
  <tr>
    <td style="width: 34%; background-color: #006AB6; height: 25px; text-align: left; color: #FFFFFF;">&nbsp;User 
    login:&nbsp;<asp:Label ID="lblUserName" runat="server" Font-Bold="True" Text="No User"></asp:Label></td>
    <td style="width: 66%; background-color: #006AB6; height: 25px; text-align: right;" colspan="2">  
    &nbsp;
    </td>
  </tr>
  <tr>
    <td  colspan="3" style="width: 100%; text-align: center; vertical-align: top;">
    <br />
    <table style="width: 600px;" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td style="width: 600px; height: 24px; text-align: left; font-family: Arial; font-size: 16px; color: #006AB6; font-weight: bold;">
                    Admin Menu - Change Practice ID</td>
			</tr>
	<tr>
			<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
                &nbsp;<br />
               <asp:UpdateProgress AssociatedUpdatePanelID="myUpdatePanel" ID="myUpdProgress" runat="server">
                    <ProgressTemplate>
                    <div style="position: absolute; width: 350px; left: 255px; top: 280px; border: solid 2px Red; padding: 4px; background-color: #507CD1; color: White;">
                         <img src="../../../Secure/Administrator/images/indicator.gif" alt="Update" />
                                    The converting process is starting 6-10 minutes&nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Please Wait ...
                                    </div>
                    </ProgressTemplate> 
                </asp:UpdateProgress> 
                Select the practice name and it is number will be change to new format:<br />
                <asp:Label ID="lblMessageProcess" runat="server" EnableViewState="False" Font-Bold="True"
                    Font-Size="16px" ForeColor="Blue"></asp:Label><br />
         <asp:UpdatePanel ID="myUpdatePanel" runat="server">
             <ContentTemplate>
                <asp:Label ID="lblPrcMessage" runat="server" ForeColor="Red"></asp:Label><br />
                <asp:DropDownList ID="drpPracticeDpl" runat="server" Width="303px" AutoPostBack="True">
                </asp:DropDownList><br />
                <asp:Button ID="btnStartNow" runat="server" Text="Start Now" />&nbsp;<br />
                &nbsp;<br />
                    <asp:GridView 
                    ID="grdPreData" 
                    runat="server" 
                    CellPadding="4" 
                    ForeColor="#333333" 
                    GridLines="None" 
                    AutoGenerateColumns="False"
                    AllowSorting="True" 
                    >
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Verdana" Font-Size="10px" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="OldPracticeId" 
                            HeaderText="Old Practice ID"
                            SortExpression="OldPracticeId">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField SortExpression="NewPracticeId" DataField="NewPracticeId" HeaderText="New Practice ID">
                                <HeaderStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField SortExpression="NewFtpId" DataField="NewFtpId" HeaderText="New FTP ID">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField SortExpression="PracticeName" DataField="PracticeName" HeaderText="Practice Name">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle HorizontalAlign="Left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField SortExpression="LogUserName" DataField="LogUserName" HeaderText="User Name">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="isSuccessFul" HeaderText="Success">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField SortExpression="RegisterDate" DataField="RegisterDate" DataFormatString="{0:d}" HeaderText="Converted Date">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                &nbsp;
                </ContentTemplate> 
         </asp:UpdatePanel> 
                
            </td>
	</tr>

	</table>
        &nbsp;
    </td>
  </tr>
  <tr>
    <td style="width: 100%; background-color: #000000; height: 1px;" colspan="3"><img src="../../../images/pixel.gif" alt="" /></td>
  </tr>
  <tr>
    <td style="width: 100%; background-color: #006AB6; height: 27px; text-align: center; color: #FFFFFF; font-size: small;" colspan="3">
    &copy;2007-2009, Vet Metrics Inc. All rights reserved.
    </td>
  </tr>
</table>    
</div>
    </form>
    
</body>
</html>
