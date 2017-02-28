Imports System

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class _Default
        Inherits System.Web.UI.Page


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            Dim loginUserName As String = "No Users are avaiable."

            If Request.IsAuthenticated = True Then
                loginUserName = User.Identity.Name
                lblUserName.Text = loginUserName

            End If



            'Response.Write("Hello, " + Server.HtmlEncode(User.Identity.Name))

            'Dim id As FormsIdentity = User.Identity
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
    End Class
End Namespace