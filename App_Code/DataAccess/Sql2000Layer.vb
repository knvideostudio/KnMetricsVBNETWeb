Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Collections
Imports System.Collections.Generic

Imports VeterinaryMetrics.BusinessLayer

Namespace VeterinaryMetrics.AccessLayerData

    Public Class Sql2000Layer
        Inherits AccessDataMainClass


        Private Const SP_ADDNEW_REMINDERDROP As String = "spAddNew_ReminderDrop"
        Private Const SP_GETALLRECORDS_REMINDERSDROP As String = "spI2_ReminderDrop_GetAll" '"spGetAll_ReminderDrop"
        Private Const SP_GET_ONE_REMINDERDROP As String = "spGetOne_ReminderDrop"
        Private Const SP_GET_PRACTICESBY_DROPID As String = "spGetPracticesByDropId"

        Private Const SP_GET_MATRIX_DROP As String = "spGetMatrixReminderDrop"
        Private Const SP_DELETE_ONEMATRIX As String = "spDeletePositionMatrix"
        Private Const SP_UPDATE_MATRIX As String = "spUpdate_PositionMatrix"
        Private Const SP_INSERT_ONEMATRIX As String = "spInsert_PositionMatrix"
        Private Const SP_ADD_MATRIX_PRACTICE As String = "spAddOne_PositionMatrix"

        ' Position Matrix Procedures
        Private Const SP_DELETE_GROUPMATRIX As String = "spDeleteGroupPstnMatrix"

        ' Private Const SP_DEL_POSITIONMATRIX As String = "spWB_DeletePM"
        Private Const SP_DEL_TARGETMATRIX_DN As String = "spWB_DeleteTC_DN"
        Private Const SP_DEL_TARGETMATRIX_HW As String = "spWB_DeleteTC_HW"
        Private Const SP_DEL_TARGETMATRIX_SR As String = "spWB_DeleteTC_SR"
        ' new
        Private Const SP_ADDHISTORY_PM As String = "spWB_PositionMetrix_ADDTOHISTORY"
        ' End Position Matrix

        Private Const SP_USER_AUTHENTICATE As String = "spUser_Authenticate"
        Private Const SP_USER_GETBYUSERNAME As String = "spUser_GetByUsername"
        Private Const SP_UPDATE_REMINDERDROP As String = "spUpdate_ReminderDrop"

        ' Activation Mail
        Private Const SP_STATUS_ACTIVATIONMAIL As String = "spActivationMail_HA_Status" ' OLD spHABuildActivationStatus
        Private Const SP_BUILD_ACTIVATIONMAIL As String = "spActivationMail_HA_BuildMailing" ' OLD spHABuildActivationMailing
        Private Const SP_APPENDTOHISTORY_ACTIVATIONMAIL As String = "spActivationMail_HA_AppendToHistory" ' OLD spHAAppendToActivationHistory
        Private Const SP_ALREADYSEND_ACTIVATIONMAIL As String = "spActivationMail_HA_AlreadySend" 'OLD spWeb_AM_GetAlreadySend
        Private Const SP_ACTMAIL_ADDFILE As String = "spActivationMail_HA_AddFile"

        Private Const SP_ACTMAIL_GETBATCH As String = "spActivationMail_HA_GetBatchId"
        Private Const SP_ACTMAIL_SETPRINTDATE As String = "spActivationMail_HA_SetPrintDate"
        Private Const SP_ACTMAIL_CLEARPNTDT As String = "spActivationMail_HA_ClearPrintDate"
        Private Const SP_ACTMAIL_SET_HISTORY As String = "spActivationMail_HA_UploadFile"

        ' Direct Mail
        Private Const SP_DIRECTMAIL_ALREADYSEND As String = "spWeb_DM_GetSendPractices"
        Private Const SP_DIRECTMAIL_GETPR As String = "spWeb_DM_GetPractices"
        Private Const SP_DIRECTMAIL_ASSIGNPR As String = "spWeb_DM_AssignPractices"
        Private Const SP_DIRECTMAIL_WAITPR As String = "spWeb_DM_WaitingPractices"
        Private Const SP_DIRECTMAIL_RUN As String = "spWeb_DM_GetCounts"

        ' Reminder Mapping
        Private Const SP_REMINDERMAP_RIGHTSIDE As String = "spMap_DescriptionRD"
        Private Const SP_REMINDERMAP_UNMAPED As String = "spMap_UnmappedServiceCodeRD"
        Private Const SP_REMINDERMAP_GETPRACTICES As String = "spMap_GetPractices"
        Private Const SP_REMINDERMAP_GETMAPPED As String = "spMap_MappedServiceCodeRD"
        ' Reminder Mapping

        ' Pet ID Project Oct 01, 2007
        Private Const SP_PETID_GETCLIENTS As String = "spPetID_Get_All_Clients"
        Private Const SP_PETID_REORDER_ADD As String = "spPetID_Reorder_Request"
        Private Const SP_PETID_REORDER_PENDING As String = "spPetID_ReorderRequest_Pending"
        ' end pet id

        ' new login feature
        Private Const SP_SECURITY_ADDUSER As String = "spAddUser"
        Private Const SP_SECURITY_READUSER As String = "spReadUser"
        ' end new login feature

        ' CRM Functionality
        Private Const SP_GETONE_PRACTICEIFO As String = "spWB_CRM_GetPracticeOne"
        Private Const SP_GET_PIMS As String = "spWB_GetPmSystems"
        Private Const SP_GET_STATESUSA As String = "spWB_GetStatesUS"
        Private Const SP_GET_REGIONS As String = "spWB_GetUsRegions"
        Private Const FN_GET_PRACTICEGROUP As String = "Select GroupID, GroupName FROM dbo.fn_GetPracticeGroups()"
        Private Const SP_GET_HISTORYGROUP As String = "spWB_GetHistoryStartGroup"
        Private Const SP_GET_REMINDRBLOCK As String = "spWB_GetReminderBlock"
        ' end CRM Functionality

        ' Dasboard Function
        Private Const SP_DBDROPISSUE As String = "spi2DropIssue"
        Private Const SP_DBDROP_PPR As String = "spI2dPPR"

        ' Admin Menu
        Private Const SP_ADLISTPERSONNEL As String = "spI2ListPersbyLast"
        Private Const SP_ADMIN_CHG_PRACTICES As String = "spWB_ChgGetPractices"
        Private Const SP_ADMIN_CHG_CONVERTED_PRACTICES As String = "spWB_ChgGetConvertedPractices"

        'Client Class
        Private Const SP_WEBGET_PRACTICES As String = "spWB_GetPractices"
        Private Const SP_ADM_CLIENTCLASS As String = "spI2ClientClassRead"
        Private Const SP_ADMUPDATE_CLIENTCLASS As String = "spI2ClientClassUpdate"
        Private Const SP_ADMUPDATE_CLIENTCLASS_2 As String = "spI2ClientClassUpdate2"

        ' Windows Login
        Private Const SP_GET_USR_IDENTITY As String = "spWB_GetUserIdentityName"

        ' Reports
        ' Current Processing 
        Private Const SP_CURRENT_PROCESS As String = "spI2CurrentProcessing"
        Private Const SP_CURRENT_PROCESSHISTORY As String = "spI2CurrentProcessingHistory"
        ' Report Queue
        Private Const SP_REPORT_QUEUE As String = "spI2ReportQueue"
        ' Report History 
        Private Const SP_REPORTHISTORY_QUEUE As String = "spI2ReportHistoryQueue"
        Private Const SP_REPORT_ERROR As String = "spI2DisplayErrors"

        ' begins here
        Private Const SP_CURRENT_PROCESS2 As String = "spi2CurrentProcessing2"
        Private Const SP_PRACTICE_INFO As String = "spi2CheckPractice1"
        Private Const SP_PRACTICE_SCHEDULE As String = "spi2CheckPractice2"
        Private Const SP_PRACTICE_EXTRACTION As String = "spi2CheckPractice3"
        Private Const SP_PRACTICE_CLIENTS As String = "spi2CheckPractice4"
        Private Const SP_PRACTICE_PATIENTS As String = "spi2CheckPractice5"
        Private Const SP_PRACTICE_REMINDERS As String = "spi2CheckPractice6"
        Private Const SP_PRACTICE_HISTORY As String = "spi2CheckPractice7"
        Private Const SP_PRACTICE_HISTORY_DATE As String = "spi2CheckPractice8"
        Private Const SP_PRACTICE_HISTORY_MONTH As String = "spi2CheckPractice9"

        Private Const SP_CURRENT_PROCESS3 As String = "spi2CurrentProcessing3"

        ' Mailer Id 
        Private Const SP_MAILER_DATA As String = "spWB_GetMailer"

        ' Adding tasks Feb 18, 2009 2:37
        Private Const SP_GROUP_TASK As String = "spWbGetGroups_List"
        Private Const SP_USER_TASK As String = "spWbGetUsers_List"
        Private Const SP_TASK_TASK As String = "spWbGetTasks_List"
        Private Const SP_TASK_BY_USER As String = "spWbGetTasks_View"
        Private Const SP_ADDTASK As String = "spWbAdd_Task"
        Private Const SP_DELTASK As String = "spWbDelete_Task"

        ' Reminder Block
        Private Const SP_GET_REMINDERSDROP As String = "spWB_GetRemindersDrop"
        Private Const SP_GET_REMINDER_PROCESS As String = "spWB_GetRemindersProcess"
        Private Const SP_EXCLUSION_LIST_AW As String = "spi2_BuildExclusionList"

        ' Reports
        Private Const SP_REPORTS_PRACTICES As String = "spI2_GetPracticesFeeAnalysis"
        Private Const SP_REPORTS_FEE_DATES As String = "spI2_GetDatesFeeAnalysis"
        Private Const SP_REPORTS_FEE_DATES_SHOW As String = "spI2_ShowFeeAnalysisSummary"

        Public Overrides Function GetReportsPractice() As CnvtPracticeCollection
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.Text, SP_REPORTS_PRACTICES)
            Dim myBlock As New GenerateCollectionFromReader(AddressOf ReturnChgPractice)

            Dim mColl As CnvtPracticeCollection = CType(ExecuteReaderCmd(cmd, myBlock, 2), CnvtPracticeCollection)

            Return (mColl)
        End Function

        Public Overrides Function GetReportsDateView(ByVal iPractice As Int32) As DataView
            Dim conn As New SqlConnection(ConnectionString)
            Dim command As New SqlCommand(SP_REPORTS_FEE_DATES_SHOW, conn)

            command.CommandType = CommandType.StoredProcedure
            AddParameterToSQLCommand(command, "@PracticeID", SqlDbType.Int, 8, ParameterDirection.Input, iPractice)

            Dim dad As New SqlDataAdapter(command)
            Dim dtPer As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtPer)
            Catch ex As Exception
                ' Catch Error
                Dim dv As New DataView()
                Return (dv)
            Finally
                If Not conn Is Nothing Then
                    conn.Close()
                    conn.Dispose()
                End If

                If Not dad Is Nothing Then dad.Dispose()
                If Not dad Is Nothing Then dtPer.Dispose()
            End Try

            Return (dtPer.DefaultView)
        End Function

        Public Overrides Function GetReportsDate() As String()
            Dim Arr(1) As String

            Dim objConn As SqlConnection = Nothing
            Dim objRd As SqlDataReader = Nothing
            Dim objCmd As SqlCommand = Nothing

            Dim sReturn As String = "Error. Please contact System Admin. have a great day!"

            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If

            Try
                objConn = New SqlConnection(ConnectionString)
                objCmd = New SqlCommand(SP_REPORTS_FEE_DATES, objConn)
                objCmd.CommandType = CommandType.StoredProcedure

                objCmd.Connection.Open()
                objRd = objCmd.ExecuteReader()

                If objRd.HasRows Then
                    Do While objRd.Read()
                        Arr(0) = objRd.GetSqlValue(0).ToString()
                        Arr(1) = objRd.GetSqlValue(1).ToString()
                    Loop ' Do While
                End If ' HasRows


            Catch ex As Exception
                Arr(0) = sReturn
                Arr(1) = ex.Message
            Finally
                If Not objRd Is Nothing Then
                    objRd.Close()
                    objRd = Nothing
                End If

                If Not objCmd.Connection Is Nothing Then
                    objCmd.Connection.Close()
                    objCmd.Connection.Dispose()
                End If
            End Try

            Return (Arr)
        End Function

        Public Overrides Function BuildRemindersExclusionList(ByVal sXML As String) As Boolean
            Dim Cmd As SqlCommand = Nothing
            Dim bResult As Boolean = False

            Try
                Cmd = New SqlCommand()
                AddParameterToSQLCommand(Cmd, "@XML", SqlDbType.NText, 0, ParameterDirection.Input, sXML)
                SetCommandType(Cmd, CommandType.StoredProcedure, SP_EXCLUSION_LIST_AW)

                ExecuteNonQuery(Cmd, 2)
                bResult = True
            Catch ex As Exception
                bResult = False
            End Try


            Return (bResult)
        End Function

        Public Overrides Function GetRemindersProcess(ByVal iDropId As Int32) As DataView
            Dim conn As New SqlConnection(ConnectionString)
            Dim command As New SqlCommand(SP_GET_REMINDER_PROCESS, conn)

            command.CommandType = CommandType.StoredProcedure
            AddParameterToSQLCommand(command, "@DROP_ID", SqlDbType.Int, 8, ParameterDirection.Input, iDropId)

            Dim dad As New SqlDataAdapter(command)
            Dim dtPer As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtPer)
            Catch ex As Exception
                ' Catch Error
                Dim dv As New DataView()
                Return (dv)
            Finally
                conn.Close()
                dad.Dispose()
                dtPer.Dispose()
            End Try

            Return (dtPer.DefaultView)
        End Function


        Public Overrides Function GetRemindersDrop() As ReminderBlockCollection
            Dim cmd As New SqlCommand

            SetCommandType(cmd, CommandType.Text, SP_GET_REMINDERSDROP)

            Dim myDrop As New GenerateCollectionFromReader(AddressOf ReturnReminderDrop)
            Dim ReminderCollection As ReminderBlockCollection = CType(ExecuteReaderCmd(cmd, myDrop, 2), ReminderBlockCollection)

            Return (ReminderCollection)
        End Function

        Public Overrides Function DeleteTask(ByVal iTask As Int32) As Boolean

            ' Execute SQL Command
            Dim myCommand As New SqlCommand

            AddParameterToSQLCommand(myCommand, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParameterToSQLCommand(myCommand, "@TASK_ID", SqlDbType.Int, 8, ParameterDirection.Input, iTask)

            SetCommandType(myCommand, CommandType.StoredProcedure, SP_DELTASK)
            ExecuteScalarCommand(myCommand, 5)

            Dim returnValue As Integer = CInt(myCommand.Parameters("@ReturnValue").Value)
            Return (IIf(returnValue = 0, True, False))
        End Function

        Public Overrides Function AddTaskNew(ByVal UserInfo() As String) As Boolean

            Dim Cmd As New SqlCommand

            Dim iUser As Int32
            Dim iGroup As Int32
            Dim iTask As Int32

            iUser = Int32.Parse(UserInfo(0))
            iGroup = Int32.Parse(UserInfo(1))
            iTask = Int32.Parse(UserInfo(2))

            AddParameterToSQLCommand(Cmd, "@USER_ID", SqlDbType.Int, 8, ParameterDirection.Input, iUser)
            AddParameterToSQLCommand(Cmd, "@GROUP_ID", SqlDbType.Int, 8, ParameterDirection.Input, iGroup)
            AddParameterToSQLCommand(Cmd, "@TASK_ID", SqlDbType.Int, 8, ParameterDirection.Input, iTask)
            AddParameterToSQLCommand(Cmd, "@STATUS", SqlDbType.VarChar, 5, ParameterDirection.Input, UserInfo(3))

            SetCommandType(Cmd, CommandType.StoredProcedure, SP_ADDTASK)

            ExecuteNonQuery(Cmd, 5)

            Return (True)
        End Function

        Public Overrides Function GetTaskByUser(ByVal User As String) As DataView
            Dim conn As New SqlConnection(DB_CrmTrackerConnection)
            Dim command As New SqlCommand(SP_TASK_BY_USER, conn)

            command.CommandType = CommandType.StoredProcedure
            AddParameterToSQLCommand(command, "@UserName", SqlDbType.VarChar, 50, ParameterDirection.Input, User)

            Dim dad As New SqlDataAdapter(command)
            Dim dtPer As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtPer)
            Catch ex As Exception
                ' Catch Error
                Dim dv As New DataView()
                Return (dv)
            Finally
                conn.Close()
                dad.Dispose()
                dtPer.Dispose()
            End Try

            Return (dtPer.DefaultView)
        End Function


        Public Overrides Function GetTasks() As UsrTaskCollection
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.StoredProcedure, SP_TASK_TASK)

            Dim myTask As New GenerateCollectionFromReader(AddressOf ReturnTskTasks)
            Dim TaskCollection As UsrTaskCollection = CType(ExecuteReaderCmd(cmd, myTask, 5), UsrTaskCollection)

            Return (TaskCollection)
        End Function

        Public Overrides Function GetGroups() As UsrTaskCollection
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.StoredProcedure, SP_GROUP_TASK)

            Dim myTask As New GenerateCollectionFromReader(AddressOf ReturnTskGroups)
            Dim TaskCollection As UsrTaskCollection = CType(ExecuteReaderCmd(cmd, myTask, 5), UsrTaskCollection)

            Return (TaskCollection)
        End Function

        Public Overrides Function GetUsers() As UsrTaskCollection
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.StoredProcedure, SP_USER_TASK)

            Dim myTask As New GenerateCollectionFromReader(AddressOf ReturnTskUsers)
            Dim TaskCollection As UsrTaskCollection = CType(ExecuteReaderCmd(cmd, myTask, 5), UsrTaskCollection)

            Return (TaskCollection)
        End Function

        Public Overrides Function GetMailers() As UsrMailerCollection
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.StoredProcedure, SP_MAILER_DATA)

            Dim myMailer As New GenerateCollectionFromReader(AddressOf ReturnMailers)
            Dim MailerCollection As UsrMailerCollection = CType(ExecuteReaderCmd(cmd, myMailer, 2), UsrMailerCollection)

            Return (MailerCollection)
        End Function

        ' begin Reports
        Public Overrides Function ReportCurrentProcess3View() As DataView
            Dim myConnect As SqlConnection = Nothing
            Dim myCmd As SqlCommand = Nothing
            Dim myAdp As SqlDataAdapter = Nothing
            Dim myTable As New DataTable()

            Try
                myConnect = New SqlConnection(ConnectionString)
                myCmd = New SqlCommand(SP_CURRENT_PROCESS3, myConnect)
                myCmd.CommandType = CommandType.StoredProcedure
                myAdp = New SqlDataAdapter(myCmd)

                myConnect.Open()
                myAdp.Fill(myTable)

            Catch ex As Exception
                ' Catch Error
                Dim myView As New DataView()
                Return (myView)
            Finally
                If Not myAdp Is Nothing Then
                    myAdp.Dispose()
                    myAdp = Nothing
                End If
                If Not myCmd Is Nothing Then
                    myCmd.Dispose()
                    myCmd = Nothing
                End If
                If Not myConnect Is Nothing Then
                    myConnect.Close()
                    myConnect.Dispose()
                    myConnect = Nothing
                End If
                ' If Not myTable Is Nothing Then myTable = Nothing
            End Try

            Return (myTable.DefaultView)
        End Function

        ' Adding a new function
        ' Dec 23, 2008 12:04 PM
        Public Overrides Function ReportCurrentProcessStatus() As String()

            Dim Arr(1) As String

            Dim objConn As SqlConnection = Nothing
            Dim objRd As SqlDataReader = Nothing
            Dim objCmd As SqlCommand = Nothing

            Dim sReturn As String = "Error. Please contact System Admin. have a great day!"

            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If

            Try
                objConn = New SqlConnection(ConnectionString)
                objCmd = New SqlCommand(SP_CURRENT_PROCESS2, objConn)
                objCmd.CommandType = CommandType.StoredProcedure

                objCmd.Connection.Open()
                objRd = objCmd.ExecuteReader()

                If objRd.HasRows Then
                    Do While objRd.Read()
                        Arr(0) = objRd.GetSqlValue(0).ToString()
                        Arr(1) = objRd.GetSqlValue(1).ToString()
                    Loop ' Do While
                End If ' HasRows


            Catch ex As Exception
                Arr(0) = sReturn
                Arr(1) = ex.Message
            Finally
                If Not objRd Is Nothing Then
                    objRd.Close()
                    objRd = Nothing
                End If

                If Not objCmd.Connection Is Nothing Then
                    objCmd.Connection.Close()
                    objCmd.Connection.Dispose()
                End If
            End Try

            Return (Arr)
        End Function

        Public Overrides Function MultiPurposeView(ByVal iParameter As Integer, ByVal iPractice As Int32) As DataView
            Dim myConnect As SqlConnection = Nothing
            Dim myCmd As SqlCommand = Nothing
            Dim myAdp As SqlDataAdapter = Nothing
            Dim myTable As New DataTable()
            Dim Sp_Name As String = String.Empty

            Select Case iParameter
                Case 1
                    Sp_Name = SP_PRACTICE_INFO
                Case 2
                    Sp_Name = SP_PRACTICE_SCHEDULE
                Case 3
                    Sp_Name = SP_PRACTICE_EXTRACTION
                Case 4
                    Sp_Name = SP_PRACTICE_CLIENTS
                Case 5
                    Sp_Name = SP_PRACTICE_PATIENTS
                Case 6
                    Sp_Name = SP_PRACTICE_REMINDERS
                Case 7
                    Sp_Name = SP_PRACTICE_HISTORY
                Case 8
                    Sp_Name = SP_PRACTICE_HISTORY_DATE
                Case 9
                    Sp_Name = SP_PRACTICE_HISTORY_MONTH
                Case Else
                    ' do nothing
            End Select


            Try
                myConnect = New SqlConnection(ConnectionString)
                myCmd = New SqlCommand(Sp_Name, myConnect)
                myCmd.CommandType = CommandType.StoredProcedure

                AddParameterToSQLCommand(myCmd, "@PRACTICE_ID", SqlDbType.Int, 8, ParameterDirection.Input, iPractice)

                myAdp = New SqlDataAdapter(myCmd)
                myConnect.Open()
                myAdp.Fill(myTable)

            Catch ex As Exception
                ' Catch Error
                Dim myView As New DataView()
                Return (myView)
            Finally
                If Not myAdp Is Nothing Then
                    myAdp.Dispose()
                    myAdp = Nothing
                End If
                If Not myCmd Is Nothing Then
                    myCmd.Dispose()
                    myCmd = Nothing
                End If
                If Not myConnect Is Nothing Then
                    myConnect.Close()
                    myConnect.Dispose()
                    myConnect = Nothing
                End If
                ' If Not myTable Is Nothing Then myTable = Nothing
            End Try

            Return (myTable.DefaultView)
        End Function

        Public Overrides Function ReportDisplayErrorView() As DataView
            Dim myConnect As SqlConnection = Nothing
            Dim myCmd As SqlCommand = Nothing
            Dim myAdp As SqlDataAdapter = Nothing
            Dim myTable As New DataTable()

            Try
                myConnect = New SqlConnection(ConnectionString)
                myCmd = New SqlCommand(SP_REPORT_ERROR, myConnect)
                myCmd.CommandType = CommandType.StoredProcedure
                myAdp = New SqlDataAdapter(myCmd)
                myConnect.Open()
                myAdp.Fill(myTable)

            Catch ex As Exception
                ' Catch Error
                Dim myView As New DataView()
                Return (myView)
            Finally
                If Not myAdp Is Nothing Then
                    myAdp.Dispose()
                    myAdp = Nothing
                End If
                If Not myCmd Is Nothing Then
                    myCmd.Dispose()
                    myCmd = Nothing
                End If
                If Not myConnect Is Nothing Then
                    myConnect.Close()
                    myConnect.Dispose()
                    myConnect = Nothing
                End If
                ' If Not myTable Is Nothing Then myTable = Nothing
            End Try

            Return (myTable.DefaultView)
        End Function

        Public Overrides Function ReportCurrentProcessHistoryView() As DataView
            Dim myConnect As SqlConnection = Nothing
            Dim myCmd As SqlCommand = Nothing
            Dim myAdp As SqlDataAdapter = Nothing
            Dim myTable As New DataTable()

            Try
                myConnect = New SqlConnection(ConnectionString)
                myCmd = New SqlCommand(SP_CURRENT_PROCESSHISTORY, myConnect)
                myCmd.CommandType = CommandType.StoredProcedure
                myAdp = New SqlDataAdapter(myCmd)
                myConnect.Open()
                myAdp.Fill(myTable)

            Catch ex As Exception
                ' Catch Error
                Dim myView As New DataView()
                Return (myView)
            Finally
                If Not myAdp Is Nothing Then
                    myAdp.Dispose()
                    myAdp = Nothing
                End If
                If Not myCmd Is Nothing Then
                    myCmd.Dispose()
                    myCmd = Nothing
                End If
                If Not myConnect Is Nothing Then
                    myConnect.Close()
                    myConnect.Dispose()
                    myConnect = Nothing
                End If
                ' If Not myTable Is Nothing Then myTable = Nothing
            End Try

            Return (myTable.DefaultView)
        End Function

        ' begin Reports
        Public Overrides Function ReportCurrentProcessView() As DataView
            Dim myConnect As SqlConnection = Nothing
            Dim myCmd As SqlCommand = Nothing
            Dim myAdp As SqlDataAdapter = Nothing
            Dim myTable As New DataTable()

            Try
                myConnect = New SqlConnection(ConnectionString)
                myCmd = New SqlCommand(SP_CURRENT_PROCESS, myConnect)
                myCmd.CommandType = CommandType.StoredProcedure

                'AddParameterToSQLCommand(command, "@PRACTICE_ID", SqlDbType.Int, 8, ParameterDirection.Input, iPractice)

                myAdp = New SqlDataAdapter(myCmd)
                myConnect.Open()
                myAdp.Fill(myTable)

            Catch ex As Exception
                ' Catch Error
                Dim myView As New DataView()
                Return (myView)
            Finally
                If Not myAdp Is Nothing Then
                    myAdp.Dispose()
                    myAdp = Nothing
                End If
                If Not myCmd Is Nothing Then
                    myCmd.Dispose()
                    myCmd = Nothing
                End If
                If Not myConnect Is Nothing Then
                    myConnect.Close()
                    myConnect.Dispose()
                    myConnect = Nothing
                End If
                ' If Not myTable Is Nothing Then myTable = Nothing
            End Try

            Return (myTable.DefaultView)
        End Function

        Public Overrides Function ReportQueueHistoryView() As DataView
            Dim myConnect As SqlConnection = Nothing
            Dim myCmd As SqlCommand = Nothing
            Dim myAdp As SqlDataAdapter = Nothing
            Dim myTable As New DataTable()

            Try
                myConnect = New SqlConnection(ConnectionString)
                myCmd = New SqlCommand(SP_REPORTHISTORY_QUEUE, myConnect)
                myCmd.CommandType = CommandType.StoredProcedure

                'AddParameterToSQLCommand(command, "@PRACTICE_ID", SqlDbType.Int, 8, ParameterDirection.Input, iPractice)

                myAdp = New SqlDataAdapter(myCmd)
                myConnect.Open()
                myAdp.Fill(myTable)

            Catch ex As Exception
                ' Catch Error
                Dim myView As New DataView()
                Return (myView)
            Finally
                If Not myAdp Is Nothing Then
                    myAdp.Dispose()
                    myAdp = Nothing
                End If
                If Not myCmd Is Nothing Then
                    myCmd.Dispose()
                    myCmd = Nothing
                End If
                If Not myConnect Is Nothing Then
                    myConnect.Close()
                    myConnect.Dispose()
                    myConnect = Nothing
                End If
                ' If Not myTable Is Nothing Then myTable = Nothing
            End Try

            Return (myTable.DefaultView)
        End Function

        Public Overrides Function ReportQueueView() As DataView
            Dim myConnect As SqlConnection = Nothing
            Dim myCmd As SqlCommand = Nothing
            Dim myAdp As SqlDataAdapter = Nothing
            Dim myTable As New DataTable()

            Try
                myConnect = New SqlConnection(ConnectionString)
                myCmd = New SqlCommand(SP_REPORT_QUEUE, myConnect)
                myCmd.CommandType = CommandType.StoredProcedure

                'AddParameterToSQLCommand(command, "@PRACTICE_ID", SqlDbType.Int, 8, ParameterDirection.Input, iPractice)

                myAdp = New SqlDataAdapter(myCmd)
                myConnect.Open()
                myAdp.Fill(myTable)

            Catch ex As Exception
                ' Catch Error
                Dim myView As New DataView()
                Return (myView)
            Finally
                If Not myAdp Is Nothing Then
                    myAdp.Dispose()
                    myAdp = Nothing
                End If
                If Not myCmd Is Nothing Then
                    myCmd.Dispose()
                    myCmd = Nothing
                End If
                If Not myConnect Is Nothing Then
                    myConnect.Close()
                    myConnect.Dispose()
                    myConnect = Nothing
                End If
                'If Not myTable Is Nothing Then myTable = Nothing
            End Try

            Return (myTable.DefaultView)
        End Function
        ' end Reports

        Public Overrides Function User_GetIdentityName(ByVal sUsrName As String) As UserLogin
            ' validate input
            If sUsrName Is Nothing OrElse sUsrName.Length = 0 Then
                Throw New ArgumentOutOfRangeException("sUsrName")
            End If
            ' Execute SQL Command
            Dim sqlCmd As New SqlCommand

            SetCommandType(sqlCmd, CommandType.StoredProcedure, SP_GET_USR_IDENTITY)
            AddParameterToSQLCommand(sqlCmd, "@USERNAME", SqlDbType.VarChar, 35, ParameterDirection.Input, sUsrName)

            Dim CreateUsr As New GenerateCollectionFromReader(AddressOf ReturnUserWinAuth)
            Dim userCollection As UserLoginCollection = CType(ExecuteReaderCmd(sqlCmd, CreateUsr), UserLoginCollection)

            If userCollection.Count > 0 Then
                Return userCollection(0)
            Else
                Return Nothing
            End If
        End Function


        ' Using Dictionary to return the 
        ' CURRENT HISTORY DATA
        Public Overrides Function GetCurrentHistoryData(ByVal iPractice As Int32, ByVal iMode As Integer) As Dictionary(Of String, Boolean)
            Dim ReminderColl As New Dictionary(Of String, Boolean)

            Dim Rd As SqlDataReader = Nothing
            Dim conn As New SqlConnection(ConnectionString)
            Dim cmd As New SqlCommand(SP_ADM_CLIENTCLASS, conn)
            Dim iUniqueID As Int32 = 0
            Dim iReminder As Int32 = 0

            cmd.CommandType = CommandType.StoredProcedure
            AddParameterToSQLCommand(cmd, "@PRACTICE_ID", SqlDbType.Int, 8, ParameterDirection.Input, iPractice)
            AddParameterToSQLCommand(cmd, "@MODE", SqlDbType.Int, 8, ParameterDirection.Input, iMode)

            Try
                cmd.Connection.Open()
                Rd = cmd.ExecuteReader()

                If Rd.HasRows Then
                    Do While Rd.Read()
                        iUniqueID = Rd.GetInt32(0)

                        Try
                            iReminder = Int32.Parse(Rd.GetSqlInt16(1))
                        Catch ex As Exception
                            iReminder = 0
                        End Try

                        If iReminder = 1 Then
                            ReminderColl.Add(iUniqueID, True)
                        Else
                            ReminderColl.Add(iUniqueID, False)
                        End If
                    Loop
                End If
            Catch ex As Exception

            Finally

                Rd.Close()
                cmd.Connection.Close()
            End Try

            Return (ReminderColl)
        End Function

        Public Overrides Function SaveAdminClientClass2(ByVal sUserId As String, ByVal iReminder As Integer, ByVal iUid As Int32) As Boolean
            Dim Cmd As New SqlCommand


            AddParameterToSQLCommand(Cmd, "@USER_ID", SqlDbType.VarChar, 35, ParameterDirection.Input, sUserId)
            AddParameterToSQLCommand(Cmd, "@REMINDER", SqlDbType.Int, 8, ParameterDirection.Input, iReminder)
            AddParameterToSQLCommand(Cmd, "@UID", SqlDbType.Int, 8, ParameterDirection.Input, iUid)

            SetCommandType(Cmd, CommandType.StoredProcedure, SP_ADMUPDATE_CLIENTCLASS_2)

            ExecuteNonQuery(Cmd)

            Return (True)
        End Function

        Public Overrides Function SaveAdminClientClass(ByVal sUserId As String, ByVal sUniqueIDs As String) As Boolean
            Dim Cmd As New SqlCommand

            If String.IsNullOrEmpty(sUniqueIDs) Then
                Throw New ArgumentOutOfRangeException("The sUniqueIDs is Empty ...")
            End If

            AddParameterToSQLCommand(Cmd, "@USER_ID", SqlDbType.VarChar, 35, ParameterDirection.Input, sUserId)
            AddParameterToSQLCommand(Cmd, "@ROWS_FOR_UPDATE_STRING", SqlDbType.VarChar, 1500, ParameterDirection.Input, sUniqueIDs)

            SetCommandType(Cmd, CommandType.StoredProcedure, SP_ADMUPDATE_CLIENTCLASS)

            ExecuteNonQuery(Cmd)

            Return (True)
        End Function

        Public Overrides Function GetAdminClientClass(ByVal iPractice As Int32, ByVal iMode As Integer) As DataView
            Dim conn As New SqlConnection(ConnectionString)
            Dim command As New SqlCommand(SP_ADM_CLIENTCLASS, conn)

            command.CommandType = CommandType.StoredProcedure
            AddParameterToSQLCommand(command, "@PRACTICE_ID", SqlDbType.Int, 8, ParameterDirection.Input, iPractice)
            AddParameterToSQLCommand(command, "@MODE", SqlDbType.Int, 8, ParameterDirection.Input, iMode)

            Dim dad As New SqlDataAdapter(command)
            Dim dtPer As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtPer)
            Catch ex As Exception
                ' Catch Error
                Dim dv As New DataView()
                Return (dv)
            Finally
                conn.Close()
                dad.Dispose()
                dtPer.Dispose()
            End Try

            Return (dtPer.DefaultView)

        End Function

        Public Overrides Function GetWebPractices() As CnvtPracticeCollection
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.Text, SP_WEBGET_PRACTICES)
            Dim myBlock As New GenerateCollectionFromReader(AddressOf ReturnChgPractice)

            Dim mColl As CnvtPracticeCollection = CType(ExecuteReaderCmd(cmd, myBlock, 2), CnvtPracticeCollection)

            Return (mColl)
        End Function

        Public Overrides Function GetDashBoardPProcess(ByVal iPractice As Int32) As DataView
            Dim conn As New SqlConnection(ConnectionString)
            Dim command As New SqlCommand(SP_DBDROP_PPR, conn)

            command.CommandType = CommandType.StoredProcedure
            AddParameterToSQLCommand(command, "@PracticeID", SqlDbType.Int, 8, ParameterDirection.Input, iPractice)

            Dim dad As New SqlDataAdapter(command)
            Dim dtPer As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtPer)
            Catch ex As Exception
                ' Catch Error
                Dim dv As New DataView()
                Return dv
            Finally
                conn.Close()
                dad.Dispose()
                dtPer.Dispose()
            End Try

            Return (dtPer.DefaultView)
        End Function

        Public Overrides Function GetAdminConvertedPrct() As DataView
            Dim conn As New SqlConnection(ConnectionString)
            Dim command As New SqlCommand(SP_ADMIN_CHG_CONVERTED_PRACTICES, conn)

            command.CommandType = CommandType.StoredProcedure

            Dim dad As New SqlDataAdapter(command)
            Dim dtPer As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtPer)
            Catch ex As Exception
                ' Catch Error
                Dim dv As New DataView()
                Return dv
            Finally
                conn.Close()
                dad.Dispose()
                dtPer.Dispose()
            End Try

            Return (dtPer.DefaultView)
        End Function

        Public Overrides Function GetAdminListPersonnel() As DataView
            Dim conn As New SqlConnection(ConnectionString)
            Dim command As New SqlCommand(SP_ADLISTPERSONNEL, conn)

            command.CommandType = CommandType.StoredProcedure

            Dim dad As New SqlDataAdapter(command)
            Dim dtPer As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtPer)
            Catch ex As Exception
                ' Catch Error
                Dim dv As New DataView()
                Return dv
            Finally
                conn.Close()
                dad.Dispose()
                dtPer.Dispose()
            End Try

            Return (dtPer.DefaultView)
        End Function

        Public Overrides Function GetDashBoardIssues() As DashBoardCollection
            Dim cmd As New SqlCommand

            SetCommandType(cmd, CommandType.Text, SP_DBDROPISSUE)

            Dim myGroup As New GenerateCollectionFromReader(AddressOf ReturnDbIssues)
            Dim dbColl As DashBoardCollection = CType(ExecuteReaderCmd(cmd, myGroup, 2), DashBoardCollection)

            Return (dbColl)
        End Function

        Public Overrides Function AddToHistory_Matrix(ByVal sMilerType As String) As Boolean
            Dim cmd As New SqlCommand
            Dim sSP As String = String.Empty

            AddParameterToSQLCommand(cmd, "@MAILERTYPE_ID", SqlDbType.Char, 2, ParameterDirection.Input, sMilerType)
            'AddParameterToSQLCommand(cmd, "@MODE", SqlDbType.Int, 8, ParameterDirection.Input, iMode)

            SetCommandType(cmd, CommandType.StoredProcedure, SP_ADDHISTORY_PM)
            ExecuteNonQuery(cmd)

            Return (True)
        End Function

        Public Overrides Function WelcomeLetterClearPrintDate(ByVal sMicroChip As String, ByVal batch As Int32) As Boolean
            Dim myCommand As New SqlCommand

            AddParameterToSQLCommand(myCommand, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParameterToSQLCommand(myCommand, "@MICROCHIP", SqlDbType.VarChar, 25, ParameterDirection.Input, sMicroChip)
            AddParameterToSQLCommand(myCommand, "@BATCH", SqlDbType.Int, 0, ParameterDirection.Input, batch)

            SetCommandType(myCommand, CommandType.StoredProcedure, SP_ACTMAIL_CLEARPNTDT)
            ExecuteScalarCommand2(myCommand)

            Dim returnValue As Integer = CInt(myCommand.Parameters("@ReturnValue").Value)
            Return IIf(returnValue = 0, True, False)
        End Function

        Public Overrides Function WelcomeLetterSetPrintDate(ByVal batch As Int32, ByVal dt As DateTime, ByVal sPrintTotal As String) As Boolean
            Dim myCommand As New SqlCommand

            If sPrintTotal.Length = 0 Then sPrintTotal = "0"

            AddParameterToSQLCommand(myCommand, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParameterToSQLCommand(myCommand, "@DATE", SqlDbType.SmallDateTime, 25, ParameterDirection.Input, dt)
            AddParameterToSQLCommand(myCommand, "@BATCH_ID", SqlDbType.Int, 0, ParameterDirection.Input, batch)
            AddParameterToSQLCommand(myCommand, "@PRINT_TOTAL", SqlDbType.VarChar, 15, ParameterDirection.Input, sPrintTotal)

            SetCommandType(myCommand, CommandType.StoredProcedure, SP_ACTMAIL_SETPRINTDATE)
            ExecuteScalarCommand2(myCommand)

            Dim returnValue As Integer = CInt(myCommand.Parameters("@ReturnValue").Value)
            Return IIf(returnValue = 0, True, False)
        End Function

        Public Overrides Function WelcomeLetterGetBatches() As WelcomeLetterCollections
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.Text, SP_ACTMAIL_GETBATCH)

            Dim Batch As New GenerateCollectionFromReader(AddressOf ReturnBatchId)
            Dim BatchCollection As WelcomeLetterCollections = CType(ExecuteReaderCmd(cmd, Batch, 4), WelcomeLetterCollections)

            Return BatchCollection
        End Function

        Public Overrides Function GetReminderBlocks() As ReminderBlockCollection
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.Text, SP_GET_REMINDRBLOCK)
            Dim myBlock As New GenerateCollectionFromReader(AddressOf ReturnReminderBlock)
            Dim ReminderCollection As ReminderBlockCollection = CType(ExecuteReaderCmd(cmd, myBlock, 2), ReminderBlockCollection)
            Return (ReminderCollection)
        End Function

        ' added July 25, 2008
        ' ////////////////////////////////////////////////////////////////////
        Public Overrides Function GetAdminPractices() As CnvtPracticeCollection
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.Text, SP_ADMIN_CHG_PRACTICES)
            Dim myBlock As New GenerateCollectionFromReader(AddressOf ReturnChgPractice)

            Dim mColl As CnvtPracticeCollection = CType(ExecuteReaderCmd(cmd, myBlock, 2), CnvtPracticeCollection)

            Return (mColl)
        End Function

        Public Overrides Function GetHistoryGroups() As HistoryGroupsCollection
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.Text, SP_GET_HISTORYGROUP)
            Dim myGroup As New GenerateCollectionFromReader(AddressOf ReturnHistoryGroups)
            Dim HistoryCollection As HistoryGroupsCollection = CType(ExecuteReaderCmd(cmd, myGroup, 2), HistoryGroupsCollection)
            Return HistoryCollection
        End Function

        Public Overrides Function GetPracticeGroups() As PracticeGroupsCollection
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.Text, FN_GET_PRACTICEGROUP)
            Dim myGroup As New GenerateCollectionFromReader(AddressOf ReturnPracticeGroups)
            Dim GroupsCollection As PracticeGroupsCollection = CType(ExecuteReaderCmd(cmd, myGroup, 2), PracticeGroupsCollection)
            Return GroupsCollection
        End Function

        Public Overrides Function GetPracticeRegions() As UsrRegionsCollection
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.StoredProcedure, SP_GET_REGIONS)
            Dim myRegion As New GenerateCollectionFromReader(AddressOf ReturnRegions)
            Dim RegionsCollection As UsrRegionsCollection = CType(ExecuteReaderCmd(cmd, myRegion, 2), UsrRegionsCollection)
            Return RegionsCollection
        End Function

        Public Overrides Function GetStatesUS() As UsrStatesCollection
            Dim cmd As New SqlCommand
            SetCommandType(cmd, CommandType.StoredProcedure, SP_GET_STATESUSA)
            Dim myStates As New GenerateCollectionFromReader(AddressOf ReturnStatesUS)
            Dim PimsCollection As UsrStatesCollection = CType(ExecuteReaderCmd(cmd, myStates, 2), UsrStatesCollection)
            Return PimsCollection
        End Function

        Public Overrides Function ReadBinaryData() As String

            Dim bufferSize As Integer = 100
            Dim outByte() As Byte = Nothing
            Dim startIndex As Long = 0
            Dim ID As Integer
            Dim sOutput As String = String.Empty

            Dim connection As SqlConnection = New SqlConnection(Me.LocalConnectionString)
            Dim command As New SqlCommand(SP_SECURITY_READUSER, connection)

            command.Connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader() ' CommandBehavior.SequentialAccess
            Do While reader.Read()

                ID = reader.GetSqlBinary(0).Length
                startIndex = 0
                'Read the bytes into outbyte[] and retain the number of bytes returned.
                'retval = myReader.GetBytes(1, startIndex, outbyte, 0, bufferSize);
                'retval = reader.GetBytes(1, startIndex, outByte, 0, bufferSize)
                outByte = reader.GetSqlBinary(0)
                ' Convert.ToInt32(outByte(0).ToString())

            Loop

            reader.Close()
            command.Connection.Close()

            sOutput = System.Text.Encoding.Unicode.GetString(outByte)
            Return sOutput


        End Function

        Public Overrides Sub AddBinaryData(ByVal bData() As Byte)
            If bData Is Nothing Or bData.Length = 0 Then
                Throw New ArgumentOutOfRangeException("The Binary data is empty or missing ...")
            End If

            Dim cmd As New SqlCommand
            AddParameterToSQLCommand(cmd, "@BFIELD", SqlDbType.VarBinary, 150, ParameterDirection.Input, bData)
            SetCommandType(cmd, CommandType.StoredProcedure, SP_SECURITY_ADDUSER)

            ExecuteNonQuery(cmd, 4)
        End Sub

        Public Overrides Function PetIdRequestPending() As DataView
            Dim conn As New SqlConnection(MSSConnectionString)
            Dim command As New SqlCommand(SP_PETID_REORDER_PENDING, conn)

            command.CommandType = CommandType.StoredProcedure

            Dim dad As New SqlDataAdapter(command)
            Dim dtPending As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtPending)
            Catch ex As Exception
                ' Catch Error
                Dim dv As New DataView()
                Return dv
            Finally
                conn.Close()
                dad.Dispose()
                dtPending.Dispose()
            End Try

            Return dtPending.DefaultView

        End Function

        Public Overrides Sub PetIdAddReorder(ByVal sClientId As String, _
                ByVal sPatientId As String, _
                ByVal sPracticeId As String, _
                ByVal sUserName As String)
            Dim cmd As New SqlCommand

            AddParameterToSQLCommand(cmd, "@PracticeID", SqlDbType.VarChar, 50, ParameterDirection.Input, sPracticeId)
            AddParameterToSQLCommand(cmd, "@ClientID", SqlDbType.VarChar, 30, ParameterDirection.Input, sClientId)
            AddParameterToSQLCommand(cmd, "@PatientID", SqlDbType.VarChar, 30, ParameterDirection.Input, sPatientId)
            AddParameterToSQLCommand(cmd, "@Username", SqlDbType.VarChar, 30, ParameterDirection.Input, sUserName)

            SetCommandType(cmd, CommandType.StoredProcedure, SP_PETID_REORDER_ADD)

            ExecuteNonQuery(cmd, 3)
        End Sub

        Public Overrides Function PetId_GetClients(ByVal sClientName As String) As UserPetIdCollection
            Dim cmd As New SqlCommand

            If sClientName = String.Empty Or sClientName.Length = 0 Then
                Throw New ArgumentOutOfRangeException("sClientName is Empty or mising...")
            End If

            AddParameterToSQLCommand(cmd, "@CLIENTLASTNAME", SqlDbType.VarChar, 50, ParameterDirection.Input, sClientName)
            SetCommandType(cmd, CommandType.StoredProcedure, SP_PETID_GETCLIENTS)

            Dim mySqlGenColl As New GenerateCollectionFromReader(AddressOf ReturnPetIdClients)
            Dim PetIdCollection As UserPetIdCollection = CType(ExecuteReaderCmd(cmd, mySqlGenColl, 3), UserPetIdCollection)

            Return PetIdCollection
        End Function

        Public Overrides Function RemainderMap_GetNotMappedCode() As DataView
            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If

            Dim conn As New SqlConnection(ConnectionString)
            Dim command As New SqlCommand(SP_REMINDERMAP_UNMAPED, conn)

            command.CommandType = CommandType.StoredProcedure

            Dim dad As New SqlDataAdapter(command)
            Dim dtActMail As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtActMail)
            Catch ex As Exception
                ' Catch Error
                Dim dv As New DataView()
                Return dv
            Finally
                conn.Close()
                dad.Dispose()
                dtActMail.Dispose()
            End Try

            Return dtActMail.DefaultView

        End Function

        Public Overrides Function RemainderMap_GetMappedCode(ByVal iPractice As Int32) As DataView
            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If
            If iPractice <= 0 Then
                Throw New ArgumentOutOfRangeException("iPractice cannot be zero or less than zero")
            End If

            Dim conn As New SqlConnection(ConnectionString)
            Dim command As New SqlCommand(SP_REMINDERMAP_GETMAPPED, conn)

            AddParameterToSQLCommand(command, "@PracticeID", SqlDbType.Int, 0, ParameterDirection.Input, iPractice)
            command.CommandType = CommandType.StoredProcedure

            Dim dad As New SqlDataAdapter(command)
            Dim dtActMail As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtActMail)
            Catch ex As Exception
                ' do something
                Dim dv As New DataView()
                Return dv
            Finally
                conn.Close()
                dad.Dispose()
                dtActMail.Dispose()
            End Try

            Return (dtActMail.DefaultView)
        End Function

        Public Overrides Function WelcomeLetterReturnAlreadySend() As DataView
            If DBVM_HomeAgainMarketing = String.Empty Then
                Throw New ArgumentOutOfRangeException("DBVM:HomeAgainMarketing")
            End If

            Dim conn As New SqlConnection(DBVM_HomeAgainMarketing)
            Dim command As New SqlCommand(SP_ALREADYSEND_ACTIVATIONMAIL, conn)

            command.CommandType = CommandType.StoredProcedure

            Dim dad As New SqlDataAdapter(command)
            Dim dtActMail As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtActMail)
            Catch ex As Exception
                ' do something
                Dim dv As New DataView()
                Return dv
            Finally
                conn.Close()
                dad.Dispose()
                dtActMail.Dispose()
            End Try

            Return (dtActMail.DefaultView)
        End Function

        Public Overrides Function RemainderMap_GetPractices() As ReminderMapCollection
            Dim cmd As New SqlCommand

            SetCommandType(cmd, CommandType.StoredProcedure, SP_REMINDERMAP_GETPRACTICES)

            Dim mySqlGenColl As New GenerateCollectionFromReader(AddressOf ReturnRmPractices)
            Dim RmMapCollection As ReminderMapCollection = CType(ExecuteReaderCmd(cmd, mySqlGenColl), ReminderMapCollection)

            Return (RmMapCollection)
        End Function

        Public Overrides Function RemainderMap_GetRightSide() As ReminderMapCollection
            Dim cmd As New SqlCommand

            SetCommandType(cmd, CommandType.StoredProcedure, SP_REMINDERMAP_RIGHTSIDE)

            Dim mySqlGenColl As New GenerateCollectionFromReader(AddressOf ReturnRmRightSide)
            Dim RmMapCollection As ReminderMapCollection = CType(ExecuteReaderCmd(cmd, mySqlGenColl), ReminderMapCollection)

            Return (RmMapCollection)
        End Function

        Public Overrides Function DirectMail_Run(ByVal iBatch As Int32) As DirectMailCollection
            Dim cmd As New SqlCommand

            SetCommandType(cmd, CommandType.StoredProcedure, SP_DIRECTMAIL_RUN)
            AddParameterToSQLCommand(cmd, "@BATCHID", SqlDbType.Int, 0, ParameterDirection.Input, iBatch)

            Dim mySqlGenColl As New GenerateCollectionFromReader(AddressOf ReturnDmExecute)
            Dim DrMailCollection As DirectMailCollection = CType(ExecuteReaderCmd(cmd, mySqlGenColl, 1), DirectMailCollection)

            Return (DrMailCollection)
        End Function

        Public Overrides Function DirectMail_PracticesWait() As DirectMailCollection
            Dim cmd As New SqlCommand

            SetCommandType(cmd, CommandType.StoredProcedure, SP_DIRECTMAIL_WAITPR)

            Dim mySqlGenColl As New GenerateCollectionFromReader(AddressOf ReturnDmPrWait)
            Dim DrMailCollection As DirectMailCollection = CType(ExecuteReaderCmd(cmd, mySqlGenColl, 1), DirectMailCollection)

            Return (DrMailCollection)
        End Function

        Public Overrides Sub DirectMail_AssignPractices(ByVal PracValues As String)
            Dim cmd As New SqlCommand

            AddParameterToSQLCommand(cmd, "@PrValues", SqlDbType.VarChar, 5000, ParameterDirection.Input, PracValues)
            SetCommandType(cmd, CommandType.StoredProcedure, SP_DIRECTMAIL_ASSIGNPR)

            ExecuteNonQuery(cmd, 1)
        End Sub

        Public Overrides Function DirectMail_GetPractices() As DirectMailCollection
            Dim cmd As New SqlCommand

            SetCommandType(cmd, CommandType.StoredProcedure, SP_DIRECTMAIL_GETPR)

            Dim mySqlGenColl As New GenerateCollectionFromReader(AddressOf ReturnDmPractices)
            Dim DrMailCollection As DirectMailCollection = CType(ExecuteReaderCmd(cmd, mySqlGenColl, 1), DirectMailCollection)

            Return (DrMailCollection)
        End Function

        Public Overrides Function DirectMail_PracticesAlreadySend() As DirectMailCollection
            Dim cmd As New SqlCommand

            SetCommandType(cmd, CommandType.StoredProcedure, SP_DIRECTMAIL_ALREADYSEND)

            Dim mySqlGenColl As New GenerateCollectionFromReader(AddressOf ReturnDirectMailPrAlreadySend)
            Dim DrMailCollection As DirectMailCollection = CType(ExecuteReaderCmd(cmd, mySqlGenColl, 1), DirectMailCollection)

            Return (DrMailCollection)
        End Function

        ' Welcome Letter Get Current Status
        Public Overrides Function WelcomeLetter_CurrentStatus( _
            ByVal sFileName As String, _
            ByVal iStatus As Integer, _
            ByVal iBatch As Int32) As String()

            Dim i As Integer = 0
            Dim sTemp(8) As String
            Dim conn As New SqlConnection(DBVM_HomeAgainMarketing)
            Dim command As New SqlCommand(SP_STATUS_ACTIVATIONMAIL, conn)
            AddParameterToSQLCommand(command, "@STATUS", SqlDbType.SmallInt, 4, ParameterDirection.Input, iStatus)
            AddParameterToSQLCommand(command, "@FILE_NAME", SqlDbType.VarChar, 100, ParameterDirection.Input, sFileName)

            ' Added on May 23, 2008
            AddParameterToSQLCommand(command, "@BATCH_ID", SqlDbType.Int, 8, ParameterDirection.Input, iBatch)

            command.CommandType = CommandType.StoredProcedure

            Try
                command.Connection.Open()


                If iStatus = 1 Then
                    Dim reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        Do While reader.Read()
                            For i = 0 To 11
                                sTemp(i) = reader.GetSqlValue(i).ToString()
                            Next i

                        Loop

                        reader.Close()
                    Else
                        sTemp(0) = "No fields has been returned."
                    End If
                End If ' End iStatus

                If iStatus = 2 Then
                    command.ExecuteNonQuery()
                    sTemp(1) = "2"
                End If

                ' Get Imported File name
                If iStatus = 3 Then
                    Dim reader As SqlDataReader = command.ExecuteReader()
                    If reader.HasRows Then
                        Do While reader.Read()
                            sTemp(0) = reader.GetSqlValue(0).ToString()
                        Loop

                        reader.Close()
                    Else
                        sTemp(0) = "No fields has been returned."
                    End If
                End If

            Catch ex As Exception
                sTemp(1) = ex.Message
            Finally
                command.Connection.Close()
            End Try

            Return (sTemp)
        End Function

        Public Overrides Function WelcomeLetterSetHistory(ByVal sFile As String, ByVal iBatch As Int32) As Boolean
            Dim myCommand As New SqlCommand

            AddParameterToSQLCommand(myCommand, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParameterToSQLCommand(myCommand, "@FILE_NAME", SqlDbType.VarChar, 150, ParameterDirection.Input, sFile)
            AddParameterToSQLCommand(myCommand, "@BATCH_ID", SqlDbType.Int, 0, ParameterDirection.Input, iBatch)

            SetCommandType(myCommand, CommandType.StoredProcedure, SP_ACTMAIL_SET_HISTORY)
            ExecuteScalarCommand2(myCommand)

            Dim returnValue As Integer = CInt(myCommand.Parameters("@ReturnValue").Value)
            Return (IIf(returnValue = 0, True, False))
        End Function

        Public Overrides Function WelcomeLetterAddFile(ByVal sFileName As String, _
            ByVal sFileSize As String, _
            ByVal iBatch As Int32) As Boolean

            Dim myCommand As New SqlCommand

            AddParameterToSQLCommand(myCommand, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParameterToSQLCommand(myCommand, "@FILE_NAME", SqlDbType.VarChar, 150, ParameterDirection.Input, sFileName)
            AddParameterToSQLCommand(myCommand, "@FILE_SIZE", SqlDbType.VarChar, 50, ParameterDirection.Input, sFileSize)
            AddParameterToSQLCommand(myCommand, "@BATCH_ID", SqlDbType.Int, 0, ParameterDirection.Input, iBatch)

            SetCommandType(myCommand, CommandType.StoredProcedure, SP_ACTMAIL_ADDFILE)
            ExecuteScalarCommand2(myCommand)

            Dim returnValue As Integer = CInt(myCommand.Parameters("@ReturnValue").Value)
            Return (IIf(returnValue = 0, True, False))
        End Function

        ' Welcome Letter Append to History table
        Public Overrides Sub WelcomeLetter_AppendToHistory(ByVal sFileName As String)
            Dim myConn As New SqlConnection(DBVM_HomeAgainMarketing)

            myConn.Open()
            Dim myCommand As New SqlCommand(SP_APPENDTOHISTORY_ACTIVATIONMAIL, myConn)
            myCommand.CommandType = CommandType.StoredProcedure
            AddParameterToSQLCommand(myCommand, "@FILE_NAME", SqlDbType.VarChar, 100, ParameterDirection.Input, sFileName)

            Try
                myCommand.ExecuteNonQuery()
            Catch ex As Exception
                Throw New ArgumentOutOfRangeException("WelcomeLetter_AppendToHistory: " & ex.Message.ToString())
            Finally
                myConn.Close()
                myConn.Dispose()
                myCommand.Dispose()
            End Try
        End Sub

        Public Overrides Sub WelcomeLetter_ProcessMaster()
            Dim myConn As New SqlConnection(DBVM_HomeAgainMarketing)

            myConn.Open()
            Dim myCommand As New SqlCommand(SP_BUILD_ACTIVATIONMAIL, myConn)
            myCommand.CommandType = CommandType.StoredProcedure

            Try
                myCommand.ExecuteNonQuery()
            Catch ex As Exception
                Throw New ArgumentOutOfRangeException("WelcomeLetter_ProcessMaster: " & ex.Message.ToString())
            Finally
                myConn.Close()
                myConn.Dispose()
                myCommand.Dispose()
            End Try
        End Sub

        Public Overrides Function Delete_GroupOfMatrix(ByVal PracticeID As Int32, ByVal dropID As Int32) As Boolean
            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If

            ' Execute SQL Command
            Dim myCommand As New SqlCommand

            AddParameterToSQLCommand(myCommand, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParameterToSQLCommand(myCommand, "@PracticeID", SqlDbType.Int, 0, ParameterDirection.Input, PracticeID)
            AddParameterToSQLCommand(myCommand, "@DropID", SqlDbType.Int, 0, ParameterDirection.Input, dropID)

            SetCommandType(myCommand, CommandType.StoredProcedure, SP_DELETE_GROUPMATRIX)
            ExecuteScalarCommand(myCommand)

            Dim returnValue As Integer = CInt(myCommand.Parameters("@ReturnValue").Value)
            Return (IIf(returnValue = 0, True, False))
        End Function

        Public Overrides Function AddNew_Matrix_WithPractice(ByVal PracticeID As Int32, ByVal dropID As Int32) As Boolean
            Dim Result As Boolean = False

            Dim myConn As New SqlConnection(ConnectionString)
            Dim myTrans As Object

            myConn.Open()
            myTrans = myConn.BeginTransaction()

            Dim myCommand As New SqlCommand(SP_ADD_MATRIX_PRACTICE, myConn)

            myCommand.CommandType = CommandType.StoredProcedure

            AddParameterToSQLCommand(myCommand, "@PracticeID", SqlDbType.Int, 0, ParameterDirection.Input, PracticeID)
            AddParameterToSQLCommand(myCommand, "@DropID", SqlDbType.Int, 0, ParameterDirection.Input, dropID)

            myCommand.Connection = myConn
            myCommand.Transaction = myTrans

            Try
                myCommand.ExecuteNonQuery()
                myTrans.Commit()
                Result = True
            Catch ex As Exception
                myTrans.Rollback()
                Result = False
            Finally
                myConn.Close()
                myConn.Dispose()
                myCommand.Dispose()
            End Try

            Return (Result)
        End Function

        Public Overrides Function AddNew_Matrix(ByVal Arr As Array) As String()
            Dim myConn As New SqlConnection(ConnectionString)
            Dim myTrans As Object
            Dim ArrMessage(1) As String

            'Dim myNullValueDate As SqlTypes.SqlDateTime

            myConn.Open()
            myTrans = myConn.BeginTransaction()

            Dim myCommand As New SqlCommand(SP_INSERT_ONEMATRIX, myConn)

            myCommand.CommandType = CommandType.StoredProcedure

            AddParameterToSQLCommand(myCommand, "@PracticeID", SqlDbType.Int, 0, ParameterDirection.Input, Int32.Parse(Arr(0)))
            AddParameterToSQLCommand(myCommand, "@DropID", SqlDbType.Int, 0, ParameterDirection.Input, Int32.Parse(Arr(1)))

            AddParameterToSQLCommand(myCommand, "@Template", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(2))
            AddParameterToSQLCommand(myCommand, "@Body", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(3))

            AddParameterToSQLCommand(myCommand, "@P1", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(4))
            AddParameterToSQLCommand(myCommand, "@P2", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(5))
            AddParameterToSQLCommand(myCommand, "@P3", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(6))

            AddParameterToSQLCommand(myCommand, "@P1Disc", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(7))
            AddParameterToSQLCommand(myCommand, "@P2Disc", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(8))
            AddParameterToSQLCommand(myCommand, "@P3Disc", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(9))

            AddParameterToSQLCommand(myCommand, "@EXP1", SqlDbType.DateTime, 25, ParameterDirection.Input, Arr(10))
            AddParameterToSQLCommand(myCommand, "@EXP2", SqlDbType.DateTime, 25, ParameterDirection.Input, Arr(11))
            AddParameterToSQLCommand(myCommand, "@EXP3", SqlDbType.DateTime, 25, ParameterDirection.Input, Arr(12))

            AddParameterToSQLCommand(myCommand, "@NPC1", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(13))
            AddParameterToSQLCommand(myCommand, "@NPC2", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(14))
            AddParameterToSQLCommand(myCommand, "@NPC3", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(15))

            AddParameterToSQLCommand(myCommand, "@Species", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(16))
            AddParameterToSQLCommand(myCommand, "@ReminderType", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(17))
            AddParameterToSQLCommand(myCommand, "@Series", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(18))

            AddParameterToSQLCommand(myCommand, "@MailerTypeID", SqlDbType.VarChar, 10, ParameterDirection.Input, Arr(19))
            AddParameterToSQLCommand(myCommand, "@PrintRunSeries", SqlDbType.VarChar, 30, ParameterDirection.Input, Arr(20))
            AddParameterToSQLCommand(myCommand, "@TWC", SqlDbType.VarChar, 100, ParameterDirection.Input, Arr(21))
            AddParameterToSQLCommand(myCommand, "@PSC", SqlDbType.VarChar, 255, ParameterDirection.Input, Arr(22))

            ' Nov 25, 2008 1:49 PM
            AddParameterToSQLCommand(myCommand, "@PX", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(23))
            AddParameterToSQLCommand(myCommand, "@PXC", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(24))
            AddParameterToSQLCommand(myCommand, "@PXCCOLOR", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(25))
            AddParameterToSQLCommand(myCommand, "@CRC", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(26))

            myCommand.Connection = myConn
            myCommand.Transaction = myTrans

            ArrMessage(0) = "5"

            Try
                myCommand.ExecuteNonQuery()
                myTrans.Commit()
                ArrMessage(0) = "0"
                ArrMessage(1) = "0"

            Catch ex As Exception
                myTrans.Rollback()
                ArrMessage(0) = "-1"
                ArrMessage(1) = "ERROR: <br>" & ex.Message.ToString() & "<br>" & ex.Source.ToString()
            Finally
                myConn.Close()
                myConn.Dispose()
                myCommand.Dispose()
            End Try

            Return (ArrMessage)
        End Function

        Public Overrides Function Update_MatrixPstn_OneRecord(ByVal Arr As Array) As Boolean
            ' Execute SQL Command
            Dim myCommand As New SqlCommand

            AddParameterToSQLCommand(myCommand, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParameterToSQLCommand(myCommand, "@PK", SqlDbType.Int, 8, ParameterDirection.Input, Arr(0))

            AddParameterToSQLCommand(myCommand, "@Template", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(1))
            AddParameterToSQLCommand(myCommand, "@Body", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(2))

            AddParameterToSQLCommand(myCommand, "@P1", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(3))
            AddParameterToSQLCommand(myCommand, "@P2", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(4))
            AddParameterToSQLCommand(myCommand, "@P3", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(5))

            AddParameterToSQLCommand(myCommand, "@P1Disc", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(6))
            AddParameterToSQLCommand(myCommand, "@P2Disc", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(7))
            AddParameterToSQLCommand(myCommand, "@P3Disc", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(8))

            AddParameterToSQLCommand(myCommand, "@EXP1", SqlDbType.DateTime, 25, ParameterDirection.Input, Arr(9))
            AddParameterToSQLCommand(myCommand, "@EXP2", SqlDbType.DateTime, 25, ParameterDirection.Input, Arr(10))
            AddParameterToSQLCommand(myCommand, "@EXP3", SqlDbType.DateTime, 25, ParameterDirection.Input, Arr(11))

            AddParameterToSQLCommand(myCommand, "@NPC1", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(12))
            AddParameterToSQLCommand(myCommand, "@NPC2", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(13))
            AddParameterToSQLCommand(myCommand, "@NPC3", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(14))

            AddParameterToSQLCommand(myCommand, "@Species", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(15))
            AddParameterToSQLCommand(myCommand, "@ReminderType", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(16))
            AddParameterToSQLCommand(myCommand, "@Series", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(17))

            AddParameterToSQLCommand(myCommand, "@MailerTypeID", SqlDbType.VarChar, 10, ParameterDirection.Input, Arr(18))
            AddParameterToSQLCommand(myCommand, "@PrintRunSeries", SqlDbType.VarChar, 30, ParameterDirection.Input, Arr(19))
            AddParameterToSQLCommand(myCommand, "@TWC", SqlDbType.VarChar, 100, ParameterDirection.Input, Arr(20))
            AddParameterToSQLCommand(myCommand, "@PSC", SqlDbType.VarChar, 255, ParameterDirection.Input, Arr(21))

            ' Updete on Aug 04, 2008
            Dim i As Int32 = 900005
            Try
                i = Int32.Parse(Arr(22))
            Catch ex As Exception

            End Try
            AddParameterToSQLCommand(myCommand, "@PRACTICE_ID", SqlDbType.Int, 8, ParameterDirection.Input, i)

            ' Update Nov 25, 2008 12:58 PM
            AddParameterToSQLCommand(myCommand, "@PX", SqlDbType.VarChar, 50, ParameterDirection.Input, Arr(23))
            AddParameterToSQLCommand(myCommand, "@PXC", SqlDbType.VarChar, 255, ParameterDirection.Input, Arr(24))
            AddParameterToSQLCommand(myCommand, "@PXCCOLOR", SqlDbType.VarChar, 255, ParameterDirection.Input, Arr(25))
            AddParameterToSQLCommand(myCommand, "@CRC", SqlDbType.VarChar, 255, ParameterDirection.Input, Arr(26))


            SetCommandType(myCommand, CommandType.StoredProcedure, SP_UPDATE_MATRIX)

            ExecuteScalarCommand(myCommand)

            Dim returnValue As Integer = CInt(myCommand.Parameters("@ReturnValue").Value)
            Return (IIf(returnValue = 0, True, False))
        End Function

        Public Overrides Function Delete_OneMatrix(ByVal PK As Int32) As Boolean
            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If

            ' Execute SQL Command
            Dim myCommand As New SqlCommand

            AddParameterToSQLCommand(myCommand, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParameterToSQLCommand(myCommand, "@MatrixID", SqlDbType.Int, 0, ParameterDirection.Input, PK)

            SetCommandType(myCommand, CommandType.StoredProcedure, SP_DELETE_ONEMATRIX)
            ExecuteScalarCommand(myCommand)

            Dim returnValue As Integer = CInt(myCommand.Parameters("@ReturnValue").Value)
            Return (IIf(returnValue = 0, True, False))
        End Function

        Public Overrides Function Retrun_All_Matrix(ByVal practiceId As Int32, ByVal dropId As Int32) As DataView

            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If

            Dim conn As New SqlConnection(ConnectionString)
            Dim command As New SqlCommand(SP_GET_MATRIX_DROP, conn)

            AddParameterToSQLCommand(command, "@PracticeID", SqlDbType.Int, 0, ParameterDirection.Input, practiceId)
            AddParameterToSQLCommand(command, "@DropID", SqlDbType.Int, 0, ParameterDirection.Input, dropId)

            command.CommandType = CommandType.StoredProcedure
            Dim dad As New SqlDataAdapter(command)
            Dim dtblDropReminder As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtblDropReminder)
            Catch ex As Exception
                ' do something
                Dim dv As New DataView()
                Return dv
            Finally
                conn.Close()
                dad.Dispose()
                dtblDropReminder.Dispose()
            End Try

            Return (dtblDropReminder.DefaultView)
        End Function

        Public Overrides Function GetPracticesByDropID(ByVal DropId As Int32) As String
            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If

            Dim i As Integer = 0
            Dim sTemp As String = String.Empty
            Dim conn As New SqlConnection(ConnectionString)
            Dim command As New SqlCommand(SP_GET_PRACTICESBY_DROPID, conn)

            AddParameterToSQLCommand(command, "@DropID", SqlDbType.Int, 0, ParameterDirection.Input, DropId)
            command.CommandType = CommandType.StoredProcedure

            Try
                command.Connection.Open()
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader.HasRows Then
                    Do While reader.Read()
                        ' sTemp = "<tr><td>"
                        If i = 10 Then
                            sTemp = sTemp & "<br />"
                            i = 0
                        End If

                        sTemp = sTemp & "&nbsp;" & "<a href=""javascript:OpenEditPractice(" & reader.GetInt32(0) & "," & _
                        DropId.ToString & ");"">" & reader.GetInt32(0) & "</a>"
                        'Console.WriteLine(vbTab & "{0}" & vbTab & "{1}", reader.GetInt32(0), reader.GetString(1))
                        i = i + 1
                    Loop

                    reader.Close()
                Else
                    sTemp = "No Practices returned."
                End If

            Catch ex As Exception
                sTemp = ex.Message
            Finally
                command.Connection.Close()
            End Try

            Return (sTemp)
        End Function

        ' --------------------------------------------------------------------------------
        '  Return all records from Reminder Drop Table
        ' --------------------------------------------------------------------------------
        Public Overrides Function ReminderDrop_GetAll(ByVal MailerId As String) As DataView

            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If

            Dim conn As New SqlConnection(ConnectionString)
            Dim command As New SqlCommand(SP_GETALLRECORDS_REMINDERSDROP, conn)

            AddParameterToSQLCommand(command, "@MAILER_ID", SqlDbType.VarChar, 10, ParameterDirection.Input, MailerId)

            command.CommandType = CommandType.StoredProcedure
            Dim dad As New SqlDataAdapter(command)
            Dim dtblDropReminder As New DataTable()

            Try
                conn.Open()
                dad.Fill(dtblDropReminder)
            Catch ex As Exception
                ' do something
                Dim dv As New DataView()
                Return (dv)
            Finally
                If Not conn Is Nothing Then
                    conn.Close()
                    conn.Dispose()
                    conn = Nothing
                End If

                If Not dad Is Nothing Then dad.Dispose() : dad = Nothing
                'If Not dtblDropReminder Is Nothing Then dtblDropReminder.Dispose() : dtblDropReminder = Nothing
            End Try

            Return (dtblDropReminder.DefaultView)
        End Function

        Public Overrides Function GetOneReminderDropById(ByVal DropId As Int32) As ReminderDrop
            'If issueId <= DefaultValues.GetIssueIdMinValue() Then
            '    Throw New ArgumentOutOfRangeException("issueId")
            'End If
            ' Execute SQL Command
            Dim command As New SqlCommand

            AddParameterToSQLCommand(command, "@DropID", SqlDbType.Int, 0, ParameterDirection.Input, DropId)

            SetCommandType(command, CommandType.StoredProcedure, SP_GET_ONE_REMINDERDROP)
            Dim sqlData As New GenerateCollectionFromReader(AddressOf GenerateReminderDropCollectionFromReader)
            Dim stsCollection As ReminderDropCollection = CType(ExecuteReaderCmd(command, sqlData), ReminderDropCollection)

            If stsCollection.Count > 0 Then
                Return (stsCollection(0))
            Else
                Return (Nothing)
            End If
        End Function

        ' --------------------------------------------------------------------------------
        '  Users Authenticate
        ' --------------------------------------------------------------------------------
        Public Overrides Function User_Authenticate(ByVal username As String, ByVal password As String) As Boolean
            ' validate Parameters
            If username Is Nothing OrElse username.Length = 0 Then
                Throw New ArgumentOutOfRangeException("username")
            End If
            If password Is Nothing OrElse password.Length = 0 Then
                Throw New ArgumentOutOfRangeException("password")
            End If

            ' Execute SQL Command
            Dim myCommand As New SqlCommand

            AddParameterToSQLCommand(myCommand, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParameterToSQLCommand(myCommand, "@Username", SqlDbType.VarChar, 100, ParameterDirection.Input, username)
            AddParameterToSQLCommand(myCommand, "@Password", SqlDbType.VarChar, 25, ParameterDirection.Input, password)

            SetCommandType(myCommand, CommandType.StoredProcedure, SP_USER_AUTHENTICATE)
            ExecuteScalarCommand(myCommand)

            Dim returnValue As Integer = CInt(myCommand.Parameters("@ReturnValue").Value)
            Return (IIf(returnValue = 0, True, False))
        End Function 'Authenticate

        Public Overrides Function User_GetByUsername(ByVal username As String) As UserLogin

            ' validate input
            If username Is Nothing OrElse username.Length = 0 Then
                Throw New ArgumentOutOfRangeException("username")
            End If

            ' Execute SQL Command
            Dim MyCommand As New SqlCommand

            AddParameterToSQLCommand(MyCommand, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParameterToSQLCommand(MyCommand, "@Username", SqlDbType.VarChar, 100, ParameterDirection.Input, username)

            SetCommandType(MyCommand, CommandType.StoredProcedure, SP_USER_GETBYUSERNAME)
            Dim test As New GenerateCollectionFromReader(AddressOf ReturnUserLoginCollectionFromReader)

            Dim userCollection As UserLoginCollection = CType(ExecuteReaderCmd(MyCommand, test), UserLoginCollection)
            If userCollection.Count > 0 Then
                Return (userCollection(0))
            Else
                Return (Nothing)
            End If
        End Function

        ' --------------------------------------------------------------------------------
        '  Update a record in Reminder Drop Table
        ' --------------------------------------------------------------------------------
        Public Overrides Function ReminderDrop_UpdOneRecord(ByVal ReminderDropForUpdate As ReminderDrop) As Boolean

            If ReminderDropForUpdate Is Nothing Then
                Throw New ArgumentNullException("ReminderDropForUpdate")
            End If
            ' Execute SQL Command
            Dim myCommand As New SqlCommand

            AddParameterToSQLCommand(myCommand, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            AddParameterToSQLCommand(myCommand, "@DropId", SqlDbType.Int, 0, ParameterDirection.Input, ReminderDropForUpdate.DropId)

            AddParameterToSQLCommand(myCommand, "@DropDescription", SqlDbType.NVarChar, 50, ParameterDirection.Input, ReminderDropForUpdate.DropDescription)
            AddParameterToSQLCommand(myCommand, "@ActiveWeb", SqlDbType.Int, 0, ParameterDirection.Input, ReminderDropForUpdate.ActiveWeb)
            AddParameterToSQLCommand(myCommand, "@ActiveDrop", SqlDbType.Int, 0, ParameterDirection.Input, ReminderDropForUpdate.ActiveDrop)

            AddParameterToSQLCommand(myCommand, "@PullDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.PullDate)
            AddParameterToSQLCommand(myCommand, "@OnlineDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.OnLineDate)
            AddParameterToSQLCommand(myCommand, "@OfflineDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.OffLineDate)
            AddParameterToSQLCommand(myCommand, "@CStartDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.CStartDate)
            AddParameterToSQLCommand(myCommand, "@CEndDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.CEndDate)
            AddParameterToSQLCommand(myCommand, "@NR1StartDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.NR1StartDate)
            AddParameterToSQLCommand(myCommand, "@NR1EndDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.NR1EndDate)
            AddParameterToSQLCommand(myCommand, "@NR2StartDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.NR2StartDate)
            AddParameterToSQLCommand(myCommand, "@NR2EndDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.NR2EndDate)
            AddParameterToSQLCommand(myCommand, "@PrintRunDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.PrintRunDate)

            AddParameterToSQLCommand(myCommand, "@PrintRunSeries", SqlDbType.NVarChar, 10, ParameterDirection.Input, ReminderDropForUpdate.PrintRunSeries)
            AddParameterToSQLCommand(myCommand, "@MailerTypeID", SqlDbType.NVarChar, 10, ParameterDirection.Input, ReminderDropForUpdate.MailerTypeID)
            AddParameterToSQLCommand(myCommand, "@VM_NPC", SqlDbType.NVarChar, 3, ParameterDirection.Input, ReminderDropForUpdate.VM_NPC)

            AddParameterToSQLCommand(myCommand, "@Report_CStartDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.Rpt_CStartDate)
            AddParameterToSQLCommand(myCommand, "@Report_CEndDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.Rpt_CEndDate)
            AddParameterToSQLCommand(myCommand, "@Report_RCYStartDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.Rpt_RCYStartDate)
            AddParameterToSQLCommand(myCommand, "@Report_RCYEndDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.Rpt_RCYEndDate)
            AddParameterToSQLCommand(myCommand, "@Rpt_CEndShortDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.ReportShortDate)
            AddParameterToSQLCommand(myCommand, "@Report_NR3StartDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.Rpt_NR3StartDate)
            AddParameterToSQLCommand(myCommand, "@Report_NR3EndDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.Rpt_NR3EndDate)

            AddParameterToSQLCommand(myCommand, "@UserName", SqlDbType.VarChar, 100, ParameterDirection.Input, ReminderDropForUpdate.UserName)

            AddParameterToSQLCommand(myCommand, "@Exp1Date", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.Exp1Date)
            AddParameterToSQLCommand(myCommand, "@Exp2Date", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.Exp2Date)
            AddParameterToSQLCommand(myCommand, "@Exp3Date", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ReminderDropForUpdate.Exp3Date)


            SetCommandType(myCommand, CommandType.StoredProcedure, SP_UPDATE_REMINDERDROP)
            ExecuteScalarCommand(myCommand)
            Dim returnValue As Integer = CInt(myCommand.Parameters("@ReturnValue").Value)
            Return (IIf(returnValue = 0, True, False))
        End Function

        ' --------------------------------------------------------------------------------
        '  Add new record from Reminder Drop Table
        ' --------------------------------------------------------------------------------
        Public Overrides Function AddNew_RmrDrop(ByVal ArrParameters As Array) As String()
            Dim myConn As New SqlConnection(ConnectionString)
            Dim myTrans As Object
            Dim ArrMessage(1) As String

            'Dim myNullValueDate As SqlTypes.SqlDateTime

            myConn.Open()
            myTrans = myConn.BeginTransaction()

            Dim myCommand As New SqlCommand(SP_ADDNEW_REMINDERDROP, myConn)

            myCommand.CommandType = CommandType.StoredProcedure

            AddParameterToSQLCommand(myCommand, "@DropDescription", SqlDbType.NVarChar, 50, ParameterDirection.Input, ArrParameters(0))
            AddParameterToSQLCommand(myCommand, "@ActiveWeb", SqlDbType.Int, 5, ParameterDirection.Input, ArrParameters(1))
            AddParameterToSQLCommand(myCommand, "@ActiveDrop", SqlDbType.Int, 5, ParameterDirection.Input, ArrParameters(2))

            AddParameterToSQLCommand(myCommand, "@PullDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(3))
            AddParameterToSQLCommand(myCommand, "@OnlineDate", SqlDbType.SmallDateTime, 35, ParameterDirection.Input, ArrParameters(4))
            AddParameterToSQLCommand(myCommand, "@OfflineDate", SqlDbType.SmallDateTime, 35, ParameterDirection.Input, ArrParameters(5))
            AddParameterToSQLCommand(myCommand, "@CStartDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(6))
            AddParameterToSQLCommand(myCommand, "@CEndDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(7))

            AddParameterToSQLCommand(myCommand, "@NR1StartDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(8))
            AddParameterToSQLCommand(myCommand, "@NR1EndDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(9))
            AddParameterToSQLCommand(myCommand, "@NR2StartDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(10))
            AddParameterToSQLCommand(myCommand, "@NR2EndDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(11))
            AddParameterToSQLCommand(myCommand, "@PrintRunDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(12))
            AddParameterToSQLCommand(myCommand, "@PrintRunSeries", SqlDbType.NVarChar, 10, ParameterDirection.Input, ArrParameters(13))
            AddParameterToSQLCommand(myCommand, "@MailerTypeID", SqlDbType.NVarChar, 10, ParameterDirection.Input, ArrParameters(14))
            AddParameterToSQLCommand(myCommand, "@VM_NPC", SqlDbType.NVarChar, 3, ParameterDirection.Input, ArrParameters(15))

            AddParameterToSQLCommand(myCommand, "@Report_CStartDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(16))
            AddParameterToSQLCommand(myCommand, "@Report_CEndDate", SqlDbType.SmallDateTime, 35, ParameterDirection.Input, ArrParameters(17))
            AddParameterToSQLCommand(myCommand, "@Report_RCYStartDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(18))
            AddParameterToSQLCommand(myCommand, "@Report_RCYEndDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(19))
            AddParameterToSQLCommand(myCommand, "@Report_CEndDateShort", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(20))
            AddParameterToSQLCommand(myCommand, "@Report_NR3StartDate", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(21))
            AddParameterToSQLCommand(myCommand, "@Report_NR3EndDate", SqlDbType.SmallDateTime, 35, ParameterDirection.Input, ArrParameters(22))
            AddParameterToSQLCommand(myCommand, "@UserName", SqlDbType.VarChar, 100, ParameterDirection.Input, ArrParameters(23))

            AddParameterToSQLCommand(myCommand, "@Exp1Date", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(24))
            AddParameterToSQLCommand(myCommand, "@Exp2Date", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(25))
            AddParameterToSQLCommand(myCommand, "@Exp3Date", SqlDbType.SmallDateTime, 15, ParameterDirection.Input, ArrParameters(26))


            myCommand.Connection = myConn
            myCommand.Transaction = myTrans

            ArrMessage(0) = "5"

            Try
                myCommand.ExecuteNonQuery()
                myTrans.Commit()
                ArrMessage(0) = "0"
                ArrMessage(1) = "0"

            Catch ex As Exception
                myTrans.Rollback()
                ArrMessage(0) = "-1"
                ArrMessage(1) = "ERROR: <br>" & ex.Message.ToString() & "<br>" & ex.Source.ToString()
            Finally
                myConn.Close()
                myConn.Dispose()
                myCommand.Dispose()
            End Try

            Return (ArrMessage)
        End Function

        Private Sub ExecuteNonQuery(ByVal sqlCmd As SqlCommand, _
        Optional ByVal ConnStrNumber As Integer = 2)

            If sqlCmd Is Nothing Then
                Throw New ArgumentNullException("myCommand")
            End If

            Dim myConnString As String = String.Empty

            Select Case ConnStrNumber
                Case 1
                    myConnString = Me.HomeAgainConnectionString
                Case 2
                    myConnString = Me.ConnectionString
                Case 3
                    myConnString = Me.MSSConnectionString
                Case 4
                    myConnString = Me.LocalConnectionString
                Case 5
                    myConnString = Me.DB_CrmTrackerConnection
            End Select


            Dim i As Integer
            Dim cn As New SqlConnection(myConnString)

            Try
                ' increase the time out
                sqlCmd.CommandTimeout = 10000
                sqlCmd.Connection = cn
                cn.Open()
                i = sqlCmd.ExecuteNonQuery()
            Finally
                sqlCmd.Dispose()
                cn.Close()
                cn.Dispose()
            End Try
        End Sub

        Private Function ExecuteReaderCmd(ByVal sqlCmd As SqlCommand, _
            ByVal gcfr As GenerateCollectionFromReader, _
            Optional ByVal ConnStrNumber As Integer = 2) As CollectionBase

            Dim myConnString As String = String.Empty

            Select Case ConnStrNumber
                Case 1
                    myConnString = Me.HomeAgainConnectionString
                Case 2
                    myConnString = Me.ConnectionString
                Case 3
                    myConnString = Me.MSSConnectionString
                Case 4
                    myConnString = Me.DBVM_HomeAgainMarketing
                Case 5
                    myConnString = Me.DB_CrmTrackerConnection
            End Select


            If ConnectionString = String.Empty Then
                Throw New ArgumentOutOfRangeException("ConnectionString")
            End If
            If sqlCmd Is Nothing Then
                Throw New ArgumentNullException("sqlCmd")
            End If
            Dim cn As New SqlConnection(myConnString)
            Try
                sqlCmd.Connection = cn
                ' Command timeout
                sqlCmd.CommandTimeout = 10000
                cn.Open()
                Dim temp As CollectionBase = gcfr(sqlCmd.ExecuteReader())
                Return temp
            Finally
                cn.Dispose()
            End Try
        End Function 'ExecuteReaderCmd

        Private Function ExecuteScalarCommand2(ByVal sqlCmd As SqlCommand) As [Object]

            If sqlCmd Is Nothing Then
                Throw New ArgumentNullException("myCommand")
            End If
            Dim result As [Object] = Nothing

            Dim cn As New SqlConnection(Me.DBVM_HomeAgainMarketing)
            Try
                sqlCmd.Connection = cn
                cn.Open()
                result = sqlCmd.ExecuteScalar()
            Finally
                cn.Dispose()
            End Try

            Return (result)
        End Function ' ExecuteScalarCommand

        '-------------------------------------------
        '- Adding private Procedures and Functions -
        '-------------------------------------------
        Private Function ExecuteScalarCommand(ByVal sqlCmd As SqlCommand, _
            Optional ByVal ConnStringOrder As Integer = 2) As [Object]

            Dim myConnString As String = String.Empty

            ' Validate Command Properties
            'If ConnectionString = String.Empty Then
            '    Throw New ArgumentOutOfRangeException("ConnectionString")
            'End If
            Select Case ConnStringOrder
                Case 2
                    myConnString = Me.ConnectionString
                Case 5
                    myConnString = Me.DB_CrmTrackerConnection
            End Select

            If sqlCmd Is Nothing Then
                Throw New ArgumentNullException("myCommand")
            End If
            Dim result As [Object] = Nothing

            Dim cn As New SqlConnection(Me.ConnectionString)
            Try
                sqlCmd.Connection = cn
                cn.Open()
                result = sqlCmd.ExecuteScalar()
            Finally
                If Not cn Is Nothing Then
                    cn.Close()
                    cn.Dispose()
                End If

            End Try

            Return (result)
        End Function ' ExecuteScalarCommand

        Private Sub SetCommandType(ByVal sqlCmd As SqlCommand, ByVal cmdType As CommandType, ByVal cmdText As String)
            sqlCmd.CommandType = cmdType
            sqlCmd.CommandText = cmdText
        End Sub

        ' Add Parameters to SQL Commnad
        Private Sub AddParameterToSQLCommand(ByVal sqlCmd As SqlCommand, ByVal paramId As String, ByVal sqlType As SqlDbType, ByVal paramSize As Integer, ByVal paramDirection As ParameterDirection, ByVal paramvalue As Object)

            ' set null value for DateTime filed in SQL Server
            Dim NullValueDate As SqlTypes.SqlDateTime
            ' Validate Parameter Properties

            If sqlCmd Is Nothing Then
                Throw New ArgumentNullException("sqlCmd")
            End If
            If paramId = String.Empty Then
                Throw New ArgumentOutOfRangeException("paramId")
            End If
            ' Add Parameter
            Dim newSqlParam As New SqlParameter
            newSqlParam.ParameterName = paramId
            newSqlParam.SqlDbType = sqlType
            newSqlParam.Direction = paramDirection

            If paramSize > 0 Then
                newSqlParam.Size = paramSize
            End If
            If Not (paramvalue Is Nothing) Then

                ' check if the value is from type DateTime 
                ' when it is empty then set to NULL
                ' by defaukt it enters year 1/1/1900
                If (sqlType = SqlDbType.SmallDateTime Or sqlType = SqlDbType.DateTime) And (paramvalue.ToString().Length = 0) Then
                    paramvalue = NullValueDate
                End If

                newSqlParam.Value = paramvalue
            End If
            sqlCmd.Parameters.Add(newSqlParam)
        End Sub

    End Class
End Namespace
