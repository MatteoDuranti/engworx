Imports System
Imports System.Web.Mvc
Imports System.Web
Imports Web.Models
Imports Web.AppCode.Session
Imports Web.AppCode.Combo
Imports Domain
Imports ServiceProxy.WcfConsumer
Imports System.IO
Imports System.Web.Configuration

Namespace Controllers
    Public Class HomeController
        Inherits BaseController

        ' GET: /Home/Index
        <AuthorizePermissionBypass()> _
        <BreadCrumb("Home")> _
        Public Function Index() As ActionResult
            If (SessionManager.getUser() Is Nothing) Then
                Return RedirectToAction("LogOn", "Account")
            End If
            Return View()
        End Function
    End Class
End Namespace
