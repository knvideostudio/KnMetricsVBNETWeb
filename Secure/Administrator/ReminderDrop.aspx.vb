Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Collections.Generic

Imports VeterinaryMetrics.AccessLayerData
Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class DisplayReminderDrop
        Inherits System.Web.UI.Page

        Private MailerColl As UsrMailerCollection = Nothing

        'Private DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
        Private dv As New DataView
        Private MailerID As String = "All"

        'Private Delegate Sub UpdateReminderDropHandler(ByVal _fields As ReminderDrop, ByRef bool As Boolean)
        'Private Event UpdateReminderDrop As UpdateReminderDropHandler

        Private Delegate Sub BindReminderDropGridViewHandler(ByVal bForUpdate As Boolean)
        Private Event BindReminderDropGridView As BindReminderDropGridViewHandler

        Private objMyGrdViewCommon As New MyGridViewFunction()

        Public Overrides Sub Dispose()
            If Not dv Is Nothing Then dv = Nothing
            If Not objMyGrdViewCommon Is Nothing Then objMyGrdViewCommon = Nothing
            If Not MailerColl Is Nothing Then MailerColl = Nothing


            MyBase.Dispose()
        End Sub

        Private Sub BindMyGridView(ByVal bForUpdate As Boolean) Handles Me.BindReminderDropGridView

            '' Add into chashe
            'If bForUpdate = True Then
            '    dv = DataLayer.Retrun_RmrDrop_AllRecords()
            '    Session("MyGridViewBind") = dv
            'End If

            'If bForUpdate = False Then
            ' when session is EMPTY
            'If Session("MyGridViewBind") Is Nothing Then
            'Session("MyGridViewBind") = dv
            'End If
            'End If

            'If (Not SortExpression Is Nothing) And (Not SortExpression Is String.Empty) Then
            '    CType(Session("MyGridViewBind"), DataView).Sort = SortExpression & " " + SortDirection
            'End If

            dv = ReminderDrop.GetAllRecords(CType(Session("mailerID"), String))
            If dv.Count > 0 Then
                If (Not SortExpression Is Nothing) And (Not SortExpression Is String.Empty) Then
                    dv.Sort = SortExpression & " " + SortDirection
                End If
                MyGrdView.DataSource = dv
                MyGrdView.DataBind()
                Session("CurrentPage") = MyGrdView.PageIndex + 1
            Else
                'MyLabelErrMessage.Text = "There are no records found in Reminder Drop table."
                MyGrdView.DataSource = Nothing
                MyGrdView.EmptyDataText = "There are no records found for <b>" & drpMailer.SelectedValue.ToString() & "</b>."
                MyGrdView.DataBind()
                Session("CurrentPage") = 1
            End If
        End Sub

        ' ---------------------------------------------
        '  Sort Expresion
        ' ---------------------------------------------
        Public Property SortExpression() As String
            Get
                If Not ViewState("SortExpression") = Nothing Then
                    Return CType(ViewState("SortExpression"), String)
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                If ViewState("SortExpression") = Nothing Then
                    ViewState.Add("SortExpression", value)
                Else
                    ViewState("SortExpression") = value
                End If
            End Set
        End Property

        Public Property SortDirection() As String
            Get
                If Not ViewState("SortDirection") = Nothing Then
                    Return CType(ViewState("SortDirection"), String)
                Else
                    Return "ASC"
                End If
            End Get
            Set(ByVal value As String)
                If ViewState("SortDirection") = Nothing Then
                    ViewState.Add("SortDirection", value)
                Else
                    ViewState("SortDirection") = value
                End If
            End Set
        End Property


        ' ---------------------------------------------------------------------
        '   Sorting enable Load event of page
        ' ---------------------------------------------------------------------
        Protected Sub grdDropReminder_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles MyGrdView.Sorting

            If SortExpression <> e.SortExpression Then
                SortExpression = e.SortExpression
                SortDirection = "ASC"
            Else
                If SortDirection = "ASC" Then
                    SortDirection = "DESC"
                Else
                    SortDirection = "ASC"
                End If
            End If

            BindMyGridView(False)
        End Sub

        Protected Sub grdDropReminder_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles MyGrdView.PageIndexChanging
            MyGrdView.PageIndex = e.NewPageIndex
            BindMyGridView(False)
        End Sub

        Protected Sub grdDropReminder_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs) Handles MyGrdView.RowEditing

            Dim row As GridViewRow = MyGrdView.Rows(e.NewEditIndex)
            Dim sControlNameID As String = row.ClientID()

            MyGrdView.EditIndex = e.NewEditIndex

            ' this is using in ASPX page by JAVA SCRIPT
            Session("RowIndex") = sControlNameID
            BindMyGridView(False)

            ' GridViewRow gvr = grdWorkUnits.Rows[grdWorkUnits.EditIndex];
            'DropDownList dept = (DropDownList)gvr.FindControl("ddlDepartment");
            'DropDownList user = (DropDownList)gvr.FindControl("ddlUser");
            'DropDownList queue = (DropDownList)gvr.FindControl("ddlQueue");
            'CheckBox active = (CheckBox)gvr.FindControl("chkActive");
            'string deptVal = Request.Params[dept.ClientID.Replace("_","$")];
            'string userVal = Request.Params[user.ClientID.Replace("_", "$")];
            'string queueVal = Request.Params[queue.ClientID.Replace("_", "$")];
            'string activeVal = Request.Params[active.ClientID.Replace("_", "$")];
        End Sub

        ' -------------------------------------------------------------------
        '  Cancel
        ' -------------------------------------------------------------------
        Protected Sub grdDropReminder_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs) Handles MyGrdView.RowCancelingEdit
            MyGrdView.EditIndex = -1
            BindMyGridView(False)
        End Sub

        Private Sub OnUpdateReminderDrop(ByVal _fields As ReminderDrop, ByRef bool As Boolean)
            bool = ReminderDrop.Update_OneRecord(_fields)
        End Sub

        Protected Sub grdDropReminder_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs) Handles MyGrdView.RowUpdating

            Dim myEditRow As GridViewRow = MyGrdView.Rows(e.RowIndex)
            Dim myFields As New ReminderDrop()


            myFields.DropId = objMyGrdViewCommon.PrimaryTableId(myEditRow, "lblDropID")

            Dim sPullDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtPullDate")
            Dim sOnLineDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtOnLineDate")
            Dim sOffLineDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtOffLineDate")
            Dim sCstartDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtCstartDate")
            Dim sCendDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtCendDate")
            Dim sNR1StartDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtNR1startDate")
            Dim sNR1EndDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtNR1endDate")
            Dim sNR2StartDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtNR2startDate")
            Dim sNR2EndDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtNR2endDate")
            Dim sPrintRunDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtPrintRunDate")
            Dim sRpt_CStartDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtRptCStartDate")
            Dim sRpt_CEndDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtRptCEndDate")
            Dim sRpt_CEndShortDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtRptCEndShortDate")
            Dim sRpt_NR3EndDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtRptNR3EndDate")
            Dim sRpt_NR3StartDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtRptNR3StartDate")
            Dim sRpt_RCYEndDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtRptRCYEndDate")
            Dim sRpt_RCYStartDate As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtRptRCYStartDate")
            Dim sDropDesc As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtDropDescription")
            Dim sVM_NCP As String = objMyGrdViewCommon.RowDropDownListValue(myEditRow, "drpVmNpcEdit")
            Dim sMilerId As String = objMyGrdViewCommon.RowDropDownListValue(myEditRow, "drpMailerIDEdit")

            Dim sExp1Date As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtExp1Date")
            Dim sExp2Date As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtExp2Date")
            Dim sExp3Date As String = objMyGrdViewCommon.RowTextValue(myEditRow, "txtExp3Date")

            myFields.DropDescription = objMyGrdViewCommon.RowTextValue(myEditRow, "txtDropDescription")

            Dim boolActiveDrop As Boolean = objMyGrdViewCommon.RowCheckBoxValue(myEditRow, "chkActiveDropEdit")
            Dim boolActiveWeb As Boolean = objMyGrdViewCommon.RowCheckBoxValue(myEditRow, "chkActiveWebEdit")

            With myFields
                .ActiveDrop = Convert.ToInt16(boolActiveDrop)
                .ActiveWeb = Convert.ToInt16(boolActiveWeb)

                .PullDate = objMyGrdViewCommon.MyFormatDateTime(sPullDate)
                .OnLineDate = objMyGrdViewCommon.MyFormatDateTime(sOnLineDate)
                .OffLineDate = objMyGrdViewCommon.MyFormatDateTime(sOffLineDate)
                .CStartDate = objMyGrdViewCommon.MyFormatDateTime(sCstartDate)
                .CEndDate = objMyGrdViewCommon.MyFormatDateTime(sCendDate)

                .NR1StartDate = objMyGrdViewCommon.MyFormatDateTime(sNR1StartDate)
                .NR1EndDate = objMyGrdViewCommon.MyFormatDateTime(sNR1EndDate)
                .NR2StartDate = objMyGrdViewCommon.MyFormatDateTime(sNR2StartDate)
                .NR2EndDate = objMyGrdViewCommon.MyFormatDateTime(sNR2EndDate)
                .PrintRunDate = objMyGrdViewCommon.MyFormatDateTime(sPrintRunDate)
                .PrintRunSeries = objMyGrdViewCommon.RowTextValue(myEditRow, "txtPrintRunSeries")

                ' this will be a drop down list
                .MailerTypeID = sMilerId
                .VM_NPC = sVM_NCP

                .Rpt_CStartDate = objMyGrdViewCommon.MyFormatDateTime(sRpt_CStartDate)
                .Rpt_CEndDate = objMyGrdViewCommon.MyFormatDateTime(sRpt_CEndDate)
                .ReportShortDate = objMyGrdViewCommon.MyFormatDateTime(sRpt_CEndShortDate)
                .Rpt_NR3EndDate = objMyGrdViewCommon.MyFormatDateTime(sRpt_NR3EndDate)
                .Rpt_NR3StartDate = objMyGrdViewCommon.MyFormatDateTime(sRpt_NR3StartDate)
                .Rpt_RCYEndDate = objMyGrdViewCommon.MyFormatDateTime(sRpt_RCYEndDate)
                .Rpt_RCYStartDate = objMyGrdViewCommon.MyFormatDateTime(sRpt_RCYStartDate)
                .UserName = User.Identity.Name

                .Exp1Date = sExp1Date
                .Exp2Date = sExp2Date
                .Exp3Date = sExp3Date
            End With

            Dim bool As Boolean = False

            ' old way
            ' using a gridview control
            'bool = DataLayer.Update_RmrDrop_OneRecord(myFields)

            ' using event handler
            OnUpdateReminderDrop(myFields, bool)

            If bool = True Then
                MyGrdView.EditIndex = -1
                'Session("StateView") = "false"
                BindMyGridView(True)
            Else
                ' do a error handlers
            End If
        End Sub

        ' ---------------------------------------------------------------------
        '  Load event of page
        ' ---------------------------------------------------------------------
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            If Request.IsAuthenticated = True Then


                lblUserName.Text = User.Identity.Name

                If Not Page.IsPostBack Then
                    MailerColl = UsrMailer.GetAll()

                    If MailerColl.Count > 0 Then
                        With drpMailer
                            .DataSource = MailerColl
                            .DataValueField = "TypeId"
                            .DataTextField = "TypeDesc"
                            .DataBind()
                        End With
                    End If

                    Session("mailerID") = MailerID
                    BindMyGridView(False)
                End If
            End If
        End Sub

        ' Showinf yes or no
        Protected Function DisplayDiscontinuedAsYESorNO(ByVal nYesNo As Integer) As String
            If nYesNo = 1 Then
                Return "Yes"
            Else
                Return "No"
            End If
        End Function

        Protected Function DisplayInCheckBoxYESorNO(ByVal nYesNo As Integer) As Boolean
            If nYesNo = 1 Then
                Return True
            Else
                Return False
            End If
        End Function



        'Protected Sub rdoNsl_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoNsl.CheckedChanged
        '    Session("mailerID") = "nsl"
        '    BindMyGridView(False)
        'End Sub

        Protected Sub drpMailer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpMailer.SelectedIndexChanged
            Session("mailerID") = drpMailer.SelectedValue.ToString()
            BindMyGridView(False)
        End Sub
    End Class

End Namespace