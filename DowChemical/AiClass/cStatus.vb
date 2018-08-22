Imports System.Data.SqlClient

Public Class cStatus
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Public Function observStatus(ByVal Action_A As Integer, ByVal Enable_B As Boolean, ByVal Action_B As Integer, ByVal Enable_C As Boolean, ByVal Action_C As Integer) As Integer
        Dim Status As Integer = 1002        '-- In-progress

        If Enable_B = False And Enable_C = False Then
            If Action_A = 1001 Then Status = 1001       '-- Recognition
            If Action_A = 1002 Then Status = 1002       '--In-progress
            If Action_A = 1003 Then Status = 1003       '-- Complete
        Else
            If Enable_B = True And Enable_C = False Then
                If Action_A = Action_B Then
                    If Action_A = 1001 And Action_B = 1001 Then Status = 1001       '-- Recognition
                    If Action_A = 1003 And Action_B = 1003 Then Status = 1003       '-- Complete
                Else
                    Status = 1002                                                   '--In-progress
                End If
            Else
                If Action_A = Action_B And Action_B = Action_C Then
                    If Action_A = 1001 And Action_B = 1001 And Action_C = 1001 Then Status = 1001       '-- Recognition
                    If Action_A = 1003 And Action_B = 1003 And Action_C = 1003 Then Status = 1003       '-- Complete
                Else
                    Status = 1002                                                                       '--In-progress
                End If
            End If
        End If

        Return Status
    End Function

    Public Sub UpdRecordActive(ByVal recId As Integer, ByVal value As Boolean)
        Dim strUpd As String = "UPDATE tblRecord SET recActive = @recActive, timestamp = @timestamp WHERE recId = @recId"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@recId", SqlDbType.Int).Value = recId
        command.Parameters.Add("@recActive", SqlDbType.Bit).Value = value
        command.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = Now()
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Sub UpdRecordIsComplete(ByVal recId As Integer)
        Dim strSql As String = "SELECT noObserve, 
                                       (SELECT COUNT(*) FROM tblRecordDetail WHERE recId = @recId AND observComplete = 1001) AS IsAllRecogCount, 
                                       (SELECT COUNT(*) FROM tblRecordDetail WHERE recId = @recId AND observComplete = 1003) AS IsAllCompleteCount 
                                FROM tblRecord WHERE recId = @recId"

        Dim NoObserve As Integer = 0
        Dim ActionNumStatus As Integer = 1002
        Dim DataRead As SqlDataReader
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strSql, conn)
            command.Parameters.Add("@recId", SqlDbType.Int).Value = recId
            DataRead = command.ExecuteReader()

            If DataRead.HasRows() Then
                DataRead.Read()
                NoObserve = CInt(DataRead("noObserve"))
                Dim IsAllRecogCount As Integer = CInt(DataRead("IsAllRecogCount"))
                Dim IsAllCompleteCount As Integer = CInt(DataRead("IsAllCompleteCount"))
                If NoObserve = IsAllRecogCount Then
                    ActionNumStatus = 1001
                Else
                    If NoObserve = IsAllCompleteCount Then ActionNumStatus = 1003
                    If IsAllRecogCount + IsAllCompleteCount = NoObserve Then ActionNumStatus = 1003
                End If
            End If
        End Using

        Dim strUpd As String = "UPDATE tblRecord SET IsComplete = @IsComplete WHERE recId = @recId"
        Dim conn2 As New SqlConnection(ConnStr)
        conn2.Open()
        Dim command2 As New SqlCommand(strUpd, conn2)
        command2.Parameters.Add("@IsComplete", SqlDbType.Int).Value = ActionNumStatus
        command2.Parameters.Add("@recId", SqlDbType.Int).Value = recId
        Dim result As Integer = command2.ExecuteNonQuery()
        conn2.Close()
    End Sub

End Class
