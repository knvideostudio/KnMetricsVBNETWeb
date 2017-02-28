Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class DashBoard

        Private _PracticeId As Int32
        Private _DropId As Int32

        Public Sub New()
        End Sub ' New

        Public Sub New(ByVal PracticeId As Int32, ByVal DropId As Int32)
            _PracticeId = PracticeId
            _DropId = DropId
        End Sub

        Public Property PracticeId() As Int32
            Get
                Return _PracticeId
            End Get
            Set(ByVal value As Int32)
                _PracticeId = value
            End Set
        End Property

        Public Property DropId() As Int32
            Get
                Return _DropId
            End Get
            Set(ByVal value As Int32)
                _DropId = value
            End Set
        End Property

        Public Shared Function GetIssues() As DashBoardCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetDashBoardIssues())
        End Function ' DashBoard Issues GetIssues

        Public Shared Function GetProcess(ByVal iPractice As Int32) As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetDashBoardPProcess(iPractice))
        End Function ' GetProcess

        ' Reports DashBoard
        Public Shared Function CurrentProcessView() As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.ReportCurrentProcessView())
        End Function ' CurrentProcessView

        Public Shared Function ReoportHistoryView() As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.ReportQueueHistoryView())
        End Function ' ReoportHistoryView

        Public Shared Function ReoportQueueView() As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.ReportQueueView())
        End Function ' ReoportQueueView

        Public Shared Function CurrentProcessHistoryView() As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.ReportCurrentProcessHistoryView())
        End Function ' CurrentProcessHistoryView

        Public Shared Function DisplayErrorView() As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.ReportDisplayErrorView())
        End Function ' DisplayErrorView

        Public Shared Function DisplayProcessStatus() As String()
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.ReportCurrentProcessStatus())
        End Function ' DisplayProcessStatus

        Public Shared Function DisplayMultiPurposeView(ByVal iParameter As Integer, ByVal iPractice As Int32) As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.MultiPurposeView(iParameter, iPractice))
        End Function ' DisplayMultiPurposeView

        ' added on Dec 23 2008
        Public Shared Function CurrentProcess3View() As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.ReportCurrentProcess3View())
        End Function ' CurrentProcess3View

    End Class
End Namespace