Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin

Imports Telerik.Web.UI

Public Class followupList
    Inherits Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Dim SqlStrOriginal As String = "Select tblRecordDetail.*, tblRecord.recActNo, tblRecord.recActDate, tblRecord.recActTime, tblRecord.durationValue, tblObsvCate.cateName, tblObsvCateSub.catesubName, 
                                    tblObsvFailPoint.failpointName, tblContractor.contractorName, tblStatusA.statusDesc AS StatusA, StatusB.statusDesc AS StatusB, StatusC.statusDesc AS StatusC, 
                                    tblDepartment.departName, tblEmployee.empDisplay, tblEmployee.empId AS ownerId
                                    FROM tblObsvCate INNER JOIN tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId ON tblObsvCate.cateId = tblRecordDetail.category 
                                    INNER JOIN tblDepartment ON tblRecord.departId = tblDepartment.departId 
                                    INNER JOIN tblStatus As tblStatusA ON tblRecordDetail.proposeStatus_A = tblStatusA.statusId 
                                    INNER JOIN tblEmployee ON dbo.tblRecord.empId = dbo.tblEmployee.empId 
                                    LEFT OUTER JOIN tblStatus As StatusB On tblRecordDetail.proposeStatus_B = StatusB.statusId 
                                    LEFT OUTER JOIN tblStatus As StatusC On tblRecordDetail.proposeStatus_C = StatusC.statusId 
                                    LEFT OUTER JOIN tblContractor On tblRecordDetail.contractor = tblContractor.contractorId 
                                    LEFT OUTER JOIN tblObsvCateSub On tblRecordDetail.categorySub = tblObsvCateSub.catesubId 
                                    LEFT OUTER JOIN tblObsvFailPoint On tblRecordDetail.failurePoint = tblObsvFailPoint.failpointId "

    Dim SqlStrOriginalSort As String = "ORDER BY tblRecord.timestamp DESC, tblRecordDetail.observItem"

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

            rcbSelMonthFrm.SelectedIndex = rcbSelMonthFrm.FindItemIndexByValue(Now.Month)
            rcbSelYearFrm.SelectedIndex = rcbSelYearFrm.FindItemIndexByValue(Now.Year)
            rcbSelMonthTo.SelectedIndex = rcbSelMonthTo.FindItemIndexByValue(Now.Month)
            rcbSelYearTo.SelectedIndex = rcbSelYearTo.FindItemIndexByValue(Now.Year)
        End If

        If User.IsInRole("SYSTEM ADMIN") Or User.IsInRole("FACILITY ADMIN") Then
            'If User.IsInRole("FACILITY ADMIN") Then SettingItem.NavigateUrl = "~/em/setUserbyDepart.aspx?sel=setuserd"     'ยกเลิก @6/3/2017 อนุญาติให้ FACILITY ADMIN ได้สิทธิเหมือน SYSTEM ADMIN
            '@22/4/2017 อนุญาติให้ FACILITY ADMIN ใช้ SETTING ได้ แต่ใช้ได้เฉพาะ setUser
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
            If e.Item.Text = "SETTING" Or e.Item.Text = "REPORT" Then
                e.Item.Selected = False
                RadPanelBar1.Items.FindItemByText("FOLLOW UP").Selected = True
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

    Private Sub rgRecordList_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rgRecordList.NeedDataSource
        If Not e.IsFromDetailTable Then
            Dim sqlStr As String = ""
            If pnAdvanceSearch.Visible Then
                Select Case RadTabStrip1.SelectedIndex
                    Case 0
                        sqlStr = SearchObservedStr(tbObserved.Text.Trim)
                    Case 1
                        sqlStr = SearchResponStr(tbRespon.Text.Trim)
                    Case 2
                        ' if check Month to Month else
                        Dim DatetimeConv As New cDatetimeConv
                        Dim usDateFrom As Date
                        Dim usDateTo As Date
                        If chkMonthToMonth.Checked Then
                            usDateFrom = rcbSelMonthFrm.SelectedValue & "/1/" & rcbSelYearFrm.SelectedValue
                            If rcbSelMonthTo.SelectedValue <> 12 Then
                                usDateTo = (CInt(rcbSelMonthTo.SelectedValue) + 1).ToString & "/1/" & rcbSelYearTo.SelectedValue
                            Else
                                usDateTo = "1/1/" & (CInt(rcbSelYearTo.SelectedValue) + 1)
                            End If
                            usDateTo = DateAdd(DateInterval.Day, -1, usDateTo)
                        Else
                            If rdpDocDateFrom.SelectedDate IsNot Nothing And rdpDocDateTo.SelectedDate IsNot Nothing Then
                                usDateFrom = rdpDocDateFrom.SelectedDate
                                usDateTo = rdpDocDateTo.SelectedDate
                            End If
                        End If
                        sqlStr = SearchDateRange(DatetimeConv.DateToStringEnUS(usDateFrom), DatetimeConv.DateToStringEnUS(usDateTo))
                    Case 3
                        sqlStr = SearchCombineStr(tbObservedCombine.Text.Trim, tbResponCombine.Text.Trim)
                End Select
            Else
                sqlStr = SearchAndFilterStr(tbSearchKeyword.Text.Trim())
            End If

            rgRecordList.DataSource = GetDataTable(sqlStr)
        End If
    End Sub

    Private Function SearchAndFilterStr(ByVal s As String) As String
        Dim SqlStr As String = SqlStrOriginal
        If Page.IsPostBack Then
            If s <> "" Then
                SqlStr = SqlStr & "WHERE recActNo LIKE '%" & s & "%' OR title LIKE '%" & s & "%' "
                If rcbDepartmentView.SelectedIndex = 0 And rcbStatusView.SelectedIndex = 0 Then
                    SqlStr = SqlStr & SqlStrOriginalSort
                ElseIf rcbDepartmentView.SelectedIndex <> 0 And rcbStatusView.SelectedIndex = 0 Then
                    SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' " & SqlStrOriginalSort
                ElseIf rcbDepartmentView.SelectedIndex = 0 And rcbStatusView.SelectedIndex <> 0 Then
                    SqlStr = SqlStr & "AND observComplete = '" & rcbStatusView.SelectedValue & "' " & SqlStrOriginalSort
                ElseIf rcbDepartmentView.SelectedIndex <> 0 And rcbStatusView.SelectedIndex <> 0 Then
                    SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' AND observComplete = '" & rcbStatusView.SelectedValue & "' " & SqlStrOriginalSort
                End If
            Else
                If rcbDepartmentView.SelectedIndex = 0 And rcbStatusView.SelectedIndex = 0 Then
                    SqlStr = SqlStr & SqlStrOriginalSort
                ElseIf rcbDepartmentView.SelectedIndex <> 0 And rcbStatusView.SelectedIndex = 0 Then
                    SqlStr = SqlStr & "WHERE tblDepartment.departName = '" & rcbDepartmentView.Text & "' " & SqlStrOriginalSort
                ElseIf rcbDepartmentView.SelectedIndex = 0 And rcbStatusView.SelectedIndex <> 0 Then
                    SqlStr = SqlStr & "WHERE observComplete = '" & rcbStatusView.SelectedValue & "' " & SqlStrOriginalSort
                ElseIf rcbDepartmentView.SelectedIndex <> 0 And rcbStatusView.SelectedIndex <> 0 Then
                    SqlStr = SqlStr & "WHERE tblDepartment.departName = '" & rcbDepartmentView.Text & "' AND observComplete = '" & rcbStatusView.SelectedValue & "' " & SqlStrOriginalSort
                End If
            End If
        Else
            SqlStr = SqlStr & SqlStrOriginalSort
        End If

        Return SqlStr
    End Function

    Protected Sub btSearchBox_Click(sender As Object, e As EventArgs) Handles btSearchBox.Click
        If tbSearchKeyword.Text <> "" Then
            imgbClearKeyword.Visible = True
        Else
            imgbClearKeyword.Visible = False
        End If

        rgRecordList.Rebind()
    End Sub
    Protected Sub imgbClearKeyword_Click(sender As Object, e As ImageClickEventArgs) Handles imgbClearKeyword.Click
        tbSearchKeyword.Text = ""
        imgbClearKeyword.Visible = False
        rgRecordList.Rebind()
    End Sub

    Private Sub CloseAdvanceSearch()
        'toggle
        pnNormalSearch.Visible = Not pnNormalSearch.Visible
        pnAdvanceSearchMenu.Visible = Not pnAdvanceSearchMenu.Visible
        pnAdvanceSearch.Visible = Not pnAdvanceSearch.Visible

        'clear textbox normal search
        tbSearchKeyword.Text = ""
        imgbClearKeyword.Visible = False
        rcbDepartmentView.SelectedIndex = 0
        rcbStatusView.SelectedIndex = 0

        'clear textbox adv. search
        tbObserved.Text = ""
        lbInfoObserved.Text = ""
        tbRespon.Text = ""
        lbInfoRespon.Text = ""
        rdpDocDateFrom.Clear()
        rdpDocDateTo.Clear()
        lbInfoFromTo.Text = ""
        tbObservedCombine.Text = ""
        tbResponCombine.Text = ""
        rdpFromCombine.Clear()
        rdpToCombine.Clear()

        rgRecordList.DataSource = GetDataTable(SqlStrOriginal & SqlStrOriginalSort)
        rgRecordList.Rebind()
    End Sub

    Protected Sub btAdvanceSeach_Click(sender As Object, e As EventArgs) Handles btAdvanceSeach.Click
        CloseAdvanceSearch()
    End Sub
    Protected Sub CloseDepartment_Click(sender As Object, e As ImageClickEventArgs) Handles CloseObserve.Click, CloseRespon.Click, CloseFromTo.Click, CloseCombine.Click
        CloseAdvanceSearch()
    End Sub

    Protected Sub btSearchObserved_Click(sender As Object, e As EventArgs) Handles btSearchObserved.Click
        If tbObserved.Text <> "" Then
            lbInfoObserved.Text = ""
            Dim sqlStr As String = SearchObservedStr(tbObserved.Text.Trim)
            rgRecordList.DataSource = GetDataTable(sqlStr)
            rgRecordList.Rebind()
        Else
            lbInfoObserved.Text = "Search keyword is empty."
        End If
    End Sub
    Protected Sub btClearObserved_Click(sender As Object, e As EventArgs) Handles btClearObserved.Click
        tbObserved.Text = ""
        lbInfoObserved.Text = ""
        Dim sqlStr As String = SqlStrOriginal & SqlStrOriginalSort
        rgRecordList.DataSource = GetDataTable(sqlStr)
        rgRecordList.Rebind()
    End Sub

    Protected Sub btSearchRespon_Click(sender As Object, e As EventArgs) Handles btSearchRespon.Click
        If tbRespon.Text <> "" Then
            lbInfoRespon.Text = ""
            Dim sqlStr As String = SearchResponStr(tbRespon.Text.Trim)
            rgRecordList.DataSource = GetDataTable(sqlStr)
            rgRecordList.Rebind()
        Else
            lbInfoRespon.Text = "Search keyword is empty."
        End If
    End Sub
    Protected Sub btCloseRespon_Click(sender As Object, e As EventArgs) Handles btCloseRespon.Click
        tbRespon.Text = ""
        lbInfoRespon.Text = ""
        Dim sqlStr As String = SqlStrOriginal & SqlStrOriginalSort
        rgRecordList.DataSource = GetDataTable(sqlStr)
        rgRecordList.Rebind()
    End Sub

    Protected Sub btSearchFromTo_Click(sender As Object, e As EventArgs) Handles btSearchFromTo.Click
        Dim UsaCulture As New CultureInfo("en-US")
        If chkMonthToMonth.Checked Then
            lbInfoFromTo.Text = ""
            Dim sDateFrom As String = rcbSelYearFrm.SelectedValue & "-" & rcbSelMonthFrm.SelectedValue & "-" & "1"
            Dim sDateTo As String = rcbSelYearTo.SelectedValue & "-" & rcbSelMonthTo.SelectedValue & "-" & "1"
            Dim DateTo As Date = Date.Parse(String.Format("{0:yyyy-MM-dd}", sDateTo)).AddMonths(1).AddDays(-1)
            sDateTo = rcbSelYearTo.SelectedValue & "-" & DateTo.Month & "-" & DateTo.Day

            Dim sqlStr As String = SearchDateRange(sDateFrom, sDateTo)
            rgRecordList.DataSource = GetDataTable(sqlStr)
            rgRecordList.Rebind()
        Else
            If rdpDocDateFrom.SelectedDate IsNot Nothing And rdpDocDateTo.SelectedDate IsNot Nothing Then
                lbInfoFromTo.Text = ""
                Dim DateFrom As Date = rdpDocDateFrom.SelectedDate
                Dim DateTo As Date = rdpDocDateTo.SelectedDate
                Dim DatetimeConv As New cDatetimeConv
                Dim sqlStr As String = SearchDateRange(DatetimeConv.DateToStringEnUS(DateFrom), DatetimeConv.DateToStringEnUS(DateTo))
                rgRecordList.DataSource = GetDataTable(sqlStr)
                rgRecordList.Rebind()
            Else
                lbInfoFromTo.Text = "Date is empty."
            End If
        End If
    End Sub

    Protected Sub btSearchCombine_Click(sender As Object, e As EventArgs) Handles btSearchCombine.Click
        Dim sqlStr As String = SearchCombineStr(tbObservedCombine.Text.Trim, tbResponCombine.Text.Trim)
        rgRecordList.DataSource = GetDataTable(sqlStr)
        rgRecordList.Rebind()
    End Sub
    Protected Sub rcbCategoryCB_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCategoryCB.SelectedIndexChanged
        rcbFailPointCB.DataBind()
    End Sub

    Private Sub chkMonthToMonth_CheckedChanged(sender As Object, e As EventArgs) Handles chkMonthToMonth.CheckedChanged
        pnMonthToMonth.Visible = chkMonthToMonth.Checked
        pnDateRange.Visible = Not chkMonthToMonth.Checked
    End Sub

    Private Function SearchObservedStr(ByVal s As String) As String
        '-- Observer1 only
        'Dim SqlStr As String = "SELECT tblRecordDetail.*, tblRecord.recActNo, tblRecord.recActDate, tblRecord.recActTime, tblRecord.durationValue, tblObsvCate.cateName, tblObsvCateSub.catesubName, tblObsvFailPoint.failpointName, 
        '                            tblContractor.contractorName, tblStatusA.statusDesc AS StatusA, StatusB.statusDesc AS StatusB, StatusC.statusDesc AS StatusC, tblDepartment.departName, tblObserver1.empFullName, tblObserver1.empId AS ownerId
        '                            FROM tblObsvCate INNER JOIN tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId ON tblObsvCate.cateId = tblRecordDetail.category  
        '                            INNER JOIN tblDepartment On tblRecord.departId = tblDepartment.departId 
        '                            INNER JOIN tblStatus As tblStatusA On tblRecordDetail.proposeStatus_A = tblStatusA.statusId 
        '                            INNER JOIN tblEmployee As tblObserver1 ON tblRecord.empId = tblObserver1.empId 
        '                            LEFT OUTER JOIN tblStatus As StatusB On tblRecordDetail.proposeStatus_B = StatusB.statusId 
        '                            LEFT OUTER JOIN tblStatus As StatusC On tblRecordDetail.proposeStatus_C = StatusC.statusId 
        '                            LEFT OUTER JOIN tblContractor On tblRecordDetail.contractor = tblContractor.contractorId 
        '                            LEFT OUTER JOIN tblObsvCateSub On tblRecordDetail.categorySub = tblObsvCateSub.catesubId 
        '                            LEFT OUTER JOIN tblObsvFailPoint On tblRecordDetail.failurePoint = tblObsvFailPoint.failpointId "

        Dim SqlStr As String = "SELECT tblRecordDetail.*, tblRecord.recActNo, tblRecord.recActDate, tblRecord.recActTime, tblRecord.durationValue, tblObsvCate.cateName, tblObsvCateSub.catesubName, tblObsvFailPoint.failpointName, 
                                tblContractor.contractorName, tblStatusA.statusDesc AS StatusA, StatusB.statusDesc AS StatusB, StatusC.statusDesc AS StatusC, tblDepartment.departName, tblObserver1.empFullName, tblObserver1.empId AS ownerId
                                FROM tblEmployee AS tblObserverOth 
                                RIGHT OUTER JOIN tblRecordOthEmpO ON tblObserverOth.empId = tblRecordOthEmpO.empIdOth 
                                RIGHT OUTER JOIN tblObsvCate 
                                INNER JOIN tblRecordDetail ON tblObsvCate.cateId = tblRecordDetail.category 
                                INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId  
                                INNER JOIN tblDepartment ON tblRecord.departId = tblDepartment.departId 
                                INNER JOIN tblStatus AS tblStatusA ON tblRecordDetail.proposeStatus_A = tblStatusA.statusId 
                                INNER JOIN tblEmployee AS tblObserver1 ON tblRecord.empId = tblObserver1.empId ON tblRecordOthEmpO.recId = dbo.tblRecord.recId 
                                LEFT OUTER JOIN tblStatus AS StatusB ON tblRecordDetail.proposeStatus_B = StatusB.statusId 
                                LEFT OUTER JOIN tblStatus AS StatusC ON tblRecordDetail.proposeStatus_C = StatusC.statusId 
                                LEFT OUTER JOIN tblContractor ON tblRecordDetail.contractor = tblContractor.contractorId 
                                LEFT OUTER JOIN tblObsvCateSub ON tblRecordDetail.categorySub = tblObsvCateSub.catesubId 
                                LEFT OUTER JOIN tblObsvFailPoint ON tblRecordDetail.failurePoint = tblObsvFailPoint.failpointId "

        Dim idx1 As Integer = s.IndexOf(" ")
        Dim idx2 As Integer = s.IndexOf("  ")
        If idx2 < 0 Then If idx1 >= 0 Then s = s.Replace(" ", "  ")
        SqlStr = SqlStr & "WHERE (tblObserver1.empFullName LIKE '%" & s & "%' " & " OR tblObserverOth.empFullName LIKE '%" & s & "%') "

        If rcbDepartmentView.SelectedIndex = 0 And rcbStatusView.SelectedIndex = 0 Then

        ElseIf rcbDepartmentView.SelectedIndex <> 0 And rcbStatusView.SelectedIndex = 0 Then
            SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' "
        ElseIf rcbDepartmentView.SelectedIndex = 0 And rcbStatusView.SelectedIndex <> 0 Then
            SqlStr = SqlStr & "AND tblRecordDetail.observComplete = '" & rcbStatusView.SelectedValue & "' "
        ElseIf rcbDepartmentView.SelectedIndex <> 0 And rcbStatusView.SelectedIndex <> 0 Then
            SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' AND tblRecordDetail.observComplete = '" & rcbStatusView.SelectedValue & "' "
        End If

        SqlStr = SqlStr & "GROUP BY tblRecordDetail.detailId, tblRecordDetail.recId, tblRecordDetail.observItem, tblRecordDetail.title, tblRecordDetail.category, 
                         tblRecordDetail.categorySub, tblRecordDetail.failurePoint, tblRecordDetail.equipment, tblRecordDetail.IsHRO, tblRecordDetail.hroChk1, 
                         tblRecordDetail.hroChk2, tblRecordDetail.hroChk3, tblRecordDetail.hroChk4, tblRecordDetail.hroChk5, tblRecordDetail.secondEye, 
                         tblRecordDetail.recognition, tblRecordDetail.observType, tblRecordDetail.contractor, tblRecordDetail.pictureCount, tblRecordDetail.description, 
                         tblRecordDetail.proposeEnable_A, tblRecordDetail.proposeDesc_A, tblRecordDetail.proposeRespPerson_A, tblRecordDetail.proposeAction_A, 
                         tblRecordDetail.proposeStatus_A, tblRecordDetail.proposeComplete_A, tblRecordDetail.whoComplete_A, tblRecordDetail.proposeEnable_B, 
                         tblRecordDetail.proposeDesc_B, tblRecordDetail.proposeRespPerson_B, tblRecordDetail.proposeAction_B, tblRecordDetail.proposeStatus_B, 
                         tblRecordDetail.proposeComplete_B, tblRecordDetail.whoComplete_B, tblRecordDetail.proposeEnable_C, tblRecordDetail.proposeDesc_C, 
                         tblRecordDetail.proposeRespPerson_C, tblRecordDetail.proposeAction_C, tblRecordDetail.proposeStatus_C, tblRecordDetail.proposeComplete_C, 
                         tblRecordDetail.whoComplete_C, tblRecordDetail.observComplete, tblRecord.timestamp, tblRecord.recActNo, tblRecord.recActDate, tblRecord.recActTime, 
                         tblRecord.durationValue, tblObsvCate.cateName, tblObsvCateSub.catesubName, tblObsvFailPoint.failpointName, tblContractor.contractorName, 
                         tblStatusA.statusDesc, StatusB.statusDesc, StatusC.statusDesc, tblDepartment.departName, tblObserver1.empFullName, tblObserver1.empId "

        SqlStr = SqlStr & SqlStrOriginalSort

        Return SqlStr
    End Function
    Private Function SearchResponStr(ByVal s As String) As String
        Dim SqlStr As String = "SELECT tblRecordDetail.*, tblRecord.recActNo, tblRecord.recActDate, tblRecord.recActTime, tblRecord.durationValue, tblObsvCate.cateName, tblObsvCateSub.catesubName, tblObsvFailPoint.failpointName, 
                                    tblContractor.contractorName, tblStatusA.statusDesc AS StatusA, StatusB.statusDesc AS StatusB, StatusC.statusDesc AS StatusC, tblDepartment.departName, 
                                    tblRespA.empFullName AS RespFullA, tblRespB.empFullName AS RespFullB, tblRespC.empFullName AS RespFullC, tblObserver1.empId AS ownerId  
                                    FROM tblObsvCate INNER JOIN tblRecordDetail INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId ON tblObsvCate.cateId = tblRecordDetail.category 
                                    INNER JOIN tblDepartment ON tblRecord.departId = tblDepartment.departId 
                                    INNER JOIN tblEmployee As tblObserver1 ON tblRecord.empId = tblObserver1.empId 
                                    INNER JOIN tblStatus AS tblStatusA ON tblRecordDetail.proposeStatus_A = tblStatusA.statusId 
                                    LEFT OUTER JOIN dbo.tblEmployee AS tblRespA ON dbo.tblRecordDetail.proposeRespPerson_A = tblRespA.empId 
                                    LEFT OUTER JOIN dbo.tblEmployee AS tblRespB ON dbo.tblRecordDetail.proposeRespPerson_B = tblRespB.empId 
                                    LEFT OUTER JOIN dbo.tblEmployee AS tblRespC ON dbo.tblRecordDetail.proposeRespPerson_C = tblRespC.empId 
                                    LEFT OUTER JOIN dbo.tblStatus AS StatusB ON dbo.tblRecordDetail.proposeStatus_B = StatusB.statusId 
                                    LEFT OUTER JOIN dbo.tblStatus AS StatusC ON dbo.tblRecordDetail.proposeStatus_C = StatusC.statusId 
                                    LEFT OUTER JOIN dbo.tblContractor ON dbo.tblRecordDetail.contractor = dbo.tblContractor.contractorId 
                                    LEFT OUTER JOIN dbo.tblObsvCateSub ON dbo.tblRecordDetail.categorySub = dbo.tblObsvCateSub.catesubId 
                                    LEFT OUTER JOIN dbo.tblObsvFailPoint ON dbo.tblRecordDetail.failurePoint = dbo.tblObsvFailPoint.failpointId "

        Dim idx1 As Integer = s.IndexOf(" ")
        Dim idx2 As Integer = s.IndexOf("  ")
        If idx2 < 0 Then If idx1 >= 0 Then s = s.Replace(" ", "  ")
        SqlStr = SqlStr & "WHERE (tblRespA.empFullName LIKE '%" & s & "%' OR tblRespB.empFullName LIKE '%" & s & "%' OR tblRespC.empFullName LIKE '%" & s & "%') "

        If rcbDepartmentView.SelectedIndex = 0 And rcbStatusView.SelectedIndex = 0 Then
            SqlStr = SqlStr & SqlStrOriginalSort
        ElseIf rcbDepartmentView.SelectedIndex <> 0 And rcbStatusView.SelectedIndex = 0 Then
            SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' " & SqlStrOriginalSort
        ElseIf rcbDepartmentView.SelectedIndex = 0 And rcbStatusView.SelectedIndex <> 0 Then
            SqlStr = SqlStr & "AND observComplete = '" & rcbStatusView.SelectedValue & "' " & SqlStrOriginalSort
        ElseIf rcbDepartmentView.SelectedIndex <> 0 And rcbStatusView.SelectedIndex <> 0 Then
            SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' AND observComplete = '" & rcbStatusView.SelectedValue & "' " & SqlStrOriginalSort
        End If

        Return SqlStr
    End Function
    Private Function SearchDateRange(ByVal DateFrom As String, ByVal DateTo As String) As String
        Dim SqlStr As String = SqlStrOriginal
        SqlStr = SqlStr & "WHERE recActDate >= '" & DateFrom & "' AND recActDate <= '" & DateTo & "' "

        If rcbDepartmentView.SelectedIndex = 0 And rcbStatusView.SelectedIndex = 0 Then
            SqlStr = SqlStr & SqlStrOriginalSort
        ElseIf rcbDepartmentView.SelectedIndex <> 0 And rcbStatusView.SelectedIndex = 0 Then
            SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' " & SqlStrOriginalSort
        ElseIf rcbDepartmentView.SelectedIndex = 0 And rcbStatusView.SelectedIndex <> 0 Then
            SqlStr = SqlStr & "AND observComplete = '" & rcbStatusView.SelectedValue & "' " & SqlStrOriginalSort
        ElseIf rcbDepartmentView.SelectedIndex <> 0 And rcbStatusView.SelectedIndex <> 0 Then
            SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' AND observComplete = '" & rcbStatusView.SelectedValue & "' " & SqlStrOriginalSort
        End If

        Return SqlStr
    End Function
    Private Function SearchCombineStr(ByVal s1 As String, ByVal s2 As String) As String
        Dim SqlStr As String = "SELECT tblRecordDetail.*, tblRecord.recActNo, tblRecord.recActDate, tblRecord.recActTime, tblRecord.durationValue, tblObsvCate.cateName, tblObsvCateSub.catesubName, tblObsvFailPoint.failpointName, 
                                tblContractor.contractorName, tblStatusA.statusDesc AS StatusA, StatusB.statusDesc AS StatusB, StatusC.statusDesc AS StatusC, tblDepartment.departName, tblObserver1.empFullName, tblObserver1.empId AS ownerId
                                FROM tblEmployee AS tblObserverOth 
                                RIGHT OUTER JOIN tblRecordOthEmpO ON tblObserverOth.empId = tblRecordOthEmpO.empIdOth 
                                RIGHT OUTER JOIN tblObsvCate 
                                INNER JOIN tblRecordDetail ON tblObsvCate.cateId = tblRecordDetail.category 
                                INNER JOIN tblRecord ON tblRecordDetail.recId = tblRecord.recId  
                                INNER JOIN tblDepartment ON tblRecord.departId = tblDepartment.departId 
                                INNER JOIN tblStatus AS tblStatusA ON tblRecordDetail.proposeStatus_A = tblStatusA.statusId 
                                INNER JOIN tblEmployee AS tblObserver1 ON tblRecord.empId = tblObserver1.empId ON tblRecordOthEmpO.recId = dbo.tblRecord.recId 
                                LEFT OUTER JOIN tblEmployee AS tblRespA ON tblRecordDetail.proposeRespPerson_A = tblRespA.empId 
                                LEFT OUTER JOIN tblEmployee AS tblRespB ON tblRecordDetail.proposeRespPerson_B = tblRespB.empId 
                                LEFT OUTER JOIN tblEmployee AS tblRespC ON tblRecordDetail.proposeRespPerson_C = tblRespC.empId 
                                LEFT OUTER JOIN tblStatus AS StatusB ON tblRecordDetail.proposeStatus_B = StatusB.statusId 
                                LEFT OUTER JOIN tblStatus AS StatusC ON tblRecordDetail.proposeStatus_C = StatusC.statusId 
                                LEFT OUTER JOIN tblContractor ON tblRecordDetail.contractor = tblContractor.contractorId 
                                LEFT OUTER JOIN tblObsvCateSub ON tblRecordDetail.categorySub = tblObsvCateSub.catesubId 
                                LEFT OUTER JOIN tblObsvFailPoint ON tblRecordDetail.failurePoint = tblObsvFailPoint.failpointId "

        If s1 <> "" Then
            Dim idx1 As Integer = s1.IndexOf(" ")
            Dim idx2 As Integer = s1.IndexOf("  ")
            If idx2 < 0 Then If idx1 >= 0 Then s1 = s1.Replace(" ", "  ")
        End If
        SqlStr = SqlStr & "WHERE (tblObserver1.empFullName LIKE '%" & s1 & "%' " & " OR tblObserverOth.empFullName LIKE '%" & s1 & "%') "

        If s2 <> "" Then
            Dim idx1 As Integer = s2.IndexOf(" ")
            Dim idx2 As Integer = s2.IndexOf("  ")
            If idx2 < 0 Then If idx1 >= 0 Then s2 = s2.Replace(" ", "  ")
            SqlStr = SqlStr & "AND (tblRespA.empFullName LIKE '%" & s2 & "%' OR tblRespB.empFullName LIKE '%" & s2 & "%' OR tblRespC.empFullName LIKE '%" & s2 & "%') "
        End If

        If rdpFromCombine.SelectedDate IsNot Nothing And rdpToCombine.SelectedDate IsNot Nothing Then
            Dim DateFrom As Date = rdpFromCombine.SelectedDate
            Dim DateTo As Date = rdpToCombine.SelectedDate

            Dim UsaCulture As New CultureInfo("en-US")
            Dim usDateFrom As String = DateFrom.ToString("yyyy-MM-dd", UsaCulture)
            Dim usDateTo As String = DateTo.ToString("yyyy-MM-dd", UsaCulture)

            SqlStr = SqlStr & "AND recActDate >= '" & usDateFrom & "' AND recActDate <= '" & usDateTo & "' "
        End If

        'Category/Sub Category/Failure Point
        If rcbCategoryCB.SelectedIndex > 0 Then
            If rcbCategorySubCB.SelectedIndex > 0 Then
                If rcbFailPointCB.SelectedIndex > 0 Then
                    'search Category AND Sub Category AND Failure Point
                    SqlStr = SqlStr & "AND category = '" & rcbCategoryCB.SelectedValue & "' AND categorySub = '" & rcbCategorySubCB.SelectedValue & "' AND failurePoint = '" & rcbFailPointCB.SelectedValue & "' "
                Else
                    'search Category, all Sub Category
                    SqlStr = SqlStr & "AND category = '" & rcbCategoryCB.SelectedValue & "' AND categorySub = '" & rcbCategorySubCB.SelectedValue & "' "
                End If
            Else
                'search Category only
                SqlStr = SqlStr & "AND category = '" & rcbCategoryCB.SelectedValue & "' "
            End If
        End If

        'Department ddl
        If rcbDepartmentView.SelectedIndex <> 0 And rcbStatusView.SelectedIndex = 0 Then
            SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' "
        ElseIf rcbDepartmentView.SelectedIndex = 0 And rcbStatusView.SelectedIndex <> 0 Then
            SqlStr = SqlStr & "AND tblRecordDetail.observComplete = '" & rcbStatusView.SelectedValue & "' "
        ElseIf rcbDepartmentView.SelectedIndex <> 0 And rcbStatusView.SelectedIndex <> 0 Then
            SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' AND tblRecordDetail.observComplete = '" & rcbStatusView.SelectedValue & "' "
        End If

        SqlStr = SqlStr & "GROUP BY tblRecordDetail.detailId, tblRecordDetail.recId, tblRecordDetail.observItem, tblRecordDetail.title, tblRecordDetail.category, 
                         tblRecordDetail.categorySub, tblRecordDetail.failurePoint, tblRecordDetail.equipment, tblRecordDetail.IsHRO, tblRecordDetail.hroChk1, 
                         tblRecordDetail.hroChk2, tblRecordDetail.hroChk3, tblRecordDetail.hroChk4, tblRecordDetail.hroChk5, tblRecordDetail.secondEye, 
                         tblRecordDetail.recognition, tblRecordDetail.observType, tblRecordDetail.contractor, tblRecordDetail.pictureCount, tblRecordDetail.description, 
                         tblRecordDetail.proposeEnable_A, tblRecordDetail.proposeDesc_A, tblRecordDetail.proposeRespPerson_A, tblRecordDetail.proposeAction_A, 
                         tblRecordDetail.proposeStatus_A, tblRecordDetail.proposeComplete_A, tblRecordDetail.whoComplete_A, tblRecordDetail.proposeEnable_B, 
                         tblRecordDetail.proposeDesc_B, tblRecordDetail.proposeRespPerson_B, tblRecordDetail.proposeAction_B, tblRecordDetail.proposeStatus_B, 
                         tblRecordDetail.proposeComplete_B, tblRecordDetail.whoComplete_B, tblRecordDetail.proposeEnable_C, tblRecordDetail.proposeDesc_C, 
                         tblRecordDetail.proposeRespPerson_C, tblRecordDetail.proposeAction_C, tblRecordDetail.proposeStatus_C, tblRecordDetail.proposeComplete_C, 
                         tblRecordDetail.whoComplete_C, tblRecordDetail.observComplete, tblRecord.timestamp, tblRecord.recActNo, tblRecord.recActDate, tblRecord.recActTime, 
                         tblRecord.durationValue, tblObsvCate.cateName, tblObsvCateSub.catesubName, tblObsvFailPoint.failpointName, tblContractor.contractorName, 
                         tblStatusA.statusDesc, StatusB.statusDesc, StatusC.statusDesc, tblDepartment.departName, tblObserver1.empFullName, tblObserver1.empId "
        SqlStr = SqlStr & SqlStrOriginalSort

        Return SqlStr
    End Function

    Private Sub rcbDepartmentView_DataBound(sender As Object, e As EventArgs) Handles rcbDepartmentView.DataBound
        Dim rcb As RadComboBox = sender
        rcb.Items.Insert(0, New RadComboBoxItem("Show All Department", "0"))
    End Sub
    Private Sub rcbStatusView_DataBound(sender As Object, e As EventArgs) Handles rcbStatusView.DataBound
        Dim rcb As RadComboBox = sender
        rcb.Items.Insert(0, New RadComboBoxItem("Show All Status", "0"))
    End Sub

    Private Sub ddlView()
        If pnAdvanceSearch.Visible Then
            Select Case RadTabStrip1.SelectedIndex
                Case 0
                    SearchObservedStr(tbObserved.Text.Trim)
                Case 1
                    SearchResponStr(tbRespon.Text.Trim)
                Case 2
                    If rdpDocDateFrom.SelectedDate IsNot Nothing And rdpDocDateTo.SelectedDate IsNot Nothing Then
                        Dim DatetimeConv As New cDatetimeConv
                        Dim usDateFrom As Date = rdpDocDateFrom.SelectedDate
                        Dim usDateTo As Date = rdpDocDateTo.SelectedDate
                        SearchDateRange(DatetimeConv.DateToStringEnUS(usDateFrom), DatetimeConv.DateToStringEnUS(usDateTo))
                    End If
                Case 3
                    SearchCombineStr(tbObservedCombine.Text.Trim, tbResponCombine.Text.Trim)
            End Select
        End If
        rgRecordList.Rebind()
    End Sub
    Protected Sub rcbDepartmentView_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbDepartmentView.SelectedIndexChanged
        ddlView()
    End Sub
    Protected Sub rcbStatusView_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbStatusView.SelectedIndexChanged
        ddlView()
    End Sub

    Private Function getImageURL(ByVal status As Integer, ByVal item As Integer) As String
        Dim ImgURL As String = ""
        Select Case status
            Case 1000
                ImgURL = "~/Images/status-blank-20.png"
            Case 1001
                ImgURL = "~/Images/status-blue-" & item.ToString & "-20.png"
            Case 1002
                ImgURL = "~/Images/status-orange-" & item.ToString & "-20.png"
            Case 1003
                ImgURL = "~/Images/status-green-" & item.ToString & "-20.png"
        End Select

        Return ImgURL
    End Function

    Private Sub rgRecordList_DetailTableDataBind(sender As Object, e As GridDetailTableDataBindEventArgs) Handles rgRecordList.DetailTableDataBind
        Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        Select Case e.DetailTableView.Name
            Case "Observe"
                If True Then
                    Dim RecordId As String = dataItem.GetDataKeyValue("recId").ToString()
                    e.DetailTableView.DataSource = GetDataTable((Convert.ToString("SELECT * FROM tblRecordDetail WHERE recId = '") & RecordId) + "'")
                    Exit Select
                End If

            Case "OrderDetails"
                If True Then
                    Dim OrderID As String = dataItem.GetDataKeyValue("OrderID").ToString()
                    e.DetailTableView.DataSource = GetDataTable(Convert.ToString("SELECT *, CONVERT (VARCHAR(5), tblRecord.recActTime, 108) AS TimeHHMM FROM [Order Details] WHERE OrderID = ") & OrderID)
                    Exit Select
                End If
        End Select
    End Sub

    Private Sub rgRecordList_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgRecordList.ItemDataBound
        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

            Dim imbStatusPropose1 As ImageButton = item.FindControl("imbPropose1")
            Dim imbStatusPropose2 As ImageButton = item.FindControl("imbPropose2")
            Dim imbStatusPropose3 As ImageButton = item.FindControl("imbPropose3")

            Dim ImageUrl As New cImage
            imbStatusPropose1.ImageUrl = ImageUrl.getImage_msg(CInt(DataBinder.Eval(e.Item.DataItem, "proposeStatus_A")), 0)
            imbStatusPropose2.ImageUrl = ImageUrl.getImage_msg(CInt(DataBinder.Eval(e.Item.DataItem, "proposeStatus_B")), 0)
            If CBool(DataBinder.Eval(e.Item.DataItem, "proposeEnable_B")) Then imbStatusPropose2.Enabled = False
            imbStatusPropose3.ImageUrl = ImageUrl.getImage_msg(CInt(DataBinder.Eval(e.Item.DataItem, "proposeStatus_C")), 0)
            If CBool(DataBinder.Eval(e.Item.DataItem, "proposeEnable_C")) Then imbStatusPropose3.Enabled = False

            Dim imbStatusProposeAll As Image = item.FindControl("imbAllPropose")
            imbStatusProposeAll.ImageUrl = ImageUrl.getImage_status(CInt(DataBinder.Eval(e.Item.DataItem, "observComplete")), 0)
        End If

        '-- EditForm
        If TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode Then
            Dim eitem As GridEditableItem = TryCast(e.Item, GridEditableItem)
            Dim observItem As Integer = CInt(DataBinder.Eval(e.Item.DataItem, "observItem"))
            Dim ownerId As Integer = CInt(DataBinder.Eval(e.Item.DataItem, "ownerId"))            'คนที่ สร้างเอกสาร (Action Owner)

            Dim eCategory As RadComboBox = eitem.FindControl("rcbCategory")
            eCategory.SelectedValue = DataBinder.Eval(e.Item.DataItem, "category")

            Dim eCategorySub As RadComboBox = eitem.FindControl("rcbCategorySub")
            Dim srcCategorySub As SqlDataSource = eitem.FindControl("srcCategorySub")
            eCategorySub.DataSource = srcCategorySub
            If DataBinder.Eval(e.Item.DataItem, "categorySub") IsNot DBNull.Value Then
                eCategorySub.SelectedValue = DataBinder.Eval(e.Item.DataItem, "categorySub")
                eCategorySub.DataBind()
            End If

            Dim eFailurePoint As RadComboBox = eitem.FindControl("rcbFailurePoint")
            Dim srcFailurePoint As SqlDataSource = eitem.FindControl("srcFailurePoint")
            eFailurePoint.DataSource = srcFailurePoint
            If DataBinder.Eval(e.Item.DataItem, "failurePoint") <> 0 Then
                eFailurePoint.SelectedValue = DataBinder.Eval(e.Item.DataItem, "failurePoint")
                eFailurePoint.DataBind()
            End If

            Dim eEquipment As TextBox = eitem.FindControl("tbEquipment")
            eEquipment.Text = DataBinder.Eval(e.Item.DataItem, "equipment")

            '-- CheckBox
            Dim eChkHRO As CheckBox = eitem.FindControl("chkHRO")
            eChkHRO.Checked = DataBinder.Eval(e.Item.DataItem, "IsHRO")
            Dim eChk2Eye As CheckBox = eitem.FindControl("chk2Eye")
            eChk2Eye.Checked = DataBinder.Eval(e.Item.DataItem, "secondEye")
            Dim eChkRecognition As CheckBox = eitem.FindControl("chkRecognition")
            eChkRecognition.Checked = DataBinder.Eval(e.Item.DataItem, "recognition")

            Dim eChkHroOp1 As CheckBox = eitem.FindControl("chkHROop1")
            eChkHroOp1.Checked = DataBinder.Eval(e.Item.DataItem, "hroChk1")
            Dim eChkHroOp2 As CheckBox = eitem.FindControl("chkHROop2")
            eChkHroOp2.Checked = DataBinder.Eval(e.Item.DataItem, "hroChk2")
            Dim eChkHroOp3 As CheckBox = eitem.FindControl("chkHROop3")
            eChkHroOp3.Checked = DataBinder.Eval(e.Item.DataItem, "hroChk3")
            Dim eChkHroOp4 As CheckBox = eitem.FindControl("chkHROop4")
            eChkHroOp4.Checked = DataBinder.Eval(e.Item.DataItem, "hroChk4")
            Dim eChkHroOp5 As CheckBox = eitem.FindControl("chkHROop5")
            eChkHroOp5.Checked = DataBinder.Eval(e.Item.DataItem, "hroChk5")

            Dim eObserveType As RadComboBox = eitem.FindControl("rcbObserveType")
            eObserveType.SelectedValue = DataBinder.Eval(e.Item.DataItem, "observType")
            If eObserveType.SelectedValue = "1" Then
                Dim eContractor As RadComboBox = eitem.FindControl("rcbContractor")
                eContractor.SelectedValue = DataBinder.Eval(e.Item.DataItem, "contractor")
                eContractor.Visible = True
            End If

            '-- Picture
            Dim PictCount As Integer = DataBinder.Eval(e.Item.DataItem, "pictureCount")
            If PictCount > 0 Then
                Dim ePictureList As DataList = eitem.FindControl("PictureList")
                Dim SqlStr As String = "SELECT Id, recId, observeItem, picItem, picUrl FROM tblRecordPictureO WHERE (recId = " & DataBinder.Eval(e.Item.DataItem, "recId") & ") AND (observeItem = " & (observItem - 1).ToString & ")"

                Dim conn As New SqlConnection(ConnStr)
                Dim adapter As New SqlDataAdapter()
                adapter.SelectCommand = New SqlCommand(SqlStr, conn)
                Dim myDataTable As New DataTable()

                conn.Open()
                Try
                    adapter.Fill(myDataTable)
                Finally
                    conn.Close()
                End Try
                ePictureList.DataSource = myDataTable
                ePictureList.DataBind()
                Dim ePnShowImage As Panel = eitem.FindControl("pnShowImage")
                ePnShowImage.Visible = True
            End If

            '-- Description
            Dim eDescription As TextBox = eitem.FindControl("tbDescription")
            eDescription.Text = DataBinder.Eval(e.Item.DataItem, "description")

            '-- User Identity
            Dim employee As New cEmployee
            Dim loginEmpId As Integer = employee.FindEmployeeIdbyUsername(User.Identity.Name)

            '-- Action Propose #1
            Dim ImageUrl As New cImage
            Dim eActionA As TextBox = eitem.FindControl("tbActionA")
            eActionA.Text = DataBinder.Eval(e.Item.DataItem, "proposeAction_A")
            Dim ResponIdA As Integer = DataBinder.Eval(e.Item.DataItem, "proposeRespPerson_A")
            Dim eResponA As TextBox = eitem.FindControl("tbResponA")
            Dim emp As New cEmployee
            emp.FindEmployeeName(ResponIdA)
            eResponA.Text = emp.EmployeeFullName

            Dim eStatusA As Image = eitem.FindControl("imgStatusA")
            Dim StatusA As Integer = DataBinder.Eval(e.Item.DataItem, "proposeStatus_A")
            eStatusA.ImageUrl = ImageUrl.getImage_msg(StatusA, 0)
            Dim eStatusDescA As Label = eitem.FindControl("lbStatusA")
            eStatusDescA.Text = DataBinder.Eval(e.Item.DataItem, "StatusA")
            Dim ePnCompleteA As Panel = eitem.FindControl("pnCompleteA")
            Dim eChkCompleteA As CheckBox = eitem.FindControl("chkCompleteA")
            Dim eEditCompleteA As ImageButton = eitem.FindControl("editCompleteA")
            Select Case StatusA
                Case 1001
                    'Recognition
                    eActionA.Enabled = False
                    ePnCompleteA.Visible = False
                Case 1002
                    '------ Allow Update - Action Owner (OwnerId), Writer (ResponIdA), Facility Admin
                    Dim logicChk As Boolean = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN") Or ownerId = loginEmpId Or ResponIdA = loginEmpId
                    '--- เปลี่ยนเป็น Facility Admin และ System Admin เท่านั้น ที่กดเปลี่ยนได้   @8/5/2017
                    Dim logicChkComplete As Boolean = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN")
                    eActionA.Enabled = logicChk
                    eChkCompleteA.Enabled = logicChkComplete
                    ePnCompleteA.Visible = True
                Case 1003
                    'Complete
                    eActionA.Enabled = False
                    eChkCompleteA.Checked = True
                    ePnCompleteA.Visible = False

                    '--- เปลี่ยนเป็น Facility Admin และ System Admin เท่านั้น ที่แก้ไข complete ได้   @8/5/2017
                    If User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN") Then eEditCompleteA.Visible = True
            End Select

            '-- Action Propose #2
            Dim panelB As Panel = eitem.FindControl("pnResponB")
            If DataBinder.Eval(e.Item.DataItem, "proposeEnable_B") Then
                panelB.Visible = True
                Dim eActionB As TextBox = eitem.FindControl("tbActionB")
                eActionB.Text = DataBinder.Eval(e.Item.DataItem, "proposeAction_B")
                Dim ResponIdB As Integer = DataBinder.Eval(e.Item.DataItem, "proposeRespPerson_B")
                Dim eResponB As TextBox = eitem.FindControl("tbResponB")
                emp.FindEmployeeName(ResponIdB)
                eResponB.Text = emp.EmployeeFullName

                Dim eStatusB As Image = eitem.FindControl("imgStatusB")
                Dim StatusB As Integer = DataBinder.Eval(e.Item.DataItem, "proposeStatus_B")
                eStatusB.ImageUrl = ImageUrl.getImage_msg(StatusB, 0)
                Dim eStatusDescB As Label = eitem.FindControl("lbStatusB")
                eStatusDescB.Text = DataBinder.Eval(e.Item.DataItem, "StatusB")
                Dim ePnCompleteB As Panel = eitem.FindControl("pnCompleteB")
                Dim eChkCompleteB As CheckBox = eitem.FindControl("chkCompleteB")
                Dim eEditCompleteB As ImageButton = eitem.FindControl("editCompleteB")
                Select Case StatusB
                    Case 1001
                        'Recognition
                        eActionB.Enabled = False
                        ePnCompleteB.Visible = False
                    Case 1002
                        '------ Allow Update - Action Owner (OwnerId), Writer (ResponIdA), Facility Admin
                        Dim logicChk As Boolean = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN") Or ownerId = loginEmpId Or ResponIdB = loginEmpId
                        '--- เปลี่ยนเป็น Facility Admin และ System Admin เท่านั้น ที่กดเปลี่ยนได้   @8/5/2017
                        Dim logicChkComplete As Boolean = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN")
                        eActionB.Enabled = logicChk
                        eChkCompleteB.Enabled = logicChkComplete
                        ePnCompleteB.Visible = True
                    Case 1003
                        eActionB.Enabled = False
                        eChkCompleteB.Checked = True
                        ePnCompleteB.Visible = False

                        '--- เปลี่ยนเป็น Facility Admin และ System Admin เท่านั้น ที่กดเปลี่ยนได้   @8/5/2017
                        If User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN") Then eEditCompleteB.Visible = True
                End Select
            End If

            '-- Action Propose #3
            Dim panelC As Panel = eitem.FindControl("pnResponC")
            If DataBinder.Eval(e.Item.DataItem, "proposeEnable_C") Then
                panelC.Visible = True
                Dim eActionC As TextBox = eitem.FindControl("tbActionC")
                eActionC.Text = DataBinder.Eval(e.Item.DataItem, "proposeAction_C")
                Dim ResponIdC As Integer = DataBinder.Eval(e.Item.DataItem, "proposeRespPerson_C")
                Dim eResponC As TextBox = eitem.FindControl("tbResponC")
                emp.FindEmployeeName(ResponIdC)
                eResponC.Text = emp.EmployeeFullName

                Dim eStatusC As Image = eitem.FindControl("imgStatusC")
                Dim StatusC As Integer = DataBinder.Eval(e.Item.DataItem, "proposeStatus_C")
                eStatusC.ImageUrl = ImageUrl.getImage_msg(StatusC, 0)
                Dim eStatusDescC As Label = eitem.FindControl("lbStatusC")
                eStatusDescC.Text = DataBinder.Eval(e.Item.DataItem, "StatusC")
                Dim ePnCompleteC As Panel = eitem.FindControl("pnCompleteC")
                Dim eChkCompleteC As CheckBox = eitem.FindControl("chkCompleteC")
                Dim eEditCompleteC As ImageButton = eitem.FindControl("editCompleteC")
                Select Case StatusC
                    Case 1001
                        'Recognition
                        eActionC.Enabled = False
                        ePnCompleteC.Visible = False
                    Case 1002
                        '------ Allow Update - Action Owner (OwnerId), Writer (ResponIdA), Facility Admin
                        Dim logicChk As Boolean = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN") Or ownerId = loginEmpId Or ResponIdC = loginEmpId
                        '--- เปลี่ยนเป็น Facility Admin และ System Admin เท่านั้น ที่กดเปลี่ยนได้   @8/5/2017
                        Dim logicChkComplete As Boolean = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN")
                        eActionC.Enabled = logicChk
                        eChkCompleteC.Enabled = logicChkComplete
                        ePnCompleteC.Visible = True
                    Case 1003
                        eActionC.Enabled = False
                        eChkCompleteC.Checked = True
                        ePnCompleteC.Visible = False

                        '--- เปลี่ยนเป็น Facility Admin และ System Admin เท่านั้น ที่กดเปลี่ยนได้   @8/5/2017
                        If User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN") Then eEditCompleteC.Visible = True
                End Select
            End If

            'Generate reporter
            Dim recId As String = DataBinder.Eval(e.Item.DataItem, "recId")
            Dim reporterStr As String = generateReporter(ownerId, CInt(recId), observItem)

            'Re-Send Email button add CommandArgument
            Dim eBtResend As Button = eitem.FindControl("btResendEmail")
            eBtResend.CommandArgument = recId & "|" & observItem.ToString
            Dim eBtBack As ImageButton = eitem.FindControl("imbBack")
            eBtBack.CommandArgument = DataBinder.Eval(e.Item.DataItem, "recActNo") & "|" & DataBinder.Eval(e.Item.DataItem, "departName") & "|" & observItem.ToString & "|" & DataBinder.Eval(e.Item.DataItem, "title") & "|" & reporterStr
        End If
    End Sub

    Private Sub rgRecordList_UpdateCommand(sender As Object, e As GridCommandEventArgs) Handles rgRecordList.UpdateCommand
        If e.CommandName = RadGrid.UpdateCommandName Then
            If TypeOf e.Item Is GridEditFormItem Then
                Dim item As GridEditFormItem = DirectCast(e.Item, GridEditFormItem)

                Dim ePanelB As Panel = DirectCast(item.FindControl("pnResponB"), Panel)
                Dim ePanelC As Panel = DirectCast(item.FindControl("pnResponC"), Panel)

                Dim eActionA As TextBox = DirectCast(item.FindControl("tbActionA"), TextBox)
                Dim eChkCompleteA As CheckBox = DirectCast(item.FindControl("chkCompleteA"), CheckBox)
                Dim eStatusA As HiddenField = DirectCast(item.FindControl("hfStatusA"), HiddenField)
                Dim StatusValueA As Integer = CInt(eStatusA.Value)

                Dim status As New cStatus
                Dim eBtResend As Button = DirectCast(item.FindControl("btResendEmail"), Button)
                Dim CommArgumentStr As String = eBtResend.CommandArgument
                Dim recId As Integer = CommArgumentStr.Substring(0, CommArgumentStr.IndexOf("|"))
                Dim DetailId As Integer = CInt(item.GetDataKeyValue("detailId"))
                Dim StrUpd As String = ""

                Dim lbUpdateInfo As Label = DirectCast(item.FindControl("lbUpdateInfo"), Label)
                Dim IsValid As Boolean = True
                Dim result As Integer
                Dim conn As New SqlConnection(ConnStr)
                conn.Open()

                Dim whoCompleteA As String = User.Identity.GetUserId
                If Not ePanelB.Visible And Not ePanelC.Visible Then
                    '1 Action
                    StrUpd = "UPDATE tblRecordDetail SET proposeAction_A = @proposeAction_A, proposeStatus_A = @proposeStatus_A, proposeComplete_A  = @proposeComplete_A, whoComplete_A = @whoComplete_A, observComplete = @observComplete WHERE detailId = @detailId"

                    Dim command As New SqlCommand(StrUpd, conn)
                    command.Parameters.Add("@detailId", SqlDbType.Int).Value = DetailId
                    command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = eActionA.Text
                    If eActionA.Text = "" Then IsValid = False

                    Dim newStatusValueA As Integer = 1003
                    If eChkCompleteA.Enabled Then       'Edit Mode
                        If Not eChkCompleteA.Checked Then newStatusValueA = 1002
                    Else
                        If Not eChkCompleteA.Checked Then
                            newStatusValueA = StatusValueA
                            whoCompleteA = "0"
                        End If
                    End If
                    command.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = newStatusValueA
                    command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = eChkCompleteA.Checked
                    command.Parameters.Add("@whoComplete_A", SqlDbType.NVarChar).Value = whoCompleteA

                    command.Parameters.Add("@observComplete", SqlDbType.NVarChar).Value = status.observStatus(newStatusValueA, False, 0, False, 0)

                    If IsValid Then
                        result = command.ExecuteNonQuery()
                        status.UpdRecordIsComplete(recId)        'update IsComplete in tblRecord (action number)
                    Else
                        lbUpdateInfo.Text = "Please check Propose Action field."
                        e.Canceled = True
                    End If
                ElseIf ePanelB.Visible And Not ePanelC.Visible Then
                    '2 Action
                    StrUpd = "UPDATE tblRecordDetail SET proposeAction_A = @proposeAction_A, proposeStatus_A = @proposeStatus_A, proposeComplete_A = @proposeComplete_A, whoComplete_A = @whoComplete_A, 
                              proposeAction_B = @proposeAction_B, proposeStatus_B = @proposeStatus_B, proposeComplete_B = @proposeComplete_B, whoComplete_B = @whoComplete_B, 
                              observComplete = @observComplete WHERE detailId = @detailId"

                    Dim command As New SqlCommand(StrUpd, conn)
                    command.Parameters.Add("@detailId", SqlDbType.Int).Value = DetailId
                    command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = eActionA.Text
                    If eActionA.Text = "" Then IsValid = False

                    Dim newStatusValueA As Integer = 1003
                    If eChkCompleteA.Enabled Then
                        If Not eChkCompleteA.Checked Then newStatusValueA = 1002
                    Else
                        If Not eChkCompleteA.Checked Then
                            newStatusValueA = StatusValueA
                            whoCompleteA = "0"
                        End If
                    End If
                    command.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = newStatusValueA
                    command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = eChkCompleteA.Checked
                    command.Parameters.Add("@whoComplete_A", SqlDbType.NVarChar).Value = whoCompleteA

                    Dim eActionB As TextBox = DirectCast(item.FindControl("tbActionB"), TextBox)
                    Dim eChkCompleteB As CheckBox = DirectCast(item.FindControl("chkCompleteB"), CheckBox)
                    command.Parameters.Add("@proposeAction_B", SqlDbType.NVarChar).Value = eActionB.Text
                    If eActionB.Text = "" Then IsValid = False

                    Dim newStatusValueB As Integer = 1003
                    Dim whoCompleteB As String = User.Identity.GetUserId
                    If eChkCompleteB.Enabled Then
                        If Not eChkCompleteB.Checked Then newStatusValueB = 1002
                    Else
                        If Not eChkCompleteB.Checked Then
                            Dim eStatusB As HiddenField = DirectCast(item.FindControl("hfStatusB"), HiddenField)
                            Dim StatusValueB As Integer = CInt(eStatusB.Value)
                            newStatusValueB = StatusValueB
                            whoCompleteB = "0"
                        End If
                    End If
                    command.Parameters.Add("@proposeStatus_B", SqlDbType.Int).Value = newStatusValueB
                    command.Parameters.Add("@proposeComplete_B", SqlDbType.Bit).Value = eChkCompleteB.Checked
                    command.Parameters.Add("@whoComplete_B", SqlDbType.NVarChar).Value = whoCompleteB

                    command.Parameters.Add("@observComplete", SqlDbType.NVarChar).Value = status.observStatus(newStatusValueA, True, newStatusValueB, False, 0)

                    If IsValid Then
                        result = command.ExecuteNonQuery()
                        status.UpdRecordIsComplete(recId)        'update IsComplete in tblRecord (action number)
                    Else
                        lbUpdateInfo.Text = "Please check Propose Action field."
                        e.Canceled = True
                    End If
                ElseIf ePanelB.Visible And ePanelC.Visible Then
                    '3 Action
                    StrUpd = "UPDATE tblRecordDetail SET proposeAction_A = @proposeAction_A, proposeStatus_A = @proposeStatus_A, proposeComplete_A  = @proposeComplete_A, whoComplete_A = @whoComplete_A, 
                              proposeAction_B = @proposeAction_B, proposeStatus_B = @proposeStatus_B, proposeComplete_B = @proposeComplete_B, whoComplete_B = @whoComplete_B, 
                              proposeAction_C = @proposeAction_C, proposeStatus_C = @proposeStatus_C, proposeComplete_C = @proposeComplete_C, whoComplete_C = @whoComplete_C, 
                              observComplete = @observComplete WHERE detailId = @detailId"

                    Dim command As New SqlCommand(StrUpd, conn)
                    command.Parameters.Add("@detailId", SqlDbType.Int).Value = DetailId
                    command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = eActionA.Text
                    If eActionA.Text = "" Then IsValid = False

                    Dim newStatusValueA As Integer = 1003
                    If eChkCompleteA.Enabled Then
                        If Not eChkCompleteA.Checked Then newStatusValueA = 1002
                    Else
                        If Not eChkCompleteA.Checked Then
                            newStatusValueA = StatusValueA
                            whoCompleteA = "0"
                        End If
                    End If
                    command.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = newStatusValueA
                    command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = eChkCompleteA.Checked
                    command.Parameters.Add("@whoComplete_A", SqlDbType.NVarChar).Value = whoCompleteA

                    Dim eActionB As TextBox = DirectCast(item.FindControl("tbActionB"), TextBox)
                    Dim eChkCompleteB As CheckBox = DirectCast(item.FindControl("chkCompleteB"), CheckBox)
                    command.Parameters.Add("@proposeAction_B", SqlDbType.NVarChar).Value = eActionB.Text
                    If eActionB.Text = "" Then IsValid = False

                    Dim newStatusValueB As Integer = 1003
                    Dim whoCompleteB As String = User.Identity.GetUserId
                    If eChkCompleteB.Enabled Then
                        If Not eChkCompleteB.Checked Then newStatusValueB = 1002
                    Else
                        If Not eChkCompleteB.Checked Then
                            Dim eStatusB As HiddenField = DirectCast(item.FindControl("hfStatusB"), HiddenField)
                            Dim StatusValueB As Integer = CInt(eStatusB.Value)
                            newStatusValueB = StatusValueB
                            whoCompleteB = "0"
                        End If
                    End If
                    command.Parameters.Add("@proposeStatus_B", SqlDbType.Int).Value = newStatusValueB
                    command.Parameters.Add("@proposeComplete_B", SqlDbType.Bit).Value = eChkCompleteB.Checked
                    command.Parameters.Add("@whoComplete_B", SqlDbType.NVarChar).Value = whoCompleteB

                    Dim eActionC As TextBox = DirectCast(item.FindControl("tbActionC"), TextBox)
                    Dim eChkCompleteC As CheckBox = DirectCast(item.FindControl("chkCompleteC"), CheckBox)
                    command.Parameters.Add("@proposeAction_C", SqlDbType.NVarChar).Value = eActionC.Text
                    If eActionC.Text = "" Then IsValid = False

                    Dim newStatusValueC As Integer = 1003
                    Dim whoCompleteC As String = User.Identity.GetUserId
                    If eChkCompleteC.Enabled Then
                        If Not eChkCompleteC.Checked Then newStatusValueC = 1002
                    Else
                        If Not eChkCompleteC.Checked Then
                            Dim eStatusC As HiddenField = DirectCast(item.FindControl("hfStatusC"), HiddenField)
                            Dim StatusValueC As Integer = CInt(eStatusC.Value)
                            newStatusValueC = StatusValueC
                            whoCompleteC = "0"
                        End If
                    End If
                    command.Parameters.Add("@proposeStatus_C", SqlDbType.Int).Value = newStatusValueC
                    command.Parameters.Add("@proposeComplete_C", SqlDbType.Bit).Value = eChkCompleteC.Checked
                    command.Parameters.Add("@whoComplete_C", SqlDbType.NVarChar).Value = whoCompleteC

                    command.Parameters.Add("@observComplete", SqlDbType.NVarChar).Value = status.observStatus(newStatusValueA, True, newStatusValueB, True, newStatusValueC)

                    If IsValid Then
                        result = command.ExecuteNonQuery()
                        status.UpdRecordIsComplete(recId)        'update IsComplete in tblRecord (action number)
                    Else
                        lbUpdateInfo.Text = "Please check Propose Action field."
                        e.Canceled = True
                    End If
                End If
                conn.Close()

                If result > 0 Then
                    rgRecordList.Rebind()
                End If
            End If
        End If
    End Sub

    Private Function generateReporter(ByVal OwnerId As Integer, ByVal RecID As Integer, ByVal observItem As Integer) As String
        Dim emp As New cEmployee
        emp.FindEmployeeName(OwnerId)
        Dim ReporterStr As String = "[" & emp.EmployeeFullName & ", " & emp.DowId & ", " & emp.DepartmentName & "] "

        'other observe
        Const StrSelect As String = "SELECT * FROM tblRecordOthEmpO WHERE recId = @recId"
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(StrSelect, connection)
            command.Parameters.Add("@recId", SqlDbType.Int).Value = RecID
            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()
            If DataRead.HasRows Then
                While DataRead.Read()
                    emp.FindEmployeeName(CInt(DataRead("empIdOth")))
                    ReporterStr = ReporterStr & "[" & emp.EmployeeFullName & ", " & emp.DowId & ", " & emp.DepartmentName & "] "
                End While
            End If
        End Using
        Return ReporterStr
    End Function

    Protected Sub btResendEmail_Click(sender As Object, e As EventArgs)
        Dim btReSend As Button = sender
        Dim eitem As GridEditableItem = btReSend.NamingContainer

        Dim pnUpdate As Panel = eitem.FindControl("pnUpdateButton")
        Dim pnReSend As Panel = eitem.FindControl("pnResendEmail")
        Dim pnSendBt As Panel = eitem.FindControl("pnSendButton")
        Dim btBack As Button = eitem.FindControl("btBackReSend")
        Dim btClose As Button = eitem.FindControl("btCloseReSend")

        If pnUpdate.Visible Then
            Dim SplitPos As Integer = btReSend.CommandArgument.IndexOf("|")
            Dim recId As Integer = CInt(btReSend.CommandArgument.Substring(0, SplitPos))
            Dim obItem As Integer = CInt(btReSend.CommandArgument.Substring(SplitPos + 1))

            '-- Bind DB
            Dim EmailList As RadGrid = eitem.FindControl("rgEmailList")
            Dim srcSendEmailList As SqlDataSource = eitem.FindControl("srcSendEmailList")
            srcSendEmailList.SelectParameters("recId").DefaultValue = recId
            EmailList.DataSource = srcSendEmailList
            EmailList.DataBind()

            Dim EmailListEachOb As RadGrid = eitem.FindControl("rgEmailListEachOb")
            Dim srcSendListEachOb As SqlDataSource = eitem.FindControl("srcSendListEachOb")
            srcSendListEachOb.SelectParameters("recId").DefaultValue = recId
            srcSendListEachOb.SelectParameters("obItem").DefaultValue = obItem
            EmailListEachOb.DataSource = srcSendListEachOb
            EmailListEachOb.DataBind()

            'MsgBox(btResendEmail.CommandArgument & " " & SplitPos.ToString & " " & recId.ToString & " " & obItem.ToString)     'debug

            pnUpdate.Visible = False
        Else
            pnUpdate.Visible = True
        End If

        btBack.Visible = Not pnUpdate.Visible
        btClose.Visible = Not pnUpdate.Visible
        pnReSend.Visible = Not pnUpdate.Visible
        pnSendBt.Visible = Not pnUpdate.Visible
    End Sub

    Protected Sub rgEmailListEachOb_ItemDataBound(sender As Object, e As GridItemEventArgs)
        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

            Dim ObserveNo As Short = CShort(DataBinder.Eval(e.Item.DataItem, "observItem"))
            Dim lbObserveNo As Label = item.FindControl("lbObserveNo")
            lbObserveNo.Text = "OBSERVE " & ObserveNo.ToString
        End If
    End Sub

    Protected Sub btSendEmail_Click(sender As Object, e As EventArgs)
        Dim btSendEmail As Button = sender
        Dim eitem As GridEditableItem = btSendEmail.NamingContainer

        'Detail() = {ObserveNo, Title, Category, CategorySub, FailurePoint, Equipment, HRO, ObservedType, Description}
        'Picture() = {UrlPic1, UrlPic2, UrlPic3, UrlPic4}
        'OtherPropose() = {ProposeActionB, StatusB, ProposeActionC, StatusC}

        Dim Detail() As String = {"", "", "", "", "", "", "", "", ""}
        Dim Picture() As String = {"", "", "", ""}
        Dim OtherPropose() As String = {"", "", "", ""}

        Dim imbtBack As ImageButton = eitem.FindControl("imbBack")
        Dim cmdArgumentStr As String = imbtBack.CommandArgument
        Dim paraArray() As String = cmdArgumentStr.Split("|")

        Dim i As Integer = CInt(paraArray(2))

        Detail(0) = i.ToString      'ObserveNo
        Detail(1) = paraArray(3)    'Title
        Dim Category As RadComboBox = eitem.FindControl("rcbCategory")
        Detail(2) = Category.Text
        Dim CategorySub As RadComboBox = eitem.FindControl("rcbCategorySub")
        Detail(3) = CategorySub.Text
        Dim FailurePoint As RadComboBox = eitem.FindControl("rcbFailurePoint")
        Detail(4) = FailurePoint.Text
        Dim Equipment As TextBox = eitem.FindControl("tbEquipment")
        Detail(5) = Equipment.Text

        Dim HRO1op1 As CheckBox = eitem.FindControl("chkHROop1")
        Dim HRO1op2 As CheckBox = eitem.FindControl("chkHROop2")
        Dim HRO1op3 As CheckBox = eitem.FindControl("chkHROop3")
        Dim HRO1op4 As CheckBox = eitem.FindControl("chkHROop4")
        Dim HRO1op5 As CheckBox = eitem.FindControl("chkHROop5")

        If HRO1op1.Checked Then Detail(6) = Detail(6) & "( x )" & HRO1op1.Text & "<br/>"
        If HRO1op2.Checked Then Detail(6) = Detail(6) & "( x )" & HRO1op2.Text & "<br/>"
        If HRO1op3.Checked Then Detail(6) = Detail(6) & "( x )" & HRO1op3.Text & "<br/>"
        If HRO1op4.Checked Then Detail(6) = Detail(6) & "( x )" & HRO1op4.Text & "<br/>"
        If HRO1op5.Checked Then Detail(6) = Detail(6) & "( x )" & HRO1op5.Text & "<br/>"
        If Detail(6).Length > 5 Then Detail(6) = Detail(6).Substring(0, Detail(6).Length - 5)

        Dim ObserveType As RadComboBox = eitem.FindControl("rcbObserveType")
        If ObserveType.SelectedValue = "0" Then
            Detail(7) = ObserveType.Text
        Else
            Dim Contractor As RadComboBox = eitem.FindControl("rcbContractor")
            Detail(7) = ObserveType.Text & "/ " & Contractor.Text
        End If

        Dim Description As TextBox = eitem.FindControl("tbDescription")
        Detail(8) = Description.Text

        Dim PictureList As DataList = eitem.FindControl("PictureList")
        For imCount As Integer = 1 To PictureList.Items.Count
            Dim Image As System.Web.UI.WebControls.Image = PictureList.Items(imCount - 1).FindControl("Image1")
            Picture(imCount - 1) = Image.ImageUrl
        Next

        Dim Recognition As CheckBox = eitem.FindControl("chkRecognition")
        Dim ActionA As TextBox = eitem.FindControl("tbActionA")
        Dim ActionAHTML As String = Replace(ActionA.Text, vbLf, "<br/>")
        Dim ResponPersonA As String = ""
        If Not Recognition.Checked Then
            Dim ResponA As TextBox = eitem.FindControl("tbResponA")
            ResponPersonA = ResponA.Text
        End If

        Dim pnResponB As Panel = eitem.FindControl("pnResponB")
        If pnResponB.Visible Then
            Dim ActionB As TextBox = eitem.FindControl("tbActionB")
            OtherPropose(0) = Replace(ActionB.Text, vbLf, "<br/>")
            If Not Recognition.Checked Then
                Dim ResponB As TextBox = eitem.FindControl("tbResponB")
                OtherPropose(1) = ResponB.Text
            End If
        End If
        Dim pnResponC As Panel = eitem.FindControl("pnResponC")
        If pnResponC.Visible Then
            Dim ActionC As TextBox = eitem.FindControl("tbActionC")
            OtherPropose(2) = Replace(ActionC.Text, vbLf, "<br/>")
            If Not Recognition.Checked Then
                Dim ResponC As TextBox = eitem.FindControl("tbResponC")
                OtherPropose(3) = ResponC.Text
            End If
        End If

        Dim btResendEmail As Button = eitem.FindControl("btResendEmail")
        Dim cmdArgumentStr2 As String = btResendEmail.CommandArgument
        Dim recId As String = cmdArgumentStr2.Substring(0, cmdArgumentStr2.IndexOf("|"))
        Dim recIdLink As String = recId

        Dim SendMail As New cSendMail
        Dim Subject As String = "EZ Path Action updated: " & Detail(1)     'EZ Path: [Title]
        Dim body As String = SendMail.PopulateBody("observeIssue.html", paraArray(1), paraArray(0), paraArray(4), Detail, Picture, ActionAHTML, ResponPersonA, OtherPropose, recIdLink)
        'PopulateBody(TemplateName, Department, ActionNumber, ReporterStr, Detail(), Picture, ProposeActionA, ResponPersonA, OtherPropose, recId)

        Dim gridEmailList As RadGrid = eitem.FindControl("rgEmailList")
        Dim gridEmailListEachOb As RadGrid = eitem.FindControl("rgEmailListEachOb")

        SendMail.SendHtmlFormattedEmail(CreateEmailToEachObserve(i, gridEmailList, gridEmailListEachOb), Subject, body)

        '-- Sent Complete
        Dim pnUpdate As Panel = eitem.FindControl("pnUpdateButton")
        Dim pnReSend As Panel = eitem.FindControl("pnResendEmail")
        Dim pnSendBt As Panel = eitem.FindControl("pnSendButton")
        Dim btBack As Button = eitem.FindControl("btBackReSend")
        Dim btClose As Button = eitem.FindControl("btCloseReSend")

        btBack.Visible = False
        btClose.Visible = False
        pnReSend.Visible = False
        pnSendBt.Visible = False
        pnUpdate.Visible = True
        MsgBoxRad("<b>Re-Send email completed</b>", 240, 76)

    End Sub

    Private Function CreateEmailToEachObserve(ByVal ObserveNo As Integer, ByVal rgEmailList As RadGrid, ByVal rgEmailListEachOb As RadGrid) As ArrayList
        Dim MailTo As New ArrayList

        For Each items As GridDataItem In rgEmailList.Items
            If TryCast(items.FindControl("chkSelectSend"), CheckBox).Checked Then
                Dim Email As Label = items.FindControl("lbEmail")
                Dim FullName As Label = items.FindControl("lbFullName")
                MailTo.AddRange({Email.Text, FullName.Text})
            End If
        Next
        For Each items As GridDataItem In rgEmailListEachOb.Items
            Dim lbObserveNo As Label = items.FindControl("lbObserveNo")
            Dim rowObserveNo As Integer = CInt(lbObserveNo.Text.Substring(lbObserveNo.Text.Length - 1))

            If TryCast(items.FindControl("chkSelectSend"), CheckBox).Checked And rowObserveNo = ObserveNo Then
                Dim lbEmail As Label = items.FindControl("lbEmail")
                Dim lbFullName As Label = items.FindControl("lbFullName")

                MailTo.AddRange({lbEmail.Text, lbFullName.Text})
            End If
        Next

        Return MailTo
    End Function

    Protected Sub editCompleteA_Click(sender As Object, e As ImageClickEventArgs)
        Dim btEditComplete As ImageButton = sender
        Dim eitem As GridEditableItem = btEditComplete.NamingContainer

        Dim pnCompleteA As Panel = eitem.FindControl("pnCompleteA")
        Dim chkCompleteA As CheckBox = eitem.FindControl("chkCompleteA")
        If Not pnCompleteA.Visible Then
            chkCompleteA.Checked = True
            chkCompleteA.Enabled = True
            pnCompleteA.Visible = True
        Else
            chkCompleteA.Enabled = False
            pnCompleteA.Visible = False
        End If
    End Sub
    Protected Sub editCompleteB_Click(sender As Object, e As ImageClickEventArgs)
        Dim btEditComplete As ImageButton = sender
        Dim eitem As GridEditableItem = btEditComplete.NamingContainer

        Dim pnCompleteB As Panel = eitem.FindControl("pnCompleteB")
        Dim chkCompleteB As CheckBox = eitem.FindControl("chkCompleteB")
        If Not pnCompleteB.Visible Then
            chkCompleteB.Checked = True
            chkCompleteB.Enabled = True
            pnCompleteB.Visible = True
        Else
            chkCompleteB.Enabled = False
            pnCompleteB.Visible = False
        End If
    End Sub
    Protected Sub editCompleteC_Click(sender As Object, e As ImageClickEventArgs)
        Dim btEditComplete As ImageButton = sender
        Dim eitem As GridEditableItem = btEditComplete.NamingContainer

        Dim pnCompleteC As Panel = eitem.FindControl("pnCompleteC")
        Dim chkCompleteC As CheckBox = eitem.FindControl("chkCompleteC")
        If Not pnCompleteC.Visible Then
            chkCompleteC.Checked = True
            chkCompleteC.Enabled = True
            pnCompleteC.Visible = True
        Else
            chkCompleteC.Enabled = False
            pnCompleteC.Visible = False
        End If
    End Sub
End Class