Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin

Partial Public Class Login2
    Inherits Page
    Dim connStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        OpenAuthLogin.ReturnUrl = Request.QueryString("ReturnUrl")
        Dim returnUrl = HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
        If Not [String].IsNullOrEmpty(returnUrl) Then
            RegisterHyperLink.NavigateUrl += "?ReturnUrl=" & returnUrl
        End If
    End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)
        If IsValid Then
            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim signinManager = Context.GetOwinContext().GetUserManager(Of ApplicationSignInManager)()

            Dim userlogin As String = Username.Text.Trim()
            Dim result As SignInStatus
            Try
                result = signinManager.PasswordSignIn(userlogin, Password.Text, RememberMe.Checked, shouldLockout:=False)
                SaveLog(result.ToString, userlogin)
            Catch ex As Exception
                SaveLog("Exception: " & ex.Message, userlogin)
            Finally
                Select Case result
                    Case SignInStatus.Success
                        lbDebugInfo.Text = Request.QueryString("ReturnUrl")
                        MsgBox(Request.QueryString("ReturnUrl"))
                        If chkDebugInfo.Checked Then
                            Response.Redirect("/")
                        Else
                            IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
                            lbDebugInfo.Text = lbDebugInfo.Text & ""
                        End If
                        Exit Select
                    Case SignInStatus.LockedOut
                        Response.Redirect("/Account/Lockout")
                        Exit Select
                    Case SignInStatus.RequiresVerification
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                        Request.QueryString("ReturnUrl"), RememberMe.Checked), True)
                        Exit Select
                    Case Else
                        FailureText.Text = "Invalid login attempt"
                        ErrorMessage.Visible = True
                        Exit Select
                End Select
            End Try
        End If

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
