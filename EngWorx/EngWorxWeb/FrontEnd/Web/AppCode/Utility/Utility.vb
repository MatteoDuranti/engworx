Imports System
Imports System.Reflection
Imports System.Web.Helpers
Imports System.Net
Imports System.IO
Imports System.Web.Mvc

Namespace AppCode.Utility
    Public Class Utility
        Public Shared Function getAppVer() As String
            Dim assem As [Assembly] = [Assembly].GetExecutingAssembly()
            Dim assemName As AssemblyName = assem.GetName()
            Return "v " & assemName.Version.Major & "." & assemName.Version.Minor & "." & assemName.Version.Build
        End Function

        Public Shared Function retrieveBinaryReport(ByVal fullReportURL As String) As Byte()
            Dim completeData As Byte() = Nothing
            Try
                Dim req As WebRequest = WebRequest.Create(fullReportURL)
                Dim resp As WebResponse = req.GetResponse()
                Dim s As Stream = resp.GetResponseStream()

                Dim cloneStream As New MemoryStream(&H1000)
                Dim mybuffer As Byte() = New Byte(4095) {}
                Dim bytesInBuffer As Integer
                bytesInBuffer = s.Read(mybuffer, 0, mybuffer.Length)
                While (bytesInBuffer > 0)
                    cloneStream.Write(mybuffer, 0, bytesInBuffer)
                    bytesInBuffer = s.Read(mybuffer, 0, mybuffer.Length)
                End While
                cloneStream.Flush()
                cloneStream.Capacity = CInt(cloneStream.Length)
                completeData = cloneStream.GetBuffer()
            Catch ex As WebException
                completeData = Nothing
            End Try
            Return completeData
        End Function

        Public Shared Function getMimeType(ByVal fileExtension As String) As String
            Dim responseContentType As String = Nothing
            Select Case fileExtension.ToUpper()
                Case ".PDF"
                    responseContentType = "application/pdf"
                    Exit Select
                Case ".XLS"
                    responseContentType = "application/vnd.ms-excel"
                    Exit Select
                Case ".CSV"
                    responseContentType = "text/csv"
                    Exit Select
                Case ".XLSX"
                    responseContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Exit Select
                Case ".GIF"
                    responseContentType = "image/gif"
                    Exit Select
                Case ".JPEG"
                    responseContentType = "image/jpg"
                    Exit Select
                Case ".JPG"
                    responseContentType = "image/jpg"
                    Exit Select
                Case ".PNG"
                    responseContentType = "image/png"
                    Exit Select
                Case Else
                    responseContentType = "application/octet-stream"
                    Exit Select
            End Select
            Return responseContentType
        End Function


    End Class
End Namespace


