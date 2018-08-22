Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class rpDataParticipation
    Inherits Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Dim reCalIntervalTime As Integer = 60

    Private Sub MsgBoxRad(ByVal Msg As String, ByVal Width As Integer, ByVal Height As Integer)
        RadWindowManager1.Width = Width
        RadWindowManager1.Height = Height
        RadWindowManager1.RadAlert(Msg, Width + 100, Height + 72, "My Alert", "", "myAlertImage.png")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim employee As New cEmployee
            employee.FindEmployeeIdbyUsername(User.Identity.Name)

            If employee.EmployeeId <> 0 Then
                lbName.Text = employee.EmployeeName & " " & employee.EmployeeSurname.Substring(0, 1) & "."
                lbEmail.Text = employee.EmployeeEmail
                lbDowId.Text = employee.DowId
                lbDepartName.Text = employee.DepartmentName
                lbAccountType.Text = "[" & employee.AccountType & "]"
            End If

            'update last data
            'Dim lastMonth As DateTime = DateAdd(DateInterval.Month, -1, Now())
            Dim lastMonth As DateTime = Now()
            rcbMonth.SelectedIndex = rcbMonth.FindItemIndexByValue(lastMonth.Month)
            rcbYear.SelectedIndex = rcbYear.FindItemIndexByValue(lastMonth.Year)
            rcbDepartment.DataBind()

            ChartDataBind()
        End If

        If User.IsInRole("SYSTEM ADMIN") Then
            RadPanelBar1.Items.FindItemByText("SETTING").Visible = True
        End If
    End Sub

    Dim maxPsceContainmentLoss As Integer = Integer.MinValue
    Dim maxPscePsnm As Integer = Integer.MinValue
    Dim maxSecondEye As Integer = Integer.MinValue
    Dim maxInjuryNearMiss As Integer = Integer.MinValue
    Dim maxProactiveCompliance As Integer = Integer.MinValue
    Dim maxReliability_wHRO As Integer = Integer.MinValue
    Dim maxQuality_wHRO As Integer = Integer.MinValue
    Dim maxReliability As Integer = Integer.MinValue

    Dim maxLeadershipVisibilityFsfl As Integer = Integer.MinValue
    Dim maxLeadershipVisibilityTech As Integer = Integer.MinValue

    Private Sub ChartDataBind()
        Dim chartData As DataSet = GetData()
        FindMaxDataValue(chartData.Tables(0))

        RadHtmlChart1.DataSource = chartData
        RadHtmlChart2.DataSource = chartData

        RadHtmlChart5.DataSource = chartData
        RadHtmlChart6.DataSource = chartData
        RadHtmlChart7.DataSource = chartData
        RadHtmlChart8.DataSource = chartData
        RadHtmlChart9.DataSource = chartData
        RadHtmlChart10.DataSource = chartData

        Dim chartDataFsfl As DataSet = GetDataFieldVisFsfl()
        maxLeadershipVisibilityFsfl = FindMaxDataValueLeadershipVisibility(chartDataFsfl.Tables(0))
        RadHtmlChart3.DataSource = chartDataFsfl

        Dim chartDataTech As DataSet = GetDataFieldVisTech()
        maxLeadershipVisibilityTech = FindMaxDataValueLeadershipVisibility(chartDataTech.Tables(0))
        RadHtmlChart4.DataSource = chartDataTech

        AllChartDataBind()
    End Sub

    Private Sub FindMaxDataValue(ByVal dt As DataTable)
        Dim currentValue As Integer
        For Each row As DataRow In dt.Rows
            currentValue = row.Field(Of Integer)("PSCE_ContainmentLoss")
            maxPsceContainmentLoss = Math.Max(maxPsceContainmentLoss, currentValue)
            currentValue = row.Field(Of Integer)("PSCE_PSNM")
            maxPscePsnm = Math.Max(maxPscePsnm, currentValue)
            currentValue = row.Field(Of Integer)("secondEye")
            maxSecondEye = Math.Max(maxSecondEye, currentValue)
            currentValue = row.Field(Of Integer)("injuryNearMiss")
            maxInjuryNearMiss = Math.Max(maxInjuryNearMiss, currentValue)
            currentValue = row.Field(Of Integer)("proactiveCompliance")
            maxProactiveCompliance = Math.Max(maxProactiveCompliance, currentValue)
            currentValue = row.Field(Of Integer)("reliability_wHRO")
            maxReliability_wHRO = Math.Max(maxReliability_wHRO, currentValue)
            currentValue = row.Field(Of Integer)("quality_wHRO")
            maxQuality_wHRO = Math.Max(maxQuality_wHRO, currentValue)
            currentValue = row.Field(Of Integer)("reliability")
            maxReliability = Math.Max(maxReliability, currentValue)
        Next
    End Sub

    Private Function FindMaxDataValueLeadershipVisibility(ByVal dt As DataTable) As Integer
        Dim currentValue As Integer
        Dim maxValue As Integer
        For Each row As DataRow In dt.Rows
            currentValue = row.Field(Of Integer)("leadershipVisibility")
            maxValue = Math.Max(maxValue, currentValue)
        Next

        Return maxValue
    End Function

    Private Function GetData() As DataSet
        Dim strSql As String = String.Format("SELECT tblEmployee.empDisplay, SUM(tblRpEmpHistorical.PSCE_ContainmentLoss) AS PSCE_ContainmentLoss, SUM(tblRpEmpHistorical.PSCE_PSNM) AS PSCE_PSNM, 
                                              SUM(tblRpEmpHistorical.secondEye) AS secondEye, SUM(tblRpEmpHistorical.injuryNearMiss) AS injuryNearMiss, SUM(tblRpEmpHistorical.proactiveCompliance) AS proactiveCompliance, 
                                              SUM(tblRpEmpHistorical.reliability_wHRO) AS reliability_wHRO, SUM(tblRpEmpHistorical.quality_wHRO) AS quality_wHRO, SUM(tblRpEmpHistorical.reliability) AS reliability 
                                              FROM tblRpEmpHistorical INNER JOIN tblEmployee ON tblRpEmpHistorical.empId = tblEmployee.empId 
                                              WHERE tblRpEmpHistorical.year = {0} AND tblRpEmpHistorical.month <= {1} AND tblEmployee.departId = {2} 
                                              GROUP BY tblEmployee.empDisplay 
                                              ORDER BY tblEmployee.empDisplay", rcbYear.SelectedValue, rcbMonth.SelectedValue, rcbDepartment.SelectedValue)
        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)

        Dim dt As New DataTable()
        conn.Open()
        Try
            adapter.Fill(dt)
        Finally
            conn.Close()
        End Try

        '-- add Goal
        Dim goalSecondEye As DataColumn = dt.Columns.Add("goalSecondEye", Type.GetType("System.String"))
        'Dim dtGoal As DataTable = GetGoalData()
        'Dim dtGoalCount As Integer = dtGoal.Rows.Count

        'For Each row As DataRow In dt.Rows      'loop Employee
        '    'If i < dtGoalCount Then
        '    '    'dt.Rows(i).Item("goalSecondEye") = Math.Ceiling(CInt(dtGoal.Rows(i).Item("secondEyeSafety")) / 12)
        '    'End If
        'Next

        Dim ds As New DataSet
        ds.Tables.Add(dt)

        Return ds
    End Function
    Private Function GetDataFieldVisFsfl() As DataSet
        Dim strSql As String = String.Format("SELECT tblEmployee.empDisplay, SUM(tblRpEmpHistorical.leadershipVisibility) AS leadershipVisibility, tblEmployee.joblvCode FROM tblRpEmpHistorical 
                                              INNER JOIN tblEmployee ON tblRpEmpHistorical.empId = tblEmployee.empId 
                                              WHERE tblEmployee.joblvCode = 'fsfl' AND tblRpEmpHistorical.year = {0} AND tblRpEmpHistorical.month <= {1} AND tblEmployee.departId = {2} 
                                              GROUP BY tblEmployee.empDisplay, tblEmployee.joblvCode 
                                              ORDER BY tblEmployee.empDisplay", rcbYear.SelectedValue, rcbMonth.SelectedValue, rcbDepartment.SelectedValue)

        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)

        Dim dt As New DataTable()
        conn.Open()
        Try
            adapter.Fill(dt)
        Finally
            conn.Close()
        End Try

        '-- add Goal column
        Dim goalFieldVisibilityFsfl As DataColumn = dt.Columns.Add("goalFieldVisibilityFsfl", Type.GetType("System.String"))
        'Dim dtGoal As DataTable = GetGoalData()
        'Dim dtGoalCount As Integer = dtGoal.Rows.Count

        'For Each row As DataRow In dt.Rows      'loop Employee
        '    'If i < dtGoalCount Then
        '    '    'dt.Rows(i).Item("goalFieldVisibilityFsfl") = CDbl(dtGoal.Rows(i).Item("percentActionCompleted")) / 100
        '    'End If
        'Next

        Dim ds As New DataSet
        ds.Tables.Add(dt)

        Return ds
    End Function
    Private Function GetDataFieldVisTech() As DataSet
        Dim strSql As String = String.Format("SELECT tblEmployee.empDisplay, SUM(tblRpEmpHistorical.leadershipVisibility) AS leadershipVisibility, tblEmployee.joblvCode FROM tblRpEmpHistorical 
                                              INNER JOIN tblEmployee ON tblRpEmpHistorical.empId = tblEmployee.empId 
                                              WHERE tblEmployee.joblvCode = 'tech' AND tblRpEmpHistorical.year = {0} AND tblRpEmpHistorical.month <= {1} AND tblEmployee.departId = {2} 
                                              GROUP BY tblEmployee.empDisplay, tblEmployee.joblvCode 
                                              ORDER BY tblEmployee.empDisplay", rcbYear.SelectedValue, rcbMonth.SelectedValue, rcbDepartment.SelectedValue)

        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)

        Dim dt As New DataTable()
        conn.Open()
        Try
            adapter.Fill(dt)
        Finally
            conn.Close()
        End Try

        '--add Goal column
        Dim goalFieldVisibilityTech As DataColumn = dt.Columns.Add("goalFieldVisibilityTech", Type.GetType("System.String"))
        'Dim dtGoal As DataTable = GetGoalData()
        'Dim dtGoalCount As Integer = dtGoal.Rows.Count

        'For Each row As DataRow In dt.Rows      'loop Employee
        '    'If i < dtGoalCount Then
        '    '    'dt.Rows(i).Item("goalFieldVisibilityTech") = CDbl(dtGoal.Rows(i).Item("percentLeadershipFieldVisibility")) / 100
        '    'End If
        'Next

        Dim ds As New DataSet
        ds.Tables.Add(dt)

        Return ds
    End Function
    Private Function GetGoalData() As DataTable
        Dim strSql As String = String.Format("SELECT tblDepartment.departName, tblGoalSetting.PSCE_ContainmentLoss, tblGoalSetting.PSCE_PSNM, tblGoalSetting.percentActionCompleted, tblGoalSetting.percentLeadershipFieldVisibility, 
                                              tblGoalSetting.complianceProactive, tblGoalSetting.secondEyeSafety, tblGoalSetting.departId FROM tblGoalSetting 
                                              INNER JOIN tblDepartment ON tblGoalSetting.departId = tblDepartment.departId 
                                              WHERE (tblGoalSetting.year = {0}) AND (tblGoalSetting.month = {1}) 
                                              ORDER BY tblGoalSetting.goalType, tblDepartment.departName", rcbYear.SelectedValue, rcbMonth.SelectedValue)

        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)

        Dim dt As New DataTable()
        conn.Open()
        Try
            adapter.Fill(dt)
        Finally
            conn.Close()
        End Try

        Return dt
    End Function

    Protected Sub btViewChart_Click(sender As Object, e As EventArgs) Handles btViewChart.Click
        Dim yearReport As Integer = rcbYear.SelectedValue
        Dim monthReport As Integer = rcbMonth.SelectedValue
        Dim rp As New cReport2(yearReport)
        Dim currentTime As DateTime = Now()
        Dim LastUpdate As DateTime = rp.ChkLastUpdateByYearMonth(currentTime, yearReport, monthReport)
        Dim chkPoint As DateTime = DateAdd(DateInterval.Minute, reCalIntervalTime, LastUpdate)
        'reGetDepartmartData(departReport)
        ChartDataBind()
        'If LastUpdate = currentTime OrElse chkPoint < Now() Then
        '    reGetDepartmartData(departReport)
        '    GetData(departReport)
        'End If
    End Sub

    Private Sub AllChartDataBind()
        RadHtmlChart1.DataBind()
        RadHtmlChart2.DataBind()
        RadHtmlChart3.DataBind()
        RadHtmlChart4.DataBind()
        RadHtmlChart5.DataBind()
        RadHtmlChart6.DataBind()
        RadHtmlChart7.DataBind()
        RadHtmlChart8.DataBind()
        RadHtmlChart9.DataBind()
        RadHtmlChart10.DataBind()
    End Sub

    Private Sub reGetDepartmartData(departId As Integer)
        Dim yearReport As Integer = rcbYear.SelectedValue
        Dim departReport As Integer = departId

        If departReport <> 0 Then
            Dim rp As New cReport2(yearReport)
            Dim WorkingHourPerMonth As Integer = 9800
            Dim TotalObserver_fsfl As Integer = rp.CountEmployee_fsfl(departReport)
            Dim ManPowerWorkingHourPerMonth As Integer = WorkingHourPerMonth * TotalObserver_fsfl

            For i As Integer = 1 To Now.Month
                'select data & update to tblRpOverallOps
                Dim thisMonth As Integer = i
                Dim totalActionNumber As Integer = rp.CountDocTotalActionNum(thisMonth, departReport)
                Dim totalObserv As Integer = rp.CountDocTotalObserv(thisMonth, departReport)

                rp.getDepartmentData(departReport, i)
                Dim PSCE_ContainmentLossCount As Integer = rp.getSumPSCE_ContainmentLoss()
                Dim PSCE_PSNMCount As Integer = rp.getSumPSCE_PSNM()

                '-- count from action number
                rp.CountStatusActionNum(thisMonth, departReport)
                Dim actionNumComplete As Integer = rp.CountActionNumComplete
                Dim actionNumRecog As Integer = rp.CountActionNumRecog
                Dim percentActionComplete As Decimal = 0
                If totalActionNumber <> actionNumRecog Then percentActionComplete = actionNumComplete / (totalActionNumber - actionNumRecog)
                '-----------------------------

                Dim sumOfDuration_fsfl As Integer = rp.getSumleadershipVisibility
                Dim percentLeadershipVisibility As Decimal = 0
                If TotalObserver_fsfl <> 0 Then percentLeadershipVisibility = sumOfDuration_fsfl / ManPowerWorkingHourPerMonth

                Dim proactiveCompliance As Integer = rp.getSumproactiveCompliance
                Dim secondEye As Integer = rp.getSumsecondEye

                Dim injuryNearMiss As Integer = rp.getSuminjuryNearMiss
                Dim reliability_wHRO As Integer = rp.getSumreliability_wHRO
                Dim quality_wHRO As Integer = rp.getSumquality_wHRO
                Dim reliability As Integer = rp.getSumreliability

                Dim ProcedureUsed As Integer = rp.getSumprocedureUsed
                Dim Safety As Integer = rp.getSumsafety
                Dim percentPUPFieldAss As Decimal = 0
                Dim percentLCSFieldAss As Decimal = 0
                If Safety <> 0 Then
                    Dim LCS As Integer = rp.getSumLCS
                    percentPUPFieldAss = ProcedureUsed / Safety
                    percentLCSFieldAss = LCS / Safety
                End If

                '-- count from action number
                rp.ActionNumStatus(thisMonth, departReport)
                Dim actionRecogAll As Decimal = rp.CountRecogAll()
                Dim offHour As Integer = rp.CountOffHour()

                Dim percentOFIIdentified As Decimal = 0
                Dim percentOffHour As Decimal = 0
                If totalActionNumber > 0 Then
                    percentOFIIdentified = 1 - (actionRecogAll / totalActionNumber)
                    percentOffHour = offHour / totalActionNumber
                End If
                '-----------------------------

                'update tblRpOverallOps WHERE departId
                Dim StrUpd As String = "UPDATE tblRpOverallOps SET totalActionNumber = @totalActionNumber, totalObserve = @totalObserve, PSCE_ContainmentLoss = @PSCE_ContainmentLoss, PSCE_PSNM = @PSCE_PSNM, 
                                            actionComplete = @actionComplete, actionRecognition = @actionRecognition, percentActionComplete = @percentActionComplete, 
                                            leadershipVisibility_fsfl = @leadershipVisibility_fsfl, percentLeadershipVisibility = @percentLeadershipVisibility, proactiveCompliance = @proactiveCompliance, 
                                            secondEye = @secondEye, injuryNearMiss = @injuryNearMiss, reliability_wHRO = @reliability_wHRO, quality_wHRO = @quality_wHRO, reliability = @reliability, 
                                            percentOFIIden = @percentOFIIden, percentPUPFieldAss = @percentPUPFieldAss, percentLCSFieldAss = @percentLCSFieldAss, 
                                            offHourCount = @offHourCount, percentOffHour = @percentOffHour, lastUpdate = @lastUpdate 
                                            WHERE departId = @departId AND month = @month AND year = @year"

                Dim conn As New SqlConnection(ConnStr)
                conn.Open()
                Dim command As New SqlCommand(StrUpd, conn)
                command.Parameters.Add("@totalActionNumber", SqlDbType.Int).Value = totalActionNumber
                command.Parameters.Add("@totalObserve", SqlDbType.Int).Value = totalObserv
                command.Parameters.Add("@PSCE_ContainmentLoss", SqlDbType.Int).Value = PSCE_ContainmentLossCount
                command.Parameters.Add("@PSCE_PSNM", SqlDbType.Int).Value = PSCE_PSNMCount
                command.Parameters.Add("@actionComplete", SqlDbType.Int).Value = actionNumComplete
                command.Parameters.Add("@actionRecognition", SqlDbType.Int).Value = actionNumRecog
                command.Parameters.Add("@percentActionComplete", SqlDbType.SmallMoney).Value = percentActionComplete
                command.Parameters.Add("@leadershipVisibility_fsfl", SqlDbType.Int).Value = sumOfDuration_fsfl
                command.Parameters.Add("@percentLeadershipVisibility", SqlDbType.SmallMoney).Value = percentLeadershipVisibility
                command.Parameters.Add("@proactiveCompliance", SqlDbType.Int).Value = proactiveCompliance
                command.Parameters.Add("@secondEye", SqlDbType.Int).Value = secondEye
                command.Parameters.Add("@injuryNearMiss", SqlDbType.Int).Value = injuryNearMiss
                command.Parameters.Add("@reliability_wHRO", SqlDbType.Int).Value = reliability_wHRO
                command.Parameters.Add("@quality_wHRO", SqlDbType.Int).Value = quality_wHRO
                command.Parameters.Add("@reliability", SqlDbType.Int).Value = reliability
                command.Parameters.Add("@percentOFIIden", SqlDbType.SmallMoney).Value = percentOFIIdentified
                command.Parameters.Add("@percentPUPFieldAss", SqlDbType.SmallMoney).Value = percentPUPFieldAss
                command.Parameters.Add("@percentLCSFieldAss", SqlDbType.SmallMoney).Value = percentLCSFieldAss
                command.Parameters.Add("@offHourCount", SqlDbType.VarChar).Value = offHour.ToString & "/" & totalActionNumber.ToString
                command.Parameters.Add("@percentOffHour", SqlDbType.SmallMoney).Value = percentOffHour
                command.Parameters.Add("@lastUpdate", SqlDbType.DateTime).Value = Now

                command.Parameters.Add("@departId", SqlDbType.Int).Value = departReport
                command.Parameters.Add("@month", SqlDbType.Int).Value = thisMonth
                command.Parameters.Add("@year", SqlDbType.Int).Value = yearReport
                Dim result As Integer = command.ExecuteNonQuery()
            Next
        End If
    End Sub

    'Protected Sub btShowGoalLine_Click(sender As Object, e As EventArgs) Handles btShowGoalLine.Click
    '    RadHtmlChart1.PlotArea.Series(1).Visible = chkShowPsceContainment.Checked
    '    RadHtmlChart2.PlotArea.Series(1).Visible = chkShowPscePsnm.Checked
    '    RadHtmlChart3.PlotArea.Series(1).Visible = chkShowActionCompleted.Checked
    '    RadHtmlChart4.PlotArea.Series(1).Visible = chkShowFieldVisibility.Checked
    '    RadHtmlChart5.PlotArea.Series(1).Visible = chkShowProactiveCompliance.Checked
    '    RadHtmlChart6.PlotArea.Series(1).Visible = chkShowSecondEye.Checked
    '    AllChartDataBind()
    'End Sub

    Private Sub RadHtmlChart1_DataBinding(sender As Object, e As EventArgs) Handles RadHtmlChart1.DataBinding
        If maxPsceContainmentLoss < 8 Then
            RadHtmlChart1.PlotArea.YAxis.Step = 1
        Else
            RadHtmlChart1.PlotArea.YAxis.Step = Nothing
        End If
    End Sub
    Private Sub RadHtmlChart2_DataBinding(sender As Object, e As EventArgs) Handles RadHtmlChart2.DataBinding
        If maxPscePsnm < 8 Then
            RadHtmlChart2.PlotArea.YAxis.Step = 1
        Else
            RadHtmlChart2.PlotArea.YAxis.Step = Nothing
        End If
    End Sub

    Private Sub RadHtmlChart3_DataBinding(sender As Object, e As EventArgs) Handles RadHtmlChart3.DataBinding
        If maxLeadershipVisibilityFsfl < 8 Then
            RadHtmlChart3.PlotArea.YAxis.Step = 1
        Else
            RadHtmlChart3.PlotArea.YAxis.Step = Nothing
        End If
    End Sub
    Private Sub RadHtmlChart4_DataBinding(sender As Object, e As EventArgs) Handles RadHtmlChart4.DataBinding
        If maxLeadershipVisibilityTech < 8 Then
            RadHtmlChart4.PlotArea.YAxis.Step = 1
        Else
            RadHtmlChart4.PlotArea.YAxis.Step = Nothing
        End If
    End Sub

    Private Sub RadHtmlChart5_DataBinding(sender As Object, e As EventArgs) Handles RadHtmlChart5.DataBinding
        If maxSecondEye < 8 Then
            RadHtmlChart5.PlotArea.YAxis.Step = 1
        Else
            RadHtmlChart5.PlotArea.YAxis.Step = Nothing
        End If
    End Sub
    Private Sub RadHtmlChart6_DataBinding(sender As Object, e As EventArgs) Handles RadHtmlChart6.DataBinding
        If maxInjuryNearMiss < 8 Then
            RadHtmlChart6.PlotArea.YAxis.Step = 1
        Else
            RadHtmlChart6.PlotArea.YAxis.Step = Nothing
        End If
    End Sub
    Private Sub RadHtmlChart7_DataBinding(sender As Object, e As EventArgs) Handles RadHtmlChart7.DataBinding
        If maxProactiveCompliance < 8 Then
            RadHtmlChart7.PlotArea.YAxis.Step = 1
        Else
            RadHtmlChart7.PlotArea.YAxis.Step = Nothing
        End If
    End Sub
    Private Sub RadHtmlChart8_DataBinding(sender As Object, e As EventArgs) Handles RadHtmlChart8.DataBinding
        If maxReliability_wHRO < 8 Then
            RadHtmlChart8.PlotArea.YAxis.Step = 1
        Else
            RadHtmlChart8.PlotArea.YAxis.Step = Nothing
        End If
    End Sub
    Private Sub RadHtmlChart9_DataBinding(sender As Object, e As EventArgs) Handles RadHtmlChart9.DataBinding
        If maxQuality_wHRO < 8 Then
            RadHtmlChart9.PlotArea.YAxis.Step = 1
        Else
            RadHtmlChart9.PlotArea.YAxis.Step = Nothing
        End If
    End Sub
    Private Sub RadHtmlChart10_DataBinding(sender As Object, e As EventArgs) Handles RadHtmlChart10.DataBinding
        If maxReliability < 8 Then
            RadHtmlChart10.PlotArea.YAxis.Step = 1
        Else
            RadHtmlChart10.PlotArea.YAxis.Step = Nothing
        End If
    End Sub

End Class