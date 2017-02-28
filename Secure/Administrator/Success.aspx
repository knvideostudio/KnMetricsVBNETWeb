<%@ Page Language="VB" %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        
        lblUserName.Text = User.Identity.Name
        
        If IsNothing(Session("strSuccess")) Then
            ' do
        Else
            lblMessage.Text = CType(Session("strSuccess"), String)
        End If
    End Sub
    
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Success</title>
</head>
<body style="font-family : Verdana; font-size : 10pt;" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
 
    <form id="form1" runat="server">
    <div>
 <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="AutoNumber1">
  <tr>
    <td width="34%" height="100" bgcolor="#000000">
        &nbsp;&nbsp;<img src="../../images/bwLogo.bmp" /></td>
    <td width="33%" height="100" bgcolor="#000000">&nbsp;</td>
    <td width="33%" height="100" bgcolor="#000000" align="right" valign="bottom"><a href="../../SignOut.aspx"><font color="#FFFFFF">Sign Out</font></a>&nbsp;&nbsp;</td>
  </tr>
      <tr>
    <td width="100%" colspan="3" bgcolor="#000000" height="25" align=left valign=middle >&nbsp;<a href="../../Default.aspx"><font color="#FFFFFF">Main Menu</font></a>
        <a href="ReminderDrop.aspx"><font color="#FFFFFF">Remainder Drop</font></a> <a href="AddNewReminder.aspx"><font color="#FFFFFF">Add New Remainder Drop</font></a>&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" colspan="3" bgcolor="#FFFFFF" height="2"><img src="../../images/pixel.gif" /></td>
  </tr>
  <tr>
    <td width="34%" bgcolor="#006AB6" height="25" align="left">&nbsp;<font color="#FFFFFF">User 
    login:&nbsp;<asp:Label ID="lblUserName" runat="server" Font-Bold="True" Text="No User"></asp:Label></font></td>
    <td width="66%" colspan="2" bgcolor="#006AB6" height="25" align="right">
    &nbsp;
    </td>
    
  </tr>
  <tr>
    <td width="100%" colspan="3" align="center" valign="top" style="height: 16px">
   &nbsp; 
   <br />
   <br />
   <br />
   <br />   
        <asp:Label ID="lblMessage" runat="server" Text="No message"></asp:Label>

    
    <br />
    <br />
    <br />
    <br />
    <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
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
    <td width="100%" colspan="3" bgcolor="#000000" height="3"><img src="../../images/pixel.gif" /></td>
  </tr>
  <tr>
    <td width="100%" colspan="3" bgcolor="#006AB6" align="center" height="25"><font color="#FFFFFF" size="1">&copy;2007-2009, Vet Metrics Inc. All rights reserved.
    </font></td>
  </tr>
</table>    
</div>
    </form>
    
</body>
</html>
