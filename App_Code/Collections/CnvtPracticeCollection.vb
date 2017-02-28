Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace VeterinaryMetrics.BusinessLayer

    Public Class CnvtPracticeCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.CnvtPractice"

        Public Enum UsrFields
            PracticeId
            PracticeText
        End Enum

        Public Sub Sort(ByVal sortField As UsrFields, ByVal isAscending As Boolean)
            Select Case sortField
                Case UsrFields.PracticeId
                    InnerList.Sort(New PracticeIdComparer())
                Case UsrFields.PracticeText
                    InnerList.Sort(New PracticeTextComparer())
            End Select
            If Not isAscending Then
                InnerList.Reverse()
            End If
        End Sub 'Sort

        Private NotInheritable Class PracticeIdComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Int32 Implements IComparer.Compare
                Dim first As CnvtPractice = CType(x, CnvtPractice)
                Dim second As CnvtPractice = CType(y, CnvtPractice)
                Return first.PracticeId.CompareTo(second.PracticeId)
            End Function ' Compare
        End Class

        Private NotInheritable Class PracticeTextComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Int32 Implements IComparer.Compare
                Dim first As CnvtPractice = CType(x, CnvtPractice)
                Dim second As CnvtPractice = CType(y, CnvtPractice)
                Return first.PracticeText.CompareTo(second.PracticeText)
            End Function ' Compare
        End Class

        Default Public Property Item(ByVal index As Int32) As CnvtPractice
            Get
                Return CType(List(index), CnvtPractice)
            End Get
            Set(ByVal value As CnvtPractice)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As CnvtPractice) As Int32
            Return List.Add(value)
        End Function

        Public Function IndexOf(ByVal value As CnvtPractice) As Int32
            Return List.IndexOf(value)
        End Function

        Public Sub Insert(ByVal index As Int32, ByVal value As CnvtPractice)
            List.Insert(index, value)
        End Sub

        Public Sub Remove(ByVal value As CnvtPractice)
            List.Remove(value)
        End Sub

        Public Function Contains(ByVal value As CnvtPractice) As Boolean
            Return List.Contains(value)
        End Function

        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type CnvtPractice.", "value")
            End If
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type CnvtPractice.", "value")
            End If
        End Sub

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type CnvtPractice.", "newValue")
            End If
        End Sub

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type CnvtPractice.")
            End If
        End Sub
    End Class
End Namespace

