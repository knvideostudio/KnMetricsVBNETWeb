Imports Microsoft.VisualBasic
Imports System
Imports System.Security.Principal

Namespace VeterinaryMetrics.BusinessLayer


    Public Class UserCustomPrincipal
        Implements IPrincipal

        Private _userID As Integer
        Private _userRole As String = String.Empty
        Private _name As String
        Private _userUniqueID As Guid

        ' Required to implement the IPrincipal interface.
        Protected _Identity As IIdentity

        Public Sub New()
        End Sub 'New


        Public Sub New(ByVal identity As IIdentity, _
                            ByVal userRole As String, _
                            ByVal name As String, _
                            ByVal userUniqueID As Guid)
            _Identity = identity
            _userRole = userRole
            _userUniqueID = userUniqueID
        End Sub

        Public Sub New(ByVal identity As IIdentity, _
                            ByVal userID As Integer, _
                            ByVal userRole As String, _
                            ByVal name As String, _
                            ByVal userUniqueID As Guid)
            _Identity = identity
            _userID = userID
            _userRole = userRole
            _userUniqueID = userUniqueID
        End Sub 'New


        Public ReadOnly Property Identity() As System.Security.Principal.IIdentity Implements System.Security.Principal.IPrincipal.Identity
            Get
                Return _Identity
            End Get
        End Property

        Public Property UserUniqueID() As Guid
            Get
                Return _userUniqueID
            End Get
            Set(ByVal value As Guid)
                _userUniqueID = value
            End Set
        End Property

        Public Property UserID() As Integer
            Get
                Return _userID
            End Get
            Set(ByVal value As Integer)
                _userID = value
            End Set
        End Property
        ' The user's role, as defined in UserLogin.

        Public Property UserRole() As String
            Get
                Return _userRole
            End Get
            Set(ByVal value As String)
                _userRole = value
            End Set
        End Property
        ' The user's name (either First and Last name, or their network username)

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property


        Public Function IsInRole(ByVal role As String) As Boolean Implements System.Security.Principal.IPrincipal.IsInRole
            Dim roleArray As String() = role.Split(New Char() {","c})

            Dim r As String
            For Each r In roleArray
                If _userRole = r Then
                    Return True
                End If
            Next r
            Return False
        End Function

    End Class

End Namespace