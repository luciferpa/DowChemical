Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports AiLib

Imports System.Net.Mail
Imports System.Configuration

Public Class cSendMail
    Dim ConnStr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
    Dim DomainUrl As String = "https://www.dowezpath.com"
    'Dim DomainUrl As String = "http://203.154.175.166"

    Public Function PopulateBody(ByVal Template As String, ByVal Department As String, ByVal ActionNumber As String, ByVal ReporterStr As String, ByVal Detail() As String, ByVal Picture() As String, ByVal ProposeActionA As String, ByVal ResponPersonA As String, ByVal OtherPropose() As String, ByVal recId As String) As String
        'Detail() = {ObserveNo, Title, Category, CategorySub, FailurePoint, Equipment, HRO, ObservedType, Description}
        'Picture() = {UrlPic1, UrlPic2, UrlPic3, UrlPic4}
        'OtherPropose() = {ProposeActionB, ResponPersonB, ProposeActionC, ResponPersonC}
        If Template = "" Then Template = "observeIssue.html"
        Dim body As String = String.Empty
        Dim reader As StreamReader = New StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/" & Template))
        body = reader.ReadToEnd
        body = body.Replace("{Department}", Department)
        body = body.Replace("{ActionNumber}", ActionNumber)

        body = body.Replace("{ObserveNo}", Detail(0))
        'body = body.Replace("{Title}", Detail(1))
        body = body.Replace("{Category}", Detail(2))
        body = body.Replace("{CategorySub}", Detail(3))
        body = body.Replace("{FailurePoint}", Detail(4))
        body = body.Replace("{Equipment}", Detail(5))
        body = body.Replace("{HRO}", Detail(6))
        'body = body.Replace("{ObservedType}", Detail(7))
        body = body.Replace("{Description}", Detail(8))

        If Picture(0) <> "" Then body = body.Replace("{imgUrl1}", DomainUrl & Picture(0).Substring(1)) Else body = body.Replace("{imgUrl1}", "")
        If Picture(1) <> "" Then body = body.Replace("{imgUrl2}", DomainUrl & Picture(1).Substring(1)) Else body = body.Replace("{imgUrl2}", "")
        If Picture(2) <> "" Then body = body.Replace("{imgUrl3}", DomainUrl & Picture(2).Substring(1)) Else body = body.Replace("{imgUrl3}", "")
        If Picture(3) <> "" Then body = body.Replace("{imgUrl4}", DomainUrl & Picture(3).Substring(1)) Else body = body.Replace("{imgUrl4}", "")

        body = body.Replace("{ProposeActionA}", ProposeActionA)
        body = body.Replace("{ResponA}", ResponPersonA)

        If OtherPropose(0) <> "" Then
            Dim strMsg1 As String = ""
            strMsg1 = strMsg1 & "<tr Class='alt'>"
            strMsg1 = strMsg1 & "<td style = 'width: 220px; text-align: right; vertical-align: top; padding-top: 4px; font: bold;'> Propose Action #2 :</td>"
            strMsg1 = strMsg1 & "<td><textarea style = 'width: 400px; height: 88px;' rows='2' cols='20'>" & OtherPropose(0) & "</textarea></td>"
            strMsg1 = strMsg1 & "</tr>"
            body = body.Replace("{ProposeActionB}", strMsg1)

            Dim strMsg2 As String = ""
            strMsg2 = strMsg2 & "<tr>"
            strMsg2 = strMsg2 & "<td style = 'width: 220px; text-align: right; font: bold;'> Responsible Person #2 :</td>"
            strMsg2 = strMsg2 & "<td>" & OtherPropose(1) & "</td>"
            strMsg2 = strMsg2 & "</tr>"
            body = body.Replace("{ResponB}", strMsg2)
        Else
            body = body.Replace("{ProposeActionB}", "")
            body = body.Replace("{ResponB}", "")
        End If
        If OtherPropose(2) <> "" Then
            Dim strMsg1 As String = ""
            strMsg1 = strMsg1 & "<tr Class='alt'>"
            strMsg1 = strMsg1 & "<td style = 'width: 220px; text-align: right; vertical-align: top; padding-top: 4px; font: bold;'> Propose Action #3 :</td>"
            strMsg1 = strMsg1 & "<td><textarea style = 'width: 400px; height: 88px;' rows='2' cols='20'>" & OtherPropose(2) & "</textarea></td>"
            strMsg1 = strMsg1 & "</tr>"
            body = body.Replace("{ProposeActionC}", strMsg1)

            Dim strMsg2 As String = ""
            strMsg2 = strMsg2 & "<tr>"
            strMsg2 = strMsg2 & "<td style = 'width: 220px; text-align: right; font: bold;'> Responsible Person #3 :</td>"
            strMsg2 = strMsg2 & "<td>" & OtherPropose(3) & "</td>"
            strMsg2 = strMsg2 & "</tr>"
            body = body.Replace("{ResponC}", strMsg2)
        Else
            body = body.Replace("{ProposeActionC}", "")
            body = body.Replace("{ResponC}", "")
        End If

        body = body.Replace("{Reporter}", ReporterStr)

        Dim Encrypt As New cEncrypt
        Dim recordStr As String = recId & "&obitem=" & (CInt(Detail(0)) - 1).ToString
        Dim linkUrl As String = DomainUrl & "/detail/observeDetail?rid="
        linkUrl = linkUrl & Encrypt.Encrypt_aiPass(recordStr)
        body = body.Replace("{Link}", linkUrl)

        Return body
    End Function

    Dim errorMsgStr As String = ""
    Public Function ErrorMsg() As String
        Return errorMsgStr
    End Function

    Public Sub SendHtmlFormattedEmail(ByVal recepientEmail As ArrayList, ByVal subject As String, ByVal body As String)
        '-- gmail setting
        '<appSettings>
        '      <addkey= "Host"value="smtp.gmail.com"/>
        '      <addkey= "EnableSsl"value="true"/>
        '      <addkey= "UserName"value="noReply@gmail.com"/>
        '      <addkey= "Password"value="xxxxx"/>
        '      <addkey= "Port"value="587"/>
        '</appSettings>

        'Dim SmtpMail As SmtpClient = New SmtpClient
        'SmtpMail.Host = ConfigurationManager.AppSettings("Host")
        'SmtpMail.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings("EnableSsl"))
        'Dim NetworkCred As System.Net.NetworkCredential = New System.Net.NetworkCredential
        'NetworkCred.UserName = ConfigurationManager.AppSettings("UserName")
        'NetworkCred.Password = ConfigurationManager.AppSettings("Password")
        'SmtpMail.UseDefaultCredentials = True
        'SmtpMail.Credentials = NetworkCred
        'SmtpMail.Port = Integer.Parse(ConfigurationManager.AppSettings("Port"))

        '-- chaiyo setting
        'Dim SmtpMail As SmtpClient = New SmtpClient
        'SmtpMail.Host = "mail.dowezpath.com"         '"fezpath@dow.com"
        'SmtpMail.EnableSsl = False
        'Dim NetworkCred As System.Net.NetworkCredential = New System.Net.NetworkCredential
        'NetworkCred.UserName = "noReply@dowezpath.com"          '"fezpath@dow.com"
        'NetworkCred.Password = "7a2nF!p6"
        'SmtpMail.UseDefaultCredentials = True
        'SmtpMail.Credentials = NetworkCred
        'SmtpMail.Port = 25

        '-- aplus setting
        Dim SmtpMail As SmtpClient = New SmtpClient
        SmtpMail.Host = "smtp.inetmail.cloud" 
        SmtpMail.EnableSsl = False
        Dim NetworkCred As System.Net.NetworkCredential = New System.Net.NetworkCredential
        NetworkCred.UserName = "noReply@dowezpath.com"
        NetworkCred.Password = "FX4Qb3JU"
        SmtpMail.UseDefaultCredentials = True
        SmtpMail.Credentials = NetworkCred
        SmtpMail.Port = 25

        If recepientEmail.Count > 0 Then
            Dim mailMessage As MailMessage = New MailMessage
            'mailMessage.From = New MailAddress(ConfigurationManager.AppSettings("UserName"))
            mailMessage.From = New MailAddress(NetworkCred.UserName)
            mailMessage.Subject = subject
            mailMessage.Body = body
            mailMessage.IsBodyHtml = True

            'create mail to and check repeat
            Dim emailList As New ArrayList
            For i As Integer = 0 To recepientEmail.Count - 1 Step 2
                Dim IsRepeat As Boolean = False
                For Each m As String In emailList
                    If recepientEmail(i) = m Then IsRepeat = True
                Next
                If Not IsRepeat Then
                    emailList.Add(recepientEmail(i))
                    mailMessage.To.Add(New MailAddress(recepientEmail(i), recepientEmail(i + 1)))
                End If
            Next

            SmtpMail.Send(mailMessage)
        End If
    End Sub

    Private Sub SetSmtpMail()
        Dim SmtpMail As New System.Net.Mail.SmtpClient("smtp.gmail.com")
        Dim Credential As New System.Net.NetworkCredential("account@gmail.com", "password")
        SmtpMail.UseDefaultCredentials = False
        SmtpMail.EnableSsl = True
        SmtpMail.Credentials = Credential
        SmtpMail.Port = 465
    End Sub


End Class
