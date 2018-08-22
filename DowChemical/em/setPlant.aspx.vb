Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class setPlant
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
                'lbName.Text = employee.EmployeeName & " " & employee.EmployeeSurname.Substring(0, 1) & "."
                'lbEmail.Text = employee.EmployeeEmail
                'lbDowId.Text = employee.DowId
                'lbDepartName.Text = employee.DepartmentName
                'lbAccountType.Text = "[" & employee.AccountType & "]"
            End If
        End If
    End Sub

    '// -- Plant Mgr
    Private Sub ClearZero()
        chkConfirmDel.Visible = False
        tbNewPlant.Text = ""
        tbNewPlantDesc.Text = ""
        tbNewPlantEmail1.Text = ""
        tbNewPlantEmail2.Text = ""
        btSave.Visible = False
        lbFailureText.Text = ""
    End Sub

    Protected Sub btNewPlant_Click(sender As Object, e As EventArgs) Handles btNew.Click
        ClearZero()

        tbNewPlant.Focus()
        btSave.Text = "Add"
        btSave.Visible = True
    End Sub
    Private Sub btEdit_Click(sender As Object, e As EventArgs) Handles btEdit.Click
        ClearZero()

        getDescription(rcbPlant.Text)
        tbNewPlant.Text = rcbPlant.Text
        tbNewPlant.Focus()
        btSave.Text = "Update"
        btSave.Visible = True
    End Sub
    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If btSave.Text = "Add" Then
            If tbNewPlant.Text <> "" Then
                Dim newPlant As String = tbNewPlant.Text.Trim()
                If Not chkRepeat(newPlant) Then
                    Dim strIns As String = "INSERT INTO tblPlant(plantName, plantDesc, plantEmail1, plantEmail2) VALUES(@plantName, @plantDesc, @plantEmail1, @plantEmail2)"

                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim commandIns As New SqlCommand(strIns, conn)
                    commandIns.Parameters.Add("@plantName", SqlDbType.VarChar).Value = newPlant
                    commandIns.Parameters.Add("@plantDesc", SqlDbType.NVarChar).Value = tbNewPlantDesc.Text.Trim
                    commandIns.Parameters.Add("@plantEmail1", SqlDbType.VarChar).Value = tbNewPlantEmail1.Text.Trim
                    commandIns.Parameters.Add("@plantEmail2", SqlDbType.VarChar).Value = tbNewPlantEmail2.Text.Trim
                    Dim errIns As Integer = commandIns.ExecuteNonQuery()
                    conn.Close()

                    If errIns > 0 Then
                        ClearZero()

                        rcbPlant.DataBind()
                        rcbPlant.Items(rcbPlant.FindItemIndexByText(newPlant)).Selected = True
                        lbFailureText.Text = "add '" & newPlant & "' completed"
                        rgPlantList.Rebind()
                    Else
                        lbFailureText.Text = "Please contact administrator"
                    End If
                Else
                    lbFailureText.Text = "This plant already exists."
                End If
            Else
                lbFailureText.Text = "Please enter a valid plant feild."
            End If
        End If

        If btSave.Text = "Update" Then
            If tbNewPlant.Text <> "" And rcbPlant.SelectedIndex <> -1 Then
                Dim newPlant As String = tbNewPlant.Text.Trim()
                Dim newDesc As String = tbNewPlantDesc.Text.Trim()
                If Not chkRepeat(newPlant) Then
                    Dim strUpd As String = "UPDATE tblPlant SET plantName = @plantName, plantDesc = @plantDesc, plantEmail1 = @plantEmail1, plantEmail2 = @plantEmail2 WHERE plantId = @plantId"
                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim cmdUpd As New SqlCommand(strUpd, conn)
                    cmdUpd.Parameters.Add("@plantId", SqlDbType.Int).Value = CInt(rcbPlant.SelectedValue)
                    cmdUpd.Parameters.Add("@plantName", SqlDbType.VarChar).Value = newPlant
                    cmdUpd.Parameters.Add("@plantDesc", SqlDbType.NVarChar).Value = newDesc
                    cmdUpd.Parameters.Add("@plantEmail1", SqlDbType.VarChar).Value = tbNewPlantEmail1.Text.Trim
                    cmdUpd.Parameters.Add("@plantEmail2", SqlDbType.VarChar).Value = tbNewPlantEmail2.Text.Trim
                    Dim errUpd As Integer = cmdUpd.ExecuteNonQuery()
                    conn.Close()

                    If errUpd > 0 Then
                        ClearZero()

                        rcbPlant.DataBind()
                        rcbPlant.Items(rcbPlant.FindItemIndexByText(newPlant)).Selected = True
                        lbFailureText.Text = "edit '" & newPlant & "' completed"
                        rgPlantList.Rebind()
                    Else
                        lbFailureText.Text = "Please contact administrator (2.105)"
                    End If
                Else
                    'chk desc
                    If rcbPlant.Text = newPlant Then
                        'update description only
                        Dim strUpd As String = "UPDATE tblPlant SET plantDesc = @plantDesc, plantEmail1 = @plantEmail1, plantEmail2 = @plantEmail2 WHERE plantId = @plantId"
                        Dim conn As New SqlConnection(ConnStr)
                        conn.Open()
                        Dim cmdUpd As New SqlCommand(strUpd, conn)
                        cmdUpd.Parameters.Add("@plantId", SqlDbType.Int).Value = CInt(rcbPlant.SelectedValue)
                        cmdUpd.Parameters.Add("@plantDesc", SqlDbType.NVarChar).Value = newDesc
                        cmdUpd.Parameters.Add("@plantEmail1", SqlDbType.VarChar).Value = tbNewPlantEmail1.Text.Trim
                        cmdUpd.Parameters.Add("@plantEmail2", SqlDbType.VarChar).Value = tbNewPlantEmail2.Text.Trim
                        Dim errUpd As Integer = cmdUpd.ExecuteNonQuery()
                        conn.Close()

                        ClearZero()

                        rcbPlant.DataBind()
                        lbFailureText.Text = "edit '" & newDesc & "' completed"
                        rgPlantList.Rebind()
                    Else
                        lbFailureText.Text = "This plant already exists."
                    End If
                End If
            Else
                lbFailureText.Text = "Please enter a valid plant feild."
            End If
        End If
    End Sub
    Private Sub btDel_Click(sender As Object, e As EventArgs) Handles btDel.Click
        lbFailureText.Text = ""

        If rcbPlant.SelectedIndex <> -1 Then
            If chkConfirmDel.Visible Then
                If chkConfirmDel.Checked Then
                    'Chk Plant
                    Dim PlantId As Integer = CInt(rcbPlant.SelectedValue)
                    Dim strCount As String = "SELECT COUNT(*) FROM tblPlant WHERE (plantId = @plantId)"
                    Dim connChk As New SqlConnection(ConnStr)
                    connChk.Open()
                    Dim commandChk As New SqlCommand(strCount, connChk)
                    commandChk.Parameters.Add("@plantId", SqlDbType.Int).Value = PlantId
                    Dim Count As Integer = commandChk.ExecuteScalar()
                    connChk.Close()

                    Dim IsAcceptDelete As Boolean = (Count = 1) And (PlantId >= 1000)
                    '-- เพิ่ม logic ถ้าถูกนำไปใช้แล้ว ไม่อนุญาติให้ลบ 
                    '///
                    '///
                    If IsAcceptDelete And chkConfirmDel.Checked Then
                        Dim strDel As String = "DELETE FROM tblPlant WHERE (plantId = @plantId)"
                        Dim conn As New SqlConnection(ConnStr)
                        conn.Open()
                        Dim command As New SqlCommand(strDel, conn)
                        command.Parameters.Add("@plantId", SqlDbType.Int).Value = PlantId
                        Dim errDel As Integer = command.ExecuteNonQuery()
                        conn.Close()

                        If errDel > 0 Then
                            chkConfirmDel.Checked = False
                            chkConfirmDel.Visible = False
                            lbFailureText.Text = "delete completed."
                            rcbPlant.DataBind()
                            rgPlantList.Rebind()
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

        tbNewPlant.Text = ""
        btSave.Visible = False
    End Sub
    Private Function chkRepeat(ByVal PlantDesc As String) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblPlant WHERE plantName = @plantName"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@plantName", SqlDbType.VarChar).Value = PlantDesc
        Dim Count As Integer = command.ExecuteScalar()
        conn.Close()

        If Count = 0 Then Return False Else Return True
    End Function

    Private Sub getDescription(ByVal PlantName As String)
        Dim PlantDesc As String = ""
        Dim PlantEmail1 As String = ""
        Dim PlantEmail2 As String = ""

        Dim strSql As String = "SELECT * FROM tblPlant WHERE (plantName = '" & PlantName & "')"
        Dim conn As New SqlConnection(ConnStr)
        Dim command As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
        conn.Open()
        Dim DataRead As SqlDataReader = command.ExecuteReader()
        If DataRead.HasRows() Then
            DataRead.Read()
            If DataRead("plantDesc") IsNot DBNull.Value Then PlantDesc = DataRead("plantDesc")
            If DataRead("plantEmail1") IsNot DBNull.Value Then PlantEmail1 = DataRead("plantEmail1")
            If DataRead("plantEmail2") IsNot DBNull.Value Then PlantEmail2 = DataRead("plantEmail2")
        End If
        conn.Close()

        tbNewPlantDesc.Text = PlantDesc
        tbNewPlantEmail1.Text = PlantEmail1
        tbNewPlantEmail2.Text = PlantEmail2

        tbShowPlantName.Text = PlantName
        tbShowPlantDesc.Text = PlantDesc
        tbShowPlantEmail1.Text = PlantEmail1
        tbShowPlantEmail2.Text = PlantEmail2
    End Sub

    Private Sub rcbPlant_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbPlant.SelectedIndexChanged
        If btSave.Visible Or chkShowDetail.Checked Then
            'update txtbox
            tbNewPlant.Text = rcbPlant.Text
            getDescription(rcbPlant.Text)
        End If
    End Sub

    Protected Sub chkShowDetail_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowDetail.CheckedChanged
        If chkShowDetail.Checked Then MultiView1.ActiveViewIndex = 1 : getDescription(rcbPlant.Text) Else MultiView1.ActiveViewIndex = 0 : ClearZero()
    End Sub
End Class