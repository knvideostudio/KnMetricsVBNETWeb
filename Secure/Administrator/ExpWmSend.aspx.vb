Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class ExpWmSend
        Inherits System.Web.UI.Page

        Private dv As System.Data.DataView = Nothing

        Public Overrides Sub Dispose()
            If Not dv Is Nothing Then
                dv.Dispose()
                dv = Nothing
            End If

            MyBase.Dispose()
        End Sub 'Dispose

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Request.IsAuthenticated = True Then

                lblUserName.Text = User.Identity.Name

                If Not Page.IsPostBack Then
                    dv = WelcomeLetter.WelcomeLetterReturnAlreadySend()
                    If dv.Count > 0 Then
                        grvTotalRecords.DataSource = dv
                        grvTotalRecords.DataBind()
                    Else
                        grvTotalRecords.EmptyDataText = "There are no records found in the Welcome letter table."
                        grvTotalRecords.DataSource = Nothing
                        grvTotalRecords.DataBind()
                    End If
                End If
            End If
        End Sub
    End Class

End Namespace