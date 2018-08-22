Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class setContractors
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

    '// -- Plant Mgr
    Private Sub ClearZero()
        chkConfirmDel.Visible = False
        tbNewContractor.Text = ""
        btSave.Visible = False
        lbFailureText.Text = ""
    End Sub

    Protected Sub btNewPlant_Click(sender As Object, e As EventArgs) Handles btNew.Click
        ClearZero()

        tbNewContractor.Focus()
        btSave.Text = "Add"
        btSave.Visible = True
    End Sub
    Private Sub btEdit_Click(sender As Object, e As EventArgs) Handles btEdit.Click
        ClearZero()

        tbNewContractor.Text = rcbContractor.Text
        tbNewContractor.Focus()
        btSave.Text = "Update"
        btSave.Visible = True
    End Sub
    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If btSave.Text = "Add" Then
            If tbNewContractor.Text <> "" Then
                Dim newContractor As String = tbNewContractor.Text.Trim()
                If Not chkRepeat(newContractor) Then
                    Dim strIns As String = "INSERT INTO tblContractor(contractorName) VALUES(@contractorName)"

                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim commandIns As New SqlCommand(strIns, conn)
                    commandIns.Parameters.Add("@contractorName", SqlDbType.NVarChar).Value = newContractor
                    Dim resultIns As Integer = commandIns.ExecuteNonQuery()
                    conn.Close()

                    If resultIns > 0 Then
                        ClearZero()

                        rcbContractor.DataBind()
                        rcbContractor.Items(rcbContractor.FindItemIndexByText(newContractor)).Selected = True
                        lbFailureText.Text = "add '" & newContractor & "' completed"
                        rgContractorList.Rebind()
                    Else
                        lbFailureText.Text = "Please contact administrator."
                    End If
                Else
                    lbFailureText.Text = "This contractor already exists."
                End If
            Else
                lbFailureText.Text = "Please enter a valid contractor feild."
            End If
        End If

        If btSave.Text = "Update" Then
            If tbNewContractor.Text <> "" And rcbContractor.SelectedIndex <> -1 Then
                Dim newContractor As String = tbNewContractor.Text.Trim()
                If Not chkRepeat(newContractor) Then
                    Dim strUpd As String = "UPDATE tblContractor SET contractorName = @contractorName WHERE contractorId = @contractorId"
                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim cmdUpd As New SqlCommand(strUpd, conn)
                    cmdUpd.Parameters.Add("@contractorId", SqlDbType.Int).Value = CInt(rcbContractor.SelectedValue)
                    cmdUpd.Parameters.Add("@contractorName", SqlDbType.VarChar).Value = newContractor
                    Dim resultUpd As Integer = cmdUpd.ExecuteNonQuery()
                    conn.Close()

                    If resultUpd > 0 Then
                        ClearZero()

                        rcbContractor.DataBind()
                        rcbContractor.Items(rcbContractor.FindItemIndexByText(newContractor)).Selected = True
                        lbFailureText.Text = "edit '" & newContractor & "' completed"
                        rgContractorList.Rebind()
                    Else
                        lbFailureText.Text = "Please contact administrator (2.93)"
                    End If
                Else
                    lbFailureText.Text = "This contractor already exists."
                End If
            Else
                lbFailureText.Text = "Please enter a valid contractor feild."
            End If
        End If
    End Sub
    Private Sub btDel_Click(sender As Object, e As EventArgs) Handles btDel.Click
        lbFailureText.Text = ""

        If rcbContractor.SelectedIndex <> -1 Then
            If chkConfirmDel.Visible Then
                If chkConfirmDel.Checked Then
                    'Chk Plant
                    Dim ContractorId As Integer = CInt(rcbContractor.SelectedValue)
                    Dim strCount As String = "SELECT COUNT(*) FROM tblContractor WHERE (contractorId = @contractorId)"
                    Dim connChk As New SqlConnection(ConnStr)
                    connChk.Open()
                    Dim commandChk As New SqlCommand(strCount, connChk)
                    commandChk.Parameters.Add("@contractorId", SqlDbType.Int).Value = ContractorId
                    Dim Count As Integer = commandChk.ExecuteScalar()
                    connChk.Close()

                    Dim IsAcceptDelete As Boolean = (Count = 1) And (ContractorId >= 1000)
                    '-- เพิ่ม logic ถ้าถูกนำไปใช้แล้ว ไม่อนุญาติให้ลบ 
                    '///
                    '///
                    If IsAcceptDelete And chkConfirmDel.Checked Then
                        Dim strDel As String = "DELETE FROM tblContractor WHERE (contractorId = @contractorId)"
                        Dim conn As New SqlConnection(ConnStr)
                        conn.Open()
                        Dim command As New SqlCommand(strDel, conn)
                        command.Parameters.Add("@contractorId", SqlDbType.Int).Value = ContractorId
                        Dim resultDel As Integer = command.ExecuteNonQuery()
                        conn.Close()

                        If resultDel > 0 Then
                            chkConfirmDel.Checked = False
                            chkConfirmDel.Visible = False
                            lbFailureText.Text = "delete completed."
                            rcbContractor.DataBind()
                            rgContractorList.Rebind()
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

        tbNewContractor.Text = ""
        btSave.Visible = False
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

    Private Sub rcbContractor_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbContractor.SelectedIndexChanged
        If btSave.Visible Then
            'update txtbox
            tbNewContractor.Text = rcbContractor.Text
        End If
    End Sub
End Class