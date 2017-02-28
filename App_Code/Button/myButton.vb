Imports System
Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic

Namespace VeterinaryMetrics.BusinessLayer.mButton

    ' Delete Button
    Public Class DeleteButtonField
        Inherits ButtonField

        Private _confirmText As String = "Delete this record?"

        Public Property ConfirmText() As String
            Get
                Return _confirmText
            End Get
            Set(ByVal value As String)
                _confirmText = value
            End Set
        End Property

        Public Sub New()
            Me.CommandName = "Delete"
            Me.Text = "Delete"
        End Sub

        Public Overrides Sub InitializeCell(ByVal cell As System.Web.UI.WebControls.DataControlFieldCell, ByVal cellType As System.Web.UI.WebControls.DataControlCellType, ByVal rowState As System.Web.UI.WebControls.DataControlRowState, ByVal rowIndex As Integer)
            MyBase.InitializeCell(cell, cellType, rowState, rowIndex)

            If cellType = DataControlCellType.DataCell Then
                Dim button As WebControl = CType(cell.Controls(0), WebControl)
                button.Attributes("onclick") = String.Format("return confirm('{0}');", _confirmText)
            End If
        End Sub
    End Class

    ' Long text field
    Public Class LongTextField
        Inherits BoundField

        Private _width As Unit = New Unit("250px")
        Private _heigth As Unit = New Unit("50px")
        Private _id As String = String.Empty

        Public Property ID() As String
            Get
                Return _id
            End Get
            Set(ByVal value As String)
                _id = value
            End Set
        End Property

        Public Property Width() As Unit
            Get
                Return _width
            End Get
            Set(ByVal value As Unit)
                _width = value
            End Set
        End Property

        Public Property Heigth() As Unit
            Get
                Return _heigth
            End Get
            Set(ByVal value As Unit)
                _heigth = value
            End Set
        End Property

        Protected Overrides Sub InitializeDataCell(ByVal cell As System.Web.UI.WebControls.DataControlFieldCell, ByVal rowState As System.Web.UI.WebControls.DataControlRowState)

            ' if not editing, show as scrolling div
            If (rowState And DataControlRowState.Edit) = 0 Then
                Dim div As HtmlGenericControl = New HtmlGenericControl("div")
                div.Attributes("class") = "longTextField"
                div.Style(HtmlTextWriterStyle.Width) = _width.ToString()
                div.Style(HtmlTextWriterStyle.Height) = _heigth.ToString()
                div.Style(HtmlTextWriterStyle.Overflow) = "auto"

                ' add my id
                div.ID = _id

                AddHandler div.DataBinding, AddressOf div_DataBinding
                cell.Controls.Add(div)
            Else
                Dim txtEdit As TextBox = New TextBox()
                txtEdit.TextMode = TextBoxMode.MultiLine
                txtEdit.Width = _width
                txtEdit.Height = _heigth
                txtEdit.ID = _id

                AddHandler txtEdit.DataBinding, AddressOf txtEdit_DataBinding
            End If
        End Sub

        Private Sub div_DataBinding(ByVal s As Object, ByVal e As EventArgs)
            Dim div As HtmlGenericControl = CType(s, HtmlGenericControl)

            ' get the field value
            Dim value As Object = Me.GetValue(div.NamingContainer)

            ' assign formatted value
            div.InnerText = Me.FormatDataValue(value, Me.HtmlEncode)
        End Sub

        Private Sub txtEdit_DataBinding(ByVal s As Object, ByVal e As EventArgs)
            Dim txtEdit As TextBox = CType(s, TextBox)

            ' get the field value
            Dim value As Object = Me.GetValue(txtEdit.NamingContainer)

            ' assign formatted value
            txtEdit.Text = Me.FormatDataValue(value, Me.HtmlEncode)
        End Sub

    End Class


End Namespace