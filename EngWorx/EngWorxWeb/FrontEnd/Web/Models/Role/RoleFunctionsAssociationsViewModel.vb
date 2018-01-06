Imports System.Web.Mvc
Imports Domain

Namespace Models
    Public Class RoleFunctionsAssociationsViewModel
        Inherits SearchViewModel(Of TFNC)
        Public Property roleFunsParameters As New RoleFunctionsSearchParameters()

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Function getDataParameters() As DataParameter
            Return roleFunsParameters.DataParameters
        End Function
    End Class

    Public Class RoleFunctionsSearchParameters
        Inherits Parameters(Of TFNC)
        Public functionAreas As SelectList = Nothing
        Public RoleList As SelectList = Nothing
        Public Property SelectedRole As String

        Public Property CODROL As String
        Public Property DesRuolo() As String
            Get
                Return m_DesRuolo
            End Get
            Set(value As String)
                m_DesRuolo = value
            End Set
        End Property
        Private m_DesRuolo As String
    End Class
End Namespace