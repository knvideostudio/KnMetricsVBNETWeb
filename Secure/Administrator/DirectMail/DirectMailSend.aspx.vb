Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class DirectMailSend
        Inherits System.Web.UI.Page

        Private PracticeList As DirectMailCollection = Nothing

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            lblUserName.Text = User.Identity.Name
            Dim tmpStr As String = String.Empty
            Dim sDate As String = ""
            Dim i As Integer = 0

            If Request.IsAuthenticated = True Then
                lblUserName.Text = User.Identity.Name

                PracticeList = DirectMail.DirectMail_GetPracticesAlreadySend()
                tmpStr = "<table style=""width: 700px;"" cellpadding=""0"" cellspacing=""0"" border=""1"">"

                If PracticeList.Count > 0 Then
                    'sDate = PracticeList.Item(0).ProcessedDate

                    tmpStr += "<tr><td>Practice ID</td><td>Name</td><td>Run Date</td><td>Species</td>" & _
                    "<td>Send</td><td>Total Canine</td><td>Total Feline</td><td>Batch Id</td></tr>"

                    For i = 0 To PracticeList.Count - 1
                        If sDate.CompareTo(PracticeList.Item(i).ProcessedDate) Then
                            tmpStr += "<tr><td colspan=""7"">&nbsp;</td></tr>"
                        End If
                        tmpStr += "<tr><td>" & PracticeList.Item(i).PracticeId.ToString() & "</td>" & _
                        "<td>" & PracticeList.Item(i).PracticeName & "</td>" & _
                        "<td>" & PracticeList.Item(i).ProcessedDate.Remove(10, PracticeList.Item(i).ProcessedDate.Length - 10) & "</td>" & _
                        "<td>" & PracticeList.Item(i).SpeciesName & "</td>" & _
                        "<td>" & PracticeList.Item(i).AlreadySendTotal.ToString() & "</td>" & _
                        "<td>" & PracticeList.Item(i).CanineTotal.ToString() & "</td>" & _
                        "<td>" & PracticeList.Item(i).FelineTotal.ToString() & "</td>" & _
                        "<td>" & PracticeList.Item(i).BatchId.ToString() & "</td>" & _
                        "</tr>"

                        sDate = PracticeList.Item(i).ProcessedDate
                    Next
                    tmpStr += "</table>"

                    lblPracticeSend.Text = tmpStr
                Else
                    lblPracticeSend.Text = "No records!"
                End If

                'If Not Page.IsPostBack Then
                'End If

            End If
        End Sub

    End Class

End Namespace