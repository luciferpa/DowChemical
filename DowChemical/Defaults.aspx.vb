Imports Telerik.Web.UI

Public Class Defaults
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack And User.Identity.Name <> "" Then
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

        If User.IsInRole("SYSTEM ADMIN") Or User.IsInRole("FACILITY ADMIN") Then
            Dim SettingItem As RadPanelItem = RadPanelBar1.Items.FindItemByText("SETTING")
            'If User.IsInRole("FACILITY ADMIN") Then SettingItem.NavigateUrl = "~/em/setUserbyDepart.aspx?sel=setuserd"     'ยกเลิก @6/3/2017 อนุญาติให้ FACILITY ADMIN ได้สิทธิเหมือน SYSTEM ADMIN
            SettingItem.Visible = True
        End If

        If Not User.Identity.IsAuthenticated Then
            pnAvatar.Visible = False
        End If

        Response.Redirect("https://www.dowezpath.com/Default2.aspx")
    End Sub

    Private Sub RadPanelBar1_ItemClick(sender As Object, e As RadPanelBarEventArgs) Handles RadPanelBar1.ItemClick
        If e.Item.Items.Count > 0 Then
            If e.Item.Text = "REPORT" Then
                e.Item.Selected = False
                RadPanelBar1.Items.FindItemByText("HOME").Selected = True
            End If
        End If
    End Sub


End Class