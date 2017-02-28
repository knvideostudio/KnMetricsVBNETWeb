<%@ Page StylesheetTheme="AdminPart" Language="VB" AutoEventWireup="false" CodeFile="dbPreProcess.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.AdminDropPracticeProcess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>DashBoard - Practice Process Results</title>

</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom : 0px; margin-left : 0px; margin-right : 0px; margin-top : 0px;" >
    <form id="frmPractProcess" runat="server">
    <div>
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
                                <span class="BannerTextCompany">&nbsp;&nbsp;<img id="Logo" alt="Logo" height="51"
                                    src="../../../images/LogoTrp.gif" width="275" /></span></td>
                        </tr>
                        <tr>
                            <td class="Banner" align="RIGHT" />
                        </tr>
                    </table>
                    &nbsp;<a href="../../../Default.aspx">Main Menu</a> | 
                    <a href="../DashBoardMenu/Default.aspx">DashBoard Menu</a> | 
                    <a href="../AdminMenu/Default.aspx">Admin Menu</a> | 
                    <a href="../ProcessingMenu/Default.aspx">Processing Menu</a> | 
                    <a href="../../../SignOut.aspx">Sign Out</a>
                       </td>
            </tr>
        </table>
    </td>
  </tr>
  <tr>
    <td style="width: 34%; background-color: #006AB6; height: 25px; text-align: left;">&nbsp;<font color="#FFFFFF">User 
    login:&nbsp;<asp:Label ID="lblUserName" runat="server" Font-Bold="True" Text="No User"></asp:Label></font></td>
    <td style="width: 66%; background-color: #006AB6; height: 25px; text-align: right;" colspan="2">  
    &nbsp;
    </td>
  </tr>
  <tr>
    <td  colspan="3" style="width: 100%; text-align: center; vertical-align: top;">
    <br />
    <table style="width: 730px;" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td style="width: 730px; height: 24px; text-align: left; font-family: Arial; font-size: 16px; color: #006AB6; font-weight: bold;">
                    DashBoard - Practice Process Results</td>
			</tr>
	<tr>
			<td style="width: 730px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
                <br />
                Enter the Practice ID:<br />
                <asp:TextBox ID="txtPractice" runat="server" MaxLength="10" Width="175px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfPreID" runat="server" ControlToValidate="txtPractice"
                    Display="Dynamic" ErrorMessage="enter practice ID" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
                <asp:Button id="btnShowNow" runat="server" Text="Show Now"></asp:Button><br />
                <br />
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label><br />
                <br />
                <asp:GridView ID="grdPreData" 
                runat="server" 
                CellPadding="4" ForeColor="#333333"
                    GridLines="None"
                    EmptyDataText="No Processes are found ..."
                    Width="100%"
                    >
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="false" />
                    <RowStyle BackColor="#EFF3FB" Wrap="false" />
                    <PagerStyle BackColor="#2461BF" Wrap="false" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Wrap="false" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Wrap="false" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" Wrap="false"  />
                </asp:GridView>

            </td>
	</tr>
	</table>
        &nbsp;
    </td>
  </tr>
  <tr>
    <td style="width: 100%; background-color: #000000; height: 1px;" colspan="3">
    <img src="../../../images/pixel.gif" alt="" /></td>
  </tr>
  <tr>
    <td style="width: 100%; background-color: #006AB6; height: 25px; text-align: center; color: #FFFFFF;" colspan="3">
    ©2007-2009, Vet Metrics Inc. All rights reserved.
    </td>
  </tr>
</table>    
</div>
    </form>
    
</body>
</html>
