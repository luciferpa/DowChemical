Imports System.Data.SqlClient
Imports System.Configuration

Public Class cFailureCategory
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Public Function GetCategory(ByVal CategoryId As Integer) As String
        Const StrSel As String = "SELECT cateName FROM tblObsvCate WHERE cateId = @cateId"
        Dim CategoryName As String = ""
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(StrSel, conn)
            command.Parameters.Add("@cateId", SqlDbType.Int).Value = CategoryId

            Dim DataRead As SqlDataReader = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                CategoryName = DataRead("cateName")
            End If
        End Using

        Return CategoryName
    End Function

    Public Function GetCategorySub(ByVal CategorySubId As Integer) As String
        Const StrSel As String = "SELECT catesubName FROM tblObsvCateSub WHERE catesubId = @catesubId"
        Dim CategorySubName As String = ""
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(StrSel, conn)
            command.Parameters.Add("@catesubId", SqlDbType.Int).Value = CategorySubId

            Dim DataRead As SqlDataReader = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                CategorySubName = DataRead("catesubName")
            End If
        End Using

        Return CategorySubName
    End Function

    Public Function GetFailPoint(ByVal FailPointId As Integer) As String
        Const StrSel As String = "SELECT failpointName FROM tblObsvFailPoint WHERE failpointId = @failpointId"
        Dim FailPointName As String = ""
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(StrSel, conn)
            command.Parameters.Add("@failpointId", SqlDbType.Int).Value = FailPointId

            Dim DataRead As SqlDataReader = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                FailPointName = DataRead("failpointName")
            End If
        End Using

        Return FailPointName
    End Function

    Private _GetCategory_bySub_returnSubCate As String
    Public ReadOnly Property GetCategory_bySub_returnSubCate() As String
        Get
            Return _GetCategory_bySub_returnSubCate
        End Get
    End Property
    Public Function GetCategory_bySub(ByVal CategorySubId As Integer) As String
        Const StrSel As String = "SELECT tblObsvCateSub.catesubName, tblObsvCate.cateName FROM tblObsvCateSub INNER JOIN tblObsvCate ON tblObsvCateSub.cateId = tblObsvCate.cateId 
                                    WHERE (tblObsvCateSub.catesubId = @catesubId)"
        Dim CategoryName As String = ""
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(StrSel, conn)
            command.Parameters.Add("@catesubId", SqlDbType.Int).Value = CategorySubId

            Dim DataRead As SqlDataReader = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                CategoryName = DataRead("cateName")
                _GetCategory_bySub_returnSubCate = DataRead("catesubName")
            End If
        End Using

        Return CategoryName
    End Function

    Public Function GetCategoryId(ByVal CategoryName As String) As Integer
        Dim StrSel As String = "SELECT cateId FROM tblObsvCate WHERE cateName = @cateName"
        Dim CategoryId As Integer = 1000
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(StrSel, conn)
            command.Parameters.Add("@cateName", SqlDbType.NVarChar).Value = CategoryName

            Dim DataRead As SqlDataReader = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                CategoryId = DataRead("cateId")
            End If
        End Using

        Return CategoryId
    End Function

    Public Function GetCategorySubId(ByVal CategorySubName As String, ByVal CategoryId As Integer) As Integer
        Dim StrSel As String = "SELECT catesubId FROM tblObsvCateSub WHERE catesubName = @catesubName AND cateId = @cateId "
        Dim CategorySubId As Integer = 1000
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(StrSel, conn)
            command.Parameters.Add("@catesubName", SqlDbType.NVarChar).Value = CategorySubName
            command.Parameters.Add("@cateId", SqlDbType.Int).Value = CategoryId

            Dim DataRead As SqlDataReader = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                CategorySubId = DataRead("catesubId")
            End If
        End Using

        Return CategorySubId
    End Function

    Public Function GetFailPointId(ByVal FailPointName As String, ByVal CategorySubId As Integer) As Integer
        Dim StrSel As String = "SELECT failpointId FROM tblObsvFailPoint WHERE failpointName = @failpointName AND catesubId = @catesubId "
        Dim FailPointId As Integer = 0
        Using conn As New SqlConnection(ConnStr)
            conn.Open()
            Dim command As New SqlCommand(StrSel, conn)
            command.Parameters.Add("@failpointName", SqlDbType.NVarChar).Value = FailPointName
            command.Parameters.Add("@catesubId", SqlDbType.Int).Value = CategorySubId

            Dim DataRead As SqlDataReader = command.ExecuteReader()
            If DataRead.HasRows() Then
                DataRead.Read()
                FailPointId = DataRead("failpointId")
            End If
        End Using

        Return FailPointId
    End Function

End Class
