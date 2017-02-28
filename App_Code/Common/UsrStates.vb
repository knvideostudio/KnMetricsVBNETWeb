Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class UsrStates

        Private _StateCode As String = String.Empty
        Private _StateText As String = String.Empty

        Public Sub New()
        End Sub ' New

        Public Sub New(ByVal StateCode As String, ByVal StateText As String)
            _StateCode = StateCode
            _StateText = StateText
        End Sub

        Public Property StateCode() As String
            Get
                Return _StateCode
            End Get
            Set(ByVal value As String)
                _StateCode = value
            End Set
        End Property

        Public Property StateText() As String
            Get
                Return _StateText
            End Get
            Set(ByVal value As String)
                _StateText = value
            End Set
        End Property

        Public Shared Function GetStatesUS() As UsrStatesCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.GetStatesUS()
        End Function ' GetStatesUS
    End Class
End Namespace
