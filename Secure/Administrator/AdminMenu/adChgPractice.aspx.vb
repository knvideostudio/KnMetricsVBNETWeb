Imports System.IO

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class AdminChangePractice
        Inherits System.Web.UI.Page

        ' Private sWinServiceChangePractice As String = "D:\\RunFileWatcher\\FleInt2ChangePrt.bin"
        ' Private sWinServiceChgPrcTest As String = "D:\\RunFileWatcher\\FleInt2ChangePrtText.txt"
        Private PeacticeColl As CnvtPracticeCollection = Nothing
        Private dvData As System.Data.DataView = Nothing

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

        Public Overrides Sub Dispose()
            If Not PeacticeColl Is Nothing Then PeacticeColl = Nothing
            MyBase.Dispose()
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            ' load user name
            lblUserName.Text = User.Identity.Name

            ' register the Java Script
            btnStartNow.Attributes.Add("onClick", "return ConfirmExec();")

            If System.IO.File.Exists(GlobalsVar.WS_CHANGE_PRACTICE) Then
                drpPracticeDpl.Enabled = False
                btnStartNow.Enabled = False
                lblMessageProcess.Text = "Please Wait! The Converting is Working ..."
            Else
                BuildDataGridView()
                drpPracticeDpl.Enabled = True
                btnStartNow.Enabled = True
                lblMessageProcess.Text = ""
            End If

            ' load Grid View
            'grdPreData.DataSource = CnvtPractice.GetPreAlreadyConverted()
            'grdPreData.DataBind()
            '  grdvChgPractice.DataSource = CnvtPractice.GetPreAlreadyConverted()
            '  grdvChgPractice.DataBind()

            If Not Page.IsPostBack Then
                PeacticeColl = CnvtPractice.GetPreNotConverted()
                If PeacticeColl.Count > 0 Then
                    With drpPracticeDpl
                        .DataSource = PeacticeColl
                        .DataValueField = "PracticeId"
                        .DataTextField = "PracticeText"
                        .DataBind()
                    End With
                End If
            Else
                ' do something else
            End If
        End Sub


        Private Sub BuildDataGridView()
            dvData = CnvtPractice.GetPreAlreadyConverted()

            If dvData.Count > 0 Then
                If (Not SortExpression Is Nothing) And (Not SortExpression Is String.Empty) Then
                    dvData.Sort = SortExpression & " " + SortDirection
                End If
                grdPreData.DataSource = dvData
                grdPreData.DataBind()
            Else
                grdPreData.DataSource = Nothing
                grdPreData.EmptyDataText = "There are no records found."
                grdPreData.DataBind()
            End If

        End Sub
        ' ---------------------------------------------------------------------
        '   Sorting enable Load event of page
        Protected Sub grdPreData_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles grdPreData.Sorting

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

            BuildDataGridView()
        End Sub

        'Private Sub CreateTextFile(ByVal sText As String)

        '    If File.Exists(sWinServiceChgPrcTest) Then
        '        Try
        '            File.Delete(sWinServiceChgPrcTest)
        '        Catch ex As Exception
        '            Throw New Exception("File cannot be deleted." & ex.Message)
        '        End Try

        '    End If

        '    Using sw As StreamWriter = New StreamWriter(sWinServiceChgPrcTest)
        '        sw.WriteLine(sText)
        '        sw.Close()
        '    End Using
        'End Sub

        'Private Function CreateEmptyFile(ByVal sPathCombineFile As String) As String
        '    Dim mybEmptyFile As Byte() = New Byte() {80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        '    Dim fs As System.IO.FileStream = Nothing
        '    Dim sTemp As String = ""

        '    Try
        '        fs = System.IO.File.Create(sPathCombineFile)
        '        fs.Write(mybEmptyFile, 0, mybEmptyFile.Length)
        '    Catch ex As Exception
        '        sTemp = "Error: " & ex.Message
        '    Finally
        '        If Not fs Is Nothing Then
        '            If Not mybEmptyFile Is Nothing Then mybEmptyFile = Nothing
        '            fs.Flush()
        '            fs.Close()
        '            fs = Nothing
        '        End If
        '    End Try

        '    Return sTemp
        'End Function

        Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender

            If System.IO.File.Exists(GlobalsVar.WS_CHANGE_PRACTICE) Then
                Response.AddHeader("REFRESH", "45;URL=adChgPractice.aspx")
            End If

            'If System.IO.File.Exists(sWinServiceUploadCsvFile) Then
            '    Response.AddHeader("REFRESH", "15;URL=Default.aspx")
            'End If

            ' <META HTTP-EQUIV=REFRESH CONTENT="5;URL=http://mbiz.co.th/tips_tricks/"> 
        End Sub

        Protected Sub btnStartNow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStartNow.Click
            Dim sTemp As String = String.Empty

            sTemp = User.Identity.Name & ";" & drpPracticeDpl.SelectedValue.ToString()
            GlobalsVar.CreateDataTextFile(sTemp, GlobalsVar.WS_CHANGE_PRACTICE_TEXT)

            sTemp = GlobalsVar.CreateEmptyBinaryFile(GlobalsVar.WS_CHANGE_PRACTICE)
            System.Threading.Thread.Sleep(3000)

            Response.Redirect("adChgPractice.aspx")
        End Sub

        Protected Sub drpPracticeDpl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpPracticeDpl.SelectedIndexChanged
            lblPrcMessage.Text = "Selceted practice is: " & drpPracticeDpl.SelectedValue.ToString()

        End Sub
    End Class

End Namespace


