Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace VeterinaryMetrics.BusinessLayer

    Public Class UsrMailerCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.UsrMailer"

        Public Enum UsrFields
            TypeId
            TypeDesc
        End Enum

        Public Sub Sort(ByVal sortField As UsrFields, ByVal isAscending As Boolean)
            Select Case sortField
                Case UsrFields.TypeId
                    InnerList.Sort(New TypeIdComparer())
                Case UsrFields.TypeDesc
                    InnerList.Sort(New TypeDescComparer())
            End Select
            If Not isAscending Then
                InnerList.Reverse()
            End If
        End Sub 'Sort

        Private NotInheritable Class TypeIdComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UsrMailer = CType(x, UsrMailer)
                Dim second As UsrMailer = CType(y, UsrMailer)
                Return first.TypeId.CompareTo(second.TypeId)
            End Function ' Compare
        End Class ' TypeIdComparer

        Private NotInheritable Class TypeDescComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UsrMailer = CType(x, UsrMailer)
                Dim second As UsrMailer = CType(y, UsrMailer)
                Return first.TypeDesc.CompareTo(second.TypeDesc)
            End Function ' Compare
        End Class ' TypeDescComparer

        Default Public Property Item(ByVal index As Integer) As UsrMailer
            Get
                Return (CType(List(index), UsrMailer))
            End Get
            Set(ByVal value As UsrMailer)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As UsrMailer) As Integer
            Return List.Add(value)
        End Function

        Public Function IndexOf(ByVal value As UsrMailer) As Integer
            Return List.IndexOf(value)
        End Function

        Public Sub Insert(ByVal index As Integer, ByVal value As UsrMailer)
            List.Insert(index, value)
        End Sub

        Public Sub Remove(ByVal value As UsrMailer)
            List.Remove(value)
        End Sub

        Public Function Contains(ByVal value As UsrMailer) As Boolean
            Return (List.Contains(value))
        End Function

        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UsrMailer.", "value")
            End If
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UsrMailer.", "value")
            End If
        End Sub

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type UsrMailer.", "newValue")
            End If
        End Sub

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UsrMailer.")
            End If
        End Sub
    End Class
End Namespace

