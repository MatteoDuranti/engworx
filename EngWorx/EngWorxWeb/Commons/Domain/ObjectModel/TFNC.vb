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
Partial Public Class TFNC
    <DataMember()>
    Public Property CODFNC As String
    <DataMember()>
    Public Property CODFNCFAT As String
    <DataMember()>
    Public Property DESFNC As String
    <DataMember()>
    Public Property DESACTCTL As String
    <DataMember()>
    Public Property DESCTL As String
    <DataMember()>
    Public Property CODODR As Nullable(Of Short)
    <DataMember()>
    Public Property CODUSRMOD As String
    <DataMember()>
    Public Property FLGMODTYP As String
    <DataMember()>
    Public Property TMSLSTMOD As Nullable(Of Date)

    <DataMember()>
    Public Overridable Property TROLFNC As ICollection(Of TROLFNC) = New HashSet(Of TROLFNC)

End Class
