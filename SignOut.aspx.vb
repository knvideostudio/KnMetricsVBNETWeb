Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.BusinessLayer


    Partial Class SignOut
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            If Request.IsAuthenticated = True Then
                lblUserName.Text = User.Identity.Name
                FormsAuthentication.SignOut()
                Session.Abandon()
            Else
                lblUserName.Text = "No User"
            End If
        End Sub

    End Class
End Namespace