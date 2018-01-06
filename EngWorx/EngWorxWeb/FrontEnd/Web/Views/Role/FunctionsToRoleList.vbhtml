@ModelType Web.Models.RoleFunctionsAssociationsViewModel

@code
    Layout = Nothing
    Dim grid As New GenericWebGrid(Html, Model, False, New With { _
     Key .id = "tabRoleFunctionList" _
    }, "roleFunctionListBody", Nothing, _
     "")
    Dim CODROL As String = Model.roleFunsParameters.CODROL
    Dim cols As New List(Of GenericWebGridColumn)()
    cols.Add(grid.CreateColumn("DESFNC", "Descrizione", Nothing, Nothing, False))
    'cols.Add(grid.CreateColumn("", "Stato", Function(item)
    '                                                Dim rtn As String = String.Empty
    '                                                rtn = "<div id=""status_" & item.CODFUNID0.ToString() & """>"
    '                                                Dim listaRuoli As IList(Of RIR.Domain.TRUOFUN)
    '                                                listaRuoli = DirectCast(item.TRUOFUN, IList(Of RIR.Domain.TRUOFUN))
    '                                                Dim check As Boolean = listaRuoli.Any(Function(x) x.CODRUOID0.Equals(New Guid(CODRUOID0)))
    '                                                If (check) Then
    '                                                    Dim img As New TagBuilder("image")
    '                                                    img.Attributes.Add("src", Url.Content("~/Images/check.gif"))
    '                                                    img.Attributes.Add("alt", "Permesso concesso")
    '                                                    img.Attributes.Add("title", "Permesso concesso")
    '                                                    rtn = rtn & img.ToString()
    '                                                End If
    '                                                rtn = rtn & "</div>"
    '                                                Return MvcHtmlString.Create(rtn)
    '                                        End Function))
    cols.Add(grid.CreateColumn("", "Stato", Function(item)
                                                    Dim rtn As String = String.Empty
                                                    rtn = "<div id=""status_" & item.CODFNC.ToString() & """>"
                                                    If DirectCast(item.TROLFNC, IList(Of Domain.TROLFNC)).Count > 0 Then
                                                        Dim img As New TagBuilder("image")
                                                        img.Attributes.Add("src", Url.Content("~/Images/check.gif"))
                                                        img.Attributes.Add("alt", "Permesso concesso")
                                                        img.Attributes.Add("title", "Permesso concesso")
                                                        rtn = rtn & img.ToString()
                                                    End If
                                                    rtn = rtn & "</div>"
                                                    Return MvcHtmlString.Create(rtn)
                                            End Function, "icon"))
    cols.Add(grid.CreateColumn("", "", Function(item)
                                               Dim addImg As String = Url.Content("~/Images/add_item.gif")
                                               Dim remImg As String = Url.Content("~/Images/forbidden.gif")
                                               Dim img As New TagBuilder("image")
                                               img.Attributes.Add("id", "btnPermssion_" & item.CODFNC.ToString())
                                               img.Attributes.Add("name", "btnPermssion_" & item.CODFNC.ToString())
                                               If DirectCast(item.TROLFNC, IList(Of Domain.TROLFNC)).Count > 0 Then
                                                   img.Attributes.Add("src", remImg)
                                                   img.Attributes.Add("alt", "Nega permesso")
                                                   img.Attributes.Add("title", "Nega permesso")
                                               Else
                                                   img.Attributes.Add("src", addImg)
                                                   img.Attributes.Add("alt", "Concedi permesso")
                                                   img.Attributes.Add("title", "Concedi permesso")
                                               End If
                                               Return MvcHtmlString.Create(Ajax.ActionLink("[ImgBtn]",
                                                                                            "AddRemovePermission",
                                                                                            New With {.codrol = CODROL.ToString(),
                                                                                                      .codfnc = item.CODFNC.ToString()},
                                                                                            New AjaxOptions With {
                                                                                                .UpdateTargetId = "status_" & item.CODFNC.ToString(),
                                                                                                .OnSuccess = "updatePermissionButton('btnPermssion_" & item.CODFNC.ToString() & "','" + addImg & "','" & remImg & "')", .OnFailure = "alert('errore di aggiornamento permesso');"}).ToHtmlString().Replace("[ImgBtn]", img.ToString()))
                                       End Function, "icon"))
    grid.AddColumns(cols)
End Code
<div id="roleFunctionListBody">
    @grid.GetHtml()
</div>