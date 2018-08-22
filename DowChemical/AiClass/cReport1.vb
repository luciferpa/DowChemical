Imports System.Data.SqlClient

Public Class cReport1
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Private _departId As Integer
    Private _sMonth As Integer
    Private _sYear As Integer


    Public Sub New(ByVal yearId As Integer, ByVal departId As Integer)
        _sYear = yearId
        _departId = departId
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

    Private Function chkReportRecordExist(ByVal yearReport As Integer, ByVal monthReport As Integer, ByVal departId As Integer) As Integer
        Dim strSql As String = "SELECT secondEye FROM tblRpOverallOps WHERE year = @year AND month = @month AND departId = @departId"

        Dim secondEyeCount As Integer = 0
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strSql, conn)
            command.Parameters.Add("@year", SqlDbType.Int).Value = yearReport
            command.Parameters.Add("@month", SqlDbType.Int).Value = monthReport
            command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()

            If DataRead.HasRows() Then
                DataRead.Read()
                secondEyeCount = CInt(DataRead("secondEye"))
            Else
                secondEyeCount = -1
            End If
        End Using

        Return secondEyeCount
    End Function

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

    Public Function CountTotalAction(ByVal Month As Integer) As Integer
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
    Public Function CountCategory(ByVal CateId As Integer, ByVal sMonth As Integer) As Integer
        Dim strSqlCount As String = "SELECT COUNT(DISTINCT tblRecord.recActNo) AS countCate_wHRO FROM tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                    WHERE tblRecordDetail.category = @category AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year "
        If _departId <> 0 Then strSqlCount = strSqlCount & "AND tblRecord.departId = '" & _departId.ToString & "'"

        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@Month", SqlDbType.Int).Value = sMonth
            command.Parameters.Add("@category", SqlDbType.Int).Value = CateId
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function

    Public Function CountSubCategory(ByVal SubCateId As Integer, ByVal Month As Integer) As Integer
        Dim strSqlCount As String = "SELECT COUNT(DISTINCT(recActNo)) FROM tblRecordDetail INNER JOIN tblRecord On tblRecordDetail.recId = tblRecord.recId 
                                    WHERE tblRecordDetail.categorySub = @categorySub AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year "
        If _departId <> 0 Then strSqlCount = strSqlCount & "AND tblRecord.departId = '" & _departId.ToString & "'"

        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@Month", SqlDbType.Int).Value = Month
            command.Parameters.Add("@categorySub", SqlDbType.Int).Value = SubCateId
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function
    'Public Function CountSubCategory_Action(ByVal SubCateId As Integer, ByVal Month As Integer) As Integer

    'End Function

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

    Public Function CountStatus(ByVal statusId As Integer, ByVal Month As Integer) As Integer
        Dim strSqlCount As String = "SELECT COUNT(DISTINCT(recActNo)) FROM tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                    WHERE tblRecordDetail.observComplete = @observComplete AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year "
        If _departId <> 0 Then strSqlCount = strSqlCount & "AND tblRecord.departId = '" & _departId.ToString & "'"

        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@Month", SqlDbType.Int).Value = Month
            command.Parameters.Add("@observComplete", SqlDbType.Int).Value = statusId
            count = command.ExecuteScalar()
        End Using
        conn.Close()

        Return count
    End Function

    Public Function SumOfDuration_fsfl(ByVal Month As Integer) As Integer
        Dim strSqlCount As String = "SELECT SUM(durationValue) AS SumDuration FROM
                                        (SELECT DISTINCT tblRecord.recActNo, tblRecord.durationValue FROM tblRecordDetail 
                                            INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId 
                                            INNER JOIN tblEmployee ON tblRecord.empId = tblEmployee.empId 
                                            WHERE tblEmployee.joblvCode = 'fsfl' AND tblRecord.recActMonth = @Month AND tblRecord.recActYear = @Year"
        If _departId <> 0 Then strSqlCount = strSqlCount & " AND tblRecord.departId = '" & _departId.ToString & "'"
        strSqlCount = strSqlCount & ") AS DistinctActNo"

        Dim count As Integer = 0
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSqlCount, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@Year", SqlDbType.Int).Value = _sYear
            command.Parameters.Add("@Month", SqlDbType.Int).Value = Month

            If command.ExecuteScalar() IsNot DBNull.Value Then
                count = command.ExecuteScalar()
            End If
        End Using
        conn.Close()

        Return count
    End Function
    Public Function CountEmployee_fsfl() As Integer
        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblEmployee WHERE joblvCode = 'fsfl'"
        If _departId <> 0 Then strSqlCount = strSqlCount & " AND departId = '" & _departId.ToString & "'"
        Dim count As Integer = 0
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(strSqlCount, connection)
            count = command.ExecuteScalar()
        End Using

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
        Dim secondEyeCount As Integer = chkReportRecordExist(sYear, sMonth, departId)
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
        Dim secondEyeCount As Integer = chkReportRecordExist(sYear, sMonth, departId)
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
