Imports System.Data.SqlClient
Imports System.Configuration

Public Class cEmployee
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Private _EmployeeId As Integer
    Private _EmployeeName As String
    Private _EmployeeSurname As String
    Private _EmployeeEmail As String
    Private _DowId As String
    Private _DepartId As Integer
    Private _PlantId As Integer
    Private _JobLevel As String
    Private _TypeId As Integer = 1000
    Private _Username As String

    Public Property EmployeeId As Integer
        Get
            Return _EmployeeId
        End Get
        Set(value As Integer)
            _EmployeeId = value
        End Set
    End Property
    Public Property EmployeeName As String
        Get
            Return _EmployeeName
        End Get
        Set(value As String)
            _EmployeeName = value
        End Set
    End Property
    Public Property EmployeeSurname As String
        Get
            Return _EmployeeSurname
        End Get
        Set(value As String)
            _EmployeeSurname = value
        End Set
    End Property
    Public ReadOnly Property EmployeeFullName As String
        Get
            Return _EmployeeName & "  " & _EmployeeSurname
        End Get
    End Property
    Public Property EmployeeEmail As String
        Get
            Return _EmployeeEmail
        End Get
        Set(value As String)
            _EmployeeEmail = value
        End Set
    End Property
    Public Property DowId As String
        Get
            Return _DowId
        End Get
        Set(value As String)
            _DowId = value
        End Set
    End Property
    Public Property DepartmentId As Integer
        Get
            Return _DepartId
        End Get
        Set(value As Integer)
            _DepartId = value
        End Set
    End Property
    Public ReadOnly Property DepartmentName As String
        Get
            Const StrSelect As String = "SELECT departName FROM tblDepartment WHERE departId = @departId"
            Dim _DepartmentName As String = ""
            Using connection As New SqlConnection(ConnStr)
                connection.Open()
                Dim command As New SqlCommand(StrSelect, connection)
                command.Parameters.Add("@departId", SqlDbType.Int).Value = _DepartId
                Dim DataRead As SqlDataReader
                DataRead = command.ExecuteReader()
                While DataRead.Read()
                    _DepartmentName = DataRead("departName")
                End While
            End Using

            Return _DepartmentName
        End Get
    End Property
    Public Property PlantId As Integer
        Get
            Return _PlantId
        End Get
        Set(value As Integer)
            _PlantId = value
        End Set
    End Property
    Public ReadOnly Property PlantName As String
        Get
            Const StrSelect As String = "SELECT plantName FROM tblPlant WHERE plantId = @plantId"
            Dim _PlantName As String = ""
            Using connection As New SqlConnection(ConnStr)
                connection.Open()
                Dim command As New SqlCommand(StrSelect, connection)
                command.Parameters.Add("@plantId", SqlDbType.Int).Value = _PlantId
                Dim DataRead As SqlDataReader
                DataRead = command.ExecuteReader()
                While DataRead.Read()
                    _PlantName = DataRead("plantName")
                End While
            End Using

            Return _PlantName
        End Get
    End Property
    Public Property JobLevel As String
        Get
            Return _JobLevel
        End Get
        Set(value As String)
            _JobLevel = value
        End Set
    End Property
    Public ReadOnly Property AccountType As String
        Get
            Const StrSelect As String = "SELECT typeName FROM tblAccountType WHERE typeId = @typeId"
            Dim _AccountType As String = ""
            Using connection As New SqlConnection(ConnStr)
                connection.Open()
                Dim command As New SqlCommand(StrSelect, connection)
                command.Parameters.Add("@typeId", SqlDbType.Int).Value = _TypeId
                Dim DataRead As SqlDataReader
                DataRead = command.ExecuteReader()
                While DataRead.Read()
                    _AccountType = DataRead("typeName")
                End While
            End Using

            Return _AccountType
        End Get
    End Property

    Public Function FindEmployeeIdbyUsername(ByVal Username As String) As Integer
        Const StrSelect As String = "SELECT * FROM tblEmployee WHERE userName = @userName"
        Dim EmployeeId As Integer
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(StrSelect, connection)
            command.Parameters.Add("@userName", SqlDbType.NVarChar).Value = Username

            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()
            While DataRead.Read()
                EmployeeId = If(DataRead("empId") IsNot DBNull.Value, DataRead("empId"), 0)

                _EmployeeName = If(DataRead("empName") IsNot DBNull.Value, DataRead("empName"), "")
                _EmployeeSurname = If(DataRead("empSurname") IsNot DBNull.Value, DataRead("empSurname"), "")
                _EmployeeEmail = If(DataRead("empEmail") IsNot DBNull.Value, DataRead("empEmail"), "")
                _DowId = If(DataRead("empDowId") IsNot DBNull.Value, DataRead("empDowId"), "")
                _DepartId = If(DataRead("departId") IsNot DBNull.Value, DataRead("departId"), 0)
                _PlantId = If(DataRead("plantId") IsNot DBNull.Value, DataRead("plantId"), 0)
                _JobLevel = If(DataRead("joblvCode") IsNot DBNull.Value, DataRead("joblvCode"), "")
                _TypeId = If(DataRead("typeId") IsNot DBNull.Value, DataRead("typeId"), 1000)
            End While
        End Using
        _EmployeeId = EmployeeId
        _Username = Username

        Return EmployeeId
    End Function

    Public Function FindEmployeebyName(ByVal EmployeeName As String) As Integer
        Const StrSelect As String = "SELECT * FROM tblEmployee WHERE empName = @empName"
        Dim EmployeeId As Integer
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(StrSelect, connection)
            command.Parameters.Add("@empName", SqlDbType.NVarChar).Value = EmployeeName

            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()
            While DataRead.Read()
                EmployeeId = If(DataRead("empId") IsNot DBNull.Value, DataRead("empId"), 0)

                _Username = If(DataRead("empName") IsNot DBNull.Value, DataRead("empName"), "")
                _EmployeeSurname = If(DataRead("empSurname") IsNot DBNull.Value, DataRead("empSurname"), "")
                _EmployeeEmail = If(DataRead("empEmail") IsNot DBNull.Value, DataRead("empEmail"), "")
                _DowId = If(DataRead("empDowId") IsNot DBNull.Value, DataRead("empDowId"), "")
                _DepartId = If(DataRead("departId") IsNot DBNull.Value, DataRead("departId"), 0)
                _PlantId = If(DataRead("plantId") IsNot DBNull.Value, DataRead("plantId"), 0)
                _JobLevel = If(DataRead("joblvCode") IsNot DBNull.Value, DataRead("joblvCode"), "")
                _TypeId = If(DataRead("typeId") IsNot DBNull.Value, DataRead("typeId"), 1000)

                '_Username = Username
            End While
        End Using
        _EmployeeId = EmployeeId
        _EmployeeName = EmployeeName

        Return EmployeeId
    End Function

    Public Function FindEmployeeName(ByVal EmployeeId As Integer) As String
        Const StrSelect As String = "SELECT * FROM tblEmployee WHERE empId = @empId"
        Dim EmployeeName As String = ""
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(StrSelect, connection)
            command.Parameters.Add("@empId", SqlDbType.Int).Value = EmployeeId

            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()
            While DataRead.Read()
                EmployeeName = If(DataRead("empName") IsNot DBNull.Value, DataRead("empName"), "")

                _EmployeeId = If(DataRead("empId") IsNot DBNull.Value, DataRead("empId"), "")
                _EmployeeSurname = If(DataRead("empSurname") IsNot DBNull.Value, DataRead("empSurname"), "")
                _EmployeeEmail = If(DataRead("empEmail") IsNot DBNull.Value, DataRead("empEmail"), "")
                _DowId = If(DataRead("empDowId") IsNot DBNull.Value, DataRead("empDowId"), "")
                _DepartId = If(DataRead("departId") IsNot DBNull.Value, DataRead("departId"), 0)
                _PlantId = If(DataRead("plantId") IsNot DBNull.Value, DataRead("plantId"), 0)
                _JobLevel = If(DataRead("joblvCode") IsNot DBNull.Value, DataRead("joblvCode"), "")
                _TypeId = If(DataRead("typeId") IsNot DBNull.Value, DataRead("typeId"), 1000)

                '_Username = Username
            End While
        End Using
        _EmployeeId = EmployeeId
        _EmployeeName = EmployeeName

        Return EmployeeName
    End Function

    Public Sub UpdEmployeeStatus(ByVal EmpId As Integer, ByVal Status As Boolean, ByVal UserName As String, ByVal AccountType As Integer, ByVal aspNetUserId As String)
        Dim errUpd As Int16
        Const StrUpdate As String = "UPDATE tblEmployee SET isSetLogin = @isSetLogin, userName = @userName, typeId = @typeId, aspNetUserId = @aspNetUserId WHERE empId = @empId"
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(StrUpdate, connection)
            command.Parameters.Add("@empId", SqlDbType.Int).Value = EmpId
            command.Parameters.Add("@isSetLogin", SqlDbType.Bit).Value = Status
            command.Parameters.Add("@typeId", SqlDbType.Int).Value = AccountType
            If UserName = "" Then
                command.Parameters.Add("@userName", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                command.Parameters.Add("@userName", SqlDbType.NVarChar).Value = UserName
            End If
            If UserName = "" Then
                command.Parameters.Add("@aspNetUserId", SqlDbType.NVarChar).Value = DBNull.Value
            Else
                command.Parameters.Add("@aspNetUserId", SqlDbType.NVarChar).Value = aspNetUserId
            End If
            errUpd = command.ExecuteNonQuery()
        End Using
    End Sub

    Public Function UpdAccountStatus(ByVal EmployeeId As Integer, ByVal Username As String, ByVal AccountType As Integer) As Integer
        Dim strUpd As String = "UPDATE tblEmployee SET userName = @userName, typeId = @typeId WHERE empId = @empId"
        Dim errCode As Integer = 0
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(strUpd, connection)
            command.Parameters.Add("@empId", SqlDbType.Int).Value = EmployeeId
            command.Parameters.Add("@userName", SqlDbType.NVarChar).Value = Username
            command.Parameters.Add("@typeId", SqlDbType.Int).Value = AccountType
            errCode = command.ExecuteNonQuery()
        End Using

        Return errCode
    End Function

    Public Function ChkIsVisibleList(ByVal Username As String) As Boolean
        Const StrSelect As String = "SELECT COUNT(*) FROM tblEmployee WHERE userName = @userName AND isVisible = 'true'"
        Dim Count As Integer = 0
        Using connection As New SqlConnection(ConnStr)
            connection.Open()
            Dim command As New SqlCommand(StrSelect, connection)
            command.Parameters.Add("@userName", SqlDbType.NVarChar).Value = Username
            Count = command.ExecuteScalar()
        End Using

        If Count > 0 Then Return True Else Return False
    End Function

    Public Function countMember_tblRecord(ByVal empId As Integer) As Integer
        Dim strSqlCount As String = "SELECT TOP 1 recId FROM tblRecord WHERE (empId = @empId)"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strSqlCount, conn)
        command.Parameters.Add("@empId", SqlDbType.int).Value = empId
        Dim dr As SqlDataReader = command.ExecuteReader()
        If dr.HasRows() Then Return 1 Else Return 0
        conn.Close()
    End Function

    Public Function countMember_tblRecordDetail(ByVal empId As Integer) As Integer
        Dim strSqlCount As String = "SELECT TOP 1 detailId FROM tblRecordDetail WHERE (proposeRespPerson_A = @empId) OR (proposeRespPerson_B = @empId) OR (proposeRespPerson_C = @empId)"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strSqlCount, conn)        
        command.Parameters.Add("@empId", SqlDbType.int).Value = empId
        Dim dr As SqlDataReader = command.ExecuteReader()
        If dr.HasRows() Then Return 1 Else Return 0
        conn.Close()
    End Function

    Public Function countMember_tblRecordOthEmpO(ByVal empId As Integer) As Integer
        Dim strSqlCount As String = "SELECT TOP 1 Id FROM tblRecordOthEmpO WHERE (empIdOth = @empId)"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strSqlCount, conn)        
        command.Parameters.Add("@empId", SqlDbType.int).Value = empId
        Dim dr As SqlDataReader = command.ExecuteReader()
        If dr.HasRows() Then Return 1 Else Return 0
        conn.Close()
    End Function

    
End Class
