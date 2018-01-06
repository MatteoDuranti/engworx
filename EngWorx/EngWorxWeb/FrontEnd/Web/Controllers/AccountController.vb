Imports System
Imports System.Collections.Generic
Imports System.Web.Mvc
Imports System.Web.Security
Imports System.Security.Principal
Imports Web.AppCode.Combo
Imports Web.AppCode.CustomProvider
Imports Web.AppCode.Session
Imports Web.Models
Imports Domain
Imports ServiceProxy.WcfConsumer

Namespace Controllers
    Public Class AccountController
        Inherits BaseController

        Private _formsAuth As IFormsAuthentication = Nothing
        Private _service As IMembershipService

        Public Sub New()
            Me.New(Nothing, Nothing)
        End Sub

        Public Sub New(ByVal formsAuth As IFormsAuthentication, ByVal service As IMembershipService)
            _formsAuth = If(formsAuth, New FormsAuthenticationService())
            _service = If(service, New AccountMembershipService())
        End Sub

        Public ReadOnly Property FormsAuth() As IFormsAuthentication
            Get
                Return _formsAuth
            End Get
        End Property

        Public ReadOnly Property MembershipService() As IMembershipService
            Get
                Return _service
            End Get
        End Property

        <AuthorizePermissionBypass()> _
        Public Function LogOn(Optional message As String = "") As ActionResult
            Try
                Dim user As TUSR = SessionManager.getUser()
                Dim catchedDeniedAccess As Boolean = SessionManager.getCatchedDeniedAccess()
                If (user IsNot Nothing AndAlso catchedDeniedAccess) Then
                    Return View(New LogonFormViewModel(CombosManager.FillDomains("USER"), "Tentativo di accesso non consentito"))
                Else
                    Return View(New LogonFormViewModel(CombosManager.FillDomains("USER"), message))
                End If
            Catch ex As Exception
                log.[Error](ex.Message, ex)
                Return View("Error", ex)
            End Try
        End Function

        Private Function dologon(username As String, password As String, domain As String) As ActionResult
            Dim result As ActionResult = Nothing
            If (Not WcfServices.CheckDbConnection()) Then
                result = View(New LogonFormViewModel(CombosManager.FillDomains(), "Il collegamento con i sistemi aziendali non è temporaneamente disponibile. Si prega di riprovare più tardi."))
            ElseIf (Not MembershipService.ValidateUser(username, password, domain)) Then
                result = View(New LogonFormViewModel(CombosManager.FillDomains(), "Utente sconosciuto, controllare Username e Password fornite"))
            ElseIf ((SessionManager.getUser() Is Nothing) OrElse (SessionManager.getUser().CODSTSUSR Is Nothing) OrElse ("D".Equals(SessionManager.getUser().CODSTSUSR.Trim().ToUpper()))) Then
                result = View(New LogonFormViewModel(CombosManager.FillDomains(), "Utente inesistente o disabilitato."))
            Else
                Dim TSYSPAR As TSYSPAR = WcfServices.GetSysParByPrimaryKey(New TSYSPAR With {.CODSYSPAR = "APP_SEMAPHORE", .NUMSYSPARIDX = 0})
                If (TSYSPAR Is Nothing) Then
                    'Semaforo non esistente
                    FormsAuth.SignOut()
                    result = View(New LogonFormViewModel(CombosManager.FillDomains(), "Semaforo applicativo non presente."))
                ElseIf ("1".Equals(TSYSPAR.DESPARVAL)) Then
                    'Semaforo occupato
                    FormsAuth.SignOut()
                    result = View(New LogonFormViewModel(CombosManager.FillDomains(), "Semaforo applicativo occupato."))
                Else
                    ' Tutto OK
                    Dim ruoli As List(Of TROL) = WcfServices.GetRolesAndAllowedFunctions(New TUSR())
                    SessionManager.setUserRoles(ruoli)
                    FormsAuth.SignIn(username)
                    result = RedirectToAction("Index", "Home")
                End If
            End If
            Return result
        End Function

        <AuthorizePermissionBypass()> _
        <AcceptVerbs(HttpVerbs.Post)> _
        Public Function LogOn(username As String, password As String, domain As String, ReturnUrl As String, matricola As String, nome As String, cognome As String, email As String, telefono As String, ente As String) As ActionResult
            If (Not String.IsNullOrEmpty(nome) And Not String.IsNullOrEmpty(cognome) And Not String.IsNullOrEmpty(email) And Not String.IsNullOrEmpty(telefono) And Not String.IsNullOrEmpty(ente)) Then
                Dim mess As String = String.Format("Tentativo di Accesso SSO {0} per questa risorsa: {1}", "non consentito", Request.UrlReferrer.ToString())
                Dim CheckReferrer As Boolean = False
                Boolean.TryParse(ConfigurationManager.AppSettings("CheckReferrer"), CheckReferrer)

                If (CheckReferrer) Then
                    Dim r As String() = ConfigurationManager.AppSettings("ReferrerList").Split(Convert.ToChar(","))
                    If (Array.Find(r, Function(s As String) s.Contains(Request.UrlReferrer.ToString().Substring(0, Request.UrlReferrer.ToString().Substring(7).IndexOf("/") + 7))) Is Nothing) Then
                        'referral non trovato nella lista.
                        log.Warn(mess)
                        log.Warn(String.Format("nome:{0},cognome:{1},email:{2},telefono:{3},ente:{4}", nome, cognome, email, telefono, ente))
                        Throw New Exception(mess)
                    End If
                End If
                username = matricola.Substring(2)
                password = Nothing
                domain = ConfigurationManager.AppSettings("ssoDomain")
                log.Info(String.Format("Tentativo di Accesso {0}per questa risorsa: {1}", "", Request.UrlReferrer.ToString()))
                log.Info(String.Format("Username:{1},Pass:{2},Domain:{3},Email:{4}", username, password, domain, email))
                Return dologon(username, password, domain)
            Else
                If (Not String.IsNullOrEmpty(username) And Not String.IsNullOrEmpty(password) And Not String.IsNullOrEmpty(domain)) Then
                    Return dologon(username, password, domain)
                End If
            End If
            Throw New Exception("Tentativo di Accesso non corretto o non consentito")
        End Function

        <AuthorizePermissionBypass()> _
        Public Function LogOff() As ActionResult
            FormsAuth.SignOut()
            Return RedirectToAction("LogOn", "Account")
        End Function
    End Class
End Namespace
