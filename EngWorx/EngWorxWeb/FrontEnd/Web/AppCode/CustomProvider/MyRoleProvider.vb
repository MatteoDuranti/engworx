Imports System.Web.Security
Imports Web.AppCode.Session
Imports Domain

Namespace AppCode.CustomProvider
    Public Class MyRoleProvider
        Inherits RoleProvider
        Public Overrides Property ApplicationName() As String
            Get
                Throw New System.NotImplementedException()
            End Get
            Set(value As String)
                Throw New System.NotImplementedException()
            End Set
        End Property

        Public Overrides Sub AddUsersToRoles(usernames As String(), roleNames As String())
            Throw New System.NotImplementedException()
        End Sub

        Public Overrides Sub CreateRole(roleName As String)
            Throw New System.NotImplementedException()
        End Sub

        Public Overrides Function DeleteRole(roleName As String, throwOnPopulatedRole As Boolean) As Boolean
            Throw New System.NotImplementedException()
        End Function

        Public Overrides Function FindUsersInRole(roleName As String, usernameToMatch As String) As String()
            Throw New System.NotImplementedException()
        End Function

        Public Overrides Function GetRolesForUser(username As String) As String()
            Dim sessionUser As TUSR = Nothing
            sessionUser = SessionManager.getUser()
            Dim list As String() = New String(sessionUser.TUSRROL.Count - 1) {}
            Dim i As Integer = 0
            For Each Role As TUSRROL In sessionUser.TUSRROL
                list(i) = Role.TROL.CODROL
                i += 1
            Next
            Return list
        End Function

        Public Overrides Function GetUsersInRole(roleName As String) As String()
            Throw New System.NotImplementedException()
        End Function

        Public Overrides Function IsUserInRole(username As String, roleName As String) As Boolean
            Dim sessionUser As TUSR = Nothing
            sessionUser = SessionManager.getUser()
            For Each Role As TUSRROL In sessionUser.TUSRROL
                If Role.TROL.CODROL.Trim().Equals(roleName.Trim()) Then
                    Return True

                End If
            Next
            Return False
        End Function

        Public Overrides Sub RemoveUsersFromRoles(usernames As String(), roleNames As String())
            Throw New System.NotImplementedException()
        End Sub

        Public Overrides Function RoleExists(roleName As String) As Boolean
            Throw New System.NotImplementedException()
        End Function

        Public Overrides Function GetAllRoles() As String()
            Throw New System.NotImplementedException()
        End Function
    End Class
End Namespace