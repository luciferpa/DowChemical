Imports System.Data.SqlClient
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports System.Globalization

Imports Telerik.Web.UI

Public Class observationList
    Inherits Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Dim SqlStrOriginal As String = "SELECT dbo.tblRecord.recId, dbo.tblRecord.timestamp, dbo.tblRecord.departId, dbo.tblDepartment.departName, 
	                                dbo.tblRecord.recActNo, dbo.tblRecord.recActMonth, dbo.tblRecord.recActYear, dbo.tblRecord.recActDate, dbo.tblRecord.recActTime, 
	                                CONVERT (VARCHAR(5), dbo.tblRecord.recActTime, 108) AS TimeHHMM, dbo.tblRecord.durationH, dbo.tblRecord.durationM, dbo.tblRecord.durationValue, 
	                                dbo.tblRecord.empId, dbo.tblEmployee.empFullName, dbo.tblRecord.oEmpCount, dbo.tblRecord.noObserve 
	                                FROM dbo.tblRecord 
	                                INNER JOIN dbo.tblEmployee ON dbo.tblRecord.empId = dbo.tblEmployee.empId 
	                                INNER JOIN dbo.tblDepartment ON dbo.tblRecord.departId = dbo.tblDepartment.departId "

    Dim SqlStrOriginalGroupBy = "GROUP BY tblRecord.recId, tblRecord.timestamp, tblRecord.departId, tblDepartment.departName, tblRecord.recActNo, tblRecord.recActMonth, 
                         tblRecord.recActYear, tblRecord.recActDate, tblRecord.recActTime, CONVERT(VARCHAR(5), tblRecord.recActTime, 108), tblRecord.durationH, 
                         tblRecord.durationM, tblRecord.durationValue, tblRecord.empId, tblEmployee.empFullName, tblRecord.oEmpCount, tblRecord.noObserve "

    Dim SqlStrOriginalWhere As String = "WHERE (dbo.tblRecord.tempFlag = 'False') AND (dbo.tblRecord.recActive = 'True') "
    Dim SqlStrOriginalSort As String = "ORDER BY dbo.tblRecord.timestamp DESC"
    Dim SqlStrSearchSort As String = "ORDER BY tblRecord.recActNo DESC"
    Dim IsRoleAllow As Boolean

    Private Sub MsgBoxRad(ByVal Msg As String, ByVal Width As Integer, ByVal Height As Integer)
        RadWindowManager1.Width = Width
        RadWindowManager1.Height = Height
        RadWindowManager1.RadAlert(Msg, Width + 100, Height + 72, "My Alert", "", "myAlertImage.png")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("ExpandedRow") = ""

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
                RadPanelBar1.Items.FindItemByText("OBSERVER").Selected = True
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

    Private Sub rgRecordList_DetailTableDataBind(sender As Object, e As GridDetailTableDataBindEventArgs) Handles rgRecordList.DetailTableDataBind
        Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        Select Case e.DetailTableView.Name
            Case "Observe"
                If True Then
                    Dim sqlStr As String = "SELECT tblRecordDetail.*, StatusA.statusDesc AS StatusDescA, StatusB.statusDesc AS StatusDescB, StatusC.statusDesc AS StatusDescC, tblRecord.empId  
                                            FROM tblRecordDetail 
                                            INNER JOIN dbo.tblRecord ON dbo.tblRecordDetail.recId = dbo.tblRecord.recId                                             
                                            LEFT OUTER JOIN tblStatus AS StatusA ON tblRecordDetail.proposeStatus_A = StatusA.statusId 
                                            LEFT OUTER JOIN tblStatus AS StatusB ON tblRecordDetail.proposeStatus_B = StatusB.statusId 
                                            LEFT OUTER JOIN tblStatus AS StatusC ON tblRecordDetail.proposeStatus_C = StatusC.statusId"

                    Dim RecordId As String = dataItem.GetDataKeyValue("recId").ToString()
                    e.DetailTableView.DataSource = GetDataTable((Convert.ToString(sqlStr & " WHERE tblRecordDetail.recId = '") & RecordId) + "'")
                    Exit Select
                End If
        End Select
    End Sub

    Private Function SearchAndFilterStr(ByVal s As String) As String
        Dim SqlStr As String = SqlStrOriginal & SqlStrOriginalWhere
        If Page.IsPostBack Then
            If s <> "" Then
                SqlStr = SqlStr & "AND recActNo LIKE '%" & s & "%' "
            End If
            If rcbDepartmentView.SelectedIndex = 0 Then
                SqlStr = SqlStr & SqlStrOriginalSort
            Else
                SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' " & SqlStrOriginalSort
            End If
        Else
            SqlStr = SqlStr & SqlStrOriginalSort
        End If

        Return SqlStr
    End Function

    Protected Sub btSearchBox_Click(sender As Object, e As EventArgs) Handles btSearchBox.Click
        If tbSearchKeyword.Text <> "" Then
            imgbClearKeyword.Visible = True
            'Dim s As String = tbSearchKeyword.Text.Trim()
            'ViewState("SelectCommand") = hfOriSelectComm.Value + hfOriWhereString.Value
            'ViewState("FilterExpression") = " empDowId LIKE '%" & s & "%' OR empName LIKE '%" & s & "%' OR empSurname LIKE '%" & s & "%' "
            'srcEmployee.FilterExpression = ViewState("FilterExpression")
        Else
            imgbClearKeyword.Visible = False
            'srcEmployee.SelectCommand = hfOriSelectComm.Value + hfOriWhereString.Value
            'srcEmployee.FilterExpression = ""
        End If

        rgRecordList.Rebind()
    End Sub
    Protected Sub imgbClearKeyword_Click(sender As Object, e As ImageClickEventArgs) Handles imgbClearKeyword.Click
        tbSearchKeyword.Text = ""
        imgbClearKeyword.Visible = False
        rgRecordList.Rebind()
    End Sub

    Private Sub CloseAdvanceSearch()
        pnNormalSearch.Visible = Not pnNormalSearch.Visible
        pnAdvanceSearchMenu.Visible = Not pnAdvanceSearchMenu.Visible
        pnAdvanceSearch.Visible = Not pnAdvanceSearch.Visible

        'clear textbox normal search
        tbSearchKeyword.Text = ""
        imgbClearKeyword.Visible = False
        rcbDepartmentView.SelectedIndex = 0

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

        Dim sqlStr As String = SqlStrOriginal & SqlStrOriginalWhere & SqlStrOriginalSort
        rgRecordList.DataSource = GetDataTable(sqlStr)
        rgRecordList.Rebind()
    End Sub

    Protected Sub btAdvanceSeach_Click(sender As Object, e As EventArgs) Handles btAdvanceSeach.Click
        CloseAdvanceSearch()
    End Sub
    Protected Sub CloseAdvSearch_Click(sender As Object, e As ImageClickEventArgs) Handles CloseObserve.Click, CloseRespon.Click, CloseFromTo.Click, CloseCombine.Click
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
        Dim sqlStr As String = SqlStrOriginal & SqlStrOriginalWhere & SqlStrOriginalSort
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
        Dim sqlStr As String = SqlStrOriginal & SqlStrOriginalWhere & SqlStrOriginalSort
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
        Dim idx1 As Integer = s.IndexOf(" ")
        Dim idx2 As Integer = s.IndexOf("  ")
        If idx2 < 0 Then If idx1 >= 0 Then s = s.Replace(" ", "  ")

        Dim SqlStr As String = SqlStrOriginal & "LEFT OUTER JOIN tblRecordOthEmpO ON tblRecord.recId = tblRecordOthEmpO.recId 
                                                 LEFT OUTER JOIN tblEmployee AS tblEmployeeOth ON tblEmployeeOth.empId = tblRecordOthEmpO.empIdOth "

        SqlStr = SqlStr & SqlStrOriginalWhere & "AND tblEmployee.empFullName LIKE '%" & s & "%' OR tblEmployeeOth.empFullName LIKE '%" & s & "%' "
        If rcbDepartmentView.SelectedIndex <> 0 Then
            SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' "
        End If

        SqlStr = SqlStr & SqlStrOriginalGroupBy
        SqlStr = SqlStr & SqlStrSearchSort

        Return SqlStr
    End Function
    Private Function SearchResponStr(ByVal s As String) As String
        Dim SqlStrGroupBy As String = "dbo.tblRecord.recId, dbo.tblRecord.timestamp, dbo.tblRecord.departId, dbo.tblDepartment.departName, dbo.tblRecord.recActNo, 
                                        dbo.tblRecord.recActMonth, dbo.tblRecord.recActYear, dbo.tblRecord.recActDate, dbo.tblRecord.recActTime, 
	                                    dbo.tblRecord.durationH, dbo.tblRecord.durationM, dbo.tblRecord.durationValue, 
	                                    dbo.tblRecord.empId, dbo.tblEmployee.empFullName, dbo.tblRecord.oEmpCount, dbo.tblRecord.noObserve"

        Dim SqlStr As String = "SELECT " & SqlStrGroupBy & ", CONVERT (VARCHAR(5), tblRecord.recActTime, 108) AS TimeHHMM FROM dbo.tblRecord 
                                INNER JOIN dbo.tblDepartment ON dbo.tblRecord.departId = dbo.tblDepartment.departId 
                                INNER JOIN dbo.tblEmployee ON dbo.tblRecord.empId = dbo.tblEmployee.empId "

        If rcbDepartmentView.SelectedIndex <> 0 Then
            SqlStr = SqlStr & SqlStrOriginalWhere & "AND (tblDepartment.departName = '" & rcbDepartmentView.Text & "') "
        End If

        Dim idx1 As Integer = s.IndexOf(" ")
        Dim idx2 As Integer = s.IndexOf("  ")
        If idx2 < 0 Then If idx1 >= 0 Then s = s.Replace(" ", "  ")
        SqlStr = SqlStr & "GROUP BY " & SqlStrGroupBy & " HAVING (dbo.tblRecord.recId IN 
                            (SELECT DISTINCT dbo.tblRecordDetail.recId FROM dbo.tblRecordDetail 
                            LEFT OUTER JOIN dbo.tblEmployee AS tblResponA ON dbo.tblRecordDetail.proposeRespPerson_A = tblResponA.empId 
                            LEFT OUTER JOIN dbo.tblEmployee AS tblResponB ON dbo.tblRecordDetail.proposeRespPerson_B = tblResponB.empId 
                            LEFT OUTER JOIN dbo.tblEmployee AS tblResponC ON dbo.tblRecordDetail.proposeRespPerson_C = tblResponC.empId 
                            WHERE (tblResponA.empFullName LIKE '%" & s & "%') OR (tblResponB.empFullName LIKE '%" & s & "%') OR (tblResponC.empFullName LIKE '%" & s & "%'))) "
        SqlStr = SqlStr & SqlStrSearchSort

        Return SqlStr
    End Function
    Private Function SearchDateRange(ByVal DateFrom As String, ByVal DateTo As String) As String
        Dim SqlStr As String = SqlStrOriginal & SqlStrOriginalWhere
        SqlStr = SqlStr & "AND recActDate >= '" & DateFrom & "' AND recActDate <= '" & DateTo & "' "

        If rcbDepartmentView.SelectedIndex <> 0 Then SqlStr = SqlStr & "AND tblDepartment.departName = '" & rcbDepartmentView.Text & "' "
        SqlStr = SqlStr & SqlStrSearchSort

        Return SqlStr
    End Function
    Private Function SearchCombineStr(ByVal s1 As String, ByVal s2 As String) As String
        Dim StrGroupBy As String = "dbo.tblRecord.recId, dbo.tblRecord.timestamp, dbo.tblRecord.departId, dbo.tblDepartment.departName, dbo.tblRecord.recActNo, 
                                        dbo.tblRecord.recActMonth, dbo.tblRecord.recActYear, dbo.tblRecord.recActDate, dbo.tblRecord.recActTime, 
	                                    dbo.tblRecord.durationH, dbo.tblRecord.durationM, dbo.tblRecord.durationValue, 
	                                    dbo.tblRecord.empId, dbo.tblEmployee.empFullName, dbo.tblRecord.oEmpCount, dbo.tblRecord.noObserve"

        Dim SqlStr As String = "SELECT " & StrGroupBy & ", CONVERT (VARCHAR(5), tblRecord.recActTime, 108) AS TimeHHMM FROM dbo.tblRecord 
                                INNER JOIN dbo.tblDepartment ON dbo.tblRecord.departId = dbo.tblDepartment.departId 
                                INNER JOIN dbo.tblEmployee ON dbo.tblRecord.empId = dbo.tblEmployee.empId "

        'join other observer if search observer
        If s1 <> "" Then
            SqlStr = SqlStr & "LEFT OUTER JOIN tblRecordOthEmpO ON tblRecord.recId = tblRecordOthEmpO.recId 
                               LEFT OUTER JOIN tblEmployee AS tblEmployeeOth ON tblEmployeeOth.empId = tblRecordOthEmpO.empIdOth "
        End If

        SqlStr = SqlStr & SqlStrOriginalWhere

        '-- Departmenr
        If rcbDepartmentView.SelectedIndex <> 0 Then
            SqlStr = SqlStr & "AND (tblDepartment.departName = '" & rcbDepartmentView.Text & "') "
        End If

        '-- Observer
        If s1 <> "" Then
            Dim idx1 As Integer = s1.IndexOf(" ")
            Dim idx2 As Integer = s1.IndexOf("  ")
            If idx2 < 0 Then If idx1 >= 0 Then s1 = s1.Replace(" ", "  ")
            SqlStr = SqlStr & "AND (tblEmployee.empFullName LIKE '%" & s1 & "%' OR tblEmployeeOth.empFullName LIKE '%" & s1 & "%') "
        End If

        '-- Date Rage
        If rdpFromCombine.SelectedDate IsNot Nothing And rdpToCombine.SelectedDate IsNot Nothing Then
            Dim DateFrom As Date = rdpFromCombine.SelectedDate
            Dim DateTo As Date = rdpToCombine.SelectedDate

            Dim UsaCulture As New CultureInfo("en-US")
            Dim usDateFrom As String = DateFrom.ToString("yyyy-MM-dd", UsaCulture)
            Dim usDateTo As String = DateTo.ToString("yyyy-MM-dd", UsaCulture)

            SqlStr = SqlStr & "AND recActDate >= '" & usDateFrom & "' AND recActDate <= '" & usDateTo & "' "
        End If

        'where tblRecordDetail
        Dim SqlStrDistinctDetail As String = "SELECT DISTINCT dbo.tblRecordDetail.recId FROM dbo.tblRecordDetail 
                            LEFT OUTER JOIN dbo.tblEmployee AS tblResponA ON dbo.tblRecordDetail.proposeRespPerson_A = tblResponA.empId 
                            LEFT OUTER JOIN dbo.tblEmployee AS tblResponB ON dbo.tblRecordDetail.proposeRespPerson_B = tblResponB.empId 
                            LEFT OUTER JOIN dbo.tblEmployee AS tblResponC ON dbo.tblRecordDetail.proposeRespPerson_C = tblResponC.empId "

        SqlStr = SqlStr & "GROUP BY " & StrGroupBy & " HAVING (dbo.tblRecord.recId IN (" & SqlStrDistinctDetail
        If s2 <> "" Or rcbCategoryCB.SelectedIndex > 0 Then SqlStr = SqlStr & "WHERE "

        '-- Respon person
        If s2 <> "" Then
            Dim idx1 As Integer = s2.IndexOf(" ")
            Dim idx2 As Integer = s2.IndexOf("  ")
            If idx2 < 0 Then If idx1 >= 0 Then s2 = s2.Replace(" ", "  ")
            SqlStr = SqlStr & "((tblResponA.empFullName LIKE '%" & s2 & "%') OR (tblResponB.empFullName LIKE '%" & s2 & "%') OR (tblResponC.empFullName LIKE '%" & s2 & "%')) "
        End If

        '-- Category/Sub Category/Failure Point
        If rcbCategoryCB.SelectedIndex > 0 Then
            If s2 <> "" Then SqlStr = SqlStr & "AND "
            If rcbCategorySubCB.SelectedIndex > 0 Then
                If rcbFailPointCB.SelectedIndex > 0 Then
                    '---search Category AND Sub Category AND Failure Point
                    SqlStr = SqlStr & "tblRecordDetail.category = '" & rcbCategoryCB.SelectedValue & "' AND tblRecordDetail.categorySub = '" & rcbCategorySubCB.SelectedValue & "' AND tblRecordDetail.failurePoint = '" & rcbFailPointCB.SelectedValue & "' "
                Else
                    '---search Category, all Sub Category
                    SqlStr = SqlStr & "tblRecordDetail.category = '" & rcbCategoryCB.SelectedValue & "' AND tblRecordDetail.categorySub = '" & rcbCategorySubCB.SelectedValue & "' "
                End If
            Else
                '---search Category only
                SqlStr = SqlStr & "tblRecordDetail.category = '" & rcbCategoryCB.SelectedValue & "' "
            End If
        End If

        SqlStr = SqlStr & ")) " & SqlStrSearchSort

        Return SqlStr
    End Function

    Private Sub rcbDepartmentView_DataBound(sender As Object, e As EventArgs) Handles rcbDepartmentView.DataBound
        Dim rcb As RadComboBox = sender
        rcb.Items.Insert(0, New RadComboBoxItem("Show All Department", "0"))
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
    Private Sub rcbDepartmentView_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbDepartmentView.SelectedIndexChanged
        ddlView()
    End Sub


    '-- GRID LIST --
    Private Sub rgRecordList_ItemCreated(sender As Object, e As GridItemEventArgs) Handles rgRecordList.ItemCreated
        IsRoleAllow = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN")
    End Sub

    Private Sub rgRecordList_PreRender(sender As Object, e As EventArgs) Handles rgRecordList.PreRender
        If Session("ExpandedRow") <> "" Then
            Dim idx As Integer = CInt(Session("ExpandedRow"))
            Session("ExpandedRow") = ""
            rgRecordList.MasterTableView.Items(idx).Expanded = True
        End If
    End Sub

    Private Sub rgRecordList_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgRecordList.ItemDataBound
        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim RecId = item.GetDataKeyValue("recId")

            '-- observComplete ==> 1000 = Not Active, 1001 = Recognition, 1002 = In-progress, 1003 = Complete
            Dim StatusSet() = {
                New With {.Ob = 1, .EnableA = False, .StatusA = 0, .CompletA = False, .EnableB = False, .StatusB = 0, .CompletB = False, .EnableC = False, .StatusC = 0, .CompletC = False, .observComplet = 1000},
                New With {.Ob = 2, .EnableA = False, .StatusA = 0, .CompletA = False, .EnableB = False, .StatusB = 0, .CompletB = False, .EnableC = False, .StatusC = 0, .CompletC = False, .observComplet = 1000},
                New With {.Ob = 3, .EnableA = False, .StatusA = 0, .CompletA = False, .EnableB = False, .StatusB = 0, .CompletB = False, .EnableC = False, .StatusC = 0, .CompletC = False, .observComplet = 1000},
                New With {.Ob = 4, .EnableA = False, .StatusA = 0, .CompletA = False, .EnableB = False, .StatusB = 0, .CompletB = False, .EnableC = False, .StatusC = 0, .CompletC = False, .observComplet = 1000},
                New With {.Ob = 5, .EnableA = False, .StatusA = 0, .CompletA = False, .EnableB = False, .StatusB = 0, .CompletB = False, .EnableC = False, .StatusC = 0, .CompletC = False, .observComplet = 1000},
                New With {.Ob = 6, .EnableA = False, .StatusA = 0, .CompletA = False, .EnableB = False, .StatusB = 0, .CompletB = False, .EnableC = False, .StatusC = 0, .CompletC = False, .observComplet = 1000}
            }

            'get Status Data
            Using connOb As New SqlConnection(ConnStr)
                connOb.Open()
                Dim strSql_getOb As String = "SELECT proposeEnable_A, proposeStatus_A, proposeComplete_A, proposeEnable_B, proposeStatus_B, proposeComplete_B, proposeEnable_C, proposeStatus_C, proposeComplete_C, observComplete FROM tblRecordDetail 
                                            WHERE (recId = @recId)"

                Dim commandOb As New SqlCommand(strSql_getOb, connOb) With {.CommandType = CommandType.Text}
                commandOb.Parameters.Add("@recId", SqlDbType.Int).Value = CInt(RecId)

                Dim DataRead As SqlDataReader
                DataRead = commandOb.ExecuteReader()
                Dim i As Integer = 0
                While DataRead.Read()
                    StatusSet(i).EnableA = DataRead("proposeEnable_A")
                    StatusSet(i).StatusA = DataRead("proposeStatus_A")
                    StatusSet(i).CompletA = DataRead("proposeComplete_A")

                    StatusSet(i).EnableB = DataRead("proposeEnable_B")
                    StatusSet(i).StatusB = DataRead("proposeStatus_B")
                    StatusSet(i).CompletB = DataRead("proposeComplete_B")

                    StatusSet(i).EnableC = DataRead("proposeEnable_C")
                    StatusSet(i).StatusC = DataRead("proposeStatus_C")
                    StatusSet(i).CompletC = DataRead("proposeComplete_C")

                    StatusSet(i).observComplet = DataRead("observComplete")
                    i = i + 1
                End While
            End Using

            If TypeOf e.Item Is GridDataItem AndAlso e.Item.OwnerTableView.Name = "ParentGrid" Then
                Dim stOb As Image
                For Each s In StatusSet
                    stOb = item.FindControl("imbObserve" & s.Ob.ToString)
                    Select Case s.observComplet
                        Case 1000
                            stOb.ImageUrl = "~/Images/status-blank-20.png"
                            stOb.ToolTip = ""
                            stOb.Visible = False
                        Case 1001
                            stOb.ImageUrl = "~/Images/status-blue-" & s.Ob.ToString & "-20.png"
                            stOb.ToolTip = "Recognition"
                        Case 1002
                            stOb.ImageUrl = "~/Images/status-orange-" & s.Ob.ToString & "-20.png"
                            stOb.ToolTip = "In-progress"
                        Case 1003
                            stOb.ImageUrl = "~/Images/status-green-" & s.Ob.ToString & "-20.png"
                            stOb.ToolTip = "Complete"
                    End Select
                Next
                
                'Delete
                Dim employee As New cEmployee
                Dim userIdenId As Integer = employee.FindEmployeeIdbyUsername(User.Identity.Name)
                Dim btDel As ImageButton = item.FindControl("imbDelete")
                btDel.CommandArgument = RecId
                btDel.Visible = cbShow_Del.Checked
                Dim ownerId As Integer = DataBinder.Eval(e.Item.DataItem, "empId")
                Dim logicChk As Boolean = IsRoleAllow Or (ownerId = userIdenId)
                If Not logicChk Then
                    btDel.ImageUrl = "~/Images/bt_delete-16h16-gray.png"
                    btDel.Enabled = False
                End If

            ElseIf TypeOf e.Item Is GridDataItem AndAlso e.Item.OwnerTableView.Name = "Observe" Then
                Dim citem As GridDataItem = TryCast(e.Item, GridDataItem)

                Dim ImageUrl As New cImage
                Dim ActionStatusA As Image = citem.FindControl("imgActionA")
                Dim ProposeStatusA As HiddenField = citem.FindControl("hfProposeStatus_A")
                Dim StatusCodeA As Integer = CInt(ProposeStatusA.Value)
                ActionStatusA.ImageUrl = ImageUrl.getImage_msg(StatusCodeA, 0)
                ActionStatusA.ToolTip = ImageUrl.getImage_msgTooltip

                Dim ActionStatusB As Image = citem.FindControl("imgActionB")
                Dim ProposeStatusB As HiddenField = citem.FindControl("hfProposeStatus_B")
                Dim StatusCodeB As Integer = CInt(ProposeStatusB.Value)
                ActionStatusB.ImageUrl = ImageUrl.getImage_msg(StatusCodeB, 0)
                If ActionStatusB.ImageUrl = "~/Images/status-blank-20.png" Then ActionStatusB.Visible = False
                ActionStatusB.ToolTip = ImageUrl.getImage_msgTooltip

                Dim ActionStatusC As Image = citem.FindControl("imgActionC")
                Dim ProposeStatusC As HiddenField = citem.FindControl("hfProposeStatus_C")
                Dim StatusCodeC As Integer = CInt(ProposeStatusC.Value)
                ActionStatusC.ImageUrl = ImageUrl.getImage_msg(StatusCodeC, 0)
                If ActionStatusC.ImageUrl = "~/Images/status-blank-20.png" Then ActionStatusC.Visible = False
                ActionStatusC.ToolTip = ImageUrl.getImage_msgTooltip

                Dim childStatus As Image = citem.FindControl("imbObserve")
                Dim ObservComplete As HiddenField = citem.FindControl("hfObservComplete")
                Dim statusCode As Integer = CInt(ObservComplete.Value)
                Select Case statusCode
                    Case 1001
                        childStatus.ImageUrl = "~/Images/status-blue-0-20.png"
                        childStatus.ToolTip = "Recognition"
                    Case 1002
                        childStatus.ImageUrl = "~/Images/status-orange-0-20.png"
                        childStatus.ToolTip = "In-progress"
                    Case 1003
                        childStatus.ImageUrl = "~/Images/status-green-0-20.png"
                        childStatus.ToolTip = "Complete"
                    Case Else
                        childStatus.ImageUrl = "~/Images/status-blank-20.png"
                        childStatus.ToolTip = ""
                End Select
            End If
        End If

        '-- EditForm
        If TypeOf e.Item Is GridEditableItem AndAlso e.Item.IsInEditMode Then
            '-- EditForm Global
            Dim RecId As Integer
            Dim employee As New cEmployee
            Dim userIdenId As Integer = employee.FindEmployeeIdbyUsername(User.Identity.Name)

            If e.Item.OwnerTableView.Name = "ParentGrid" Then
                Dim pitem As GridEditableItem = TryCast(e.Item, GridEditableItem)
                RecId = pitem.GetDataKeyValue("recId")

                '-- EditForm Parent
                Dim srcDepartment As SqlDataSource = pitem.FindControl("srcDepartment")
                Dim eDepartment As RadComboBox = pitem.FindControl("rcbDepartment")
                eDepartment.DataSource = srcDepartment
                eDepartment.SelectedValue = DataBinder.Eval(e.Item.DataItem, "departId")
                eDepartment.DataBind()

                Dim eDocDate As RadDatePicker = pitem.FindControl("rdpDocDate")
                eDocDate.SelectedDate = DataBinder.Eval(e.Item.DataItem, "recActDate")
                Dim Doctime As TimeSpan = DataBinder.Eval(e.Item.DataItem, "recActTime")
                Dim eTimeHH As RadComboBox = pitem.FindControl("rcbTimeHH")
                eTimeHH.SelectedValue = Doctime.Hours.ToString
                Dim eTimeMM As RadComboBox = pitem.FindControl("rcbTimeMM")
                eTimeMM.SelectedValue = Doctime.Minutes.ToString
                Dim eDurationH As RadComboBox = pitem.FindControl("rcbDurationH")
                eDurationH.SelectedValue = DataBinder.Eval(e.Item.DataItem, "durationH")
                Dim eDurationM As RadComboBox = pitem.FindControl("rcbDurationM")
                eDurationM.SelectedValue = DataBinder.Eval(e.Item.DataItem, "durationM")

                'get Other Observe
                Dim eObservBox As RadAutoCompleteBox = pitem.FindControl("racObservBox")
                eObservBox.Enabled = False

                'get Other Employee
                Using conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim strSql As String = "SELECT tblRecordOthEmpO.*, tblEmployee.empDowId, tblEmployee.empName, tblEmployee.empSurname, tblEmployee.empFullName, tblEmployee.empDisplay, tblEmployee.empEmail 
                                            FROM tblRecordOthEmpO INNER JOIN tblEmployee ON tblRecordOthEmpO.empIdOth = tblEmployee.empId WHERE (recId = @recId) ORDER BY recItem DESC"

                    Dim command As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
                    command.Parameters.Add("@recId", SqlDbType.Int).Value = CInt(RecId)

                    Dim DataRead As SqlDataReader
                    DataRead = command.ExecuteReader()
                    While DataRead.Read()
                        Dim othFullName As String = DataRead("empFullName")
                        Dim othValue As String = DataRead("empIdOth")
                        eObservBox.Entries.Insert(0, New AutoCompleteBoxEntry(othFullName, othValue))
                    End While
                End Using

                Dim eOtherObCount As HiddenField = pitem.FindControl("hfOtherObCount")
                eOtherObCount.Value = eObservBox.Entries.Count

                '## enable/disable Update button
                Dim ownerId As Integer = DataBinder.Eval(e.Item.DataItem, "empId")
                'Dim logicChk As Boolean = IsRoleAllow Or ownerId = userIdenId      'ยกเลิก เปลี่ยนเป็นไม่ให้เจ้าของ (owner) ลบได้ @6/3/2017
                Dim logicChk As Boolean = IsRoleAllow
                Dim btEditOthObserve As ImageButton = pitem.FindControl("editOthObserve")
                btEditOthObserve.Visible = logicChk
                Dim pnUpdateButton As Panel = pitem.FindControl("pnUpdateButtonMaster")
                pnUpdateButton.Visible = logicChk
                Dim imbBack As ImageButton = pitem.FindControl("imbBack")
                imbBack.Visible = Not logicChk

            ElseIf e.Item.OwnerTableView.Name = "Observe" Then
                '-- EditForm Child
                Dim citem As GridEditableItem = TryCast(e.Item, GridEditableItem)
                Dim Id = citem.GetDataKeyValue("detailId")
                RecId = DataBinder.Eval(e.Item.DataItem, "recId")

                Dim eTitle As TextBox = citem.FindControl("tbTitle")
                eTitle.Text = DataBinder.Eval(e.Item.DataItem, "title")

                Dim eCategory As RadComboBox = citem.FindControl("rcbCategory")
                Dim srcCategory As SqlDataSource = citem.FindControl("srcCategory")
                eCategory.DataSource = srcCategory
                eCategory.SelectedValue = DataBinder.Eval(e.Item.DataItem, "category")
                eCategory.DataBind()

                Dim eCategorySub As RadComboBox = citem.FindControl("rcbCategorySub")
                Dim srcCategorySub As SqlDataSource = citem.FindControl("srcCategorySub")
                eCategorySub.DataSource = srcCategorySub
                eCategorySub.SelectedValue = DataBinder.Eval(e.Item.DataItem, "categorySub")
                eCategorySub.DataBind()

                Dim eFailurePoint As RadComboBox = citem.FindControl("rcbFailurePoint")
                Dim srcFailurePoint As SqlDataSource = citem.FindControl("srcFailurePoint")
                If DataBinder.Eval(e.Item.DataItem, "failurePoint") <> 0 Then
                    eFailurePoint.DataSource = srcFailurePoint
                    eFailurePoint.SelectedValue = DataBinder.Eval(e.Item.DataItem, "failurePoint")
                End If
                eFailurePoint.DataBind()

                Dim eEquipment As TextBox = citem.FindControl("tbEquipment")
                If DataBinder.Eval(e.Item.DataItem, "equipment") IsNot DBNull.Value Then
                    eEquipment.Text = DataBinder.Eval(e.Item.DataItem, "equipment")
                End If

                Dim eChkHRO As CheckBox = citem.FindControl("chkHRO")
                Dim eChk2Eye As CheckBox = citem.FindControl("chk2Eye")
                Dim eChkRecognition As CheckBox = citem.FindControl("chkRecognition")
                eChkHRO.Checked = DataBinder.Eval(e.Item.DataItem, "IsHRO")
                eChk2Eye.Checked = DataBinder.Eval(e.Item.DataItem, "secondEye")
                eChkRecognition.Checked = DataBinder.Eval(e.Item.DataItem, "recognition")

                Dim eChkHROop1 As CheckBox = citem.FindControl("chkHROop1")
                Dim eChkHROop2 As CheckBox = citem.FindControl("chkHROop2")
                Dim eChkHROop3 As CheckBox = citem.FindControl("chkHROop3")
                Dim eChkHROop4 As CheckBox = citem.FindControl("chkHROop4")
                Dim eChkHROop5 As CheckBox = citem.FindControl("chkHROop5")
                eChkHROop1.Checked = DataBinder.Eval(e.Item.DataItem, "hroChk1")
                eChkHROop2.Checked = DataBinder.Eval(e.Item.DataItem, "hroChk2")
                eChkHROop3.Checked = DataBinder.Eval(e.Item.DataItem, "hroChk3")
                eChkHROop4.Checked = DataBinder.Eval(e.Item.DataItem, "hroChk4")
                eChkHROop5.Checked = DataBinder.Eval(e.Item.DataItem, "hroChk5")

                Dim eObserveType As RadComboBox = citem.FindControl("rcbObserveType")
                eObserveType.SelectedValue = DataBinder.Eval(e.Item.DataItem, "observType")
                If eObserveType.SelectedValue = "1" Then
                    Dim eContractor As RadComboBox = citem.FindControl("rcbContractor")
                    Dim srcContractor As SqlDataSource = citem.FindControl("srcContractor")
                    eContractor.DataSource = srcContractor
                    eContractor.SelectedValue = DataBinder.Eval(e.Item.DataItem, "contractor")
                    eContractor.Visible = True
                    eContractor.DataBind()
                End If

                Dim picCount As Integer = DataBinder.Eval(e.Item.DataItem, "pictureCount")
                Dim picPanel As Panel = citem.FindControl("pnShowImage")
                If picCount > 0 Then
                    picPanel.Visible = True
                    Dim PictureList As DataList = citem.FindControl("PictureList")
                    Dim srcPicture As SqlDataSource = citem.FindControl("srcPicture")

                    srcPicture.SelectParameters("recId").DefaultValue = RecId
                    srcPicture.SelectParameters("observeItem").DefaultValue = (CInt(DataBinder.Eval(e.Item.DataItem, "observItem")) - 1).ToString
                    PictureList.DataSource = srcPicture
                    PictureList.DataBind()
                Else
                    picPanel.Visible = False
                End If

                Dim eDepartment As TextBox = citem.FindControl("tbDescription")
                eDepartment.Text = DataBinder.Eval(e.Item.DataItem, "description")

                '---- Action Propose #1
                Dim eActionA As TextBox = citem.FindControl("tbActionA")
                eActionA.Text = DataBinder.Eval(e.Item.DataItem, "proposeAction_A")
                Dim ResponIdA As Integer = DataBinder.Eval(e.Item.DataItem, "proposeRespPerson_A")
                Dim eResponA As TextBox = citem.FindControl("tbResponA")
                Dim emp As New cEmployee
                emp.FindEmployeeName(ResponIdA)
                eResponA.Text = emp.EmployeeFullName

                Dim eStatusA As Image = citem.FindControl("imgStatusA")
                Dim StatusA As Integer = DataBinder.Eval(e.Item.DataItem, "proposeStatus_A")
                Dim ImageUrl As New cImage
                eStatusA.ImageUrl = ImageUrl.getImage_msg(StatusA, 0)
                Dim eStatusDescA As Label = citem.FindControl("lbStatusA")
                eStatusDescA.Text = DataBinder.Eval(e.Item.DataItem, "StatusDescA")
                If StatusA = 1001 Or StatusA = 1003 Then
                    'Recognition or Complete
                    eActionA.Enabled = False
                End If

                '---- Action Propose #2
                Dim panelB As Panel = citem.FindControl("pnResponB")
                If DataBinder.Eval(e.Item.DataItem, "proposeEnable_B") Then
                    panelB.Visible = True
                    Dim eActionB As TextBox = citem.FindControl("tbActionB")
                    eActionB.Text = DataBinder.Eval(e.Item.DataItem, "proposeAction_B")
                    Dim ResponIdB As Integer = DataBinder.Eval(e.Item.DataItem, "proposeRespPerson_B")
                    Dim eResponB As TextBox = citem.FindControl("tbResponB")
                    emp.FindEmployeeName(ResponIdB)
                    eResponB.Text = emp.EmployeeFullName

                    Dim eStatusB As Image = citem.FindControl("imgStatusB")
                    Dim StatusB As Integer = DataBinder.Eval(e.Item.DataItem, "proposeStatus_B")
                    eStatusB.ImageUrl = ImageUrl.getImage_msg(StatusB, 0)
                    Dim eStatusDescB As Label = citem.FindControl("lbStatusB")
                    eStatusDescB.Text = DataBinder.Eval(e.Item.DataItem, "StatusDescB")
                    If StatusB = 1001 Or StatusB = 1003 Then
                        'Recognition or Complete
                        eActionB.Enabled = False
                    End If
                End If

                '---- Action Propose #3
                Dim panelC As Panel = citem.FindControl("pnResponC")
                If DataBinder.Eval(e.Item.DataItem, "proposeEnable_C") Then
                    panelC.Visible = True
                    Dim eActionC As TextBox = citem.FindControl("tbActionC")
                    eActionC.Text = DataBinder.Eval(e.Item.DataItem, "proposeAction_C")
                    Dim ResponIdC As Integer = DataBinder.Eval(e.Item.DataItem, "proposeRespPerson_C")
                    Dim eResponC As TextBox = citem.FindControl("tbResponC")
                    emp.FindEmployeeName(ResponIdC)
                    eResponC.Text = emp.EmployeeFullName

                    Dim eStatusC As Image = citem.FindControl("imgStatusC")
                    Dim StatusC As Integer = DataBinder.Eval(e.Item.DataItem, "proposeStatus_C")
                    eStatusC.ImageUrl = ImageUrl.getImage_msg(StatusC, 0)
                    Dim eStatusDescC As Label = citem.FindControl("lbStatusC")
                    eStatusDescC.Text = DataBinder.Eval(e.Item.DataItem, "StatusDescC")
                    If StatusC = 1001 Or StatusC = 1003 Then
                        'Recognition or Complete
                        eActionC.Enabled = False
                    End If
                End If

                '## enable/disable Update button panel
                Dim ownerId As Integer = DataBinder.Eval(e.Item.DataItem, "empId")
                Dim logicChk As Boolean = IsRoleAllow Or (ownerId = userIdenId)
                Dim pnUpdateButton As Panel = citem.FindControl("pnUpdateButtonDetail")
                pnUpdateButton.Visible = logicChk
                'Dim imbBack As ImageButton = pitem.FindControl("imbBack")
                'imbBack.Visible = Not logicChk

                Dim btChangeRecog As Button = citem.FindControl("btChangeRecog")
                Dim pnDelete As panel = citem.FindControl("pnDelete")
                If not eChkRecognition.Checked Then btChangeRecog.Visible = IsRoleAllow                
                
                Dim parentItem = CType(e.Item.OwnerTableView.ParentItem, GridDataItem)
                'Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
                Dim noObsParent As Integer = CInt(parentItem.Item("noObserve").Text)
                pnDelete.Enabled = logicChk And (noObsParent > 1)
            End If
        End If
    End Sub

    Private Sub rgRecordList_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgRecordList.ItemCommand
        If e.CommandName = RadGrid.ExpandCollapseCommandName Then
            Dim item As GridItem
            For Each item In e.Item.OwnerTableView.Items
                If item.Expanded AndAlso Not item Is e.Item Then
                    item.Expanded = False
                End If
            Next item
        End If

        If e.CommandName = "Change" Then
            If TypeOf e.Item Is GridEditableItem And e.Item.OwnerTableView.Name = "Observe" Then
                Dim eitem As GridEditableItem = DirectCast(e.Item, GridEditableItem)
                Dim detailId = CInt(eitem.GetDataKeyValue("detailId"))
                Dim recId = CInt(eitem.GetDataKeyValue("recId"))

                ChangeToRecognition(detailId, recId, eitem.ItemIndex)
            End If
        End If
    End Sub

    Private Sub rgRecordList_UpdateCommand(sender As Object, e As GridCommandEventArgs) Handles rgRecordList.UpdateCommand
        If e.CommandName = RadGrid.UpdateCommandName Then
            If TypeOf e.Item Is GridEditFormItem And e.Item.OwnerTableView.Name = "ParentGrid" Then
                Dim eitem As GridEditFormItem = DirectCast(e.Item, GridEditFormItem)
                Dim RecId = eitem.GetDataKeyValue("recId")
                Dim lbUpdateInfo As Label = eitem.FindControl("lbUpdateInfo") : lbUpdateInfo.Text = ""

                Dim eDepartment As RadComboBox = eitem.FindControl("rcbDepartment")
                Dim eDocDate As RadDatePicker = eitem.FindControl("rdpDocDate")
                Dim eTimeHH As RadComboBox = eitem.FindControl("rcbTimeHH")
                Dim eTimeMM As RadComboBox = eitem.FindControl("rcbTimeMM")
                Dim eDurationH As RadComboBox = eitem.FindControl("rcbDurationH")
                Dim eDurationM As RadComboBox = eitem.FindControl("rcbDurationM")
                Dim eObservBox As RadAutoCompleteBox = eitem.FindControl("racObservBox")

                Dim DurationValue As Integer = (60 * CInt(eDurationH.SelectedValue)) + CInt(eDurationM.SelectedValue)
                If DurationValue <> 0 Then
                    Dim StrUpd As String = "UPDATE tblRecord SET timestamp = @timestamp, departId = @departId, recActMonth = @recActMonth, recActYear  = @recActYear, recActDate = @recActDate, recActTime = @recActTime, 
                                        durationH = @durationH, durationM = @durationM, durationValue = @durationValue, oEmpCount = @oEmpCount WHERE recId = @recId"

                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim command As New SqlCommand(StrUpd, conn)
                    command.Parameters.Add("@recId", SqlDbType.Int).Value = RecId

                    'prepare othObs
                    Dim strIns As String = ""
                    Dim rItem As Integer = 0
                    Dim racObservBox As RadAutoCompleteBox = eitem.FindControl("racObservBox")
                    If racObservBox.Enabled Then
                        If racObservBox.Entries.Count > 0 Then
                            Dim iCount As Integer = 1
                            While iCount <= racObservBox.Entries.Count
                                If racObservBox.Entries.Item(iCount - 1).Value.ToString <> "" Then
                                    rItem = rItem + 1
                                    If rItem = 1 Then
                                        strIns = strIns & "INSERT INTO tblRecordOthEmpO(recId, recItem, empIdOth) SELECT " & RecId.ToString & ", 1, " & racObservBox.Entries.Item(0).Value.ToString
                                    Else
                                        strIns = strIns & " UNION ALL SELECT " & RecId.ToString & ", " & rItem.ToString & ", " & racObservBox.Entries.Item(iCount - 1).Value.ToString
                                    End If
                                End If
                                iCount = iCount + 1
                            End While
                        End If
                    Else
                        rItem = eObservBox.Entries.Count
                    End If

                    command.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = Now
                    command.Parameters.Add("@departId", SqlDbType.Int).Value = eDepartment.SelectedValue
                    command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = CDate(eDocDate.SelectedDate).Month
                    command.Parameters.Add("@recActYear", SqlDbType.Int).Value = CDate(eDocDate.SelectedDate).Year
                    command.Parameters.Add("@recActDate", SqlDbType.Date).Value = eDocDate.SelectedDate
                    command.Parameters.Add("@recActTime", SqlDbType.Time).Value = eTimeHH.SelectedValue & ":" & eTimeMM.SelectedValue
                    command.Parameters.Add("@durationH", SqlDbType.Int).Value = CInt(eDurationH.SelectedValue)
                    command.Parameters.Add("@durationM", SqlDbType.Int).Value = CInt(eDurationM.SelectedValue)
                    command.Parameters.Add("@durationValue", SqlDbType.Int).Value = (60 * CInt(eDurationH.SelectedValue)) + CInt(eDurationM.SelectedValue)
                    command.Parameters.Add("@oEmpCount", SqlDbType.Int).Value = rItem

                    Dim result As Integer = command.ExecuteNonQuery()

                    'update racObservBox
                    If racObservBox.Enabled Then
                        'delete all item
                        deleteOthObservers(RecId)
                        'delete mail list @tblSendEmail

                        'insert new items
                        If racObservBox.Entries.Count <> 0 Then
                            'MsgBox(RecId & ", " & racObservBox.Entries.Count.ToString & ", " & strIns)
                            '-- Insert Command all item
                            Dim connIns As New SqlConnection(ConnStr)
                            Dim commandIns As New SqlCommand(strIns, connIns)
                            connIns.Open()
                            commandIns.ExecuteNonQuery()
                            connIns.Close()

                            '-- Insert Command @tblSendEmail

                        End If
                    End If
                Else
                    lbUpdateInfo.Text = "Please check duration value."
                    e.Canceled = True
                End If
            End If

            If TypeOf e.Item Is GridEditFormItem And e.Item.OwnerTableView.Name = "Observe" Then
                Dim citem As GridEditFormItem = DirectCast(e.Item, GridEditFormItem)
                Dim Id = citem.GetDataKeyValue("detailId")

                Dim eTitle As TextBox = citem.FindControl("tbTitle")
                Dim eCategory As RadComboBox = citem.FindControl("rcbCategory")
                Dim eCategorySub As RadComboBox = citem.FindControl("rcbCategorySub")
                Dim eFailurePoint As RadComboBox = citem.FindControl("rcbFailurePoint")
                Dim eEquipment As TextBox = citem.FindControl("tbEquipment")
                Dim eHROop1 As CheckBox = citem.FindControl("chkHROop1")
                Dim eHROop2 As CheckBox = citem.FindControl("chkHROop2")
                Dim eHROop3 As CheckBox = citem.FindControl("chkHROop3")
                Dim eHROop4 As CheckBox = citem.FindControl("chkHROop4")
                Dim eHROop5 As CheckBox = citem.FindControl("chkHROop5")
                Dim eSecondEye As CheckBox = citem.FindControl("chk2Eye")
                Dim eObserveType As RadComboBox = citem.FindControl("rcbObserveType")
                Dim eContractor As RadComboBox = citem.FindControl("rcbContractor")
                Dim eDescription As TextBox = citem.FindControl("tbDescription")
                Dim eActionA As TextBox = citem.FindControl("tbActionA")
                Dim ePnResponB As Panel = citem.FindControl("pnResponB")
                Dim ePnResponC As Panel = citem.FindControl("pnResponC")

                '-- validate
                Dim IsValid As Boolean = True
                Dim errMsg As String = "Please check "
                If eTitle.Text = "" Then IsValid = False : errMsg = errMsg & "'Title', "
                If eCategorySub.SelectedIndex = 0 Or eCategorySub.SelectedIndex = -1 Then IsValid = False : errMsg = errMsg & "'Sub Category', "
                If eFailurePoint.SelectedIndex = 0 Or eFailurePoint.SelectedIndex = -1 Then IsValid = False : errMsg = errMsg & "'Failure Point', "
                If eDescription.Text = "" Then IsValid = False : errMsg = errMsg & "'Description', "
                If eActionA.Text = "" Then IsValid = False : errMsg = errMsg & "'Propose Action #1', "
                Dim lbUpdateInfo As Label = citem.FindControl("lbUpdateInfo")

                If IsValid Then
                    Dim StrUpd As String = "UPDATE tblRecordDetail SET title = @title, category = @category, categorySub = @categorySub, failurePoint = @failurePoint, equipment = @equipment, IsHRO = @IsHRO, 
                                            hroChk1 = @hroChk1, hroChk2 = @hroChk2, hroChk3 = @hroChk3, hroChk4 = @hroChk4, hroChk5 = @hroChk5, secondEye = @secondEye, observType = @observType, contractor = @contractor, 
                                            description = @description, proposeAction_A = @proposeAction_A, proposeAction_B = @proposeAction_B, proposeAction_C = @proposeAction_C WHERE detailId = @detailId"

                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim command As New SqlCommand(StrUpd, conn)
                    command.Parameters.Add("@detailId", SqlDbType.Int).Value = Id

                    command.Parameters.Add("@title", SqlDbType.NVarChar).Value = eTitle.Text
                    command.Parameters.Add("@category", SqlDbType.Int).Value = eCategory.SelectedValue
                    command.Parameters.Add("@categorySub", SqlDbType.Int).Value = eCategorySub.SelectedValue
                    command.Parameters.Add("@failurePoint", SqlDbType.Int).Value = eFailurePoint.SelectedValue
                    'If eCategorySub.Text <> "" Then command.Parameters.Add("@categorySub", SqlDbType.Int).Value = eCategorySub.SelectedValue Else command.Parameters.Add("@categorySub", SqlDbType.Int).Value = DBNull.Value
                    'If eFailurePoint.Text <> "" Then command.Parameters.Add("@failurePoint", SqlDbType.Int).Value = eFailurePoint.SelectedValue Else command.Parameters.Add("@failurePoint", SqlDbType.Int).Value = DBNull.Value

                    command.Parameters.Add("@equipment", SqlDbType.NVarChar).Value = eEquipment.Text
                    command.Parameters.Add("@IsHRO", SqlDbType.Bit).Value = eHROop1.Checked Or eHROop2.Checked Or eHROop3.Checked Or eHROop4.Checked Or eHROop5.Checked
                    command.Parameters.Add("@hroChk1", SqlDbType.Bit).Value = eHROop1.Checked
                    command.Parameters.Add("@hroChk2", SqlDbType.Bit).Value = eHROop2.Checked
                    command.Parameters.Add("@hroChk3", SqlDbType.Bit).Value = eHROop3.Checked
                    command.Parameters.Add("@hroChk4", SqlDbType.Bit).Value = eHROop4.Checked
                    command.Parameters.Add("@hroChk5", SqlDbType.Bit).Value = eHROop5.Checked
                    command.Parameters.Add("@secondEye", SqlDbType.Bit).Value = eSecondEye.Checked

                    command.Parameters.Add("@observType", SqlDbType.Int).Value = eObserveType.SelectedValue
                    command.Parameters.Add("@description", SqlDbType.NVarChar).Value = eDescription.Text
                    If eObserveType.SelectedValue = "1" Then command.Parameters.Add("@contractor", SqlDbType.Int).Value = eContractor.SelectedValue Else command.Parameters.Add("@contractor", SqlDbType.Int).Value = 0
                    command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = eActionA.Text

                    If ePnResponB.Visible Then
                        Dim eActionB As TextBox = citem.FindControl("tbActionB")
                        If eActionB.Text = "" Then IsValid = False : errMsg = errMsg & "'Propose Action #2', "
                        command.Parameters.Add("@proposeAction_B", SqlDbType.NVarChar).Value = eActionB.Text
                    Else
                        command.Parameters.Add("@proposeAction_B", SqlDbType.NVarChar).Value = DBNull.Value
                    End If

                    If ePnResponC.Visible Then
                        Dim eActionC As TextBox = citem.FindControl("tbActionC")
                        If eActionC.Text = "" Then IsValid = False : errMsg = errMsg & "'Propose Action #3', "
                        command.Parameters.Add("@proposeAction_C", SqlDbType.NVarChar).Value = eActionC.Text
                    Else
                        command.Parameters.Add("@proposeAction_C", SqlDbType.NVarChar).Value = DBNull.Value
                    End If

                    ' update if valid
                    If IsValid Then
                        Dim result As Integer = command.ExecuteNonQuery()
                    Else
                        lbUpdateInfo.Text = errMsg.Substring(0, errMsg.Length - 2)
                        e.Canceled = True
                    End If
                Else
                    lbUpdateInfo.Text = errMsg.Substring(0, errMsg.Length - 2)
                    e.Canceled = True
                End If
            End If
        End If
    End Sub

    Private Sub rgRecordList_DeleteCommand(sender As Object, e As GridCommandEventArgs) Handles rgRecordList.DeleteCommand
        If e.CommandName = RadGrid.DeleteCommandName Then
            If TypeOf e.Item Is GridEditableItem And e.Item.OwnerTableView.Name = "Observe" Then
                Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)

                Dim detailId = CInt(item.GetDataKeyValue("detailId"))
                Dim recId = CInt(item.GetDataKeyValue("recId"))
                Dim totalObs As Integer = e.Item.OwnerTableView.Items.Count

                If totalObs > 1 Then
                    '-- delete item by detailId
                    Dim delStr As String = "DELETE FROM tblRecordDetail WHERE (detailId = @detailId)"
                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim command As New SqlCommand(delStr, conn)
                    command.Parameters.Add("@detailId", SqlDbType.Int).Value = detailId
                    command.ExecuteNonQuery()
                    conn.Close()

                    '-- update noObserve @tblRecord
                    Dim updStr As String = "UPDATE tblRecord SET noObserve = @noObserve WHERE recId = @recId"
                    Dim conn2 As New SqlConnection(ConnStr)
                    conn2.Open()
                    Dim command2 As New SqlCommand(updStr, conn2)
                    command2.Parameters.Add("@noObserve", SqlDbType.Int).Value = totalObs - 1
                    command2.Parameters.Add("@recId", SqlDbType.Int).Value = recId
                    command2.ExecuteNonQuery()
                    conn2.Close()

                    '-- re-index                
                    Dim selSql As String = "SELECT detailId FROM tblRecordDetail WHERE (recId = @recId) ORDER BY recId"
                    Dim updSql As String = "UPDATE tblRecordDetail SET observItem = @observItem WHERE detailId = @detailId"

                    Dim selConn As New SqlConnection(ConnStr)
                    selConn.Open()
                    Using commandSel As New SqlCommand(selSql, selConn) With {.CommandType = CommandType.Text}
                        commandSel.Parameters.Add("@recId", SqlDbType.Int).Value = recId

                        Dim updConn As New SqlConnection(ConnStr)
                        updConn.Open()
                        Using commandUpd As New SqlCommand(updSql, updConn) With {.CommandType = CommandType.Text}
                            Dim para_detailId As SqlParameter = commandUpd.Parameters.Add("@detailId", SqlDbType.Int)          'WHERE
                            Dim para_observItem As SqlParameter = commandUpd.Parameters.Add("@observItem", SqlDbType.Int)      'VALUE

                            Dim DataRead As SqlDataReader
                            Dim listNoCount As Integer = 1
                            DataRead = commandSel.ExecuteReader()
                            While DataRead.Read()
                                para_detailId.Value = CInt(DataRead("detailId"))
                                para_observItem.Value = listNoCount
                                listNoCount = listNoCount + 1
                                commandUpd.ExecuteNonQuery()
                            End While
                        End Using
                        updConn.Close()
                    End Using
                    selConn.Close()                    

                    '-- update screen
                    Dim parentItem = CType(e.Item.OwnerTableView.ParentItem, GridDataItem)                    
                    Session("ExpandedRow") = (parentItem.ItemIndex).ToString
                    item.FireCommandEvent("Cancel", String.Empty)
                    rgRecordList.MasterTableView.Rebind()
                End If
            End If
        End If
    End Sub

    Private Sub deleteOthObservers(ByVal recId As String)
        Dim delStr As String = "DELETE FROM tblRecordOthEmpO WHERE (recId = @recId)"
        Dim conn As New SqlConnection(ConnStr)

        conn.Open()
        Dim command As New SqlCommand(delStr, conn)
        command.Parameters.Add("@recId", SqlDbType.Int).Value = recId
        command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Protected Sub btCancelRecord_Click(sender As Object, e As ImageClickEventArgs)
        Dim btSender As ImageButton = sender
        Dim recordId As Integer = CInt(btSender.CommandArgument)

        '-- Delete Record Document
        Dim delStr As String = "DELETE FROM tblRecord WHERE (recId = @recId)"
        Dim conn As New SqlConnection(ConnStr)

        conn.Open()
        Dim command As New SqlCommand(delStr, conn)
        command.Parameters.Add("@recId", SqlDbType.Int).Value = recordId
        command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Protected Sub rcbCategory_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Dim Category As RadComboBox = sender
        Dim citem As GridEditableItem = Category.NamingContainer

        Dim eCategory As RadComboBox = citem.FindControl("rcbCategory")

        Dim eCategorySub As RadComboBox = citem.FindControl("rcbCategorySub")
        Dim srcCategorySub As SqlDataSource = citem.FindControl("srcCategorySub")
        srcCategorySub.SelectParameters("CateId").DefaultValue = eCategory.SelectedValue
        eCategorySub.DataSource = srcCategorySub
        eCategorySub.DataBind()

        Dim eFailurePoint As RadComboBox = citem.FindControl("rcbFailurePoint")
        Dim srcFailurePoint As SqlDataSource = citem.FindControl("srcFailurePoint")
        If eCategorySub.SelectedValue <> "" Then
            srcFailurePoint.SelectParameters("catesubId").DefaultValue = eCategorySub.SelectedValue
            eFailurePoint.DataSource = srcFailurePoint
        Else
            For i = 0 To eFailurePoint.Items.Count - 1
                eFailurePoint.Items.Remove(i)
            Next
            eFailurePoint.Items.Insert(0, New RadComboBoxItem("", ""))
            eFailurePoint.SelectedIndex = 0
        End If
        eFailurePoint.DataBind()
    End Sub

    Protected Sub rcbCategorySub_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Dim CategorySub As RadComboBox = sender
        Dim citem As GridEditableItem = CategorySub.NamingContainer

        Dim eCategorySub As RadComboBox = citem.FindControl("rcbCategorySub")

        Dim eFailurePoint As RadComboBox = citem.FindControl("rcbFailurePoint")
        Dim srcFailurePoint As SqlDataSource = citem.FindControl("srcFailurePoint")
        If eCategorySub.SelectedValue <> "" Then
            srcFailurePoint.SelectParameters("catesubId").DefaultValue = eCategorySub.SelectedValue
            eFailurePoint.DataSource = srcFailurePoint
        Else
            For i = 0 To eFailurePoint.Items.Count - 1
                eFailurePoint.Items.Remove(i)
            Next
            eFailurePoint.Items.Insert(0, New RadComboBoxItem("", ""))
            eFailurePoint.SelectedIndex = 0
        End If
        eFailurePoint.DataBind()
    End Sub

    Protected Sub chkHRO_CheckedChanged(sender As Object, e As EventArgs)
        Dim ChkHRO As CheckBox = sender
        Dim citem As GridEditableItem = ChkHRO.NamingContainer

        If Not ChkHRO.Checked Then
            Dim eChkHROop1 As CheckBox = citem.FindControl("chkHROop1")
            Dim eChkHROop2 As CheckBox = citem.FindControl("chkHROop2")
            Dim eChkHROop3 As CheckBox = citem.FindControl("chkHROop3")
            Dim eChkHROop4 As CheckBox = citem.FindControl("chkHROop4")
            Dim eChkHROop5 As CheckBox = citem.FindControl("chkHROop5")

            eChkHROop1.Checked = False
            eChkHROop2.Checked = False
            eChkHROop3.Checked = False
            eChkHROop4.Checked = False
            eChkHROop5.Checked = False
        End If
    End Sub
    Protected Sub chkRecognition_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkRecognition As CheckBox = sender
        Dim citem As GridEditableItem = chkRecognition.NamingContainer

        Dim ActionA As TextBox = citem.FindControl("tbActionA")
        Dim ActionB As TextBox = citem.FindControl("tbActionB")
        Dim ActionC As TextBox = citem.FindControl("tbActionC")

        Dim MsgRecognition As String = "Recognition *Propose action is not required*"
        If chkRecognition.Checked Then
            ActionA.Text = MsgRecognition
            ActionB.Text = MsgRecognition
            ActionC.Text = MsgRecognition
        Else
            ActionA.Text = ""
            ActionB.Text = ""
            ActionC.Text = ""
        End If
        ActionA.Enabled = Not chkRecognition.Checked
        ActionB.Enabled = Not chkRecognition.Checked
        ActionC.Enabled = Not chkRecognition.Checked
    End Sub

    Protected Sub rcbObserveType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        Dim ObserveType As RadComboBox = sender
        Dim citem As GridEditableItem = ObserveType.NamingContainer

        Dim Contractor As RadComboBox = citem.FindControl("rcbContractor")
        If ObserveType.SelectedValue = "1" Then
            Dim srcContractor As SqlDataSource = citem.FindControl("srcContractor")
            Contractor.DataSource = srcContractor
            Contractor.Visible = True
            Contractor.DataBind()
        Else
            Contractor.Visible = False
        End If
    End Sub

    Protected Sub cbShow_Del_CheckedChanged(sender As Object, e As EventArgs) Handles cbShow_Del.CheckedChanged
        rgRecordList.Rebind()
    End Sub

    Protected Sub editOthObserve_Click(sender As Object, e As ImageClickEventArgs)
        Dim btEditObserver As ImageButton = sender
        Dim eitem As GridEditableItem = btEditObserver.NamingContainer

        Dim racObservBox As RadAutoCompleteBox = eitem.FindControl("racObservBox")
        racObservBox.Enabled = Not racObservBox.Enabled
    End Sub

    Private Sub ChangeToRecognition(ByVal detailId As Integer, ByVal recId As Integer, ByVal rgIndex As Integer)
        Dim oRecog As Boolean
        Dim oEnA As Boolean
        Dim oEnB As Boolean
        Dim oEnC As Boolean

        '-- get data from tblRecordDetail
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim strSql As String = "SELECT detailId, recognition AS Recog, proposeEnable_A AS enA, proposeEnable_B AS enB, proposeEnable_C AS enC FROM tblRecordDetail WHERE (detailId = @detailId)"
            Dim command As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
            command.Parameters.Add("@detailId", SqlDbType.Int).Value = detailId

            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()
            While DataRead.Read()
                oRecog = CBool(DataRead("Recog"))
                oEnA = CBool(DataRead("enA"))               'enable A             
                oEnB = CBool(DataRead("enB"))               'enable B
                oEnC = CBool(DataRead("enC"))               'enable C
            End While
        End Using

        '-- new status
        Dim nRecog As Boolean = True
        Dim nEnA As Boolean = oEnA
        Dim nEnB As Boolean = oEnB
        Dim nEnC As Boolean = oEnC

        '-- change recognition & update sql
        If Not oRecog Then
            Dim strUpd As String = "UPDATE tblRecordDetail SET recognition = @recognition, observComplete = @observComplete, proposeRespPerson_A = @proposeRespPerson_A, proposeAction_A = @proposeAction_A, proposeStatus_A = @proposeStatus_A, proposeComplete_A = @proposeComplete_A"
            Dim connUpd As New SqlConnection(ConnStr)

            connUpd.Open()
            Dim result As Integer
            If oEnA = True And oEnB = False And oEnC = False Then
                strUpd = strUpd & " WHERE detailId = @detailId"
                Dim command As New SqlCommand(strUpd, connUpd)
                command.Parameters.Add("@recognition", SqlDbType.Bit).Value = nRecog
                command.Parameters.Add("@observComplete", SqlDbType.Int).Value = 1001
                command.Parameters.Add("@detailId", SqlDbType.Int).Value = detailId

                command.Parameters.Add("@proposeRespPerson_A", SqlDbType.Int).Value = 0
                command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = "Recognition *Propose action is not required*"
                command.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = 1001
                command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = False

                result = command.ExecuteNonQuery()
            ElseIf oEnA = True And oEnB = True And oEnC = False Then
                strUpd = strUpd & ", proposeRespPerson_B = @proposeRespPerson_B, proposeAction_B = @proposeAction_B, proposeStatus_B = @proposeStatus_B, proposeComplete_B = @proposeComplete_B"
                strUpd = strUpd & " WHERE detailId = @detailId"
                Dim command As New SqlCommand(strUpd, connUpd)
                command.Parameters.Add("@recognition", SqlDbType.Bit).Value = nRecog
                command.Parameters.Add("@observComplete", SqlDbType.Int).Value = 1001
                command.Parameters.Add("@detailId", SqlDbType.Int).Value = detailId

                command.Parameters.Add("@proposeRespPerson_A", SqlDbType.Int).Value = 0
                command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = "Recognition *Propose action is not required*"
                command.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = 1001
                command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = False

                command.Parameters.Add("@proposeRespPerson_B", SqlDbType.Int).Value = 0
                command.Parameters.Add("@proposeAction_B", SqlDbType.NVarChar).Value = "Recognition *Propose action is not required*"
                command.Parameters.Add("@proposeStatus_B", SqlDbType.Int).Value = 1001
                command.Parameters.Add("@proposeComplete_B", SqlDbType.Bit).Value = False

                result = command.ExecuteNonQuery()
            ElseIf oEnA = True And oEnB = True And oEnC = True Then
                strUpd = strUpd & ", proposeRespPerson_B = @proposeRespPerson_B, proposeAction_B = @proposeAction_B, proposeStatus_B = @proposeStatus_B, proposeComplete_B = @proposeComplete_B"
                strUpd = strUpd & ", proposeRespPerson_C = @proposeRespPerson_C, proposeAction_C = @proposeAction_C, proposeStatus_C = @proposeStatus_C, proposeComplete_C = @proposeComplete_C"
                strUpd = strUpd & " WHERE detailId = @detailId"
                Dim command As New SqlCommand(strUpd, connUpd)
                command.Parameters.Add("@recognition", SqlDbType.Bit).Value = nRecog
                command.Parameters.Add("@observComplete", SqlDbType.Int).Value = 1001
                command.Parameters.Add("@detailId", SqlDbType.Int).Value = detailId

                command.Parameters.Add("@proposeRespPerson_A", SqlDbType.Int).Value = 0
                command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = "Recognition *Propose action is not required*"
                command.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = 1001
                command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = False

                command.Parameters.Add("@proposeRespPerson_B", SqlDbType.Int).Value = 0
                command.Parameters.Add("@proposeAction_B", SqlDbType.NVarChar).Value = "Recognition *Propose action is not required*"
                command.Parameters.Add("@proposeStatus_B", SqlDbType.Int).Value = 1001
                command.Parameters.Add("@proposeComplete_B", SqlDbType.Bit).Value = False

                command.Parameters.Add("@proposeRespPerson_C", SqlDbType.Int).Value = 0
                command.Parameters.Add("@proposeAction_C", SqlDbType.NVarChar).Value = "Recognition *Propose action is not required*"
                command.Parameters.Add("@proposeStatus_C", SqlDbType.Int).Value = 1001
                command.Parameters.Add("@proposeComplete_C", SqlDbType.Bit).Value = False

                result = command.ExecuteNonQuery()
            End If
            connUpd.Close()

            '-- update obs
            Dim status As New cStatus
            status.UpdRecordIsComplete(recId)
            Session("ExpandedRow") = rgIndex.ToString
            rgRecordList.MasterTableView.Rebind()

        End If
    End Sub


End Class