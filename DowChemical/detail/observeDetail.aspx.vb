Imports System.Web.Services
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports AiLib
Imports System.Drawing
Imports System.IO
Imports Telerik.Web.UI.Calendar
Imports Microsoft.AspNet.Identity

Public Class ObserveDetail
    Inherits Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Dim MsgRecognition As String = "Recognition *Propose action is not required*"

    Dim EncryptError As String = ""

    Private Sub MsgBoxRad(ByVal Msg As String, ByVal Width As Integer, ByVal Height As Integer)
        RadWindowManager1.Width = Width
        RadWindowManager1.Height = Height
        RadWindowManager1.RadAlert(Msg, Width + 100, Height + 72, "My Alert", "", "myAlertImage.png")
    End Sub

    Private Sub ObserveDetail_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Request.QueryString("rid") <> "" Then
            Dim Encrypt As New cEncrypt
            Dim encodeStr As String = Request.QueryString("rid")
            Dim ParaStr As String = Nothing
            Try
                ParaStr = Encrypt.Decrypt_aiPass(encodeStr)
            Catch ex As Exception
                EncryptError = ex.Message
            End Try

            If ParaStr IsNot Nothing Then
                Session("recId") = ParaStr.Substring(0, ParaStr.IndexOf("&obitem"))
                Session("observItem") = ParaStr.Substring(ParaStr.Length - 1)
            Else
                Session("recId") = ""
                Session("observItem") = ""
            End If
        End If
    End Sub

    Private Sub RadPanelBar1_ItemClick(sender As Object, e As RadPanelBarEventArgs) Handles RadPanelBar1.ItemClick
        If e.Item.Items.Count > 0 Then
            If e.Item.Text = "SETTING" Or e.Item.Text = "REPORT" Then
                e.Item.Selected = False
            End If
        End If
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

            If Request.QueryString("rid") <> "" And Session("recId") <> "" And Session("observItem") <> "" Then
                RadMultiPage1.SelectedIndex = 1
                '-- get Status
                Dim recId As Integer = Session("recId")
                Dim observItem As Integer = Session("observItem")
                hfRecId.Value = recId.ToString

                '-- get Data
                Dim conn As New SqlConnection(ConnStr)
                Dim strSql As String = "SELECT tblRecord.*, actionDepartment.departName AS actionDepartName, empOwner.empDowId AS ownerDowId, empOwner.empName AS ownerName, empOwner.empSurname AS ownerSurname, empOwner.empFullName AS ownerFullname, 
                                        empOwner.empDisplay AS ownerEmpDisplay, OwnerDepartment.departName AS ownerDepartName, tblRecord.oEmpCount, tblRecord.noObserve FROM tblRecord 
                                        INNER JOIN tblEmployee AS empOwner ON tblRecord.empId = empOwner.empId 
                                        INNER JOIN tblDepartment AS actionDepartment ON tblRecord.departId = actionDepartment.departId 
                                        INNER JOIN tblDepartment AS OwnerDepartment ON empOwner.departId = OwnerDepartment.departId 
                                        WHERE (dbo.tblRecord.recId = @recId)"
                Dim command As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
                command.Parameters.Add("@recId", SqlDbType.Int).Value = recId

                conn.Open()
                Dim DataRead As SqlDataReader = command.ExecuteReader()
                If DataRead.HasRows() Then
                    DataRead.Read()
                    If DataRead("recActNo") IsNot DBNull.Value Then
                        hfOwnerEmpId.Value = DataRead("empId")
                        hfOwnerDepartName.Value = DataRead("ownerDepartname")
                        lbObDepartName.Text = DataRead("actionDepartName")
                        lbActionNum.Text = DataRead("recActNo")
                        lbDocDate.Text = CDate(DataRead("recActDate")).ToShortDateString
                        Dim Doctime As TimeSpan = DataRead("recActTime")
                        lbDocTime.Text = Doctime.Hours.ToString("00") & ":" & Doctime.Minutes.ToString("00")
                        lbDuration.Text = DataRead("durationH") & " Hour " & DataRead("durationM") & " Minite"
                        lbOwnerFullname.Text = DataRead("ownerFullname")
                        lbOwnerDowId.Text = DataRead("ownerDowId")
                        lbOwnerDepart.Text = DataRead("ownerDepartName")

                        '-- get observe data
                        Using connOb As New SqlConnection(ConnStr)
                            connOb.Open()
                            Dim strSql_getOb As String = "SELECT tblRecordDetail.detailId, tblRecordDetail.observItem, tblRecordDetail.title, tblObsvCate.cateName, tblObsvCateSub.catesubName, tblObsvFailPoint.failpointName, 
                                                          tblRecordDetail.equipment, tblRecordDetail.IsHRO, tblRecordDetail.hroChk1, tblRecordDetail.hroChk2, tblRecordDetail.hroChk3, tblRecordDetail.hroChk4, tblRecordDetail.hroChk5, 
                                                          tblRecordDetail.secondEye, tblRecordDetail.recognition, tblRecordDetail.observType, tblContractor.contractorName, tblRecordDetail.pictureCount, tblRecordDetail.description, 
                                                          tblRecordDetail.proposeEnable_A, tblRecordDetail.proposeRespPerson_A, tblEmployee_A.empFullName AS empFullNameA, tblRecordDetail.proposeAction_A, tblRecordDetail.proposeStatus_A, tblStatus_A.statusDesc AS statusDescA, tblRecordDetail.proposeComplete_A, 
                                                          tblRecordDetail.proposeEnable_B, tblRecordDetail.proposeRespPerson_B, tblEmployee_B.empFullName AS empFullNameB, tblRecordDetail.proposeAction_B, tblRecordDetail.proposeStatus_B, tblStatus_B.statusDesc AS statusDescB, tblRecordDetail.proposeComplete_B, 
                                                          tblRecordDetail.proposeEnable_C, tblRecordDetail.proposeRespPerson_C, tblEmployee_C.empFullName AS empFullNameC, tblRecordDetail.proposeAction_C, tblRecordDetail.proposeStatus_C, tblStatus_C.statusDesc AS statusDescC, tblRecordDetail.proposeComplete_C, 
                                                          tblRecordDetail.observComplete FROM dbo.tblRecordDetail 
                                                          INNER JOIN dbo.tblObsvCate ON dbo.tblRecordDetail.category = dbo.tblObsvCate.cateId 
                                                          INNER JOIN dbo.tblObsvCateSub ON dbo.tblRecordDetail.categorySub = dbo.tblObsvCateSub.catesubId 
                                                          INNER JOIN dbo.tblObsvFailPoint ON dbo.tblRecordDetail.failurePoint = dbo.tblObsvFailPoint.failpointId 
                                                          LEFT OUTER JOIN dbo.tblContractor ON dbo.tblRecordDetail.contractor = dbo.tblContractor.contractorId 
                                                          LEFT OUTER JOIN dbo.tblStatus AS tblStatus_A ON dbo.tblRecordDetail.proposeStatus_A = tblStatus_A.statusId 
                                                          LEFT OUTER JOIN dbo.tblStatus AS tblStatus_B ON dbo.tblRecordDetail.proposeStatus_B = tblStatus_B.statusId 
                                                          LEFT OUTER JOIN dbo.tblStatus AS tblStatus_C ON dbo.tblRecordDetail.proposeStatus_C = tblStatus_C.statusId 
                                                          LEFT OUTER JOIN dbo.tblEmployee AS tblEmployee_A ON dbo.tblRecordDetail.proposeRespPerson_A = tblEmployee_A.empId 
                                                          LEFT OUTER JOIN dbo.tblEmployee AS tblEmployee_B ON dbo.tblRecordDetail.proposeRespPerson_B = tblEmployee_B.empId 
                                                          LEFT OUTER JOIN dbo.tblEmployee AS tblEmployee_C ON dbo.tblRecordDetail.proposeRespPerson_C = tblEmployee_C.empId 
                                                          WHERE (dbo.tblRecordDetail.recId = @recId) AND (dbo.tblRecordDetail.observItem = @observItem)"

                            Dim commandOb As New SqlCommand(strSql_getOb, connOb) With {.CommandType = CommandType.Text}
                            commandOb.Parameters.Add("@recId", SqlDbType.Int).Value = recId
                            commandOb.Parameters.Add("@observItem", SqlDbType.Int).Value = observItem + 1

                            Dim obDataRead As SqlDataReader
                            obDataRead = commandOb.ExecuteReader()
                            If obDataRead.HasRows() Then
                                obDataRead.Read()
                                If obDataRead("detailId") IsNot DBNull.Value Then
                                    hfDetailId.Value = obDataRead("detailId")
                                    lbObserveNo.Text = observItem + 1
                                    tbTitle.Text = obDataRead("title")
                                    tbCategory.Text = obDataRead("cateName")
                                    tbCategorySub.Text = obDataRead("catesubName")
                                    tbFailurePoint.Text = obDataRead("failpointName")
                                    tbEquipment.Text = obDataRead("equipment")

                                    Dim IsHRO As Boolean = CBool(obDataRead("IsHRO"))
                                    pnHRO.Visible = IsHRO
                                    chkHRO.Checked = IsHRO
                                    If IsHRO Then
                                        chkHROop1.Checked = obDataRead("hroChk1")
                                        chkHROop2.Checked = obDataRead("hroChk2")
                                        chkHROop3.Checked = obDataRead("hroChk3")
                                        chkHROop4.Checked = obDataRead("hroChk4")
                                        chkHROop5.Checked = obDataRead("hroChk5")
                                    End If
                                    chk2Eye.Checked = obDataRead("secondEye")
                                    chkRecognition.Checked = obDataRead("recognition")

                                    Dim obType As Integer = obDataRead("observType")
                                    If obType = 1 Then
                                        tbObserveType.Text = "Contractor"
                                        tbContractor.Text = obDataRead("contractorName")
                                        tbContractor.Visible = True
                                    Else
                                        tbObserveType.Text = "Employee"
                                        tbContractor.Visible = False
                                    End If

                                    Dim pictureCount As Integer = obDataRead("pictureCount")
                                    If pictureCount > 0 Then pnShowImage.Visible = True

                                    tbDescription.Text = obDataRead("description")

                                    Dim empLogin As New cEmployee
                                    Dim loginEmpId As Integer = empLogin.FindEmployeeIdbyUsername(User.Identity.Name)
                                    Dim ownerId As Integer = CInt(hfOwnerEmpId.Value)

                                    tbActionA.Text = obDataRead("proposeAction_A")
                                    If chkRecognition.Checked Then tbResponA.Text = "" Else tbResponA.Text = obDataRead("empFullNameA")
                                    Dim StatusA As Integer = CInt(obDataRead("proposeStatus_A"))
                                    hfStatusA.Value = StatusA
                                    Dim ImageUrl As New cImage
                                    imgStatusA.ImageUrl = ImageUrl.getImage_msg(StatusA, 0)
                                    lbStatusA.Text = obDataRead("statusDescA")
                                    If StatusA = 1003 Then chkCompleteA.Checked = True

                                    Dim ResponIdA As Integer = CInt(obDataRead("proposeRespPerson_A"))
                                    Dim logicChkA As Boolean = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN") Or ownerId = loginEmpId Or ResponIdA = loginEmpId
                                    tbActionA.Enabled = logicChkA And StatusA = 1002
                                    If tbActionA.Enabled Then tbActionA.CssClass = "form-control input-sm" : pnActionA.Attributes.Add("class", "row thisEdit")      'Edit Mode
                                    chkCompleteA.Enabled = logicChkA And StatusA = 1002
                                    If chkCompleteA.Enabled Then chkBorderA.Attributes.Add("class", "statusCompleteEdit")

                                    Dim EnableB As Boolean = obDataRead("proposeEnable_B")
                                    If EnableB Then
                                        pnResponB.Visible = True
                                        tbActionB.Text = obDataRead("proposeAction_B")
                                        If chkRecognition.Checked Then tbResponB.Text = "" Else tbResponB.Text = obDataRead("empFullNameB")
                                        Dim StatusB As Integer = CInt(obDataRead("proposeStatus_B"))
                                        hfStatusB.Value = StatusB
                                        imgStatusB.ImageUrl = ImageUrl.getImage_msg(StatusB, 0)
                                        lbStatusB.Text = obDataRead("statusDescB")
                                        If StatusB = 1003 Then chkCompleteB.Checked = True

                                        Dim ResponIdB As Integer = CInt(obDataRead("proposeRespPerson_B"))
                                        Dim logicChkB As Boolean = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN") Or ownerId = loginEmpId Or ResponIdB = loginEmpId
                                        tbActionB.Enabled = logicChkB And StatusB = 1002
                                        If tbActionB.Enabled Then tbActionB.CssClass = "form-control input-sm" : pnActionB.Attributes.Add("class", "row thisEdit")
                                        chkCompleteB.Enabled = logicChkB And StatusB = 1002
                                        If chkCompleteB.Enabled Then chkBorderB.Attributes.Add("class", "statusCompleteEdit")
                                    End If

                                    Dim EnableC As Boolean = obDataRead("proposeEnable_C")
                                    If EnableC Then
                                        pnResponC.Visible = True
                                        tbActionC.Text = obDataRead("proposeAction_C")
                                        If chkRecognition.Checked Then tbResponC.Text = "" Else tbResponC.Text = obDataRead("empFullNameC")
                                        Dim StatusC As Integer = CInt(obDataRead("proposeStatus_C"))
                                        hfStatusC.Value = StatusC
                                        imgStatusC.ImageUrl = ImageUrl.getImage_msg(StatusC, 0)
                                        lbStatusC.Text = obDataRead("statusDescC")
                                        If StatusC = 1003 Then chkCompleteC.Checked = True

                                        Dim ResponIdC As Integer = CInt(obDataRead("proposeRespPerson_C"))
                                        Dim logicChkC As Boolean = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN") Or ownerId = loginEmpId Or ResponIdC = loginEmpId
                                        tbActionC.Enabled = logicChkC And StatusC = 1002
                                        If tbActionC.Enabled Then tbActionC.CssClass = "form-control input-sm" : pnActionC.Attributes.Add("class", "row thisEdit")
                                        chkCompleteC.Enabled = logicChkC And StatusC = 1002
                                        If chkCompleteC.Enabled = True Then chkBorderC.Attributes.Add("class", "statusCompleteEdit")
                                    End If

                                    'Show Update button
                                    If tbActionA.Enabled Or tbActionB.Enabled Or tbActionC.Enabled Then
                                        pnUpdateButton.Visible = True
                                    End If
                                End If
                            End If
                        End Using
                    End If
                End If
                conn.Close()
            End If
            'Else
            'Using connOb As New SqlConnection(ConnStr)
            '    connOb.Open()
            '    Dim strSql_getOb As String = "SELECT tblRecordDetail.detailId, tblRecordDetail.observItem, tblRecordDetail.title, tblObsvCate.cateName, tblObsvCateSub.catesubName, tblObsvFailPoint.failpointName, 
            '                                              tblRecordDetail.equipment, tblRecordDetail.IsHRO, tblRecordDetail.hroChk1, tblRecordDetail.hroChk2, tblRecordDetail.hroChk3, tblRecordDetail.hroChk4, tblRecordDetail.hroChk5, 
            '                                              tblRecordDetail.secondEye, tblRecordDetail.recognition, tblRecordDetail.observType, tblContractor.contractorName, tblRecordDetail.pictureCount, tblRecordDetail.description, 
            '                                              tblRecordDetail.proposeEnable_A, tblRecordDetail.proposeRespPerson_A, tblEmployee_A.empFullName AS empFullNameA, tblRecordDetail.proposeAction_A, tblRecordDetail.proposeStatus_A, tblStatus_A.statusDesc AS statusDescA, tblRecordDetail.proposeComplete_A, 
            '                                              tblRecordDetail.proposeEnable_B, tblRecordDetail.proposeRespPerson_B, tblEmployee_B.empFullName AS empFullNameB, tblRecordDetail.proposeAction_B, tblRecordDetail.proposeStatus_B, tblStatus_B.statusDesc AS statusDescB, tblRecordDetail.proposeComplete_B, 
            '                                              tblRecordDetail.proposeEnable_C, tblRecordDetail.proposeRespPerson_C, tblEmployee_C.empFullName AS empFullNameC, tblRecordDetail.proposeAction_C, tblRecordDetail.proposeStatus_C, tblStatus_C.statusDesc AS statusDescC, tblRecordDetail.proposeComplete_C, 
            '                                              tblRecordDetail.observComplete FROM dbo.tblRecordDetail 
            '                                              INNER JOIN dbo.tblObsvCate ON dbo.tblRecordDetail.category = dbo.tblObsvCate.cateId 
            '                                              INNER JOIN dbo.tblObsvCateSub ON dbo.tblRecordDetail.categorySub = dbo.tblObsvCateSub.catesubId 
            '                                              INNER JOIN dbo.tblObsvFailPoint ON dbo.tblRecordDetail.failurePoint = dbo.tblObsvFailPoint.failpointId 
            '                                              LEFT OUTER JOIN dbo.tblContractor ON dbo.tblRecordDetail.contractor = dbo.tblContractor.contractorId 
            '                                              LEFT OUTER JOIN dbo.tblStatus AS tblStatus_A ON dbo.tblRecordDetail.proposeStatus_A = tblStatus_A.statusId 
            '                                              LEFT OUTER JOIN dbo.tblStatus AS tblStatus_B ON dbo.tblRecordDetail.proposeStatus_B = tblStatus_B.statusId 
            '                                              LEFT OUTER JOIN dbo.tblStatus AS tblStatus_C ON dbo.tblRecordDetail.proposeStatus_C = tblStatus_C.statusId 
            '                                              LEFT OUTER JOIN dbo.tblEmployee AS tblEmployee_A ON dbo.tblRecordDetail.proposeRespPerson_A = tblEmployee_A.empId 
            '                                              LEFT OUTER JOIN dbo.tblEmployee AS tblEmployee_B ON dbo.tblRecordDetail.proposeRespPerson_B = tblEmployee_B.empId 
            '                                              LEFT OUTER JOIN dbo.tblEmployee AS tblEmployee_C ON dbo.tblRecordDetail.proposeRespPerson_C = tblEmployee_C.empId 
            '                                              WHERE (dbo.tblRecordDetail.recId = @recId) AND (dbo.tblRecordDetail.observItem = @observItem)"

            '    Dim commandOb As New SqlCommand(strSql_getOb, connOb) With {.CommandType = CommandType.Text}
            '    commandOb.Parameters.Add("@recId", SqlDbType.Int).Value = CInt(Session("recId"))
            '    commandOb.Parameters.Add("@observItem", SqlDbType.Int).Value = CInt(Session("observItem")) + 1

            '    Dim obDataRead As SqlDataReader
            '    obDataRead = commandOb.ExecuteReader()
            '    If obDataRead.HasRows() Then
            '        obDataRead.Read()
            '        If obDataRead("detailId") IsNot DBNull.Value Then
            '            Dim empLogin As New cEmployee
            '            Dim loginEmpId As Integer = empLogin.FindEmployeeIdbyUsername(User.Identity.Name)
            '            Dim ownerId As Integer = CInt(hfOwnerEmpId.Value)

            '            tbActionA.Text = obDataRead("proposeAction_A")
            '            If chkRecognition.Checked Then tbResponA.Text = "" Else tbResponA.Text = obDataRead("empFullNameA")
            '            Dim StatusA As Integer = CInt(obDataRead("proposeStatus_A"))
            '            hfStatusA.Value = StatusA
            '            Dim ImageUrl As New cImage
            '            imgStatusA.ImageUrl = ImageUrl.getImage_msg(StatusA, 0)
            '            lbStatusA.Text = obDataRead("statusDescA")
            '            If StatusA = 1003 Then chkCompleteA.Checked = True

            '            Dim ResponIdA As Integer = CInt(obDataRead("proposeRespPerson_A"))
            '            Dim logicChkA As Boolean = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN") Or ownerId = loginEmpId Or ResponIdA = loginEmpId
            '            tbActionA.Enabled = logicChkA And StatusA = 1002
            '            If tbActionA.Enabled Then tbActionA.CssClass = "form-control input-sm" : pnActionA.Attributes.Add("class", "row thisEdit")      'Edit Mode
            '            chkCompleteA.Enabled = logicChkA And StatusA = 1002
            '            If chkCompleteA.Enabled Then chkBorderA.Attributes.Add("class", "statusCompleteEdit")

            '            Dim EnableB As Boolean = obDataRead("proposeEnable_B")
            '            If EnableB Then
            '                pnResponB.Visible = True
            '                tbActionB.Text = obDataRead("proposeAction_B")
            '                If chkRecognition.Checked Then tbResponB.Text = "" Else tbResponB.Text = obDataRead("empFullNameB")
            '                Dim StatusB As Integer = CInt(obDataRead("proposeStatus_B"))
            '                hfStatusB.Value = StatusB
            '                imgStatusB.ImageUrl = ImageUrl.getImage_msg(StatusB, 0)
            '                lbStatusB.Text = obDataRead("statusDescB")
            '                If StatusB = 1003 Then chkCompleteB.Checked = True

            '                Dim ResponIdB As Integer = CInt(obDataRead("proposeRespPerson_B"))
            '                Dim logicChkB As Boolean = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN") Or ownerId = loginEmpId Or ResponIdB = loginEmpId
            '                tbActionB.Enabled = logicChkB And StatusB = 1002
            '                If tbActionB.Enabled Then tbActionB.CssClass = "form-control input-sm" : pnActionB.Attributes.Add("class", "row thisEdit")
            '                chkCompleteB.Enabled = logicChkB And StatusB = 1002
            '                If chkCompleteB.Enabled Then chkBorderB.Attributes.Add("class", "statusCompleteEdit")
            '            End If

            '            Dim EnableC As Boolean = obDataRead("proposeEnable_C")
            '            If EnableC Then
            '                pnResponC.Visible = True
            '                tbActionC.Text = obDataRead("proposeAction_C")
            '                If chkRecognition.Checked Then tbResponC.Text = "" Else tbResponC.Text = obDataRead("empFullNameC")
            '                Dim StatusC As Integer = CInt(obDataRead("proposeStatus_C"))
            '                hfStatusC.Value = StatusC
            '                imgStatusC.ImageUrl = ImageUrl.getImage_msg(StatusC, 0)
            '                lbStatusC.Text = obDataRead("statusDescC")
            '                If StatusC = 1003 Then chkCompleteC.Checked = True

            '                Dim ResponIdC As Integer = CInt(obDataRead("proposeRespPerson_C"))
            '                Dim logicChkC As Boolean = User.IsInRole("FACILITY ADMIN") Or User.IsInRole("SYSTEM ADMIN") Or ownerId = loginEmpId Or ResponIdC = loginEmpId
            '                tbActionC.Enabled = logicChkC And StatusC = 1002
            '                If tbActionC.Enabled Then tbActionC.CssClass = "form-control input-sm" : pnActionC.Attributes.Add("class", "row thisEdit")
            '                chkCompleteC.Enabled = logicChkC And StatusC = 1002
            '                If chkCompleteC.Enabled = True Then chkBorderC.Attributes.Add("class", "statusCompleteEdit")
            '            End If
            '        End If
            '    End If
            '    connOb.Close()
            'End Using
        End If

        If User.IsInRole("SYSTEM ADMIN") Or User.IsInRole("FACILITY ADMIN") Then
            Dim SettingItem As RadPanelItem = RadPanelBar1.Items.FindItemByText("SETTING")
            If User.IsInRole("FACILITY ADMIN") Then SettingItem.NavigateUrl = "~/em/setUserbyDepart.aspx?sel=setuserd"
            SettingItem.Visible = True
        End If

        If Not User.Identity.IsAuthenticated Then
            pnAvatar.Visible = False
        End If
    End Sub

    Private Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click
        Dim status As New cStatus
        Dim StrUpd As String = ""
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim result As Integer
        Dim IsValid As Boolean = True
        If Not pnResponB.Visible And Not pnResponC.Visible Then
            '1 Action
            StrUpd = "UPDATE tblRecordDetail SET proposeAction_A = @proposeAction_A, proposeStatus_A = @proposeStatus_A, proposeComplete_A  = @proposeComplete_A, whoComplete_A = @whoComplete_A, observComplete = @observComplete WHERE detailId = @detailId"
            Dim command As New SqlCommand(StrUpd, conn)
            command.Parameters.Add("@detailId", SqlDbType.Int).Value = CInt(hfDetailId.Value)
            command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = tbActionA.Text
            If tbActionA.Text = "" Then IsValid = False

            Dim newStatusValueA As Integer
            If chkCompleteA.Checked Then
                newStatusValueA = 1003      'Change to Complete
                command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = True
                command.Parameters.Add("@whoComplete_A", SqlDbType.NVarChar).Value = User.Identity.GetUserId
            Else
                newStatusValueA = CInt(hfStatusA.Value)
                command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = False
                command.Parameters.Add("@whoComplete_A", SqlDbType.NVarChar).Value = "0"
            End If
            command.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = newStatusValueA
            command.Parameters.Add("@observComplete", SqlDbType.NVarChar).Value = status.observStatus(newStatusValueA, False, 0, False, 0)

            If IsValid Then
                Result = command.ExecuteNonQuery()
                status.UpdRecordIsComplete(CInt(hfRecId.Value))        'update IsComplete in tblRecord (action number)
            Else
                lbUpdateInfo.Text = "Please check Propose Action field."
            End If
        ElseIf pnResponB.Visible And Not pnResponC.Visible Then
            '2 Action
            StrUpd = "UPDATE tblRecordDetail SET proposeAction_A = @proposeAction_A, proposeStatus_A = @proposeStatus_A, proposeComplete_A = @proposeComplete_A, whoComplete_A = @whoComplete_A, 
                        proposeAction_B = @proposeAction_B, proposeStatus_B = @proposeStatus_B, proposeComplete_B = @proposeComplete_B, whoComplete_B = @whoComplete_B, 
                        observComplete = @observComplete WHERE detailId = @detailId"
            Dim command As New SqlCommand(StrUpd, conn)
            command.Parameters.Add("@detailId", SqlDbType.Int).Value = CInt(hfDetailId.Value)
            command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = tbActionA.Text
            command.Parameters.Add("@proposeAction_B", SqlDbType.NVarChar).Value = tbActionB.Text
            If tbActionA.Text = "" Then IsValid = False
            If tbActionB.Text = "" Then IsValid = False

            Dim newStatusValueA As Integer
            If chkCompleteA.Checked Then
                newStatusValueA = 1003      'Change to Complete
                command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = True
                command.Parameters.Add("@whoComplete_A", SqlDbType.NVarChar).Value = User.Identity.GetUserId
            Else
                newStatusValueA = CInt(hfStatusA.Value)
                command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = False
                command.Parameters.Add("@whoComplete_A", SqlDbType.NVarChar).Value = "0"
            End If
            command.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = newStatusValueA

            Dim newStatusValueB As Integer
            If chkCompleteB.Checked Then
                newStatusValueB = 1003      'Complete
                command.Parameters.Add("@proposeComplete_B", SqlDbType.Bit).Value = True
                command.Parameters.Add("@whoComplete_B", SqlDbType.NVarChar).Value = User.Identity.GetUserId
            Else
                newStatusValueB = CInt(hfStatusB.Value)
                command.Parameters.Add("@proposeComplete_B", SqlDbType.Bit).Value = False
                command.Parameters.Add("@whoComplete_B", SqlDbType.NVarChar).Value = "0"
            End If
            command.Parameters.Add("@proposeStatus_B", SqlDbType.Int).Value = newStatusValueB
            command.Parameters.Add("@observComplete", SqlDbType.NVarChar).Value = status.observStatus(newStatusValueA, True, newStatusValueB, False, 0)

            If IsValid Then
                result = command.ExecuteNonQuery()
                status.UpdRecordIsComplete(CInt(hfRecId.Value))        'update IsComplete in tblRecord (action number)
            Else
                lbUpdateInfo.Text = "Please check Propose Action field."
            End If
        ElseIf pnResponB.Visible And pnResponC.Visible Then
            '3 Action
            StrUpd = "UPDATE tblRecordDetail SET proposeAction_A = @proposeAction_A, proposeStatus_A = @proposeStatus_A, proposeComplete_A  = @proposeComplete_A, whoComplete_A = @whoComplete_A, 
                        proposeAction_B = @proposeAction_B, proposeStatus_B = @proposeStatus_B, proposeComplete_B = @proposeComplete_B, whoComplete_B = @whoComplete_B, 
                        proposeAction_C = @proposeAction_C, proposeStatus_C = @proposeStatus_C, proposeComplete_C = @proposeComplete_C, whoComplete_C = @whoComplete_C, 
                        observComplete = @observComplete WHERE detailId = @detailId"
            Dim command As New SqlCommand(StrUpd, conn)
            command.Parameters.Add("@detailId", SqlDbType.Int).Value = CInt(hfDetailId.Value)
            If tbActionA.Text = "" Then IsValid = False Else command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = tbActionA.Text
            If tbActionB.Text = "" Then IsValid = False Else command.Parameters.Add("@proposeAction_B", SqlDbType.NVarChar).Value = tbActionB.Text
            If tbActionC.Text = "" Then IsValid = False Else command.Parameters.Add("@proposeAction_C", SqlDbType.NVarChar).Value = tbActionC.Text

            Dim newStatusValueA As Integer
            If chkCompleteA.Checked Then
                newStatusValueA = 1003      'Complete
                command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = True
                command.Parameters.Add("@whoComplete_A", SqlDbType.NVarChar).Value = User.Identity.GetUserId
            Else
                newStatusValueA = CInt(hfStatusA.Value)
                command.Parameters.Add("@proposeComplete_A", SqlDbType.Bit).Value = False
                command.Parameters.Add("@whoComplete_A", SqlDbType.NVarChar).Value = "0"
            End If
            command.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = newStatusValueA

            Dim newStatusValueB As Integer
            If chkCompleteB.Checked Then
                newStatusValueB = 1003      'Complete
                command.Parameters.Add("@proposeComplete_B", SqlDbType.Bit).Value = True
                command.Parameters.Add("@whoComplete_B", SqlDbType.NVarChar).Value = User.Identity.GetUserId
            Else
                newStatusValueB = CInt(hfStatusB.Value)
                command.Parameters.Add("@proposeComplete_B", SqlDbType.Bit).Value = False
                command.Parameters.Add("@whoComplete_B", SqlDbType.NVarChar).Value = "0"
            End If
            command.Parameters.Add("@proposeStatus_B", SqlDbType.Int).Value = newStatusValueB

            Dim newStatusValueC As Integer
            If chkCompleteC.Checked Then
                newStatusValueC = 1003      'Complete
                command.Parameters.Add("@proposeComplete_C", SqlDbType.Bit).Value = True
                command.Parameters.Add("@whoComplete_C", SqlDbType.NVarChar).Value = User.Identity.GetUserId
            Else
                newStatusValueC = CInt(hfStatusC.Value)
                command.Parameters.Add("@proposeComplete_C", SqlDbType.Bit).Value = False
                command.Parameters.Add("@whoComplete_C", SqlDbType.NVarChar).Value = "0"
            End If
            command.Parameters.Add("@proposeStatus_C", SqlDbType.Int).Value = newStatusValueC
            command.Parameters.Add("@observComplete", SqlDbType.NVarChar).Value = status.observStatus(newStatusValueA, True, newStatusValueB, True, newStatusValueC)

            If IsValid Then
                result = command.ExecuteNonQuery()
                status.UpdRecordIsComplete(CInt(hfRecId.Value))        'update IsComplete in tblRecord (action number)
            Else
                lbUpdateInfo.Text = "Please check Propose Action field."
            End If
        End If
        conn.Close()

        If result > 0 Then
            'MsgBoxRad("<b>Update completed</b>", 240, 76)
            Response.Redirect("observeDetail.aspx?rid=" & Request.QueryString("rid"))
        End If
    End Sub


End Class