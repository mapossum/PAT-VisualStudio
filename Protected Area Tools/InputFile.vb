Imports System.Windows.Forms
Imports System.Reflection

Public Class InputFile

    Dim typ As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        If typ = "INEDIT_SCRATCH" Then

            Dim putback As String = Environment.CurrentDirectory

            Dim patscratch As String = ""
            'rkey = "HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\Session Manager\Environment"
            'Dim regKey As Microsoft.Win32.RegistryKey
            'regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\Session Manager\Environment", False)
            'patscratch = regKey.GetValue("PAT_SCRATCH")
            'regKey.Close()

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
                    If vals(0) = "MARXANPLACE" Then marxanplace = vals(1)
                    If vals(0) = "PATSCRATCH" Then patplace = vals(1)
                    If vals(0) = "RPLACE" Then rplace = vals(1)
                    If vals(0) = "INEDITPLACE" Then ined = vals(1)
                    'patplace = regKey.GetValue("PATSCRATCH")
                    'marxanplace = regKey.GetValue("MARXANHOME")
                    'ineditplace = regKey.GetValue("INEDITHOME")
                    'rplace = regKey.GetValue("RHOME")
                    patscratch = patplace
                End If
                line = readpaths.ReadLine
            Loop

            readpaths.Close()

            Dim cmd As String = "copy "

            cmd = "copy " + Chr(34) + ined + Chr(34) + " " + Chr(34) + TextBox1.Text + "\Inedit.exe" + Chr(34) + " /Y"

            Dim fi As New IO.FileInfo(patscratch & "\ined.bat")
            fi.Delete()

            'If System.IO.File.Exists(TextBox1.Text + "\input.dat") = False Then
            '    Dim b As Awriter = New Awriter(TextBox1.Text + "\input.dat")
            '    b.WriteLine("")
            '    b.Close()
            'End If

            Dim a As Awriter = New Awriter(patscratch & "\ined.bat")
            a.WriteLine(cmd)
            a.WriteLine("cd " + Chr(34) + TextBox1.Text + Chr(34))
            a.WriteLine("Inedit.exe")
            a.Close()

            Me.Hide()
            Shell(patscratch & "\ined.bat")


            'regKey.Close()

            Me.Close()
            Exit Sub
        End If

        If Not (My.Computer.FileSystem.FileExists(TextBox1.Text) Or My.Computer.FileSystem.DirectoryExists(TextBox1.Text)) Then
            MsgBox("Specified file does not exist")
        ElseIf typ <> "MEX" Then
            Try
                Dim rkey As String
                rkey = "HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\Session Manager\Environment"
                Dim regKey As Microsoft.Win32.RegistryKey
                regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\Session Manager\Environment", True)
                regKey.SetValue(typ, TextBox1.Text)
                'System.Environment.SetEnvironmentVariable(typ, TextBox1.Text)
            Catch ex As Exception
                MsgBox("You do not have adaquate permissions to edit system variables.", MsgBoxStyle.Critical, "USER PERMISSION ERROR")
            End Try
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()

        Else 'If 'typ = "MEX" Then

            Me.Hide()
            Dim marxanplace As String = ""
            Dim patscratch As String = ""


            Dim matrixplace As String = ""
            'Dim marxanplace As String = ""
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
                    'patplace = regKey.GetValue("PATSCRATCH")
                    'marxanplace = regKey.GetValue("MARXANHOME")
                    'ineditplace = regKey.GetValue("INEDITHOME")
                    'rplace = regKey.GetValue("RHOME")
                    patscratch = patplace
                End If
                line = readpaths.ReadLine
            Loop

            readpaths.Close()

            If patscratch = "" Then
                If marxanplace = "" Then
                    MsgBox("You must set the PAT SCRATCH and MARXAN folder in options menu.", MsgBoxStyle.Exclamation, "PAT SCRATCH not found")
                    Exit Sub
                End If
            End If

            Dim putback As String = Environment.CurrentDirectory

            Dim destfile As String

            Dim testFile As System.IO.FileInfo
            testFile = My.Computer.FileSystem.GetFileInfo(TextBox1.Text)
            Dim folderPath As String = testFile.DirectoryName
            destfile = folderPath + "\Marxan.exe"

            Dim cmd As String = "copy "

            cmd = "copy " + Chr(34) + marxanplace + Chr(34) + " " + Chr(34) + destfile + Chr(34) + " /Y"

            Dim fi As New IO.FileInfo(patscratch & "\mar.bat")
            fi.Delete()

            Dim a As Awriter = New Awriter(patscratch & "\mar.bat")
            a.WriteLine(cmd)
            a.WriteLine("cd " + Chr(34) + folderPath + Chr(34))
            a.WriteLine("Marxan.exe input.dat")
            a.Close()

            Me.Hide()
            'regKey.Close()
            Shell(patscratch & "\mar.bat")

            Me.Close()

            Environment.CurrentDirectory = putback

        End If


    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        If InStr(typ, "_SCRATCH") Then
            fbd.ShowNewFolderButton = True
            fbd.ShowDialog()
            TextBox1.Text = fbd.SelectedPath
        Else
            ofd.ShowDialog()
            TextBox1.Text = ofd.FileName
        End If



    End Sub

    Public Sub New(ByVal VARSTR As String)
        InitializeComponent()
        ' This call is required by the Windows Form Designer.
        typ = VARSTR
    End Sub

    Private Sub InputFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If InStr(typ, "MARXAN") Then
            Label1.Text = "Specifiy location of Marxan executable:"
            Me.Text = "MARXAN Executable"
            ofd.Filter = "Executables(*.exe)|*.exe"
        End If
        If InStr(typ, "MEX") Then
            Label1.Text = "Specifiy location of the input.dat file to use:"
            Me.Text = "INPUT.DAT"
            ofd.Filter = "Marxan input files(input.dat)|input.dat"
        End If
        If InStr(typ, "INEDIT") Then
            Label1.Text = "Specifiy location of Inedit executable:"
            Me.Text = "Inedit Executable"
            ofd.Filter = "Executables(*.exe)|*.exe"
        End If
        If InStr(typ, "PAT_SCRATCH") Then
            Label1.Text = "Specifiy location of the temporary scratch directory:"
            Me.Text = "PAT Scratch Directory"
            fbd.Description = "PAT Scratch Directory"
        End If

        If InStr(typ, "INEDIT_SCRATCH") Then
            Label1.Text = "Specifiy A Marxan Output Directory:"
            Me.Text = "MARXAN Directory"
            fbd.Description = "MARXAN Output Directory"
        End If

    End Sub
End Class
