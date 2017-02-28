<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RmdDropPractice.aspx.vb" Inherits="VeterinaryMetrics.BusinessLayer.ReminderDropPractice" %>
<%@ Register TagPrefix="custom" Namespace="VeterinaryMetrics.BusinessLayer.mButton"  %>
<%@ OutputCache Location="None" VaryByParam="None" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Practices by Drop Id, Edit, Delete, and Insert ...</title>
    <style type="text/css" >
        a 
        {
        }
        
        .grid
        {
            font : 11px Verdana, Arial, Sans-Serif;
        }
        
        .grid td, .grid th
        {
            padding : 8px;
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
    </style> 
    <script language="javascript" type="text/javascript" >
    var MyCalendar;
    
    function OpenCalendar2(idname)
    {
	MyCalendar = window.open('include/SpecialCalendar.aspx?formname=' + document.forms[0].name + 
		'&id=' + idname + '&selected=' + document.forms[0].elements[idname].value, 
		'MyCaledar', 
		'width=270,height=300,left=200,top=200');
	MyCalendar.focus();
    }
    
    function SetDate(formName, id, newDate)
    {
	eval('var theform = document.' + formName + ';');
	MyCalendar.close();
	theform.elements[id].value = newDate;
	theform.elements[id].focus();
    }
    
    
    function CloseCalendar2()
    {
        if (MyCalendar.closed == false)
        {
           MyCalendar.close();
        }
    }

    </script>   
</head>
<body onunload="CloseCalendar2();" style="font-family : Verdana; font-size : 10pt; margin: 0px;">
    <form id="frmReminderDropPractice" runat="server">
      <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" id="AutoNumber1">
  <tr>
    <td style="width: 34%; height: 100; background-color: #000000;">
        &nbsp;&nbsp;<img src="../../images/bwLogo.bmp" alt="" /></td>
    <td style="width: 33%; height: 100; background-color: #000000;">&nbsp;</td>
    <td style="width: 33%; height: 100; background-color: #000000; text-align: right; vertical-align: text-bottom; color: #FFFFFF;"><a href="../../SignOut.aspx">Sign Out</a>&nbsp;&nbsp;</td>
  </tr>
  <tr>
    <td style="width: 33%; height: 25; background-color: #006AB6; text-align: left; color: #FFFFFF;">&nbsp;User 
    login:&nbsp;<asp:Label ID="lblUserName" runat="server" Font-Bold="True" Text="No User"></asp:Label></td>
    <td style="width: 66%; height: 25; background-color: #006AB6; text-align: left;" colspan="2">
    &nbsp;
    </td>
    
  </tr>
  <tr>
    <td style="width: 100%; height: 16px; text-align: left; vertical-align: top;" colspan="3">
   &nbsp;
    <div>
    <asp:Panel  ID="MyEditPanel" runat="server" ScrollBars="Horizontal" Width="1050px">
        <asp:GridView 
        GridLines="None" 
        EmptyDataText="No records." 
        ID="grdvPractice" 
        runat="server"
        DataKeyNames="PK" 
        AutoGenerateColumns="False"
        CssClass="grid" 
        HeaderStyle-CssClass="header" 
        RowStyle-CssClass="row" 
        AlternatingRowStyle-CssClass="alternating" 
        AutoGenerateEditButton="True" 
        >
        <Columns>
        
        <asp:ButtonField CommandName="Delete" Text="Delete" />
          <asp:TemplateField HeaderText="PK" >
            <HeaderStyle Wrap="False" />
            <ItemTemplate>
                <asp:Label ID="lblRowID" runat="server" Text='<%# Bind("PK") %>' />
            </ItemTemplate>
        </asp:TemplateField> 

        <asp:TemplateField HeaderText="Practice Name">
            <HeaderStyle Wrap="False" />
            <ItemStyle Wrap="false" />  
          <ItemTemplate>
            <asp:Label ID="lblPracticeName" runat="server" Text='<%# Bind("PracticeName") %>' />    
          </ItemTemplate> 
        </asp:TemplateField>        

        <asp:TemplateField HeaderText="PracticeID">
          <ItemTemplate>
            <asp:Label ID="lblPractice" runat="server" Text='<%# Bind("PracticeID") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtPracticeEdit" Width="50" MaxLength="10" runat="server" Text='<%# Bind("PracticeID") %>'></asp:TextBox>
          </EditItemTemplate>  
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="DropID">
          <ItemTemplate>
            <asp:Label ID="lblDropID" runat="server" Text='<%# Bind("DropID") %>' />    
          </ItemTemplate> 
        </asp:TemplateField>
        
       <asp:TemplateField HeaderText="Species">
        <ItemStyle Wrap="false" /> 
          <ItemTemplate>
            <asp:Label ID="lblSpecies" runat="server" Text='<%# Bind("Species") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtSpecies" Width="150" MaxLength="50" runat="server" Text='<%# Bind("Species") %>'></asp:TextBox>
          </EditItemTemplate> 
          <FooterTemplate>
               <asp:TextBox ID="txtIstSpecies" Width="150" MaxLength="50" runat="server" Text='<%# Bind("Species") %>'></asp:TextBox>
          </FooterTemplate>           
       </asp:TemplateField>
        
       <asp:TemplateField HeaderText="Series">
          <ItemTemplate>
            <asp:Label ID="lblSeries" runat="server" Text='<%# Bind("Series") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtSeries" Width="150" MaxLength="50" runat="server" Text='<%# Bind("Series") %>'></asp:TextBox>
          </EditItemTemplate>
          <FooterTemplate>
              <asp:TextBox ID="txtIstSeries" Width="150" MaxLength="50" runat="server" Text='<%# Bind("Series") %>'></asp:TextBox>
          </FooterTemplate>           
        </asp:TemplateField> 
                
       <asp:TemplateField HeaderText="Reminder Type">
          <HeaderStyle Wrap="False" />
          <ItemTemplate>
            <asp:Label ID="lblReminderType" runat="server" Text='<%# Bind("ReminderType") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtReminderType" Width="150" MaxLength="50" runat="server" Text='<%# Bind("ReminderType") %>'></asp:TextBox>
          </EditItemTemplate> 
          <FooterTemplate>
              <asp:TextBox ID="txtIstReminderType" Width="150" MaxLength="50" runat="server" Text='<%# Bind("ReminderType") %>'></asp:TextBox>
          </FooterTemplate> 
        </asp:TemplateField> 
        
        <asp:TemplateField HeaderText="Template">
          <ItemTemplate>
            <asp:Label ID="lblTemplate" runat="server" Text='<%# Bind("Template") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtTemplate" Width="100" MaxLength="50" runat="server" Text='<%# Bind("Template") %>'></asp:TextBox>
          </EditItemTemplate>
          <FooterTemplate>
              <asp:TextBox ID="txtIstTemplate" Width="100" runat="server" Text='<%# Bind("Template") %>' MaxLength="10" />
          </FooterTemplate>
            <ControlStyle Width="100px" />
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Body">
          <ItemTemplate>
            <asp:Label ID="lblBody" runat="server" Text='<%# Bind("Body") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtBody" Width="100" MaxLength="50" runat="server" Text='<%# Bind("Body") %>'></asp:TextBox>
          </EditItemTemplate>
          <FooterTemplate>
              <asp:TextBox ID="txtIstBody" Width="100" runat="server" Text='<%# Bind("Body") %>' MaxLength="10" />
          </FooterTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="P1">
          <ItemTemplate>
            <asp:Label ID="lblP1" runat="server" Text='<%# Bind("P1") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtP1" Width="90" MaxLength="50" runat="server" Text='<%# Bind("P1") %>'></asp:TextBox>
          </EditItemTemplate>  
          <FooterTemplate>
              <asp:TextBox ID="txtIstP1" Width="100" runat="server" Text='<%# Bind("P1") %>' MaxLength="50" />
          </FooterTemplate>                  
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="P2">
          <ItemTemplate>
            <asp:Label ID="lblP2" runat="server" Text='<%# Bind("P2") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtP2" Width="90" MaxLength="50" runat="server" Text='<%# Bind("P2") %>'></asp:TextBox>
          </EditItemTemplate>
          <FooterTemplate>
              <asp:TextBox ID="txtIstP2" Width="100" runat="server" Text='<%# Bind("P2") %>' MaxLength="50" />
          </FooterTemplate>                
        </asp:TemplateField> 
            
        <asp:TemplateField HeaderText="P3">
          <ItemTemplate>
            <asp:Label ID="lblP3" runat="server" Text='<%# Bind("P3") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtP3" Width="90" MaxLength="50" runat="server" Text='<%# Bind("P3") %>'></asp:TextBox>
          </EditItemTemplate>   
          <FooterTemplate>
              <asp:TextBox ID="txtIstP3" Width="100" runat="server" Text='<%# Bind("P3") %>' MaxLength="50" />
          </FooterTemplate>                       
            <ControlStyle Width="90px" />
        </asp:TemplateField>    

        <asp:TemplateField HeaderText="PX">
          <ItemTemplate>
            <asp:Label ID="lblPX" runat="server" Text='<%# Bind("PX") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtPX" Width="90" MaxLength="50" runat="server" Text='<%# Bind("PX") %>'></asp:TextBox>
          </EditItemTemplate>   
          <FooterTemplate>
              <asp:TextBox ID="txtIstPX" Width="100" runat="server" Text='<%# Bind("PX") %>' MaxLength="50" />
          </FooterTemplate>                       
            <ControlStyle Width="90px" />
        </asp:TemplateField> 
                       
        <asp:TemplateField HeaderText="P1 Disc">
             <HeaderStyle Wrap="False" />
          <ItemTemplate>
            <asp:Label ID="lblP1_Disc" runat="server" Text='<%# Bind("P1Disc") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtP1_Disc" Width="120" MaxLength="50" runat="server" Text='<%# Bind("P1Disc") %>'></asp:TextBox>
          </EditItemTemplate> 
          <FooterTemplate>
              <asp:TextBox ID="txtIstP1_Disc" Width="120" runat="server" Text='<%# Bind("P1Disc") %>' MaxLength="50" />
          </FooterTemplate>          
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="P2 Disc">
        <HeaderStyle Wrap="False" />
          <ItemTemplate>
            <asp:Label ID="lblP2_Disc" runat="server" Text='<%# Bind("P2Disc") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtP2_Disc" Width="120" MaxLength="50" runat="server" Text='<%# Bind("P2Disc") %>'></asp:TextBox>
          </EditItemTemplate> 
          <FooterTemplate>
              <asp:TextBox ID="txtIstP2_Disc" Width="120" runat="server" Text='<%# Bind("P2Disc") %>' MaxLength="50" />
          </FooterTemplate>                    
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="P3 Disc">
        <HeaderStyle Wrap="False" />
          <ItemTemplate>
            <asp:Label ID="lblP3_Disc" runat="server" Text='<%# Bind("P3Disc") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtP3_Disc" Width="120" MaxLength="50" runat="server" Text='<%# Bind("P3Disc") %>'></asp:TextBox>
          </EditItemTemplate>  
          <FooterTemplate>
              <asp:TextBox ID="txtIstP3_Disc" Width="120" runat="server" Text='<%# Bind("P3Disc") %>' MaxLength="50" />
          </FooterTemplate>                      
        </asp:TemplateField>  
           
        <asp:TemplateField HeaderText="Exp1">
          <ItemTemplate>
            <asp:Label ID="lblExp1" runat="server" Text='<%# Bind("Exp1") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtExp1Date" Width="100" MaxLength="25" runat="server" Text='<%# Bind("Exp1") %>' />&nbsp;<a href="javascript:OpenCalendar2('<%# CType(Session("RowUpdIndex"), String) %>_txtExp1Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar 1" /></a>
          </EditItemTemplate> 
          <FooterTemplate>
            <asp:TextBox ID="txtIstExp1Date" Width="100" MaxLength="25" runat="server" Text='<%# Bind("Exp1") %>' />&nbsp;<a href="javascript:OpenCalendar2('<%# CType(Session("RowInsIndex"), String) %>_txtIstExp1Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar 1" /></a>
          </FooterTemplate>
        </asp:TemplateField> 
        
        <asp:TemplateField HeaderText="Exp2">
          <ItemTemplate>
            <asp:Label ID="lblExp2" runat="server" Text='<%# Bind("Exp2") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtExp2Date" Width="100" MaxLength="25" runat="server" Text='<%# Bind("Exp2") %>' />&nbsp;<a href="javascript:OpenCalendar2('<%# CType(Session("RowUpdIndex"), String) %>_txtExp2Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar 2" /></a>
          </EditItemTemplate>  
          <FooterTemplate>
            <asp:TextBox ID="txtIstExp2Date" Width="100" MaxLength="25" runat="server" Text='<%# Bind("Exp2") %>' />&nbsp;<a href="javascript:OpenCalendar2('<%# CType(Session("RowInsIndex"), String) %>_txtIstExp2Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar 1" /></a>
          </FooterTemplate>                  
        </asp:TemplateField>      
           
        <asp:TemplateField HeaderText="Exp3">
          <ItemTemplate>
            <asp:Label ID="lblExp3" runat="server" Text='<%# Bind("Exp3") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtExp3Date" Width="100" MaxLength="25" runat="server" Text='<%# Bind("Exp3") %>' />&nbsp;<a href="javascript:OpenCalendar2('<%# CType(Session("RowUpdIndex"), String) %>_txtExp3Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar 3" /></a>
          </EditItemTemplate>  
          <FooterTemplate>
            <asp:TextBox ID="txtIstExp3Date" Width="100" MaxLength="25" runat="server" Text='<%# Bind("Exp3") %>' />&nbsp;<a href="javascript:OpenCalendar2('<%# CType(Session("RowInsIndex"), String) %>_txtIstExp3Date')"><img src="images/sCalendar.gif" style="border: 0;" alt="Pick Up Calendar 1" /></a>
          </FooterTemplate>                  
        </asp:TemplateField>  
        
        <asp:TemplateField HeaderText="NPC1">
          <ItemTemplate>
            <asp:Label ID="lblNPC1" runat="server" Text='<%# Bind("NPC1") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtNPC1" Width="100" MaxLength="50" runat="server" Text='<%# Bind("NPC1") %>'></asp:TextBox>
          </EditItemTemplate>
          <FooterTemplate>
               <asp:TextBox ID="txtIstNPC1" Width="100" MaxLength="50" runat="server" Text='<%# Bind("NPC1") %>'></asp:TextBox>
          </FooterTemplate>            
        </asp:TemplateField> 
        
      <asp:TemplateField HeaderText="NPC2">
          <ItemTemplate>
            <asp:Label ID="lblNPC2" runat="server" Text='<%# Bind("NPC2") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtNPC2" Width="100" MaxLength="50" runat="server" Text='<%# Bind("NPC2") %>'></asp:TextBox>
          </EditItemTemplate> 
          <FooterTemplate>
               <asp:TextBox ID="txtIstNPC2" Width="100" MaxLength="50" runat="server" Text='<%# Bind("NPC2") %>'></asp:TextBox>
          </FooterTemplate>                    
        </asp:TemplateField> 
        
        <asp:TemplateField HeaderText="NPC3">
          <ItemTemplate>
            <asp:Label ID="lblNPC3" runat="server" Text='<%# Bind("NPC3") %>' />    
          </ItemTemplate> 
          <EditItemTemplate>
              <asp:TextBox ID="txtNPC3" Width="100" MaxLength="50" runat="server" Text='<%# Bind("NPC3") %>'></asp:TextBox>
          </EditItemTemplate>  
          <FooterTemplate>
               <asp:TextBox ID="txtIstNPC3" Width="100" MaxLength="50" runat="server" Text='<%# Bind("NPC3") %>'></asp:TextBox>
          </FooterTemplate>                   
        </asp:TemplateField> 

       <asp:TemplateField HeaderText="Mailer TypeID">
            <HeaderStyle Wrap="false" />
            <ItemStyle Wrap="false" />
          <ItemTemplate>
            <asp:Label ID="lblMailerTypeID" runat="server" Text='<%# Bind("MailerTypeID") %>' />    
          </ItemTemplate>
          <EditItemTemplate>
              <asp:TextBox ID="txtMailerTypeID" Width="150" MaxLength="50" runat="server" Text='<%# Bind("MailerTypeID") %>'></asp:TextBox>
          </EditItemTemplate>
          <FooterTemplate>
                <asp:TextBox ID="txtIstMailerTypeID" Width="150" MaxLength="50" runat="server" Text='<%# Bind("MailerTypeID") %>'></asp:TextBox>
          </FooterTemplate>            
        </asp:TemplateField> 

        <asp:TemplateField HeaderText="Print Run Series">
            <HeaderStyle Wrap="false" />
            <ItemStyle Wrap="false" />        
          <ItemTemplate>
            <asp:Label ID="lblPrintRunSeries" runat="server" Text='<%# Bind("PrintRunSeries") %>' />    
          </ItemTemplate>
             <EditItemTemplate>
              <asp:TextBox ID="txtPrintRunSeries" Width="100" MaxLength="20" runat="server" Text='<%# Bind("PrintRunSeries") %>' />
            </EditItemTemplate> 
            <FooterTemplate>
              <asp:TextBox ID="txtIstPrintRunSeries" Width="100" MaxLength="20" runat="server" Text='<%# Bind("PrintRunSeries") %>' />
            </FooterTemplate>         
        </asp:TemplateField> 
        
        <asp:TemplateField HeaderText="TWC">
            <ItemTemplate>
              <asp:TextBox ID="txtTWC" Font-Names="Verdana, Arial" Font-Size="10px" TextMode="MultiLine" Width="150" Height="40" ReadOnly="true" runat="server" Text='<%# Bind("TWC") %>' />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtEditTWC" Font-Names="Verdana, Arial" Font-Size="10px" MaxLength="100" TextMode="MultiLine" Width="150" Height="40" runat="server" Text='<%# Bind("TWC") %>' />
            </EditItemTemplate>
            <FooterTemplate>
                 <asp:TextBox ID="txtIstEditTWC" Font-Names="Verdana, Arial" Font-Size="10px" MaxLength="100" TextMode="MultiLine" Width="150" Height="40" runat="server" Text='<%# Bind("TWC") %>' />
            </FooterTemplate>        
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="PSC">
            <ItemTemplate>
              <asp:TextBox ID="txtPSC" TextMode="MultiLine" Width="250" Height="40" ReadOnly="true" runat="server" Text='<%# Bind("PSC") %>' Font-Names="Verdana, Arial" Font-Size="10px" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtEditPSC" Font-Names="Verdana, Arial" Font-Size="10px" MaxLength="255" TextMode="MultiLine" Width="250" Height="40" runat="server" Text='<%# Bind("PSC") %>' />
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtIstEditPSC" Font-Names="Verdana, Arial" Font-Size="10px" MaxLength="255" TextMode="MultiLine" Width="250" Height="40" runat="server" Text='<%# Bind("PSC") %>' />
            </FooterTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="PXC">
            <ItemTemplate>
              <asp:TextBox ID="txtPXC" TextMode="MultiLine" Width="250" Height="40" ReadOnly="true" runat="server" Text='<%# Bind("PXC") %>' Font-Names="Verdana, Arial" Font-Size="10px" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtEditPXC" Font-Names="Verdana, Arial" Font-Size="10px" MaxLength="255" TextMode="MultiLine" Width="250" Height="40" runat="server" Text='<%# Bind("PXC") %>' />
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtIstEditPXC" Font-Names="Verdana, Arial" Font-Size="10px" MaxLength="255" TextMode="MultiLine" Width="250" Height="40" runat="server" Text='<%# Bind("PXC") %>' />
            </FooterTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Pxc Color">
            <ItemTemplate>
              <asp:TextBox ID="txtPxcColor" TextMode="MultiLine" Width="250" Height="40" ReadOnly="true" runat="server" Text='<%# Bind("PxcColor") %>' Font-Names="Verdana, Arial" Font-Size="10px" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtEditPxcColor" Font-Names="Verdana, Arial" Font-Size="10px" MaxLength="255" TextMode="MultiLine" Width="250" Height="40" runat="server" Text='<%# Bind("PxcColor") %>' />
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtIstEditPxcColor" Font-Names="Verdana, Arial" Font-Size="10px" MaxLength="255" TextMode="MultiLine" Width="250" Height="40" runat="server" Text='<%# Bind("PxcColor") %>' />
            </FooterTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="CRC">
            <ItemTemplate>
              <asp:TextBox ID="txtCRC" TextMode="MultiLine" Width="250" Height="40" ReadOnly="true" runat="server" Text='<%# Bind("CRC") %>' Font-Names="Verdana, Arial" Font-Size="10px" />
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtEditCRC" Font-Names="Verdana, Arial" Font-Size="10px" MaxLength="255" TextMode="MultiLine" Width="250" Height="40" runat="server" Text='<%# Bind("CRC") %>' />
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtIstEditCRC" Font-Names="Verdana, Arial" Font-Size="10px" MaxLength="255" TextMode="MultiLine" Width="250" Height="40" runat="server" Text='<%# Bind("CRC") %>' />
            </FooterTemplate>
        </asp:TemplateField>        

        </Columns> 
            <RowStyle CssClass="row" />
            <HeaderStyle CssClass="header" />
            <AlternatingRowStyle CssClass="alternating" />
        </asp:GridView>
     </asp:Panel>
        <br />
        &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnBtnAdd" Text="Add new Record" CausesValidation="False" runat="server" />
        <asp:LinkButton runat="server" ID="lnBtnGridInsertRecord" Text="Insert" Visible="False" />&nbsp;<asp:LinkButton runat="server" ID="lnBtnGridCancelRecord" CausesValidation="False" Text="Cancel" Visible="False"/>
        <a href="javascript:self.close();">Close Window</a>
        <br />
    </div>
    </td>
    </tr> 
    <tr>
        <td style="width: 100%; background-color: #000000;" colspan="3"><img src="../../images/pixel.gif" alt="" /></td>
    </tr> 
    <tr>
        <td style="height: 26px; width: 100%; background-color: #006AB6; text-align: center; color: #FFFFFF; font-size :small;" colspan="3">&copy;2007-2009, Vet Metrics Inc. All rights reserved.
        </td>
  </tr>    
    </table> 
    </form>
</body>
</html>
