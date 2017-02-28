Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class UsrMailer

        Private _TypeId As String = String.Empty
        Private _TypeDesc As String = String.Empty

        Public Sub New()
        End Sub ' New

        Public Sub New(ByVal TypeId As String, ByVal TypeDesc As String)
            _TypeId = TypeId
            _TypeDesc = TypeDesc
        End Sub

        Public Property TypeId() As String
            Get
                Return (_TypeId)
            End Get
            Set(ByVal value As String)
                _TypeId = value
            End Set
        End Property

        Public Property TypeDesc() As String
            Get
                Return (_TypeDesc)
            End Get
            Set(ByVal value As String)
                _TypeDesc = value
            End Set
        End Property

        Public Shared Function GetAll() As UsrMailerCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetMailers())
        End Function ' GetAll
    End Class
End Namespace


