Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class setCategory
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

    '// -- Category Mgr
    Private Sub ClearZero()
        chkConfirmDel.Visible = False
        tbNewCategoryName.Text = ""
        btSave.Visible = False
        lbFailureText.Text = ""
    End Sub

    Private Function chkCateRepeat(ByVal CateName As String) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblObsvCate WHERE cateName = @cateName"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@cateName", SqlDbType.NVarChar).Value = CateName
        Dim Count As Integer = command.ExecuteScalar()
        conn.Close()

        If Count = 0 Then Return False Else Return True
    End Function

    Protected Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        ClearZero()

        tbNewCategoryName.Focus()
        btSave.Text = "Add"
        btSave.Visible = True
    End Sub
    Protected Sub btEdit_Click(sender As Object, e As EventArgs) Handles btEdit.Click
        ClearZero()
        If rcbCategory.SelectedIndex > 0 Then
            tbNewCategoryName.Text = rcbCategory.Text
            tbNewCategoryName.Focus()
            btSave.Text = "Update"
            btSave.Visible = True
        End If
    End Sub
    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If btSave.Text = "Add" Then
            If tbNewCategoryName.Text <> "" Then
                Dim newCategory As String = tbNewCategoryName.Text.Trim
                If Not chkCateRepeat(newCategory) Then
                    Dim strIns As String = "INSERT INTO tblObsvCate(cateName) VALUES(@cateName)"
                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim commandIns As New SqlCommand(strIns, conn)
                    commandIns.Parameters.Add("@cateName", SqlDbType.NVarChar).Value = newCategory
                    Dim errIns As Integer = commandIns.ExecuteNonQuery()
                    conn.Close()

                    If errIns > 0 Then
                        ClearZero()

                        rcbCategory.DataBind()
                        rcbCategory.Items(rcbCategory.FindItemIndexByText(newCategory)).Selected = True
                        lbFailureText.Text = "add '" & newCategory & "' completed"
                        rgCategoryList.Rebind()

                        rcbCategory2.DataBind()
                        rcbCategory3.DataBind()
                    Else
                        lbFailureText.Text = "Please contact administrator"
                    End If
                Else
                    lbFailureText.Text = ".. already"
                End If
            Else
                lbFailureText.Text = ""
            End If
        End If

        If btSave.Text = "Update" Then
            If tbNewCategoryName.Text <> "" And rcbCategory.SelectedIndex > 0 Then
                Dim newCategory As String = tbNewCategoryName.Text.Trim()
                If Not chkCateRepeat(newCategory) Then
                    Dim strUpd As String = "UPDATE tblObsvCate SET cateName = @cateName WHERE cateId = @cateId"
                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim cmdUpd As New SqlCommand(strUpd, conn)
                    cmdUpd.Parameters.Add("@cateName", SqlDbType.NVarChar).Value = newCategory
                    cmdUpd.Parameters.Add("@cateId", SqlDbType.Int).Value = CInt(rcbCategory.SelectedValue)
                    Dim errUpd As Integer = cmdUpd.ExecuteNonQuery()
                    conn.Close()

                    If errUpd > 0 Then
                        ClearZero()

                        rcbCategory.DataBind()
                        rcbCategory.Items(rcbCategory.FindItemIndexByText(newCategory)).Selected = True
                        lbFailureText.Text = "Edit '" & newCategory & "' complete"
                        rgCategoryList.Rebind()

                        rcbCategory2.DataBind()
                        rcbCategory3.DataBind()
                    Else
                        lbFailureText.Text = "Edit fail, please contact admin (2.3)"
                    End If
                Else
                    lbFailureText.Text = ""
                End If
            Else
                lbFailureText.Text = ""
            End If
        End If
    End Sub
    Protected Sub btDel_Click(sender As Object, e As EventArgs) Handles btDel.Click
        lbFailureText.Text = ""

        If rcbCategory.SelectedIndex > 0 Then
            If chkConfirmDel.Visible Then
                If chkConfirmDel.Checked Then
                    'Chk Category
                    Dim CategoryId As Integer = CInt(rcbCategory.SelectedValue)
                    Dim strCount As String = "SELECT COUNT(*) FROM tblObsvCate WHERE (cateId = @cateId)"
                    Dim connChk As New SqlConnection(ConnStr)
                    connChk.Open()
                    Dim commandChk As New SqlCommand(strCount, connChk)
                    commandChk.Parameters.Add("@cateId", SqlDbType.Int).Value = CategoryId
                    Dim Count As Integer = commandChk.ExecuteScalar()
                    connChk.Close()

                    Dim IsAcceptDelete As Boolean = (Count = 1) And (CategoryId > 1000)
                    '-- เพิ่ม logic ถ้าถูกนำไปใช้แล้ว ไม่อนุญาติให้ลบ 
                    '///
                    '///
                    '///
                    If IsAcceptDelete And chkConfirmDel.Checked Then
                        Dim strDel As String = "DELETE FROM tblObsvCate WHERE (cateId = @cateId)"
                        Dim conn As New SqlConnection(ConnStr)
                        conn.Open()
                        Dim command As New SqlCommand(strDel, conn)
                        command.Parameters.Add("@cateId", SqlDbType.Int).Value = CategoryId
                        Dim errDel As Integer = command.ExecuteNonQuery()
                        conn.Close()

                        If errDel > 0 Then
                            chkConfirmDel.Checked = False
                            chkConfirmDel.Visible = False
                            lbFailureText.Text = "Delete complete"
                            rcbCategory.DataBind()
                            rgCategoryList.Rebind()

                            rcbCategory2.DataBind()
                            rcbCategory3.DataBind()
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

        tbNewCategoryName.Text = ""
        btSave.Visible = False
    End Sub

    Protected Sub rcbCategory_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCategory.SelectedIndexChanged
        If rcbCategory.SelectedIndex = 0 Then
            tbNewCategoryName.Text = ""
            btSave.Visible = False
            chkConfirmDel.Visible = False
        End If

        If btSave.Visible Then
            'update txtbox
            tbNewCategoryName.Text = rcbCategory.Text
        End If
    End Sub

    '// -- Sub Category Mgr
    Private Sub ClearZero2()
        chkConfirmDel2.Visible = False
        tbNewCategorySubName.Text = ""
        btSave2.Visible = False
        lbFailureText2.Text = ""
    End Sub

    Private Function chkCateSubRepeat(ByVal SubCateName As String, ByVal CateId As Integer) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblObsvCateSub WHERE catesubName = @catesubName AND cateId = @cateId"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@catesubName", SqlDbType.NVarChar).Value = SubCateName
        command.Parameters.Add("@cateId", SqlDbType.Int).Value = CateId
        Dim Count As Integer = command.ExecuteScalar()
        conn.Close()

        If Count = 0 Then Return False Else Return True
    End Function

    Protected Sub btNew2_Click(sender As Object, e As EventArgs) Handles btNew2.Click
        ClearZero2()
        If rcbCategory2.SelectedIndex > 0 Then
            tbNewCategorySubName.Focus()
            btSave2.Text = "Add"
            btSave2.Visible = True
        End If
    End Sub
    Protected Sub btEdit2_Click(sender As Object, e As EventArgs) Handles btEdit2.Click
        ClearZero2()

        If rcbCategory2.SelectedIndex > 0 And rcbCategorySub2.Text <> "" Then
            tbNewCategorySubName.Text = rcbCategorySub2.Text
            tbNewCategorySubName.Focus()
            btSave2.Text = "Update"
            btSave2.Visible = True
        End If
    End Sub
    Protected Sub btSave2_Click(sender As Object, e As EventArgs) Handles btSave2.Click
        If btSave2.Text = "Add" Then
            If tbNewCategorySubName.Text <> "" Then
                Dim CategoryId As Integer = CInt(rcbCategory2.SelectedValue)
                Dim newSubCategory As String = tbNewCategorySubName.Text.Trim
                If Not chkCateSubRepeat(newSubCategory, CategoryId) Then
                    Dim strIns As String = "INSERT INTO tblObsvCateSub(catesubName, cateId) VALUES(@catesubName, @cateId)"

                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim commandIns As New SqlCommand(strIns, conn)
                    commandIns.Parameters.Add("@catesubName", SqlDbType.NVarChar).Value = newSubCategory
                    commandIns.Parameters.Add("@cateId", SqlDbType.Int).Value = CategoryId
                    Dim errIns As Integer = commandIns.ExecuteNonQuery()
                    conn.Close()

                    If errIns > 0 Then
                        ClearZero2()

                        rcbCategorySub2.DataBind()
                        rcbCategorySub2.Items(rcbCategorySub2.FindItemIndexByText(newSubCategory)).Selected = True
                        lbFailureText2.Text = "add '" & newSubCategory & "' completed"
                        rgSubCategoryList.Rebind()

                        rcbCategorySub3.DataBind()
                    Else
                        lbFailureText2.Text = "Please contact administrator"
                    End If
                Else
                    lbFailureText2.Text = ".. already"
                End If
            Else
                lbFailureText2.Text = ""
            End If
        End If

        If btSave2.Text = "Update" Then
            If tbNewCategorySubName.Text <> "" And rcbCategorySub2.SelectedIndex >= 0 Then
                Dim CategoryId As Integer = CInt(rcbCategory2.SelectedValue)
                Dim newSubCategory As String = tbNewCategorySubName.Text.Trim()
                If Not chkCateSubRepeat(newSubCategory, CategoryId) Then
                    Dim strUpd As String = "UPDATE tblObsvCateSub SET catesubName = @catesubName WHERE catesubId = @catesubId"
                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim cmdUpd As New SqlCommand(strUpd, conn)
                    cmdUpd.Parameters.Add("@catesubName", SqlDbType.NVarChar).Value = newSubCategory
                    cmdUpd.Parameters.Add("@catesubId", SqlDbType.Int).Value = CInt(rcbCategorySub2.SelectedValue)
                    Dim errUpd As Integer = cmdUpd.ExecuteNonQuery()
                    conn.Close()

                    If errUpd > 0 Then
                        ClearZero2()

                        rcbCategorySub2.DataBind()
                        rcbCategorySub2.Items(rcbCategorySub2.FindItemIndexByText(newSubCategory)).Selected = True
                        lbFailureText2.Text = "Edit '" & newSubCategory & "' complete"
                        rgSubCategoryList.Rebind()

                        rcbCategorySub3.DataBind()
                    Else
                        lbFailureText2.Text = "Edit fail, please contact admin (2.3)"
                    End If
                Else
                    lbFailureText2.Text = ".. already"
                End If
            Else
                lbFailureText2.Text = ""
            End If
        End If
    End Sub
    Protected Sub btDel2_Click(sender As Object, e As EventArgs) Handles btDel2.Click
        lbFailureText2.Text = ""

        If rcbCategory2.SelectedIndex > 0 And rcbCategorySub2.Text <> "" Then
            If chkConfirmDel2.Visible Then
                If chkConfirmDel2.Checked Then
                    'Chk Sub Category
                    Dim CateSubId As Integer = CInt(rcbCategorySub2.SelectedValue)
                    Dim strCount As String = "SELECT COUNT(*) FROM tblObsvCateSub WHERE (catesubId = @catesubId)"
                    Dim connChk As New SqlConnection(ConnStr)
                    connChk.Open()
                    Dim commandChk As New SqlCommand(strCount, connChk)
                    commandChk.Parameters.Add("@catesubId", SqlDbType.Int).Value = CateSubId
                    Dim Count As Integer = commandChk.ExecuteScalar()
                    connChk.Close()

                    Dim IsAcceptDelete As Boolean = (Count = 1) And (CateSubId > 1000)
                    '-- เพิ่ม logic ถ้าถูกนำไปใช้แล้ว ไม่อนุญาติให้ลบ 
                    '///
                    '///
                    '///
                    If IsAcceptDelete And chkConfirmDel2.Checked Then
                        Dim strDel As String = "DELETE FROM tblObsvCateSub WHERE (catesubId = @catesubId)"
                        Dim conn As New SqlConnection(ConnStr)
                        conn.Open()
                        Dim command As New SqlCommand(strDel, conn)
                        command.Parameters.Add("@catesubId", SqlDbType.Int).Value = CateSubId
                        Dim errDel As Integer = command.ExecuteNonQuery()
                        conn.Close()

                        If errDel > 0 Then
                            chkConfirmDel2.Checked = False
                            chkConfirmDel2.Visible = False
                            lbFailureText2.Text = "Delete complete"
                            rcbCategorySub2.DataBind()
                            rgSubCategoryList.Rebind()

                            rcbCategorySub3.DataBind()
                        End If
                    End If
                Else
                    chkConfirmDel2.Visible = False
                End If
            Else
                chkConfirmDel2.Visible = True
            End If
        End If
    End Sub

    Private Sub rcbCategory2_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCategory2.SelectedIndexChanged
        If rcbCategory2.SelectedIndex = 0 Or btSave2.Visible Or chkConfirmDel2.Visible Then
            ClearZero2()
        End If

        rcbCategorySub2.DataBind()
    End Sub
    Private Sub rcbCategorySub2_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCategorySub2.SelectedIndexChanged
        If btSave2.Visible Then
            'update txtbox
            tbNewCategorySubName.Text = rcbCategorySub2.Text
        End If
    End Sub

    '// -- Fail Point Category Mgr
    Private Sub ClearZero3()
        chkConfirmDel3.Visible = False
        tbNewFailPointName.Text = ""
        btSave3.Visible = False
        lbFailureText3.Text = ""
    End Sub

    Private Function chkFailPointRepeat(ByVal FailPoint As String, ByVal SubCateId As Integer) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblObsvFailPoint WHERE failpointName = @failpointName AND catesubId = @catesubId"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@failpointName", SqlDbType.NVarChar).Value = FailPoint
        command.Parameters.Add("@catesubId", SqlDbType.Int).Value = SubCateId
        Dim Count As Integer = command.ExecuteScalar()
        conn.Close()

        If Count = 0 Then Return False Else Return True
    End Function

    Protected Sub btNew3_Click(sender As Object, e As EventArgs) Handles btNew3.Click
        ClearZero3()
        If rcbCategory3.SelectedIndex > 0 And rcbCategorySub3.SelectedIndex >= 0 Then
            tbNewFailPointName.Focus()
            btSave3.Text = "Add"
            btSave3.Visible = True
        End If
    End Sub
    Protected Sub btEdit3_Click(sender As Object, e As EventArgs) Handles btEdit3.Click
        ClearZero3()

        If rcbCategory3.SelectedIndex > 0 And rcbCategorySub3.SelectedIndex >= 0 And rcbFailPoint3.Text <> "" Then
            tbNewFailPointName.Text = rcbFailPoint3.Text
            tbNewFailPointName.Focus()
            btSave3.Text = "Update"
            btSave3.Visible = True
        End If
    End Sub
    Protected Sub btSave3_Click(sender As Object, e As EventArgs) Handles btSave3.Click
        If btSave3.Text = "Add" Then
            If tbNewFailPointName.Text <> "" Then
                Dim SubCategoryId As Integer = CInt(rcbCategorySub3.SelectedValue)
                Dim newFailPoint As String = tbNewFailPointName.Text.Trim
                If Not chkCateSubRepeat(newFailPoint, SubCategoryId) Then
                    Dim strIns As String = "INSERT INTO tblObsvFailPoint(failpointName, catesubId) VALUES(@failpointName, @catesubId)"

                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim commandIns As New SqlCommand(strIns, conn)
                    commandIns.Parameters.Add("@failpointName", SqlDbType.NVarChar).Value = newFailPoint
                    commandIns.Parameters.Add("@catesubId", SqlDbType.Int).Value = SubCategoryId
                    Dim errIns As Integer = commandIns.ExecuteNonQuery()
                    conn.Close()

                    If errIns > 0 Then
                        ClearZero3()

                        rcbFailPoint3.DataBind()
                        rcbFailPoint3.Items(rcbFailPoint3.FindItemIndexByText(newFailPoint)).Selected = True
                        lbFailureText3.Text = "add '" & newFailPoint & "' completed"
                        rgFailPointList.Rebind()
                    Else
                        lbFailureText3.Text = "Please contact administrator"
                    End If
                Else
                    lbFailureText3.Text = ".. already"
                End If
            Else
                lbFailureText3.Text = ""
            End If
        End If

        If btSave3.Text = "Update" Then
            If tbNewFailPointName.Text <> "" And rcbFailPoint3.SelectedIndex >= 0 Then
                Dim SubCategoryId As Integer = CInt(rcbCategorySub3.SelectedValue)
                Dim newFailPoint As String = tbNewFailPointName.Text.Trim
                If Not chkCateSubRepeat(newFailPoint, SubCategoryId) Then
                    Dim strUpd As String = "UPDATE tblObsvFailPoint SET failpointName = @failpointName WHERE failpointId = @failpointId"
                    Dim conn As New SqlConnection(ConnStr)
                    conn.Open()
                    Dim cmdUpd As New SqlCommand(strUpd, conn)
                    cmdUpd.Parameters.Add("@failpointName", SqlDbType.NVarChar).Value = newFailPoint
                    cmdUpd.Parameters.Add("@failpointId", SqlDbType.Int).Value = CInt(rcbFailPoint3.SelectedValue)
                    Dim errUpd As Integer = cmdUpd.ExecuteNonQuery()
                    conn.Close()

                    If errUpd > 0 Then
                        ClearZero3()

                        rcbFailPoint3.DataBind()
                        rcbFailPoint3.Items(rcbFailPoint3.FindItemIndexByText(newFailPoint)).Selected = True
                        lbFailureText3.Text = "Edit '" & newFailPoint & "' complete"
                        rgFailPointList.Rebind()
                    Else
                        lbFailureText3.Text = "Edit fail, please contact admin (2.3)"
                    End If
                Else
                    lbFailureText3.Text = ".. already"
                End If
            Else
                lbFailureText3.Text = ""
            End If
        End If
    End Sub
    Protected Sub btDel3_Click(sender As Object, e As EventArgs) Handles btDel3.Click
        lbFailureText3.Text = ""

        If rcbCategory3.SelectedIndex > 0 And rcbFailPoint3.Text <> "" Then
            If chkConfirmDel3.Visible Then
                If chkConfirmDel3.Checked Then
                    'Chk ...
                    Dim FailPointId As Integer = CInt(rcbFailPoint3.SelectedValue)
                    Dim strCount As String = "SELECT COUNT(*) FROM tblObsvFailPoint WHERE (failpointId = @failpointId)"
                    Dim connChk As New SqlConnection(ConnStr)
                    connChk.Open()
                    Dim commandChk As New SqlCommand(strCount, connChk)
                    commandChk.Parameters.Add("@failpointId", SqlDbType.Int).Value = FailPointId
                    Dim Count As Integer = commandChk.ExecuteScalar()
                    connChk.Close()

                    Dim IsAcceptDelete As Boolean = (Count = 1)
                    '-- เพิ่ม logic ถ้าถูกนำไปใช้แล้ว ไม่อนุญาติให้ลบ 
                    '///
                    '///
                    '///
                    If IsAcceptDelete And chkConfirmDel3.Checked Then
                        Dim strDel As String = "DELETE FROM tblObsvFailPoint WHERE (failpointId = @failpointId)"
                        Dim conn As New SqlConnection(ConnStr)
                        conn.Open()
                        Dim command As New SqlCommand(strDel, conn)
                        command.Parameters.Add("@failpointId", SqlDbType.Int).Value = FailPointId
                        Dim errDel As Integer = command.ExecuteNonQuery()
                        conn.Close()

                        If errDel > 0 Then
                            chkConfirmDel3.Checked = False
                            chkConfirmDel3.Visible = False
                            lbFailureText3.Text = "Delete complete"
                            rcbFailPoint3.DataBind()
                            rgFailPointList.Rebind()
                        End If
                    End If
                Else
                    chkConfirmDel3.Visible = False
                End If
            Else
                chkConfirmDel3.Visible = True
            End If
        End If
    End Sub

    Protected Sub rcbCategory3_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCategory3.SelectedIndexChanged
        If rcbCategory3.SelectedIndex = 0 Or btSave3.Visible Or chkConfirmDel3.Visible Then
            ClearZero3()
        End If

        If IsCategorySubNothing(rcbCategory3.SelectedIndex) Then
            rcbFailPoint3.Items.Clear()
        End If

        rcbCategorySub3.DataBind()
        rcbFailPoint3.DataBind()
        rgFailPointList.Rebind()
    End Sub
    Protected Sub rcbCategorySub3_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCategorySub3.SelectedIndexChanged
        If btSave3.Visible Or chkConfirmDel3.Visible Then
            ClearZero3()
        End If
    End Sub
    Protected Sub rcbFailPoint3_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbFailPoint3.SelectedIndexChanged
        If btSave3.Visible Then
            'update txtbox when edit mode
            tbNewFailPointName.Text = rcbFailPoint3.Text
        End If
    End Sub

    Private Function IsCategorySubNothing(CateId) As Boolean
        Dim strSql As String = "SELECT catesubName FROM tblObsvCateSub WHERE (cateId = @cateId)"
        Dim conn As New SqlConnection(ConnStr)
        Dim command As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
        command.Parameters.Add("@cateId", SqlDbType.Int).Value = CateId
        conn.Open()
        Dim dataRead As SqlDataReader = command.ExecuteReader()

        If dataRead.HasRows() Then Return False Else Return True
    End Function

    'Private Sub RadTabStrip1_TabClick(sender As Object, e As RadTabStripEventArgs) Handles RadTabStrip1.TabClick
    '    For Each tab As RadTab In RadTabStrip1.Tabs
    '        If tab Is e.Tab Then
    '            MsgBox(e.Tab.Index.ToString)
    '        End If
    '    Next
    'End Sub
End Class