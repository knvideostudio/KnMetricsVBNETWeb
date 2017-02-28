Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class CnvtPractice

        Private _PracticeId As Int32
        Private _PracticeText As String = String.Empty

        Public Sub New()
        End Sub ' New

        Public Sub New(ByVal PracticeId As Int32, ByVal PracticeText As String)
            _PracticeId = PracticeId
            _PracticeText = PracticeText
        End Sub

        Public Property PracticeId() As Int32
            Get
                Return _PracticeId
            End Get
            Set(ByVal value As Int32)
                _PracticeId = value
            End Set
        End Property

        Public Property PracticeText() As String
            Get
                Return _PracticeText
            End Get
            Set(ByVal value As String)
                _PracticeText = value
            End Set
        End Property

        Public Shared Function GetPreNotConverted() As CnvtPracticeCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetAdminPractices())
        End Function ' GetPreNotConverted

        Public Shared Function GetPreAlreadyConverted() As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetAdminConvertedPrct())
        End Function ' GetPreAlreadyConverted

        ' feb 27, 2009 16:44 PM
        Public Shared Function GetPracticesSummaryReport() As CnvtPracticeCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetReportsPractice())
        End Function ' GetPracticesSummaryReport

    End Class
End Namespace
