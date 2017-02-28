Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class ReorderPetIdPending
        Inherits System.Web.UI.Page

        Private dv As System.Data.DataView = Nothing

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Request.IsAuthenticated = True Then

                lblUserName.Text = User.Identity.Name

                If Not Page.IsPostBack Then
                    dv = UserPetId.PetId_RequesrReorderPending()
                    If dv.Count > 0 Then
                        lblPetIdTotal.Text = dv.Count.ToString()
                        grvPending.DataSource = dv.Table.DefaultView
                        grvPending.DataBind()
                    Else
                        'grvPending.EmptyDataText = "There are no records found in the Welcome letter table."
                        ' grvPending.DataSource = Nothing
                        'grvPending.DataBind()
                    End If
                End If
            End If
        End Sub

        Public Overrides Sub Dispose()
            If Not dv Is Nothing Then
                dv.Dispose()
                dv = Nothing
            End If

            MyBase.Dispose()
        End Sub 'Dispose

    End Class
End Namespace