@Imports Web.AppCode.Session
@Imports Web.AppCode.Utility

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
     <!-- IE8 Compatibility -->
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>@ViewBag.Title</title>
    <link href="@Url.Content("~/Images/favicon.ico")" rel="shortcut icon" />

    <!-- LISTA CSS COMUNI -->
    <link href="@Url.Content("~/Content/common.css")?v=0.1" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/Gap.css")?v=0.1" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/superfish/superfish.css")" rel="stylesheet" type="text/css" />

    <!-- LISTA JAVASCRIPT COMUNI -->
    <script src="@Url.Content("~/Scripts/jquery-1.5.1.min.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/jquery.unobtrusive-ajax.min.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/hoverIntent.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/superfish.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/Gap.js")?v=0.1" type="text/javascript"></script> 

    <!-- LISTA CSS E JAVASCRIPT PERSONALIZZATI (INIZIO) -->
    @* Section Style : Per aggiungere a runtime css *@
    @RenderSection("HeaderSectionCSS", false)
    @* Section Script : Per aggiungere a runtime javascript *@
    @RenderSection("HeaderSectionJS", false)

    <script type="text/javascript">
        // initialise plugins
        jQuery(function () {
            //jQuery('ul.sf-menu').superfish();
        });

        $(document).ready(function () {
            $(document).ajaxError(function (e, request, settings, exception) {
                //alert('qui');
                //gestore errori server
                if (request.status == 403) {
                    //alert('2');
                    window.location = '@Url.Action("LogOn", "Account", New With {.message = "Sessione scaduta. Si prega di effettuare di nuovo il login"})' + window.location.hash;
                    return;
                }
                window.location = '@Url.Action("Index", "Error")';
            });
        });
    </script>
</head>
<body id="root">
    <div id="page"> 
        <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b>
        <div id="header">
            @* HEADER DELL'APPLICAZIONE*@
            <div class="logo">
                <a href="@Url.Content("~/Home/Index")"></a>
            </div>
            <div class="version">
                <span id="appName">EngWorx&nbsp;<span id="appVersion">@Utility.getAppVer()</span></span>
            </div>
            <div class="logout">
                @If (Request.IsAuthenticated) Then
                @:<a href="@Url.Action("Logoff", "Account")" >
                    @:<img alt="logoff" src="@Url.Content("~/Images/Logout.gif")" />
                @:</a>
                End If
            </div>
        </div>

        <div id="main">
            <div id="divmenu">
                @Html.Action("Index", "Menu")
            </div>
            <div class="welcome">
                @If SessionManager.getUser() IsNot Nothing AndAlso Not String.IsNullOrEmpty(SessionManager.getUser().DESFSTNAMUSR) Then
                    @:<label>Benvenuto&nbsp&nbsp</label><b>@SessionManager.getUser().DESFSTNAMUSR @SessionManager.getUser().DESLSTNAMUSR</b>
                End If
            </div>
            <div id="divcontent">
                <h3>@Html.BreadCrumbNav(15)</h3>
                <br />
                @RenderBody()
            </div>
        </div>
        <div id="footer">
            <div class="footer-right">
                <span>
                    © Copyright MyEngWorxApp - EngWorx Company S.p.A.
                    P. IVA 12420982766<br/>
                    150, Northcote Street – SW1 1RG London (UK)
                </span>
            </div>
        </div>
        <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b>
    </div>
    <!-- LISTA CSS E JAVASCRIPT PERSONALIZZATI (FINE) -->
    @RenderSection("BottomSectionJS", false)
    @* div per il caricamento del popup in stile modal*@
    <div id="modalPopupDetail"></div>
</body>
</html>
