Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Runtime.Serialization
Imports System.Xml

Public Class ProxyDataContractResolver
    Inherits DataContractResolver

    Private _exporter As New XsdDataContractExporter()

    Public Overrides Function ResolveName(ByVal typeName As String, ByVal typeNamespace As String, ByVal declaredType As Type, ByVal knownTypeResolver As DataContractResolver) As Type
        Return knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, Nothing)
    End Function

    Public Overrides Function TryResolveType(ByVal dataContractType As Type, ByVal declaredType As Type, ByVal knownTypeResolver As DataContractResolver, ByRef typeName As XmlDictionaryString, ByRef typeNamespace As XmlDictionaryString) As Boolean
        'TODO MATTEO
        Dim nonProxyType As Type = Entity.Core.Objects.ObjectContext.GetObjectType(dataContractType)
        If nonProxyType IsNot dataContractType Then
            ' Type was a proxy type, so map the name to the non-proxy name
            Dim qualifiedName As XmlQualifiedName = _exporter.GetSchemaTypeName(nonProxyType)
            Dim dictionary As New XmlDictionary(2)
            typeName = New XmlDictionaryString(dictionary, qualifiedName.Name, 0)
            typeNamespace = New XmlDictionaryString(dictionary, qualifiedName.[Namespace], 1)
            Return True
        Else
            ' Type was not a proxy type, so do the default
            Return knownTypeResolver.TryResolveType(dataContractType, declaredType, Nothing, typeName, typeNamespace)
        End If
    End Function
End Class


