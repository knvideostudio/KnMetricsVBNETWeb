Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports System.Xml
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class vmEncryption

        Private Shared IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

        Public Sub New()
        End Sub 'New

        Public Shared Function LoadKey(ByVal sPath As String, ByVal iID As Int32, ByVal iOrder As Integer) As String
            Dim m_xmld As XmlDocument
            Dim m_node As XmlNode
            Dim sTemp As String = String.Empty

            'Create the XML Document
            m_xmld = New XmlDocument()

            'Load the Xml file
            m_xmld.Load(sPath)

            ' RecordID="1" RandomNumber="1"
            m_node = m_xmld.SelectSingleNode("Keys/Data[@RandomNumber='2' and @RecordID='1']")
            If m_node Is Nothing Then
                sTemp = "NoKey"
            Else
                sTemp = m_node.Attributes("key").Value
            End If
            ' m_node.Attributes.GetNamedItem("gender").Value

            If Not m_xmld Is Nothing Then
                m_xmld = Nothing
            End If

            Return sTemp
        End Function

        Public Shared Function ReadBynaryRecord() As String
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.ReadBinaryData()
        End Function

        Public Shared Sub AddBynaryRecord(ByVal bData() As Byte)
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            DataLayer.AddBinaryData(bData)

        End Sub

        Private Function myTextEncrypt2(ByVal sText As String, ByVal sMyKey As String) As String
            Dim byKey() As Byte = {}
            Try
                byKey = System.Text.Encoding.UTF8.GetBytes(Left(sMyKey, 8))

                Dim des As New DESCryptoServiceProvider()
                Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(sText)
                Dim ms As New MemoryStream()

                Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Return Convert.ToBase64String(ms.ToArray())

            Catch ex As Exception
                Return ex.Message
            End Try

        End Function

        Public Shared Function myTextEncrypt(ByVal sText As String, ByVal sMyKey As String) As String
            Dim encrData As vmEncryption = vmEncryptionClassHelper.GetEncryptionLayer()
            Return encrData.myTextEncrypt2(sText, sMyKey)
        End Function

        Public Shared Function myTextDecrypt(ByVal sText As String, ByVal sDecrKey As String) As String
            Dim byKey() As Byte = {}

            Dim inputByteArray(sText.Length) As Byte

            Try
                byKey = System.Text.Encoding.UTF8.GetBytes(Left(sDecrKey, 8))

                Dim des As New DESCryptoServiceProvider()
                inputByteArray = Convert.FromBase64String(sText)

                Dim ms As New MemoryStream()
                Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)

                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8

                Return encoding.GetString(ms.ToArray())

            Catch ex As Exception
                Return ex.Message
            End Try
        End Function
    End Class

    Public Class vmEncryptionClassHelper

        Private Const SQL2000_NAME = "VeterinaryMetrics.BusinessLayer.vmEncryption"

        Public Shared Function GetEncryptionLayer() As vmEncryption
            Dim trp As Type = Type.GetType(SQL2000_NAME, True)

            ' Throw an error if wrong base type
            ' If Not trp.BaseType Is Type.GetType(SQL2000_NAME) Then
            ' Throw New Exception("Data Access Layer does not inherit AccessDataMainClass!")
            ' End If

            Dim dc As vmEncryption = CType(Activator.CreateInstance(trp), vmEncryption)
            Return dc
        End Function

    End Class
End Namespace