Imports System
Imports System.Web.Mvc
Imports System.Web
Imports Web.Models
Imports Web.AppCode.Session
Imports Web.AppCode.Combo
Imports Domain
Imports ServiceProxy.WcfConsumer

Namespace Controllers
    Public Class RoleController
        Inherits BaseController
#Region "Create"

        <HttpGet()> _
        <BreadCrumb("Aggiungi Ruolo")> _
        Public Function Create() As ActionResult
            Return View()
        End Function
        <HttpPost()> _
        Public Function Create(role As TROL) As ActionResult
            '' UPPER CASE CAMPI
            role.CODROL = role.CODROL.ToUpper()
            role.DESROL = role.DESROL.ToUpper()
            If (WcfServices.GetRoleByPrimaryKey(role) IsNot Nothing) Then

                ModelState.AddModelError("DuplicateRole", "Ruolo " & role.CODROL & " già presente nella Banca Dati.")
                Return View("Create", role)
            End If
            '' Assegno il nuovo codice

            If (Not WcfServices.InsertRole(role)) Then
                '' TODO : Eventuale gestione errore
            End If
            Return RedirectToAction("Search", New With {.from = "create"})
        End Function
#End Region

#Region "Search"
        <BreadCrumb("Gestione Ruoli", True)> _
        Public Function Search(Optional page As Integer = 0, Optional sort As String = "", Optional sortDir As String = "", Optional from As String = "", Optional postmethod As Integer = 0) As ActionResult
            Dim c As New RoleSearchViewModel()
            If page = 0 AndAlso "".Equals(sort) Then
                If "".Equals(from) AndAlso postmethod <> 1 Then
                    ' Provengo dal menù
                    SessionManager.setRolesSearchParameter(Nothing)
                ElseIf "edit".Equals(from) OrElse "delete".Equals(from) OrElse "create".Equals(from) OrElse postmethod = 1 Then
                    c = SessionManager.getRolesSearchParameter()
                    ' From delete
                    If "delete".Equals(from) Then
                        c.DataParameters.pageNumber = 1
                        SessionManager.setRolesSearchParameter(c)
                    End If
                    Dim conteggio As Integer = 0

                    c.Data = WcfServices.GetFilteredRoles(c.roleParameters.SearchParameters, c.roleParameters.DataParameters.sort, c.roleParameters.DataParameters.sortDir, c.roleParameters.DataParameters.pageNumber, c.roleParameters.DataParameters.pageSize, conteggio)
                    c.DataParameters.totalRows = conteggio
                End If
            Else
                ' Ordinamento o paginazione
                c = SessionManager.getRolesSearchParameter()
                If page <> 0 Then
                    c.DataParameters.pageNumber = page
                End If
                If Not "".Equals(sort) Then
                    c.DataParameters.sort = sort
                    c.DataParameters.sortDir = sortDir
                End If
                SessionManager.setRolesSearchParameter(c)
                Dim conteggio As Integer = 0

                c.Data = WcfServices.GetFilteredRoles(c.roleParameters.SearchParameters, c.roleParameters.DataParameters.sort, c.roleParameters.DataParameters.sortDir, c.roleParameters.DataParameters.pageNumber, c.roleParameters.DataParameters.pageSize, conteggio)
                c.DataParameters.totalRows = conteggio
            End If
            Return View(c)
        End Function

        <HttpPost()> _
        <BreadCrumb("Gestione Ruoli", True)> _
        Public Function Search(rolevm As RoleSearchViewModel) As ActionResult
            ModelState.Clear()
            SessionManager.setRolesSearchParameter(rolevm)
            Dim conteggio As Integer = 0
            rolevm.Data = WcfServices.GetFilteredRoles(rolevm.roleParameters.SearchParameters, rolevm.roleParameters.DataParameters.sort, rolevm.roleParameters.DataParameters.sortDir, rolevm.roleParameters.DataParameters.pageNumber, rolevm.roleParameters.DataParameters.pageSize, conteggio)
            rolevm.DataParameters.totalRows = conteggio
            Return View(rolevm)
        End Function
#End Region

#Region "Delete"
        Public Function Delete(codrol As String) As ActionResult
            Dim r As New TROL()
            r.CODROL = codrol
            If (Not WcfServices.DeleteRole(r, False)) Then
                ' GET FALSE
            End If
            Return RedirectToAction("Search", New With {.from = "delete"})
        End Function
#End Region

#Region "Edit Role"
        <HttpGet()> _
        <BreadCrumb("Modifica Ruolo")> _
        Public Function Edit(codrol As String) As ActionResult
            Return View(WcfServices.GetRoleByPrimaryKey(New TROL With {.CODROL = codrol}))
        End Function

        <HttpPost()> _
        Public Function Edit(role As TROL) As ActionResult
            '' UPPER CASE CAMPI
            role.DESROL = role.DESROL.ToUpper()
            WcfServices.UpdateRole(role)
            Return RedirectToAction("Search", New With {.from = "edit"})
        End Function
#End Region

#Region "Associate Function to Role"
        ''' <summary>
        ''' Associazione ad un ruolo le funzioni ( controller/action ) eseguibili
        ''' L'Action può essere richiamata sia dal menu che dalla griglia dei ruoli
        ''' Nel caso venga richiamata dal menu deve rendere visibile la selezione dei ruoli
        ''' altrimenti visualizza direttamente le funzioalità del ruolo scelto dalla griglia 
        ''' Anche per questo duplice ingresso si è scelto di utlizzare un objcet e non un guid come
        ''' firma del metodo.
        ''' </summary>
        ''' <param name="codruo">Codice Ruolo</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <HttpGet()> _
        <BreadCrumb("Associazione Ruolo/Funzioni")> _
        Public Function AssociateFunctionsToRole(Optional ByVal codrol As String = "") As ActionResult
            Dim rfavm As New RoleFunctionsAssociationsViewModel()
            If (Not String.IsNullOrEmpty(codrol)) Then
                '' Proviene dalla griglia dei ruoli
                rfavm.roleFunsParameters.DesRuolo = WcfServices.GetRoleByPrimaryKey(New TROL With {.CODROL = codrol}).DESROL
                rfavm.Data = WcfServices.GetFunctionsWithPermissions(New TROL With {.CODROL = codrol})
            Else
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '' proviene dal menù; quindi l'utente può selezionare il ruolo ''
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'rfavm.roleFunsParameters.RoleList = CombosManager.FillRoles(_repositoryRoles.GetAll())
                rfavm.roleFunsParameters.RoleList = CombosManager.FillRoles(WcfServices.GetAllRoles().ToList())
            End If
            rfavm.roleFunsParameters.CODROL = codrol.ToString
            Return View(rfavm)
        End Function

        <HttpPost()> _
        <BreadCrumb("Associazione Ruolo/Funzioni")> _
        Public Function AssociateFunctionsToRole(ByVal rolevm As RoleFunctionsAssociationsViewModel) As ActionResult
            Dim rfavm As New RoleFunctionsAssociationsViewModel()
            'rfavm.roleFunsParameters.RoleList = CombosManager.FillRoles(_repositoryRoles.GetAll(), RolesDropDownList.ToString())

            If (Not String.IsNullOrEmpty(rolevm.roleFunsParameters.SelectedRole)) Then
                rfavm.roleFunsParameters.RoleList = CombosManager.FillRoles(WcfServices.GetAllRoles.ToList(), rolevm.roleFunsParameters.SelectedRole.ToString())
                'rfavm.Data = _repositoryFunctions.GetFuncionsWithPermissions(RolesDropDownList)
                rfavm.Data = WcfServices.GetFunctionsWithPermissions(New TROL With {.CODROL = rolevm.roleFunsParameters.SelectedRole.ToString()})
                rfavm.roleFunsParameters.CODROL = rolevm.roleFunsParameters.SelectedRole.ToString()
            Else
                rfavm.roleFunsParameters.RoleList = CombosManager.FillRoles(WcfServices.GetAllRoles.ToList())
            End If
            Return View(rfavm)
        End Function
#End Region

#Region "WebMethod"
        <AuthorizePermissionBypass()> _
        <OutputCache(NoStore:=True, Duration:=0, VaryByParam:="*")> _
        Public Function AddRemovePermission(codrol As String, codfnc As String) As IHtmlString
            Dim result As String = String.Empty
            Try
                Dim insertedElement As Boolean = WcfServices.InsertDeleteAssociationFunctToRole(New TROLFNC With {.CODROL = codrol, .CODFNC = codfnc})
                If Not insertedElement Then
                    Dim span As New TagBuilder("span")
                    result = span.ToString()
                    log.Info("Cancellazione permesso della funzione " & codfnc.ToString & " al ruolo " & codrol.ToString)
                Else
                    Dim img As New TagBuilder("image")
                    img.Attributes.Add("src", Url.Content("~/Images/check.gif"))
                    img.Attributes.Add("alt", "Permesso concesso")
                    img.Attributes.Add("title", "Permesso concesso")
                    result = img.ToString()
                    log.Info("Inserimento permesso della funzione " & codfnc.ToString & " al ruolo " & codrol.ToString)
                End If
            Catch ex As Exception
                log.[Error]("ERROR: " + ex.Message, ex)
                Throw New Exception()
            End Try
            Return New HtmlString(result)
        End Function

#End Region

    End Class
End Namespace
