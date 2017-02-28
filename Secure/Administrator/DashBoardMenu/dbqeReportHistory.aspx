<%@ Page StylesheetTheme="Default" Language="VB" AutoEventWireup="false" CodeFile="dbqeReportHistory.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.ReportHistoryQueue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>DashBoard - Report History Queue.</title>
</head>
<body>
<form id="frmRptHistory" runat="server">
<div>
 <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="MainTable">
  <tr>
    <td colspan="3">
        <table cellpadding="2" cellspacing="0" border="0" style="background-color: #cecece;"  width="100%">
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
                            <td class="Banner" align="right" />
                        </tr>
                    </table>
                    &nbsp;<a href="../../../Default.aspx">Main Menu</a> | 
                    &nbsp;<a href="../../../Default.aspx">Main Menu</a> | 
                    <a href="../DashBoardMenu/Default.aspx">DashBoard Menu</a> | 
                    <a href="../AdminMenu/Default.aspx">Admin Menu</a> | 
                    <a href="../ProcessingMenu/Default.aspx">Processing Menu</a> | 
                    <a href="dbqeCurrentProcess.aspx">Current Process</a> | 
                    <a href="dbqeReport.aspx">Report Queue</a> | 
                    <a href="dbqeReportHistory.aspx">Refresh</a> | 
                    <a href="../../../SignOut.aspx">Sign Out</a>
                       </td>
            </tr>
        </table>
    </td>
  </tr>
  <tr>
    <td style="width: 34%; background-color: #006AB6; height: 25px; text-align: left; color: #FFFFFF;">&nbsp;User 
    login:&nbsp;<asp:Label ID="lblUserName" runat="server" Font-Bold="True" Text="No User"></asp:Label>
    </td>
    <td style="width: 66%; background-color: #006AB6; height: 25px; text-align: right;" colspan="2">  
    &nbsp;
    </td>
  </tr>
  <tr>
    <td  colspan="3" style="width: 100%; text-align: center; vertical-align: top;">
    <br />
    <table style="width: 700px;" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td style="width: 700px; height: 24px; text-align: left; font-family: Arial; font-size: 16px; color: #006AB6; font-weight: bold;">
                    Report History Queue</td>
			</tr>
	<tr>
			<td style="width: 700px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
             <asp:GridView 
             Width="100%" 
             ID="grvRepHistory" 
             runat="server" 
             BackColor="White" 
             BorderColor="#999999" 
             BorderStyle="None" 
             BorderWidth="1px" 
             CellPadding="3" 
             GridLines="Vertical" 
             EmptyDataText="No records ..."
             AutoGenerateColumns="False" 
             >
                 <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                 <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                 <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                 <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                 <HeaderStyle Height="25px" BackColor="#000084" Font-Bold="True" ForeColor="White" />
                 <AlternatingRowStyle BackColor="Gainsboro" />
                 <Columns>
                    <asp:BoundField DataField="UserID" HeaderText="User">
                         <ItemStyle Wrap="True" Width="35px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="JobDesc" HeaderText="Job Description">
                         <ItemStyle Wrap="True" Width="230px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="DurationinMinutes" HeaderText="Minutes">
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="StartTime" DataFormatString="{0:MM/dd/yyyy hh:mm:ss tt}" HeaderText="Start Time">
                         <ItemStyle Wrap="False" VerticalAlign="Top" HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="EndTime" DataFormatString="{0:MM/dd/yyyy hh:mm:ss tt}" HeaderText="End Time">
                         <ItemStyle Wrap="False" VerticalAlign="Top" HorizontalAlign="Left" />
                     </asp:BoundField>
                     <asp:BoundField DataField="Status" HeaderText="Status">
                        <ItemStyle Wrap="False" VerticalAlign="Top" HorizontalAlign="Center" />
                     </asp:BoundField>
                     <asp:BoundField DataField="CreateDate" DataFormatString="{0:MM/dd/yyyy hh:mm:ss tt}" HeaderText="Create Date">
                         <ItemStyle Wrap="False" VerticalAlign="Top" HorizontalAlign="Left" />
                     </asp:BoundField>
                 </Columns>
             </asp:GridView>
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
    <td style="color: #FFFFFF; width: 100%; background-color: #006AB6; height: 25px; text-align: center;" colspan="3">
    ©2007-2009, Vet Metrics Inc. All rights reserved.
    </td>
  </tr>
</table>    
</div>
    </form>
    
</body>
</html>
