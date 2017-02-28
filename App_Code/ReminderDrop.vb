Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer
    Public Class ReminderDrop

        Private _DropId As Int32
        Private _DropDescription As String
        Private _ActiveWeb As Integer
        Private _ActiveDrop As Integer
        Private _PullDate As String

        Private _OnLineDate As String = String.Empty
        Private _OffLineDate As String = String.Empty
        Private _CStartDate As String = String.Empty
        Private _CEndDate As String = String.Empty
        Private _NR1StartDate As String = String.Empty
        Private _NR1EndDate As String = String.Empty
        Private _NR2StartDate As String = String.Empty
        Private _NR2EndDate As String = String.Empty
        Private _PrintRunDate As String = String.Empty
        Private _PrintRunSeries As String = String.Empty

        Private _MailerTypeID As String = String.Empty
        Private _VM_NPC As String = String.Empty

        Private _Rpt_CStartDate As String = String.Empty
        Private _Rpt_CEndDate As String = String.Empty
        Private _Rpt_RCYStartDate As String = String.Empty
        Private _Rpt_RCYEndDate As String = String.Empty
        Private _ReportShortDate As String = String.Empty
        Private _Rpt_NR3StartDate As String = String.Empty
        Private _Rpt_NR3EndDate As String = String.Empty
        Private _UserName As String

        Private _Exp1Date As String = String.Empty
        Private _Exp2Date As String = String.Empty
        Private _Exp3Date As String = String.Empty


        ' Constructors
        Public Sub New()
        End Sub ' NEW

        Public Sub New(ByVal dropID As Int32, _
                ByVal dropDescription As String, _
                ByVal ActiveWeb As Integer, _
                ByVal ActiveDrop As Integer, _
                ByVal PullDate As String, _
                ByVal OnLineDate As String, _
                ByVal OffLineDate As String, _
                ByVal CStartDate As String, _
                ByVal CEndDate As String, _
                ByVal NR1StartDate As String, _
                ByVal NR1EndDate As String, _
                ByVal NR2StartDate As String, _
                ByVal NR2EndDate As String, _
                ByVal PrintRunDate As String, _
                ByVal PrintRunSeries As String, _
                ByVal MailerTypeID As String, _
                ByVal VM_NPC As String, _
                ByVal Rpt_CStartDate As String, _
                ByVal Rpt_CEndDate As String, _
                ByVal Rpt_RCYStartDate As String, _
                ByVal Rpt_RCYEndDate As String, _
                ByVal ReportShortDate As String, _
                ByVal Rpt_NR3StartDate As String, _
                ByVal Rpt_NR3EndDate As String, _
                ByVal Exp1Date As String, _
                ByVal Exp2Date As String, _
                ByVal Exp3Date As String)
            _DropId = dropID
            _DropDescription = dropDescription
            _ActiveWeb = ActiveWeb
            _ActiveDrop = ActiveDrop
            _PullDate = PullDate
            _OnLineDate = OnLineDate
            _OffLineDate = OffLineDate
            _CStartDate = CStartDate
            _CEndDate = CEndDate
            _NR1StartDate = NR1StartDate
            _NR1EndDate = NR1EndDate
            _NR2StartDate = NR2StartDate
            _NR2EndDate = NR2EndDate
            _PrintRunDate = PrintRunDate
            _PrintRunSeries = PrintRunSeries
            _MailerTypeID = MailerTypeID
            _VM_NPC = VM_NPC
            _Rpt_CStartDate = Rpt_CStartDate
            _Rpt_CEndDate = Rpt_CEndDate
            _Rpt_RCYStartDate = Rpt_RCYStartDate
            _Rpt_RCYEndDate = Rpt_RCYEndDate
            _ReportShortDate = ReportShortDate
            _Rpt_NR3StartDate = Rpt_NR3StartDate
            _Rpt_NR3EndDate = Rpt_NR3EndDate
            _Exp1Date = Exp1Date
            _Exp2Date = Exp2Date
            _Exp3Date = Exp3Date
        End Sub 'New

        Public Property ReportShortDate() As String
            Get
                Return _ReportShortDate
            End Get
            Set(ByVal value As String)
                _ReportShortDate = value
            End Set
        End Property

        ' Public Properties
        Public Property DropId() As Int32
            Get
                Return _DropId
            End Get
            Set(ByVal value As Int32)
                _DropId = value
            End Set
        End Property

        Public Property MailerTypeID() As String
            Get
                Return _MailerTypeID
            End Get
            Set(ByVal value As String)
                _MailerTypeID = value
            End Set
        End Property

        Public Property VM_NPC() As String
            Get
                Return _VM_NPC
            End Get
            Set(ByVal value As String)
                _VM_NPC = value
            End Set
        End Property

        Public Property DropDescription() As String
            Get
                Return _DropDescription
            End Get
            Set(ByVal value As String)
                _DropDescription = value
            End Set
        End Property

        Public Property ActiveWeb() As Integer
            Get
                Return _ActiveWeb
            End Get
            Set(ByVal value As Integer)
                _ActiveWeb = value
            End Set
        End Property

        Public Property ActiveDrop() As Integer
            Get
                Return _ActiveDrop
            End Get
            Set(ByVal value As Integer)
                _ActiveDrop = value
            End Set
        End Property

        Public Property PullDate() As String
            Get
                Return _PullDate
            End Get
            Set(ByVal value As String)
                _PullDate = value
            End Set
        End Property

        Public Property OnLineDate() As String
            Get
                Return _OnLineDate
            End Get
            Set(ByVal value As String)
                _OnLineDate = value
            End Set
        End Property

        Public Property OffLineDate() As String
            Get
                Return _OffLineDate
            End Get
            Set(ByVal value As String)
                _OffLineDate = value
            End Set
        End Property

        Public Property CStartDate() As String
            Get
                Return _CStartDate
            End Get
            Set(ByVal value As String)
                _CStartDate = value
            End Set
        End Property

        Public Property CEndDate() As String
            Get
                Return _CEndDate
            End Get
            Set(ByVal value As String)
                _CEndDate = value
            End Set
        End Property

        Public Property NR1StartDate() As String
            Get
                Return _NR1StartDate
            End Get
            Set(ByVal value As String)
                _NR1StartDate = value
            End Set
        End Property

        Public Property NR1EndDate() As String
            Get
                Return _NR1EndDate
            End Get
            Set(ByVal value As String)
                _NR1EndDate = value
            End Set
        End Property

        Public Property NR2StartDate() As String
            Get
                Return _NR2StartDate
            End Get
            Set(ByVal value As String)
                _NR2StartDate = value
            End Set
        End Property

        Public Property NR2EndDate() As String
            Get
                Return _NR2EndDate
            End Get
            Set(ByVal value As String)
                _NR2EndDate = value
            End Set
        End Property

        Public Property PrintRunDate() As String
            Get
                Return _PrintRunDate
            End Get
            Set(ByVal value As String)
                _PrintRunDate = value
            End Set
        End Property

        Public Property PrintRunSeries() As String
            Get
                Return _PrintRunSeries
            End Get
            Set(ByVal value As String)
                _PrintRunSeries = value
            End Set
        End Property

        Public Property Rpt_CStartDate() As String
            Get
                Return _Rpt_CStartDate
            End Get
            Set(ByVal value As String)
                _Rpt_CStartDate = value
            End Set
        End Property

        Public Property Rpt_CEndDate() As String
            Get
                Return _Rpt_CEndDate
            End Get
            Set(ByVal value As String)
                _Rpt_CEndDate = value
            End Set
        End Property

        Public Property Rpt_RCYStartDate() As String
            Get
                Return _Rpt_RCYStartDate
            End Get
            Set(ByVal value As String)
                _Rpt_RCYStartDate = value
            End Set
        End Property

        Public Property Rpt_RCYEndDate() As String
            Get
                Return _Rpt_RCYEndDate
            End Get
            Set(ByVal value As String)
                _Rpt_RCYEndDate = value
            End Set
        End Property

        Public Property Rpt_NR3StartDate() As String
            Get
                Return _Rpt_NR3StartDate
            End Get
            Set(ByVal value As String)
                _Rpt_NR3StartDate = value
            End Set
        End Property

        Public Property Rpt_NR3EndDate() As String
            Get
                Return _Rpt_NR3EndDate
            End Get
            Set(ByVal value As String)
                _Rpt_NR3EndDate = value
            End Set
        End Property

        Public Property UserName() As String
            Get
                Return _UserName
            End Get
            Set(ByVal value As String)
                _UserName = value
            End Set
        End Property

        Public Property Exp1Date() As String
            Get
                Return _Exp1Date
            End Get
            Set(ByVal value As String)
                _Exp1Date = value
            End Set
        End Property

        Public Property Exp2Date() As String
            Get
                Return _Exp2Date
            End Get
            Set(ByVal value As String)
                _Exp2Date = value
            End Set
        End Property

        Public Property Exp3Date() As String
            Get
                Return _Exp3Date
            End Get
            Set(ByVal value As String)
                _Exp3Date = value
            End Set
        End Property

        ' get all records
        Public Shared Function GetAllRecords(ByVal MailerId As String) As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.ReminderDrop_GetAll(MailerId)
        End Function

        Public Shared Function GetOneReminderDropById(ByVal DropId As Int32) As ReminderDrop
            ' validate input
            'If issueId <= DefaultValues.GetIssueIdMinValue() Then
            '    Throw New ArgumentOutOfRangeException("issueId")
            'End If
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.GetOneReminderDropById(DropId)
        End Function

        Public Shared Function GetPracticesByDropID(ByVal DropId As Int32) As String
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.GetPracticesByDropID(DropId)
        End Function


        Public Shared Function Update_OneRecord(ByVal ReminderDropForUpdate As ReminderDrop) As Boolean
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.ReminderDrop_UpdOneRecord(ReminderDropForUpdate)
        End Function

        ' Position Matrix
        ' Add to History table
        Public Shared Function AfterAddToHistory(ByVal sMilerType As String) As Boolean
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.AddToHistory_Matrix(sMilerType))
        End Function

    End Class
End Namespace

