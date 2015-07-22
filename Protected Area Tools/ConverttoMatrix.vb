Imports ESRI.ArcGIS.ArcMapUI
Imports System.Reflection

Public Class ConverttoMatrix
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()

        Dim mdoc As IMxDocument
        mdoc = My.ArcMap.Document

        Dim patscratch As String = ""

        Dim matrixplace As String = ""
        Dim marxanplace As String = ""
        Dim patplace As String = ""
        Dim ineditplace As String = ""
        Dim rplace As String = ""

        Dim readpaths As New System.IO.StreamReader(IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) & "\PATInfo\paths.txt")

        Dim line As String

        ' Read first line.
        line = readpaths.ReadLine

        ' Loop over each line in file, While list is Not Nothing.
        Do While (Not line Is Nothing)
            Dim vals As String()
            vals = line.Split("|")
            If vals.Length = 2 Then
                If vals(0) = "MATRIXPLACE" Then marxanplace = vals(1)
                'If vals(0) = "MARXANPLACE" Then marxanplace = vals(1)
                If vals(0) = "PATSCRATCH" Then patplace = vals(1)
                If vals(0) = "RPLACE" Then rplace = vals(1)
                If vals(0) = "INEDITPLACE" Then ineditplace = vals(1)
                'patplace = regKey.GetValue("PATSCRATCH")
                'marxanplace = regKey.GetValue("MARXANHOME")
                'ineditplace = regKey.GetValue("INEDITHOME")
                'rplace = regKey.GetValue("RHOME")
                patscratch = patplace
            End If
            line = readpaths.ReadLine
        Loop

        readpaths.Close()

        Dim thing As InputFile

        If Not My.Computer.FileSystem.FileExists(marxanplace) Then
            marxanplace = ""
        End If

        Dim yea As Microsoft.VisualBasic.MsgBoxResult
        'marxanplace = System.Environment.GetEnvironmentVariable("MARXANHOME")
        'MsgBox(marxanplace)
        If marxanplace = "" Then
            MsgBox("The Location of convert_mtx executable not specfied or invalid.  Please Specify in the Options Menu before proceeding.", MsgBoxStyle.Exclamation, "Convert Matrix Not Found")
            Exit Sub
            'If yea = MsgBoxResult.Yes Then

            '    thing = New InputFile("MATRIXHOME")
            '    thing.Show(New Win32HWNDWrapper(m_application.hWnd))
            '    Exit Sub
            'Else
            '    Exit Sub
            'End If
        End If

        Dim that As RMATX_FORM
        that = New RMATX_FORM(My.ArcMap.Application, marxanplace)
        that.Show(New Win32HWNDWrapper(My.ArcMap.Application.hWnd))

    End Sub

    Protected Overrides Sub OnUpdate()

    End Sub
End Class
