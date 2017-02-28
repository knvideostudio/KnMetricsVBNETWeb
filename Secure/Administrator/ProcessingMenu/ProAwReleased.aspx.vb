
Namespace VeterinaryMetrics.BusinessLayer


    Partial Class ProAwReleased
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            lblUserName.Text = User.Identity.Name

            If Not (Page.IsPostBack) Then

            End If
        End Sub

        Public Overrides Sub Dispose()
            MyBase.Dispose()
        End Sub

    End Class
End Namespace