Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.BusinessLayer


    Partial Class ReOrderPetId
        Inherits System.Web.UI.Page

        Private PetIdColl As UserPetIdCollection = Nothing
        Private sSearchingWord As String = String.Empty

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim iColumnOrder As Integer
            Dim iSortOrder As Integer
            Dim ArrSort(3) As String
            ArrSort(0) = "0" : ArrSort(1) = "0" : ArrSort(2) = "0" : ArrSort(3) = "0"

            If Request.IsAuthenticated = True Then

                'txtSearchName.Text = CType(Session("srtWord"), String)
                lblUserName.Text = User.Identity.Name
                ' get data from database

                If Request.QueryString.Count > 0 Then

                    PetIdColl = UserPetId.PetId_GetClient(CType(Session("srtWord"), String))
                    iColumnOrder = Integer.Parse(Request.QueryString("cn").ToString())
                    iSortOrder = Integer.Parse(Request.QueryString("sort").ToString())

                    Select Case iColumnOrder
                        Case 1
                            If iSortOrder = 0 Then
                                PetIdColl.Sort(UserPetIdCollection.UserFields.PatientName, False)
                                ArrSort(0) = "5"
                            Else
                                ArrSort(0) = "0"
                                PetIdColl.Sort(UserPetIdCollection.UserFields.PatientName, True)
                            End If

                        Case 2
                            If iSortOrder = 0 Then
                                PetIdColl.Sort(UserPetIdCollection.UserFields.ClientLastName, False)
                                ArrSort(1) = "5"
                            Else
                                ArrSort(1) = "0"
                                PetIdColl.Sort(UserPetIdCollection.UserFields.ClientLastName, True)
                            End If
                        Case 3
                            If iSortOrder = 0 Then
                                PetIdColl.Sort(UserPetIdCollection.UserFields.ClientFirstName, False)
                                ArrSort(2) = "5"
                            Else
                                ArrSort(2) = "0"
                                PetIdColl.Sort(UserPetIdCollection.UserFields.ClientFirstName, True)
                            End If
                        Case 4
                            If iSortOrder = 0 Then
                                ArrSort(3) = "5"
                                PetIdColl.Sort(UserPetIdCollection.UserFields.PracticeName, False)
                            Else
                                ArrSort(3) = "0"
                                PetIdColl.Sort(UserPetIdCollection.UserFields.PracticeName, True)
                            End If
                    End Select

                    If PetIdColl.Count > 0 Then
                        lblPetIdText.Text = ReturnPetIdAsHTML(PetIdColl, ArrSort)
                    End If

                Else

                    If Not Page.IsPostBack Then
                        btnRequestPetId.Visible = False
                        If Not PetIdColl Is Nothing Then
                            lblPetIdTotal.Text = "Count: " & PetIdColl.Count.ToString()
                            lblPetIdText.Text = ReturnPetIdAsHTML(PetIdColl, ArrSort)
                        End If
                    End If
                End If ' QueryString

            End If ' end Authenticated
        End Sub

        Private Function ReturnPetIdAsHTML(ByVal coll As UserPetIdCollection, ByVal SortId As String()) As String
            Dim strTemp As String = String.Empty
            Dim i As Integer
            Dim sUniqueId As String = String.Empty

            strTemp = "<table cellspacing=""0"" border=""1"" cellpadding=""0"" >"
            strTemp += "<tr>"
            strTemp += "<td height=""25"">&nbsp;</td>"
            strTemp += "<td height=""25""><a href=""ReOrderPetId.aspx?cn=1&sort=" & SortId(0) & """>Patient Name</a></td>"
            strTemp += "<td height=""25""><a href=""ReOrderPetId.aspx?cn=2&sort=" & SortId(1) & """>Last Name</a></td>"
            strTemp += "<td height=""25""><a href=""ReOrderPetId.aspx?cn=3&sort=" & SortId(2) & """>First Name</a></td>"
            strTemp += "<td height=""25""><a href=""ReOrderPetId.aspx?cn=4&sort=" & SortId(3) & """>Practice Name</a></td>"
            strTemp += "<td height=""25"">Batch</td>"
            strTemp += "</tr>"
            If coll.Count > 0 Then
                For i = 0 To coll.Count - 1
                    sUniqueId = coll.Item(i).ClientID.ToString & "_" & _
                    coll.Item(i).PatientID.ToString() & "_" & coll.Item(i).StrPracticeID

                    strTemp += "<tr id=""" & sUniqueId & """ onclick=""cb('" & sUniqueId & "')"">"
                    strTemp += "<td><input type=""checkbox"" name=""" & sUniqueId & _
                    """ /></td>"
                    strTemp += "<td NoWrap>" & coll.Item(i).PatientName & "</td>"
                    strTemp += "<td NoWrap>" & coll.Item(i).ClientLastName & "</td>"
                    strTemp += "<td NoWrap>" & coll.Item(i).ClientFirstName & "</td>"
                    strTemp += "<td NoWrap>" & coll.Item(i).PracticeName & "</td>"
                    strTemp += "<td NoWrap>" & coll.Item(i).OrderBatchID.ToString() & "</td>"
                    strTemp += "</tr>"
                Next i
            Else
                strTemp += "<tr>"
                strTemp += "<td colspan=""6"" NoWrap>No Records.</td>"
                strTemp += "</tr>"
            End If

            strTemp += "</table>"
            Return strTemp
        End Function

        Public Overrides Sub Dispose()
            If Not PetIdColl Is Nothing Then
                PetIdColl = Nothing
            End If
            MyBase.Dispose()
        End Sub 'Dispose

        Protected Sub btnRequestPetId_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRequestPetId.Click

            Dim i As Integer
            'Dim k As Integer
            Dim sCollectedValues As String = String.Empty
            sCollectedValues = txtSelectedValue.Value
            Dim sArrRowSelected As String() = Nothing
            Dim sRowSelected As String = String.Empty
            Dim sArrSingle As String() = Nothing

            ' ********************************************************************
            ' * get selected information and separate
            ' * by clientid, patientid, and practiceid
            ' ********************************************************************
            If sCollectedValues.Length > 0 Then
                sCollectedValues = sCollectedValues.Remove(sCollectedValues.Length - 1, 1)
                sArrRowSelected = sCollectedValues.Split(";")

                For i = 0 To sArrRowSelected.Length - 1
                    sRowSelected = sArrRowSelected(i)
                    sArrSingle = sRowSelected.Split("_")

                    If sArrSingle.Length > 0 Then
                        'For k = 0 To sArrSingle.Length - 1
                        'Next k
                        UserPetId.PetId_ReorderAdd(sArrSingle(0), sArrSingle(1), sArrSingle(2), User.Identity.Name)
                        sArrSingle = Nothing
                    End If

                Next i ' end LOOP

                ' Empty the selected value and refresh the page
                txtSelectedValue.Value = ""

                PetIdColl = UserPetId.PetId_GetClient(CType(Session("srtWord"), String))
                lblPetIdTotal.Text = "Count: " & PetIdColl.Count.ToString()

                'If PetIdColl.Count > 0 Then
                Dim ArrSort(3) As String
                ArrSort(0) = "0" : ArrSort(1) = "0" : ArrSort(2) = "0" : ArrSort(3) = "0"

                lblPetIdText.Text = ReturnPetIdAsHTML(PetIdColl, ArrSort)
                'End If
                lblMessage.Text = ""
            Else
                lblMessage.Text = "Please select patient(s) and then try again."

            End If
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Dim ArrSort(3) As String

            ArrSort(0) = "0" : ArrSort(1) = "0" : ArrSort(2) = "0" : ArrSort(3) = "0"

            lblPetIdTotal.Text = Nothing
            lblPetIdText.Text = Nothing

            txtSelectedValue.Value = ""
            Session("srtWord") = "%" & txtSearchName.Text.Trim() & "%"
            sSearchingWord = CType(Session("srtWord"), String)
            If sSearchingWord.Length > 3 Then

                btnSearch.Enabled = True
                PetIdColl = UserPetId.PetId_GetClient(sSearchingWord)

                If PetIdColl.Count > 0 Then
                    lblPetIdTotal.Text = "Count: " & PetIdColl.Count.ToString()
                    lblPetIdText.Text = ReturnPetIdAsHTML(PetIdColl, ArrSort)
                    lblMessage.Text = ""
                    btnRequestPetId.Visible = True
                Else
                    lblMessage.Text = "No records are found ..."
                    btnRequestPetId.Visible = False

                End If


            Else
                btnSearch.Enabled = False
            End If
        End Sub
    End Class
End Namespace