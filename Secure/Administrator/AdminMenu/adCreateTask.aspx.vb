
Namespace VeterinaryMetrics.BusinessLayer


    Partial Class adCreateTask
        Inherits System.Web.UI.Page

        Private UsrColl As UsrTaskCollection = Nothing
        Private GrpColl As UsrTaskCollection = Nothing
        Private TskColl As UsrTaskCollection = Nothing

        Private TskView As System.Data.DataView = Nothing

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            lblUserName.Text = User.Identity.Name

            If Not Page.IsPostBack Then
                UsrColl = UsrTask.GetUsers()
                GrpColl = UsrTask.GetGroups()
                TskColl = UsrTask.GetTasks()

                If UsrColl.Count > 0 Then

                    With drpUserList
                        .DataSource = UsrColl
                        .DataValueField = "Id"
                        .DataTextField = "Desc"
                        .DataBind()
                        '.SelectedIndex = 0
                    End With
                End If ' Count

                If GrpColl.Count > 0 Then

                    With drpGroupList
                        .DataSource = GrpColl
                        .DataValueField = "Id"
                        .DataTextField = "Desc"
                        .DataBind()
                    End With
                End If ' Count

                If TskColl.Count > 0 Then

                    With dtpTaskList
                        .DataSource = TskColl
                        .DataValueField = "Id"
                        .DataTextField = "Desc"
                        .DataBind()
                    End With
                End If ' Count


            End If 'Page.IsPostBack
        End Sub

        Protected Sub grvTaskData_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs) Handles grvTaskData.RowDeleting

            Dim bDeleted As Boolean = False
            Dim grvEditRow As GridViewRow = grvTaskData.Rows(e.RowIndex)
            Dim i As Int32 = GetGridViewRowOrderID(grvEditRow)

            If i > 0 Then
                bDeleted = UsrTask.DeleteTask(i)
                BuildGridTaskView(drpUserList.SelectedItem.Text)
            End If
        End Sub

        Private Function GetGridViewRowOrderID(ByVal row As GridViewRow) As Int32
            Dim n As Int32 = 0
            Dim _ctl As Label = CType(row.FindControl("lblRowID"), Label)

            If _ctl Is Nothing Then
                Throw New Exception("get")
            End If

            n = Int32.Parse(_ctl.Text)
            Return (n)

        End Function

        Private Sub BuildGridTaskView(ByVal UserName As String)
            TskView = UsrTask.GetTaskByUser(UserName)
            grvTaskData.DataSource = TskView
            grvTaskData.DataBind()
        End Sub


        Public Overrides Sub Dispose()
            If Not UsrColl Is Nothing Then UsrColl = Nothing
            If Not GrpColl Is Nothing Then GrpColl = Nothing
            If Not TskColl Is Nothing Then TskColl = Nothing
            If Not TskView Is Nothing Then TskColl = Nothing

            MyBase.Dispose()
        End Sub

        Protected Sub btnAddNewTask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewTask.Click
            Dim UserInfoArr(5) As String

            lblMessage.Text = String.Empty

            Try
                UserInfoArr(0) = drpUserList.SelectedValue
                UserInfoArr(1) = drpGroupList.SelectedValue
                UserInfoArr(2) = dtpTaskList.SelectedValue
                UserInfoArr(3) = "1"
                UserInfoArr(4) = "n"

                ' create a new task
                UsrTask.AddTaskNew(UserInfoArr)

                ' bring the view
                BuildGridTaskView(drpUserList.SelectedItem.Text)

                ' message
                lblMessage.Text = "New task has been created for user <b>" & drpUserList.SelectedItem.Text & "</b>."
            Catch ex As Exception
                lblMessage.Text = ex.Message

            End Try

        End Sub

        Protected Sub drpUserList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpUserList.SelectedIndexChanged
            Dim sUserName As String = drpUserList.SelectedItem.Text

            lblMessage.Text = "Showing tasks for User <b>" & sUserName & "</b>."
            BuildGridTaskView(sUserName)
        End Sub
    End Class
End Namespace