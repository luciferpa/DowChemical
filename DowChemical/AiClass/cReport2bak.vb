Imports System.Data.SqlClient

Public Class cReport2bak
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Private _sYear As Integer
    Private _sMonth As Integer
    Private _departId As Integer

    Public Sub New(ByVal yearId As Integer)
        _sYear = yearId
    End Sub

    Public Property Month As Integer
        Get
            Return _sMonth
        End Get
        Set(value As Integer)
            _sMonth = value
        End Set
    End Property
    Public Property Year As Integer
        Get
            Return _sYear
        End Get
        Set(value As Integer)
            _sYear = value
        End Set
    End Property

    Public Property Department As Integer
        Get
            Return _departId
        End Get
        Set(value As Integer)
            _departId = value
        End Set
    End Property

    Public Sub createThisYearAllDepartment(ByVal yearReport As Integer)
        Dim strSql As String = "SELECT departId FROM tblDepartment"
        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)

        Dim tbDepartment As New DataTable()
        conn.Open()
        Try
            adapter.Fill(tbDepartment)
        Finally
            conn.Close()
        End Try

        If Not chkThisYearDataExist(yearReport, 0) Then createThisYear(yearReport, 0)
        For Each row As DataRow In tbDepartment.Rows
            Dim departId As Integer = row.Field(Of Integer)(0)
            If Not chkThisYearDataExist(yearReport, departId) Then createThisYear(yearReport, departId)
        Next
    End Sub

    Public Sub createThisYear(ByVal yearReport As Integer, ByVal departId As Integer)
        Dim strIns As String = "INSERT INTO tblRpOverallOps(rpType, departId, month, monthDesc, year) 
                                SELECT 'g', @departId, '0', 'GOAL', @year UNION ALL 
                                SELECT 'm', @departId, '1', 'JAN', @year UNION ALL 
                                SELECT 'm', @departId, '2', 'FEB', @year UNION ALL 
                                SELECT 'm', @departId, '3', 'MAR', @year UNION ALL 
                                SELECT 'm', @departId, '4', 'APR', @year UNION ALL 
                                SELECT 'm', @departId, '5', 'MAY', @year UNION ALL 
                                SELECT 'm', @departId, '6', 'JUN', @year UNION ALL 
                                SELECT 'm', @departId, '7', 'JUL', @year UNION ALL 
                                SELECT 'm', @departId, '8', 'AUG', @year UNION ALL 
                                SELECT 'm', @departId, '9', 'SEP', @year UNION ALL 
                                SELECT 'm', @departId, '10', 'OCT', @year UNION ALL 
                                SELECT 'm', @departId, '11', 'NOV', @year UNION ALL 
                                SELECT 'm', @departId, '12', 'DEC', @year UNION ALL 
                                SELECT 'y', @departId, '13', 'YTD', @year"
        Dim conn As New SqlConnection(ConnStr)
        Dim command As New SqlCommand(strIns, conn)
        conn.Open()
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@year", SqlDbType.Int).Value = yearReport
        Dim result As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Sub createThisMonthEmpHistorical(ByVal yearReport As Integer, ByVal month As Integer)
        Dim strSql As String = "SELECT empId, departId FROM tblEmployee WHERE empEnable = 'true' AND empId > '100000'"
        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)

        Dim tbEmployee As New DataTable()
        conn.Open()
        Try
            adapter.Fill(tbEmployee)
        Finally
            conn.Close()
        End Try

        Dim strIns As String = "INSERT INTO tblRpEmpHistorical(departId, month, year, empId) "
        For Each row As DataRow In tbEmployee.Rows
            strIns = strIns & "SELECT '" & row.Field(Of Integer)(1).ToString & "', @month, @year, '" & row.Field(Of Integer)(0).ToString & "' UNION ALL "
        Next
        strIns = strIns.Substring(0, strIns.Length - 10)
        Dim conn2 As New SqlConnection(ConnStr)
        Dim command2 As New SqlCommand(strIns, conn2)
        conn2.Open()
        command2.Parameters.Add("@month", SqlDbType.Int).Value = month
        command2.Parameters.Add("@year", SqlDbType.Int).Value = yearReport
        Dim result As Integer = command2.ExecuteNonQuery()
        conn2.Close()
    End Sub
    Public Sub createSingleRecordEmpHistorical(ByVal yearReport As Integer, ByVal month As Integer, ByVal empId As Integer, ByVal departId As Integer)
        Dim strIns As String = "INSERT INTO tblRpEmpHistorical(departId, month, year, empId) VALUES(@departId, @month, @year, @empId)"
        Dim conn As New SqlConnection(ConnStr)
        Dim command As New SqlCommand(strIns, conn)
        conn.Open()
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
        command.Parameters.Add("@month", SqlDbType.Int).Value = month
        command.Parameters.Add("@year", SqlDbType.Int).Value = yearReport
        Dim result As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Function CountSubCategory(ByVal SubCateId As Integer, ByVal Month As Integer) As Integer
        Dim strSql As String = "SELECT tblRecordDetail.detailId, tblRecord.empId, tblEmployee.departId FROM tblRecord 
                                        INNER JOIN tblRecordDetail ON tblRecord.recId = tblRecordDetail.recId 
                                        INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
                                        WHERE tblRecordDetail.categorySub = '" & SubCateId.ToString & "' AND tblRecord.recActMonth = '" & Month.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"

        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)

        Dim tbSubCategorySelected As New DataTable()
        conn.Open()
        Try
            adapter.Fill(tbSubCategorySelected)
        Finally
            conn.Close()
        End Try

        Dim counter As Integer = 0
        For Each row As DataRow In tbSubCategorySelected.Rows
            Dim empId As Integer = row.Field(Of Integer)(1)
            Dim departId As Integer = row.Field(Of Integer)(2)
            counter = counter + 1

            'update tblRpOverallOps and tblRpEmpHistorical
            Select Case SubCateId
                Case 1013
                    updatePSCE_ContainmentLossEmpHis(empId, departId, Month, counter)
                    updatePSCE_ContainmentLossOverAll(departId, Month, counter)
                Case 1014
                    updatePSCE_PSNMEmpHis(empId, departId, Month, counter)
                    updatePSCE_PSNMOverAll(departId, Month, counter)
            End Select
        Next

        Return tbSubCategorySelected.Rows.Count
    End Function

    Public Function CountStatus(ByVal statusId As Integer, ByVal Month As Integer) As Integer
        Dim strSql As String = "SELECT tblRecordDetail.recId, tblRecord.empId, tblEmployee.departId FROM tblRecordDetail 
                                        INNER JOIN tblRecord On tblRecordDetail.recId = tblRecord.recId 
                                        INNER JOIN tblEmployee On tblRecord.empId = tblEmployee.empId 
                                        WHERE tblRecordDetail.observComplete = '" & statusId.ToString & "' AND tblRecord.recActMonth = '" & Month.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"

        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)

        Dim tbStatusSelected As New DataTable()
        conn.Open()
        Try
            adapter.Fill(tbStatusSelected)
        Finally
            conn.Close()
        End Try

        Dim counter As Integer = 0
        For Each row As DataRow In tbStatusSelected.Rows
            Dim empId As Integer = row.Field(Of Integer)(1)
            Dim departId As Integer = row.Field(Of Integer)(2)
            counter = counter + 1

            'update tblRpOverallOps and tblRpEmpHistorical
            Select Case statusId
                Case 1003   'Complete
                    updateActionCompletedEmpHis(empId, departId, Month, counter)
                    updateActionCompletedOverAll(departId, Month, counter)
                Case 1001   'Recognition
                    updateRecognitionEmpHis(empId, departId, Month, counter)
                    updateRecognitionOverAll(departId, Month, counter)
            End Select
        Next

        Return tbStatusSelected.Rows.Count
    End Function

    Public Function CountEmployee_fsfl() As Integer
        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblEmployee WHERE joblvCode = 'fsfl' AND empEnable = 'true' AND empId > 100000"
        If _departId <> 0 Then strSqlCount = strSqlCount & " AND departId = '" & _departId.ToString & "'"
        Dim count As Integer = 0
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(strSqlCount, connection)
            count = command.ExecuteScalar()
        End Using

        Return count
    End Function

    Public Function SumOfDuration_fsfl(ByVal Month As Integer) As Integer
        'Observer#1
        Dim strSql1 As String = "SELECT tblRecordDetail.detailId, tblRecord.empId, tblEmployee.departId, tblEmployee.joblvCode, tblRecord.durationValue FROM tblRecordDetail 
                                INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
                                WHERE tblRecord.recActMonth = '" & Month.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"
        Dim conn1 As New SqlConnection(ConnStr)
        Dim adapter1 As New SqlDataAdapter()
        adapter1.SelectCommand = New SqlCommand(strSql1, conn1)
        Dim tbDurationListObs1 As New DataTable()
        conn1.Open()
        Try
            adapter1.Fill(tbDurationListObs1)
        Finally
            conn1.Close()
        End Try

        'Other Observer
        Dim strSql2 As String = "SELECT tblRecordDetail.detailId, tblEmployee.empId, tblEmployee.departId, tblEmployee.joblvCode, tblRecord.durationValue FROM tblRecordDetail 
                                INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                INNER JOIN tblRecordOthEmpO ON tblRecord.recId = tblRecordOthEmpO.recId 
                                INNER JOIN tblEmployee ON tblRecordOthEmpO.empIdOth = tblEmployee.empId 
                                WHERE tblRecord.recActMonth = '" & Month.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"
        Dim conn2 As New SqlConnection(ConnStr)
        Dim adapter2 As New SqlDataAdapter()
        adapter2.SelectCommand = New SqlCommand(strSql2, conn2)
        Dim tbDurationListOthObs As New DataTable()
        conn2.Open()
        Try
            adapter2.Fill(tbDurationListOthObs)
        Finally
            conn2.Close()
        End Try

        tbDurationListObs1.Merge(tbDurationListOthObs)
        For Each row As DataRow In tbDurationListObs1.Rows
            Dim empId As Integer = row.Field(Of Integer)(1)
            Dim departId As Integer = row.Field(Of Integer)(2)
            resetLeadershipVisibility(empId, departId, Month)
        Next

        Dim SumDuration As Integer = 0
        Dim counter As Integer = 0
        For Each row As DataRow In tbDurationListObs1.Rows
            Dim empId As Integer = row.Field(Of Integer)(1)
            Dim departId As Integer = row.Field(Of Integer)(2)
            Dim joblevel As String = row.Field(Of String)(3)
            Dim duration As Integer = row.Field(Of Integer)(4)
            counter = counter + 1

            'update tblRpEmpHistorical
            updateLeadershipVisibilityEmpHis(empId, departId, Month, duration)

            'update tblRpOverallOps 
            If joblevel = "fsfl" Then
                SumDuration = SumDuration + duration
                updateLeadershipVisibilityOverAll(departId, Month, counter, duration)
            End If
        Next

        Return SumDuration
    End Function

    Private Sub resetLeadershipVisibility(ByVal empId As Integer, ByVal departId As Integer, ByVal sMonth As Integer)
        Dim strUpd As String = "UPDATE tblRpEmpHistorical SET leadershipVisibility = @leadershipVisibility WHERE empId = @empId AND departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
        command.Parameters.Add("@leadershipVisibility", SqlDbType.Int).Value = 0
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()

        Dim strUpd2 As String = "UPDATE tblRpOverallOps SET leadershipVisibility_fsfl = @leadershipVisibility_fsfl WHERE departId = @departId AND month = @month AND year = @year"
        Dim conn2 As New SqlConnection(ConnStr)
        conn2.Open()
        Dim command2 As New SqlCommand(strUpd2, conn2)
        command2.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command2.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command2.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command2.Parameters.Add("@leadershipVisibility_fsfl", SqlDbType.Int).Value = 0
        Dim errCode2 As Integer = command2.ExecuteNonQuery()
        conn2.Close()
    End Sub







    '====== Update Value ======
    Private Sub updatePSCE_ContainmentLossEmpHis(ByVal empId As Integer, ByVal departId As Integer, sMonth As Integer, ByVal counter As Integer)
        'check exist
        Dim existPSCE_ContainmentLossVal As Integer = chkRpHisExist("PSCE_ContainmentLoss", empId, departId, sMonth)
        If existPSCE_ContainmentLossVal = -1 Then
            createSingleRecordEmpHistorical(_sYear, sMonth, empId, departId)     'create personal data
            existPSCE_ContainmentLossVal = 0
        Else
            If counter = 1 Then existPSCE_ContainmentLossVal = 0
        End If

        Dim strUpd As String = "UPDATE tblRpEmpHistorical SET PSCE_ContainmentLoss = @PSCE_ContainmentLoss WHERE empId = @empId AND departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
        command.Parameters.Add("@PSCE_ContainmentLoss", SqlDbType.Int).Value = existPSCE_ContainmentLossVal + 1
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub
    Private Sub updatePSCE_ContainmentLossOverAll(ByVal departId As Integer, sMonth As Integer, ByVal counter As Integer)
        'check exist and get last value
        Dim existPSCE_ContainmentLossVal As Integer = chkRpOverallExist("PSCE_ContainmentLoss", departId, sMonth)
        If counter = 1 Then existPSCE_ContainmentLossVal = 0

        Dim strUpd As String = "UPDATE tblRpOverallOps SET PSCE_ContainmentLoss = @PSCE_ContainmentLoss WHERE departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@PSCE_ContainmentLoss", SqlDbType.Int).Value = existPSCE_ContainmentLossVal + 1
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub updatePSCE_PSNMEmpHis(ByVal empId As Integer, ByVal departId As Integer, sMonth As Integer, ByVal counter As Integer)
        'check exist
        Dim existPSCE_PSNMVal As Integer = chkRpHisExist("PSCE_PSNM", empId, departId, sMonth)
        If counter = 1 Then existPSCE_PSNMVal = 0       'don't check exist data and create personal data 

        Dim strUpd As String = "UPDATE tblRpEmpHistorical SET PSCE_PSNM = @PSCE_PSNM WHERE empId = @empId AND departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
        command.Parameters.Add("@PSCE_PSNM", SqlDbType.Int).Value = existPSCE_PSNMVal + 1
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub
    Private Sub updatePSCE_PSNMOverAll(ByVal departId As Integer, sMonth As Integer, ByVal counter As Integer)
        'check exist and get last value
        Dim existPSCE_PSNMVal As Integer = chkRpOverallExist("PSCE_PSNM", departId, sMonth)
        If counter = 1 Then existPSCE_PSNMVal = 0

        Dim strUpd As String = "UPDATE tblRpOverallOps SET PSCE_PSNM = @PSCE_PSNM WHERE departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@PSCE_PSNM", SqlDbType.Int).Value = existPSCE_PSNMVal + 1
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub updateActionCompletedEmpHis(ByVal empId As Integer, ByVal departId As Integer, sMonth As Integer, ByVal counter As Integer)
        'check exist
        Dim existActionCompletedVal As Integer = chkRpHisExist("actionCompleted", empId, departId, sMonth)
        If counter = 1 Then existActionCompletedVal = 0       'don't check exist data and create personal data 

        Dim strUpd As String = "UPDATE tblRpEmpHistorical SET actionCompleted = @actionCompleted WHERE empId = @empId AND departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
        command.Parameters.Add("@actionCompleted", SqlDbType.Int).Value = existActionCompletedVal + 1
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub
    Private Sub updateActionCompletedOverAll(ByVal departId As Integer, sMonth As Integer, ByVal counter As Integer)
        'check exist and get last value
        Dim existactionCompleteVal As Integer = chkRpOverallExist("actionComplete", departId, sMonth)
        If counter = 1 Then existactionCompleteVal = 0

        Dim strUpd As String = "UPDATE tblRpOverallOps SET actionComplete = @actionComplete WHERE departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@actionComplete", SqlDbType.Int).Value = existactionCompleteVal + 1
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub updateRecognitionEmpHis(ByVal empId As Integer, ByVal departId As Integer, sMonth As Integer, ByVal counter As Integer)
        'check exist
        Dim existRecognitionVal As Integer = chkRpHisExist("recognition", empId, departId, sMonth)
        If counter = 1 Then existRecognitionVal = 0       'don't check exist data and create personal data 

        Dim strUpd As String = "UPDATE tblRpEmpHistorical SET recognition = @recognition WHERE empId = @empId AND departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
        command.Parameters.Add("@recognition", SqlDbType.Int).Value = existRecognitionVal + 1
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub
    Private Sub updateRecognitionOverAll(ByVal departId As Integer, sMonth As Integer, ByVal counter As Integer)
        'check exist and get last value
        Dim existaAtionRecognitionVal As Integer = chkRpOverallExist("actionRecognition", departId, sMonth)
        If counter = 1 Then existaAtionRecognitionVal = 0

        Dim strUpd As String = "UPDATE tblRpOverallOps SET actionRecognition = @actionRecognition WHERE departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@actionRecognition", SqlDbType.Int).Value = existaAtionRecognitionVal + 1
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub updateLeadershipVisibilityEmpHis(ByVal empId As Integer, ByVal departId As Integer, sMonth As Integer, ByVal duration As Integer)
        'check exist
        Dim existLeadershipVisibilityVal As Integer = chkRpHisExist("leadershipVisibility", empId, departId, sMonth)

        Dim strUpd As String = "UPDATE tblRpEmpHistorical SET leadershipVisibility = @leadershipVisibility WHERE empId = @empId AND departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
        command.Parameters.Add("@leadershipVisibility", SqlDbType.Int).Value = existLeadershipVisibilityVal + duration
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub
    Private Sub updateLeadershipVisibilityOverAll(ByVal departId As Integer, sMonth As Integer, ByVal counter As Integer, ByVal duration As Integer)
        'check exist and get last value
        Dim existLeadershipVisibilityVal As Integer = chkRpOverallExist("leadershipVisibility_fsfl", departId, sMonth)
        If counter = 1 Then existLeadershipVisibilityVal = 0

        Dim strUpd As String = "UPDATE tblRpOverallOps SET leadershipVisibility_fsfl = @leadershipVisibility_fsfl WHERE departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@leadershipVisibility_fsfl", SqlDbType.Int).Value = existLeadershipVisibilityVal + duration
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub


    '====== Get Existing ======
    Public Function chkThisYearDataExist(ByVal yearReport As Integer, ByVal departId As Integer) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblRpOverallOps WHERE year = @year AND departId = @departId"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = yearReport
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        Dim count As Integer = command.ExecuteScalar()
        conn.Close()

        If count = 0 Then Return False Else Return True
    End Function

    Public Function chkEmpHistoricalExist(ByVal yearReport As Integer, ByVal month As Integer) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblRpEmpHistorical WHERE year = @year AND month = @month"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = yearReport
        command.Parameters.Add("@month", SqlDbType.Int).Value = month
        Dim count As Integer = command.ExecuteScalar()
        conn.Close()

        If count = 0 Then Return False Else Return True
    End Function

    Private Function chkRpHisExist(ByVal dBColumnName As String, ByVal empId As Integer, ByVal departId As Integer, ByVal monthReport As Integer) As Integer
        Dim strSql As String = "SELECT " & dBColumnName & " FROM tblRpEmpHistorical WHERE empId = @empId AND departId = @departId AND month = @month AND year = @year"

        Dim Count As Integer = 0
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strSql, conn)
            command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@month", SqlDbType.Int).Value = monthReport
            command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
            command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()

            If DataRead.HasRows() Then
                DataRead.Read()
                Count = CInt(DataRead(dBColumnName))
            Else
                Count = -1
            End If
        End Using

        Return Count
    End Function
    Private Function chkRpOverallExist(ByVal dBColumnName As String, ByVal departId As Integer, ByVal monthReport As Integer) As Integer
        Dim strSql As String = "SELECT " & dBColumnName & " FROM tblRpOverallOps WHERE departId = @departId AND month = @month AND  year = @year"

        Dim Count As Integer = 0
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strSql, conn)
            command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@month", SqlDbType.Int).Value = monthReport
            command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()

            If DataRead.HasRows() Then
                DataRead.Read()
                Count = CInt(DataRead(dBColumnName))
            Else
                Count = -1
            End If
        End Using

        Return Count
    End Function




    Public Function CountTotalActionNumber(ByVal Month As Integer) As Integer
        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblRecord WHERE tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year "
        If _departId <> 0 Then strSqlCount = strSqlCount & "AND tblRecord.departId = '" & _departId.ToString & "'"

        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@Month", SqlDbType.Int).Value = Month
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function
    Public Function CountTotalObserv(ByVal Month As Integer) As Integer
        Dim strSqlCount As String = "SELECT COUNT(tblRecord.recActNo) AS CountObserv FROM tblRecordDetail 
                                        INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                        WHERE tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year "
        If _departId <> 0 Then strSqlCount = strSqlCount & "AND tblRecord.departId = '" & _departId.ToString & "'"

        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@Month", SqlDbType.Int).Value = Month
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function

    Public Function CountCategory(ByVal CateId As Integer, ByVal Month As Integer) As Integer
        Dim strSqlCount As String = "SELECT COUNT(DISTINCT tblRecord.recActNo) AS countCate_wHRO FROM tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                    WHERE tblRecordDetail.category = @category AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year "
        If _departId <> 0 Then strSqlCount = strSqlCount & "AND tblRecord.departId = '" & _departId.ToString & "'"

        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@Month", SqlDbType.Int).Value = Month
            command.Parameters.Add("@category", SqlDbType.Int).Value = CateId
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function
    Public Function CountCategory_wHRO(ByVal CateId As Integer, ByVal Month As Integer) As Integer
        Dim strSqlCount As String = "SELECT COUNT(DISTINCT tblRecord.recActNo) AS countCate_wHRO FROM tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                    WHERE tblRecordDetail.category = @category AND tblRecordDetail.IsHRO = 'true' AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year "
        If _departId <> 0 Then strSqlCount = strSqlCount & "AND tblRecord.departId = '" & _departId.ToString & "'"

        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@Month", SqlDbType.Int).Value = Month
            command.Parameters.Add("@category", SqlDbType.Int).Value = CateId
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function

    Public Function CountFailurePoint(ByVal failurePoint As Integer, ByVal Month As Integer) As Integer
        Dim strSqlCount As String = "SELECT COUNT(DISTINCT(recActNo)) FROM tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                    WHERE tblRecordDetail.failurePoint = @failurePoint AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year "
        If _departId <> 0 Then strSqlCount = strSqlCount & "AND tblRecord.departId = '" & _departId.ToString & "'"

        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@Month", SqlDbType.Int).Value = Month
            command.Parameters.Add("@failurePoint", SqlDbType.Int).Value = failurePoint
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function



    Public Sub Count2ndEyeAllDepartAllMonth(ByVal thisMonth As Integer)
        If thisMonth > 1 Then
            Count2ndEyeAllDepart(thisMonth)
            Count2ndEyeAllDepart(thisMonth - 1)
        Else
            Count2ndEyeAllDepart(thisMonth)
        End If
    End Sub
    Public Sub Count2ndEyeAllDepart(ByVal sMonth As Integer)
        'Observer#1
        Dim strCount1 As String = "SELECT COUNT(*) AS Count2ndEyeObs1 
                                    FROM (SELECT DISTINCT tblRecord.recId, tblRecord.recActNo, tblRecord.oEmpCount, tblRecord.empId, tblDepartment.departId FROM tblRecordDetail 
                                        INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                        INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
                                        INNER JOIN tblDepartment ON tblEmployee.departId = tblDepartment.departId 
                                        WHERE tblRecordDetail.secondEye = 'true' AND tblRecord.recActMonth = '" & sMonth.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "') AS tbIsSecondEye"
        Dim countObs1 As Integer = 0
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(strCount1, connection)
            countObs1 = command.ExecuteScalar()
        End Using

        'Other Observer
        Dim strCount2 As String = "SELECT SUM(oEmpCount) AS Count2ndEyeOthObs 
                                    FROM (SELECT DISTINCT tblRecord.recId, tblRecord.recActNo, tblRecord.oEmpCount, tblRecord.empId, tblDepartment.departId FROM tblRecordDetail 
                                        INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                        INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
                                        INNER JOIN tblDepartment ON tblEmployee.departId = tblDepartment.departId 
                                        WHERE tblRecord.oEmpCount <> 0 AND tblRecordDetail.secondEye = 'true' AND tblRecord.recActMonth = '" & sMonth.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "') AS tbIsSecondEye"
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(strCount2, connection)
            countObs1 = countObs1 + command.ExecuteScalar()
        End Using

        update2ndEye(_sYear, sMonth, 0, countObs1)
    End Sub

    Public Function Count2ndEye(ByVal sMonth As Integer) As Integer
        'reset all value [secondEye]
        Dim strSql1 As String = "UPDATE tblRpOverallOps SET secondEye = '0' WHERE departId <> '0' AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strSql1, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()

        'Observer#1
        Dim strSql2 As String = "SELECT departId, COUNT(departId) AS departCount 
                                FROM (SELECT DISTINCT tblRecord.recId, tblRecord.recActNo, tblRecord.oEmpCount, tblRecord.empId, tblDepartment.departId FROM tblRecordDetail 
                                        INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                        INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
                                        INNER JOIN tblDepartment ON tblEmployee.departId = tblDepartment.departId 
                                        WHERE (tblRecord.recActMonth = '" & sMonth.ToString & "') AND (tblRecord.recActYear = '" & _sYear.ToString & "') AND (tblRecordDetail.secondEye = 'true')) AS DistinctActNo
                                GROUP BY departId"
        Dim conn2 As New SqlConnection(ConnStr)
        Dim adapter2 As New SqlDataAdapter()
        adapter2.SelectCommand = New SqlCommand(strSql2, conn2)

        Dim tbSecondEyeObserver1 As New DataTable()
        conn2.Open()
        Try
            adapter2.Fill(tbSecondEyeObserver1)
        Finally
            conn2.Close()
        End Try

        For Each row As DataRow In tbSecondEyeObserver1.Rows
            Dim departId As Integer = row.Field(Of Integer)(0)
            update2ndEye(_sYear, sMonth, departId, row.Field(Of Integer)(1))
        Next

        'Other Observer
        Dim strSql3 As String = "SELECT DISTINCT tblRecord.recId, tblRecord.recActNo, tblRecord.oEmpCount FROM tblRecordDetail 
                                        INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                        INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
                                        INNER JOIN tblDepartment ON tblEmployee.departId = tblDepartment.departId 
                                        WHERE tblRecord.oEmpCount <> 0 AND tblRecordDetail.secondEye = 'true' AND tblRecord.recActMonth = '" & sMonth.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"
        Dim conn3 As New SqlConnection(ConnStr)
        Dim adapter3 As New SqlDataAdapter()
        adapter3.SelectCommand = New SqlCommand(strSql3, conn3)

        Dim tbSecondEyeOtherObserver As New DataTable()
        conn3.Open()
        Try
            adapter3.Fill(tbSecondEyeOtherObserver)
        Finally
            conn3.Close()
        End Try

        For Each row As DataRow In tbSecondEyeOtherObserver.Rows
            Dim recId As Integer = row.Field(Of Integer)(0)
            Dim strSql4 As String = "SELECT departId, COUNT(departId) AS departCount 
                                        FROM (SELECT tblRecordOthEmpO.recId, tblRecordOthEmpO.recItem, tblRecordOthEmpO.empIdOth, tblEmployee.departId 
                                                FROM tblRecordOthEmpO INNER JOIN tblEmployee ON tblRecordOthEmpO.empIdOth = tblEmployee.empId 
                                                WHERE (tblRecordOthEmpO.recId = '" & recId.ToString & "')) AS DistinctRecord 
                                    GROUP BY departId"
            Dim conn4 As New SqlConnection(ConnStr)
            Dim adapter4 As New SqlDataAdapter()
            adapter4.SelectCommand = New SqlCommand(strSql4, conn4)

            Dim tbSecondEyeOthObsGroupDepart As New DataTable()
            conn4.Open()
            Try
                adapter4.Fill(tbSecondEyeOthObsGroupDepart)
            Finally
                conn4.Close()
            End Try

            For Each row4 As DataRow In tbSecondEyeOthObsGroupDepart.Rows
                Dim departId As Integer = row4.Field(Of Integer)(0)
                update2ndEyeAddValue(_sYear, sMonth, departId, row4.Field(Of Integer)(1))
            Next
        Next

        Return True
    End Function

    Private Sub update2ndEye(ByVal sYear As Integer, sMonth As Integer, ByVal departId As Integer, ByVal count As Integer)
        'check exist
        Dim secondEyeCount As Integer '= chkReportRecordExist(sYear, sMonth, departId)
        If secondEyeCount = -1 Then createThisYear(sYear, departId)

        Dim strUpd As String = "UPDATE tblRpOverallOps SET secondEye = @secondEye WHERE departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@secondEye", SqlDbType.Int).Value = count
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub
    Private Sub update2ndEyeAddValue(ByVal sYear As Integer, sMonth As Integer, ByVal departId As Integer, ByVal count As Integer)
        'check exist
        Dim secondEyeCount As Integer '= chkReportRecordExist(sYear, sMonth, departId)
        If secondEyeCount = -1 Then createThisYear(sYear, departId)

        Dim strUpd As String = "UPDATE tblRpOverallOps SET secondEye = @secondEye WHERE departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@secondEye", SqlDbType.Int).Value = secondEyeCount + count
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Function CountRecognition(ByVal sMonth As Integer) As Integer
        Dim strSqlCount As String = "SELECT COUNT(*) 
                                     FROM (SELECT recId, noObserve, SUM(CASE WHEN recognition = 1 THEN 1 ELSE 0 END) AS countRecog 
                                           FROM (SELECT tblRecord.recId, tblRecord.recActNo, tblRecord.empId, tblRecordDetail.recognition, tblRecord.noObserve 
                                                 FROM tblRecord INNER JOIN tblRecordDetail ON tblRecord.recId = tblRecordDetail.recId 
                                                 WHERE tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year "
        If _departId <> 0 Then strSqlCount = strSqlCount & "AND tblRecord.departId = '" & _departId.ToString & "'"
        strSqlCount = strSqlCount & ") AS rawData
                                           GROUP BY recId, noObserve) AS countCompare
                                     WHERE noObserve = countRecog "
        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@Month", SqlDbType.Int).Value = sMonth
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function
    '///////////////////////////////////////////////////////////

    Public Function totalActionNnumberCount() As Integer
        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId WHERE tblRecord.departId = @DepartId AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year"
        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@DepartId", SqlDbType.Int).Value = _departId
            command.Parameters.Add("@Month", SqlDbType.Int).Value = _sMonth
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function

    Public Function pLifeNearMissCount() As Integer
        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId WHERE tblRecord.departId = @DepartId AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year AND tblRecordDetail.categorySub = @categorySub"
        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@DepartId", SqlDbType.Int).Value = _departId
            command.Parameters.Add("@Month", SqlDbType.Int).Value = _sMonth
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@categorySub", SqlDbType.Int).Value = 1012
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function
    Public Function PSCE_ContainmentLossCount() As Integer
        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId WHERE tblRecord.departId = @DepartId AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year AND tblRecordDetail.categorySub = @categorySub"
        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@DepartId", SqlDbType.Int).Value = _departId
            command.Parameters.Add("@Month", SqlDbType.Int).Value = _sMonth
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@categorySub", SqlDbType.Int).Value = 1013      'Safety / PSCE_ContainmentLoss
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function
    Public Function PSCE_PSNMCount() As Integer
        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId WHERE tblRecord.departId = @DepartId AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year AND tblRecordDetail.categorySub = @categorySub"
        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@DepartId", SqlDbType.Int).Value = _departId
            command.Parameters.Add("@Month", SqlDbType.Int).Value = _sMonth
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@categorySub", SqlDbType.Int).Value = 1014
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function
    Public Function LeadershipVisibilityCount() As Integer
        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId WHERE tblRecord.departId = @DepartId AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year AND tblRecordDetail.categorySub = @categorySub"
        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@DepartId", SqlDbType.Int).Value = _departId
            command.Parameters.Add("@Month", SqlDbType.Int).Value = _sMonth
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function



    Public Function IndicatorsCount() As String
        Dim strSqlCount As String = "SELECT SUM(CASE WHEN tblRecordDetail.categorySub = @pLifeNearMiss THEN 1 END) AS TotalpLifeNearMiss, 
                                        SUM(CASE WHEN tblRecordDetail.categorySub = @PSCEContainmentLoss THEN 1 END) AS TotalPSCEContainmentLoss, 
                                        FROM tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId WHERE tblRecord.departId = @DepartId AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year"
        Dim CountStr As String = ""
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@DepartId", SqlDbType.Int).Value = _departId
            command.Parameters.Add("@Month", SqlDbType.Int).Value = _sMonth
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@pLifeNearMiss", SqlDbType.Int).Value = 1012
            command.Parameters.Add("@PSCEContainmentLoss", SqlDbType.Int).Value = 1013
            Dim DataRead As SqlDataReader = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                CountStr = CountStr & DataRead("TotalpLifeNearMiss") & "|" & DataRead("TotalPSCEContainmentLoss")
            End If
        End Using
        conn.Close()

        Return CountStr
    End Function
End Class
