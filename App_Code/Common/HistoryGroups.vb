Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class HistoryGroups

        Private _GroupID As Integer
        Private _GroupText As String = String.Empty

        Public Sub New()
        End Sub ' New

        Public Sub New(ByVal GroupID As Integer, ByVal GroupText As String)
            _GroupID = GroupID
            _GroupText = GroupText
        End Sub

        Public Property GroupID() As Integer
            Get
                Return _GroupID
            End Get
            Set(ByVal value As Integer)
                _GroupID = value
            End Set
        End Property

        Public Property GroupText() As String
            Get
                Return _GroupText
            End Get
            Set(ByVal value As String)
                _GroupText = value
            End Set
        End Property

        Public Shared Function GetGroups() As HistoryGroupsCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.GetHistoryGroups()
        End Function ' GetGroups
    End Class
End Namespace