@ModelType Domain.TROL

@Code
    ViewBag.Title = "Ruoli - Modifica Ruolo"
End Code

<script src="@Url.Content("~/Scripts/jquery.validate.min.js")" type="text/javascript"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>
@Using Html.BeginForm()
    @<div id = "roleDetailBody">
    @Html.ValidationSummary(true)
        @code
            Html.RenderPartial("_RoleFields", Model)
        End Code
        <br />
        <table id="tabRoleDetailButton">
            <tr>
                <td align="right">
                    <input type="submit" value="Salva" />
                </td>
                <td align="left">
                    @Html.BackButton("Indietro")
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    @Html.ValidationSummary()
                </td>
            </tr>
        </table>
    </div>    
End Using

