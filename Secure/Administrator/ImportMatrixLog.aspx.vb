
Namespace VeterinaryMetrics.BusinessLayer


    Partial Class ImportMatrixLog
        Inherits System.Web.UI.Page

        Private Const PATH_LOG_FOLDER As String = "C:\VetMet_Logs\DtsPackageExec\DtsPackages_Exec_Log.txt"
        'Private Const PATH_LOG_FOLDER As String = "\\vmweb01\c$\VetMet_Logs\DtsPackageExec\DtsPackages_Exec_Log.txt"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            If Not Page.IsPostBack Then

            End If
        End Sub

        Private Function ReadTextFileLog(ByVal sFilePathName As String) As String
            Dim objSr As System.IO.StreamReader = Nothing
            Dim sContent As String = String.Empty

            Try
                If System.IO.File.Exists(sFilePathName) Then
                    objSr = System.IO.File.OpenText(sFilePathName)
                    sContent = objSr.ReadToEnd()

                Else
                    sContent = "File cannot be found. Please check the path and try again!"
                End If

            Catch ex As Exception
                sContent = "ERROR: " & ex.Message
            Finally
                If Not objSr Is Nothing Then
                    objSr.Close()
                    objSr.Dispose()
                End If
            End Try

            Return (sContent)
        End Function

        Public Overrides Sub Dispose()
            MyBase.Dispose()
        End Sub

        Protected Sub btnLoadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            txtLogInfo.Text = ReadTextFileLog(PATH_LOG_FOLDER)
        End Sub
    End Class
End Namespace