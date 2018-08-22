Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Class aAutoNumber
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Public Property xYear As String
    Public Property xMonth As String
    Public Property xNumber As String

    Public Function ActNumberAutoNum(ByVal DepartName As String, ByVal SelectDate As Date) As Integer
        Return getNextActNumber(getDepartId(DepartName), SelectDate)
    End Function
    Public Function ActNumberAutoNumDepartId(ByVal DepartId As Integer, ByVal SelectDate As Date) As Integer
        Return getNextActNumber(DepartId, SelectDate)
    End Function

    Private Function getNextActNumber(ByVal DepartId As String, ByVal SelectDate As Date) As Integer
        Dim SelMonth As Integer = SelectDate.Month
        Dim SelYear As Integer = SelectDate.Year

        Dim conn As New SqlConnection(ConnStr)
        Dim strSql As String = "SELECT recActNoValue FROM tblRecord WHERE (recActNoValue = (SELECT MAX(recActNoValue) FROM tblRecord WHERE departId = @departId AND recActMonth = @recActMonth AND recActYear = @recActYear))"
        Dim command As New SqlCommand(strSql, conn) With {.CommandType = CommandType.Text}
        command.Parameters.Add("@departId", SqlDbType.Int).Value = DepartId
        command.Parameters.Add("@recActMonth", SqlDbType.Int).Value = SelMonth
        command.Parameters.Add("@recActYear", SqlDbType.Int).Value = SelYear

        Dim lastActNo As String = ""
        Dim lastNum As String = ""
        Dim lastMounth As String = ""
        Dim lastYear As String = ""

        conn.Open()
        Dim DataRead As SqlDataReader = command.ExecuteReader()
        If DataRead.HasRows() Then
            DataRead.Read()
            If DataRead("recActNoValue") IsNot DBNull.Value Then
                lastActNo = DataRead("recActNoValue")
                lastNum = Format(lastActNo.Substring(4), "00000").ToString
                lastMounth = Format(lastActNo.Substring(2, 2), "00").ToString
                lastYear = Format(lastActNo.Substring(0, 2), "00").ToString
            End If
        Else
            lastNum = "00000"
            lastMounth = Format(SelMonth, "00").ToString
            lastYear = SelYear.ToString.Substring(2)
            lastActNo = lastYear & lastMounth & lastNum
        End If
        conn.Close()

        Return CInt(lastActNo) + 1
    End Function

    Private Function getDepartId(ByVal DepartName As String) As Integer
        Const StrSelect As String = "SELECT departId FROM tblDepartment WHERE departName = @departName"
        Dim DepartId As Integer
        Using conn As New SqlConnection()
            conn.ConnectionString = ConnStr
            conn.Open()
            Dim command As New SqlCommand(StrSelect, conn)
            command.Parameters.Add("@departName", SqlDbType.VarChar).Value = DepartName
            Dim DataRead As SqlDataReader
            DataRead = command.ExecuteReader()
            While DataRead.Read()
                DepartId = CInt(DataRead("departId"))
            End While
        End Using

        Return DepartId
    End Function



End Class
