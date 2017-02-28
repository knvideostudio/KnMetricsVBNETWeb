
Namespace VeterinaryMetrics.BusinessLayer

    Partial Class CurrentProcess
        Inherits System.Web.UI.Page

        Private mView As System.Data.DataView = Nothing
        Private m3View As System.Data.DataView = Nothing
        Private MessageArr() As String

        Public Overrides Sub Dispose()
            If Not mView Is Nothing Then mView = Nothing
            If Not m3View Is Nothing Then m3View = Nothing

            MyBase.Dispose()
        End Sub

        Private Function GetStatusMessage(ByVal obj As Object) As String
            Dim i As Integer = -1
            Dim sReturn As String = "Cannot find any status."

            Try
                i = CType(obj, Integer)
            Catch ex As Exception

            End Try

            Select Case i
                Case 0
                    sReturn = "Extract to Production running ..."
                Case 1
                    sReturn = "Waiting to process upload ..."
                Case 2
                    sReturn = "Processing upload ..."
                Case 3
                    sReturn = "Processing on hold ..."
                Case Else
            End Select

            Return (sReturn)
        End Function

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim sTextMessage As String = ""

            ' load user name
            lblUserName.Text = User.Identity.Name

            If Not Page.IsPostBack Then
                ' Display Status


                MessageArr = DashBoard.DisplayProcessStatus()
                sTextMessage = GetStatusMessage(MessageArr(0))
                sTextMessage += "<br /> " & MessageArr(1)

                ' Showing the Message
                lblDisplayStatus.Text = sTextMessage

                ' first GridView
                mView = DashBoard.CurrentProcessView()
                grvCurrentProcess.DataSource = mView
                grvCurrentProcess.DataBind()

                m3View = DashBoard.CurrentProcess3View()
                grvCurrent3Process.DataSource = m3View
                grvCurrent3Process.DataBind()
            End If
        End Sub

    End Class
End Namespace
