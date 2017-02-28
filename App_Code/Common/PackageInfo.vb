Imports Microsoft.VisualBasic
Imports myDtsPackageLibrary
'Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class PackageInfo

        Private _ServerName As String = String.Empty
        Private _UserName As String = String.Empty
        Private _Password As String = String.Empty

        Sub New()
        End Sub ' New

        Public Function ReturnExtention(ByVal sFileName As String) As Boolean
            Dim fileOK As Boolean = False
            Dim fileExtension As String = System.IO.Path.GetExtension(sFileName).ToLower()
            Dim i As Integer

            Dim arrOfAllowedExts As String() = {".xls"}
            ' Dim arrOfAllowedExts As String() = {".jpg", ".jpeg", ".png", ".gif"}

            For i = 0 To arrOfAllowedExts.Length - 1
                If fileExtension = arrOfAllowedExts(i) Then
                    fileOK = True
                End If
            Next i

            Return fileOK
        End Function

        Public Function Execute_Matrix_DTS_Package(ByVal sPackageName As String, _
                ByVal myFileName As String, _
                ByVal myDropId As String, _
                ByVal myObjPkg As AccessPackage, _
                 ByVal myStartFolder As String) As String

            Dim sResult As String = String.Empty
            Dim pliServerName As String = ServerName()
            Dim pliUserName As String = UserName()
            Dim pliPassword As String = Password()

            ' create instance for dts package
            myObjPkg = New AccessPackage(pliServerName, pliUserName, pliPassword, sPackageName, Nothing, Nothing, Nothing)

            ' Array for DTS Package Global Variables
            Dim ArrParam(2) As String
            ArrParam(0) = myStartFolder 'GlobalsVar.DTSInputFolder

            'If myButton.SelectedIndex > 0 Then
            ArrParam(1) = myFileName
            ArrParam(2) = myDropId

            sResult = myObjPkg.LoadFromSQLServer(ArrParam)

            ' myErrorMessage.Text = "File name """ & myButton.SelectedItem.Value & """ has been processed ..."
            '  End If


            If Not ArrParam Is Nothing Then ArrParam = Nothing
            If Not myObjPkg Is Nothing Then myObjPkg = Nothing

            Return (sResult)
        End Function

        Private ReadOnly Property ArrSecureInfo() As String()
            Get
                Dim PackageLogInfo As String = GlobalsVar.DtsPackageLogInfoVmrpt01
                Dim myChar As Char = ";"

                Return PackageLogInfo.Split(myChar)
            End Get
        End Property

        Public Property ServerName() As String
            Get
                Return ArrSecureInfo(0)
            End Get
            Set(ByVal value As String)
                _ServerName = value
            End Set
        End Property

        Public Property UserName() As String
            Get
                Return ArrSecureInfo(1)
            End Get
            Set(ByVal value As String)
                _UserName = value
            End Set
        End Property

        Public Property Password() As String
            Get
                Return ArrSecureInfo(2)
            End Get
            Set(ByVal value As String)
                _Password = value
            End Set
        End Property
    End Class ' end of 
End Namespace