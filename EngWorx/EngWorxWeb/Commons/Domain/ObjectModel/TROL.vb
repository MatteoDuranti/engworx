'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic
Imports System.Runtime.Serialization

<DataContract(IsReference:=True)>
Partial Public Class TROL
    <DataMember()>
    Public Property CODROL As String
    <DataMember()>
    Public Property DESROL As String
    <DataMember()>
    Public Property CODUSRMOD As String
    <DataMember()>
    Public Property FLGMODTYP As String
    <DataMember()>
    Public Property TMSLSTMOD As Nullable(Of Date)

    <DataMember()>
    Public Overridable Property TROLFNC As ICollection(Of TROLFNC) = New HashSet(Of TROLFNC)
    <DataMember()>
    Public Overridable Property TUSRROL As ICollection(Of TUSRROL) = New HashSet(Of TUSRROL)

End Class