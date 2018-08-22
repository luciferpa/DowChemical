Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class setGoal2
    Inherits Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Dim Observer_fsfl As Integer
    Dim Observer_tech As Integer
    Dim SecondEyeSafety As Integer
    Dim PSCE_ContainmentLoss As Integer
    Dim PSCE_PSNM As Integer
    Dim PercentLeadershipFieldVisibility As Double
    Dim PercentActionCompleted As Double
    Dim ComplianceProactiveReport As Integer
    Dim TotalObserver As Integer

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
            Dim SettingItem As RadPanelItem = RadPanelBar1.Items.FindItemByText("SETTING")
            SettingItem.Visible = True
            SettingItem.Items.FindItemByText("DEPARTMENT").Visible = False
            SettingItem.Items.FindItemByText("CONTRACTOR").Visible = False
            SettingItem.Items.FindItemByText("GOAL SETTING").Visible = False
            SettingItem.Items.FindItemByText("CATEGORY").Visible = False
            SettingItem.Items.FindItemByText("IMPORT DATA").Visible = False
        End If
    End Sub

    Dim totalObserver_fsfl As Integer
    Dim totalObserver_tech As Integer
    Dim totalSeconfEyeSafety As Integer
    Dim totalPSCE_ContainmentLoss As Integer
    Dim totalPSCE_PSNM As Integer
    Dim totalPercentLeadershipFieldVisibility As Double
    Dim totalPercentActionCompleted As Double
    Dim totalComplianceProactiveReport As Integer
    Private Sub rgGoalSetting_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgGoalSetting.ItemDataBound
        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

            Dim hfDepartId As HiddenField = item.FindControl("hfDepartId")
            Dim lbfsflUser As Label = item.FindControl("lb_fsflUser")
            Dim lbtechUser As Label = item.FindControl("lb_techUser")
            Dim lbSeconfEyeSafety As Label = item.FindControl("lb_seconfEyeSafety")
            Dim lbPSCE_ContainmentLoss As Label = item.FindControl("lb_PSCE_ContainmentLoss")
            Dim lbPSCE_PSNM As Label = item.FindControl("lb_PSCE_PSNM")
            Dim lbPercentLeadershipFieldVisibility As Label = item.FindControl("lb_percentLeadershipFieldVisibility")
            Dim lbPercentActionCompleted As Label = item.FindControl("lb_percentActionCompleted")
            Dim lbComplianceProactiveReport As Label = item.FindControl("lb_complianceProactiveReport")
            Dim lbTimestamp As Label = item.FindControl("lbTimestamp")

            'get last update goal data
            Using conn As New SqlConnection(ConnStr)
                conn.Open()
                Dim strSql As String = "SELECT TOP (1) goalId, goalType, departId, totalObserver_fsfl, totalObserver_tech, seconfEyeSafety, PSCE_ContainmentLoss, PSCE_PSNM, 
                                        percentLeadershipFieldVisibility, percentActionCompleted, complianceProactiveReport, timestamp  
                                        FROM tblGoalSetting WHERE departId = @departId AND month = @month AND year = @year ORDER BY goalId DESC"

                Dim comm As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
                comm.Parameters.Add("@departId", SqlDbType.Int).Value = CInt(hfDepartId.Value)
                comm.Parameters.Add("@month", SqlDbType.Int).Value = CInt(rcbMonth.SelectedValue)
                comm.Parameters.Add("@year", SqlDbType.Int).Value = CInt(rcbYear.SelectedValue)

                Dim DataRead As SqlDataReader
                DataRead = comm.ExecuteReader()
                If DataRead.HasRows Then
                    While DataRead.Read()
                        lbfsflUser.Text = FormatNumber(DataRead("totalObserver_fsfl"), 0, , , TriState.True)
                        lbtechUser.Text = FormatNumber(DataRead("totalObserver_tech"), 0, , , TriState.True)
                        lbSeconfEyeSafety.Text = FormatNumber(DataRead("seconfEyeSafety"), 0, , , TriState.True)
                        lbPSCE_ContainmentLoss.Text = FormatNumber(DataRead("PSCE_ContainmentLoss"), 0, , , TriState.True)
                        lbPSCE_PSNM.Text = FormatNumber(DataRead("PSCE_PSNM"), 0, , , TriState.True)

                        Dim plfvValue As Double = CDbl(DataRead("percentLeadershipFieldVisibility"))
                        plfvValue = plfvValue * 10
                        If (plfvValue Mod 10) = 0 Then
                            lbPercentLeadershipFieldVisibility.Text = (plfvValue / 1000).ToString("P0")
                        Else
                            lbPercentLeadershipFieldVisibility.Text = (plfvValue / 1000).ToString("P1")
                        End If

                        Dim pcompValue As Double = CDbl(DataRead("percentActionCompleted"))
                        pcompValue = pcompValue * 10
                        If (pcompValue Mod 10) = 0 Then
                            lbPercentActionCompleted.Text = (pcompValue / 1000).ToString("P0")
                        Else
                            lbPercentActionCompleted.Text = (pcompValue / 1000).ToString("P1")
                        End If

                        lbComplianceProactiveReport.Text = FormatNumber(DataRead("complianceProactiveReport"), 0, , , TriState.True)
                        lbTimestamp.Text = DataRead("timestamp")
                    End While
                Else
                    lbfsflUser.Text = "0"
                    lbtechUser.Text = "0"
                    lbSeconfEyeSafety.Text = "0"
                    lbPSCE_ContainmentLoss.Text = "0"
                    lbPSCE_PSNM.Text = "0"
                    lbPercentLeadershipFieldVisibility.Text = "0 %"
                    lbPercentActionCompleted.Text = "0 %"
                    lbComplianceProactiveReport.Text = "0"
                    lbTimestamp.Text = ""
                End If

                Observer_fsfl = lbfsflUser.Text
                Observer_tech = lbtechUser.Text
                SecondEyeSafety = lbSeconfEyeSafety.Text
                PSCE_ContainmentLoss = lbPSCE_ContainmentLoss.Text
                PSCE_PSNM = lbPSCE_PSNM.Text
                PercentLeadershipFieldVisibility = lbPercentLeadershipFieldVisibility.Text.Substring(0, lbPercentLeadershipFieldVisibility.Text.IndexOf(" %"))
                PercentActionCompleted = lbPercentActionCompleted.Text.Substring(0, lbPercentActionCompleted.Text.IndexOf(" %"))
                ComplianceProactiveReport = lbComplianceProactiveReport.Text

                totalObserver_fsfl = totalObserver_fsfl + Observer_fsfl
                totalObserver_tech = totalObserver_tech + Observer_tech
                totalSeconfEyeSafety = totalSeconfEyeSafety + SecondEyeSafety
                totalPSCE_ContainmentLoss = totalPSCE_ContainmentLoss + PSCE_ContainmentLoss
                totalPSCE_PSNM = totalPSCE_PSNM + PSCE_PSNM
                totalComplianceProactiveReport = totalComplianceProactiveReport + ComplianceProactiveReport

                'get MTP Ops data



                totalPercentLeadershipFieldVisibility = ""
                totalPercentActionCompleted = ""
            End Using
        End If

        '-- GridFooter
        If TypeOf e.Item Is GridFooterItem Then
            Dim fitem As GridFooterItem = CType(e.Item, GridFooterItem)

            Dim lbft_Footer_fsflUser As Label = fitem.FindControl("lbFooter_fsflUser")
            lbft_Footer_fsflUser.Text = totalObserver_fsfl.ToString

            Dim lbft_Footer_techUser As Label = fitem.FindControl("lbFooter_techUser")
            lbft_Footer_techUser.Text = totalObserver_tech.ToString

            Dim lbft_seconfEyeSafety As Label = fitem.FindControl("lbFooter_seconfEyeSafety")
            lbft_seconfEyeSafety.Text = FormatNumber(totalSeconfEyeSafety, 0, , , TriState.True)

            Dim lbft_PSCE_ContainmentLoss As Label = fitem.FindControl("lbFooter_PSCE_ContainmentLoss")
            lbft_PSCE_ContainmentLoss.Text = FormatNumber(totalPSCE_ContainmentLoss, 0, , , TriState.True)

            Dim lbft_PSCE_PSNM As Label = fitem.FindControl("lbFooter_PSCE_PSNM")
            lbft_PSCE_PSNM.Text = FormatNumber(totalPSCE_PSNM, 0, , , TriState.True)

            Dim lbft_percentLeadershipFieldVisibility As Label = fitem.FindControl("lbFooter_percentLeadershipFieldVisibility")
            lbft_percentLeadershipFieldVisibility.Text = totalPercentLeadershipFieldVisibility.ToString & " %"

            Dim lbft_percentActionCompleted As Label = fitem.FindControl("lbFooter_percentActionCompleted")
            lbft_percentActionCompleted.Text = totalPercentActionCompleted.ToString & " %"

            Dim lbft_complianceProactiveReport As Label = fitem.FindControl("lbFooter_complianceProactiveReport")
            lbft_complianceProactiveReport.Text = totalComplianceProactiveReport.ToString

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
            tbn_fsflUser.Text = Observer_fsfl.ToString
            Dim tbn_techUser As RadNumericTextBox = eitem.FindControl("tbn_total_tech")
            tbn_techUser.Text = Observer_tech.ToString

            Dim tbnSecondEye As RadNumericTextBox = eitem.FindControl("tbn_seconfEyeSafety")
            tbnSecondEye.Text = SecondEyeSafety.ToString
            Dim tbnPSCE_ContainmentLoss As RadNumericTextBox = eitem.FindControl("tbn_PSCE_ContainmentLoss")
            tbnPSCE_ContainmentLoss.Text = PSCE_ContainmentLoss.ToString
            Dim tbnPSCE_PSNM As RadNumericTextBox = eitem.FindControl("tbn_PSCE_PSNM")
            tbnPSCE_PSNM.Text = PSCE_PSNM.ToString

            Dim tbnPercentLeadershipFieldVisibility As RadNumericTextBox = eitem.FindControl("tbn_percentLeadershipFieldVisibility")
            tbnPercentLeadershipFieldVisibility.Text = PercentLeadershipFieldVisibility.ToString
            Dim tbnPercentActionCompleted As RadNumericTextBox = eitem.FindControl("tbn_percentActionCompleted")
            tbnPercentActionCompleted.Text = PercentActionCompleted.ToString

            Dim tbnComplianceProactiveReport As RadNumericTextBox = eitem.FindControl("tbn_complianceProactiveReport")
            tbnComplianceProactiveReport.Text = ComplianceProactiveReport.ToString

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

                Dim tb_pLifeNearMissIncident As RadNumericTextBox = DirectCast(item.FindControl("tbn_pLifeNearMissIncident"), RadNumericTextBox)
                Dim tb_seconfEyeSafety As RadNumericTextBox = DirectCast(item.FindControl("tbn_seconfEyeSafety"), RadNumericTextBox)
                Dim tb_PSCE_ContainmentLoss As RadNumericTextBox = DirectCast(item.FindControl("tbn_PSCE_ContainmentLoss"), RadNumericTextBox)
                Dim tb_PSCE_PSNM As RadNumericTextBox = DirectCast(item.FindControl("tbn_PSCE_PSNM"), RadNumericTextBox)
                Dim tb_percentLeadershipFieldVisibility As RadNumericTextBox = DirectCast(item.FindControl("tbn_percentLeadershipFieldVisibility"), RadNumericTextBox)
                Dim tb_complianceProactiveReport As RadNumericTextBox = DirectCast(item.FindControl("tbn_complianceProactiveReport"), RadNumericTextBox)
                Dim tb_totalObserver As RadNumericTextBox = DirectCast(item.FindControl("tbn_totalObserver"), RadNumericTextBox)

                Dim strIns As String = "INSERT INTO tblGoalSetting(goalType, departId, pLifeNearMissIncident, seconfEyeSafety, PSCE_ContainmentLoss, PSCE_PSNM, percentLeadershipFieldVisibility, complianceProactiveReport, totalObserver, month, year, timestamp) 
                                    VALUES(@goalType, @departId, @pLifeNearMissIncident, @seconfEyeSafety, @PSCE_ContainmentLoss, @PSCE_PSNM, @percentLeadershipFieldVisibility, @complianceProactiveReport, @totalObserver, @month, @year, @timestamp)"

                Dim conn As New SqlConnection(ConnStr)
                conn.Open()
                Dim command As New SqlCommand(strIns, conn)
                command.Parameters.Add("@goalType", SqlDbType.Int).Value = 0
                command.Parameters.Add("@departId", SqlDbType.Int).Value = item.GetDataKeyValue("departId")
                command.Parameters.Add("@pLifeNearMissIncident", SqlDbType.Int).Value = CInt(tb_pLifeNearMissIncident.Text)
                command.Parameters.Add("@seconfEyeSafety", SqlDbType.Int).Value = CInt(tb_seconfEyeSafety.Text)
                command.Parameters.Add("@PSCE_ContainmentLoss", SqlDbType.Int).Value = CInt(tb_PSCE_ContainmentLoss.Text)
                command.Parameters.Add("@PSCE_PSNM", SqlDbType.Int).Value = CInt(tb_PSCE_PSNM.Text)
                command.Parameters.Add("@percentLeadershipFieldVisibility", SqlDbType.Money).Value = CDbl(tb_percentLeadershipFieldVisibility.Text)
                command.Parameters.Add("@complianceProactiveReport", SqlDbType.Int).Value = CInt(tb_complianceProactiveReport.Text)
                command.Parameters.Add("@totalObserver", SqlDbType.Int).Value = CInt(tb_totalObserver.Text)
                command.Parameters.Add("@month", SqlDbType.Int).Value = CInt(rcbMonth.SelectedValue)
                command.Parameters.Add("@year", SqlDbType.Int).Value = CInt(rcbYear.SelectedValue)
                command.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = Now()
                Dim err As Integer = command.ExecuteNonQuery()
                conn.Close()

                MsgBoxRad("<b>Goal update.</b>", 240, 76)
            End If
        End If
    End Sub

    Private Function chkRepeat(ByVal ContractorName As String) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblContractor WHERE contractorName = @contractorName"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@contractorName", SqlDbType.VarChar).Value = ContractorName
        Dim Count As Integer = command.ExecuteScalar()
        conn.Close()

        If Count = 0 Then Return False Else Return True
    End Function

    Protected Sub rcbYear_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbYear.SelectedIndexChanged
        rgGoalSetting.Rebind()
    End Sub

    Protected Sub rcbMonth_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbMonth.SelectedIndexChanged
        rgGoalSetting.Rebind()
    End Sub

    Protected Sub btGetData_fsfl_Click(sender As Object, e As EventArgs)
        Dim btGetData As Button = sender
        Dim departId As Integer = CInt(btGetData.CommandArgument)

        Dim strSqlCount As String = "SELECT COUNT(*) FROM tblEmployee WHERE joblvCode = 'fsfl' AND departId = '" & departId.ToString & "'"
        Dim count As Integer = 0
        Using connection As New SqlConnection(ConnStr)
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
        Using connection As New SqlConnection(ConnStr)
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

        tbn_editpercentLeadershipFieldVisibility.Value = lbFieldVisibility.Text.Substring(0, lbFieldVisibility.Text.IndexOf(" %"))
        tbn_editpercentActionCompleted.Value = lbActionComplete.Text.Substring(0, lbActionComplete.Text.IndexOf(" %"))
        pnEditMTP.Visible = True
        rgGoalSetting.Rebind()
    End Sub

    Protected Sub btCloseMTP_Click(sender As Object, e As EventArgs) Handles btCloseMTP.Click
        pnEditMTP.Visible = False
    End Sub
End Class