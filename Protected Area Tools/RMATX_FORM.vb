Imports System.Windows.Forms
Imports System.Reflection
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework


Public Class RMATX_FORM

    Public mapp As IApplication
    Public m_application As IMxApplication
    Public maxanplace As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim rest As String

        If matrix.Checked Then
            rest = " 2 " + Chr(34) + puvspr.Text + Chr(34) + " " + Chr(34) + output.Text + Chr(34)

        Else
            rest = " 1 " + Chr(34) + puvspr.Text + Chr(34) + " " + Chr(34) + output.Text + Chr(34)
        End If


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
                'patplace = regKey.GetValue("PATSCRATCH")
                'marxanplace = regKey.GetValue("MARXANHOME")
                'ineditplace = regKey.GetValue("INEDITHOME")
                'rplace = regKey.GetValue("RHOME")
                patscratch = patplace
            End If
            line = readpaths.ReadLine
        Loop

        readpaths.Close()

        Dim putback As String = Environment.CurrentDirectory

        Dim fi As New IO.FileInfo(patscratch & "\mx.bat")
        fi.Delete()

        Dim a As Awriter = New Awriter(patscratch & "\mx.bat")
        a.WriteLine(Chr(34) + maxanplace + Chr(34) + rest)
        a.Close()

        Shell(patscratch & "\mx.bat")

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()


        Environment.CurrentDirectory = putback

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub New(ByVal app As IApplication, ByVal matrixplace As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        maxanplace = matrixplace
        ' Add any initialization after the InitializeComponent() call.
        m_application = app
    End Sub

    Private Sub RMATX_FORM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        sfd.ShowDialog()
        output.Text = sfd.FileName
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ofd.ShowDialog()
        puvspr.Text = ofd.FileName
    End Sub
End Class
