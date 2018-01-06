@ModelType Web.Models.RoleSearchViewModel
    
@Imports Domain 

@code
    Layout = Nothing
    
    Dim lstImageLinkSx As New List(Of ImageExtensions.ImageLinkClass)
    
    lstImageLinkSx.Add(New ImageExtensions.ImageLinkClass("Create", "Role", Nothing, Url.Content("~/Images/add_item.gif"), "Inserisci Ruole"))
    
    Dim grid As GenericWebGrid = New GenericWebGrid(Html, Model, True, New With {.id = "tabRolesList"}, "roleListBody", Nothing, "Role", lstImageLinkSx)
    Dim cols As New List(Of GenericWebGridColumn)

    cols.Add(grid.CreateColumn("CODROL", "Codice", , , True))
    cols.Add(grid.CreateColumn("DESROL", "Descrizione", , , True))
    cols.Add(grid.CreateColumn("", "", Function(item) Html.ImageLink("Edit", "Role", New With {.codrol = item.CODROL}, Url.Content("~/Images/edit_item.png"), "Modifica Ruolo", False), "icon"))
    cols.Add(grid.CreateColumn("", "", Function(item) Html.ImageLink("Delete", "Role", New With {.codrol = item.CODROL}, Url.Content("~/Images/delete_item.gif"), "Elimina Ruolo", Nothing, Nothing, "Si vuol cancellare il Ruolo?", False), "icon"))
    cols.Add(grid.CreateColumn("", "", Function(item) Html.ImageLink("AssociateFunctionsToRole", "Role", New With {.codrol = item.CODRol}, Url.Content("~/Images/permission.gif"), "Permessi", False), "icon"))

    grid.AddColumns(cols)
End Code

<div id="roleListBody">
    @grid.GetHtml()
</div>