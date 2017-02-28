Imports System.IO
Imports System.Web.UI.WebControls

Namespace VeterinaryMetrics.BusinessLayer


    Partial Class FeeAnalysisSummary
        Inherits System.Web.UI.Page

        Private Const CURRENT_WEB_PAGE As String = "FeeAnalysisSummary.aspx"

        Private PeacticeColl As CnvtPracticeCollection = Nothing
        Private dtArr() As String = Nothing
        Private SummaryView As System.Data.DataView = Nothing


        Private ReadOnly Property iPractice() As Int32
            Get
                Dim i As Int32

                Try
                    i = Int32.Parse(drpPractice.SelectedValue.ToString())

                Catch ex As Exception
                    i = -1
                End Try

                Return (i)
            End Get
        End Property

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            ' Display User Name
            lblUserName.Text = User.Identity.Name

            ' get reports date
            dtArr = ReminderBlock.GetReportsDate()
            lblPrevStartDate.Text = dtArr(0)
            lblPrevEndDate.Text = dtArr(1)

            If System.IO.File.Exists(GlobalsVar.WS_FEE_ANALYSIS_SUMMARY) Then
                btnReBuild.BackColor = Drawing.Color.DarkGray
                btnReBuild.Enabled = False
                'btnStartNow.Enabled = False
                lblMessageShow.Text = "Please Wait! The Report is building ..."
            Else
                'BuildDataGridView()
                'drpPracticeDpl.Enabled = True
                btnReBuild.BackColor = Drawing.Color.DarkBlue
                btnReBuild.Enabled = True

                lblMessageShow.Text = ""
            End If

            If Not Page.IsPostBack Then
                PeacticeColl = CnvtPractice.GetPracticesSummaryReport()

                If PeacticeColl.Count > 0 Then
                    With drpPractice
                        .DataSource = PeacticeColl
                        .DataValueField = "PracticeId"
                        .DataTextField = "PracticeText"
                        .DataBind()
                    End With
                End If
            End If
        End Sub

        ' Dispose
        Public Overrides Sub Dispose()
            If Not PeacticeColl Is Nothing Then PeacticeColl = Nothing
            If Not SummaryView Is Nothing Then SummaryView = Nothing

            MyBase.Dispose()
        End Sub

        Protected Sub btnReBuild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReBuild.Click
            Dim dt As DateTime
            Dim StartDate As String = ""
            Dim EndDate As String = ""
            Dim sTemp As String = ""

            Try
                dt = DateTime.Parse(txtStartDate.Text)
                StartDate = txtStartDate.Text
            Catch ex As Exception
                lblMessageShow.ForeColor = Drawing.Color.Red
                lblMessageShow.Text = "Start Date is invalid or Empty!"

            End Try

            Try
                dt = DateTime.Parse(txtEndDate.Text)
                EndDate = txtEndDate.Text
            Catch ex As Exception
                lblMessageShow.ForeColor = Drawing.Color.Red
                lblMessageShow.Text = "End Date is invalid or Empty!"
            End Try

            sTemp = StartDate & ";" & EndDate
            GlobalsVar.CreateDataTextFile(sTemp, GlobalsVar.WS_FEE_ANALYSIS_SUMMARY_TEXT)
            System.Threading.Thread.Sleep(1000)

            sTemp = GlobalsVar.CreateEmptyBinaryFile(GlobalsVar.WS_FEE_ANALYSIS_SUMMARY)
            System.Threading.Thread.Sleep(3000)

            Response.Redirect(CURRENT_WEB_PAGE)
        End Sub

        Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender

            If System.IO.File.Exists(GlobalsVar.WS_FEE_ANALYSIS_SUMMARY) Then
                Response.AddHeader("REFRESH", "60;URL=" & CURRENT_WEB_PAGE)
            End If

        End Sub

        Protected Sub drpPractice_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpPractice.SelectedIndexChanged
           
            If iPractice > 0 Then
                SummaryView = ReminderBlock.GetReportsDateView(iPractice)

                grvSummary.DataSource = SummaryView
                grvSummary.DataBind()

            End If

        End Sub


        'Protected Overrides Sub RenderChildren(ByVal output As HtmlTextWriter)
        '    If HasControls() Then
        '        ' Render Children in reverse order.
        '        Dim i As Integer

        '        For i = Controls.Count - 1 To 0 Step -1
        '            Controls(i).RenderControl(output)
        '        Next

        '    End If
        'End Sub 'RenderChildren

        'Protected Overrides Sub Render(ByVal output As HtmlTextWriter)
        '    output.Write(("<br>Message from Control : " + Message))
        '    output.Write(("Showing Custom controls created in reverse" + "order"))
        '    ' Render Controls.
        '    RenderChildren(output)
        'End Sub

        Protected Sub btnGenExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenExcel.Click

            Dim dt As System.Data.DataTable
            Dim tab As String = ""
            Dim sb As New System.Text.StringBuilder()
            Dim sFinalData As String = String.Empty
            Dim i As Int32 = -1

            SummaryView = ReminderBlock.GetReportsDateView(iPractice)
            dt = SummaryView.Table

            For Each dc As System.Data.DataColumn In dt.Columns
                sb.Append(tab).Append(dc.ColumnName)
                tab = vbTab
            Next dc


            sFinalData = sb.ToString()
            sb.AppendLine()

            For Each dr As System.Data.DataRow In dt.Rows

                tab = ""

                For i = 0 To dt.Columns.Count - 1
                    sb.Append(tab).Append(dr(i).ToString)
                    tab = vbTab
                Next i

                sb.AppendLine()
            Next dr

            Dim FilePathName As String = Server.MapPath(".") & "\myTest.xls"

            If File.Exists(FilePathName) Then
                Try
                    File.Delete(FilePathName)
                Catch ex As Exception

                End Try
            End If

            Dim fs As New FileStream(FilePathName, FileMode.Create)
            Dim BWriter As New BinaryWriter(fs, Encoding.GetEncoding("UTF-8"))
            BWriter.Write(sb.ToString())
            BWriter.Close()
            fs.Close()

            lblMessageShow.Text = "<a href=""myTest.xls"">Click here to " & _
            "download the file.</a>"
            'Dim filename As String
            'Dim FilePathName As String
            'Dim DocFileName As String
            'Dim HtmlInfo As String


            'Dim tw As New StringWriter
            'Dim hw As New HtmlTextWriter(tw)

            'Dim form = New HtmlForm()

            'grvSummary.Page.EnableViewState = False
            'form.Controls.Add(grvSummary)
            ' ''grvSummary.RenderControl(hw)
            'Me.Controls.Add(form)



            'HtmlInfo = tw.ToString().Trim()
            'form.RenderControl(hw)
            ' '' DocFileName = filename + "\test.xls"
            'FilePathName = filename + "\test.xls" ' Request.PhysicalPath
            ' '' FilePathName = FilePathName.Substring(0, FilePathName.LastIndexOf("\"))
            ' '' FilePathName = FilePathName + "\" + DocFileName
            ' '' File.Delete(FilePathName)

            'Dim fs As New FileStream(FilePathName, FileMode.Create)
            'Dim BWriter As New BinaryWriter(fs, Encoding.GetEncoding("UTF-8"))
            'BWriter.Write(HtmlInfo)
            'BWriter.Close()
            'fs.Close()

        End Sub
    End Class

End Namespace