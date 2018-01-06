Imports Domain

Namespace Models
    Public Class RoleSearchViewModel
        Inherits SearchViewModel(Of TROL)

        Public Property roleParameters As New RoleSearchParameters()

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Function getDataParameters() As DataParameter
            Return roleParameters.DataParameters
        End Function
    End Class

    Public Class RoleSearchParameters
        Inherits Parameters(Of TROL)

    End Class
End Namespace