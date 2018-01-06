@ModelType Web.Models.UserSearchViewModel

@Code
    ViewBag.Title = "Utenti - Ricerca Utenti"
End Code

@Using Html.BeginForm("Search", "User")
@<div id="userSearchBody"> 
    <table id = "tabUserSearch">
        <tr>
            <td>
                @Html.LabelFor(Function(model) model.userParameters.SearchParameters.CODUSR)
            </td>
            <td>
                @Html.TextBoxFor(Function(model) model.userParameters.SearchParameters.CODUSR, New With {.maxlength = 6}, TextBoxForExtensions.TextCase.toUpper)
            </td>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td>
                @Html.LabelFor(Function(model) model.userParameters.SearchParameters.DESFSTNAMUSR)
            </td>
            <td>
                @Html.TextBoxFor(Function(model) model.userParameters.SearchParameters.DESFSTNAMUSR, New With {.maxlength = 30}, TextBoxForExtensions.TextCase.toUpper)
            </td>
           <td>
                @Html.LabelFor(Function(model) model.userParameters.SearchParameters.DESLSTNAMUSR)
            </td>
            <td>
                @Html.TextBoxFor(Function(model) model.userParameters.SearchParameters.DESLSTNAMUSR, New With {.maxlength = 30}, TextBoxForExtensions.TextCase.toUpper)
            </td>
        </tr>
        <tr>
            <td>
                @Html.LabelFor(Function(model) model.userParameters.SearchParameters.CODSTSUSR)
            </td>
            <td>
                @Html.DropDownListFor((Function(model) model.userParameters.SearchParameters.CODSTSUSR), ViewData("UserStates"))
            </td>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <input type="submit" id="btnSearch" name="btnSearch" value="Cerca" />&nbsp;&nbsp;
                <label id="lblWait" for="WAIT">Attendere...</label>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(document).ready(function () {
            setWaitingOnClick($("#btnSearch"), $("#lblWait"), $("#userListBody"));
        });
    </script>
</div>
End Using

@Code
    If ((Model IsNot Nothing) AndAlso (Model.Data IsNot Nothing)) Then
        Html.RenderPartial("_List", Model)
    End If
End Code
