Imports System.Data.SqlClient

Public Class cScore
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Private _departId As Integer
    Private _sMonth As Integer
    Private _sYear As Integer

    Public Sub New(ByVal yearId As Integer, ByVal departId As Integer)
        _sYear = yearId
        _departId = departId
    End Sub

    Public Property Month As Integer
        Get
            Return _sMonth
        End Get
        Set(value As Integer)
            _sMonth = value
        End Set
    End Property
    Public Property Year As Integer
        Get
            Return _sYear
        End Get
        Set(value As Integer)
            _sYear = value
        End Set
    End Property
End Class
