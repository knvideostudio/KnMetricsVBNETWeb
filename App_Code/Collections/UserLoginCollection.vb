Imports Microsoft.VisualBasic

Imports System
Imports System.Collections

Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class UserLoginCollection
        Inherits CollectionBase

        Private Const STR_CLASSNAME As String = "VeterinaryMetrics.BusinessLayer.UserLogin"

        Public Enum UserFields
            InitValue
            Username
            DisplayName
            RoleName
            UniqueValueID
        End Enum

        Public Sub Sort(ByVal sortField As UserFields, ByVal isAscending As Boolean)
            Select Case sortField
                Case UserFields.Username
                    InnerList.Sort(New UsernameComparer())
                Case UserFields.DisplayName
                    InnerList.Sort(New DisplayNameComparer())
                Case UserFields.RoleName
                    InnerList.Sort(New RoleNameComparer())
            End Select
            If Not isAscending Then
                InnerList.Reverse()
            End If
        End Sub 'Sort


        Private NotInheritable Class UsernameComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UserLogin = CType(x, UserLogin)
                Dim second As UserLogin = CType(y, UserLogin)
                Return first.Username.CompareTo(second.Username)
            End Function 'Compare
        End Class 'UsernameComparer


        Private NotInheritable Class DisplayNameComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UserLogin = CType(x, UserLogin)
                Dim second As UserLogin = CType(y, UserLogin)
                Return first.DisplayName.CompareTo(second.DisplayName)
            End Function 'Compare
        End Class 'DisplayNameComparer



        Private NotInheritable Class RoleNameComparer
            Implements IComparer

            Public Function [Compare](ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
                Dim first As UserLogin = CType(x, UserLogin)
                Dim second As UserLogin = CType(y, UserLogin)
                Return first.RoleName.CompareTo(second.RoleName)
            End Function 'Compare
        End Class 'RoleNameComparer

        Default Public Property Item(ByVal index As Integer) As UserLogin
            Get
                Return CType(List(index), UserLogin)
            End Get
            Set(ByVal value As UserLogin)
                List(index) = value
            End Set
        End Property

        Public Function Add(ByVal value As UserLogin) As Integer
            Return List.Add(value)
        End Function 'Add


        Public Function IndexOf(ByVal value As UserLogin) As Integer
            Return List.IndexOf(value)
        End Function 'IndexOf


        Public Sub Insert(ByVal index As Integer, ByVal value As UserLogin)
            List.Insert(index, value)
        End Sub 'Insert


        Public Sub Remove(ByVal value As UserLogin)
            List.Remove(value)
        End Sub 'Remove


        Public Function Contains(ByVal value As UserLogin) As Boolean
            Return List.Contains(value)
        End Function 'Contains

        Protected Overrides Sub OnInsert(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UserLogin.", "value")
            End If
        End Sub 'OnInsert

        Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UserLogin.", "value")
            End If
        End Sub 'OnRemove

        Protected Overrides Sub OnSet(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
            If Not newValue.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("newValue must be of type UserLogin.", "newValue")
            End If
        End Sub 'OnSet

        Protected Overrides Sub OnValidate(ByVal value As Object)
            If Not value.GetType() Is Type.GetType(STR_CLASSNAME) Then
                Throw New ArgumentException("value must be of type UserLogin.")
            End If
        End Sub 'OnValidate

    End Class
End Namespace