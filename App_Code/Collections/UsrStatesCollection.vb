Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace VeterinaryMetrics.BusinessLayer

    Public Class UsrStatesCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.UsrStates"

        Public Enum UsrFields
            StateByName
            StateByCode
        End Enum

        Public Sub Sort(ByVal sortField As UsrFields, ByVal isAscending As Boolean)
            Select Case sortField
                Case UsrFields.StateByName
                    InnerList.Sort(New StateByNameComparer())
                Case UsrFields.StateByCode
                    InnerList.Sort(New StateByCodeComparer())
            End Select
            If Not isAscending Then
                InnerList.Reverse()
            End If
        End Sub 'Sort

        Private NotInheritable Class StateByCodeComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UsrStates = CType(x, UsrStates)
                Dim second As UsrStates = CType(y, UsrStates)
                Return first.StateCode.CompareTo(second.StateCode)
            End Function ' Compare
        End Class ' StateByNameComparer

        Private NotInheritable Class StateByNameComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UsrStates = CType(x, UsrStates)
                Dim second As UsrStates = CType(y, UsrStates)
                Return first.StateText.CompareTo(second.StateText)
            End Function ' Compare
        End Class ' StateByNameComparer

        Default Public Property Item(ByVal index As Integer) As UsrStates
            Get
                Return CType(List(index), UsrStates)
            End Get
            Set(ByVal value As UsrStates)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As UsrStates) As Integer
            Return List.Add(value)
        End Function

        Public Function IndexOf(ByVal value As UsrStates) As Integer
            Return List.IndexOf(value)
        End Function

        Public Sub Insert(ByVal index As Integer, ByVal value As UsrStates)
            List.Insert(index, value)
        End Sub

        Public Sub Remove(ByVal value As UsrStates)
            List.Remove(value)
        End Sub

        Public Function Contains(ByVal value As UsrStates) As Boolean
            Return List.Contains(value)
        End Function

        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UsrStates.", "value")
            End If
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UsrStates.", "value")
            End If
        End Sub

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type UsrStates.", "newValue")
            End If
        End Sub

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UsrStates.")
            End If
        End Sub
    End Class
End Namespace