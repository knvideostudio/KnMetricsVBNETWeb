<%@ Page StylesheetTheme="AdminPart" Language="VB" AutoEventWireup="false" CodeFile="ProAwExclusion.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.ProAwExclusion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Build AW Exclusion List</title>
    <script language="javascript" type="text/javascript">
    
    function GetOnlySelect()
    {
      var selText = "";
      var finalText = "";
      var inputElements = document.getElementsByTagName('input');
         
      for (var i = 0 ; i < inputElements.length ; i++) 
      {
          var myElement = inputElements[i];
          // Filter through the input types looking for checkboxes

          if (myElement.type == "checkbox") 
          {
             // Use the involker (our calling element) as the reference 
             //  for our checkbox status
             if (myElement.checked)
             {
              // alert(myElement.value);
                 selText += myElement.value + ';';
             }
           }
       }
         
       finalText = selText.substring(0, selText.length - 1);

       // alert(finalText);
       document.forms[0].hdfReadyToPost.value = finalText;
       // document.txtReadyToPost.value =  finalText;
   }
    
    function selectAll(involker) 
    {
        var inputElements = document.getElementsByTagName('input');

        for (var i = 0 ; i < inputElements.length ; i++) 
        {
            var myElement = inputElements[i];
            // Filter through the input types looking for checkboxes

            if (myElement.type == "checkbox") 
            {
               // Use the involker (our calling element) as the reference 
               //  for our checkbox status

                myElement.checked = involker.checked;
            }
        }
 }  
</script> 
</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom : 0px; margin-left : 0px; margin-right : 0px; margin-top : 0px;" >
<form id="frmAwExcl" runat="server">
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
				<td style="width: 600px; height: 24px; text-align: left; font-family: Arial; font-size: 16px; color: #006AB6; font-weight: bold;">
                    Build Exclusion List</td>
			</tr>
	<tr>
		<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
        <table cellpadding="0" cellspacing="0" width="600px">
            <tr>
                <td style="width: 150px; text-align: right; height: 13px;">Drop Id:&nbsp; &nbsp;</td>
                <td style="width: 450px; height: 13px;">
                    <asp:DropDownList ID="drpReminderList" runat="server" Width="350px" CssClass="fld" AutoPostBack="True">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="2" style="width: 600px; text-align: center; height: 30px;">
                    <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                </td>
            </tr>            
            <tr>
                <td colspan="2" style="width: 600px; text-align: center; vertical-align: top;">
                    <asp:GridView ID="grdExclusionView" 
                    runat="server" 
                    BackColor="White" 
                    BorderColor="#E7E7FF" 
                    BorderStyle="None" 
                    BorderWidth="1px" 
                    CellPadding="3" GridLines="Horizontal"
                    AutoGenerateColumns="false" 
                    EmptyDataText="No Records ..."
                    >
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <HeaderStyle HorizontalAlign="Left" BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Wrap="False" />
                                <HeaderTemplate>
                                    <input runat="server" id="cbSelectAll" name="cbSelectAll" type="checkbox" onclick="selectAll(this)"  />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input 
                                    runat="server"  
                                    id="chkDrpId" 
                                    name="chkDrpId" 
                                    type="checkbox" 
                                    value='<%# DataBinder.Eval(Container.DataItem, "PracticeId") %>'
                                    /> 
                                </ItemTemplate>
                            </asp:TemplateField> 
                         <asp:BoundField DataField="PracticeName" HeaderText="Practice Name">
                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PracticeId" HeaderText="Practice Id">
                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                        </asp:BoundField>   
                        <asp:BoundField DataField="HistoryEndDate" HeaderText="History Date" DataFormatString="{0:MM/dd/yyyy}">
                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                        </asp:BoundField>                           
                        <asp:BoundField DataField="RemindersCount" HeaderText="Counts">
                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
                        </asp:BoundField>                    
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 600px; text-align: center; vertical-align: top;">
                    <asp:HiddenField ID="hdfReadyToPost" runat="server" /> 
                </td> 
            </tr> 
            <tr>
                <td style="width: 150px; height: 13px; text-align: right">
                </td>
                <td style="width: 450px; height: 13px">
                    <asp:Button 
                        ID="btnPopulate" 
                        runat="server" 
                        CssClass="fld" 
                        Text="Build Exclution List"
                        OnClientClick="GetOnlySelect()"
                        Width="164px" />
                </td>
            </tr>            
        </table>  
        
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
