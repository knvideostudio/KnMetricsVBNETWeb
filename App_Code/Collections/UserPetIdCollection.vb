Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class UserPetIdCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.UserPetId"

        Public Enum UserFields
            ClientLastName
            ClientFirstName
            PracticeName
            ClientID
            PatientID
            PracticeID
            OrderBatchID
            PatientName
        End Enum

        Public Sub Sort(ByVal sortField As UserFields, ByVal isAscending As Boolean)
            Select Case sortField
                Case UserFields.ClientLastName
                    InnerList.Sort(New ClientLastNameComparer())
                Case UserFields.ClientFirstName
                    InnerList.Sort(New ClientFirstNameComparer())
                Case UserFields.PracticeName
                    InnerList.Sort(New PracticeNameComparer())
                Case UserFields.PatientName
                    InnerList.Sort(New PatientNameComparer())
            End Select
            If Not isAscending Then
                InnerList.Reverse()
            End If
        End Sub 'Sort

        Private NotInheritable Class PatientNameComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UserPetId = CType(x, UserPetId)
                Dim second As UserPetId = CType(y, UserPetId)
                Return first.PatientName.CompareTo(second.PatientName)
            End Function
        End Class 'Compare

        Private NotInheritable Class PracticeNameComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UserPetId = CType(x, UserPetId)
                Dim second As UserPetId = CType(y, UserPetId)
                Return first.PracticeName.CompareTo(second.PracticeName)
            End Function
        End Class 'Compare

        Private NotInheritable Class ClientLastNameComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UserPetId = CType(x, UserPetId)
                Dim second As UserPetId = CType(y, UserPetId)
                Return first.ClientLastName.CompareTo(second.ClientLastName)
            End Function
        End Class 'Compare

        Private NotInheritable Class ClientFirstNameComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UserPetId = CType(x, UserPetId)
                Dim second As UserPetId = CType(y, UserPetId)
                Return first.ClientFirstName.CompareTo(second.ClientFirstName)
            End Function
        End Class 'Compare

        Default Public Property Item(ByVal index As Integer) As UserPetId
            Get
                Return CType(List(index), UserPetId)
            End Get
            Set(ByVal value As UserPetId)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As UserPetId) As Integer
            Return List.Add(value)
        End Function 'Add


        Public Function IndexOf(ByVal value As UserPetId) As Integer
            Return List.IndexOf(value)
        End Function 'IndexOf


        Public Sub Insert(ByVal index As Integer, ByVal value As UserPetId)
            List.Insert(index, value)
        End Sub 'Insert


        Public Sub Remove(ByVal value As UserPetId)
            List.Remove(value)
        End Sub 'Remove


        Public Function Contains(ByVal value As UserPetId) As Boolean
            Return List.Contains(value)
        End Function 'Contains

        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UserPetId.", "value")
            End If
        End Sub 'OnInsert

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UserPetId.", "value")
            End If
        End Sub 'OnRemove

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type UserPetId.", "newValue")
            End If
        End Sub 'OnSet

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UserPetId.")
            End If
        End Sub 'OnValidate

    End Class
End Namespace

