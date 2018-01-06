Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class LogonFormViewModel

        <DisplayName("Username:")> _
        <Required(errorMessage:="Il campo Matricola è obbligatorio")> _
        <StringLength(6, MinimumLength:=6, errorMessage:="Il campo Azienda deve avere una lunghezza di sei caratteri")> _
        Property username() As String

        <DisplayName("Password:")> _
        <Required(errorMessage:="Il campo Password è obbligatorio")> _
        <PasswordPropertyText(True)> _
        Property password() As String

        <DisplayName("Dominio:")> _
        Property domain() As SelectList

        Property errorMessage() As String

        Public Sub New(ByVal domain As SelectList, ByVal errorMessage As String)
            Me.domain = domain
            Me.errorMessage = errorMessage
        End Sub
    End Class

End Namespace
