@Code
    ViewBag.Title = "Home Index"
End Code
<div id="homePageBody"> 
    <ul id="alertList">
    @code
        Dim ret As new StringBuilder()
        Dim sAlertNetLine As String = String.Empty
        Dim sAlertCodTskGapNonInDoc As String = String.Empty
        Dim sAlertMatricole As String = String.Empty
        
        ' CHECK ALERTS TO DISPLAY IN HOME PAGE
        If (1 = 1) Then
            Dim img As New TagBuilder("image")
            img.Attributes.Add("src", Url.Content("~/Images/alert_info.png"))
            img.Attributes.Add("alt", "Attenzione")
            ret.Append("<li>")
            ret.Append(img.ToString())
            ret.Append(" ")
            ret.Append("Sono presenti documenti da elaborare")
            ret.Append("</li>")
        End If
        If (1 = 1) Then
            Dim img As New TagBuilder("image")
            img.Attributes.Add("src", Url.Content("~/Images/alert_warning.png"))
            img.Attributes.Add("alt", "Alert!")
            ret.Append("<li>")
            ret.Append(img.ToString())
            ret.Append(" ")
            ret.Append(Html.ActionLink("Warning generico!", "MyAction", "MyController").ToHtmlString())
            ret.Append("</li>")
        End If
    End Code
    @MvcHtmlString.Create(ret.ToString())
    </ul>   
 </div>
 