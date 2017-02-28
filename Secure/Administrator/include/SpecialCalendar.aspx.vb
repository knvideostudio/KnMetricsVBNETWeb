
Imports VeterinaryMetrics.BusinessLayer

' this is the common namespace
' all common functionality will be here

Namespace VeterinaryMetrics.Utility.Common


    Partial Class SpecialCalendar
        Inherits System.Web.UI.Page

        ' Special Calendar
        Private StrJavaScript As String = String.Empty

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            'Put user code to initialize the page here
            Dim selected As String = Request.QueryString("selected")
            Dim id As String = Request.QueryString("id")
            Dim form As String = Request.QueryString("formname")
            Dim strjscript As String = String.Empty

            If Not Page.IsPostBack Then
                Try
                    MyCalendar.SelectedDate = Convert.ToDateTime(selected)
                    MyCalendar.VisibleDate = Convert.ToDateTime(selected)
                Catch
                    MyCalendar.SelectedDate = DateTime.Today
                    MyCalendar.VisibleDate = DateTime.Today
                End Try

                InitializeCalendar()
                SelectTheRightDate()

                strjscript = "<script language=""javascript"">"
                strjscript = strjscript & "window.opener.SetDate('" + form + "','" + id + "', document.forms[0].elements[""mychosendate""].value);window.close();"
                strjscript = strjscript & "</script" & ">"

                StrJavaScript = strjscript
                btnWinClose.Attributes.Add("onClick", "window.opener.SetDate('" + form + "','" + id + "', document.forms[0].elements[""mychosendate""].value);")
            Else
                strjscript = "<script language=""javascript"">"
                strjscript = strjscript & "window.opener.SetDate('" + form + "','" + id + "', document.forms[0].elements[""mychosendate""].value);window.close();"
                strjscript = strjscript & "</script" & ">"
                StrJavaScript = strjscript
            End If
        End Sub

        Private Sub SelectTheRightDate()
            lblMySelectedDate.Text = MyCalendar.SelectedDate.ToShortDateString()
            mychosendate.Value = lblMySelectedDate.Text
            drpListMonth.SelectedIndex = drpListMonth.Items.IndexOf(drpListMonth.Items.FindByValue(MyCalendar.SelectedDate.Month.ToString()))
            drpListYear.SelectedIndex = drpListYear.Items.IndexOf(drpListYear.Items.FindByValue(MyCalendar.SelectedDate.Year.ToString()))
        End Sub 'SelectCorrectValues


        Private Sub InitializeCalendar()
            Dim thisdate As New DateTime(DateTime.Today.Year, 1, 1)

            ' Fills in month values
            Dim x As Integer
            For x = 0 To 11
                ' Loops through 12 months of the year and fills in each month value
                Dim li As New ListItem(thisdate.ToString("MMMM"), thisdate.Month.ToString())
                drpListMonth.Items.Add(li)
                thisdate = thisdate.AddMonths(1)
            Next x

            ' Fills in year values and change y value to other years if necessary
            Dim y As Int32
            Dim i As Int32 = Int32.Parse(GlobalsVar.DefaultYearForCalendar)
            Dim i2 As Int32 = Int32.Parse(GlobalsVar.DefaultEndYearForCalendar)

            For y = i To i2
                drpListYear.Items.Add(y.ToString())
            Next y
        End Sub

        Protected Sub MyCalendar_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyCalendar.SelectionChanged
            MyCalendar.VisibleDate = MyCalendar.SelectedDate
            SelectTheRightDate()
            MyLiteral.Text = StrJavaScript
        End Sub

        Protected Sub drpListMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpListMonth.SelectedIndexChanged
            MyCalendar.VisibleDate = New DateTime(Int32.Parse(drpListYear.SelectedItem.Value), Int32.Parse(drpListMonth.SelectedItem.Value), 1)
            MyCalendar.SelectedDate = MyCalendar.VisibleDate
            SelectTheRightDate()
        End Sub

        Protected Sub drpListYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpListYear.SelectedIndexChanged
            MyCalendar.VisibleDate = New DateTime(Int32.Parse(drpListYear.SelectedItem.Value), Int32.Parse(drpListMonth.SelectedItem.Value), 1)
            MyCalendar.SelectedDate = MyCalendar.VisibleDate
            SelectTheRightDate()
        End Sub

        Protected Sub MyCalendar_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles MyCalendar.DayRender
            If e.Day.Date = DateTime.Now().ToString("d") Then
                e.Cell.BackColor = System.Drawing.Color.LightGray
            End If
        End Sub

    End Class
End Namespace