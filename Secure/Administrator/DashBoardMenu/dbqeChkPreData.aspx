<%@ Page StylesheetTheme="Default" Language="VB" AutoEventWireup="false" CodeFile="dbqeChkPreData.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.CheckPracticeData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>DashBoard - Check Practice Data</title>
    <style type="text/css">
      
        .prTitle
        {
            color : #006AB6; 
            font-family : Verdana; 
            font-weight : bold; 
            font-size : 13px;
	        margin-left : 5px;
            background-color : Yellow;
            height : 28px;
        }
        
        .grid
        {
           font : 10px Verdana, Arial, Sans-Serif;
        }
        
        .grid td, .grid th
        {
            padding : 2px;
        }
        
        .header
        {
           text-align : left;
           color : white;
           background : #006AB6;
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
<body>
    <form id="frmChkPreData" runat="server">
    <div>
 <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="MainTable">
  <tr>
    <td colspan="3">
        <table cellpadding="2" cellspacing="0" border="0" style="background-color :#cecece;" width="100%">
            <tr>
                <td style="height: 85px; vertical-align: middle;">
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
                    <a href="dbqeReport.aspx">Report Queue</a> | 
                    <a href="dbqeReportHistory.aspx">Report Queue History</a> | 
                    <a href="dbqeCurrentProcess.aspx">Refresh</a> | 
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
    <asp:ScriptManager ID="smChkData" runat="server" />
    <table style="width: 600px;" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td style="width: 600px; height: 24px; text-align: left; font-family: Arial; font-size: 16px; color: #006AB6; font-weight: bold;">
                    Check Practice Data</td>
			</tr>
			<tr>
			    <td>
			    <!-- Update panel process -->
                      <asp:UpdateProgress AssociatedUpdatePanelID="UpdpPracticeData" ID="UpdProgPracticeData" runat="server">
                         <ProgressTemplate>
                             <div style="position: absolute; width: 310px; left: 230px; top: 210px; border: solid 2px Red; padding: 4px; background-color: #006AB6; color: White;">
                                 <img src="../../../Secure/Administrator/images/indicator.gif" alt="Update" />&nbsp;&nbsp;Data is retrieving now. Please Wait ...</div>
                             </ProgressTemplate> 
                      </asp:UpdateProgress> 
                 <!-- Update Panel is starting here --> 			    
			    </td>
			</tr>
	<tr>
		<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
        <asp:UpdatePanel ID="UpdpPracticeData" runat="server">
        <ContentTemplate>        
               <table width="100%" cellpadding="0" cellspacing="0" style="border: 0;">
               <tr>
                <td>
                    Select Practice:<br />
                        <asp:DropDownList ID="drpPracticeInfo" runat="server" AutoPostBack="True" Width="254px">
                        </asp:DropDownList>
                        <asp:Label ID="lblPracticeSel" runat="server" Font-Bold="True" ForeColor="Red" Text="Selected"></asp:Label>
                 </td>       
                </tr>
                <tr>
                    <td class="prTitle">&nbsp;Practice Information</td> 
                </tr> 
                <tr> 
                    <td>   
                     <asp:GridView Width="100%" ID="grvChkPre_Info" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                           <SelectedRowStyle CssClass="dd" />
                           <HeaderStyle Height="23px" CssClass="header" />     
                           <RowStyle CssClass="row" />
                           <AlternatingRowStyle CssClass="alternating" />
                     </asp:GridView>
                    </td> 
                </tr> 
                <tr>
                    <td class="prTitle">&nbsp;Practice Process Schedule</td> 
                </tr> 
                <tr>
                  <td>
                    <asp:GridView Width="100%" ID="grvChkPre_Schedule" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                           <SelectedRowStyle CssClass="dd" />
                           <HeaderStyle Height="23px" CssClass="header" />     
                           <RowStyle CssClass="row" />
                           <AlternatingRowStyle CssClass="alternating" />            
                    </asp:GridView>
                  </td>
                </tr>
                <tr>
                    <td class="prTitle">&nbsp;Custom Extraction</td> 
                </tr> 
                <tr>
                   <td>
                    <asp:GridView Width="100%" ID="grvChkPre_Extraction" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                           <SelectedRowStyle CssClass="dd" />
                           <HeaderStyle Height="23px" CssClass="header" />     
                           <RowStyle CssClass="row" />
                           <AlternatingRowStyle CssClass="alternating" />  
                    </asp:GridView>
                  </td>
                </tr>
                <tr>
                    <td class="prTitle">&nbsp;Clients</td> 
                </tr> 
                <tr>
                  <td>
                    <asp:GridView Width="100%" ID="grvChkPre_Clients" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                           <SelectedRowStyle CssClass="dd" />
                           <HeaderStyle Height="23px" CssClass="header" />     
                           <RowStyle CssClass="row" />
                           <AlternatingRowStyle CssClass="alternating" />  
                    </asp:GridView>
                   </td>
                 </tr> 
                 <tr>
                    <td class="prTitle">&nbsp;Patients</td> 
                 </tr> 
                 <tr>
                   <td>
                    <asp:GridView Width="100%" ID="grvChkPre_Patients" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                           <SelectedRowStyle CssClass="dd" />
                           <HeaderStyle Height="23px" CssClass="header" />     
                           <RowStyle CssClass="row" />
                           <AlternatingRowStyle CssClass="alternating" />  
                    </asp:GridView>
                   </td>
                 </tr>
                 <tr>
                    <td class="prTitle">&nbsp;Raw Reminders</td>
                 </tr> 
                 <tr>
                   <td>
                    <asp:GridView Width="100%" ID="grvChkPre_RawReminders" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                           <SelectedRowStyle CssClass="dd" />
                           <HeaderStyle Height="23px" CssClass="header" />     
                           <RowStyle CssClass="row" />
                           <AlternatingRowStyle CssClass="alternating" />  
                    </asp:GridView>
                   </td>
                </tr> 
                <tr>
                    <td class="prTitle">&nbsp;History</td>
                </tr> 
                <tr>
                  <td> 
                    <asp:GridView Width="100%" ID="grvChkPre_History" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                           <SelectedRowStyle CssClass="dd" />
                           <HeaderStyle Height="23px" CssClass="header" />     
                           <RowStyle CssClass="row" />
                           <AlternatingRowStyle CssClass="alternating" />  
                    </asp:GridView>
                  </td>
               </tr>
                <tr>
                    <td class="prTitle">&nbsp;Practice History Date</td> 
                </tr> 
                <tr>
                   <td>
                    <asp:GridView Width="100%" ID="grvChkPre_HistoryDate" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                           <SelectedRowStyle CssClass="dd" />
                           <HeaderStyle Height="23px" CssClass="header" />     
                           <RowStyle CssClass="row" />
                           <AlternatingRowStyle CssClass="alternating" />  
                    </asp:GridView>
                   </td>
                </tr>
                <tr>
                    <td class="prTitle">&nbsp;History by Year / Month</td>
                </tr> 
                <tr>
                   <td>
                    <asp:GridView Width="100%" ID="grvChkPre_HistoryMonth" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                           <SelectedRowStyle CssClass="dd" />
                           <HeaderStyle Height="23px" CssClass="header" />     
                           <RowStyle CssClass="row" />
                           <AlternatingRowStyle CssClass="alternating" />  
                    </asp:GridView>
                   </td> 
                 </tr> 
                </table>               
              </ContentTemplate> 
          </asp:UpdatePanel>       
          <!-- End Update Panel --->  
         
          
          
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
    <td style="color : #FFFFFF; width: 100%; background-color: #006AB6; height: 25px; text-align: center;" colspan="3">
    ©2007-2009, Vet Metrics Inc. All rights reserved.
    </td>
  </tr>
</table>    
</div>
    </form>
    
</body>
</html>
