Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class ReminderBlock

        Private _BlockId As Integer
        Private _BlockText As String = String.Empty

        Public Sub New()
        End Sub ' New

        Public Sub New(ByVal BlockId As Integer, ByVal BlockText As String)
            _BlockId = BlockId
            _BlockText = BlockText
        End Sub

        Public Property BlockId() As Integer
            Get
                Return _BlockId
            End Get
            Set(ByVal value As Integer)
                _BlockId = value
            End Set
        End Property

        Public Property BlockText() As String
            Get
                Return _BlockText
            End Get
            Set(ByVal value As String)
                _BlockText = value
            End Set
        End Property

        Public Shared Function GetBlocks() As ReminderBlockCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetReminderBlocks())
        End Function ' GetBlocks

        ' added Feb 19 2009 1:41
        Public Shared Function GetDrops() As ReminderBlockCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetRemindersDrop())
        End Function ' GetDrops

        Public Shared Function GetRemindersProcess(ByVal iDropId As Int32) As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetRemindersProcess(iDropId))
        End Function ' GetRemindersProcess

        ' Feb 20, 2009 
        Public Shared Function BuildRemindersExclusionList(ByVal sXML As String) As Boolean
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.BuildRemindersExclusionList(sXML))
        End Function ' BuildRemindersExclusionList

        ' Feb 27, 2009 10:16 AM
        ' Reports DATA
        Public Shared Function GetReportsDate() As String()
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetReportsDate())
        End Function ' GetReportsDate

        ' Display GridView
        Public Shared Function GetReportsDateView(ByVal iPractice As Int32) As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetReportsDateView(iPractice))
        End Function ' GetReportsDateView

    End Class
End Namespace
