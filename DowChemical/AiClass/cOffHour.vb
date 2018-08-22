Imports System.Data.SqlClient

Public Class cOffHour
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Dim HolidayList As New ArrayList()
    Dim oStart As TimeSpan
    Dim oClosed As TimeSpan

    Public Sub New()
        HolidayList.Clear()

        'get holidays
        Dim strSql As String = "SELECT holidayDate FROM tblOffHourHoliday ORDER BY holidayDate"
        Dim conn As New SqlConnection(ConnStr)
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand(strSql, conn)
        Dim tbHoliday As New DataTable()
        conn.Open()
        Try
            adapter.Fill(tbHoliday)
        Finally
            conn.Close()
        End Try

        For Each row As DataRow In tbHoliday.Rows
            Dim hDate As Date = CDate(row.Item(0))
            HolidayList.Add(hDate)
        Next

        'get work hour for check time
        Dim strSql2 As String = "SELECT * FROM tblOffHourWorkTime"
        Using conn2 As New SqlConnection(ConnStr)
            conn2.Open()
            Dim command2 As New SqlCommand(strSql2, conn2)
            Dim DataRead As SqlDataReader
            DataRead = command2.ExecuteReader()
            While DataRead.Read()
                oStart = DataRead("workStart")
                oClosed = DataRead("workEnd")
            End While
        End Using
    End Sub

    Public Function chkOffHour(ByVal sDate As Date, ByVal sTime As TimeSpan) As Boolean
        Dim IsOffHour As Boolean = False
        If HolidayList.Contains(sDate) Then
            IsOffHour = True
        Else
            Dim DayOfWeek As Integer = sDate.DayOfWeek
            If DayOfWeek = 0 Or DayOfWeek = 6 Then
                IsOffHour = True
            Else
                If sTime > oClosed Or sTime < oStart Then
                    IsOffHour = True
                End If
            End If
        End If

        Return IsOffHour
    End Function

End Class
