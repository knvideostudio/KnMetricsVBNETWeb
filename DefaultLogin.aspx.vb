Imports VeterinaryMetrics.BusinessLayer

Partial Class DefaultLogin
    Inherits System.Web.UI.Page

    ' Page load
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'Request.IsAuthenticated

        'lblTextMessage.Text = Request.IsAuthenticated.ToString()

        'If Request.IsAuthenticated = True And User.IsInRole("Administrator") Then
        '    Response.Redirect("~/Secure/Administrator/ReminderDrop.aspx")
        'End If

        'If Request.IsAuthenticated = True And User.IsInRole("Manager") Then
        '    Response.Redirect("~/Secure/Manager/Default.aspx")
        'End If

        'If Request.IsAuthenticated = True And User.IsInRole("User") Then
        '    Response.Redirect("~/Secure/User/Default.aspx")
        'End If

        If Request.IsAuthenticated = True Then
            Response.Redirect("Default.aspx")

        End If
        'Response.Write("Hello, " + Server.HtmlEncode(User.Identity.Name))

        'Dim id As System.Web.Security. .FormsIdentity = User.Identity
        'Dim ticket As FormsAuthenticationTicket = id.Ticket

        '' FormsIdentity id = (FormsIdentity)User.Identity;
        ''FormsAuthenticationTicket ticket = id.Ticket;

        'Response.Write("<p/>TicketName: " + ticket.Name)
        'Response.Write("<br/>Cookie Path: " + ticket.CookiePath)
        'Response.Write("<br/>Ticket Expiration: " & ticket.Expiration.ToString())
        'Response.Write("<br/>Expired: " + ticket.Expired.ToString())
        'Response.Write("<br/>Persistent: " + ticket.IsPersistent.ToString())
        'Response.Write("<br/>IssueDate: " + ticket.IssueDate.ToString())
        'Response.Write("<br/>UserData: " + ticket.UserData)
        'Response.Write("<br/>Version: " + ticket.Version.ToString())

    End Sub

    'Protected Sub btnLoginButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Page.IsValid Then
    '        If UserLogin.User_Authenticate(txtUsername.Text, txtPassword.Text) Then
    '            FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, chkRemember.Checked)
    '        Else
    '            lblError.Text = "Invalid username or password"
    '        End If
    '    End If

    '    Dim str As String = "0009"

    'End Sub

    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'lblTextMessage.Text = Request.IsAuthenticated.ToString()

        If Page.IsValid Then
            If UserLogin.User_Authenticate(loginTemplate.UserName, loginTemplate.Password) Then
                FormsAuthentication.RedirectFromLoginPage(loginTemplate.UserName.ToString(), loginTemplate.RememberMeSet())
            Else
                loginTemplate.FailureText = "Invalid username or password."
            End If
        End If
    End Sub
End Class
