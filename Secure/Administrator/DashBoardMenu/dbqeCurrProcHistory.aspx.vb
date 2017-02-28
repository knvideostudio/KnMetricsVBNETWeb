
Namespace VeterinaryMetrics.BusinessLayer


    Partial Class CurrentProcessHistory
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
                mView = DashBoard.CurrentProcessHistoryView()

                grvCurrProcHistory.DataSource = mView
                grvCurrProcHistory.DataBind()
            End If
        End Sub
    End Class
End Namespace