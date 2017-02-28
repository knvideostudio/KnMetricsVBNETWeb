Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class AdminDropIssue
        Inherits System.Web.UI.Page

        Private dbIssueColl As DashBoardCollection = Nothing

        Public Overrides Sub Dispose()
            If Not dbIssueColl Is Nothing Then dbIssueColl = Nothing

            MyBase.Dispose()
        End Sub 'Dispose

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            ' load user name
            'Dim i As Integer = 0
            lblUserName.Text = User.Identity.Name

            If Not Page.IsPostBack Then

                dbIssueColl = DashBoard.GetIssues()

                If dbIssueColl.Count > 0 Then
                    repDbIssue.DataSource = dbIssueColl
                    repDbIssue.DataBind()

                    'For i = 0 To dbIssueColl.Count - 1
                    'Next i
                End If
            End If

        End Sub

    End Class

End Namespace


