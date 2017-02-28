Namespace VeterinaryMetrics.BusinessLayer

    Public Class MyGridViewFunction
        Inherits System.Web.UI.Page

        Public Sub New()
        End Sub

        Public Function RowCheckBoxValue(ByVal row As System.Web.UI.WebControls.GridViewRow, ByVal sControlName As String) As Boolean
            Dim sFieldValue As Boolean
            Dim _ctl As CheckBox

            _ctl = CType(row.FindControl(sControlName), CheckBox)

            If _ctl Is Nothing Then
                Throw New Exception("RoCheckBoxValue: could not find " & sControlName & " control!")
            End If

            If _ctl.Checked = True Then
                sFieldValue = True
            Else
                sFieldValue = False
            End If

            Return sFieldValue
        End Function

        Public Function RowTextValue(ByVal row As System.Web.UI.WebControls.GridViewRow, ByVal sControlName As String) As String
            Dim sFieldValue As String = String.Empty

            Dim _Ctl As TextBox
            _Ctl = CType(row.FindControl(sControlName), TextBox)

            If _Ctl Is Nothing Then
                Throw New Exception("RowTextValue: could not find " + sControlName + " control!")
            End If

            sFieldValue = _Ctl.Text.Trim()

            Return sFieldValue
        End Function

        Public Function RowLabelValue(ByVal row As System.Web.UI.WebControls.GridViewRow, ByVal sControlName As String) As String
            Dim sFieldValue As String = String.Empty
            Dim _ctl As Label

            ' convert control to ctype
            _ctl = CType(row.FindControl(sControlName), Label)

            If _ctl Is Nothing Then
                Throw New Exception("RowLabelValue: could not find " & sControlName & " control!")
            End If

            sFieldValue = _ctl.Text
            Return sFieldValue

        End Function

        Public Function RowDropDownListValue(ByVal row As System.Web.UI.WebControls.GridViewRow, ByVal sControlName As String) As String
            Dim sFieldValue As String = String.Empty
            Dim _ctl As DropDownList

            ' convert control to ctype
            _ctl = CType(row.FindControl(sControlName), DropDownList)

            If _ctl Is Nothing Then
                Throw New Exception("RowDropDownListValue: could not find " & sControlName & " control!")
            End If

            sFieldValue = _ctl.SelectedValue
            Return sFieldValue

        End Function

        Public Function PrimaryTableId(ByVal row As System.Web.UI.WebControls.GridViewRow, ByVal sControlName As String) As Int32
            Dim id As Int32 = 0
            Dim _ctl As Label = CType(row.FindControl(sControlName), Label)

            If _ctl Is Nothing Then
                Throw New Exception(sControlName & "lblDropID: could not find OrderID control!")
            End If

            id = Convert.ToInt32(_ctl.Text)
            Return id
        End Function

        Public Function MyFormatDateTime(ByVal sDateTime As String) As String
            Dim sTemp As String = String.Empty
            Dim sHour As String = String.Empty
            Dim sMinutes As String = String.Empty

            'check for Date
            If IsDate(sDateTime) = True Then
                Dim dt As System.DateTime = System.DateTime.Parse(sDateTime)

                If sDateTime.Length < 12 Then
                    sTemp = dt.ToString("MM/dd/yyyy")
                Else

                    If sDateTime.ToString().LastIndexOfAny("AM") Then
                        sTemp = sDateTime.ToString().Remove(sDateTime.ToString().Length - 2)
                    ElseIf sDateTime.ToString().LastIndexOfAny("PM") Then
                        sTemp = sDateTime.ToString().Remove(sDateTime.ToString().Length - 2)
                    End If

                    sHour = dt.Hour.ToString()
                    If sHour.Length = 1 Then sHour = "0" & sHour

                    sMinutes = dt.Minute.ToString()
                    If sMinutes.Length = 1 Then sMinutes = "0" & sMinutes

                    sTemp = dt.ToString("MM/dd/yyyy") & " " & sHour & ":" & sMinutes
                End If
            Else
                sTemp = String.Empty
            End If

            Return sTemp
        End Function
    End Class
End Namespace