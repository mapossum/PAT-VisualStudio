<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MARXAN_1_FORM
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.pubox = New System.Windows.Forms.ComboBox()
        Me.pufield = New System.Windows.Forms.ComboBox()
        Me.sumbut = New System.Windows.Forms.RadioButton()
        Me.meanbut = New System.Windows.Forms.RadioButton()
        Me.maxbut = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.StatusValueBox = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ThresholdBox = New System.Windows.Forms.TextBox()
        Me.statusbox = New System.Windows.Forms.ComboBox()
        Me.useField = New System.Windows.Forms.RadioButton()
        Me.useLayer = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.costfield = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.costbox = New System.Windows.Forms.ComboBox()
        Me.CreateBound = New System.Windows.Forms.CheckBox()
        Me.CreateBlock = New System.Windows.Forms.CheckBox()
        Me.LCF = New System.Windows.Forms.TextBox()
        Me.ACF = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.targetbox = New System.Windows.Forms.ListBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.outputbox = New System.Windows.Forms.TextBox()
        Me.newspec = New System.Windows.Forms.CheckBox()
        Me.includeXYcheck = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(299, 315)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'pubox
        '
        Me.pubox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.pubox.FormattingEnabled = True
        Me.pubox.Location = New System.Drawing.Point(100, 5)
        Me.pubox.Margin = New System.Windows.Forms.Padding(2)
        Me.pubox.Name = "pubox"
        Me.pubox.Size = New System.Drawing.Size(155, 21)
        Me.pubox.TabIndex = 1
        '
        'pufield
        '
        Me.pufield.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.pufield.FormattingEnabled = True
        Me.pufield.Location = New System.Drawing.Point(100, 31)
        Me.pufield.Margin = New System.Windows.Forms.Padding(2)
        Me.pufield.Name = "pufield"
        Me.pufield.Size = New System.Drawing.Size(155, 21)
        Me.pufield.TabIndex = 2
        '
        'sumbut
        '
        Me.sumbut.AutoSize = True
        Me.sumbut.Checked = True
        Me.sumbut.Location = New System.Drawing.Point(52, 41)
        Me.sumbut.Margin = New System.Windows.Forms.Padding(2)
        Me.sumbut.Name = "sumbut"
        Me.sumbut.Size = New System.Drawing.Size(46, 17)
        Me.sumbut.TabIndex = 8
        Me.sumbut.TabStop = True
        Me.sumbut.Text = "Sum"
        Me.sumbut.UseVisualStyleBackColor = True
        '
        'meanbut
        '
        Me.meanbut.AutoSize = True
        Me.meanbut.Location = New System.Drawing.Point(99, 41)
        Me.meanbut.Margin = New System.Windows.Forms.Padding(2)
        Me.meanbut.Name = "meanbut"
        Me.meanbut.Size = New System.Drawing.Size(52, 17)
        Me.meanbut.TabIndex = 9
        Me.meanbut.Text = "Mean"
        Me.meanbut.UseVisualStyleBackColor = True
        '
        'maxbut
        '
        Me.maxbut.AutoSize = True
        Me.maxbut.Location = New System.Drawing.Point(151, 42)
        Me.maxbut.Margin = New System.Windows.Forms.Padding(2)
        Me.maxbut.Name = "maxbut"
        Me.maxbut.Size = New System.Drawing.Size(45, 17)
        Me.maxbut.TabIndex = 10
        Me.maxbut.Text = "Max"
        Me.maxbut.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Planning Unit:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 33)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Planning ID Field:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.StatusValueBox)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.ThresholdBox)
        Me.GroupBox1.Controls.Add(Me.statusbox)
        Me.GroupBox1.Controls.Add(Me.useField)
        Me.GroupBox1.Controls.Add(Me.useLayer)
        Me.GroupBox1.Location = New System.Drawing.Point(259, 5)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(193, 137)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Status Options"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(7, 63)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 13)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "Status Value:"
        '
        'StatusValueBox
        '
        Me.StatusValueBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StatusValueBox.Enabled = False
        Me.StatusValueBox.FormattingEnabled = True
        Me.StatusValueBox.Location = New System.Drawing.Point(7, 81)
        Me.StatusValueBox.Margin = New System.Windows.Forms.Padding(2)
        Me.StatusValueBox.Name = "StatusValueBox"
        Me.StatusValueBox.Size = New System.Drawing.Size(172, 21)
        Me.StatusValueBox.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 111)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(130, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Status Overlap Threshold:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(170, 113)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(15, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "%"
        '
        'ThresholdBox
        '
        Me.ThresholdBox.Enabled = False
        Me.ThresholdBox.Location = New System.Drawing.Point(142, 110)
        Me.ThresholdBox.Margin = New System.Windows.Forms.Padding(2)
        Me.ThresholdBox.Name = "ThresholdBox"
        Me.ThresholdBox.Size = New System.Drawing.Size(26, 20)
        Me.ThresholdBox.TabIndex = 11
        Me.ThresholdBox.Text = "50"
        '
        'statusbox
        '
        Me.statusbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.statusbox.FormattingEnabled = True
        Me.statusbox.Location = New System.Drawing.Point(7, 39)
        Me.statusbox.Margin = New System.Windows.Forms.Padding(2)
        Me.statusbox.Name = "statusbox"
        Me.statusbox.Size = New System.Drawing.Size(172, 21)
        Me.statusbox.TabIndex = 10
        '
        'useField
        '
        Me.useField.AutoSize = True
        Me.useField.Location = New System.Drawing.Point(80, 17)
        Me.useField.Margin = New System.Windows.Forms.Padding(2)
        Me.useField.Name = "useField"
        Me.useField.Size = New System.Drawing.Size(87, 17)
        Me.useField.TabIndex = 9
        Me.useField.Text = "Use PU Field"
        Me.useField.UseVisualStyleBackColor = True
        '
        'useLayer
        '
        Me.useLayer.AutoSize = True
        Me.useLayer.Checked = True
        Me.useLayer.Location = New System.Drawing.Point(7, 16)
        Me.useLayer.Margin = New System.Windows.Forms.Padding(2)
        Me.useLayer.Name = "useLayer"
        Me.useLayer.Size = New System.Drawing.Size(73, 17)
        Me.useLayer.TabIndex = 8
        Me.useLayer.TabStop = True
        Me.useLayer.Text = "Use Layer"
        Me.useLayer.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.costfield)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.costbox)
        Me.GroupBox2.Controls.Add(Me.sumbut)
        Me.GroupBox2.Controls.Add(Me.meanbut)
        Me.GroupBox2.Controls.Add(Me.maxbut)
        Me.GroupBox2.Location = New System.Drawing.Point(259, 147)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox2.Size = New System.Drawing.Size(193, 107)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Cost Options"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 63)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Cost Surface Field:"
        '
        'costfield
        '
        Me.costfield.FormattingEnabled = True
        Me.costfield.Location = New System.Drawing.Point(7, 80)
        Me.costfield.Margin = New System.Windows.Forms.Padding(2)
        Me.costfield.Name = "costfield"
        Me.costfield.Size = New System.Drawing.Size(174, 21)
        Me.costfield.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 43)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Method:"
        '
        'costbox
        '
        Me.costbox.FormattingEnabled = True
        Me.costbox.Location = New System.Drawing.Point(6, 17)
        Me.costbox.Margin = New System.Windows.Forms.Padding(2)
        Me.costbox.Name = "costbox"
        Me.costbox.Size = New System.Drawing.Size(174, 21)
        Me.costbox.TabIndex = 11
        '
        'CreateBound
        '
        Me.CreateBound.AutoSize = True
        Me.CreateBound.Location = New System.Drawing.Point(148, 236)
        Me.CreateBound.Margin = New System.Windows.Forms.Padding(2)
        Me.CreateBound.Name = "CreateBound"
        Me.CreateBound.Size = New System.Drawing.Size(110, 17)
        Me.CreateBound.TabIndex = 15
        Me.CreateBound.Text = "Create Bound File"
        Me.CreateBound.UseVisualStyleBackColor = True
        '
        'CreateBlock
        '
        Me.CreateBlock.AutoSize = True
        Me.CreateBlock.Location = New System.Drawing.Point(10, 236)
        Me.CreateBlock.Margin = New System.Windows.Forms.Padding(2)
        Me.CreateBlock.Name = "CreateBlock"
        Me.CreateBlock.Size = New System.Drawing.Size(126, 17)
        Me.CreateBlock.TabIndex = 16
        Me.CreateBlock.Text = "Create Block Def File"
        Me.CreateBlock.UseVisualStyleBackColor = True
        '
        'LCF
        '
        Me.LCF.Location = New System.Drawing.Point(138, 327)
        Me.LCF.Margin = New System.Windows.Forms.Padding(2)
        Me.LCF.Name = "LCF"
        Me.LCF.Size = New System.Drawing.Size(112, 20)
        Me.LCF.TabIndex = 17
        Me.LCF.Text = "1000"
        Me.LCF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ACF
        '
        Me.ACF.Location = New System.Drawing.Point(138, 303)
        Me.ACF.Margin = New System.Windows.Forms.Padding(2)
        Me.ACF.Name = "ACF"
        Me.ACF.Size = New System.Drawing.Size(112, 20)
        Me.ACF.TabIndex = 18
        Me.ACF.Text = "10000"
        Me.ACF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 329)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(132, 13)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Length Conversion Factor:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 306)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(121, 13)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Area Conversion Factor:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(10, 54)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 13)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Target Inputs:"
        '
        'targetbox
        '
        Me.targetbox.FormattingEnabled = True
        Me.targetbox.Location = New System.Drawing.Point(23, 72)
        Me.targetbox.Margin = New System.Windows.Forms.Padding(2)
        Me.targetbox.Name = "targetbox"
        Me.targetbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.targetbox.Size = New System.Drawing.Size(227, 160)
        Me.targetbox.TabIndex = 21
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(418, 282)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(26, 19)
        Me.Button1.TabIndex = 28
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(256, 264)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 13)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "Output Directory:"
        '
        'outputbox
        '
        Me.outputbox.BackColor = System.Drawing.SystemColors.Window
        Me.outputbox.Location = New System.Drawing.Point(264, 282)
        Me.outputbox.Margin = New System.Windows.Forms.Padding(2)
        Me.outputbox.Name = "outputbox"
        Me.outputbox.ReadOnly = True
        Me.outputbox.Size = New System.Drawing.Size(150, 20)
        Me.outputbox.TabIndex = 26
        '
        'newspec
        '
        Me.newspec.AutoSize = True
        Me.newspec.Location = New System.Drawing.Point(10, 258)
        Me.newspec.Margin = New System.Windows.Forms.Padding(2)
        Me.newspec.Name = "newspec"
        Me.newspec.Size = New System.Drawing.Size(215, 17)
        Me.newspec.TabIndex = 29
        Me.newspec.Text = "Create New Style Species (spec.dat) file"
        Me.newspec.UseVisualStyleBackColor = True
        '
        'includeXYcheck
        '
        Me.includeXYcheck.AutoSize = True
        Me.includeXYcheck.Location = New System.Drawing.Point(10, 280)
        Me.includeXYcheck.Margin = New System.Windows.Forms.Padding(2)
        Me.includeXYcheck.Name = "includeXYcheck"
        Me.includeXYcheck.Size = New System.Drawing.Size(204, 17)
        Me.includeXYcheck.TabIndex = 30
        Me.includeXYcheck.Text = "Include x and y location field in pu.dat"
        Me.includeXYcheck.UseVisualStyleBackColor = True
        '
        'MARXAN_1_FORM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(458, 357)
        Me.Controls.Add(Me.includeXYcheck)
        Me.Controls.Add(Me.newspec)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.outputbox)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.targetbox)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.ACF)
        Me.Controls.Add(Me.LCF)
        Me.Controls.Add(Me.CreateBlock)
        Me.Controls.Add(Me.CreateBound)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pufield)
        Me.Controls.Add(Me.pubox)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MARXAN_1_FORM"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MARXAN INPUT GENERATOR"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents pubox As System.Windows.Forms.ComboBox
    Friend WithEvents pufield As System.Windows.Forms.ComboBox
    Friend WithEvents sumbut As System.Windows.Forms.RadioButton
    Friend WithEvents meanbut As System.Windows.Forms.RadioButton
    Friend WithEvents maxbut As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents useField As System.Windows.Forms.RadioButton
    Friend WithEvents useLayer As System.Windows.Forms.RadioButton
    Friend WithEvents statusbox As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ThresholdBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents costbox As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CreateBound As System.Windows.Forms.CheckBox
    Friend WithEvents CreateBlock As System.Windows.Forms.CheckBox
    Friend WithEvents LCF As System.Windows.Forms.TextBox
    Friend WithEvents ACF As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents targetbox As System.Windows.Forms.ListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents outputbox As System.Windows.Forms.TextBox
    Friend WithEvents costfield As System.Windows.Forms.ComboBox
    Friend WithEvents StatusValueBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents newspec As System.Windows.Forms.CheckBox
    Friend WithEvents includeXYcheck As System.Windows.Forms.CheckBox

End Class
