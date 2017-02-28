Imports Microsoft.VisualBasic

Imports VeterinaryMetrics.AccessLayerData


Namespace VeterinaryMetrics.BusinessLayer

    Public Class UserLogin

        Private _Id As Integer
        Private _Username As String = String.Empty
        Private _RoleName As String
        Private _DisplayName As String = String.Empty

        Private _Password As String = String.Empty
        Private _Email As String = String.Empty
        Private _UniqueID As Guid

        Public Sub New()
        End Sub 'New

        Public Sub New(ByVal username As String)
            _Username = username
        End Sub 'New

        Public Sub New(ByVal UserId As Int32, ByVal UserName As String, ByVal RoleName As String, ByVal DisplayName As String)
            _Id = UserId
            _Username = UserName
            _RoleName = RoleName
            _DisplayName = DisplayName
        End Sub 'New

        Public Sub New(ByVal userId As Integer, _
                        ByVal username As String, _
                        ByVal roleName As String, _
                        ByVal email As String, _
                        ByVal displayName As String, _
                        ByVal password As String, _
                        ByVal uniqueid As Guid)
            _Id = userId
            _Username = username
            _RoleName = roleName
            _Email = email
            _DisplayName = displayName
            _Password = password
            _UniqueID = uniqueid
        End Sub 'New

        Public ReadOnly Property Id() As Integer
            Get
                Return _Id
            End Get
        End Property

        Public ReadOnly Property UniqueID() As Guid
            Get
                Return _UniqueID
            End Get
        End Property


        Public Property Username() As String
            Get
                Return _Username
            End Get
            Set(ByVal value As String)
                _Username = value
            End Set
        End Property

        Public Property DisplayName() As String
            Get
                Return _DisplayName
            End Get
            Set(ByVal value As String)
                _DisplayName = value
            End Set
        End Property

        Public Property RoleName() As String
            Get
                Return _RoleName
            End Get
            Set(ByVal value As String)
                _RoleName = value
            End Set
        End Property

        '-------------------------------------------------------------
        ' User Authenticate
        '-------------------------------------------------------------
        Public Shared Function User_Authenticate(ByVal username As String, ByVal password As String) As Boolean
            If username Is Nothing OrElse username.Length = 0 Then
                Throw New ArgumentOutOfRangeException("username")
            End If
            If password Is Nothing OrElse password.Length = 0 Then
                Throw New ArgumentOutOfRangeException("password")
            End If
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.User_Authenticate(username, password)
        End Function 'User_Authenticate


        Public Shared Function User_GetByUsername(ByVal username As String) As UserLogin
            If username Is Nothing OrElse username.Length = 0 Then
                Throw New ArgumentOutOfRangeException("username")
            End If
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.User_GetByUsername(username)
        End Function 'GetUserByUsername

        ' Using the Windows Login Accounts
        Public Shared Function User_WindowsIdentityName(ByVal sUserName As String) As UserLogin
            If sUserName Is Nothing OrElse sUserName.Length = 0 Then
                Throw New ArgumentOutOfRangeException("sUserName")
            End If
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return DataLayer.User_GetIdentityName(sUserName)
        End Function 'GetUserByUsername


    End Class



End Namespace
