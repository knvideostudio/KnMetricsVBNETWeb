<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.SecureActivationMail2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Welcome Letter - Version 5.0</title>
    <style type="text/css"> 

body {margin-top:20px; margin-left:20px; margin-right:20px; color:#000000; font-family:Verdana; background-color:white}
A:link 
{
font-weight:normal; 
color:#000066; 
text-decoration:none
}
A:visited {font-weight:normal; color:#000066; text-decoration:none}
A:active {font-weight:normal; text-decoration:none}
A:hover {font-weight:normal; color:#FF6600; text-decoration:none}
.BannerColumn {background-color:#000000}
.Banner {border:0; padding:0; height:8px; margin-top:0px; color:#ffffff; filter:progid:DXImageTransform.Microsoft.Gradient(GradientType=1,StartColorStr='#1c5280',EndColorStr='#FFFFFF');}
.BannerTextCompany {font:bold; font-size:20px; color:#cecece; font-family:Tahoma; height:0px; margin-top:0; margin-left:8px; margin-bottom:0; padding:0px; white-space:nowrap; filter:progid:DXImageTransform.Microsoft.dropshadow(OffX=2,OffY=2,Color='black',Positive='true');}
.BannerTextApplication {font:bold; font-size:14px; font-family:Tahoma; height:0px; margin-top:0; margin-left:8px; margin-bottom:0; padding:0px; white-space:nowrap; filter:progid:DXImageTransform.Microsoft.dropshadow(OffX=2,OffY=2,Color='black',Positive='true');}
.BannerText {font:bold; font-size:18pt; font-family:Tahoma; height:0px; margin-top:0; margin-left:8px; margin-bottom:0; padding:0px; filter:progid:DXImageTransform.Microsoft.dropshadow(OffX=2,OffY=2,Color='black',Positive='true');}
.BannerSubhead {border:0; padding:0; height:16px; margin-top:0px; margin-left:10px; color:#ffffff; filter:progid:DXImageTransform.Microsoft.Gradient(GradientType=1,StartColorStr='#4B3E1A',EndColorStr='#FFFFFF');}
.BannerSubheadText {font:bold; height:11px; font-size:11px; font-family:Tahoma; margin-top:1; margin-left:10; filter:progid:DXImageTransform.Microsoft.dropshadow(OffX=2,OffY=2,Color='black',Positive='true');}
</style>
    <script language="javascript" type="text/javascript">
var popUp; 

    function OpenCalendar(idname)
    {
	    popUp = window.open('../include/SpecialCalendar.aspx?formname=' + document.forms[0].name + 
		    '&id=' + idname + '&selected=' + document.forms[0].elements[idname].value, 
		    'popupcal', 
		    'width=270,height=300,left=200,top=200');
	    popUp.focus();
    }

    function SetDate(formName, id, newDate)
    {
	    eval('var theform = document.' + formName + ';');
	    popUp.close();
	    theform.elements[id].value = newDate;
	    theform.elements[id].focus();
    }	

    function ShowProgress()
    {
        var progress = document.getElementById("ProgressBar");
        progress.style.display = "";
    }
    
    function StopProgress()
    {
        var progress = document.getElementById("ProgressBar");
        progress.style.display = "none";
    }
    
    function UpdatePage(results, context)
    {
        StopProgress();
        //var label = document.getElementById("lblStep3");
        //label.innerHTML = results;
        
        var button1 = document.getElementById("btnImportFile");
        button1.disabled = false;
    }
    
    function DisableButtons()
    {
        var button1 = document.getElementById("btnImportFile");
        
        if (button1 != null)
        {
		    button1.disabled = true;
        }
        //var textbox1 = document.getElementById("TextBox1");
       // textbox1.disabled = true;
        
        var progress = document.getElementById("ProgressBar");
        progress.style.display = "";
        return false;
    }
    

    function mySave(m)
    {
        document.execCommand('SaveAs', false, m); 
    }
    
</script>
</head>
<body style="font-family : Verdana; font-size : 10pt; margin-bottom : 0px; margin-left : 0px; margin-right : 0px; margin-top : 0px;" >
    <form id="frmActMail2" runat="server" onsubmit="DisableButtons()" submitdisabledcontrols="true">
    <div>
    <ajaxToolkit:ToolkitScriptManager ID="scManager" runat="server" />
 <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="MainTable">
  <tr>
    <td colspan="3">
        <table cellpadding="2" cellspacing="0" border="0" bgcolor="#cecece" width="100%">
            <tr>
                <td style="height: 81px; vertical-align: middle;">
                    <table bgcolor="#1c5280" width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td class="Banner" />
                        </tr>
                        <tr>
                            <td class="Banner">
                                <span class="BannerTextCompany">Veterinary Metrics, Inc.</span></td>
                        </tr>
                        <tr>
                            <td class="Banner" style="height: 8px">
                                <span class="BannerTextApplication">Welcome Letter 5</span></td>
                        </tr>
                        <tr>
                            <td class="Banner" align="RIGHT" />
                        </tr>
                    </table>
                    &nbsp;<a href="../../../Default.aspx">Main Menu</a> | <a href="../ExpWmSend.aspx">Report</a>
                    | <a href="Default.aspx">Page Refresh</a> | <a href="Default.aspx?act=files">Display
                        Files</a> | <a href="../../../SignOut.aspx">Sign Out</a>
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
                    Welcome Letter Process - Version 5</td>
			</tr>
	<tr>
			<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Desktop"></asp:Label><br />
                <div id="ProgressBar" style="display:none; font-weight: bold; font-size: 11px; color: navy; font-family: Verdana; background-color: #ffff99;">
                    <img alt="" src="../images/indicator.gif"  />
                    <span id="Msg">Please Wait ... </span>
                </div>
            </td>
	</tr>
	<tr>
			<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
			<asp:Panel ID="pnlDownload" runat="server" ScrollBars="None" BackColor="#FFFFC0" BorderColor="Blue" BorderWidth="1px" Width="100%">
			<!-- begin pnlDownload Table -->
			<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="600" id="dnlTable">
			<tr>
				<td style="width: 600px; height: 25px; vertical-align: middle; text-align: left;" colspan="2"><b>&nbsp;Step-1</b> Download Files from DataCore Secure ftp ...</td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left; vertical-align: top;">
                    &nbsp;DownLoad Files</td>
				<td style="width: 500px; text-align: left;">
                    This step will take several minutes. Once the step is launched you must wait until
                    it is finished.&nbsp;<br />
                    <asp:Button ID="btnDownLoad" runat="server" Text="Begin Download" BackColor="Cyan" Font-Names="Verdana" Font-Size="10px" ForeColor="GradientActiveCaption" ValidationGroup="DownLoad" Width="130px" /></td>
			</tr>
			</table>
			<!-- End pnlDownload Table -->
			</asp:Panel></td>
	</tr>
	<tr>
			<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
			<asp:Panel ID="pnlImport" runat="server" ScrollBars="None" BackColor="#FFFFC0" BorderColor="Blue" BorderWidth="1px" Width="100%">
			<!-- begin pnlImport Table -->
			<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="600" id="Table1">
			<tr>
				<td style="width: 600px; height: 25px; vertical-align: middle; text-align: left;" colspan="2">
                    <b>&nbsp;Step-2</b> Import Files to our Master Table ...</td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left; vertical-align: top;">
                    &nbsp;Import Files</td>
				<td style="width: 500px; text-align: left;">
                    <asp:Label ID="lblImportFile" runat="server" Text="Import Files"></asp:Label><br />
                    <asp:Button ID="btnImportFile" UseSubmitBehavior="false" runat="server" Text="Start Import" BackColor="Cyan" Font-Names="Verdana" Font-Size="10px" ForeColor="GradientActiveCaption" ValidationGroup="Import" Width="130px" /></td>
			</tr>
			</table>
			<!-- End pnlImport Table -->
			</asp:Panel> 			
			</td>
	</tr>
	<tr>
			<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
			<asp:Panel Visible="false" ID="pnlGenerateFile" runat="server" ScrollBars="None" BackColor="#FFFFC0" BorderColor="Blue" BorderWidth="1px" Width="100%">
			<!-- begin pnlDownload Table -->
			<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="600" id="Table2">
			<tr>
				<td style="width: 600px; height: 25px; vertical-align: middle; text-align: left;" colspan="2"><b>&nbsp;Step-3</b> Files Generated</td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left; vertical-align: top;"></td>
				<td style="width: 500px; text-align: left;">
                    <asp:Label ID="lblFileGenerated" runat="server" Text="Files"></asp:Label></td>
			</tr>
			</table>
			<!-- End pnlDownload Table -->
			</asp:Panel>			
			</td>
	</tr>
<tr>
			<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
<asp:Panel ID="pnlUploadFiles" runat="server" ScrollBars="None" BackColor="#FFFFC0" BorderColor="Blue" BorderWidth="1px" Width="100%">
			<!-- begin pnlPrintDate Table -->
			<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="600" id="Table6">
			<tr>
				<td style="width: 600px; height: 25px; vertical-align: middle; text-align: left;" colspan="2"><b>&nbsp;Upload Print Files</b></td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left; vertical-align: top;">
                    &nbsp;Files</td>
				<td style="width: 500px; text-align: left; vertical-align: top;">
                    <asp:Label ID="lblUploadFiles" runat="server"></asp:Label><br />
                    <asp:Button ID="btnUploadTextFile" runat="server" BackColor="Green" Height="22px" Text="Upload Print File"
                        ValidationGroup="genCsv" Width="130px" Font-Names="Verdana" Font-Size="10px" UseSubmitBehavior="False" ForeColor="White" /></td>
			</tr>
			</table>
			<!-- End pnlPrintDate Table -->
			</asp:Panel>				
			</td>
	</tr>					
	<tr>
			<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
			<asp:Panel ID="pnlPrintDate" runat="server" ScrollBars="None" BackColor="#80FFFF" BorderColor="Blue" BorderWidth="1px" Width="100%">
			<!-- begin pnlPrintDate Table -->
			<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="600" id="Table3">
			<tr>
				<td style="width: 600px; height: 25px; vertical-align: middle; text-align: left;" colspan="2"><b>&nbsp;Update Print Date</b></td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left; vertical-align: top;">
                    &nbsp;Print Date
                    <asp:RequiredFieldValidator ID="rfvPrintDate" runat="server" ControlToValidate="txtPrintDate"
                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="PrintDate"></asp:RequiredFieldValidator></td>
				<td style="width: 500px; text-align: left; vertical-align: top;">
                    <asp:TextBox ID="txtPrintDate" runat="server" ValidationGroup="PrintDate" Width="146px" Font-Names="Verdana" Font-Size="10px"></asp:TextBox>&nbsp;
                    <ajaxToolkit:CalendarExtender ID="myCalendar" runat="server" FirstDayOfWeek="Monday" PopupPosition="TopLeft"
                        TargetControlID="txtPrintDate">
                    </ajaxToolkit:CalendarExtender>
                    Batch
                    Id
                    <asp:DropDownList ID="drpBoxBatchID" runat="server" ValidationGroup="PrintDate" Width="185px" Font-Names="Verdana" Font-Size="10px">
                    </asp:DropDownList>
                    </td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left; vertical-align: top;">
                    &nbsp;Total Print
				</td>
				<td style="width: 500px; text-align: left; vertical-align: top;">
				<asp:TextBox ID="txtTotalPrint" runat="server" Width="90px" Font-Names="Verdana" Font-Size="10px">0</asp:TextBox>
				<br />
				<asp:Button ID="btnPrintDate" runat="server" BackColor="#FFFFC1" Height="22px" Text="Set Print Date"
                        ValidationGroup="PrintDate" Width="130px" Font-Names="Verdana" Font-Size="10px" UseSubmitBehavior="False" />
				</td> 
			</tr> 
			</table>
			<!-- End pnlPrintDate Table -->
			</asp:Panel>			
			</td>
	</tr>
	<tr>
			<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
			<asp:Panel ID="pnlBadAddess" runat="server" ScrollBars="None" BackColor="#80FFFF" BorderColor="Blue" BorderWidth="1px" Width="100%">
			<!-- begin pnlPrintDate Table -->
			<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="600" id="Table4">
			<tr>
				<td style="width: 600px; height: 25px; vertical-align: middle; text-align: left;" colspan="2"><b>&nbsp;Clear Print Date</b></td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left; vertical-align: top;">
                    &nbsp;Microchip Id
                    <asp:RequiredFieldValidator ID="rvfMicroChip" runat="server" ControlToValidate="txtMicroChipId"
                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="clrPrintDate"></asp:RequiredFieldValidator></td>
				<td style="width: 500px; text-align: left; vertical-align: top;">
                    <asp:TextBox ID="txtMicroChipId" runat="server" ValidationGroup="clrPrintDate" Width="146px" Font-Names="Verdana" Font-Size="10px"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp; &nbsp; Batch
                    Id
                    <asp:DropDownList ID="drpBadAddress" runat="server" ValidationGroup="clrPrintDate" Width="185px" Font-Names="Verdana" Font-Size="10px">
                    </asp:DropDownList><br />
                    <asp:Button ID="btnMicroChip" runat="server" BackColor="#FFFFC1" Height="22px" Text="Clear Print Date"
                        ValidationGroup="clrPrintDate" Width="130px" Font-Names="Verdana" Font-Size="10px" UseSubmitBehavior="False" /></td>
			</tr>
			</table>
			<!-- End pnlPrintDate Table -->
			</asp:Panel>			
			
			</td>
	</tr>
	<tr>
			<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
<asp:Panel ID="pnlGenerateOutFiles" runat="server" ScrollBars="None" BackColor="#80FFFF" BorderColor="Blue" BorderWidth="1px" Width="100%">
			<!-- begin pnlPrintDate Table -->
			<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="600" id="Table5">
			<tr>
				<td style="width: 600px; height: 25px; vertical-align: middle; text-align: left;" colspan="2"><b>&nbsp;Generate CSV files</b></td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left; vertical-align: top;">
                    &nbsp;Batch Id</td>
				<td style="width: 500px; text-align: left; vertical-align: top;">
                    <asp:DropDownList ID="drpGenCvsFiles" runat="server" ValidationGroup="genCsv" Width="185px" Font-Names="Verdana" Font-Size="10px">
                    </asp:DropDownList><br />
                    <asp:Label ID="lblDisplayCvsFiles" runat="server"></asp:Label><br />
                    <asp:Button ID="btnGenerateCsvFile" runat="server" BackColor="Green" Height="22px" Text="Generate Csv File"
                        ValidationGroup="genCsv" Width="130px" Font-Names="Verdana" Font-Size="10px" UseSubmitBehavior="False" ForeColor="White" /></td>
			</tr>
			</table>
			<!-- End pnlPrintDate Table -->
			</asp:Panel>				
			</td>
	</tr>
		<tr>
			<td style="width: 600px; text-align: left; vertical-align: top; font-family: Verdana; font-size: 11px;">
<asp:Panel ID="pnlUploadCsvFile" runat="server" ScrollBars="None" BackColor="#80FFFF" BorderColor="Blue" BorderWidth="1px" Width="100%">
			<!-- begin pnlPrintDate Table -->
			<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="600" id="Table7">
			<tr>
				<td style="width: 600px; height: 25px; vertical-align: middle; text-align: left;" colspan="2"><b>&nbsp;Upload CSV files</b></td>
			</tr>
			<tr>
				<td style="width: 100px; text-align: left; vertical-align: top;">
                    &nbsp;Files</td>
				<td style="width: 500px; text-align: left; vertical-align: top;">
                    <asp:Label ID="lblUploadCsv" runat="server"></asp:Label><br />
                    <asp:Button ID="btnUploadCvsFile" runat="server" BackColor="Green" Height="22px" Text="Upload Csv File"
                        ValidationGroup="genCsv" Width="130px" Font-Names="Verdana" Font-Size="10px" UseSubmitBehavior="False" ForeColor="White" /></td>
			</tr>
			</table>
			<!-- End pnlPrintDate Table -->
			</asp:Panel>				
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
    <td style="width: 100%; background-color: #006AB6; height: 25px; text-align: center;" colspan="3"><font color="#FFFFFF" size="1">&copy; 2007-2009, Vet Metrics Inc. All rights reserved.
    </font></td>
  </tr>
</table>    
</div>
    </form>
    
</body>
</html>
