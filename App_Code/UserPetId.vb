Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class UserPetId

        Private _ClientLastName As String = String.Empty
        Private _ClientFirstName As String = String.Empty
        Private _PracticeName As String = String.Empty
        Private _StrPracticeID As String = String.Empty
        Private _PatientName As String = String.Empty
        Private _ClientID As Int32
        Private _PatientID As Int32
        Private _OrderBatchID As Int32

        Public Sub New()
        End Sub ' New

        Public Sub New(ByVal ClientLastName As String, _
                ByVal ClientFirstName As String, _
                ByVal PracticeName As String, _
                ByVal ClientID As Int32, _
                ByVal PatientID As Int32, _
                ByVal StrPracticeID As String, _
                ByVal OrderBatchID As Int32, _
                ByVal PatientName As String)
            _ClientLastName = ClientLastName
            _ClientFirstName = ClientFirstName
            _PracticeName = PracticeName
            _ClientID = ClientID
            _PatientID = PatientID
            _StrPracticeID = StrPracticeID
            _OrderBatchID = OrderBatchID
            _PatientName = PatientName
        End Sub ' New

        Public Property PatientName() As String
            Get
                Return _PatientName
            End Get
            Set(ByVal value As String)
                _PatientName = value
            End Set
        End Property

        Public Property OrderBatchID() As Int32
            Get
                Return _OrderBatchID
            End Get
            Set(ByVal value As Int32)
                _OrderBatchID = value
            End Set
        End Property

        Public Property PatientID() As Int32
            Get
                Return _PatientID
            End Get
            Set(ByVal value As Int32)
                _PatientID = value
            End Set
        End Property

        Public Property ClientID() As Int32
            Get
                Return _ClientID
            End Get
            Set(ByVal value As Int32)
                _ClientID = value
            End Set
        End Property

        Public Property ClientLastName() As String
            Get
                Return _ClientLastName
            End Get
            Set(ByVal value As String)
                _ClientLastName = value
            End Set
        End Property

        Public Property ClientFirstName() As String
            Get
                Return _ClientFirstName
            End Get
            Set(ByVal value As String)
                _ClientFirstName = value
            End Set
        End Property

        Public Property PracticeName() As String
            Get
                Return _PracticeName
            End Get
            Set(ByVal value As String)
                _PracticeName = value
            End Set
        End Property

        Public Property StrPracticeID() As String
            Get
                Return _StrPracticeID
            End Get
            Set(ByVal value As String)
                _StrPracticeID = value
            End Set
        End Property

        Public Shared Function PetId_RequesrReorderPending() As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.PetIdRequestPending()
        End Function ' PetId_RequesrReorderPending

        Public Shared Sub PetId_ReorderAdd(ByVal sClient As String, _
            ByVal sPatient As String, _
            ByVal sPracticeId As String, _
            ByVal sUserName As String)
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            DataLayer.PetIdAddReorder(sClient, sPatient, sPracticeId, sUserName)
        End Sub ' PetId_ReorderAdd

        Public Shared Function PetId_GetClient(ByVal sClientName As String) As UserPetIdCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.PetId_GetClients(sClientName)
        End Function ' PetId_GetClient

    End Class
End Namespace