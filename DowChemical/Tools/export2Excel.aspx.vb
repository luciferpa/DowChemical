Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI
Imports Telerik.Web.UI.GridExcelBuilder
Imports xi = Telerik.Web.UI.ExportInfrastructure
Imports System.Drawing
Imports System.Data.SqlClient

Public Class export2Excel
    Inherits Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Dim _1001Desc As String = "Recognition"
    Dim _1002Desc As String = "In-progress"
    Dim _1003Desc As String = "Complete"

    Private Sub MsgBoxRad(ByVal Msg As String, ByVal Width As Integer, ByVal Height As Integer)
        'RadWindowManager1.Width = Width
        'RadWindowManager1.Height = Height
        'RadWindowManager1.RadAlert(Msg, Width + 100, Height + 72, "My Alert", "", "myAlertImage.png")
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

            '-- set dropdown
            Dim ThisMonth As Integer = Now.Month
            Dim ThisYear As Integer = Now.Year
            rcbSelMonth.SelectedIndex = ThisMonth - 1
            rcbSelYear.SelectedIndex = ThisYear
        Else
            RadPanelBar1.Items.FindItemByText("REPORT").Items.FindItemByText("Export to Excel").Selected = True
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

            If User.IsInRole("SYSTEM ADMIN") Then
                btSupportTools.Visible = True
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
                RadPanelBar1.Items.FindItemByText("REPORT").Items.FindItemByText("Export to Excel").Selected = True
            End If
        End If
    End Sub

    Private Sub rcbDepartment_DataBound(sender As Object, e As EventArgs) Handles rcbDepartment.DataBound
        Dim rcb As RadComboBox = sender
        rcb.Items.Insert(0, New RadComboBoxItem("Show All Department", "0"))
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
    'Private Sub RadGrid1_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
    '    If Not e.IsFromDetailTable Then
    '        Dim sqlStr As String
    '        sqlStr = "SELECT tblRecord.recId, tblRecord.recActNo, tblRecord.recActDate, tblRecord.recActTime, tblRecord.durationValue, tblObserver1.empFullName, tblObserver1.empDowId, 
    '                    tblDepartment.departName, tblRecord.noObserve,
    '                         (SELECT tblEmployee1a.empFullName
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO1a 
    '                            INNER JOIN tblEmployee AS tblEmployee1a ON tblRecordOthEmpO1a.empIdOth = tblEmployee1a.empId
    '                            WHERE (tblRecordOthEmpO1a.recId = tblRecord.recId) AND (tblRecordOthEmpO1a.recItem = '1')) AS othNameObs1,
    '                         (SELECT tblEmployee1b.empDowId
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO1b 
    '                            INNER JOIN tblEmployee AS tblEmployee1b ON tblRecordOthEmpO1b.empIdOth = tblEmployee1b.empId
    '                            WHERE (tblRecordOthEmpO1b.recId = tblRecord.recId) AND (tblRecordOthEmpO1b.recItem = '1')) AS othDowIdObs1,
    '                         (SELECT tblDepartment1c.departName
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO1c 
    '                            INNER JOIN tblEmployee AS tblEmployee1c ON tblRecordOthEmpO1c.empIdOth = tblEmployee1c.empId 
    '                            INNER JOIN tblDepartment AS tblDepartment1c ON tblEmployee1c.departId = tblDepartment1c.departId
    '                            WHERE (tblRecordOthEmpO1c.recId = tblRecord.recId) AND (tblRecordOthEmpO1c.recItem = '1')) AS othDepartObs1,
    '                         (SELECT tblEmployee2a.empFullName
    '                           FROM tblRecordOthEmpO AS tblRecordOthEmpO2a 
    '                            INNER JOIN tblEmployee AS tblEmployee2a ON tblRecordOthEmpO2a.empIdOth = tblEmployee2a.empId
    '                            WHERE (tblRecordOthEmpO2a.recId = tblRecord.recId) AND (tblRecordOthEmpO2a.recItem = '2')) AS othNameObs2,
    '                         (SELECT tblEmployee2b.empDowId
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO2b 
    '                            INNER JOIN tblEmployee AS tblEmployee2b ON tblRecordOthEmpO2b.empIdOth = tblEmployee2b.empId
    '                            WHERE (tblRecordOthEmpO2b.recId = tblRecord.recId) AND (tblRecordOthEmpO2b.recItem = '2')) AS othDowIdObs2,
    '                         (SELECT tblDepartment2c.departName
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO2c 
    '                            INNER JOIN tblEmployee AS tblEmployee2c ON tblRecordOthEmpO2c.empIdOth = tblEmployee2c.empId 
    '                            INNER JOIN tblDepartment AS tblDepartment2c ON tblEmployee2c.departId = tblDepartment2c.departId
    '                            WHERE (tblRecordOthEmpO2c.recId = tblRecord.recId) AND (tblRecordOthEmpO2c.recItem = '2')) AS othDepartObs2,
    '                         (SELECT tblEmployee3a.empFullName
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO3a 
    '                            INNER JOIN tblEmployee AS tblEmployee3a ON tblRecordOthEmpO3a.empIdOth = tblEmployee3a.empId
    '                            WHERE (tblRecordOthEmpO3a.recId = tblRecord.recId) AND (tblRecordOthEmpO3a.recItem = '3')) AS othNameObs3,
    '                         (SELECT tblEmployee3b.empDowId
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO3b 
    '                            INNER JOIN tblEmployee AS tblEmployee3b ON tblRecordOthEmpO3b.empIdOth = tblEmployee3b.empId
    '                            WHERE (tblRecordOthEmpO3b.recId = tblRecord.recId) AND (tblRecordOthEmpO3b.recItem = '3')) AS othDowIdObs3,
    '                         (SELECT tblDepartment3c.departName
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO3c 
    '                            INNER JOIN tblEmployee AS tblEmployee3c ON tblRecordOthEmpO3c.empIdOth = tblEmployee3c.empId 
    '                            INNER JOIN tblDepartment AS tblDepartment3c ON tblEmployee3c.departId = tblDepartment3c.departId
    '                            WHERE (tblRecordOthEmpO3c.recId = tblRecord.recId) AND (tblRecordOthEmpO3c.recItem = '3')) AS othDepartObs3,
    '                         (SELECT tblEmployee4a.empFullName
    '                           FROM tblRecordOthEmpO AS tblRecordOthEmpO4a 
    '                            INNER JOIN tblEmployee AS tblEmployee4a ON tblRecordOthEmpO4a.empIdOth = tblEmployee4a.empId
    '                            WHERE (tblRecordOthEmpO4a.recId = tblRecord.recId) AND (tblRecordOthEmpO4a.recItem = '4')) AS othNameObs4,
    '                         (SELECT tblEmployee4b.empDowId
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO4b 
    '                            INNER JOIN tblEmployee AS tblEmployee4b ON tblRecordOthEmpO4b.empIdOth = tblEmployee4b.empId
    '                            WHERE (tblRecordOthEmpO4b.recId = tblRecord.recId) AND (tblRecordOthEmpO4b.recItem = '4')) AS othDowIdObs4,
    '                         (SELECT tblDepartment4c.departName
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO4c 
    '                            INNER JOIN tblEmployee AS tblEmployee4c ON tblRecordOthEmpO4c.empIdOth = tblEmployee4c.empId 
    '                            INNER JOIN tblDepartment AS tblDepartment4c ON tblEmployee4c.departId = tblDepartment4c.departId
    '                            WHERE (tblRecordOthEmpO4c.recId = tblRecord.recId) AND (tblRecordOthEmpO4c.recItem = '4')) AS othDepartObs4,
    '                         (SELECT tblEmployee5a.empFullName
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO5a 
    '                            INNER JOIN tblEmployee AS tblEmployee5a ON tblRecordOthEmpO5a.empIdOth = tblEmployee5a.empId
    '                            WHERE (tblRecordOthEmpO5a.recId = tblRecord.recId) AND (tblRecordOthEmpO5a.recItem = '5')) AS othNameObs5,
    '                         (SELECT tblEmployee5b.empDowId
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO5b 
    '                            INNER JOIN tblEmployee AS tblEmployee5b ON tblRecordOthEmpO5b.empIdOth = tblEmployee5b.empId
    '                            WHERE (tblRecordOthEmpO5b.recId = tblRecord.recId) AND (tblRecordOthEmpO5b.recItem = '5')) AS othDowIdObs5,
    '                         (SELECT tblDepartment5c.departName
    '                            FROM tblRecordOthEmpO AS tblRecordOthEmpO5c 
    '                            INNER JOIN tblEmployee AS tblEmployee5c ON tblRecordOthEmpO5c.empIdOth = tblEmployee5c.empId 
    '                            INNER JOIN tblDepartment AS tblDepartment5c ON tblEmployee5c.departId = tblDepartment5c.departId
    '                            WHERE (tblRecordOthEmpO5c.recId = tblRecord.recId) AND (tblRecordOthEmpO5c.recItem = '5')) AS othDepartObs5
    '                    FROM tblRecord 
    '                    INNER JOIN tblEmployee AS tblObserver1 ON tblRecord.empId = tblObserver1.empId 
    '                    INNER JOIN tblDepartment ON tblObserver1.departId = tblDepartment.departId 
    '                    WHERE (tblRecord.recActive = 'true') AND (tblRecord.recActMonth = '" & rcbSelMonth.SelectedValue & "') AND (tblRecord.recActYear ='" & rcbSelYear.SelectedValue & "') "

    '        If rcbDepartment.SelectedIndex > 0 Then
    '            sqlStr = sqlStr & "AND (tblRecord.departId = '" & rcbDepartment.SelectedValue & "')"
    '        End If

    '        RadGrid1.DataSource = GetDataTable(sqlStr)
    '    End If
    'End Sub

    Protected Sub btSearchReport_Click(sender As Object, e As EventArgs) Handles btSearchReport.Click
        Dim sqlStr As String
        sqlStr = "SELECT tblRecord.recId, tblRecord.recActNo, tblRecord.recActDate, tblRecord.recActTime, tblRecord.durationValue, tblObserver1.empFullName, tblObserver1.empDowId, 
                        tblDepartment.departName, tblRecord.noObserve,
                             (SELECT tblEmployee1a.empFullName
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO1a 
                                INNER JOIN tblEmployee AS tblEmployee1a ON tblRecordOthEmpO1a.empIdOth = tblEmployee1a.empId
                                WHERE (tblRecordOthEmpO1a.recId = tblRecord.recId) AND (tblRecordOthEmpO1a.recItem = '1')) AS othNameObs1,
                             (SELECT tblEmployee1b.empDowId
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO1b 
                                INNER JOIN tblEmployee AS tblEmployee1b ON tblRecordOthEmpO1b.empIdOth = tblEmployee1b.empId
                                WHERE (tblRecordOthEmpO1b.recId = tblRecord.recId) AND (tblRecordOthEmpO1b.recItem = '1')) AS othDowIdObs1,
                             (SELECT tblDepartment1c.departName
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO1c 
                                INNER JOIN tblEmployee AS tblEmployee1c ON tblRecordOthEmpO1c.empIdOth = tblEmployee1c.empId 
                                INNER JOIN tblDepartment AS tblDepartment1c ON tblEmployee1c.departId = tblDepartment1c.departId
                                WHERE (tblRecordOthEmpO1c.recId = tblRecord.recId) AND (tblRecordOthEmpO1c.recItem = '1')) AS othDepartObs1,
                             (SELECT tblEmployee2a.empFullName
                               FROM tblRecordOthEmpO AS tblRecordOthEmpO2a 
                                INNER JOIN tblEmployee AS tblEmployee2a ON tblRecordOthEmpO2a.empIdOth = tblEmployee2a.empId
                                WHERE (tblRecordOthEmpO2a.recId = tblRecord.recId) AND (tblRecordOthEmpO2a.recItem = '2')) AS othNameObs2,
                             (SELECT tblEmployee2b.empDowId
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO2b 
                                INNER JOIN tblEmployee AS tblEmployee2b ON tblRecordOthEmpO2b.empIdOth = tblEmployee2b.empId
                                WHERE (tblRecordOthEmpO2b.recId = tblRecord.recId) AND (tblRecordOthEmpO2b.recItem = '2')) AS othDowIdObs2,
                             (SELECT tblDepartment2c.departName
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO2c 
                                INNER JOIN tblEmployee AS tblEmployee2c ON tblRecordOthEmpO2c.empIdOth = tblEmployee2c.empId 
                                INNER JOIN tblDepartment AS tblDepartment2c ON tblEmployee2c.departId = tblDepartment2c.departId
                                WHERE (tblRecordOthEmpO2c.recId = tblRecord.recId) AND (tblRecordOthEmpO2c.recItem = '2')) AS othDepartObs2,
                             (SELECT tblEmployee3a.empFullName
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO3a 
                                INNER JOIN tblEmployee AS tblEmployee3a ON tblRecordOthEmpO3a.empIdOth = tblEmployee3a.empId
                                WHERE (tblRecordOthEmpO3a.recId = tblRecord.recId) AND (tblRecordOthEmpO3a.recItem = '3')) AS othNameObs3,
                             (SELECT tblEmployee3b.empDowId
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO3b 
                                INNER JOIN tblEmployee AS tblEmployee3b ON tblRecordOthEmpO3b.empIdOth = tblEmployee3b.empId
                                WHERE (tblRecordOthEmpO3b.recId = tblRecord.recId) AND (tblRecordOthEmpO3b.recItem = '3')) AS othDowIdObs3,
                             (SELECT tblDepartment3c.departName
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO3c 
                                INNER JOIN tblEmployee AS tblEmployee3c ON tblRecordOthEmpO3c.empIdOth = tblEmployee3c.empId 
                                INNER JOIN tblDepartment AS tblDepartment3c ON tblEmployee3c.departId = tblDepartment3c.departId
                                WHERE (tblRecordOthEmpO3c.recId = tblRecord.recId) AND (tblRecordOthEmpO3c.recItem = '3')) AS othDepartObs3,
                             (SELECT tblEmployee4a.empFullName
                               FROM tblRecordOthEmpO AS tblRecordOthEmpO4a 
                                INNER JOIN tblEmployee AS tblEmployee4a ON tblRecordOthEmpO4a.empIdOth = tblEmployee4a.empId
                                WHERE (tblRecordOthEmpO4a.recId = tblRecord.recId) AND (tblRecordOthEmpO4a.recItem = '4')) AS othNameObs4,
                             (SELECT tblEmployee4b.empDowId
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO4b 
                                INNER JOIN tblEmployee AS tblEmployee4b ON tblRecordOthEmpO4b.empIdOth = tblEmployee4b.empId
                                WHERE (tblRecordOthEmpO4b.recId = tblRecord.recId) AND (tblRecordOthEmpO4b.recItem = '4')) AS othDowIdObs4,
                             (SELECT tblDepartment4c.departName
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO4c 
                                INNER JOIN tblEmployee AS tblEmployee4c ON tblRecordOthEmpO4c.empIdOth = tblEmployee4c.empId 
                                INNER JOIN tblDepartment AS tblDepartment4c ON tblEmployee4c.departId = tblDepartment4c.departId
                                WHERE (tblRecordOthEmpO4c.recId = tblRecord.recId) AND (tblRecordOthEmpO4c.recItem = '4')) AS othDepartObs4,
                             (SELECT tblEmployee5a.empFullName
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO5a 
                                INNER JOIN tblEmployee AS tblEmployee5a ON tblRecordOthEmpO5a.empIdOth = tblEmployee5a.empId
                                WHERE (tblRecordOthEmpO5a.recId = tblRecord.recId) AND (tblRecordOthEmpO5a.recItem = '5')) AS othNameObs5,
                             (SELECT tblEmployee5b.empDowId
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO5b 
                                INNER JOIN tblEmployee AS tblEmployee5b ON tblRecordOthEmpO5b.empIdOth = tblEmployee5b.empId
                                WHERE (tblRecordOthEmpO5b.recId = tblRecord.recId) AND (tblRecordOthEmpO5b.recItem = '5')) AS othDowIdObs5,
                             (SELECT tblDepartment5c.departName
                                FROM tblRecordOthEmpO AS tblRecordOthEmpO5c 
                                INNER JOIN tblEmployee AS tblEmployee5c ON tblRecordOthEmpO5c.empIdOth = tblEmployee5c.empId 
                                INNER JOIN tblDepartment AS tblDepartment5c ON tblEmployee5c.departId = tblDepartment5c.departId
                                WHERE (tblRecordOthEmpO5c.recId = tblRecord.recId) AND (tblRecordOthEmpO5c.recItem = '5')) AS othDepartObs5
                        FROM tblRecord 
                        INNER JOIN tblEmployee AS tblObserver1 ON tblRecord.empId = tblObserver1.empId 
                        INNER JOIN tblDepartment ON tblObserver1.departId = tblDepartment.departId 
                        WHERE (tblRecord.recActive = 'true') AND (tblRecord.recActMonth = '" & rcbSelMonth.SelectedValue & "') AND (tblRecord.recActYear ='" & rcbSelYear.SelectedValue & "') "

        If rcbDepartment.SelectedIndex > 0 Then
            sqlStr = sqlStr & "AND (tblRecord.departId = '" & rcbDepartment.SelectedValue & "') "
        End If

        exportOption()

        tbShowQuery.Text = sqlStr & "ORDER BY tblRecord.recActNo"

        RadGrid1.DataSource = GetDataTable(sqlStr & "ORDER BY tblRecord.recActNo")
        RadGrid1.Rebind()
    End Sub

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim hfRecId As HiddenField = dataItem.FindControl("hfRecId")
            Dim recId As Integer = hfRecId.Value

            Dim StrSelect As String = "SELECT tblRecordDetail.title, tblObsvCate.cateName, tblObsvCateSub.catesubName, tblObsvFailPoint.failpointName, tblRecordDetail.equipment, 
                                        tblRecordDetail.IsHRO, tblRecordDetail.hroChk1, tblRecordDetail.hroChk2, tblRecordDetail.hroChk3, tblRecordDetail.hroChk4, tblRecordDetail.hroChk5, 
                                        tblRecordDetail.secondEye, dbo.tblRecordDetail.recognition, tblRecordDetail.observType, tblRecordDetail.contractor, tblRecordDetail.description, 
                                        tblRecordDetail.proposeAction_A, tblRecordDetail.proposeRespPerson_A, tblRecordDetail.proposeStatus_A, 
                                        tblRecordDetail.proposeAction_B, tblRecordDetail.proposeRespPerson_B, tblRecordDetail.proposeStatus_B, 
                                        tblRecordDetail.proposeAction_C, tblRecordDetail.proposeRespPerson_C, tblRecordDetail.proposeStatus_C, 
                                        tblRecordDetail.observComplete, tblRecordDetail.pictureCount 
                                        FROM dbo.tblRecordDetail 
                                        INNER JOIN dbo.tblObsvCate ON dbo.tblRecordDetail.category = dbo.tblObsvCate.cateId 
                                        INNER JOIN dbo.tblObsvCateSub ON dbo.tblRecordDetail.categorySub = dbo.tblObsvCateSub.catesubId 
                                        INNER JOIN dbo.tblObsvFailPoint ON dbo.tblRecordDetail.failurePoint = dbo.tblObsvFailPoint.failpointId 
                                        WHERE recId = @recId AND observItem = @observItem"

            For i As Integer = 1 To 6
                Using conn As New SqlConnection(ConnStr)
                    Dim cTitleObs As TableCell = dataItem("titleObs" & i.ToString) : cTitleObs.Text = ""
                    Dim cCateObs As TableCell = dataItem("categoryObs" & i.ToString) : cCateObs.Text = ""
                    Dim cCateSubObs As TableCell = dataItem("categorySubObs" & i.ToString) : cCateSubObs.Text = ""
                    Dim cFailPointObs As TableCell = dataItem("FailPointObs" & i.ToString) : cFailPointObs.Text = ""
                    Dim cEquipmentObs As TableCell = dataItem("equipmentObs" & i.ToString) : cEquipmentObs.Text = ""

                    Dim cHROObs As TableCell = dataItem("HROObs" & i.ToString) : cHROObs.Text = ""
                    Dim c2ndEyeObs As TableCell = dataItem("2ndEyeObs" & i.ToString) : c2ndEyeObs.Text = ""
                    Dim cDescriptObs As TableCell = dataItem("descriptionObs" & i.ToString) : cDescriptObs.Text = ""

                    Dim cProposeActionAObs As TableCell = dataItem("proposeActionAObs" & i.ToString) : cProposeActionAObs.Text = ""
                    Dim cProposeActionBObs As TableCell = dataItem("proposeActionBObs" & i.ToString) : cProposeActionBObs.Text = ""
                    Dim cProposeActionCObs As TableCell = dataItem("proposeActionCObs" & i.ToString) : cProposeActionCObs.Text = ""

                    Dim cResponAObs As TableCell = dataItem("responAObs" & i.ToString) : cResponAObs.Text = ""
                    Dim cResponBObs As TableCell = dataItem("responBObs" & i.ToString) : cResponBObs.Text = ""
                    Dim cResponCObs As TableCell = dataItem("responCObs" & i.ToString) : cResponCObs.Text = ""

                    Dim cStatusAObs As TableCell = dataItem("statusAObs" & i.ToString) : cStatusAObs.Text = ""
                    Dim cStatusBObs As TableCell = dataItem("statusBObs" & i.ToString) : cStatusBObs.Text = ""
                    Dim cStatusCObs As TableCell = dataItem("statusCObs" & i.ToString) : cStatusCObs.Text = ""

                    Dim cObservTypeObs As TableCell = dataItem("observTypeObs" & i.ToString) : cObservTypeObs.Text = "0"
                    Dim cContractorObs As TableCell = dataItem("contractorObs" & i.ToString) : cContractorObs.Text = "0"
                    Dim cPicCountObs As TableCell = dataItem("picCountObs" & i.ToString) : cPicCountObs.Text = ""
                    Dim cPicUrl1Obs As TableCell = dataItem("picUrl1Obs" & i.ToString) : cPicUrl1Obs.Text = ""
                    Dim cPicUrl2Obs As TableCell = dataItem("picUrl2Obs" & i.ToString) : cPicUrl2Obs.Text = ""
                    Dim cPicUrl3Obs As TableCell = dataItem("picUrl3Obs" & i.ToString) : cPicUrl3Obs.Text = ""
                    Dim cPicUrl4Obs As TableCell = dataItem("picUrl4Obs" & i.ToString) : cPicUrl4Obs.Text = ""

                    conn.Open()
                    Dim command As New SqlCommand(StrSelect, conn)
                    command.Parameters.Add("@recId", SqlDbType.Int).Value = recId
                    command.Parameters.Add("@observItem", SqlDbType.Int).Value = i
                    Dim DataRead As SqlDataReader
                    DataRead = command.ExecuteReader()
                    If DataRead.HasRows Then
                        While DataRead.Read()
                            cTitleObs.Text = "'" & CStr(DataRead("title")) & " /" & recId
                            cCateObs.Text = CStr(DataRead("cateName"))
                            cCateSubObs.Text = CStr(DataRead("catesubName"))
                            cFailPointObs.Text = DataRead("failpointName")
                            If DataRead("equipment") IsNot DBNull.Value Then cEquipmentObs.Text = "'" & CStr(DataRead("equipment"))
                            cObservTypeObs.Text = CInt(DataRead("observType"))
                            cContractorObs.Text = CInt(DataRead("contractor"))

                            Dim HROStr As String = ""
                            If CBool(DataRead("IsHRO")) Then
                                If CBool(DataRead("hroChk1")) Then HROStr = HROStr & "Expect the Unexpected (คาดคะเนในสิ่งที่ไม่น่าจะเกิดขึ้น), "
                                If CBool(DataRead("hroChk2")) Then HROStr = HROStr & "Do Not Generalize (ไม่ทำสิ่งที่ไม่ปกติให้เป็นสิ่งปกติ), "
                                If CBool(DataRead("hroChk3")) Then HROStr = HROStr & "Identify Trend & Anticipate Impact (ระบุแนวโน้มและคาดการณ์ถึงผลที่จะกระทบ), "
                                If CBool(DataRead("hroChk4")) Then HROStr = HROStr & "Engage & Apply Expertise (ปรึกษาและให้ผู้เชี่ยวชาญร่วมแก้ปัญหา), "
                                If CBool(DataRead("hroChk5")) Then HROStr = HROStr & "Commit to Resilience (หาทางกลับมาสถานะเดิมอย่างรวดเร็ว), "
                                If HROStr.Length > 2 Then HROStr = HROStr.Substring(0, HROStr.Length - 2)
                            End If
                            cHROObs.Text = HROStr
                            c2ndEyeObs.Text = CStr(DataRead("secondEye"))
                            cDescriptObs.Text = "' " & DataRead("description") '.ToString.Replace(vbCr, " ").Replace(vbLf, " ")

                            Dim emp As New cEmployee
                            cProposeActionAObs.Text = "'" & CStr(DataRead("proposeAction_A"))
                            If rcbExpPerson.SelectedValue = 0 Then
                                cResponAObs.Text = emp.FindEmployeeName(CInt(DataRead("proposeRespPerson_A"))) & " " & emp.EmployeeSurname
                            ElseIf rcbExpPerson.SelectedValue = 1 Then
                                emp.FindEmployeeName(CInt(DataRead("proposeRespPerson_A")))
                                cResponAObs.Text = emp.DowId
                            End If
                            cStatusAObs.Text = getStatusDesc(CInt(DataRead("proposeStatus_A")))

                            If DataRead("proposeAction_B") IsNot DBNull.Value Then
                                cProposeActionBObs.Text = "'" & CStr(DataRead("proposeAction_B"))
                                If CInt(DataRead("proposeRespPerson_B")) <> 0 Then
                                    If rcbExpPerson.SelectedValue = 0 Then
                                        cResponBObs.Text = emp.FindEmployeeName(CInt(DataRead("proposeRespPerson_B"))) & " " & emp.EmployeeSurname
                                    ElseIf rcbExpPerson.SelectedValue = 1 Then
                                        emp.FindEmployeeName(CInt(DataRead("proposeRespPerson_B")))
                                        cResponBObs.Text = emp.DowId
                                    End If
                                End If
                                cStatusBObs.Text = getStatusDesc(CInt(DataRead("proposeStatus_B")))
                            End If
                            If DataRead("proposeAction_C") IsNot DBNull.Value Then
                                cProposeActionCObs.Text = "'" & CStr(DataRead("proposeAction_C"))
                                If CInt(DataRead("proposeRespPerson_C")) <> 0 Then
                                    If rcbExpPerson.SelectedValue = 0 Then
                                        cResponCObs.Text = emp.FindEmployeeName(CInt(DataRead("proposeRespPerson_C"))) & " " & emp.EmployeeSurname
                                    ElseIf rcbExpPerson.SelectedValue = 1 Then
                                        emp.FindEmployeeName(CInt(DataRead("proposeRespPerson_C")))
                                        cResponCObs.Text = emp.DowId
                                    End If
                                End If
                                cStatusCObs.Text = getStatusDesc(CInt(DataRead("proposeStatus_C")))
                            End If

                            Dim pictureCount As Integer = CInt(DataRead("pictureCount"))
                            If pictureCount > 0 And chkImgUrl.Checked Then
                                cPicCountObs.Text = pictureCount.ToString

                                'get ImageURL
                                Dim imgSelStr As String = "SELECT picUrl FROM tblRecordPictureO WHERE recId = @recId AND observeItem = @observeItem ORDER BY picItem"
                                Using connImgUrl As New SqlConnection(ConnStr)
                                    connImgUrl.Open()
                                    Dim commandImgUrl As New SqlCommand(imgSelStr, connImgUrl)
                                    commandImgUrl.Parameters.Add("@recId", SqlDbType.Int).Value = recId
                                    commandImgUrl.Parameters.Add("@observeItem", SqlDbType.Int).Value = i - 1
                                    Dim dr As SqlDataReader
                                    dr = commandImgUrl.ExecuteReader()
                                    Dim picUrl() As String = {"", "", "", ""}
                                    For picItem As Integer = 0 To pictureCount - 1
                                        dr.Read()
                                        picUrl(picItem) = dr("picUrl")
                                    Next

                                    cPicUrl1Obs.Text = picUrl(0)
                                    cPicUrl2Obs.Text = picUrl(1)
                                    cPicUrl3Obs.Text = picUrl(2)
                                    cPicUrl4Obs.Text = picUrl(3)
                                End Using
                            End If
                        End While
                    End If
                End Using
            Next
        End If
    End Sub

    Protected Sub btExportExcel_Click(sender As Object, e As EventArgs) Handles btExportExcel.Click
        If RadGrid1.Items.Count > 0 Then
            RadGrid1.ExportSettings.Excel.Format = DirectCast([Enum].Parse(GetType(GridExcelExportFormat), "Xlsx"), GridExcelExportFormat)
            'RadGrid1.ExportSettings.Excel.Format = DirectCast([Enum].Parse(GetType(GridExcelExportFormat), "ExcelML"), GridExcelExportFormat)
            RadGrid1.ExportSettings.IgnorePaging = True
            RadGrid1.ExportSettings.ExportOnlyData = True
            RadGrid1.ExportSettings.OpenInNewWindow = True

            Dim expTypeName As String
            If rcbDepartment.SelectedIndex = 0 Then expTypeName = "All" Else expTypeName = rcbDepartment.Text

            RadGrid1.ExportSettings.FileName = "EZPath_" & expTypeName & "_" & Now.Year.ToString.Substring(2, 2) & Now.Month.ToString("00") & Now.Day.ToString("00") & "." & Now.Hour.ToString("00") & Now.Minute.ToString("00") & Now.Millisecond.ToString("000")
            RadGrid1.MasterTableView.ExportToExcel()
        End If
    End Sub

    Private Function getStatusDesc(ByVal statusCode As Integer) As String
        Dim StatusDesc As String = ""
        If statusCode = 1001 Then
            StatusDesc = _1001Desc
        ElseIf statusCode = 1002 Then
            StatusDesc = _1002Desc
        ElseIf statusCode = 1003 Then
            StatusDesc = _1003Desc
        End If
        Return StatusDesc
    End Function

    Private Sub exportOption()
        Select Case rcbNoOthObserver.SelectedValue
            Case 3
                RadGrid1.MasterTableView.GetColumn("othNameObs4").Display = False
                RadGrid1.MasterTableView.GetColumn("othDowIdObs4").Display = False
                RadGrid1.MasterTableView.GetColumn("othDepartObs4").Display = False
                RadGrid1.MasterTableView.GetColumn("othNameObs5").Display = False
                RadGrid1.MasterTableView.GetColumn("othDowIdObs5").Display = False
                RadGrid1.MasterTableView.GetColumn("othDepartObs5").Display = False
            Case 5
                RadGrid1.MasterTableView.GetColumn("othNameObs4").Display = True
                RadGrid1.MasterTableView.GetColumn("othDowIdObs4").Display = True
                RadGrid1.MasterTableView.GetColumn("othDepartObs4").Display = True
                RadGrid1.MasterTableView.GetColumn("othNameObs5").Display = True
                RadGrid1.MasterTableView.GetColumn("othDowIdObs5").Display = True
                RadGrid1.MasterTableView.GetColumn("othDepartObs5").Display = True
        End Select

        Select Case rcbNoPropose.SelectedValue
            Case 1
                RadGrid1.MasterTableView.GetColumn("proposeActionBObs1").Display = False
                RadGrid1.MasterTableView.GetColumn("responBObs1").Display = False
                RadGrid1.MasterTableView.GetColumn("statusBObs1").Display = False
                RadGrid1.MasterTableView.GetColumn("proposeActionCObs1").Display = False
                RadGrid1.MasterTableView.GetColumn("responCObs1").Display = False
                RadGrid1.MasterTableView.GetColumn("statusCObs1").Display = False

                RadGrid1.MasterTableView.GetColumn("proposeActionBObs2").Display = False
                RadGrid1.MasterTableView.GetColumn("responBObs2").Display = False
                RadGrid1.MasterTableView.GetColumn("statusBObs2").Display = False
                RadGrid1.MasterTableView.GetColumn("proposeActionCObs2").Display = False
                RadGrid1.MasterTableView.GetColumn("responCObs2").Display = False
                RadGrid1.MasterTableView.GetColumn("statusCObs2").Display = False

                RadGrid1.MasterTableView.GetColumn("proposeActionBObs3").Display = False
                RadGrid1.MasterTableView.GetColumn("responBObs3").Display = False
                RadGrid1.MasterTableView.GetColumn("statusBObs3").Display = False
                RadGrid1.MasterTableView.GetColumn("proposeActionCObs3").Display = False
                RadGrid1.MasterTableView.GetColumn("responCObs3").Display = False
                RadGrid1.MasterTableView.GetColumn("statusCObs3").Display = False

                RadGrid1.MasterTableView.GetColumn("proposeActionBObs4").Display = False
                RadGrid1.MasterTableView.GetColumn("responBObs4").Display = False
                RadGrid1.MasterTableView.GetColumn("statusBObs4").Display = False
                RadGrid1.MasterTableView.GetColumn("proposeActionCObs4").Display = False
                RadGrid1.MasterTableView.GetColumn("responCObs4").Display = False
                RadGrid1.MasterTableView.GetColumn("statusCObs4").Display = False

                RadGrid1.MasterTableView.GetColumn("proposeActionBObs5").Display = False
                RadGrid1.MasterTableView.GetColumn("responBObs5").Display = False
                RadGrid1.MasterTableView.GetColumn("statusBObs5").Display = False
                RadGrid1.MasterTableView.GetColumn("proposeActionCObs5").Display = False
                RadGrid1.MasterTableView.GetColumn("responCObs5").Display = False
                RadGrid1.MasterTableView.GetColumn("statusCObs5").Display = False

                RadGrid1.MasterTableView.GetColumn("proposeActionBObs6").Display = False
                RadGrid1.MasterTableView.GetColumn("responBObs6").Display = False
                RadGrid1.MasterTableView.GetColumn("statusBObs6").Display = False
                RadGrid1.MasterTableView.GetColumn("proposeActionCObs6").Display = False
                RadGrid1.MasterTableView.GetColumn("responCObs6").Display = False
                RadGrid1.MasterTableView.GetColumn("statusCObs6").Display = False
            Case 3
                RadGrid1.MasterTableView.GetColumn("proposeActionBObs1").Display = True
                RadGrid1.MasterTableView.GetColumn("responBObs1").Display = True
                RadGrid1.MasterTableView.GetColumn("statusBObs1").Display = True
                RadGrid1.MasterTableView.GetColumn("proposeActionCObs1").Display = True
                RadGrid1.MasterTableView.GetColumn("responCObs1").Display = True
                RadGrid1.MasterTableView.GetColumn("statusCObs1").Display = True

                RadGrid1.MasterTableView.GetColumn("proposeActionBObs2").Display = True
                RadGrid1.MasterTableView.GetColumn("responBObs2").Display = True
                RadGrid1.MasterTableView.GetColumn("statusBObs2").Display = True
                RadGrid1.MasterTableView.GetColumn("proposeActionCObs2").Display = True
                RadGrid1.MasterTableView.GetColumn("responCObs2").Display = True
                RadGrid1.MasterTableView.GetColumn("statusCObs2").Display = True

                RadGrid1.MasterTableView.GetColumn("proposeActionBObs3").Display = True
                RadGrid1.MasterTableView.GetColumn("responBObs3").Display = True
                RadGrid1.MasterTableView.GetColumn("statusBObs3").Display = True
                RadGrid1.MasterTableView.GetColumn("proposeActionCObs3").Display = True
                RadGrid1.MasterTableView.GetColumn("responCObs3").Display = True
                RadGrid1.MasterTableView.GetColumn("statusCObs3").Display = True

                RadGrid1.MasterTableView.GetColumn("proposeActionBObs4").Display = True
                RadGrid1.MasterTableView.GetColumn("responBObs4").Display = True
                RadGrid1.MasterTableView.GetColumn("statusBObs4").Display = True
                RadGrid1.MasterTableView.GetColumn("proposeActionCObs4").Display = True
                RadGrid1.MasterTableView.GetColumn("responCObs4").Display = True
                RadGrid1.MasterTableView.GetColumn("statusCObs4").Display = True

                RadGrid1.MasterTableView.GetColumn("proposeActionBObs5").Display = True
                RadGrid1.MasterTableView.GetColumn("responBObs5").Display = True
                RadGrid1.MasterTableView.GetColumn("statusBObs5").Display = True
                RadGrid1.MasterTableView.GetColumn("proposeActionCObs5").Display = True
                RadGrid1.MasterTableView.GetColumn("responCObs5").Display = True
                RadGrid1.MasterTableView.GetColumn("statusCObs5").Display = True

                RadGrid1.MasterTableView.GetColumn("proposeActionBObs6").Display = True
                RadGrid1.MasterTableView.GetColumn("responBObs6").Display = True
                RadGrid1.MasterTableView.GetColumn("statusBObs6").Display = True
                RadGrid1.MasterTableView.GetColumn("proposeActionCObs6").Display = True
                RadGrid1.MasterTableView.GetColumn("responCObs6").Display = True
                RadGrid1.MasterTableView.GetColumn("statusCObs6").Display = True
        End Select

        If chkImgUrl.Checked Then
            RadGrid1.MasterTableView.GetColumn("picCountObs1").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl1Obs1").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl2Obs1").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl3Obs1").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl4Obs1").Display = True

            RadGrid1.MasterTableView.GetColumn("picCountObs2").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl1Obs2").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl2Obs2").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl3Obs2").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl4Obs2").Display = True

            RadGrid1.MasterTableView.GetColumn("picCountObs3").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl1Obs3").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl2Obs3").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl3Obs3").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl4Obs3").Display = True

            RadGrid1.MasterTableView.GetColumn("picCountObs4").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl1Obs4").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl2Obs4").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl3Obs4").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl4Obs4").Display = True

            RadGrid1.MasterTableView.GetColumn("picCountObs5").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl1Obs5").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl2Obs5").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl3Obs5").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl4Obs5").Display = True

            RadGrid1.MasterTableView.GetColumn("picCountObs6").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl1Obs6").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl2Obs6").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl3Obs6").Display = True
            RadGrid1.MasterTableView.GetColumn("picUrl4Obs6").Display = True
        Else
            RadGrid1.MasterTableView.GetColumn("picCountObs1").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl1Obs1").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl2Obs1").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl3Obs1").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl4Obs1").Display = False

            RadGrid1.MasterTableView.GetColumn("picCountObs2").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl1Obs2").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl2Obs2").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl3Obs2").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl4Obs2").Display = False

            RadGrid1.MasterTableView.GetColumn("picCountObs3").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl1Obs3").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl2Obs3").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl3Obs3").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl4Obs3").Display = False

            RadGrid1.MasterTableView.GetColumn("picCountObs4").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl1Obs4").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl2Obs4").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl3Obs4").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl4Obs4").Display = False

            RadGrid1.MasterTableView.GetColumn("picCountObs5").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl1Obs5").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl2Obs5").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl3Obs5").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl4Obs5").Display = False

            RadGrid1.MasterTableView.GetColumn("picCountObs6").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl1Obs6").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl2Obs6").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl3Obs6").Display = False
            RadGrid1.MasterTableView.GetColumn("picUrl4Obs6").Display = False
        End If

        If chkContractor.Checked Then
            RadGrid1.MasterTableView.GetColumn("observTypeObs1").Display = True
            RadGrid1.MasterTableView.GetColumn("contractorObs1").Display = True
            RadGrid1.MasterTableView.GetColumn("observTypeObs2").Display = True
            RadGrid1.MasterTableView.GetColumn("contractorObs2").Display = True
            RadGrid1.MasterTableView.GetColumn("observTypeObs3").Display = True
            RadGrid1.MasterTableView.GetColumn("contractorObs3").Display = True
            RadGrid1.MasterTableView.GetColumn("observTypeObs4").Display = True
            RadGrid1.MasterTableView.GetColumn("contractorObs4").Display = True
            RadGrid1.MasterTableView.GetColumn("observTypeObs5").Display = True
            RadGrid1.MasterTableView.GetColumn("contractorObs5").Display = True
            RadGrid1.MasterTableView.GetColumn("observTypeObs6").Display = True
            RadGrid1.MasterTableView.GetColumn("contractorObs6").Display = True
        Else
            RadGrid1.MasterTableView.GetColumn("observTypeObs1").Display = False
            RadGrid1.MasterTableView.GetColumn("contractorObs1").Display = False
            RadGrid1.MasterTableView.GetColumn("observTypeObs2").Display = False
            RadGrid1.MasterTableView.GetColumn("contractorObs2").Display = False
            RadGrid1.MasterTableView.GetColumn("observTypeObs3").Display = False
            RadGrid1.MasterTableView.GetColumn("contractorObs3").Display = False
            RadGrid1.MasterTableView.GetColumn("observTypeObs4").Display = False
            RadGrid1.MasterTableView.GetColumn("contractorObs4").Display = False
            RadGrid1.MasterTableView.GetColumn("observTypeObs5").Display = False
            RadGrid1.MasterTableView.GetColumn("contractorObs5").Display = False
            RadGrid1.MasterTableView.GetColumn("observTypeObs6").Display = False
            RadGrid1.MasterTableView.GetColumn("contractorObs6").Display = False
        End If
    End Sub

    Private Sub ClearGrid()
        Dim myDataTable As New DataTable()
        RadGrid1.DataSource = myDataTable
        RadGrid1.Rebind()
    End Sub

    Protected Sub rcbNoOthObserver_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbNoOthObserver.SelectedIndexChanged
        ClearGrid()
    End Sub
    Protected Sub rcbNoPropose_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbNoPropose.SelectedIndexChanged
        ClearGrid()
    End Sub
    Protected Sub rcbExpPerson_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbExpPerson.SelectedIndexChanged
        ClearGrid()
    End Sub
    Protected Sub chkImgUrl_CheckedChanged(sender As Object, e As EventArgs) Handles chkImgUrl.CheckedChanged
        ClearGrid()
    End Sub
    Protected Sub chkContractor_CheckedChanged(sender As Object, e As EventArgs) Handles chkContractor.CheckedChanged
        ClearGrid()
    End Sub

    '--------------- SUPPORT TOOLS '---------------
    Protected Sub btSupportTools_Click(sender As Object, e As EventArgs) Handles btSupportTools.Click
        pnSupportTools.Visible = not pnSupportTools.Visible
    End Sub
End Class