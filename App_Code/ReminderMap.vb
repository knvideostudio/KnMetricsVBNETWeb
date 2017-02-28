Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class ReminderMap

        Private _PracticeId As Int32
        Private _PracticeName As String = String.Empty
        Private _RdescId As Int32
        Private _vmRdesc As String = String.Empty
        Private _Rdesc As String = String.Empty

        Private _ReminderType As String = String.Empty

        Public Sub New(ByVal PracticeName As String, _
                    ByVal PracticeId As Int32)
            _PracticeName = PracticeName
            _PracticeId = PracticeId
        End Sub

        Public Sub New(ByVal RdescId As Int32, _
                        ByVal vmRdesc As String)
            _RdescId = RdescId
            _vmRdesc = vmRdesc
        End Sub ' NEW

        Public Sub New()
        End Sub ' NEW

        Public Property ReminderType() As String
            Get
                Return _ReminderType
            End Get
            Set(ByVal value As String)
                _ReminderType = value
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

        Public Property vmRdesc() As String
            Get
                Return _vmRdesc
            End Get
            Set(ByVal value As String)
                _vmRdesc = value
            End Set
        End Property

        Public Property Rdesc() As String
            Get
                Return _Rdesc
            End Get
            Set(ByVal value As String)
                _Rdesc = value
            End Set
        End Property

        Public Property RdescId() As Int32
            Get
                Return _RdescId
            End Get
            Set(ByVal value As Int32)
                _RdescId = value
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

        Public Shared Function GetNotMappedCode() As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.RemainderMap_GetNotMappedCode()
        End Function

        Public Shared Function GetMappedCode(ByVal iPractice As Int32) As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.RemainderMap_GetMappedCode(iPractice)
        End Function

        Public Shared Function GetPractices() As ReminderMapCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.RemainderMap_GetPractices()
        End Function

        Public Shared Function ReminderMapCreateDtTable(ByVal coll As ReminderMapCollection, _
            ByVal sTableName As String, _
            ByVal sColumnID As String, _
            ByVal sColumnDesc As String) As System.Data.DataTable

            Dim m As Integer = 0
            Dim idRow As System.Data.DataRow
            Dim dt As New System.Data.DataTable(sTableName)
            Dim sDescID As String = String.Empty
            Dim i As System.Data.DataColumn = New System.Data.DataColumn(sColumnID)

            i.DataType = System.Type.GetType("System.Int32")
            dt.Columns.Add(i)

            Dim sName As System.Data.DataColumn = New System.Data.DataColumn(sColumnDesc)
            sName.DataType = System.Type.GetType("System.String")
            dt.Columns.Add(sName)


            For m = 0 To coll.Count - 1
                idRow = dt.NewRow()

                If sColumnID = "RdescId" Then
                    sDescID = coll.Item(m).RdescId.ToString()

                    If sDescID.Length = 1 Then sDescID = "00" & sDescID
                    If sDescID.Length = 2 Then sDescID = "0" & sDescID

                    idRow.Item(sColumnID) = coll.Item(m).RdescId
                    idRow.Item(sColumnDesc) = sDescID & " " & coll.Item(m).vmRdesc
                End If
                If sColumnID = "PracticeID" Then
                    idRow.Item(sColumnID) = coll.Item(m).PracticeId
                    idRow.Item(sColumnDesc) = coll.Item(m).PracticeId.ToString() & " " & coll.Item(m).PracticeName
                End If

                dt.Rows.Add(idRow)
            Next m

            Return dt
        End Function

        Public Shared Function GetRightSide() As ReminderMapCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.RemainderMap_GetRightSide()
        End Function
    End Class
End Namespace