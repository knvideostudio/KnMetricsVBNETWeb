Imports System
Imports System.Data
Imports System.Data.SqlClient

Imports System.Web.UI.WebControls

Imports VeterinaryMetrics.AccessLayerData
Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class ReminderDropPractice
        Inherits System.Web.UI.Page

        Private sPracticeId As String = String.Empty
        Private sDropId As String = String.Empty
        Private DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
        Private dv As New DataView

        Private objGrdViewFunction As New MyGridViewFunction()

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            lblUserName.Text = User.Identity.Name

            If Request.QueryString.Count > 0 Then
                sPracticeId = Request.QueryString("id").ToString()
                sDropId = Request.QueryString("dropId").ToString()

            End If

            If Not Page.IsPostBack Then
                BindGridView(Int32.Parse(sPracticeId), Int32.Parse(sDropId))
            End If


        End Sub

        Protected Sub grdvPractice_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs) Handles grdvPractice.RowDeleting

            Dim grvEditRow As GridViewRow = grdvPractice.Rows(e.RowIndex)
            Dim i As Int32 = GetGridViewRowOrderID(grvEditRow)
            Dim bDeleted As Boolean = False

            ' delete an actual record from database
            bDeleted = DataLayer.Delete_OneMatrix(i)

            ' bund again the datagrid
            sPracticeId = Request.QueryString("id").ToString()
            sDropId = Request.QueryString("dropId").ToString()
            BindGridView(Int32.Parse(sPracticeId), Int32.Parse(sDropId))

        End Sub

        Protected Sub grdDropReminder_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs) Handles grdvPractice.RowEditing

            Dim row As GridViewRow = grdvPractice.Rows(e.NewEditIndex)
            Dim sControlNameID As String = row.ClientID()

            grdvPractice.EditIndex = e.NewEditIndex

            ' this is using in ASPX page by JAVA SCRIPT
            Session("RowUpdIndex") = sControlNameID
            BindGridView(Int32.Parse(sPracticeId), Int32.Parse(sDropId))
        End Sub

        Protected Sub grdDropReminder_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs) Handles grdvPractice.RowCancelingEdit
            grdvPractice.EditIndex = -1
            BindGridView(Int32.Parse(sPracticeId), Int32.Parse(sDropId))
        End Sub

        ' Row Updating
        ' Nov 25, 2008 12:49 PM
        Protected Sub grdDropReminder_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs) Handles grdvPractice.RowUpdating

            ' increase the Array
            Dim ArrGrdFileds(27) As String
            Dim myEditRow As GridViewRow = grdvPractice.Rows(e.RowIndex)
            Dim sExp1 As String = String.Empty
            Dim sExp2 As String = String.Empty
            Dim sExp3 As String = String.Empty

            With objGrdViewFunction
                ArrGrdFileds(0) = .PrimaryTableId(myEditRow, "lblRowID")
                ArrGrdFileds(1) = .RowTextValue(myEditRow, "txtTemplate")
                ArrGrdFileds(2) = .RowTextValue(myEditRow, "txtBody")
                ArrGrdFileds(3) = .RowTextValue(myEditRow, "txtP1")
                ArrGrdFileds(4) = .RowTextValue(myEditRow, "txtP2")
                ArrGrdFileds(5) = .RowTextValue(myEditRow, "txtP3")

                ArrGrdFileds(6) = .RowTextValue(myEditRow, "txtP1_Disc")
                ArrGrdFileds(7) = .RowTextValue(myEditRow, "txtP2_Disc")
                ArrGrdFileds(8) = .RowTextValue(myEditRow, "txtP3_Disc")

                sExp1 = .RowTextValue(myEditRow, "txtExp1Date")
                sExp2 = .RowTextValue(myEditRow, "txtExp2Date")
                sExp3 = .RowTextValue(myEditRow, "txtExp3Date")

                ArrGrdFileds(9) = .MyFormatDateTime(sExp1)
                ArrGrdFileds(10) = .MyFormatDateTime(sExp2)
                ArrGrdFileds(11) = .MyFormatDateTime(sExp3)

                ArrGrdFileds(12) = .RowTextValue(myEditRow, "txtNPC1")
                ArrGrdFileds(13) = .RowTextValue(myEditRow, "txtNPC2")
                ArrGrdFileds(14) = .RowTextValue(myEditRow, "txtNPC3")
                ArrGrdFileds(15) = .RowTextValue(myEditRow, "txtSpecies")

                ArrGrdFileds(16) = .RowTextValue(myEditRow, "txtReminderType")
                ArrGrdFileds(17) = .RowTextValue(myEditRow, "txtSeries")
                ArrGrdFileds(18) = .RowTextValue(myEditRow, "txtMailerTypeID")

                ArrGrdFileds(19) = .RowTextValue(myEditRow, "txtPrintRunSeries")
                ArrGrdFileds(20) = .RowTextValue(myEditRow, "txtEditTWC")
                ArrGrdFileds(21) = .RowTextValue(myEditRow, "txtEditPSC")

                ' added on Aug 4, 2008
                ArrGrdFileds(22) = .RowTextValue(myEditRow, "txtPracticeEdit")

                ' added on Nov 25, 2008 12:53 PM
                ArrGrdFileds(23) = .RowTextValue(myEditRow, "txtPX")
                ArrGrdFileds(24) = .RowTextValue(myEditRow, "txtEditPXC")
                ArrGrdFileds(25) = .RowTextValue(myEditRow, "txtEditPxcColor")
                ArrGrdFileds(26) = .RowTextValue(myEditRow, "txtEditCRC")

            End With

            ' Update table
            Dim myBool As Boolean = False
            myBool = DataLayer.Update_MatrixPstn_OneRecord(ArrGrdFileds)

            If myBool = True Then
                grdvPractice.EditIndex = -1
            End If

            ' Erase ArrGrdFileds
            ' RmdDropPractice.aspx?id=900&dropId=370
            Response.Redirect("RmdDropPractice.aspx?id=" & ArrGrdFileds(22) & "&dropId=" & sDropId)
            ' BindGridView(Int32.Parse(sPracticeId), Int32.Parse(sDropId))
        End Sub

        Private Function GetGridViewRowOrderID(ByVal row As GridViewRow) As Int32
            Dim n As Int32 = 0
            Dim _ctl As Label = CType(row.FindControl("lblRowID"), Label)

            If _ctl Is Nothing Then
                Throw New Exception("get")
            End If

            n = Int32.Parse(_ctl.Text)
            Return n

        End Function

        ' bind gridview
        Private Sub BindGridView(ByVal PracticeId As Int32, ByVal DropId As Int32)
            dv = DataLayer.Retrun_All_Matrix(PracticeId, DropId)

            grdvPractice.DataSource = dv
            grdvPractice.DataBind()
        End Sub

        Protected Sub lnBtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnBtnAdd.Click
            lnBtnGridInsertRecord.Visible = True
            lnBtnGridCancelRecord.Visible = True
            grdvPractice.ShowFooter = True
            lnBtnAdd.Visible = False

            'Dim row As GridViewRow = grdvPractice.Rows(grdvPractice.FooterRow.RowIndex)
            ' Dim sControlNameID As String = row.ClientID()
            ' this is using in ASPX page by JAVA SCRIPT
            Session("RowInsIndex") = grdvPractice.FooterRow.ClientID() 'sControlNameID

            BindGridView(Int32.Parse(sPracticeId), Int32.Parse(sDropId))
        End Sub

        ' Last Updated on Nov 25, 2008 1:34 PM
        Protected Sub lnBtnGridInsertRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnBtnGridInsertRecord.Click

            Dim myGrdRow As GridViewRow = grdvPractice.FooterRow

            Dim ArrGrdFileds(29) As String
            Dim ArrErrors(2) As String

            Dim sExp1 As String = String.Empty
            Dim sExp2 As String = String.Empty
            Dim sExp3 As String = String.Empty

            Dim TwsLongText As String = String.Empty
            Dim PscLongText As String = String.Empty
            Dim PxcLongText As String = String.Empty
            Dim PxcColorLongText As String = String.Empty
            Dim CrcLongText As String = String.Empty

            Try
                sPracticeId = Request.QueryString("id").ToString()
                sDropId = Request.QueryString("dropId").ToString()
            Catch ex As Exception

            End Try


            With objGrdViewFunction
                ArrGrdFileds(0) = sPracticeId
                ArrGrdFileds(1) = sDropId
                ArrGrdFileds(2) = .RowTextValue(myGrdRow, "txtIstTemplate")
                ArrGrdFileds(3) = .RowTextValue(myGrdRow, "txtIstBody")
                ArrGrdFileds(4) = .RowTextValue(myGrdRow, "txtIstP1")
                ArrGrdFileds(5) = .RowTextValue(myGrdRow, "txtIstP2")
                ArrGrdFileds(6) = .RowTextValue(myGrdRow, "txtIstP3")

                ArrGrdFileds(7) = .RowTextValue(myGrdRow, "txtIstP1_Disc")
                ArrGrdFileds(8) = .RowTextValue(myGrdRow, "txtIstP2_Disc")
                ArrGrdFileds(9) = .RowTextValue(myGrdRow, "txtIstP3_Disc")

                sExp1 = .RowTextValue(myGrdRow, "txtIstExp1Date")
                sExp2 = .RowTextValue(myGrdRow, "txtIstExp2Date")
                sExp3 = .RowTextValue(myGrdRow, "txtIstExp3Date")

                ArrGrdFileds(10) = .MyFormatDateTime(sExp1)
                ArrGrdFileds(11) = .MyFormatDateTime(sExp2)
                ArrGrdFileds(12) = .MyFormatDateTime(sExp3)

                ArrGrdFileds(13) = .RowTextValue(myGrdRow, "txtIstNPC1")
                ArrGrdFileds(14) = .RowTextValue(myGrdRow, "txtIstNPC2")
                ArrGrdFileds(15) = .RowTextValue(myGrdRow, "txtIstNPC3")
                ArrGrdFileds(16) = .RowTextValue(myGrdRow, "txtIstSpecies")

                ArrGrdFileds(17) = .RowTextValue(myGrdRow, "txtIstReminderType")
                ArrGrdFileds(18) = .RowTextValue(myGrdRow, "txtIstSeries")
                ArrGrdFileds(19) = .RowTextValue(myGrdRow, "txtIstMailerTypeID")

                ArrGrdFileds(20) = .RowTextValue(myGrdRow, "txtIstPrintRunSeries")

                TwsLongText = .RowTextValue(myGrdRow, "txtIstEditTWC")
                TwsLongText = Server.HtmlEncode(TwsLongText)
                ArrGrdFileds(21) = TwsLongText

                PscLongText = .RowTextValue(myGrdRow, "txtIstEditPSC")
                PscLongText = Server.HtmlEncode(PscLongText)
                ArrGrdFileds(22) = PscLongText

                ' Nov 25, 2008
                ArrGrdFileds(23) = .RowTextValue(myGrdRow, "txtIstPX")

                PxcLongText = .RowTextValue(myGrdRow, "txtIstEditPXC")
                PxcLongText = Server.HtmlEncode(PxcLongText)
                ArrGrdFileds(24) = PxcLongText

                PxcColorLongText = .RowTextValue(myGrdRow, "txtIstEditPxcColor")
                PxcColorLongText = Server.HtmlEncode(PxcColorLongText)
                ArrGrdFileds(25) = PxcColorLongText

                CrcLongText = .RowTextValue(myGrdRow, "txtIstEditCRC")
                CrcLongText = Server.HtmlEncode(CrcLongText)
                ArrGrdFileds(26) = CrcLongText


            End With

            ' verify the DATA
            ArrErrors = DataLayer.AddNew_Matrix(ArrGrdFileds)

            Erase ArrGrdFileds

            lnBtnAdd.Visible = True
            grdvPractice.ShowFooter = False
            lnBtnGridInsertRecord.Visible = False
            lnBtnGridCancelRecord.Visible = False
            BindGridView(Int32.Parse(sPracticeId), Int32.Parse(sDropId))
        End Sub

        Protected Sub lnBtnGridCancelRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnBtnGridCancelRecord.Click

            lnBtnAdd.Visible = True
            grdvPractice.ShowFooter = False
            lnBtnGridInsertRecord.Visible = False
            lnBtnGridCancelRecord.Visible = False
            BindGridView(Int32.Parse(sPracticeId), Int32.Parse(sDropId))
        End Sub
    End Class

End Namespace
