Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Collections.Generic
Imports VeterinaryMetrics.AccessLayerData
' Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class AddNewReminder
        Inherits System.Web.UI.Page

        Private sDropID As String = String.Empty
        Private sActionType As String = String.Empty

        Private bActiveWeb As Boolean = False
        Private bActiveDrop As Boolean = False
        Public MailerID As String = String.Empty
        Public Vm_Ncp As String = String.Empty

        Private DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()

        Private Const strLinksAddress As String = "<a href=""../../Default.aspx""><font color=""#FFFFFF"">Main Menu</font></a> | <a href=""ReminderDrop.aspx""><font color=""#FFFFFF"">Reminder drop</font></a>&nbsp;"

        Private MailerColl As UsrMailerCollection = Nothing

        Private Sub GetAllCurrentData()
            Dim fldRemiderDrop As ReminderDrop = ReminderDrop.GetOneReminderDropById(Int32.Parse(sDropID))

            lblActionType.Text = "Edit Reminder Drop Id: " & sDropID
            txtDescription.Text = fldRemiderDrop.DropDescription

            bActiveWeb = CBool(fldRemiderDrop.ActiveWeb)
            bActiveDrop = CBool(fldRemiderDrop.ActiveDrop)

            If bActiveWeb = True Then
                rdbActiveWebYes.Checked = True
                rdbActiveWebNo.Checked = False
            Else
                rdbActiveWebYes.Checked = False
                rdbActiveWebNo.Checked = True
            End If

            If bActiveDrop = True Then
                rdbActiveDropYes.Checked = True
                rdbActiveDropNo.Checked = False
            Else
                rdbActiveDropYes.Checked = False
                rdbActiveDropNo.Checked = True
            End If

            Dim objMyGrdViewCommon As New MyGridViewFunction()

            txtOnLineDate.Text = objMyGrdViewCommon.MyFormatDateTime(fldRemiderDrop.OnLineDate)
            txtOffLineDate.Text = objMyGrdViewCommon.MyFormatDateTime(fldRemiderDrop.OffLineDate)

            txtPullDate.Text = fldRemiderDrop.PullDate

            txtStartCDate.Text = fldRemiderDrop.CStartDate
            txtEndCDate.Text = fldRemiderDrop.CEndDate

            txtStartNR1Date.Text = fldRemiderDrop.NR1StartDate
            txtEndNR1Date.Text = fldRemiderDrop.NR1EndDate

            txtStartNR2Date.Text = fldRemiderDrop.NR2StartDate
            txtEndNR2Date.Text = fldRemiderDrop.NR2EndDate

            MailerID = fldRemiderDrop.MailerTypeID
            Vm_Ncp = fldRemiderDrop.VM_NPC

            drpMailerID.SelectedValue = MailerID
            drpVmNpc.SelectedValue = Vm_Ncp

            txtPrintRunSeries.Text = fldRemiderDrop.PrintRunSeries
            txtPrintRunDate.Text = fldRemiderDrop.PrintRunDate

            txtRptStartCDate.Text = fldRemiderDrop.Rpt_CStartDate
            txtRptEndCDate.Text = objMyGrdViewCommon.MyFormatDateTime(fldRemiderDrop.Rpt_CEndDate)

            txtRptStartRCYDate.Text = fldRemiderDrop.Rpt_RCYStartDate
            txtRptEndRCYDate.Text = fldRemiderDrop.Rpt_RCYEndDate

            txtRptEndShortDate.Text = objMyGrdViewCommon.MyFormatDateTime(fldRemiderDrop.ReportShortDate)

            txtRptStartNR3Date.Text = fldRemiderDrop.Rpt_NR3StartDate
            txtRptEndNR3Date.Text = objMyGrdViewCommon.MyFormatDateTime(fldRemiderDrop.Rpt_NR3EndDate)

            txtExp1Date.Text = fldRemiderDrop.Exp1Date
            txtExp2Date.Text = fldRemiderDrop.Exp2Date
            txtExp3Date.Text = fldRemiderDrop.Exp3Date

            lblPractices.Text = ReminderDrop.GetPracticesByDropID(Int32.Parse(sDropID))

            btnSaveReminder.Visible = False
            btnUpdateRmd.Visible = True
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            ' load user name
            lblUserName.Text = User.Identity.Name
            Dim sFormName As String = String.Empty
            Dim sPostBack As String = String.Empty
            Dim sControlName As String = String.Empty


            If Request.QueryString.Count > 0 Then
                sDropID = Request.QueryString("id").ToString()
                sActionType = Request.QueryString("type").ToString()
                sFormName = Request.QueryString("formName").ToString()
                sPostBack = Request.QueryString("postBack").ToString()
                sControlName = Request.QueryString("control").ToString()

                btnCloseWindow.Attributes.Add("onClick", "window.opener.MyPostBack('" + sFormName + "', '" + sControlName + "', " _
                + sPostBack + ");")
                btnAddPractice.Attributes.Add("onClick", "AddPractice();")
                btnDelPractice.Attributes.Add("onClick", "DelPractice();")

                lblLinkAddress.Text = ""

            Else
                lblActionType.Text = "Add new Reminder Drop "
                lblLinkAddress.Text = strLinksAddress
            End If

            If sDropID.Length > 0 And sActionType = "edit" Then
                ' retrieve the user name


            Else
                btnSaveReminder.Visible = True
                btnUpdateRmd.Visible = False
                btnCloseWindow.Visible = False
                MyPanelPractices.Visible = False

            End If

            If Not Page.IsPostBack Then

                MailerColl = UsrMailer.GetAll()

                If MailerColl.Count > 0 Then
                    With drpMailerID
                        .DataSource = MailerColl
                        .DataValueField = "TypeId"
                        .DataTextField = "TypeDesc"
                        .DataBind()
                    End With
                End If
                If sActionType = "edit" Then
                    GetAllCurrentData()
                End If

                'btnUpdateRmd.Attributes.Add("onClick", "window.opener.__doPostBack('','')")


            End If
        End Sub

        ' populate the date
        Protected Sub btnPopulate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPopulate.Click

            Dim PullDate As DateTime = "1990/01/01"
            Dim CStartDate As String = String.Empty
            Dim CEndDate As String = String.Empty
            Dim ReportCStartDate As String = String.Empty
            Dim ReportCEndDate As String = String.Empty
            Dim ReportRCYStartDate As String = String.Empty
            Dim ReportRCYEndDate As String = String.Empty
            Dim NR1_StartDate As String = String.Empty
            Dim NR1_EndDate As String = String.Empty
            Dim NR2_StartDate As String = String.Empty
            Dim NR2_EndDate As String = String.Empty
            Dim ReportNR3_StartDate As String = String.Empty
            Dim ReportNR3_EndDate As String = String.Empty

            Dim MailerID As String = drpMailerID.SelectedValue
            Dim objRmdDate As New ReminderDate(txtPullDate.Text)
            btnSaveReminder.Enabled = True

            Const DATE_FORMAT As String = "MM/dd/yyyy"
            ' begin
            ' get AW
            ' **********************************************************
            ' DN, DO, HW, PM, SR, WB, WF 
            ' June 27, 2008 - Change requested by Randy
            ' adding 1 month PLUS

            ' New functionallity added 
            ' July 24, 2008
            ' by Randy
            If MailerID = "ID" Then
                ' check for date
                If IsDate(txtPullDate.Text) = False Then
                    lblErrorMessage.Text = "The value in field \""Pull Date: \"" is not a correct date format."
                    txtPullDate.Focus()
                    Exit Sub
                End If

                ' get Date
                PullDate = DateTime.Parse(txtPullDate.Text)

                txtEndCDate.Text = ""
                txtStartCDate.Text = ""

                txtStartNR1Date.Text = ""
                txtEndNR1Date.Text = ""

                txtStartNR2Date.Text = ""
                txtEndNR2Date.Text = ""

                txtRptStartNR3Date.Text = ""
                txtRptEndNR3Date.Text = ""

                txtRptStartCDate.Text = ""
                txtRptEndCDate.Text = ""

                txtRptStartRCYDate.Text = ""
                txtRptEndRCYDate.Text = ""
                txtRptEndShortDate.Text = ""

                ' + 1 day
                txtOnLineDate.Text = PullDate.AddDays(1).ToString("d", System.Globalization.DateTimeFormatInfo.InvariantInfo)

                ' + 4 days
                txtOffLineDate.Text = PullDate.AddDays(4).ToString("d", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                txtPrintRunDate.Text = txtPullDate.Text
            End If
            ' end ID

            If MailerID = "AWL" Or MailerID = "DN" Or MailerID = "DO" _
             Or MailerID = "HW" Or MailerID = "PM" Or MailerID = "SR" _
             Or MailerID = "WB" Or MailerID = "WF" Or _
             MailerID = "AWPC" Or MailerID = "DNPC" Or MailerID = "SRPC" Then

                ' check for date
                If IsDate(txtPullDate.Text) = False Then
                    lblErrorMessage.Text = "The value in field \""Pull Date: \"" is not a correct date format."
                    txtPullDate.Focus()
                    Exit Sub
                End If

                ' date format is MM/DD/YYYY
                If objRmdDate.beginMonth = 12 Then
                    CStartDate = "01/01/" & (objRmdDate.beginYear + 1).ToString()
                    CEndDate = "01/" & DateTime.DaysInMonth(objRmdDate.beginYear + 1, 1).ToString() & _
                    "/" & (objRmdDate.beginYear + 1).ToString()
                Else
                    CStartDate = (objRmdDate.beginMonth + 1).ToString() & "/01" & _
                     "/" & objRmdDate.beginYear.ToString()
                    CEndDate = (objRmdDate.beginMonth + 1).ToString() & "/" & _
                    DateTime.DaysInMonth(objRmdDate.beginYear, objRmdDate.beginMonth + 1).ToString() & "/" & _
                    objRmdDate.beginYear.ToString()

                End If

                txtStartCDate.Text = CStartDate
                txtEndCDate.Text = CEndDate

                ' NR1 dates
                NR1_StartDate = objRmdDate.beginMonth.ToString() & "/01/" & objRmdDate.beginYear.ToString()
                NR1_EndDate = objRmdDate.beginMonth.ToString() & "/" & _
                DateTime.DaysInMonth(objRmdDate.beginYear, objRmdDate.beginMonth).ToString() & "/" & _
                objRmdDate.beginYear.ToString()

                txtStartNR1Date.Text = NR1_StartDate
                txtEndNR1Date.Text = NR1_EndDate
                ' end NR 1

                ' Reports Start Date C 
                ' the same like previous dates
                ' Changes are made June 27, 2008
                ' dim s as string = dDate.tostring("MM/dd/yyyy hh:mm:ss TT")

                Dim dtRptSTART As DateTime = DateTime.Parse(NR1_StartDate).AddMonths(1)
                Dim dtRptEND As DateTime = DateTime.Parse(NR1_EndDate).AddMonths(1)
                Dim dtDaysRptEndMonth As Integer = -1
                ' Old way
                ' txtRptStartCDate.Text = NR1_StartDate
                ' txtRptEndCDate.Text = NR1_EndDate & " 23:59"

                txtRptStartCDate.Text = dtRptSTART.ToString(DATE_FORMAT)

                ' get the dates
                dtDaysRptEndMonth = DateTime.DaysInMonth(dtRptEND.Year, dtRptEND.Month)
                txtRptEndCDate.Text = dtRptEND.Month.ToString() & "/" & _
                        dtDaysRptEndMonth.ToString() & "/" & dtRptEND.Year & _
                        " 23:59"
                ' dtRptEND.ToString(DATE_FORMAT) & " 23:59"
                ' Short date 
                ' txtRptEndShortDate.Text = NR1_EndDate & " 23:59"
                txtRptEndShortDate.Text = dtRptEND.Month.ToString() & "/" & _
                        dtDaysRptEndMonth.ToString() & "/" & dtRptEND.Year & _
                        " 23:59"

                ' NR-2
                If objRmdDate.beginMonth = 1 Then
                    NR2_StartDate = "12/01/" & (objRmdDate.beginYear - 1).ToString()
                    NR2_EndDate = "12/" & DateTime.DaysInMonth(objRmdDate.beginYear - 1, 12).ToString() & "/" & _
                    (objRmdDate.beginYear - 1).ToString()
                Else
                    NR2_StartDate = (objRmdDate.beginMonth - 1).ToString() & "/01/" & objRmdDate.beginYear.ToString()
                    NR2_EndDate = (objRmdDate.beginMonth - 1).ToString() & "/" & _
                    DateTime.DaysInMonth(objRmdDate.beginYear, objRmdDate.beginMonth - 1).ToString() & "/" & _
                    objRmdDate.beginYear.ToString()
                End If

                txtStartNR2Date.Text = NR2_StartDate
                txtEndNR2Date.Text = NR2_EndDate
                ' end NR2

                '  ReportNR3
                If objRmdDate.beginMonth = 1 Then
                    ReportNR3_StartDate = "11/01/" & (objRmdDate.beginYear - 1).ToString()
                    ReportNR3_EndDate = "11/" & DateTime.DaysInMonth(objRmdDate.beginYear - 1, 11).ToString() & "/" & _
                    (objRmdDate.beginYear - 1).ToString() & " 23:59"
                Else
                    If objRmdDate.beginMonth = 2 Then
                        ReportNR3_StartDate = "12/01/" & (objRmdDate.beginYear - 1).ToString()
                        ReportNR3_EndDate = "12/" & DateTime.DaysInMonth(objRmdDate.beginYear - 1, 1).ToString() & "/" & _
                        (objRmdDate.beginYear - 1).ToString() & " 23:59"
                    Else
                        ReportNR3_StartDate = (objRmdDate.beginMonth - 2).ToString() & "/01/" & objRmdDate.beginYear.ToString()
                        ReportNR3_EndDate = (objRmdDate.beginMonth - 2).ToString() & "/" & _
                        DateTime.DaysInMonth(objRmdDate.beginYear, objRmdDate.beginMonth - 2).ToString() & "/" & _
                        objRmdDate.beginYear.ToString() & " 23:59"
                    End If
                End If
                txtRptStartNR3Date.Text = ReportNR3_StartDate
                txtRptEndNR3Date.Text = ReportNR3_EndDate

                ' Report RCY StartDate
                Dim dt As DateTime = objRmdDate.beginMonth() & "/01/" & objRmdDate.beginYear.ToString()
                Dim rcyFirstDate As DateTime = dt.AddMonths(16)
                ReportRCYStartDate = dt.AddMonths(2).ToShortDateString()

                ReportRCYEndDate = rcyFirstDate.Month.ToString() & "/" & _
                    DateTime.DaysInMonth(rcyFirstDate.Year, rcyFirstDate.Month).ToString() & "/" & _
                    rcyFirstDate.Year.ToString()

                txtRptStartRCYDate.Text = ReportRCYStartDate
                txtRptEndRCYDate.Text = ReportRCYEndDate
                ' Report RCY StartDate

                'System.DateTime.DaysInMonth(2001, July);
                ' Starting date C
                ' + 1 month
            End If ' MailerID = "aw" 
            ' **********************************************************

            ' MCbegin 
            If MailerID = "MC" Then

                ' date format is MM/DD/YYYY
                ' check for date
                If IsDate(txtPullDate.Text) = False Then
                    lblErrorMessage.Text = "The value in field \""Pull Date: \"" is not a correct date format."
                    txtPullDate.Focus()
                    Exit Sub
                End If

                ' C Start Dates
                CStartDate = objRmdDate.beginMonth.ToString() & "/01/" & objRmdDate.beginYear.ToString()
                CEndDate = objRmdDate.beginMonth.ToString() & "/" & _
                DateTime.DaysInMonth(objRmdDate.beginYear, objRmdDate.beginMonth).ToString() & "/" & _
                objRmdDate.beginYear.ToString()

                txtStartCDate.Text = CStartDate
                txtEndCDate.Text = CEndDate
                'end C Start Dates

                ' NR-1
                If objRmdDate.beginMonth = 1 Then
                    NR1_StartDate = "01/01/" & (objRmdDate.beginYear - 1).ToString()
                    NR1_EndDate = "01/" & DateTime.DaysInMonth(objRmdDate.beginYear - 1, 1).ToString() & "/" & _
                    (objRmdDate.beginYear - 1).ToString()
                Else
                    NR1_StartDate = (objRmdDate.beginMonth - 1).ToString() & "/01/" & objRmdDate.beginYear.ToString()
                    NR1_EndDate = (objRmdDate.beginMonth - 1).ToString() & "/" & _
                    DateTime.DaysInMonth(objRmdDate.beginYear, objRmdDate.beginMonth - 1).ToString() & "/" & _
                    objRmdDate.beginYear.ToString()
                End If
                txtStartNR1Date.Text = NR1_StartDate
                txtEndNR1Date.Text = NR1_EndDate
                ' end NR-1

                ' Reports Start NR1 Dates 
                ' the same like previous dates
                txtRptStartCDate.Text = NR1_StartDate
                txtRptEndCDate.Text = NR1_EndDate & " 23:59"

                ' Short date
                txtRptEndShortDate.Text = NR1_EndDate & " 23:59"

                '  Begin NR-2
                If objRmdDate.beginMonth = 1 Then
                    NR2_StartDate = "02/01/" & (objRmdDate.beginYear - 1).ToString()
                    NR2_EndDate = "02/" & DateTime.DaysInMonth(objRmdDate.beginYear - 1, 2).ToString() & "/" & _
                    (objRmdDate.beginYear - 1).ToString() & " 23:59"
                Else
                    If objRmdDate.beginMonth = 2 Then
                        NR2_StartDate = "01/01/" & (objRmdDate.beginYear - 1).ToString()
                        NR2_EndDate = "01/" & DateTime.DaysInMonth(objRmdDate.beginYear - 1, 1).ToString() & "/" & _
                        (objRmdDate.beginYear - 1).ToString()
                    Else
                        NR2_StartDate = (objRmdDate.beginMonth - 2).ToString() & "/01/" & objRmdDate.beginYear.ToString()
                        NR2_EndDate = (objRmdDate.beginMonth - 2).ToString() & "/" & _
                        DateTime.DaysInMonth(objRmdDate.beginYear, objRmdDate.beginMonth - 2).ToString() & "/" & _
                        objRmdDate.beginYear.ToString()
                    End If
                End If
                txtStartNR2Date.Text = NR2_StartDate
                txtEndNR2Date.Text = NR2_EndDate

                Dim dt As DateTime = objRmdDate.beginMonth.ToString() & "/01/" & objRmdDate.beginYear.ToString()
                Dim nr3Date As DateTime = dt.AddMonths(-3)

                txtRptStartNR3Date.Text = nr3Date.ToShortDateString()
                txtRptEndNR3Date.Text = nr3Date.Month.ToString() & "/" & _
                    DateTime.DaysInMonth(nr3Date.Year, nr3Date.Month).ToString() & "/" & _
                    nr3Date.Year.ToString() & " 23:59"

                ReportRCYStartDate = dt.ToShortDateString()
                txtRptStartRCYDate.Text = ReportRCYStartDate

                ' Report RCY StartDate
                'Dim dt As DateTime = objRmdDate.beginMonth() & "/01/" & objRmdDate.beginYear.ToString()
                Dim rcyFirstDate As DateTime = dt.AddMonths(14)
                ReportRCYEndDate = rcyFirstDate.Month.ToString() & "/" & _
                    DateTime.DaysInMonth(rcyFirstDate.Year, rcyFirstDate.Month).ToString() & "/" & _
                    rcyFirstDate.Year.ToString()
                txtRptEndRCYDate.Text = ReportRCYEndDate
            End If ' MailerID = "MC" 
            ' **********************************************************


        End Sub

        '*************************************************************
        '            If MailerID = "AW" Or MailerID = "DN" Or MailerID = "DO" _
        '     Or MailerID = "HW" Or MailerID = "PM" Or MailerID = "SR" _
        '     Or MailerID = "WB" Or MailerID = "WF" Then

        '' check for date
        '        If IsDate(txtPullDate.Text) = False Then
        '            lblErrorMessage.Text = "The value in field \""Pull Date: \"" is not a correct date format."
        '            txtPullDate.Focus()
        '            Exit Sub
        '        End If

        '' date format is MM/DD/YYYY
        '        If objRmdDate.beginMonth = 12 Then
        '            CStartDate = "01/01/" & (objRmdDate.beginYear + 1).ToString()
        '            CEndDate = "01/" & DateTime.DaysInMonth(objRmdDate.beginYear + 1, 1).ToString() & _
        '            "/" & (objRmdDate.beginYear + 1).ToString()
        '        Else
        '            CStartDate = (objRmdDate.beginMonth + 1).ToString() & "/01" & _
        '             "/" & objRmdDate.beginYear.ToString()
        '            CEndDate = (objRmdDate.beginMonth + 1).ToString() & "/" & _
        '            DateTime.DaysInMonth(objRmdDate.beginYear, objRmdDate.beginMonth + 1).ToString() & "/" & _
        '            objRmdDate.beginYear.ToString()

        '        End If

        '        txtStartCDate.Text = CStartDate
        '        txtEndCDate.Text = CEndDate

        '' Reports Start Date C 
        '' the same like previous dates
        '        txtRptStartCDate.Text = CStartDate
        '        txtRptEndCDate.Text = CEndDate & " 23:59"

        '' Short date
        '        txtRptEndShortDate.Text = CEndDate & " 23:59"

        '' NR1 dates
        '        NR1_StartDate = objRmdDate.beginMonth.ToString() & "/01/" & objRmdDate.beginYear.ToString()
        '        NR1_EndDate = objRmdDate.beginMonth.ToString() & "/" & _
        '        DateTime.DaysInMonth(objRmdDate.beginYear, objRmdDate.beginMonth).ToString() & "/" & _
        '        objRmdDate.beginYear.ToString()

        '        txtStartNR1Date.Text = NR1_StartDate
        '        txtEndNR1Date.Text = NR1_EndDate
        '' end NR 1

        '' NR-2
        '        If objRmdDate.beginMonth = 1 Then
        '            NR2_StartDate = "01/01/" & (objRmdDate.beginYear - 1).ToString()
        '            NR2_EndDate = "01/" & DateTime.DaysInMonth(objRmdDate.beginYear - 1, 1).ToString() & "/" & _
        '            (objRmdDate.beginYear - 1).ToString()
        '        Else
        '            NR2_StartDate = (objRmdDate.beginMonth - 1).ToString() & "/01/" & objRmdDate.beginYear.ToString()
        '            NR2_EndDate = (objRmdDate.beginMonth - 1).ToString() & "/" & _
        '            DateTime.DaysInMonth(objRmdDate.beginYear, objRmdDate.beginMonth - 1).ToString() & "/" & _
        '            objRmdDate.beginYear.ToString()
        '        End If
        '        txtStartNR2Date.Text = NR2_StartDate
        '        txtEndNR2Date.Text = NR2_EndDate
        '' end NR2

        ''  ReportNR3
        '        If objRmdDate.beginMonth = 1 Then
        '            ReportNR3_StartDate = "02/01/" & (objRmdDate.beginYear - 1).ToString()
        '            ReportNR3_EndDate = "02/" & DateTime.DaysInMonth(objRmdDate.beginYear - 1, 2).ToString() & "/" & _
        '            (objRmdDate.beginYear - 1).ToString() & " 23:59"
        '        Else
        '            If objRmdDate.beginMonth = 2 Then
        '                ReportNR3_StartDate = "01/01/" & (objRmdDate.beginYear - 1).ToString()
        '                ReportNR3_EndDate = "01/" & DateTime.DaysInMonth(objRmdDate.beginYear - 1, 1).ToString() & "/" & _
        '                (objRmdDate.beginYear - 1).ToString() & " 23:59"
        '            Else
        '                ReportNR3_StartDate = (objRmdDate.beginMonth - 2).ToString() & "/01/" & objRmdDate.beginYear.ToString()
        '                ReportNR3_EndDate = (objRmdDate.beginMonth - 2).ToString() & "/" & _
        '                DateTime.DaysInMonth(objRmdDate.beginYear, objRmdDate.beginMonth - 2).ToString() & "/" & _
        '                objRmdDate.beginYear.ToString() & " 23:59"
        '            End If
        '        End If
        '        txtRptStartNR3Date.Text = ReportNR3_StartDate
        '        txtRptEndNR3Date.Text = ReportNR3_EndDate

        '' Report RCY StartDate
        'Dim dt As DateTime = objRmdDate.beginMonth() & "/01/" & objRmdDate.beginYear.ToString()
        'Dim rcyFirstDate As DateTime = dt.AddMonths(16)
        '        ReportRCYStartDate = dt.AddMonths(2).ToShortDateString()
        '        ReportRCYEndDate = rcyFirstDate.Month.ToString() & "/" & _
        '            DateTime.DaysInMonth(rcyFirstDate.Year, rcyFirstDate.Month).ToString() & "/" & _
        '            rcyFirstDate.Year.ToString()

        '        txtRptStartRCYDate.Text = ReportRCYStartDate
        '        txtRptEndRCYDate.Text = ReportRCYEndDate
        '' Report RCY StartDate

        ''System.DateTime.DaysInMonth(2001, July);
        '' Starting date C
        '' + 1 month
        '    End If ' MailerID = "aw" 

        Protected Sub btnSaveReminder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveReminder.Click

            Dim iActiveDrop As Integer
            Dim iActiveWeb As Integer
            Dim ArrFieldsText(27) As String
            Dim ArrReturnValues(1) As String
            Dim sTempDate As String = String.Empty

            Dim i As Integer

            ArrFieldsText(0) = txtDescription.Text

            If (rdbActiveWebNo.Checked) Then iActiveWeb = 0
            If (rdbActiveWebYes.Checked) Then iActiveWeb = 1
            ArrFieldsText(1) = iActiveWeb

            If (rdbActiveDropNo.Checked) Then iActiveDrop = 0
            If (rdbActiveDropYes.Checked) Then iActiveDrop = 1

            ArrFieldsText(2) = iActiveDrop
            ArrFieldsText(3) = txtPullDate.Text

            If txtOnLineDate.Text.Length > 12 Then
                sTempDate = txtOnLineDate.Text
            Else
                sTempDate = txtOnLineDate.Text & " 11:00"
            End If

            ArrFieldsText(4) = sTempDate ' & " 9:00 AM"

            If txtOffLineDate.Text.Length > 12 Then
                sTempDate = txtOffLineDate.Text
            Else
                sTempDate = txtOffLineDate.Text & " 20:00"
            End If

            ArrFieldsText(5) = sTempDate ' & " 8:00 PM"
            ArrFieldsText(6) = txtStartCDate.Text
            ArrFieldsText(7) = txtEndCDate.Text
            ArrFieldsText(8) = txtStartNR1Date.Text
            ArrFieldsText(9) = txtEndNR1Date.Text
            ArrFieldsText(10) = txtStartNR2Date.Text
            ArrFieldsText(11) = txtEndNR2Date.Text
            ArrFieldsText(12) = txtPrintRunDate.Text
            ArrFieldsText(13) = txtPrintRunSeries.Text
            ArrFieldsText(14) = drpMailerID.SelectedValue.ToString()
            ArrFieldsText(15) = drpVmNpc.SelectedValue.ToString()
            ArrFieldsText(16) = txtRptStartCDate.Text


            If txtRptEndCDate.Text.Length > 12 Then
                sTempDate = txtRptEndCDate.Text
            Else
                sTempDate = txtRptEndCDate.Text & " 23:59"
            End If
            ArrFieldsText(17) = sTempDate

            ArrFieldsText(18) = txtRptStartRCYDate.Text
            ArrFieldsText(19) = txtRptEndRCYDate.Text

            If txtRptEndShortDate.Text.Length > 12 Then
                sTempDate = txtRptEndShortDate.Text
            Else
                sTempDate = txtRptEndShortDate.Text & " 23:59"
            End If

            ArrFieldsText(20) = sTempDate

            ArrFieldsText(21) = txtRptStartNR3Date.Text

            If txtRptEndNR3Date.Text.Length > 12 Then
                sTempDate = txtRptEndNR3Date.Text
            Else
                sTempDate = txtRptEndNR3Date.Text & " 23:59"
            End If

            ArrFieldsText(22) = sTempDate
            ArrFieldsText(23) = User.Identity.Name
            ArrFieldsText(24) = txtExp1Date.Text
            ArrFieldsText(25) = txtExp2Date.Text
            ArrFieldsText(26) = txtExp3Date.Text

            ' --------------------------------------------------------------------------------
            ArrReturnValues = DataLayer.AddNew_RmrDrop(ArrFieldsText)

            Erase ArrFieldsText
            i = Convert.ToInt32(ArrReturnValues(0))

            If i = 0 Then
                Session("strSuccess") = "Record with desc '" & txtDescription.Text & "' has been added to the database."
                Session("MyGridViewBind") = Nothing
                Response.Redirect("Success.aspx")
            End If

            If i < 0 Then
                lblErrorMessage.Text = ArrReturnValues(1)
            End If

            Erase ArrReturnValues
            ' If i = 0 Then Response.Redirect("Success.aspx")

        End Sub

        Protected Sub btnUpdateRmd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateRmd.Click

            Dim myFields As New ReminderDrop()
            Dim iActiveWeb As Integer
            Dim iActiveDrop As Integer
            Dim bSuccess As Boolean = False


            With myFields
                .DropId = Int32.Parse(Request.QueryString("id").ToString())
                .DropDescription = txtDescription.Text

                If (rdbActiveWebNo.Checked) Then iActiveWeb = 0
                If (rdbActiveWebYes.Checked) Then iActiveWeb = 1
                If (rdbActiveDropNo.Checked) Then iActiveDrop = 0
                If (rdbActiveDropYes.Checked) Then iActiveDrop = 1

                .ActiveDrop = iActiveDrop
                .ActiveWeb = iActiveWeb

                .PullDate = txtPullDate.Text
                .OnLineDate = txtOnLineDate.Text
                .OffLineDate = txtOffLineDate.Text
                .CStartDate = txtStartCDate.Text
                .CEndDate = txtEndCDate.Text

                .NR1StartDate = txtStartNR1Date.Text
                .NR1EndDate = txtEndNR1Date.Text

                .NR2StartDate = txtStartNR2Date.Text
                .NR2EndDate = txtEndNR2Date.Text
                .PrintRunDate = txtPrintRunDate.Text
                .PrintRunSeries = txtPrintRunSeries.Text

                ' this will be a drop down list
                .MailerTypeID = drpMailerID.SelectedValue
                .VM_NPC = drpVmNpc.SelectedValue

                .Rpt_CStartDate = txtRptStartCDate.Text
                .Rpt_CEndDate = txtRptEndCDate.Text
                .ReportShortDate = txtRptEndShortDate.Text
                .Rpt_NR3EndDate = txtRptEndNR3Date.Text
                .Rpt_NR3StartDate = txtRptStartNR3Date.Text
                .Rpt_RCYEndDate = txtRptEndRCYDate.Text
                .Rpt_RCYStartDate = txtRptStartRCYDate.Text
                .UserName = User.Identity.Name

                .Exp1Date = txtExp1Date.Text
                .Exp2Date = txtExp2Date.Text
                .Exp3Date = txtExp3Date.Text
            End With


            bSuccess = ReminderDrop.Update_OneRecord(myFields)

            If bSuccess = True Then
                lblActionType.Text = "Record id: " & Int32.Parse(Request.QueryString("id").ToString()) & " has been updated."
            End If
        End Sub

        ' add button
        Protected Sub btnAddPractice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddPractice.Click
            Dim bool As Boolean = False

            If txtAddDelPracticeID.Text.Length > 0 Then

                ' add new practice
                bool = DataLayer.AddNew_Matrix_WithPractice(Int32.Parse(txtAddDelPracticeID.Text), Int32.Parse(sDropID))
                lblAddDelPractice.Text = "One Practice has been added to PositionMatrix ..."
                lblPractices.Text = ReminderDrop.GetPracticesByDropID(Int32.Parse(sDropID))
            Else
                lblAddDelPractice.Text = ""
            End If

        End Sub

        ' delete button
        Protected Sub btnDelPractice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelPractice.Click
            Dim bool As Boolean = False

            If txtAddDelPracticeID.Text.Length > 0 Then
                ' delete several
                bool = DataLayer.Delete_GroupOfMatrix(Int32.Parse(txtAddDelPracticeID.Text), Int32.Parse(sDropID))

                lblAddDelPractice.Text = "All records for practice id: " & txtAddDelPracticeID.Text & " have been deleted!"
                lblPractices.Text = ReminderDrop.GetPracticesByDropID(Int32.Parse(sDropID))
            Else
                lblAddDelPractice.Text = ""
            End If
        End Sub
    End Class

    ' Begin new Implementation of Date
    Public Class ReminderDate

        Private _StringSelDate As String
        Private _beginDate As DateTime

        Public Sub New()
        End Sub ' NEW

        Public Sub New(ByVal StringSelDate As String)
            _StringSelDate = StringSelDate
        End Sub ' NEW

        '' STATIC MEMBERS
        'Public Shared Function isDateValidate(ByVal sDate As String) As Boolean
        'End Function


        Public ReadOnly Property beginYear() As Int32
            Get
                Return beginDate.Year()
            End Get
        End Property

        Public ReadOnly Property beginMonth() As Int32
            Get
                Return beginDate.Month()
            End Get
        End Property

        Public ReadOnly Property beginDay() As Int32
            Get
                Return beginDate.Day()
            End Get
        End Property

        Public Property beginDate() As DateTime
            Get
                If isValidDate Then
                    Return DateTime.Parse(_StringSelDate)
                Else
                    Return DateTime.Parse("01/01/1990")
                End If
            End Get
            Set(ByVal value As DateTime)
                _beginDate = value
            End Set
        End Property

        Public ReadOnly Property isValidDate() As Boolean
            Get
                If IsDate(StringSelDate) Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property

        Public Property StringSelDate() As String
            Get
                Return _StringSelDate
            End Get
            Set(ByVal value As String)
                _StringSelDate = value
            End Set
        End Property
    End Class

    ' begin ReminderDateHelper
    Public Class ReminderDateHelper

        Private Const BASE As String = "VeterinaryMetrics.BusinessLayer.ReminderDate"

        Public Shared Function GetInstance() As ReminderDate
            Dim trp As Type = Type.GetType(BASE, True)
            Dim mainIns As ReminderDate = CType(Activator.CreateInstance(trp), ReminderDate)
            Return mainIns
        End Function
    End Class

End Namespace