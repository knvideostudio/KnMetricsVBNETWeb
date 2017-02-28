Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.BusinessLayer


    Partial Class DirectMailAssign
        Inherits System.Web.UI.Page

        Private PracticeList As DirectMailCollection = Nothing
        Private PrTable As System.Data.DataTable = Nothing


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            If Request.IsAuthenticated = True Then
                lblUserName.Text = User.Identity.Name

                PracticeList = DirectMail.DirectMail_GetPractices()

                If PracticeList.Count > 0 Then


                    PrTable = DirectMail.DirectMail_CreateDtTable(PracticeList, "AvailPractices")

                    ' onchange="SelectText();"
                    ' add Java Script Function
                    PractListBox.Attributes.Add("onchange", "SelectText();")

                    PractListBox.DataSource = PrTable
                    PractListBox.DataTextField = "PrName"
                    PractListBox.DataValueField = "PrId"
                    ' PractListBox.Rows = ListOfCodes.Count
                    PractListBox.DataBind()

                    btnPracticeAdd.Attributes.Add("onclick", "SaveValues();")

                End If


                If Not Page.IsPostBack Then



                End If
            End If

        End Sub

        Protected Sub btnPracticeAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPracticeAdd.Click
            Dim myValues As String = String.Empty
            myValues = txtValues.Value


            If myValues.Length > 0 Then
                lblMessageShow.Text = "Practices have been added!"

                DirectMail.DirectMail_AssignPractices(myValues)
                txtValues.Value = ""
                Response.Redirect("DirectMailAssign.aspx")
            End If

            If myValues.Length = 0 Then
                lblMessageShow.Text = "Please select practice from the left side."
            End If



        End Sub
    End Class
End Namespace