Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace VeterinaryMetrics.BusinessLayer

    Public Class HistoryGroupsCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.HistoryGroups"

        Public Enum UsrFields
            GroupID
            GroupName
        End Enum

        Public Sub Sort(ByVal sortField As UsrFields, ByVal isAscending As Boolean)
            Select Case sortField
                Case UsrFields.GroupID
                    InnerList.Sort(New GroupIDComparer())
                Case UsrFields.GroupName
                    InnerList.Sort(New GroupNameComparer())
            End Select
            If Not isAscending Then
                InnerList.Reverse()
            End If
        End Sub 'Sort

        Private NotInheritable Class GroupIDComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As HistoryGroups = CType(x, HistoryGroups)
                Dim second As HistoryGroups = CType(y, HistoryGroups)
                Return first.GroupID.CompareTo(second.GroupID)
            End Function ' Compare
        End Class

        Private NotInheritable Class GroupNameComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As HistoryGroups = CType(x, HistoryGroups)
                Dim second As HistoryGroups = CType(y, HistoryGroups)
                Return first.GroupText.CompareTo(second.GroupText)
            End Function ' Compare
        End Class

        Default Public Property Item(ByVal index As Integer) As HistoryGroups
            Get
                Return CType(List(index), HistoryGroups)
            End Get
            Set(ByVal value As HistoryGroups)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As HistoryGroups) As Integer
            Return List.Add(value)
        End Function

        Public Function IndexOf(ByVal value As HistoryGroups) As Integer
            Return List.IndexOf(value)
        End Function

        Public Sub Insert(ByVal index As Integer, ByVal value As HistoryGroups)
            List.Insert(index, value)
        End Sub

        Public Sub Remove(ByVal value As HistoryGroups)
            List.Remove(value)
        End Sub

        Public Function Contains(ByVal value As HistoryGroups) As Boolean
            Return List.Contains(value)
        End Function

        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type HistoryGroups.", "value")
            End If
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type HistoryGroups.", "value")
            End If
        End Sub

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type HistoryGroups.", "newValue")
            End If
        End Sub

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type HistoryGroups.")
            End If
        End Sub
    End Class
End Namespace
