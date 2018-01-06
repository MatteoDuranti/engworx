Imports Domain
Imports Repository
Imports System.Security.Cryptography
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Reflection
Imports Common
Imports System.Web.Configuration
Imports System.Drawing
Imports RepositoryInterface

Friend Class ServiceUtility
    Public Shared Function createMessageHeaderOut(ByVal inHead As HeaderInput, ByVal serviceParamValue As Object, ByVal inEntity As Object) As HeaderOutput
        Dim headOut As New HeaderOutput With {.securityToken = inHead.securityToken, .serviceParam = serviceParamValue}
        Dim SECURITY_ENABLED As String = ConfigurationManager.AppSettings("SECURITY_ENABLED").ToUpper()
        If ("TRUE".Equals(SECURITY_ENABLED)) Then
            If (Not ServiceUtility.checkSecurityToken(inHead.securityToken, inEntity)) Then
                Throw New Exception("Invalid Security Token")
            End If
        End If
        Return headOut
    End Function

    Public Shared Function checkSecurityToken(ByVal cryptedToken As String, ByVal bodyIn As Object) As Boolean
        'decifratura del security token
        Dim plainToken As String = CommonUtility.DecryptStringAES(cryptedToken)
        Dim tokenData() As String = plainToken.Split(Convert.ToChar("|"))
        'controllo del formato del security token
        If (tokenData.Length <> 4) Then
            Throw New Exception("Invalid Security Token: format error")
        End If
        Dim userCompanyCode As String = tokenData(0)
        Dim userMemberCode As String = tokenData(1)
        Dim tokenDateTimeStr As String = tokenData(2)
        Dim messageBodyHashCode As String = tokenData(3)
        'controllo dell'hash code
        If Not (messageBodyHashCode.Equals(Common.CommonUtility.GenerateHashCode(bodyIn))) Then
            Throw New Exception("Invalid Security Token: invalid code")
        End If
        'controllo della data del token
        Dim tokenDateTime As DateTime = DateTime.MinValue
        Date.TryParse(tokenDateTimeStr, tokenDateTime)
        Dim SECURITY_TIME_SYNC_GAP As Integer = Integer.Parse(ConfigurationManager.AppSettings("SECURITY_TIME_SYNC_GAP"))
        If ((tokenDateTime < Date.Now.AddMinutes(-1 * SECURITY_TIME_SYNC_GAP)) Or (tokenDateTime > Date.Now.AddMinutes(SECURITY_TIME_SYNC_GAP))) Then
            Throw New Exception("Invalid Security Token: invalid request")
        End If
        'controllo dell'utente che effettua la chiamata
        Dim db As New dbEntities()
        Dim _repository As IUserRepository = New UserRepository(db)
        Dim u As TUSR = _repository.GetByPrimaryKey(userCompanyCode, userMemberCode)
        If (u Is Nothing) Then
            Throw New Exception("Invalid Security Token: user not allowed")
        End If
        Return True
    End Function

    Public Shared Function ToDbString(ByVal obj As Object) As String
        If TypeOf obj Is Integer? Then
            If DirectCast(obj, Integer?).HasValue Then
                Return DirectCast(obj, Integer?).Value.ToString
            End If
            Return "NULL"
        End If
        If TypeOf obj Is Integer Then
            Return DirectCast(obj, Integer?).Value.ToString
        End If
        If TypeOf obj Is Short? Then
            If DirectCast(obj, Short?).HasValue Then
                Return DirectCast(obj, Short?).Value.ToString
            End If
            Return "NULL"
        End If
        If TypeOf obj Is Short Then
            Return DirectCast(obj, Short?).Value.ToString
        End If
        If TypeOf obj Is Date? Then
            If DirectCast(obj, Date?).HasValue Then
                Return "TO_DATE('" & DirectCast(obj, Date?).Value.ToString("dd-MM-yyyy") & "', 'dd-mm-yyyy')"
            End If
            Return "NULL"
        End If
        If TypeOf obj Is Date Then
            Return "TO_DATE('" & DirectCast(obj, Date).ToString("dd-MM-yyyy") & "', 'dd-mm-yyyy')"
        End If
        If obj Is Nothing Then
            Return "NULL"
        End If
        Return "'" & obj.ToString & "'"
    End Function
End Class
