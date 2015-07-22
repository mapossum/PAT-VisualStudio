<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDS_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

       'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDS_Form))
        Me.mainGrid = New System.Windows.Forms.DataGridView()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FIleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PullSelectionFromMapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RevertToLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RevertToPreviousMarxanRunToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RunToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SingleRunSameAsGreenButtonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CalibrToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Clustercheck = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.FieldNames = New System.Windows.Forms.ComboBox()
        Me.ProjectBox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.featuresBox = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.inputbox = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.cancelbut = New System.Windows.Forms.Button()
        Me.Okbut = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.prFolder = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.mFiles = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.mcollist = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.inputdatfile = New System.Windows.Forms.TextBox()
        Me.RunMarxan = New System.Windows.Forms.Button()
        Me.ProcessPanel = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextMessages = New System.Windows.Forms.TextBox()
        Me.TotalProgress = New System.Windows.Forms.ProgressBar()
        Me.MarxanProgress = New System.Windows.Forms.ProgressBar()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Calmin = New System.Windows.Forms.TextBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.ParameterText = New System.Windows.Forms.ComboBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Calmax = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.CalStep = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ExPanel = New System.Windows.Forms.Panel()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.ExGrid = New System.Windows.Forms.DataGridView()
        CType(Me.mainGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.ProcessPanel.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.ExPanel.SuspendLayout()
        CType(Me.ExGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'mainGrid
        '
        Me.mainGrid.AllowUserToAddRows = False
        Me.mainGrid.AllowUserToDeleteRows = False
        Me.mainGrid.AllowUserToResizeRows = False
        Me.mainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.mainGrid.Location = New System.Drawing.Point(-2, 82)
        Me.mainGrid.Name = "mainGrid"
        Me.mainGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.mainGrid.Size = New System.Drawing.Size(697, 488)
        Me.mainGrid.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FIleToolStripMenuItem, Me.EditToolStripMenuItem, Me.RunToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(695, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FIleToolStripMenuItem
        '
        Me.FIleToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateProjectToolStripMenuItem, Me.OpenProjectToolStripMenuItem, Me.SaveProjectToolStripMenuItem})
        Me.FIleToolStripMenuItem.Name = "FIleToolStripMenuItem"
        Me.FIleToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FIleToolStripMenuItem.Text = "File"
        '
        'CreateProjectToolStripMenuItem
        '
        Me.CreateProjectToolStripMenuItem.Name = "CreateProjectToolStripMenuItem"
        Me.CreateProjectToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.CreateProjectToolStripMenuItem.Text = "Create Project"
        '
        'OpenProjectToolStripMenuItem
        '
        Me.OpenProjectToolStripMenuItem.Name = "OpenProjectToolStripMenuItem"
        Me.OpenProjectToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.OpenProjectToolStripMenuItem.Text = "Open Project"
        '
        'SaveProjectToolStripMenuItem
        '
        Me.SaveProjectToolStripMenuItem.Name = "SaveProjectToolStripMenuItem"
        Me.SaveProjectToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.SaveProjectToolStripMenuItem.Text = "Save Project"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PullSelectionFromMapToolStripMenuItem, Me.RevertToLToolStripMenuItem, Me.RevertToPreviousMarxanRunToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'PullSelectionFromMapToolStripMenuItem
        '
        Me.PullSelectionFromMapToolStripMenuItem.Name = "PullSelectionFromMapToolStripMenuItem"
        Me.PullSelectionFromMapToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.PullSelectionFromMapToolStripMenuItem.Text = "Pull Selection from Map"
        '
        'RevertToLToolStripMenuItem
        '
        Me.RevertToLToolStripMenuItem.Name = "RevertToLToolStripMenuItem"
        Me.RevertToLToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.RevertToLToolStripMenuItem.Text = "Revert to Last Marxan Run"
        '
        'RevertToPreviousMarxanRunToolStripMenuItem
        '
        Me.RevertToPreviousMarxanRunToolStripMenuItem.Name = "RevertToPreviousMarxanRunToolStripMenuItem"
        Me.RevertToPreviousMarxanRunToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.RevertToPreviousMarxanRunToolStripMenuItem.Text = "View Previous Marxan Runs"
        '
        'RunToolStripMenuItem
        '
        Me.RunToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SingleRunSameAsGreenButtonToolStripMenuItem, Me.CalibrToolStripMenuItem, Me.Clustercheck})
        Me.RunToolStripMenuItem.Name = "RunToolStripMenuItem"
        Me.RunToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.RunToolStripMenuItem.Text = "Run"
        '
        'SingleRunSameAsGreenButtonToolStripMenuItem
        '
        Me.SingleRunSameAsGreenButtonToolStripMenuItem.Name = "SingleRunSameAsGreenButtonToolStripMenuItem"
        Me.SingleRunSameAsGreenButtonToolStripMenuItem.Size = New System.Drawing.Size(321, 22)
        Me.SingleRunSameAsGreenButtonToolStripMenuItem.Text = "Single Run (Same as Green Button)"
        '
        'CalibrToolStripMenuItem
        '
        Me.CalibrToolStripMenuItem.Name = "CalibrToolStripMenuItem"
        Me.CalibrToolStripMenuItem.Size = New System.Drawing.Size(321, 22)
        Me.CalibrToolStripMenuItem.Text = "Calibration Run..."
        '
        'Clustercheck
        '
        Me.Clustercheck.CheckOnClick = True
        Me.Clustercheck.Name = "Clustercheck"
        Me.Clustercheck.Size = New System.Drawing.Size(321, 22)
        Me.Clustercheck.Text = "Output Cluster Analysis (Must have R installed)"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.FieldNames)
        Me.Panel1.Controls.Add(Me.ProjectBox)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.featuresBox)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.inputbox)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.cancelbut)
        Me.Panel1.Controls.Add(Me.Okbut)
        Me.Panel1.Controls.Add(Me.TextBox2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.prFolder)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Location = New System.Drawing.Point(11, 92)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(671, 160)
        Me.Panel1.TabIndex = 3
        Me.Panel1.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Planning Unit ID Field"
        '
        'FieldNames
        '
        Me.FieldNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FieldNames.FormattingEnabled = True
        Me.FieldNames.Location = New System.Drawing.Point(150, 121)
        Me.FieldNames.Name = "FieldNames"
        Me.FieldNames.Size = New System.Drawing.Size(329, 21)
        Me.FieldNames.TabIndex = 14
        '
        'ProjectBox
        '
        Me.ProjectBox.Location = New System.Drawing.Point(164, 180)
        Me.ProjectBox.Name = "ProjectBox"
        Me.ProjectBox.Size = New System.Drawing.Size(464, 20)
        Me.ProjectBox.TabIndex = 13
        Me.ProjectBox.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(28, 183)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Project Name"
        Me.Label4.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Planning Unit Features"
        '
        'featuresBox
        '
        Me.featuresBox.Location = New System.Drawing.Point(150, 88)
        Me.featuresBox.Name = "featuresBox"
        Me.featuresBox.ReadOnly = True
        Me.featuresBox.Size = New System.Drawing.Size(464, 20)
        Me.featuresBox.TabIndex = 10
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(620, 85)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(32, 23)
        Me.Button3.TabIndex = 9
        Me.Button3.Text = "..."
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Marxan input.dat file"
        '
        'inputbox
        '
        Me.inputbox.Location = New System.Drawing.Point(150, 49)
        Me.inputbox.Name = "inputbox"
        Me.inputbox.ReadOnly = True
        Me.inputbox.Size = New System.Drawing.Size(465, 20)
        Me.inputbox.TabIndex = 7
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(621, 46)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(32, 23)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "..."
        Me.Button2.UseVisualStyleBackColor = True
        '
        'cancelbut
        '
        Me.cancelbut.Location = New System.Drawing.Point(496, 120)
        Me.cancelbut.Name = "cancelbut"
        Me.cancelbut.Size = New System.Drawing.Size(75, 23)
        Me.cancelbut.TabIndex = 5
        Me.cancelbut.Text = "Cancel"
        Me.cancelbut.UseVisualStyleBackColor = True
        '
        'Okbut
        '
        Me.Okbut.Location = New System.Drawing.Point(577, 120)
        Me.Okbut.Name = "Okbut"
        Me.Okbut.Size = New System.Drawing.Size(75, 23)
        Me.Okbut.TabIndex = 4
        Me.Okbut.Text = "OK"
        Me.Okbut.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Location = New System.Drawing.Point(14, 164)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(638, 64)
        Me.TextBox2.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Directory (new is best)"
        '
        'prFolder
        '
        Me.prFolder.Location = New System.Drawing.Point(150, 13)
        Me.prFolder.Name = "prFolder"
        Me.prFolder.ReadOnly = True
        Me.prFolder.Size = New System.Drawing.Size(465, 20)
        Me.prFolder.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(621, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(32, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'mFiles
        '
        Me.mFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mFiles.FormattingEnabled = True
        Me.mFiles.Location = New System.Drawing.Point(12, 51)
        Me.mFiles.Name = "mFiles"
        Me.mFiles.Size = New System.Drawing.Size(99, 21)
        Me.mFiles.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(94, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Marxan File to Edit"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(125, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(131, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Replace the values in the "
        '
        'mcollist
        '
        Me.mcollist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.mcollist.FormattingEnabled = True
        Me.mcollist.Location = New System.Drawing.Point(268, 46)
        Me.mcollist.Name = "mcollist"
        Me.mcollist.Size = New System.Drawing.Size(121, 21)
        Me.mcollist.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(398, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(114, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "column with this value:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(518, 47)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(96, 20)
        Me.TextBox1.TabIndex = 9
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(620, 44)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(62, 22)
        Me.Button4.TabIndex = 10
        Me.Button4.Text = "Replace"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'inputdatfile
        '
        Me.inputdatfile.Location = New System.Drawing.Point(0, 82)
        Me.inputdatfile.Multiline = True
        Me.inputdatfile.Name = "inputdatfile"
        Me.inputdatfile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.inputdatfile.Size = New System.Drawing.Size(696, 488)
        Me.inputdatfile.TabIndex = 11
        Me.inputdatfile.Visible = False
        '
        'RunMarxan
        '
        Me.RunMarxan.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.RunMarxan.Location = New System.Drawing.Point(586, 0)
        Me.RunMarxan.Name = "RunMarxan"
        Me.RunMarxan.Size = New System.Drawing.Size(110, 23)
        Me.RunMarxan.TabIndex = 12
        Me.RunMarxan.Text = "RUN MARXAN"
        Me.RunMarxan.UseVisualStyleBackColor = False
        '
        'ProcessPanel
        '
        Me.ProcessPanel.Controls.Add(Me.Label10)
        Me.ProcessPanel.Controls.Add(Me.Label9)
        Me.ProcessPanel.Controls.Add(Me.TextMessages)
        Me.ProcessPanel.Controls.Add(Me.TotalProgress)
        Me.ProcessPanel.Controls.Add(Me.MarxanProgress)
        Me.ProcessPanel.Location = New System.Drawing.Point(633, 72)
        Me.ProcessPanel.Name = "ProcessPanel"
        Me.ProcessPanel.Size = New System.Drawing.Size(696, 570)
        Me.ProcessPanel.TabIndex = 13
        Me.ProcessPanel.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(40, 285)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 13)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Overall Progress"
        Me.Label10.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(40, 191)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(86, 13)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Marxan Progress"
        '
        'TextMessages
        '
        Me.TextMessages.BackColor = System.Drawing.SystemColors.Control
        Me.TextMessages.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextMessages.Location = New System.Drawing.Point(43, 72)
        Me.TextMessages.Multiline = True
        Me.TextMessages.Name = "TextMessages"
        Me.TextMessages.ReadOnly = True
        Me.TextMessages.Size = New System.Drawing.Size(612, 94)
        Me.TextMessages.TabIndex = 2
        Me.TextMessages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TotalProgress
        '
        Me.TotalProgress.Location = New System.Drawing.Point(43, 308)
        Me.TotalProgress.Name = "TotalProgress"
        Me.TotalProgress.Size = New System.Drawing.Size(612, 23)
        Me.TotalProgress.TabIndex = 1
        Me.TotalProgress.Visible = False
        '
        'MarxanProgress
        '
        Me.MarxanProgress.Location = New System.Drawing.Point(43, 212)
        Me.MarxanProgress.Name = "MarxanProgress"
        Me.MarxanProgress.Size = New System.Drawing.Size(612, 23)
        Me.MarxanProgress.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Location = New System.Drawing.Point(11, 258)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(727, 574)
        Me.Panel2.TabIndex = 6
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Calmin)
        Me.Panel3.Controls.Add(Me.Button6)
        Me.Panel3.Controls.Add(Me.ParameterText)
        Me.Panel3.Controls.Add(Me.Button5)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.Calmax)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Controls.Add(Me.CalStep)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Location = New System.Drawing.Point(194, 186)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(393, 233)
        Me.Panel3.TabIndex = 10
        '
        'Calmin
        '
        Me.Calmin.Location = New System.Drawing.Point(191, 40)
        Me.Calmin.Name = "Calmin"
        Me.Calmin.Size = New System.Drawing.Size(165, 20)
        Me.Calmin.TabIndex = 2
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(46, 167)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(94, 23)
        Me.Button6.TabIndex = 9
        Me.Button6.Text = "OK"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'ParameterText
        '
        Me.ParameterText.FormattingEnabled = True
        Me.ParameterText.Items.AddRange(New Object() {"BLM"})
        Me.ParameterText.Location = New System.Drawing.Point(27, 39)
        Me.ParameterText.Name = "ParameterText"
        Me.ParameterText.Size = New System.Drawing.Size(144, 21)
        Me.ParameterText.TabIndex = 0
        Me.ParameterText.Text = "BLM"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(46, 123)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(94, 23)
        Me.Button5.TabIndex = 8
        Me.Button5.Text = "Cancel"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(24, 12)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(85, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Input Parameter:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(194, 154)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(29, 13)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "Step"
        '
        'Calmax
        '
        Me.Calmax.Location = New System.Drawing.Point(191, 106)
        Me.Calmax.Name = "Calmax"
        Me.Calmax.Size = New System.Drawing.Size(165, 20)
        Me.Calmax.TabIndex = 3
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(194, 88)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 13)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "Maximum"
        '
        'CalStep
        '
        Me.CalStep.Location = New System.Drawing.Point(191, 172)
        Me.CalStep.Name = "CalStep"
        Me.CalStep.Size = New System.Drawing.Size(165, 20)
        Me.CalStep.TabIndex = 4
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(194, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(48, 13)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "Minimum"
        '
        'ExPanel
        '
        Me.ExPanel.Controls.Add(Me.Button9)
        Me.ExPanel.Controls.Add(Me.Button8)
        Me.ExPanel.Controls.Add(Me.Button7)
        Me.ExPanel.Controls.Add(Me.TextBox3)
        Me.ExPanel.Controls.Add(Me.ExGrid)
        Me.ExPanel.Location = New System.Drawing.Point(0, 0)
        Me.ExPanel.Name = "ExPanel"
        Me.ExPanel.Size = New System.Drawing.Size(696, 570)
        Me.ExPanel.TabIndex = 11
        Me.ExPanel.Visible = False
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(3, 533)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(259, 33)
        Me.Button9.TabIndex = 4
        Me.Button9.Text = "Delete Selected Run"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(280, 533)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(170, 33)
        Me.Button8.TabIndex = 3
        Me.Button8.Text = "Cancel"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(471, 533)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(223, 33)
        Me.Button7.TabIndex = 2
        Me.Button7.Text = "Revert to this Run"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox3.Location = New System.Drawing.Point(0, 232)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox3.Size = New System.Drawing.Size(695, 295)
        Me.TextBox3.TabIndex = 1
        Me.TextBox3.WordWrap = False
        '
        'ExGrid
        '
        Me.ExGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ExGrid.Location = New System.Drawing.Point(0, 1)
        Me.ExGrid.MultiSelect = False
        Me.ExGrid.Name = "ExGrid"
        Me.ExGrid.ReadOnly = True
        Me.ExGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ExGrid.Size = New System.Drawing.Size(696, 230)
        Me.ExGrid.TabIndex = 0
        '
        'MDS_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(695, 568)
        Me.Controls.Add(Me.ExPanel)
        Me.Controls.Add(Me.ProcessPanel)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.RunMarxan)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.mcollist)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.mFiles)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.inputdatfile)
        Me.Controls.Add(Me.mainGrid)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "MDS_Form"
        Me.Text = "Marxan Decision Support"
        CType(Me.mainGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ProcessPanel.ResumeLayout(False)
        Me.ProcessPanel.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ExPanel.ResumeLayout(False)
        Me.ExPanel.PerformLayout()
        CType(Me.ExGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mainGrid As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FIleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents prFolder As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents cancelbut As System.Windows.Forms.Button
    Friend WithEvents Okbut As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents inputbox As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents featuresBox As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents FieldNames As System.Windows.Forms.ComboBox
    Friend WithEvents ProjectBox As System.Windows.Forms.TextBox
    Friend WithEvents mFiles As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents mcollist As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PullSelectionFromMapToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertToLToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertToPreviousMarxanRunToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents inputdatfile As System.Windows.Forms.TextBox
    Friend WithEvents RunToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SingleRunSameAsGreenButtonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RunMarxan As System.Windows.Forms.Button
    Friend WithEvents CalibrToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Clustercheck As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProcessPanel As System.Windows.Forms.Panel
    Friend WithEvents TextMessages As System.Windows.Forms.TextBox
    Friend WithEvents TotalProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents MarxanProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ParameterText As System.Windows.Forms.ComboBox
    Friend WithEvents Calmax As System.Windows.Forms.TextBox
    Friend WithEvents Calmin As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents CalStep As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents SaveProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents ExPanel As System.Windows.Forms.Panel
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents ExGrid As System.Windows.Forms.DataGridView
End Class
