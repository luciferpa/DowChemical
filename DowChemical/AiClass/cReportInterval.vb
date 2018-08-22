Imports System.Data.SqlClient

Public Class cReportInterval
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Private _IntervalAdmin As Integer
    Private _IntervalUser As Integer

    Public Function GetIntervalAdmin() As Integer
        Dim strSql As String = "SELECT reCalcIntervalAdmin, reCalcInterval FROM tblRpSetting"
        Dim dataRead As SqlDataReader
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(strSql, conn)
            dataRead = command.ExecuteReader()

            If dataRead.HasRows() Then
                dataRead.Read()
                _IntervalAdmin = CInt(dataRead("reCalcIntervalAdmin"))
                _IntervalUser = CInt(dataRead("reCalcInterval"))
            End If
        End Using

        Return _IntervalAdmin
    End Function

    Public Function GetIntervalUser() As Integer
        Return _IntervalUser
    End Function

    Public Function SetInterval(ByVal IntervalAdmin As Integer, ByVal IntervalUser As Integer) As Integer
        Dim strUpd As String = "UPDATE tblRpSetting SET reCalcIntervalAdmin = @reCalcIntervalAdmin, reCalcInterval = @reCalcInterval"
        Dim conn As New SqlConnection(ConnStr)
        conn.Open()
        Dim command As New SqlCommand(strUpd, conn)

        command.Parameters.Add("@reCalcIntervalAdmin", SqlDbType.Int).Value = IntervalAdmin
        command.Parameters.Add("@reCalcInterval", SqlDbType.Int).Value = IntervalUser
        Dim errCode As Integer = command.ExecuteNonQuery()
        conn.Close()

        Return errCode
    End Function


End Class
