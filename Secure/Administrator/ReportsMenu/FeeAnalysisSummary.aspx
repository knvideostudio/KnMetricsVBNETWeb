<%@ Page StylesheetTheme="AdminPart" Language="VB" AutoEventWireup="false" CodeFile="FeeAnalysisSummary.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.FeeAnalysisSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Fee Analysis Summary</title>
</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom : 0px; margin-left : 0px; margin-right : 0px; margin-top : 0px;" >
<form id="frmAnalysis" runat="server" >
<div>
 <ajaxToolkit:ToolkitScriptManager ID="scManager" runat="server" />
 
 <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="MainTable">
  <tr>
    <td colspan="3">
        <table cellpadding="2" cellspacing="0" border="0" style="background-color: #cecece; width: 100%;">
            <tr>
                <td style="height: 81px; vertical-align: middle;">
                    <table style="background-color: #1c5280; width: 100%;" cellpadding="0" cellspacing="0" border="0">
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
				<td colspan="4" style="width: 600px; height: 24px; text-align: left; font-family: Arial; font-size: 16px; color: #006AB6; font-weight: bold;">
                    Fee Analysis Summary</td>
			</tr>
			<tr>
				<td colspan="4" style="width: 600px; height: 25px; text-align: left;">
                    <asp:Label ID="lblMessageShow" runat="server" ForeColor="GradientActiveCaption"></asp:Label></td>
			</tr>			
	        <tr>
			    <td class="sml" style="width: 150px; text-align: right; vertical-align: middle; height: 26px;">
                    Previous start date:&nbsp;</td>
                <td class="sml" style="width: 150px; text-align: left; vertical-align: middle; height: 26px;">
                    <asp:Label ID="lblPrevStartDate" runat="server"></asp:Label></td>
			    <td class="sml" style="width: 150px; text-align: right; vertical-align: middle; height: 26px;">
                    Previous end date:&nbsp;</td>
                <td class="sml" style="width: 150px; text-align: left; vertical-align: middle; height: 26px;">
                    &nbsp;<asp:Label ID="lblPrevEndDate" runat="server"></asp:Label></td>
                    
	        </tr>			
	        <tr>
			    <td style="width: 150px; text-align: right; vertical-align: middle; height: 30px;">
                    Start Date:
                </td>
                <td style="width: 150px; text-align: left; vertical-align: middle; height: 30px;">
                    <asp:TextBox ID="txtStartDate" runat="server" Width="83px" BackColor="#FFFFC0" BorderColor="DodgerBlue" ValidationGroup="Fee" MaxLength="15" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSrtDate" runat="server" ErrorMessage="enter date" ControlToValidate="txtStartDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Fee">*</asp:RequiredFieldValidator>
                    <ajaxToolkit:CalendarExtender ID="cldrStartDate" runat="server" FirstDayOfWeek="Monday" PopupPosition="BottomRight"
                        TargetControlID="txtStartDate">
                    </ajaxToolkit:CalendarExtender>
                    </td>
			    <td style="width: 150px; text-align: right; vertical-align: middle; height: 30px;">
                    End Date:
                </td>
                <td style="width: 150px; text-align: left; vertical-align: middle; height: 30px;">
                    <asp:TextBox CssClass="fld" ID="txtEndDate" runat="server" Width="83px" BackColor="#FFFFC0" BorderColor="DodgerBlue" ValidationGroup="Fee" MaxLength="15" TabIndex="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                        Display="Dynamic" ErrorMessage="enter date" SetFocusOnError="True" ValidationGroup="Fee">*</asp:RequiredFieldValidator>
                     <ajaxToolkit:CalendarExtender ID="cldrEndDate" runat="server" FirstDayOfWeek="Monday" PopupPosition="BottomRight"
                        TargetControlID="txtEndDate">
                    </ajaxToolkit:CalendarExtender>
                    
                    </td>
	        </tr>
	        <tr>
			    <td style="width: 150px; text-align: right; vertical-align: middle; height: 16px;">
                    &nbsp;</td>
                <td style="width: 150px; text-align: right; vertical-align: middle; height: 16px;">
                    <asp:Button ID="btnReBuild" runat="server" BackColor="ActiveCaption" BorderColor="GradientActiveCaption"
                        BorderWidth="1px" CssClass="fld" ForeColor="White" Text="ReBuild" Width="106px" ValidationGroup="Fee" TabIndex="3" Height="23px" /></td>
			    <td style="width: 150px; text-align: center; vertical-align: middle; height: 16px;">
                    &nbsp;<asp:Button ID="btnGenExcel" runat="server" Text="Generate Excel" Width="111px" BackColor="ActiveCaption" BorderColor="GradientActiveCaption" BorderWidth="1px" CssClass="fld" ForeColor="White" Height="23px" ValidationGroup="Gen" /></td>
                <td style="width: 150px; text-align: left; vertical-align: middle; height: 16px;">
                    &nbsp;</td>
                    
	        </tr>	
	        <tr>
			    <td style="width: 150px; text-align: right; vertical-align: middle; height: 16px;">
                    Practices:&nbsp;</td>
                <td colspan="3" style="width: 450px; text-align: left; vertical-align: middle; height: 16px;">
                    <asp:DropDownList ID="drpPractice" runat="server" Width="210px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:Label ID="lblPracticeSel" runat="server"></asp:Label></td>
	        </tr>			                
        <tr>
		    <td colspan="4" style="width: 600px; height: 24px; text-align: center; ">
                <asp:GridView ID="grvSummary" 
                EmptyDataText="No Records." 
                runat="server" 
                BackColor="White" 
                BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                CellPadding="3" GridLines="Vertical"
                AutoGenerateColumns="False"
                >
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Names="Arial" Font-Size="11px"  />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle Font-Names="Arial" Font-Size="10px" HorizontalAlign="Left" BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="PracticeID" HeaderText="Practice ID">
                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PracticeName" HeaderText="Practice Name">
                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="190px" />
                        </asp:BoundField>   
                        <asp:BoundField DataField="PracticeServiceCode" HeaderText="Code">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>  
                        <asp:BoundField DataField="PracticeCharge" HeaderText="Charge">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>   
                        <asp:BoundField DataField="Dispensed" HeaderText="Dispensed">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>     
                        <asp:BoundField DataField="Description" HeaderText="Description">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                        </asp:BoundField>   
                        <asp:BoundField DataField="SDID" HeaderText="SDID">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>                          
                        <asp:BoundField DataField="sddescr" HeaderText="Sd Description">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                        </asp:BoundField>                          
                        <asp:BoundField DataField="FTEDr" HeaderText="FTE">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VMRegion" HeaderText="VM Region">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CountCodesinRegion" HeaderText="Count Region">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CountPracticesinRegion" HeaderText="Count Prt Region">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px" />
                        </asp:BoundField>
                                                                                                
                        <asp:BoundField DataField="TransStartDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy hh:mm}">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px" />
                        </asp:BoundField>   
                        <asp:BoundField DataField="TransEndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy hh:mm}">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px" />
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
    &copy;2007-2009, Vet Metrics Inc. All rights reserved.
    </td>
  </tr>
</table>    
</div>
</form>
</body>
</html>
