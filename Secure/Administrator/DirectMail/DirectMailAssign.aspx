<%@ Page enableEventValidation="false" Language="VB" AutoEventWireup="false" CodeFile="DirectMailAssign.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.DirectMailAssign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DirectMail - Assign Practices</title>
<script language="javascript" type="text/javascript">
function SelectText()  
{

    var boxLength = document.frmDmAssign.PracticeSel.length;
    var selectedItem = document.frmDmAssign.PractListBox.selectedIndex;
    var selectedText = document.frmDmAssign.PractListBox.options[selectedItem].text;
    var selectedValue = document.frmDmAssign.PractListBox.options[selectedItem].value;
    var i;
    var isNew = true;
    if (boxLength != 0) {
        for (i = 0; i < boxLength; i++) {
            thisitem = document.frmDmAssign.PracticeSel.options[i].text;
            if (thisitem == selectedText) {
            isNew = false;
            break;
                  }
           }
     } 
    if (isNew) {
        newoption = new Option(selectedText, selectedValue, false, false);
        document.frmDmAssign.PracticeSel.options[boxLength] = newoption;
    }
    
    document.frmDmAssign.PractListBox.selectedIndex=-1;
}

function RemoveSelected() {
    var boxLength = document.frmDmAssign.PracticeSel.length;
    arrSelected = new Array();
    var count = 0;
    
    for (i = 0; i < boxLength; i++) {
        if (document.frmDmAssign.PracticeSel.options[i].selected) {
        arrSelected[count] = document.frmDmAssign.PracticeSel.options[i].value;
        }
    count++;
    }

    var x;
    for (i = 0; i < boxLength; i++) {
    for (x = 0; x < arrSelected.length; x++) {
    if (document.frmDmAssign.PracticeSel.options[i].value == arrSelected[x]) {
    document.frmDmAssign.PracticeSel.options[i] = null;
         }
    }
    boxLength = document.frmDmAssign.PracticeSel.length;
   }
}


function SaveValues() {
    var strValues = "";
    var boxLength = document.frmDmAssign.PracticeSel.length;
    var count = 0;

    if (boxLength != 0) {
    for (i = 0; i < boxLength; i++) {
    if (count == 0) {
        strValues = document.frmDmAssign.PracticeSel.options[i].value;
        } else {
        strValues = strValues + "," + document.frmDmAssign.PracticeSel.options[i].value;
        }
        count++;
       }
    }
    if (strValues.length == 0) {
         alert("You have not made any selections!");
    }
    else {
     // alert("Here are the values you've selected:\r\n" + strValues);
     document.frmDmAssign.txtValues.value = strValues;
    }
}
</script>        
</head>
<body style="font-family: Verdana; font-size: 10pt;" bottommargin="0" leftmargin="0"
    topmargin="0" rightmargin="0">
    <form id="frmDmAssign" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                width="100%" id="AutoNumber1">
                <tr>
                    <td width="34%" height="100" bgcolor="#000000">
                        &nbsp;&nbsp;<img src="../../images/bwLogo.bmp" /></td>
                    <td width="33%" height="100" bgcolor="#000000">
                        &nbsp;</td>
    <td width="33%" height="100" bgcolor="#000000" align="right" valign="bottom"><a href="../../SignOut.aspx"><font color="#FFFFFF">Sign Out</font></a>&nbsp;&nbsp;</td>

                </tr>
                <tr>
                    <td width="100%" colspan="3" bgcolor="#000000" height="25" align="left" valign="middle">
                        &nbsp;<a href="../../Default.aspx"><font color="#FFFFFF">Main Menu</font></a><font color="#FFFFFF"> 
                            | <a href="DirectMail.aspx"><font color="#FFFFFF">Direct Mail</font></a></font></td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" bgcolor="#FFFFFF" height="2">
                        <img src="../../images/pixel.gif" /></td>
                </tr>
                <tr>
                    <td width="34%" bgcolor="#006AB6" height="25" align="left">
                        &nbsp;<font color="#FFFFFF">User login:&nbsp;<asp:Label ID="lblUserName" runat="server"
                            Font-Bold="True" Text="No User"></asp:Label></font></td>
                    <td width="66%" colspan="2" bgcolor="#006AB6" height="25" align="right">
                        &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" align="center" valign="top" style="height: 16px">
                        <br />
                        <table style="width: 600px;" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="width: 600px; height: 25px; text-align: left;" colspan="2">
                                    Direct-Mail <strong>Add new Practices</strong>
                                    <br />
                                    <asp:Label ID="lblMessageShow" runat="server" EnableViewState="false"></asp:Label>
                                    <br />
                                    &nbsp;</td>
                            </tr>
                            <tr>
                             <td valign="top" style="width: 300px; text-align: left;">
                                 Avaiable Practices:<asp:ListBox ID="PractListBox" runat="server" Height="168px" Width="300px"></asp:ListBox></td>
                                <td style="width: 300px; text-align: left;">
                                 Selected Practices:<br />
                                    <asp:ListBox ID="PracticeSel" runat="server" SelectionMode="Multiple" Height="168px" Width="300px"></asp:ListBox></td> 
                            </tr>                            
                            <tr>
                             <td valign="top" style="width: 300px; text-align: left;"><asp:HiddenField ID="txtValues" runat="server" EnableViewState="false" /></td>
                                <td style="width: 300px; text-align: left;">
                                    <asp:Button ID="btnPracticeAdd" runat="server" Text="Assign Practices" />
                                    <input type="button" value="Remove" onclick="RemoveSelected();" /></td> 
                            </tr>
                            <td valign="top" style="width: 100px; text-align: left;">
                                 </td>
                                <td style="width: 500px; text-align: left;">
                                    &nbsp;</td> 
                            <tr>
                                <td style="width: 200px; text-align: left;"  colspan="2">
                                    </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" bgcolor="#000000" height="3">
                        <img src="../../images/pixel.gif" /></td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" bgcolor="#006AB6" align="center" height="25">
                        <font color="#FFFFFF" size="1">&copy;2007-2009, Vet Metrics Inc. All rights reserved. </font>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
