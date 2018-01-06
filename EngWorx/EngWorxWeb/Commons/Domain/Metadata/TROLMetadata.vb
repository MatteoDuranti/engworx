Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

<MetadataType(GetType(TROLMetadata))>
Partial Public Class TROL
    Public Sub New()
    End Sub
End Class

Public Class TROLMetadata
    <DisplayName("Codice Ruolo")> _
    <Required(ErrorMessage:="Il codice ruolo è obbligatorio.")> _
    <StringLength(3, ErrorMessage:="Il codice ruolo  deve avere massimo 3 caratteri.")> _
    Public Property CODROL As Global.System.String

    <DisplayName("Descrizione Ruolo")> _
    <Required(ErrorMessage:="La descrizione è obbligatoria.")> _
    <StringLength(50, ErrorMessage:="La descrizione deve avere massimo 50 caratteri.")> _
    Public Property DESROL As Global.System.String
End Class

