@ModelType Web.Models.RoleSearchViewModel
    
@Code
    ViewBag.Title = "Ruoli"
End Code

@Using Html.BeginForm("Search", "Role")
@<div id="roleSearchBody"> 
    <table id="tabRoleSearch">
        <tr>
            <td class="firstCol">Descrizione Ruolo</td>
            <td class="secondCol">
                @Html.TextBoxFor(Function(model) model.roleParameters.SearchParameters.DESROL,Nothing,TextBoxForExtensions.TextCase.toUpper)
                <input type="submit" name="btnSearch" id="btnSearch" value="Cerca" />
            </td>
            <td class="thirdCol">
                <label id="lblWait" for="WAIT">Attendere...</label>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(document).ready(function () {
            setWaitingOnClick($("#btnSearch"), $("#lblWait"), $("#roleListBody"));
        });
    </script>
</div>
End Using

@Code
    If ((Model IsNot Nothing) AndAlso (Model.Data IsNot Nothing)) Then
        Html.RenderPartial("_List", Model)
    End If
End Code
