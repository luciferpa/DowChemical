Imports Telerik.Web.UI

Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'https://
            If Not Request.IsLocal And Not HttpContext.Current.Request.IsSecureConnection Then
                Response.Redirect("https://www.dowezpath.com")
            End If

            Dim IPStrIdx As Integer = HttpContext.Current.Request.Url.ToString.IndexOf("203.154.0.175")
            If IPStrIdx > 0 Then
                MsgBox(HttpContext.Current.Request.Url.ToString)
                Dim pageUrl As String = HttpContext.Current.Request.Url.ToString
                pageUrl = pageUrl.Substring(IPStrIdx, pageUrl.LastIndexOf("203.154.0.175") - IPStrIdx + 1)
                Response.Redirect("https://www.dowezpath.com" & pageUrl)
            End If

            If User.Identity.Name <> "" Then
                Dim employee As New cEmployee
                employee.FindEmployeeIdbyUsername(User.Identity.Name)

                If employee.EmployeeId <> 0 Then
                    lbName.Text = employee.EmployeeName & " " & employee.EmployeeSurname.Substring(0, 1) & "."
                    lbEmail.Text = employee.EmployeeEmail
                    lbDowId.Text = employee.DowId
                    lbDepartName.Text = employee.DepartmentName
                    HdDepartId.Value = employee.DepartmentId
                    HdEmpId.Value = employee.EmployeeId
                    lbAccountType.Text = "[" & employee.AccountType & "]"

                End If
            Else
                Response.Redirect("/Account/Login.aspx")
            End If
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

        If Not User.Identity.IsAuthenticated Then
            pnAvatar.Visible = False
        End If

    End Sub

    Private Sub RadPanelBar1_ItemClick(sender As Object, e As RadPanelBarEventArgs) Handles RadPanelBar1.ItemClick
        If e.Item.Items.Count > 0 Then
            If e.Item.Text = "SETTING" Or e.Item.Text = "REPORT" Then
                e.Item.Selected = False
                RadPanelBar1.Items.FindItemByText("HOME").Selected = True
            End If
        End If

        'If e.Item.Items.Count > 0 Then
        '    If e.Item.Text = "SETTING" Or e.Item.Text = "REPORT" Then
        '        RadPanelBar1.Items.FindItemByText("HOME").Selected = False
        '        RadPanelBar1.Items.FindItemByText("OBSERVER").Selected = False
        '        RadPanelBar1.Items.FindItemByText("FOLLOW UP").Selected = False
        '    End If
        'End If
    End Sub

    Protected Sub rlvIndicator_NeedDataSource(sender As Object, e As RadListViewNeedDataSourceEventArgs) Handles rlvIndicator.NeedDataSource

    End Sub
End Class