Imports System.Data

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class ProAwExclusion
        Inherits System.Web.UI.Page

        Private _iDrop As Int32
        Private Const CURRENT_WEB_PAGE As String = "ProAwExclusion.aspx"
        Private dropView As System.Data.DataView = Nothing

        Public Property iDrop() As Int32
            Get
                Dim sDrop As String = String.Empty

                Try
                    sDrop = drpReminderList.SelectedValue()
                    Return (Int32.Parse(sDrop))
                Catch ex As Exception
                    Return (0)
                End Try
            End Get
            Set(ByVal value As Int32)
                _iDrop = value
            End Set
        End Property

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            lblUserName.Text = User.Identity.Name

            If System.IO.File.Exists(GlobalsVar.WS_BUILD_EXCLUSION_LIST) Then
                ' btnPopulate.BackColor = Drawing.Color.DarkGray
                btnPopulate.Enabled = False
                drpReminderList.Enabled = False
                lblMessage.Text = "Please Wait! The Exclusion List is building ..."
            Else
                ' btnPopulate.BackColor = Drawing.Color.DarkBlue
                btnPopulate.Enabled = True
                drpReminderList.Enabled = True

                lblMessage.Text = ""
            End If

            If Not (Page.IsPostBack) Then
                With drpReminderList
                    .DataSource = CreateDataSource(ReminderBlock.GetDrops())
                    .DataValueField = "BlockId"
                    .DataTextField = "BlockText"
                    .DataBind()
                End With

                lblMessage.Text = "Selected Reminder Drop is " & _
                "<b>" & drpReminderList.SelectedItem.Text & "</b>"

                BuildDataView(iDrop)

                If (grdExclusionView.Rows.Count = 0) Then
                    btnPopulate.Enabled = False
                Else
                    btnPopulate.Enabled = True
                End If
            End If ' Page.IsPostBack

        End Sub

        Private Sub BuildDataView(ByVal iDrop As Int32)
            dropView = ReminderBlock.GetRemindersProcess(iDrop)
            grdExclusionView.DataSource = dropView
            grdExclusionView.DataBind()
        End Sub

        Private Function CreateDataSource(ByVal drpColl As ReminderBlockCollection) As ICollection
            Dim dt As New DataTable()
            Dim dr As DataRow
            Dim i As Integer = 0

            dt.Columns.Add(New DataColumn("BlockId", GetType(Int32)))
            dt.Columns.Add(New DataColumn("BlockText", GetType(String)))

            drpColl = ReminderBlock.GetDrops()

            For i = 0 To drpColl.Count - 1
                dr = dt.NewRow()
                dr(0) = drpColl.Item(i).BlockId
                dr(1) = drpColl.Item(i).BlockId.ToString() & " " & drpColl.Item(i).BlockText
                dt.Rows.Add(dr)
            Next i

            Dim dv As New DataView(dt)

            Return (dv)
        End Function


        Public Overrides Sub Dispose()
            If Not dropView Is Nothing Then dropView = Nothing

            MyBase.Dispose()
        End Sub

        Protected Sub drpReminderList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpReminderList.SelectedIndexChanged
            lblMessage.Text = "Selected Reminder Drop is " & _
                 "<b>" & drpReminderList.SelectedItem.Text & "</b>"

            BuildDataView(iDrop)
            If (grdExclusionView.Rows.Count = 0) Then
                btnPopulate.Enabled = False
            Else
                btnPopulate.Enabled = True
            End If
        End Sub

        Protected Sub btnPopulate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPopulate.Click

            ' ================================
            ' = Example of XML file          =
            ' ================================
            ' <Root>
            '   <Pro dr="234" pr="600001" />
            '   <Pro dr="234" pr="600002" />
            '   <Pro dr="234" pr="600003" />
            '   <Pro dr="234" pr="600004" />
            '   <Pro dr="234" pr="600005" />
            ' </Root>

            Dim sResult As String = String.Empty
            Dim myArr() As String = Nothing
            Dim sXML As String = "<Root>" & vbNewLine
            Dim i As Integer = 0
            Dim strDropId As String = iDrop.ToString()

            sResult = hdfReadyToPost.Value.Replace("on;", "")
            ' txtReadyToPost.Text()

            If sResult.Length > 2 Then
                myArr = sResult.Split(";")

                For i = 0 To myArr.Length - 1
                    sXML += "<Pro dr=""" & strDropId & """ pr=""" & myArr(i) & """ />" & vbNewLine
                Next i
                sXML += "</Root>"

                ' Reminder Block
                ReminderBlock.BuildRemindersExclusionList(sXML)

                ' Exec the Sp_Procedure
                GlobalsVar.CreateEmptyBinaryFile(GlobalsVar.WS_BUILD_EXCLUSION_LIST)
                System.Threading.Thread.Sleep(1000)

            End If

        End Sub

        Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender

            If System.IO.File.Exists(GlobalsVar.WS_BUILD_EXCLUSION_LIST) Then
                btnPopulate.Enabled = False
                drpReminderList.Enabled = False
                Response.AddHeader("REFRESH", "60;URL=" & CURRENT_WEB_PAGE)
            End If

        End Sub
    End Class

End Namespace

