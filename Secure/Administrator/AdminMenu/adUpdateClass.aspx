<%@ Page StylesheetTheme="AdminPart" Language="VB" AutoEventWireup="false" CodeFile="adUpdateClass.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.AdminUpdateClass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>Admin Menu - Update Client Class.</title>
<script language="javascript" type="text/javascript">
var SKIPHEADER_CHECKBOX = "grdPreData_ctl01_headerCheckBox";

function ConfirmExec()
{
    var confirmed = window.confirm("Are you sure?");
    return (confirmed);
}

function ReadHistoryArray()
{
   var theForm = document.forms[0];
   var elementsSum = theForm.elements.length;
   var myCurrent;
   var myStrTemp = '';
   var finalString = '';
            
   if (elementsSum != 0) 
   {
       for (var i = 0; i < elementsSum; i++) 
       {
           var myElement = theForm.elements[i];
                    
           if (myElement.type == "checkbox" && myElement.id != SKIPHEADER_CHECKBOX) 
           {
                if (myElement.checked)
                {
                    myStrTemp += myElement.value + '=true;';
                }  
                else
                {
                    myStrTemp += myElement.value + '=false;';
                }
            } // end type checbox
       } // end For
    } // end if

      finalString =  myStrTemp.substring(0, myStrTemp.length - 1);
      document.forms[0].txtRecordCheck.value = finalString;
}
</script> 
</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom : 0px; margin-left : 0px; margin-right : 0px; margin-top : 0px;">
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
                    Admin Menu - Update Client Class</td>
			</tr>
	<tr>
			<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
               <asp:UpdateProgress AssociatedUpdatePanelID="myUpdatePanel" ID="myUpdProgress" runat="server">
                    <ProgressTemplate>
                    <div style="position: absolute; width: 350px; left: 255px; top: 280px; border: solid 2px Red; padding: 4px; background-color: #507CD1; color: White;">
                         <img src="../../../Secure/Administrator/images/indicator.gif" alt="Update" />
                                    The web page is updating ...&nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Please Wait ...
                                    </div>
                    </ProgressTemplate> 
                </asp:UpdateProgress> 
                <asp:Label ID="lblMessageProcess" runat="server" EnableViewState="False" Font-Bold="True"
                    Font-Size="16px" ForeColor="Blue"></asp:Label><br />
                <asp:UpdatePanel ID="myUpdatePanel" runat="server">
             <ContentTemplate>
                 First slelect a Practice from dropdownlist.<br />
                 Then hit the Display button.<br />
                 Finally select the Rows from Grid and update the database.<br />
                <asp:Label ID="lblPrcMessage" runat="server" ForeColor="Red"></asp:Label><br />
                <asp:DropDownList ID="drpPracticeDpl" runat="server" Width="303px" AutoPostBack="True">
                </asp:DropDownList><br />
                <asp:Button ID="btnStartNow" runat="server" Text="Display" Width="101px" />&nbsp;<asp:Button
                    ID="btnSave" runat="server" Enabled="False" OnClick="btnSave_Click" Text="Save"
                    Width="103px" /><br />
                &nbsp;<br />
                    <asp:GridView ID="grdPreData" runat="server" 
                    CellPadding="4" 
                    ForeColor="#333333" 
                    GridLines="None"
                    EmptyDataText="No Records found ..."
                    AutoGenerateColumns="False" 
                    DataKeyNames="UniqueID"
                    >
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField HeaderText="Description" DataField="Descr" >
                                <ItemStyle Width="175px" Wrap="false"  />
                                <HeaderStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Class ID" DataField="ClassID"  >
                                <ItemStyle Width="50px" />
                                <HeaderStyle Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Practice ID" DataField="PracticeID"  >
                                <ItemStyle Width="50px" />
                                <HeaderStyle Wrap="false" />
                            </asp:BoundField>
                           <asp:TemplateField HeaderText="Reminders">
                                <ItemTemplate>
                                   <input type="checkbox" 
                                    id="myRowCheckBox" 
                                    name="myRowCheckBox" 
                                    checked='<%# DisplayTick(CType(DataBinder.Eval(Container.DataItem, "Reminders"), Object)) %>'
                                    value='<%# DataBinder.Eval(Container.DataItem, "UniqueID") %>' 
                                    onclick="ReadHistoryArray();"
                                    title='<%# DataBinder.Eval(Container.DataItem, "UniqueID") %>'
                                    runat="server"
                                    />
                                    <asp:Label 
                                    ID="lblYesOrNo" 
                                    runat="server" 
                                    Text='<%# DisplayYESorNO(CType(DataBinder.Eval(Container.DataItem, "Reminders"), Object)) %>' 
                                    />
                                </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Last Modified By" DataField="LastModifiedBy">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Width="70px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataFormatString="{0:d}" DataField="VMCreateDate" HeaderText="VM Creatre Date" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Width="90px" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataFormatString="{0:d}" DataField="VMLastModDate" HeaderText="VM Last Mod Date" >
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Width="90px" Wrap="False" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                &nbsp; &nbsp;<br />
                <asp:HiddenField ID="txtRecordCheck" runat="server" />
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
