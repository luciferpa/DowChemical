Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class rpDepartmentPerform
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

            chartDataBind()
        End If

        If User.IsInRole("SYSTEM ADMIN") Then
            RadPanelBar1.Items.FindItemByText("SETTING").Visible = True
        End If
    End Sub

    Private Sub chartDataBind()
        Dim chartData As DataSet = GetData()
        RadHtmlChart1.DataSource = chartData
        RadHtmlChart2.DataSource = chartData
        RadHtmlChart3.DataSource = chartData
        RadHtmlChart4.DataSource = chartData
        RadHtmlChart5.DataSource = chartData
        RadHtmlChart6.DataSource = chartData
        RadHtmlChart7.DataSource = chartData
        RadHtmlChart8.DataSource = chartData
        RadHtmlChart9.DataSource = chartData
        RadHtmlChart10.DataSource = chartData
        RadHtmlChart11.DataSource = chartData
        'RadHtmlChart12.DataSource = chartData
        'RadHtmlChart13.DataSource = chartData
        RadHtmlChart14.DataSource = chartData

        AllChartDataBind()
    End Sub

    Private Function GetData() As DataSet
        ''-- month
        'Dim strSql As String = String.Format("SELECT tblDepartment.departName, tblDepartment.departName AS departNameMTP, tblRpOverallOps.PSCE_ContainmentLoss, tblRpOverallOps.PSCE_PSNM, tblRpOverallOps.percentActionComplete, 
        '                                      tblRpOverallOps.percentLeadershipVisibility, tblRpOverallOps.proactiveCompliance, tblRpOverallOps.secondEye, tblRpOverallOps.injuryNearMiss, 
        '                                      tblRpOverallOps.reliability_wHRO, tblRpOverallOps.quality_wHRO, tblRpOverallOps.reliability, tblRpOverallOps.percentOFIIden, 
        '                                      tblRpOverallOps.percentPUPFieldAss, tblRpOverallOps.percentLCSFieldAss, tblRpOverallOps.percentOffHour, tblRpOverallOps.totalActionNumber
        '                                      FROM tblRpOverallOps 
        '                                      INNER JOIN tblDepartment ON tblRpOverallOps.departId = dbo.tblDepartment.departId 
        '                                      WHERE (tblRpOverallOps.rpType <> 'g') AND (tblRpOverallOps.year = {0}) AND (tblRpOverallOps.month = {1}) 
        '                                      ORDER BY tblDepartment.departName", rcbYear.SelectedValue, rcbMonth.SelectedValue)

        '-- YTD
        Dim strSql As String = String.Format("SELECT tblDepartment.departName, tblDepartment.departName AS departNameMTP, SUM(tblRpOverallOps.PSCE_ContainmentLoss) AS PSCE_ContainmentLoss, SUM(tblRpOverallOps.PSCE_PSNM) AS PSCE_PSNM, 
                                              SUM(tblRpOverallOps.percentActionComplete) AS percentActionComplete, SUM(tblRpOverallOps.percentLeadershipVisibility) AS percentLeadershipVisibility, 
                                              SUM(tblRpOverallOps.proactiveCompliance) AS proactiveCompliance, SUM(tblRpOverallOps.secondEye) AS secondEye, SUM(tblRpOverallOps.injuryNearMiss) AS injuryNearMiss, 
                                              SUM(tblRpOverallOps.reliability_wHRO) AS reliability_wHRO, SUM(tblRpOverallOps.quality_wHRO) AS quality_wHRO, SUM(tblRpOverallOps.reliability) AS reliability, 
                                              SUM(tblRpOverallOps.percentOFIIden) AS percentOFIIden, SUM(tblRpOverallOps.percentPUPFieldAss) AS percentPUPFieldAss, SUM(tblRpOverallOps.percentLCSFieldAss) AS percentLCSFieldAss, 
                                              SUM(tblRpOverallOps.percentOffHour) AS percentOffHour, SUM(tblRpOverallOps.totalActionNumber) AS totalActionNumber
                                              FROM tblRpOverallOps 
                                              INNER JOIN tblDepartment ON tblRpOverallOps.departId = dbo.tblDepartment.departId 
                                              WHERE (tblRpOverallOps.rpType <> 'g') AND (tblRpOverallOps.year = {0}) AND (tblRpOverallOps.month <= {1}) 
                                              GROUP BY tblDepartment.departName 
                                              ORDER BY tblDepartment.departName", rcbYear.SelectedValue, rcbMonth.SelectedValue)

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

        'addd YTD row
        Dim YTDRow As DataRow = dt.NewRow
        YTDRow("departName") = ""
        YTDRow("departNameMTP") = "MTP Ops."
        dt.Rows.Add(YTDRow)

        'add Goal column
        Dim colorBar As DataColumn = dt.Columns.Add("colorBar", Type.GetType("System.String"))
        Dim goalPsceContainmentLoss As DataColumn = dt.Columns.Add("goalPSCE_ContainmentLoss", Type.GetType("System.String"))
        Dim goalPscePsnm As DataColumn = dt.Columns.Add("goalPSCE_PSNM", Type.GetType("System.String"))
        Dim goalPercentActionCompleted As DataColumn = dt.Columns.Add("goalPercentActionCompleted", Type.GetType("System.String"))
        Dim goalPercentLeadershipFieldVisibility As DataColumn = dt.Columns.Add("goalPercentLeadershipFieldVisibility", Type.GetType("System.String"))
        Dim goalComplianceProactive As DataColumn = dt.Columns.Add("goalComplianceProactive", Type.GetType("System.String"))
        Dim goalSecondEyeSafety As DataColumn = dt.Columns.Add("goalSecondEyeSafety", Type.GetType("System.String"))

        Dim dtGoal As DataTable = GetGoalData()
        Dim dtGoalCount As Integer = dtGoal.Rows.Count

        'totalPSCE_ContainmentLoss = 0
        'totalPSCE_PSNM = 0
        totalpercentActionComplete = 0
        totalpercentLeadershipVisibility = 0
        'totalproactiveCompliance = 0
        'totalsecondEye = 0

        'totalinjuryNearMiss = 0
        'totalreliabilityHRO = 0
        'totalqualityHRO = 0
        'totalreliability = 0
        totalpercentOFIIden = 0
        'totalpercentPUPFieldAss = 0
        'totalpercentLCSFieldAss = 0
        totalpercentOffHour = 0

        Dim departDivider As Integer = dt.Rows.Count - 1
        Dim i As Integer = 0
        Dim numOfMonth As Integer = CInt(rcbMonth.SelectedValue)
        For Each row As DataRow In dt.Rows      'loop Department + YTD
            If i <> dt.Rows.Count - 1 Then
                '-- department items
                row.Item(4) = (CDbl(row.Field(Of Decimal)(4)) / numOfMonth).ToString        'percentActionComplete
                row.Item(5) = (CDbl(row.Field(Of Decimal)(5)) / numOfMonth).ToString        'percentLeadershipVisibility
                row.Item(12) = (CDbl(row.Field(Of Decimal)(12)) / numOfMonth).ToString      'percentOFIIden
                row.Item(15) = (CDbl(row.Field(Of Decimal)(15)) / numOfMonth).ToString      'percentOffHour

                '-- total value
                'totalPSCE_ContainmentLoss = totalPSCE_ContainmentLoss + CInt(row.Field(Of Integer)(2))
                'totalPSCE_PSNM = totalPSCE_PSNM + CInt(row.Field(Of Integer)(3))
                totalpercentActionComplete = totalpercentActionComplete + CDbl(row.Field(Of Decimal)(4))
                totalpercentLeadershipVisibility = totalpercentLeadershipVisibility + CDbl(row.Field(Of Decimal)(5))
                'totalproactiveCompliance = totalproactiveCompliance + CInt(row.Field(Of Integer)(6))
                'totalsecondEye = totalsecondEye + CInt(row.Field(Of Integer)(7))

                'totalinjuryNearMiss = totalinjuryNearMiss + CInt(row.Field(Of Integer)(8))
                'totalreliabilityHRO = totalreliabilityHRO + CInt(row.Field(Of Integer)(9))
                'totalqualityHRO = totalqualityHRO + CInt(row.Field(Of Integer)(10))
                'totalreliability = totalreliability + CInt(row.Field(Of Integer)(11))
                totalpercentOFIIden = totalpercentOFIIden + CDbl(row.Field(Of Decimal)(12))
                ' 'totalpercentPUPFieldAss = totalpercentPUPFieldAss + CDbl(row.Field(Of Decimal)(13))
                ' 'totalpercentLCSFieldAss = totalpercentLCSFieldAss + CDbl(row.Field(Of Decimal)(14))
                totalpercentOffHour = totalpercentOffHour + CDbl(row.Field(Of Decimal)(15))

                'add goal value
                If i < dtGoalCount Then
                    'dt.Rows(i).Item("goalPSCE_ContainmentLoss") = Math.Ceiling(CInt(dtGoal.Rows(i).Item("PSCE_ContainmentLoss")) / 12)
                    'dt.Rows(i).Item("goalPSCE_PSNM") = Math.Ceiling(CInt(dtGoal.Rows(i).Item("PSCE_PSNM")) / 12)
                    'dt.Rows(i).Item("goalPercentActionCompleted") = CDbl(dtGoal.Rows(i).Item("percentActionCompleted")) / 100
                    'dt.Rows(i).Item("goalPercentLeadershipFieldVisibility") = CDbl(dtGoal.Rows(i).Item("percentLeadershipFieldVisibility")) / 100
                    'dt.Rows(i).Item("goalComplianceProactive") = Math.Ceiling(CInt(dtGoal.Rows(i).Item("complianceProactive")) / 12)
                    'dt.Rows(i).Item("goalSecondEyeSafety") = Math.Ceiling(CInt(dtGoal.Rows(i).Item("secondEyeSafety")) / 12)

                    'dt.Rows(i).Item("goalPSCE_ContainmentLoss") = CInt(dtGoal.Rows(i).Item("PSCE_ContainmentLoss"))
                    'dt.Rows(i).Item("goalPSCE_PSNM") = CInt(dtGoal.Rows(i).Item("PSCE_PSNM"))
                    'dt.Rows(i).Item("goalPercentActionCompleted") = CDbl(dtGoal.Rows(i).Item("percentActionCompleted")) / 100
                    'dt.Rows(i).Item("goalPercentLeadershipFieldVisibility") = CDbl(dtGoal.Rows(i).Item("percentLeadershipFieldVisibility")) / 100
                    'dt.Rows(i).Item("goalComplianceProactive") = CInt(dtGoal.Rows(i).Item("complianceProactive"))
                    dt.Rows(i).Item("goalSecondEyeSafety") = CInt(dtGoal.Rows(i).Item("secondEyeSafety"))
                End If
            Else
                dt.Rows(i).Item("colorBar") = "#5cb85c"

                If departDivider <> 0 Then
                    dt.Rows(i).Item("PSCE_ContainmentLoss") = DBNull.Value                  'totalPSCE_ContainmentLoss
                    dt.Rows(i).Item("PSCE_PSNM") = DBNull.Value                             'totalPSCE_PSNM
                    dt.Rows(i).Item("percentActionComplete") = totalpercentActionComplete / departDivider
                    dt.Rows(i).Item("percentLeadershipVisibility") = totalpercentLeadershipVisibility / departDivider
                    dt.Rows(i).Item("proactiveCompliance") = DBNull.Value                   'totalproactiveCompliance
                    dt.Rows(i).Item("secondEye") = DBNull.Value                             'totalsecondEye

                    dt.Rows(i).Item("injuryNearMiss") = DBNull.Value                        'totalinjuryNearMiss
                    dt.Rows(i).Item("reliability_wHRO") = DBNull.Value                      'totalreliabilityHRO
                    dt.Rows(i).Item("quality_wHRO") = DBNull.Value                          'totalqualityHRO
                    dt.Rows(i).Item("reliability") = DBNull.Value                           'totalreliability
                    dt.Rows(i).Item("percentOFIIden") = totalpercentOFIIden / departDivider
                    'dt.Rows(i).Item("percentPUPFieldAss") = totalpercentPUPFieldAss / monthDivider
                    'dt.Rows(i).Item("percentLCSFieldAss") = totalpercentLCSFieldAss / monthDivider
                    dt.Rows(i).Item("percentOffHour") = totalpercentOffHour / departDivider
                End If
            End If
            i = i + 1
        Next

        Dim ds As New DataSet
        ds.Tables.Add(dt)

        Return ds
    End Function

    Private Function GetGoalData() As DataTable
        Dim strSql As String = String.Format("SELECT tblDepartment.departName, SUM(tblGoalSetting.PSCE_ContainmentLoss) AS PSCE_ContainmentLoss, SUM(tblGoalSetting.PSCE_PSNM) AS PSCE_PSNM, SUM(tblGoalSetting.percentActionCompleted) AS percentActionCompleted, 
                                              SUM(tblGoalSetting.percentLeadershipFieldVisibility) AS percentLeadershipFieldVisibility, SUM(tblGoalSetting.complianceProactive) AS complianceProactive, SUM(tblGoalSetting.secondEyeSafety) AS secondEyeSafety
                                              FROM tblGoalSetting INNER JOIN tblDepartment ON tblGoalSetting.departId = tblDepartment.departId 
                                              WHERE (tblGoalSetting.year = {0}) AND (tblGoalSetting.month <= {1}) 
                                              GROUP BY tblDepartment.departName 
                                              ORDER BY tblDepartment.departName", rcbYear.SelectedValue, rcbMonth.SelectedValue)

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
        chartDataBind()
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
        RadHtmlChart11.DataBind()
        'RadHtmlChart12.DataBind()
        'RadHtmlChart13.DataBind()
        RadHtmlChart14.DataBind()
    End Sub

    Dim totalActionNumber As Integer
    Dim totalObserve As Integer
    Dim totalPSCE_ContainmentLoss As Integer
    Dim totalPSCE_PSNM As Integer
    Dim totalpercentActionComplete As Double
    Dim totalpercentLeadershipVisibility As Double
    Dim totalproactiveCompliance As Integer
    Dim totalsecondEye As Integer
    Dim totalinjuryNearMiss As Integer
    Dim totalreliabilityHRO As Integer
    Dim totalqualityHRO As Integer
    Dim totalreliability As Integer
    Dim totalpercentOFIIden As Double
    Dim totalpercentPUPFieldAss As Double
    Dim totalpercentLCSFieldAss As Double
    Dim totalpercentOffHour As Double

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

    Protected Sub btShowGoalLine_Click(sender As Object, e As EventArgs) Handles btShowGoalLine.Click
        RadHtmlChart1.PlotArea.Series(1).Visible = chkShowPsceContainment.Checked
        RadHtmlChart2.PlotArea.Series(1).Visible = chkShowPscePsnm.Checked
        RadHtmlChart3.PlotArea.Series(1).Visible = chkShowActionCompleted.Checked
        RadHtmlChart4.PlotArea.Series(1).Visible = chkShowFieldVisibility.Checked
        RadHtmlChart5.PlotArea.Series(1).Visible = chkShowProactiveCompliance.Checked
        RadHtmlChart6.PlotArea.Series(1).Visible = chkShowSecondEye.Checked
        AllChartDataBind()
    End Sub
End Class