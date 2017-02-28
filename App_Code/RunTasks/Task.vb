Imports Microsoft.VisualBasic

Namespace VeterinaryMetrics.BusinessLayer

    Public MustInherit Class Task

        Protected _lblStep3 As Object
        Protected _status As String
        Protected _taskID As String

        Public Sub New(ByVal taskID As String)
            _taskID = taskID
        End Sub

        Public MustOverride Sub Run()
        Public MustOverride Sub Run(ByVal data As Object)

        Public MustOverride Function RunDtsPakage(ByVal TextFileFolder As String, ByVal sBatch As String) As String

        Public ReadOnly Property lblStep3() As Object
            Get
                Return _lblStep3
            End Get
        End Property
    End Class


    Public Class TaskHelpers
        Public Shared Sub UpdateStatus(ByVal taskID As String, ByVal info As String)
            Dim context As HttpContext = HttpContext.Current
            context.Cache(taskID) = info
        End Sub

        Public Shared Function GetStatus(ByVal taskID As String) As String
            Dim context As HttpContext = HttpContext.Current

            Dim o As Object
            o = context.Cache(taskID)

            If o Is Nothing Then
                Return String.Empty
            End If

            Return DirectCast(o, String)
        End Function

        Public Shared Sub ClearStatus(ByVal taskID As String)
            Dim context As HttpContext = HttpContext.Current
            context.Cache.Remove(taskID)
        End Sub
    End Class


    Public Class ScriptHelpers
        Public Shared Function GetStarterScript(ByVal pageObject As Page, ByVal funcName As String, ByVal ctlName As String) As String
            Dim context As HttpContext = HttpContext.Current

            Dim sb As New StringBuilder
            sb.AppendLine("function GetGuid() {")
            sb.AppendLine("   var ranNum = Math.floor(Math.random()*100000000);")
            sb.AppendLine("   return ranNum;")
            sb.AppendLine("}")

            sb.AppendLine("var taskID;")

            sb.AppendFormat("function {0}()", funcName)
            sb.AppendLine(" {")
            sb.AppendFormat("  var ctl = document.getElementById('{0}');", ctlName)
            sb.AppendLine("")
            sb.AppendLine("  ctl.disabled = true;")
            sb.AppendLine("  var id = GetGuid();")
            sb.AppendLine("  taskID = id; ")
            sb.AppendLine("  ShowProgress();")
            sb.AppendLine(pageObject.ClientScript.GetCallbackEventReference( _
                pageObject, "id", "UpdatePage", "null", False))
            sb.AppendLine("}")
            Return sb.ToString()
        End Function
    End Class
End Namespace