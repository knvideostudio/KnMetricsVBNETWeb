
Partial Class Secure_include_PickUpCalendar
    Inherits System.Web.UI.Page

    Public strFrmName As String
    Public strControlName As String
    Public strSelCaledarDate As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            PickUpCalendar.SelectedDate = System.DateTime.Now()
            drpCalendarMonth.Items.FindByValue(DateTime.Now.Month).Selected = True
            drpCalendarYear.Items.FindByValue(DateTime.Now.Year).Selected = True
        End If

        strSelCaledarDate = PickUpCalendar.SelectedDate.ToString("MM/dd/yyyy")
        strFrmName = Request.QueryString("strFrmName").ToString()
        strControlName = Request.QueryString("strControlName").ToString()
    End Sub


    Protected Sub PickUpCalendar_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PickUpCalendar.SelectionChanged
        strSelCaledarDate = PickUpCalendar.SelectedDate.ToString("MM/dd/yyyy")
    End Sub


    Protected Sub PickUpMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpCalendarMonth.SelectedIndexChanged
        PickUpCalendar.VisibleDate = New DateTime(CInt(drpCalendarYear.SelectedItem.Value), CInt(drpCalendarMonth.SelectedItem.Value), 1)
    End Sub
End Class
