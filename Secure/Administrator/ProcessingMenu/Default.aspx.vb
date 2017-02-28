Namespace VeterinaryMetrics.BusinessLayer

    Partial Class Processing
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            lblUserName.Text = User.Identity.Name

        End Sub

        Public Overrides Sub Dispose()
            MyBase.Dispose()
        End Sub

    End Class
End Namespace