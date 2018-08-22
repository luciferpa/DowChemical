Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class setGoal
    Inherits Page
    Dim connStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Dim observerFsfl As Integer
    Dim observerTech As Integer
    Dim secondEyeSafety As Double
    Dim PSCE_ContainmentLoss As Double
    Dim PSCE_PSNM As Double
    Dim percentLeadershipFieldVisibility As Double
    Dim percentActionCompleted As Double
    Dim complianceProactive As Double

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
        End If

        If User.IsInRole("FACILITY ADMIN") Then
            Dim settingItem As RadPanelItem = RadPanelBar1.Items.FindItemByText("SETTING")
            settingItem.Visible = True
            settingItem.Items.FindItemByText("DEPARTMENT").Visible = False
            settingItem.Items.FindItemByText("CONTRACTOR").Visible = False
            settingItem.Items.FindItemByText("GOAL SETTING").Visible = False
            settingItem.Items.FindItemByText("CATEGORY").Visible = False
            settingItem.Items.FindItemByText("IMPORT DATA").Visible = False
        End If
    End Sub

    Dim totalObserverFsfl As Integer
    Dim totalObserverTech As Integer
    Dim totalSecondEyeSafety As Double
    Dim totalPSCE_ContainmentLoss As Double
    Dim totalPSCE_PSNM As Double
    Dim totalComplianceProactive As Double
    Private Sub rgGoalSetting_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgGoalSetting.ItemDataBound
        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

            Dim hfDepartId As HiddenField = item.FindControl("hfDepartId")
            Dim lbfsflUser As Label = item.FindControl("lb_fsflUser")
            Dim lbtechUser As Label = item.FindControl("lb_techUser")
            Dim lbSecondEyeSafety As Label = item.FindControl("lb_secondEyeSafety")
            Dim lbPSCE_ContainmentLoss As Label = item.FindControl("lb_PSCE_ContainmentLoss")
            Dim lbPSCE_PSNM As Label = item.FindControl("lb_PSCE_PSNM")
            Dim lbPercentLeadershipFieldVisibility As Label = item.FindControl("lb_percentLeadershipFieldVisibility")
            Dim lbPercentActionCompleted As Label = item.FindControl("lb_percentActionCompleted")
            Dim lbComplianceProactive As Label = item.FindControl("lb_complianceProactive")
            Dim lbTimestamp As Label = item.FindControl("lbTimestamp")

            'get last update goal data
            Using conn As New SqlConnection(connStr)
                conn.Open()
                Dim strSql As String = "SELECT TOP (1) goalId, goalType, departId, totalObserver_fsfl, totalObserver_tech, secondEyeSafety, PSCE_ContainmentLoss, PSCE_PSNM, 
                                        percentLeadershipFieldVisibility, percentActionCompleted, complianceProactive, timestamp  
                                        FROM tblGoalSetting WHERE departId = @departId AND month = @month AND year = @year ORDER BY goalId DESC"

                Dim comm As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
                comm.Parameters.Add("@departId", SqlDbType.Int).Value = CInt(hfDepartId.Value)
                comm.Parameters.Add("@month", SqlDbType.Int).Value = CInt(rcbMonth.SelectedValue)
                comm.Parameters.Add("@year", SqlDbType.Int).Value = CInt(rcbYear.SelectedValue)

                Dim dataRead As SqlDataReader
                dataRead = comm.ExecuteReader()
                If dataRead.HasRows Then
                    While dataRead.Read()
                        lbfsflUser.Text = FormatNumber(dataRead("totalObserver_fsfl"), 0, , , TriState.True)
                        lbtechUser.Text = FormatNumber(dataRead("totalObserver_tech"), 0, , , TriState.True)
                        lbSecondEyeSafety.Text = FormatNumber(dataRead("secondEyeSafety"), 1, , , TriState.True)
                        lbPSCE_ContainmentLoss.Text = FormatNumber(dataRead("PSCE_ContainmentLoss"), 1, , , TriState.True)
                        lbPSCE_PSNM.Text = FormatNumber(dataRead("PSCE_PSNM"), 1, , , TriState.True)

                        Dim plfvValue As Double = CDbl(dataRead("percentLeadershipFieldVisibility"))
                        plfvValue = plfvValue * 10
                        If (plfvValue Mod 10) = 0 Then
                            lbPercentLeadershipFieldVisibility.Text = (plfvValue / 1000).ToString("P0")
                        Else
                            lbPercentLeadershipFieldVisibility.Text = (plfvValue / 1000).ToString("P1")
                        End If

                        Dim pcompValue As Double = CDbl(dataRead("percentActionCompleted"))
                        pcompValue = pcompValue * 10
                        If (pcompValue Mod 10) = 0 Then
                            lbPercentActionCompleted.Text = (pcompValue / 1000).ToString("P0")
                        Else
                            lbPercentActionCompleted.Text = (pcompValue / 1000).ToString("P1")
                        End If

                        lbComplianceProactive.Text = FormatNumber(dataRead("complianceProactive"), 1, , , TriState.True)
                        If dataRead("timestamp") IsNot DBNull.Value Then lbTimestamp.Text = dataRead("timestamp")
                    End While
                Else
                    lbfsflUser.Text = "0"
                    lbtechUser.Text = "0"
                    lbSecondEyeSafety.Text = "0.0"
                    lbPSCE_ContainmentLoss.Text = "0.0"
                    lbPSCE_PSNM.Text = "0.0"
                    lbPercentLeadershipFieldVisibility.Text = "0 %"
                    lbPercentActionCompleted.Text = "0 %"
                    lbComplianceProactive.Text = "0.0"
                    lbTimestamp.Text = ""
                End If
            End Using

            observerFsfl = lbfsflUser.Text
            observerTech = lbtechUser.Text
            secondEyeSafety = lbSecondEyeSafety.Text
            PSCE_ContainmentLoss = lbPSCE_ContainmentLoss.Text
            PSCE_PSNM = lbPSCE_PSNM.Text
            complianceProactive = lbComplianceProactive.Text

            percentLeadershipFieldVisibility = lbPercentLeadershipFieldVisibility.Text.Substring(0, lbPercentLeadershipFieldVisibility.Text.IndexOf(" %"))
            percentActionCompleted = lbPercentActionCompleted.Text.Substring(0, lbPercentActionCompleted.Text.IndexOf(" %"))

            totalObserverFsfl = totalObserverFsfl + observerFsfl
            totalObserverTech = totalObserverTech + observerTech
            totalSecondEyeSafety = totalSecondEyeSafety + secondEyeSafety
            totalPSCE_ContainmentLoss = totalPSCE_ContainmentLoss + PSCE_ContainmentLoss
            totalPSCE_PSNM = totalPSCE_PSNM + PSCE_PSNM
            totalComplianceProactive = totalComplianceProactive + complianceProactive
        End If

        '-- GridFooter
        If TypeOf e.Item Is GridFooterItem Then
            Dim fitem As GridFooterItem = CType(e.Item, GridFooterItem)

            Dim lbftFooterFsflUser As Label = fitem.FindControl("lbFooter_fsflUser")
            lbftFooterFsflUser.Text = totalObserverFsfl.ToString
            Dim lbftFooterTechUser As Label = fitem.FindControl("lbFooter_techUser")
            lbftFooterTechUser.Text = totalObserverTech.ToString

            Dim lbftSecondEyeSafety As Label = fitem.FindControl("lbFooter_secondEyeSafety")
            lbftSecondEyeSafety.Text = FormatNumber(totalSecondEyeSafety, 1, , , TriState.True)
            hftotalSecondEyeSafety.Value = totalSecondEyeSafety

            Dim lbftPSCE_ContainmentLoss As Label = fitem.FindControl("lbFooter_PSCE_ContainmentLoss")
            lbftPSCE_ContainmentLoss.Text = FormatNumber(totalPSCE_ContainmentLoss, 1, , , TriState.True)
            hftotalPSCE_ContainmentLoss.Value = totalPSCE_ContainmentLoss

            Dim lbftPSCE_PSNM As Label = fitem.FindControl("lbFooter_PSCE_PSNM")
            lbftPSCE_PSNM.Text = FormatNumber(totalPSCE_PSNM, 1, , , TriState.True)
            hftotalPSCE_PSNM.Value = totalPSCE_PSNM

            Dim lbftComplianceProactive As Label = fitem.FindControl("lbFooter_complianceProactive")
            lbftComplianceProactive.Text = FormatNumber(totalComplianceProactive.ToString, 1, , , TriState.True)
            hfcomplianceProactive.Value = totalComplianceProactive

            'get percentLeadershipFieldVisibility and percentActionCompleted
            Dim lbft_percentLeadershipFieldVisibility As Label = fitem.FindControl("lbFooter_percentLeadershipFieldVisibility")
            Dim lbft_percentActionCompleted As Label = fitem.FindControl("lbFooter_percentActionCompleted")
            Using conn2 As New SqlConnection(connStr)
                conn2.Open()
                Dim strSql2 As String = "SELECT TOP (1) percentLeadershipFieldVisibility, percentActionCompleted, timestamp FROM tblGoalSetting 
                                        WHERE goalType = '1' AND departId = '0' AND month = @month AND year = @year ORDER BY goalId DESC"

                Dim comm2 As New SqlCommand(strSql2, conn2) With {.CommandType = CommandType.Text}
                comm2.Parameters.Add("@month", SqlDbType.Int).Value = CInt(rcbMonth.SelectedValue)
                comm2.Parameters.Add("@year", SqlDbType.Int).Value = CInt(rcbYear.SelectedValue)
                Dim DataRead2 As SqlDataReader
                DataRead2 = comm2.ExecuteReader()
                If DataRead2.HasRows Then
                    While DataRead2.Read()
                        Dim MTPFieldVisibility As Double = CDbl(DataRead2("percentLeadershipFieldVisibility"))
                        MTPFieldVisibility = MTPFieldVisibility * 10
                        If (MTPFieldVisibility Mod 10) = 0 Then
                            lbft_percentLeadershipFieldVisibility.Text = (MTPFieldVisibility / 1000).ToString("P0")
                        Else
                            lbft_percentLeadershipFieldVisibility.Text = (MTPFieldVisibility / 1000).ToString("P1")
                        End If

                        Dim MTPActionComplete As Double = CDbl(DataRead2("percentActionCompleted"))
                        MTPActionComplete = MTPActionComplete * 10
                        If (MTPActionComplete Mod 10) = 0 Then
                            lbft_percentActionCompleted.Text = (MTPActionComplete / 1000).ToString("P0")
                        Else
                            lbft_percentActionCompleted.Text = (MTPActionComplete / 1000).ToString("P1")
                        End If
                    End While
                Else
                    lbft_percentLeadershipFieldVisibility.Text = "? %"
                    lbft_percentActionCompleted.Text = "? %"
                End If
            End Using

            'pass parameter to buttom
            Dim edit1 As ImageButton = fitem.FindControl("imgEdit1")
            edit1.CommandArgument = "FieldVisibility"
            Dim edit2 As ImageButton = fitem.FindControl("imgEdit2")
            edit2.CommandArgument = "ActionComplete"
        End If

        '-- EditForm
        If TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode Then
            Dim eitem As GridEditableItem = TryCast(e.Item, GridEditableItem)

            Dim tbn_fsflUser As RadNumericTextBox = eitem.FindControl("tbn_total_fsfl")
            tbn_fsflUser.Text = observerFsfl.ToString
            Dim tbn_techUser As RadNumericTextBox = eitem.FindControl("tbn_total_tech")
            tbn_techUser.Text = observerTech.ToString

            Dim tbnSecondEye As RadNumericTextBox = eitem.FindControl("tbn_secondEyeSafety")
            tbnSecondEye.Text = secondEyeSafety.ToString
            Dim tbnPSCE_ContainmentLoss As RadNumericTextBox = eitem.FindControl("tbn_PSCE_ContainmentLoss")
            tbnPSCE_ContainmentLoss.Text = PSCE_ContainmentLoss.ToString
            Dim tbnPSCE_PSNM As RadNumericTextBox = eitem.FindControl("tbn_PSCE_PSNM")
            tbnPSCE_PSNM.Text = PSCE_PSNM.ToString

            Dim tbnPercentLeadershipFieldVisibility As RadNumericTextBox = eitem.FindControl("tbn_percentLeadershipFieldVisibility")
            tbnPercentLeadershipFieldVisibility.Text = percentLeadershipFieldVisibility.ToString
            Dim tbnPercentActionCompleted As RadNumericTextBox = eitem.FindControl("tbn_percentActionCompleted")
            tbnPercentActionCompleted.Text = percentActionCompleted.ToString

            Dim tbnComplianceProactive As RadNumericTextBox = eitem.FindControl("tbn_complianceProactive")
            tbnComplianceProactive.Text = complianceProactive.ToString

            'pass departId to buttom
            Dim eBtGetDataFsfl As Button = eitem.FindControl("btGetData_fsfl")
            eBtGetDataFsfl.CommandArgument = eitem.GetDataKeyValue("departId")
            Dim eBtGetDataTech As Button = eitem.FindControl("btGetData_tech")
            eBtGetDataTech.CommandArgument = eitem.GetDataKeyValue("departId")
        End If
    End Sub

    Private Sub rgGoalSetting_UpdateCommand(sender As Object, e As GridCommandEventArgs) Handles rgGoalSetting.UpdateCommand
        If e.CommandName = RadGrid.UpdateCommandName Then
            If TypeOf e.Item Is GridEditFormItem Then
                Dim item As GridEditFormItem = DirectCast(e.Item, GridEditFormItem)
                Dim departId As Integer = item.GetDataKeyValue("departId")

                'chk exist database
                selDepart = ChkExistAllDepart(rcbYear.SelectedValue, rcbMonth.SelectedValue, departId)
                If selDepart > 0 Then
                    'update
                    updateGoalSetting(item, departId)
                Else
                    If allDepart > 0 Then
                        'create only this depart (for new department)
                        CreateGoalSingleRecord(0, departId, rcbMonth.SelectedValue)
                    Else
                        'chk exist last manth, this year and create all months
                        Dim lastMonth = getLastMonth(rcbYear.SelectedValue)
                        CreateGoalThisYear(lastMonth)
                    End If
                    'update
                    updateGoalSetting(item, departId)
                End If

                MsgBoxRad("<b>Goal update.</b>", 240, 76)
            End If
        End If
    End Sub

    Dim selDepart As Integer
    Dim allDepart As Integer
    Private Function ChkExistAllDepart(ByVal sYear As Integer, ByVal sMonth As Integer, Optional ByVal departId As Integer = 0, Optional ByVal goalType As Integer = 0) As Integer
        Dim strSql As String = "SELECT COUNT(*) AS selDepart,
                                        (SELECT COUNT(*) AS count FROM tblGoalSetting
                                         WHERE month = @month AND year = @year AND goalType = @goalType) AS allDepart
                                  FROM tblGoalSetting AS tblGoalSetting_1
                                  WHERE month = @month AND year = @year AND goalType = @goalType AND departId = @departId"

        Using conn As New SqlConnection(connStr)
            conn.Open()
            Dim comm As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
            comm.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
            comm.Parameters.Add("@year", SqlDbType.Int).Value = sYear
            comm.Parameters.Add("@goalType", SqlDbType.Int).Value = goalType
            comm.Parameters.Add("@departId", SqlDbType.Int).Value = departId
            Dim dataRead As SqlDataReader
            dataRead = comm.ExecuteReader()

            selDepart = 0
            allDepart = 0
            If dataRead.HasRows Then
                While dataRead.Read()
                    selDepart = CInt(dataRead("selDepart"))
                    allDepart = CInt(dataRead("allDepart"))
                End While
            End If
        End Using

        Return selDepart
    End Function

    Private Function getLastMonth(ByVal sYear As Integer) As Integer
        Dim strSql As String = String.Format("SELECT DISTINCT(month) FROM tblGoalSetting WHERE year = {0}", rcbYear.SelectedValue)
        Dim lastMonth As Integer = 0
        Using conn As New SqlConnection(connStr)
            conn.Open()
            Dim command As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
            Dim dataRead As SqlDataReader = command.ExecuteReader()
            If dataRead.HasRows Then
                dataRead.Read()
                lastMonth = CInt(dataRead("month"))
            End If
        End Using

        Return lastMonth
    End Function

    Private Function updateGoalSetting(item As GridEditFormItem, ByVal departId As Integer) As Boolean
        Dim tb_secondEyeSafety As RadNumericTextBox = DirectCast(item.FindControl("tbn_secondEyeSafety"), RadNumericTextBox)
        Dim tb_PSCE_ContainmentLoss As RadNumericTextBox = DirectCast(item.FindControl("tbn_PSCE_ContainmentLoss"), RadNumericTextBox)
        Dim tb_PSCE_PSNM As RadNumericTextBox = DirectCast(item.FindControl("tbn_PSCE_PSNM"), RadNumericTextBox)
        Dim tb_percentLeadershipFieldVisibility As RadNumericTextBox = DirectCast(item.FindControl("tbn_percentLeadershipFieldVisibility"), RadNumericTextBox)
        Dim tb_percentActionCompletes As RadNumericTextBox = DirectCast(item.FindControl("tbn_percentActionCompleted"), RadNumericTextBox)
        Dim tb_complianceProactive As RadNumericTextBox = DirectCast(item.FindControl("tbn_complianceProactive"), RadNumericTextBox)
        Dim tb_totalObserver As RadNumericTextBox = DirectCast(item.FindControl("tbn_totalObserver"), RadNumericTextBox)

        Dim tb_total_fsfl As RadNumericTextBox = DirectCast(item.FindControl("tbn_total_fsfl"), RadNumericTextBox)
        Dim tb_total_tech As RadNumericTextBox = DirectCast(item.FindControl("tbn_total_tech"), RadNumericTextBox)

        Dim strSql As String = "UPDATE tblGoalSetting SET totalObserver_fsfl = @totalObserver_fsfl, totalObserver_tech = @totalObserver_tech, secondEyeSafety = @secondEyeSafety, 
                                            PSCE_ContainmentLoss = @PSCE_ContainmentLoss, PSCE_PSNM = @PSCE_PSNM, percentLeadershipFieldVisibility = @percentLeadershipFieldVisibility, 
                                            percentActionCompleted = @percentActionCompleted, complianceProactive = @complianceProactive, timestamp = @timestamp 
                                            WHERE departId = @departId AND year = @year "
        If Not chkUpdateAllYear.Checked Then strSql = strSql & "AND month = @month"

        Dim conn As New SqlConnection(connStr)
        conn.Open()
        Dim command As New SqlCommand(strSql, conn)
        command.Parameters.Add("@totalObserver_fsfl", SqlDbType.Int).Value = CInt(tb_total_fsfl.Value)
        command.Parameters.Add("@totalObserver_tech", SqlDbType.Int).Value = CInt(tb_total_tech.Value)
        command.Parameters.Add("@secondEyeSafety", SqlDbType.SmallMoney).Value = CDbl(tb_secondEyeSafety.Value)
        command.Parameters.Add("@PSCE_ContainmentLoss", SqlDbType.SmallMoney).Value = CDbl(tb_PSCE_ContainmentLoss.Value)
        command.Parameters.Add("@PSCE_PSNM", SqlDbType.SmallMoney).Value = CDbl(tb_PSCE_PSNM.Value)
        command.Parameters.Add("@percentLeadershipFieldVisibility", SqlDbType.Money).Value = CDbl(tb_percentLeadershipFieldVisibility.Value)
        command.Parameters.Add("@percentActionCompleted", SqlDbType.Money).Value = CDbl(tb_percentActionCompletes.Value)
        command.Parameters.Add("@complianceProactive", SqlDbType.SmallMoney).Value = CDbl(tb_complianceProactive.Value)
        command.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = Now()

        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@year", SqlDbType.Int).Value = CInt(rcbYear.SelectedValue)
        If Not chkUpdateAllYear.Checked Then command.Parameters.Add("@month", SqlDbType.Int).Value = CInt(rcbMonth.SelectedValue)
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()

        If errCode > 0 Then Return True Else Return False
    End Function
    Private Function updateGoalSettingMTP(ByVal sYear As Integer, ByVal sMonth As Integer, ByVal FieldVisibility As Double, ByVal ActionComplete As Double) As Boolean
        Dim strSql As String = "UPDATE tblGoalSetting SET percentLeadershipFieldVisibility = @percentLeadershipFieldVisibility, percentActionCompleted = @percentActionCompleted, timestamp = @timestamp,
                                secondEyeSafety = @secondEyeSafety, PSCE_ContainmentLoss = @PSCE_ContainmentLoss, PSCE_PSNM = @PSCE_PSNM, complianceProactive = @complianceProactive
                                WHERE goalType = '1' AND departId = '0' AND year = @year "
        If Not chkUpdateAllYear.Checked Then strSql = strSql & "AND month = @month"

        Dim conn As New SqlConnection(connStr)
        conn.Open()
        Dim command As New SqlCommand(strSql, conn)
        command.Parameters.Add("@percentLeadershipFieldVisibility", SqlDbType.Money).Value = FieldVisibility
        command.Parameters.Add("@percentActionCompleted", SqlDbType.Money).Value = ActionComplete
        command.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = Now()

        command.Parameters.Add("@secondEyeSafety", SqlDbType.SmallMoney).Value = CDbl(hftotalSecondEyeSafety.Value)
        command.Parameters.Add("@PSCE_ContainmentLoss", SqlDbType.SmallMoney).Value = CDbl(hftotalPSCE_ContainmentLoss.Value)
        command.Parameters.Add("@PSCE_PSNM", SqlDbType.SmallMoney).Value = CDbl(hftotalPSCE_PSNM.Value)
        command.Parameters.Add("@complianceProactive", SqlDbType.SmallMoney).Value = CDbl(hfcomplianceProactive.Value)

        command.Parameters.Add("@year", SqlDbType.Int).Value = sYear
        If Not chkUpdateAllYear.Checked Then command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()

        If errCode > 0 Then Return True Else Return False
    End Function

    Private Function CreateGoalThisYear(Optional ByVal lastMonth As Integer = 0) As Boolean
        Dim createCount As Integer = 0
        For i As Integer = (lastMonth + 1) To 12
            createCount = createCount + CreateGoalThisMonth(i)
        Next

        If createCount = (12 - lastMonth) Then Return True Else Return False
    End Function

    Private Function CreateGoalThisMonth(ByVal sMonth As Integer) As Boolean
        Dim strSql As String = "SELECT departId FROM tblDepartment"
        Dim conn1 As New SqlConnection(connStr)
        Dim adapter1 As New SqlDataAdapter()
        adapter1.SelectCommand = New SqlCommand(strSql, conn1)

        Dim tbDepartment As New DataTable()
        conn1.Open()
        Try
            adapter1.Fill(tbDepartment)
        Finally
            conn1.Close()
        End Try

        Dim strIns As String = "INSERT INTO tblGoalSetting(goalType, departId, month, year) "
        For Each row As DataRow In tbDepartment.Rows
            strIns = strIns & "SELECT '0', '" & row.Field(Of Integer)(0).ToString & "', @month, @year UNION ALL "
        Next
        strIns = strIns & "SELECT '1', '0', @month, @year"
        Dim conn2 As New SqlConnection(connStr)
        Dim command2 As New SqlCommand(strIns, conn2)
        conn2.Open()
        command2.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command2.Parameters.Add("@year", SqlDbType.Int).Value = rcbYear.SelectedValue
        Dim result As Integer = command2.ExecuteNonQuery()
        conn2.Close()

        Return result
    End Function

    Private Function CreateGoalSingleRecord(ByVal goalType As Integer, ByVal departId As Integer, ByVal sMonth As Integer) As Boolean
        Dim strIns As String = "INSERT INTO tblGoalSetting(goalType, departId, month, year) VALUES(@goalType, @departId, @month, @year)"
        Dim conn As New SqlConnection(connStr)
        Dim command As New SqlCommand(strIns, conn)
        conn.Open()
        command.Parameters.Add("@goalType", SqlDbType.Int).Value = goalType
        command.Parameters.Add("@departId", SqlDbType.Int).Value = departId
        command.Parameters.Add("@month", SqlDbType.Int).Value = sMonth
        command.Parameters.Add("@year", SqlDbType.Int).Value = rcbYear.SelectedValue
        Dim result As Integer = command.ExecuteNonQuery()
        conn.Close()

        Return result
    End Function

    Private Function chkRepeat(ByVal ContractorName As String) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblContractor WHERE contractorName = @contractorName"
        Dim conn As New SqlConnection(connStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@contractorName", SqlDbType.VarChar).Value = ContractorName
        Dim Count As Integer = command.ExecuteScalar()
        conn.Close()

        If Count = 0 Then Return False Else Return True
    End Function

    Protected Sub rcbYear_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbYear.SelectedIndexChanged
        pnEditMTP.Visible = False
        rgGoalSetting.Rebind()
    End Sub

    Protected Sub rcbMonth_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbMonth.SelectedIndexChanged
        pnEditMTP.Visible = False
        rgGoalSetting.Rebind()
    End Sub

    Protected Sub btGetData_fsfl_Click(sender As Object, e As EventArgs)
        Dim btGetData As Button = sender
        Dim departId As Integer = CInt(btGetData.CommandArgument)

        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblEmployee WHERE joblvCode = 'fsfl' AND departId = '" & departId.ToString & "'"
        Dim count As Integer = 0
        Using connection As New SqlConnection(connStr)
            connection.Open()
            Dim command As New SqlCommand(strSqlCount, connection)
            count = command.ExecuteScalar()
        End Using

        Dim eitem As GridEditableItem = btGetData.NamingContainer
        Dim tbnTotalObserver As RadNumericTextBox = eitem.FindControl("tbn_total_fsfl")
        tbnTotalObserver.Text = count
    End Sub
    Protected Sub btGetData_tech_Click(sender As Object, e As EventArgs)
        Dim btGetData As Button = sender
        Dim departId As Integer = CInt(btGetData.CommandArgument)

        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblEmployee WHERE joblvCode = 'tech' AND departId = '" & departId.ToString & "'"
        Dim count As Integer = 0
        Using connection As New SqlConnection(connStr)
            connection.Open()
            Dim command As New SqlCommand(strSqlCount, connection)
            count = command.ExecuteScalar()
        End Using

        Dim eitem As GridEditableItem = btGetData.NamingContainer
        Dim tbnTotalObserver As RadNumericTextBox = eitem.FindControl("tbn_total_tech")
        tbnTotalObserver.Text = count
    End Sub

    Protected Sub imgEdit1_Click(sender As Object, e As ImageClickEventArgs)
        Dim btEditData As ImageButton = sender
        Dim paraPass As String = btEditData.CommandArgument
        Dim citem As GridFooterItem = btEditData.NamingContainer
        Dim lbFieldVisibility As Label = citem.FindControl("lbFooter_percentLeadershipFieldVisibility")
        Dim lbActionComplete As Label = citem.FindControl("lbFooter_percentActionCompleted")

        Dim FieldVisibilityStr As String = lbFieldVisibility.Text.Substring(0, lbFieldVisibility.Text.IndexOf(" %"))
        If FieldVisibilityStr <> "?" Then tbn_editpercentLeadershipFieldVisibility.Value = FieldVisibilityStr Else tbn_editpercentLeadershipFieldVisibility.Value = 0
        Dim ActionCompleteStr As String = lbActionComplete.Text.Substring(0, lbActionComplete.Text.IndexOf(" %"))
        If ActionCompleteStr <> "?" Then tbn_editpercentActionCompleted.Value = ActionCompleteStr Else tbn_editpercentActionCompleted.Value = 0

        pnEditMTP.Visible = True
        rgGoalSetting.Rebind()
    End Sub

    Protected Sub btCloseMTP_Click(sender As Object, e As EventArgs) Handles btCloseMTP.Click
        pnEditMTP.Visible = False
    End Sub

    Protected Sub btUpdateMTP_Click(sender As Object, e As EventArgs) Handles btUpdateMTP.Click
        'chk exist database (MTP)
        Dim departId As Integer = 0
        Dim goalType As Integer = 1
        ChkExistAllDepart(rcbYear.SelectedValue, rcbMonth.SelectedValue, departId, goalType)

        If selDepart = 0 Then
            'chk exist last manth, this year and create all months
            Dim lastMonth = getLastMonth(rcbYear.SelectedValue)
            CreateGoalThisYear(lastMonth)
        End If

        'update database
        updateGoalSettingMTP(rcbYear.SelectedValue, rcbMonth.SelectedValue, tbn_editpercentLeadershipFieldVisibility.Value, tbn_editpercentActionCompleted.Value)

        rgGoalSetting.Rebind()
        pnEditMTP.Visible = False
    End Sub

End Class