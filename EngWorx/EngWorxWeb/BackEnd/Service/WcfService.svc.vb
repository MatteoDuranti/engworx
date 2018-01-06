Imports Service.MessageContracts
Imports Domain
Imports RepositoryInterface
Imports Repository
Imports log4net
Imports Service.DataContracts
Imports System.Data.Entity

<Assembly: log4net.Config.XmlConfigurator()> 
Public Class WcfService
    Implements IWcfService

    Private Shared ReadOnly log As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

    Public Sub New()
        'HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize()
    End Sub

#Region "ServiceFunction"
    Public Function CheckDbConnection() As Boolean Implements IWcfService.CheckDbConnection
        log.Debug("INIT")
        Dim db As New dbEntities()
        Dim _repository As IDbFunctionRepository
        Dim ret As Boolean
        Try
            _repository = New DbFunctionRepository(db)
            ret = _repository.CheckDbConnection()
        Catch ex As Exception
            ThrowFaultException(ex)
        Finally
            log.Debug("END")
        End Try
        Return ret
    End Function

    Private Sub ThrowFaultException(ByVal ex As Exception)
        log.Error(ex.Message, ex)
        Throw (New FaultException(New FaultReason(ex.GetBaseException().Message)))
    End Sub
#End Region

#Region "USER"
    Public Function GetUserRolesByCompanyAndUsername(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of TUSR) Implements IWcfService.GetUserRolesByCompanyAndUsername
        Dim msgOut As New MessageOutput(Of TUSR)
        Dim db As New dbEntities()
        Dim _repository As IUserRepository
        Try
            _repository = New UserRepository(db)
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            msgOut.Entity = _repository.GetUserRolesByCompanyAndUsername(inputMsg.Entity)
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgOut
    End Function

    Public Function GetFilteredUsers(ByVal inputMsg As MessageInput(Of GetFiltered(Of TUSR))) As MessageOutput(Of IList(Of TUSR)) Implements IWcfService.GetFilteredUsers
        Dim msgOut As New MessageOutput(Of IList(Of TUSR))
        Dim db As New dbEntities()
        Dim _repository As IUserRepository
        Try
            _repository = New UserRepository(db)
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, inputMsg.Entity.totalrows, inputMsg.Entity)
            msgOut.Entity = _repository.GetFiltered(inputMsg.Entity.parameters, inputMsg.Entity.sort, inputMsg.Entity.sortDirection, inputMsg.Entity.pageNumber, inputMsg.Entity.pageSize, inputMsg.Entity.totalrows)
            msgOut.MessageHeader.serviceParam = inputMsg.Entity.totalrows
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgOut
    End Function

    Public Function GetUserByPrimaryKey(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of TUSR) Implements IWcfService.GetUserByPrimaryKey
        Dim db As New dbEntities()
        Dim _repository As IUserRepository
        Dim msgOut As New MessageOutput(Of TUSR)
        Try
            _repository = New UserRepository(db)
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            msgOut.Entity = _repository.GetByPrimaryKey(inputMsg.Entity.CODUSR)
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgOut
    End Function

    Public Function DeleteUser(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of TUSR) Implements IWcfService.DeleteUser
        Dim db As New dbEntities()
        Dim _repository As IUserRepository
        Dim u As TUSR
        Dim msgOut As New MessageOutput(Of TUSR)
        Try
            _repository = New UserRepository(db)
            u = _repository.GetUserRolesByPrimaryKey(inputMsg.Entity)
            _repository.DeleteOnSubmit(u, Convert.ToBoolean(inputMsg.MessageHeader.serviceParam))
            _repository.SaveChanges()
            msgOut.Entity = Nothing
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgOut
    End Function

    Public Function UpdateUser(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of TUSR) Implements IWcfService.UpdateUser
        Dim db As New dbEntities()
        Dim _repository As IUserRepository
        Dim msgOut As New MessageOutput(Of TUSR)
        Try
            _repository = New UserRepository(db)
            _repository.Update(inputMsg.Entity)
            _repository.SaveChanges()
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgOut
    End Function

    Public Function InsertUser(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of TUSR) Implements IWcfService.InsertUser
        Dim db As New dbEntities()
        Dim _repository As IUserRepository
        Dim msgout As New MessageOutput(Of TUSR)
        Try
            _repository = New UserRepository(db)
            _repository.InsertOnSubmit(inputMsg.Entity)
            _repository.SaveChanges()
            msgout.Entity = inputMsg.Entity
            msgout.MessageHeader = New HeaderOutput() With {.serviceParam = True}
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgout
    End Function
#End Region

#Region "ROLE"

    Public Function DeleteRole(ByVal inputMsg As MessageInput(Of TROL)) As MessageOutput(Of TROL) Implements IWcfService.DeleteRole
        Dim db As New dbEntities()
        Dim _repository As IRoleRepository
        Dim msgout As New MessageOutput(Of TROL)
        Try
            _repository = New RoleRepository(db)
            Dim r As TROL = _repository.GetRoleUsersFuncs(inputMsg.Entity)
            _repository.DeleteOnSubmit(r, CBool(inputMsg.MessageHeader.serviceParam))
            _repository.SaveChanges()
            msgout.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            msgout.Entity = Nothing
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgout
    End Function

    Public Function InsertRole(ByVal inputMsg As MessageInput(Of TROL)) As MessageOutput(Of TROL) Implements IWcfService.InsertRole
        Dim db As New dbEntities()
        Dim _repository As IRoleRepository
        Dim msgout As New MessageOutput(Of TROL)
        Try
            _repository = New RoleRepository(db)
            _repository.InsertOnSubmit(inputMsg.Entity)
            _repository.SaveChanges()
            msgout.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            msgout.Entity = Nothing
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgout
    End Function

    Public Function GetFilteredRoles(ByVal inputMsg As MessageInput(Of GetFiltered(Of TROL))) As MessageOutput(Of IList(Of TROL)) Implements IWcfService.GetFilteredRoles
        Dim msgOut As New MessageOutput(Of IList(Of TROL))
        Dim db As New dbEntities()
        Dim _repository As IRoleRepository
        Try
            _repository = New RoleRepository(db)
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, inputMsg.Entity.totalrows, inputMsg.Entity)
            msgOut.Entity = _repository.GetFiltered(inputMsg.Entity.parameters, inputMsg.Entity.sort, inputMsg.Entity.sortDirection, inputMsg.Entity.pageNumber, inputMsg.Entity.pageSize, Convert.ToInt32(msgOut.MessageHeader.serviceParam))
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgOut
    End Function

    Public Function GetRoleByPrimaryKey(ByVal inputMsg As MessageInput(Of TROL)) As MessageOutput(Of TROL) Implements IWcfService.GetRoleByPrimaryKey
        Dim db As New dbEntities()
        Dim _repository As IRoleRepository
        Dim msgOut As New MessageOutput(Of TROL)
        Try
            _repository = New RoleRepository(db)
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            msgOut.Entity = _repository.GetByPrimaryKey(inputMsg.Entity.CODROL)
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgOut
    End Function

    Public Function GetAllRoles(ByVal inputMsg As MessageInput(Of TROL)) As MessageOutput(Of IList(Of TROL)) Implements IWcfService.GetAllRoles
        Dim db As New dbEntities()
        Dim _repository As IRoleRepository
        Dim msgOut As New MessageOutput(Of IList(Of TROL))
        Try
            _repository = New RoleRepository(db)
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            msgOut.Entity = _repository.GetAll()
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgOut
    End Function

    Public Function GetRolesAssociableToUser(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of IList(Of TROL)) Implements IWcfService.GetRolesAssociableToUser
        Dim db As New dbEntities()
        Dim _repository As IRoleRepository
        Dim msgout As New MessageOutput(Of IList(Of TROL))
        Try
            _repository = New RoleRepository(db)
            msgout.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            msgout.Entity = _repository.GetRolesAssociableToUser(inputMsg.Entity)
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgout
    End Function

    Public Function GetRolesAssociatedToUser(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of List(Of TROL)) Implements IWcfService.GetRolesAssociatedToUser
        Dim db As New dbEntities()
        Dim _repository As IRoleRepository
        Dim msgout As New MessageOutput(Of List(Of TROL))
        Try
            _repository = New RoleRepository(db)
            msgout.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            msgout.Entity = _repository.GetRolesAssociatedToUser(inputMsg.Entity).ToList
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgout
    End Function

    Public Function GetRolesAndAllowedFunctions(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of List(Of TROL)) Implements IWcfService.GetRolesAndAllowedFunctions
        Dim db As New dbEntities()
        Dim _repository As IRoleRepository
        Dim msgout As New MessageOutput(Of List(Of TROL))
        Try
            _repository = New RoleRepository(db)
            msgout.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            msgout.Entity = _repository.GetRolesAndAllowedFunctions()
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgout
    End Function

    Public Function UpdateRole(ByVal inputMsg As MessageInput(Of TROL)) As MessageOutput(Of TROL) Implements IWcfService.UpdateRole
        Dim db As New dbEntities()
        Dim _repository As IRoleRepository
        Dim msgout As New MessageOutput(Of TROL)
        Try
            _repository = New RoleRepository(db)
            _repository.Update(inputMsg.Entity)
            _repository.SaveChanges()
            msgout.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            msgout.Entity = Nothing
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgout
    End Function

    Public Function getRolesByControllerAction(ByVal inputMsg As MessageInput(Of TFNC)) As MessageOutput(Of List(Of TROL)) Implements IWcfService.getRolesByControllerAction
        log.Debug("INIT")
        Dim db As New dbEntities()
        Dim _repository As IRoleRepository
        Dim msgOut As New MessageOutput(Of List(Of TROL))
        Try
            _repository = New RoleRepository(db)
            msgOut.Entity = CType(_repository.GetRolesByControllerAction(inputMsg.Entity.DESCTL, inputMsg.Entity.DESACTCTL), List(Of TROL))
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
        Catch ex As Exception
            ThrowFaultException(ex)
        Finally
            log.Debug("END")
        End Try
        Return msgOut
    End Function
#End Region

#Region "Function"
    Public Function GetFunctionsWithPermissions(ByVal inputMsg As MessageInput(Of TROL)) As MessageOutput(Of List(Of TFNC)) Implements IWcfService.GetFunctionsWithPermissions
        Dim db As New dbEntities()
        Dim _repository As IFunctionRepository
        Dim msgOut As New MessageOutput(Of List(Of TFNC))
        Try
            _repository = New FunctionRepository(db)
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            msgOut.Entity = _repository.GetFunctionsWithPermissions(inputMsg.Entity)
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgOut
    End Function

    Public Function InsertDeleteAssociationFunctToRole(ByVal inputMsg As MessageInput(Of TROLFNC)) As MessageOutput(Of TROLFNC) Implements IWcfService.InsertDeleteAssociationFunctToRole
        Dim db As New dbEntities()
        Dim _repository As IRoleFunctionsRepository
        Dim perm As TROLFNC
        Dim result As Boolean = False
        Dim msgOut As New MessageOutput(Of TROLFNC)
        Try
            _repository = New RoleFunctionsRepository(db)
            perm = _repository.GetByPrimaryKey(inputMsg.Entity.CODROL, inputMsg.Entity.CODFNC)

            If perm IsNot Nothing Then
                '' Cancellazione
                _repository.DeleteOnSubmit(perm)
            Else
                perm = New TROLFNC()
                perm.CODFNC = inputMsg.Entity.CODFNC
                perm.CODROL = inputMsg.Entity.CODROL
                perm.DATASC = DateTime.Now
                _repository.InsertOnSubmit(perm)
                result = True
            End If
            _repository.SaveChanges()
            msgOut.Entity = perm
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, result, inputMsg.Entity)
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgOut
    End Function
#End Region

#Region "UserRole"
    Public Function DeleteUserRole(ByVal inputMsg As MessageInput(Of List(Of TUSRROL))) As MessageOutput(Of TUSRROL) Implements IWcfService.DeleteUserRole
        Dim db As New dbEntities()
        Dim _repository As IUserRolesRepository
        Dim msgOut As New MessageOutput(Of TUSRROL)
        Try
            _repository = New UserRolesRepository(db)
            For Each el As TUSRROL In inputMsg.Entity
                _repository.GetUserRoles(el).ForEach(Function(e) _repository.DeleteOnSubmit(e))
            Next
            msgOut.Entity = Nothing
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            _repository.SaveChanges()
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgOut
    End Function

    Public Function InsertUserRole(ByVal inputMsg As MessageInput(Of List(Of TUSRROL))) As MessageOutput(Of TUSRROL) Implements IWcfService.InsertUserRole
        Dim db As New dbEntities()
        Dim _repository As IUserRolesRepository
        Dim msgOut As New MessageOutput(Of TUSRROL)
        Try
            _repository = New UserRolesRepository(db)
            For Each el As TUSRROL In inputMsg.Entity
                'el.DATASSRUO = Date.Now
                _repository.InsertOnSubmit(el)
            Next
            msgOut.Entity = Nothing
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            _repository.SaveChanges()
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgOut
    End Function
#End Region

#Region "Menù"
    Public Function GetMenuByUser(ByVal inputMsg As MessageInput(Of TUSR)) As MessageOutput(Of List(Of VMEN)) Implements IWcfService.GetMenuByUser
        Dim db As New dbEntities()
        Dim _repository As IMenuRepository
        Dim msgout As New MessageOutput(Of List(Of VMEN))
        Try
            _repository = New MenuRepository(db)
            msgout.Entity = _repository.GetMenuByUser(inputMsg.Entity).ToList
            msgout.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
        Catch ex As Exception
            ThrowFaultException(ex)
        End Try
        Return msgout
    End Function
#End Region

#Region "System Parameter"
    Public Function GetSysParByPrimaryKey(ByVal inputMsg As MessageInput(Of TSYSPAR)) As MessageOutput(Of TSYSPAR) Implements IWcfService.GetSysParByPrimaryKey
        log.Debug("INIT")
        Dim db As New dbEntities()
        Dim _repository As ISysParRepository
        Dim msgOut As New MessageOutput(Of TSYSPAR)
        Try
            _repository = New SysParRepository(db)
            msgOut.MessageHeader = ServiceUtility.createMessageHeaderOut(inputMsg.MessageHeader, True, inputMsg.Entity)
            msgOut.Entity = _repository.GetByPrimaryKey(inputMsg.Entity.CODSYSPAR, inputMsg.Entity.NUMSYSPARIDX)
        Catch ex As Exception
            ThrowFaultException(ex)
        Finally
            log.Debug("END")
        End Try
        Return msgOut
    End Function
#End Region

End Class
