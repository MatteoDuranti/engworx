Imports System.Web.Mvc
Imports System
Imports Web.AppCode.Session
Imports log4net
Imports Web.AppCode.CustomProvider
Imports System.Collections.Generic
Imports System.Web.Routing
Imports EngWorxCore.Library.Session
Imports Domain
Imports System.ServiceModel
Imports ServiceProxy.WcfConsumer

Namespace Controllers

    Public Class BaseController
        Inherits Controller

        Protected Shared log As ILog = LogManager.GetLogger((System.Reflection.MethodBase.GetCurrentMethod().DeclaringType))

        Protected Overrides Sub OnActionExecuting(ByVal filterContext As ActionExecutingContext)
            MyBase.OnActionExecuting(filterContext)

            Dim fullActionName As String = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "." + filterContext.ActionDescriptor.ActionName
            log.Debug("INIT - " + fullActionName + " in " + filterContext.HttpContext.Request.HttpMethod())

            ' Check Breadcrumb
            If (filterContext.ActionDescriptor.GetCustomAttributes(GetType(BreadCrumbAttribute), True).Length > 0) Then
                Dim lst As List(Of BreadCrumb) = SessionManagerLib.getBreadCrumb()
                Dim rootElement As Boolean = False
                Dim firstElement As Boolean = False

                If (lst Is Nothing) Then
                    lst = New List(Of BreadCrumb)()
                    firstElement = True
                End If
                log.Debug("BreadCrumb Add to LIST - " + fullActionName + " in " + filterContext.HttpContext.Request.HttpMethod)
                Dim item As New BreadCrumb()
                For Each attr As Attribute In filterContext.ActionDescriptor.GetCustomAttributes(GetType(BreadCrumbAttribute), True)
                    item.description = DirectCast(attr, BreadCrumbAttribute).description.ToString()

                    If (filterContext.HttpContext.Request.QueryString("menu") = "1" OrElse DirectCast(attr, BreadCrumbAttribute).isRoot) Then
                        rootElement = True '
                    End If

                Next
                If String.IsNullOrEmpty(item.description) Then
                    item.description = "???"
                End If
                item.controller = "" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName
                item.action = "" + filterContext.ActionDescriptor.ActionName
                item.httpMethod = filterContext.HttpContext.Request.HttpMethod
                item.attributes = filterContext.ActionParameters

                ' Ricerca esistenza Controller/Action
                Dim index As Integer = lst.FindIndex(Function(x) x.action.Equals(item.action, StringComparison.OrdinalIgnoreCase) AndAlso
                                                                 x.controller.Equals(item.controller, StringComparison.OrdinalIgnoreCase))
                If (index = -1) Then
                    If (rootElement AndAlso Not firstElement) Then
                        lst.Insert(1, item)
                        If lst.Count > 2 Then
                            lst.RemoveRange(2, lst.Count - 2)
                        End If
                    Else
                        lst.Add(item)
                    End If
                Else
                    If (Not rootElement) Then
                        ' Rimuove gli elementi fino a
                        lst.RemoveRange(index, lst.Count - (index))
                        lst.Add(item)
                    Else
                        lst.Insert(1, item)
                        If (lst.Count > 2) Then
                            lst.RemoveRange(2, lst.Count - 2)
                        End If
                    End If
                End If
                SessionManagerLib.setBreadCrumb(lst)
            End If
            log.Debug("END - " + fullActionName + " in " + filterContext.HttpContext.Request.HttpMethod())
        End Sub

        Protected Overrides Sub OnActionExecuted(filterContext As ActionExecutedContext)
            MyBase.OnActionExecuted(filterContext)

            Dim fullActionName As String = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "." + filterContext.ActionDescriptor.ActionName
            log.Debug("INIT - " + fullActionName + " in " + filterContext.HttpContext.Request.HttpMethod())
            log.Debug("END - " + fullActionName + " in " + filterContext.HttpContext.Request.HttpMethod)
        End Sub

        Protected Overrides Sub OnAuthorization(ByVal filterContext As AuthorizationContext)
            MyBase.OnAuthorization(filterContext)

            Dim fullActionName As String = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "." + filterContext.ActionDescriptor.ActionName
            log.Debug("INIT - " + fullActionName)

            If (filterContext.ActionDescriptor.GetCustomAttributes(GetType(AuthorizePermissionBypass), True).Length > 0) Then
                log.Debug("ByPass Authorization: " + fullActionName)
            Else
                'Si controlla se la sessione è scaduta
                If (SessionManager.getUser() Is Nothing) Then
                    Dim b As Boolean = DirectCast(filterContext.RequestContext.HttpContext, System.Web.HttpContextWrapper).Request.IsAjaxRequest()
                    If (b) Then
                        'chiamata Ajax
                        filterContext.Result = New HttpStatusCodeResult(403)
                        Return
                    Else
                        'chiamata non Ajax
                        filterContext.Result = RedirectToAction("Logon", "Account", New With {.message = "Sessione scaduta. Si prega di effettuare di nuovo il login"})
                        Return
                    End If
                End If
                'Si controllano le autorizzazioni di esecuzione
                If (Not isAuthorized(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName)) Then
                    If (Request.IsAuthenticated()) Then
                        Dim username As String = "UNKNOWN"
                        If (SessionManager.getUser() IsNot Nothing) Then
                            username = SessionManager.getUser().CODUSR.Trim()
                        End If
                        log.Warn("UNAUTHORIZED USER: " + username + " on " + fullActionName)
                        filterContext.Result = New HttpUnauthorizedResult()
                        Throw (New UnauthorizedAccessException("Non si hanno i permessi per eseguire l'operazione selezionata [ " + fullActionName + " ]"))
                    End If
                End If
            End If
            log.Debug("END - " + fullActionName)
        End Sub

        <AuthorizePermissionBypass()> _
        Public Function BreadCrumbNavigation(ByVal pos As Integer) As ActionResult
            Dim breadcrumb As List(Of BreadCrumb) = SessionManagerLib.getBreadCrumb()
            Dim obj As New RouteValueDictionary()
            If "GET".Equals(breadcrumb(pos - 1).httpMethod) Then
                obj = New RouteValueDictionary(breadcrumb(pos - 1).attributes)
            ElseIf "POST".Equals(breadcrumb(pos - 1).httpMethod) Then
                obj.Add("postmethod", "1")
            End If
            Return RedirectToAction(breadcrumb(pos - 1).action, breadcrumb(pos - 1).controller, obj)
        End Function

        Protected Function isAuthorized(ByVal controller As String, ByVal action As String) As Boolean
            log.Debug("INIT")
            Dim result As Boolean = False
            'Dim ruoli As List(Of TROL) = WcfServices.getRolesByControllerAction(controller, action).ToList()
            Dim ruoli As List(Of TROL) = SessionManager.getUserRoles()

            Dim user As TUSR = SessionManager.getUser()
            If (user Is Nothing) Then
                Return False
            Else
                For Each r As TUSRROL In user.TUSRROL
                    If (BaseConstants.SYSTEM_ADMIN_ROLE.Equals(r.CODROL)) Then
                        Return True
                    End If
                Next
            End If

            For Each dbRole In ruoli
                For Each userRole In user.TUSRROL
                    If dbRole.CODROL = userRole.CODROL Then
                        For Each rolFunc In dbRole.TROLFNC
                            If rolFunc.TFNC.DESCTL <> "" AndAlso
                               rolFunc.TFNC.DESACTCTL <> "" AndAlso
                               rolFunc.TFNC.DESCTL.Trim.ToUpper.Equals(controller.Trim().ToUpper()) AndAlso
                               rolFunc.TFNC.DESACTCTL.Trim.ToUpper.Equals(action.Trim.ToUpper) Then
                                result = True
                                log.Debug("END")
                                Return result
                            End If
                        Next
                    End If
                Next
            Next

            'For Each role As TUSRROL In user.TUSRROL
            '    Dim r As TROL = role.TROL
            '    If ruoli.Find(Function(item) item.CODROL.Equals(r.CODROL)) IsNot Nothing Then
            '        result = True
            '        Exit For
            '    End If
            'Next
            log.Debug("END")
            Return result
        End Function

        Public Sub BreadCrumbError()
            If (Not SessionManagerLib.getBreadCrumb() Is Nothing) Then
                Dim lst As List(Of BreadCrumb) = SessionManagerLib.getBreadCrumb()
                Dim itemB As New BreadCrumb()
                itemB.description = "Errore"
                itemB.controller = "[Errore]"
                itemB.action = "[Errore]"
                If (Not DirectCast(lst.Last(), BreadCrumb).description = (itemB.description)) Then
                    lst.Add(itemB)
                End If
                SessionManagerLib.setBreadCrumb(lst)
            End If
        End Sub

        Public Sub BreadCrumbErrorAuth()
            If (Not SessionManagerLib.getBreadCrumb() Is Nothing) Then
                Dim lst As List(Of BreadCrumb) = SessionManagerLib.getBreadCrumb()
                Dim itemB As New BreadCrumb()
                itemB.description = "Autorizzazione Negata"
                itemB.controller = "[Auth]"
                itemB.action = "[Auth]"
                If (Not DirectCast(lst.Last(), BreadCrumb).description = (itemB.description)) Then
                    lst.Add(itemB)
                End If
                SessionManagerLib.setBreadCrumb(lst)
            End If
        End Sub

        Protected Overrides Sub OnException(ByVal filterContext As System.Web.Mvc.ExceptionContext)
            log.Debug("INIT")
            If (Not filterContext.ExceptionHandled) Then
                log.Error(filterContext.Exception)
                If (TypeOf (filterContext.Exception) Is System.ServiceModel.EndpointNotFoundException) Then
                    Dim exx As New Exception("Impossibile connettersi al servizio")
                    filterContext.Result = View("Error", exx)
                    BreadCrumbError()
                ElseIf (TypeOf (filterContext.Exception) Is UnauthorizedAccessException) Then
                    Dim b As Boolean = DirectCast(filterContext.RequestContext.HttpContext, System.Web.HttpContextWrapper).Request.IsAjaxRequest()
                    If (b) Then
                        'caso Ajax
                        filterContext.Result = View("ErrorPartial", filterContext.Exception)
                    Else
                        'caso non Ajax
                        filterContext.Result = View("Error", filterContext.Exception)
                    End If
                    BreadCrumbErrorAuth()
                Else
                    filterContext.Result = View("Error", filterContext.Exception)
                    BreadCrumbError()
                End If
                'BreadCrumbError()
                filterContext.ExceptionHandled = True
            End If
            MyBase.OnException(filterContext)
            log.Debug("END")
        End Sub
    End Class
End Namespace
