<%@ Page StylesheetTheme="AdminPart" Language="VB" AutoEventWireup="false" CodeFile="adCreateTask.aspx.vb" Inherits=" VeterinaryMetrics.BusinessLayer.adCreateTask" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Admin Menu - Create new tasks</title>
</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom : 0px; margin-left : 0px; margin-right : 0px; margin-top : 0px;" >
    <form id="frmCreateTask" runat="server" >
    <div>
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
				<td colspan="2" style="width: 600px; height: 24px; text-align: left; font-family: Arial; font-size: 16px; color: #006AB6; font-weight: bold;">
                    Create new task</td>
			</tr>
	        <tr>
			    <td style="width: 130px; text-align: right; vertical-align: middle;">
			        User Id:
                </td>
                <td style="width: 470px; text-align: left; vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="drpUserList" CssClass="fld" Width="140px" AutoPostBack="True" ></asp:DropDownList>
                </td>
	        </tr>
	        <tr>
			    <td style="width: 130px; text-align: right; vertical-align: middle;">
			        Group Description:
                </td>
                <td style="width: 470px; text-align: left; vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="drpGroupList" CssClass="fld" Width="140px" ></asp:DropDownList>
                </td>
	        </tr>
	        <tr>
			    <td style="width: 130px; text-align: right; vertical-align: middle;">
			        Task Description:
                </td>
                <td style="width: 470px; text-align: left; vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="dtpTaskList" CssClass="fld" Width="140px" ></asp:DropDownList>
                </td>
	        </tr>	
	        <tr>
			    <td style="width: 130px; text-align: right; vertical-align: middle;">
			      :
                </td>
                <td style="width: 470px; text-align: left; vertical-align: middle;">
                    <asp:Button ID="btnAddNewTask" runat="server" Text="Add new" Width="103px" />
                    </td>
	        </tr>        	        
        <tr>
            <td style="vertical-align: middle; width: 130px; text-align: right">
                &nbsp;
            </td>
            <td style="vertical-align: middle; width: 470px; text-align: left">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label></td>
        </tr>
        <tr>
		    <td colspan="2" style="width: 600px; height: 24px; text-align: center; ">
                <asp:GridView ID="grvTaskData" 
                EmptyDataText="Records are not found." 
                runat="server" 
                BackColor="White" 
                BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                CellPadding="3" GridLines="Vertical"
                AutoGenerateColumns="False"
                DataKeyNames="UsersTasksId"
                >
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle HorizontalAlign="Left" BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField Visible="false" HeaderText="PK" >
                            <HeaderStyle Wrap="False" />
                            <ItemTemplate>
                                <asp:Label ID="lblRowID" runat="server" Text='<%# Bind("UsersTasksId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:BoundField DataField="GroupDesc" HeaderText="Group Desc">
                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="170px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TaskDesc" HeaderText="Task Desc">
                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                        </asp:BoundField>   
                        <asp:BoundField DataField="CreatedDate" HeaderText="Create Time" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="180px" />
                        </asp:BoundField>  
                        <asp:ButtonField CommandName="Delete" Text="DEL" />              
                        
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
