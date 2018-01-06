Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics
Imports log4net
Imports System.Web.Security

Namespace AppCode.CustomProvider
    Public Class AccountMembershipService
        Implements IMembershipService
        Private _provider As MyMembershipProvider

        Shared log As ILog = LogManager.GetLogger((System.Reflection.MethodBase.GetCurrentMethod().DeclaringType))

        Public Sub New()
            Me.New(Nothing)
        End Sub

        Public Sub New(provider As MyMembershipProvider)
            _provider = DirectCast(If(provider, Membership.Provider), MyMembershipProvider)
        End Sub

        Public ReadOnly Property MinPasswordLength() As Integer Implements IMembershipService.MinPasswordLength
            Get
                Return _provider.MinRequiredPasswordLength
            End Get
        End Property

        Public Function ChangePassword(userName As String, oldPassword As String, newPassword As String) As Boolean Implements IMembershipService.ChangePassword
            log.Debug("INIT")
            Dim currentUser As MembershipUser = _provider.GetUser(userName, True)
            Dim result As Boolean = currentUser.ChangePassword(oldPassword, newPassword)
            log.Debug("END")
            Return result
        End Function

        Public Function CreateUser(userName As String, password As String, email As String) As MembershipCreateStatus Implements IMembershipService.CreateUser
            log.Debug("INIT")
            Dim status As MembershipCreateStatus = Nothing
            _provider.CreateUser(userName, password, email, Nothing, Nothing, True, _
             Nothing, status)
            log.Debug("END")
            Return status
        End Function

        Public Function ValidateUser(userName As String, password As String) As Boolean Implements IMembershipService.ValidateUser
            log.Debug("INIT")
            Dim result As Boolean = _provider.ValidateUser(userName.ToUpper(), password.ToUpper())
            log.Debug("END")
            Return result
        End Function

        Public Function ValidateUser(userName As String, password As String, domain As String) As Boolean Implements IMembershipService.ValidateUser
            log.Debug("INIT")
            Dim result As Boolean = _provider.ValidateUser(userName.ToUpper(), password, domain.ToUpper())
            log.Debug("END")
            Return result
        End Function
    End Class
End Namespace