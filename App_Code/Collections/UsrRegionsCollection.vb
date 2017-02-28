Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace VeterinaryMetrics.BusinessLayer

    Public Class UsrRegionsCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.UsrRegions"

        Public Enum UsrFields
            RegionByName
            RegionByCode
        End Enum

        Public Sub Sort(ByVal sortField As UsrFields, ByVal isAscending As Boolean)
            Select Case sortField
                Case UsrFields.RegionByName
                    InnerList.Sort(New RegionByNameComparer())
                Case UsrFields.RegionByCode
                    InnerList.Sort(New RegionByCodeComparer())
            End Select
            If Not isAscending Then
                InnerList.Reverse()
            End If
        End Sub 'Sort

        Private NotInheritable Class RegionByCodeComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UsrRegions = CType(x, UsrRegions)
                Dim second As UsrRegions = CType(y, UsrRegions)
                Return first.RegionCode.CompareTo(second.RegionCode)
            End Function ' Compare
        End Class ' StateByNameComparer

        Private NotInheritable Class RegionByNameComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UsrRegions = CType(x, UsrRegions)
                Dim second As UsrRegions = CType(y, UsrRegions)
                Return first.RegionText.CompareTo(second.RegionText)
            End Function ' Compare
        End Class ' StateByNameComparer

        Default Public Property Item(ByVal index As Integer) As UsrRegions
            Get
                Return CType(List(index), UsrRegions)
            End Get
            Set(ByVal value As UsrRegions)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As UsrRegions) As Integer
            Return List.Add(value)
        End Function

        Public Function IndexOf(ByVal value As UsrRegions) As Integer
            Return List.IndexOf(value)
        End Function

        Public Sub Insert(ByVal index As Integer, ByVal value As UsrRegions)
            List.Insert(index, value)
        End Sub

        Public Sub Remove(ByVal value As UsrRegions)
            List.Remove(value)
        End Sub

        Public Function Contains(ByVal value As UsrRegions) As Boolean
            Return List.Contains(value)
        End Function

        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UsrRegions.", "value")
            End If
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UsrRegions.", "value")
            End If
        End Sub

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type UsrRegions.", "newValue")
            End If
        End Sub

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UsrRegions.")
            End If
        End Sub
    End Class
End Namespace