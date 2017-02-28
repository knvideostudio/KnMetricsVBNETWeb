Imports VeterinaryMetrics.BusinessLayer
Imports System.IO
Imports myDtsPackageLibrary
Imports System.Data
' ziping files
'Imports VeterinaryMetrics

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class ExpWelcomeMail
        Inherits System.Web.UI.Page ': Implements ICallbackEventHandler

        Private ArrStatus() As String
        Private sExecutableFolder As String = Server.MapPath(".\") & "ActivationMail"
        Private sOutgoingFolder As String = Server.MapPath(".\") & "ActivationMail\Outgoing"
        Private sIncomingFolder As String = Server.MapPath(".\") & "ActivationMail\Incoming"
        Private sArchiveFolder As String = Server.MapPath(".\") & "ActivationMail\Archive"
        Private strFileName As String = String.Empty

        Private collWL As WelcomeLetterCollections = Nothing

        Private _taskID As String = String.Empty
        Private _WorkingFolder As String

        ' set Working Folder
        Private Property WorkingFolder() As String
            Get
                Return _WorkingFolder
            End Get
            Set(ByVal value As String)
                _WorkingFolder = value
            End Set
        End Property

        ' returns the total files
        Private ReadOnly Property iFileCount() As Integer
            Get
                Dim RootFolder As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(WorkingFolder)
                Dim RootFileList() As System.IO.FileInfo = RootFolder.GetFiles("*.csv")
                Return RootFileList.Length
            End Get
        End Property

        Public Overrides Sub Dispose()
            If Not collWL Is Nothing Then collWL = Nothing
            If Not ArrStatus Is Nothing Then ArrStatus = Nothing

            MyBase.Dispose()
        End Sub 'Dispose

        Function CreateDataSource() As ICollection
            Dim dt As New DataTable()
            Dim dr As DataRow
            Dim i As Integer = 0

            dt.Columns.Add(New DataColumn("BatchID", GetType(Int32)))
            dt.Columns.Add(New DataColumn("StartDate", GetType(String)))

            collWL = WelcomeLetter.WelcomeLetterGetBatches()


            For i = 0 To collWL.Count - 1
                dr = dt.NewRow()

                dr(0) = collWL.Item(i).BatchID
                dr(1) = collWL.Item(i).BatchID.ToString() & " >> " & collWL.Item(i).StartDate
                dt.Rows.Add(dr)
            Next i

            Dim dv As New DataView(dt)
            Return dv

        End Function


        ' Page Load any time when you request the page
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            Dim CurrentDate As DateTime = DateTime.Now

            ' web site is Authorizate
            If Request.IsAuthenticated = True Then
                WorkingFolder = sIncomingFolder
                lblCurrentDate.Text = "Today is: <b>" & CurrentDate.ToLongDateString() & "</b>"
                lblUserName.Text = User.Identity.Name

                If Not Page.IsPostBack Then

                    ' Load the dropdountList
                    With drpBoxBatchID
                        .DataSource = CreateDataSource()
                        .DataValueField = "BatchID"
                        .DataTextField = "StartDate"
                        .DataBind()
                        .SelectedIndex = 0
                    End With

                    System.Threading.Thread.Sleep(1000)
                    If iFileCount > 0 Then

                        lblMessage.Text = "<b>Step-1</b> Importing Excel file(s). Total files to import " & iFileCount.ToString() & "."

                    Else
                        btnImportFile.Enabled = False
                        btnImportFile.Visible = False

                        lblMessage.Text = "<b>Step-2</b>  Process Master Records."
                        btnProcessMaster.Enabled = True
                        btnProcessMaster.Visible = True
                    End If
                End If
            End If
        End Sub

        ' Process Master table
        ' 
        Protected Sub btnProcessMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcessMaster.Click

            Dim nTotalReadyToProcess As Long
            Dim strTmp As String = String.Empty

            ' Execute the first step
            WelcomeLetter.WelcomeLetterProcessMaster()
            btnProcessMaster.Enabled = False
            btnProcessMaster.Visible = False

            ' current status
            ArrStatus = WelcomeLetter.WelcomeLetterCurrentStatus("", 1, 0)

            nTotalReadyToProcess = ArrStatus(5)
            strTmp = "<b>Step-3</b>  Building the mailing list Output text file and append to the History table." & _
            "<br />Date: " & ArrStatus(8) & "<br />" & _
            "Total by unique Microchip ID: " & ArrStatus(3) & "<br />" & _
            "Total duplicates: " & ArrStatus(4) & "<br />" & _
            "Total ready to send: " & ArrStatus(5)

            If nTotalReadyToProcess = 0 Then
                btnBuildOutput.Enabled = False
                btnBuildOutput.Visible = False
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Text = strTmp & "<br /><b>There are no records to process. Please try again latter.</b>"
            Else
                btnBuildOutput.Enabled = True
                btnBuildOutput.Visible = True
                lblMessage.ForeColor = Drawing.Color.Black
                lblMessage.Text = strTmp
            End If

        End Sub

        Protected Sub btnBuildOutput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuildOutput.Click
            Session.Timeout = 900

            Dim sOutputFile As String = ""

            Dim task As New LengthyTask("")
            sOutputFile = task.RunDtsPakage(sOutgoingFolder, "434")

            WelcomeLetter.WelcomeLetterAppendToHistory(sOutputFile)
            lblMessage.Text = "The Welcome letter has been executed successfuly."
        End Sub

        'Protected Sub btnCleanUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCleanUp.Click

        '    btnBuildOutput.Enabled = False
        '    btnBuildOutput.Visible = False

        '    btnCleanUp.Enabled = False
        '    btnCleanUp.Visible = False

        '    WelcomeLetter.WelcomeLetterAppendToHistory()

        '    lblStep3.Text = ""
        '    lblMessage.Text = "The Welcome letter has been executed successfuly."
        'End Sub

        Private Function RunLocalDtsPackage() As ArrayList
            Dim PackageLogInfo As String = GlobalsVar.DTSPackageLoginInfo
            Dim PackageNameImportFile As String = GlobalsVar.DtsNameImportTextFile

            Dim Arr() As String = Nothing
            Dim ArrParameter(1) As String
            Dim myChar As Char = ";"
            Arr = PackageLogInfo.Split(myChar)

            Dim objPackage As New AccessPackage(Arr(0), Arr(1), Arr(2), PackageNameImportFile, Nothing, Nothing, Nothing)

            Dim RootFolder As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(sIncomingFolder)
            Dim RootFileList() As System.IO.FileInfo = RootFolder.GetFiles("*.csv")
            Dim _fileList As New ArrayList
            Dim IsFileImported As Boolean = False

            ' ***************************************************
            ' * For Each thisControl As System.Windows.Forms.Control In thisForm.Controls
            For Each flList As System.IO.FileInfo In RootFileList

                ArrParameter(0) = sIncomingFolder
                ArrParameter(1) = flList.Name

                ArrStatus = WelcomeLetter.WelcomeLetterCurrentStatus(flList.Name, 2, 0)

                IsFileImported = False 'WelcomeLetter.WelcomeLetterAddFile(flList.Name, flList.Length.ToString())

                If IsFileImported = True Then
                    _fileList.Add(flList)

                    ' Execute the Package with Parameters ...
                    objPackage.LoadFromSQLServer(ArrParameter)
                    System.Threading.Thread.Sleep(1000)
                Else
                    flList.Delete()
                End If
            Next flList

            Return _fileList
        End Function

        ' Formating the Date as string
        Private Function StrFormatDate(ByVal sFileName As String) As String

            Dim dt As DateTime = DateTime.Now
            Dim sTemp As String = String.Empty
            Const U As String = "_"

            Dim sDay As String = dt.Day.ToString()
            Dim sYear As String = dt.Year.ToString()
            Dim sMonth As String = dt.Month.ToString()
            Dim sHour As String = dt.Hour.ToString()
            Dim sMinute As String = dt.Minute.ToString()
            Dim sSecond As String = dt.Second.ToString()
            'SPHA_WelcomeKit_VetMet_Ack_YYYY_MM_DD_HH_MM.csv

            If sDay.Length = 1 Then sDay = "0" & sDay
            If sMonth.Length = 1 Then sMonth = "0" & sMonth
            If sHour.Length = 1 Then sHour = "0" & sHour
            If sMinute.Length = 1 Then sMinute = "0" & sMinute
            If sSecond.Length = 1 Then sSecond = "0" & sSecond

            sTemp = sFileName & U & sYear & U & sMonth & U & sDay & U & sHour & sMinute & sSecond

            Return sTemp
        End Function

        ' Import Package
        Protected Sub btnImportFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportFile.Click
            Dim _fileList As ArrayList = Nothing

            ' if there is files then execute Dts Package to import them
            If iFileCount > 0 Then

                _fileList = RunLocalDtsPackage()

                Dim dt As DateTime = DateTime.Now
                Dim objZip As VeterinaryMetrics.vmZipUnzipFile
                Dim sDate As String = StrFormatDate("SPHA_WelcomeKit_VetMet")

                objZip = New VeterinaryMetrics.vmZipUnzipFile(sIncomingFolder, sArchiveFolder & "\" + sDate + ".zip")
                objZip.RunProgram()
                System.Threading.Thread.Sleep(1000)

                ' Zip files
                ' delete files in Incoming FOLDER
                For Each flList As System.IO.FileInfo In _fileList
                    flList.Delete()
                Next

            End If

            btnImportFile.Enabled = False
            btnImportFile.Visible = False

            lblMessage.Text = "<b>Step-2</b>  Process Master Records."
            btnProcessMaster.Enabled = True
            btnProcessMaster.Visible = True
        End Sub

        Protected Sub btnPrintDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintDate.Click
            Dim result As Boolean = False

            Dim selDate As DateTime = DateTime.Parse(txtPrintDate.Text)
            Dim selBatch As Int32 = Int32.Parse(drpBoxBatchID.SelectedValue)
            Dim selPrintTotal As String = "0"

            result = WelcomeLetter.SetPrintDate(selBatch, selDate, selPrintTotal)
            If result = True Then
                lblMessageDisplay.Text = "Print date has been set to '" & selDate.ToShortDateString() & "' for Batch id '" & selBatch.ToString() & "'."

                ' Load the dropdountList
                With drpBoxBatchID
                    .DataSource = CreateDataSource()
                    .DataValueField = "BatchID"
                    .DataTextField = "StartDate"
                    .DataBind()
                    .SelectedIndex = 0
                End With
            Else
                lblMessageDisplay.Text = "Error"
            End If

        End Sub

        Protected Sub btnImportFile_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportFile.PreRender

        End Sub
    End Class

End Namespace