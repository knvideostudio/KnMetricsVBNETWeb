
Namespace VeterinaryMetrics.BusinessLayer


    Partial Class ReportQueue
        Inherits System.Web.UI.Page

        Private mView As System.Data.DataView = Nothing

        Public Overrides Sub Dispose()
            If Not mView Is Nothing Then mView = Nothing

            MyBase.Dispose()
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            ' load user name
            lblUserName.Text = User.Identity.Name

            If Not Page.IsPostBack Then
                mView = DashBoard.ReoportQueueView()

                grvRepQueue.DataSource = mView
                grvRepQueue.DataBind()
            End If
        End Sub

    End Class
End Namespace