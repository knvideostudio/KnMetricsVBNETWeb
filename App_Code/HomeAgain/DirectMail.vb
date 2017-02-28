Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class DirectMail

        Private _PracticeId As Int32
        Private _ProcessedDate As String = String.Empty
        Private _PracticeName As String = String.Empty
        Private _SpeciesName As String = String.Empty
        Private _ActualTotal As Int32
        Private _AlreadySendTotal As Int32
        Private _CanineTotal As Int32
        Private _FelineTotal As Int32
        Private _SignUpDate As String = String.Empty
        Private _BatchId As Int32

        ' Constructors
        Public Sub New()
        End Sub

        Public Sub New(ByVal BatchId As Int32)
            _BatchId = BatchId
        End Sub

        Public Sub New(ByVal PracticeId As Int32, _
                    ByVal PracticeName As String, _
                    ByVal SignUpDate As String, _
                    ByVal BatchId As Int32)
            _PracticeId = PracticeId
            _PracticeName = PracticeName
            _SignUpDate = SignUpDate
            _BatchId = BatchId
        End Sub

        Public Sub New(ByVal PracticeId As Int32, _
                    ByVal PracticeName As String)
            _PracticeId = PracticeId
            _PracticeName = PracticeName
        End Sub

        Public Sub New(ByVal PracticeId As Int32, _
                    ByVal ProcessedDate As String, _
                    ByVal PracticeName As String, _
                    ByVal SpeciesName As String, _
                    ByVal ActualTotal As Int32, _
                    ByVal AlreadySendTotal As Int32, _
                    ByVal CanineTotal As Int32, _
                    ByVal FelineTotal As Int32, _
                    ByVal BatchId As Int32)

            _PracticeId = PracticeId
            _ProcessedDate = ProcessedDate
            _PracticeName = PracticeName
            _SpeciesName = SpeciesName
            _ActualTotal = ActualTotal
            _AlreadySendTotal = AlreadySendTotal
            _CanineTotal = CanineTotal
            _FelineTotal = FelineTotal
            _BatchId = BatchId
        End Sub

        Public Property BatchId() As Int32
            Get
                Return _BatchId
            End Get
            Set(ByVal value As Int32)
                _BatchId = value
            End Set
        End Property

        Public Property SignUpDate() As String
            Get
                Return _SignUpDate
            End Get
            Set(ByVal value As String)
                _SignUpDate = value
            End Set
        End Property

        Public Property CanineTotal() As Int32
            Get
                Return _CanineTotal
            End Get
            Set(ByVal value As Int32)
                _CanineTotal = value
            End Set
        End Property

        Public Property FelineTotal() As Int32
            Get
                Return _FelineTotal
            End Get
            Set(ByVal value As Int32)
                _FelineTotal = value
            End Set
        End Property

        Public Property ActualTotal() As Int32
            Get
                Return _ActualTotal
            End Get
            Set(ByVal value As Int32)
                _ActualTotal = value
            End Set
        End Property

        Public Property AlreadySendTotal() As Int32
            Get
                Return _AlreadySendTotal
            End Get
            Set(ByVal value As Int32)
                _AlreadySendTotal = value
            End Set
        End Property

        Public Property PracticeId() As Int32
            Get
                Return _PracticeId
            End Get
            Set(ByVal value As Int32)
                _PracticeId = value
            End Set
        End Property

        Public Property ProcessedDate() As String
            Get
                Return _ProcessedDate
            End Get
            Set(ByVal value As String)
                _ProcessedDate = value
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

        Public Property SpeciesName() As String
            Get
                Return _SpeciesName
            End Get
            Set(ByVal value As String)
                _SpeciesName = value
            End Set
        End Property

        Public Shared Function DirectMailRun(ByVal batchid As Int32) As DirectMailCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.DirectMail_Run(batchid)
        End Function

        Public Shared Function DirectMail_PracticesWait() As DirectMailCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.DirectMail_PracticesWait()
        End Function

        Public Shared Sub DirectMail_AssignPractices(ByVal PracValues As String)
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            DataLayer.DirectMail_AssignPractices(PracValues)
        End Sub

        ' return table in ListBox
        Public Shared Function DirectMail_CreateDtTable(ByVal coll As DirectMailCollection, _
                    ByVal sTableName As String) As System.Data.DataTable

            Dim m As Integer = 0
            Dim idRow As System.Data.DataRow
            Dim dt As New System.Data.DataTable(sTableName)
            Dim i As System.Data.DataColumn = New System.Data.DataColumn("PrId")

            i.DataType = System.Type.GetType("System.Int32")
            dt.Columns.Add(i)

            Dim sName As System.Data.DataColumn = New System.Data.DataColumn("PrName")
            sName.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sName)


            For m = 0 To coll.Count - 1
                idRow = dt.NewRow()
                idRow.Item("PrId") = coll.Item(m).PracticeId
                idRow.Item("PrName") = coll.Item(m).PracticeName & ";" & coll.Item(m).PracticeId.ToString()
                dt.Rows.Add(idRow)
            Next m

            Return dt
        End Function

        ' get Practices Already Send
        Public Shared Function DirectMail_GetPractices() As DirectMailCollection

            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.DirectMail_GetPractices()
        End Function

        ' get Practices Already Send
        Public Shared Function DirectMail_GetPracticesAlreadySend() As DirectMailCollection

            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.DirectMail_PracticesAlreadySend()
        End Function


    End Class
End Namespace