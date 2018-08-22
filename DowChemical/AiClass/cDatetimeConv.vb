Imports System.Globalization

Public Class cDatetimeConv
    Public Function DateToStringEnUS(ByVal inputDate As Date) As String
        Dim UsaCulture As New CultureInfo("en-US")
        Dim DateFrom As Date = inputDate
        Dim usDateFrom As String = DateFrom.ToString("yyyy-MM-dd", UsaCulture)

        Return usDateFrom
    End Function
End Class
