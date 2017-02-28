Imports Microsoft.VisualBasic
Imports System.Threading
Imports System.IO
Imports myDtsPackageLibrary

Namespace VeterinaryMetrics.BusinessLayer


    Public Class LengthyTask : Inherits Task
        Private _seconds As Integer

        Public Sub New(ByVal taskID As String)
            MyBase.New(taskID)
            _seconds = 5
        End Sub

        Public Property Seconds() As Integer
            Get
                Return _seconds
            End Get
            Set(ByVal value As Integer)
                _seconds = value
            End Set
        End Property

        Public Overrides Sub Run()
            For i As Integer = 1 To _seconds
                TaskHelpers.UpdateStatus(_taskID, _
                    String.Format("Step {0} out of {1}", i, _seconds))
                Thread.Sleep(1000)
            Next

            Thread.Sleep(1000)
            _lblStep3 = String.Format("Task completed at {0}", DateTime.Now)
            TaskHelpers.ClearStatus(_taskID)
        End Sub

        Public Overrides Sub Run(ByVal data As Object)
            For i As Integer = 1 To _seconds
                TaskHelpers.UpdateStatus(_taskID, _
                    String.Format("Step {0} out of {1}", i, _seconds))
                Thread.Sleep(1000)
            Next

            Thread.Sleep(1000)
            _lblStep3 = String.Format("Task completed at {0} : Data='{1}'", DateTime.Now, data)
            TaskHelpers.ClearStatus(_taskID)
        End Sub

        Public Overrides Function RunDtsPakage(ByVal TextFileFolder As String, _
            ByVal sBatch As String) As String


            Dim PackageLogInfo As String = GlobalsVar.DTSPackageLoginInfo
            Dim PackageNameGenTextFile As String = GlobalsVar.DtsNameGeneratePrintFile

            Dim ARRPackageLogInfo() As String = Nothing
            Dim myChar As Char = ";"
            ARRPackageLogInfo = PackageLogInfo.Split(myChar)

            Dim ServerName As String = String.Empty
            Dim DtsUserLoginName As String = String.Empty
            Dim DtsUserPassword As String = String.Empty

            If ARRPackageLogInfo.Length > 0 Then
                ServerName = ARRPackageLogInfo(0)
                DtsUserLoginName = ARRPackageLogInfo(1)
                DtsUserPassword = ARRPackageLogInfo(2)

            End If

            Dim objPackage As New AccessPackage(ServerName, _
            DtsUserLoginName, _
            DtsUserPassword, _
            PackageNameGenTextFile, _
            Nothing, _
            Nothing, _
            Nothing)

            Dim dt As DateTime = DateTime.Now
            Dim sDay As String = dt.Day.ToString()
            Dim sYear As String = dt.Year.ToString()
            Dim sMonth As String = dt.Month.ToString()
            Dim sHour As String = dt.Hour.ToString()
            Dim sMinute As String = dt.Minute.ToString()
            'SPHA_WelcomeKit_VetMet_Ack_YYYY_MM_DD_HH_MM.csv

            If sDay.Length = 1 Then sDay = "0" & sDay
            If sMonth.Length = 1 Then sMonth = "0" & sMonth
            If sHour.Length = 1 Then sHour = "0" & sHour
            If sMinute.Length = 1 Then sMinute = "0" & sMinute

            ' 112607_HA_Wel_24.txt

            Dim sPrintFileName As String = sMonth & sDay & sYear & "_HA_Wel_" & sBatch & ".txt"
            Dim ArrDtsParameters(1) As String

            ArrDtsParameters(0) = TextFileFolder
            ArrDtsParameters(1) = sPrintFileName

            objPackage.LoadFromSQLServer(ArrDtsParameters)

            objPackage = Nothing

            'Dim DownloadFolder As DirectoryInfo = New DirectoryInfo(myTextFileFolder)

            'Dim sFileName As String = TextFileFolder & "\" & sFileNameText
            'Dim strTemp As String = String.Empty

            ' Check if file exists
            ' Web_352007__ReadyToSend.txt
            'If File.Exists(sFileName) Then
            '    strTemp = "<a href=""ActivationMail/Outgoing/" & sOnlyFileName & """>" & sOnlyFileName & "</a>"
            '    strTemp = "<a href=""ActivationMail/Outgoing/" & sFileNameText & """>" & sFileNameText & "</a>"
            'Else
            '    strTemp = "Generation of file fail."

            'End If

            'Dim xslFileList() As FileInfo = DownloadFolder.GetFiles("Web_*.txt")

            _lblStep3 = String.Format("Task completed at {0}", DateTime.Now)
            TaskHelpers.ClearStatus(_taskID)

            ' disable buton
            ' btnBuildFile.Enabled = False
            ' btnBuildFile.Visible = False
            ' lblFileText.Text = "<br /><br />File has been created: <br />" & strTemp
            Return sPrintFileName
        End Function

    End Class
End Namespace