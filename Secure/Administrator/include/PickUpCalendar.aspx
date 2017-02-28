<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PickUpCalendar.aspx.vb" Inherits="Secure_include_PickUpCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Calendar</title>
    <script type="text/javascript">
    function RtnSelectedDate()
    {
      //var PickUpDate = document.forms[0].elements["selectedDate"].value
      // document.getElementsByName("selectedDate")
      // document.forms["frmCalendar"].namedItem["selectedDate"];
      
      window.opener.document.forms["<%= strFrmName %>"].elements["<%= strControlName %>"].value = "<%= strSelCaledarDate %>";
      window.close();
    
    }
        
    function ClsWindow()
    {
        window.close();
    }
    </script>
</head>
<body style="font-family : Verdana; font-size : 10px;">
    <form id="frmCalendar" runat="server">
    <div>
    <table>
            <tr>
                <td align="Center">
                    Month:
                    <asp:DropDownList style="font-family : Verdana; font-size : 10px;" id="drpCalendarMonth" runat="server" OnSelectedIndexChanged="PickUpMonth_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    Year:
                    <asp:DropDownList style="font-family : Verdana; font-size : 10px;" id="drpCalendarYear" runat="server" OnSelectedIndexChanged="PickUpMonth_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="2005">2005</asp:ListItem>
                        <asp:ListItem Value="2006">2006</asp:ListItem>
                        <asp:ListItem Value="2007">2007</asp:ListItem>
                        <asp:ListItem Value="2008">2008</asp:ListItem>
                        <asp:ListItem Value="2009">2009</asp:ListItem>
                        <asp:ListItem Value="2010">2010</asp:ListItem>
                        <asp:ListItem Value="2011">2011</asp:ListItem>
                        <asp:ListItem Value="2012">2010</asp:ListItem>
                        <asp:ListItem Value="2013">2011</asp:ListItem>         
                        <asp:ListItem Value="2014">2014</asp:ListItem>
                        <asp:ListItem Value="2015">2015</asp:ListItem>                                                                   
              </asp:DropDownList>
          </td>
            </tr>
            <tr>
                <td align="Center">
                    <asp:Calendar id="PickUpCalendar" runat="server" BorderWidth="1px" BackColor="#AED7FF" Width="220px" DayNameFormat="FirstLetter" ForeColor="#3211EE" Height="200px" Font-Size="8pt" Font-Names="Verdana" BorderColor="#3211EE" ShowGridLines="True" OnSelectionChanged="PickUpCalendar_SelectionChanged">
                        <SelectorStyle backcolor="Red"></SelectorStyle>
                        <NextPrevStyle font-size="9pt" forecolor="#FFFFCC"></NextPrevStyle>
                        <DayHeaderStyle height="1px" backcolor="#FFCC66"></DayHeaderStyle>
                        <SelectedDayStyle font-bold="True" backcolor="#CCCCFF"></SelectedDayStyle>
                        <TitleStyle font-size="9pt" font-bold="True" forecolor="Black" backcolor="#3211EE"></TitleStyle>
                        <OtherMonthDayStyle forecolor="Red"></OtherMonthDayStyle>
                    </asp:Calendar>
                </td>
            </tr>
            <tr>
                <td align="Center">
                    <input id="btnRtnSelectedDate" onclick="Javascript:RtnSelectedDate()" type="button" value="Select" runat="Server" />&nbsp; 
                    <input id="btnClsWindow" onclick="Javascript:ClsWindow()" type="button" value="Close" runat="Server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
