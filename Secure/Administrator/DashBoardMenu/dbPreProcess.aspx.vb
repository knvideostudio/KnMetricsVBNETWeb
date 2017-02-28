Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class AdminDropPracticeProcess
        Inherits System.Web.UI.Page

        ' Private PeacticeColl As CnvtPracticeCollection = Nothing

        Public Overrides Sub Dispose()
            ' If Not PeacticeColl Is Nothing Then PeacticeColl = Nothing

            MyBase.Dispose()
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            ' load user name
            ' Dim i As Integer = 0
            lblUserName.Text = User.Identity.Name

            If Not Page.IsPostBack Then
                ' PeacticeColl = CnvtPractice.GetPreNotConverted()
                ' If PeacticeColl.Count > 0 Then
                '    With drpPracticeDpl
                '        .DataSource = PeacticeColl
                '        .DataValueField = "PracticeId"
                '        .DataTextField = "PracticeText"
                '        .DataBind()
                '    End With
                ' End If
            Else
                ' Do Something Else
            End If

        End Sub

        Protected Sub btnShowNow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowNow.Click
            Dim sPractice As String = ""
            Dim iPractice As Int32 = 0

            Try
                sPractice = txtPractice.Text.ToString()
                iPractice = Int32.Parse(sPractice)

                grdPreData.DataSource = DashBoard.GetProcess(iPractice)
                grdPreData.DataBind()
                lblErrorMsg.Text = ""
            Catch ex As Exception
                lblErrorMsg.Text = "Error: <br />" & ex.Message
            End Try


        End Sub
    End Class

End Namespace


