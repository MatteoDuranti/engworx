Namespace DataContracts

    <DataContract(Name:="GetFilteredOf{0}")>
    Public Class GetFiltered(Of T)
        <DataMember(Name:="Parameters")>
        Public Property parameters As T
        <DataMember()>
        Public Property sort As String
        <DataMember()>
        Public Property sortDirection As String
        <DataMember()>
        Public Property pageNumber As Integer
        <DataMember()>
        Public Property pageSize As Integer
        <DataMember()>
        Public Property totalrows As Integer
    End Class

End Namespace
