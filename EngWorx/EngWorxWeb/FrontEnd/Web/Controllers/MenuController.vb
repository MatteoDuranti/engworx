Imports System
Imports System.Collections.Generic
Imports System.Web.Mvc
Imports Web.AppCode.Session
Imports System.Web.Routing
Imports Web.Models
Imports System.Web.Security
Imports Domain
Imports ServiceProxy.WcfConsumer
Imports Web.AppCode.Utility

Namespace Controllers
    Public Class MenuController
        Inherits BaseController
        'Private _repository As IMenuRepository = Nothing

        Public Sub New()
            '_repository = New MenuRepository(_db)
        End Sub

        ' GET: /Menu/Index
        <AuthorizePermissionBypass()> _
        Public Function Index() As ActionResult
            Try
                Dim sessionuser As TUSR = SessionManager.getUser()
                Dim m As New List(Of NavLink)()
                If ((sessionuser Is Nothing) AndAlso (HttpContext.Request.IsAuthenticated = False)) Then
                    Return View(LoadFromUser(False))
                Else
                    Return View(LoadFromUser(True))
                End If
            Catch ex As Exception
                log.Error(ex.Message, ex)
                Return View("Error", ex)
            End Try
        End Function

        Private Function LoadFromUser(draw As Boolean) As MenuViewModel
            Dim menu As New MenuViewModel()
            Dim route As RouteValueDictionary = Nothing
            Dim text As String = Nothing

            Dim u As TUSR = SessionManager.getUser()
            If (draw AndAlso u IsNot Nothing AndAlso u.CODSTSUSR IsNot Nothing AndAlso u.CODSTSUSR.Trim().ToUpper().Equals("A") AndAlso Not SessionManager.getCatchedDeniedAccess()) Then
                ' Load Menu header
                Dim menuSession = SessionManager.getMenu
                If (menuSession Is Nothing) Then
                    '' Workaround
                    ''''' There was an error while trying to serialize parameter http://tempuri.org/:user. 
                    ''''' Object graph for type 'Domain.TUTERUO' contains cycles and cannot be serialized if reference tracking is disabled.
                    Dim menuList As IList(Of VMEN) = WcfServices.GetMenuByUser(New TUSR With {.CODUSR = u.CODUSR})
                    For Each men As VMEN In menuList
                        text = Convert.ToString(men.DESFNC.Trim())
                        If [String].IsNullOrEmpty(men.DESCTL) OrElse [String].IsNullOrEmpty(men.DESACTCTL) Then
                            route = Nothing
                        Else
                            route = New RouteValueDictionary(New With {.controller = men.DESCTL.Trim().ToLower(), .action = men.DESACTCTL.Trim().ToLower()})
                        End If
                        menu.menu.Add(New NavLink(route, text, men.CODLVL, men.CODFNCFAT))
                    Next

                    'menu.menu.Add(New NavLink(New RouteValueDictionary(New With {.controller = "menu", .action = "guide"}), "Manuale", 1, "000"))

                    SessionManager.setMenu(menu)
                Else
                    menu = menuSession
                End If
            Else
                FormsAuthentication.SignOut()
                SessionManager.cleanSession()
            End If
            Return menu
        End Function

        <AuthorizePermissionBypass()> _
        Public Function Guide() As FilePathResult
            Return File("/guide.pdf", Utility.getMimeType("pdf"), "guide.pdf")
        End Function
    End Class
End Namespace
