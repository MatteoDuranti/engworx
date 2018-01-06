Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

<MetadataType(GetType(TUSRMetadata))>
Partial Public Class TUSR
    Public Sub New()
    End Sub
End Class

Public Class TUSRMetadata
    <DisplayName("Matricola")> _
    <Required(ErrorMessage:="La matricola è obbligatoria.")> _
    <StringLength(6, MinimumLength:=6, ErrorMessage:="La matricola deve di 6 caratteri.")> _
    Public Property CODUSR As Global.System.String

    <DisplayName("Nome")> _
    <Required(ErrorMessage:="Il nome è obbligatorio.")> _
    <StringLength(40, ErrorMessage:="Il nome deve essere minore o uguale a 40 caratteri.")> _
    Public Property DESFSTNAMUSR As Global.System.String

    <DisplayName("Cognome")> _
    <Required(ErrorMessage:="Il cognome è obbligatorio.")> _
    <StringLength(40, ErrorMessage:="Il cognome utente deve essere minore o uguale a 40 caratteri.")> _
    Public Property DESLSTNAMUSR As Global.System.String

    <DisplayName("Stato")> _
    <Required(ErrorMessage:="Selezionare lo stato utente.")> _
    Public Property CODSTSUSR As Global.System.String

    <DisplayName("Email")> _
    <StringLength(50, ErrorMessage:="L'email deve essere minore o uguale a 50 caratteri.")> _
    Public Property DESEMLUSR As Global.System.String

    <DisplayName("Ente")> _
    <StringLength(6, ErrorMessage:="L'entedeve essere minore o uguale a 6 caratteri.")> _
    Public Property DESENYUSR As Global.System.String

    <DisplayName("Telefono")> _
    <StringLength(30, ErrorMessage:="L'entedeve essere minore o uguale a 30 caratteri.")> _
    Public Property DESTELUSR As Global.System.String
End Class
