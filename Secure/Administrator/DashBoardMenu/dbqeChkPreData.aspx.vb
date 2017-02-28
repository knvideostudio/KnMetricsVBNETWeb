Imports System.Threading

Namespace VeterinaryMetrics.BusinessLayer

    Partial Class CheckPracticeData
        Inherits System.Web.UI.Page

        Private mView1 As System.Data.DataView = Nothing
        Private mView2 As System.Data.DataView = Nothing
        Private mView3 As System.Data.DataView = Nothing
        Private mView4 As System.Data.DataView = Nothing
        Private mView5 As System.Data.DataView = Nothing
        Private mView6 As System.Data.DataView = Nothing
        Private mView7 As System.Data.DataView = Nothing
        Private mView8 As System.Data.DataView = Nothing
        Private mView9 As System.Data.DataView = Nothing

        Private PeacticeColl As CnvtPracticeCollection = Nothing
        Private _iPractice As Int32

        ' Delegates
        Delegate Sub dgeBindDropDownList(ByVal drp As DropDownList)
        Delegate Sub dgeBindGridView(ByVal grv As GridView, ByVal dv As System.Data.DataView)

        ' Events
        Protected Event BindDropDownList_Event As dgeBindDropDownList
        Protected Event BindGridView_Event As dgeBindGridView


        Public Property iPractice() As Int32
            Get
                Return (_iPractice)
            End Get
            Set(ByVal value As Int32)
                _iPractice = value
            End Set
        End Property

        Public Overrides Sub Dispose()
            'If Not mView Is Nothing Then mView = Nothing
            If Not PeacticeColl Is Nothing Then PeacticeColl = Nothing

            MyBase.Dispose()
        End Sub

        Protected Sub BindGridView(ByVal grv As GridView, ByVal dv As System.Data.DataView) Handles Me.BindGridView_Event

            grv.DataSource = dv
            grv.EmptyDataText = "No Records found ..."
            grv.DataBind()
        End Sub

        Protected Sub BindDropDownList(ByVal drp As DropDownList) Handles Me.BindDropDownList_Event

            ' PeacticeColl = CnvtPractice.GetPreNotConverted()
            PeacticeColl = AdminTasks.GetPractices()
            If PeacticeColl.Count > 0 Then
                With drp
                    .DataSource = PeacticeColl
                    .DataValueField = "PracticeId"
                    .DataTextField = "PracticeText"
                    .DataBind()
                End With
            End If
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            ' load user name
            lblUserName.Text = User.Identity.Name

            If Not Page.IsPostBack Then

                lblPracticeSel.Text = ""

                ' Load Practice Info
                BindDropDownList(drpPracticeInfo)


                ' Display Status

                'lblDisplayStatus.Text = DashBoard.DisplayProcessStatus()

                ' mView = DashBoard.CurrentProcessView()

                ' grvCurrentProcess.DataSource = mView
                ' grvCurrentProcess.DataBind()
            Else

            End If
        End Sub

        ' practice Info
        Protected Sub Bind_PracticeInfo()
            mView1 = DashBoard.DisplayMultiPurposeView(1, iPractice)
            BindGridView(grvChkPre_Info, mView1)
            If Not mView1 Is Nothing Then mView1 = Nothing
        End Sub

        Protected Sub Bind_Schedule()
            mView2 = DashBoard.DisplayMultiPurposeView(2, iPractice)
            BindGridView(grvChkPre_Schedule, mView2)
            If Not mView2 Is Nothing Then mView2 = Nothing
        End Sub

        Protected Sub Bind_Extraction()
            mView3 = DashBoard.DisplayMultiPurposeView(3, iPractice)
            BindGridView(grvChkPre_Extraction, mView3)
            If Not mView3 Is Nothing Then mView3 = Nothing
        End Sub

        Protected Sub Bind_Clients()
            ' Clients
            mView4 = DashBoard.DisplayMultiPurposeView(4, iPractice)
            BindGridView(grvChkPre_Clients, mView4)
            If Not mView4 Is Nothing Then mView4 = Nothing
        End Sub

        Protected Sub Bind_Patients()
            ' Patients
            mView5 = DashBoard.DisplayMultiPurposeView(5, iPractice)
            BindGridView(grvChkPre_Patients, mView5)
            If Not mView5 Is Nothing Then mView5 = Nothing
        End Sub

        Protected Sub Bind_RawReminders()
            ' RawReminders
            mView6 = DashBoard.DisplayMultiPurposeView(6, iPractice)
            BindGridView(grvChkPre_RawReminders, mView6)
            If Not mView6 Is Nothing Then mView6 = Nothing
        End Sub

        Protected Sub Bind_History()
            ' History
            mView7 = DashBoard.DisplayMultiPurposeView(7, iPractice)
            BindGridView(grvChkPre_History, mView7)
            If Not mView7 Is Nothing Then mView7 = Nothing
        End Sub

        Protected Sub Bind_HistoryDate()
            ' Practice History Date
            mView8 = DashBoard.DisplayMultiPurposeView(8, iPractice)
            BindGridView(grvChkPre_HistoryDate, mView8)
            If Not mView8 Is Nothing Then mView8 = Nothing
        End Sub

        Protected Sub Bind_HistoryMonth()
            mView9 = DashBoard.DisplayMultiPurposeView(9, iPractice)
            BindGridView(grvChkPre_HistoryMonth, mView9)
            If Not mView9 Is Nothing Then mView9 = Nothing
        End Sub

        Protected Sub drpPracticeInfo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpPracticeInfo.SelectedIndexChanged
            Dim PracticeSel As String = drpPracticeInfo.SelectedValue.ToString()
            Dim sPracticeNameSel As String = drpPracticeInfo.SelectedItem.Text

            ' TimeSpan()
            '  Dim iPractice As Int32 = 0

            lblPracticeSel.Text = sPracticeNameSel & ": " & PracticeSel

            Try
                iPractice = Int32.Parse(PracticeSel)

            Catch ex As Exception
                Exit Sub
            End Try

            ' Dim trePracticeInfo As New Thread(New ThreadStart(AddressOf Bind_PracticeInfo))
            Call Bind_PracticeInfo()

            ' Dim treSchedule As New Thread(New ThreadStart(AddressOf Bind_Schedule))
            Call Bind_Schedule()

            ' Dim treExtraction As New Thread(New ThreadStart(AddressOf Bind_Extraction))
            Call Bind_Extraction()

            ' Dim treClients As New Thread(New ThreadStart(AddressOf Bind_Clients))
            Call Bind_Clients()

            ' Dim trePatients As New Thread(New ThreadStart(AddressOf Bind_Patients))
            Call Bind_Patients()

            ' Dim treRawReminders As New Thread(New ThreadStart(AddressOf Bind_RawReminders))
            Call Bind_RawReminders()

            ' Dim treHistory As New Thread(New ThreadStart(AddressOf Bind_History))
            Call Bind_History()

            ' Dim treHistoryDate As New Thread(New ThreadStart(AddressOf Bind_HistoryDate))
            Call Bind_HistoryDate()

            ' Dim treHistoryMonth As New Thread(New ThreadStart(AddressOf Bind_HistoryMonth))
            Call Bind_HistoryMonth()

            'trePracticeInfo.Start()
            'treSchedule.Start()
            'treExtraction.Start()
            'treClients.Start()
            'trePatients.Start()
            'treRawReminders.Start()
            'treHistory.Start()
            'treHistoryDate.Start()
            'treHistoryMonth.Start()



        End Sub
    End Class
End Namespace
