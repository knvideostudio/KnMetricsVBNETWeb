<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="VeterinaryMetrics.BusinessLayer" %>
<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
    Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.BeginRequest
        
        ' Default to English if there are no user languages
        If Not (Request.UserLanguages Is Nothing) Then
            Try
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Request.UserLanguages(0))
            Catch
            End Try
        Else
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-us")
        End If
        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture
    End Sub
    
    Protected Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.AuthenticateRequest
        
        Dim userInformation As String = [String].Empty
        
        If Request.IsAuthenticated = True Then
            
            ' Create the roles cookie if it doesn't exist yet for this session.
            If Request.Cookies(GlobalsVar.UserRoles) Is Nothing OrElse Request.Cookies(GlobalsVar.UserRoles).Value = "" Then

                ' retrieve the user name
                'Dim user As UserLogin = UserLogin.User_GetByUsername(Context.User.Identity.Name)
                Dim user As UserLogin = UserLogin.User_WindowsIdentityName(Context.User.Identity.Name)

                ' Create a string to persist the role and user id
                ' old way
                ' userInformation = user.Id & ";" & user.RoleName & ";" & user.Username
                userInformation = user.UniqueID.ToString() & ";" & user.RoleName & ";" & user.Username
            
                ' Create a cookie authentication ticket.
                Dim ticket As New FormsAuthenticationTicket(1, Context.User.Identity.Name, DateTime.Now, DateTime.Now.AddDays(60), False, userInformation)

                ' Encrypt the ticket
                Dim cookieStr As [String] = FormsAuthentication.Encrypt(ticket)

                ' Send the cookie to the client
                Response.Cookies(GlobalsVar.UserRoles).Value = cookieStr
                Response.Cookies(GlobalsVar.UserRoles).Path = "/"
                Response.Cookies(GlobalsVar.UserRoles).Expires = DateTime.Now.AddDays(60)

                ' Add our own custom principal to the request containing the user's identity, the user id, and
                ' the user's role
                'Context.User = New UserCustomPrincipal(Context.User.Identity, user.Id, user.RoleName, user.Username, user.UniqueID)
                
                ' Using another constructor
                Context.User = New UserCustomPrincipal(Context.User.Identity, user.RoleName, user.Username, user.UniqueID)
            Else
                ' Get roles from roles cookie
                Dim ticket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(Context.Request.Cookies(GlobalsVar.UserRoles).Value)
                userInformation = ticket.UserData

                ' Add our own custom principal to the request containing the user's identity, the user id, and
                ' the user's role from the auth ticket
                Dim info As String() = userInformation.Split(New Char() {";"c})
                '     Context.User = New UserCustomPrincipal(User.Identity, Convert.ToInt32(info(0).ToString()), info(1).ToString(), info(2).ToString(), Convert.ChangeType(info(3), TypeCode.Char))
                
                ' Converting String valuie to GUID type
                Dim g As Guid
                g = System.ComponentModel.TypeDescriptor.GetConverter(g).ConvertFrom(info(0).ToString())
                
                
                'Dim SearchGUID As Guid
                'SearchGUID = GetConverter(SearchGUID).ConvertFrom(SearchID)
                'GetConverter is used with the following Imports Object:
                'Imports System.ComponentModel.TypeDescriptor
                
                'g = Convert.ChangeType(info(3), TypeCode.Object)
                
                ' Context.User = New UserCustomPrincipal(User.Identity, Convert.ToInt32(info(0).ToString()), info(1).ToString(), info(2).ToString(), g)
                
                Context.User = New UserCustomPrincipal(User.Identity, info(1).ToString(), info(2).ToString(), g)

            End If
        End If
    End Sub
    
</script>