Imports myDtsPackageLibrary
Imports System.Data
Imports System.Net.Mail

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class SecureActivationMail2
        Inherits System.Web.UI.Page

        Private sExecutableFolder As String = Server.MapPath(".\")
        Private sOutgoingFolder As String = Server.MapPath(".\") & "Outgoing"
        Private sIncomingFolder As String = Server.MapPath(".\") & "Incoming"
        Private sArchiveFolder As String = Server.MapPath(".\") & "Archive"

        Private sWinServiceDownloadFile As String = "D:\\RunFileWatcher\\garry23.bin"
        Private sWinServiceUploadCsvFile As String = "D:\\RunFileWatcher\\garry33.bin"
        Private sWinServiceUploadTxtFile As String = "D:\\RunFileWatcher\\garry53.bin"

        Private sFilesCsvExtention As String = "*.csv"
        Private sFilesTxtExtention As String = "*.txt"
        Private ArrStatus() As String
        Private batchColl As WelcomeLetterCollections = Nothing
        Private objPackage As AccessPackage = Nothing

        Private Const DPAGE As String = "Default.aspx"


        Private _WorkingFolder As String

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
                Dim RootFileList() As System.IO.FileInfo = RootFolder.GetFiles(sFilesCsvExtention)
                Return RootFileList.Length
            End Get
        End Property

        Private ReadOnly Property iOutCsvFileCount() As Integer
            Get
                Dim RootFolder As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(sOutgoingFolder)
                Dim RootFileList() As System.IO.FileInfo = RootFolder.GetFiles(sFilesCsvExtention)
                Return RootFileList.Length
            End Get
        End Property

        Private ReadOnly Property iOutTxtFileCount() As Integer
            Get
                Dim RootFolder As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(sOutgoingFolder)
                Dim RootFileList() As System.IO.FileInfo = RootFolder.GetFiles(sFilesTxtExtention)
                Return RootFileList.Length
            End Get
        End Property

        Private Function GetGeneartedFiles(ByVal sFileExtention As String) As String
            Dim RootFolder As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(sOutgoingFolder)
            Dim RootFileList() As System.IO.FileInfo = RootFolder.GetFiles(sFileExtention)
            Dim sTemp As String = ""

            For Each flList As System.IO.FileInfo In RootFileList
                sTemp = sTemp & "<a href="""
                sTemp = sTemp & "Outgoing/" & flList.Name & """>" & flList.Name & "</a><br />"
            Next flList

            Return sTemp
        End Function

        Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender

            If System.IO.File.Exists(sWinServiceDownloadFile) Then
                Response.AddHeader("REFRESH", "45;URL=Default.aspx")
            End If

            If System.IO.File.Exists(sWinServiceUploadCsvFile) Then
                Response.AddHeader("REFRESH", "15;URL=Default.aspx")
            End If

            ' <META HTTP-EQUIV=REFRESH CONTENT="5;URL=http"> 
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim sAction As String = ""
            WorkingFolder = sIncomingFolder

            If Session("objBatches") Is Nothing Then
                Session("objBatches") = CreateDataSource()
            End If

            If Request.QueryString.Count > 0 Then
                sAction = Request.QueryString("act").ToString()
            End If

            ' for files 
            If sAction.CompareTo("files") = 0 Then
                Dim sTempStr As String = GetGeneartedFiles(sFilesTxtExtention)
                sTempStr = sTempStr & "<br />" & GetGeneartedFiles(sFilesCsvExtention)
                pnlGenerateFile.Visible = True
                lblFileGenerated.Text = sTempStr
            End If

            If sAction.CompareTo("process") <> 0 Then

                If System.IO.File.Exists(sWinServiceUploadCsvFile) Then
                    pnlDownload.Visible = False
                    pnlGenerateFile.Visible = False
                    pnlImport.Visible = False
                    btnUploadCvsFile.Enabled = False
                    btnUploadCvsFile.BackColor = Drawing.Color.Wheat
                End If

                If System.IO.File.Exists(sWinServiceDownloadFile) Then
                    btnDownLoad.BackColor = Drawing.Color.Wheat
                    btnDownLoad.Enabled = False

                    pnlImport.Visible = False
                    lblMessage.Text = "Files are downloading now ... Please Wait ... <br />Page is Refreshing on every 50 seconds ..."
                    pnlGenerateOutFiles.Visible = False
                Else
                    lblMessage.Text = ""
                    btnDownLoad.Enabled = True

                    If iFileCount > 0 Then
                        pnlImport.Visible = True
                        lblImportFile.Text = "Total files " & iFileCount.ToString()
                    Else
                        lblImportFile.Text = ""
                        pnlImport.Visible = False
                    End If
                End If
            End If ' sAction.CompareTo

            If Request.IsAuthenticated = True Then
                lblUserName.Text = User.Identity.Name

                ' ////////////////////////////////////////////////////
                ' // OLD Version             ///
                ' // Not used anymore Since  ///
                ' // May 20,2008             ///
                ' ////////////////////////////////////////////////////
                If sAction.CompareTo("process") = 0 Then
                    Dim nTotalReadyToProcess As Long = 0
                    Dim sBatch As String = ""

                    lblMessage.Text = "Please Wait ..."
                    ' Execute the first step
                    WelcomeLetter.WelcomeLetterProcessMaster()
                    ArrStatus = WelcomeLetter.WelcomeLetterCurrentStatus("", 1, 0)

                    If ArrStatus.Length > 3 Then
                        nTotalReadyToProcess = ArrStatus(3)
                        sBatch = ArrStatus(0)
                    End If


                    If nTotalReadyToProcess = 0 Then
                        ' continue
                        lblMessage.Text = "<b>There are no records to process. Please try again latter.</b>"
                    Else
                        Dim sOutputFile As String = ""

                        Dim task As New LengthyTask("")
                        sOutputFile = task.RunDtsPakage(sOutgoingFolder, sBatch)

                        WelcomeLetter.WelcomeLetterAppendToHistory(sOutputFile)
                        lblMessage.Text = "The Print file has been executed Successfuly."

                        pnlDownload.Visible = False
                        pnlImport.Visible = False
                        pnlGenerateFile.Visible = True
                        lblFileGenerated.Text = GetGeneartedFiles(sFilesTxtExtention)
                    End If
                End If

                If Not Page.IsPostBack Then

                    BuildDropList(drpBoxBatchID, 0)
                    BuildDropList(drpBadAddress, 0)
                    BuildDropList(drpGenCvsFiles, 0)
                End If ' Page.IsPostBack
            End If ' IsAuthenticated
        End Sub

        Private Sub BuildDropList(ByVal drp As DropDownList, ByVal i As Int32)
            With drp
                .DataSource = CType(Session("objBatches"), ICollection)
                .DataValueField = "BatchID"
                .DataTextField = "StartDate"
                .DataBind()
                If i > 0 Then
                    .SelectedIndex = 0
                End If
            End With
        End Sub


        Public Overrides Sub Dispose()
            If Not ArrStatus Is Nothing Then ArrStatus = Nothing
            If Not objPackage Is Nothing Then objPackage = Nothing

            MyBase.Dispose()
        End Sub

        Private Function SendEmail(ByVal sFileName As String) As String
            Dim result As String = ""

            Dim objFromAddress As MailAddress = New MailAddress("krassimir3@hotmail.com")
            Dim objToMailAddress As MailAddressCollection = New MailAddressCollection()
            Dim objTo As MailAddress = New MailAddress("knikov@vetmet.com", "Kriss N")

            Dim objMailMsg As MailMessage = New MailMessage()
            Dim objAttachment As Attachment = Nothing

            If System.IO.File.Exists(sFileName) Then
                objAttachment = New Attachment(sFileName)
            End If

            objToMailAddress.Add("knikov@vetmet.com")
            objToMailAddress.Add("krassimir3@hotmail.com")

            objMailMsg.From = objFromAddress
            objMailMsg.IsBodyHtml = False
            objMailMsg.Priority = MailPriority.High
            objMailMsg.To.Add(objTo)
            'objMailMsg.CC.Add("krassimir3@hotmail.com")

            objMailMsg.Subject = "Auto Generated file - Welcome Letter"
            If System.IO.File.Exists(sFileName) Then
                objMailMsg.Attachments.Add(objAttachment)
            End If

            objMailMsg.Body = "The file has been generated successfully."
            Dim client As SmtpClient = New SmtpClient()

            Try
                client.Host = "mscrm.vetmetrics.local"
                client.Port = 25
                client.Send(objMailMsg)
                result = "Success"
            Catch ex As Exception
                result = ex.Message
            End Try

            Return result
        End Function

        ' ****************************************************************
        ' * SAVE FILES to Database
        ' * Last Modified: May 23, 2008
        ' * change request from Randy
        ' * renaming the Act file when we send to DataCore
        ' ****************************************************************
        Private Sub SaveFileToDatabase(ByVal sExt As String)
            Dim RootFolder As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(sOutgoingFolder)
            Dim RootFileList() As System.IO.FileInfo = RootFolder.GetFiles(sExt)
            Dim sBatchId As String = ""
            Dim iBatch As Int32 = 0
            Dim IsFileImported As Boolean = False

            ' Example 
            ' 11282007_HA_Wel_24.txt()
            For Each flList As System.IO.FileInfo In RootFileList
                sBatchId = flList.Name.Substring(flList.Name.LastIndexOf("_") + 1, flList.Name.LastIndexOf(".") - flList.Name.LastIndexOf("_") - 1)
                If IsNumeric(sBatchId) Then
                    iBatch = Int32.Parse(sBatchId)
                End If

                If sExt = "*.csv" Then
                    Dim bHistory As Boolean = False
                    bHistory = WelcomeLetter.SetHistory(flList.Name, iBatch)
                End If

                ' add file to database
                'IsFileImported = WelcomeLetter.WelcomeLetterAddFile(flList.Name, flList.Length.ToString(), iBatch)

            Next flList

            If Not RootFolder Is Nothing Then RootFolder = Nothing
            If Not RootFileList Is Nothing Then RootFileList = Nothing

        End Sub '   SaveFileToDatabase()

        ' //////////////////////////////////////////////////////////////////
        ' // Verify if the files are procesed
        ' // May 20, 2008
        ' //////////////////////////////////////////////////////////////////
        Private Function ManageFiles() As ArrayList
            Dim RootFolder As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(sIncomingFolder)
            Dim RootFileList() As System.IO.FileInfo = RootFolder.GetFiles(sFilesCsvExtention)
            Dim _fileList As New ArrayList
            Dim IsFileImported As Boolean = False

            For Each flList As System.IO.FileInfo In RootFileList

                ' check if the file is imported already
                IsFileImported = WelcomeLetter.WelcomeLetterAddFile(flList.Name, flList.Length.ToString(), 0)

                If IsFileImported = True Then
                    _fileList.Add(flList)
                    System.Threading.Thread.Sleep(1000)
                Else
                    System.Threading.Thread.Sleep(500)
                    flList.Delete()
                End If
            Next flList

            Return (_fileList)
        End Function
        ' end verifyed files


        Private Function RunLocalDtsPackage() As ArrayList
            Dim PackageLogInfo As String = GlobalsVar.DTSPackageLoginInfo
            Dim PackageNameImportFile As String = GlobalsVar.DtsNameImportTextFile

            Dim Arr() As String = Nothing
            Dim ArrParameter(1) As String
            Dim myChar As Char = ";"
            Arr = PackageLogInfo.Split(myChar)

            objPackage = New AccessPackage(Arr(0), Arr(1), Arr(2), PackageNameImportFile, Nothing, Nothing, Nothing)

            Dim RootFolder As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(sIncomingFolder)
            Dim RootFileList() As System.IO.FileInfo = RootFolder.GetFiles(sFilesCsvExtention)
            Dim _fileList As New ArrayList
            Dim IsFileImported As Boolean = False

            ' ***************************************************
            ' * For Each thisControl As System.Windows.Forms.Control In thisForm.Controls
            For Each flList As System.IO.FileInfo In RootFileList

                ArrParameter(0) = sIncomingFolder
                ArrParameter(1) = flList.Name

                IsFileImported = WelcomeLetter.WelcomeLetterAddFile(flList.Name, flList.Length.ToString(), 0)

                If IsFileImported = True Then

                    ArrStatus = WelcomeLetter.WelcomeLetterCurrentStatus(flList.Name, 2, 0)
                    _fileList.Add(flList)


                    ' Execute the Package with Parameters ...
                    System.Threading.Thread.Sleep(1000)

                    objPackage.LoadFromSQLServer(ArrParameter)
                    System.Threading.Thread.Sleep(1000)
                Else
                    flList.Delete()
                End If

            Next flList

            ' If Not objPackage Is Nothing Then objPackage = Nothing
            If Not ArrParameter Is Nothing Then ArrParameter = Nothing
            If Not ArrStatus Is Nothing Then ArrStatus = Nothing

            Return _fileList
        End Function

        Private Function CreateEmptyFile(ByVal sPathCombineFile As String) As String
            Dim mybEmptyFile As Byte() = New Byte() {80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            Dim fs As System.IO.FileStream = Nothing
            Dim sTemp As String = ""

            Try
                fs = System.IO.File.Create(sPathCombineFile)
                fs.Write(mybEmptyFile, 0, mybEmptyFile.Length)
            Catch ex As Exception
                sTemp = "Error: " & ex.Message
            Finally
                If Not fs Is Nothing Then
                    If Not mybEmptyFile Is Nothing Then mybEmptyFile = Nothing
                    fs.Flush()
                    fs.Close()
                    fs = Nothing
                End If
            End Try

            Return sTemp
        End Function


        Protected Sub btnDownLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownLoad.Click
            Dim sTemp As String = String.Empty

            sTemp = CreateEmptyFile(sWinServiceDownloadFile)
            System.Threading.Thread.Sleep(3000)

            If sTemp.Length < 2 Then
                btnDownLoad.Enabled = False
                lblMessage.Text = "Files are downloading now ... Please Wait ... <br />Page is Refreshing on every 50 seconds ..."
                pnlImport.Visible = False
                Response.Redirect(DPAGE)
            End If

        End Sub

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

        ' ///////////////////////////////////////////////////////////////////////////
        '  Execute the DTS Package
        '  May 20, 2008
        ' /////////////////////////////////////////////////////////////
        Private Function RunDtsPackage_ImportTEXT_File(ByVal sFileName As String) As Boolean
            ' Dim bool As Boolean = False
            Dim PkgLogInfo As String = GlobalsVar.DTSPackageLoginInfo
            Dim PkgNameImportFile As String = GlobalsVar.DtsNameImportTextFile

            Dim ObjInputParARR() As String = Nothing
            Dim myChar As Char = ";"

            ' using for DTS Package GLOBAL Variables
            Dim PkgParametersARR(1) As String

            ' the incoming folder
            PkgParametersARR(0) = sIncomingFolder
            ' import file name
            PkgParametersARR(1) = sFileName

            ObjInputParARR = PkgLogInfo.Split(myChar)

            ' create an instance to DTS Package Module
            objPackage = New AccessPackage(ObjInputParARR(0), _
                            ObjInputParARR(1), _
                            ObjInputParARR(2), _
                            PkgNameImportFile, _
                            Nothing, Nothing, Nothing)

            ' execute the DTS Package
            objPackage.LoadFromSQLServer(PkgParametersARR)

            If Not PkgParametersARR Is Nothing Then PkgParametersARR = Nothing

            Return (True)
        End Function

        Protected Sub btnImportFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportFile.Click
            Dim _fileList As ArrayList = Nothing
            Dim isZipped As Boolean = False
            Dim isImported As Boolean = False

            ' set session to nothing
            Session("objBatches") = Nothing

            ' if there is files then execute Dts Package to import them
            If iFileCount > 0 Then

                ' new version May 20, 2008
                _fileList = ManageFiles()

                ' Zip files
                If _fileList.Count > 0 Then
                    Dim dt As DateTime = DateTime.Now
                    Dim objZip As VeterinaryMetrics.vmZipUnzipFile
                    Dim sDate As String = StrFormatDate("SPHA_WelcomeKit_VetMet_Incoming_")

                    objZip = New VeterinaryMetrics.vmZipUnzipFile(sIncomingFolder, sArchiveFolder & "\" + sDate + ".zip")

                    ' Wait few seconds
                    System.Threading.Thread.Sleep(2000)

                    ' Verify if the Zip exists
                    isZipped = objZip.AddGroupZipFiles("*.csv")

                    ' Wait another second
                    System.Threading.Thread.Sleep(500)
                    If Not objZip Is Nothing Then objZip = Nothing
                End If
                'end Zipping the files

                ' old
                ' _fileList = RunLocalDtsPackage()
                Dim iProcessReady As Long = 0

                ' NEW LOOP through the file LIST Array
                For Each flList As System.IO.FileInfo In _fileList

                    ' Update batch Id into File's table
                    ArrStatus = WelcomeLetter.WelcomeLetterCurrentStatus(flList.Name, 2, 0)
                    System.Threading.Thread.Sleep(1000)

                    ' Import the first Text File
                    isImported = RunDtsPackage_ImportTEXT_File(flList.Name)

                    '  exit for
                    ' Wait a second
                    System.Threading.Thread.Sleep(3000)

                    Dim nTotalReadyToProcess As Long = 0
                    Dim sBatch As String = ""

                    lblMessage.Text = "Please Wait ..."

                    ' Process the Master Records
                    WelcomeLetter.WelcomeLetterProcessMaster()

                    ' Get the Batch ID and Def Variables
                    ArrStatus = WelcomeLetter.WelcomeLetterCurrentStatus("", 1, 0)
                    sBatch = String.Empty

                    If ArrStatus.Length > 3 Then
                        iProcessReady = ArrStatus(3)
                        sBatch = ArrStatus(0)
                    End If

                    If iProcessReady = 0 And sBatch.Length = 0 Then
                        ' continue
                        lblMessage.Text = "<b>There are no records to process, or Batch Id is missing. Please try again latter.</b>"
                    Else
                        Dim sOutputFile As String = ""
                        Dim task As New LengthyTask("")

                        ' Output file Name
                        sOutputFile = task.RunDtsPakage(sOutgoingFolder, sBatch)

                        ' Append to history
                        WelcomeLetter.WelcomeLetterAppendToHistory(sOutputFile)
                        lblMessage.Text = "The Print file has been executed Successfuly."

                        pnlDownload.Visible = False
                        pnlImport.Visible = False
                        pnlGenerateFile.Visible = True
                        lblFileGenerated.Text = GetGeneartedFiles(sFilesTxtExtention)
                    End If


                Next flList

                'Dim dt As DateTime = DateTime.Now
                'Dim objZip As VeterinaryMetrics.vmZipUnzipFile
                'Dim sDate As String = StrFormatDate("SPHA_WelcomeKit_VetMet_Incoming_")

                'objZip = New VeterinaryMetrics.vmZipUnzipFile(sIncomingFolder, sArchiveFolder & "\" + sDate + ".zip")

                'System.Threading.Thread.Sleep(2000)
                'isZipped = objZip.AddGroupZipFiles("*.csv")

                ' System.Threading.Thread.Sleep(1000)

                ' Zip files
                ' DELETE files in Incoming FOLDER
                If isZipped Then
                    For Each flList As System.IO.FileInfo In _fileList
                        flList.Delete()
                    Next
                End If

                System.Threading.Thread.Sleep(3000)

                ' If Not objZip Is Nothing Then objZip = Nothing
                If Not _fileList Is Nothing Then _fileList = Nothing

                ' Response.Redirect("Default.aspx?act=process")

            End If ' END iFileCount
        End Sub

        Function CreateDataSource() As ICollection
            Dim dt As New DataTable()
            Dim dr As DataRow
            Dim i As Integer = 0

            dt.Columns.Add(New DataColumn("BatchID", GetType(Int32)))
            dt.Columns.Add(New DataColumn("StartDate", GetType(String)))

            batchColl = WelcomeLetter.WelcomeLetterGetBatches()


            For i = 0 To batchColl.Count - 1
                dr = dt.NewRow()

                dr(0) = batchColl.Item(i).BatchID
                dr(1) = batchColl.Item(i).BatchID.ToString() & " >> " & batchColl.Item(i).StartDate
                dt.Rows.Add(dr)
            Next i

            If Not batchColl Is Nothing Then batchColl = Nothing

            Dim dv As New DataView(dt)
            Return dv

        End Function

        Protected Sub btnPrintDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintDate.Click
            Dim result As Boolean = False

            Dim selDate As DateTime = DateTime.Parse(txtPrintDate.Text)
            Dim selBatch As Int32 = Int32.Parse(drpBoxBatchID.SelectedValue)
            Dim selPrntTotal As String = txtTotalPrint.Text

            result = WelcomeLetter.SetPrintDate(selBatch, selDate, selPrntTotal)
            If result = True Then
                lblMessage.Text = "Print date has been set to '" & selDate.ToShortDateString() & "' for Batch id '" & selBatch.ToString() & "'."

                ' Load the dropdountList
                ' BuildDropList(drpBoxBatchID, 0)
           
            Else
                lblMessage.Text = "Error"
            End If
        End Sub

        Protected Sub btnMicroChip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMicroChip.Click
            Dim result As Boolean = False

            Dim selMicrochipId As String = txtMicroChipId.Text
            Dim selBatch As Int32 = Int32.Parse(drpBoxBatchID.SelectedValue)

            result = WelcomeLetter.ClearPrintDate(selMicrochipId, selBatch)

            If result = True Then
                lblMessage.Text = "The date was clear for MicroChip ID: '" & selMicrochipId & "'; Batch id '" & selBatch.ToString() & "'."

                ' Load the dropdountList
                With drpBadAddress
                    .DataSource = CreateDataSource()
                    .DataValueField = "BatchID"
                    .DataTextField = "StartDate"
                    .DataBind()
                    .SelectedIndex = 0
                End With
            Else
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Text = "Error: Update failed. Wrong Microchip ID: '" & selMicrochipId & "' ..."
            End If
        End Sub

        ' ************************************************************************
        ' * Generating the CSV file using DTS Package
        ' * Date: May 23, 2008
        ' ************************************************************************
        Protected Sub btnGenerateCsvFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateCsvFile.Click

            Dim selBatch As String = drpGenCvsFiles.SelectedValue
            Dim sFile As String = ""

            If selBatch.Length > 0 Then

                If CType(Session("strBatch"), String) = selBatch Then
                    lblMessage.Text = "The Csv file is already created for Batch Id: '" & selBatch & "'. Try another one ..."
                    Exit Sub
                End If

                ' Actual File Name
                sFile = GenerateCvsFile(sOutgoingFolder, selBatch)
                If sFile.Length > 10 Then
                    Session("strBatch") = selBatch
                    lblDisplayCvsFiles.Text = sFile
                    lblMessage.Text = "The Csv file has been created for Batch Id: '" & selBatch & "' ..."

                    ' Send an E-mail with the file
                    SendEmail(sOutgoingFolder & "\" & sFile)
                    Response.Redirect(DPAGE)
                Else
                    lblMessage.Text = "File name CANNOT be found."
                End If

            End If
        End Sub

        ' ***************************************************************************
        ' * Generating the CSV File
        ' * May 23, 2008
        ' ***************************************************************************
        Private Function GenerateCvsFile( _
            ByVal sTargetFolder As String, _
            ByVal sBatchID As String) As String

            Dim PkgLogInfo As String = GlobalsVar.DTSPackageLoginInfo
            Dim PkgExportName As String = GlobalsVar.DtsNameGenerateCsvFile
            Dim sOutputFile As String = "", sTEMP As String = ""

            Dim iBatch32 As Int32 = 0

            Dim Arr() As String = Nothing
            Dim ArrParameter(2) As String
            Dim myChar As Char = ";"
            Arr = PkgLogInfo.Split(myChar)

            objPackage = New AccessPackage(Arr(0), Arr(1), Arr(2), PkgExportName, Nothing, Nothing, Nothing)

            ' PREVIOUS FUNCTION
            'Dim dt As DateTime = DateTime.Now
            'Dim sDay As String = dt.Day.ToString()
            'Dim sYear As String = dt.Year.ToString()
            'Dim sMonth As String = dt.Month.ToString()
            'Dim sHour As String = dt.Hour.ToString()
            'Dim sMinute As String = dt.Minute.ToString()
            'SPHA_WelcomeKit_VetMet_Ack_YYYY_MM_DD_HH_MM.csv

            'If sDay.Length = 1 Then sDay = "0" & sDay
            'If sMonth.Length = 1 Then sMonth = "0" & sMonth
            'If sHour.Length = 1 Then sHour = "0" & sHour
            'If sMinute.Length = 1 Then sMinute = "0" & sMinute
            ArrStatus = Nothing
            If sBatchID.Length > 0 Then
                Try
                    iBatch32 = Int32.Parse(sBatchID)

                    ' Get the file Name that was already downloaded
                    ArrStatus = WelcomeLetter.WelcomeLetterCurrentStatus("", 3, iBatch32)
                    sTEMP = ArrStatus(0)
                Catch ex As Exception

                End Try
            End If

            ' sOutputFile = "SPHA_WelcomeKit_VetMet_Ack_" & sYear & "_" & sMonth & "_" & sDay & "_" & sHour & "_" & sMinute & "_" & sBatchID & ".csv"

            ' **************************
            ' Output file generation
            ' SPHA_WelcomeKit_VetMet_2008_05_14_18_04_Ack_58.csv
            If sTEMP.Length > 10 Then
                sOutputFile = sTEMP.Replace(".csv", "")
                sOutputFile = sOutputFile & "_Ack_" & sBatchID & ".csv"
            Else
                lblDisplayCvsFiles.Text = "File name CANNOT be found. Check SP: spActivationMail_HA_Status."
                Return (String.Empty)
            End If

            ArrParameter(0) = sTargetFolder
            ArrParameter(1) = sOutputFile
            ArrParameter(2) = sBatchID

            objPackage.LoadFromSQLServer(ArrParameter)
            System.Threading.Thread.Sleep(5000)

            ' If Not objPackage Is Nothing Then objPackage = Nothing
            If Not ArrParameter Is Nothing Then ArrParameter = Nothing
            If Not Arr Is Nothing Then Arr = Nothing

            Return sOutputFile
        End Function

        Private Sub ZipTargetFiles(ByVal sExtFile As String)
            Dim dt As DateTime = DateTime.Now
            Dim objZip As VeterinaryMetrics.vmZipUnzipFile
            Dim sZipFile As String = StrFormatDate("SPHA_Outgoing_")
            Dim isZipped As Boolean = False

            objZip = New VeterinaryMetrics.vmZipUnzipFile(sOutgoingFolder, sArchiveFolder & "\" + sZipFile + ".zip")
            System.Threading.Thread.Sleep(300)
            isZipped = objZip.AddGroupZipFiles(sExtFile)
            'System.Threading.Thread.Sleep(1000)
        End Sub

        Protected Sub btnUploadCvsFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadCvsFile.Click
            Dim sTemp As String = String.Empty

            If iOutCsvFileCount = 0 Then
                lblMessage.Text = "There are no Csv Files to upload . Please try later."
                Exit Sub
            Else
                ' Place them into database
                SaveFileToDatabase("*.csv")

                ' Zip only csv files
                ZipTargetFiles("*.csv")
            End If

            sTemp = CreateEmptyFile(sWinServiceUploadCsvFile)
            System.Threading.Thread.Sleep(3000)

            If sTemp.Length < 2 Then
                pnlDownload.Visible = False
                pnlGenerateFile.Visible = False
                lblMessage.Text = "Csv Files are Uploading now. Please Wait ..."
                pnlImport.Visible = False
            End If

        End Sub

        Protected Sub btnUploadTextFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadTextFile.Click
            Dim sTemp As String = String.Empty

            If iOutTxtFileCount = 0 Then
                lblMessage.Text = "There are no Text Files to upload . Please try later."
                Exit Sub
            Else
                ' Place them into database
                SaveFileToDatabase("*.txt")

                ' Zip Files
                ZipTargetFiles("*.txt")
            End If

            sTemp = CreateEmptyFile(sWinServiceUploadTxtFile)
            System.Threading.Thread.Sleep(3000)

            If sTemp.Length < 2 Then
                pnlDownload.Visible = False
                pnlGenerateFile.Visible = False
                pnlUploadCsvFile.Visible = False
                lblMessage.Text = "Text Files are Uploading now. Please Wait ..."
                pnlImport.Visible = False
                Response.Redirect(DPAGE)
            End If
        End Sub
    End Class
End Namespace
