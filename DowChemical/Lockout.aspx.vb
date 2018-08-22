
Imports Microsoft.AspNet.Identity

Partial Public Class LockoutRoot
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs)
        Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
        Response.Redirect("~/Account/Login")
    End Sub
End Class
