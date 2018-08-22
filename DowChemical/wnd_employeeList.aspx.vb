Imports System.Web.Configuration
Imports System.Data.SqlClient
Imports DevExpress.Web
Imports Telerik.Web.UI

Public Class wnd_employeeList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ViewState("FilterExpression") = ""
        If Request.QueryString("who") <> "" Then
            hfWhoCall.Value = Request.QueryString("who")
        End If

        hfEmpFilterStr.Value = ViewState("FilterExpression")
        srcEmployeeList.FilterExpression = ViewState("FilterExpression")
    End Sub

    Private Sub rcbrcbDepartment_DataBound(sender As Object, e As EventArgs) Handles rcbDepartment.DataBound
        Dim combo As RadComboBox = DirectCast(sender, RadComboBox)
        combo.Items.Insert(0, New RadComboBoxItem("Show all department", "0"))
    End Sub

    Private Sub grid_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles grid.CommandButtonInitialize
        If e.ButtonType <> DevExpress.Web.ColumnCommandButtonType.Select Then
            Return
        End If

        Dim childGrid As ASPxGridView = TryCast(sender, ASPxGridView)
        Dim isRowSelected As Boolean = childGrid.Selection.IsRowSelected(e.VisibleIndex)
        If isRowSelected Then
            e.Image.Url = "../Images/tick_1-18.png"
        Else
            e.Image.Url = "../Images/blank4h18.png"
        End If
    End Sub
    Private Sub grid_SelectionChanged(sender As Object, e As EventArgs) Handles grid.SelectionChanged
        If grid.Selection.Count <> 0 Then
            Dim sel_id As List(Of Object) = grid.GetSelectedFieldValues("empId")
            Dim sel_name As List(Of Object) = grid.GetSelectedFieldValues("empName")
            Dim sel_surename As List(Of Object) = grid.GetSelectedFieldValues("empSurname")
            hfSelEmpId.Value = sel_id(0).ToString
            hfSelEmpFullname.Value = sel_name(0).ToString & "  " & sel_surename(0).ToString
        Else
            hfSelEmpId.Value = 0
        End If
    End Sub
    Private Sub grid_PageIndexChanged(sender As Object, e As EventArgs) Handles grid.PageIndexChanged
        SearchKeyword()
    End Sub
    Private Sub grid_HtmlRowCreated(sender As Object, e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles grid.HtmlRowCreated
        If e.RowType = DevExpress.Web.GridViewRowType.Data Then
            e.Row.Height = Unit.Pixel(200)
        End If
    End Sub

    Private Sub grid_CustomColumnDisplayText(sender As Object, e As ASPxGridViewColumnDisplayTextEventArgs) Handles grid.CustomColumnDisplayText
        If e.Column.FieldName = "empName" Then
            If e.Value IsNot Nothing Then
                Dim TextMaxLength As Integer = 20
                Dim cellValue As String = e.Value.ToString()
                If cellValue.Length > TextMaxLength Then
                    e.DisplayText = cellValue.Substring(0, TextMaxLength) & "..."
                End If
            End If
        End If
        If e.Column.FieldName = "empSurname" Then
            If e.Value IsNot Nothing Then
                Dim TextMaxLength As Integer = 30
                Dim cellValue As String = e.Value.ToString()
                If cellValue.Length > TextMaxLength Then
                    e.DisplayText = cellValue.Substring(0, TextMaxLength) & "..."
                End If
            End If
        End If
    End Sub

    Private Sub SearchKeyword()
        Dim s As String = tbSearchKeyword.Text.Trim()
        If rcbDepartment.SelectedIndex = 0 Then
            ViewState("FilterExpression") = "(empName LIKE '%" & s & "%' OR empSurname LIKE '%" & s & "%' OR empDowId LIKE '%" & s & "%')"

            If tbSearchKeyword.Text <> "" Then
                srcEmployeeList.FilterExpression = ViewState("FilterExpression")
                imbSearch_Cancel.Visible = True
            Else
                'ไม่มี Keyword ต้อง reset ปุ่มกลับเป็นปกติ
                srcEmployeeList.FilterExpression = hfEmpFilterStr.Value.ToString
                ViewState("FilterExpression") = hfEmpFilterStr.Value.ToString

                imbSearch_Cancel.Visible = False
            End If
        Else
            Dim ddl As String = rcbDepartment.Text
            ViewState("FilterExpression") = "(empName LIKE '%" & s & "%' OR empSurname LIKE '%" & s & "%' OR empDowId LIKE '%" & s & "%') AND (departName LIKE '%" & ddl & "%')"

            srcEmployeeList.FilterExpression = ViewState("FilterExpression")

            imbSearch_Cancel.Visible = True
        End If

        grid.DataBind()
    End Sub

    Private Sub rcbDepartment_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbDepartment.SelectedIndexChanged
        'Dim sqlStr As String = srcEmployeeList.SelectCommand & " AND departName = '" & rcbDepartment.Text & "'"
        'grid.DataSource = srcEmployeeList

        SearchKeyword()
    End Sub

    Protected Sub imbSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imbSearch.Click
        SearchKeyword()
    End Sub
    Protected Sub imbSearch_Cancel_Click(sender As Object, e As ImageClickEventArgs) Handles imbSearch_Cancel.Click
        tbSearchKeyword.Text = ""
        '///srcCustomer.FilterExpression = hfEmpFilterStr.Value.ToString
        ViewState("FilterExpression") = hfEmpFilterStr.Value.ToString
        imbSearch_Cancel.Visible = False
        grid.DataBind()
    End Sub

End Class