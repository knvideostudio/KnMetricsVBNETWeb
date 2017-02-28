Imports System.IO
Imports myDtsPackageLibrary
Imports VeterinaryMetrics.BusinessLayer
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class WebDirectMail
        Inherits System.Web.UI.Page

        Private collPracticeWait As DirectMailCollection = Nothing

        Private _connection As SqlConnection
        Private _command As SqlCommand
        Private _reader As SqlDataReader

        Public Function BeginAsyncOperation(ByVal sender As Object, ByVal e As EventArgs, ByVal cb As AsyncCallback, ByVal state As Object) As IAsyncResult

            'string connect = WebConfigurationManager.ConnectionStrings["PubsConnectionString"].ConnectionString;
            Dim sConnect As String = WebConfigurationManager.ConnectionStrings("StrConnectionHomeAgainDBasync").ConnectionString
            Dim newSqlParam As New SqlParameter

            '_connection = new SqlConnection(connect);
            _connection = New SqlConnection(sConnect)

            '_connection.Open();
            _connection.Open()
            _command = New SqlCommand("spWeb_DM_GetCounts", _connection)
            _command.CommandType = CommandType.StoredProcedure

            newSqlParam.ParameterName = "@BATCHID"
            newSqlParam.SqlDbType = SqlDbType.Int
            newSqlParam.Direction = ParameterDirection.Input
            newSqlParam.Value = 5
            _command.Parameters.Add(newSqlParam)
            btnDirectMail.Text = "Please Wait"
            btnDirectMail.Enabled = False

            Return _command.BeginExecuteReader(cb, state)
        End Function

        Public Sub EndAsyncOperation(ByVal ar As IAsyncResult)
            '_reader = _command.EndExecuteReader(ar);
            _reader = _command.EndExecuteReader(ar)
        End Sub

        Private Sub TimeoutAsyncOperation(ByVal ar As IAsyncResult)

        End Sub

        Private Sub Page_PreRenderComplete(ByVal sender As Object, ByVal e As EventArgs) ' Handles MyBase.PreRenderComplete

            dmGridView.DataSource = _reader
            dmGridView.DataBind()

            btnDirectMail.Text = "Run Direct Mail"
            btnDirectMail.Enabled = True
        End Sub 'Page_PreRenderComplete   

        Public Overrides Sub Dispose()
            If Not _connection Is Nothing Then
                _connection.Close()
            End If

            MyBase.Dispose()
        End Sub 'Dispose

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            lblUserName.Text = User.Identity.Name
            Dim i As Integer = 0
            Dim tmpStr As String = String.Empty

            'Dim bool As Boolean = False
            ' bool = Page.IsAsync

            If Not Page.IsPostBack Then
                collPracticeWait = DirectMail.DirectMail_PracticesWait()
                If collPracticeWait.Count > 0 Then
                    For i = 0 To collPracticeWait.Count - 1
                        'PracticeList.Item(i).ProcessedDate
                        tmpStr += (i + 1).ToString() & "&nbsp;" & collPracticeWait.Item(i).PracticeId.ToString() & _
                        "&nbsp;" & collPracticeWait.Item(i).PracticeName & "<br />"
                    Next i

                    lblMessageDspl.Text = tmpStr
                    btnDirectMail.Enabled = True
                Else
                    btnDirectMail.Enabled = False
                    lblMessageDspl.Text = "There are no Practice(s) assign, please assign one or more Practices."
                End If

            End If
        End Sub

        Protected Sub btnDirectMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDirectMail.Click

            AddHandler Me.PreRenderComplete, New EventHandler(AddressOf Page_PreRenderComplete)
            Page.AddOnPreRenderCompleteAsync(New BeginEventHandler(AddressOf BeginAsyncOperation), New EndEventHandler(AddressOf EndAsyncOperation))
        End Sub
    End Class
End Namespace