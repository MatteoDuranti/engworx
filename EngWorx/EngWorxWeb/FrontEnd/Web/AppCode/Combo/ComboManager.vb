Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports log4net
Imports System.Web.Mvc
Imports System.Configuration
Imports System.Web
Imports Domain

Namespace AppCode.Combo
    Public Class CombosManager
        Private Shared log As ILog = LogManager.GetLogger((System.Reflection.MethodBase.GetCurrentMethod().DeclaringType))

        ''' <summary>
        ''' Popola la combo del dominio ( sul logon )
        ''' </summary>
        ''' <param name="selectedValue">Se valorizzato rappresenta il valore selezionato di default nella lista</param>
        ''' <returns>Ritorna una SelectList popolata dal file Domain.xml in App_Data</returns>
        Public Shared Function FillDomains(Optional selectedValue As String = Nothing) As SelectList
            log.Debug("INIT")
            Dim domain As New List(Of String)()
            Dim CompanyFile As String = ConfigurationManager.AppSettings("xmlDomain")
            Dim ds As New DataSet()
            Dim sqlSortField As String = "Code"
            ds.ReadXml(HttpContext.Current.Server.MapPath(CompanyFile))
            Dim dv As New DataView(ds.Tables(0), Nothing, sqlSortField, DataViewRowState.CurrentRows)
            Dim row As DataRow = Nothing
            For Each row_loopVariable As DataRow In dv.Table.Rows
                row = row_loopVariable
                domain.Add(Convert.ToString(row(0)))
            Next
            Dim returnList As New SelectList(domain, selectedValue)
            log.Debug("END")
            Return returnList
        End Function

        Public Shared Function FillRoles(rolesList As List(Of TROL), Optional selectedValue As String = Nothing) As SelectList
            log.Debug("INIT")
            Dim returnList As SelectList = Nothing
            rolesList.Insert(0, New TROL With {.DESROL = ""})
            returnList = New SelectList(rolesList.OrderBy(Function(x) x.DESROL).ToList(), "CODROL", "DESROL", selectedValue)
            log.Debug("END")
            Return returnList
        End Function

        Public Shared Function FillFunctionAreas(functionAreasList As IEnumerable, Optional selectedValue As String = Nothing, Optional blankValue As Boolean = False) As SelectList
            log.Debug("INIT")
            Dim returnList As SelectList = Nothing

            Dim funAreaslst As List(Of TFNC) = DirectCast(functionAreasList, List(Of TFNC))

            If blankValue Then
                Dim blankFun As New TFNC()
                blankFun.DESFNC = "Tutte le Aree"
                funAreaslst.Insert(0, blankFun)
            End If
            returnList = New SelectList(funAreaslst, "CODFNC", "DESFNC", selectedValue)

            log.Debug("END")
            Return returnList
        End Function

        ''' <summary>
        ''' Popola la combo dello stato utente ( A -> Abilitato  / D -> Disabilitato )
        ''' </summary>
        ''' <param name="selectedValue"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function FillUserStates(Optional ByVal selectedValue As String = Nothing) As SelectList
            log.Debug("INIT")
            Dim userStates As New Dictionary(Of String, String)
            userStates.Add("", "")
            userStates.Add("A", "Attivo")
            userStates.Add("D", "Disabilitato")
            Dim returnList As SelectList = New SelectList(userStates, "Key", "Value", selectedValue)
            log.Debug("END")
            Return returnList
        End Function
    End Class
End Namespace
