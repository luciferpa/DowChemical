Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Web
Imports System.Web.Configuration
Imports System.Web.UI
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Owin

Partial Public Class Login
    Inherits Page
    Dim connStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'OpenAuthLogin.ReturnUrl = Request.QueryString("ReturnUrl")
        'Dim returnUrl = HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
        'If Not [String].IsNullOrEmpty(returnUrl) Then
        '    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" & returnUrl
        'End If

        If Not Page.IsPostBack Then
            Dim BrowserName As String = Request.Browser.Browser
            Dim BrowserVersion As Integer = Request.Browser.Version

            If BrowserName = "InternetExplorer" Then
                If BrowserVersion < 11 Then
                    btLogIn.Enabled = False
                End If
                'Else
                '    lbPageInfo.Text = "Warning : Browser is " & BrowserName
            End If

            'Dim rWebConfig As System.Configuration.Configuration
            'rWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
            'Dim customClearLogin As System.Configuration.KeyValueConfigurationElement
            'customClearLogin = rWebConfig.AppSettings.Settings("autoClearLogin")
            'Dim last As DateTime = CDate(customClearLogin.Value)

            'Dim resetInterval As Integer = 2
            'Dim lastUpdate As DateTime = last
            'If last.Year > 2500 Then lastUpdate = New DateTime(last.Year - 543, last.Month, last.Day, last.Hour, last.Minute, last.Second)
            'Dim chkPoint As DateTime = DateAdd(DateInterval.Hour, resetInterval, lastUpdate)

            ''Dim loopCount As Integer = 0
            ''Dim loopStr As String = ""
            'Dim nowStr As String = Now.ToShortDateString & " " & Now.ToShortTimeString
            ''If loopCount > 4 Then
            ''    SaveLog(chkPoint.ToShortDateString & " " & chkPoint.ToShortTimeString & " / " & nowStr & "# ", "TEST0")
            ''    Exit Sub
            ''End If

            'customClearLogin.Value = nowStr
            'rWebConfig.Save()
            'If chkPoint < Now() Then
            '    'loopCount = loopCount + 1
            '    'loopStr = loopStr & " " & chkPoint.ToShortDateString & " " & chkPoint.ToShortTimeString & " / " & nowStr
            '    SaveLog(chkPoint.ToShortDateString & " " & chkPoint.ToShortTimeString & " / " & nowStr & "# ", "TEST")
            '    customClearLogin.Value = nowStr
            '    rWebConfig.Save()
            'End If
            'lbautoClearLogin.Text = customClearLogin.Value & " @" & nowStr
            'MsgBox(loopStr)
        End If
    End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)
        If IsValid Then
            HttpContext.Current.Session("RunSession") = "initSession"

            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim signinManager = Context.GetOwinContext().GetUserManager(Of ApplicationSignInManager)()

            Context.GetOwinContext().Response.Cookies.Append("OwinCookie", "SomeValue")
            Context.Response.Cookies("ASPCookie").Value = "SomeValue"
            Context.Response.Cookies.Remove("ASPCookie")

            signinManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
            Dim userlogin As String = Username.Text.Trim()
            Dim result As SignInStatus
            Try
                result = signinManager.PasswordSignIn(userlogin, Password.Text, True, shouldLockout:=False)
                'result = signinManager.PasswordSignIn(userlogin, Password.Text, RememberMe.Checked, shouldLockout:=False)
                'SaveLog(result.ToString, userlogin)
            Catch ex As Exception
                SaveLog("Exception: " & ex.Message, userlogin)
            Finally
                Select Case result
                    Case SignInStatus.Success
                        'SaveLog("Finally: " & "url> " & Request.QueryString("ReturnUrl") & " $" & Request.Browser.Browser & ", " & Request.Browser.Version, userlogin)
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
                        Exit Select
                    Case SignInStatus.LockedOut
                        SaveLog("Finally: " & "LockedOut", userlogin)
                        Response.Redirect("~/Account/Lockout")
                        Exit Select
                    Case SignInStatus.RequiresVerification
                        SaveLog("Finally: " & "RequiresVerification", userlogin)
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                        Request.QueryString("ReturnUrl"), RememberMe.Checked), True)
                        Exit Select
                    Case Else
                        SaveLog("Finally: " & "Failure", userlogin)
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

    Protected Sub imbReset_Click(sender As Object, e As ImageClickEventArgs) Handles imbReset.Click
        Dim signinManager = Context.GetOwinContext().GetUserManager(Of ApplicationSignInManager)()
        signinManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie)

        Dim s As String = ""
        With Request.Browser
            s &= "Browser Capabilities" & vbCrLf
            s &= "Type = " & .Type & vbCrLf
            s &= "Name = " & .Browser & vbCrLf
            s &= "Version = " & .Version & vbCrLf
            s &= "Major Version = " & .MajorVersion & vbCrLf
            s &= "Minor Version = " & .MinorVersion & vbCrLf
            s &= "Platform = " & .Platform & vbCrLf
            s &= "Is Beta = " & .Beta & vbCrLf
            s &= "Is Crawler = " & .Crawler & vbCrLf
            s &= "Is AOL = " & .AOL & vbCrLf
            s &= "Is Win16 = " & .Win16 & vbCrLf
            s &= "Is Win32 = " & .Win32 & vbCrLf
            s &= "Supports Frames = " & .Frames & vbCrLf
            s &= "Supports Tables = " & .Tables & vbCrLf
            s &= "Supports Cookies = " & .Cookies & vbCrLf
            s &= "Supports VBScript = " & .VBScript & vbCrLf
            s &= "Supports JavaScript = " &
                .EcmaScriptVersion.ToString() & vbCrLf
            s &= "Supports Java Applets = " & .JavaApplets & vbCrLf
            s &= "Supports ActiveX Controls = " & .ActiveXControls &
                vbCrLf
            s &= "Supports JavaScript Version = " &
                Request.Browser("JavaScriptVersion") & vbCrLf
        End With
        Username.Text = s

        'Dim config As System.Configuration.Configuration
        'config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
        'Dim configSection As System.Configuration.AppSettingsSection
        'configSection = CType(config.GetSection("appSettings"), System.Configuration.AppSettingsSection)

        'Dim customClearLogin As KeyValueConfigurationElement
        'customClearLogin = config.AppSettings.Settings("autoClearLogin")
        'Dim last As DateTime = Convert.ToDateTime(customClearLogin.Value)

        'Dim lastUpdate As New DateTime(543 + last.Year, last.Month, last.Day, last.Hour, last.Minute, last.Second)
        'Dim chkPoint As DateTime = DateAdd(DateInterval.Hour, 2, lastUpdate)
        'Dim nowStr As String = Now.Day & "/" & Now.Month & "/" & Now.Year & " " & Now.ToShortTimeString
        'customClearLogin.Value = nowStr

        'If Not configSection.SectionInformation.IsLocked Then
        '    config.Save()
        '    lbautoClearLogin.Text = "** Configuration updated."
        'Else
        '    lbautoClearLogin.Text = "** Could not update, section is locked."
        'End If
    End Sub
End Class
