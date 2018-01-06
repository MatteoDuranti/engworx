Imports EngWorxCore.Service
Imports Service.MessageContracts
Imports Domain
Imports Service.DataContracts

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
<ServiceContract()>
Public Interface IWcfService

#Region "USER"
    <OperationContract(Name:="GetUserRolesByCompanyAndUsername")> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Ritorna l'User con la lista dei ruoli")> _
    Function GetUserRolesByCompanyAndUsername(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of TUSR)

    <OperationContract(Name:="GetFilteredUsers")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Ritorna la lista degli utenti filtrata e Ordinata ")> _
    Function GetFilteredUsers(ByVal inputMsg As MessageInput(Of GetFiltered(Of TUSR))) As MessageOutput(Of IList(Of TUSR))

    <OperationContract(Name:="GetUserByPrimaryKey")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Ritorna l'user in base alla primary key (coduteid0) ")> _
    Function GetUserByPrimaryKey(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of TUSR)

    <OperationContract(Name:="InsertUser")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Inserisce utente")> _
    Function InsertUser(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of TUSR)

    <OperationContract(Name:="DeleteUser")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Cancella l'utente e la lista dei ruoli associati all'utente")> _
    Function DeleteUser(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of TUSR)

    <OperationContract(Name:="UpdateUser")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Aggiorna l'utente ")> _
    Function UpdateUser(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of TUSR)
#End Region

#Region "ROLE"
    <OperationContract(Name:="GetFilteredRoles")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Ritorna la lista dei ruoli filtrata e Ordinata ")> _
    Function GetFilteredRoles(ByVal inputMsg As MessageInput(Of GetFiltered(Of TROL))) As MessageOutput(Of IList(Of TROL))

    <OperationContract(Name:="GetRoleByPrimaryKey")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Ritorna il Ruolo in base alla primary key (codruoid0) ")> _
    Function GetRoleByPrimaryKey(ByVal inputMsg As MessageInput(Of TROL)) As MessageOutput(Of TROL)

    <OperationContract(Name:="GetAllRoles")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Ritorna tutta lista dei ruoli")> _
    Function GetAllRoles(ByVal inputMsg As MessageInput(Of TROL)) As MessageOutput(Of IList(Of TROL))

    <OperationContract(Name:="GetRolesAndAllowedFunctions")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Ritorna la lista dei ruoli con le funzioni a cui hanno accesso")> _
    Function GetRolesAndAllowedFunctions(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of List(Of TROL))

    <OperationContract(Name:="GetRolesAssociableToUser")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Ritorna tutta lista dei ruoli che possono essere associati ad un utente")> _
    Function GetRolesAssociableToUser(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of IList(Of TROL))

    <OperationContract(Name:="GetRolesAssociatedToUser")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Ritorna tutta lista dei ruoli associati ad un utente")> _
    Function GetRolesAssociatedToUser(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of List(Of TROL))

    <OperationContract(Name:="UpdateRole")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Aggiorna il ruolo ")> _
    Function UpdateRole(ByVal inputMsg As MessageInput(Of TROL)) As MessageOutput(Of TROL)

    <OperationContract(Name:="InsertRole")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Crea nuovo ruolo ")> _
    Function InsertRole(ByVal inputMsg As MessageInput(Of TROL)) As MessageOutput(Of TROL)

    <OperationContract(Name:="DeleteRole")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Elimina ruolo ")> _
    Function DeleteRole(ByVal inputMsg As MessageInput(Of TROL)) As MessageOutput(Of TROL)

    <OperationContract()> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Aggiorna il ruolo ")> _
    Function getRolesByControllerAction(ByVal inputMsg As MessageInput(Of TFNC)) As MessageOutput(Of List(Of TROL))
#End Region

#Region "Function"
    <OperationContract(Name:="GetFuncionsWithPermissions")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Ritorna la lista delle funzioni con l'autorizzazione di esecuzione per un determinata ruolo")> _
    Function GetFunctionsWithPermissions(ByVal inputMsg As MessageInput(Of TROL)) As MessageOutput(Of List(Of TFNC))

    <OperationContract(Name:="InsertDeleteAssociationFunctToRole")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("In base al codruoid0 ed al codfunid0 inserisce ( se non esiste ) o cancella ( se esiste ) l'associazione della Funzione al ruolo" & vbCrLf & _
    "Ritorna il valore true se viene inserita l'associzione, false se viene cancellata")> _
    Function InsertDeleteAssociationFunctToRole(ByVal inputMsg As MessageInput(Of TROLFNC)) As MessageOutput(Of TROLFNC)
#End Region

#Region "UserRole"
    <OperationContract(Name:="InsertUserRole")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Associa ad un utente un ruolo")> _
    Function InsertUserRole(ByVal inputMsg As MessageInput(Of List(Of TUSRROL))) As MessageOutput(Of TUSRROL)

    <OperationContract(Name:="DeleteUserRole")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Rimuove l'associazione di un ruolo ad un utente")> _
    Function DeleteUserRole(ByVal inputMsg As MessageInput(Of List(Of TUSRROL))) As MessageOutput(Of TUSRROL)
#End Region

#Region "Menù"
    <OperationContract(Name:="GetMenuByUser")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Ritorna il menu in base all'utente")> _
    Function GetMenuByUser(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of List(Of VMEN))
#End Region

#Region "CheckDb"
    <OperationContract()> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("CheckDbConnection")> _
    Function CheckDbConnection() As Boolean
#End Region

#Region "System Parameter"
    <OperationContract(Name:="GetSysParByPrimaryKey")> _
    <ApplyDataContractResolver()> _
    <CyclicReferencesAware(True)> _
    <WsdlDocumentation("Ritorna i parametri in base alla primary key")> _
    Function GetSysParByPrimaryKey(ByVal inputMsg As MessageInput(Of TSYSPAR)) As MessageOutput(Of TSYSPAR)
#End Region

End Interface
