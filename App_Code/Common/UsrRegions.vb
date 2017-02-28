Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class UsrRegions

        Private _RegionCode As String = String.Empty
        Private _RegionText As String = String.Empty

        Public Sub New()
        End Sub ' New

        Public Sub New(ByVal RegionCode As String, ByVal RegionText As String)
            _RegionCode = RegionCode
            _RegionText = RegionText
        End Sub

        Public Property RegionCode() As String
            Get
                Return _RegionCode
            End Get
            Set(ByVal value As String)
                _RegionCode = value
            End Set
        End Property

        Public Property RegionText() As String
            Get
                Return _RegionText
            End Get
            Set(ByVal value As String)
                _RegionText = value
            End Set
        End Property

        Public Shared Function GetPracticeRegions() As UsrRegionsCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.GetPracticeRegions()
        End Function ' GetPracticeRegions
    End Class
End Namespace

