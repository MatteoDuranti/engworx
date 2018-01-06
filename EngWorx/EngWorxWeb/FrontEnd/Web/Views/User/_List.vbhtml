@ModelType Web.Models.UserSearchViewModel
    
@Imports Domain

@code
    Layout = Nothing
    Dim lstImageLinkSx As New List(Of ImageExtensions.ImageLinkClass)
    lstImageLinkSx.Add(New ImageExtensions.ImageLinkClass("Create", "User", Nothing, Url.Content("~/Images/add_item.gif"), "Inserisci Utente"))
    Dim grid As GenericWebGrid = New GenericWebGrid(Html, Model, True, New With {.id = "tabUserList"}, "userListBody", Nothing, "Utenti", lstImageLinkSx,"","grid")
    Dim cols As New List(Of GenericWebGridColumn)
    cols.Add(grid.CreateColumn("DESFSTNAMUSR", "Nome", , , True))
    cols.Add(grid.CreateColumn("DESLSTNAMUSR", "Cognome", , , True))
    cols.Add(grid.CreateColumn("CODUSR", "Matricola", , , True))
    cols.Add(grid.CreateColumn("", "", Function(item) Html.ImageLink("Edit", "User", New With {.codusr = item.CODUSR}, Url.Content("~/Images/edit_item.png"), "Modifica Utente", False), "icon"))
    cols.Add(grid.CreateColumn("", "", Function(item) Html.ImageLink("AssociateRolesToUser", "User", New With {.codusr = item.CODUSR}, Url.Content("~/Images/roles_add.png"), "Associa Ruoli all'Utente", False), "icon"))
    cols.Add(grid.CreateColumn("", "", Function(item) Html.ImageLink("Delete", "User", New With {.codusr = item.CODUSR}, Url.Content("~/Images/delete_item.gif"), "Elimina Utente", Nothing, Nothing, "Si vuol cancellare l\'utente?", False), "icon"))
    grid.AddColumns(cols)
    
End Code
<div id="userListBody">
    @grid.GetHtml()
</div>
