Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace VeterinaryMetrics.BusinessLayer

    Public Class ReminderBlockCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.ReminderBlock"

        Public Enum UsrFields
            BlockId
            BlockText
        End Enum

        Public Sub Sort(ByVal sortField As UsrFields, ByVal isAscending As Boolean)
            Select Case sortField
                Case UsrFields.BlockId
                    InnerList.Sort(New BlockIdComparer())
                Case UsrFields.BlockText
                    InnerList.Sort(New BlockTextComparer())
            End Select
            If Not isAscending Then
                InnerList.Reverse()
            End If
        End Sub 'Sort

        Private NotInheritable Class BlockIdComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As ReminderBlock = CType(x, ReminderBlock)
                Dim second As ReminderBlock = CType(y, ReminderBlock)
                Return first.BlockId.CompareTo(second.BlockId)
            End Function ' Compare
        End Class

        Private NotInheritable Class BlockTextComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As ReminderBlock = CType(x, ReminderBlock)
                Dim second As ReminderBlock = CType(y, ReminderBlock)
                Return first.BlockText.CompareTo(second.BlockText)
            End Function ' Compare
        End Class

        Default Public Property Item(ByVal index As Integer) As ReminderBlock
            Get
                Return CType(List(index), ReminderBlock)
            End Get
            Set(ByVal value As ReminderBlock)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As ReminderBlock) As Integer
            Return List.Add(value)
        End Function

        Public Function IndexOf(ByVal value As ReminderBlock) As Integer
            Return List.IndexOf(value)
        End Function

        Public Sub Insert(ByVal index As Integer, ByVal value As ReminderBlock)
            List.Insert(index, value)
        End Sub

        Public Sub Remove(ByVal value As ReminderBlock)
            List.Remove(value)
        End Sub

        Public Function Contains(ByVal value As ReminderBlock) As Boolean
            Return List.Contains(value)
        End Function

        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type ReminderBlock.", "value")
            End If
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type ReminderBlock.", "value")
            End If
        End Sub

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type ReminderBlock.", "newValue")
            End If
        End Sub

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type ReminderBlock.")
            End If
        End Sub
    End Class
End Namespace
