Imports System.Reflection

Public Class RunInedit
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()

        Dim patscratch As String = ""

        Dim matrixplace As String = ""
        Dim marxanplace As String = ""
        Dim patplace As String = ""
        Dim ined As String = ""
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
                If vals(0) = "MATRIXPLACE" Then matrixplace = vals(1)
                'If vals(0) = "MARXANPLACE" Then marxanplace = vals(1)
                If vals(0) = "PATSCRATCH" Then patplace = vals(1)
                If vals(0) = "RPLACE" Then rplace = vals(1)
                If vals(0) = "INEDITPLACE" Then marxanplace = vals(1)

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

        If marxanplace = "" Then
            MsgBox("The Location of Inedit executable not specfied or invalid.  Please Specify in the Options Menu before proceeding.", MsgBoxStyle.Exclamation, "InEdit Not Found")
            Exit Sub

        End If

        thing = New InputFile("INEDIT_SCRATCH")
        thing.Show(New Win32HWNDWrapper(My.ArcMap.Application.hWnd))



    End Sub

    Protected Overrides Sub OnUpdate()

    End Sub
End Class
