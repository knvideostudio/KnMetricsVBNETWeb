<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ReminderDrop.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.DisplayReminderDrop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Vet Metrics - Reminder Drops</title>
    <style type="text/css">
    a 
    {
    }
         
    .grid
    {
       font : 10px Verdana, Arial, Sans-Serif;
    }
        
    .grid td, .grid th
    {
        padding : 2px;
    }
            
.header
{
   text-align : left;
   color : white;
   background : #B3B3FF;
}
        
.row td
{
   border-bottom : solid 1px blue;
}
        
.alternating
{
   background-color : #eeeeee;
}

.alternating td
{
    border-bottom : solid 1px blue;
}        

thead th, thead th.locked	{
font-size: 10px;
font-weight: bold;
text-align: center;
background-color: navy;
color: white;
border-right: 1px solid silver;
position: relative;
cursor: default; 
}
	
thead th 
{
top: expression(document.getElementById("tbl-container").scrollTop-2);
z-index: 20;
}

thead th.locked 
{
    z-index: 30;
}

td.locked,  th.locked
{
background-color: #AAFFAA;
font-weight: bold;
border-right: 1px solid green;
left: expression(parentNode.parentNode.parentNode.parentNode.scrollLeft); 
position: relative;
z-index: 10;
}
</style> 
    <script language="javascript" type="text/javascript">
        var MyEditWindow;
        var AddEditWindow;
        
        function OpenAddEditWindow(reminderId, control, postBack)
        {
            AddEditWindow = window.open('AddNewReminder.aspx?formName=' + document.forms[0].name  + '&id=' + reminderId + '&type=edit&postBack=' + postBack + '&control='+ control, 'EditReminderDrop', 'width=930,height=750,status=yes,toolbar=no,resizable=yes,menubar=no,location=no,scrollbars=yes,left=50,top=50');
            AddEditWindow.focus();
        }
        
        function MyPostBack(formName, control, postBack)
        {
            //eval('var theForm = document.' + formName + ';');
            //alert(control)
            // session is stored the current page of gridview
           // alert(<%= CType(Session("CurrentPage"), String) %>)
           
           if (postBack)
             __doPostBack(control, 'Page$' + <%= CType(Session("CurrentPage"), String) %> );
           
            AddEditWindow.close();
           
        }
        
        
        function OpenEditWindow(id)
        {
            MyEditWindow = window.open('AddNewReminder.aspx?id=' + id + '&type=edit&post=2', 'EditReminderDrop', 'width=930,height=750,status=yes,toolbar=no,resizable=yes,menubar=no,location=no,scrollbars=yes,left=50,top=50');
            MyEditWindow.focus();
        }
        
        function CloseWindow()
        {
            if (MyEditWindow.closed == false)
            {
                MyEditWindow.close();
                //__doPostBack('MyGrdView','');

            }
        }
    
    function lockCol(tblID) {

	var table = document.getElementById(tblID);
	var button = document.getElementById('toggle');
	var cTR = table.getElementsByTagName('tr');  //collection of rows

	if (table.rows[0].cells[0].className == '') {
		for (i = 0; i < cTR.length; i++)
			{
			var tr = cTR.item(i);
			tr.cells[0].className = 'locked';
			tr.cells[1].className = 'locked';
			tr.cells[2].className = 'locked';
			}
		button.innerText = "Unlock First Column";
		}
		else {
		for (i = 0; i < cTR.length; i++)
			{
			var tr = cTR.item(i);
			tr.cells[0].className = '';
			tr.cells[1].className = '';
			tr.cells[2].className = '';
			}
		button.innerText = "Lock First Column";
		}
}

    </script>   
</head>
<body onunload="CloseWindow();" style="font-family: Verdana; font-size: 10pt; margin-bottom: 0px; margin-left: 0px; margin-right: 0px; margin-top: 0px;">
    <form id="frmRemainderDrop" runat="server">
  <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="AutoNumber1">
  <tr>
    <td style="width: 34%; height: 100px; background-color: #000000;">
        &nbsp;&nbsp;<img src="../../images/bwLogo.bmp" alt="" /></td>
    <td style="width: 33%; height: 100px; background-color: #000000;">&nbsp;</td>
    <td style="width: 33%; height: 100px; background-color: #000000; text-align: right; vertical-align: bottom;"><a href="../../SignOut.aspx"><font color="#FFFFFF">Sign Out</font></a>&nbsp;&nbsp;</td>
  </tr>
    <tr>
    <td style="width: 100%; height: 25px; background-color: #000000; text-align: left; vertical-align: middle;" colspan="3">&nbsp;
    <a href="../../Default.aspx"><font color="#FFFFFF">Main Menu</font></a> | <a href="AddNewReminder.aspx"><font color="#FFFFFF">Add new Reminder drop</font></a>&nbsp;</td>
  </tr>
  <tr>
    <td style="width: 100%; height: 2px; background-color: #FFFFFF;"><img src="../../images/pixel.gif" alt="" /></td>
  </tr>
  <tr>
    <td style="width: 34%; height: 25px; background-color: #006AB6; text-align: left;">&nbsp;<font color="#FFFFFF">User 
    login:&nbsp;<asp:Label ID="lblUserName" runat="server" Font-Bold="True" Text="No User"></asp:Label></font></td>
    <td style="width: 66%; height: 25px; background-color: #006AB6; text-align: left;" colspan="2">
    &nbsp;
    </td>
    
  </tr>
  <tr>
    <td colspan="3" style="height: 16px; width: 100%; text-align: left; vertical-align: top;">
   &nbsp;
   <button onClick="lockCol('MyGrdView')"  name="toggle" id="toggle">Lock First 3 Columns</button>
    <br />
         Mailer ID: <asp:DropDownList ID="drpMailer" runat="server" AutoPostBack="True" Width="228px" ></asp:DropDownList>
 <div id="tbl-container" style="vertical-align: top; width: 950px; overflow-x: scroll;">
       <asp:GridView ID="MyGrdView" AutoGenerateColumns="False" Width="2500px" 
       GridLines="None" 
       runat="server" 
       AllowSorting="True" 
       CssClass="grid" 
       HeaderStyle-CssClass="header" 
       RowStyle-CssClass="row" 
       AlternatingRowStyle-CssClass="alternating" 
       DataKeyNames="DropID"  
       AllowPaging="True" 
       PageSize="15"
       >
       <SelectedRowStyle CssClass="dd" />
       <HeaderStyle Height="25px" CssClass="header" />     
       <RowStyle CssClass="row" />
       <AlternatingRowStyle CssClass="alternating" />
       <Columns>

           <asp:TemplateField HeaderText="E">
             <HeaderStyle HorizontalAlign="Center" />
             <ItemStyle HorizontalAlign="Center" />
             <ItemTemplate>
                <asp:HyperLink id="hplEdit" NavigateUrl='<%# "javascript:OpenAddEditWindow(" & DataBinder.Eval(Container.DataItem, "DropID") & ", &#39;MyGrdView&#39;, true);" %>' Text="Edit" runat="server" /> 
             </ItemTemplate>
           </asp:TemplateField>

           <asp:TemplateField HeaderText="ID" SortExpression="DropID">
               <ItemStyle HorizontalAlign="Left" />
               <HeaderStyle HorizontalAlign="Left" />
               <ItemTemplate>
                   <asp:Label ID="lblDropID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DropID") %>'></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Description" SortExpression="DropDescription">
             <HeaderStyle HorizontalAlign="Left" />
             <ItemStyle HorizontalAlign="Left" Width="400px" Wrap="True" />
               <ItemTemplate>
                   <asp:Label ID="lblDropDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DropDescription") %>'></asp:Label>
               </ItemTemplate>               
               <EditItemTemplate>
                   <asp:TextBox ID="txtDropDescription" Width="150" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DropDescription") %>'></asp:TextBox>
               </EditItemTemplate>
          </asp:TemplateField>
            <asp:TemplateField HeaderText="Web" SortExpression="ActiveWeb">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:CheckBox ID="chkActiveWeb" Enabled="false" 
                    AutoPostBack="false" EnableViewState="false" 
                    ToolTip='<%# DisplayDiscontinuedAsYESorNO(CType(DataBinder.Eval(Container.DataItem, "ActiveWeb"), Object)) %>' 
                    Checked='<%# DisplayInCheckBoxYESorNO(CType(DataBinder.Eval(Container.DataItem, "ActiveWeb"), Object)) %>' runat="server" />  
                </ItemTemplate>
               <EditItemTemplate>
                    <asp:CheckBox ID="chkActiveWebEdit" runat="server" 
                    ToolTip='<%# DisplayDiscontinuedAsYESorNO(CType(DataBinder.Eval(Container.DataItem, "ActiveWeb"), Object)) %>' 
                    Checked='<%# DisplayInCheckBoxYESorNO(CType(DataBinder.Eval(Container.DataItem, "ActiveWeb"), Object)) %>'
                    />  
                </EditItemTemplate>
                
            </asp:TemplateField> 
            
            <asp:TemplateField HeaderText="Drop" SortExpression="ActiveDrop">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:CheckBox ID="chkActiveDrop" Enabled="false" 
                    AutoPostBack="false" EnableViewState="false" 
                    ToolTip='<%# DisplayDiscontinuedAsYESorNO(CType(DataBinder.Eval(Container.DataItem, "ActiveDrop"), Object)) %>' 
                    Checked='<%# DisplayInCheckBoxYESorNO(CType(DataBinder.Eval(Container.DataItem, "ActiveDrop"), Object)) %>' runat="server" />  
                 </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="chkActiveDropEdit" runat="server"
                    ToolTip='<%# DisplayDiscontinuedAsYESorNO(CType(DataBinder.Eval(Container.DataItem, "ActiveDrop"), Object)) %>' 
                    Checked='<%# DisplayInCheckBoxYESorNO(CType(DataBinder.Eval(Container.DataItem, "ActiveDrop"), Object)) %>'
                     /> 
                </EditItemTemplate>
            </asp:TemplateField>            
           <asp:TemplateField HeaderText="Pull Date">
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemStyle HorizontalAlign="Center" />
                <ItemTemplate>
                   <asp:Label ID="lblPullDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PullDate") %>'></asp:Label>
               </ItemTemplate>              
               <EditItemTemplate>
                   <asp:TextBox ID="txtPullDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "PullDate") %>'></asp:TextBox>
               </EditItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="OnLine Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" Width="220px" />
               <ItemTemplate>
                   <asp:Label ID="lblOnLineDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OnLineDate", "{0:m/dd/yyyy hh:mm}") %>'></asp:Label>
               </ItemTemplate>
               <EditItemTemplate>
                   <asp:TextBox ID="txtOnLineDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "OnLineDate", "{0:f}") %>'></asp:TextBox>
               </EditItemTemplate>               
           </asp:TemplateField>
           <asp:TemplateField HeaderText="OffLine Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" Width="220px" />
               <ItemTemplate>
                   <asp:Label ID="lblOffLineDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OfflineDate", "{0:m/dd/yyyy hh:mm}") %>'></asp:Label>
               </ItemTemplate>
               <EditItemTemplate>
                   <asp:TextBox ID="txtOffLineDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "OfflineDate", "MM/dd/yyyy hh:mm") %>'></asp:TextBox>
               </EditItemTemplate>               
           </asp:TemplateField>
           <asp:TemplateField HeaderText="C-Start Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" Width="100px" />
               <ItemTemplate>
                   <asp:Label ID="lblCstartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CStartDate") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                   <asp:TextBox ID="txtCstartDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "CStartDate") %>'></asp:TextBox>
                   &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtCstartDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>              
           </asp:TemplateField>
           <asp:TemplateField HeaderText="C-End Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" Width="100px" />
               <ItemTemplate>
                   <asp:Label ID="lblCendDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CEndDate") %>'></asp:Label>
               </ItemTemplate>
               <EditItemTemplate>
                   <asp:TextBox ID="txtCendDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "CEndDate") %>'></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtCendDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>               
           </asp:TemplateField>
           <asp:TemplateField HeaderText="NR1-Start Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblNR1startDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NR1StartDate") %>'></asp:Label>
               </ItemTemplate>
               <EditItemTemplate>
                   <asp:TextBox ID="txtNR1startDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "NR1StartDate") %>'></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtNR1startDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>               
           </asp:TemplateField>
           <asp:TemplateField HeaderText="NR1-End Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblNR1endDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NR1EndDate") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                   <asp:TextBox ID="txtNR1endDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "NR1EndDate") %>'></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtNR1endDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>              
           </asp:TemplateField>
           <asp:TemplateField HeaderText="NR2-Start Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblNR2startDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NR2StartDate") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                   <asp:TextBox ID="txtNR2startDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "NR2StartDate") %>'></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtNR2startDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
              </EditItemTemplate>              
           </asp:TemplateField>
           <asp:TemplateField HeaderText="NR1-End Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblNR2endDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NR2EndDate") %>'></asp:Label>
               </ItemTemplate>
                 <EditItemTemplate>
                   <asp:TextBox ID="txtNR2endDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "NR2EndDate") %>'></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtNR2endDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>             
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Print Run Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblPrintRunDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PrintRunDate") %>'></asp:Label>
               </ItemTemplate>
                 <EditItemTemplate>
                   <asp:TextBox ID="txtPrintRunDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "PrintRunDate") %>'></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtPrintRunDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>             
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Print Run Series">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblPrintRunSeries" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PrintRunSeries") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                   <asp:TextBox ID="txtPrintRunSeries" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PrintRunSeries") %>'></asp:TextBox>
               </EditItemTemplate>              
           </asp:TemplateField>
           
           <asp:TemplateField HeaderText="Mailer ID">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblMailerTypeID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MailerTypeID") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
               <asp:DropDownList ID="drpMailerIDEdit" SelectedValue='<%# DataBinder.Eval(Container.DataItem, "MailerTypeID") %>' runat="server" ToolTip="Mailer ID" Width="80px" TabIndex="1" >
                    <asp:ListItem />
                    <asp:ListItem Text="AW" Value="AW" />
                    <asp:ListItem Text="CO" Value="CO" />
                    <asp:ListItem Text="DN" Value="DN" />
                    <asp:ListItem Text="DO" Value="DO" />
                    <asp:ListItem Text="HW" Value="HW" />
                    <asp:ListItem Text="MC" Value="MC" />
                    <asp:ListItem Text="PM" Value="PM" />
                    <asp:ListItem Text="SR" Value="SR" />  
                    <asp:ListItem Text="WB" Value="WB" />  
                    <asp:ListItem Text="WF" Value="WF" />              
                </asp:DropDownList>
               </EditItemTemplate>              
           </asp:TemplateField>           
           
           <asp:TemplateField HeaderText="VM NPC">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblVM_NCP" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VM_NPC") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="drpVmNpcEdit" SelectedValue='<%# DataBinder.Eval(Container.DataItem, "VM_NPC") %>' runat="server" Width="80px">
                    <asp:ListItem />
                    <asp:ListItem Text="IND" Value="IND" />
                    <asp:ListItem Text="NPC" Value="NPC" />
                    <asp:ListItem Text="VM" Value="VM" />
                </asp:DropDownList>
               </EditItemTemplate>              
           </asp:TemplateField> 

           <asp:TemplateField HeaderText="Exp 1">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblExp1Date" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Exp1Date") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                   <asp:TextBox ID="txtExp1Date" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "Exp1Date") %>'></asp:TextBox>
                   &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtExp1Date')"><img src="images/sCalendar.gif" border="0" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>              
           </asp:TemplateField> 
           
           <asp:TemplateField HeaderText="Exp 2">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblExt2Date" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Exp2Date") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                   <asp:TextBox ID="txtExp2Date" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "Exp2Date") %>'></asp:TextBox>
                   &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtExp2Date')"><img src="images/sCalendar.gif" border="0" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>              
           </asp:TemplateField> 
           
           <asp:TemplateField HeaderText="Exp 3">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblExp3Date" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Exp3Date") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                   <asp:TextBox ID="txtExp3Date" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "Exp3Date") %>'></asp:TextBox>
                   &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtExp3Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>              
           </asp:TemplateField>                       


           
           <asp:TemplateField HeaderText="Report C Start Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblRptCStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Report_CStartDate") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                   <asp:TextBox ID="txtRptCStartDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "Report_CStartDate") %>'></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtRptCStartDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>              
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Report C End Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblRptCEndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Report_CEndDate", "{0:m/dd/yyyy hh:mm}") %>'></asp:Label>
               </ItemTemplate>
               <EditItemTemplate>
                   <asp:TextBox ID="txtRptCEndDate" runat="server" BorderStyle="Solid" BorderWidth="1px"
                       Text='<%# DataBinder.Eval(Container.DataItem, "Report_CEndDate", "{0:m/dd/yyyy hh:mm}") %>'
                       Width="80px"></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtRptCEndDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>               
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Report RCY Start Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblRptRCYStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Report_RCYStartDate") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                   <asp:TextBox ID="txtRptRCYStartDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "Report_RCYStartDate") %>'></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtRptRCYStartDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
                </EditItemTemplate>              
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Report RCY End Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblRptRCYEndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Report_RCYEndDate") %>'></asp:Label>
               </ItemTemplate>
               <EditItemTemplate>
                   <asp:TextBox ID="txtRptRCYEndDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "Report_RCYEndDate") %>'></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtRptRCYEndDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>               
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Report C End Date Short">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblRptCEndShortDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RptMyShortDate", "{0:mm/dd/yyyy hh:mm}") %>'></asp:Label>
               </ItemTemplate>
                <EditItemTemplate>
                   <asp:TextBox ID="txtRptCEndShortDate" runat="server" BorderStyle="Solid" BorderWidth="1px"
                       Text='<%# DataBinder.Eval(Container.DataItem, "RptMyShortDate", "{0:mm/dd/yyyy hh:mm}") %>'
                       Width="80px"></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtRptCEndShortDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>              
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Report NR3 Start Date">
               <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblRptNR3StartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Report_NR3StartDate") %>'></asp:Label>
               </ItemTemplate>
               <EditItemTemplate>
                   <asp:TextBox ID="txtRptNR3StartDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "Report_NR3StartDate") %>'></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtRptNR3StartDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>               
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Report NR3 End Date">
              <ItemStyle HorizontalAlign="Center" />
               <HeaderStyle HorizontalAlign="Center" />
               <ItemTemplate>
                   <asp:Label ID="lblRptNR3EndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Report_NR3EndDate", "{0:m/dd/yyyy hh:mm}") %>'></asp:Label>
               </ItemTemplate>
               <EditItemTemplate>
                   <asp:TextBox ID="txtRptNR3EndDate" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "Report_NR3EndDate", "{mm/dd/yyyy hh:mm}") %>'></asp:TextBox>
                    &nbsp;<a href="javascript:OpenCalendar('<%# CType(Session("RowIndex"), String) %>_txtRptNR3EndDate')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar" /></a>
               </EditItemTemplate>               
           </asp:TemplateField>
       </Columns> 
       <PagerStyle HorizontalAlign="Left"/>
       </asp:GridView>
       </div>  
       <br /> 
       <br />
    </td>
  </tr>
  <tr>
    <td style="width: 100%; background-color: #000000;" colspan="3"><img alt="" src="../../images/pixel.gif" /></td>
  </tr>
  <tr>
    <td style="width: 100%; height: 25px; background-color: #006AB6; text-align: center;" colspan="3"">
    <font color="#FFFFFF" size="1">&copy;2007-2009, Vet Metrics Inc. All rights reserved.
    </font></td>
  </tr>
</table>
</form>    
</body>
</html>
