Imports Web.Models
Imports Domain

Namespace AppCode.Session
    <Serializable()> _
    Public Class MySession

        Public Sub New()
            userRoles = New List(Of TROL)
        End Sub

        '' Porperty per il logon
        Public Property User() As TUSR
        Public Property catchedDeniedAccess As Boolean
        Public Property UserRoles As List(Of TROL)

        '' Menù
        Public Property Menu As MenuViewModel
        '' Property per la ricerca
        Public Property UsersSearchParameters As UserSearchViewModel
        '' Property per la ricerca dei Ruoli
        Public Property RolesSearchParameters As RoleSearchViewModel
    End Class
End Namespace