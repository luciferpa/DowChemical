Imports System.Data.SqlClient
Imports System.Threading
Imports Telerik.Web.UI

Public Class rpOverallPerformance
    Inherits Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Dim reCalIntervalTime As Integer = 360
    Dim reCalIntervalTimeAdmin As Integer = 150

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

            'get interval time
            Dim rpInterval As New cReportInterval
            reCalIntervalTimeAdmin = rpInterval.GetIntervalAdmin()
            reCalIntervalTime = rpInterval.GetIntervalUser()

            'update last data
            Dim rcbIndex As Integer = rcbYear.FindItemIndexByValue(Now.Year)
            rcbYear.SelectedIndex = rcbIndex
        End If

        RadPanelBar1.Items.FindItemByText("REPORT").Items.FindItemByText("Overall Performance").Selected = True
        If User.IsInRole("SYSTEM ADMIN") Or User.IsInRole("FACILITY ADMIN") Then
            Dim SettingItem As RadPanelItem = RadPanelBar1.Items.FindItemByText("SETTING")
            SettingItem.Visible = True
            If User.IsInRole("FACILITY ADMIN") Then
                SettingItem.Items.FindItemByText("DEPARTMENT").Visible = False
                SettingItem.Items.FindItemByText("CONTRACTOR").Visible = False
                SettingItem.Items.FindItemByText("CATEGORY").Visible = False
                SettingItem.Items.FindItemByText("GOAL SETTING").Visible = False
                SettingItem.Items.FindItemByText("OFF HOUR SETTING").Visible = False
                SettingItem.Items.FindItemByText("IMPORT DATA").Visible = False

                btGenReport.Visible = False
                'btGenReportSelMonth.Visible = False
            Else                
                btGenReport.Visible = True
                'btGenReportSelMonth.Visible = True
                reCalIntervalTime = reCalIntervalTimeAdmin
            End If
        End If

        Dim rp As New cReport2(rcbYear.SelectedValue)
        Dim currentTime As DateTime = Now()
        Dim LastUpdate As DateTime = rp.chkLastUpdate(currentTime)
        Dim chkPoint As DateTime = DateAdd(DateInterval.Minute, reCalIntervalTime, LastUpdate)
        If LastUpdate = currentTime OrElse chkPoint < Now() Then
            GenerateReport(Now.Month)
            GenerateReport(Now.Month - 1)
        End If
    End Sub

    Private Sub RadPanelBar1_ItemClick(sender As Object, e As RadPanelBarEventArgs) Handles RadPanelBar1.ItemClick
        If e.Item.Items.Count > 0 Then
            If e.Item.Text = "SETTING" Then
                e.Item.Selected = False
                RadPanelBar1.Items.FindItemByText("REPORT").Selected = True
            End If
            If e.Item.Text = "REPORT" Then
                RadPanelBar1.Items.FindItemByText("REPORT").Items.FindItemByText("Overall Performance").Selected = True
            End If
        End If
    End Sub

    Private Sub rcbDepartment_DataBound(sender As Object, e As EventArgs) Handles rcbDepartment.DataBound
        Dim rcb As RadComboBox = sender
        rcb.Items.Insert(0, New RadComboBoxItem("Show All Department", "0"))
    End Sub

    Protected Sub btGenReport_Click(sender As Object, e As EventArgs) Handles btGenReport.Click
        Page.Server.ScriptTimeout = 4000
        'check database exist
        Dim yearReport As Integer = rcbYear.SelectedValue
        Dim rp As New cReport2(yearReport)
        If Not rp.chkRpOverallExist(0) Then rp.createThisYearAllDepartment(yearReport)

        Response.Redirect("~/Report/autoReport.aspx")


        ''-- code before build autoReport
        'For i As Integer = 1 To Now.Month
        '    GenerateReport(i)
        'Next

        'Dim rcbIndex As Integer = rcbYear.FindItemIndexByValue(Now.Year)
        'rcbYear.SelectedIndex = rcbIndex
        'rcbDepartment.SelectedIndex = 0
        'rlvIndicator.Rebind()
    End Sub

    Protected Sub btViewReport_Click(sender As Object, e As EventArgs) Handles btViewReport.Click
        Dim yearReport As Integer = rcbYear.SelectedValue
        Dim departReport As Integer = rcbDepartment.SelectedValue

        If departReport <> 0 Then
            'check last update
            Dim rp As New cReport2(yearReport)
            Dim currentTime As DateTime = Now()
            Dim LastUpdate As DateTime = rp.chkLastUpdate(currentTime, departReport)
            Dim chkPoint As DateTime = DateAdd(DateInterval.Minute, reCalIntervalTime, LastUpdate)
            If LastUpdate = currentTime OrElse chkPoint < Now() Then
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
        End If

        rlvIndicator.Rebind()
    End Sub

    Private Sub GenerateReport(ByVal sMonth As Integer)
        Dim yearReport As Integer = rcbYear.SelectedValue
        Dim rp As New cReport2(yearReport)

        If Not rp.chkEmpHistoricalExist(yearReport, sMonth) Then
            rp.createThisMonthEmpHistorical(sMonth)
        Else
            rp.resetThisMonthEmpHistorical(sMonth)
        End If

        Dim WorkingHourPerMonth As Integer = 9800
        Dim TotalObserver_fsfl As Integer = rp.CountEmployee_fsfl()
        Dim ManPowerWorkingHourPerMonth As Integer = WorkingHourPerMonth * TotalObserver_fsfl

        Dim thisMonth As Integer = sMonth
        Dim totalActionNumber As Integer = rp.CountDocTotalActionNum(thisMonth)
        Dim totalObserv As Integer = rp.CountDocTotalObserv(thisMonth)
        If totalActionNumber > 0 Then
            'set last update protect other user gen-report
            setLastUpdateReport(thisMonth)

            'count other data
            Dim PSCE_ContainmentLossCount As Integer = rp.CountSubCategory(1013, thisMonth)
            Dim PSCE_PSNMCount As Integer = rp.CountSubCategory(1014, thisMonth)

            '-- count from observe list, send personal score
            Dim observeComplete As Integer = rp.CountStatusObserve(1003, thisMonth)
            Dim observeRecognition As Integer = rp.CountStatusObserve(1001, thisMonth)
            Dim observeTotal As Integer = rp.CountStatusObserve(1000, thisMonth)        'update Column "actionTotal" in [tblRpEmpHistorical]

            '-- count from action number
            rp.CountStatusActionNum(thisMonth)
            Dim actionNumComplete As Integer = rp.CountActionNumComplete
            Dim actionNumRecog As Integer = rp.CountActionNumRecog
            Dim percentActionComplete As Decimal = 0
            If totalActionNumber <> actionNumRecog Then percentActionComplete = actionNumComplete / (totalActionNumber - actionNumRecog)

            '-- count from observ
            Dim sumOfDuration_fsfl As Integer = rp.SumOfDuration_fsfl(thisMonth)
            Dim percentLeadershipVisibility As Decimal = 0
            If TotalObserver_fsfl <> 0 Then percentLeadershipVisibility = sumOfDuration_fsfl / ManPowerWorkingHourPerMonth

            Dim proactiveCompliance As Integer = rp.CountFailurePoint(1014, thisMonth)
            Dim secondEye As Integer = rp.CountSecondEye(thisMonth)

            Dim injuryNearMiss As Integer = rp.CountSubCategory(1010, thisMonth)
            Dim reliability_wHRO As Integer = rp.CountCategory(1003, thisMonth, True)
            Dim quality_wHRO As Integer = rp.CountCategory(1002, thisMonth, True)
            Dim reliability As Integer = rp.CountCategory(1003, thisMonth, False)

            Dim ProcedureUsed As Integer = rp.CountFailurePoint(1003, thisMonth)
            Dim Safety As Integer = rp.CountCategory(1001, thisMonth, False)
            Dim percentPUPFieldAss As Decimal = 0
            Dim percentLCSFieldAss As Decimal = 0
            If Safety <> 0 Then
                Dim LCS As Integer = rp.CountSubCategory(1011, thisMonth)
                percentPUPFieldAss = ProcedureUsed / Safety
                percentLCSFieldAss = LCS / Safety
            End If

            '-- count from action number
            rp.ActionNumStatus(thisMonth)
            Dim actionRecogAll As Decimal = rp.CountRecogAll()
            Dim offHour As Integer = rp.CountOffHour()

            Dim percentOfiIdentified As Decimal = 0
            Dim percentOffHour As Decimal = 0
            If totalActionNumber > 0 Then
                percentOfiIdentified = 1 - (actionRecogAll / totalActionNumber)
                percentOffHour = offHour / totalActionNumber
            End If



            'update all department
            Dim strUpd As String = "UPDATE tblRpOverallOps SET totalActionNumber = @totalActionNumber, totalObserve = @totalObserve, PSCE_ContainmentLoss = @PSCE_ContainmentLoss, PSCE_PSNM = @PSCE_PSNM, 
                                            actionComplete = @actionComplete, actionRecognition = @actionRecognition, percentActionComplete = @percentActionComplete, 
                                            leadershipVisibility_fsfl = @leadershipVisibility_fsfl, percentLeadershipVisibility = @percentLeadershipVisibility, proactiveCompliance = @proactiveCompliance, 
                                            secondEye = @secondEye, injuryNearMiss = @injuryNearMiss, reliability_wHRO = @reliability_wHRO, quality_wHRO = @quality_wHRO, reliability = @reliability, 
                                            percentOFIIden = @percentOFIIden, percentPUPFieldAss = @percentPUPFieldAss, percentLCSFieldAss = @percentLCSFieldAss, 
                                            offHourCount = @offHourCount, percentOffHour = @percentOffHour, lastUpdate = @lastUpdate 
                                            WHERE departId = @departId AND month = @month AND year = @year"

            Dim conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strUpd, conn)
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
            command.Parameters.Add("@percentOFIIden", SqlDbType.SmallMoney).Value = percentOfiIdentified
            command.Parameters.Add("@percentPUPFieldAss", SqlDbType.SmallMoney).Value = percentPUPFieldAss
            command.Parameters.Add("@percentLCSFieldAss", SqlDbType.SmallMoney).Value = percentLCSFieldAss
            command.Parameters.Add("@offHourCount", SqlDbType.VarChar).Value = offHour.ToString & "/" & totalActionNumber.ToString
            command.Parameters.Add("@percentOffHour", SqlDbType.SmallMoney).Value = percentOffHour
            command.Parameters.Add("@lastUpdate", SqlDbType.DateTime).Value = Now

            command.Parameters.Add("@departId", SqlDbType.Int).Value = 0        'for all department
            command.Parameters.Add("@month", SqlDbType.Int).Value = thisMonth
            command.Parameters.Add("@year", SqlDbType.Int).Value = yearReport
            Dim result As Integer = command.ExecuteNonQuery()
        End If
    End Sub
    Private Sub setLastUpdateReport(ByVal sMonth As Integer)
        Dim strUpd As String = "UPDATE tblRpOverallOps SET lastUpdate = @lastUpdate WHERE departId = @departId AND month = @month AND year = @year"

        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@lastUpdate", SqlDbType.DateTime).Value = Now
        command.Parameters.Add("@departId", SqlDbType.Int).Value = 0        'for all department
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@year", SqlDbType.Int).Value = rcbYear.SelectedValue
        Dim result As Integer = command.ExecuteNonQuery()
    End Sub

    Dim totalActionNumber As Integer
    Dim totalObserve As Integer
    Dim totalActionComplete As Integer
    Dim totalActionRecognition As Integer
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
    Dim totaloffHour As Integer
    Dim totalpercentOffHour As Integer
    Dim monthDivider As Integer

    Private Sub rlvIndicator_ItemDataBound(sender As Object, e As RadListViewItemEventArgs) Handles rlvIndicator.ItemDataBound
        If e.Item.ItemType = RadListViewItemType.DataItem OrElse e.Item.ItemType = RadListViewItemType.AlternatingItem Then
            Dim item As RadListViewDataItem = TryCast(e.Item, RadListViewDataItem)

            Dim hfMonthItem As HiddenField = item.FindControl("hfMonth")
            Dim sMonth As Integer = CInt(hfMonthItem.Value)
            Dim hfYearItem As HiddenField = item.FindControl("hfYear")
            Dim sYear As Integer = CInt(hfYearItem.Value)

            Dim lbTotalActionLabel As Label = item.FindControl("TotalActionLabel")
            Dim lbTotalObserveLabel As Label = item.FindControl("TotalObserveLabel")
            Dim lbTotalActionCompleteLabel As Label = item.FindControl("TotalActionCompleteLabel")
            Dim lbTotalActionRecogLabel As Label = item.FindControl("TotalActionRecogLabel")
            Dim lbPSCE_ContainmentLoss As Label = item.FindControl("PSCE_ContainmentLossLabel")
            Dim lbPSCE_PSNM As Label = item.FindControl("PSCE_PSNMLabel")
            Dim lbpercentActionComplete As Label = item.FindControl("percentActionCompleteLabel")
            Dim lbpercentLeadershipVisibility As Label = item.FindControl("percentLeadershipVisibilityLabel")
            Dim lbproactiveCompliance As Label = item.FindControl("proactiveComplianceLabel")
            Dim lbsecondEye As Label = item.FindControl("secondEyeLabel")
            Dim lbinjuryNearMiss As Label = item.FindControl("injuryNearMissLabel")
            Dim lbreliabilityHRO As Label = item.FindControl("reliabilityHROLabel")
            Dim lbqualityHRO As Label = item.FindControl("qualityHROLabel")
            Dim lbreliability As Label = item.FindControl("reliabilityLabel")
            Dim lbpercentOFIIden As Label = item.FindControl("percentOFIIdenLabel")
            Dim lbpercentPUPFieldAss As Label = item.FindControl("percentPUPFieldAssLabel")
            Dim lbpercentLCSFieldAss As Label = item.FindControl("percentLCSFieldAssLabel")
            Dim lboffHour As Label = item.FindControl("offHourLabel")
            Dim lbpercentOffHour As Label = item.FindControl("percentOffHourLabel")

            If sYear < Now.Year OrElse (sYear = Now.Year And sMonth <= Now.Month) Then
                If CInt(lbTotalActionLabel.Text) <> 0 Then monthDivider = monthDivider + 1

                totalActionNumber = totalActionNumber + lbTotalActionLabel.Text
                totalObserve = totalObserve + lbTotalObserveLabel.Text
                totalActionComplete = totalActionComplete + lbTotalActionCompleteLabel.Text
                totalActionRecognition = totalActionRecognition + lbTotalActionRecogLabel.Text

                totalPSCE_ContainmentLoss = totalPSCE_ContainmentLoss + lbPSCE_ContainmentLoss.Text
                totalPSCE_PSNM = totalPSCE_PSNM + lbPSCE_PSNM.Text
                totalproactiveCompliance = totalproactiveCompliance + lbproactiveCompliance.Text
                totalsecondEye = totalsecondEye + lbsecondEye.Text
                totalinjuryNearMiss = totalinjuryNearMiss + lbinjuryNearMiss.Text
                totalreliabilityHRO = totalreliabilityHRO + lbreliabilityHRO.Text
                totalqualityHRO = totalqualityHRO + lbqualityHRO.Text
                totalreliability = totalreliability + lbreliability.Text

                totalpercentActionComplete = totalpercentActionComplete + lbpercentActionComplete.Text.Substring(0, lbpercentActionComplete.Text.IndexOf(" %"))
                totalpercentLeadershipVisibility = totalpercentLeadershipVisibility + lbpercentLeadershipVisibility.Text.Substring(0, lbpercentLeadershipVisibility.Text.IndexOf(" %"))
                totalpercentOFIIden = totalpercentOFIIden + lbpercentOFIIden.Text.Substring(0, lbpercentOFIIden.Text.IndexOf(" %"))
                totalpercentPUPFieldAss = totalpercentPUPFieldAss + lbpercentPUPFieldAss.Text.Substring(0, lbpercentPUPFieldAss.Text.IndexOf(" %"))
                totalpercentLCSFieldAss = totalpercentLCSFieldAss + lbpercentLCSFieldAss.Text.Substring(0, lbpercentLCSFieldAss.Text.IndexOf(" %"))

                Dim monthOffHour As Integer
                Dim offHourStr As String = lboffHour.Text
                If offHourStr <> "" Then
                    monthOffHour = offHourStr.Substring(0, offHourStr.IndexOf("/"))
                    totaloffHour = totaloffHour + monthOffHour
                End If

                'show last update
                If sMonth = Now.Month And DataBinder.Eval(item.DataItem, "lastUpdate") IsNot DBNull.Value Then
                    lbLastUpdate.Text = "last update: " & DataBinder.Eval(item.DataItem, "lastUpdate")
                End If
            Else
                lbTotalActionLabel.Text = "-"
                lbTotalObserveLabel.Text = "-"
                lbTotalActionCompleteLabel.Text = "-"
                lbTotalActionRecogLabel.Text = "-"

                lbPSCE_ContainmentLoss.Text = "-"
                lbPSCE_PSNM.Text = "-"
                lbpercentActionComplete.Text = "-"
                lbpercentLeadershipVisibility.Text = "-"
                lbproactiveCompliance.Text = "-"
                lbsecondEye.Text = "-"
                lbinjuryNearMiss.Text = "-"
                lbreliabilityHRO.Text = "-"
                lbqualityHRO.Text = "-"
                lbreliability.Text = "-"
                lbpercentOFIIden.Text = "-"
                lbpercentPUPFieldAss.Text = "-"
                lbpercentLCSFieldAss.Text = "-"
                lboffHour.Text = "-"
                lbpercentOffHour.Text = "-"
            End If

            If sYear <= Now.Year And sMonth = 13 Then
                lbTotalActionLabel.Text = totalActionNumber.ToString
                lbTotalObserveLabel.Text = totalObserve.ToString
                lbTotalActionCompleteLabel.Text = totalActionComplete.ToString
                lbTotalActionRecogLabel.Text = totalActionRecognition.ToString

                lbPSCE_ContainmentLoss.Text = totalPSCE_ContainmentLoss.ToString
                lbPSCE_PSNM.Text = totalPSCE_PSNM.ToString
                lbproactiveCompliance.Text = totalproactiveCompliance.ToString
                lbsecondEye.Text = totalsecondEye.ToString
                lbinjuryNearMiss.Text = totalinjuryNearMiss.ToString
                lbreliabilityHRO.Text = totalreliabilityHRO.ToString
                lbqualityHRO.Text = totalqualityHRO.ToString
                lbreliability.Text = totalreliability.ToString

                If monthDivider <> 0 Then
                    lbpercentActionComplete.Text = FormatNumber(totalpercentActionComplete / monthDivider, 2, , , TriState.True) & " %"
                    lbpercentLeadershipVisibility.Text = FormatNumber(totalpercentLeadershipVisibility / monthDivider, 2, , , TriState.True) & " %"
                    lbpercentOFIIden.Text = FormatNumber(totalpercentOFIIden / monthDivider, 2, , , TriState.True) & " %"
                    lbpercentPUPFieldAss.Text = FormatNumber(totalpercentPUPFieldAss / monthDivider, 2, , , TriState.True) & " %"
                    lbpercentLCSFieldAss.Text = FormatNumber(totalpercentLCSFieldAss / monthDivider, 2, , , TriState.True) & " %"
                Else
                    lbpercentActionComplete.Text = "-"
                    lbpercentLeadershipVisibility.Text = "-"
                    lbpercentOFIIden.Text = "-"
                    lbpercentPUPFieldAss.Text = "-"
                    lbpercentLCSFieldAss.Text = "-"
                End If

                lboffHour.Text = totaloffHour.ToString & "/" & totalActionNumber.ToString
                If totalActionNumber <> 0 Then
                    lbpercentOffHour.Text = FormatNumber((100 * totaloffHour / totalActionNumber).ToString, 2, , , TriState.True) & " %"
                Else
                    lbpercentOffHour.Text = "-"
                End If
            End If
        End If
    End Sub

    Public Function ShowNothingValue(ByVal value As Object) As String
        If value Is Nothing Then
            Return "-"
        End If

        Return value.ToString
    End Function

    Protected Sub btGenReportSelMonth_Click(sender As Object, e As ImageClickEventArgs) Handles btGenReportSelMonth.Click
        pnSelMonth.Visible = Not pnSelMonth.Visible
        If pnSelMonth.Visible Then
            For i As Integer = 1 To 12
                Dim bt As Button = pnSelMonth.FindControl("Gen" & i.ToString)
                If i <= Now.Month Then
                    bt.Visible = True
                Else
                    bt.Visible = False
                End If
            Next
        End If
    End Sub

    Protected Sub Gen1_Click(sender As Object, e As EventArgs) Handles Gen1.Click
        'GenerateReport(1)
        Response.Redirect("autoReport?auto=0&month=1")
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen2_Click(sender As Object, e As EventArgs) Handles Gen2.Click
        'GenerateReport(2)
        Response.Redirect("autoReport?auto=0&month=2")
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen3_Click(sender As Object, e As EventArgs) Handles Gen3.Click
        'GenerateReport(3)
        Response.Redirect("autoReport?auto=0&month=3")
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen4_Click(sender As Object, e As EventArgs) Handles Gen4.Click
        'GenerateReport(4)
        Response.Redirect("autoReport?auto=0&month=4")
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen5_Click(sender As Object, e As EventArgs) Handles Gen5.Click
        'GenerateReport(5)
        Response.Redirect("autoReport?auto=0&month=5")
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen6_Click(sender As Object, e As EventArgs) Handles Gen6.Click
        'GenerateReport(6)
        Response.Redirect("autoReport?auto=0&month=6")
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen7_Click(sender As Object, e As EventArgs) Handles Gen7.Click
        'GenerateReport(7)
        Response.Redirect("autoReport?auto=0&month=7")
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen8_Click(sender As Object, e As EventArgs) Handles Gen8.Click
        'GenerateReport(8)
        Response.Redirect("autoReport?auto=0&month=8")
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen9_Click(sender As Object, e As EventArgs) Handles Gen9.Click
        'GenerateReport(9)
        Response.Redirect("autoReport?auto=0&month=9")
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen10_Click(sender As Object, e As EventArgs) Handles Gen10.Click
        'GenerateReport(10)
        Response.Redirect("autoReport?auto=0&month=10")
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen11_Click(sender As Object, e As EventArgs) Handles Gen11.Click
        'GenerateReport(11)
        Response.Redirect("autoReport?auto=0&month=11")
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen12_Click(sender As Object, e As EventArgs) Handles Gen12.Click
        'GenerateReport(12)
        Response.Redirect("autoReport?auto=0&month=12")
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub


End Class