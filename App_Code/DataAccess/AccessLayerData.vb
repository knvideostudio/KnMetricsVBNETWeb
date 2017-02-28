Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Collections
Imports System.Collections.Generic

Imports VeterinaryMetrics.BusinessLayer

' ----------------------------------------------
' Data Access Layer
' ----------------------------------------------

Namespace VeterinaryMetrics.AccessLayerData

    Public MustInherit Class AccessDataMainClass

        Private _connectionString As String


        Public ReadOnly Property StrConnectionRptsrvVetmet() As String
            Get
                Dim str As String = WebConfigurationManager.ConnectionStrings("StrConnectionRptsrvVetmet").ConnectionString
                If str Is Nothing OrElse str.Length <= 0 Then
                    Throw New ApplicationException("ConnectionString configuration " & _
                    "is missing from you web.config. It should contain  " & _
                    "<appSettings><add key=""StrConnectionRptsrvVetmet"" " & _
                    "value=""database=;server=localhost;"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        Public ReadOnly Property DB_CrmTrackerConnection() As String
            Get
                Dim str As String = WebConfigurationManager.ConnectionStrings("DB:CrmTrackerConnection").ConnectionString
                If str Is Nothing OrElse str.Length <= 0 Then
                    Throw New ApplicationException("ConnectionString configuration " & _
                    "is missing from you web.config. It should contain  " & _
                    "<appSettings><add key=""DB:CrmTrackerConnection"" " & _
                    "value=""database=;server=localhost;"" /></appSettings> ")
                Else
                    Return (str)
                End If
            End Get
        End Property

        ' Access Marketing Database
        ' Nov 20, 2007
        Public ReadOnly Property DBVM_HomeAgainMarketing() As String
            Get
                Dim str As String = WebConfigurationManager.ConnectionStrings("DBVM:HomeAgainMarketing").ConnectionString
                If str Is Nothing OrElse str.Length <= 0 Then
                    Throw New ApplicationException("ConnectionString configuration " & _
                    "is missing from you web.config. It should contain  " & _
                    "<appSettings><add key=""DBVM:HomeAgainMarketing"" " & _
                    "value=""database=;server=localhost;"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
        End Property

        Public Property ConnectionString() As String
            Get
                Dim str As String = WebConfigurationManager.ConnectionStrings("StrConnectionVetMetDB").ConnectionString
                If str Is Nothing OrElse str.Length <= 0 Then
                    Throw New ApplicationException("ConnectionString configuration is missing from you web.config. It should contain  <appSettings><add key=""StrConnectionVetMetDB"" value=""database=;server=localhost;"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
            Set(ByVal value As String)
                _connectionString = value
            End Set
        End Property

        Public Property HomeAgainConnectionString() As String
            Get
                Dim str As String = WebConfigurationManager.ConnectionStrings("StrConnectionHomeAgainDB").ConnectionString
                If str Is Nothing OrElse str.Length <= 0 Then
                    Throw New ApplicationException("ConnectionString configuration is missing from you web.config. It should contain  <appSettings><add key=""StrConnectionHomeAgainDB"" value=""database=;server=localhost"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
            Set(ByVal value As String)
                _connectionString = value
            End Set
        End Property

        Public Property MSSConnectionString() As String
            Get
                Dim str As String = WebConfigurationManager.ConnectionStrings("StrConnectionMSS").ConnectionString
                If str Is Nothing OrElse str.Length <= 0 Then
                    Throw New ApplicationException("ConnectionString configuration is missing from you web.config. It should contain  <appSettings><add key=""StrConnectionMSS"" value=""database=;server=localhost"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
            Set(ByVal value As String)
                _connectionString = value
            End Set
        End Property

        Public Property LocalConnectionString() As String
            Get
                Dim str As String = WebConfigurationManager.ConnectionStrings("StrConnectionLocalDB").ConnectionString
                If str Is Nothing OrElse str.Length <= 0 Then
                    Throw New ApplicationException("ConnectionString configuration is missing from you web.config. It should contain  <appSettings><add key=""StrConnectionLocalDB"" value=""database=;server=localhost"" /></appSettings> ")
                Else
                    Return str
                End If
            End Get
            Set(ByVal value As String)
                _connectionString = value
            End Set
        End Property

        ' will be used later
        Delegate Function GenerateCollectionFromReader(ByVal returnData As IDataReader) As CollectionBase

        ' return all records from Reminder Drop Table
        Public MustOverride Function GetPracticesByDropID(ByVal DropId As Int32) As String

        Public MustOverride Function Retrun_All_Matrix(ByVal practiceId As Int32, ByVal dropId As Int32) As DataView
        Public MustOverride Function Delete_OneMatrix(ByVal PK As Int32) As Boolean
        Public MustOverride Function Update_MatrixPstn_OneRecord(ByVal Arr As Array) As Boolean
        Public MustOverride Function AddNew_Matrix(ByVal Arr As Array) As String()
        Public MustOverride Function AddNew_Matrix_WithPractice(ByVal PracticeID As Int32, ByVal dropID As Int32) As Boolean

        ' Position Matrix
        Public MustOverride Function Delete_GroupOfMatrix(ByVal PracticeID As Int32, ByVal dropID As Int32) As Boolean
        Public MustOverride Function AddToHistory_Matrix(ByVal sMilerType As String) As Boolean
        'end Position Matrix

        ' add new record to Reminder Drop
        Public MustOverride Function AddNew_RmrDrop(ByVal ArrParameters As Array) As String()
        Public MustOverride Function ReminderDrop_UpdOneRecord(ByVal ReminderDropForUpdate As ReminderDrop) As Boolean
        ' return only one record of Reminder Drop table
        Public MustOverride Function GetOneReminderDropById(ByVal DropId As Int32) As ReminderDrop
        Public MustOverride Function ReminderDrop_GetAll(ByVal MailerId As String) As DataView

        ' User Tables
        Public MustOverride Function User_Authenticate(ByVal username As String, ByVal password As String) As Boolean
        Public MustOverride Function User_GetByUsername(ByVal username As String) As UserLogin
        ' login as Windows User account
        Public MustOverride Function User_GetIdentityName(ByVal sUsrName As String) As UserLogin


        ' ***************** for HomeAgain ********************************
        ' * Welcome Letter / Activation Mail
        ' ****************************************************************
        Public MustOverride Function WelcomeLetter_CurrentStatus(ByVal sFileName As String, ByVal iStatus As Integer, ByVal iBatch As Int32) As String()
        Public MustOverride Sub WelcomeLetter_ProcessMaster()
        Public MustOverride Sub WelcomeLetter_AppendToHistory(ByVal sFileName As String)
        Public MustOverride Function WelcomeLetterReturnAlreadySend() As DataView
        Public MustOverride Function WelcomeLetterAddFile(ByVal sFileName As String, ByVal sFileSize As String, ByVal iBatch As Int32) As Boolean

        Public MustOverride Function WelcomeLetterGetBatches() As WelcomeLetterCollections
        Public MustOverride Function WelcomeLetterSetPrintDate(ByVal batch As Int32, ByVal dt As DateTime, ByVal sPrintTotal As String) As Boolean
        Public MustOverride Function WelcomeLetterClearPrintDate(ByVal sMicroChip As String, ByVal batch As Int32) As Boolean

        Public MustOverride Function WelcomeLetterSetHistory(ByVal sFile As String, ByVal iBatch As Int32) As Boolean

        ' END Welcome Letter / Activation Mail
        ' **** BEGIN DIRECT MAIL 

        Public MustOverride Function DirectMail_PracticesAlreadySend() As DirectMailCollection
        Public MustOverride Function DirectMail_GetPractices() As DirectMailCollection
        Public MustOverride Sub DirectMail_AssignPractices(ByVal PracValues As String)
        Public MustOverride Function DirectMail_PracticesWait() As DirectMailCollection
        Public MustOverride Function DirectMail_Run(ByVal iBatch As Int32) As DirectMailCollection
        ' **** END  DIRECT MAIL

        ' BEGIN Code Mapping SECTION
        Public MustOverride Function RemainderMap_GetRightSide() As ReminderMapCollection
        Public MustOverride Function RemainderMap_GetPractices() As ReminderMapCollection
        Public MustOverride Function RemainderMap_GetMappedCode(ByVal iPractice As Int32) As DataView
        Public MustOverride Function RemainderMap_GetNotMappedCode() As DataView
        ' END Code Mapping SECTION

        ' PetId Project - Oct 01, 2007
        Public MustOverride Function PetId_GetClients(ByVal sClientName As String) As UserPetIdCollection
        Public MustOverride Sub PetIdAddReorder(ByVal sClientId As String, ByVal sPatientId As String, ByVal sPracticeId As String, ByVal sUserName As String)
        Public MustOverride Function PetIdRequestPending() As DataView
        ' End PetId

        ' New Security DB
        ' Have a look latter 
        ' IMPORTANT
        Public MustOverride Sub AddBinaryData(ByVal bData() As Byte)
        Public MustOverride Function ReadBinaryData() As String
        ' end new security DB

        ' CRM Functionality
        ' Public MustOverride Function CRM_GetParcticeOneInfo(ByVal CrmUniqueId As String) As String()
        ' Public MustOverride Function CRM_GetPIMS() As CrmViewCollection
        Public MustOverride Function GetStatesUS() As UsrStatesCollection
        Public MustOverride Function GetPracticeRegions() As UsrRegionsCollection
        Public MustOverride Function GetPracticeGroups() As PracticeGroupsCollection
        Public MustOverride Function GetHistoryGroups() As HistoryGroupsCollection
        Public MustOverride Function GetReminderBlocks() As ReminderBlockCollection
        ' end CRM Functionality

        ' DashBoard Function
        ' July 16 2008 - Randy
        Public MustOverride Function GetDashBoardIssues() As DashBoardCollection
        Public MustOverride Function GetDashBoardPProcess(ByVal iPractice As Int32) As DataView

        ' Admin Menu - List Personnel
        Public MustOverride Function GetAdminListPersonnel() As DataView
        Public MustOverride Function GetAdminPractices() As CnvtPracticeCollection
        Public MustOverride Function GetAdminConvertedPrct() As DataView

        ' Class Client Page
        Public MustOverride Function GetWebPractices() As CnvtPracticeCollection
        Public MustOverride Function GetAdminClientClass(ByVal iPractice As Int32, ByVal iMode As Integer) As DataView
        Public MustOverride Function GetCurrentHistoryData(ByVal iPractice As Int32, ByVal iMode As Integer) As Dictionary(Of String, Boolean)
        Public MustOverride Function SaveAdminClientClass(ByVal sUserId As String, ByVal sUniqueIDs As String) As Boolean

        Public MustOverride Function SaveAdminClientClass2(ByVal sUserId As String, ByVal iReminder As Integer, ByVal iUid As Int32) As Boolean

        ' Reports Nov 04, 2008 3:45
        Public MustOverride Function ReportCurrentProcessView() As DataView
        Public MustOverride Function ReportCurrentProcessHistoryView() As DataView
        Public MustOverride Function ReportQueueView() As DataView
        Public MustOverride Function ReportQueueHistoryView() As DataView
        Public MustOverride Function ReportDisplayErrorView() As DataView

        ' universal function 
        ' Nov 14, 2008 10:38 est
        Public MustOverride Function MultiPurposeView(ByVal iParameter As Integer, ByVal iPractice As Int32) As DataView
        Public MustOverride Function ReportCurrentProcessStatus() As String()
        Public MustOverride Function ReportCurrentProcess3View() As DataView

        ' new 2009 feb 16
        Public MustOverride Function GetMailers() As UsrMailerCollection

        ' new Feb 18 2009 3:40
        Public MustOverride Function GetUsers() As UsrTaskCollection
        Public MustOverride Function GetGroups() As UsrTaskCollection
        Public MustOverride Function GetTasks() As UsrTaskCollection
        Public MustOverride Function GetTaskByUser(ByVal User As String) As DataView
        Public MustOverride Function AddTaskNew(ByVal UserInfo() As String) As Boolean
        Public MustOverride Function DeleteTask(ByVal iTask As Int32) As Boolean

        ' Feb 19 2009 1:39
        Public MustOverride Function GetRemindersDrop() As ReminderBlockCollection
        Public MustOverride Function GetRemindersProcess(ByVal iDropId As Int32) As DataView
        Public MustOverride Function BuildRemindersExclusionList(ByVal sXML As String) As Boolean

        ' Reports Screen
        Public MustOverride Function GetReportsDate() As String()
        Public MustOverride Function GetReportsPractice() As CnvtPracticeCollection
        Public MustOverride Function GetReportsDateView(ByVal iPractice As Int32) As DataView


        Protected Function ReturnReminderDrop(ByVal returnData As IDataReader) As CollectionBase
            Dim dpColl As New ReminderBlockCollection

            While (returnData.Read())
                Dim dpBlk As New ReminderBlock(CInt(returnData("DropId")), CStr(returnData("DropDescription")))
                dpColl.Add(dpBlk)
            End While

            Return (dpColl)
        End Function

        Protected Function ReturnUserWinAuth(ByVal returnData As IDataReader) As CollectionBase
            Dim userCollection As New UserLoginCollection
            While returnData.Read()
                Dim newUser As New UserLogin(CInt(returnData("SKey")), _
                    CStr(returnData("UserName")), _
                    CStr(returnData("UserRoleName")), _
                    CStr(returnData("UserDisplayName")))
                userCollection.Add(newUser)
            End While

            Return userCollection
        End Function ' GenerateUsrAccessFromReader

        Protected Function ReturnTskTasks(ByVal returnData As IDataReader) As CollectionBase
            Dim myColl As New UsrTaskCollection

            While returnData.Read()
                Dim myView As New UsrTask(CInt(returnData("TaskId")), _
                                            CStr(returnData("TaskDesc")))
                myColl.Add(myView)
            End While

            Return (myColl)
        End Function

        Protected Function ReturnTskGroups(ByVal returnData As IDataReader) As CollectionBase
            Dim myColl As New UsrTaskCollection

            While returnData.Read()
                Dim myView As New UsrTask(CInt(returnData("GroupId")), _
                                            CStr(returnData("GroupDesc")))
                myColl.Add(myView)
            End While

            Return (myColl)
        End Function

        Protected Function ReturnTskUsers(ByVal returnData As IDataReader) As CollectionBase
            Dim myColl As New UsrTaskCollection

            While returnData.Read()
                Dim myView As New UsrTask(CInt(returnData("UserID")), _
                                            CStr(returnData("UserName")))
                myColl.Add(myView)
            End While

            Return (myColl)
        End Function

        Protected Function ReturnMailers(ByVal returnData As IDataReader) As CollectionBase
            Dim myColl As New UsrMailerCollection

            While returnData.Read()
                Dim myView As New UsrMailer(CStr(returnData("TypeId")), _
                                            CStr(returnData("TypeDesc")))
                myColl.Add(myView)
            End While

            Return (myColl)
        End Function

        Protected Function ReturnDbIssues(ByVal returnData As IDataReader) As CollectionBase
            Dim dbColl As New DashBoardCollection

            While returnData.Read()
                Dim db As New DashBoard(CInt(returnData("PracticeID")), _
                                            CStr(returnData("DropId")))
                dbColl.Add(db)
            End While

            Return (dbColl)
        End Function

        Protected Function ReturnBatchId(ByVal returnData As IDataReader) As CollectionBase
            Dim batColl As New WelcomeLetterCollections

            While returnData.Read()
                Dim batWel As New WelcomeLetter(CInt(returnData("BatchID")), _
                                            CStr(returnData("EndDate")))
                batColl.Add(batWel)
            End While
            Return batColl
        End Function

        Protected Function ReturnChgPractice(ByVal returnData As IDataReader) As CollectionBase
            Dim mColl As New CnvtPracticeCollection

            While returnData.Read()
                Dim pre As New CnvtPractice(CInt(returnData("PracticeID")), _
                                            CStr(returnData("PracticeName")))
                mColl.Add(pre)
            End While

            Return (mColl)
        End Function

        Protected Function ReturnReminderBlock(ByVal returnData As IDataReader) As CollectionBase
            Dim BlockColl As New ReminderBlockCollection
            While returnData.Read()
                Dim ReminderBlk As New ReminderBlock(CInt(returnData("BlockID")), _
                                            CStr(returnData("BlockDesc")))
                BlockColl.Add(ReminderBlk)
            End While
            Return BlockColl
        End Function

        Protected Function ReturnHistoryGroups(ByVal returnData As IDataReader) As CollectionBase
            Dim GroupColl As New HistoryGroupsCollection
            While returnData.Read()
                Dim PrGroup As New HistoryGroups(CInt(returnData("UniqueID")), _
                                            CStr(returnData("HistoryGroupName")))
                GroupColl.Add(PrGroup)
            End While
            Return GroupColl
        End Function

        Protected Function ReturnPracticeGroups(ByVal returnData As IDataReader) As CollectionBase
            Dim GroupColl As New PracticeGroupsCollection
            While returnData.Read()
                Dim PrGroup As New PracticeGroups(CInt(returnData("GroupID")), _
                                            CStr(returnData("GroupName")))
                GroupColl.Add(PrGroup)
            End While
            Return GroupColl
        End Function

        Protected Function ReturnRegions(ByVal returnData As IDataReader) As CollectionBase
            Dim RegionColl As New UsrRegionsCollection

            While returnData.Read()
                Dim UsrReg As New UsrRegions(CStr(returnData("VMRegionID")), _
                                            CStr(returnData("VMRegion")))
                RegionColl.Add(UsrReg)
            End While

            Return RegionColl
        End Function

        Protected Function ReturnStatesUS(ByVal returnData As IDataReader) As CollectionBase
            Dim CrmViewColl As New UsrStatesCollection

            While returnData.Read()
                Dim CrmView As New UsrStates(CStr(returnData("StateCode")), _
                                            CStr(returnData("StateName")))
                CrmViewColl.Add(CrmView)
            End While

            Return CrmViewColl
        End Function

        'Protected Function ReturnPIMS(ByVal returnData As IDataReader) As CollectionBase
        '    ' declare a collection not a Method
        '    Dim PimsColl As New CrmViewCollection

        '    While returnData.Read()
        '        'If IsNumeric(returnData("Batch")) Then
        '        '    iBatch = CInt(returnData("Batch"))
        '        'Else
        '        '    iBatch = 0
        '        'End If

        '        Dim crmview As New CrmView(CInt(returnData("PMSID")), _
        '                                    CStr(returnData("PMSDescr")))
        '        PimsColl.Add(crmview)
        '    End While

        '    Return PimsColl
        'End Function

        Protected Function ReturnPetIdClients(ByVal returnData As IDataReader) As CollectionBase
            ' declare a collection not a Method
            Dim PIdColl As New UserPetIdCollection
            Dim iBatch As Int32

            While returnData.Read()
                If IsNumeric(returnData("Batch")) Then
                    iBatch = CInt(returnData("Batch"))
                Else
                    iBatch = 0
                End If

                Dim UsrPetId As New UserPetId(CStr(returnData("ClientLastName")), _
                                            CStr(returnData("ClientFirstName")), _
                                            CStr(returnData("PracticeName")), _
                                            CInt(returnData("ClientID")), _
                                            CInt(returnData("PatientID")), _
                                            CStr(returnData("PracticeID")), _
                                            iBatch, _
                                            CStr(returnData("PatientName")))
                PIdColl.Add(UsrPetId)
            End While

            Return PIdColl
        End Function

        Protected Function ReturnRmPractices(ByVal returnData As IDataReader) As CollectionBase

            Dim RmMapColl As New ReminderMapCollection
            Dim sPracticeName As String = String.Empty

            While returnData.Read()
                If returnData("PracticeName").ToString().Length = 0 Then
                    sPracticeName = returnData("PracticeID").ToString()
                Else
                    sPracticeName = CStr(returnData("PracticeName"))
                End If

                Dim RmMap As New ReminderMap(sPracticeName, _
                                            CInt(returnData("PracticeID")))
                RmMapColl.Add(RmMap)
            End While

            Return RmMapColl
        End Function

        Protected Function ReturnRmRightSide(ByVal returnData As IDataReader) As CollectionBase
            ' declare a collection not a Method
            Dim RmMapColl As New ReminderMapCollection
            Dim sVMRDesc As String = ""

            While returnData.Read()
                If returnData("VMRDesc").ToString().Length = 0 Then
                    sVMRDesc = ""
                Else
                    sVMRDesc = CStr(returnData("VMRDesc"))
                End If

                Dim RmMap As New ReminderMap(CInt(returnData("RDescID")), _
                                            sVMRDesc)
                RmMapColl.Add(RmMap)
            End While

            Return RmMapColl
        End Function

        Protected Function ReturnDmExecute(ByVal returnData As IDataReader) As CollectionBase

            ' declare a collection not a Method
            Dim DrMailCollection As New DirectMailCollection
            Dim PracticeName As String = ""

            While returnData.Read()
                Dim DrMail As New DirectMail(CInt(returnData("BatchId")))
                DrMailCollection.Add(DrMail)
            End While

            Return DrMailCollection
        End Function

        Protected Function ReturnDmPrWait(ByVal returnData As IDataReader) As CollectionBase

            ' declare a collection not a Method
            Dim DrMailCollection As New DirectMailCollection
            Dim PracticeName As String = ""

            While returnData.Read()
                If returnData("PracticeName").ToString().Length = 0 Then
                    PracticeName = ""
                Else
                    PracticeName = CStr(returnData("PracticeName"))
                End If

                Dim DrMail As New DirectMail(CInt(returnData("PracticeID")), _
                                    PracticeName, _
                                    CStr(returnData("SignUpDate")), _
                                    CInt(returnData("BatchId")))

                DrMailCollection.Add(DrMail)
            End While

            Return DrMailCollection
        End Function

        Protected Function ReturnDmPractices(ByVal returnData As IDataReader) As CollectionBase

            ' declare a collection not a Method
            Dim DrMailCollection As New DirectMailCollection

            While returnData.Read()

                Dim DrMail As New DirectMail(CInt(returnData("PracticeID")), _
                                    CStr(returnData("PracticeName")))

                DrMailCollection.Add(DrMail)
            End While

            Return DrMailCollection
        End Function

        Protected Function ReturnDirectMailPrAlreadySend(ByVal returnData As IDataReader) As CollectionBase

            ' declare a collection not a Method
            Dim DrMailCollection As New DirectMailCollection

            While returnData.Read()

                Dim DrMail As New DirectMail(CInt(returnData("PracticeID")), _
                                    CStr(returnData("ProcessedDate")), _
                                    CStr(returnData("Account_NM")), _
                                    CStr(returnData("SpeciesName")), _
                                    CInt(returnData("Total")), _
                                    CInt(returnData("TotalReady")), _
                                    CInt(returnData("TotalCanine")), _
                                    CInt(returnData("TotalFeline")), _
                                    CStr(returnData("BatchID")))

                DrMailCollection.Add(DrMail)
            End While

            Return DrMailCollection
        End Function
        ' --------------------------------------------------
        '  Return collection from database
        ' --------------------------------------------------
        Protected Function ReturnUserLoginCollectionFromReader(ByVal returnData As IDataReader) As CollectionBase
            Dim userCollection As New UserLoginCollection
            While returnData.Read()
                Dim newUser As New UserLogin(CInt(returnData("UserId")), _
                                    CStr(returnData("Username")), _
                                    CStr(returnData("RoleName")), _
                                    CStr(returnData("UserEmail")), _
                                    CStr(returnData("UserDisplayName")), _
                                    CStr(returnData("UserPassword")), _
                                    CType(returnData("UserUniqueID"), Guid))


                'UserID, UserName, UserPassword, UserEmail, UserDisplayName, RoleName
                userCollection.Add(newUser)
            End While
            Return userCollection
        End Function


        Protected Function GenerateReminderDropCollectionFromReader(ByVal returnData As IDataReader) As CollectionBase
            Dim DropCollection As New ReminderDropCollection

            While returnData.Read()

                Dim NewDrop As New ReminderDrop(Int32.Parse(returnData("DropID")), _
                   CStr(returnData("DropDescription")), _
                   Integer.Parse(returnData("ActiveWeb")), _
                   Integer.Parse(returnData("ActiveDrop")), _
                   CStr(returnData("PullDate")), _
                   CStr(returnData("OnlineDate")), _
                   CStr(returnData("OfflineDate")), _
                   CStr(returnData("CStartDate")), _
                   CStr(returnData("CEndDate")), _
                   CStr(returnData("NR1StartDate")), _
                   CStr(returnData("NR1EndDate")), _
                   CStr(returnData("NR2StartDate")), _
                   CStr(returnData("NR2EndDate")), _
                   CStr(returnData("PrintRunDate")), _
                   CStr(returnData("PrintRunSeries")), _
                   CStr(returnData("MailerTypeID")), _
                   CStr(returnData("VM_NPC")), _
                   CStr(returnData("Report_CStartDate")), _
                   CStr(returnData("Report_CEndDate")), _
                   CStr(returnData("Report_RCYStartDate")), _
                   CStr(returnData("Report_RCYEndDate")), _
                   CStr(returnData("RptMyShortDate")), _
                   CStr(returnData("Report_NR3StartDate")), _
                   CStr(returnData("Report_NR3EndDate")), _
                   CStr(returnData("Exp1Date")), _
                   CStr(returnData("Exp2Date")), _
                   CStr(returnData("Exp3Date")))

                DropCollection.Add(NewDrop)
            End While

            Return DropCollection
        End Function

        Private Sub SetCommandType(ByVal sqlCmd As SqlCommand, ByVal cmdType As CommandType, ByVal cmdText As String)
            sqlCmd.CommandType = cmdType
            sqlCmd.CommandText = cmdText
        End Sub 'SetCommandType

    End Class


    ' --------------------------------------------------
    '  Access trhough ....
    ' ---------------------------------------------------

    Public Class AccessDataLayerBaseClassHelper

        Private Const SQL2000_NAME = "VeterinaryMetrics.AccessLayerData.AccessDataMainClass"

        Public Shared Function GetDataAccessLayer() As AccessDataMainClass
            Dim trp As Type = Type.GetType(GlobalsVar.DataAccessType, True)
            ' Throw an error if wrong base type

            If Not trp.BaseType Is Type.GetType(SQL2000_NAME) Then
                Throw New Exception("Data Access Layer does not inherit AccessDataMainClass!")
            End If

            Dim dc As AccessDataMainClass = CType(Activator.CreateInstance(trp), AccessDataMainClass)
            Return dc
        End Function

    End Class
End Namespace


