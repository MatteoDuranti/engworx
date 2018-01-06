Imports System.ServiceModel.Description
Imports System.ServiceModel.Channels

Public Class ApplyDataContractResolverAttribute
    Inherits Attribute
    Implements IOperationBehavior

    Public Sub New()
    End Sub

    Public Sub AddBindingParameters(ByVal description As OperationDescription, ByVal parameters As BindingParameterCollection) Implements IOperationBehavior.AddBindingParameters
    End Sub

    Public Sub ApplyClientBehavior(ByVal description As OperationDescription, ByVal proxy As System.ServiceModel.Dispatcher.ClientOperation) Implements IOperationBehavior.ApplyClientBehavior
        Dim dataContractSerializerOperationBehavior As DataContractSerializerOperationBehavior = description.Behaviors.Find(Of DataContractSerializerOperationBehavior)()
        dataContractSerializerOperationBehavior.DataContractResolver = New ProxyDataContractResolver()
    End Sub

    Public Sub ApplyDispatchBehavior(ByVal description As OperationDescription, ByVal dispatch As System.ServiceModel.Dispatcher.DispatchOperation) Implements IOperationBehavior.ApplyDispatchBehavior
        Dim dataContractSerializerOperationBehavior As DataContractSerializerOperationBehavior = description.Behaviors.Find(Of DataContractSerializerOperationBehavior)()
        dataContractSerializerOperationBehavior.DataContractResolver = New ProxyDataContractResolver()
    End Sub

    Public Sub Validate(ByVal description As OperationDescription) Implements IOperationBehavior.Validate
        ' Do validation.
    End Sub

End Class
