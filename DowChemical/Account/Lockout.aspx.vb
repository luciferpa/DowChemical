
Imports System.Data.SqlClient
Imports Microsoft.AspNet.Identity

Partial Public Class Lockout
    Inherits System.Web.UI.Page
    Dim connStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs)
        SaveLog("#LockOut -Lockout.aspx page", User.Identity.Name)
        Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
        Response.Redirect("~/Account/Login")
    End Sub

    Private Sub SaveLog(ByVal msg As String, ByVal user As String)
        Dim strIns As String = "INSERT INTO tblLogLogin(logMsg, userLogin, timestamp) VALUES(@logMsg, @userLogin, @timestamp)"

        Using conn As New SqlConnection(connStr)
            conn.Open()
            Dim command As New SqlCommand(strIns, conn)
            command.Parameters.Add("@logMsg", SqlDbType.NVarChar).Value = msg
            command.Parameters.Add("@userLogin", SqlDbType.NVarChar).Value = user
            command.Parameters.Add("@timestamp", SqlDbType.DateTime).Value = Now()
            command.ExecuteNonQuery()
        End Using
    End Sub
End Class
