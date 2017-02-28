Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class ReminderMapCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.ReminderMap"

        Public Enum MapCodes
            CodeId
            CodeDescription
        End Enum

        Public Sub Sort(ByVal sortField As MapCodes, ByVal isAscending As Boolean)
            Select Case sortField
                Case MapCodes.CodeId
                    InnerList.Sort(New ComparerByRdescId())
                Case MapCodes.CodeDescription
                    InnerList.Sort(New ComparerByVmRdesc())
            End Select
            If Not isAscending Then
                InnerList.Reverse()
            End If
        End Sub

        Private NotInheritable Class ComparerByRdescId
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As ReminderMap = CType(x, ReminderMap)
                Dim second As ReminderMap = CType(y, ReminderMap)
                Return first.RdescId.CompareTo(second.RdescId)
            End Function
        End Class

        Private NotInheritable Class ComparerByVmRdesc
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As ReminderMap = CType(x, ReminderMap)
                Dim second As ReminderMap = CType(y, ReminderMap)
                Return first.vmRdesc.CompareTo(second.vmRdesc)
            End Function
        End Class

        Default Public Property Item(ByVal index As Integer) As ReminderMap
            Get
                Return CType(List(index), ReminderMap)
            End Get
            Set(ByVal value As ReminderMap)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As ReminderMap) As Integer
            Return List.Add(value)
        End Function

        Public Function IndexOf(ByVal value As ReminderMap) As Integer
            Return List.IndexOf(value)
        End Function

        Public Sub Insert(ByVal index As Integer, ByVal value As ReminderMap)
            List.Insert(index, value)
        End Sub

        Public Sub Remove(ByVal value As ReminderMap)
            List.Remove(value)
        End Sub

        Public Function Contains(ByVal value As ReminderMap) As Boolean
            Return List.Contains(value)
        End Function

        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type ReminderMap.", "value")
            End If
        End Sub

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type ReminderMap.", "value")
            End If
        End Sub

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type ReminderMap.", "newValue")
            End If
        End Sub

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type ReminderMap.")
            End If
        End Sub

    End Class
End Namespace