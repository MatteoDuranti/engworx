Namespace MessageContracts

    <MessageContract()> _
    Public Class MessageOutput(Of T)
        <MessageHeader(Name:="HeadOut")>
        Public MessageHeader As HeaderOutput

        <MessageBodyMember(Name:="BodyOut")>
        Public Entity As T
    End Class

End Namespace
