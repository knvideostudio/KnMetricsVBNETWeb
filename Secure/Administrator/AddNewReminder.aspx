<%@ Page StylesheetTheme="DefaultPages" Language="VB" AutoEventWireup="false" CodeFile="AddNewReminder.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.AddNewReminder" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Add or Edit Reminder Drop</title>
    <script language="javascript" type="text/jscript" src="include/MyCalendar.js"></script> 
    <style type="text/css" >
    </style>
    <script language="javascript" type="text/javascript">
    
    function AddPractice() 
    {
	    //var answer = confirm("Do you want to continue?");
    	var record =  document.forms[0].txtAddPracticeID.value
	    if (record.length > 0)
	    {
		    alert("Adding Practice Id: \"" + document.forms[0].txtAddPracticeID.value + "\"");
	    }
	    else
	    {
		    //alert("No ...");
		    return false;
	    }
	}
	
	function DelPractice() 
    {
	    //var answer = confirm("Do you want to continue?");
    	var record =  document.forms[0].txtDelPracticeID.value
	    if (record.length > 0)
	    {
		    alert("Deleting Practice Id: \"" + document.forms[0].txtDelPracticeID.value + "\"");
	    }
	    else
	    {
		    //alert("No ...");
		    return false;
	    }
	}

    </script>   
</head>
<body onunload="CloseCalendar();ClosePractice();" style="font-family : Verdana; font-size : 10pt;">

    <form id="frmAddReminderDrop" runat="server">
      <div class="content">
     <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="AutoNumber1">
  <tr>
    <td style="width: 34%; height: 100px; background-color: #000000">
        &nbsp;&nbsp;<img src="../../images/bwLogo.bmp" alt="Logo" /></td>
    <td style="width: 33%; height: 100px; background-color: #000000">&nbsp;</td>
    <td style="width: 33%; height: 100px; background-color: #000000; text-align: right; vertical-align: bottom;">&nbsp;</td>
  </tr>
      <tr>
    <td style="width: 100%; height: 25px; background-color: #000000; text-align: left; vertical-align: middle;" colspan="3" >&nbsp;
        <asp:Label ID="lblLinkAddress" runat="server" EnableViewState="False" ForeColor="White"></asp:Label></td>
  </tr>
  <tr>
    <td style="width: 100%; background-color: #FFFFFF; height: 2px;" colspan="3"><img alt="" src="../../images/pixel.gif" /></td>
  </tr>
  <tr>
    <td style="width: 34%; height: 25px; text-align: left; background-color: #006AB6; color: #FFFFFF;">&nbsp;User 
    login:&nbsp;<asp:Label ID="lblUserName" runat="server" Font-Bold="True" Text="No User"></asp:Label></td>
    <td style="width: 66%; height: 25px; background-color: #006AB6; text-align: right;">&nbsp;
    </td>
    
  </tr>
  <tr>
    <td style="width: 100%; height: 16px; text-align: center; vertical-align: top;" colspan="3">
    
   <!-- Second table begins -->
    <table style="width: 800px; border: 0;" >
            <tr>
            <td colspan="4" style="width: 700px; text-align: left; vertical-align: top;">
            <h3>
                <asp:Label ID="lblActionType" runat="server" Text="Type of action"></asp:Label>&nbsp;</h3>
                The time in the required fields are in 24 hours format.<br />
                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" ></asp:Label>
            </td>
        </tr>
        
            <tr>
            <td align="left" style="width: 230px" valign="top" >
                Mailer ID</td>
            <td align="left" style="width: 243px"  valign="top">
            <asp:DropDownList ID="drpMailerID" runat="server" ToolTip="Mailer ID" Width="227px" TabIndex="1" >
            </asp:DropDownList>
            
            </td>
            <td align="left" style="width: 237px" valign="top" >
                VM NPC</td>
            <td align="left" style="width: 170px"  valign="top">
                <asp:DropDownList ID="drpVmNpc" runat="server" Width="80px" TabIndex="2">
                    <asp:ListItem />
                    <asp:ListItem Text="IND" Value="IND" />
                    <asp:ListItem Text="NPC" Value="NPC" />
                    <asp:ListItem Text="VM" Value="VM" />
                </asp:DropDownList></td>
        </tr> 
       <tr>
            <td align="left" valign="top" style="width: 230px;">
                Drop Description</td>
            <td align="left" style="width: 570px;" valign="top" colspan="3">
                <asp:TextBox ID="txtDescription" runat="server"
                    Width="344px" MaxLength="50" BorderStyle="Solid" BorderWidth="1px" TabIndex="3"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDropDesc" runat="server" ControlToValidate="txtDescription"
                    ErrorMessage="enter value"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="left" valign="top" style="width: 230px;">
                Active Web</td>
            <td align="left" style="width: 570px;" valign="top" colspan="3">
                <asp:RadioButton ID="rdbActiveWebYes" GroupName="ActiveWeb" runat="server" Text="Yes" TabIndex="4" />
                <asp:RadioButton ID="rdbActiveWebNo" GroupName="ActiveWeb" runat="server" Checked="True" Text="No" TabIndex="5" /></td>
        </tr>
         <tr>
            <td align="left" style="width: 230px" valign="top" >
                Active Drop</td>
            <td align="left" style="width: 570px" valign="top" colspan="3">
                <asp:RadioButton ID="rdbActiveDropYes" GroupName="ActiveDrop" runat="server" Checked="True" Text="Yes" TabIndex="6" />
                <asp:RadioButton ID="rdbActiveDropNo" GroupName="ActiveDrop" runat="server" Text="No" TabIndex="7" /></td>
        </tr>
        <tr>
            <td align="left" style="width: 230px; " valign="top" >
                <asp:Label ID="lblPulDate" runat="server" AssociatedControlID="txtPullDate" Text="Pull Date" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 243px" valign="top" >
                <asp:TextBox ID="txtPullDate" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" ValidationGroup="Dates" MaxLength="10" TabIndex="8"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtPullDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
                <asp:RequiredFieldValidator ID="rfvPullDate" runat="server" ControlToValidate="txtPullDate"
                    ErrorMessage="enter date" ValidationGroup="Dates"></asp:RequiredFieldValidator></td>
            <td align="left" style="width: 237px" valign="top">
            <!-- Populate button update the rest dates -->
            <asp:Button ID="btnPopulate" runat="server" Text="Populate" ValidationGroup="Dates" TabIndex="9" /></td>
            <td align="left" style="width: 170px"  valign="top"></td>
        </tr>

                 <tr>
            <td align="left" style="width: 230px" valign="top" >
                <asp:Label ID="lblOnLineDate" runat="server" AssociatedControlID="txtOnLineDate" EnableViewState="False" Text="On Line Date"></asp:Label></td>
            <td align="left" style="width: 243px"  valign="top">
                 <asp:TextBox ID="txtOnLineDate" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="128px" MaxLength="50" TabIndex="10"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtOnLineDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
                </td>
            <td align="left" style="width: 237px" valign="top" >
                 <asp:Label ID="lblOffLineDate" AssociatedControlID="txtOffLineDate" runat="server" Text="Off Line Date" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 170px"  valign="top">
                <asp:TextBox ID="txtOffLineDate" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="128px" TabIndex="11" MaxLength="50"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtOffLineDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
                </td>
        </tr>   
        <tr>
            <td align="left" style="width: 230px" valign="top" >
                <asp:Label ID="lblStartCDate" AssociatedControlID="txtStartCDate" runat="server" Text="Starting Date C" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 243px"  valign="top">
                <asp:TextBox ID="txtStartCDate" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" MaxLength="10" TabIndex="12"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtStartCDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
            <td align="left" style="width: 237px" valign="top" >
                <asp:Label ID="lblRptStartCDate" AssociatedControlID="txtRptStartCDate" runat="server" Text="Report Start Date C" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 170px"  valign="top">
                <asp:TextBox ID="txtRptStartCDate" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" MaxLength="10" TabIndex="13"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtRptStartCDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
        </tr>
       <tr>
            <td align="left" style="width: 230px" valign="top" >
                <asp:Label ID="lblEndCDate" AssociatedControlID="txtEndCDate" runat="server" Text="End Date C" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 243px"  valign="top">
                <asp:TextBox ID="txtEndCDate" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" MaxLength="10" TabIndex="14"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtEndCDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
                         <td align="left" style="width: 237px" valign="top" >
                <asp:Label ID="lblRptEndCDate" AssociatedControlID="txtRptEndCDate" runat="server" Text="Report End Date C" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 170px"  valign="top">
                <asp:TextBox ID="txtRptEndCDate" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="128px" MaxLength="50" TabIndex="15"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtRptEndCDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
        </tr>
       <tr>
            <td align="left" style="width: 230px" valign="top" >
                <asp:Label ID="lblStartNR1Date" AssociatedControlID="txtStartNR1Date" runat="server" Text="Start Date NR1" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 243px"  valign="top">
                <asp:TextBox ID="txtStartNR1Date" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" MaxLength="10" TabIndex="16"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtStartNR1Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
          <td align="left" style="width: 237px" valign="top" >
                <asp:Label ID="lblRptStartRCYDate" AssociatedControlID="txtRptStartRCYDate" runat="server" Text="Report Start Date RCY" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 170px"  valign="top">
                <asp:TextBox ID="txtRptStartRCYDate" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" MaxLength="10" TabIndex="17"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtRptStartRCYDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>

        </tr>
        <tr>
            <td align="left" style="width: 230px" valign="top" >
                <asp:Label ID="lblEndNR1Date" AssociatedControlID="txtEndNR1Date" runat="server" Text="End Date NR1" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 243px"  valign="top">
                <asp:TextBox ID="txtEndNR1Date" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" MaxLength="10" TabIndex="18"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtEndNR1Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
           <td align="left" style="width: 237px" valign="top" >
                <asp:Label ID="lblRptEndRCYDate" AssociatedControlID="txtRptEndRCYDate" runat="server" Text="Report End Date RCY" EnableViewState="False"></asp:Label></td>
          <td align="left" style="width: 170px"  valign="top">
                    <asp:TextBox ID="txtRptEndRCYDate" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" MaxLength="10" TabIndex="19"></asp:TextBox>
                    <a href="javascript:OpenCalendar('txtRptEndRCYDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
        </tr>     
        <tr>
            <td align="left" style="width: 230px; height: 24px;" valign="top" >
                <asp:Label ID="lblStartNR2Date" AssociatedControlID="txtStartNR2Date" runat="server" Text="Start Date NR2" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 243px; height: 24px;"  valign="top">
                <asp:TextBox ID="txtStartNR2Date" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" MaxLength="10" TabIndex="20"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtStartNR2Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
            <td align="left" style="width: 237px; height: 24px;" valign="top">
                <asp:Label ID="lblEndShortDate" AssociatedControlID="txtOffLineDate" runat="server" Text="Report End Date Short" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 170px; height: 24px;"  valign="top">
               <asp:TextBox ID="txtRptEndShortDate" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="128px" MaxLength="50" TabIndex="21"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtRptEndShortDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
                
        </tr> 
        <tr>
            <td align="left" style="width: 230px" valign="top" >
                <asp:Label ID="lblEndNR2Date" AssociatedControlID="txtEndNR2Date" runat="server" Text="End Date NR2" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 243px"  valign="top">
                <asp:TextBox ID="txtEndNR2Date" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" MaxLength="10" TabIndex="22"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtEndNR2Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
            <td align="left" style="width: 237px" valign="top" >
                <asp:Label ID="lblPrintRunDate" AssociatedControlID="txtPrintRunDate" runat="server" Text="Print Run Date" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 170px"  valign="top">
                <asp:TextBox ID="txtPrintRunDate" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" MaxLength="10" TabIndex="23"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtPrintRunDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
        </tr>  
        <tr>
            <td align="left" style="width: 230px" valign="top">
                <asp:Label ID="lblStartNR3Date" AssociatedControlID="txtOffLineDate" runat="server" Text="Report Start Date NR3" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 243px"  valign="top">
                <asp:TextBox ID="txtRptStartNR3Date" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" MaxLength="10" TabIndex="24"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtRptStartNR3Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
            <td align="left" style="width: 237px" valign="top">
                Exp Date-1 </td>
            <td align="left" style="width: 170px"  valign="top">
                <asp:TextBox ID="txtExp1Date" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px" MaxLength="10" TabIndex="25"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtExp1Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
        </tr> 
          <tr>
            <td align="left" style="width: 230px" valign="top" >
                <asp:Label ID="lblEndNR3Date" AssociatedControlID="txtOffLineDate" runat="server" Text="Report End Date NR3" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 243px"  valign="top">
                <asp:TextBox ID="txtRptEndNR3Date" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="128px" MaxLength="50" TabIndex="26"></asp:TextBox>
                <a href="javascript:OpenCalendar('txtRptEndNR3Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
             <td align="left" style="width: 237px" valign="top">
                 Exp Date-2</td>
             <td align="left" style="width: 170px"  valign="top">
                 <asp:TextBox ID="txtExp2Date" runat="server" BorderStyle="Solid" BorderWidth="1px"
                     MaxLength="10" Width="80px" TabIndex="27"></asp:TextBox>
                 <a href="javascript:OpenCalendar('txtExp2Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
        </tr> 
        <tr>
            <td align="left" style="width: 230px" valign="top" >
                <asp:Label ID="lblPrintRunSeries" AssociatedControlID="txtPrintRunSeries" runat="server" Text="Print Run Series" EnableViewState="False"></asp:Label></td>
            <td align="left" style="width: 243px"  valign="top">
                <asp:TextBox ID="txtPrintRunSeries" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="120px" MaxLength="10" TabIndex="28"></asp:TextBox></td>
             <td align="left" style="width: 237px" valign="top">
                 Exp Date-3</td>
             <td align="left" style="width: 170px"  valign="top">
                 <asp:TextBox ID="txtExp3Date" runat="server" BorderStyle="Solid" BorderWidth="1px"
                     MaxLength="10" Width="80px" TabIndex="29"></asp:TextBox>
                 <a href="javascript:OpenCalendar('txtExp3Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a></td>
        </tr> 
        <tr>
            <td align="left" style="width: 230px" valign="top" >
                </td>
            <td align="left" style="width: 243px"  valign="top">
                &nbsp;&nbsp;
             </td>
            <td style="width: 237px"></td>
             <td></td>
        </tr>                 
        <tr>
            <td align="left" style="width: 100px" valign="top" >
                </td>
            <td align="left" style="width: 700px" valign="top" colspan="3">
                &nbsp;<asp:Button ID="btnSaveReminder" runat="server" Text="Save Reminders" TabIndex="30" />
                <asp:Button ID="btnUpdateRmd" runat="server" Text="Update Reminder" TabIndex="30" />&nbsp;<asp:Button
                    ID="btnCloseWindow" runat="server"  EnableViewState="False" Text="Close Window" TabIndex="31" /></td>
        </tr>
    </table> 
    
    <!-- End second table -->
        <br />
        &nbsp;
        <asp:Panel ID="MyPanelPractices" runat="server" BorderWidth="1px" Width="500px">
        <table border="0" style="width: 500px" >
        <tr>
            <td style="text-align: left; background-color: #6699ff; height: 25;" colspan="2">
                &nbsp;Practices
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 250px;">
                <asp:TextBox ID="txtAddDelPracticeID" runat="server" Width="100px" MaxLength="10" TabIndex="32" />
                <asp:Button ID="btnAddPractice" runat="server" Text="Add" TabIndex="33" />
                <asp:Button ID="btnDelPractice" runat="server" ValidationGroup="DeletePractice"  Text="Delete" TabIndex="35" /></td>
             <td align="left" style="width: 250px;">
                 <asp:Label ID="lblAddDelPractice" runat="server" EnableViewState="False"></asp:Label></td>
        </tr>
                <tr>
            <td align="left" style="width: 250px;">
                &nbsp;</td>
             <td align="left" style="width: 250px;">
                 </td>
        </tr>
        <tr>
            <td align="left"  colspan="2">
            <asp:Label ID="lblPractices" runat="server"></asp:Label>
            </td>
        </tr>
        </table>
            </asp:Panel>
    </td>
  </tr>
  <tr>
    <td style="width: 100%; background-color: #000000;" colspan="3"><img alt="" src="../../images/pixel.gif" /></td>
  </tr>
  <tr>
    <td style="width: 100%; height: 26px; background-color: #006AB6; color: #FFFFFF; font-size: small; text-align: center;" colspan="3">©2007-2009, Vet Metrics Inc. All rights reserved.
    </td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>
