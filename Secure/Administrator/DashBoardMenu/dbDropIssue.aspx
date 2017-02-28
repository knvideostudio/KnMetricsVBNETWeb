<%@ Page StylesheetTheme="AdminPart" Language="VB" AutoEventWireup="false" CodeFile="dbDropIssue.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.AdminDropIssue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>DashBoard - Version 3.00</title>
</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom : 0px; margin-left : 0px; margin-right : 0px; margin-top : 0px;" >
    <form id="frmDropIssue" runat="server">
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
    <table style="width: 600px;" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td style="width: 600px; height: 24px; text-align: left; font-family: Arial; font-size: 16px; color: #006AB6; font-weight: bold;">
                    DashBoard - Drop Issue</td>
			</tr>
	<tr>
			<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
               <asp:Repeater ID="repDbIssue" runat="server">
                   <HeaderTemplate>
                      <table cellpadding="2" cellspacing="2" style="border: 1px; border-color: Blue;" id="tbDrop">
                        <tr>
                          <td class="header"><b>PracticeId</b></td>
                          <td class="header"><b>DropId</b></td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                         <tr class="row">
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "PracticeId")%>
                            </td>
                            <td> 
                                <%#DataBinder.Eval(Container.DataItem, "DropId")%>
                            </td>
                          </tr>
                    </ItemTemplate> 
                    <AlternatingItemTemplate>
                           <tr class="altclr">
                            <td>
                                <%#DataBinder.Eval(Container.DataItem, "PracticeId")%>
                            </td>
                            <td> 
                                <%#DataBinder.Eval(Container.DataItem, "DropId")%>
                            </td>
                          </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
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
    <td style="width: 100%; color: #FFFFFF; background-color: #006AB6; height: 25px; text-align: center;" colspan="3">
    ©2007-2009, Vet Metrics Inc. All rights reserved.
    </td>
  </tr>
</table>    
</div>
    </form>
    
</body>
</html>
