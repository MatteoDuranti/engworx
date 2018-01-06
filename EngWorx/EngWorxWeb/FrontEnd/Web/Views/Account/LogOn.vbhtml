@ModelType Web.Models.LogonFormViewModel
@Imports System.Globalization

@Code
    ViewBag.Title = "LogOn"
    Dim bypass As Boolean = "TRUE".Equals(ConfigurationManager.AppSettings("AUTHENTICATION_BYPASS").ToUpper())
End Code

@section HeaderSectionJS
    <script src="@Url.Content("~/Scripts/jquery.validate.min.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/jquery.autotab-1.1b.js")" type="text/javascript" ></script>
End section

@Using Html.BeginForm()
@<div id="loginBody">
    <table id="tabLogin">
        <tr>
            <td class="tabTitle" colspan="2">Login</td>
        </tr>
        <tr>
            <td class="firstCol">
                @Html.LabelFor(Function(model) model.username)
            </td>
            <td class="secondCol">
                @*Html.TextBoxFor(Function(model) model.username, New With {.maxlength = 6, .autocomplete = "off", .title = "Inserire la Matricola su sei caratteri"})*@
                @Html.TextBox("username", If(bypass, "GI0118", ""), New With {.maxlength = 6, .autocomplete = "off"})
                @Html.ValidationMessageFor(Function(model) model.username, "*")
            </td>                
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td>
                @Html.LabelFor(Function(model) model.password)
            </td>
            <td>
                @*Html.PasswordFor(Function(model) model.password, New With {.maxlength = 20, .title = "Inserire la Password" })*@
                @Html.Password("password", If(bypass, "1234", ""), New With {.maxlength = 20})
                @Html.ValidationMessageFor(Function(model) model.password,"*")
            </td>
        </tr>
        <tr><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td>
                @Html.LabelFor(Function(model) model.domain)
            </td>
            <td>
                @Html.DropDownListFor(Function(model) model.domain, Nothing)
                @*Html.DropDownList("domain", Model.domains)*@
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <input type="submit" name="btnLogOn" id="btnLogOn" value="Login" />
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <label id="lblWait" for="WAIT">Attendere...</label>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
    </table>
    <table id="tabStatus">
        <tr>
            <td class="errMsg" align="center">
                @Html.ValidationSummary()
                <span id="lblErrorMessage" class="redText">@Model.errorMessage</span>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#username, #password').autotab_magic();
            setWaitingOnClick($("#btnLogOn"), $("#lblWait"));
            setFocus("username");
        });
    </script>
</div>
End Using

