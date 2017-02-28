Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class DirectMailCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.DirectMail"

        Default Public Property Item(ByVal index As Integer) As DirectMail
            Get
                Return CType(List(index), DirectMail)
            End Get
            Set(ByVal value As DirectMail)
                List(index) = value
            End Set
        End Property


        Public Function Add(ByVal value As DirectMail) As Integer
            Return List.Add(value)
        End Function


        Public Function IndexOf(ByVal value As DirectMail) As Integer
            Return List.IndexOf(value)
        End Function


        Public Sub Insert(ByVal index As Integer, ByVal value As DirectMail)
            List.Insert(index, value)
        End Sub


        Public Sub Remove(ByVal value As DirectMail)
            List.Remove(value)
        End Sub


        Public Function Contains(ByVal value As DirectMail) As Boolean
            Return List.Contains(value)
        End Function


        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type DirectMail.", "value")
            End If
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type DirectMail.", "value")
            End If
        End Sub

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type DirectMail.", "newValue")
            End If
        End Sub

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type DirectMail.")
            End If
        End Sub

    End Class
End Namespace
