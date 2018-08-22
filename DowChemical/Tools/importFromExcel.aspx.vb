Imports Telerik.Web.UI
Imports Microsoft.Office.Interop
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports AiLib

Public Class importFromExcel
    Inherits System.Web.UI.Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            rcbYear.SelectedValue = Now.Year
            infobox.Items.Clear()
        Else
            infoboxCssStyle()
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

    Private Sub infoboxCssStyle()
        For i = 0 To infobox.Items.Count - 1
            If i = 0 Or Session("fontcolor") = 0 Then
                infobox.Items(0).Attributes.CssStyle.Add("font-weight", "bold")
                infobox.Items(0).Attributes.CssStyle.Add("color", "#696969")
            Else
                infobox.Items(i).Attributes.CssStyle.Add("font-weight", "normal")
                If Session("fontcolor") = 0 Then
                    infobox.Items(i).Attributes.CssStyle.Add("color", "#FF2B1C")
                Else
                    infobox.Items(i).Attributes.CssStyle.Add("color", "#696969")
                End If
            End If
        Next
    End Sub

    Private Sub AddListBox(ByVal msg As String, ByVal fontcolor As Integer)
        If msg IsNot Nothing And msg <> "" Then
            If infobox.Items.Count = 0 Then
                infobox.Items.Add("Validate Info:")
            End If
            msg = msg + Environment.NewLine
            'Dim MaxLine As Integer = 20
            'If infobox.Items.Count = MaxLine Then
            '    infobox.Items.RemoveAt(1)
            'End If
            infobox.Items.Add(msg)
            Session("fontcolor") = fontcolor
            infoboxCssStyle()
        End If
    End Sub

    Protected Sub btUploadXls_Click(sender As Object, e As EventArgs) Handles btUploadXls.Click
        Dim fileCount As Integer = RadUpload1.UploadedFiles.Count.ToString
        If fileCount <> 0 Then
            'กำหนดชื่อไฟล์
            Dim FolderPath As String = "ExcelUpload\"
            Dim CurrentPath As String = Request.PhysicalApplicationPath
            Dim fileExtension As String = RadUpload1.UploadedFiles(0).GetExtension().ToString()

            If fileExtension = ".xls" Then
                Dim rnd As New Random
                Dim intrnd As Integer = rnd.Next(1000, 9999)
                Dim uniqueFilename As String
                uniqueFilename = Now.Year.ToString.Substring(2, 2) & Now.Month.ToString("00") & intrnd.ToString

                Dim SaveFilesname As String = ""
                Dim oFileName As String = ""
                Dim SaveFilesPath As String = ""
                CurrentPath = CurrentPath & FolderPath
                For Each f As UploadedFile In RadUpload1.UploadedFiles
                    oFileName = Strings.Left(f.GetNameWithoutExtension, 50)
                    SaveFilesname = uniqueFilename & "_" & Strings.Left(oFileName, 50) & fileExtension
                    SaveFilesPath = CurrentPath & SaveFilesname

                    f.SaveAs(SaveFilesPath)
                Next
                lbInfo.Text = SaveFilesname & " save."
                If rbtDataSource.SelectedValue = 1 Then
                    ImportDataOleDB(SaveFilesname, fileExtension)
                ElseIf rbtDataSource.SelectedValue = 2 Then
                    ImportDataOleDB_newMPT(SaveFilesname, fileExtension)
                End If

                RadUpload1.UploadedFiles.Clear()
                RadUpload1.Enabled = False
            Else
                lbInfo.Text = "Not support this file Extension"
            End If
        End If
    End Sub

    Private Sub ImportDataOleDB(ByVal xlsFilename As String, ByVal Extension As String)
        Dim FolderPath As String = "~/ExcelUpload/"
        Dim CurrentPath As String = Server.MapPath(FolderPath & xlsFilename)

        Dim excelConStr As String = ""
        Select Case Extension
            Case ".xls"
                'Excel 97-03
                excelConStr = ConfigurationManager.ConnectionStrings("Excel03Connection").ConnectionString
            Case ".xlsx"
                'Excel 07
                excelConStr = ConfigurationManager.ConnectionStrings("Excel07Connection").ConnectionString
        End Select
        excelConStr = String.Format(excelConStr, CurrentPath, "Yes")

        Dim connExcel As New OleDbConnection(excelConStr)
        Dim commandExcel As New OleDbCommand()
        Dim oleda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        commandExcel.Connection = connExcel
        If connExcel.State = ConnectionState.Closed Then connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        If SheetName.IndexOf("'") >= 0 Then SheetName = SheetName.Substring(1, SheetName.Length - 2)

        lbInfo.Text = "Import '" & xlsFilename & "', Sheet '" & SheetName & "'"
        Dim queryRange As String = "SELECT * From [" & SheetName & "B:BZ]"

        commandExcel.CommandText = queryRange
        oleda.SelectCommand = commandExcel
        oleda.Fill(dt)
        connExcel.Close()

        dt.Columns(0).ColumnName = "#"                      'A
        dt.Columns(1).ColumnName = "ActionNumber"           'B
        dt.Columns(2).ColumnName = "ActionDate"             'C
        dt.Columns(3).ColumnName = "ActionTime"             'D
        dt.Columns(4).ColumnName = "DurationValue"          'E
        dt.Columns(5).ColumnName = "Observer#1_Name"        'F
        dt.Columns(6).ColumnName = "Observer#1_DowID"       'G
        dt.Columns(7).ColumnName = "Observer#1_Depart"      'H
        dt.Columns(8).ColumnName = "OthObserv1_Name"
        dt.Columns(9).ColumnName = "OthObserv1_DowID"
        dt.Columns(10).ColumnName = "OthObserv1_Depart"
        dt.Columns(11).ColumnName = "OthObserv2_Name"
        dt.Columns(12).ColumnName = "OthObserv2_DowID"
        dt.Columns(13).ColumnName = "OthObserv2_Depart"
        dt.Columns(14).ColumnName = "OthObserv3_Name"
        dt.Columns(15).ColumnName = "OthObserv3_DowID"
        dt.Columns(16).ColumnName = "OthObserv3_Depart"
        dt.Columns(17).ColumnName = "Link"                  'R
        dt.Columns(18).ColumnName = "No.Observe"            'S

        For i = 0 To 4
            dt.Columns(19 + (i * 11)).ColumnName = "Title_Obs" & (i + 1).ToString            'T /AF /
            dt.Columns(20 + (i * 11)).ColumnName = "Category_Obs" & (i + 1).ToString         'U /AG /
            dt.Columns(21 + (i * 11)).ColumnName = "SubCategory_Obs" & (i + 1).ToString      'V /AH /
            dt.Columns(22 + (i * 11)).ColumnName = "FailPoint_Obs" & (i + 1).ToString        'W /AI /
            dt.Columns(23 + (i * 11)).ColumnName = "Equipmen_Obs" & (i + 1).ToString         'X /AJ /
            dt.Columns(24 + (i * 11)).ColumnName = "HRO_Obs" & (i + 1).ToString              'Y /AK /
            dt.Columns(25 + (i * 11)).ColumnName = "2ndEye_Obs" & (i + 1).ToString           'Z /AL /
            dt.Columns(26 + (i * 11)).ColumnName = "Description_Obs" & (i + 1).ToString      'AA /AM /
            dt.Columns(27 + (i * 11)).ColumnName = "PropAction_Obs" & (i + 1).ToString       'AB /AN /
            dt.Columns(28 + (i * 11)).ColumnName = "ResponDowID_Obs" & (i + 1).ToString      'AD /AP /
            dt.Columns(29 + (i * 11)).ColumnName = "Status_Obs" & (i + 1).ToString           'AE /AQ /
        Next

        'Debug --Bind Data to GridView
        RadGrid1.DataSource = dt
        RadGrid1.DataBind()

        Page.Server.ScriptTimeout = 4000
        Dim j As Integer = 0
        Dim count As Integer = 0
        For Each row As DataRow In dt.Rows
            Dim drow As DataRow = dt.Rows.Item(j)

            If dt.Rows.Item(j)("ActionNumber") IsNot DBNull.Value AndAlso dt.Rows.Item(j)("ActionNumber") <> "" Then
                Dim result As Integer = ImportDataAction(drow)

                Select Case result
                    Case 0
                        Dim InputId As Integer = getEmpId(dt.Rows.Item(j)("Observer#1_DowID"))
                        If InputId = 0 Then
                            AddListBox("Insert action data, " & dt.Rows.Item(j)("ActionNumber") & ".., " & dt.Rows.Item(j)("Observer#1_DowID") & " not exist.", 0)
                        Else
                            AddListBox("Insert action data, " & dt.Rows.Item(j)("ActionNumber") & ".., FAIL!!.", 0)
                        End If
                    Case 200 + CInt(dt.Rows.Item(j)("No.Observe"))
                        AddListBox("Insert action data, Action Number " & dt.Rows.Item(j)("ActionNumber") & ".... OK.", 1)
                    Case 100
                        AddListBox("Action Number " & dt.Rows.Item(j)("ActionNumber") & " repeat record.", 1)
                    Case Else
                        Dim InputId As Integer = getEmpId(dt.Rows.Item(j)("Observer#1_DowID"))
                        If InputId = 0 Then
                            AddListBox("Insert action data, " & dt.Rows.Item(j)("ActionNumber") & "..," & dt.Rows.Item(j)("Observer#1_DowID") & " not exist.", 0)
                        Else
                            AddListBox("Some process fail.", 0)
                        End If
                End Select
                count = count + 1
            End If
            j = j + 1
        Next
        AddListBox("#### Imported " & count & " records complete.", 0)
    End Sub

    Private Function ImportDataAction(ByVal actionRow As DataRow) As Integer
        Dim dr As DataRow = actionRow
        Dim recActNo As String = dr.Item("ActionNumber").ToString.Trim

        Dim IsCheckRepeart As Boolean
        If chkContinue.Checked Then IsCheckRepeart = True Else IsCheckRepeart = Not recActNoRepeat(recActNo)

        '---- check repeat
        If IsCheckRepeart Then
            Dim result As Integer = 0
            Dim IsCancel As Boolean = False
            Dim strIns As String = "INSERT INTO tblRecord(tempFlag, tempLock, timestamp, departId, recActive, recActNo, recActNoValue, recActMonth, recActYear, recActDate, recActTime, durationH, durationM, durationValue, empId, oEmpCount, noObserve) 
                                VALUES(@tempFlag, @tempLock, @timestamp, @departId, @recActive, @recActNo, @recActNoValue, @recActMonth, @recActYear, @recActDate, @recActTime, @durationH, @durationM, @durationValue, @empId, @oEmpCount, @noObserve)"

            Using connection As New SqlConnection(ConnStr)
                connection.Open()
                Dim command As New SqlCommand(strIns, connection)
                command.Parameters.Add("@recActive", SqlDbType.Bit).Value = True
                command.Parameters.Add("@tempFlag", SqlDbType.Bit).Value = False
                command.Parameters.Add("@tempLock", SqlDbType.DateTime).Value = Now()
                command.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = Now()

                '====== Date Format =======
                Dim recDate As Date = dr.Item("ActionDate")
                command.Parameters.Add("@recActDate", SqlDbType.Date).Value = recDate

                '====== Doc Number ========
                Dim idxYear As Integer = 0
                Dim idxYearStr As String = ""
                Dim recActNo_New As String = ""
                If rbtActNumberFormat.SelectedValue = "1" Then      'YY
                    idxYearStr = rcbYear.Text.Substring(2, 2)
                    idxYear = recActNo.IndexOf(idxYearStr)
                    'command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = recActNo.Substring(idxYear + 2, 2)
                    command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = recDate.Month
                    command.Parameters.Add("@recActYear", SqlDbType.Int).Value = CInt(recActNo.Substring(idxYear, 2)) + 2000
                    command.Parameters.Add("@recActNoValue", SqlDbType.Int).Value = CInt(recActNo.Substring(idxYear))
                    recActNo_New = recActNo.Substring(0, idxYear) & rcbYear.Text.Substring(2, 2) & recActNo.Substring(idxYear + 2)
                ElseIf rbtActNumberFormat.SelectedValue = "2" Then
                    idxYearStr = rcbYear.Text
                    idxYear = recActNo.IndexOf(idxYearStr)          'YYYY

                    'command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = CInt(recActNo.Substring(idxYear + 4, 2))
                    command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = recDate.Month
                    command.Parameters.Add("@recActYear", SqlDbType.Int).Value = recDate.Year

                    'พบข้อมูลก่อน import ไม่ได้ เพราะ Action Number ไม่ได้ standdard'
                    If idxYear = -1 Then
                        idxYearStr = "017"
                        idxYear = recActNo.IndexOf(idxYearStr)

                        command.Parameters.Add("@recActNoValue", SqlDbType.Int).Value = CInt(recActNo.Substring(idxYear + 1))
                        recActNo_New = recActNo.Substring(0, idxYear) & rcbYear.Text.Substring(2, 2) & recActNo.Substring(idxYear + 3)
                    Else
                        command.Parameters.Add("@recActNoValue", SqlDbType.Int).Value = CInt(recActNo.Substring(idxYear + 2))
                        recActNo_New = recActNo.Substring(0, idxYear) & rcbYear.Text.Substring(2, 2) & recActNo.Substring(idxYear + 4)
                    End If
                End If
                Dim departId As Integer = getDepartId(recActNo.Substring(0, idxYear))
                command.Parameters.Add("@departId", SqlDbType.Int).Value = departId

                If Not chkContinue.Checked Then
                    command.Parameters.Add("@recActNo", SqlDbType.NVarChar).Value = recActNo_New
                Else
                    'continue actionnumber
                    Dim autoNumber As New aAutoNumber
                    Dim actualNumbervalue As Integer = autoNumber.ActNumberAutoNumDepartId(departId, CDate(dr.Item("ActionDate")))

                    recActNo_New = recActNo.Substring(0, idxYear) & (actualNumbervalue + 1).ToString
                    command.Parameters.Add("@recActNo", SqlDbType.NVarChar).Value = recActNo_New
                End If
                '==========================

                Dim ActTime As String = dr.Item("ActionTime")
                Dim idx As Integer = ActTime.IndexOf(":") + 1
                command.Parameters.Add("@recActTime", SqlDbType.Time).Value = ActTime.Substring(0, idx) & (5 * CInt(ActTime.Substring(idx)) \ 5).ToString & ":00"

                Dim duValue As Integer = CInt(dr.Item("DurationValue"))
                command.Parameters.Add("@durationH", SqlDbType.Int).Value = duValue \ 60
                command.Parameters.Add("@durationM", SqlDbType.Int).Value = duValue Mod 60
                command.Parameters.Add("@durationValue", SqlDbType.Int).Value = duValue

                Dim InputId As Integer = getEmpId(dr.Item("Observer#1_DowID"))
                command.Parameters.Add("@empId", SqlDbType.Int).Value = InputId
                If InputId = 0 Then IsCancel = True

                Dim oEmpCount As Integer = 0
                Dim oEmpList As New ArrayList
                If dr.Item("OthObserv1_DowID") IsNot DBNull.Value AndAlso Trim(dr.Item("OthObserv1_DowID")) <> "" Then
                    oEmpCount = oEmpCount + 1
                    oEmpList.Add(getEmpId(dr.Item(9)))
                    If dr.Item("OthObserv2_DowID") IsNot DBNull.Value AndAlso Trim(dr.Item("OthObserv2_DowID")) <> "" Then oEmpCount = oEmpCount + 1 : oEmpList.Add(getEmpId(dr.Item("OthObserv2_DowID")))
                    If dr.Item("OthObserv3_DowID") IsNot DBNull.Value AndAlso Trim(dr.Item("OthObserv3_DowID")) <> "" Then oEmpCount = oEmpCount + 1 : oEmpList.Add(getEmpId(dr.Item("OthObserv3_DowID")))
                    'If dr.Item("OthObserv4_DowID") IsNot DBNull.Value AndAlso Trim(dr.Item("OthObserv4_DowID")) <> "" Then oEmpCount = oEmpCount + 1 : oEmpList.Add(getEmpId(dr.Item("OthObserv4_DowID")))
                    'If dr.Item("OthObserv5_DowID") IsNot DBNull.Value AndAlso Trim(dr.Item("OthObserv5_DowID")) <> "" Then oEmpCount = oEmpCount + 1 : oEmpList.Add(getEmpId(dr.Item("OthObserv5_DowID")))
                End If
                command.Parameters.Add("@oEmpCount", SqlDbType.Int).Value = oEmpCount
                command.Parameters.Add("@noObserve", SqlDbType.Int).Value = CInt(dr.Item("No.Observe"))
                If Not IsCancel Then result = command.ExecuteNonQuery()

                'save error
                If result = 0 Then
                    If tbError.Text = "" Then
                        tbError.Text = "Import fail list." & vbNewLine & recActNo & " (" & recActNo_New & ")" & vbNewLine
                    Else
                        tbError.Text = tbError.Text & recActNo & " (" & recActNo_New & ")" & vbNewLine
                    End If
                End If

                'save otherObserv
                Dim recId As Integer = getRecId(recActNo_New)
                If Not IsCancel Then saveOtherObserv(recId, oEmpList)

                'save action detail
                If result > 0 Then Return (result * 200) + saveObservDetail(recId, actionRow) Else Return 0
            End Using
        Else
            Return 100
        End If
    End Function

    Private Sub saveOtherObserv(ByVal recId As Integer, ByVal oEmpList As ArrayList)
        Dim strIns As String = "INSERT INTO tblRecordOthEmpO(recId, recItem, empIdOth) VALUES(@recId, @recItem, @empIdOth)"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As SqlCommand
        For i As Integer = 1 To oEmpList.Count
            command = New SqlCommand(strIns, conn)
            command.Parameters.Add("@recId", SqlDbType.Int).Value = recId
            command.Parameters.Add("@recItem", SqlDbType.Int).Value = i
            command.Parameters.Add("@empIdOth", SqlDbType.Int).Value = oEmpList(i - 1)

            command.ExecuteNonQuery()
        Next
        conn.Close()
    End Sub

    Private Function saveObservDetail(ByVal recId As Integer, ByVal actionDataRow As DataRow) As Integer
        Dim Result As Integer = 0
        Dim dr As DataRow = actionDataRow
        Dim NoObs As Integer = CInt(dr.Item("No.Observe"))

        For j = 1 To NoObs
            Dim strIns As String = "INSERT INTO tblRecordDetail(recId, observItem, title, category, categorySub, failurePoint, equipment, IsHRO, hroChk1, hroChk2, hroChk3, hroChk4, hroChk5, secondEye, recognition, observType, description, 
                                        proposeEnable_A, proposeRespPerson_A, proposeAction_A, proposeStatus_A, proposeComplete_A, proposeEnable_B, proposeEnable_C, observComplete) 
                                        VALUES(@recId, @observItem, @title, @category, @categorySub, @failurePoint, @equipment, @IsHRO, @hroChk1, @hroChk2, @hroChk3, @hroChk4, @hroChk5, @secondEye, @recognition, @observType, @description, 
                                        @proposeEnable_A, @proposeRespPerson_A, @proposeAction_A, @proposeStatus_A, @proposeComplete_A, @proposeEnable_B, @proposeEnable_C, @observComplete)"

            Using conn As New SqlConnection(ConnStr)
                conn.Open()
                Dim command As New SqlCommand(strIns, conn)
                command.Parameters.Add("@recId", SqlDbType.Int).Value = recId
                command.Parameters.Add("@observItem", SqlDbType.Int).Value = j

                Dim TitleObs As String = dr.Item("Title_Obs" & j.ToString).ToString.Trim
                If TitleObs.Substring(0, 1) = "-" Or TitleObs.Substring(0, 1) = "+" Or TitleObs.Substring(0, 1) = "=" Then TitleObs = "'" & TitleObs
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = TitleObs

                Dim failureAndCate As New cFailureCategory
                Dim cateId As Integer = failureAndCate.GetCategoryId(dr.Item("Category_Obs" & j.ToString))
                Dim cateSubId As Integer = 1000
                If dr.Item("SubCategory_Obs" & j.ToString) IsNot DBNull.Value Then cateSubId = failureAndCate.GetCategorySubId(dr.Item("SubCategory_Obs" & j.ToString), cateId)
                Dim failurePointId As Integer = 1000
                If dr.Item("FailPoint_Obs" & j.ToString) IsNot DBNull.Value Then failurePointId = failureAndCate.GetFailPointId(dr.Item("FailPoint_Obs" & j.ToString), cateSubId)
                command.Parameters.Add("@category", SqlDbType.Int).Value = cateId
                command.Parameters.Add("@categorySub", SqlDbType.Int).Value = cateSubId
                command.Parameters.Add("@failurePoint", SqlDbType.Int).Value = failurePointId

                Dim EquipmentObs As String = dr.Item("Equipmen_Obs" & j.ToString).ToString.Trim
                If EquipmentObs <> "" AndAlso (EquipmentObs.Substring(0, 1) = "-" Or EquipmentObs.Substring(0, 1) = "+" Or EquipmentObs.Substring(0, 1) = "=") Then EquipmentObs = "'" & EquipmentObs
                command.Parameters.Add("@equipment", SqlDbType.NVarChar).Value = EquipmentObs

                Dim HRO As Boolean = False
                Dim HROop1 As Boolean = False
                Dim HROop2 As Boolean = False
                Dim HROop3 As Boolean = False
                Dim HROop4 As Boolean = False
                Dim HROop5 As Boolean = False

                Dim HROStrImport As String = ""
                If dr.Item("HRO_Obs" & j.ToString) IsNot DBNull.Value Then HROStrImport = dr.Item("HRO_Obs" & j.ToString)
                Dim HROStr As String = ""
                Dim idx As Integer = 0
                While HROStrImport <> ""
                    idx = HROStrImport.IndexOf(", ")
                    If idx <> -1 Then
                        HROStr = HROStrImport.Substring(0, idx)
                        HROStrImport = HROStrImport.Substring(idx + 2)
                    Else
                        HROStr = HROStrImport
                        HROStrImport = ""
                    End If

                    If HROStr = "Expect the Unexpected" Then
                        HROop1 = True
                    ElseIf HROStr = "Do Not Generalize" Then
                        HROop2 = True
                    ElseIf HROStr = "Identify Trend & Anticipate Impact" Then
                        HROop3 = True
                    ElseIf HROStr = "Engage & Apply Expertise" Then
                        HROop4 = True
                    ElseIf HROStr = "Commit to Resilience" Then
                        HROop5 = True
                    End If
                End While
                If HROop1 = True Or HROop2 = True Or HROop3 = True Or HROop4 = True Or HROop5 = True Then HRO = True

                command.Parameters.Add("@IsHRO", SqlDbType.Bit).Value = HRO
                command.Parameters.Add("@hroChk1", SqlDbType.Bit).Value = HROop1
                command.Parameters.Add("@hroChk2", SqlDbType.Bit).Value = HROop2
                command.Parameters.Add("@hroChk3", SqlDbType.Bit).Value = HROop3
                command.Parameters.Add("@hroChk4", SqlDbType.Bit).Value = HROop4
                command.Parameters.Add("@hroChk5", SqlDbType.Bit).Value = HROop5

                Dim secondEye As Boolean = CBool(dr.Item("2ndEye_Obs" & j.ToString))
                command.Parameters.Add("@secondEye", SqlDbType.Bit).Value = secondEye

                'Dim Status As Integer = 1002
                'Dim IsRecog As Boolean = False
                'Dim RecogStrChk As String = (dr.Item("PropAction_Obs" & j.ToString)).ToString.Trim
                'If RecogStrChk = "Recognition *Propose action is not required*" Then IsRecog = True
                'If IsRecog Then Status = 1001
                'Dim IsComplete As Boolean = False
                'If dr.Item("Status_Obs" & j.ToString).ToString.Trim = "Completed" Then Status = 1003

                Dim Status As Integer = 1002
                If dr.Item("Status_Obs" & j.ToString).ToString.Trim = "Completed" OR dr.Item("Status_Obs" & j.ToString).ToString.Trim = "Complete" Then Status = 1003
                If dr.Item("Status_Obs" & j.ToString).ToString.Trim = "Recognition" Then Status = 1001
                Dim IsRecog As Boolean = False
                If Status = 1001 Then IsRecog = True
                Dim IsComplete As Boolean = False
                If Status = 1003 Then IsComplete = True

                command.Parameters.Add("@recognition", SqlDbType.Bit).Value = IsRecog
                command.Parameters.Add("@observType", SqlDbType.Int).Value = 0

                Dim DescriptionObs As String = dr.Item("Description_Obs" & j.ToString).ToString.Trim
                If DescriptionObs <> "" AndAlso (DescriptionObs.Substring(0, 1) = "-" Or DescriptionObs.Substring(0, 1) = "+" Or DescriptionObs.Substring(0, 1) = "=") Then DescriptionObs = "'" & DescriptionObs
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = DescriptionObs

                Dim propEnaA As Boolean = False
                Dim propStrChk As String = dr.Item("Title_Obs" & j.ToString)
                If propStrChk <> "" Then propEnaA = True
                command.Parameters.Add("@proposeEnable_A", SqlDbType.Bit).Value = propEnaA

                If IsRecog Then
                    command.Parameters.Add("@proposeRespPerson_A", SqlDbType.Int).Value = 0
                Else
                    Dim ResponId As Integer = 0
                    If dr.Item("ResponDowID_Obs" & j.ToString) IsNot DBNull.Value OrElse dr.Item("ResponDowID_Obs" & j.ToString).ToString <> "" Then
                        ResponId = getEmpId(dr.Item("ResponDowID_Obs" & j.ToString))
                    Else
                        ResponId = getEmpId("EMP_NA")      'N/A employee
                    End If
                    command.Parameters.Add("@proposeRespPerson_A", SqlDbType.Int).Value = ResponId
                End If
                command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = dr.Item("PropAction_Obs" & j.ToString).ToString.Trim
                command.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = Status
                command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = IsComplete

                command.Parameters.Add("@proposeEnable_B", SqlDbType.Bit).Value = False
                command.Parameters.Add("@proposeEnable_C", SqlDbType.Bit).Value = False
                command.Parameters.Add("@observComplete", SqlDbType.Int).Value = Status

                Dim resultItem As Integer = command.ExecuteNonQuery()

                Result = Result + resultItem
                If resultItem > 0 Then AddListBox("Insert observe data, " & dr.Item("ActionNumber") & ", observe " & j.ToString & " .. OK.", 1)
            End Using

            'calculate all observe
            If j = NoObs Then
                Dim allStatus As New cStatus
                allStatus.UpdRecordIsComplete(recId)
            End If
        Next

        Return Result
    End Function

    Private Function getRecId(ByVal recActNo As String) As Integer
        Dim recId As Integer
        Const StrSelect As String = "SELECT recId FROM tblRecord WHERE recActNo = @recActNo"
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(StrSelect, connection)
            command.Parameters.Add("@recActNo", SqlDbType.NVarChar).Value = recActNo
            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()
            While DataRead.Read()
                recId = DataRead("recId")
            End While
        End Using

        Return recId
    End Function

    Private Function getDepartId(ByVal departName As String) As Integer
        Dim departNameNew As String = ""
        If departName = "E2SS" Then
            departNameNew = "ESS"
        ElseIf departName = "EOUP" Then
            departNameNew = "EOU"
        ElseIf departName = "ESOL" Then
            departNameNew = "ENG"
        ElseIf departName = "HPPO" Then
            departNameNew = "PO"
        ElseIf departName = "PULX" Then
            departNameNew = "PULTX"
        ElseIf departName = "SCOP" Then
            departNameNew = "SCO"
        ElseIf departName = "MTCP" Then
            departNameNew = "MTC"
        ElseIf departName = "MTC1" Then
            departNameNew = "MTC"
        ElseIf departName = "MTC2" Then
            departNameNew = "MTC"
        ElseIf departName = "MTC3" Then
            departNameNew = "MTC"
        ElseIf departName = "MTC4" Then
            departNameNew = "MTC"
        ElseIf departName = "MTC5" Then
            departNameNew = "MTC"
        ElseIf departName = "MTC6" Then
            departNameNew = "MTC"
        ElseIf departName = "MTC7" Then
            departNameNew = "MTC"
        ElseIf departName = "MTC8" Then
            departNameNew = "MTC"
        ElseIf departName = "QC01" Then
            departNameNew = "QAQC"
        ElseIf departName = "QC02" Then
            departNameNew = "QAQC"
        ElseIf departName = "QC03" Then
            departNameNew = "QAQC"
        ElseIf departName = "SCO1" Then
            departNameNew = "SCO"
        ElseIf departName = "SCO2" Then
            departNameNew = "SCO"
        ElseIf departName = "SCO3" Then
            departNameNew = "SCO"
        ElseIf departName = "SCO4" Then
            departNameNew = "SCO"
        ElseIf departName = "SPE1" Then
            departNameNew = "SPE"
        ElseIf departName = "SPE2" Then
            departNameNew = "SPE"
        ElseIf departName = "PSOP" Then
            departNameNew = "PS"
        ElseIf departName = "RCOP" Then
            departNameNew = "RC"
        ElseIf departName = "SEOP" Then
            departNameNew = "SE"
        ElseIf departName = "PTSD" Then
            departNameNew = "TS&D"
        End If

        If departNameNew <> "" Then departName = departNameNew

        Dim departId As Integer
        Const StrSelect As String = "SELECT departId FROM tblDepartment WHERE departName = @departName"
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(StrSelect, connection)
            command.Parameters.Add("@departName", SqlDbType.VarChar).Value = departName
            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()
            While DataRead.Read()
                departId = DataRead("departId")
            End While
        End Using

        Return departId
    End Function

    Private Function getEmpId(ByVal DowId As String) As Integer
        Dim empID As Integer
        Const StrSelect As String = "SELECT empId FROM tblEmployee WHERE empDowId = @empDowId"
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(StrSelect, connection)
            command.Parameters.Add("@empDowId", SqlDbType.VarChar).Value = DowId.ToUpper
            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()
            While DataRead.Read()
                empID = DataRead("empId")
            End While
        End Using

        Return empID
    End Function

    Public Function recActNoRepeat(ByVal recActNo As String) As Boolean
        Const StrSelect As String = "SELECT COUNT(*) FROM tblRecord WHERE recActNo = @recActNo"
        Dim Count As Integer = 0
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(StrSelect, connection)
            command.Parameters.Add("@recActNo", SqlDbType.NVarChar).Value = recActNo
            Count = command.ExecuteScalar()
        End Using

        If Count > 0 Then Return True Else Return False
    End Function

    '================ New MPT Operations Tool '================
    Protected Sub rbtDataSource_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbtDataSource.SelectedIndexChanged
        If rbtDataSource.SelectedValue = 1 Then
            chkContinue.Enabled = False
        ElseIf rbtDataSource.SelectedValue = 2 Then
            chkContinue.Enabled = True
        End If
    End Sub

    Private Sub ImportDataOleDB_newMPT(ByVal xlsFilename As String, ByVal Extension As String)
        Dim FolderPath As String = "~/ExcelUpload/"
        Dim CurrentPath As String = Server.MapPath(FolderPath & xlsFilename)

        Dim excelConStr As String = ""
        Select Case Extension
            Case ".xls"
                'Excel 97-03
                excelConStr = ConfigurationManager.ConnectionStrings("Excel03Connection").ConnectionString
            Case ".xlsx"
                'Excel 07
                excelConStr = ConfigurationManager.ConnectionStrings("Excel07Connection").ConnectionString
        End Select
        excelConStr = String.Format(excelConStr, CurrentPath, "Yes")

        Dim connExcel As New OleDbConnection(excelConStr)
        Dim commandExcel As New OleDbCommand()
        Dim oleda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        commandExcel.Connection = connExcel
        If connExcel.State = ConnectionState.Closed Then connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()

        lbInfo.Text = "Import '" & xlsFilename & "', Sheet '" & SheetName & "'"
        Dim queryRange As String = "SELECT * From [" & SheetName & "A:FM]"

        commandExcel.CommandText = queryRange
        oleda.SelectCommand = commandExcel
        oleda.Fill(dt)
        connExcel.Close()

        dt.Columns(0).ColumnName = "#"                      'A
        dt.Columns(1).ColumnName = "ActionNumber"           'B
        dt.Columns(2).ColumnName = "ActionDate"             'C
        dt.Columns(3).ColumnName = "ActionTime"             'D
        dt.Columns(4).ColumnName = "DurationValue"          'E
        dt.Columns(5).ColumnName = "Observer#1_Name"        'F
        dt.Columns(6).ColumnName = "Observer#1_DowID"       'G
        dt.Columns(7).ColumnName = "Observer#1_Depart"      'H
        dt.Columns(8).ColumnName = "OthObserv1_Name"
        dt.Columns(9).ColumnName = "OthObserv1_DowID"
        dt.Columns(10).ColumnName = "OthObserv1_Depart"
        dt.Columns(11).ColumnName = "OthObserv2_Name"
        dt.Columns(12).ColumnName = "OthObserv2_DowID"
        dt.Columns(13).ColumnName = "OthObserv2_Depart"
        dt.Columns(14).ColumnName = "OthObserv3_Name"
        dt.Columns(15).ColumnName = "OthObserv3_DowID"
        dt.Columns(16).ColumnName = "OthObserv3_Depart"
        dt.Columns(17).ColumnName = "OthObserv4_Name"
        dt.Columns(18).ColumnName = "OthObserv4_DowID"
        dt.Columns(19).ColumnName = "OthObserv4_Depart"
        dt.Columns(20).ColumnName = "OthObserv5_Name"
        dt.Columns(21).ColumnName = "OthObserv5_DowID"
        dt.Columns(22).ColumnName = "OthObserv5_Depart"
        dt.Columns(23).ColumnName = "No.Observe"            'X

        For i = 0 To 5
            dt.Columns(24 + (i * 17)).ColumnName = "Title_Obs" & (i + 1).ToString            'Y /AP /
            dt.Columns(25 + (i * 17)).ColumnName = "Category_Obs" & (i + 1).ToString         'Z /AQ /
            dt.Columns(26 + (i * 17)).ColumnName = "SubCategory_Obs" & (i + 1).ToString      'AA /AR /
            dt.Columns(27 + (i * 17)).ColumnName = "FailPoint_Obs" & (i + 1).ToString        'AB /AS /
            dt.Columns(28 + (i * 17)).ColumnName = "Equipmen_Obs" & (i + 1).ToString         'AC /AT /
            dt.Columns(29 + (i * 17)).ColumnName = "HRO_Obs" & (i + 1).ToString              'AD /AU /
            dt.Columns(30 + (i * 17)).ColumnName = "2ndEye_Obs" & (i + 1).ToString           'AE /AV /
            dt.Columns(31 + (i * 17)).ColumnName = "Description_Obs" & (i + 1).ToString      'AF /AW /
            dt.Columns(32 + (i * 17)).ColumnName = "PropActionA_Obs" & (i + 1).ToString       'AG /AX /
            dt.Columns(33 + (i * 17)).ColumnName = "ResponDowIDA_Obs" & (i + 1).ToString      'AH /AY /
            dt.Columns(34 + (i * 17)).ColumnName = "StatusA_Obs" & (i + 1).ToString           'AI /AZ /
            dt.Columns(35 + (i * 17)).ColumnName = "PropActionB_Obs" & (i + 1).ToString       'AJ /BA /
            dt.Columns(36 + (i * 17)).ColumnName = "ResponDowIDB_Obs" & (i + 1).ToString      'AK /BB /
            dt.Columns(37 + (i * 17)).ColumnName = "StatusB_Obs" & (i + 1).ToString           'AL /BC /
            dt.Columns(38 + (i * 17)).ColumnName = "PropActionC_Obs" & (i + 1).ToString       'AM /BD /
            dt.Columns(39 + (i * 17)).ColumnName = "ResponDowIDC_Obs" & (i + 1).ToString      'AN /BE /
            dt.Columns(40 + (i * 17)).ColumnName = "StatusC_Obs" & (i + 1).ToString           'AO /BF /

            dt.Columns(126 + (i * 7)).ColumnName = "observTypeObs" & (i + 1).ToString
            dt.Columns(127 + (i * 7)).ColumnName = "contractorObs" & (i + 1).ToString
            dt.Columns(128 + (i * 7)).ColumnName = "picCountObs" & (i + 1).ToString
            dt.Columns(129 + (i * 7)).ColumnName = "picUrl1Obs" & (i + 1).ToString
            dt.Columns(130 + (i * 7)).ColumnName = "picUrl2Obs" & (i + 1).ToString
            dt.Columns(131 + (i * 7)).ColumnName = "picUrl3Obs" & (i + 1).ToString
            dt.Columns(132 + (i * 7)).ColumnName = "picUrl4Obs" & (i + 1).ToString
        Next

        'Debug --Bind Data to GridView
        RadGrid1.DataSource = dt
        RadGrid1.DataBind()

        Page.Server.ScriptTimeout = 4000
        Dim j As Integer = 0
        Dim count As Integer = 0
        For Each row As DataRow In dt.Rows
            Dim drow As DataRow = dt.Rows.Item(j)

            If dt.Rows.Item(j)("ActionNumber") IsNot DBNull.Value AndAlso dt.Rows.Item(j)("ActionNumber") <> "" Then
                Dim result As Integer = ImportDataAction_newMPT(drow)

                Select Case result
                    Case 0
                        Dim InputId As Integer = getEmpId(dt.Rows.Item(j)("Observer#1_DowID"))
                        If InputId = 0 Then
                            AddListBox("Insert action data, " & dt.Rows.Item(j)("ActionNumber") & ".., " & dt.Rows.Item(j)("Observer#1_DowID") & " not exist.", 0)
                        Else
                            AddListBox("Insert action data, " & dt.Rows.Item(j)("ActionNumber") & ".., FAIL!!.", 0)
                        End If
                    Case 200 + CInt(dt.Rows.Item(j)("No.Observe"))
                        AddListBox("Insert action data, Action Number " & dt.Rows.Item(j)("ActionNumber") & ".... OK.", 1)
                    Case 100
                        AddListBox("Action Number " & dt.Rows.Item(j)("ActionNumber") & " repeat record.", 1)
                    Case Else
                        Dim InputId As Integer = getEmpId(dt.Rows.Item(j)("Observer#1_DowID"))
                        If InputId = 0 Then
                            AddListBox("Insert action data, " & dt.Rows.Item(j)("ActionNumber") & "..," & dt.Rows.Item(j)("Observer#1_DowID") & " not exist.", 0)
                        Else
                            AddListBox("Some process fail.", 0)
                        End If
                End Select
                count = count + 1
            End If
            j = j + 1
        Next
        AddListBox("#### Imported " & count & " records complete.", 0)
    End Sub

    Private Function ImportDataAction_newMPT(ByVal actionRow As DataRow) As Integer
        Dim dr As DataRow = actionRow
        Dim recActNo As String = dr.Item("ActionNumber").ToString.Trim

        If Not chkContinue.Checked Then
            'new record
            If recActNoRepeat(recActNo) Then
                Return 100
            End If
        End If

        Dim result As Integer = 0
        Dim IsCancel As Boolean = False
        Dim strIns As String = "INSERT INTO tblRecord(tempFlag, tempLock, timestamp, departId, recActive, recActNo, recActNoValue, recActMonth, recActYear, recActDate, recActTime, durationH, durationM, durationValue, empId, oEmpCount, noObserve) 
                                VALUES(@tempFlag, @tempLock, @timestamp, @departId, @recActive, @recActNo, @recActNoValue, @recActMonth, @recActYear, @recActDate, @recActTime, @durationH, @durationM, @durationValue, @empId, @oEmpCount, @noObserve)"

        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(strIns, connection)
            command.Parameters.Add("@recActive", SqlDbType.Bit).Value = True
            command.Parameters.Add("@tempFlag", SqlDbType.Bit).Value = False
            command.Parameters.Add("@tempLock", SqlDbType.DateTime).Value = Now()
            command.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = Now()

            '====== Date Format =======
            Dim recDate As Date = dr.Item("ActionDate")
            command.Parameters.Add("@recActDate", SqlDbType.Date).Value = recDate

            '====== Doc Number ========
            Dim idxYear As Integer = 0
            Dim idxYearStr As String = ""
            Dim recActNo_New As String = ""
            Dim recActNoValue As Integer
            If rbtActNumberFormat.SelectedValue = "1" Then      'YY
                idxYearStr = rcbYear.Text.Substring(2, 2)
                idxYear = recActNo.IndexOf(idxYearStr)
                'command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = recActNo.Substring(idxYear + 2, 2)
                command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = recDate.Month
                command.Parameters.Add("@recActYear", SqlDbType.Int).Value = CInt(recActNo.Substring(idxYear, 2)) + 2000
                recActNoValue = CInt(recActNo.Substring(idxYear))
                recActNo_New = recActNo     'original recNo
            ElseIf rbtActNumberFormat.SelectedValue = "2" Then
                idxYearStr = rcbYear.Text
                idxYear = recActNo.IndexOf(idxYearStr)          'YYYY
                'command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = CInt(recActNo.Substring(idxYear + 4, 2))
                command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = recDate.Month
                command.Parameters.Add("@recActYear", SqlDbType.Int).Value = CInt(recActNo.Substring(idxYear, 4))
                recActNoValue = CInt(recActNo.Substring(idxYear + 2))
                recActNo_New = recActNo.Substring(0, idxYear) & rcbYear.Text.Substring(2, 2) & recActNo.Substring(idxYear + 4)
            End If

            Dim departId As Integer = getDepartId(recActNo.Substring(0, idxYear))
            command.Parameters.Add("@departId", SqlDbType.Int).Value = departId

            If Not chkContinue.Checked Then
                command.Parameters.Add("@recActNoValue", SqlDbType.Int).Value = recActNoValue
                command.Parameters.Add("@recActNo", SqlDbType.NVarChar).Value = recActNo_New
            Else
                'continue actionnumber
                Dim autoNumber As New aAutoNumber
                Dim actualNumbervalue As Integer = autoNumber.ActNumberAutoNumDepartId(departId, CDate(dr.Item("ActionDate")))

                recActNo_New = recActNo.Substring(0, idxYear) & (actualNumbervalue + 1).ToString
                command.Parameters.Add("@recActNoValue", SqlDbType.Int).Value = actualNumbervalue + 1
                command.Parameters.Add("@recActNo", SqlDbType.NVarChar).Value = recActNo_New
            End If
            '==========================

            Dim ActTime As String = dr.Item("ActionTime")
            Dim idx As Integer = ActTime.IndexOf(":") + 1
            command.Parameters.Add("@recActTime", SqlDbType.Time).Value = ActTime.Substring(0, idx) & ActTime.Substring(idx, 2).ToString & ":00"

            Dim duValue As Integer = CInt(dr.Item("DurationValue"))
            command.Parameters.Add("@durationH", SqlDbType.Int).Value = duValue \ 60
            command.Parameters.Add("@durationM", SqlDbType.Int).Value = duValue Mod 60
            command.Parameters.Add("@durationValue", SqlDbType.Int).Value = duValue

            Dim InputId As Integer = getEmpId(dr.Item("Observer#1_DowID"))
            command.Parameters.Add("@empId", SqlDbType.Int).Value = InputId
            If InputId = 0 Then IsCancel = True

            '====== Other Employee ========
            Dim oEmpCount As Integer = 0
            Dim oEmpList As New ArrayList
            If dr.Item("OthObserv1_DowID") IsNot DBNull.Value AndAlso Trim(dr.Item("OthObserv1_DowID")) <> "" Then
                oEmpCount = oEmpCount + 1
                oEmpList.Add(getEmpId(dr.Item(9)))      'DowId
                If dr.Item("OthObserv2_DowID") IsNot DBNull.Value AndAlso Trim(dr.Item("OthObserv2_DowID")) <> "" Then oEmpCount = oEmpCount + 1 : oEmpList.Add(getEmpId(dr.Item("OthObserv2_DowID")))
                If dr.Item("OthObserv3_DowID") IsNot DBNull.Value AndAlso Trim(dr.Item("OthObserv3_DowID")) <> "" Then oEmpCount = oEmpCount + 1 : oEmpList.Add(getEmpId(dr.Item("OthObserv3_DowID")))
                If dr.Item("OthObserv4_DowID") IsNot DBNull.Value AndAlso Trim(dr.Item("OthObserv4_DowID")) <> "" Then oEmpCount = oEmpCount + 1 : oEmpList.Add(getEmpId(dr.Item("OthObserv4_DowID")))
                If dr.Item("OthObserv5_DowID") IsNot DBNull.Value AndAlso Trim(dr.Item("OthObserv5_DowID")) <> "" Then oEmpCount = oEmpCount + 1 : oEmpList.Add(getEmpId(dr.Item("OthObserv5_DowID")))
            End If
            command.Parameters.Add("@oEmpCount", SqlDbType.Int).Value = oEmpCount
            command.Parameters.Add("@noObserve", SqlDbType.Int).Value = CInt(dr.Item("No.Observe"))
            If Not IsCancel Then result = command.ExecuteNonQuery()

            'save otherObserv
            Dim recId As Integer = getRecId(recActNo_New)
            If Not IsCancel Then saveOtherObserv(recId, oEmpList)

            'save action detail
            If result > 0 Then Return (result * 200) + saveObservDetail_newMPT(recId, actionRow) Else Return 0
        End Using
    End Function

    Private Function saveObservDetail_newMPT(ByVal recId As Integer, ByVal actionDataRow As DataRow) As Integer
        Dim Result As Integer = 0
        Dim dr As DataRow = actionDataRow
        Dim NoObs As Integer = CInt(dr.Item("No.Observe"))
        For j = 1 To NoObs
            Dim strIns As String = "INSERT INTO tblRecordDetail(recId, observItem, title, category, categorySub, failurePoint, equipment, IsHRO, hroChk1, hroChk2, hroChk3, hroChk4, hroChk5, secondEye, recognition, observType, contractor, 
                                    description, proposeEnable_A, proposeRespPerson_A, proposeAction_A, proposeStatus_A, proposeComplete_A, proposeEnable_B, proposeRespPerson_B, proposeAction_B, proposeStatus_B, proposeComplete_B, 
                                    proposeEnable_C, proposeRespPerson_C, proposeAction_C, proposeStatus_C, proposeComplete_C, observComplete) 
                                    VALUES(@recId, @observItem, @title, @category, @categorySub, @failurePoint, @equipment, @IsHRO, @hroChk1, @hroChk2, @hroChk3, @hroChk4, @hroChk5, @secondEye, @recognition, @observType, @contractor, 
                                    @description, @proposeEnable_A, @proposeRespPerson_A, @proposeAction_A, @proposeStatus_A, @proposeComplete_A, @proposeEnable_B, @proposeRespPerson_B, @proposeAction_B, @proposeStatus_B, @proposeComplete_B, 
                                    @proposeEnable_C, @proposeRespPerson_C, @proposeAction_C, @proposeStatus_C, @proposeComplete_C, @observComplete)"

            Using conn As New SqlConnection(ConnStr)
                conn.Open()
                Dim command As New SqlCommand(strIns, conn)
                command.Parameters.Add("@recId", SqlDbType.Int).Value = recId
                command.Parameters.Add("@observItem", SqlDbType.Int).Value = j

                Dim TitleObs As String = dr.Item("Title_Obs" & j.ToString).ToString.Trim
                'If TitleObs.Substring(0, 1) = "-" Or TitleObs.Substring(0, 1) = "+" Or TitleObs.Substring(0, 1) = "=" Then TitleObs = "'" & TitleObs
                command.Parameters.Add("@title", SqlDbType.NVarChar).Value = TitleObs.Substring(1)

                Dim failureAndCate As New cFailureCategory
                Dim cateId As Integer = failureAndCate.GetCategoryId(dr.Item("Category_Obs" & j.ToString))
                Dim cateSubId As Integer = 1000
                If dr.Item("SubCategory_Obs" & j.ToString) IsNot DBNull.Value Then cateSubId = failureAndCate.GetCategorySubId(dr.Item("SubCategory_Obs" & j.ToString), cateId)
                Dim failurePointId As Integer = 1000
                If dr.Item("FailPoint_Obs" & j.ToString) IsNot DBNull.Value Then failurePointId = failureAndCate.GetFailPointId(dr.Item("FailPoint_Obs" & j.ToString), cateSubId)
                command.Parameters.Add("@category", SqlDbType.Int).Value = cateId
                command.Parameters.Add("@categorySub", SqlDbType.Int).Value = cateSubId
                command.Parameters.Add("@failurePoint", SqlDbType.Int).Value = failurePointId

                Dim EquipmentObs As String = dr.Item("Equipmen_Obs" & j.ToString).ToString.Trim
                'If EquipmentObs <> "" AndAlso (EquipmentObs.Substring(0, 1) = "-" Or EquipmentObs.Substring(0, 1) = "+" Or EquipmentObs.Substring(0, 1) = "=") Then EquipmentObs = "'" & EquipmentObs
                command.Parameters.Add("@equipment", SqlDbType.NVarChar).Value = EquipmentObs.Substring(1)

                Dim HRO As Boolean = False
                Dim HROop1 As Boolean = False
                Dim HROop2 As Boolean = False
                Dim HROop3 As Boolean = False
                Dim HROop4 As Boolean = False
                Dim HROop5 As Boolean = False

                If dr.Item("HRO_Obs" & j.ToString) IsNot DBNull.Value Then
                    Dim HROStrImport As String = dr.Item("HRO_Obs" & j.ToString)

                    Dim HROStr As String = ""
                    Dim idx As Integer = 0
                    While HROStrImport <> ""
                        idx = HROStrImport.IndexOf(", ")
                        If idx <> -1 Then
                            HROStr = HROStrImport.Substring(0, idx)
                            HROStrImport = HROStrImport.Substring(idx + 2)
                        Else
                            HROStr = HROStrImport
                            HROStrImport = ""
                        End If

                        If HROStr = "Expect the Unexpected" Then
                            HROop1 = True
                        ElseIf HROStr = "Do Not Generalize" Then
                            HROop2 = True
                        ElseIf HROStr = "Identify Trend & Anticipate Impact" Then
                            HROop3 = True
                        ElseIf HROStr = "Engage & Apply Expertise" Then
                            HROop4 = True
                        ElseIf HROStr = "Commit to Resilience" Then
                            HROop5 = True
                        End If
                    End While
                    If HROop1 = True Or HROop2 = True Or HROop3 = True Or HROop4 = True Or HROop5 = True Then HRO = True
                End If

                command.Parameters.Add("@IsHRO", SqlDbType.Bit).Value = HRO
                command.Parameters.Add("@hroChk1", SqlDbType.Bit).Value = HROop1
                command.Parameters.Add("@hroChk2", SqlDbType.Bit).Value = HROop2
                command.Parameters.Add("@hroChk3", SqlDbType.Bit).Value = HROop3
                command.Parameters.Add("@hroChk4", SqlDbType.Bit).Value = HROop4
                command.Parameters.Add("@hroChk5", SqlDbType.Bit).Value = HROop5
                command.Parameters.Add("@secondEye", SqlDbType.Bit).Value = CBool(dr.Item("2ndEye_Obs" & j.ToString))

                'status
                Dim StatusA As Integer = 1002
                Dim IsRecogA As Boolean = False
                If dr.Item("StatusA_Obs" & j.ToString).ToString.Trim = "Completed" Or dr.Item("StatusA_Obs" & j.ToString).ToString.Trim = "Complete" Then StatusA = 1003
                If dr.Item("StatusA_Obs" & j.ToString).ToString.Trim = "Recognition" Then StatusA = 1001
                If StatusA = 1001 Then IsRecogA = True
                Dim IsCompleteA As Boolean = False
                If StatusA = 1003 Then IsCompleteA = True

                command.Parameters.Add("@recognition", SqlDbType.Bit).Value = IsRecogA
                command.Parameters.Add("@observType", SqlDbType.Int).Value = CInt(dr.Item("observTypeObs" & j.ToString))
                command.Parameters.Add("@contractor", SqlDbType.Int).Value = CInt(dr.Item("contractorObs" & j.ToString))

                Dim DescriptionObs As String = dr.Item("Description_Obs" & j.ToString).ToString.Trim
                'If DescriptionObs <> "" AndAlso (DescriptionObs.Substring(0, 1) = "-" Or DescriptionObs.Substring(0, 1) = "+" Or DescriptionObs.Substring(0, 1) = "=") Then DescriptionObs = "'" & DescriptionObs
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = DescriptionObs.Substring(1)

                Dim propEnaA As Boolean = False
                Dim propStrChk As String = dr.Item("Title_Obs" & j.ToString)
                If propStrChk <> "" Then propEnaA = True
                command.Parameters.Add("@proposeEnable_A", SqlDbType.Bit).Value = propEnaA

                If IsRecogA Then
                    command.Parameters.Add("@proposeRespPerson_A", SqlDbType.Int).Value = 0
                Else
                    Dim ResponId As Integer = 0
                    If dr.Item("ResponDowIDA_Obs" & j.ToString) IsNot DBNull.Value OrElse dr.Item("ResponDowIDA_Obs" & j.ToString).ToString <> "" Then
                        ResponId = getEmpId(dr.Item("ResponDowIDA_Obs" & j.ToString))
                    Else
                        ResponId = getEmpId("EMP_NA")      'N/A employee
                    End If
                    command.Parameters.Add("@proposeRespPerson_A", SqlDbType.Int).Value = ResponId
                End If
                command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = dr.Item("PropActionA_Obs" & j.ToString).ToString.Trim.Substring(1)
                command.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = StatusA
                command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = IsCompleteA

                '-- proprose B
                Dim propEnaB As Boolean = False
                Dim StatusB As Integer = 1002
                Dim IsRecogB As Boolean = False
                Dim IsCompleteB As Boolean = False
                Select Case dr.Item("StatusB_Obs" & j.ToString).ToString.Trim
                    Case "Completed"
                        StatusB = 1003
                        IsCompleteB = True
                    Case "Complete"
                        StatusB = 1003
                        IsCompleteB = True
                    Case "In-progress"
                        StatusB = 1002
                    Case "Recognition"
                        StatusB = 1001
                        IsRecogB = True
                    Case Else
                        StatusB = 1000
                End Select

                If dr.Item("PropActionB_Obs" & j.ToString).ToString <> "" And dr.Item("StatusB_Obs" & j.ToString).ToString <> "" Then
                    propEnaB = True
                    If IsRecogB Then
                        command.Parameters.Add("@proposeRespPerson_B", SqlDbType.Int).Value = 0
                    Else
                        Dim ResponId As Integer = 0
                        If dr.Item("ResponDowIDB_Obs" & j.ToString) IsNot DBNull.Value OrElse dr.Item("ResponDowIDB_Obs" & j.ToString).ToString <> "" Then
                            ResponId = getEmpId(dr.Item("ResponDowIDB_Obs" & j.ToString))
                        Else
                            ResponId = getEmpId("EMP_NA")      'N/A employee
                        End If
                        command.Parameters.Add("@proposeRespPerson_B", SqlDbType.Int).Value = ResponId
                    End If
                    command.Parameters.Add("@proposeAction_B", SqlDbType.NVarChar).Value = dr.Item("PropActionB_Obs" & j.ToString).ToString.Trim.Substring(1)
                Else
                    command.Parameters.Add("@proposeRespPerson_B", SqlDbType.Bit).Value = DBNull.Value
                    command.Parameters.Add("@proposeAction_B", SqlDbType.NVarChar).Value = DBNull.Value
                End If
                command.Parameters.Add("@proposeEnable_B", SqlDbType.Bit).Value = propEnaB
                command.Parameters.Add("@proposeStatus_B", SqlDbType.Int).Value = StatusB
                command.Parameters.Add("@proposeComplete_B", SqlDbType.Bit).Value = IsCompleteB

                '-- proprose C
                Dim propEnaC As Boolean = False
                Dim StatusC As Integer = 1002
                Dim IsRecogC As Boolean = False
                Dim IsCompleteC As Boolean = False
                Select Case dr.Item("StatusC_Obs" & j.ToString).ToString.Trim
                    Case "Completed"
                        StatusC = 1003
                        IsCompleteC = True
                    Case "Complete"
                        StatusC = 1003
                        IsCompleteC = True
                    Case "In-progress"
                        StatusC = 1002
                    Case "Recognition"
                        StatusC = 1001
                        IsRecogC = True
                    Case Else
                        StatusC = 1000
                End Select

                If dr.Item("PropActionC_Obs" & j.ToString).ToString <> "" And dr.Item("StatusC_Obs" & j.ToString).ToString <> "" Then
                    propEnaC = True
                    If IsRecogC Then
                        command.Parameters.Add("@proposeRespPerson_C", SqlDbType.Int).Value = 0
                    Else
                        Dim ResponId As Integer = 0
                        If dr.Item("ResponDowIDC_Obs" & j.ToString) IsNot DBNull.Value OrElse dr.Item("ResponDowIDC_Obs" & j.ToString).ToString <> "" Then
                            ResponId = getEmpId(dr.Item("ResponDowIDC_Obs" & j.ToString))
                        Else
                            ResponId = getEmpId("EMP_NA")      'N/A employee
                        End If
                        command.Parameters.Add("@proposeRespPerson_C", SqlDbType.Int).Value = ResponId
                    End If
                    command.Parameters.Add("@proposeAction_C", SqlDbType.NVarChar).Value = dr.Item("PropActionC_Obs" & j.ToString).ToString.Trim.Substring(1)
                Else
                    command.Parameters.Add("@proposeRespPerson_C", SqlDbType.Bit).Value = DBNull.Value
                    command.Parameters.Add("@proposeAction_C", SqlDbType.NVarChar).Value = DBNull.Value
                End If
                command.Parameters.Add("@proposeEnable_C", SqlDbType.Bit).Value = propEnaC
                command.Parameters.Add("@proposeStatus_C", SqlDbType.Int).Value = StatusC
                command.Parameters.Add("@proposeComplete_C", SqlDbType.Bit).Value = IsCompleteC

                '-- all proprose 
                Dim allStatus As New cStatus
                command.Parameters.Add("@observComplete", SqlDbType.Int).Value = allStatus.observStatus(StatusA, propEnaB, StatusB, propEnaC, StatusC)

                Dim resultItem As Integer = command.ExecuteNonQuery()

                If resultItem > 0 Then
                    'Add Images
                    Dim PictureCount As Integer = 0
                    If dr.Item("picCountObs" & j.ToString) IsNot DBNull.Value OrElse dr.Item("picCountObs" & j.ToString).ToString <> "" Then
                        PictureCount = CInt(dr.Item("picCountObs" & j.ToString))
                    End If

                    If PictureCount > 0 Then
                        Dim strInsPic As String = "INSERT INTO tblRecordPictureO(recId, observeItem, picItem, picUrl) VALUES(@recId, @observeItem, @picItem, @picUrl)"
                        Dim connInsPic As New SqlConnection(ConnStr)
                        connInsPic.Open()
                        Dim commInsPic As SqlCommand

                        For k As Integer = 1 To PictureCount
                            commInsPic = New SqlCommand(strInsPic, connInsPic)
                            commInsPic.Parameters.Add("@recId", SqlDbType.Int).Value = recId
                            commInsPic.Parameters.Add("@observeItem", SqlDbType.Int).Value = j - 1
                            commInsPic.Parameters.Add("@picItem", SqlDbType.Int).Value = k
                            commInsPic.Parameters.Add("@picUrl", SqlDbType.NVarChar).Value = dr.Item("picUrl" & k.ToString & "Obs" & j.ToString)

                            commInsPic.ExecuteNonQuery()
                        Next
                        connInsPic.Close()
                    End If

                    'view log
                    AddListBox("Insert observe data, " & dr.Item("ActionNumber") & ", observe " & j.ToString & " .. OK.", 1)
                End If

                Result = Result + resultItem

                'calculate all observe
                If j = NoObs Then
                    allStatus.UpdRecordIsComplete(recId)
                End If
            End Using
        Next

        Return Result
    End Function













    '==========================================================

    Private Sub ImportData(ByVal xlsFilename As String)
        Dim xlsApp As New Excel.Application
        Dim xlsBook As Excel.Workbook
        Dim xlsSheet1 As Excel.Worksheet

        xlsBook = xlsApp.Workbooks.Open(xlsFilename)
        xlsBook.Application.Visible = False
        xlsSheet1 = xlsBook.Worksheets(1)


        Dim dt As New System.Data.DataTable
        Dim dr As System.Data.DataRow
        dt.Columns.Add("departName")
        dt.Columns.Add("recActNo")
        dt.Columns.Add("recActDate")
        dt.Columns.Add("recActTime")
        dt.Columns.Add("durationValue")
        dt.Columns.Add("observer1")
        dt.Columns.Add("oEmpCount")
        dt.Columns.Add("noObserve")

        Dim oEmpCount As Integer = 1
        Dim rowStart As Integer = 2
        Dim i As Integer = rowStart
        Do While Not Trim(xlsSheet1.Cells.Item(i, 1).Value) = ""
            dr = dt.NewRow
            Dim ActionNumber As String = xlsSheet1.Cells.Item(i, 3).Value.ToString
            dr("departName") = ActionNumber.Substring(0, ActionNumber.IndexOf("2017"))
            dr("recActNo") = ActionNumber
            dr("recActDate") = xlsSheet1.Cells.Item(i, 4).Value
            dr("recActTime") = xlsSheet1.Cells.Item(i, 5).Value
            dr("durationValue") = xlsSheet1.Cells.Item(i, 6).Value
            dr("observer1") = xlsSheet1.Cells.Item(i, 8).Value
            If Trim(xlsSheet1.Cells.Item(i, 14).Value) <> "" Then oEmpCount = oEmpCount + 1
            If Trim(xlsSheet1.Cells.Item(i, 17).Value) <> "" Then oEmpCount = oEmpCount + 1
            If Trim(xlsSheet1.Cells.Item(i, 20).Value) <> "" Then oEmpCount = oEmpCount + 1
            dr("oEmpCount") = oEmpCount.ToString
            dr("noObserve") = xlsSheet1.Cells.Item(i, 20).Value
            dt.Rows.Add(dr)
            i = i + 1
        Loop
        AddListBox("Generate master data.., complete.", 1)

        '-- insert data
        Dim strIns As String = "INSERT INTO tblRecord(tempFlag, tempLock, timestamp, departId, recActive, recActNo, recActNoValue, recActMonth, recActYear, recActDate, recActTime, durationH, durationM, durationValue, empId, oEmpCount, noObserve) 
                                VALUES(@tempFlag, @tempLock, @timestamp, @departId, @recActive, @recActNo, @recActNoValue, @recActMonth, @recActYear, @recActDate, @recActTime, @durationH, @durationM, @durationValue, @empId, @oEmpCount, @noObserve)"

        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As SqlCommand
        Dim result As Integer = 0
        For i = 0 To dt.Rows.Count - 1
            command = New SqlCommand(strIns, conn)
            command.Parameters.Add("@recActive", SqlDbType.Bit).Value = True
            command.Parameters.Add("@tempFlag", SqlDbType.Bit).Value = False
            command.Parameters.Add("@tempLock", SqlDbType.DateTime).Value = Now()
            command.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = Now()

            command.Parameters.Add("@departId", SqlDbType.Int).Value = getDepartId(dt.Rows(i)("departName"))

            Dim exNo As String = dt.Rows(i)("recActNo")
            Dim idxYear As Integer = exNo.IndexOf("2017")
            Dim recActNo_New As String = exNo.Substring(0, idxYear) & exNo.Substring(idxYear + 2)
            Dim recActNo As String = dt.Rows(i)("recActNo")
            command.Parameters.Add("@recActNo", SqlDbType.NVarChar).Value = recActNo
            command.Parameters.Add("@recActNoValue", SqlDbType.Int).Value = CInt(exNo.Substring(idxYear + 2))
            command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = exNo.Substring(idxYear + 4, 2)
            command.Parameters.Add("@recActYear", SqlDbType.Int).Value = exNo.Substring(idxYear, 4)

            Dim recDate As Date = dt.Rows(i)("recActDate")
            Dim recDay As Integer = recDate.Day
            Dim recMonth As Integer = recDate.Month
            Dim recYear As Integer = recDate.Year
            If recYear < 2000 Then recYear = recYear + 543
            If recYear < 2500 Then recYear = recYear + 543

            command.Parameters.Add("@recActDate", SqlDbType.Date).Value = recDay & "/" & recMonth & "/" & recYear
            command.Parameters.Add("@recActTime", SqlDbType.Time).Value = dt.Rows(i)("recActTime")

            Dim duValue As Integer = CInt(dt.Rows(i)("durationValue"))
            command.Parameters.Add("@durationH", SqlDbType.Int).Value = duValue \ 60
            command.Parameters.Add("@durationM", SqlDbType.Int).Value = duValue Mod 60
            command.Parameters.Add("@durationValue", SqlDbType.Int).Value = duValue

            command.Parameters.Add("@empId", SqlDbType.Int).Value = getEmpId(dt.Rows(i)("observer1"))
            command.Parameters.Add("@oEmpCount", SqlDbType.Int).Value = dt.Rows(i)("oEmpCount")
            command.Parameters.Add("@noObserve", SqlDbType.Int).Value = dt.Rows(i)("noObserve")

            '---- check repeat
            If Not recActNoRepeat(recActNo) Then
                result = command.ExecuteNonQuery()
                If result > 0 Then
                    AddListBox("Insert Master data, " & recActNo & ".., complete.", 1)
                Else
                    AddListBox("Insert Master data, " & recActNo & ".., FAIL!!.", 1)
                End If

                '-- get last insert record
                Dim recId As Integer = 0
                Dim selRecIdStr As String = "SELECT recId FROM tblRecord WHERE recActNo = @recActNo"
                Using connection2 As New SqlConnection(ConnStr)
                    connection2.Open()
                    Dim command2 As New SqlCommand(selRecIdStr, connection2)
                    Dim recActNUmber As String = recActNo
                    command2.Parameters.Add("@recActNo", SqlDbType.NVarChar).Value = recActNUmber
                    Dim DataRead As SqlDataReader
                    DataRead = command2.ExecuteReader()
                    While DataRead.Read()
                        recId = DataRead("recId")
                    End While
                End Using

                '---- insert detail
                Dim dt2 As New System.Data.DataTable
                Dim dr2 As System.Data.DataRow
                dt2.Columns.Add("title")
                dt2.Columns.Add("category")
                dt2.Columns.Add("categorySub")
                dt2.Columns.Add("failurePoint")
                dt2.Columns.Add("equipment")
                dt2.Columns.Add("HRO")
                dt2.Columns.Add("secondEye")
                dt2.Columns.Add("description")
                dt2.Columns.Add("proposeDesc")

                Dim k As Integer = rowStart
                Dim noOb As Integer = CInt(dt.Rows(i)("noObserve"))
                For j = 0 To noOb - 1
                    dr2 = dt2.NewRow
                    dr2("title") = xlsSheet1.Cells.Item(k, (j * 9) + 21).Value
                    dr2("category") = xlsSheet1.Cells.Item(k, (j * 9) + 22).Value
                    dr2("categorySub") = xlsSheet1.Cells.Item(k, (j * 9) + 23).Value
                    dr2("failurePoint") = xlsSheet1.Cells.Item(k, (j * 9) + 24).Value
                    dr2("equipment") = xlsSheet1.Cells.Item(k, (j * 9) + 25).Value
                    dr2("HRO") = xlsSheet1.Cells.Item(k, (j * 9) + 26).Value
                    dr2("secondEye") = xlsSheet1.Cells.Item(k, (j * 9) + 27).Value
                    dr2("description") = xlsSheet1.Cells.Item(k, (j * 9) + 28).Value
                    dr2("proposeDesc") = xlsSheet1.Cells.Item(k, (j * 9) + 29).Value
                    dt2.Rows.Add(dr2)
                Next

                Dim result3 As Integer = 0
                For j = 0 To noOb - 1
                    Dim strIns3 As String = "INSERT INTO tblRecordDetail(recId, observItem, title, category, categorySub, failurePoint, equipment, IsHRO, hroChk1, hroChk2, hroChk3, hroChk4, hroChk5, secondEye, recognition, observType, description, 
                                        proposeEnable_A, proposeRespPerson_A, proposeAction_A, proposeStatus_A, proposeComplete_A, proposeEnable_B, proposeEnable_C, observComplete) 
                                        VALUES(@recId, @observItem, @title, @category, @categorySub, @failurePoint, @equipment, @IsHRO, @hroChk1, @hroChk2, @hroChk3, @hroChk4, @hroChk5, @secondEye, @recognition, @observType, @description, 
                                        @proposeEnable_A, @proposeRespPerson_A, @proposeAction_A, @proposeStatus_A, @proposeComplete_A, @proposeEnable_B, @proposeEnable_C, @observComplete)"

                    Dim connection3 As New SqlConnection(ConnStr)
                    connection3.Open()
                    Dim command3 As SqlCommand
                    command3 = New SqlCommand(strIns3, connection3)
                    command3.Parameters.Add("@recId", SqlDbType.Int).Value = recId
                    command3.Parameters.Add("@observItem", SqlDbType.Int).Value = j + 1
                    command3.Parameters.Add("@title", SqlDbType.NVarChar).Value = dt2.Rows(j)("title")

                    Dim failureAndCate As New cFailureCategory
                    Dim cateId As Integer = failureAndCate.GetCategoryId(dt2.Rows(j)("category"))
                    Dim cateSubId As Integer = failureAndCate.GetCategorySubId(dt2.Rows(j)("categorySub"), cateId)
                    Dim failurePointId As Integer = failureAndCate.GetFailPointId(dt2.Rows(j)("failurePoint"), cateSubId)
                    command3.Parameters.Add("@category", SqlDbType.Int).Value = cateId
                    command3.Parameters.Add("@categorySub", SqlDbType.Int).Value = cateSubId
                    command3.Parameters.Add("@failurePoint", SqlDbType.Int).Value = failurePointId
                    command3.Parameters.Add("@equipment", SqlDbType.NVarChar).Value = dt2.Rows(j)("equipment")

                    Dim HRO As Boolean = False
                    Dim HROop1 As Boolean = False
                    Dim HROop2 As Boolean = False
                    Dim HROop3 As Boolean = False
                    Dim HROop4 As Boolean = False
                    Dim HROop5 As Boolean = False

                    Dim HROStrImport As String = dt2.Rows(j)("HRO")
                    Dim HROStr As String = ""
                    Dim idx As Integer = 0
                    While HROStrImport <> ""
                        idx = HROStrImport.IndexOf(", ")
                        HROStr = HROStrImport.Substring(0, idx)
                        HROStrImport = HROStrImport.Substring(idx + 2)

                        If HROStr = "Expect the Unexpected" Then
                            HROop1 = True
                        ElseIf HROStr = "Do Not Generalize" Then
                            HROop2 = True
                        ElseIf HROStr = "Identify Trend & Anticipate Impact" Then
                            HROop3 = True
                        ElseIf HROStr = "Engage & Apply Expertise" Then
                            HROop4 = True
                        ElseIf HROStr = "Commit to Resilience" Then
                            HROop5 = True
                        End If
                    End While

                    If HROop1 = True Or HROop2 = True Or HROop3 = True Or HROop4 = True Or HROop5 = True Then
                        HRO = True
                    End If

                    command3.Parameters.Add("@IsHRO", SqlDbType.Bit).Value = HRO
                    command3.Parameters.Add("@hroChk1", SqlDbType.Bit).Value = HROop1
                    command3.Parameters.Add("@hroChk2", SqlDbType.Bit).Value = HROop2
                    command3.Parameters.Add("@hroChk3", SqlDbType.Bit).Value = HROop3
                    command3.Parameters.Add("@hroChk4", SqlDbType.Bit).Value = HROop4
                    command3.Parameters.Add("@hroChk5", SqlDbType.Bit).Value = HROop5

                    command3.Parameters.Add("@secondEye", SqlDbType.Bit).Value = dt2.Rows(j)("secondEye")

                    Dim IsRecog As Boolean = False
                    Dim RecoqStrChk As String = dt2.Rows(j)("proposeDesc")
                    If RecoqStrChk = "Recognition *Propose action is not required*" Then IsRecog = True
                    Dim Status As Integer = 1002
                    If IsRecog Then Status = 1001
                    command3.Parameters.Add("@recognition", SqlDbType.Bit).Value = IsRecog
                    command3.Parameters.Add("@observType", SqlDbType.Int).Value = 0
                    command3.Parameters.Add("@description", SqlDbType.NVarChar).Value = dt2.Rows(j)("description")

                    Dim proEnaA As Boolean = False
                    Dim proStrChk As String = dt2.Rows(j)("title")
                    If proStrChk <> "" Then proEnaA = True
                    command3.Parameters.Add("@proposeEnable_A", SqlDbType.Bit).Value = proEnaA
                    command3.Parameters.Add("@proposeRespPerson_A", SqlDbType.Int).Value = 0
                    command3.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = dt2.Rows(j)("proposeDesc")
                    command3.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = Status
                    command3.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = False

                    command3.Parameters.Add("@proposeEnable_B", SqlDbType.Bit).Value = False
                    command3.Parameters.Add("@proposeEnable_C", SqlDbType.Bit).Value = False
                    command3.Parameters.Add("@observComplete", SqlDbType.Int).Value = Status

                    result3 = command3.ExecuteNonQuery()
                    connection3.Close()

                    If result3 > 0 Then
                        AddListBox("Insert observe data, " & recActNo & "(" & j + 1 & ").., complete.", 1)
                    Else
                        AddListBox("Insert observe data, " & recActNo & "(" & j + 1 & ").., FAIL!!.", 1)
                    End If
                Next
            Else
                AddListBox("recActNo & " & "Repeat record.", 1)
            End If
            infobox.DataBind()
        Next
        conn.Close()

        AddListBox("Import data.., complete.", 1)
        xlsApp.Application.Quit()
        xlsApp.Quit()
        xlsSheet1 = Nothing
        xlsBook = Nothing
        xlsApp = Nothing
    End Sub


End Class