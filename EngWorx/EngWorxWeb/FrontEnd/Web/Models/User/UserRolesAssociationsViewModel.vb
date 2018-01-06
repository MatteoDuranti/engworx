Imports Domain

Namespace Models

    Public Class UserRolesAssociationsViewModel
        Private _user As TUSR
        Private _associableRoles As RoleSearchViewModel
        Private _associatedRoles As RoleSearchViewModel

        Public Property User() As TUSR
            Get
                Return _user
            End Get
            Set(value As TUSR)
                _user = value
            End Set
        End Property

        Public Property AssociableRoles() As RoleSearchViewModel
            Get
                Return _associableRoles
            End Get
            Set(value As RoleSearchViewModel)
                _associableRoles = value
            End Set
        End Property

        Public Property AssociatedRoles() As RoleSearchViewModel
            Get
                Return _associatedRoles
            End Get
            Set(value As RoleSearchViewModel)
                _associatedRoles = value
            End Set
        End Property

        Public Sub New()
            AssociableRoles = New RoleSearchViewModel()
            AssociatedRoles = New RoleSearchViewModel()
        End Sub

    End Class
End Namespace