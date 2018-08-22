Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class setOffHour
    Inherits Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

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

            rcbYear.SelectedValue = Now.Year
            rdpDate.SelectedDate = Now
            Dim strSql As String = "SELECT * FROM tblOffHourWorkTime"
            Dim conn As New SqlConnection(ConnStr)
            conn.Open()
            Using command As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
                Dim DataRead As SqlDataReader = command.ExecuteReader()
                If DataRead.HasRows() Then
                    DataRead.Read()
                    rtpOfficeOpen.SelectedTime = DataRead("workStart")
                    rtpOfficeClose.SelectedTime = DataRead("workEnd")
                End If
            End Using
            conn.Close()
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

    Private Function updateOpenTime() As Integer
        Dim strUpd As String = "UPDATE tblOffHourWorkTime SET workStart = @workStart"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@workStart", SqlDbType.Time).Value = rtpOfficeOpen.SelectedTime
        Dim errCode As Integer = command.ExecuteNonQuery()
        Return errCode
    End Function
    Private Function updateClosedTime() As Integer
        Dim strUpd As String = "UPDATE tblOffHourWorkTime SET workEnd = @workEnd"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)
        command.Parameters.Add("@workEnd", SqlDbType.Time).Value = rtpOfficeClose.SelectedTime
        Dim errCode As Integer = command.ExecuteNonQuery()
        Return errCode
    End Function

    Protected Sub chkEnEdit_CheckedChanged(sender As Object, e As EventArgs) Handles chkEnEdit.CheckedChanged
        If chkEnEdit.Checked Then
            updateOpenTime()
            updateClosedTime()
        Else
            lbInfo.Text = ""
        End If
    End Sub

    Protected Sub rtpOfficeOpen_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles rtpOfficeOpen.SelectedDateChanged
        If chkEnEdit.Checked Then
            Dim result As Integer = updateOpenTime()
            If result = 1 Then lbInfo.Text = "Open time update to " & rtpOfficeOpen.SelectedTime.ToString & "." Else lbInfo.Text = ""
        Else
            lbInfo.Text = ""
        End If
    End Sub
    Protected Sub rtpOfficeEnd_SelectedDateChanged(sender As Object, e As Calendar.SelectedDateChangedEventArgs) Handles rtpOfficeClose.SelectedDateChanged
        If chkEnEdit.Checked Then
            Dim result As Integer = updateClosedTime()
            If result = 1 Then lbInfo.Text = "Closed time update to " & rtpOfficeClose.SelectedTime.ToString & "." Else lbInfo.Text = ""
        Else
            lbInfo.Text = ""
        End If
    End Sub

    Protected Sub rcbYear_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbYear.SelectedIndexChanged
        rgHolidayList.Rebind()
    End Sub

    Protected Sub btAddHoliday_Click(sender As Object, e As EventArgs) Handles btAddHoliday.Click
        If tbDesc.Text.Trim <> "" Then
            Dim strIns As String = "INSERT INTO tblOffHourHoliday(holidayYear, holidayDate, holidayDesc) 
                                    VALUES(@holidayYear, @holidayDate, @holidayDesc)"

            Dim conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strIns, conn)
            command.Parameters.Add("@holidayYear", SqlDbType.Int).Value = CInt(rcbYear.SelectedValue)
            command.Parameters.Add("@holidayDate", SqlDbType.DateTime).Value = rdpDate.SelectedDate
            command.Parameters.Add("@holidayDesc", SqlDbType.NVarChar).Value = tbDesc.Text.Trim
            Dim err As Integer = command.ExecuteNonQuery()
            conn.Close()

            If err = 1 Then
                tbDesc.Text = ""
                rgHolidayList.Rebind()
            End If
        End If
    End Sub

    Protected Sub cbShow_Del_CheckedChanged(sender As Object, e As EventArgs) Handles cbShow_Del.CheckedChanged
        rgHolidayList.Rebind()
    End Sub

    Private itemCount As Integer = 0
    Private Sub rgHolidayList_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgHolidayList.ItemDataBound
        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim HolidayId = item.GetDataKeyValue("holidayId")

            If TypeOf e.Item Is GridDataItem Then
                Dim btDel As ImageButton = item.FindControl("imgbDel")
                btDel.CommandArgument = HolidayId
                btDel.Visible = cbShow_Del.Checked

                itemCount = itemCount + 1
            End If
        End If
        If TypeOf e.Item Is GridFooterItem Then
            Dim footer As GridFooterItem = DirectCast((e.Item), GridFooterItem)
            DirectCast(footer("TemplateColumn").FindControl("lbTotalItems"), Label).Text = itemCount.ToString
        End If
    End Sub

    Protected Sub imgbDel_Click(sender As Object, e As ImageClickEventArgs)
        Dim btSender As ImageButton = sender
        Dim HolidayId As Integer = CInt(btSender.CommandArgument)

        '-- Delete Record Document
        Dim delStr As String = "DELETE FROM tblOffHourHoliday WHERE (holidayId = @holidayId)"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(delStr, conn)
        command.Parameters.Add("@holidayId", SqlDbType.Int).Value = HolidayId
        Dim err1 As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub
End Class