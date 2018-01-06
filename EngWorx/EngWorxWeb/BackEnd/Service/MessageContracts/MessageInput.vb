Namespace MessageContracts

    <MessageContract()> _
    Public Class MessageInput(Of T)
        <MessageHeader(Name:="HeadIn")>
        Public MessageHeader As HeaderInput

        <MessageBodyMember(Name:="BodyIn")>
        Public Entity As T
    End Class

End Namespace
