Imports System.Data

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class AdminPersonalList
        Inherits System.Web.UI.Page

        Public Property SortExpression() As String
            Get
                If Not ViewState("SortExpression") = Nothing Then
                    Return CType(ViewState("SortExpression"), String)
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                If ViewState("SortExpression") = Nothing Then
                    ViewState.Add("SortExpression", value)
                Else
                    ViewState("SortExpression") = value
                End If
            End Set
        End Property

        Public Property SortDirection() As String
            Get
                If Not ViewState("SortDirection") = Nothing Then
                    Return CType(ViewState("SortDirection"), String)
                Else
                    Return "ASC"
                End If
            End Get
            Set(ByVal value As String)
                If ViewState("SortDirection") = Nothing Then
                    ViewState.Add("SortDirection", value)
                Else
                    ViewState("SortDirection") = value
                End If
            End Set
        End Property

        Public Overrides Sub Dispose()
            ' If Not defaultView Is Nothing Then defaultView = Nothing

            MyBase.Dispose()
        End Sub 'Dispose

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            ' load user name
            lblUserName.Text = User.Identity.Name

            If Not Page.IsPostBack Then
                ' defaultView = AdminTasks.GetListPersonel()

                If Session("dvPersonnel") Is Nothing Then
                    Session("dvPersonnel") = AdminTasks.GetListPersonel()
                End If

                grdListPersonnel.DataSource = CType(Session("dvPersonnel"), System.Data.DataView) ' .Table.DefaultView
                grdListPersonnel.DataBind()
            Else
                ' do something else
            End If
        End Sub

        Protected Sub gvStates_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdListPersonnel.RowCreated

            Dim ddl As DropDownList

            If Not Page.IsPostBack Then

                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim dt As New DataTable()
                    Dim dr As DataRow

                    dt.Columns.Add(New DataColumn("PwsId", GetType(String)))
                    dt.Columns.Add(New DataColumn("PwsText", GetType(String)))

                    dr = dt.NewRow()
                    dr(0) = "0"
                    dr(1) = "******"
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr(0) = "1"
                    dr(1) = e.Row.DataItem(5)
                    dt.Rows.Add(dr)

                    Dim dv As New DataView(dt)

                    ddl = CType(e.Row.FindControl("drpPsdShow"), DropDownList)

                    With ddl
                        .DataSource = dv
                        .DataValueField = "PwsId"
                        .DataTextField = "PwsText"
                        .DataBind()
                    End With

                    ' ddl.DataSource = dv
                    'ddl.DataBind()
                    'ddl.DataSource = ((State)e.Row.DataItem).;
                    'ddl.DataBind();
                End If

            End If
        End Sub

        Private Sub BindListPersonnel()
            Dim Records As Int32
            Dim personnelView As System.Data.DataView

            If Session("dvPersonnel") Is Nothing Then
                Session("dvPersonnel") = AdminTasks.GetListPersonel()
            End If

            personnelView = CType(Session("dvPersonnel"), System.Data.DataView)

            If personnelView.Count > 0 Then
                If (Not SortExpression Is Nothing) And (Not SortExpression Is String.Empty) Then
                    personnelView.Sort = SortExpression & " " + SortDirection
                End If

                Records = personnelView.Count

                grdListPersonnel.DataSource = personnelView
                grdListPersonnel.DataBind()
                'lblTotalRecords.Text = "Page: " & (grvReminderDrop.PageIndex + 1).ToString() & _
                '    " of " & grvReminderDrop.PageCount.ToString() & _
                '    "&nbsp;&nbsp;&nbsp;Total Records: " & Records.ToString()
                ' lblTotalPets.Text = MAIL_COUNT_MESSAGEA & iTotalPets.ToString()
            Else
                Records = 0
                'lblTotalPets.Text = ""
                'lblTotalRecords.Text = "Total Records: " & Records.ToString()
                grdListPersonnel.DataSource = Nothing
                grdListPersonnel.DataBind()
            End If
        End Sub

        Protected Sub grdListPersonnel_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles grdListPersonnel.Sorting

            If SortExpression <> e.SortExpression Then
                SortExpression = e.SortExpression
                SortDirection = "ASC"
            Else
                If SortDirection = "ASC" Then
                    SortDirection = "DESC"
                Else
                    SortDirection = "ASC"
                End If
            End If

            BindListPersonnel()
        End Sub

    End Class

End Namespace


