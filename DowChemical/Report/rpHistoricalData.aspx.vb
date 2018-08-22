Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class rpHistoricalData
    Inherits Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

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
            Dim rcbIdxY As Integer = rcbSelectYear.FindItemIndexByValue(Now().Year)
            rcbSelectYear.SelectedIndex = rcbIdxY
            Dim rcbIdxM As Integer = rcbSelectMonth.FindItemIndexByValue(Now().Month)
            rcbSelectMonth.SelectedIndex = rcbIdxM
        Else
            RadPanelBar1.Items.FindItemByText("REPORT").Items.FindItemByText("Historical Data").Selected = True
        End If

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
            End If
        End If
    End Sub

    Private Sub RadPanelBar1_ItemClick(sender As Object, e As RadPanelBarEventArgs) Handles RadPanelBar1.ItemClick
        If e.Item.Items.Count > 0 Then
            If e.Item.Text = "SETTING" Then
                e.Item.Selected = False
                RadPanelBar1.Items.FindItemByText("REPORT").Selected = True
            End If
            If e.Item.Text = "REPORT" Then
                RadPanelBar1.Items.FindItemByText("REPORT").Items.FindItemByText("Historical Data").Selected = True
            End If
        End If
    End Sub

    Private Sub rcbDepartment_DataBound(sender As Object, e As EventArgs) Handles rcbDepartment.DataBound
        Dim rcb As RadComboBox = sender
        Dim lastItem As Integer = rcb.Items.Count
        rcb.Items.Insert(lastItem, New RadComboBoxItem("Show All Department", "0"))
        rcb.Items.Insert(0, New RadComboBoxItem("[ Select Department ]", "1"))
    End Sub

    Private Function getQueryStr() As String
        Dim sqlStr As String = ""
        If rcbDepartment.SelectedIndex >= 0 Then
            sqlStr = "SELECT tblRpEmpHistorical.year, tblRpEmpHistorical.month, tblDepartment.departName, tblEmployee.empDisplay, tblEmployee.empDowId, tblEmployee.joblvCode, 
                      tblRpEmpHistorical.pLifeNearMiss, tblRpEmpHistorical.PSCE_ContainmentLoss, tblRpEmpHistorical.PSCE_PSNM, tblRpEmpHistorical.leadershipVisibility, 
                      tblRpEmpHistorical.secondEye, tblRpEmpHistorical.injuryNearMiss, tblRpEmpHistorical.proactiveCompliance, tblRpEmpHistorical.actionTotal, 
                      tblRpEmpHistorical.actionCompleted, tblRpEmpHistorical.recognition, tblRpEmpHistorical.reliability_wHRO, tblRpEmpHistorical.quality_wHRO, 
                      tblRpEmpHistorical.reliability FROM tblRpEmpHistorical 
                      INNER JOIN tblEmployee ON tblRpEmpHistorical.empId = tblEmployee.empId 
                      INNER JOIN tblDepartment ON tblEmployee.departId = tblDepartment.departId 
                      WHERE tblRpEmpHistorical.year = '" & rcbSelectYear.SelectedValue & "' AND tblRpEmpHistorical.month = '" & rcbSelectMonth.SelectedValue & "' "

            If chkOnly_fsfl.Checked Then sqlStr = sqlStr & "AND tblEmployee.joblvCode = 'fsfl' "
            If rcbDepartment.SelectedValue <> "0" Then
                sqlStr = sqlStr & "AND tblRpEmpHistorical.departId = '" & rcbDepartment.SelectedValue & "' ORDER BY tblEmployee.empDisplay"
            Else
                sqlStr = sqlStr & " ORDER BY tblDepartment.departName, tblEmployee.empDisplay"
            End If

        End If

        Return sqlStr
    End Function

    Private Sub rgRecordList_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rgRecordList.NeedDataSource
        If rcbDepartment.SelectedIndex >= 0 Then
            If Not e.IsFromDetailTable Then
                Dim sqlStr As String = getQueryStr()
                rgRecordList.DataSource = GetDataTable(sqlStr)
            End If
        End If
    End Sub

    Public Function GetDataTable(query As String) As DataTable
        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(query, conn)
        Dim myDataTable As New DataTable()

        conn.Open()
        Try
            adapter.Fill(myDataTable)
        Finally
            conn.Close()
        End Try

        Return myDataTable
    End Function

    'Protected Sub rcbSelectYear_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbSelectYear.SelectedIndexChanged
    '    rgRecordList.Rebind()
    'End Sub
    'Protected Sub rcbSelectMonth_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbSelectMonth.SelectedIndexChanged
    '    rgRecordList.Rebind()
    'End Sub
    'Protected Sub rcbDepartment_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbDepartment.SelectedIndexChanged
    '    rgRecordList.Rebind()
    'End Sub
    Protected Sub chkOnly_fsfl_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnly_fsfl.CheckedChanged
        rgRecordList.Rebind()
    End Sub

    Dim totalpLifeNearMiss As Integer
    Dim totalPSCE_ContainmentLoss As Integer
    Dim totalPSCE_PSNM As Integer
    Dim totalleadershipVisibility As Integer
    Dim totalsecondEye As Integer
    Dim totalinjuryNearMiss As Integer
    Dim totalproactiveCompliance As Integer
    Dim totalactionTotal As Integer
    Dim totalactionCompleted As Integer
    Dim totalrecognition As Integer
    Dim totalreliability_wHRO As Integer
    Dim totalquality_wHRO As Integer
    Dim totalreliability As Integer
    Private Sub rgRecordList_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgRecordList.ItemDataBound
        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

            Dim pLifeNearMissLabel As Label = item.FindControl("pLifeNearMissLabel")
            totalpLifeNearMiss = totalpLifeNearMiss + pLifeNearMissLabel.Text
            Dim PSCE_ContainmentLossLabel As Label = item.FindControl("PSCE_ContainmentLossLabel")
            totalPSCE_ContainmentLoss = totalPSCE_ContainmentLoss + PSCE_ContainmentLossLabel.Text
            Dim PSCE_PSNMLabel As Label = item.FindControl("PSCE_PSNMLabel")
            totalPSCE_PSNM = totalPSCE_PSNM + PSCE_PSNMLabel.Text
            Dim leadershipVisibilityLabel As Label = item.FindControl("leadershipVisibilityLabel")
            totalleadershipVisibility = totalleadershipVisibility + leadershipVisibilityLabel.Text
            leadershipVisibilityLabel.Text = FormatNumber(leadershipVisibilityLabel.Text, 0, , , TriState.True)

            Dim secondEyeLabel As Label = item.FindControl("secondEyeLabel")
            totalsecondEye = totalsecondEye + secondEyeLabel.Text
            Dim injuryNearMissLabel As Label = item.FindControl("injuryNearMissLabel")
            totalinjuryNearMiss = totalinjuryNearMiss + injuryNearMissLabel.Text
            Dim proactiveComplianceLabel As Label = item.FindControl("proactiveComplianceLabel")
            totalproactiveCompliance = totalproactiveCompliance + proactiveComplianceLabel.Text
            Dim actionTotalLabel As Label = item.FindControl("actionTotalLabel")
            totalactionTotal = totalactionTotal + actionTotalLabel.Text
            Dim actionCompletedLabel As Label = item.FindControl("actionCompletedLabel")
            totalactionCompleted = totalactionCompleted + actionCompletedLabel.Text
            Dim recognitionLabel As Label = item.FindControl("recognitionLabel")
            totalrecognition = totalrecognition + recognitionLabel.Text
            Dim reliability_wHROLabel As Label = item.FindControl("reliability_wHROLabel")
            totalreliability_wHRO = totalreliability_wHRO + reliability_wHROLabel.Text
            Dim quality_wHROLabel As Label = item.FindControl("quality_wHROLabel")
            totalquality_wHRO = totalquality_wHRO + quality_wHROLabel.Text
            Dim reliabilityLabel As Label = item.FindControl("reliabilityLabel")
            totalreliability = totalreliability + recognitionLabel.Text
        End If

        If TypeOf e.Item Is GridFooterItem Then
            Dim fitem As GridFooterItem = CType(e.Item, GridFooterItem)

            Dim ft_pLifeNearMissLabel As Label = fitem.FindControl("ft_pLifeNearMissLabel")
            ft_pLifeNearMissLabel.Text = totalpLifeNearMiss
            Dim ft_PSCE_ContainmentLossLabel As Label = fitem.FindControl("ft_PSCE_ContainmentLossLabel")
            ft_PSCE_ContainmentLossLabel.Text = totalPSCE_ContainmentLoss
            Dim ft_PSCE_PSNMLabel As Label = fitem.FindControl("ft_PSCE_PSNMLabel")
            ft_PSCE_PSNMLabel.Text = totalPSCE_PSNM
            Dim ft_leadershipVisibilityLabel As Label = fitem.FindControl("ft_leadershipVisibilityLabel")
            ft_leadershipVisibilityLabel.Text = FormatNumber(totalleadershipVisibility, 0, , , TriState.True)

            Dim ft_secondEyeLabel As Label = fitem.FindControl("ft_secondEyeLabel")
            ft_secondEyeLabel.Text = totalsecondEye
            Dim ft_injuryNearMissLabel As Label = fitem.FindControl("ft_injuryNearMissLabel")
            ft_injuryNearMissLabel.Text = totalinjuryNearMiss
            Dim ft_proactiveComplianceLabel As Label = fitem.FindControl("ft_proactiveComplianceLabel")
            ft_proactiveComplianceLabel.Text = totalproactiveCompliance
            Dim ft_actionTotalLabel As Label = fitem.FindControl("ft_actionTotalLabel")
            ft_actionTotalLabel.Text = totalactionTotal
            Dim ft_actionCompletedLabel As Label = fitem.FindControl("ft_actionCompletedLabel")
            ft_actionCompletedLabel.Text = totalactionCompleted
            Dim ft_recognitionLabel As Label = fitem.FindControl("ft_recognitionLabel")
            ft_recognitionLabel.Text = totalrecognition
            Dim ft_reliability_wHROLabel As Label = fitem.FindControl("ft_reliability_wHROLabel")
            ft_reliability_wHROLabel.Text = totalreliability_wHRO
            Dim ft_quality_wHROLabel As Label = fitem.FindControl("ft_quality_wHROLabel")
            ft_quality_wHROLabel.Text = totalquality_wHRO
            Dim ft_reliabilityLabel As Label = fitem.FindControl("ft_reliabilityLabel")
            ft_reliabilityLabel.Text = totalreliability
        End If
    End Sub

    Protected Sub btSearchHistory_Click(sender As Object, e As EventArgs) Handles btSearchHistory.Click
        If rcbDepartment.SelectedIndex >= 0 Then
            Dim sqlStr As String = getQueryStr()
            rgRecordList.DataSource = GetDataTable(sqlStr)
            rgRecordList.Rebind()
        End If
    End Sub
End Class