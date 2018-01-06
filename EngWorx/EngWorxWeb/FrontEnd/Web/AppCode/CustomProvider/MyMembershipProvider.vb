Imports System.Security.Cryptography
Imports log4net
Imports Web.AppCode.Session
Imports System.Web.Security
Imports System.Configuration
Imports System.Text
Imports System.ServiceModel
Imports System.Net
Imports Domain
Imports ServiceProxy.WcfConsumer

Namespace AppCode.CustomProvider
    Public Class MyMembershipProvider
        Inherits MembershipProvider
        Shared log As ILog = LogManager.GetLogger((System.Reflection.MethodBase.GetCurrentMethod().DeclaringType))

        Public Overrides Property ApplicationName() As String
            Get
                Return Nothing
            End Get
            Set(value As String)
            End Set
        End Property

        Public Overrides Function ChangePassword(username As String, oldPassword As String, newPassword As String) As Boolean
            Return False
        End Function

        Public Overrides Function ChangePasswordQuestionAndAnswer(username As String, password As String, newPasswordQuestion As String, newPasswordAnswer As String) As Boolean
            Return False
        End Function

        Public Overrides Function CreateUser(ByVal username As String, ByVal password As String, ByVal email As String, ByVal passwordQuestion As String, ByVal passwordAnswer As String, ByVal isApproved As Boolean, ByVal providerUserKey As Object, ByRef status As MembershipCreateStatus) As MembershipUser
            status = MembershipCreateStatus.Success
            Return Nothing
        End Function

        Public Overrides Function DeleteUser(username As String, deleteAllRelatedData As Boolean) As Boolean
            Return False
        End Function

        Public Overrides ReadOnly Property EnablePasswordReset() As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property EnablePasswordRetrieval() As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides Function FindUsersByEmail(emailToMatch As String, pageIndex As Integer, pageSize As Integer, ByRef totalRecords As Integer) As MembershipUserCollection
            totalRecords = 0
            Return Nothing
        End Function

        Public Overrides Function FindUsersByName(usernameToMatch As String, pageIndex As Integer, pageSize As Integer, ByRef totalRecords As Integer) As MembershipUserCollection
            totalRecords = 0
            Return Nothing
        End Function

        Public Overrides Function GetAllUsers(pageIndex As Integer, pageSize As Integer, ByRef totalRecords As Integer) As MembershipUserCollection
            totalRecords = 0
            Return Nothing
        End Function

        Public Overrides Function GetNumberOfUsersOnline() As Integer
            Return 0
        End Function

        Public Overrides Function GetPassword(username As String, answer As String) As String
            Return Nothing
        End Function

        Public Overrides Function GetUser(providerUserKey As Object, userIsOnline As Boolean) As System.Web.Security.MembershipUser
            Return Nothing
        End Function

        Public Overrides Function GetUser(username As String, userIsOnline As Boolean) As System.Web.Security.MembershipUser
            Return Nothing
        End Function

        Public Overrides Function GetUserNameByEmail(email As String) As String
            Return Nothing
        End Function

        Public Overrides ReadOnly Property MaxInvalidPasswordAttempts() As Integer
            Get
                Return 0
            End Get
        End Property

        Public Overrides ReadOnly Property MinRequiredNonAlphanumericCharacters() As Integer
            Get
                Return 0
            End Get
        End Property

        Public Overrides ReadOnly Property MinRequiredPasswordLength() As Integer
            Get
                Return 0
            End Get
        End Property

        Public Overrides ReadOnly Property PasswordAttemptWindow() As Integer
            Get
                Return 0
            End Get
        End Property

        Public Overrides ReadOnly Property PasswordFormat() As MembershipPasswordFormat
            Get
                Return MembershipPasswordFormat.Clear
            End Get
        End Property

        Public Overrides ReadOnly Property PasswordStrengthRegularExpression() As String
            Get
                Return Nothing
            End Get
        End Property

        Public Overrides ReadOnly Property RequiresQuestionAndAnswer() As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property RequiresUniqueEmail() As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides Function ResetPassword(username As String, answer As String) As String
            Return Nothing
        End Function

        Public Overrides Function UnlockUser(userName As String) As Boolean
            Return False
        End Function

        Public Overrides Sub UpdateUser(user As System.Web.Security.MembershipUser)
        End Sub

        Public Overrides Function ValidateUser(username As String, password As String) As Boolean
            Return False
        End Function

        Public Overloads Function ValidateUser(username As String, password As String, domain As String) As Boolean
            log.Debug("INIT")
            Dim esito As Boolean = False
            Try
                'CONTROLLO AUTENTICAZIONE SU ACTIVE DIRECTORY
                'si controlla se è attivo il bypassing dell'autenticazione su Active Directory
                Dim bypassAD As String = ConfigurationManager.AppSettings("AUTHENTICATION_BYPASS").ToUpper()
                If (Not "TRUE".Equals(bypassAD)) Then
                    'TODO implementare l'autenticazione AD
                    Throw New NotImplementedException()
                Else
                    'CASO in cui si effettua il BYPASS dell'autenticazione su Active Directory
                    esito = True
                End If

                'CONTROLLO AUTORIZZAZIONE SU BANCA DATI
                If (esito) Then
                    'per effettuare la chiamata di controllo ho bisogno di memorizzare temporaneamente in sessione
                    'il nome dell'utente che tenta il login
                    Dim tempUser As New TUSR With {.CODUSR = username}
                    SessionManager.setUser(tempUser)
                    Dim loggedUser As TUSR = WcfServices.GetUserRolesByCompanyAndUsername(tempUser)
                    If (loggedUser Is Nothing) Then
                        'Autenticazione utente NON ANDATA a buon fine
                        SessionManager.setUser(Nothing)
                        log.Info("Autorizzazione fallita per l'utente: " & username)
                        esito = False
                    Else
                        'Autenticazione utente ANDATA a buon fine
                        SessionManager.setUser(loggedUser)
                        esito = True
                    End If
                End If
            Catch ex As Exception
                SessionManager.setUser(Nothing)
                log.Error(ex.Message, ex)
                Return False
            Finally
                log.Debug("END")
            End Try
            Return esito
        End Function

        Public Shared Function EncryptBase64(ByVal Data As String) As String
            log.Debug("INIT")
            Dim shaM As New SHA1Managed()
            Convert.ToBase64String(shaM.ComputeHash(Encoding.ASCII.GetBytes(Data)))
            Dim eNC_data As Byte() = ASCIIEncoding.ASCII.GetBytes(Data)
            Dim eNC_str As String = Convert.ToBase64String(eNC_data)
            log.Debug("END")
            Return eNC_str
        End Function
    End Class
End Namespace