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
Partial Public Class TROLFNC
    <DataMember()>
    Public Property CODROL As String
    <DataMember()>
    Public Property CODFNC As String
    <DataMember()>
    Public Property DATASC As Nullable(Of Date)
    <DataMember()>
    Public Property CODUSRMOD As String
    <DataMember()>
    Public Property FLGMODTYP As String
    <DataMember()>
    Public Property TMSLSTMOD As Nullable(Of Date)

    <DataMember()>
    Public Overridable Property TFNC As TFNC
    <DataMember()>
    Public Overridable Property TROL As TROL

End Class
