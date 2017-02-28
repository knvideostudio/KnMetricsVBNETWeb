<%@ Page StylesheetTheme="AdminPart" Language="VB" AutoEventWireup="false" CodeFile="adListPersonel.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.AdminPersonalList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Admin Menu - Version 1.00</title>
<script language="javascript" type="text/javascript" src="../include/sorttable.js"></script>
</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom : 0px; margin-left : 0px; margin-right : 0px; margin-top : 0px;" >
    <form id="frmListPer" runat="server">
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
    <table style="width: 800px;" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td style="width: 800px; height: 24px; text-align: left; font-family: Arial; font-size: 16px; color: #006AB6; font-weight: bold;">
                    Admin Menu - List Personnel</td>
			</tr>
	<tr>
			<td style="width: 800px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
                <br />
                Click on the header names in order to sort.<br />
                &nbsp;<br />
                <asp:GridView CssClass="sortable" ID="grdListPersonnel" runat="server" CellPadding="4" ForeColor="#333333" 
                GridLines="None"
                AutoGenerateColumns="False" 
                DataKeyNames="PersID" 
                EmptyDataText="No records found in the Personnel Table ..."
                >
                    <Columns>
                        <asp:BoundField HeaderText="First Name" DataField="PersFirst" SortExpression="PersFirst" ReadOnly="True" />
                        <asp:BoundField HeaderText="Last Name" DataField="PersLast" SortExpression="PersLast" ReadOnly="True" />
                        <asp:BoundField HeaderText="E-mail" DataField="PersEmail" SortExpression="PersEmail" ReadOnly="True" />
                        <asp:BoundField HeaderText="User ID" DataField="UserID" SortExpression="UserID" ReadOnly="True" >
                            <ControlStyle Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Pass">
                             <HeaderStyle HorizontalAlign="Left" />
                             <ItemStyle HorizontalAlign="Left" />
                             <ItemTemplate>
                                 <asp:DropDownList ID="drpPsdShow" runat="server" >
                                     <asp:ListItem Value="88888" Text="*****" />
                                     <asp:ListItem Value="pass" Text="myPassword" /> 
                                 </asp:DropDownList> 
                             </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Practice Name" DataField="PracticeName" SortExpression="PracticeName" ReadOnly="True" >
                            <ControlStyle Width="350px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
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
    <td style="width: 100%; background-color: #006AB6; height: 25px; text-align: center; color: #FFFFFF; font-size: small;" colspan="3">
    &copy;2007-2009, Vet Metrics Inc. All rights reserved.
    </td>
  </tr>
</table>    
</div>
    </form>
    
</body>
</html>
