Imports Microsoft.VisualBasic

Namespace VeterinaryMetrics.BusinessLayer
    Public Class UserSecurity

        Public Shared Function GetUserID() As Integer
            Return CType(HttpContext.Current.User, UserCustomPrincipal).UserID
        End Function 'GetUserID

        Public Shared Function GetUserRole() As String
            Return CType(HttpContext.Current.User, UserCustomPrincipal).UserRole
        End Function 'GetUserRole

        Public Shared Function GetName() As String
            Return CType(HttpContext.Current.User, UserCustomPrincipal).Name
        End Function 'GetName

        Public Shared Function GetUserUniqueID() As Guid
            Return CType(HttpContext.Current.User, UserCustomPrincipal).UserUniqueID
        End Function 'GetName

        Public Sub New()
        End Sub 'New
    End Class
End Namespace