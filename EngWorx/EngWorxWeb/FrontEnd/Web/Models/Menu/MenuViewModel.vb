Imports System.Web.Routing
Imports System.Collections
Imports System.Collections.Generic

Namespace Models
    Public Class MenuViewModel
        Public menu As List(Of NavLink)
        Public Sub New()
            menu = New List(Of NavLink)()
        End Sub
    End Class
    Public Class NavLink
        Public Property Text() As String
            Get
                Return m_Text
            End Get
            Set(value As String)
                m_Text = value
            End Set
        End Property
        Private m_Text As String
        Public Property RouteValues() As RouteValueDictionary
            Get
                Return m_RouteValues
            End Get
            Set(value As RouteValueDictionary)
                m_RouteValues = value
            End Set
        End Property
        Private m_RouteValues As RouteValueDictionary
        Public Property level() As System.Nullable(Of Decimal)
            Get
                Return m_level
            End Get
            Set(value As System.Nullable(Of Decimal))
                m_level = value
            End Set
        End Property
        Private m_level As System.Nullable(Of Decimal)
        Public Property parentId() As String
            Get
                Return m_parentId
            End Get
            Set(value As String)
                m_parentId = value
            End Set
        End Property
        Private m_parentId As String

        Public Sub New()
        End Sub

        Public Sub New(route As RouteValueDictionary, text As String, level As System.Nullable(Of Decimal), parentId As String)
            Me.Text = text
            Me.RouteValues = route
            Me.level = level

            Me.parentId = parentId
        End Sub
        Public Sub New(text As String, level As System.Nullable(Of Decimal), parentId As String)
            Me.Text = text
            Me.level = level

            Me.parentId = parentId
        End Sub

        Public Sub New(route As RouteValueDictionary, text As String)
            Me.Text = text
            Me.RouteValues = route
        End Sub
        Public Sub New(text As String)
            Me.Text = text
        End Sub
    End Class
End Namespace