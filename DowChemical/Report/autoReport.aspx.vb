Imports System.Data.SqlClient
Imports System.Threading
Imports Telerik.Web.UI

Public Class autoReport
    Inherits Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Dim reCalIntervalTime As Integer = 60
    Dim reCalIntervalTimeAdmin As Integer = 20

    Private Sub MsgBoxRad(ByVal Msg As String, ByVal Width As Integer, ByVal Height As Integer)
        RadWindowManager1.Width = Width
        RadWindowManager1.Height = Height
        RadWindowManager1.RadAlert(Msg, Width + 100, Height + 72, "My Alert", "", "myAlertImage.png")
    End Sub

    Dim oldCookiesDate As Date
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

            '-- get interval time
            Dim rpInterval As New cReportInterval
            reCalIntervalTimeAdmin = rpInterval.GetIntervalAdmin()
            reCalIntervalTime = rpInterval.GetIntervalUser()
            tbnIntervalAdmin.Value = reCalIntervalTimeAdmin
            tbnIntervalUser.Value = reCalIntervalTime

            '-- update last data
            Dim rcbIndex As Integer = rcbYear.FindItemIndexByValue(Now.Year)
            rcbYear.SelectedIndex = rcbIndex
            rcbYear2.SelectedIndex = rcbIndex
            Dim lastMonth As DateTime = DateAdd(DateInterval.Month, -1, Now())
            rcbYearValidate.SelectedIndex = rcbYearValidate.FindItemIndexByValue(lastMonth.Year)
            rcbMonthValidate.SelectedIndex = rcbMonthValidate.FindItemIndexByValue(lastMonth.Month)

            rcbMonth.SelectedIndex = Now.Month - 1
            If Request.QueryString("auto") = "1" Then
                rcbIsAuto.SelectedIndex = 1
                btStart.Visible = False
                rcbMonth.Enabled = False
                rcbYear2.Enabled = False
            Else
                rcbIsAuto.SelectedIndex = 0
                btStart.Visible = True
                If Request.QueryString("month") <> "" Then rcbMonth.SelectedIndex = CInt(Request.QueryString("month")) - 1
                rcbMonth.Enabled = True
                rcbYear2.Enabled = True
                Timer1.Enabled = False
            End If

            '-- interval time
            If Request.QueryString("intv") <> "" Then
                rcbInterval.SelectedValue = Request.QueryString("intv")
            End If

            '-- keep coolies date
            'oldCookiesDate = Response.Cookies(".AspNet.ApplicationCookie").Expires

            '-- offset time
            If Request.QueryString("offset") <> "" Then
                Dim timeOffset As TimeSpan = TimeSpan.Parse(Request.QueryString("offset"))
                rdpCallTime.SelectedTime = timeOffset
                imgClock.ToolTip = rdpCallTime.SelectedTime.ToString
                imgClock.ImageUrl = "~/Images/clock-32.png"
            End If

            '-- validate data
            Timer2.Enabled = False
        Else
            If Request.QueryString("auto") = "1" Then
                rcbMonth.SelectedIndex = Now.Month - 1
                rcbYear2.SelectedValue = Now.Year

                '-- set expire cookies
                If User.IsInRole("SYSTEM ADMIN") Then
                    Dim appCookie As HttpCookie = Request.Cookies(".AspNet.ApplicationCookie")
                    appCookie.Expires = DateTime.Now.AddDays(60)
                    Response.Cookies.Add(appCookie)
                End If                

                '-- watch-dog
                Dim wdCount As Integer = CInt(lbCount.Text)
                Dim wdCountIntv As Integer = CInt(lbCountInterval.Text)
                If wdCount > wdCountIntv Then
                    lbCount.Text = "0"
                    lbCurrent.Text = "0"
                End If
            End If

            If Request.QueryString("auto") = "0" Then
                Response.Cookies(".AspNet.ApplicationCookie").Expires = oldCookiesDate
            End If

            infoboxCssStyle()
        End If

        '-- role
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

                'btGenReport.Visible = False
                'btGenReportSelMonth.Visible = False

                '-- disable rcbIsAuto
                rcbIsAuto.Enabled = False
            Else
                'btGenReport.Visible = True
                'btGenReportSelMonth.Visible = True
                reCalIntervalTime = reCalIntervalTimeAdmin
            End If
        End If
        
        ''-- GenerateReport by User
        'Dim rp As New cReport2(rcbYear.SelectedValue)
        'Dim currentTime As DateTime = Now()
        'Dim LastUpdate As DateTime = rp.chkLastUpdate(currentTime)
        'Dim chkPoint As DateTime = DateAdd(DateInterval.Minute, reCalIntervalTime, LastUpdate)
        'If LastUpdate = currentTime OrElse chkPoint < Now() Then
        '    'GenerateReport(Now.Month)
        '    'GenerateReport(Now.Month - 1)
        'End If
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

    Private Sub rcbDepartment_DataBound(sender As Object, e As EventArgs) Handles rcbDepartment.DataBound, rcbDepartment2.DataBound
        Dim rcb As RadComboBox = sender
        If rcb.ID = "rcbDepartment" Then
            rcb.Items.Insert(0, New RadComboBoxItem("Show All Department", "0"))
        ElseIf rcb.ID = "rcbDepartment2" then
            rcb.Items.Insert(0, New RadComboBoxItem("All Department", "0"))
        End If        
    End Sub

    Protected Sub btGenReport_Click(sender As Object, e As EventArgs) Handles btGenReport.Click
        Page.Server.ScriptTimeout = 4000
        'check database exist
        Dim yearReport As Integer = rcbYear.SelectedValue
        Dim rp As New cReport2(yearReport)
        If Not rp.chkRpOverallExist(0) Then rp.createThisYearAllDepartment(yearReport)

        Response.Redirect("autoReport?auto=1&month=1")
        'For i As Integer = 1 To Now.Month
        '    GenerateReport(i)
        'Next

        Dim rcbIndex As Integer = rcbYear.FindItemIndexByValue(Now.Year)
        rcbYear.SelectedIndex = rcbIndex
        rcbDepartment.SelectedIndex = 0
        rlvIndicator.Rebind()
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

                Dim startMOnth As Integer = 1
                If Now.Month > 3 Then startMOnth = Now.Month - 2

                For i As Integer = startMOnth To Now.Month
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
            End If
        End If
    End Sub

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
        GenerateReport(1)
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen2_Click(sender As Object, e As EventArgs) Handles Gen2.Click
        GenerateReport(2)
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen3_Click(sender As Object, e As EventArgs) Handles Gen3.Click
        GenerateReport(3)
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen4_Click(sender As Object, e As EventArgs) Handles Gen4.Click
        GenerateReport(4)
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen5_Click(sender As Object, e As EventArgs) Handles Gen5.Click
        GenerateReport(5)
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen6_Click(sender As Object, e As EventArgs) Handles Gen6.Click
        GenerateReport(6)
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen7_Click(sender As Object, e As EventArgs) Handles Gen7.Click
        GenerateReport(7)
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen8_Click(sender As Object, e As EventArgs) Handles Gen8.Click
        GenerateReport(8)
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen9_Click(sender As Object, e As EventArgs) Handles Gen9.Click
        GenerateReport(9)
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen10_Click(sender As Object, e As EventArgs) Handles Gen10.Click
        GenerateReport(10)
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen11_Click(sender As Object, e As EventArgs) Handles Gen11.Click
        GenerateReport(11)
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub
    Protected Sub Gen12_Click(sender As Object, e As EventArgs) Handles Gen12.Click
        GenerateReport(12)
        pnSelMonth.Visible = False
        rlvIndicator.Rebind()
    End Sub


    '----------- Auto Report -----------

    Private Sub AddListBox(ByVal msg As String, ByVal fontcolor As Integer)
        If msg IsNot Nothing And msg <> "" Then
            If infobox.Items.Count = 0 Then
                infobox.Items.Add("Massage Info:")
            End If
            msg = msg + Environment.NewLine
            Dim MaxLine As Integer = 32
            If infobox.Items.Count = MaxLine Then
                infobox.Items.RemoveAt(1)
            End If
            infobox.Items.Add(msg)
            Session("fontcolor") = fontcolor
            infoboxCssStyle()
        End If
    End Sub

    Private Sub infoboxCssStyle()
        For i = 0 To infobox.Items.Count - 1
            If i = 0 Or Session("fontcolor") = 0 Then
                infobox.Items(0).Attributes.CssStyle.Add("font-weight", "bold")
                infobox.Items(0).Attributes.CssStyle.Add("color", "#696969")
            Else
                infobox.Items(i).Attributes.CssStyle.Add("font-weight", "normal")
                Select Case Session("fontcolor")
                    Case 0
                        infobox.Items(i).Attributes.CssStyle.Add("color", "#696969")
                    Case 1
                        infobox.Items(i).Attributes.CssStyle.Add("color", "red")
                    Case 2
                        infobox.Items(i).Attributes.CssStyle.Add("color", "blue")
                    Case 3
                        infobox.Items(i).Attributes.CssStyle.Add("color", "#009900")
                    Case Else
                        infobox.Items(i).Attributes.CssStyle.Add("color", "darkgray")
                End Select
            End If
        Next
    End Sub

    Private Sub ClearLabelInfo()
        'lb_totalActionNumber.Text = ""
        'lb_totalObserv.Text = ""
        lb_actionNumComplete.Text = "0"
        lb_actionNumRecog.Text = "0"
        lb_percentActionComplete.Text = "0 %"
        lb_observeComplete.Text = "0"
        lb_observeRecognition.Text = "0"
        lb_PSCE_ContainmentLossCount.Text = "0"
        lb_PSCE_PSNMCount.Text = "0"
        lb_sumOfDuration_fsfl.Text = "0"
        lb_percentLeadershipVisibility.Text = "0 %"
        lb_proactiveCompliance.Text = "0"
        lb_secondEye.Text = "0"
        lb_injuryNearMiss.Text = "0"
        lb_reliability_wHRO.Text = "0"
        lb_quality_wHRO.Text = "0"
        lb_reliability.Text = "0"
        lb_percentOfiIdentified.Text = "0 %"
        lb_percentPUPFieldAss.Text = "0 %"
        lb_percentLCSFieldAss.Text = "0 %"
        lb_offHourCountStr.Text = "../.."
        lb_percentOffHour.Text = "0 %"
    End Sub
    
    Private Sub GenerateRp(ByVal sYear As Integer, ByVal sMonth As Integer, ByRef currentStep As Integer)
        Dim rp As New cReport2(sYear)

        Select Case currentStep
            Case 0  
                hfInProgress.Value = True
                lbStartTime.Text = Now.ToShortDateString & "  " & Now.ToLongTimeString
                lbCurrentMonth.Text = sMonth.ToString & "-" & sYear.ToString
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & " Prepare parameter.... " & lbCurrentMonth.Text, 2)

                If Not rp.chkEmpHistoricalExist(sYear, sMonth) Then
                    rp.createThisMonthEmpHistorical(sMonth)
                Else
                    rp.resetThisMonthEmpHistorical(sMonth)
                End If 

                Dim WorkingHourPerMonth As Integer = 9800
                Dim TotalObserver_fsfl As Integer = rp.CountEmployee_fsfl()
                hfTotalObserver_fsfl.Value = TotalObserver_fsfl
                hfManPowerWorkingHourPerMonth.Value = WorkingHourPerMonth * TotalObserver_fsfl
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count observer fsfl.", 2)
                
                lb_totalActionNumber.Text = rp.CountDocTotalActionNum(sMonth).ToString
                lb_totalObserv.Text = rp.CountDocTotalObserv(sMonth).ToString
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count action & observe. /Action No.=" & lb_totalActionNumber.Text & " Observe=" & lb_totalObserv.Text, 2)

                ClearLabelInfo()
                hfInProgress.Value = False
                currentStep = 1
            Case 1
                hfInProgress.Value = True
                'count other PSCE data
                Dim TotalActionNo As Integer = CInt(lb_totalActionNumber.Text)
                If TotalActionNo > 0 Then
                    lb_PSCE_ContainmentLossCount.Text = rp.CountSubCategory(1013, sMonth)
                    AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count PSCE_ContainmentLoss. /PSCE_ContainmentLossCount=" & lb_PSCE_ContainmentLossCount.Text, 2)
                    lb_PSCE_PSNMCount.Text = rp.CountSubCategory(1014, sMonth)
                    AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count PSCE_PSNM. /PSCE_PSNMCount=" & lb_PSCE_PSNMCount.Text, 2)
                End If
                hfInProgress.Value = False
                currentStep = 2
            Case 2
                hfInProgress.Value = True
                '-- count from observe list, send personal score
                lb_observeComplete.Text = rp.CountStatusObserve(1003, sMonth).ToString
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count observeComplete. /observeComplete=" & lb_observeComplete.Text, 2)
                hfInProgress.Value = False
                currentStep = 3
            Case 3
                hfInProgress.Value = True
                '-- count from observe list, send personal score
                lb_observeRecognition.Text = rp.CountStatusObserve(1001, sMonth).ToString
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count observeRecognition. /observeRecognition=" & lb_observeRecognition.Text, 2)
                hfInProgress.Value = False
                currentStep = 4
            Case 4
                hfInProgress.Value = True
                '-- update Column "actionTotal" in [tblRpEmpHistorical]
                rp.CountStatusObserve(1000, sMonth).ToString        
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Update actionTotal @tblRpEmpHistorical", 2)
                hfInProgress.Value = False
                currentStep = 5
            Case 5
                hfInProgress.Value = True
                '-- count from action number
                rp.CountStatusActionNum(sMonth)
                Dim actionNumComplete As Integer = rp.CountActionNumComplete
                Dim actionNumRecog As Integer = rp.CountActionNumRecog
                lb_actionNumComplete.Text = actionNumComplete.ToString
                lb_actionNumRecog.Text = actionNumRecog.ToString
                Dim TotalActionNo As Integer = CInt(lb_totalActionNumber.Text)
                If TotalActionNo <> actionNumRecog Then lb_percentActionComplete.Text = (actionNumComplete / (TotalActionNo - actionNumRecog)).ToString("P4")
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count percentActionComplete. /percentActionComplete=" & lb_percentActionComplete.Text, 2)
                hfInProgress.Value = False
                currentStep = 6
            Case 6
                hfInProgress.Value = True
                '-- count from observ
                Dim sumOfDuration_fsfl As Integer = rp.SumOfDuration_fsfl(sMonth)
                lb_sumOfDuration_fsfl.Text = sumOfDuration_fsfl.ToString
                Dim TotalObserver_fsfl As Integer = CInt(hfTotalObserver_fsfl.Value)
                If TotalObserver_fsfl <> 0 Then lb_percentLeadershipVisibility.Text = (sumOfDuration_fsfl / CInt(hfManPowerWorkingHourPerMonth.Value)).ToString("P4")
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count percentLeadershipVisibility. /percentLeadershipVisibility=" & lb_percentLeadershipVisibility.Text, 2)
                hfInProgress.Value = False
                currentStep = 7
            Case 7
                hfInProgress.Value = True
                lb_proactiveCompliance.Text = rp.CountFailurePoint(1014, sMonth).ToString
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count proactiveCompliance. /proactiveCompliance=" & lb_proactiveCompliance.Text, 2)
                hfInProgress.Value = False
                currentStep = 8
            Case 8
                hfInProgress.Value = True
                lb_secondEye.Text = rp.CountSecondEye(sMonth).ToString
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count secondEye. /secondEye=" & lb_secondEye.Text, 2)
                hfInProgress.Value = False
                currentStep = 9
            Case 9
                hfInProgress.Value = True
                lb_injuryNearMiss.Text = rp.CountSubCategory(1010, sMonth).ToString
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count injuryNearMiss. /injuryNearMiss=" & lb_injuryNearMiss.Text, 2)
                hfInProgress.Value = False
                currentStep = 10
            Case 10
                hfInProgress.Value = True
                lb_reliability_wHRO.Text = rp.CountCategory(1003, sMonth, True).ToString
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count reliability_wHRO. /reliability_wHRO=" & lb_reliability_wHRO.Text, 2)
                hfInProgress.Value = False
                currentStep = 11
            Case 11
                hfInProgress.Value = True
                lb_quality_wHRO.Text = rp.CountCategory(1002, sMonth, True).ToString
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count quality_wHRO. /quality_wHRO=" & lb_quality_wHRO.Text, 2)
                hfInProgress.Value = False
                currentStep = 12
            Case 12
                hfInProgress.Value = True
                lb_reliability.Text = rp.CountCategory(1003, sMonth, False).ToString
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count reliability. /reliability=" & lb_reliability.Text, 2)
                hfInProgress.Value = False
                currentStep = 13
            Case 13
                hfInProgress.Value = True
                Dim ProcedureUsed As Integer = rp.CountFailurePoint(1003, sMonth)
                Dim Safety As Integer = rp.CountCategory(1001, sMonth, False)
                If Safety <> 0 Then
                    Dim LCS As Integer = rp.CountSubCategory(1011, sMonth)
                    lb_percentPUPFieldAss.Text = (ProcedureUsed / Safety).ToString("P4")
                    lb_percentLCSFieldAss.Text = (LCS / Safety).ToString("P4")
                End If
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count percent PUP, LCS FieldAss. /percentPUPFieldAss=" & lb_percentPUPFieldAss.Text & ", percentLCSFieldAss=" & lb_percentLCSFieldAss.Text, 2)
                hfInProgress.Value = False
                currentStep = 14
            Case 14
                hfInProgress.Value = True
                '-- count from action number
                Dim TotalActionNo As Integer = CInt(lb_totalActionNumber.Text)
                rp.ActionNumStatus(sMonth)
                Dim actionRecogAll As Decimal = rp.CountRecogAll()
                Dim offHour As Integer = rp.CountOffHour()
                
                If TotalActionNo > 0 Then
                    lb_percentOfiIdentified.Text = (1 - (actionRecogAll / TotalActionNo)).ToString("P4")
                    lb_percentOffHour.Text = (offHour / TotalActionNo).ToString("P4")
                End If
                lb_offHourCountStr.Text = offHour.ToString & "/" & TotalActionNo.ToString
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count percentOfiIdentified, percentOffHour. /percentOfiIdentified=" & lb_percentOfiIdentified.Text & ", percentOffHour=" & lb_percentOffHour.Text, 2)
                hfInProgress.Value = False
                currentStep = 15
            Case 15
                hfInProgress.Value = True
                Dim percentActionComplete As Double = 0
                Dim percentLeadershipVisibility As Double = 0
                Dim percentOfiIdentified As Double = 0
                Dim percentPUPFieldAss As Double = 0
                Dim percentLCSFieldAss As Double = 0
                Dim percentOffHour As Double = 0

                If lb_percentActionComplete.Text.IndexOf(" %") > 0 Then percentActionComplete = CDbl(lb_percentActionComplete.Text.Substring(0,lb_percentActionComplete.Text.IndexOf(" %")))/100
                If lb_percentLeadershipVisibility.Text.IndexOf(" %") > 0 Then percentLeadershipVisibility = CDbl(lb_percentLeadershipVisibility.Text.Substring(0,lb_percentLeadershipVisibility.Text.IndexOf(" %")))/100
                If lb_percentOfiIdentified.Text.IndexOf(" %") > 0 Then percentOfiIdentified = CDbl(lb_percentOfiIdentified.Text.Substring(0,lb_percentOfiIdentified.Text.IndexOf(" %")))/100
                If lb_percentPUPFieldAss.Text.IndexOf(" %") > 0 Then percentPUPFieldAss = CDbl(lb_percentPUPFieldAss.Text.Substring(0,lb_percentPUPFieldAss.Text.IndexOf(" %")))/100
                If lb_percentLCSFieldAss.Text.IndexOf(" %") > 0 Then percentLCSFieldAss = CDbl(lb_percentLCSFieldAss.Text.Substring(0,lb_percentLCSFieldAss.Text.IndexOf(" %")))/100
                If lb_percentOffHour.Text.IndexOf(" %") > 0 Then percentOffHour = CDbl(lb_percentOffHour.Text.Substring(0,lb_percentOffHour.Text.IndexOf(" %")))/100
                
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
                command.Parameters.Add("@totalActionNumber", SqlDbType.Int).Value = CInt(lb_totalActionNumber.Text)
                command.Parameters.Add("@totalObserve", SqlDbType.Int).Value = CInt(lb_totalObserv.Text)
                command.Parameters.Add("@PSCE_ContainmentLoss", SqlDbType.Int).Value = CInt(lb_PSCE_ContainmentLossCount.Text)
                command.Parameters.Add("@PSCE_PSNM", SqlDbType.Int).Value = CInt(lb_PSCE_PSNMCount.Text)
                command.Parameters.Add("@actionComplete", SqlDbType.Int).Value = CInt(lb_actionNumComplete.Text)
                command.Parameters.Add("@actionRecognition", SqlDbType.Int).Value = CInt(lb_actionNumRecog.Text)
                command.Parameters.Add("@percentActionComplete", SqlDbType.SmallMoney).Value = percentActionComplete
                command.Parameters.Add("@leadershipVisibility_fsfl", SqlDbType.Int).Value = CInt(lb_sumOfDuration_fsfl.Text)
                command.Parameters.Add("@percentLeadershipVisibility", SqlDbType.SmallMoney).Value = percentLeadershipVisibility
                command.Parameters.Add("@proactiveCompliance", SqlDbType.Int).Value = CInt(lb_proactiveCompliance.Text)
                command.Parameters.Add("@secondEye", SqlDbType.Int).Value = CInt(lb_secondEye.Text)
                command.Parameters.Add("@injuryNearMiss", SqlDbType.Int).Value = CInt(lb_injuryNearMiss.Text)
                command.Parameters.Add("@reliability_wHRO", SqlDbType.Int).Value = CInt(lb_reliability_wHRO.Text)
                command.Parameters.Add("@quality_wHRO", SqlDbType.Int).Value = CInt(lb_quality_wHRO.Text)
                command.Parameters.Add("@reliability", SqlDbType.Int).Value = CInt(lb_reliability.Text)
                command.Parameters.Add("@percentOFIIden", SqlDbType.SmallMoney).Value = percentOfiIdentified
                command.Parameters.Add("@percentPUPFieldAss", SqlDbType.SmallMoney).Value = percentPUPFieldAss
                command.Parameters.Add("@percentLCSFieldAss", SqlDbType.SmallMoney).Value = percentLCSFieldAss
                command.Parameters.Add("@offHourCount", SqlDbType.VarChar).Value = lb_offHourCountStr.Text
                command.Parameters.Add("@percentOffHour", SqlDbType.SmallMoney).Value = percentOffHour
                command.Parameters.Add("@lastUpdate", SqlDbType.DateTime).Value = Now

                command.Parameters.Add("@departId", SqlDbType.Int).Value = 0        'for all department
                command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
                command.Parameters.Add("@year", SqlDbType.Int).Value = sYear
                Dim result As Integer = command.ExecuteNonQuery()

                'set last update protect other user gen-report
                setLastUpdateReport(sMonth)
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "last update " & Now.ToShortDateString & " " & Now.ToLongTimeString, 2)
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & " .................................... Complete ....................................", 3)
                rlvIndicator.Rebind()
                currentStep = 16
                hfInProgress.Value = False
            Case 16
                hfInProgress.Value = True
                rcbDepartment2.SelectedIndex = rcbDepartment2.SelectedIndex + 1
                Dim departReport As Integer = rcbDepartment2.SelectedValue
                Dim WorkingHourPerMonth As Integer = 9800
                Dim TotalObserver_fsfl As Integer = rp.CountEmployee_fsfl(departReport)
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Count observer fsfl. //department " & rcbDepartment2.SelectedItem.Text, 2)

                Dim totalActionNumber As Integer = rp.CountDocTotalActionNum(sMonth, departReport)
                Dim totalObserv As Integer = rp.CountDocTotalObserv(sMonth, departReport)

                rp.getDepartmentData(departReport, sMonth)
                Dim PSCE_ContainmentLossCount As Integer = rp.getSumPSCE_ContainmentLoss()
                Dim PSCE_PSNMCount As Integer = rp.getSumPSCE_PSNM()
                
                '-- count from action number
                rp.CountStatusActionNum(sMonth, departReport)
                Dim actionNumComplete As Integer = rp.CountActionNumComplete
                Dim actionNumRecog As Integer = rp.CountActionNumRecog
                Dim percentActionComplete As Decimal = 0
                If totalActionNumber <> actionNumRecog Then percentActionComplete = actionNumComplete / (totalActionNumber - actionNumRecog)
                
                Dim sumOfDuration_fsfl As Integer = rp.getSumleadershipVisibility
                Dim percentLeadershipVisibility As Decimal = 0
                If TotalObserver_fsfl <> 0 Then percentLeadershipVisibility = sumOfDuration_fsfl / (WorkingHourPerMonth * TotalObserver_fsfl)

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

                rp.ActionNumStatus(sMonth, departReport)
                Dim actionRecogAll As Decimal = rp.CountRecogAll()
                Dim offHour As Integer = rp.CountOffHour()
                Dim percentOFIIdentified As Decimal = 0
                Dim percentOffHour As Decimal = 0
                If totalActionNumber > 0 Then
                    percentOFIIdentified = 1 - (actionRecogAll / totalActionNumber)
                    percentOffHour = offHour / totalActionNumber
                End If

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
                command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
                command.Parameters.Add("@year", SqlDbType.Int).Value = sYear
                Dim result As Integer = command.ExecuteNonQuery()
                AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Update department " & rcbDepartment2.SelectedItem.Text & " complete...", 2)

                If rcbDepartment2.SelectedIndex = rcbDepartment2.Items.Count - 1 Then
                    AddListBox(Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & " ..................................................................................", 3)

                    If Request.QueryString("auto") = "1" Then
                        rcbDepartment2.SelectedIndex = 0

                        If CBool(hfCurrentMonth.Value) Then
                            hfCurrentMonth.Value = False
                            currentStep = 0
                        Else
                            hfCurrentMonth.Value = True
                            currentStep = 17        'stop keyword
                        End If
                    Else
                        currentStep = 0
                        btStart.Text = "Start"
                        btStart.Enabled = True
                        rcbMonth.Enabled = True
                        rcbYear2.Enabled = True
                        rcbDepartment2.SelectedIndex = 0
                        Timer1.Enabled = False
                    End If
                End If
                
                hfInProgress.Value = False  
        End Select
    End Sub

    Private Sub AutoStart()
        Dim currentStep As Integer = CInt(lbCurrent.Text)
        Dim counter As Integer = CInt(lbCount.Text)
        counter = counter + 1

        If not CBool(hfInProgress.Value) Then
            Dim sMonth As Integer = rcbMonth.SelectedValue
            Dim sYear As Integer = rcbYear2.SelectedValue
            If CBool(hfCurrentMonth.Value) Then
                GenerateRp(sYear, sMonth, currentStep)
            Else
                If sMonth = 1 Then
                    sMonth = 13
                    sYear = sYear -1
                End If
                GenerateRp(sYear, sMonth - 1, currentStep)
            End If
        End If

        '--
        Dim intervalHr As Integer = CInt(rcbInterval.SelectedValue)      'min.
        Dim intervalLoop As Integer = intervalHr * 60000 / Timer1.Interval
        'If currentStep = 10 Then currentStep = 0
        If counter >= intervalLoop Then
            counter = 0
            currentStep = 0
            imgClock.ImageUrl = "~/Images/clock-32Dis.png"
            pnTimeOffset.Visible = False
        End If
        lbCurrent.Text = currentStep.ToString

        '-- case offset
        If counter < 50 and imgClock.ImageUrl = "~/Images/clock-32.png" Then     'set offset
            Dim offsetTime As TimeSpan = rdpCallTime.SelectedTime
            Dim timediff As TimeSpan = offsetTime.Subtract(TimeSpan.Parse(Now.Hour.ToString & ":" & Now.Minute.ToString & ":" & Now.Second.ToString))
            Dim diff As Integer = timediff.TotalMinutes
            If diff < 0 Then diff = diff + 1440
            diff = diff Mod intervalHr
            diff = intervalHr - diff
            counter = counter + (diff * 60000) / Timer1.Interval
            imgClock.ImageUrl = "~/Images/clock-32Dis.png"
        End If

        lbCount.Text = counter.ToString 
        lbCountInterval.Text = intervalLoop.ToString
    End Sub
    
    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        AutoStart()  
    End Sub
    
    Protected Sub rcbIsAuto_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbIsAuto.SelectedIndexChanged
        Dim TimeOffsetStr As String = "&intv=" & rcbInterval.SelectedValue
        If Not rdpCallTime.IsEmpty Then TimeOffsetStr = TimeOffsetStr & "&offset=" & rdpCallTime.SelectedTime.ToString 

        If rcbIsAuto.SelectedValue = 1 Then  
            Response.Redirect("autoReport?auto=1" & TimeOffsetStr)
        ElseIf rcbIsAuto.SelectedValue = 0 then
            Response.Redirect("autoReport?auto=0" & TimeOffsetStr)
        End If
    End Sub

    Protected Sub btStart_Click(sender As Object, e As EventArgs) Handles btStart.Click
        btStart.Text = "Process..."
        btStart.Enabled = False
        rcbMonth.Enabled = False
        rcbYear2.Enabled = False
        Timer1.Enabled = True
        AutoStart()
    End Sub


    '----------- Validate data -----------
          
    Protected Sub btValidate_Click(sender As Object, e As EventArgs) Handles btValidate.Click
        btValidate.Text = "Process..."
        btValidate.Enabled = False
        rcbMonthValidate.Enabled = False
        rcbYearValidate.Enabled = False
        Timer2.Enabled = True
        ValidateStart()
    End Sub

    Protected Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        ValidateStart()
    End Sub

    Private Sub ValidateStart()
        Dim vStep As Integer = CInt(lbCurrent2.Text)

        If not CBool(hfInProgress2.Value) Then
            Dim vMonth As Integer = rcbMonthValidate.SelectedValue
            Dim vYear As Integer = rcbYearValidate.SelectedValue
            ValidateData(vYear, vMonth, vStep)
        End If    
        lbCurrent2.Text = vStep.ToString
    End Sub


    Private Sub ValidateData(ByVal sYear As Integer, ByVal sMonth As Integer, ByRef currentStep As Integer)
        Select Case currentStep
            Case 0
                'check Other Observer
                hfInProgress2.Value = True
                lbStartTime2.Text = Now.ToShortDateString & "  " & Now.ToLongTimeString

                tbValidateInfo.Text = Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Check other Observer" & vbNewLine
                Dim strSql As String = String.Format("SELECT tblRecord.oEmpCount, (SELECT COUNT(*) FROM tblRecordOthEmpO WHERE recId = tblRecord.recId) AS oEmpCountDetail, tblRecord.recId FROM tblRecord WHERE recActYear = {0} AND recActMonth = {1}", sYear.ToString, sMonth.ToString)
                Dim conn As New SqlConnection(ConnStr)
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(strSql, conn)

                Dim tbl As New DataTable()
                conn.Open()
                Try
                    adapter.Fill(tbl)
                Finally
                    conn.Close()
                End Try

                Dim errorCount As Integer = 0
                For Each row As DataRow In tbl.Rows
                    Dim oEmpCount As Integer = row.Field(Of Integer)(0)
                    Dim oEmpCountDetail As Integer = row.Field(Of Integer)(1)
                    If oEmpCount <> oEmpCountDetail Then
                        errorCount = errorCount + 1
                        Dim recId As Integer = row.Field(Of Integer)(2)
                        Dim optionStr As String = ""
                        If oEmpCount < oEmpCountDetail Then
                            optionStr = "/Other emplyee detail more then master record."
                        End If
                        tbValidateInfo.Text = tbValidateInfo.Text & Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "RecordId " & recId.ToString & " incorrect. " & optionStr & vbNewLine
                    End If
                Next
                tbValidateInfo.Text = tbValidateInfo.Text & Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & errorCount.ToString & " record incorrect." & vbNewLine

                currentStep = 1
                hfInProgress2.Value = False
            Case 1
                'check No Observe
                hfInProgress2.Value = True

                tbValidateInfo.Text = tbValidateInfo.Text & Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Check No Observe" & vbNewLine
                Dim strSql As String = String.Format("SELECT tblRecord.noObserve, (SELECT COUNT(*) FROM tblRecordDetail WHERE recId = tblRecord.recId) AS recordDetail, tblRecord.recId FROM tblRecord WHERE recActYear = {0} AND recActMonth = {1}", sYear.ToString, sMonth.ToString)
                Dim conn As New SqlConnection(ConnStr)
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(strSql, conn)

                Dim tbl As New DataTable()
                conn.Open()
                Try
                    adapter.Fill(tbl)
                Finally
                    conn.Close()
                End Try
                
                Dim errorCount As Integer = 0
                For Each row As DataRow In tbl.Rows
                    Dim NoObserve As Integer = row.Field(Of Integer)(0)
                    Dim NorecordDetail As Integer = row.Field(Of Integer)(1)
                    If NoObserve <> NorecordDetail Then
                        errorCount = errorCount + 1
                        Dim recId As Integer = row.Field(Of Integer)(2)
                        Dim optionStr As String = ""
                        If NoObserve < NorecordDetail Then
                            optionStr = "/Detail record (observe record) more then No Observe."
                        End If
                        tbValidateInfo.Text = tbValidateInfo.Text & Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "RecordId " & recId.ToString & " incorrect. " & optionStr & vbNewLine
                    End If
                Next
                tbValidateInfo.Text = tbValidateInfo.Text & Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & errorCount.ToString & " record incorrect." & vbNewLine
                                
                currentStep = 2
                hfInProgress2.Value = False
            Case 2
                'check IsComplete /ActionNumber
                hfInProgress2.Value = True

                If hfPercentUpdate.Value = "0" then tbValidateInfo.Text = tbValidateInfo.Text & Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & "Check ActionNumber complete status" & vbNewLine
                Dim strSql As String = String.Format("SELECT tblRecord.recId FROM tblRecord WHERE recActYear = {0} AND recActMonth = {1}", sYear.ToString, sMonth.ToString)
                Dim conn As New SqlConnection(ConnStr)
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(strSql, conn)

                Dim tbl As New DataTable()
                conn.Open()
                Try
                    adapter.Fill(tbl)
                Finally
                    conn.Close()
                End Try

                Dim status As New cStatus
                Dim row As DataRow
                Dim loopStart As Integer = CInt(hfLoopStart.Value)
                Dim endPoint As Integer = Math.Ceiling(tbl.Rows.Count / 20) - 1
                Dim loopEnd As Integer = loopStart + endPoint
                If loopEnd >= tbl.Rows.Count - 1 Then loopEnd = tbl.Rows.Count - 1
                For i As Integer = loopStart To loopEnd
                    row = tbl.Rows.Item(i)
                    Dim recId As Integer = row.Field(Of Integer)(0)
                    status.UpdRecordIsComplete(recId)
                Next
                'For Each row As DataRow In tbl.Rows
                '    Dim recId As Integer = row.Field(Of Integer)(0)
                '    status.UpdRecordIsComplete(recId)
                'Next

                hfLoopStart.Value = loopEnd + 1
                hfPercentUpdate.Value = CInt(hfPercentUpdate.Value) + 5
                lbPercent.Text = " ... (" & hfPercentUpdate.Value.ToString & " %)"
                
                If hfPercentUpdate.Value = "100" Then
                    tbValidateInfo.Text = tbValidateInfo.Text & Now.ToShortDateString & " " & Now.ToLongTimeString & ": " & tbl.Rows.Count.ToString & " record update." & vbNewLine
                    hfPercentUpdate.Value = "0"
                    lbPercent.Text = ""
                    currentStep = 0
                    btValidate.Text = "Start"
                    btValidate.Enabled = True
                    rcbMonthValidate.Enabled = True
                    rcbYearValidate.Enabled = True
                    Timer2.Enabled = False
                End If

                hfInProgress2.Value = False
        End Select
    End Sub

    Protected Sub btSetOffset_Click(sender As Object, e As EventArgs) Handles btSetOffset.Click
        If Not rdpCallTime.IsEmpty Then imgClock.ToolTip = rdpCallTime.SelectedTime.ToString Else imgClock.ToolTip = ""
        pnTimeOffset.Visible = Not pnTimeOffset.Visible
    End Sub

    Protected Sub rdpCallTime_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles rdpCallTime.SelectedDateChanged
        If not rdpCallTime.IsEmpty Then imgClock.ImageUrl = "~/Images/clock-32.png" Else imgClock.ImageUrl = "~/Images/clock-32Dis.png"
    End Sub

    Protected Sub btSaveInterval_Click(sender As Object, e As EventArgs) Handles btSaveInterval.Click
        'set interval time
        Dim rpInterval As New cReportInterval
        if rpInterval.SetInterval(CInt(tbnIntervalAdmin.Value), CInt(tbnIntervalUser.Value)) = 1 Then
            

            If rcbInterval.Items.FindItemByValue(tbnIntervalAdmin.Value.ToString) IsNot Nothing Then
                rcbInterval.SelectedValue = tbnIntervalAdmin.Value
                lbSaveInfo.Text = "Updated and change interval."
            Else
                lbSaveInfo.Text = "Updated!"
            End If
        End If
    End Sub

    Protected Sub rcbInterval_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbInterval.SelectedIndexChanged
        Dim intervalHr As Integer = CInt(rcbInterval.SelectedValue)        
        If Not rdpCallTime.IsEmpty Then 
            Dim offsetTime As TimeSpan = rdpCallTime.SelectedTime
            Dim timediff As TimeSpan = offsetTime.Subtract(TimeSpan.Parse(Now.Hour.ToString & ":" & Now.Minute.ToString & ":" & Now.Second.ToString))
            Dim diff As Integer = timediff.TotalMinutes
            If diff < 0 Then diff = diff + 1440
            diff = diff Mod intervalHr
            diff = intervalHr - diff

            lbCount.Text = ((diff * 60000) / Timer1.Interval).ToString 
            lbCountInterval.Text = (intervalHr * 60000 / Timer1.Interval).ToString
        End If
    End Sub
End Class