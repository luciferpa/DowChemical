Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class rpOverallPerformChart
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
            Dim rcbIndex As Integer = rcbYear.FindItemIndexByValue(Now.Year)
            rcbYear.SelectedIndex = rcbIndex

            chartDataBind()
        End If

        If User.IsInRole("SYSTEM ADMIN") Then
            RadPanelBar1.Items.FindItemByText("SETTING").Visible = True
        End If
    End Sub

    Private Sub rcbDepartment_DataBound(sender As Object, e As EventArgs) Handles rcbDepartment.DataBound
        Dim rcb As RadComboBox = sender
        rcb.Items.Insert(0, New RadComboBoxItem("Show All Department", "0"))
    End Sub

    'Dim MaxValueChart4 As Double
    Private Sub chartDataBind(Optional ByVal departId As Integer = 0)
        Dim chartData As DataSet = GetData(departId)
        RadHtmlChart1.DataSource = chartData
        RadHtmlChart2.DataSource = chartData
        RadHtmlChart3.DataSource = chartData
        RadHtmlChart4.DataSource = chartData

        '-- fix scale
        'Dim maxchartValue As Double = MaxValueChart4 * 1.1
        'If maxchartValue < 0.1 Then
        '    RadHtmlChart4.PlotArea.YAxis.MaxValue = Math.Ceiling(maxchartValue * 100) / 100
        'Else
        '    RadHtmlChart4.PlotArea.YAxis.MaxValue = Math.Ceiling(maxchartValue * 10) / 10
        'End If

        RadHtmlChart5.DataSource = chartData
        RadHtmlChart6.DataSource = chartData
        RadHtmlChart7.DataSource = chartData
        RadHtmlChart8.DataSource = chartData
        RadHtmlChart9.DataSource = chartData
        RadHtmlChart10.DataSource = chartData
        RadHtmlChart11.DataSource = chartData
        RadHtmlChart12.DataSource = chartData
        RadHtmlChart13.DataSource = chartData
        RadHtmlChart14.DataSource = chartData

        AllChartDataBind()
    End Sub

    Private Function GetData(ByVal departId As Integer) As DataSet
        Dim strSql As String = String.Format("SELECT month, monthDesc, PSCE_ContainmentLoss, PSCE_PSNM, percentActionComplete, percentLeadershipVisibility, proactiveCompliance, secondEye, injuryNearMiss, reliability_wHRO, quality_wHRO, reliability, percentOFIIden, percentPUPFieldAss, percentLCSFieldAss, percentOffHour, totalActionNumber FROM dbo.tblRpOverallOps 
                                WHERE (rpType <> 'g') AND (departId = {0}) AND (year = {1}) ORDER BY month", departId.ToString, rcbYear.SelectedValue)

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

        Dim colorBar As DataColumn = dt.Columns.Add("colorBar", Type.GetType("System.String"))
        Dim goalPsceContainmentLoss As DataColumn = dt.Columns.Add("goalPSCE_ContainmentLoss", Type.GetType("System.String"))
        Dim goalPscePsnm As DataColumn = dt.Columns.Add("goalPSCE_PSNM", Type.GetType("System.String"))
        Dim goalPercentActionCompleted As DataColumn = dt.Columns.Add("goalPercentActionCompleted", Type.GetType("System.String"))
        Dim goalPercentLeadershipFieldVisibility As DataColumn = dt.Columns.Add("goalPercentLeadershipFieldVisibility", Type.GetType("System.String"))
        Dim goalComplianceProactive As DataColumn = dt.Columns.Add("goalComplianceProactive", Type.GetType("System.String"))
        Dim goalSecondEyeSafety As DataColumn = dt.Columns.Add("goalSecondEyeSafety", Type.GetType("System.String"))

        Dim dtGoal As DataTable = GetGoalData(departId)
        Dim dtGoalCount As Integer = dtGoal.Rows.Count

        totalPSCE_ContainmentLoss = 0
        totalPSCE_PSNM = 0
        totalpercentActionComplete = 0
        totalpercentLeadershipVisibility = 0
        totalproactiveCompliance = 0
        totalsecondEye = 0

        totalinjuryNearMiss = 0
        totalreliabilityHRO = 0
        totalqualityHRO = 0
        totalreliability = 0
        totalpercentOFIIden = 0
        totalpercentPUPFieldAss = 0
        totalpercentLCSFieldAss = 0
        totalpercentOffHour = 0

        'MaxValueChart4 = 0
        Dim monthDivider As Integer = 0
        Dim i As Integer = 0
        For Each row As DataRow In dt.Rows      'loop (JAN - DEC) + YTD
            If i <> 12 Then
                totalPSCE_ContainmentLoss = totalPSCE_ContainmentLoss + CInt(row.Field(Of Integer)(2))
                totalPSCE_PSNM = totalPSCE_PSNM + CInt(row.Field(Of Integer)(3))
                totalpercentActionComplete = totalpercentActionComplete + CDbl(row.Field(Of Decimal)(4))
                totalpercentLeadershipVisibility = totalpercentLeadershipVisibility + CDbl(row.Field(Of Decimal)(5))
                totalproactiveCompliance = totalproactiveCompliance + CInt(row.Field(Of Integer)(6))
                totalsecondEye = totalsecondEye + CInt(row.Field(Of Integer)(7))

                totalinjuryNearMiss = totalinjuryNearMiss + CInt(row.Field(Of Integer)(8))
                totalreliabilityHRO = totalreliabilityHRO + CInt(row.Field(Of Integer)(9))
                totalqualityHRO = totalqualityHRO + CInt(row.Field(Of Integer)(10))
                totalreliability = totalreliability + CInt(row.Field(Of Integer)(11))
                totalpercentOFIIden = totalpercentOFIIden + CDbl(row.Field(Of Decimal)(12))
                totalpercentPUPFieldAss = totalpercentPUPFieldAss + CDbl(row.Field(Of Decimal)(13))
                totalpercentLCSFieldAss = totalpercentLCSFieldAss + CDbl(row.Field(Of Decimal)(14))
                totalpercentOffHour = totalpercentOffHour + CDbl(row.Field(Of Decimal)(15))

                If CInt(row.Field(Of Integer)(16)) <> 0 Then monthDivider = monthDivider + 1     'check from totalActionNumber

                'add goal value
                If i < dtGoalCount Then
                    If departId <> 0 Then
                        dt.Rows(i).Item("goalPSCE_ContainmentLoss") = CInt(dtGoal.Rows(i).Item("PSCE_ContainmentLoss"))
                        dt.Rows(i).Item("goalPSCE_PSNM") = CInt(dtGoal.Rows(i).Item("PSCE_PSNM"))
                        dt.Rows(i).Item("goalPercentActionCompleted") = CDbl(dtGoal.Rows(i).Item("percentActionCompleted")) / 100
                        dt.Rows(i).Item("goalPercentLeadershipFieldVisibility") = CDbl(dtGoal.Rows(i).Item("percentLeadershipFieldVisibility")) / 100
                        dt.Rows(i).Item("goalComplianceProactive") = CInt(dtGoal.Rows(i).Item("complianceProactive"))
                        dt.Rows(i).Item("goalSecondEyeSafety") = CInt(dtGoal.Rows(i).Item("secondEyeSafety"))
                    Else
                        dt.Rows(i).Item("goalPSCE_ContainmentLoss") = Math.Ceiling(CInt(dtGoal.Rows(i).Item("PSCE_ContainmentLoss")) / 12)
                        dt.Rows(i).Item("goalPSCE_PSNM") = Math.Ceiling(CInt(dtGoal.Rows(i).Item("PSCE_PSNM")) / 12)
                        dt.Rows(i).Item("goalPercentActionCompleted") = CDbl(dtGoal.Rows(i).Item("percentActionCompleted")) / 100
                        dt.Rows(i).Item("goalPercentLeadershipFieldVisibility") = CDbl(dtGoal.Rows(i).Item("percentLeadershipFieldVisibility")) / 100
                        dt.Rows(i).Item("goalComplianceProactive") = Math.Ceiling(CInt(dtGoal.Rows(i).Item("complianceProactive")) / 12)
                        dt.Rows(i).Item("goalSecondEyeSafety") = Math.Ceiling(CInt(dtGoal.Rows(i).Item("secondEyeSafety")) / 12)
                    End If
                End If

                ''-- find Max data (Chart4 Only)
                'If dt.Rows(i).Item("percentLeadershipVisibility") > MaxValueChart4 Then
                '    MaxValueChart4 = dt.Rows(i).Item("percentLeadershipVisibility")
                'End If
                'If dt.Rows(i).Item("goalPercentLeadershipFieldVisibility") > MaxValueChart4 Then
                '    MaxValueChart4 = dt.Rows(i).Item("goalPercentLeadershipFieldVisibility")
                'End If
                'If totalpercentLeadershipVisibility > MaxValueChart4 Then
                '    MaxValueChart4 = totalpercentLeadershipVisibility
                'End If
            Else
                dt.Rows(i).Item("colorBar") = "#5cb85c"

                If monthDivider <> 0 Then
                    dt.Rows(i).Item("PSCE_ContainmentLoss") = totalPSCE_ContainmentLoss
                    dt.Rows(i).Item("PSCE_PSNM") = totalPSCE_PSNM
                    dt.Rows(i).Item("percentActionComplete") = totalpercentActionComplete / monthDivider
                    dt.Rows(i).Item("percentLeadershipVisibility") = totalpercentLeadershipVisibility / monthDivider
                    dt.Rows(i).Item("proactiveCompliance") = totalproactiveCompliance
                    dt.Rows(i).Item("secondEye") = totalsecondEye

                    dt.Rows(i).Item("injuryNearMiss") = totalinjuryNearMiss
                    dt.Rows(i).Item("reliability_wHRO") = totalreliabilityHRO
                    dt.Rows(i).Item("quality_wHRO") = totalqualityHRO
                    dt.Rows(i).Item("reliability") = totalreliability
                    dt.Rows(i).Item("percentOFIIden") = totalpercentOFIIden / monthDivider
                    dt.Rows(i).Item("percentPUPFieldAss") = totalpercentPUPFieldAss / monthDivider
                    dt.Rows(i).Item("percentLCSFieldAss") = totalpercentLCSFieldAss / monthDivider
                    dt.Rows(i).Item("percentOffHour") = totalpercentOffHour / monthDivider
                End If
            End If
            i = i + 1
        Next

        Dim ds As New DataSet
        ds.Tables.Add(dt)

        Return ds
    End Function

    Private Function GetGoalData(ByVal departId As Integer) As DataTable
        Dim strSql As String = String.Format("SELECT month, PSCE_ContainmentLoss, PSCE_PSNM, percentActionCompleted, percentLeadershipFieldVisibility, complianceProactive, secondEyeSafety FROM tblGoalSetting
                                WHERE (departId = {0}) AND (year = {1}) ", departId.ToString, rcbYear.SelectedValue)
        If departId <> 0 Then strSql = strSql & "AND (goalType = 0) " Else strSql = strSql & "AND (goalType = 1) "
        strSql = strSql & "ORDER BY month"

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

    'Protected Sub rcbYear_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbYear.SelectedIndexChanged
    '    AllChartDataBind()
    'End Sub

    'Protected Sub rcbDepartment_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbDepartment.SelectedIndexChanged
    '    AllChartDataBind()
    'End Sub

    Protected Sub btViewReport_Click(sender As Object, e As EventArgs) Handles btViewReport.Click
        Dim departReport As Integer = rcbDepartment.SelectedValue
        If departReport <> 0 Then
            Dim yearReport As Integer = rcbYear.SelectedValue

            Dim rp As New cReport2(yearReport)
            Dim currentTime As DateTime = Now()
            Dim LastUpdate As DateTime = rp.chkLastUpdate(currentTime, departReport)
            Dim chkPoint As DateTime = DateAdd(DateInterval.Minute, reCalIntervalTime, LastUpdate)

            reGetDepartmartData(departReport)
            chartDataBind(departReport)

            'for timeInterval
            'If LastUpdate = currentTime OrElse chkPoint < Now() Then
            '    reGetDepartmartData(departReport)
            '    GetData(departReport)
            'End If
        Else
            chartDataBind()
        End If
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
        RadHtmlChart12.DataBind()
        RadHtmlChart13.DataBind()
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