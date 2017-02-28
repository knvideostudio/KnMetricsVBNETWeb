Imports System.IO
Imports myDtsPackageLibrary

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class ImportTargetMX
        Inherits System.Web.UI.Page

        Private myUploadFolder As String = GlobalsVar.DTSInputFolder
        Private oPackageLogInfo As New PackageInfo()
        Private objPackage As AccessPackage = Nothing

        Private Const OUT_ZIP_FOLDER As String = "ArchiveZipMatrixExcelFiles"
        Private Const PACKAGE_NAME_DTS As String = "ImportTargetCriteria_All"

        Public Overrides Sub Dispose()
            Dim sFileSource As String = String.Empty
            Dim sFileTarget As String = String.Empty
            If Not oPackageLogInfo Is Nothing Then oPackageLogInfo = Nothing
            If Not objPackage Is Nothing Then objPackage = Nothing

            If Not Session("seFileNm") Is Nothing Then
                sFileSource = GlobalsVar.DTSInputFolder & "\" & CType(Session("seFileNm"), String)
                sFileTarget = GlobalsVar.DTSInputFolder & "\" & OUT_ZIP_FOLDER & "\" & CType(Session("seFileNm"), String) & "_" & Session.SessionID
                If sFileSource.Length > 1 Then
                    If File.Exists(sFileSource) Then
                        Try
                            File.Move(sFileSource, sFileTarget)
                            'File.Delete(sFileSource)
                            'Response.AddHeader("REFRESH", "10;URL=ImportTargetMX.aspx")

                        Catch ex As Exception
                            Dim sError As String = ex.Message
                            lblErrorMessage.Text = sError
                            ' Response.AddHeader("REFRESH", "10;URL=ImportTargetMX.aspx")

                        End Try
                    End If
                End If
            End If

            MyBase.Dispose()

            ' Response.Redirect("ImportTargetMX.aspx")

        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            lblUserName.Text = User.Identity.Name
            Dim ArrFileList As New ArrayList
            Dim UploadFolder As DirectoryInfo = New DirectoryInfo(myUploadFolder)

            ' get all xsl files
            Dim xslFileList() As FileInfo = UploadFolder.GetFiles("*.xls")
            Dim TotalFiles As Int32 = xslFileList.Length

            Session("seFileNm") = Nothing

            If Not Page.IsPostBack Then
                If TotalFiles = 0 Then
                    lblErrorMessage.Text = "No files"
                Else

                    If TotalFiles = 0 Then
                        lblErrorMessage.Text = "No files"
                    Else
                        For Each fileTmp As FileInfo In xslFileList

                            'If fileTmp.Extension = ".txt" Or fileTmp.Extension = ".xls" Then
                            ArrFileList.Add(fileTmp.Name) ' & " " & fileTmp.CreationTime.ToShortDateString() & " " & fileTmp.CreationTime.ToShortTimeString())
                            'End If

                        Next
                        rdoListButton.Enabled = True
                        rdoListButton.DataSource = ArrFileList
                        rdoListButton.DataBind()
                    End If
                End If
            End If

            If Not xslFileList Is Nothing Then xslFileList = Nothing
            If Not ArrFileList Is Nothing Then ArrFileList = Nothing
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

                If sMatrixType.CompareTo("tm") Then
                    btnMtxTarget.Enabled = False
                    lblSelection.Text = ""
                    lblErrorMessage.Text = "This is not a Target Matrix file."
                Else
                    btnMtxTarget.Enabled = True
                    lblSelection.Text = sResultText
                End If
            Else
                btnMtxTarget.Enabled = False
                lblErrorMessage.Text = "File name format is not valid."
            End If

        End Sub

        Protected Sub btnMtxTarget_Click(ByVal sender As Object, ByVal e As System.EventArgs)
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
                lblSelection.Text = "File " & sSelectedValue & " has been imported successfully!"
                Session("seFileNm") = sSelectedValue

                'Response.AddHeader("REFRESH", "15;URL=ImportTargetMX.aspx")
            End If


        End Sub
    End Class

End Namespace