Imports System.ServiceModel
Imports Domain
Imports ServiceProxy.WcfService

Namespace WcfConsumer
    Public Class WcfServices

#Region "USER - ROLE -FUNCTION -TSK"
        Public Shared Function InsertUser(ByVal newUser As TUSR) As Boolean
            Dim ret As Boolean = False
            Dim msgIn = New MessageInputOf_TUSR() With {.BodyIn = newUser, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_TUSR()
            msgOut = Wcf.InsertUser(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = Convert.ToBoolean(msgOut.HeadOut.serviceParam)
            Return ret
        End Function

        Public Shared Function GetUserRolesByCompanyAndUsername(ByVal usr As TUSR) As TUSR
            Dim tUte As New TUSR
            Dim msgIn = New MessageInputOf_TUSR() With {.BodyIn = usr, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_TUSR
            msgOut = Wcf.GetUserRolesByCompanyAndUsername(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            tUte = msgOut.BodyOut
            Return tUte
        End Function

        Public Shared Function GetUserByPrimaryKey(ByVal usr As TUSR) As TUSR
            Dim tUte As New TUSR
            Dim msgIn = New MessageInputOf_TUSR() With {.BodyIn = usr, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_TUSR
            msgOut = Wcf.GetUserByPrimaryKey(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            tUte = msgOut.BodyOut
            Return tUte
        End Function

        Public Shared Function GetRolesAndAllowedFunctions(ByVal user As TUSR) As List(Of TROL)
            Dim tListRoles As New List(Of TROL)
            Dim msgIn = New MessageInputOf_TUSR() With {.BodyIn = user, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_ListOf_TROL
            msgOut = Wcf.GetRolesAndAllowedFunctions(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            tListRoles = msgOut.BodyOut.ToList
            Return tListRoles
        End Function

        Public Shared Function GetFilteredUsers(ByVal parameters As TUSR, ByVal sort As String, ByVal sortDirection As String, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalrows As Integer) As IList(Of TUSR)
            Dim lTute As IList(Of TUSR)
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgIn As New MessageInputOf_GetFilteredOf_TUSR
            Dim fT As New GetFilteredOfTUSR With {
                .Parameters = parameters,
                .sort = sort,
                .sortDirection = sortDirection,
                .pageSize = pageSize,
                .pageNumber = pageNumber,
                .totalrows = totalrows
            }
            msgIn.BodyIn = fT
            Dim msgOut As New MessageOutputOf_IListOf_TUSR
            msgIn.HeadIn = ServiceProxyUtility.createMessageHeaderInput(msgIn.BodyIn)
            msgOut = Wcf.GetFilteredUsers(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            totalrows = Convert.ToInt32(msgOut.HeadOut.serviceParam)
            lTute = msgOut.BodyOut
            Return lTute
        End Function

        Public Shared Function UpdateUser(ByVal usr As TUSR) As Boolean
            Dim ret As Boolean = False
            Dim tUte As New TUSR
            Dim msgIn = New MessageInputOf_TUSR() With {.BodyIn = usr, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_TUSR
            msgOut = Wcf.UpdateUser(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = Convert.ToBoolean(msgOut.HeadOut.serviceParam)
            Return ret
        End Function

        Public Shared Function GetFilteredRoles(ByVal parameters As TROL, ByVal sort As String, ByVal sortDirection As String, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalrows As Integer) As IList(Of TROL)
            Dim lTruo As IList(Of TROL)
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgIn As New MessageInputOf_GetFilteredOf_TROL
            Dim msgOut As New MessageOutputOf_IListOf_TROL
            Dim p As New GetFilteredOfTROL With {
              .Parameters = parameters,
              .sort = sort,
              .sortDirection = sortDirection,
              .pageSize = pageSize,
              .pageNumber = pageNumber,
              .totalrows = totalrows
            }
            msgIn.BodyIn = p
            msgIn.HeadIn = ServiceProxyUtility.createMessageHeaderInput(msgIn.BodyIn)
            msgOut = Wcf.GetFilteredRoles(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            totalrows = Convert.ToInt32(msgOut.HeadOut.serviceParam)
            lTruo = msgOut.BodyOut
            Return lTruo
        End Function

        Public Shared Function DeleteUser(ByVal user As TUSR, Optional ByVal DeleteOnCascade As Boolean = False) As Boolean
            Dim ret As Boolean = False
            Dim msgIn = New MessageInputOf_TUSR() With {.BodyIn = user, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            msgIn.HeadIn.serviceParam = DeleteOnCascade
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_TUSR
            msgOut = Wcf.DeleteUser(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = Convert.ToBoolean(msgOut.HeadOut.serviceParam)
            Return ret
        End Function

        Public Shared Function InsertUserRole(ByVal lstUserRole As List(Of TUSRROL)) As Boolean
            Dim ret As Boolean
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgIn As New MessageInputOf_ListOf_TUSRROL
            Dim msgOut As New MessageOutputOf_TUSRROL
            msgIn.BodyIn = lstUserRole.ToArray()
            msgIn.HeadIn = ServiceProxyUtility.createMessageHeaderInput(msgIn.BodyIn)
            msgOut = Wcf.InsertUserRole(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = Convert.ToBoolean(msgOut.HeadOut.serviceParam)
            Return ret
        End Function

        Public Shared Function DeleteUserRole(ByVal lstUserRole As List(Of TUSRROL)) As Boolean
            Dim ret As Boolean
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgIn As New MessageInputOf_ListOf_TUSRROL
            Dim msgOut As New MessageOutputOf_TUSRROL
            msgIn.BodyIn = lstUserRole.ToArray()
            msgIn.HeadIn = ServiceProxyUtility.createMessageHeaderInput(msgIn.BodyIn)
            msgOut = Wcf.DeleteUserRole(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = Convert.ToBoolean(msgOut.HeadOut.serviceParam)
            Return ret
        End Function

        Public Shared Function GetRoleByPrimaryKey(ByVal role As TROL) As TROL
            Dim ret As New TROL
            Dim msgIn = New MessageInputOf_TROL With {.BodyIn = role, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_TROL
            msgOut = Wcf.GetRoleByPrimaryKey(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = msgOut.BodyOut
            Return ret
        End Function

        Public Shared Function GetFunctionsWithPermissions(ByVal role As TROL) As ICollection(Of TFNC)
            Dim ret As ICollection(Of TFNC)
            Dim msgIn = New MessageInputOf_TROL() With {.BodyIn = role, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_ListOf_TFNC
            msgOut = Wcf.GetFuncionsWithPermissions(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = msgOut.BodyOut
            Return ret
        End Function

        Public Shared Function GetAllRoles() As IList(Of TROL)
            Dim ret As IList(Of TROL)
            Dim msgIn = New MessageInputOf_TROL() With {.BodyIn = New TROL(), .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_IListOf_TROL
            msgOut = Wcf.GetAllRoles(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = msgOut.BodyOut
            Return ret
        End Function

        Public Shared Function GetMenuByUser(ByVal user As TUSR) As IList(Of VMEN)
            Dim ret As IList(Of VMEN)
            Dim msgIn = New MessageInputOf_TUSR() With {.BodyIn = user, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_ListOf_VMEN
            msgOut = Wcf.GetMenuByUser(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = msgOut.BodyOut
            Return ret
        End Function

        Public Shared Function GetRolesAssociableToUser(ByVal user As TUSR) As IList(Of TROL)
            Dim ret As IList(Of TROL)
            Dim msgIn = New MessageInputOf_TUSR() With {.BodyIn = user, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_IListOf_TROL
            msgOut = Wcf.GetRolesAssociableToUser(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = msgOut.BodyOut
            Return ret
        End Function

        Public Shared Function getRolesAssociatedToUser(ByVal user As TUSR) As IList(Of TROL)
            Dim ret As IList(Of TROL)
            Dim msgIn = New MessageInputOf_TUSR With {.BodyIn = user, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_ListOf_TROL
            msgOut = Wcf.getRolesAssociatedToUser(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = msgOut.BodyOut
            Return ret
        End Function

        Public Shared Function getRolesByControllerAction(ByVal controller As String, ByVal action As String) As IList(Of TROL)
            Dim ret As IList(Of TROL)
            Dim tfun As New TFNC
            tfun.DESACTCTL = action
            tfun.DESCTL = controller
            Dim msgIn = New MessageInputOf_TFNC With {.BodyIn = tfun, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_ListOf_TROL
            msgOut = Wcf.getRolesByControllerAction(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = msgOut.BodyOut
            Return ret
        End Function

        Public Shared Function InsertDeleteAssociationFunctToRole(ByVal roleFunction As TROLFNC) As Boolean
            Dim ret As Boolean = False
            Dim msgIn = New MessageInputOf_TROLFNC With {.BodyIn = roleFunction, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_TROLFNC
            msgOut = Wcf.InsertDeleteAssociationFunctToRole(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = Convert.ToBoolean(msgOut.HeadOut.serviceParam)
            Return ret
        End Function

        Public Shared Function UpdateRole(ByVal role As TROL) As Boolean
            Dim ret As Boolean = False
            Dim msgIn = New MessageInputOf_TROL With {.BodyIn = role, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_TROL
            msgOut = Wcf.UpdateRole(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = Convert.ToBoolean(msgOut.HeadOut.serviceParam)
            Return ret
        End Function

        Public Shared Function InsertRole(ByVal role As TROL) As Boolean
            Dim ret As Boolean = False
            Dim msgIn = New MessageInputOf_TROL With {.BodyIn = role, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_TROL
            msgOut = Wcf.InsertRole(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = Convert.ToBoolean(msgOut.HeadOut.serviceParam)
            Return ret
        End Function

        Public Shared Function DeleteRole(ByVal role As TROL, Optional ByVal DeleteOnCascade As Boolean = False) As Boolean
            Dim ret As Boolean = False
            Dim msgIn = New MessageInputOf_TROL With {.BodyIn = role, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            msgIn.HeadIn.serviceParam = DeleteOnCascade
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_TROL
            msgOut = Wcf.DeleteRole(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            ret = CBool(msgOut.HeadOut.serviceParam)
            Return ret
        End Function

        Public Shared Function CheckDbConnection() As Boolean
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim ret As Boolean = False
            ret = Wcf.CheckDbConnection()
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            Return ret
        End Function
#End Region

#Region "System Parameter"
        Public Shared Function GetSysParByPrimaryKey(ByVal t As TSYSPAR) As TSYSPAR
            Dim msgIn = New MessageInputOf_TSYSPAR With {.BodyIn = t, .HeadIn = ServiceProxyUtility.createMessageHeaderInput(.BodyIn)}
            Dim channel As ChannelFactory(Of WcfService.IWcfService) = ServiceProxyUtility.getwcfChannelGapOpened()
            Dim Wcf = channel.CreateChannel()
            Dim msgOut = New MessageOutputOf_TSYSPAR
            msgOut = Wcf.GetSysParByPrimaryKey(msgIn)
            ServiceProxyUtility.closeChannel(DirectCast(channel, ChannelFactory))
            Return msgOut.BodyOut
        End Function
#End Region

    End Class
End Namespace

