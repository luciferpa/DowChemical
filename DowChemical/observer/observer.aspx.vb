Imports System.Web.Services
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports AiLib
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports Telerik.Web.UI.Calendar

Public Class observer
    Inherits Page
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Dim MsgRecognition As String = "Recognition *Propose action is not required*"

    Private Sub MsgBoxRad(ByVal Msg As String, ByVal Width As Integer, ByVal Height As Integer)
        RadWindowManager1.Width = Width
        RadWindowManager1.Height = Height
        RadWindowManager1.RadAlert(Msg, Width + 100, Height + 72, "My Alert", "", "myAlertImage.png")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        '-- init owner
        'Label2.Text = User.Identity.Name        '-- debug

        Dim employee As New cEmployee
        employee.FindEmployeeIdbyUsername(User.Identity.Name)

        Dim oEmail As String = employee.EmployeeEmail
        Dim oDowId As String = employee.DowId
        Dim oDepart As String = employee.DepartmentName
        Dim oJiblb As String = employee.JobLevel

        If employee.EmployeeId <> 0 Then
            hfOwnerEmpId.Value = employee.EmployeeId
            tbMyFullname.Text = employee.EmployeeFullName
            tbMyDowId.Text = oDowId
            tbMyDepart.Text = oDepart

            lbName.Text = employee.EmployeeName & " " & employee.EmployeeSurname.Substring(0, 1) & "."
            lbEmail.Text = oEmail
            lbDowId.Text = oDowId
            lbDepartName.Text = oDepart
            lbAccountType.Text = "[" & employee.AccountType & "]"
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

        '/// debug
        'Label1.Text = employee.EmployeeId.ToString
        'Label2.Text = hfRecId.Value

        If Not Page.IsPostBack Then
            '-- init database
            Dim RecordId As Integer = 0
            Do Until ChkTempRecord(RecordId) <> 0
                InsBlankRecord()
            Loop
            LockRecordId(RecordId)
            hfRecId.Value = RecordId
            'Label1.Text = RecordId      '-- debug

            '-- clear fail record
            Dim connection As SqlConnection = New SqlConnection(ConnStr)
            Dim command As New SqlCommand
            connection.Open()
            command.Connection = connection
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "recordDetail_init_record"

            Dim paramRecordId As SqlParameter = New SqlParameter("@RecordId", RecordId) With {.Direction = ParameterDirection.Input, .DbType = DbType.Int32}
            command.Parameters.Add(paramRecordId)
            command.ExecuteNonQuery()
            connection.Close()

            '-- auto numnber
            Dim AutoNumber As New aAutoNumber
            lbActionNum.Text = oDepart & CStr(AutoNumber.ActNumberAutoNum(oDepart, Today()))

            rdpDocDate.SelectedDate = Now.ToShortDateString
            '@25/4/2017 บังคับให้ user ใส่เวลา
            'Dim timeIdx As Integer
            'If Now.Hour < 7 Then timeIdx = Now.Hour + 17 Else timeIdx = Now.Hour - 7
            'rcbTimeHH.SelectedIndex = timeIdx
            'rcbTimeMM.SelectedIndex = Now.Minute \ 5

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "pnHideHROStart", "pnHideHROStart();", True)
        Else
            infoboxCssStyle()

            '-- fix style display=none;
            For i As Integer = 1 To rcbNoObserve.SelectedValue
                Dim hfPnHRO As HiddenField = RadMultiPage1.FindControl("hfPnHRO" & i.ToString)
                If Not CBool(hfPnHRO.Value) Then
                    hfPnHRO.Visible = True
                    Dim ScriptStr As String = "pnHideCaseHRO" & i.ToString & "();"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), ScriptStr, ScriptStr, True)
                End If
            Next
        End If
    End Sub

    Private Sub RadPanelBar1_ItemClick(sender As Object, e As RadPanelBarEventArgs) Handles RadPanelBar1.ItemClick
        If e.Item.Items.Count > 0 Then
            If e.Item.Text = "SETTING" Or e.Item.Text = "REPORT" Then
                e.Item.Selected = False
                RadPanelBar1.Items.FindItemByText("OBSERVER").Selected = True
            End If
        End If
    End Sub

    Private Sub rcbDepartment_DataBound(sender As Object, e As EventArgs) Handles rcbDepartment.DataBound
        If Not Page.IsPostBack And lbDepartName.Text <> "" Then      'lbEmail.Text = ownerEmail
            'rcbDepartment.Items(rcbDepartment.FindItemIndexByText(lbDepartName.Text)).Selected = True
            Dim rcb As RadComboBox = sender
            rcb.Items.Insert(0, New RadComboBoxItem("[ Select Department ]", ""))
        End If
    End Sub
    'Private Sub rcbTimeHH_DataBound(sender As Object, e As EventArgs) Handles rcbTimeHH.DataBound
    '    Dim rcb As RadComboBox = sender
    '    rcb.Items.Insert(0, New RadComboBoxItem("&nbsp;", "x"))
    'End Sub
    'Private Sub rcbTimeMM_DataBound(sender As Object, e As EventArgs) Handles rcbTimeMM.DataBound
    '    Dim rcb As RadComboBox = sender
    '    rcb.Items.Insert(0, New RadComboBoxItem("&;", "x"))
    'End Sub


    '//---- Event Control ----
    Private Sub rcbDepartment_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbDepartment.SelectedIndexChanged
        '-- auto numnber
        Dim AutoNumber As New aAutoNumber
        lbActionNum.Text = rcbDepartment.Text & CStr(AutoNumber.ActNumberAutoNum(rcbDepartment.Text, rdpDocDate.SelectedDate))
        rdpDocDate.SelectedDate = Now.ToShortDateString
    End Sub

    Private Sub rdpDocDate_Load(sender As Object, e As EventArgs) Handles rdpDocDate.Load
        If Not Page.IsPostBack Then
            Dim rdpDocumentDate As RadDatePicker = sender
            rdpDocumentDate.MinDate = DateTime.Today.AddMonths(-1)
            rdpDocumentDate.MaxDate = DateTime.Today
        End If
    End Sub
    Private Sub rdpDocDate_SelectedDateChanged(sender As Object, e As SelectedDateChangedEventArgs) Handles rdpDocDate.SelectedDateChanged
        '-- update auto numnber
        Dim AutoNumber As New aAutoNumber
        lbActionNum.Text = rcbDepartment.Text & CStr(AutoNumber.ActNumberAutoNum(rcbDepartment.Text, rdpDocDate.SelectedDate))
    End Sub

    Private Sub racObservBox_EntryAdded(sender As Object, e As AutoCompleteEntryEventArgs) Handles racObservBox.EntryAdded
        Dim empId As String = e.Entry.Value.ToString
        Dim EmpFullName As String = e.Entry.Text
        racObservBoxAddEntry(empId, EmpFullName)
    End Sub

    Private Sub racObservBox_EntryRemoved(sender As Object, e As AutoCompleteEntryEventArgs) Handles racObservBox.EntryRemoved
        Dim strDel As String = "DELETE FROM tblRecordOthEmp WHERE (recId = @recId) AND (empIdOth = @empIdOth)"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strDel, conn)
        command.Parameters.Add("@recId", SqlDbType.Int).Value = CInt(hfRecId.Value)
        command.Parameters.Add("@empIdOth", SqlDbType.Int).Value = CInt(e.Entry.Value)
        Dim errDel As Integer = command.ExecuteNonQuery()
        conn.Close()

        Dim count As Integer = racObservBox.Entries.Count
        If errDel > 0 Then
            If count > 0 Then
                're-index
                ReIndexItem()

                rgObserverList.DataBind()
                pnOtherObserver.Visible = True
            Else
                pnOtherObserver.Visible = False
            End If
        End If
    End Sub

    Protected Sub imgAddEntry_Click(sender As Object, e As ImageClickEventArgs) Handles imgAddEntry.Click
        If hfEmpIdSelect.Value <> "0" Then
            racObservBoxAddEntry(hfEmpIdSelect.Value, hfFullNameSelect.Value, True)
            hfEmpIdSelect.Value = "0"
            hfFullNameSelect.Value = ""
        End If
    End Sub

    Protected Sub rcbNoObserve_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbNoObserve.SelectedIndexChanged
        For i As Integer = 0 To 5
            If i < rcbNoObserve.SelectedValue Then
                rtabStrip1.Tabs(i).Visible = True
            Else
                rtabStrip1.Tabs(i).Visible = False
            End If
        Next
        If rtabStrip1.SelectedIndex > rcbNoObserve.SelectedValue Then
            rtabStrip1.SelectedIndex = 0
        End If
    End Sub


    '//---- Private Function ----
    Private Function ChkTempRecord(ByRef RecordId As Integer) As Integer
        Dim strSql As String = "SELECT MAX(recActDate) AS lastDate, recId FROM tblRecord WHERE (tempFlag = 'true') AND (tempLock IS NULL OR tempLock < { fn NOW() }) GROUP BY recId"
        '                       
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Using command As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
            Dim DataRead As SqlDataReader = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                RecordId = CInt(DataRead("recId"))
            End If
        End Using
        conn.Close()

        Return RecordId
    End Function

    Private Sub InsBlankRecord()
        Dim strIns As String = "INSERT INTO tblRecord(recActDate) VALUES(@recActDate)"

        '-- Insert Command
        Dim conn As New SqlConnection(ConnStr)
        Dim command As New SqlCommand(strIns, conn)
        conn.Open()
        command.Parameters.Add("@recActDate", SqlDbType.DateTime).Value = Now()
        Dim err As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Function LockRecordId(ByVal Id As Integer) As Integer
        Dim protectTime As Integer = 12   'Hour

        '-- Lock [tblRecord]
        Dim strUpd As String = "UPDATE tblRecord SET tempLock = @tempLock WHERE recId = @recId"

        '-- Update Command
        Dim conn As New SqlConnection(ConnStr)
        Dim command As New SqlCommand(strUpd, conn)

        conn.Open()
        Dim LockDate As DateTime = DateAdd(DateInterval.Hour, protectTime, Now())
        command.Parameters.Add("@recId", SqlDbType.Int).Value = Id
        command.Parameters.Add("@tempLock", SqlDbType.DateTime).Value = LockDate
        Dim err As Integer = command.ExecuteNonQuery()
        conn.Close()

        Return err
    End Function

    Private Sub AddObservListToDB(ByVal empId As Integer, ByRef count As Integer)
        Dim strIns As String = "INSERT INTO tblRecordOthEmp(recId, recItem, empIdOth) VALUES(@recId, @recItem, @empIdOth)"
        Dim item As Integer = count

        '-- Insert Command
        Dim conn As New SqlConnection(ConnStr)
        Dim command As New SqlCommand(strIns, conn)
        conn.Open()
        command.Parameters.Add("@recId", SqlDbType.Int).Value = CInt(hfRecId.Value)
        command.Parameters.Add("@recItem", SqlDbType.Int).Value = item
        command.Parameters.Add("@empIdOth", SqlDbType.Int).Value = empId
        Dim result As Integer = command.ExecuteNonQuery()
        conn.Close()

        If result = 1 Then count = count + 1
    End Sub

    Public Function chkOtherEmployeeRepeat(ByVal EmpID As Integer) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblRecordOthEmp WHERE (recId = @recId) AND (empIdOth = @empIdOth)"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@recId", SqlDbType.Int).Value = CInt(hfRecId.Value)
        command.Parameters.Add("@empIdOth", SqlDbType.Int).Value = EmpID
        Dim Count As Integer = command.ExecuteScalar()
        conn.Close()

        If Count > 0 Then Return True Else Return False
    End Function

    Public Sub racObservBoxAddEntry(ByVal empId As String, ByVal EmpFullName As String, Optional ByVal IsFromWnd As Boolean = 0)
        Dim count As Integer = racObservBox.Entries.Count

        ' Obs#1
        If empId = "" Then
            racObservBox.Entries.RemoveAt(count - 1)
            Return
        End If

        '-- chk repeat
        If empId = hfOwnerEmpId.Value.ToString Then
            racObservBox.Entries.RemoveAt(count - 1)
            MsgBoxRad("<b>This Observer same Observer #1</b>", 240, 76)
            Return
        End If

        ' OtherObs
        '-- chk OtherObs Max
        If count > 5 Then
            racObservBox.Entries.RemoveAt(count - 1)
            Return
        End If

        '-- chk otherEmp repeat
        If Not chkOtherEmployeeRepeat(CInt(empId)) Then
            If IsFromWnd Then
                'racObservBox.Entries.Insert(count - 1, New AutoCompleteBoxEntry(EmpFullName, empId))
                racObservBox.Entries.Insert(count, New AutoCompleteBoxEntry(EmpFullName, empId))
                racObservBox.DataBind()
                count = count + 1
            End If
            AddObservListToDB(empId, count)

            If count > 1 Then
                hfEmpIdSelect.Value = "0"
                rgObserverList.DataBind()
                pnOtherObserver.Visible = True
                racObservBox.Focus()
            End If
            tokenInfo.Text = ""
        Else
            '-- remove token
            racObservBox.Entries.RemoveAt(count - 1)
            MsgBoxRad("<b>The observer with this name already exists.</b>", 280, 76)
            'tokenInfo.Text = "Token Remove"
        End If
    End Sub

    Private Sub ReIndexItem()
        '-- Re-Index
        Dim RecId As Integer = CInt(hfRecId.Value)
        Dim selConn As New SqlConnection(ConnStr)
        Dim selSql As String = "SELECT Id FROM tblRecordOthEmp WHERE (recId = @recId) ORDER BY recItem"
        Dim updSql As String = "UPDATE tblRecordOthEmp SET recItem = @recItem WHERE Id = @Id"

        selConn.Open()
        Using commandSel As New SqlCommand(selSql, selConn) With {.CommandType = CommandType.Text}
            commandSel.Parameters.Add("@recId", SqlDbType.Int).Value = RecId
            Dim updConn As New SqlConnection(ConnStr)

            updConn.Open()
            Using commandUpd As New SqlCommand(updSql, updConn) With {.CommandType = CommandType.Text}
                Dim para_itemNoOri As SqlParameter = commandUpd.Parameters.Add("@Id", SqlDbType.Int)            'WHERE
                Dim para_itemNo As SqlParameter = commandUpd.Parameters.Add("@recItem", SqlDbType.Int)          'VALUE

                Dim itemNoCount As Integer = 1
                Dim DataRead As SqlDataReader
                DataRead = commandSel.ExecuteReader()
                While DataRead.Read()
                    para_itemNoOri.Value = CInt(DataRead("Id"))
                    para_itemNo.Value = itemNoCount
                    itemNoCount = itemNoCount + 1
                    commandUpd.ExecuteNonQuery()
                End While
            End Using
            updConn.Close()
        End Using
        selConn.Close()
    End Sub

    Private Sub ClearPicture(ByVal observeItem As Integer, ByVal ButtonClose As ImageButton)
        Dim recId As Integer = CInt(hfRecId.Value)
        Dim imbt As ImageButton = DirectCast(ButtonClose, ImageButton)
        Dim strDel As String = "DELETE FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = @observeItem) AND (picItem = @picItem)"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strDel, conn)
        command.Parameters.Add("@recId", SqlDbType.Int).Value = recId
        command.Parameters.Add("@observeItem", SqlDbType.Int).Value = observeItem
        command.Parameters.Add("@picItem", SqlDbType.Int).Value = CInt(Right(imbt.ClientID, 1)) + 1
        Dim errDel As Integer = command.ExecuteNonQuery()
        conn.Close()

        '-- Re-Picture Index
        Dim selConn As New SqlConnection(ConnStr)
        Dim selSql As String = "SELECT Id FROM tblRecordPicture WHERE (recId = @recId) AND (observeItem = @observeItem) ORDER BY picItem"
        Dim updSql As String = "UPDATE tblRecordPicture SET picItem = @picItem WHERE Id = @Id"

        selConn.Open()
        Using commandSel As New SqlCommand(selSql, selConn) With {.CommandType = CommandType.Text}
            commandSel.Parameters.Add("@recId", SqlDbType.Int).Value = recId
            commandSel.Parameters.Add("@observeItem", SqlDbType.Int).Value = observeItem

            Dim updConn As New SqlConnection(ConnStr)

            updConn.Open()
            Using commandUpd As New SqlCommand(updSql, updConn) With {.CommandType = CommandType.Text}
                commandUpd.Parameters.Add("@recId", SqlDbType.Int).Value = recId
                Dim para_itemNoOri As SqlParameter = commandUpd.Parameters.Add("@Id", SqlDbType.Int)            'WHERE
                Dim para_itemNo As SqlParameter = commandUpd.Parameters.Add("@picItem", SqlDbType.Int)          'VALUE

                Dim itemNoCount As Integer = 1
                Dim DataRead As SqlDataReader
                DataRead = commandSel.ExecuteReader()
                While DataRead.Read()
                    para_itemNoOri.Value = CInt(DataRead("Id"))
                    para_itemNo.Value = itemNoCount
                    itemNoCount = itemNoCount + 1
                    commandUpd.ExecuteNonQuery()
                End While
            End Using
            updConn.Close()
        End Using
        selConn.Close()
    End Sub

    Private Sub UploadImage(ByVal RadUpload As RadAsyncUpload, ByVal ObserveTab As Integer, ByVal PictureCount As Integer)
        'กำหนดชื่อไฟล์
        Dim CurrentPath As String = Request.PhysicalApplicationPath
        CurrentPath += "ImagesUpload\"

        Dim rnd As New Random
        Dim intrnd As Integer = rnd.Next(1000, 9999)
        Dim uniqueFilename As String
        uniqueFilename = Now.Year.ToString.Substring(2, 2) & Now.Month.ToString("00") & intrnd.ToString & ObserveTab.ToString & PictureCount.ToString
        Dim imageExtension As String = RadUpload.UploadedFiles(0).GetExtension().ToString()
        Dim SaveFilesOri As String = ""
        Dim oFileName As String = ""
        For Each f As UploadedFile In RadUpload.UploadedFiles
            oFileName = Strings.Left(f.GetNameWithoutExtension, 20)
            SaveFilesOri = CurrentPath & uniqueFilename & Strings.Left(oFileName, 20) & "_original" & imageExtension
            f.SaveAs(SaveFilesOri)
        Next
        RadUpload.UploadedFiles.Clear()

        'resize image
        Dim oriImage As System.Drawing.Image
        oriImage = System.Drawing.Image.FromFile(SaveFilesOri)
        Dim newHeight As Integer
        Dim newWidth As Integer
        If oriImage.Height > 720 Then
            newHeight = 720
            newWidth = (oriImage.Width * newHeight) / oriImage.Height
        Else
            newHeight = oriImage.Height
            newWidth = oriImage.Width
        End If

        'quality
        Dim newImage As New Bitmap(newWidth, newHeight)
        Using g As Graphics = Graphics.FromImage(DirectCast(newImage, System.Drawing.Image))
            g.DrawImage(oriImage, 0, 0, newWidth, newHeight)
            g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        End Using
        Dim Info As ImageCodecInfo() = ImageCodecInfo.GetImageEncoders()
        Dim Params As New EncoderParameters(1)
        Params.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L)

        Dim SaveFilesName As String
        SaveFilesName = CurrentPath & uniqueFilename & Strings.Left(oFileName, 20) & imageExtension
        newImage.Save(SaveFilesName, Info(1), Params)
        newImage.Dispose()
        oriImage.Dispose()

        'delete original image
        Dim FileInServ As New FileInfo(SaveFilesOri)
        If FileInServ.Exists Then
            FileInServ.Delete()
        End If

        '-- Insert Command
        Dim strIns As String = "INSERT INTO tblRecordPicture(recId, empId, observeItem, picItem, picUrl) VALUES(@recId, @empId, @observeItem, @picItem, @picUrl)"
        Dim conn As New SqlConnection(ConnStr)
        Dim command As New SqlCommand(strIns, conn)
        conn.Open()
        command.Parameters.Add("@recId", SqlDbType.Int).Value = CInt(hfRecId.Value)
        command.Parameters.Add("@empId", SqlDbType.Int).Value = CInt(hfOwnerEmpId.Value)
        command.Parameters.Add("@observeItem", SqlDbType.Int).Value = ObserveTab
        command.Parameters.Add("@picItem", SqlDbType.Int).Value = PictureCount + 1
        command.Parameters.Add("@picUrl", SqlDbType.NVarChar).Value = "~/ImagesUpload/" & uniqueFilename & oFileName & imageExtension
        Dim result As Integer = command.ExecuteNonQuery()
        conn.Close()
    End Sub

    Private Sub CreateBigThumbNail(ByVal newWidth As Integer, ByVal file As UploadedFile)
        Dim target As String = Server.MapPath("~/ImagesUpload")
        If Not Directory.Exists(target) Then
            Directory.CreateDirectory(target)
        End If
        file.GetName()
        file.SaveAs(Path.Combine(target, file.GetName()))
        Using originalImage As New Bitmap(file.InputStream)
            Dim width As Integer = newWidth
            Dim height As Integer = (originalImage.Height * newWidth) / originalImage.Width
            Dim thumbnail As New Bitmap(width, height)
            Using g As Graphics = Graphics.FromImage(DirectCast(thumbnail, System.Drawing.Image))
                g.DrawImage(originalImage, 0, 0, width, height)
            End Using
            Dim thumbnailFileName As String = Path.Combine(target, String.Format("{0}_bthumb{1}", file.GetNameWithoutExtension(), file.GetExtension()))
            thumbnail.Save(thumbnailFileName)
        End Using
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


    '//---- Observe TAB #1
    Protected Sub rcbCategory1_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCategory1.SelectedIndexChanged
        'If IsCategorySubNothing(rcbCategory1.SelectedIndex) Then
        '    rcbFailurePoint1.Items.Clear()
        'End If
        rcbCategorySub1.DataBind()
        rcbFailurePoint1.DataBind()
    End Sub
    Protected Sub chkRecognition1_CheckedChanged(sender As Object, e As EventArgs) Handles chkRecognition1.CheckedChanged
        If chkRecognition1.Checked Then
            tbAction1a.Text = MsgRecognition
            tbAction1a.Enabled = False

            racRespon1a.Enabled = False
            imbtFindRespon1a.Enabled = False

            If pnRespon1b.Visible Then tbAction1b.Text = MsgRecognition : tbAction1b.Enabled = False
            If pnRespon1c.Visible Then tbAction1c.Text = MsgRecognition : tbAction1c.Enabled = False

            chkNonRecognition1.Checked = True
        Else
            tbAction1a.Text = ""
            tbAction1a.Enabled = True

            racRespon1a.Enabled = True
            imbtFindRespon1a.Enabled = True

            If pnRespon1b.Visible Then tbAction1b.Text = "" : tbAction1b.Enabled = True
            If pnRespon1c.Visible Then tbAction1c.Text = "" : tbAction1c.Enabled = True

            chkNonRecognition1.Checked = False
        End If
    End Sub

    Private Sub chkNonRecognition1_CheckedChanged(sender As Object, e As EventArgs) Handles chkNonRecognition1.CheckedChanged
        If chkNonRecognition1.Checked Then
            tbAction1a.Text = MsgRecognition
            tbAction1a.Enabled = False

            racRespon1a.Enabled = False
            imbtFindRespon1a.Enabled = False

            If pnRespon1b.Visible Then tbAction1b.Text = MsgRecognition : tbAction1b.Enabled = False
            If pnRespon1c.Visible Then tbAction1c.Text = MsgRecognition : tbAction1c.Enabled = False
            chkRecognition1.Checked = True
        Else
            tbAction1a.Text = ""
            tbAction1a.Enabled = True

            racRespon1a.Enabled = True
            imbtFindRespon1a.Enabled = True

            If pnRespon1b.Visible Then tbAction1b.Text = "" : tbAction1b.Enabled = True
            If pnRespon1c.Visible Then tbAction1c.Text = "" : tbAction1c.Enabled = True
            chkRecognition1.Checked = False
        End If
    End Sub

    Protected Sub rcbObserveType1_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbObserveType1.SelectedIndexChanged
        If rcbObserveType1.SelectedIndex = 1 Then rcbContractor1.Visible = True Else rcbContractor1.Visible = False
    End Sub

    '------ upload picture ----
    Protected Sub btUploadImg1_Click(sender As Object, e As EventArgs) Handles btUploadImg1.Click
        Dim fileCount As Integer = RadUpload1.UploadedFiles.Count.ToString
        If fileCount <> 0 And PictureList1.Items.Count < 4 Then
            UploadImage(RadUpload1, 0, PictureList1.Items.Count)    'rtabStrip1.SelectedIndex = 0

            PictureList1.DataBind()
            pnShowImage1.Visible = True
        End If
    End Sub
    Protected Sub imbtClose1_Click(sender As Object, e As ImageClickEventArgs)
        ClearPicture(0, sender)                                     'rtabStrip1.SelectedIndex = 0
        PictureList1.DataBind()
    End Sub

    '------ Propose Action ----
    Protected Sub imbtOtherAction1a_Click(sender As Object, e As EventArgs) Handles imbtOtherAction1a.Click
        If pnRespon1b.Visible Then
            pnRespon1b.Visible = False
            pnRespon1c.Visible = False
        Else
            pnRespon1b.Visible = True
            imbtOtherAction1a.Visible = False
            If chkRecognition1.Checked Then tbAction1b.Text = MsgRecognition Else tbAction1b.Text = ""
            tbAction1b.Enabled = Not chkRecognition1.Checked
            chkRecognition1.Enabled = False
            If chkNonRecognition1.Checked Then tbAction1b.Text = MsgRecognition Else tbAction1b.Text = ""
            tbAction1b.Enabled = Not chkNonRecognition1.Checked
            chkNonRecognition1.Enabled = False
        End If
    End Sub
    Protected Sub imbtOtherAction1b_Click(sender As Object, e As EventArgs) Handles imbtOtherAction1b.Click
        If pnRespon1c.Visible Then
            pnRespon1c.Visible = False
        Else
            pnRespon1c.Visible = True
            imbtOtherAction1b.Visible = False
            If chkRecognition1.Checked Then tbAction1c.Text = MsgRecognition Else tbAction1c.Text = ""
            tbAction1c.Enabled = Not chkRecognition1.Checked
            If chkNonRecognition1.Checked Then tbAction1c.Text = MsgRecognition Else tbAction1c.Text = ""
            tbAction1c.Enabled = Not chkNonRecognition1.Checked
        End If
    End Sub
    Protected Sub imbtCloseAction1b_Click(sender As Object, e As EventArgs) Handles imbtCloseAction1b.Click
        chkRecognition1.Enabled = True
        chkNonRecognition1.Enabled = True

        pnRespon1b.Visible = False
        imbtOtherAction1a.Visible = True

        pnRespon1c.Visible = False
        imbtOtherAction1b.Visible = True
    End Sub
    Protected Sub imbtCloseAction1c_Click(sender As Object, e As EventArgs) Handles imbtCloseAction1c.Click
        pnRespon1c.Visible = False
        imbtOtherAction1b.Visible = True
    End Sub


    '//---- Observe TAB #2
    Protected Sub rcbCategory2_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCategory2.SelectedIndexChanged
        'If IsCategorySubNothing(rcbCategory2.SelectedIndex) Then
        '    rcbFailurePoint2.Items.Clear()
        'End If
        rcbCategorySub2.DataBind()
        rcbFailurePoint2.DataBind()
    End Sub
    Protected Sub chkRecognition2_CheckedChanged(sender As Object, e As EventArgs) Handles chkRecognition2.CheckedChanged
        If chkRecognition2.Checked Then
            tbAction2a.Text = MsgRecognition
            tbAction2a.Enabled = False

            racRespon2a.Enabled = False
            imbtFindRespon2a.Enabled = False

            If pnRespon2b.Visible Then tbAction2b.Text = MsgRecognition : tbAction2b.Enabled = False
            If pnRespon2c.Visible Then tbAction2c.Text = MsgRecognition : tbAction2c.Enabled = False
        Else
            tbAction2a.Text = ""
            tbAction2a.Enabled = True

            racRespon2a.Enabled = True
            imbtFindRespon2a.Enabled = True

            If pnRespon2b.Visible Then tbAction2b.Text = "" : tbAction2b.Enabled = True
            If pnRespon2c.Visible Then tbAction2c.Text = "" : tbAction2c.Enabled = True
        End If
    End Sub
    Protected Sub rcbObserveType2_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbObserveType2.SelectedIndexChanged
        If rcbObserveType2.SelectedIndex = 1 Then rcbContractor2.Visible = True Else rcbContractor2.Visible = False
    End Sub

    '------ upload picture ----
    Protected Sub btUploadImg2_Click(sender As Object, e As EventArgs) Handles btUploadImg2.Click
        Dim fileCount As Integer = RadUpload2.UploadedFiles.Count.ToString
        If fileCount <> 0 And PictureList2.Items.Count < 4 Then
            UploadImage(RadUpload2, 1, PictureList2.Items.Count)    'rtabStrip1.SelectedIndex = 1

            PictureList2.DataBind()
            pnShowImage2.Visible = True
        End If
    End Sub
    Protected Sub imbtClose2_Click(sender As Object, e As ImageClickEventArgs)
        ClearPicture(1, sender)                                     'rtabStrip1.SelectedIndex = 1
        PictureList2.DataBind()
    End Sub

    '------ Propose Action ----
    Protected Sub imbtOtherAction2a_Click(sender As Object, e As EventArgs) Handles imbtOtherAction2a.Click
        If pnRespon2b.Visible Then
            pnRespon2b.Visible = False
            pnRespon2c.Visible = False
        Else
            pnRespon2b.Visible = True
            imbtOtherAction2a.Visible = False
            If chkRecognition2.Checked Then tbAction2b.Text = MsgRecognition Else tbAction2b.Text = ""
            tbAction2b.Enabled = Not chkRecognition2.Checked
            chkRecognition2.Enabled = False
        End If
    End Sub
    Protected Sub imbtOtherAction2b_Click(sender As Object, e As EventArgs) Handles imbtOtherAction2b.Click
        If pnRespon2c.Visible Then
            pnRespon2c.Visible = False
        Else
            pnRespon2c.Visible = True
            imbtOtherAction2b.Visible = False
            If chkRecognition2.Checked Then tbAction2c.Text = MsgRecognition Else tbAction2c.Text = ""
            tbAction2c.Enabled = Not chkRecognition2.Checked
        End If
    End Sub
    Protected Sub imbtCloseAction2b_Click(sender As Object, e As EventArgs) Handles imbtCloseAction2b.Click
        chkRecognition2.Enabled = True

        pnRespon2b.Visible = False
        imbtOtherAction2a.Visible = True

        pnRespon2c.Visible = False
        imbtOtherAction2b.Visible = True
    End Sub
    Protected Sub imbtCloseAction2c_Click(sender As Object, e As EventArgs) Handles imbtCloseAction2c.Click
        pnRespon2c.Visible = False
        imbtOtherAction2b.Visible = True
    End Sub


    '//---- Observe TAB #3
    Protected Sub rcbCategory3_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCategory3.SelectedIndexChanged
        'If IsCategorySubNothing(rcbCategory3.SelectedIndex) Then
        '    rcbFailurePoint3.Items.Clear()
        'End If
        rcbCategorySub3.DataBind()
        rcbFailurePoint3.DataBind()
    End Sub
    Protected Sub chkRecognition3_CheckedChanged(sender As Object, e As EventArgs) Handles chkRecognition3.CheckedChanged
        If chkRecognition3.Checked Then
            tbAction3a.Text = MsgRecognition
            tbAction3a.Enabled = False

            racRespon3a.Enabled = False
            imbtFindRespon3a.Enabled = False

            If pnRespon3b.Visible Then tbAction3b.Text = MsgRecognition : tbAction3b.Enabled = False
            If pnRespon3c.Visible Then tbAction3c.Text = MsgRecognition : tbAction3c.Enabled = False
        Else
            tbAction3a.Text = ""
            tbAction3a.Enabled = True

            racRespon3a.Enabled = True
            imbtFindRespon3a.Enabled = True

            If pnRespon3b.Visible Then tbAction1b.Text = "" : tbAction3b.Enabled = True
            If pnRespon3c.Visible Then tbAction1c.Text = "" : tbAction3c.Enabled = True
        End If
    End Sub
    Protected Sub rcbObserveType3_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbObserveType3.SelectedIndexChanged
        If rcbObserveType3.SelectedIndex = 1 Then rcbContractor3.Visible = True Else rcbContractor3.Visible = False
    End Sub

    '------ upload picture ----
    Protected Sub btUploadImg3_Click(sender As Object, e As EventArgs) Handles btUploadImg3.Click
        Dim fileCount As Integer = RadUpload3.UploadedFiles.Count.ToString
        If fileCount <> 0 And PictureList3.Items.Count < 4 Then
            UploadImage(RadUpload3, 2, PictureList3.Items.Count)    'rtabStrip1.SelectedIndex = 2

            PictureList3.DataBind()
            pnShowImage3.Visible = True
        End If
    End Sub
    Protected Sub imbtClose3_Click(sender As Object, e As ImageClickEventArgs)
        ClearPicture(2, sender)                                     'rtabStrip1.SelectedIndex = 2
        PictureList3.DataBind()
    End Sub

    '------ Propose Action ----
    Protected Sub imbtOtherAction3a_Click(sender As Object, e As EventArgs) Handles imbtOtherAction3a.Click
        If pnRespon3b.Visible Then
            pnRespon3b.Visible = False
            pnRespon3c.Visible = False
        Else
            pnRespon3b.Visible = True
            imbtOtherAction3a.Visible = False
            If chkRecognition3.Checked Then tbAction3b.Text = MsgRecognition Else tbAction3b.Text = ""
            tbAction3b.Enabled = Not chkRecognition3.Checked
            chkRecognition3.Enabled = False
        End If
    End Sub
    Protected Sub imbtOtherAction3b_Click(sender As Object, e As EventArgs) Handles imbtOtherAction3b.Click
        If pnRespon3c.Visible Then
            pnRespon3c.Visible = False
        Else
            pnRespon3c.Visible = True
            imbtOtherAction3b.Visible = False
            If chkRecognition3.Checked Then tbAction3c.Text = MsgRecognition Else tbAction3c.Text = ""
            tbAction3c.Enabled = Not chkRecognition3.Checked
        End If
    End Sub
    Protected Sub imbtCloseAction3b_Click(sender As Object, e As EventArgs) Handles imbtCloseAction3b.Click
        chkRecognition3.Enabled = True

        pnRespon3b.Visible = False
        imbtOtherAction3a.Visible = True

        pnRespon3c.Visible = False
        imbtOtherAction3b.Visible = True
    End Sub
    Protected Sub imbtCloseAction3c_Click(sender As Object, e As EventArgs) Handles imbtCloseAction3c.Click
        pnRespon3c.Visible = False
        imbtOtherAction3b.Visible = True
    End Sub


    '//---- Observe TAB #4
    Protected Sub rcbCategory4_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCategory4.SelectedIndexChanged
        'If IsCategorySubNothing(rcbCategory4.SelectedIndex) Then
        '    rcbFailurePoint4.Items.Clear()
        'End If
        rcbCategorySub4.DataBind()
        rcbFailurePoint4.DataBind()
    End Sub
    Protected Sub chkRecognition4_CheckedChanged(sender As Object, e As EventArgs) Handles chkRecognition4.CheckedChanged
        If chkRecognition4.Checked Then
            tbAction4a.Text = MsgRecognition
            tbAction4a.Enabled = False

            racRespon4a.Enabled = False
            imbtFindRespon4a.Enabled = False

            If pnRespon4b.Visible Then tbAction4b.Text = MsgRecognition : tbAction4b.Enabled = False
            If pnRespon4c.Visible Then tbAction4c.Text = MsgRecognition : tbAction4c.Enabled = False
        Else
            tbAction4a.Text = ""
            tbAction4a.Enabled = True

            racRespon4a.Enabled = True
            imbtFindRespon4a.Enabled = True

            If pnRespon4b.Visible Then tbAction4b.Text = "" : tbAction4b.Enabled = True
            If pnRespon4c.Visible Then tbAction4c.Text = "" : tbAction4c.Enabled = True
        End If
    End Sub
    Protected Sub rcbObserveType4_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbObserveType4.SelectedIndexChanged
        If rcbObserveType4.SelectedIndex = 1 Then rcbContractor4.Visible = True Else rcbContractor4.Visible = False
    End Sub

    '------ upload picture ----
    Protected Sub btUploadImg4_Click(sender As Object, e As EventArgs) Handles btUploadImg4.Click
        Dim fileCount As Integer = RadUpload4.UploadedFiles.Count.ToString
        If fileCount <> 0 And PictureList4.Items.Count < 4 Then
            UploadImage(RadUpload4, 3, PictureList4.Items.Count)    'rtabStrip1.SelectedIndex = 3

            PictureList4.DataBind()
            pnShowImage4.Visible = True
        End If
    End Sub
    Protected Sub imbtClose4_Click(sender As Object, e As ImageClickEventArgs)
        ClearPicture(3, sender)                                     'rtabStrip1.SelectedIndex = 3
        PictureList4.DataBind()
    End Sub

    '------ Propose Action ----
    Protected Sub imbtOtherAction4a_Click(sender As Object, e As EventArgs) Handles imbtOtherAction4a.Click
        If pnRespon4b.Visible Then
            pnRespon4b.Visible = False
            pnRespon4c.Visible = False
        Else
            pnRespon4b.Visible = True
            imbtOtherAction4a.Visible = False
            If chkRecognition4.Checked Then tbAction4b.Text = MsgRecognition Else tbAction4b.Text = ""
            tbAction4b.Enabled = Not chkRecognition4.Checked
            chkRecognition4.Enabled = False
        End If
    End Sub
    Protected Sub imbtOtherAction4b_Click(sender As Object, e As EventArgs) Handles imbtOtherAction4b.Click
        If pnRespon4c.Visible Then
            pnRespon4c.Visible = False
        Else
            pnRespon4c.Visible = True
            imbtOtherAction4b.Visible = False
            If chkRecognition4.Checked Then tbAction4c.Text = MsgRecognition Else tbAction4c.Text = ""
            tbAction4c.Enabled = Not chkRecognition4.Checked
        End If
    End Sub
    Protected Sub imbtCloseAction4b_Click(sender As Object, e As EventArgs) Handles imbtCloseAction4b.Click
        chkRecognition4.Enabled = True

        pnRespon4b.Visible = False
        imbtOtherAction4a.Visible = True

        pnRespon4c.Visible = False
        imbtOtherAction4b.Visible = True
    End Sub
    Protected Sub imbtCloseAction4c_Click(sender As Object, e As EventArgs) Handles imbtCloseAction4c.Click
        pnRespon4c.Visible = False
        imbtOtherAction4b.Visible = True
    End Sub


    '//---- Observe TAB #5
    Protected Sub rcbCategory5_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCategory5.SelectedIndexChanged
        'If IsCategorySubNothing(rcbCategory5.SelectedIndex) Then
        '    rcbFailurePoint5.Items.Clear()
        'End If
        rcbCategorySub5.DataBind()
        rcbFailurePoint5.DataBind()
    End Sub
    Protected Sub chkRecognition5_CheckedChanged(sender As Object, e As EventArgs) Handles chkRecognition5.CheckedChanged
        If chkRecognition5.Checked Then
            tbAction5a.Text = MsgRecognition
            tbAction5a.Enabled = False

            racRespon5a.Enabled = False
            imbtFindRespon5a.Enabled = False

            If pnRespon5b.Visible Then tbAction5b.Text = MsgRecognition : tbAction5b.Enabled = False
            If pnRespon5c.Visible Then tbAction5c.Text = MsgRecognition : tbAction5c.Enabled = False
        Else
            tbAction5a.Text = ""
            tbAction5a.Enabled = True

            racRespon5a.Enabled = True
            imbtFindRespon5a.Enabled = True

            If pnRespon5b.Visible Then tbAction5b.Text = "" : tbAction5b.Enabled = True
            If pnRespon5c.Visible Then tbAction5c.Text = "" : tbAction5c.Enabled = True
        End If
    End Sub
    Protected Sub rcbObserveType5_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbObserveType5.SelectedIndexChanged
        If rcbObserveType5.SelectedIndex = 1 Then rcbContractor5.Visible = True Else rcbContractor5.Visible = False
    End Sub

    '------ upload picture ----
    Protected Sub btUploadImg5_Click(sender As Object, e As EventArgs) Handles btUploadImg5.Click
        Dim fileCount As Integer = RadUpload5.UploadedFiles.Count.ToString
        If fileCount <> 0 And PictureList5.Items.Count < 4 Then
            UploadImage(RadUpload5, 4, PictureList5.Items.Count)    'rtabStrip1.SelectedIndex = 4

            PictureList5.DataBind()
            pnShowImage5.Visible = True
        End If
    End Sub
    Protected Sub imbtClose5_Click(sender As Object, e As ImageClickEventArgs)
        ClearPicture(4, sender)                                     'rtabStrip1.SelectedIndex = 4
        PictureList5.DataBind()
    End Sub

    '------ Propose Action ----
    Protected Sub imbtOtherAction5a_Click(sender As Object, e As EventArgs) Handles imbtOtherAction5a.Click
        If pnRespon5b.Visible Then
            pnRespon5b.Visible = False
            pnRespon5c.Visible = False
        Else
            pnRespon5b.Visible = True
            imbtOtherAction5a.Visible = False
            If chkRecognition5.Checked Then tbAction5b.Text = MsgRecognition Else tbAction5b.Text = ""
            tbAction5b.Enabled = Not chkRecognition5.Checked
            chkRecognition5.Enabled = False
        End If
    End Sub
    Protected Sub imbtOtherAction5b_Click(sender As Object, e As EventArgs) Handles imbtOtherAction5b.Click
        If pnRespon5c.Visible Then
            pnRespon5c.Visible = False
        Else
            pnRespon5c.Visible = True
            imbtOtherAction5b.Visible = False
            If chkRecognition5.Checked Then tbAction5c.Text = MsgRecognition Else tbAction5c.Text = ""
            tbAction5c.Enabled = Not chkRecognition5.Checked
        End If
    End Sub
    Protected Sub imbtCloseAction5b_Click(sender As Object, e As EventArgs) Handles imbtCloseAction5b.Click
        chkRecognition5.Enabled = True

        pnRespon5b.Visible = False
        imbtOtherAction5a.Visible = True

        pnRespon5c.Visible = False
        imbtOtherAction5b.Visible = True
    End Sub
    Protected Sub imbtCloseAction5c_Click(sender As Object, e As EventArgs) Handles imbtCloseAction5c.Click
        pnRespon5c.Visible = False
        imbtOtherAction5b.Visible = True
    End Sub


    '//---- Observe TAB #6
    Protected Sub rcbCategory6_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbCategory6.SelectedIndexChanged
        'If IsCategorySubNothing(rcbCategory6.SelectedIndex) Then
        '    rcbFailurePoint6.Items.Clear()
        'End If
        rcbCategorySub6.DataBind()
        rcbFailurePoint6.DataBind()
    End Sub
    Protected Sub chkRecognition6_CheckedChanged(sender As Object, e As EventArgs) Handles chkRecognition6.CheckedChanged
        If chkRecognition6.Checked Then
            tbAction6a.Text = MsgRecognition
            tbAction6a.Enabled = False

            racRespon6a.Enabled = False
            imbtFindRespon6a.Enabled = False

            If pnRespon6b.Visible Then tbAction6b.Text = MsgRecognition : tbAction6b.Enabled = False
            If pnRespon6c.Visible Then tbAction6c.Text = MsgRecognition : tbAction6c.Enabled = False
        Else
            tbAction6a.Text = ""
            tbAction6a.Enabled = True

            racRespon6a.Enabled = True
            imbtFindRespon6a.Enabled = True

            If pnRespon6b.Visible Then tbAction6b.Text = "" : tbAction6b.Enabled = True
            If pnRespon6c.Visible Then tbAction6c.Text = "" : tbAction6c.Enabled = True
        End If
    End Sub
    Protected Sub rcbObserveType6_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles rcbObserveType6.SelectedIndexChanged
        If rcbObserveType6.SelectedIndex = 1 Then rcbContractor6.Visible = True Else rcbContractor6.Visible = False
    End Sub

    '------ upload picture ----
    Protected Sub btUploadImg6_Click(sender As Object, e As EventArgs) Handles btUploadImg6.Click
        Dim fileCount As Integer = RadUpload6.UploadedFiles.Count.ToString
        If fileCount <> 0 And PictureList6.Items.Count < 4 Then
            UploadImage(RadUpload6, 5, PictureList6.Items.Count)    'rtabStrip1.SelectedIndex = 5

            PictureList6.DataBind()
            pnShowImage6.Visible = True
        End If
    End Sub
    Protected Sub imbtClose6_Click(sender As Object, e As ImageClickEventArgs)
        ClearPicture(5, sender)                                     'rtabStrip1.SelectedIndex = 5
        PictureList6.DataBind()
    End Sub

    '------ Propose Action ----
    Protected Sub imbtOtherAction6a_Click(sender As Object, e As EventArgs) Handles imbtOtherAction6a.Click
        If pnRespon6b.Visible Then
            pnRespon6b.Visible = False
            pnRespon6c.Visible = False
        Else
            pnRespon6b.Visible = True
            imbtOtherAction6a.Visible = False
            If chkRecognition6.Checked Then tbAction6b.Text = MsgRecognition Else tbAction6b.Text = ""
            tbAction6b.Enabled = Not chkRecognition6.Checked
            chkRecognition6.Enabled = False
        End If
    End Sub
    Protected Sub imbtOtherAction6b_Click(sender As Object, e As EventArgs) Handles imbtOtherAction6b.Click
        If pnRespon6c.Visible Then
            pnRespon6c.Visible = False
        Else
            pnRespon6c.Visible = True
            imbtOtherAction6b.Visible = False
            If chkRecognition6.Checked Then tbAction6c.Text = MsgRecognition Else tbAction6c.Text = ""
            tbAction6c.Enabled = Not chkRecognition6.Checked
        End If
    End Sub
    Protected Sub imbtCloseAction6b_Click(sender As Object, e As EventArgs) Handles imbtCloseAction6b.Click
        chkRecognition6.Enabled = True

        pnRespon6b.Visible = False
        imbtOtherAction6a.Visible = True

        pnRespon6c.Visible = False
        imbtOtherAction6b.Visible = True
    End Sub
    Protected Sub imbtCloseAction6c_Click(sender As Object, e As EventArgs) Handles imbtCloseAction6c.Click
        pnRespon6c.Visible = False
        imbtOtherAction6b.Visible = True
    End Sub


    '------------------------------------------------------------------------------
    '------------------------------------------------------------------------------
    '
    Protected Sub btSaveAndSend_Click(sender As Object, e As EventArgs) Handles btSaveAndSend.Click
        '-- validate data
        If ValidateData() Then
            '---- save record
            Dim result As Integer = SaveMasterRecObservation()
            If result = 1 Then
                Dim RecId As Integer = CInt(hfRecId.Value)
                Dim ownerId As Integer = CInt(hfOwnerEmpId.Value)
                Dim groupList As New ArrayList

                'Dim groupMail As New cGroupMail(ownerId)
                'groupMail.AddEmailList(RecId, 2001, groupMail.Mail1, 0, groupMail.GroupMailName, False)
                'groupList.Add(groupMail.Mail1)
                'If groupMail.Mail2 <> "" Then groupMail.AddEmailList(RecId, 2001, groupMail.Mail2, 0, groupMail.GroupMailName, False) : groupList.Add(groupMail.Mail2)

                Dim groupMail As New cGroupMail(rcbDepartment.Text)
                groupMail.AddEmailList(RecId, 11, 2021, groupMail.Mail1, 0, groupMail.GroupMailName, True)
                groupList.Add(groupMail.Mail1)
                If groupMail.Mail2 <> "" Then groupMail.AddEmailList(RecId, 12, 2022, groupMail.Mail2, 0, groupMail.GroupMailName, True) : groupList.Add(groupMail.Mail2)
                Dim ownerEmail As String = lbEmail.Text.Trim
                groupMail.AddEmailList(RecId, 0, 1001, ownerEmail, hfOwnerEmpId.Value, tbMyFullname.Text, True)
                groupList.Add(ownerEmail)

                '---- Save Other Employee
                SaveOtherEmployee()
                LockControlMaster()        ' Lock Master Control

                '---- Save Other Employee GroupMail
                'SaveGroupMailMaster(groupList)

                '---- Save detail record
                Dim resultSaveDetail As Integer = SaveDetailObserve(groupList)
                If resultSaveDetail > 0 Then
                    pnSaveAndSend.Visible = False
                    pnSendEmail.Visible = True
                    Dim NoObserve As Integer = CInt(rcbNoObserve.SelectedValue)
                    For i As Integer = 1 To NoObserve
                        LockControlObserve(i)      ' Lock Detail Control
                    Next

                    Dim status As New cStatus
                    status.UpdRecordActive(RecId, True)

                    'confirm Action Number ที่บันทึกจริงกับส่วนของหน้าจอ
                    lbActionNum.Text = Session("actualrecActNo")
                    lbActionNumberComplete.Text = Session("actualrecActNo")
                End If
            Else
                MsgBoxRad("<b>Save error, Please contact your administrator.</b>", 240, 76)
            End If
        End If
    End Sub

    Private Function SaveMasterRecObservation() As Integer
        Dim updStr As String = "UPDATE tblRecord SET tempFlag = @tempFlag, tempLock = @tempLock, timestamp = @timestamp, departId = @departId, recActNo = @recActNo, 
                                recActNoValue = @recActNoValue, recActMonth = @recActMonth, recActYear = @recActYear, recActDate = @recActDate, recActTime = @recActTime, 
                                durationH = @durationH, durationM = @durationM, durationValue = @durationValue, empId = @empId, oEmpCount = @oEmpCount, 
                                noObserve = @noObserve, IsComplete = @IsComplete WHERE recId = @recId"
        '-- Update Command
        Dim conn As New SqlConnection(ConnStr)
        Dim command As New SqlCommand(updStr, conn)

        'chk repeat Action No. and Lock Action No.
        Dim AutoNumber As New aAutoNumber
        Dim actualNumbervalue As Integer = AutoNumber.ActNumberAutoNum(rcbDepartment.Text, rdpDocDate.SelectedDate)
        Dim actualNumber As String = rcbDepartment.Text & actualNumbervalue.ToString
        Session("actualrecActNo") = actualNumber

        'check Recognition
        Dim NoObserve As Integer = CInt(rcbNoObserve.SelectedValue)
        Dim IsAllRecognition As Boolean = True
        For i As Integer = 1 To NoObserve
            Dim chkRecog As CheckBox = RadMultiPage1.FindControl("chkRecognition" & i.ToString)
            IsAllRecognition = IsAllRecognition And chkRecog.Checked
        Next
        Dim IsCompleteStatus As Integer = 1002
        If IsAllRecognition Then IsCompleteStatus = 1001

        command.Parameters.Add("@recId", SqlDbType.Int).Value = CInt(hfRecId.Value)

        command.Parameters.Add("@tempFlag", SqlDbType.Bit).Value = False
        command.Parameters.Add("@tempLock", SqlDbType.DateTime).Value = Now()
        command.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = Now()
        command.Parameters.Add("@departId", SqlDbType.Int).Value = CInt(rcbDepartment.SelectedValue)
        command.Parameters.Add("@recActNo", SqlDbType.NVarChar).Value = actualNumber
        command.Parameters.Add("@recActNoValue", SqlDbType.Int).Value = actualNumbervalue
        command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = CDate(rdpDocDate.SelectedDate).Month
        command.Parameters.Add("@recActYear", SqlDbType.Int).Value = CDate(rdpDocDate.SelectedDate).Year
        command.Parameters.Add("@recActDate", SqlDbType.Date).Value = rdpDocDate.SelectedDate
        command.Parameters.Add("@recActTime", SqlDbType.Time).Value = rcbTimeHH.SelectedValue & ":" & rcbTimeMM.SelectedValue
        command.Parameters.Add("@durationH", SqlDbType.Int).Value = CInt(rcbDurationH.SelectedValue)
        command.Parameters.Add("@durationM", SqlDbType.Int).Value = CInt(rcbDurationM.SelectedValue)
        command.Parameters.Add("@durationValue", SqlDbType.Int).Value = (60 * CInt(rcbDurationH.SelectedValue)) + CInt(rcbDurationM.SelectedValue)
        command.Parameters.Add("@empId", SqlDbType.Int).Value = CInt(hfOwnerEmpId.Value)
        command.Parameters.Add("@oEmpCount", SqlDbType.Int).Value = racObservBox.Entries.Count
        command.Parameters.Add("@noObserve", SqlDbType.Int).Value = NoObserve
        command.Parameters.Add("@IsComplete", SqlDbType.Int).Value = IsCompleteStatus

        conn.Open()
        Dim result As Integer = command.ExecuteNonQuery()
        conn.Close()

        Return result
    End Function

    Private Sub SaveOtherEmployee()
        If racObservBox.Entries.Count > 0 Then
            '-- re-index othObs (fix bug 8/8/2017 user ป้อน OthObs ไม่เป็นลำดับ)
            Dim selSql As String = "SELECT Id FROM tblRecordOthEmp WHERE recId = @recId ORDER BY Id"
            Dim updSql As String = "UPDATE tblRecordOthEmp SET recItem = @recItem WHERE Id = @Id"

            Dim selConn As New SqlConnection(ConnStr)
            selConn.Open()
            Using commandSel As New SqlCommand(selSql, selConn) With {.CommandType = CommandType.Text}
                commandSel.Parameters.Add("@recId", SqlDbType.Int).Value = Cint(hfRecId.Value)

                Dim updConn As New SqlConnection(ConnStr)
                updConn.Open()
                Using commandUpd As New SqlCommand(updSql, updConn) With {.CommandType = CommandType.Text}
                    Dim para_IdOri As SqlParameter = commandUpd.Parameters.Add("@Id", SqlDbType.Int)              'WHERE
                    Dim para_Item As SqlParameter = commandUpd.Parameters.Add("@recItem", SqlDbType.int)          'VALUE

                    Dim idxCount As Integer = 1
                    Dim DataRead As SqlDataReader
                    DataRead = commandSel.ExecuteReader()
                    While DataRead.Read()
                        para_IdOri.Value = CInt(DataRead("Id"))
                        para_Item.Value = idxCount
                        idxCount = idxCount + 1
                        commandUpd.ExecuteNonQuery()
                    End While
                End Using
                updConn.Close()
            End Using
            selConn.Close()

            '-- insert tblRecordOthEmpO from tblRecordOthEmp
            Dim connection As SqlConnection = New SqlConnection(ConnStr)
            Dim command As New SqlCommand
            connection.Open()
            command.Connection = connection
            command.CommandType = CommandType.StoredProcedure
            command.CommandText = "recordOtherEmployeeO"
            Dim paramRecordId As SqlParameter = New SqlParameter("@RecordId", hfRecId.Value) With {.Direction = ParameterDirection.Input, .DbType = DbType.Int32}
            command.Parameters.Add(paramRecordId)
            Dim result As Integer = command.ExecuteNonQuery()
            connection.Close()
        End If
    End Sub

    Private Sub SaveGroupMailMaster(ByRef groupList As ArrayList)
        Dim RecId As Integer = CInt(hfRecId.Value)
        Dim selConn As New SqlConnection(ConnStr)
        Dim selSql As String = "SELECT emailType, email, empId FROM tblSendEmail WHERE (recId = @recId) AND (emailType < 2000)"

        selConn.Open()
        Using commandSel As New SqlCommand(selSql, selConn) With {.CommandType = CommandType.Text}
            commandSel.Parameters.Add("@recId", SqlDbType.Int).Value = RecId

            Dim DataRead As SqlDataReader
            DataRead = commandSel.ExecuteReader()
            While DataRead.Read()
                Dim othEmpId As Integer = CInt(DataRead("empId"))
                Dim type As Integer = CInt(DataRead("emailType"))
                Dim IsRepeat As Boolean

                Dim groupMail As New cGroupMail(othEmpId)
                If groupMail.Mail1 <> "" Then
                    IsRepeat = False
                    For Each m As String In groupList
                        If groupMail.Mail1 = m Then IsRepeat = True
                    Next
                    If Not IsRepeat Then
                        groupList.Add(groupMail.Mail1)
                        groupMail.AddEmailList(RecId, 11, type + 1000, groupMail.Mail1, 0, groupMail.GroupMailName, False)
                    End If
                End If

                If groupMail.Mail2 <> "" Then
                    IsRepeat = False
                    For Each m As String In groupList
                        If groupMail.Mail2 = m Then IsRepeat = True
                    Next
                    If Not IsRepeat Then
                        groupList.Add(groupMail.Mail2)
                        groupMail.AddEmailList(RecId, 12, type + 1000, groupMail.Mail2, 0, groupMail.GroupMailName, False)
                    End If
                End If

                groupList.Add(DataRead("email"))  'email employee
            End While
        End Using
        selConn.Close()
    End Sub

    Private Function ValidateData() As Boolean
        'clear infobox
        infobox.Items.Clear()
        Dim IsValid As Boolean = True

        'validate master action number
        '-- Observed Department
        If rcbDepartment.SelectedIndex = 0 Then
            IsValid = False
            AddListBox("Observed Department is blank, please select Observed Department.", 1)
        End If

        '-- force select time
        If rcbTimeHH.SelectedValue = "x" Or rcbTimeMM.SelectedValue = "x" Then
            IsValid = False
            AddListBox("Time is blank, please check Time.", 1)
        End If

        '-- duration
        Dim durationValue As Integer = (60 * CInt(rcbDurationH.SelectedValue)) + CInt(rcbDurationM.SelectedValue)
        If durationValue = 0 Then
            IsValid = False
            AddListBox("Duration = 0, please check Duration.", 1)
        End If

        If Not IsValid Then Return IsValid

        'validate detail (observe) action number
        Dim observeCount As Integer = rcbNoObserve.SelectedValue
        For i = 1 To observeCount
            '-- title
            Dim tbTitle As TextBox = RadMultiPage1.FindControl("tbTitle" & i.ToString)
            If tbTitle.Text = "" Then IsValid = False : AddListBox("Observe " & i.ToString & ": please check Title.", 1)

            '-- category
            Dim rcbCate As RadComboBox = RadMultiPage1.FindControl("rcbCategory" & i.ToString)
            If rcbCate.SelectedIndex = 0 Then IsValid = False : AddListBox("Observe " & i.ToString & ": please check Category.", 1)
            Dim rcbCateSub As RadComboBox = RadMultiPage1.FindControl("rcbCategorySub" & i.ToString)
            If rcbCateSub.SelectedIndex = 0 Or rcbCateSub.SelectedIndex = -1 Then IsValid = False : AddListBox("Observe " & i.ToString & ": please check Sub Category.", 1)
            Dim rcbFailPoint As RadComboBox = RadMultiPage1.FindControl("rcbFailurePoint" & i.ToString)
            If rcbFailPoint.SelectedIndex = 0 Or rcbFailPoint.SelectedIndex = -1 Then IsValid = False : AddListBox("Observe " & i.ToString & ": please check Failure Point.", 1)
            'If rcbCateSub.Text = "" Then IsValid = False : AddListBox("Observe " & i.ToString & ": please check Sub Category.", 1)
            'If rcbFailPoint.Text = "" Then IsValid = False : AddListBox("Observe " & i.ToString & ": please check Failure Point.", 1)

            '-- equipment
            'Dim tbEquipment As TextBox = RadMultiPage1.FindControl("tbEquipment" & i.ToString)
            'If tbEquipment.Text = "" Then IsValid = False : AddListBox("Observe " & i.ToString & ": please check Equipment.", 1)

            '-- description
            Dim tbDescription As TextBox = RadMultiPage1.FindControl("tbDescription" & i.ToString)
            If tbDescription.Text = "" Then IsValid = False : AddListBox("Observe " & i.ToString & ": please check Description.", 1)

            '-- propost action
            Dim chkRecognition As CheckBox = RadMultiPage1.FindControl("chkRecognition" & i.ToString)

            '---- chk Propost Action #1
            Dim tbActionA As TextBox = RadMultiPage1.FindControl("tbAction" & i.ToString & "a")
            If tbActionA.Text = "" Then IsValid = False : AddListBox("Observe " & i.ToString & ": please check Propose Action #1.", 1)
            Dim racResponA As RadAutoCompleteBox = RadMultiPage1.FindControl("racRespon" & i.ToString & "a")
            If racResponA.Entries.Count = 0 And Not chkRecognition.Checked Then
                IsValid = False
                AddListBox("Observe " & i.ToString & ": please check Responsible Person #1.", 1)
            End If

            '---- chk Propost Action #2
            Dim PanelB As Panel = RadMultiPage1.FindControl("pnRespon" & i.ToString & "b")
            If PanelB.Visible Then
                Dim tbActionB As TextBox = RadMultiPage1.FindControl("tbAction" & i.ToString & "b")
                If tbActionB.Text = "" Then IsValid = False : AddListBox("Observe " & i.ToString & ": please check Propose Action #2.", 1)
                Dim racResponB As RadAutoCompleteBox = RadMultiPage1.FindControl("racRespon" & i.ToString & "b")
                If racResponB.Entries.Count = 0 And Not chkRecognition.Checked Then
                    IsValid = False
                    AddListBox("Observe " & i.ToString & ": please check Responsible Person #2.", 1)
                End If
            End If

            '---- chk Propost Action #3
            Dim PanelC As Panel = RadMultiPage1.FindControl("pnRespon" & i.ToString & "c")
            If PanelC.Visible Then
                Dim tbActionC As TextBox = RadMultiPage1.FindControl("tbAction" & i.ToString & "c")
                If tbActionC.Text = "" Then IsValid = False : AddListBox("Observe " & i.ToString & ": please check Propose Action #3.", 1)
                Dim racResponC As RadAutoCompleteBox = RadMultiPage1.FindControl("racRespon" & i.ToString & "c")
                If racResponC.Entries.Count = 0 And Not chkRecognition.Checked Then
                    IsValid = False
                    AddListBox("Observe " & i.ToString & ": please check Responsible Person #3.", 1)
                End If
            End If
        Next

        Return IsValid
    End Function

    Private Sub AddListBox(ByVal msg As String, ByVal fontcolor As Integer)
        If msg IsNot Nothing And msg <> "" Then
            If infobox.Items.Count = 0 Then
                infobox.Items.Add("Validate Info:")
            End If
            msg = msg + Environment.NewLine
            Dim MaxLine As Integer = 7
            If infobox.Items.Count = MaxLine Then
                infobox.Items.RemoveAt(1)
            End If
            infobox.Items.Add(msg)
            Session("fontcolor") = fontcolor
            infoboxCssStyle()
        End If
    End Sub

    Private Sub infoboxCssStyle()
        For i = 0 To infobox.Items.Count - 1
            If i = 0 Or Session("fontcolor") = 0 Then
                infobox.Items(0).Attributes.CssStyle.Add("font-weight", "bold")
                infobox.Items(0).Attributes.CssStyle.Add("color", "#696969")
            Else
                infobox.Items(i).Attributes.CssStyle.Add("font-weight", "normal")
                If Session("fontcolor") = 0 Then
                    infobox.Items(i).Attributes.CssStyle.Add("color", "#696969")
                Else
                    infobox.Items(i).Attributes.CssStyle.Add("color", "red")
                End If
            End If
        Next
    End Sub

    Private Function SaveDetailObserve(ByRef groupList As ArrayList) As Integer
        Dim msg As String = ""
        Dim resultCount As Integer = 0
        Dim observeCount As Integer = rcbNoObserve.SelectedValue
        For i = 1 To observeCount
            Dim tbTitle As TextBox = RadMultiPage1.FindControl("tbTitle" & i.ToString)
            Dim rcbCate As RadComboBox = RadMultiPage1.FindControl("rcbCategory" & i.ToString)
            Dim rcbCateSub As RadComboBox = RadMultiPage1.FindControl("rcbCategorySub" & i.ToString)
            Dim rcbFailurePoint As RadComboBox = RadMultiPage1.FindControl("rcbFailurePoint" & i.ToString)
            Dim tbEquipment As TextBox = RadMultiPage1.FindControl("tbEquipment" & i.ToString)
            Dim rcbLocation As RadComboBox = RadMultiPage1.FindControl("RCBLocation" & i.ToString)
            Dim chkHRO As CheckBox = RadMultiPage1.FindControl("chkHRO" & i.ToString)
            Dim chkHRO_op1 As CheckBox = RadMultiPage1.FindControl("chkHRO" & i.ToString & "op1")
            Dim chkHRO_op2 As CheckBox = RadMultiPage1.FindControl("chkHRO" & i.ToString & "op2")
            Dim chkHRO_op3 As CheckBox = RadMultiPage1.FindControl("chkHRO" & i.ToString & "op3")
            Dim chkHRO_op4 As CheckBox = RadMultiPage1.FindControl("chkHRO" & i.ToString & "op4")
            Dim chkHRO_op5 As CheckBox = RadMultiPage1.FindControl("chkHRO" & i.ToString & "op5")
            Dim chk2Eye As CheckBox = RadMultiPage1.FindControl("chk2Eye" & i.ToString)
            Dim chkRecognition As CheckBox = RadMultiPage1.FindControl("chkRecognition" & i.ToString)
            Dim rcbObserveType As RadComboBox = RadMultiPage1.FindControl("rcbObserveType" & i.ToString)
            Dim contractorId As Integer = 0
            If rcbObserveType.SelectedValue = "1" Then
                Dim rcbContractor As RadComboBox = RadMultiPage1.FindControl("rcbContractor" & i.ToString)
                contractorId = CInt(rcbContractor.SelectedValue)
            End If
            Dim PictureList As DataList = RadMultiPage1.FindControl("PictureList" & i.ToString)
            Dim tbDescription As TextBox = RadMultiPage1.FindControl("tbDescription" & i.ToString)
            Dim racResponA As RadAutoCompleteBox = RadMultiPage1.FindControl("racRespon" & i.ToString & "a")
            Dim tbActionA As TextBox = RadMultiPage1.FindControl("tbAction" & i.ToString & "a")
            Dim PanelB As Panel = RadMultiPage1.FindControl("pnRespon" & i.ToString & "b")
            Dim racResponB As RadAutoCompleteBox = RadMultiPage1.FindControl("racRespon" & i.ToString & "b")
            Dim tbActionB As TextBox = RadMultiPage1.FindControl("tbAction" & i.ToString & "b")
            Dim PanelC As Panel = RadMultiPage1.FindControl("pnRespon" & i.ToString & "c")
            Dim racResponC As RadAutoCompleteBox = RadMultiPage1.FindControl("racRespon" & i.ToString & "c")
            Dim tbActionC As TextBox = RadMultiPage1.FindControl("tbAction" & i.ToString & "c")

            Dim strIns As String = "INSERT INTO tblRecordDetail(recId, observItem, title, category, categorySub, failurePoint, equipment, IsHRO, hroChk1, hroChk2, hroChk3, hroChk4, hroChk5, 
                                    secondEye, recognition, observType, contractor, pictureCount, description, proposeEnable_A, proposeDesc_A, proposeRespPerson_A, proposeAction_A, proposeStatus_A, 
                                    proposeEnable_B, proposeDesc_B, proposeRespPerson_B, proposeAction_B, proposeStatus_B, proposeEnable_C, proposeDesc_C, proposeRespPerson_C, proposeAction_C, proposeStatus_C, observComplete) 
                                    VALUES(@recId, @observItem, @title, @category, @categorySub, @failurePoint, @equipment, @IsHRO, @hroChk1, @hroChk2, @hroChk3, @hroChk4, @hroChk5, 
                                    @secondEye, @recognition, @observType, @contractor, @pictureCount, @description, @proposeEnable_A, @proposeDesc_A, @proposeRespPerson_A, @proposeAction_A, @proposeStatus_A, 
                                    @proposeEnable_B, @proposeDesc_B, @proposeRespPerson_B, @proposeAction_B, @proposeStatus_B, @proposeEnable_C, @proposeDesc_C, @proposeRespPerson_C, @proposeAction_C, @proposeStatus_C, @observComplete)"
            '-- Insert Command
            Dim conn As New SqlConnection(ConnStr)
            Dim command As New SqlCommand(strIns, conn)
            conn.Open()
            command.Parameters.Add("@recId", SqlDbType.Int).Value = CInt(hfRecId.Value)
            command.Parameters.Add("@observItem", SqlDbType.Int).Value = i
            command.Parameters.Add("@title", SqlDbType.NVarChar).Value = tbTitle.Text
            command.Parameters.Add("@category", SqlDbType.Int).Value = CInt(rcbCate.SelectedValue)
            If rcbCateSub.SelectedValue <> "" Then command.Parameters.Add("@categorySub", SqlDbType.Int).Value = CInt(rcbCateSub.SelectedValue) _
                Else command.Parameters.Add("@categorySub", SqlDbType.Int).Value = DBNull.Value
            If rcbFailurePoint.SelectedValue <> "" Then command.Parameters.Add("@failurePoint", SqlDbType.Int).Value = CInt(rcbFailurePoint.SelectedValue) _
                Else command.Parameters.Add("@failurePoint", SqlDbType.Int).Value = DBNull.Value
            command.Parameters.Add("@equipment", SqlDbType.NVarChar).Value = rcbLocation.Text & "|" & tbEquipment.Text
            command.Parameters.Add("@IsHRO", SqlDbType.Bit).Value = chkHRO.Checked
            command.Parameters.Add("@hroChk1", SqlDbType.Bit).Value = chkHRO_op1.Checked
            command.Parameters.Add("@hroChk2", SqlDbType.Bit).Value = chkHRO_op2.Checked
            command.Parameters.Add("@hroChk3", SqlDbType.Bit).Value = chkHRO_op3.Checked
            command.Parameters.Add("@hroChk4", SqlDbType.Bit).Value = chkHRO_op4.Checked
            command.Parameters.Add("@hroChk5", SqlDbType.Bit).Value = chkHRO_op5.Checked
            command.Parameters.Add("@secondEye", SqlDbType.Bit).Value = chk2Eye.Checked
            command.Parameters.Add("@recognition", SqlDbType.Bit).Value = chkRecognition.Checked
            command.Parameters.Add("@observType", SqlDbType.Int).Value = CInt(rcbObserveType.SelectedValue)
            command.Parameters.Add("@contractor", SqlDbType.Int).Value = contractorId
            command.Parameters.Add("@pictureCount", SqlDbType.Int).Value = PictureList.Items.Count
            command.Parameters.Add("@description", SqlDbType.NVarChar).Value = tbDescription.Text

            command.Parameters.Add("@proposeEnable_A", SqlDbType.Bit).Value = True
            command.Parameters.Add("@proposeDesc_A", SqlDbType.NVarChar).Value = ""
            command.Parameters.Add("@proposeAction_A", SqlDbType.NVarChar).Value = tbActionA.Text

            Dim reponPersonA As Integer = 0
            Dim Status_A As Integer = 1000      '1001 = Recognition, 1002 = Inprogress
            If chkRecognition.Checked Then
                Status_A = 1001
            Else
                Status_A = 1002
                reponPersonA = CInt(racResponA.Entries.Item(0).Value)

                '---- email Responsible..
                Dim groupMailA As New cGroupMail(reponPersonA, True)
                groupMailA.AddEmailList(CInt(hfRecId.Value), i, 1000 + (i * 100) + 1, groupMailA.EmpEmail, reponPersonA, groupMailA.EmpFullName, True)
                groupList.Add(groupMailA.EmpEmail)
            End If
            command.Parameters.Add("@proposeStatus_A", SqlDbType.Int).Value = Status_A
            command.Parameters.Add("@proposeRespPerson_A", SqlDbType.Int).Value = reponPersonA

            '---- email & group email list
            'SaveGroupMailDetailPart(groupList, reponPersonA, 1000 + (i * 10) + 1)
            'Dim groupMail As New cGroupMail(reponPersonA)
            'groupMail.AddEmailList(CInt(hfRecId.Value), 3000 + (i * 10) + 1, groupMail.Mail1, 0, groupMail.GroupMailName, False)
            'If groupMail.Mail2 <> "" Then groupMail.AddEmailList(CInt(hfRecId.Value), 3000 + (i * 10) + 1, groupMail.Mail2, 0, groupMail.GroupMailName, False)

            Dim Status_B As Integer = 1000      '1001 = Recognition, 1002 = Inprogress
            If PanelB.Visible Then
                command.Parameters.Add("@proposeEnable_B", SqlDbType.Bit).Value = True
                command.Parameters.Add("@proposeDesc_B", SqlDbType.NVarChar).Value = ""
                command.Parameters.Add("@proposeAction_B", SqlDbType.NVarChar).Value = tbActionB.Text

                Dim reponPersonB As Integer = 0
                If chkRecognition.Checked Then
                    Status_B = 1001
                Else
                    Status_B = 1002
                    reponPersonB = CInt(racResponB.Entries.Item(0).Value)

                    '---- email Responsible..
                    Dim groupMailB As New cGroupMail(reponPersonB, True)
                    groupMailB.AddEmailList(CInt(hfRecId.Value), i, 1000 + (i * 100) + 2, groupMailB.EmpEmail, reponPersonB, groupMailB.EmpFullName, True)
                    groupList.Add(groupMailB.EmpEmail)
                End If
                command.Parameters.Add("@proposeStatus_B", SqlDbType.Int).Value = Status_B
                command.Parameters.Add("@proposeRespPerson_B", SqlDbType.Int).Value = reponPersonB

                '---- email & group email list
                'SaveGroupMailDetailPart(groupList, reponPersonB, 1000 + (i * 10) + 2)
                'Dim groupMailB As New cGroupMail(reponPersonB)
                'groupMailB.AddEmailList(CInt(hfRecId.Value), 3000 + (i * 10) + 2, groupMailB.Mail1, 0, groupMailB.GroupMailName, False)
                'If groupMailB.Mail2 <> "" Then groupMailB.AddEmailList(CInt(hfRecId.Value), 3000 + (i * 10) + 2, groupMailB.Mail2, 0, groupMailB.GroupMailName, False)
            Else
                command.Parameters.Add("@proposeEnable_B", SqlDbType.Bit).Value = False
                command.Parameters.Add("@proposeDesc_B", SqlDbType.NVarChar).Value = DBNull.Value
                command.Parameters.Add("@proposeRespPerson_B", SqlDbType.Int).Value = DBNull.Value
                command.Parameters.Add("@proposeAction_B", SqlDbType.NVarChar).Value = DBNull.Value
                command.Parameters.Add("@proposeStatus_B", SqlDbType.Int).Value = Status_B
            End If

            Dim Status_C As Integer = 1000      '1001 = Recognition, 1002 = Inprogress
            If PanelC.Visible Then
                command.Parameters.Add("@proposeEnable_C", SqlDbType.Bit).Value = True
                command.Parameters.Add("@proposeDesc_C", SqlDbType.NVarChar).Value = ""
                command.Parameters.Add("@proposeAction_C", SqlDbType.NVarChar).Value = tbActionC.Text

                Dim reponPersonC As Integer = 0
                If chkRecognition.Checked Then
                    Status_C = 1001
                Else
                    Status_C = 1002
                    reponPersonC = CInt(racResponC.Entries.Item(0).Value)

                    '---- email Responsible..
                    Dim groupMailC As New cGroupMail(reponPersonC, True)
                    groupMailC.AddEmailList(CInt(hfRecId.Value), i, 1000 + (i * 100) + 3, groupMailC.EmpEmail, reponPersonC, groupMailC.EmpFullName, True)
                    groupList.Add(groupMailC.EmpEmail)
                End If
                command.Parameters.Add("@proposeStatus_C", SqlDbType.Int).Value = Status_C
                command.Parameters.Add("@proposeRespPerson_C", SqlDbType.Int).Value = reponPersonC

                '---- email & group email list
                'SaveGroupMailDetailPart(groupList, reponPersonC, 1000 + (i * 10) + 3)
                'Dim groupMailC As New cGroupMail(reponPersonC)
                'groupMailC.AddEmailList(CInt(hfRecId.Value), 3000 + (i * 10) + 3, groupMailC.Mail1, 0, groupMailC.GroupMailName, False)
                'If groupMailC.Mail2 <> "" Then groupMailC.AddEmailList(CInt(hfRecId.Value), 3000 + (i * 10) + 3, groupMailC.Mail2, 0, groupMailC.GroupMailName, False)
            Else
                command.Parameters.Add("@proposeEnable_C", SqlDbType.Bit).Value = False
                command.Parameters.Add("@proposeDesc_C", SqlDbType.NVarChar).Value = DBNull.Value
                command.Parameters.Add("@proposeRespPerson_C", SqlDbType.Int).Value = DBNull.Value
                command.Parameters.Add("@proposeAction_C", SqlDbType.NVarChar).Value = DBNull.Value
                command.Parameters.Add("@proposeStatus_C", SqlDbType.Int).Value = Status_C
            End If

            '-- update observComplete status
            Dim Status As New cStatus
            command.Parameters.Add("@observComplete", SqlDbType.Int).Value = Status.observStatus(Status_A, PanelB.Visible, Status_B, PanelC.Visible, Status_C)

            Dim result As Integer = command.ExecuteNonQuery()
            conn.Close()

            If result = 1 Then
                If PictureList.Items.Count > 0 Then
                    '-- insert picture
                    saveObservPicture(CInt(hfRecId.Value), i - 1)
                End If
                resultCount = resultCount + 1
            End If
        Next

        Return resultCount
    End Function

    Private Sub SaveGroupMailDetailPart(ByRef groupList As ArrayList, ByVal empId As Integer, ByVal type As Integer)
        Dim groupMail As New cGroupMail(empId)
        Dim IsRepeat As Boolean = False
        For Each m As String In groupList
            If groupMail.EmpEmail = m Then IsRepeat = True
        Next
        If Not IsRepeat Then
            groupList.Add(groupMail.EmpEmail)
            groupMail.AddEmailList(CInt(hfRecId.Value), 0, type, groupMail.EmpEmail, empId, groupMail.EmpFullName, False)
        End If

        If groupMail.Mail1 <> "" Then
            IsRepeat = False
            For Each m As String In groupList
                If groupMail.Mail1 = m Then IsRepeat = True
            Next
            If Not IsRepeat Then
                groupList.Add(groupMail.Mail1)
                groupMail.AddEmailList(CInt(hfRecId.Value), 11, type + 1000, groupMail.Mail1, 0, groupMail.GroupMailName, False)
            End If
        End If

        If groupMail.Mail2 <> "" Then
            IsRepeat = False
            For Each m As String In groupList
                If groupMail.Mail2 = m Then IsRepeat = True
            Next
            If Not IsRepeat Then
                groupList.Add(groupMail.Mail2)
                groupMail.AddEmailList(CInt(hfRecId.Value), 12, type + 1000, groupMail.Mail2, 0, groupMail.GroupMailName, False)
            End If
        End If
    End Sub

    Private Sub saveObservPicture(ByVal recId As Integer, ByVal observeItem As Integer)
        Dim connection As SqlConnection = New SqlConnection(ConnStr)
        Dim command As New SqlCommand
        connection.Open()
        command.Connection = connection
        command.CommandType = CommandType.StoredProcedure
        command.CommandText = "recordPictureFailureO"
        Dim paramRecordId As SqlParameter = New SqlParameter("@RecordId", hfRecId.Value) With {.Direction = ParameterDirection.Input, .DbType = DbType.Int32}
        Dim paramObservItem As SqlParameter = New SqlParameter("@ObservItem", observeItem) With {.Direction = ParameterDirection.Input, .DbType = DbType.Int32}
        command.Parameters.Add(paramRecordId)
        command.Parameters.Add(paramObservItem)
        Dim result As Integer = command.ExecuteNonQuery()
        connection.Close()
    End Sub

    Private Sub LockControlMaster()
        rcbDepartment.Enabled = False
        rdpDocDate.Enabled = False
        rcbTimeHH.Enabled = False
        rcbTimeMM.Enabled = False
        rcbDurationH.Enabled = False
        rcbDurationM.Enabled = False
        tbMyFullname.Enabled = False
        racObservBox.Enabled = False
        imbtFindObserv.Enabled = False
        rcbNoObserve.Enabled = False
    End Sub
    Private Sub LockControlObserve(ByVal Observe As Integer)
        Dim tbTitle As TextBox = RadMultiPage1.FindControl("tbTitle" & Observe.ToString)
        Dim rcbCate As RadComboBox = RadMultiPage1.FindControl("rcbCategory" & Observe.ToString)
        Dim rcbCateSub As RadComboBox = RadMultiPage1.FindControl("rcbCategorySub" & Observe.ToString)
        Dim rcbFailurePoint As RadComboBox = RadMultiPage1.FindControl("rcbFailurePoint" & Observe.ToString)
        Dim tbEquipment As TextBox = RadMultiPage1.FindControl("tbEquipment" & Observe.ToString)
        Dim chkHRO As CheckBox = RadMultiPage1.FindControl("chkHRO" & Observe.ToString)
        Dim chkHRO_op1 As CheckBox = RadMultiPage1.FindControl("chkHRO" & Observe.ToString & "op1")
        Dim chkHRO_op2 As CheckBox = RadMultiPage1.FindControl("chkHRO" & Observe.ToString & "op2")
        Dim chkHRO_op3 As CheckBox = RadMultiPage1.FindControl("chkHRO" & Observe.ToString & "op3")
        Dim chkHRO_op4 As CheckBox = RadMultiPage1.FindControl("chkHRO" & Observe.ToString & "op4")
        Dim chkHRO_op5 As CheckBox = RadMultiPage1.FindControl("chkHRO" & Observe.ToString & "op5")
        Dim chk2Eye As CheckBox = RadMultiPage1.FindControl("chk2Eye" & Observe.ToString)
        Dim chkRecognition As CheckBox = RadMultiPage1.FindControl("chkRecognition" & Observe.ToString)
        Dim chkSendEmail As CheckBox = RadMultiPage1.FindControl("chkSendEmail" & Observe.ToString)
        Dim rcbObserveType As RadComboBox = RadMultiPage1.FindControl("rcbObserveType" & Observe.ToString)
        If rcbObserveType.SelectedValue = "1" Then
            Dim rcbContractor As RadComboBox = RadMultiPage1.FindControl("rcbContractor" & Observe.ToString)
            rcbContractor.Enabled = False
        End If
        Dim RadUpload As RadAsyncUpload = RadMultiPage1.FindControl("RadUpload" & Observe.ToString)
        Dim btUploadImg As Button = RadMultiPage1.FindControl("btUploadImg" & Observe.ToString)
        Dim tbDescription As TextBox = RadMultiPage1.FindControl("tbDescription" & Observe.ToString)

        tbTitle.Enabled = False
        rcbCate.Enabled = False
        rcbCateSub.Enabled = False
        rcbFailurePoint.Enabled = False
        tbEquipment.Enabled = False
        chkHRO.Enabled = False
        chkHRO_op1.Enabled = False
        chkHRO_op2.Enabled = False
        chkHRO_op3.Enabled = False
        chkHRO_op4.Enabled = False
        chkHRO_op5.Enabled = False
        chk2Eye.Enabled = False
        chkRecognition.Enabled = False
        chkSendEmail.Enabled = False
        rcbObserveType.Enabled = False
        RadUpload.Enabled = False
        btUploadImg.Enabled = False
        tbDescription.Enabled = False

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "lockControl", "lockControl();", True)

        'Dim tbActionA As TextBox = RadMultiPage1.FindControl("tbAction" & Observe.ToString & "a")
        'Dim racResponA As RadAutoCompleteBox = RadMultiPage1.FindControl("racRespon" & Observe.ToString & "a")
        'Dim imbtOtherActionA As Button = RadMultiPage1.FindControl("imbtOtherAction" & Observe.ToString & "a")
        'Dim imbtFindResponA As ImageButton = RadMultiPage1.FindControl("imbtFindRespon" & Observe.ToString & "a")
        'tbActionA.Enabled = False
        'racResponA.Enabled = False
        'imbtOtherActionA.Enabled = False
        'imbtFindResponA.Enabled = False

        'Dim PanelB As Panel = RadMultiPage1.FindControl("pnRespon" & Observe.ToString & "b")
        'If PanelB.Visible Then
        '    Dim tbActionB As TextBox = RadMultiPage1.FindControl("tbAction" & Observe.ToString & "b")
        '    Dim racResponB As RadAutoCompleteBox = RadMultiPage1.FindControl("racRespon" & Observe.ToString & "b")
        '    Dim imbtCloseActionB As Button = RadMultiPage1.FindControl("imbtCloseAction" & Observe.ToString & "b")
        '    Dim imbtOtherActionB As Button = RadMultiPage1.FindControl("imbtOtherAction" & Observe.ToString & "b")
        '    Dim imbtFindResponB As ImageButton = RadMultiPage1.FindControl("imbtFindRespon" & Observe.ToString & "b")
        '    tbActionB.Enabled = False
        '    racResponB.Enabled = False
        '    imbtCloseActionB.Enabled = False
        '    imbtOtherActionB.Enabled = False
        '    imbtFindResponB.Enabled = False
        'End If

        'Dim PanelC As Panel = RadMultiPage1.FindControl("pnRespon" & Observe.ToString & "c")
        'If PanelC.Visible Then
        '    Dim tbActionC As TextBox = RadMultiPage1.FindControl("tbAction" & Observe.ToString & "c")
        '    Dim racResponC As RadAutoCompleteBox = RadMultiPage1.FindControl("racRespon" & Observe.ToString & "c")
        '    Dim imbtCloseActionC As Button = RadMultiPage1.FindControl("imbtCloseAction" & Observe.ToString & "c")
        '    Dim imbtFindResponC As ImageButton = RadMultiPage1.FindControl("imbtFindRespon" & Observe.ToString & "c")
        '    tbActionC.Enabled = False
        '    racResponC.Enabled = False
        '    imbtCloseActionC.Enabled = False
        '    imbtFindResponC.Enabled = False
        'End If
    End Sub

    Protected Sub btSendEmail_Click(sender As Object, e As EventArgs) Handles btSendEmail.Click
        'SendEmail
        Dim FailResult As Integer = 0
        SendEmailEachObserve()
        Response.Redirect("observationList.aspx")
    End Sub


    '------------------------------------------------------------------------------
    ' Send Email
    '------------------------------------------------------------------------------
    '
    Protected Sub chkSendEachObserve_CheckedChanged(sender As Object, e As EventArgs) Handles chkSendEachObserve.CheckedChanged
        pnSendEachObserve.Visible = chkSendEachObserve.Checked
    End Sub

    Private Sub rgEmailListEachOb_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles rgEmailListEachOb.ItemDataBound
        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

            Dim ObserveNo As Short = CShort(DataBinder.Eval(e.Item.DataItem, "observItem"))
            Dim lbObserveNo As Label = item.FindControl("lbObserveNo")
            lbObserveNo.Text = "OBSERVE " & ObserveNo.ToString

            Dim ObserveNoIsSend As CheckBox = RadMultiPage1.FindControl("chkSendEmail" & ObserveNo.ToString)
            Dim IsSuggestChk As CheckBox = item.FindControl("chkSelectSend")
            IsSuggestChk.Checked = ObserveNoIsSend.Checked
            If Not IsSuggestChk.Checked Then
                IsSuggestChk.Enabled = False
            End If
        End If
    End Sub

    Private Function SendEmailEachObserve() As Integer
        'Detail() = {ObserveNo, Title, Category, CategorySub, FailurePoint, Equipment, HRO, ObservedType, Description}
        'Picture() = {UrlPic1, UrlPic2, UrlPic3, UrlPic4}
        'OtherPropose() = {ProposeActionB, StatusB, ProposeActionC, StatusC}

        Dim Detail() As String = {"", "", "", "", "", "", "", "", "", ""}
        Dim Picture() As String = {"", "", "", ""}
        Dim OtherPropose() As String = {"", "", "", ""}

        'generate reporter
        Dim ReporterStr As String = "[" & tbMyFullname.Text.Trim & ", " & tbMyDowId.Text.Trim & ", " & tbMyDepart.Text.Trim & "] "
        If rgObserverList.Items.Count > 0 Then
            For Each items As GridDataItem In rgObserverList.Items
                ReporterStr = ReporterStr & "[" & items.Cells(3).Text.Trim & ", " & items.Cells(4).Text.Trim & ", " & items.Cells(5).Text.Trim & "] "
            Next
        End If

        For i = 1 To CInt(rcbNoObserve.SelectedValue)
            Detail(0) = i.ToString
            Dim Title As TextBox = RadMultiPage1.FindControl("tbTitle" & i.ToString)
            Detail(1) = Title.Text
            Dim Category As RadComboBox = RadMultiPage1.FindControl("rcbCategory" & i.ToString)
            Detail(2) = Category.Text
            Dim CategorySub As RadComboBox = RadMultiPage1.FindControl("rcbCategorySub" & i.ToString)
            Detail(3) = CategorySub.Text
            Dim FailurePoint As RadComboBox = RadMultiPage1.FindControl("rcbFailurePoint" & i.ToString)
            Detail(4) = FailurePoint.Text
            Dim Equipment As TextBox = RadMultiPage1.FindControl("tbEquipment" & i.ToString)
            Detail(5) = Equipment.Text

            Dim HROop1 As CheckBox = RadMultiPage1.FindControl("chkHRO" & i.ToString & "op1")
            Dim HROop2 As CheckBox = RadMultiPage1.FindControl("chkHRO" & i.ToString & "op2")
            Dim HROop3 As CheckBox = RadMultiPage1.FindControl("chkHRO" & i.ToString & "op3")
            Dim HROop4 As CheckBox = RadMultiPage1.FindControl("chkHRO" & i.ToString & "op4")
            Dim HROop5 As CheckBox = RadMultiPage1.FindControl("chkHRO" & i.ToString & "op5")

            Detail(6) = ""
            If HROop1.Checked Then Detail(6) = Detail(6) & "( x )" & HROop1.Text & "<br/>"
            If HROop2.Checked Then Detail(6) = Detail(6) & "( x )" & HROop2.Text & "<br/>"
            If HROop3.Checked Then Detail(6) = Detail(6) & "( x )" & HROop3.Text & "<br/>"
            If HROop4.Checked Then Detail(6) = Detail(6) & "( x )" & HROop4.Text & "<br/>"
            If HROop5.Checked Then Detail(6) = Detail(6) & "( x )" & HROop5.Text & "<br/>"
            If Detail(6).Length > 5 Then Detail(6) = Detail(6).Substring(0, Detail(6).Length - 5)

            Dim ObserveType As RadComboBox = RadMultiPage1.FindControl("rcbObserveType" & i.ToString)
            If ObserveType.SelectedValue = "0" Then
                Detail(7) = ObserveType.Text
            Else
                Dim Contractor As RadComboBox = RadMultiPage1.FindControl("rcbContractor" & i.ToString)
                Detail(7) = ObserveType.Text & "/ " & Contractor.Text
            End If
            Dim Description As TextBox = RadMultiPage1.FindControl("tbDescription" & i.ToString)
            Detail(8) = Description.Text

            Dim PictureList As DataList = RadMultiPage1.FindControl("PictureList" & i.ToString)
            For imCount As Integer = 1 To 4
                If PictureList.Items.Count >= imCount Then
                    Dim Image As System.Web.UI.WebControls.Image = PictureList.Items(imCount - 1).FindControl("Image" & i.ToString)
                    Picture(imCount - 1) = Image.ImageUrl
                Else
                    Picture(imCount - 1) = ""
                End If
            Next

            Dim Recognition As CheckBox = RadMultiPage1.FindControl("chkRecognition" & i.ToString)
            Dim ActionA As TextBox = RadMultiPage1.FindControl("tbAction" & i.ToString & "a")
            Dim ActionAHTML As String = Replace(ActionA.Text, vbLf, "<br/>")
            Dim ResponPersonA As String = ""
            If Not Recognition.Checked Then
                Dim ResponA As RadAutoCompleteBox = RadMultiPage1.FindControl("racRespon" & i.ToString & "a")
                ResponPersonA = ResponA.Entries.Item(0).Text
            End If

            Dim pnResponB As Panel = RadMultiPage1.FindControl("pnRespon" & i.ToString & "b")
            If pnResponB.Visible Then
                Dim ActionB As TextBox = RadMultiPage1.FindControl("tbAction" & i.ToString & "b")
                OtherPropose(0) = Replace(ActionB.Text, vbLf, "<br/>")
                If Not Recognition.Checked Then
                    Dim ResponB As RadAutoCompleteBox = RadMultiPage1.FindControl("racRespon" & i.ToString & "b")
                    OtherPropose(1) = ResponB.Entries.Item(0).Text
                End If
            Else
                OtherPropose(0) = ""
                OtherPropose(1) = ""
            End If
            Dim pnResponC As Panel = RadMultiPage1.FindControl("pnRespon" & i.ToString & "c")
            If pnResponC.Visible Then
                Dim ActionC As TextBox = RadMultiPage1.FindControl("tbAction" & i.ToString & "c")
                OtherPropose(2) = Replace(ActionC.Text, vbLf, "<br/>")
                If Not Recognition.Checked Then
                    Dim ResponC As RadAutoCompleteBox = RadMultiPage1.FindControl("racRespon" & i.ToString & "c")
                    OtherPropose(3) = ResponC.Entries.Item(0).Text
                End If
            Else
                OtherPropose(2) = ""
                OtherPropose(3) = ""
            End If

            Dim SendMail As New cSendMail
            Dim Subject As String = "EZ Path: " & Title.Text
            Dim recIdLink As String = hfRecId.Value
            Dim body As String = SendMail.PopulateBody("observeIssue.html", rcbDepartment.Text, lbActionNum.Text, ReporterStr, Detail, Picture, ActionAHTML, ResponPersonA, OtherPropose, recIdLink)

            SendMail.SendHtmlFormattedEmail(CreateEmailToEachObserve(i), Subject, body)
        Next

        Return True
    End Function

    Private Function CreateEmailToEachObserve(ByVal ObserveNo As Integer) As ArrayList
        Dim MailTo As New ArrayList

        For Each items As GridDataItem In rgEmailList.Items
            If TryCast(items.FindControl("chkSelectSend"), CheckBox).Checked Then
                Dim Email As Label = items.FindControl("lbEmail")
                Dim FullName As Label = items.FindControl("lbFullName")
                MailTo.AddRange({Email.Text, FullName.Text})
            End If
        Next
        For Each items As GridDataItem In rgEmailListEachOb.Items
            Dim lbObserveNo As Label = items.FindControl("lbObserveNo")
            Dim rowObserveNo As Integer = CInt(lbObserveNo.Text.Substring(lbObserveNo.Text.Length - 1))

            If TryCast(items.FindControl("chkSelectSend"), CheckBox).Checked And rowObserveNo = ObserveNo Then
                Dim lbEmail As Label = items.FindControl("lbEmail")
                Dim lbFullName As Label = items.FindControl("lbFullName")

                MailTo.AddRange({lbEmail.Text, lbFullName.Text})
            End If
        Next

        Return MailTo
    End Function


    '------------------------------------------------------------------------------
    '
    Protected Sub btObservationList_Click(sender As Object, e As EventArgs) Handles btObservationList.Click
        Response.Redirect("observationList.aspx")
    End Sub
    Protected Sub btNewObserve_Click(sender As Object, e As EventArgs) Handles btNewObserve.Click
        Response.Redirect("observation.aspx")
    End Sub


End Class