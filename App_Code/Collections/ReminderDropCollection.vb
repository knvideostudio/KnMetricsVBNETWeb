Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer


    Public Class ReminderDropCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.ReminderDrop"


        Default Public Property Item(ByVal index As Integer) As ReminderDrop
            Get
                Return CType(List(index), ReminderDrop)
            End Get
            Set(ByVal value As ReminderDrop)
                List(index) = value
            End Set
        End Property


        Public Function Add(ByVal value As ReminderDrop) As Integer
            Return List.Add(value)
        End Function


        Public Function IndexOf(ByVal value As ReminderDrop) As Integer
            Return List.IndexOf(value)
        End Function


        Public Sub Insert(ByVal index As Integer, ByVal value As ReminderDrop)
            List.Insert(index, value)
        End Sub


        Public Sub Remove(ByVal value As ReminderDrop)
            List.Remove(value)
        End Sub


        Public Function Contains(ByVal value As ReminderDrop) As Boolean
            Return List.Contains(value)
        End Function


        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type ReminderDrop.", "value")
            End If
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type ReminderDrop.", "value")
            End If
        End Sub

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type ReminderDrop.", "newValue")
            End If
        End Sub

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type ReminderDrop.")
            End If
        End Sub

    End Class


End Namespace