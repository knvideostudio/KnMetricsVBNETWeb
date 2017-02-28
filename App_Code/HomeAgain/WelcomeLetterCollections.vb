Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace VeterinaryMetrics.BusinessLayer

    Public Class WelcomeLetterCollections
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.WelcomeLetter"

        Public Enum WelFields
            BatchID
            StartDate
        End Enum

        Public Sub Sort(ByVal sortField As WelFields, ByVal isAscending As Boolean)
            Select Case sortField
                Case WelFields.StartDate
                    InnerList.Sort(New StartDateComparer())
            End Select
            If Not isAscending Then
                InnerList.Reverse()
            End If
        End Sub 'Sort

        Private NotInheritable Class StartDateComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As WelcomeLetter = CType(x, WelcomeLetter)
                Dim second As WelcomeLetter = CType(y, WelcomeLetter)
                Return first.StartDate.CompareTo(second.StartDate)
            End Function ' Compare
        End Class ' StartDateComparer

        Default Public Property Item(ByVal index As Integer) As WelcomeLetter
            Get
                Return CType(List(index), WelcomeLetter)
            End Get
            Set(ByVal value As WelcomeLetter)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As WelcomeLetter) As Integer
            Return List.Add(value)
        End Function

        Public Function IndexOf(ByVal value As WelcomeLetter) As Integer
            Return List.IndexOf(value)
        End Function

        Public Sub Insert(ByVal index As Integer, ByVal value As WelcomeLetter)
            List.Insert(index, value)
        End Sub

        Public Sub Remove(ByVal value As WelcomeLetter)
            List.Remove(value)
        End Sub

        Public Function Contains(ByVal value As WelcomeLetter) As Boolean
            Return List.Contains(value)
        End Function

        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type WelcomeLetter.", "value")
            End If
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type WelcomeLetter.", "value")
            End If
        End Sub

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type WelcomeLetter.", "newValue")
            End If
        End Sub

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type WelcomeLetter.")
            End If
        End Sub
    End Class
End Namespace