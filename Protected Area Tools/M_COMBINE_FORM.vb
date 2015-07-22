Imports System.Windows.Forms
Imports System.Diagnostics.Process
Imports System.Reflection

Public Class M_COMBINE_FORM

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'fs = CreateObject("Scripting.FileSystemObject")
        'a = fs.CreateTextFile(OUTPUTBOX.Text, True)

        Dim patscratch As String = ""
        'rkey = "HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\Session Manager\Environment"
        'Dim regKey As Microsoft.Win32.RegistryKey
        'regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\Session Manager\Environment", False)
        'patscratch = regKey.GetValue("PAT_SCRATCH")
        'regKey.Close()

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

        Dim lb As String
        For Each lb In lbfile.Items
            'MsgBox(lb)
            cmd = cmd + Chr(34) + lb + Chr(34) + " + "
        Next

        cmd = cmd.Remove(cmd.Length - 2) + Chr(34) + TextBox1.Text + Chr(34) + " /Y"

        Dim fi As New IO.FileInfo(patscratch & "\c.bat")
        fi.Delete()

        Dim a As Awriter = New Awriter(patscratch & "\c.bat")
        a.WriteLine(cmd)
        a.Close()


        Shell(patscratch & "\c.bat")

        ' MsgBox(cmd)

        'a.Close()
        MsgBox("Processing Complete")
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub but1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles but1.Click
        'On Error GoTo er
        cmdialog.ShowDialog()
        If cmdialog.FileName <> "" Then
            lbfile.Items.Add(cmdialog.FileName)
        End If
        cmdialog.FileName = ""
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        sfd.ShowDialog()
        TextBox1.Text = sfd.FileName
    End Sub

    Private Sub M_COMBINE_FORM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
