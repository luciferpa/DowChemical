Imports System.Data.SqlClient
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin

Imports Telerik.Web.UI

Public Class setUserbyDepart
    Inherits Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

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

                '## increase only setUserbyDepartment
                hfDepartId.Value = employee.DepartmentId
            End If
        End If

        If User.IsInRole("FACILITY ADMIN") Then
            Dim SettingItem As RadPanelItem = RadPanelBar1.Items.FindItemByText("SETTING")
            SettingItem.Visible = True
            'SettingItem.Items.FindItemByText("DEPARTMENT").Visible = False
            'SettingItem.Items.FindItemByText("CONTRACTOR").Visible = False
            'SettingItem.Items.FindItemByText("GOAL SETTING").Visible = False
            'SettingItem.Items.FindItemByText("CATEGORY").Visible = False
            'SettingItem.Items.FindItemByText("IMPORT DATA").Visible = False
        End If
    End Sub

    Private Sub resetForm()
        tbDowId.Text = ""
        tbName.Text = ""
        tbSurname.Text = ""
        tbEmail.Text = ""
        hfEmployeeId.Value = "0"

        cbShow_Del.Visible = True
        RadMultiPage1.SelectedIndex = 0
    End Sub

    'Protected Sub RadTabStrip1_TabClick(sender As Object, e As RadTabStripEventArgs) Handles RadTabStrip1.TabClick
    '    If RadTabStrip1.SelectedIndex = 0 Then
    '        rgEmployeeLast.Rebind()
    '    End If

    '    If RadTabStrip1.SelectedIndex = 1 Then
    '        rgEmployeeList.Rebind()
    '    End If
    'End Sub

    Protected Sub btAddUser_Click(sender As Object, e As EventArgs) Handles btAddUser.Click
        If tbDowId.Text.Trim <> "" And tbName.Text.Trim <> "" And tbSurname.Text <> "" And tbEmail.Text <> "" Then
            '-- check repeart
            Dim aiLib As New AiLib.aEmployee
            Dim DowId As String = tbDowId.Text.Trim()
            Dim Name As String = tbName.Text.Trim()
            Dim Surname As String = tbSurname.Text.Trim()
            'Dim Display As String = tbDisplayname.Text.Trim()

            If aiLib.chkEnployeeCodeRepeat(DowId) Then
                MsgBoxRad("<b>Dow ID มีอยู่แล้วในระบบ </br>กรูณาใช้ ID อื่น</b>", 280, 76)
                Return
            End If
            If aiLib.chkEnployeeFullNameRepeat(Name, Surname) Then
                MsgBoxRad("<b>ชื่อ นามสกุล นี้มีอยู่แล้วในระบบ</b>", 280, 76)
                Return
            End If
            If aiLib.chkEnployeeDisplayRepeat(Surname) Then
                MsgBoxRad("<b>Display name นี้มีอยู่แล้วในระบบ</b>", 280, 76)
                Return
            End If

            '-- prepare Display Name
            Dim DisplayName As String = Name & "." & Surname.Substring(0, 1)
            Dim strCount As String = "SELECT COUNT(*) FROM tblEmployee WHERE (empDisplay = @empDisplay)"
            Dim connCount As New SqlConnection(ConnStr)
            connCount.Open()
            Dim commandCount As New SqlCommand(strCount, connCount)
            commandCount.Parameters.Add("@empDisplay", SqlDbType.NVarChar).Value = DisplayName
            Dim Count As Integer = commandCount.ExecuteScalar()
            connCount.Close()

            If Count > 0 Then
                DisplayName = DisplayName & "1"
            Else
                DisplayName = DisplayName
            End If


            '-- insert data
            Dim strIns As String = "INSERT INTO tblEmployee(empDowId, empName, empSurname, empFullName, empDisplay, empEmail, departId, joblvCode, empEnable) 
                                    VALUES(@empDowId, @empName, @empSurname, @empFullName, @empDisplay, @empEmail, @departId, @joblvCode, @empEnable)"

            Dim conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strIns, conn)

            command.Parameters.Add("@empDowId", SqlDbType.VarChar).Value = DowId
            command.Parameters.Add("@empName", SqlDbType.NVarChar).Value = Name
            command.Parameters.Add("@empSurname", SqlDbType.NVarChar).Value = Surname
            command.Parameters.Add("@empFullName", SqlDbType.NVarChar).Value = Name & ", " & Surname
            command.Parameters.Add("@empDisplay", SqlDbType.NVarChar).Value = DisplayName
            command.Parameters.Add("@empEmail", SqlDbType.NVarChar).Value = tbEmail.Text.Trim
            command.Parameters.Add("@departId", SqlDbType.Int).Value = rcbDepartment.SelectedValue
            command.Parameters.Add("@joblvCode", SqlDbType.VarChar).Value = rcbJobLevel.SelectedValue
            command.Parameters.Add("@empEnable", SqlDbType.Bit).Value = chkEnUser.Checked

            Dim err As Integer = command.ExecuteNonQuery()
            conn.Close()

            If err > 0 Then
                'ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "ClearEmployee", "ClearEmployee();", True)
                tbDowId.Text = ""
                tbName.Text = ""
                tbSurname.Text = ""
                tbEmail.Text = ""
                rcbDepartment.SelectedIndex = 0
                rcbJobLevel.SelectedIndex = 0
                chkEnUser.Checked = False

                '-- update grid
                'Dim expression As GridSortExpression = New GridSortExpression()
                'expression.FieldName = "empId"
                'expression.SortOrder = GridSortOrder.Descending
                'rgEmployeeLast.MasterTableView.SortExpressions.AddSortExpression(expression)
                'rgEmployeeLast.MasterTableView.Rebind()
                rgEmployeeLast.Rebind()
                rgEmployeeList.Rebind()
            Else
                MsgBoxRad("<b>ติดต่อผู้ดูแล.. การเพิ่ม Employee ไม่สมบูรณ์</b>", 240, 76)
            End If
        Else
            MsgBoxRad("<b>Dow ID///ห้ามว่าง</b>", 240, 76)
        End If
    End Sub

    Private Sub rgEmployeeLast_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgEmployeeLast.ItemDataBound
        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

            '-- แสดง Login Name
            ShowLoginName(item)

            '-- แสดงปุ่ม User En/Dis
            ShowUserEnable(item, e)

            '-- แสดงปุ่ม isLogin
            ShowIsLogin(item, e)

            '-- กำหนด parameter ให้ปุ่ม setLogin
            SetParameterArgument(item, e)
        End If
    End Sub

    Private Sub rgEmployeeLast_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgEmployeeLast.ItemCommand
        Dim EmpId As Integer = CInt(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("empId"))

        If e.CommandName = "empenable" Then
            EmployeeEnable(e, EmpId)
            rgEmployeeLast.Rebind()
            rgEmployeeList.Rebind()
        End If
        If e.CommandName = "setlogin" Then
            SetLogin(e, EmpId, True)
        End If
    End Sub

    Private Sub rgEmployeeLast_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rgEmployeeLast.NeedDataSource
        rgEmployeeLast.Rebind()
    End Sub

    Protected Sub btCreateAccount_Click(sender As Object, e As EventArgs) Handles btCreateAccount.Click
        If tbUsername.Text <> "" And tbPassword.Text <> "" Then
            Dim userName As String = tbUsername.Text.Trim

            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim user = New ApplicationUser() With {.UserName = userName, .Email = tbEmail_Acc.Text}

            '-- check Password and Confirm Password
            If tbPassword.Text = tbPasswordConfirm.Text Then
                Dim result = manager.Create(user, tbPassword.Text)
                If result.Succeeded Then
                    '-- create User to Role 
                    Dim roleActions As New cRoleActions
                    roleActions.AddNewUserToRole(userName, rcbAccountType.Text)
                    Dim aspNetUserId As String = roleActions.AddNewUserToRole_AspNetUserId

                    '-- confirm Employee Status (isSetlogin, Username)
                    Dim roleEmployee As New cEmployee
                    roleEmployee.UpdEmployeeStatus(CInt(hfEmployeeId.Value), True, userName, CInt(rcbAccountType.SelectedValue), aspNetUserId)
                    rgEmployeeLast.DataBind()
                    rgEmployeeList.Rebind()

                    MsgBoxRad("<b>Account " + userName + " create complete</b>", 240, 76)
                    resetForm()
                Else
                    Dim resultErr As String = result.Errors.FirstOrDefault()
                    lbErrorMsg.Text = resultErr
                End If
            Else
                '--
                MsgBoxRad("<b>The password And confirmation password Do Not match. </b>", 260, 76)
            End If
        End If
    End Sub
    Protected Sub btBack_Click(sender As Object, e As EventArgs) Handles btBack.Click, btBack_Change.Click
        Dim IsGridLast As Boolean = CBool(Session("IsGridLast"))

        tbDowId.Text = ""
        tbName.Text = ""
        tbSurname.Text = ""
        tbEmail.Text = ""
        hfEmployeeId.Value = "0"
        cbShow_Del.Visible = True
        If IsGridLast Then RadMultiPage1.SelectedIndex = 0 Else RadMultiPage1.SelectedIndex = 1
    End Sub

    Protected Sub btEditAccount_Click(sender As Object, e As EventArgs) Handles btEditAccount.Click
        If tbUsername_Change.Text <> "" And tbEmail_Change.Text <> "" Then
            Dim userName As String = tbUsername_Change.Text.Trim
            Dim email As String = tbEmail_Change.Text.Trim

            Dim aspNetUserId As String = hfAspNetUserId_Ori.Value
            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim user = manager.FindById(aspNetUserId)
            Dim oRoleName = manager.GetRoles(aspNetUserId)
            user.UserName = userName
            user.Email = email

            Dim result = manager.Update(user)
            If result.Succeeded Then
                '-- update Role
                manager.RemoveFromRole(aspNetUserId, oRoleName.Item(0).ToString)
                manager.AddToRole(aspNetUserId, rcbAccountType_Change.Text)

                '-- update Employee Status
                Dim Employee As New cEmployee
                Employee.UpdAccountStatus(CInt(hfEmployeeId.Value), userName, rcbAccountType_Change.SelectedValue)

                rgEmployeeLast.Rebind()
                rgEmployeeList.Rebind()
                RadMultiPage1.SelectedIndex = RadTabStrip1.SelectedIndex
            Else
                Dim resultErr As String = result.Errors.FirstOrDefault()
                lbErrorMsg_Change.Text = resultErr
            End If
        End If
    End Sub
    Protected Sub btResetPassword_Change_Click(sender As Object, e As EventArgs) Handles btResetPassword_Change.Click
        RadMultiPage2.SelectedIndex = 2
    End Sub
    Protected Sub btDeleteAccount_Click(sender As Object, e As EventArgs) Handles btDeleteAccount.Click
        RadMultiPage2.SelectedIndex = 3
    End Sub

    Protected Sub btResetPassword_Reset_Click(sender As Object, e As EventArgs) Handles btResetPassword_Reset.Click
        '-- Reset
        If tbPassword_Reset.Text = tbPasswordConfirm_Reset.Text Then
            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim user = manager.FindByName(tbUsername_Reset.Text)
            If user Is Nothing Then
                lbErrorMsg_Reset.Text = "No user found"
                Return
            End If

            user.PasswordHash = manager.PasswordHasher.HashPassword(tbPassword_Reset.Text)
            manager.UpdateSecurityStamp(user.Id)

            Dim result = manager.Update(user)
            If result.Succeeded Then
                tbPassword_Reset.Text = ""
                tbPasswordConfirm.Text = ""
                hfEmployeeId.Value = "0"
                cbShow_Del.Visible = True
                RadMultiPage1.SelectedIndex = 0
                Return
            End If
            lbErrorMsg_Reset.Text = result.Errors.FirstOrDefault()
            Return
        Else
            lbErrorMsg_Reset.Text = "The password And confirmation password Do Not match."
            Return
        End If
    End Sub
    Protected Sub btBack_Reset_Click(sender As Object, e As EventArgs) Handles btBack_Reset.Click
        RadMultiPage2.SelectedIndex = 1
    End Sub

    Protected Sub btConfirmDelete_Click(sender As Object, e As EventArgs) Handles btConfirmDelete.Click
        '-- Delete
        Dim aspNetUserId As String = hfAspNetUserId_Ori.Value
        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim user = manager.FindById(aspNetUserId)
        If user Is Nothing Then
            MsgBoxRad("<b>No user found</b>", 200, 76)
            Return
        End If

        'user.PasswordHash = manager.PasswordHasher.HashPassword(tbPassword.Text)
        Dim result = manager.Delete(user)
        If result.Succeeded Then
            '-- update Employee Status (isSetlogin, Username)
            Dim roleEmployee As New cEmployee
            roleEmployee.UpdEmployeeStatus(CInt(hfEmployeeId.Value), False, "", 1000, "")
            rgEmployeeLast.DataBind()
            rgEmployeeList.Rebind()

            resetForm()
            Return
        Else
            MsgBoxRad("<b>" & result.Errors.FirstOrDefault() & "</b>", 240, 76)
        End If
    End Sub
    Protected Sub btBack_Delete_Click(sender As Object, e As EventArgs) Handles btBack_Delete.Click
        RadMultiPage2.SelectedIndex = 1
    End Sub

    Private Sub rgEmployeeList_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgEmployeeList.ItemDataBound
        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

            '-- แสดง Login Name
            ShowLoginName(item)

            '-- แสดงปุ่ม User En/Dis
            ShowUserEnable(item, e)

            '-- แสดงปุ่ม isLogin
            ShowIsLogin(item, e)

            '-- แสดงปุ่มลบ
            Dim deleteButton As ImageButton = item.FindControl("imgbDel")
            deleteButton.Visible = cbShow_Del.Checked

            '-- กำหนด parameter ให้ปุ่ม setLogin
            SetParameterArgument(item, e)
        End If
    End Sub

    Private Sub rgEmployeeList_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles rgEmployeeList.ItemCommand
        If e.CommandName = "empenable" Then
            Dim EmpId As Integer = CInt(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("empId"))
            EmployeeEnable(e, EmpId)
            rgEmployeeList.Rebind()
            rgEmployeeLast.Rebind()
        End If

        If e.CommandName = "setlogin" Then
            Dim EmpId As Integer = CInt(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("empId"))
            SetLogin(e, EmpId, False)
        End If

        If e.CommandName = "Delete" Then
            '-- check other table before Delete


            '-- delete userAccount if exist
            Dim EmpIsSetLogin As Boolean = CBool(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("IsSetLogin"))
            Dim DepartId As Integer = CInt(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("departId"))



            '-- delete this employee
            ''Dim cMember As New cEmployee
            ''Dim countCheck As Integer = cMember.countMember_tbCustomer(EmpId) + cMember.countMember_tbMediaMgr(EmpId) + cMember.countMember_tbSalesRecord(EmpId)
            Dim countCheck As Integer = 0

            If DepartId <> 1000 Then
                Dim EmpId As Integer = CInt(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("empId"))
                Dim existlogic As Boolean = True

                Dim strDel As String = "DELETE FROM tblEmployee WHERE (empId = @empId)"
                Dim conn As New SqlConnection(ConnStr)
                conn.Open()
                Dim command As New SqlCommand(strDel, conn)
                command.Parameters.Add("@empId", SqlDbType.Int).Value = EmpId
                Dim result As Integer = command.ExecuteNonQuery()
                conn.Close()

                If result = 1 Then
                    rgEmployeeList.Rebind()
                    rgEmployeeLast.Rebind()
                End If
            End If
        End If
    End Sub

    Private Sub rgEmployeeList_UpdateCommand(sender As Object, e As GridCommandEventArgs) Handles rgEmployeeList.UpdateCommand
        If e.CommandName = RadGrid.UpdateCommandName Then
            If TypeOf e.Item Is GridEditFormItem Then
                Dim item As GridEditFormItem = DirectCast(e.Item, GridEditFormItem)

                Dim EditDowId As TextBox = DirectCast(item.FindControl("tbEditDowId"), TextBox)
                Dim EditName As TextBox = DirectCast(item.FindControl("tbEditName"), TextBox)
                Dim EditSurname As TextBox = DirectCast(item.FindControl("tbEditSurname"), TextBox)
                Dim EditEmail As TextBox = DirectCast(item.FindControl("tbEditEmail"), TextBox)
                Dim EditDisplay As TextBox = DirectCast(item.FindControl("tbEditDisplay"), TextBox)
                If EditDowId.Text <> "" And EditName.Text <> "" And EditSurname.Text <> "" And EditEmail.Text <> "" And EditDisplay.Text <> "" Then
                    Dim EditDepartment As RadComboBox = DirectCast(item.FindControl("rcbEditDepart"), RadComboBox)
                    Dim EditJobLevel As RadComboBox = DirectCast(item.FindControl("rcbEditJobLevel"), RadComboBox)

                    Dim eDowId As String = EditDowId.Text.Trim
                    Dim oDowId As String = DirectCast(item.FindControl("hfEditDowId"), HiddenField).Value
                    Dim eName As String = EditName.Text.Trim
                    Dim oName As String = DirectCast(item.FindControl("hfEditName"), HiddenField).Value
                    Dim eSurname As String = EditSurname.Text.Trim
                    Dim oSurname As String = DirectCast(item.FindControl("hfEditSurname"), HiddenField).Value
                    Dim eEmail As String = EditEmail.Text.Trim
                    Dim oEmail As String = DirectCast(item.FindControl("hfEditEmail"), HiddenField).Value
                    Dim eDisplay As String = EditDisplay.Text.Trim
                    Dim oDiaplay As String = DirectCast(item.FindControl("hfEditDisplay"), HiddenField).Value

                    'check repeat
                    Dim aiLib As New AiLib.aEmployee
                    Dim lbErrorMsgEditMode As Label = DirectCast(item.FindControl("lbErrorMsgEditMode"), Label)
                    If eDowId <> oDowId Then
                        If aiLib.chkEnployeeCodeRepeat(eDowId) Then
                            lbErrorMsgEditMode.Text = "Dow ID already exists."
                            e.Canceled = True
                            Return
                        End If
                    End If

                    If eName <> oName Or eSurname <> oSurname Then
                        If aiLib.chkEnployeeFullNameRepeat(eName, eSurname) Then
                            lbErrorMsgEditMode.Text = "Name And Surname already exists."
                            e.Canceled = True
                            Return
                        End If
                    End If

                    If eEmail <> oEmail Then
                        If aiLib.chkEnployeeEmailRepeat(eEmail) Then
                            lbErrorMsgEditMode.Text = "E-Mail already exists."
                            e.Canceled = True
                            Return
                        End If
                    End If

                    If eDisplay <> oDiaplay Then
                        If aiLib.chkEnployeeDisplayRepeat(eDisplay) Then
                            lbErrorMsgEditMode.Text = "Display name already exists."
                            e.Canceled = True
                            Return
                        End If
                    End If

                    Dim StrUpd As String = "UPDATE tblEmployee Set empDowId = @empDowId, empName = @empName, empSurname = @empSurname, empFullName = @empFullName, empDisplay = @empDisplay, empEmail  = @empEmail, departId = @departId, joblvCode = @joblvCode WHERE empId = @empId"
                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim command As New SqlCommand(StrUpd, conn)

                    command.Parameters.Add("@empDowId", SqlDbType.VarChar).Value = eDowId
                    command.Parameters.Add("@empName", SqlDbType.NVarChar).Value = eName
                    command.Parameters.Add("@empSurname", SqlDbType.NVarChar).Value = eSurname
                    command.Parameters.Add("@empFullName", SqlDbType.NVarChar).Value = eName & "  " & eSurname
                    command.Parameters.Add("@empEmail", SqlDbType.NVarChar).Value = eEmail
                    command.Parameters.Add("@empDisplay", SqlDbType.NVarChar).Value = eDisplay
                    command.Parameters.Add("@departId", SqlDbType.Int).Value = CInt(EditDepartment.SelectedValue)
                    command.Parameters.Add("@joblvCode", SqlDbType.VarChar).Value = EditJobLevel.SelectedValue
                    command.Parameters.Add("@empId", SqlDbType.Int).Value = CInt(item.GetDataKeyValue("empId"))
                    Dim err As Integer = command.ExecuteNonQuery()
                    conn.Close()

                    If err > 0 Then
                        lbErrorMsg.Text = ""
                        rgEmployeeList.Rebind()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub rgEmployeeList_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles rgEmployeeList.NeedDataSource
        rgEmployeeList.Rebind()
    End Sub

    Protected Sub cbShow_Del_CheckedChanged(sender As Object, e As EventArgs) Handles cbShow_Del.CheckedChanged
        rgEmployeeList.Rebind()
    End Sub
    Private Sub rgEmployeeList_DeleteCommand(sender As Object, e As GridCommandEventArgs) Handles rgEmployeeList.DeleteCommand
        '-- check other table before Delete


        '-- delete
        'Dim EmpId As String = CStr(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("empId"))
        'Dim EmpPositionId As String = CStr(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("empPos_id"))
        ''Dim cMember As New cEmployee
        ''Dim countCheck As Integer = cMember.countMember_tbCustomer(EmpId) + cMember.countMember_tbMediaMgr(EmpId) + cMember.countMember_tbSalesRecord(EmpId)
        'Dim countCheck As Integer = 0

        'If EmpPositionId <> 1000 Then
        '    '-- Check before Delete
        '    Dim existlogic As Boolean = True
        '    If countCheck = 0 Then
        '        existlogic = False
        '    End If
        '    If existlogic Then
        '        MsgBoxRad("<b>รายการนี้ถูกนำไปใช้แล้ว ไม่อนุญาติให้ลบ</br>", 200, 76)
        '        e.Canceled = True
        '    End If
        'End If


    End Sub

    Protected Sub btSearchBox_Click(sender As Object, e As EventArgs) Handles btSearchBox.Click
        If tbSearchKeyword.Text <> "" Then
            imgbClearKeyword.Visible = True

            Dim s As String = tbSearchKeyword.Text.Trim()
            'ViewState("SelectCommand") = hfOriSelectComm.Value + hfOriWhereString.Value
            ViewState("FilterExpression") = " empDowId LIKE '%" & s & "%' OR empName LIKE '%" & s & "%' OR empSurname LIKE '%" & s & "%' "

            'srcMedia.SelectCommand = ViewState("SelectCommand")
            srcEmployee.FilterExpression = ViewState("FilterExpression")
        Else
            imgbClearKeyword.Visible = False

            'srcEmployee.SelectCommand = hfOriSelectComm.Value + hfOriWhereString.Value
            srcEmployee.FilterExpression = ""
        End If

        rgEmployeeList.Rebind()
    End Sub
    Protected Sub imgbClearKeyword_Click(sender As Object, e As ImageClickEventArgs) Handles imgbClearKeyword.Click
        tbSearchKeyword.Text = ""
        imgbClearKeyword.Visible = False
        srcEmployee.FilterExpression = ""
        rgEmployeeList.Rebind()
    End Sub

    '--------------------------------------------------------------
    '---- Co-Function - ItemDataBound
    '--------------------------------------------------------------

    Private Sub ShowLoginName(ByVal item As GridDataItem)
        Dim EmployeeLoginName As Label = item.FindControl("lbEmployeeLoginName")
        If EmployeeLoginName.Text <> "" Then
            EmployeeLoginName.Text = " [login: " & EmployeeLoginName.Text.Trim() & "]"
            Dim hfAccTypeId As HiddenField = item.FindControl("hfAccType")
            Dim imgAccountType As Image = item.FindControl("imgAccountType")
            Dim AccTypeId As Integer = 0
            If hfAccTypeId.Value <> "" Then AccTypeId = CInt(hfAccTypeId.Value)
            If AccTypeId = 2000 Then
                imgAccountType.ImageUrl = "~/Images/user-16h24-blue.png"
                imgAccountType.ToolTip = "Admin"
            Else
                If AccTypeId = 9000 Then
                    imgAccountType.ImageUrl = "~/Images/user-16h24-red.png"
                    imgAccountType.ToolTip = "System admin"
                End If
            End If
        End If
    End Sub

    Private Sub ShowUserEnable(ByVal item As GridDataItem, e As GridItemEventArgs)
        Dim UserEnableButton As ImageButton = item.FindControl("imbEditEmpEnable")
        Dim UserEnableStatus As Boolean = DataBinder.Eval(e.Item.DataItem, "empEnable")
        If UserEnableStatus Then
            UserEnableButton.ImageUrl = "~/Images/user-24.png"
            UserEnableButton.ToolTip = "Click to Disable"
        Else
            UserEnableButton.ImageUrl = "~/Images/user-24-gray80.png"
            UserEnableButton.ToolTip = "Click to Enable"
        End If
    End Sub

    Private Sub ShowIsLogin(ByVal item As GridDataItem, e As GridItemEventArgs)
        Dim isLoginButton As ImageButton = item.FindControl("imbSetLogin")
        Dim isLoginStatus As Boolean = DataBinder.Eval(e.Item.DataItem, "IsSetLogin")
        If isLoginStatus Then
            isLoginButton.ImageUrl = "~/Images/IsKey.png"
            isLoginButton.ToolTip = "Change to your account"
        Else
            isLoginButton.ImageUrl = "~/Images/IsKey-gray80.png"
            isLoginButton.ToolTip = "Create a new account"
        End If
    End Sub

    Private Sub SetParameterArgument(ByVal item As GridDataItem, e As GridItemEventArgs)
        Dim imbSetLogin As ImageButton = item.FindControl("imbSetLogin")
        Dim empId As String = DataBinder.Eval(e.Item.DataItem, "empId")
        Dim empDowId As String = DataBinder.Eval(e.Item.DataItem, "empDowId")
        Dim empName As String = DataBinder.Eval(e.Item.DataItem, "empName")
        Dim empSurname As String = DataBinder.Eval(e.Item.DataItem, "empSurname")
        Dim empEmail As String = DataBinder.Eval(e.Item.DataItem, "empEmail")
        imbSetLogin.CommandArgument = empDowId + "?" + empDowId + "?" + empName + "?" + empSurname + "?" + empEmail
    End Sub

    '--------------------------------------------------------------
    '---- Co-Function - ItemDataBound
    '--------------------------------------------------------------

    Private Sub EmployeeEnable(e As GridCommandEventArgs, ByVal empId As Integer)
        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
        Dim hfEmpEnable As HiddenField = item.FindControl("hfEditEmpEnable")

        Dim StrUpd As String = "UPDATE tblEmployee SET empEnable = @empEnable WHERE empId = @empId"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim comand As New SqlCommand(StrUpd, conn)
        comand.Parameters.Add("@empId", SqlDbType.Int).Value = empId
        comand.Parameters.Add("@empEnable", SqlDbType.Bit).Value = Not CBool(hfEmpEnable.Value)
        Dim err As Integer = comand.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub SetLogin(e As GridCommandEventArgs, ByVal empId As Integer, ByVal IsGridLast As Boolean)
        Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

        hfEmployeeId.Value = empId
        Dim hfIsSetLogin As HiddenField = item.FindControl("hfIsSetLogin")
        Dim IsSetLogin As Boolean = CBool(hfIsSetLogin.Value)

        '-- Display Detail
        Dim EmployeeName As Label = item.FindControl("lbEmployeeName")
        Dim EmployeeSurname As Label = item.FindControl("lbEmployeeSurName")
        Dim empName As String = EmployeeName.Text
        Dim empSurname As String = EmployeeSurname.Text

        tbName_Acc.Text = empName
        tbSurname_Acc.Text = empSurname
        tbDowId_Acc.Text = item("empDowId").Text
        tbEmail_Acc.Text = item("empEmail").Text
        tbDepart_Acc.Text = item("departName").Text
        tbJoblv_Acc.Text = item("joblvCode").Text

        Dim hfDisplayname As HiddenField = item.FindControl("hfDisplayName")
        tbDiaplay_Acc.Text = hfDisplayname.Value

        If IsSetLogin Then
            '-- SetLogin แล้ว
            Dim hfAccountType As HiddenField = item.FindControl("hfAccType")
            rcbAccountType_Change.SelectedValue = hfAccountType.Value
            rcbAccountType_Change.DataBind()
            Dim hfUserName As HiddenField = item.FindControl("hfUserName")
            hfUsername_Ori.Value = hfUserName.Value             'Change Page
            tbUsername_Change.Text = hfUserName.Value           'Change Page
            tbUsername_Reset.Text = hfUserName.Value            'Reset Page
            tbUsername_Delete.Text = hfUserName.Value           'Delete Page

            Dim hfAspNetUserId As HiddenField = item.FindControl("hfAspNetUserId")
            hfAspNetUserId_Ori.Value = hfAspNetUserId.Value     'Change Page
            tbEmail_Change.Text = item("empEmail").Text         'Change Page

            RadMultiPage2.SelectedIndex = 1
        Else
            '-- create new account
            rcbAccountType.SelectedIndex = 0
            tbUsername.Text = empSurname.Substring(0, 1) & empName
            tbPassword.Text = ""
            tbPasswordConfirm.Text = ""
            RadMultiPage2.SelectedIndex = 0
        End If

        lbErrorMsg.Text = ""
        Session("IsGridLast") = IsGridLast
        Session("selRadMultiPage1") = RadMultiPage1.SelectedIndex
        RadMultiPage1.SelectedIndex = 2
    End Sub











    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "myFunction", "myFunction();", True)
    'End Sub
End Class