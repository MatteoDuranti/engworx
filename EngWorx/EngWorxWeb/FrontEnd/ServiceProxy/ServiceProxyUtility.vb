Imports System
Imports System.IO
Imports System.Text
Imports System.Reflection
Imports System.ServiceModel
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices
Imports System.ServiceModel.Configuration
Imports System.Web
Imports System.Configuration
Imports Common
Imports ServiceProxy.WcfService
Imports Domain

Namespace WcfConsumer

    Public Class ServiceProxyUtility
        Public Shared Function getwcfChannelGapOpened() As ChannelFactory(Of WcfService.IWcfService)
            Dim bindingConfig As String = "BasicHttpBinding_IWcfService"
            Dim myEndpoint As System.ServiceModel.EndpointAddress = Nothing
            Dim myBinding As New System.ServiceModel.BasicHttpBinding(bindingConfig)
            Dim cliEndSect As ClientSection = DirectCast(ConfigurationManager.GetSection("system.serviceModel/client"), ClientSection)
            For Each E As ChannelEndpointElement In cliEndSect.Endpoints
                If (E.Name = bindingConfig) Then
                    myEndpoint = New EndpointAddress(E.Address)
                End If
            Next
            Dim channel As New System.ServiceModel.ChannelFactory(Of WcfService.IWcfService)(myBinding, myEndpoint)
            'channel.Open()
            Return channel
        End Function

        Public Shared Sub closeChannel(ByRef channel As ChannelFactory)
            If (channel.State = CommunicationState.Opened) Then
                channel.Close()
            End If
        End Sub

        Public Shared Function createMessageHeaderInput(ByVal bodyIn As Object) As HeaderInput
            Dim headIn As New HeaderInput()
            If (bodyIn IsNot Nothing) Then
                Dim SECURITY_ENABLED As String = ConfigurationManager.AppSettings("SECURITY_ENABLED").ToUpper()
                If ("TRUE".Equals(SECURITY_ENABLED)) Then
                    headIn.securityToken = generateSecurityToken(bodyIn)
                End If
            Else
                Throw New ArgumentNullException("bodyIn")
            End If
            Return headIn
        End Function

        Private Shared Function generateSecurityToken(ByVal bodyIn As Object) As String
            Dim userCompanyCode As String = Nothing
            Dim userMemberCode As String = Nothing
            getUserNameFromSession(userMemberCode)
            Dim tokenDateTime As String = Date.Now.ToString()
            Dim messageBodyHashCode As String = CommonUtility.GenerateHashCode(bodyIn)
            Dim tokenSchema As String = "{0}|{1}|{2}|{3}"
            Dim plainToken As String = String.Format(tokenSchema, userCompanyCode, userMemberCode, tokenDateTime, messageBodyHashCode)
            Dim cryptedToken As String = CommonUtility.EncryptStringAES(plainToken)
            Return cryptedToken
        End Function

        Private Shared Sub getUserNameFromSession(ByRef codmat As String)
            If (Not HttpContext.Current.Session("mysession") Is Nothing) Then
                Dim sesType As Type = HttpContext.Current.Session("mysession").GetType()
                Dim p As System.Reflection.PropertyInfo = sesType.GetProperty("User")
                Dim t As TUSR = DirectCast(p.GetValue(HttpContext.Current.Session("mysession"), Nothing), TUSR)
                codmat = t.CODUSR
            End If
        End Sub
    End Class
End Namespace