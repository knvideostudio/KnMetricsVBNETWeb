Imports Microsoft.VisualBasic
Imports System.Web
Imports System.Configuration

Namespace VeterinaryMetrics.BusinessLayer

    Public Class GlobalsVar

        Public Shared ReadOnly Property DtsPackageLogInfoVmrpt01() As String
            Get
                Dim str As String = ConfigurationManager.AppSettings("DtsPackageLogInfo_Vmrpt01")
                If str Is Nothing OrElse str = String.Empty Then
                    Throw New ApplicationException("DtsPackageLogInfo_Vmrpt01 configuration is missing from you web.config. " & _
                    "It should contain  <appSettings><add key=""DtsPackageLogInfo_Vmrpt01"" value=""Normal"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        Public Shared ReadOnly Property DefaultFilePath() As String
            Get
                Dim str As String = ConfigurationManager.AppSettings("DefaultFilePath")
                If str Is Nothing OrElse str = String.Empty Then
                    Throw New ApplicationException("DefaultFilePath configuration is missing from you web.config. " & _
                    "It should contain  <appSettings><add key=""DefaultFilePath"" value=""Normal"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        Public Shared ReadOnly Property DtsNameImportTextFile() As String
            Get
                Dim str As String = ConfigurationManager.AppSettings("DtsNameImportTextFile")
                If str Is Nothing OrElse str = String.Empty Then
                    Throw New ApplicationException("DtsNameImportTextFile configuration is missing from you web.config. " & _
                    "It should contain  <appSettings><add key=""DtsNameImportTextFile"" value=""Normal"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        Public Shared ReadOnly Property DtsNameGenerateCsvFile() As String
            Get
                Dim str As String = ConfigurationManager.AppSettings("DtsNameGenerateCsvFile")
                If str Is Nothing OrElse str = String.Empty Then
                    Throw New ApplicationException("DtsNameGenerateCsvFile configuration is missing from you web.config. " & _
                    "It should contain  <appSettings><add key=""DtsNameGenerateCsvFile"" value=""Normal"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        Public Shared ReadOnly Property DtsNameGeneratePrintFile() As String
            Get
                Dim str As String = ConfigurationManager.AppSettings("DtsNameGeneratePrintFile")
                If str Is Nothing OrElse str = String.Empty Then
                    Throw New ApplicationException("DtsNameGeneratePrintFile configuration is missing from you web.config. " & _
                    "It should contain  <appSettings><add key=""DtsNameGeneratePrintFile"" value=""Normal"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        Public Shared ReadOnly Property DTSInputFolder() As String
            Get
                Dim str As String = ConfigurationManager.AppSettings("DTSInputFolder")
                If str Is Nothing OrElse str = String.Empty Then
                    Throw New ApplicationException("DTSInputFolder configuration is missing from you web.config. " & _
                    "It should contain  <appSettings><add key=""DTSInputFolder"" value=""Normal"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        Public Shared ReadOnly Property DTSPackageLoginInfo() As String
            Get
                Dim str As String = ConfigurationManager.AppSettings("DTSPackageLoginInfo")
                If str Is Nothing OrElse str = String.Empty Then
                    Throw New ApplicationException("DTSPackageLoginInfo configuration is missing from you web.config. " & _
                    "It should contain  <appSettings><add key=""DTSPackageLoginInfo"" value=""Normal"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        Public Shared ReadOnly Property DataAccessType() As String
            Get
                Dim str As String = ConfigurationManager.AppSettings("DataAccessType")
                If str Is Nothing OrElse str = String.Empty Then
                    Throw New ApplicationException("DataAccessType configuration is missing from you web.config. It should contain  <appSettings><add key=""DataAccessType"" value=""data access type"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        Public Shared ReadOnly Property DefaultRoleForNewUser() As String
            Get
                Dim str As String = ConfigurationManager.AppSettings("NewUserDefaultRole")
                If str Is Nothing OrElse str = String.Empty Then
                    Throw New ApplicationException("NewUserDefaultRole configuration is missing from you web.config. It should contain  <appSettings><add key=""NewUserDefaultRole"" value=""Normal"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        Public Shared ReadOnly Property DefaultYearForCalendar() As String
            Get
                Dim str As String = ConfigurationManager.AppSettings("DefaultYearForCalendar")
                If str Is Nothing OrElse str = String.Empty Then
                    Throw New ApplicationException("DefaultYerForCalendar configuration is missing from you web.config. It should contain  <appSettings><add key=""DefaultYearForCalendar"" value=""Normal"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        Public Shared ReadOnly Property DefaultEndYearForCalendar() As String
            Get
                Dim str As String = ConfigurationManager.AppSettings("DefaultEndYearForCalendar")
                If str Is Nothing OrElse str = String.Empty Then
                    Throw New ApplicationException("DefaultYerForCalendar configuration is missing from you web.config. It should contain  <appSettings><add key=""DefaultEndYearForCalendar"" value=""Normal"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        ' Binary File
        Public Shared Function CreateEmptyBinaryFile(ByVal sPathCombineFile As String) As String
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

            Return (sTemp)
        End Function

        ' Text File
        Public Shared Sub CreateDataTextFile(ByVal sText As String, ByVal sFile As String)

            If System.IO.File.Exists(sFile) Then
                Try
                    System.IO.File.Delete(sFile)
                Catch ex As Exception
                    Throw New Exception("File cannot be deleted." & ex.Message)
                End Try

            End If

            Using sw As System.IO.StreamWriter = New System.IO.StreamWriter(sFile)
                sw.WriteLine(sText)
                sw.Close()
            End Using
        End Sub

        ' declare another roles
        Public Const UserRoles As String = "myuserroles"

        Public Const WS_CHANGE_PRACTICE As String = "D:\\RunFileWatcher\\FleInt2ChangePrt.bin"
        Public Const WS_CHANGE_PRACTICE_TEXT As String = "D:\\RunFileWatcher\\FleInt2ChangePrtText.txt"

        Public Const WS_FEE_ANALYSIS_SUMMARY As String = "D:\\RunFileWatcher\\FleInt2FeeAnSum.bin"
        Public Const WS_FEE_ANALYSIS_SUMMARY_TEXT As String = "D:\\RunFileWatcher\\FleInt2FeeAnSumText.txt"

        Public Const WS_BUILD_EXCLUSION_LIST As String = "D:\\RunFileWatcher\\FleInt2BldExcList.bin"

    End Class
End Namespace