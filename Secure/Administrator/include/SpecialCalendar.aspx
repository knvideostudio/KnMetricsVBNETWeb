<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SpecialCalendar.aspx.vb" Inherits="VeterinaryMetrics.Utility.Common.SpecialCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Special Calendar</title>
</head>
<body style="font-family : Verdana; font-size : 10px;">
    <form id="frmCalendar" runat="server">
    <div>
 <table>
            <tr>
                <td align="Center">
                    Month:
                    <asp:DropDownList style="font-family : Verdana; font-size : 11px;" Width="90px" Height="22px" ID="drpListMonth" runat="server" AutoPostBack="True" />
                    &nbsp;
                    Year:
                    <asp:DropDownList style="font-family : Verdana; font-size : 10px;" Width="60px" Height="22px" ID="drpListYear" runat="server" AutoPostBack="True" />

          </td>
            </tr>
            <tr>
                <td align="Center">    
        <asp:Calendar ID="MyCalendar" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="FirstLetter" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px" NextMonthText="" PrevMonthText="" VisibleDate="2007-02-23" FirstDayOfWeek="Monday" >
            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
            <WeekendDayStyle BackColor="#CCCCFF" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True"
                Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
        </asp:Calendar>
        <asp:Label ID="lblMySelectedDate" runat="server" /> 
        <br />  
        <input id="mychosendate" name="mychosendate" type="hidden" runat="server" />
        <asp:Literal ID="MyLiteral" runat="server"></asp:Literal> 
                    <br />
                    <asp:Button ID="btnWinClose" runat="server" Text="Close Window" /></td>
            </tr>
            </table>          
        </div>
    </form>
</body>
</html>
