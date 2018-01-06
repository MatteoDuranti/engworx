Imports System.Web
Imports System.Collections.Generic
Imports Web.Models
Imports Domain

Namespace AppCode.Session
    Public Class SessionManager
        Private Shared Function getSession() As MySession
            If (HttpContext.Current IsNot Nothing) Then
                If ((HttpContext.Current.Session("mysession") IsNot Nothing)) Then
                    Return DirectCast(HttpContext.Current.Session("mysession"), MySession)
                Else
                    Return New MySession()
                End If
            Else
                Return New MySession()
            End If
        End Function

        Private Shared Sub writeSession(session As MySession)
            If (HttpContext.Current IsNot Nothing) Then
                HttpContext.Current.Session.Add("mysession", session)
            End If
        End Sub

        Public Shared Sub cleanSession()
            If (HttpContext.Current IsNot Nothing) Then
                If ((HttpContext.Current.Session("mysession") IsNot Nothing)) Then
                    HttpContext.Current.Session.Clear()
                End If
            End If
        End Sub

        Public Shared Function getUser() As TUSR
            Dim objSession As MySession = getSession()
            Return objSession.User
        End Function

        Public Shared Sub setUser(user As TUSR)
            Dim objSession As MySession = SessionManager.getSession()
            objSession.User = user
            SessionManager.writeSession(objSession)
        End Sub

        Public Shared Function getCatchedDeniedAccess() As Boolean
            Dim objSession As MySession = SessionManager.getSession()
            Return objSession.catchedDeniedAccess
        End Function

        Public Shared Sub setCatchedDeniedAccess(catchedDeniedAccess As Boolean)
            Dim objSession As MySession = SessionManager.getSession()
            objSession.CatchedDeniedAccess = catchedDeniedAccess
            SessionManager.writeSession(objSession)
        End Sub

        Public Shared Function getMenu() As MenuViewModel
            Dim objSession As MySession = getSession()
            Return objSession.Menu
        End Function

        Public Shared Sub setMenu(menu As MenuViewModel)
            Dim objSession As MySession = SessionManager.getSession()
            objSession.Menu = menu
            SessionManager.writeSession(objSession)
        End Sub

        Public Shared Function getUsersSearchParameter() As UserSearchViewModel
            Dim objSession As MySession = SessionManager.getSession()
            Return objSession.UsersSearchParameters
        End Function

        Public Shared Sub setUsersSearchParameter(userSearchParameters As UserSearchViewModel)
            Dim objSession As MySession = SessionManager.getSession()
            objSession.UsersSearchParameters = userSearchParameters
            SessionManager.writeSession(objSession)
        End Sub

        Public Shared Function getRolesSearchParameter() As RoleSearchViewModel
            Dim objSession As MySession = SessionManager.getSession()
            Return objSession.RolesSearchParameters
        End Function

        Public Shared Sub setRolesSearchParameter(roleSearchParameters As RoleSearchViewModel)
            Dim objSession As MySession = SessionManager.getSession()
            objSession.RolesSearchParameters = roleSearchParameters
            SessionManager.writeSession(objSession)
        End Sub

        Public Shared Sub setUserRoles(roles As List(Of TROL))
            Dim objSession As MySession = SessionManager.getSession()
            objSession.UserRoles = roles
            SessionManager.writeSession(objSession)
        End Sub

        Public Shared Function getUserRoles() As List(Of TROL)
            Dim objSession As MySession = SessionManager.getSession()
            Return objSession.UserRoles
        End Function
    End Class
End Namespace