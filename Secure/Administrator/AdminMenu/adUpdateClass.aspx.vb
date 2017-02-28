Imports System.IO
Imports System.Collections.Generic

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class AdminUpdateClass
        Inherits System.Web.UI.Page

        Private PeacticeColl As CnvtPracticeCollection = Nothing
        Private ReminderColl As Dictionary(Of String, Boolean)
        Private RdCurrentColl As Dictionary(Of String, Boolean)
        'Public iTotalRows As Int32 = 0

        Public Overrides Sub Dispose()

            If Not ReminderColl Is Nothing Then ReminderColl = Nothing
            If Not RdCurrentColl Is Nothing Then RdCurrentColl = Nothing
            If Not PeacticeColl Is Nothing Then PeacticeColl = Nothing

            MyBase.Dispose()
        End Sub



        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            ' load user name
            lblUserName.Text = User.Identity.Name
            '  iTotalRows = grdPreData.Rows.Count

            'Session("sessArrSize") = 1
            ' register the Java Script
            ' btnStartNow.Attributes.Add("onClick", "return ConfirmExec();")

            ' btnStartNow.Attributes.Add("onClick", "myCount();")
            'If System.IO.File.Exists(sWinServiceChangePractice) Then
            '    drpPracticeDpl.Enabled = False
            '    btnStartNow.Enabled = False
            '    lblMessageProcess.Text = "Please Wait! The Converting is Working ..."
            'Else
            '    drpPracticeDpl.Enabled = True
            '    btnStartNow.Enabled = True
            '    lblMessageProcess.Text = ""
            'End If

            ' load Grid View
            
            '  grdvChgPractice.DataSource = CnvtPractice.GetPreAlreadyConverted()
            '  grdvChgPractice.DataBind()

            If Not Page.IsPostBack Then
                PeacticeColl = AdminTasks.GetPractices()
                If PeacticeColl.Count > 0 Then
                    With drpPracticeDpl
                        .DataSource = PeacticeColl
                        .DataValueField = "PracticeId"
                        .DataTextField = "PracticeText"
                        .DataBind()
                    End With
                End If
            Else
                ' do something else
            End If
        End Sub

        'Private Sub CreateTextFile(ByVal sText As String)

        '    If File.Exists(sWinServiceChgPrcTest) Then
        '        Try
        '            File.Delete(sWinServiceChgPrcTest)
        '        Catch ex As Exception
        '            Throw New Exception("File cannot be deleted." & ex.Message)
        '        End Try

        '    End If

        '    Using sw As StreamWriter = New StreamWriter(sWinServiceChgPrcTest)
        '        sw.WriteLine(sText)
        '        sw.Close()
        '    End Using
        'End Sub

        'Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender

        '    If System.IO.File.Exists(sWinServiceChangePractice) Then
        '        Response.AddHeader("REFRESH", "45;URL=adChgPractice.aspx")
        '    End If

        '    'If System.IO.File.Exists(sWinServiceUploadCsvFile) Then
        '    '    Response.AddHeader("REFRESH", "15;URL=Default.aspx")
        '    'End If

        '    ' <META HTTP-EQUIV=REFRESH CONTENT="5;URL=http://mbiz.co.th/tips_tricks/"> 
        'End Sub

        Protected Sub btnStartNow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStartNow.Click

            Dim strPractice As String = String.Empty
            Dim iPractice As Int32 = 901
            strPractice = drpPracticeDpl.SelectedValue.ToString()

            Try
                iPractice = Int32.Parse(strPractice)
            Catch ex As Exception

            End Try

            ' RdCurrentColl = AdminTasks.GetCurrentData(iPractice, 1)

            'sTemp = CreateEmptyFile(sWinServiceChangePractice)
            'System.Threading.Thread.Sleep(3000)

            'Response.Redirect("adChgPractice.aspx")
            ' iMode is 0 brings all fields into GridView
            grdPreData.DataSource = AdminTasks.GetClientClass(iPractice, 0)
            grdPreData.DataBind()

            'If grdPreData.Rows.Count > 0 Then
            '    Session("sessArrSize") = grdPreData.Rows.Count.ToString()
            'End If

            btnStartNow.Enabled = False
            btnSave.Enabled = True



            '  litJavaScriptExec.Text = "<script language=""javascript"" type=""text/javascript"">GetPreviousHistory();</script>"
            'Dim cs As ClientScriptManager = Page.ClientScript
            'Dim csName As String = "jonnymike"
            'Dim PageType As Type = Me.GetType()

            '' If (Not cs.IsClientScriptBlockRegistered(PageType, csName)) Then

            '' cs.RegisterClientScriptInclude(cstype, csName, csurl)
            'cs.RegisterClientScriptInclude(PageType, csName, "myCount();")

            '  End If




            'Page.ClientScript.RegisterClientScriptBlock(PageType, "one", "myCount();")


            'txtRecordCheck.Value = ""
            ' txtRecordCheck.Text = CType(Session("sessArrSize"), String)
        End Sub

        Protected Sub drpPracticeDpl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpPracticeDpl.SelectedIndexChanged
            lblPrcMessage.Text = "Selceted practice is: " & drpPracticeDpl.SelectedValue.ToString()

            grdPreData.DataSource = Nothing
            grdPreData.EmptyDataText = "Please press the Display button ..."
            grdPreData.DataBind()

            btnStartNow.Enabled = True
            btnSave.Enabled = False
            ' txtRecordCheck.Text = ""
        End Sub

        ' Display number tick or untick
        Protected Function DisplayTick(ByVal objYesNo As Object) As Boolean
            Dim iYesNo As Integer

            ' System.DBNull 
            'If String.IsNullOrEmpty(objYesNo) Then
            Try
                iYesNo = Integer.Parse(objYesNo)
            Catch ex As Exception
                iYesNo = 4
            End Try

            'End If

            If iYesNo = 1 Then Return (True)
            If iYesNo = 0 Then Return (False)
            If iYesNo = 4 Then Return (False)

            Return (False)
        End Function

        Protected Function DisplayYESorNO(ByVal objYesNo As Object) As String
            Dim iYesNo As Integer

            ' System.DBNull 
            'If String.IsNullOrEmpty(objYesNo) Then
            Try
                iYesNo = Integer.Parse(objYesNo)
            Catch ex As Exception
                iYesNo = 4
            End Try

            'End If

            If iYesNo = 1 Then Return ("Yes")
            If iYesNo = 0 Then Return ("No")
            If iYesNo = 4 Then Return ("None")

            Return ("None")
        End Function

        ' Save data to database
        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

            Dim SelectedRows As String = txtRecordCheck.Value
            Dim SelectedPractice As String = ""
            Dim iPractice As Int32 = -1
            Dim RowsArr() As String = Nothing
            Dim RowValueArr() As String = Nothing
            Dim i As Integer = 0
            Dim sUserLogged As String = User.Identity.Name


            ' Get Current Values from Database
            Try
                SelectedPractice = drpPracticeDpl.SelectedValue.ToString()
                iPractice = Int32.Parse(SelectedPractice)

            Catch ex As Exception

            End Try

            RdCurrentColl = AdminTasks.GetCurrentData(iPractice, 1)


            ' new object created as Dictionary
            ReminderColl = New Dictionary(Of String, Boolean)

            If Not String.IsNullOrEmpty(SelectedRows) Then
                RowsArr = SelectedRows.Split(";")

                For i = 0 To RowsArr.Length - 1
                    RowValueArr = RowsArr(i).Split("=")

                    If RowValueArr.Length = 2 Then
                        If Not ReminderColl.ContainsKey(RowValueArr(0)) Then
                            ReminderColl.Add(RowValueArr(0), CType(RowValueArr(1), Boolean))
                        End If
                    End If
                Next

                For Each kvp As KeyValuePair(Of String, Boolean) In ReminderColl
                    'OLD one
                    If RdCurrentColl(kvp.Key) <> kvp.Value Then
                        If kvp.Value = True Then
                            SaveRemindersData(sUserLogged, 1, Int32.Parse(kvp.Key))
                        Else
                            SaveRemindersData(sUserLogged, 0, Int32.Parse(kvp.Key))
                        End If
                    End If

                    'If kvp.Value = True Then
                    '    SaveRemindersData(sUserLogged, 1, Int32.Parse(kvp.Key))
                    'Else
                    '    SaveRemindersData(sUserLogged, 0, Int32.Parse(kvp.Key))
                    'End If
                Next kvp

                'For Each kvp As KeyValuePair(Of String, String) In openWith
                '    Console.WriteLine("Key = {0}, Value = {1}", _
                '        kvp.Key, kvp.Value)
                'Next kvp


                ' Save Data
                ' AdminTasks.SaveAdminClientClass(User.Identity.Name, SelectedRows)

                ' Recreate the Grid again 
                ' with new values
                grdPreData.DataSource = AdminTasks.GetClientClass(iPractice, 0)
                grdPreData.DataBind()

                txtRecordCheck.Value = ""

                Erase RowsArr
                Erase RowValueArr
            Else
                lblMessageProcess.Text = "Please select any Row from the Grid View below."
            End If

        End Sub

        Private Sub SaveRemindersData(ByVal sUser As String, ByVal iReminder As Integer, ByVal iUnique As Int32)
            AdminTasks.SaveAdmClientClass2(sUser, iReminder, iUnique)
        End Sub




    End Class

End Namespace


