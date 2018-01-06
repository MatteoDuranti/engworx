@Imports Web.Models 
@ModelType Web.Models.MenuViewModel

@Code
    Layout = Nothing
End Code

<ul class="sf-menu">
@Code
Dim prevlevel As Nullable(Of Decimal) = 0

    For Each m As NavLink In Model.menu
        '' Chiudere gli LI/UL
        If prevlevel - m.level > 0 Then
            For i = 1 To prevlevel - m.level
            @:</ul>
            @:</li>                            
        Next
    End If
    '' Check Padre/Figlio
    If m.RouteValues IsNot Nothing Then
        '' Figlio
        @:<li><a href="@Url.RouteUrl(m.RouteValues)?menu=1">@m.Text</a></li>
    Else
        If m.level = 1 Then
            @:<li><a href="#">@m.Text</a><ul>    
        Else
            @:<li><a href="#">@m.Text &raquo;</a><ul>
        End If
        
    End If
    prevlevel = m.level
Next
If prevlevel <> 0 Then
        @:</ul>
        @:</li>       
End If
End Code

</ul>

