Imports System.Data.SqlClient
Imports System.Configuration

Public Class cGroupMail
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Private _EmpId As Integer
    Private _EmpFullName As String
    Private _EmpEmail As String
    Private _DepartId As Integer
    Private _DepartEmail1 As String
    Private _DepartEmail2 As String
    Private _GroupMailName As String

    Public Property EmpEmail As String
        Get
            Return _EmpEmail
        End Get
        Set(value As String)
            _EmpEmail = value
        End Set
    End Property

    Public Property EmpFullName As String
        Get
            Return _EmpFullName
        End Get
        Set(value As String)
            _EmpFullName = value
        End Set
    End Property

    Public Property Mail1 As String
        Get
            Return _DepartEmail1
        End Get
        Set(value As String)
            _DepartEmail1 = value
        End Set
    End Property

    Public Property Mail2 As String
        Get
            Return _DepartEmail2
        End Get
        Set(value As String)
            _DepartEmail2 = value
        End Set
    End Property

    Public Property GroupMailName As String
        Get
            Return _GroupMailName
        End Get
        Set(value As String)
            _GroupMailName = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(ByVal empId As Integer, ByVal IsRespon As Boolean)
        Dim sqlStr As String = ""
        If IsRespon Then
            sqlStr = "SELECT departId, empFullName, empEmail FROM tblEmployee WHERE empId = @empId"
        Else
            sqlStr = "SELECT tblDepartment.departId, tblDepartment.departEmail1, tblDepartment.departEmail2, tblDepartment.groupMailName, tblEmployee.empEmail, tblEmployee.empFullName FROM tblEmployee INNER JOIN tblDepartment On tblEmployee.departId = tblDepartment.departId WHERE tblEmployee.empId = @empId"
        End If
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(sqlStr, conn)
            command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()
            While DataRead.Read()
                _EmpId = empId
                If IsRespon Then
                    _DepartEmail1 = ""
                    _DepartEmail2 = ""
                    _GroupMailName = ""

                    _EmpFullName = DataRead("empFullName")
                    _EmpEmail = DataRead("empEmail")
                    _DepartId = DataRead("departId")
                Else
                    _DepartEmail1 = DataRead("departEmail1")
                    _DepartEmail2 = DataRead("departEmail2")
                    _GroupMailName = DataRead("groupMailName")

                    _EmpFullName = DataRead("empFullName")
                    _EmpEmail = DataRead("empEmail")
                    _DepartId = DataRead("departId")
                End If
            End While
        End Using
    End Sub
    Public Sub New(ByVal departName As String)
        Dim selStr As String = "SELECT departId, departEmail1, departEmail2, groupMailName FROM tblDepartment WHERE departName = @departName"
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(selStr, conn)
            command.Parameters.Add("@departName", SqlDbType.VarChar).Value = departName
            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()
            While DataRead.Read()
                _DepartId = DataRead("departId")
                _DepartEmail1 = DataRead("departEmail1")
                _DepartEmail2 = DataRead("departEmail2")
                _GroupMailName = DataRead("groupMailName")

                _EmpId = 0
                _EmpFullName = ""
                _EmpEmail = ""
            End While
        End Using
    End Sub

    Public Function AddEmailList(ByVal recId As Integer, ByVal item As Integer, ByVal type As Integer, ByVal email As String, ByVal empId As Integer, ByVal empFullName As String, ByVal IsSuggest As Boolean) As Integer
        Dim strIns As String = "INSERT INTO tblSendEmail(recId, observItem, emailType, email, empId, empFullName, IsSuggest) VALUES(@recId, @observItem, @emailType, @email, @empId, @empFullName, @IsSuggest)"
        Dim Result As Integer
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strIns, conn)
            command.Parameters.Add("@recId", SqlDbType.Int).Value = recId
            command.Parameters.Add("@observItem", SqlDbType.Int).Value = item
            command.Parameters.Add("@emailType", SqlDbType.Int).Value = type
            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email
            command.Parameters.Add("@empId", SqlDbType.Int).Value = empId
            command.Parameters.Add("@empFullName", SqlDbType.NVarChar).Value = empFullName
            command.Parameters.Add("@IsSuggest", SqlDbType.Bit).Value = IsSuggest
            Result = command.ExecuteNonQuery()
        End Using

        Return Result
    End Function
End Class
