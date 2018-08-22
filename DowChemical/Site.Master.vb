Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Telerik.Web.UI
Imports System.Web.UI.Page
Imports System.Web.Configuration
Imports System.Configuration
Imports System.Data.SqlClient

Public Class SiteMaster
    Inherits MasterPage
    Private Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Private Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Private _antiXsrfTokenValue As String
    Dim connStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Protected Sub Page_Init(sender As Object, e As EventArgs)
        ' The code below helps to protect against XSRF attacks
        Dim requestCookie = Request.Cookies(AntiXsrfTokenKey)
        Dim requestCookieGuidValue As Guid
        If requestCookie IsNot Nothing AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue) Then
            ' Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value
            Page.ViewStateUserKey = _antiXsrfTokenValue
        Else
            ' Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
            Page.ViewStateUserKey = _antiXsrfTokenValue

            Dim responseCookie = New HttpCookie(AntiXsrfTokenKey) With {
                 .HttpOnly = True,
                 .Value = _antiXsrfTokenValue
            }
            If FormsAuthentication.RequireSSL AndAlso Request.IsSecureConnection Then
                responseCookie.Secure = True
            End If
            Response.Cookies.[Set](responseCookie)
        End If

        AddHandler Page.PreLoad, AddressOf master_Page_PreLoad
    End Sub

    Protected Sub master_Page_PreLoad(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            ' Set Anti-XSRF token
            ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
            ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, [String].Empty)
        Else
            ' Validate the Anti-XSRF token
            If DirectCast(ViewState(AntiXsrfTokenKey), String) <> _antiXsrfTokenValue OrElse DirectCast(ViewState(AntiXsrfUserNameKey), String) <> (If(Context.User.Identity.Name, [String].Empty)) Then
                Throw New InvalidOperationException("Validation of Anti-XSRF token failed.")
                Response.Redirect("~/Account/Login")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            'Dim config As Configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.Config")
            'Dim section As SessionStateSection = DirectCast(config.GetSection("system.web/logoutTime"), SessionStateSection)
            'Dim timeout As Integer = CInt(section.Timeout.TotalMinutes) * 1000 * 60

            Dim timeout As Integer
            If Not Page.IsPostBack Then
                Dim rWebConfig As System.Configuration.Configuration
                rWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
                Dim sessionTimeout As System.Configuration.KeyValueConfigurationElement
                sessionTimeout = rWebConfig.AppSettings.Settings("logoutTime")
                timeout = 1000 * 60 * CInt(sessionTimeout.Value)

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "SessionAlert", "SessionExpireAlert(" & timeout & ");", True)
                'Else
                '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "SessionAlert", "SessionExpireAlert(" & timeout & ");", True)
            End If
        End If
    End Sub

    Protected Sub Unnamed_LoggingOut(sender As Object, e As LoginCancelEventArgs)
        'SaveLog("#LockOut -Site.Master", HttpContext.Current.User.Identity.Name)
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

    Protected Sub btnChange_Click(sender As Object, e As EventArgs)
        Dim RadToolTip As RadToolTip = LoginView1.FindControl("RadToolTip1")
        Dim currentPass As TextBox = RadToolTip.FindControl("CurrentPassword")
        Dim newPass As TextBox = RadToolTip.FindControl("NewPassword")

        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim signInManager = Context.GetOwinContext().Get(Of ApplicationSignInManager)()
        Dim result As IdentityResult = manager.ChangePassword(HttpContext.Current.User.Identity.GetUserId(), currentPass.Text, newPass.Text)
        If result.Succeeded Then
            Dim userInfo = manager.FindById(HttpContext.Current.User.Identity.GetUserId())
            signInManager.SignIn(userInfo, isPersistent:=False, rememberBrowser:=False)
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie)   'logoff
            Response.Redirect("~/Account/Login")
        Else
            Dim lbInfo As Label = RadToolTip.FindControl("lbInfo")
            lbInfo.Text = "Change password fail. (" & result.Errors.ToString & ")"
        End If
    End Sub

    'Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    '    If Not Page.User.Identity.IsAuthenticated Then
    '        SaveLog("#LockOut -Site.Master", HttpContext.Current.User.Identity.Name)
    '        Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
    '    End If
    'End Sub
End Class