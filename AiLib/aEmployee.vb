Imports System.Data.SqlClient
Imports System.Configuration

Public Class aEmployee
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Public Function chkEnployeeCodeRepeat(ByVal DowID As String) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblEmployee WHERE (empDowId = @empDowId)"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@empDowId", SqlDbType.VarChar).Value = DowID
        Dim Count As Integer = command.ExecuteScalar()
        conn.Close()

        If Count > 0 Then Return True Else Return False
    End Function

    Public Function chkEnployeeFullNameRepeat(ByVal Name As String, ByVal SurName As String) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblEmployee WHERE ((empName = @empName) AND (empSurname = @empSurname))"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@empName", SqlDbType.NVarChar).Value = Name
        command.Parameters.Add("@empSurname", SqlDbType.NVarChar).Value = SurName
        Dim Count As Integer = command.ExecuteScalar()
        conn.Close()

        If Count > 0 Then Return True Else Return False
    End Function

    Public Function chkEnployeeDisplayRepeat(ByVal Display As String) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblEmployee WHERE (empDisplay = @empDisplay)"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@empDisplay", SqlDbType.NVarChar).Value = Display
        Dim Count As Integer = command.ExecuteScalar()
        conn.Close()

        If Count > 0 Then Return True Else Return False
    End Function

    Public Function chkEnployeeEmailRepeat(ByVal EmpEmail As String) As Boolean
        Dim strCount As String = "SELECT COUNT(*) FROM tblEmployee WHERE (empEmail = @empEmail)"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strCount, conn)
        command.Parameters.Add("@empEmail", SqlDbType.NVarChar).Value = EmpEmail
        Dim Count As Integer = command.ExecuteScalar()
        conn.Close()

        If Count > 0 Then Return True Else Return False
    End Function

    '--------- External Project '---------





    'Public Function countMember_tbCustomer(ByVal employee_id As String) As Integer
    '    Dim strSqlCount As String
    '    strSqlCount = "SELECT COUNT(*) FROM tbl_customer WHERE (employee_id1 = '"
    '    strSqlCount &= employee_id & "') OR (employee_id2 = '" & employee_id & "')"
    '    Dim connectionChk As New SqlConnection(ConnStr)
    '    connectionChk.Open()
    '    Dim commandChk As New SqlCommand(strSqlCount, connectionChk)
    '    Dim Count As String = commandChk.ExecuteScalar()
    '    connectionChk.Close()

    '    Return Int(Count)
    'End Function

    'Public Function countMember_tbMediaMgr(ByVal employee_id As String) As Integer
    '    Dim strSqlCount As String
    '    strSqlCount = "SELECT COUNT(*) FROM tbl_mediaMgr WHERE (employee_id = '"
    '    strSqlCount &= employee_id & "')"
    '    Dim connectionChk As New SqlConnection(ConnStr)
    '    connectionChk.Open()
    '    Dim commandChk As New SqlCommand(strSqlCount, connectionChk)
    '    Dim Count As String = commandChk.ExecuteScalar()
    '    connectionChk.Close()

    '    Return Int(Count)
    'End Function

    'Public Function countMember_tbSalesRecord(ByVal employee_id As String) As Integer
    '    Dim strSqlCount As String
    '    strSqlCount = "SELECT COUNT(*) FROM tbl_record WHERE (employee_id = '"
    '    strSqlCount &= employee_id & "')"
    '    Dim connectionChk As New SqlConnection(ConnStr)
    '    connectionChk.Open()
    '    Dim commandChk As New SqlCommand(strSqlCount, connectionChk)
    '    Dim Count As String = commandChk.ExecuteScalar()
    '    connectionChk.Close()

    '    Return Int(Count)
    'End Function





End Class
