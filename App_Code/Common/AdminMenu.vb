Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports VeterinaryMetrics.AccessLayerData

Namespace VeterinaryMetrics.BusinessLayer

    Public Class AdminTasks

        Public Sub New()
        End Sub ' New

        Public Shared Function GetListPersonel() As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetAdminListPersonnel())
        End Function ' List Personel

        ' Added on Sep 05, 2008
        ' Request by Randy
        ' get Practices
        Public Shared Function GetPractices() As CnvtPracticeCollection
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetWebPractices())
        End Function ' GetPractices

        Public Shared Function GetClientClass(ByVal iPractice As Int32, ByVal iMode As Integer) As System.Data.DataView
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetAdminClientClass(iPractice, iMode))
        End Function ' GetClientClass

        Public Shared Function SaveAdminClientClass(ByVal sUserId As String, ByVal sUniqueIDs As String) As Boolean
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.SaveAdminClientClass(sUserId, sUniqueIDs))
        End Function ' SaveAdminClientClass

        ' Added Sep 15, 2008
        Public Shared Function SaveAdmClientClass2(ByVal sUserId As String, ByVal iReminder As Integer, ByVal iUid As Int32) As Boolean
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.SaveAdminClientClass2(sUserId, iReminder, iUid))
        End Function ' SaveAdmClientClass2

        ' Added on Sep 30 2008
        Public Shared Function GetCurrentData(ByVal iPractice As Int32, ByVal iMode As Integer) As Dictionary(Of String, Boolean)
            Dim DataLayer As AccessDataMainClass = AccessDataLayerBaseClassHelper.GetDataAccessLayer()
            Return (DataLayer.GetCurrentHistoryData(iPractice, iMode))
        End Function

    End Class
End Namespace

