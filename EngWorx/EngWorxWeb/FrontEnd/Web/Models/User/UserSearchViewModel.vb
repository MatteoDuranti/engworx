Imports Domain

Namespace Models

    Public Class UserSearchViewModel
        Inherits SearchViewModel(Of TUSR)

        Public Property userParameters() As UserSearchParameters
            Get
                Return m_userParameters
            End Get
            Set(value As UserSearchParameters)
                m_userParameters = value
            End Set
        End Property

        Private m_userParameters As UserSearchParameters

        Public Sub New()
            MyBase.New()
            userParameters = New UserSearchParameters()
        End Sub

        Protected Overrides Function getDataParameters() As DataParameter
            Return userParameters.DataParameters
        End Function
    End Class

    Public Class UserSearchParameters
        Inherits Parameters(Of TUSR)
    End Class
End Namespace