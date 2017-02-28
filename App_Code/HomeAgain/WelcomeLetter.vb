Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer
    Public Class WelcomeLetter

        Private _BatchID As Int32
        Private _StartDate As String = String.Empty

        ' Constructors
        Public Sub New()
        End Sub ' NEW

        Public Sub New(ByVal BatchID As Int32, ByVal StartDate As String)
            _BatchID = BatchID
            _StartDate = StartDate
        End Sub

        Public Property BatchID() As Int32
            Get
                Return _BatchID
            End Get
            Set(ByVal value As Int32)
                _BatchID = value
            End Set
        End Property

        Public Property StartDate() As String
            Get
                Return _StartDate
            End Get
            Set(ByVal value As String)
                _StartDate = value
            End Set
        End Property

        ' Clear Print Date
        Public Shared Function ClearPrintDate(ByVal sMicroChip As String, ByVal batch As Int32) As Boolean
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.WelcomeLetterClearPrintDate(sMicroChip, batch)
        End Function

        ' Set Print Date
        Public Shared Function SetPrintDate(ByVal batch As Int32, ByVal dt As DateTime, ByVal sPrintTotal As String) As Boolean
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.WelcomeLetterSetPrintDate(batch, dt, sPrintTotal)
        End Function

        Public Shared Function WelcomeLetterGetBatches() As WelcomeLetterCollections
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.WelcomeLetterGetBatches()
        End Function

        Public Shared Function WelcomeLetterAddFile(ByVal sFileName As String, ByVal sFileSize As String, ByVal iBatch As Int32) As Boolean
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.WelcomeLetterAddFile(sFileName, sFileSize, iBatch)
        End Function

        Public Shared Function SetHistory(ByVal sFile As String, ByVal iBatch As Int32) As Boolean
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.WelcomeLetterSetHistory(sFile, iBatch)
        End Function
        '        // Get the full file path
        'string strFilePath = “c:\\temp\\test.bat”;

        '// Create the ProcessInfo object
        'System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("cmd.exe");
        'psi.UseShellExecute = false; 
        'psi.RedirectStandardOutput = true;
        'psi.RedirectStandardInput = true;
        'psi.RedirectStandardError = true;
        'psi.WorkingDirectory = “c:\\temp\\“;

        '// Start the process
        'System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi);

        Public Shared Function WLetterRunCommandPrompt(ByVal sWorkFolder As String) As Boolean
            Dim ProInfo As System.Diagnostics.ProcessStartInfo
            Dim currProcess As System.Diagnostics.Process

            ProInfo = New System.Diagnostics.ProcessStartInfo(sWorkFolder & "\pscp.exe")

            ProInfo.UseShellExecute = False
            ProInfo.CreateNoWindow = True
            ProInfo.RedirectStandardOutput = True
            ProInfo.RedirectStandardInput = True
            ProInfo.RedirectStandardError = True
            ProInfo.WorkingDirectory = sWorkFolder ' & "\"
            'ProInfo.FileName = "pscp.exe"
            ProInfo.Arguments = " -batch -sftp -pw ""0yinvm>hO"""" ashackelford@sftp.datacoremarketing.com:./outgoing/* D:\Inetpub\intranet2vetmetnet\Secure\Administrator\ActivationMail\Incoming"
            ProInfo.WindowStyle = Diagnostics.ProcessWindowStyle.Minimized

            currProcess = System.Diagnostics.Process.Start(ProInfo)
            currProcess.WaitForExit()

            Dim strOutput As String = currProcess.StandardOutput.ReadToEnd()
            Dim strError As String = currProcess.StandardError.ReadToEnd()


            currProcess.Close()



        End Function

        ' Execute the Ftp DOWNLOAD and UPLOAD
        Public Shared Function WLetterRunFtp(ByVal sFileBasePath As String) As Boolean
            Dim win2 As System.Diagnostics.ProcessStartInfo = Nothing
            Dim myProcess As System.Diagnostics.Process
            Dim bExit As Boolean = False

            win2 = New System.Diagnostics.ProcessStartInfo(sFileBasePath)
            win2.CreateNoWindow = True

            win2.Arguments = Nothing
            win2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden


            Try
                myProcess = System.Diagnostics.Process.Start(win2)
                myProcess.WaitForExit()
                bExit = True
            Catch ex As Exception
                bExit = False
            End Try

            Return bExit
        End Function

        Public Shared Function WelcomeLetterReturnAlreadySend() As System.Data.DataView

            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.WelcomeLetterReturnAlreadySend()
        End Function

        ' get the satus
        Public Shared Function WelcomeLetterCurrentStatus(ByVal sFileName As String, ByVal iStatus As Integer, ByVal iBatch As Int32) As String()

            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.WelcomeLetter_CurrentStatus(sFileName, iStatus, iBatch)
        End Function

        ' step 1 process master data
        Public Shared Sub WelcomeLetterProcessMaster()

            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            DataLayer.WelcomeLetter_ProcessMaster()
        End Sub

        ' step 3 process master data
        Public Shared Sub WelcomeLetterAppendToHistory(ByVal sFileName As String)
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            DataLayer.WelcomeLetter_AppendToHistory(sFileName)
        End Sub
    End Class
End Namespace

