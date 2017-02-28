Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class UsrTask

        Private _Id As Int32
        Private _Desc As String = String.Empty

        Public Sub New()
        End Sub ' New

        Public Sub New(ByVal Id As Int32, ByVal Desc As String)
            _Id = Id
            _Desc = Desc
        End Sub

        Public Property Id() As Int32
            Get
                Return (_Id)
            End Get
            Set(ByVal value As Int32)
                _Id = value
            End Set
        End Property

        Public Property Desc() As String
            Get
                Return (_Desc)
            End Get
            Set(ByVal value As String)
                _Desc = value
            End Set
        End Property

        Public Shared Function GetUsers() As UsrTaskCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetUsers())
        End Function ' GetUsers

        Public Shared Function GetGroups() As UsrTaskCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetGroups())
        End Function ' GetGroups

        Public Shared Function GetTasks() As UsrTaskCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetTasks())
        End Function ' GetTasks

        Public Shared Function GetTaskByUser(ByVal User As String) As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetTaskByUser(User))
        End Function ' GetTaskByUser

        Public Shared Function AddTaskNew(ByVal UserInfo() As String) As Boolean
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.AddTaskNew(UserInfo))
        End Function ' AddTaskNew

        Public Shared Function DeleteTask(ByVal iTask As Int32) As Boolean
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.DeleteTask(iTask))
        End Function ' DeleteTask

    End Class
End Namespace

