Imports System
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports Domain
Imports Repository
Imports RepositoryInterface
Imports System.Web.Configuration

Public Class GenerateReport
    Inherits System.Web.UI.Page

    Private REPORT_SOURCE_PATH As String = WebConfigurationManager.AppSettings("REPORT_SOURCE_PATH")
    Private REPORT_OUTPUT_PATH As String = WebConfigurationManager.AppSettings("REPORT_OUTPUT_PATH")

    Public Sub New()
    End Sub

    'Public Function GenerateRptFltHrs(ByVal obj As Domain.TRPTROSFLTHRS, ByVal sort As String, ByVal sortDirection As String, ByVal pdf As Boolean, ByVal MeseAnno As Date?, ByVal Anni As Integer?) As String
    '    Dim reportName As String = "RptFltHrs.rdlc"
    '    Dim db As dbEntities = New dbEntities
    '    Dim r As IReportFltHrsRepository = New ReportFltHrsRepository(db)
    '    Dim intRow As Integer = 0
    '    Dim listToPrint As List(Of TRPTROSFLTHRS) = r.GetFiltered(obj, sort, sortDirection, 0, 0, intRow).ToList()

    '    Dim parameterList As New List(Of ReportParameter)
    '    Dim pCodSch As New ReportParameter("CODSCH", obj.CODSCH.ToString)
    '    parameterList.Add(pCodSch)
    '    Dim pAnno0 As New ReportParameter("MeseAnno", MeseAnno.Value.ToShortDateString())
    '    parameterList.Add(pAnno0)
    '    Dim filtri As String = ""
    '    If (Not obj.CODCMPGRP Is Nothing AndAlso Not "".Equals(obj.CODCMPGRP)) Then
    '        filtri = filtri + String.Format(" Azienda={0};", obj.CODCMPGRP)
    '    End If
    '    If (Not obj.CODCRWMEM Is Nothing AndAlso Not "".Equals(obj.CODCRWMEM)) Then
    '        filtri = filtri + String.Format(" Matricola={0};", obj.CODCRWMEM)
    '    End If
    '    If (Not obj.NAMFSTNAM Is Nothing AndAlso Not "".Equals(obj.NAMFSTNAM)) Then
    '        filtri = filtri + String.Format(" Nome={0};", obj.NAMFSTNAM)
    '    End If
    '    If (Not obj.NAMLSTNAM Is Nothing AndAlso Not "".Equals(obj.NAMLSTNAM)) Then
    '        filtri = filtri + String.Format(" Cognome={0};", obj.NAMLSTNAM)
    '    End If
    '    If (filtri.Trim = "") Then
    '        filtri = "Filtri: Nessuno"
    '    Else
    '        filtri = String.Format("Filtri: {0}", filtri)
    '    End If
    '    Dim pFiltri As New ReportParameter("filtri", filtri)
    '    parameterList.Add(pFiltri)
    '    'Anni = 5
    '    Dim pAnni As New ReportParameter("Anni", Anni.ToString())
    '    parameterList.Add(pAnni)

    '    Return SaveAndGetFileName(pdf, parameterList, listToPrint, reportName)
    'End Function

    Private Function SaveAndGetFileName(ByVal pdf As Boolean, ByVal lpar As List(Of ReportParameter), ByVal l As IList, ByVal ReportName As String) As String
        Dim rv As New ReportViewer()
        Dim filext As String
        Dim fileType As String
        If (pdf) Then
            filext = "pdf"
            fileType = "pdf"
        Else
            fileType = "Excel"
            filext = "xls"
        End If

        If (l.Count = 0) Then
            Throw New Exception("I dati del report non sono disponibili")
        End If
        rv.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
        rv.LocalReport.ReportPath = REPORT_SOURCE_PATH + ReportName
        rv.LocalReport.DataSources.Add(New Microsoft.Reporting.WebForms.ReportDataSource(rv.LocalReport.GetDataSourceNames()(0), l))
        rv.LocalReport.SetParameters(lpar)

        Dim streamBytes As Byte() = Nothing
        Dim mimeType As String = String.Empty
        Dim encoding As String = String.Empty
        Dim filenameExtension As String = String.Empty
        Dim streamids As [String]() = Nothing
        Dim warnings As Warning() = Nothing

        streamBytes = rv.LocalReport.Render(fileType, Nothing, mimeType, encoding, filenameExtension, streamids, warnings)
        Dim sFileNameGuid = Guid.NewGuid().ToString()
        Dim _FileStream As New System.IO.FileStream(String.Format("{0}{1}.{2}", REPORT_OUTPUT_PATH, sFileNameGuid, filext), System.IO.FileMode.Create, System.IO.FileAccess.Write)
        _FileStream.Write(streamBytes, 0, streamBytes.Length)
        _FileStream.Close()
        Return String.Format("{0}.{1}", sFileNameGuid, filext)
    End Function

    'Public Function GenerateRptCsvImoGap(ByVal obj As Domain.TRPTIMOGAP) As String
    '    Dim filext As String = "csv"
    '    Dim separator As String = ";"

    '    Dim db As dbEntities = New dbEntities
    '    Dim r1 As IReportImoGapRepository = New ReportImoGapRepository(db)

    '    Dim intRow As Integer = 0
    '    obj.FLGEXPSAP = "Y"
    '    Dim lst As IList(Of TRPTIMOGAP) = r1.GetFiltered(obj, "", "", 0, 0, intRow)
    '    Dim sFileNameGuid = Guid.NewGuid().ToString()
    '    Using file As New System.IO.StreamWriter(String.Format("{0}{1}.{2}", REPORT_OUTPUT_PATH, sFileNameGuid, filext))
    '        If lst.Count > 0 Then
    '            For Each x As TRPTIMOGAP In lst
    '                file.Write(x.CODEMRHUMRSR)
    '                file.Write(separator & x.NAMLSTNAM)
    '                file.Write(separator & x.NAMFSTNAM)
    '                file.Write(separator & x.CODCMPGRP)
    '                file.Write(separator & x.CODCRWMEM)
    '                file.Write(separator & x.CODQUFTYP)
    '                file.Write(separator & x.CODPFSLEV)
    '                file.Write(separator & x.CODTYPCTR)
    '                file.Write(separator & x.CODTYPEPL)
    '                file.Write(separator & x.DESTYPCTR)
    '                file.Write(separator & x.DATIMOBEG.ToString("dd/MM/yyyy"))
    '                file.Write(separator & x.DATIMOEND.ToString("dd/MM/yyyy"))
    '                file.Write(separator & x.NUMIMOTOTDAY)
    '                file.Write(separator & x.CODTSK)
    '                file.Write(vbNewLine)
    '            Next
    '        Else
    '            file.Write(vbNewLine)
    '        End If
    '    End Using
    '    Return String.Format("{0}.{1}", sFileNameGuid, filext)
    'End Function
End Class
