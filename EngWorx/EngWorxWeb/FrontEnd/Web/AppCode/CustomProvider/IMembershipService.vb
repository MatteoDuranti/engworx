Imports System.Web.Security

Namespace AppCode.CustomProvider
    Public Interface IMembershipService
        ReadOnly Property MinPasswordLength() As Integer
        Function ChangePassword(userName As String, oldPassword As String, newPassword As String) As Boolean
        Function CreateUser(userName As String, password As String, email As String) As MembershipCreateStatus
        Function ValidateUser(userName As String, password As String) As Boolean
        Function ValidateUser(userName As String, password As String, domain As String) As Boolean
    End Interface
End Namespace