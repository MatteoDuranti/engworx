@ModelType Exception
@Code
    ViewBag.Title = "Errore"
    Layout = Nothing
End Code

    <div class="redText" style='margin-top:1%;font-weight: bold;'>
        @Model.Message
        <br />
        @Model.InnerException
    </div>
