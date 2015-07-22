Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Reflection
Imports ESRI.ArcGIS.Framework


Public Class PATSettings
    Private m_application As IApplication
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim writepaths As New System.IO.StreamWriter(IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) & "\PATInfo\paths.txt")

        If PatScratchBox.Text <> "" Then writepaths.WriteLine("PATSCRATCH|" + PatScratchBox.Text)
        If MatrixBox.Text <> "" Then writepaths.WriteLine("MATRIXPLACE|" + MatrixBox.Text)
        If Marxanbox.Text <> "" Then writepaths.WriteLine("MARXANPLACE|" + Marxanbox.Text)
        If rtext.Text <> "" Then writepaths.WriteLine("RPLACE|" + rtext.Text)
        If Ineditbox.Text <> "" Then writepaths.WriteLine("INEDITPLACE|" + Ineditbox.Text)

        writepaths.Flush()
        writepaths.Close()

        Me.Close()

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Public Sub New(ByVal app As IApplication)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_application = app
    End Sub


    Private Sub Options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'MsgBox(IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))

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

                Marxanbox.Text = marxanplace
                MatrixBox.Text = matrixplace
                PatScratchBox.Text = patplace
                Ineditbox.Text = ineditplace
                rtext.Text = rplace
            End If
            line = readpaths.ReadLine
        Loop

        readpaths.Close()

    End Sub

    Private Sub PatScratchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatScratchButton.Click
        fbd.ShowNewFolderButton = True
        fbd.ShowDialog()
        PatScratchBox.Text = fbd.SelectedPath
    End Sub

    Private Sub MarxanButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MarxanButton.Click
        ofd.ShowDialog()
        Marxanbox.Text = ofd.FileName
    End Sub

    Private Sub IneditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IneditButton.Click
        ofd.ShowDialog()
        Ineditbox.Text = ofd.FileName
    End Sub

    Private Sub MatrixButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MatrixButton.Click
        ofd.ShowDialog()
        MatrixBox.Text = ofd.FileName
    End Sub

    Private Sub PatScratchBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PatScratchBox.TextChanged

    End Sub

    Private Sub rbutton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbutton.Click
        ofd.ShowDialog()
        rtext.Text = ofd.FileName
    End Sub
End Class
