Imports System.Windows.Forms
Imports System.IO
Imports System.Reflection

Public Class RunMarxan
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()

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
                If vals(0) = "MATRIXPLACE" Then matrixplace = vals(1)
                If vals(0) = "MARXANPLACE" Then marxanplace = vals(1)
                If vals(0) = "PATSCRATCH" Then patplace = vals(1)
                If vals(0) = "RPLACE" Then rplace = vals(1)
                If vals(0) = "INEDITPLACE" Then ineditplace = vals(1)

                patscratch = patplace
            End If
            line = readpaths.ReadLine
        Loop

        readpaths.Close()


        If marxanplace = "" Then
            'regKey.Close()
            MsgBox("The Location of Marxan executable not specfied or invalid.  Please Specify in the Options Menu before proceeding.", MsgBoxStyle.Exclamation, "MARXAN Not Found")
            Exit Sub
            '    'If yea = MsgBoxResult.Yes Then

            '    '    thing = New InputFile("MARXANHOME")
            '    '    thing.Show(New Win32HWNDWrapper(m_application.hWnd))
            '    '    Exit Sub

            '    'Else
            '    '    Exit Sub
            '    'End If
        End If

        Dim putback As String = Environment.CurrentDirectory

        Dim openFileDialog1 As New OpenFileDialog()

        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "dat files (*.dat)|*.dat|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Choose Marxan input.dat file"
        openFileDialog1.Multiselect = False

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Dim result As String = ""
            Dim p As New Process()

            Environment.CurrentDirectory = Path.GetDirectoryName(openFileDialog1.FileName)  '"C:\r\ZonaeCogito122\ZonaeCogito122\sample"
            'Dim psi As New ProcessStartInfo(marxanplace)
            'psi.Arguments = "input.dat"
            p.StartInfo.FileName = marxanplace
            p.StartInfo.CreateNoWindow = True
            p.StartInfo.Arguments = openFileDialog1.FileName
            p.StartInfo.RedirectStandardError = True
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.UseShellExecute = False

            p.EnableRaisingEvents = True

            ' add an Exited event handler
            AddHandler p.OutputDataReceived, AddressOf showresultsHandler

            p.Start()
            My.ArcMap.Application.StatusBar.ProgressBar.Message = "Running Marxan"
            My.ArcMap.Application.StatusBar.ProgressBar.Show()
            'Dim sortStreamWriter As StreamWriter = p.StandardInput

            ' Start the asynchronous read of the sort output stream.
            p.BeginOutputReadLine()

        End If

        Environment.CurrentDirectory = putback


    End Sub


    Public Sub showresultsHandler(ByVal sendingProcess As Object, ByVal outLine As DataReceivedEventArgs)

        'Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
        Dim runner As String()
        'MsgBox(outLine.Data)
        ' Collect the sort command output.
        If Not String.IsNullOrEmpty(outLine.Data) Then
            'numOutputLines += 1
            'GP.AddMessage(outLine.Data)
            If outLine.Data.Contains("Run") Then
                runner = outLine.Data.Split(" ")
                Debug.Print(outLine.Data)
                Debug.Print(runner(1))
                My.ArcMap.Application.StatusBar.ProgressBar.Message = outLine.Data
                My.ArcMap.Application.StatusBar.ProgressBar.Position = CInt(runner(1))
            End If
            If outLine.Data.Contains("The End") Then
                My.ArcMap.Application.StatusBar.ProgressBar.Message = "The End"
                My.ArcMap.Application.StatusBar.ProgressBar.Position = 0
                My.ArcMap.Application.StatusBar.ProgressBar.Hide()
            End If
            '' Add the text to the collected output.
            'sortOutput.Append(Environment.NewLine + "[" _
            '             + numOutputLines.ToString() + "] - " _
            '             + outLine.Data)

        End If
    End Sub

End Class
