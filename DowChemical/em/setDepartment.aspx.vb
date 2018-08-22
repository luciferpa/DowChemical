Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class setDepartment
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

    '// -- Department Mgr
    Private Sub ClearZero()
        chkConfirmDel.Visible = False
        tbNewDepartment.Text = ""
        tbNewDepartDesc.Text = ""
        tbNewGroupName.Text = ""
        tbNewDepartEmail1.Text = ""
        tbNewDepartEmail2.Text = ""
        btSave.Visible = False
        lbFailureText.Text = ""
    End Sub

    Protected Sub btNewDepartment_Click(sender As Object, e As EventArgs) Handles btNew.Click
        ClearZero()

        tbNewDepartment.Focus()
        btSave.Text = "Add"
        btSave.Visible = True
    End Sub
    Private Sub btEdit_Click(sender As Object, e As EventArgs) Handles btEdit.Click
        ClearZero()

        getDescription(rcbDepartment.Text)
        tbNewDepartment.Text = rcbDepartment.Text
        tbNewDepartment.Focus()
        btSave.Text = "Update"
        btSave.Visible = True
    End Sub
    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If btSave.Text = "Add" Then
            If tbNewDepartment.Text <> "" Then
                Dim newDepartment As String = tbNewDepartment.Text.Trim()
                If Not chkRepeat(newDepartment) Then
                    Dim strIns As String = "INSERT INTO tblDepartment(departName, departDesc, departEmail1, departEmail2, groupMailName) VALUES(@departName, @departDesc, @departEmail1, @departEmail2, @groupMailName)"

                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim commandIns As New SqlCommand(strIns, conn)
                    commandIns.Parameters.Add("@departName", SqlDbType.VarChar).Value = newDepartment
                    commandIns.Parameters.Add("@departDesc", SqlDbType.NVarChar).Value = tbNewDepartDesc.Text.Trim
                    commandIns.Parameters.Add("@departEmail1", SqlDbType.VarChar).Value = tbNewDepartEmail1.Text.Trim
                    commandIns.Parameters.Add("@departEmail2", SqlDbType.VarChar).Value = tbNewDepartEmail2.Text.Trim
                    commandIns.Parameters.Add("@groupMailName", SqlDbType.NVarChar).Value = tbNewGroupName.Text.Trim
                    Dim errIns As Integer = commandIns.ExecuteNonQuery()
                    conn.Close()

                    If errIns > 0 Then
                        ClearZero()

                        rcbDepartment.DataBind()
                        rcbDepartment.Items(rcbDepartment.FindItemIndexByText(newDepartment)).Selected = True
                        lbFailureText.Text = "add '" & newDepartment & "' completed"
                        rgDepartmentList.Rebind()
                    Else
                        lbFailureText.Text = "Please contact administrator"
                    End If
                Else
                    lbFailureText.Text = "This departmaent already exists."
                End If
            Else
                lbFailureText.Text = "Please enter a valid departmaent feild."
            End If
        End If

        If btSave.Text = "Update" Then
            If tbNewDepartment.Text <> "" And rcbDepartment.SelectedIndex <> -1 Then
                Dim newDepartment As String = tbNewDepartment.Text.Trim()
                Dim newDesc As String = tbNewDepartDesc.Text.Trim()
                If Not chkRepeat(newDepartment) Then
                    Dim strUpd As String = "UPDATE tblDepartment SET departName = @departName, departDesc = @departDesc, departEmail1 = @departEmail1, departEmail2 = @departEmail2, groupMailName = @groupMailName WHERE departId = @departId"
                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim cmdUpd As New SqlCommand(strUpd, conn)
                    cmdUpd.Parameters.Add("@departId", SqlDbType.Int).Value = CInt(rcbDepartment.SelectedValue)
                    cmdUpd.Parameters.Add("@departName", SqlDbType.VarChar).Value = newDepartment
                    cmdUpd.Parameters.Add("@departDesc", SqlDbType.NVarChar).Value = newDesc
                    cmdUpd.Parameters.Add("@departEmail1", SqlDbType.VarChar).Value = tbNewDepartEmail1.Text.Trim
                    cmdUpd.Parameters.Add("@departEmail2", SqlDbType.VarChar).Value = tbNewDepartEmail2.Text.Trim
                    cmdUpd.Parameters.Add("@groupMailName", SqlDbType.NVarChar).Value = tbNewGroupName.Text.Trim
                    Dim errUpd As Integer = cmdUpd.ExecuteNonQuery()
                    conn.Close()

                    If errUpd > 0 Then
                        ClearZero()

                        rcbDepartment.DataBind()
                        rcbDepartment.Items(rcbDepartment.FindItemIndexByText(newDepartment)).Selected = True
                        lbFailureText.Text = "edit '" & newDepartment & "' completed"
                        rgDepartmentList.Rebind()
                    Else
                        lbFailureText.Text = "Please contact administrator (2.105)"
                    End If
                Else
                    'chk desc
                    If rcbDepartment.Text = newDepartment Then
                        'update description only
                        Dim strUpd As String = "UPDATE tblDepartment SET departDesc = @departDesc, departEmail1 = @departEmail1, departEmail2 = @departEmail2, groupMailName = @groupMailName WHERE departId = @departId"
                        Dim conn As New SqlConnection(ConnStr)
                        conn.Open()
                        Dim cmdUpd As New SqlCommand(strUpd, conn)
                        cmdUpd.Parameters.Add("@departId", SqlDbType.Int).Value = CInt(rcbDepartment.SelectedValue)
                        cmdUpd.Parameters.Add("@departDesc", SqlDbType.NVarChar).Value = newDesc
                        cmdUpd.Parameters.Add("@departEmail1", SqlDbType.VarChar).Value = tbNewDepartEmail1.Text.Trim
                        cmdUpd.Parameters.Add("@departEmail2", SqlDbType.VarChar).Value = tbNewDepartEmail2.Text.Trim
                        cmdUpd.Parameters.Add("@groupMailName", SqlDbType.NVarChar).Value = tbNewGroupName.Text.Trim
                        Dim errUpd As Integer = cmdUpd.ExecuteNonQuery()
                        conn.Close()

                        ClearZero()

                        rcbDepartment.DataBind()
                        lbFailureText.Text = "edit '" & newDesc & "' completed"
                        rgDepartmentList.Rebind()
                    Else
                        lbFailureText.Text = "This departmaent already exists."
                    End If
                End If
            Else
                lbFailureText.Text = "Please enter a valid departmaent feild."
            End If
        End If
    End Sub
    Private Sub btDel_Click(sender As Object, e As EventArgs) Handles btDel.Click
        lbFailureText.Text = ""

        If rcbDepartment.SelectedIndex <> -1 Then
            If chkConfirmDel.Visible Then
                If chkConfirmDel.Checked Then
                    'Chk Department
                    Dim DepartId As Integer = CInt(rcbDepartment.SelectedValue)
                    Dim strCount As String = "SELECT COUNT(*) FROM tblDepartment WHERE (departId = @departId)"
                    Dim connChk As New SqlConnection(ConnStr)
                    connChk.Open()
                    Dim commandChk As New SqlCommand(strCount, connChk)
                    commandChk.Parameters.Add("@departId", SqlDbType.Int).Value = DepartId
                    Dim Count As Integer = commandChk.ExecuteScalar()
                    connChk.Close()

                    Dim IsAcceptDelete As Boolean = (Count = 1) And (DepartId >= 1000)
                    '-- เพิ่ม logic ถ้าถูกนำไปใช้แล้ว ไม่อนุญาติให้ลบ 
                    '///
                    '///
                    If IsAcceptDelete And chkConfirmDel.Checked Then
                        Dim strDel As String = "DELETE FROM tblDepartment WHERE (departId = @departId)"
                        Dim conn As New SqlConnection(ConnStr)
                        conn.Open()
                        Dim command As New SqlCommand(strDel, conn)
                        command.Parameters.Add("@departId", SqlDbType.Int).Value = DepartId
                        Dim errDel As Integer = command.ExecuteNonQuery()
                        conn.Close()

                        If errDel > 0 Then
                            chkConfirmDel.Checked = False
                            chkConfirmDel.Visible = False
                            lbFailureText.Text = "delete completed."
                            rcbDepartment.DataBind()
                            rgDepartmentList.Rebind()
                        End If
                    End If
                Else
                    chkConfirmDel.Visible = False
                End If
            Else
                chkConfirmDel.Visible = True
            End If
        Else
            chkConfirmDel.Visible = False
        End If

        tbNewDepartment.Text = ""
        btSave.Visible = False
    End Sub
    Private Function chkRepeat(ByVal DepartDesc As String) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblDepartment WHERE departName = @departName"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@departName", SqlDbType.VarChar).Value = DepartDesc
        Dim Count As Integer = command.ExecuteScalar()
        conn.Close()

        If Count = 0 Then Return False Else Return True
    End Function

    Private Sub getDescription(ByVal DepartName As String)
        Dim DepartDesc As String = ""
        Dim DepartEmail1 As String = ""
        Dim DepartEmail2 As String = ""
        Dim DepartGroupName As String = ""

        Dim strSql As String = "SELECT * FROM tblDepartment WHERE (departName = '" & DepartName & "')"
        Dim conn As New SqlConnection(ConnStr)
        Dim command As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
        conn.Open()
        Dim DataRead As SqlDataReader = command.ExecuteReader()
        If DataRead.HasRows() Then
            DataRead.Read()
            If DataRead("departDesc") IsNot DBNull.Value Then DepartDesc = DataRead("departDesc")
            If DataRead("departEmail1") IsNot DBNull.Value Then DepartEmail1 = DataRead("departEmail1")
            If DataRead("departEmail2") IsNot DBNull.Value Then DepartEmail2 = DataRead("departEmail2")
            If DataRead("groupMailName") IsNot DBNull.Value Then DepartGroupName = DataRead("groupMailName")
        End If
        conn.Close()

        tbNewDepartDesc.Text = DepartDesc
        tbNewDepartEmail1.Text = DepartEmail1
        tbNewDepartEmail2.Text = DepartEmail2
        tbNewGroupName.Text = DepartGroupName

        tbShowDepartName.Text = DepartName
        tbShowDepartDesc.Text = DepartDesc
        tbShowDepartEmail1.Text = DepartEmail1
        tbShowDepartEmail2.Text = DepartEmail2
        tbShowGroupName.Text = DepartGroupName
    End Sub

    Private Sub rcbDepartment_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbDepartment.SelectedIndexChanged
        If btSave.Visible Or chkShowDetail.Checked Then
            'update txtbox
            tbNewDepartment.Text = rcbDepartment.Text
            getDescription(rcbDepartment.Text)
        End If
    End Sub

    Protected Sub chkShowDetail_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowDetail.CheckedChanged
        If chkShowDetail.Checked Then MultiView1.ActiveViewIndex = 1 : getDescription(rcbDepartment.Text) Else MultiView1.ActiveViewIndex = 0 : ClearZero()
    End Sub
End Class