Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace VeterinaryMetrics.BusinessLayer

    Public Class UsrTaskCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.UsrTask"

        Public Enum UsrFields
            Id
            Desc
        End Enum

        Public Sub Sort(ByVal sortField As UsrFields, ByVal isAscending As Boolean)
            Select Case sortField
                Case UsrFields.Id
                    InnerList.Sort(New TaskIdComparer())
                Case UsrFields.Desc
                    InnerList.Sort(New TaskDescComparer())
            End Select
            If Not isAscending Then
                InnerList.Reverse()
            End If
        End Sub 'Sort

        Private NotInheritable Class TaskIdComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UsrTask = CType(x, UsrTask)
                Dim second As UsrTask = CType(y, UsrTask)
                Return first.Id.CompareTo(second.Id)
            End Function ' Compare
        End Class ' TaskIdComparer

        Private NotInheritable Class TaskDescComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UsrTask = CType(x, UsrTask)
                Dim second As UsrTask = CType(y, UsrTask)
                Return first.Desc.CompareTo(second.Desc)
            End Function ' Compare
        End Class ' TaskDescComparer

        Default Public Property Item(ByVal index As Int32) As UsrTask
            Get
                Return (CType(List(index), UsrTask))
            End Get
            Set(ByVal value As UsrTask)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As UsrTask) As Int32
            Return List.Add(value)
        End Function

        Public Function IndexOf(ByVal value As UsrTask) As Int32
            Return List.IndexOf(value)
        End Function

        Public Sub Insert(ByVal index As Int32, ByVal value As UsrTask)
            List.Insert(index, value)
        End Sub

        Public Sub Remove(ByVal value As UsrTask)
            List.Remove(value)
        End Sub

        Public Function Contains(ByVal value As UsrTask) As Boolean
            Return (List.Contains(value))
        End Function

        Protected Overrides Sub OnInsert(ByVal index As Int32, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UsrTask.", "value")
            End If
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Int32, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UsrTask.", "value")
            End If
        End Sub

        Protected Overrides Sub OnSet(ByVal index As Int32, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type UsrTask.", "newValue")
            End If
        End Sub

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UsrTask.")
            End If
        End Sub
    End Class
End Namespace

