@ModelType Domain.TROL

@code
    Layout = Nothing
End Code
<table id = "tabRoleDetail">
 <tr >
        
        <td>
            @Html.LabelFor(Function(model) model.CODROL)
        </td>
        <td> 
            @Html.TextBoxFor(Function(model) model.CODROL, IIf("create".Equals(ViewData("OP")), New With {.maxlength = 3, .autocomplete = "off"}, New With {.readonly = "readonly", .disabled = "disabled"}),TextBoxForExtensions.TextCase.toUpper) 
            @Html.ValidationMessageFor(Function(model) model.CODROL, "*")
        </td>
    </tr>

    <tr >
        
        <td>
            @Html.LabelFor(Function(model) model.DESROL)
        </td>
        <td>
            @Html.EditorFor(Function(model) model.DESROL)
            @Html.ValidationMessageFor(Function(model) model.DESROl, "*")
        </td>
    </tr>
</table>