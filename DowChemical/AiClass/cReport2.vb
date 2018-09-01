Imports System.Data.SqlClient

Public Class cReport2
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Private _sYear As Integer
    Private _sMonth As Integer
    Private _departId As Integer

    Public Sub New(ByVal yearId As Integer)
        _sYear = yearId
    End Sub

    Public Property Year As Integer
        Get
            Return _sYear
        End Get
        Set(value As Integer)
            _sYear = value
        End Set
    End Property

    Public Property Month As Integer
        Get
            Return _sMonth
        End Get
        Set(value As Integer)
            _sMonth = value
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
        

    'tblRpEmpHistorical
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

    Public Sub createThisMonthEmpHistorical(ByVal sMonth As Integer)
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
        command2.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command2.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        Dim result As Integer = command2.ExecuteNonQuery()
        conn2.Close()
    End Sub

    Public Sub resetThisMonthEmpHistorical(ByVal sMonth As Integer)
        Dim strUpd As String = "UPDATE tblRpEmpHistorical SET 
                                    pLifeNearMiss = @pLifeNearMiss, 
                                    PSCE_ContainmentLoss = @PSCE_ContainmentLoss, 
                                    PSCE_PSNM = @PSCE_PSNM, 
                                    leadershipVisibility = @leadershipVisibility, 
                                    secondEye = @secondEye, 
                                    injuryNearMiss = @injuryNearMiss, 
                                    proactiveCompliance = @proactiveCompliance, 
                                    actionTotal = @actionTotal, 
                                    actionCompleted = @actionCompleted, 
                                    recognition = @recognition, 
                                    reliability_wHRO = @reliability_wHRO, 
                                    quality_wHRO = @quality_wHRO, 
                                    reliability = @reliability,
                                    procedureUsed = @procedureUsed,
                                    safety = @safety,
                                    LCS = @LCS
                                WHERE month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@pLifeNearMiss", SqlDbType.Int).Value = 0
        command.Parameters.Add("@PSCE_ContainmentLoss", SqlDbType.Int).Value = 0
        command.Parameters.Add("@PSCE_PSNM", SqlDbType.Int).Value = 0
        command.Parameters.Add("@leadershipVisibility", SqlDbType.Int).Value = 0
        command.Parameters.Add("@secondEye", SqlDbType.Int).Value = 0
        command.Parameters.Add("@injuryNearMiss", SqlDbType.Int).Value = 0
        command.Parameters.Add("@proactiveCompliance", SqlDbType.Int).Value = 0
        command.Parameters.Add("@actionTotal", SqlDbType.Int).Value = 0
        command.Parameters.Add("@actionCompleted", SqlDbType.Int).Value = 0
        command.Parameters.Add("@recognition", SqlDbType.Int).Value = 0
        command.Parameters.Add("@reliability_wHRO", SqlDbType.Int).Value = 0
        command.Parameters.Add("@quality_wHRO", SqlDbType.Int).Value = 0
        command.Parameters.Add("@reliability", SqlDbType.Int).Value = 0
        command.Parameters.Add("@procedureUsed", SqlDbType.Int).Value = 0
        command.Parameters.Add("@safety", SqlDbType.Int).Value = 0
        command.Parameters.Add("@LCS", SqlDbType.Int).Value = 0
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub createSingleRecordEmpHistorical(ByVal month As Integer, ByVal empId As Integer, ByVal departId As Integer)
        Dim strIns As String = "INSERT INTO tblRpEmpHistorical(departId, month, year, empId) VALUES(@departId, @month, @year, @empId)"
        Dim conn As New SqlConnection(ConnStr)
        Dim command As New SqlCommand(strIns, conn)
        conn.Open()
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
        command.Parameters.Add("@month", SqlDbType.Int).Value = month
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        Dim result As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    'tblRpOverallOps
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

        If Not chkRpOverallExist(0) Then createThisYear(yearReport, 0)
        For Each row As DataRow In tbDepartment.Rows
            Dim departId As Integer = row.Field(Of Integer)(0)
            If Not chkRpOverallExist(departId) Then createThisYear(yearReport, departId)
        Next
    End Sub

    Private Sub createThisYear(ByVal yearReport As Integer, ByVal departId As Integer)
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

    'actionnumber and observe document
    Public Function CountDocTotalActionNum(ByVal Month As Integer, Optional ByVal departId As Integer = 0) As Integer
        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblRecord WHERE tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year "
        If departId <> 0 Then strSqlCount = strSqlCount & String.Format("AND tblRecord.departId = '{0}'", departId.ToString)

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

    Public Function CountDocTotalObserv(ByVal sMonth As Integer, Optional ByVal departId As Integer = 0) As Integer
        Dim strSqlCount As String = "SELECT COUNT(tblRecord.recActNo) AS CountObserv FROM tblRecordDetail 
                                        INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                        WHERE tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year "
        If departId <> 0 Then strSqlCount = strSqlCount & String.Format("AND tblRecord.departId = '{0}'", departId.ToString)

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

    'cal and show data
    Public Function CountCategory(ByVal CateId As Integer, ByVal sMonth As Integer, ByVal IsHRO As Boolean) As Integer
        Dim strSql As String = "SELECT tblRecordDetail.detailId, tblRecord.empId, tblEmployee.departId FROM tblRecord 
                                            INNER JOIN tblRecordDetail ON tblRecord.recId = tblRecordDetail.recId 
                                            INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
                                            WHERE tblRecordDetail.category = '" & CateId.ToString & "' AND tblRecord.recActMonth = '" & sMonth.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "' "
        If IsHRO Then strSql = strSql & "AND tblRecordDetail.IsHRO = 'true'"

        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)

        Dim tbCategorySelected As New DataTable()
        conn.Open()
        Try
            adapter.Fill(tbCategorySelected)
        Finally
            conn.Close()
        End Try

        For Each row As DataRow In tbCategorySelected.Rows
            Dim empId As Integer = row.Field(Of Integer)(1)
            Dim departId As Integer = row.Field(Of Integer)(2)

            'update tblRpOverallOps and tblRpEmpHistorical
            Select Case CateId
                Case 1001
                    updateColumnNameEmpHis("safety", empId, departId, sMonth)       '***
                Case 1002
                    updateColumnNameEmpHis("quality_wHRO", empId, departId, sMonth)
                Case 1003
                    If IsHRO Then
                        updateColumnNameEmpHis("reliability_wHRO", empId, departId, sMonth)
                    Else
                        updateColumnNameEmpHis("reliability", empId, departId, sMonth)
                    End If
            End Select
        Next

        Return tbCategorySelected.Rows.Count
    End Function

    Public Function CountSubCategory(ByVal SubCateId As Integer, ByVal sMonth As Integer) As Integer
        Dim strSql As String = String.Format("SELECT tblRecordDetail.detailId, tblRecord.empId, tblEmployee.departId FROM tblRecord 
                                        INNER JOIN tblRecordDetail ON tblRecord.recId = tblRecordDetail.recId 
                                        INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
                                        WHERE tblRecordDetail.categorySub = {0} AND tblRecord.recActMonth = {1} AND tblRecord.recActYear = {2}", SubCateId.ToString, sMonth.ToString, _sYear.ToString)
        'Dim strSql As String = "SELECT tblRecordDetail.detailId, tblRecord.empId, tblEmployee.departId FROM tblRecord 
        '                                INNER JOIN tblRecordDetail ON tblRecord.recId = tblRecordDetail.recId 
        '                                INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
        '                                WHERE tblRecordDetail.categorySub = '" & SubCateId.ToString & "' AND tblRecord.recActMonth = '" & sMonth.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"
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

        For Each row As DataRow In tbSubCategorySelected.Rows
            Dim empId As Integer = row.Field(Of Integer)(1)
            Dim departId As Integer = row.Field(Of Integer)(2)

            'update tblRpOverallOps and tblRpEmpHistorical
            Select Case SubCateId
                Case 1010
                    updateColumnNameEmpHis("injuryNearMiss", empId, departId, sMonth)
                Case 1011
                    updateColumnNameEmpHis("LCS", empId, departId, sMonth)              '***
                Case 1013
                    updateColumnNameEmpHis("PSCE_ContainmentLoss", empId, departId, sMonth)
                Case 1014
                    updateColumnNameEmpHis("PSCE_PSNM", empId, departId, sMonth)
            End Select
        Next

        Return tbSubCategorySelected.Rows.Count
    End Function

    Public Function CountFailurePoint(ByVal failurePoint As Integer, ByVal sMonth As Integer) As Integer
        Dim strSql As String = "SELECT tblRecordDetail.detailId, tblRecord.empId, tblEmployee.departId FROM tblRecord 
                                            INNER JOIN tblRecordDetail ON tblRecord.recId = tblRecordDetail.recId 
                                            INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
                                            WHERE tblRecordDetail.failurePoint = '" & failurePoint.ToString & "' AND tblRecord.recActMonth = '" & sMonth.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"

        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)

        Dim tbFailurePointSelected As New DataTable()
        conn.Open()
        Try
            adapter.Fill(tbFailurePointSelected)
        Finally
            conn.Close()
        End Try

        For Each row As DataRow In tbFailurePointSelected.Rows
            Dim empId As Integer = row.Field(Of Integer)(1)
            Dim departId As Integer = row.Field(Of Integer)(2)

            'update tblRpOverallOps and tblRpEmpHistorical
            Select Case failurePoint
                Case 1003
                    updateColumnNameEmpHis("procedureUsed", empId, departId, sMonth)        '***
                Case 1014
                    updateColumnNameEmpHis("proactiveCompliance", empId, departId, sMonth)
            End Select
        Next

        Return tbFailurePointSelected.Rows.Count
    End Function

    Public Function CountStatusObserve(ByVal statusId As Integer, ByVal sMonth As Integer) As Integer
        Dim strSql As String = "SELECT tblRecordDetail.recId, tblRecord.empId, tblEmployee.departId FROM tblRecordDetail 
                                        INNER JOIN tblRecord On tblRecordDetail.recId = tblRecord.recId 
                                        INNER JOIN tblEmployee On tblRecord.empId = tblEmployee.empId "
        If statusId <> 1000 Then
            strSql = strSql & "WHERE tblRecordDetail.observComplete = '" & statusId.ToString & "' AND tblRecord.recActMonth = '" & sMonth.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"
        Else
            strSql = strSql & "WHERE tblRecord.recActMonth = '" & sMonth.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"
        End If

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

        For Each row As DataRow In tbStatusSelected.Rows
            Dim empId As Integer = row.Field(Of Integer)(1)
            Dim departId As Integer = row.Field(Of Integer)(2)

            'update tblRpOverallOps and tblRpEmpHistorical
            Select Case statusId
                Case 1000   'All Status
                    updateColumnNameEmpHis("actionTotal", empId, departId, sMonth)
                Case 1001   'Recognition
                    updateColumnNameEmpHis("recognition", empId, departId, sMonth)
                Case 1003   'Complete
                    updateColumnNameEmpHis("actionCompleted", empId, departId, sMonth)
            End Select
        Next

        Return tbStatusSelected.Rows.Count
    End Function

    Dim _countAllRecog As Integer
    Dim _countAllComplete As Integer
    Public Sub CountStatusActionNum(ByVal sMonth As Integer, Optional ByVal departId As Integer = 0)
        Dim strSql As String = ""
        If departId = 0 Then
            strSql = "SELECT (SELECT COUNT(IsComplete) AS countRecog FROM tblRecord
                              WHERE (tempFlag = 0) AND (recActive = 1) AND (IsComplete = 1001)
                              AND recActMonth = @recActMonth AND recActYear = @recActYear) AS AllRecog,
                             (SELECT COUNT(IsComplete) AS countComplete FROM tblRecord AS tblRecord_1
                              WHERE (tempFlag = 0) AND (recActive = 1) AND (IsComplete = 1003)
                              AND recActMonth = @recActMonth AND recActYear = @recActYear) AS AllComplete"
        Else
            strSql = "SELECT (SELECT COUNT(IsComplete) AS countRecog FROM tblRecord
                              WHERE (tempFlag = 0) AND (recActive = 1) AND (IsComplete = 1001)
                              AND recActMonth = @recActMonth AND recActYear = @recActYear AND departId = @departId) AS AllRecog,
                             (SELECT COUNT(IsComplete) AS countComplete FROM tblRecord AS tblRecord_1
                              WHERE (tempFlag = 0) AND (recActive = 1) AND (IsComplete = 1003)
                              AND recActMonth = @recActMonth AND recActYear = @recActYear AND departId = @departId) AS AllComplete"
        End If

        Dim DataRead As SqlDataReader
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strSql, conn)
            command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = sMonth
            command.Parameters.Add("@recActYear", SqlDbType.Int).Value = _sYear
            If departId <> 0 Then command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
            DataRead = command.ExecuteReader()

            If DataRead.HasRows() Then
                DataRead.Read()
                _countAllRecog = CInt(DataRead("AllRecog"))
                _countAllComplete = CInt(DataRead("AllComplete"))
            End If
        End Using
    End Sub

    Public ReadOnly Property CountActionNumRecog As Integer
        Get
            Return _countAllRecog
        End Get
    End Property
    Public ReadOnly Property CountActionNumComplete As Integer
        Get
            Return _countAllComplete
        End Get
    End Property

    Public Function SumOfDuration_fsfl(ByVal sMonth As Integer) As Integer
        'Observer#1
        Dim strSql1 As String = "SELECT tblRecord.recId AS IdxId, tblRecord.empId, tblEmployee.departId, tblEmployee.joblvCode, tblRecord.durationValue FROM tblRecord                                 
                                INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
                                WHERE tblRecord.recActMonth = '" & sMonth.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"
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
        Dim strSql2 As String = "SELECT tblRecordOthEmpO.recId AS idxId, tblRecordOthEmpO.empIdOth AS empId, tblEmployee.departId, tblEmployee.joblvCode, tblRecord.durationValue FROM tblRecordOthEmpO 
                                INNER JOIN tblRecord ON tblRecordOthEmpO.recId = tblRecord.recId 
                                INNER JOIN tblEmployee ON tblRecordOthEmpO.empIdOth = tblEmployee.empId 
                                WHERE tblRecord.recActMonth = '" & sMonth.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"
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

        Dim SumDuration As Integer = 0
        For Each row As DataRow In tbDurationListObs1.Rows
            Dim empId As Integer = row.Field(Of Integer)(1)
            Dim departId As Integer = row.Field(Of Integer)(2)
            Dim joblevel As String = row.Field(Of String)(3)
            Dim duration As Integer = row.Field(Of Integer)(4)

            'update tblRpEmpHistorical
            updateLeadershipVisibilityEmpHis(duration, empId, departId, sMonth)

            'update tblRpOverallOps 
            If joblevel = "fsfl" Then
                SumDuration = SumDuration + duration
            End If
        Next

        Return SumDuration
    End Function
    Public Function CountEmployee_fsfl(Optional ByVal departId As Integer = 0) As Integer
        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblEmployee WHERE joblvCode = 'fsfl' AND empEnable = 'true' AND empId > 100000"
        If departId <> 0 Then strSqlCount = strSqlCount & " AND departId = '" & departId.ToString & "'"
        Dim count As Integer = 0
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(strSqlCount, connection)
            count = command.ExecuteScalar()
        End Using

        Return count
    End Function

    Public Function CountSecondEye(ByVal sMonth As Integer) As Integer
        'Observer#1
        Dim strSql1 As String = String.Format("SELECT tblRecordDetail.detailId, tblRecord.empId, tblEmployee.departId FROM tblRecordDetail 
                                INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
                                WHERE tblRecordDetail.secondEye = 'True' 
                                AND tblRecord.recActMonth = {0} AND tblRecord.recActYear = {1}", sMonth.ToString, _sYear.ToString)
        'Dim strSql1 As String = "SELECT tblRecordDetail.detailId, tblRecord.empId, tblEmployee.departId FROM tblRecordDetail 
        '                        INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
        '                        INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
        '                        WHERE tblRecordDetail.secondEye = 'True' 
        '                        AND tblRecord.recActMonth = '" & sMonth.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"
        Dim conn1 As New SqlConnection(ConnStr)
        Dim adapter1 As New SqlDataAdapter()
        adapter1.SelectCommand = New SqlCommand(strSql1, conn1)
        Dim tbIsSecondEye As New DataTable()        'Obs#1
        conn1.Open()
        Try
            adapter1.Fill(tbIsSecondEye)
        Finally
            conn1.Close()
        End Try

        'Other Observer
        Dim strSql2 As String = String.Format("SELECT tblRecordDetail.detailId, tblEmployee.empId, tblEmployee.departId FROM tblRecordDetail 
                                INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                INNER JOIN tblRecordOthEmpO ON tblRecord.recId = tblRecordOthEmpO.recId 
                                INNER JOIN tblEmployee ON tblRecordOthEmpO.empIdOth = tblEmployee.empId 
                                WHERE (tblRecordDetail.secondEye = 'True') AND (tblRecord.oEmpCount > 0) 
                                AND tblRecord.recActMonth = {0} AND tblRecord.recActYear = {1}", sMonth.ToString, _sYear.ToString)
        'Dim strSql2 As String = "SELECT tblRecordDetail.detailId, tblEmployee.empId, tblEmployee.departId FROM tblRecordDetail 
        '                        INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
        '                        INNER JOIN tblRecordOthEmpO ON tblRecord.recId = tblRecordOthEmpO.recId 
        '                        INNER JOIN tblEmployee ON tblRecordOthEmpO.empIdOth = tblEmployee.empId 
        '                        WHERE (tblRecordDetail.secondEye = 'True') AND (tblRecord.oEmpCount > 0) 
        '                        AND tblRecord.recActMonth = '" & sMonth.ToString & "' AND tblRecord.recActYear = '" & _sYear.ToString & "'"
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

        tbIsSecondEye.Merge(tbDurationListOthObs)

        Dim SumDuration As Integer = 0
        For Each row As DataRow In tbIsSecondEye.Rows
            Dim empId As Integer = row.Field(Of Integer)(1)
            Dim departId As Integer = row.Field(Of Integer)(2)

            'update tblRpEmpHistorical
            updateColumnNameEmpHis("secondEye", empId, departId, sMonth)
        Next

        Return tbIsSecondEye.Rows.Count
    End Function

    Public Function CountRecogAll() As Integer
        Return _recogAllObserveCount
    End Function
    Public Function CountOffHour() As Integer
        Return _offHourCount
    End Function

    Private _recogAllObserveCount As Integer
    Private _offHourCount As Integer
    Public Sub ActionNumStatus(ByVal sMonth As Integer, Optional ByVal departId As Integer = 0)
        Dim strSql As String = String.Format("SELECT recId, recActDate, recActTime FROM tblRecord 
                                              WHERE tempFlag = 'false' AND recActMonth = '{0}' AND recActYear = '{1}' ", sMonth.ToString, _sYear.ToString)
        If departId <> 0 Then strSql = strSql & String.Format("AND departId = '{0}'", departId)

        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)
        Dim tbActionNumber As New DataTable()
        conn.Open()
        Try
            adapter.Fill(tbActionNumber)
        Finally
            conn.Close()
        End Try

        Dim recognitionCount As Integer = 0
        Dim offHourCount As Integer = 0
        Dim OffHour As New cOffHour
        For Each row As DataRow In tbActionNumber.Rows
            'count Recognition Opportunity Identify (cross function)
            Dim recId As Integer = row.Field(Of Integer)(0)
            Dim sumValueStatus As Integer = 0
            Dim strSqlSum As String = String.Format("SELECT SUM(observComplete) AS sumValStatus FROM tblRecordDetail WHERE (recId = '{0}')", recId.ToString)
            Using connection As New SqlConnection(ConnStr)
                connection.Open()
                Dim command As New SqlCommand(strSqlSum, connection)
                Dim result As Object = command.ExecuteScalar()

                If Not IsDBNull(result) Then sumValueStatus = CInt(result)
            End Using
            If IsRecogFromAllObserve(sumValueStatus) Then
                recognitionCount = recognitionCount + 1
            End If

            'check OffHour
            Dim sDate As Date = row.Field(Of Date)(1)
            Dim sTime As TimeSpan = row.Field(Of TimeSpan)(2)
            If OffHour.chkOffHour(sDate, sTime) Then
                offHourCount = offHourCount + 1
            End If
        Next

        _recogAllObserveCount = recognitionCount
        _offHourCount = offHourCount
    End Sub

    Public Sub CountOffHourByEmpId(Optional ByVal sMonth As Integer = 0, Optional ByVal empId As Integer = 0)
        Dim strSql As String

        If sMonth <> 0 Then
            strSql = String.Format("SELECT recId, recActDate, recActTime FROM tblRecord WHERE tempFlag = 'false' AND recActMonth = '{0}' AND recActYear = YEAR(getdate()) ", sMonth.ToString)
            strSql = strSql & String.Format("AND empId = '{0}'", empId)
        Else
            strSql = "SELECT recId, recActDate, recActTime FROM tblRecord WHERE tempFlag = 'false' AND recActYear = YEAR(getdate()) "
            strSql = strSql & String.Format("AND empId = '{0}'", empId)
        End If

        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)
        Dim tbActionNumber As New DataTable()
        conn.Open()
        Try
            adapter.Fill(tbActionNumber)
        Finally
            conn.Close()
        End Try

        Dim recognitionCount As Integer = 0
        Dim offHourCount As Integer = 0
        Dim OffHour As New cOffHour
        For Each row As DataRow In tbActionNumber.Rows
            'count Recognition Opportunity Identify (cross function)
            Dim recId As Integer = row.Field(Of Integer)(0)
            Dim sumValueStatus As Integer = 0
            Dim strSqlSum As String = String.Format("SELECT SUM(observComplete) AS sumValStatus FROM tblRecordDetail WHERE (recId = '{0}')", recId.ToString)
            Using connection As New SqlConnection(ConnStr)
                connection.Open()
                Dim command As New SqlCommand(strSqlSum, connection)
                Dim result As Object = command.ExecuteScalar()

                If Not IsDBNull(result) Then sumValueStatus = CInt(result)
            End Using
            If IsRecogFromAllObserve(sumValueStatus) Then
                recognitionCount = recognitionCount + 1
            End If

            'check OffHour
            Dim sDate As Date = row.Field(Of Date)(1)
            Dim sTime As TimeSpan = row.Field(Of TimeSpan)(2)
            If OffHour.chkOffHour(sDate, sTime) Then
                offHourCount = offHourCount + 1
            End If
        Next

        _recogAllObserveCount = recognitionCount
        _offHourCount = offHourCount
    End Sub

    Private Function IsRecogFromAllObserve(ByVal sumValueStatus As Integer) As Boolean
        Dim observeCount1 As Integer = sumValueStatus \ 1000
        Dim observeCount2 As Integer = sumValueStatus - (observeCount1 * 1000)

        If observeCount1 = observeCount2 Then Return True Else Return False
    End Function

    'Public Function CountTotalActionNum(ByVal Month As Integer, ByVal departId As Integer) As Integer
    '    Dim strSqlCount As String
    '    If departId <> 0 Then
    '        strSqlCount = "SELECT COUNT(*) FROM tblRecord INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
    '                                    WHERE tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year AND tblEmployee.departId = '" & departId.ToString & "'"
    '    Else
    '        strSqlCount = "SELECT COUNT(*) FROM tblRecord WHERE tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year"
    '    End If

    '    Dim count As Integer = 0
    '    Dim conn As New SqlConnection(ConnStr)
    '    conn.Open()
    '    Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
    '        command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
    '        command.Parameters.Add("@Month", SqlDbType.Int).Value = Month
    '        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
    '        count = command.ExecuteScalar()
    '    End Using
    '    conn.Close()

    '    Return count
    'End Function

    'Public Function CountTotalObserv(ByVal sMonth As Integer, Optional ByVal departId As Integer = 0) As Integer
    '    Dim strSqlCount As String = "SELECT COUNT(tblRecord.recActNo) AS CountObserv FROM tblRecordDetail 
    '                                    INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
    '                                    WHERE tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year "
    '    If departId <> 0 Then strSqlCount = strSqlCount & "AND tblRecord.departId = '" & departId.ToString & "'"
    '    Dim count As Integer = 0
    '    Dim conn As New SqlConnection(ConnStr)
    '    conn.Open()
    '    Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
    '        command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
    '        command.Parameters.Add("@Month", SqlDbType.Int).Value = sMonth
    '        count = command.ExecuteScalar()
    '    End Using
    '    conn.Close()

    '    Return count
    'End Function

    'update value
    Private Sub updateColumnNameEmpHis(ByVal dBColumnName As String, ByVal empId As Integer, ByVal departId As Integer, sMonth As Integer)
        'check exist
        Dim existVal As Integer = chkRpHisExist(dBColumnName, empId, departId, sMonth)
        If existVal = -1 Then
            createSingleRecordEmpHistorical(sMonth, empId, departId)     'create personal data
            existVal = 0
        End If

        Dim strUpd As String = "UPDATE tblRpEmpHistorical SET " & dBColumnName & " = @" & dBColumnName & " WHERE empId = @empId AND departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
        command.Parameters.Add("@" & dBColumnName, SqlDbType.Int).Value = existVal + 1
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub
    Private Sub updateLeadershipVisibilityEmpHis(ByVal duration As Integer, ByVal empId As Integer, ByVal departId As Integer, sMonth As Integer)
        'check exist
        Dim existVal As Integer = chkRpHisExist("leadershipVisibility", empId, departId, sMonth)
        If existVal = -1 Then
            createSingleRecordEmpHistorical(sMonth, empId, departId)     'create personal data
            existVal = 0
        End If

        Dim strUpd As String = "UPDATE tblRpEmpHistorical SET leadershipVisibility = @leadershipVisibility WHERE empId = @empId AND departId = @departId AND month = @month AND year = @year"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
        command.Parameters.Add("@leadershipVisibility", SqlDbType.Int).Value = existVal + duration
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    'get existing 
    Public Function chkRpOverallExist(ByVal departId As Integer) As Boolean
        Dim strSql As String = "SELECT rpId FROM tblRpOverallOps WHERE departId = @departId AND year = @year"

        Dim Count As Integer = 0
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strSql, conn)
            command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()

            If DataRead.HasRows() Then Return True Else Return False            
        End Using
    End Function

    Public Function chkRpHisExist(ByVal dBColumnName As String, ByVal empId As Integer, ByVal departId As Integer, ByVal monthReport As Integer) As Integer
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

    Public Function chkLastUpdate(ByRef currentTime As DateTime, Optional ByVal departId As Integer = 0) As DateTime
        Dim StrSql As String = "SELECT MAX(lastUpdate) AS last FROM tblRpOverallOps "
        If departId <> 0 Then StrSql = StrSql & String.Format("WHERE departId = {0}", departId.ToString)

        Dim lastUpdate As DateTime = currentTime
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(StrSql, connection)
            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()
            While (DataRead.Read)
                If DataRead("last") IsNot DBNull.Value Then lastUpdate = CDate(DataRead("last"))
            End While
        End Using
        Return lastUpdate
    End Function
    Public Function ChkLastUpdateByYearMonth(ByRef currentTime As DateTime, Optional ByVal sYear As Integer = 0, Optional ByVal sMonth As Integer = 0) As DateTime
        Dim strSql As String = "SELECT MAX(lastUpdate) AS last FROM tblRpOverallOps "

        If sYear = 0 And sMonth = 0 Then
            sYear = Now.Year
            sMonth = Now.Month
        End If
        strSql = strSql & String.Format("WHERE year = {0} AND month = {1}", sYear.ToString, sMonth.ToString)

        Dim lastUpdate As DateTime = currentTime
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(strSql, connection)
            Dim dataRead As SqlDataReader
            dataRead = command.ExecuteReader()
            While (dataRead.Read)
                If dataRead("last") IsNot DBNull.Value Then lastUpdate = CDate(dataRead("last"))
            End While
        End Using
        Return lastUpdate
    End Function
    'get department data from tblRpEmpHistorical
    Private _sumpLifeNearMiss As Integer
    Private _sumPSCE_ContainmentLoss As Integer
    Private _sumPSCE_PSNM As Integer
    Private _sumactionTotal As Integer
    Private _sumactionCompleted As Integer
    Private _sumrecognition As Integer
    Private _sumleadershipVisibility As Integer
    Private _sumproactiveCompliance As Integer
    Private _sumsecondEye As Integer
    Private _suminjuryNearMiss As Integer
    Private _sumreliability_wHRO As Integer
    Private _sumquality_wHRO As Integer
    Private _sumreliability As Integer
    Private _sumprocedureUsed As Integer
    Private _sumsafety As Integer
    Private _sumLCS As Integer
    'new
    Private _SumTotalAction As Integer
    Private _SumCompleted As Integer
    Private _SumInProgress As Integer
    Private _SumObservedPending As Integer
    Private _SumObservedCompleted As Integer
    Private _SumactionRecognition As Integer

    Public Sub getDepartmentData(ByVal departId As Integer, ByVal sMonth As Integer)
        Dim strSql As String = "SELECT SUM(pLifeNearMiss) AS sumpLifeNearMiss, SUM(PSCE_ContainmentLoss) AS sumPSCE_ContainmentLoss, SUM(PSCE_PSNM) AS sumPSCE_PSNM, 
                                    SUM(actionTotal) AS sumactionTotal, SUM(actionCompleted) AS sumactionCompleted, SUM(recognition) AS sumrecognition, SUM(leadershipVisibility) AS sumleadershipVisibility, 
                                    SUM(proactiveCompliance) AS sumproactiveCompliance, SUM(secondEye) AS sumsecondEye, SUM(injuryNearMiss) AS suminjuryNearMiss, 
                                    SUM(reliability_wHRO) AS sumreliability_wHRO, SUM(quality_wHRO) AS sumquality_wHRO, SUM(reliability) AS sumreliability,
                                    SUM(procedureUsed) AS sumprocedureUsed, SUM(safety) AS sumsafety, SUM(LCS) AS sumLCS
                                    FROM tblRpEmpHistorical
                                    WHERE (departId = @departId) And (month = @month) And (year = @year)"

        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strSql, conn)
            command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
            command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
            command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear

            Dim DataRead As SqlDataReader = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                If DataRead("sumpLifeNearMiss") IsNot DBNull.Value Then
                    _sumpLifeNearMiss = DataRead("sumpLifeNearMiss")
                    _sumPSCE_ContainmentLoss = DataRead("sumPSCE_ContainmentLoss")
                    _sumPSCE_PSNM = DataRead("sumPSCE_PSNM")
                    _sumactionTotal = DataRead("sumactionTotal")
                    _sumactionCompleted = DataRead("sumactionCompleted")
                    _sumrecognition = DataRead("sumrecognition")
                    _sumleadershipVisibility = getDepartmentData_fsfl(departId, sMonth)
                    _sumproactiveCompliance = DataRead("sumproactiveCompliance")
                    _sumsecondEye = DataRead("sumsecondEye")
                    _suminjuryNearMiss = DataRead("suminjuryNearMiss")
                    _sumreliability_wHRO = DataRead("sumreliability_wHRO")
                    _sumquality_wHRO = DataRead("sumquality_wHRO")
                    _sumreliability = DataRead("sumreliability")
                    _sumprocedureUsed = DataRead("sumprocedureUsed")
                    _sumsafety = DataRead("sumsafety")
                    _sumLCS = DataRead("sumLCS")
                Else
                    _sumpLifeNearMiss = 0
                    _sumPSCE_ContainmentLoss = 0
                    _sumPSCE_PSNM = 0
                    _sumactionTotal = 0
                    _sumactionCompleted = 0
                    _sumrecognition = 0
                    _sumleadershipVisibility = 0
                    _sumproactiveCompliance = 0
                    _sumsecondEye = 0
                    _suminjuryNearMiss = 0
                    _sumreliability_wHRO = 0
                    _sumquality_wHRO = 0
                    _sumreliability = 0
                    _sumprocedureUsed = 0
                    _sumsafety = 0
                    _sumLCS = 0
                End If
            End If
        End Using
    End Sub
    Public Function getDepartmentData_fsfl(ByVal departId As Integer, ByVal sMonth As Integer) As Integer
        Dim strSql As String = "SELECT SUM(leadershipVisibility) AS sumleadershipVisibility FROM tblRpEmpHistorical 
                                    INNER JOIN tblEmployee ON tblRpEmpHistorical.empId = tblEmployee.empId
                                    WHERE tblRpEmpHistorical.month = @month And tblRpEmpHistorical.year = @year 
                                    AND tblRpEmpHistorical.departId = @departId 
                                    AND tblEmployee.joblvCode = 'fsfl'"
        Dim SumLeadershipVisibility As Integer = 0
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strSql, conn)
            command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
            command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
            command.Parameters.Add("@year", SqlDbType.Int).Value = _sYear

            Dim DataRead As SqlDataReader = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                If DataRead("sumleadershipVisibility") IsNot DBNull.Value Then
                    SumLeadershipVisibility = DataRead("sumleadershipVisibility")
                End If
            End If
        End Using

        Return SumLeadershipVisibility
    End Function

    Public Sub getObserverMyActionStatus(ByVal empId As Integer)
        Dim strSql As String = ""
        strSql = "
                select 1 as rpId, a.empId, a.departId , YEAR(getdate()) as year, 'YTD' as monthDesc,
                (SELECT COUNT(*) FROM tblRecord b WHERE b.recActYear = YEAR(getdate()) and b.empId = a.empId) as totalActionNumber,
                (SELECT count(*) FROM tblRecord d3 WHERE d3.recActYear = YEAR(getdate()) and d3.IsComplete = 1003 and d3.empId = a.empId) as actionComplete,
                (SELECT count(*) FROM tblRecord d4 WHERE d4.recActYear = YEAR(getdate()) and d4.IsComplete = 1001 and d4.empId = a.empId) as actionRecognition
                from tblEmployee a
                where a.empId = @empId
                "
        Dim DataRead As SqlDataReader
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strSql, conn)
            command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
            DataRead = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                If DataRead("totalActionNumber") IsNot DBNull.Value Then
                    _SumTotalAction = CInt(DataRead("totalActionNumber"))
                Else
                    _SumTotalAction = 0
                End If
                If DataRead("actionComplete") IsNot DBNull.Value Then
                    _SumCompleted = CInt(DataRead("actionComplete"))
                Else
                    _SumCompleted = 0
                End If
                If DataRead("actionRecognition") IsNot DBNull.Value Then
                    _SumactionRecognition = CInt(DataRead("actionRecognition"))
                Else
                    _SumactionRecognition = 0
                End If
                _SumInProgress = _SumTotalAction - _SumCompleted - _SumactionRecognition
            End If
        End Using
    End Sub

    Public Sub getFollowupMyResponsibleOwner(ByVal empId As Integer)
        Dim strSql As String = ""
        strSql = "
                    select sum(pending) as totalpending, sum(completed) as totalcompleted
                    from
                    (
	                    SELECT a.recId, a.IsComplete,
	                    case when a.IsComplete = 1002 then 1 else 0 end as pending,
	                    case when a.IsComplete = 1003 then 1 else 0 end as completed
	                    FROM tblRecord a
	                    join tblRecordDetail b on a.recId = b.recId 
	                    where a.recActYear = YEAR(getdate()) and (b.proposeRespPerson_A = @empId or b.proposeRespPerson_B = @empId or b.proposeRespPerson_C = @empId)
	                    group by a.recId, a.IsComplete
                    ) AAA
                "
        Dim DataRead As SqlDataReader
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strSql, conn)
            command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
            DataRead = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                If DataRead("totalpending") IsNot DBNull.Value Then
                    _SumObservedPending = CInt(DataRead("totalpending"))
                Else
                    _SumObservedPending = 0
                End If
                If DataRead("totalcompleted") IsNot DBNull.Value Then
                    _SumObservedCompleted = CInt(DataRead("totalcompleted"))
                Else
                    _SumObservedCompleted = 0
                End If
            End If
        End Using
    End Sub

    Public Function getSumpLifeNearMiss() As Integer
        Return _sumpLifeNearMiss
    End Function
    Public Function getSumPSCE_ContainmentLoss() As Integer
        Return _sumPSCE_ContainmentLoss
    End Function
    Public Function getSumPSCE_PSNM() As Integer
        Return _sumPSCE_PSNM
    End Function
    Public Function getSumactionTotal() As Integer
        Return _sumactionTotal
    End Function
    Public Function getSumactionCompleted() As Integer
        Return _sumactionCompleted
    End Function
    Public Function getSumrecognition() As Integer
        Return _sumrecognition
    End Function
    Public Function getSumleadershipVisibility() As Integer
        Return _sumleadershipVisibility
    End Function
    Public Function getSumproactiveCompliance() As Integer
        Return _sumproactiveCompliance
    End Function
    Public Function getSumsecondEye() As Integer
        Return _sumsecondEye
    End Function
    Public Function getSuminjuryNearMiss() As Integer
        Return _suminjuryNearMiss
    End Function
    Public Function getSumreliability_wHRO() As Integer
        Return _sumreliability_wHRO
    End Function
    Public Function getSumquality_wHRO() As Integer
        Return _sumquality_wHRO
    End Function
    Public Function getSumreliability() As Integer
        Return _sumreliability
    End Function
    Public Function getSumprocedureUsed() As Integer
        Return _sumprocedureUsed
    End Function
    Public Function getSumsafety() As Integer
        Return _sumsafety
    End Function
    Public Function getSumLCS() As Integer
        Return _sumLCS
    End Function

    'new
    Public Function getSumTotalAction() As Integer
        Return _SumTotalAction
    End Function
    Public Function getSumCompleted() As Integer
        Return _SumCompleted
    End Function
    Public Function getSumInProgress() As Integer
        Return _SumInProgress
    End Function
    Public Function getSumObservedPending() As Integer
        Return _SumObservedPending
    End Function
    Public Function getSumObservedCompleted() As Integer
        Return _SumObservedCompleted
    End Function


End Class
