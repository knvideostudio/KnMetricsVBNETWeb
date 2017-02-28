Imports System.IO
Imports myDtsPackageLibrary

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class ImportMatrix
        Inherits System.Web.UI.Page

        Private myUploadFolder As String = GlobalsVar.DTSInputFolder
        Private oPackageLogInfo As New PackageInfo()
        Private objPackage As AccessPackage = Nothing

        Private Const OUT_ZIP_FOLDER As String = "ArchiveZipMatrixExcelFiles"
        Private Const PACKAGE_NAME_DTS As String = "ImportPositionMatrix_All_xls"

        Public Overrides Sub Dispose()
            Dim sFileSource As String = String.Empty
            Dim sFileTarget As String = String.Empty
            If Not oPackageLogInfo Is Nothing Then oPackageLogInfo = Nothing
            If Not objPackage Is Nothing Then objPackage = Nothing

            If Not Session("seFileNmPosition") Is Nothing Then
                sFileSource = GlobalsVar.DTSInputFolder & "\" & CType(Session("seFileNmPosition"), String)
                sFileTarget = GlobalsVar.DTSInputFolder & "\" & OUT_ZIP_FOLDER & "\" & CType(Session("seFileNmPosition"), String) & "_" & Session.SessionID
                If sFileSource.Length > 1 Then
                    If File.Exists(sFileSource) Then
                        Try
                            File.Move(sFileSource, sFileTarget)
                            'File.Delete(sFileSource)
                            'Response.AddHeader("REFRESH", "10;URL=ImportMatrix.aspx")

                        Catch ex As Exception
                            Dim sError As String = ex.Message
                            lblErrorMessage.Text = sError

                            ' Response.AddHeader("REFRESH", "10;URL=ImportMatrix.aspx")
                        End Try

                    End If
                End If
            End If

            MyBase.Dispose()

        End Sub

        Private Function GetFilesXLS(ByVal sFileName As String) As ArrayList

            Dim ArrFileList As New ArrayList
            Dim UploadFolder As DirectoryInfo = New DirectoryInfo(myUploadFolder)

            ' get all xsl files
            '"*.xls"
            Dim xslFileList() As FileInfo = UploadFolder.GetFiles(sFileName)
            Dim TotalFiles As Int32 = xslFileList.Length

            If TotalFiles > 0 Then
                For Each fileTmp As FileInfo In xslFileList
                    ArrFileList.Add(fileTmp.Name)
                Next
            End If

            Return (ArrFileList)
        End Function

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            ' Capture the ERRoR
            Try

                lblUserName.Text = User.Identity.Name
                Dim ArrFileList As ArrayList = Nothing
                Dim UploadFolder As DirectoryInfo = New DirectoryInfo(myUploadFolder)

                ' get all xsl files
                Dim xslFileList() As FileInfo = UploadFolder.GetFiles("*.xls")
                Dim TotalFiles As Int32 = xslFileList.Length

                If Not Page.IsPostBack Then
                    If TotalFiles = 0 Then
                        lblErrorMessage.Text = "No files"
                        'btnImportPosition.Enabled = False
                        ' drpPositionType.Enabled = False
                    Else
                        '   btnImportPosition.Enabled = True

                        If TotalFiles = 0 Then
                            lblErrorMessage.Text = "No files"
                            '   btnImportPosition.Enabled = False
                            '   drpPositionType.Enabled = False
                        Else
                            '  btnImportPosition.Enabled = True
                            '    drpPositionType.Enabled = True

                            ArrFileList = GetFilesXLS("*.xls")

                            If ArrFileList.Count > 0 Then
                                rdoListButton.Enabled = True
                                rdoListButton.DataSource = ArrFileList
                                rdoListButton.DataBind()
                            End If
                        End If

                    End If

                End If

                If Not xslFileList Is Nothing Then xslFileList = Nothing
                If Not ArrFileList Is Nothing Then ArrFileList = Nothing
            Catch exc As Exception
                lblErrorMessage.ForeColor = Drawing.Color.Red
                lblErrorMessage.Text = exc.Message.ToString()
            End Try
        End Sub

        Protected Sub rdoListButton_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim sSelectedValue As String = rdoListButton.SelectedItem.Value
            Dim Arr() As String = Nothing
            Dim sResultText As String = String.Empty
            Dim sMatrixType As String = String.Empty

            txtSelFileName.Text = sSelectedValue
            lblErrorMessage.Text = ""

            Arr = sSelectedValue.Split("_")

            If Arr.Length = 4 Then

                sMatrixType = Arr(2).ToLower()
                sResultText = "Drop ID: " & Arr(0) & " Type: " & Arr(3) & " " & sMatrixType

                If sMatrixType.CompareTo("pm") Then
                    btnMtxPosition.Enabled = False
                    lblSelection.Text = ""
                    lblErrorMessage.Text = "This is not a Position Matrix file."
                Else
                    btnMtxPosition.Enabled = True
                    lblSelection.Text = sResultText
                End If
            Else
                btnMtxPosition.Enabled = False
                lblErrorMessage.Text = "File name format is not valid."
            End If

        End Sub

        Protected Sub btnMtxPosition_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim sSelectedValue As String = txtSelFileName.Text
            Dim Arr() As String = Nothing
            Dim sReturnText As String = String.Empty

            System.Threading.Thread.Sleep(1000)
            Arr = sSelectedValue.Split("_")

            ' Execute the DTS Package
            sReturnText = oPackageLogInfo.Execute_Matrix_DTS_Package(PACKAGE_NAME_DTS, _
                    sSelectedValue, Arr(0), _
                    objPackage, GlobalsVar.DTSInputFolder)

            System.Threading.Thread.Sleep(1000)


            If sReturnText.Length > 2 Then
                lblErrorMessage.Text = sReturnText
            Else
                ' Adding to History Record
                ReminderDrop.AfterAddToHistory(Arr(3))

                lblSelection.Text = "File " & sSelectedValue & " has been imported successfully!"
                Session("seFileNmPosition") = sSelectedValue

                ' Response.AddHeader("REFRESH", "15;URL=ImportMatrix.aspx")
            End If
        End Sub
    End Class ' end of page class
End Namespace