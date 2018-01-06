@ModelType Web.Models.UserRolesAssociationsViewModel

@Code
    ViewBag.Title = "Utenti - Associazione Ruoli ad Utenti"
End Code

<div id = "associateRolesToUserBody">
    <table id="tabAssociateRolesToUserDetail">
        <colgroup>
            <col class="colField"/>
            <col />
            <col class="colField"/>
            <col />
        </colgroup>
        <tr> 
            <td>
                @Html.LabelFor(Function(model) model.User.DESFSTNAMUSR)
            </td>
            <td>
                <b>@Html.DisplayTextFor(Function(model) model.User.DESFSTNAMUSR)</b>
            </td>
            <td>
                @Html.LabelFor(Function(model) model.User.DESLSTNAMUSR)
            </td>
            <td>
                <b>@Html.DisplayTextFor(Function(model) model.User.DESLSTNAMUSR)</b>
            </td>
        </tr>
        <tr>
            <td>
                @Html.LabelFor(Function(model) model.User.CODUSR, "Matricola")
            </td>
            <td>
                <b>@Html.DisplayTextFor(function(model) model.User.CODUSR)</b>
            </td>
            <td colspan="2"></td>
        </tr>
    </table>
@code
    Dim grid1 As GenericWebGrid = New GenericWebGrid(Html, Model.AssociableRoles, False, New With {.id = "tabAssociableRolesList"}, "associableRolesListBody", "", "")
    Dim cols1 As New List(Of GenericWebGridColumn)
    cols1.Add(grid1.CreateColumnCheckBox("chk1", "CODROL"))
    cols1.Add(grid1.CreateColumn("DESROL", "Ruolo", , , True))
    grid1.AddColumns(cols1)

    Dim grid2 As GenericWebGrid = New GenericWebGrid(Html, Model.AssociatedRoles, False, New With {.id = "tabAssociateRolesList"}, "associateRolesListBody", "", "")
    Dim cols2 As New List(Of GenericWebGridColumn)
    cols2.Add(grid2.CreateColumn("DESROL", "Ruolo", , , True))
    cols2.Add(grid2.CreateColumnCheckBox("chk2", "CODROL", "", True))
    grid2.AddColumns(cols2)
End Code

@Using Html.BeginForm("AssociateRolesToUser", "User")
@<input id="CODUSR" name="CODUSR" type="hidden" value="@Model.User.CODUSR" />
    @<br />
    @<table id="tabAssociateRolesToUserLists" cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th>Ruoli Associati</th>
                <th></th>
                <th>Ruoli Associabili</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td valign="top">
                    <div class ="roleAssociateListBody">
                        @grid2.GetHtml()
                    </div>
                </td>
                <td valign="top">
                    <br />
                    <input type="submit" name="btnDeleteRole" id="btnDeleteRole" value=">" />
                    <br />
                    <input type="submit" name="btnAddRole" id="btnAddRole" value="<" />
                </td>
                <td valign="top">
                    <div class ="associableRolesListBody">
                        @grid1.GetHtml()
                    </div>
                </td>
            </tr>
            <tr><td colspan="3"><br /></td></tr>
            <tr><td colspan="3"><br /></td></tr>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="3" style="margin-left: 20px">@Html.BackButton("Indietro")</td>
            </tr>
        </tfoot>
    </table>
End Using
</div>
