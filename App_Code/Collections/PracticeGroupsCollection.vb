Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace VeterinaryMetrics.BusinessLayer

    Public Class PracticeGroupsCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.PracticeGroups"

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
                Dim first As PracticeGroups = CType(x, PracticeGroups)
                Dim second As PracticeGroups = CType(y, PracticeGroups)
                Return first.GroupID.CompareTo(second.GroupID)
            End Function ' Compare
        End Class

        Private NotInheritable Class GroupNameComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As PracticeGroups = CType(x, PracticeGroups)
                Dim second As PracticeGroups = CType(y, PracticeGroups)
                Return first.GroupText.CompareTo(second.GroupText)
            End Function ' Compare
        End Class

        Default Public Property Item(ByVal index As Integer) As PracticeGroups
            Get
                Return CType(List(index), PracticeGroups)
            End Get
            Set(ByVal value As PracticeGroups)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As PracticeGroups) As Integer
            Return List.Add(value)
        End Function

        Public Function IndexOf(ByVal value As PracticeGroups) As Integer
            Return List.IndexOf(value)
        End Function

        Public Sub Insert(ByVal index As Integer, ByVal value As PracticeGroups)
            List.Insert(index, value)
        End Sub

        Public Sub Remove(ByVal value As PracticeGroups)
            List.Remove(value)
        End Sub

        Public Function Contains(ByVal value As PracticeGroups) As Boolean
            Return List.Contains(value)
        End Function

        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type PracticeGroups.", "value")
            End If
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type PracticeGroups.", "value")
            End If
        End Sub

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type PracticeGroups.", "newValue")
            End If
        End Sub

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type PracticeGroups.")
            End If
        End Sub
    End Class
End Namespace
