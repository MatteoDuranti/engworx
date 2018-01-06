Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics
Imports log4net
Imports System.Web.Security
Imports System.Web

Namespace AppCode.CustomProvider
    Public Class FormsAuthenticationService
        Implements IFormsAuthentication
        Shared log As ILog = LogManager.GetLogger((System.Reflection.MethodBase.GetCurrentMethod().DeclaringType))

        Public Sub SignIn(username As String) Implements IFormsAuthentication.SignIn
            log.Debug("INIT")
            FormsAuthentication.SetAuthCookie(username, False)
            log.Debug("END")
        End Sub

        Public Sub SignIn(company As String, username As String) Implements IFormsAuthentication.SignIn
            log.Debug("INIT")
            FormsAuthentication.SetAuthCookie(company & username, False)
            log.Debug("END")
        End Sub

        Public Sub SignOut() Implements IFormsAuthentication.SignOut
            log.Debug("INIT")
            FormsAuthentication.SignOut()
            HttpContext.Current.Session.Clear()
            HttpContext.Current.Session.Abandon()
            log.Debug("END")
        End Sub
    End Class
End Namespace