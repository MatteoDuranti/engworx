@ModelType Domain.TUSR
@code
    Layout = Nothing
End Code
        <table id="tabUserDetail">
            <tr>
                <td>
                    @Html.LabelFor(Function(model) model.CODUSR)
                </td>
                <td>
                    @Html.TextBoxFor(Function(model) model.CODUSR, IIf("create".Equals(ViewData("OP")), New With {.maxlength = 6, .autocomplete = "off"}, New With {.readonly = "readonly", .disabled = "disabled"}), TextBoxForExtensions.TextCase.toUpper)
                    @Html.ValidationMessageFor(Function(model) model.CODUSR, "*")
                </td>
            </tr>
            <tr>
                <td>
                    @Html.LabelFor(Function(model) model.DESFSTNAMUSR)
                </td>
                <td>
                    @Html.TextBoxFor(Function(model) model.DESFSTNAMUSR,Nothing,TextBoxForExtensions.TextCase.toUpper)
                    @Html.ValidationMessageFor(Function(model) model.DESFSTNAMUSR, "*")
                </td>
            </tr>
            <tr>
                <td>
                    @Html.LabelFor(Function(model) model.DESLSTNAMUSR)
                </td>
                <td>
                    @Html.TextBoxFor(Function(model) model.DESLSTNAMUSR,Nothing,TextBoxForExtensions.TextCase.toUpper )
                    @Html.ValidationMessageFor(Function(model) model.DESLSTNAMUSR, "*")
                </td>
            </tr>
            <tr>
                <td>
                    @Html.LabelFor(Function(model) model.DESEMLUSR)
                </td>
                <td>
                    @Html.EditorFor(Function(model) model.DESEMLUSR)
                    @Html.ValidationMessageFor(Function(model) model.DESEMLUSR,"*")
                </td>
            </tr>
            <tr>
                <td>
                    @Html.LabelFor(Function(model) model.DESTELUSR)
                </td>
                <td>
                    @Html.EditorFor(Function(model) model.DESTELUSR)
                    @Html.ValidationMessageFor(Function(model) model.DESTELUSR,"*")
                </td>
            </tr>
            <tr>
                <td>
                    @Html.LabelFor(Function(model) model.DESENYUSR)
                </td>
                <td>
                    @Html.EditorFor(Function(model) model.DESENYUSR)
                    @Html.ValidationMessageFor(Function(model) model.DESENYUSR,"*")
                </td>
            </tr>
            <tr>
                <td>
                    @Html.LabelFor(Function(model) model.CODSTSUSR)
                </td>
                <td>
                    @Html.DropDownListFor((Function(model) model.CODSTSUSR), ViewData("UserStates"))
                </td>
            </tr>
        </table>    
    
    

