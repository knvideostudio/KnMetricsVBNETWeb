Imports System.IO
Imports myDtsPackageLibrary
Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class ProcessPetId
        Inherits System.Web.UI.Page

        Private sKey As String = "glory3new19"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            lblUserName.Text = User.Identity.Name

        End Sub

        Protected Sub btnPetId_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPetId.Click
            Dim str As String = String.Empty
            Dim bstr() As Byte = Nothing

            str = vmEncryption.LoadKey("C:\Reports\vetmetkeys.xml", 1, 1)

            ' ADD Encrypted  symbol to Database
            'str = vmEncryption.myTextEncrypt(txtEncryptText.Text, sKey)
            'bstr = System.Text.Encoding.Unicode.GetBytes(str)

            'lblDisplayText.Text = str
            'If bstr.Length > 0 Then
            '    vmEncryption.AddBynaryRecord(bstr)
            'End If


        End Sub

        Protected Sub btnDecrypt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDecrypt.Click
            Dim str As String = String.Empty
            Dim str2 As String = String.Empty
            str2 = vmEncryption.ReadBynaryRecord()

            str = vmEncryption.myTextDecrypt(str2, sKey)
            lblDisplayText.Text = str
        End Sub
    End Class
End Namespace