<%@ Page StylesheetTheme="DefaultPages" Language="VB" AutoEventWireup="false" CodeFile="ReOrderPetId.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.ReOrderPetId" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ReOrder Pet Id</title>
    <script language="javascript" type="text/jscript">
    function cb(rowID) 
    { 
        var color = document.getElementById(rowID).style.backgroundColor;
        var currentValue;
        var myElement = document.forms[0].namedItem(rowID);
  
        // myElement.click();
        
       if (color != 'lightgreen') 
       {
            old5Color = color;
            // myElement.click();
       }
                
       if (color == 'lightgreen') 
       {
              document.getElementById(rowID).style.backgroundColor = old5Color; 
              currentValue = document.forms[0].txtSelectedValue.value;  
              currentValue = currentValue.replace(rowID + ';', '');
              document.forms[0].txtSelectedValue.value = currentValue;
       }
       else 
       {
            currentValue = document.forms[0].txtSelectedValue.value;
            if (currentValue == null)
            {
                currentValue = rowID;
                document.forms[0].txtSelectedValue.value = rowID;
            }
            else
            {
                currentValue = document.forms[0].txtSelectedValue.value 
                currentValue = currentValue + rowID + ';' ;
                document.forms[0].txtSelectedValue.value = currentValue;
            }
            document.getElementById(rowID).style.backgroundColor = 'lightgreen';
       }
       var finalString =  currentValue.substring(0, currentValue.length - 1);
    }
  </script>
</head>
<body>
    <form id="frmReorderPetId" runat="server">
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
                        &nbsp;<a href="../../Default.aspx"><font color="#FFFFFF">Main Menu</font></a><font color="#FFFFFF">&nbsp;|&nbsp;</font> 
                        <a href="ReorderPetIdPending.aspx"><font color="#FFFFFF">Pending Pet-Ids</font></a>
                        </td>
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
                    </td>
                </tr>
                <tr>
                    <td width="100%" colspan="3" align="center" valign="top" style="height: 16px">
                        <br />
                        <table style="width: 600px;" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="width: 600px; height: 25px; text-align: left; background-color: #6baaf5;" colspan="3">
                                    <b>&nbsp;Re-Order Pet Ids:</b>&nbsp;
                                    <asp:Label ID="lblPetIdTotal" runat="server" EnableViewState="false"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 100px; vertical-align: top; text-align: right;">
                                    Last Name:&nbsp;
                                </td> 
                                <td style="width: 500px; text-align: left;" colspan="2">
                                    <asp:TextBox ID="txtSearchName" runat="server" Width="171px" AutoCompleteType="Search" ValidationGroup="Search"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="rfValidatorName" runat="server" ControlToValidate="txtSearchName" Display="Dynamic"
                                        ErrorMessage="Enter Value" Font-Bold="False" Font-Size="Smaller" ValidationGroup="Search"></asp:RequiredFieldValidator><br />
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label></td>                                 
                            </tr> 
                             <tr>
                                <td style="width: 100px; text-align: right;">
                                    &nbsp;
                                </td>
                                <td style="width: 250px; text-align: left;">
                                <asp:Button ID="btnSearch" runat="server" Text="New Search" ValidationGroup="Search" /></td>
                                <td style="width: 250px; text-align: left;"><asp:Button ID="btnRequestPetId" runat="server" Text="Request Re-Order" Width="131px" /></td> 
                            </tr> 
                            <tr>
                                <td style="width: 600px; text-align: left;"  colspan="3">
                                    <asp:Label ID="lblPetIdText" runat="server"></asp:Label><br />
                                    <asp:HiddenField ID="txtSelectedValue" runat="server"></asp:HiddenField>
                                    </td>
                            </tr>
                        </table>
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
