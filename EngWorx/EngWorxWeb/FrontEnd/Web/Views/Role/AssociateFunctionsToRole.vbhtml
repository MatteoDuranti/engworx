@ModelType Web.Models.RoleFunctionsAssociationsViewModel

@Code
    ViewBag.Title = "AssociateFunctionsToRole"
End Code
<div id="roleFunctionSearchBody">
    <table id="tabRoleFunctionSearch">
        <tr>
            <td>
                Ruolo 
            </td>
            <td> &nbsp;</td>
            <td>

            @code
                If (Model.roleFunsParameters.RoleList IsNot Nothing) Then
                    Using Html.BeginForm()
                        @Html.DropDownListFor(Function(m) m.roleFunsParameters.SelectedRole, DirectCast(Model.roleFunsParameters.RoleList, System.Web.Mvc.SelectList), New With {.onchange = "this.form.submit();"})             
                    End Using
                Else
                    @:<b>@Html.LabelForModel(Model.roleFunsParameters.DesRuolo) </b>
                End If
            End Code
            </td>
        </tr>
    </table>
</div>

@if ((Model isNot Nothing ) AndAlso (Model.Data isnot Nothing))
    Html.RenderPartial("FunctionsToRoleList", Model)
end if
<br />
<div id="roleFunctionSearchBodyBtn">
    <table>
        <tr>
            <td>
@If (Model.roleFunsParameters.RoleList Is Nothing) Then
    @Html.BackButton("Indietro")    
End If
            </td>
        </tr>
    </table>
</div>



