Imports System
Imports System.Web.Mvc
Imports Web.Models
Imports System.Collections.Generic
Imports Web.AppCode.Session
Imports Domain
Imports Web.AppCode.Combo
Imports ServiceProxy.WcfConsumer

Namespace Controllers
    Public Class UserController
        Inherits BaseController

#Region "Search Users"

        ' GET: /Users/  
        '[OutputCache(Duration = 600, VaryByParam = "page;sort;sortDir")]
        <BreadCrumb("Gestione Utenti", True)> _
        Public Function Search(Optional page As Integer = 0, Optional sort As String = "", Optional sortDir As String = "", Optional gridsrc As String = "", Optional from As String = "", Optional postmethod As Integer = 0) As ActionResult
            Dim c As New UserSearchViewModel()
            If page = 0 AndAlso "".Equals(sort) Then
                If "".Equals(from) AndAlso postmethod <> 1 Then
                    ' Provengo dal menù
                    SessionManager.setUsersSearchParameter(Nothing)
                ElseIf "edit".Equals(from) OrElse "delete".Equals(from) OrElse "create".Equals(from) OrElse postmethod = 1 Then
                    c = SessionManager.getUsersSearchParameter()
                    ' From delete
                    If "delete".Equals(from) Then
                        c.DataParameters.pageNumber = 1
                        SessionManager.setUsersSearchParameter(c)
                    End If
                    Dim conteggio As Integer = 0

                    c.Data = WcfServices.GetFilteredUsers(c.userParameters.SearchParameters, c.userParameters.DataParameters.sort, c.userParameters.DataParameters.sortDir, c.userParameters.DataParameters.pageNumber, c.userParameters.DataParameters.pageSize, conteggio)
                    c.DataParameters.totalRows = conteggio
                End If
            Else
                ' Ordinamento o paginazione
                c = SessionManager.getUsersSearchParameter()
                If page <> 0 Then
                    c.DataParameters.pageNumber = page
                End If
                If Not "".Equals(sort) Then
                    c.DataParameters.sort = sort
                    c.DataParameters.sortDir = sortDir
                End If
                SessionManager.setUsersSearchParameter(c)
                Dim conteggio As Integer = 0
                c.Data = WcfServices.GetFilteredUsers(c.userParameters.SearchParameters, c.userParameters.DataParameters.sort, c.userParameters.DataParameters.sortDir, c.userParameters.DataParameters.pageNumber, c.userParameters.DataParameters.pageSize, conteggio)
                c.DataParameters.totalRows = conteggio
            End If
            ViewData("UserStates") = CombosManager.FillUserStates()
            Return View(c)
        End Function

        <HttpPost()> _
        <BreadCrumb("Gestione Utenti", True)> _
        Public Function Search(uservm As UserSearchViewModel) As ActionResult
            '' Workaround sulla ricerca per la validazione
            '''''''''''''''''''''''''''''''''''''''''''''''
            ModelState.Clear()
            Dim conteggio As Integer = 0
            uservm.Data = WcfServices.GetFilteredUsers(uservm.userParameters.SearchParameters, uservm.userParameters.DataParameters.sort, uservm.userParameters.DataParameters.sortDir, uservm.userParameters.DataParameters.pageNumber, uservm.userParameters.DataParameters.pageSize, conteggio)
            uservm.DataParameters.totalRows = conteggio
            SessionManager.setUsersSearchParameter(uservm)
            ViewData("UserStates") = CombosManager.FillUserStates()
            Return View(uservm)

        End Function
#End Region

#Region "Edit User"
        <HttpGet()> _
        <BreadCrumb("Modifica Utente")> _
        Public Function Edit(codusr As String) As ActionResult
            ViewData("UserStates") = CombosManager.FillUserStates()
            Return View(WcfServices.GetUserByPrimaryKey(New TUSR With {.CODUSR = codusr}))

        End Function

        <HttpPost()> _
        Public Function Edit(user As TUSR) As ActionResult
            If (ModelState.IsValid()) Then
                'Dim usr As TUTE = _repositoryUsers.GetByPrimaryKey(user.CODUTEID0)
                Dim usr As TUSR = WcfServices.GetUserByPrimaryKey(user)
                '' Nel caso di modifica CODMAT o CODAZI controllo se l'utente è gia presente nella banca dati ( per CODAZI/CODMAT )
                If (Not usr.CODUSR.Trim().Equals(user.CODUSR.Trim())) Then
                    'If (_repositoryUsers.GetUserRolesByCompanyAndUsername(user.CODAZI, user.CODMAT) IsNot Nothing) Then
                    If (WcfServices.GetUserRolesByCompanyAndUsername(user) IsNot Nothing) Then
                        ViewData("UserStates") = CombosManager.FillUserStates()
                        ModelState.AddModelError("DuplicateUser", "Utente " & user.CODUSR & " già presente nella Banca Dati.")
                        Return View("Edit", user)
                    End If
                End If
                usr.DESFSTNAMUSR = user.DESFSTNAMUSR
                usr.DESLSTNAMUSR = user.DESLSTNAMUSR
                usr.CODUSR = user.CODUSR
                usr.CODSTSUSR = user.CODSTSUSR
                usr.DESTELUSR = user.DESTELUSR
                usr.DESENYUSR = user.DESENYUSR
                usr.DESEMLUSR = user.DESEMLUSR
                If (WcfServices.UpdateUser(usr)) Then

                End If
                Return RedirectToAction("Search", New With {.from = "edit"})
            Else
                ModelState.AddModelError("Errore", "Errore in inserimento dati")
                Return View(user)
            End If
        End Function
#End Region

#Region "Create User"
        <HttpGet()> _
        <BreadCrumb("Aggiungi Utente")> _
        Public Function Create() As ActionResult
            ViewData("UserStates") = CombosManager.FillUserStates()
            Return View()
        End Function

        <HttpPost()> _
        Public Function Create(user As TUSR) As ActionResult
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '' IMPORTANT! TODO                                                          ''''''''''''''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '' Fare un metodo ad hoc per questa richiesta in quanto il seguente oltre a ritornare   ''
            '' l'utente, ritorna anche la lista dei ruoli ( TUTERUO )                               ''
            '' Creare sui servizio ( ed anche nel repository ) un metodo che dato in ingresso       ''
            '' CODAZI e CODMAT ritorna true/false                                                   ''
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            '' uppercase del codice utente ( codusr )
            user.CODUSR = user.CODUSR.ToUpper()
            '' uppercase del nome e del cognome
            user.DESFSTNAMUSR = user.DESFSTNAMUSR.ToUpper()
            user.DESLSTNAMUSR = user.DESLSTNAMUSR.ToUpper()

            '' Controllo se il codice già esiste
            ''''''''''''''''''''''''''''''''''''
            If (WcfServices.GetUserRolesByCompanyAndUsername(user) IsNot Nothing) Then
                ViewData("UserStates") = CombosManager.FillUserStates()
                ModelState.AddModelError("DuplicateUser", "Utente " & user.CODUSR & " già presente nella Banca Dati.")
                Return View("Create", user)
            End If
            '' Assegno il nuovo codice

            user.DATLSTLOG = System.DateTime.Now
            user.FLGDEL = "N"
            user.CODUSR = user.CODUSR.ToUpper()
            '' Inserimento Utente
            If (Not WcfServices.InsertUser(user)) Then
                '' TODO : Eventuale gestione errore
            End If
            Return RedirectToAction("Search", New With {.from = "create"})
        End Function
#End Region

#Region "Delete User"
        Public Function Delete(codusr As String) As ActionResult
            Dim user As New TUSR()
            user.CODUSR = codusr
            If (WcfServices.DeleteUser(user, True)) Then

            End If

            Return RedirectToAction("Search", New With {.from = "delete"})
        End Function

#End Region

#Region "Association User / Role "
        <HttpGet()> _
        <BreadCrumb("Associazione Ruoli/Utente ")> _
        Public Function AssociateRolesToUser(codusr As String) As ActionResult
            'Dim userRolesAssoc As UserRolesAssociationsViewModel(coduteid0, _repositoryUsers)
            'userRolesAssoc.AssociableRoles.Data = _repositoryRoles.getRolesAssociableToUser(coduteid0)
            'userRolesAssoc.AssociatedRoles.Data = _repositoryRoles.getRolesAssociatedToUser(coduteid0)
            Dim userRolesAssoc As New UserRolesAssociationsViewModel()
            Dim tUte As TUSR = New TUSR With {.CODUSR = codusr}
            userRolesAssoc.User = WcfServices.GetUserByPrimaryKey(tUte)
            userRolesAssoc.AssociableRoles.Data = WcfServices.GetRolesAssociableToUser(tUte)
            userRolesAssoc.AssociatedRoles.Data = WcfServices.getRolesAssociatedToUser(tUte)

            Return View(userRolesAssoc)
        End Function

        <HttpPost()> _
        <BreadCrumb("Associazione Ruoli/Utente ")> _
        Public Function AssociateRolesToUser(formCol As FormCollection) As ActionResult
            Dim codusr As String = formCol("codusr")
            If formCol("btnAddRole") <> Nothing Then
                If Not String.IsNullOrEmpty(formCol("chk1")) Then
                    Dim codruos As String() = Array.ConvertAll(formCol("chk1").Split(Convert.ToChar(",")), Function(x) (x))
                    Dim lstTUTERUO As New List(Of TUSRROL)
                    For Each codruo As String In codruos
                        Dim userRole As New TUSRROL
                        userRole.CODROL = codruo
                        userRole.CODUSR = codusr
                        userRole.DATASC = Date.Now
                        userRole.CODGRPARE = ConfigurationManager.AppSettings("GENERIC_CODE_AREA")
                        lstTUTERUO.Add(userRole)
                    Next
                    WcfServices.InsertUserRole(lstTUTERUO)
                End If
            ElseIf formCol("btnDeleteRole") <> Nothing Then
                If Not [String].IsNullOrEmpty(formCol("chk2")) Then
                    Dim codruos As String() = Array.ConvertAll(formCol("chk2").Split(Convert.ToChar(",")), Function(x) (x))
                    Dim lstTUTERUO As New List(Of TUSRROL)
                    For Each codruo As String In codruos
                        Dim userRole As New TUSRROL
                        userRole.CODROL = codruo
                        userRole.CODUSR = codusr
                        lstTUTERUO.Add(userRole)
                    Next
                    WcfServices.DeleteUserRole(lstTUTERUO)
                End If
            End If
            Return RedirectToAction("AssociateRolesToUser", New With {Key .codusr = codusr.ToString()})
        End Function
#End Region

#Region "Web Method"
        '<AuthorizePermissionBypass()> _
        'Public Function getNominativi(query As String) As ActionResult
        '    Return Json(_repositoryUsers.AutocompleteNominativo(query), JsonRequestBehavior.AllowGet)
        'End Function
#End Region

    End Class
End Namespace
