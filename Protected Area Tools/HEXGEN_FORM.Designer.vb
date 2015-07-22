<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HEXGEN_FORM
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.outputbox = New System.Windows.Forms.TextBox()
        Me.extentbox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.areabox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.unitidbox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.stepbox = New System.Windows.Forms.TextBox()
        Me.overlapbox = New System.Windows.Forms.CheckBox()
        Me.hexnumber = New System.Windows.Forms.RadioButton()
        Me.hexarea = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel1.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(138, 188)
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
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(253, 137)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(26, 19)
        Me.Button1.TabIndex = 35
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(4, 137)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 13)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "Output Dataset:"
        '
        'outputbox
        '
        Me.outputbox.BackColor = System.Drawing.SystemColors.Window
        Me.outputbox.Location = New System.Drawing.Point(89, 137)
        Me.outputbox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.outputbox.Name = "outputbox"
        Me.outputbox.ReadOnly = True
        Me.outputbox.Size = New System.Drawing.Size(160, 20)
        Me.outputbox.TabIndex = 33
        '
        'extentbox
        '
        Me.extentbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.extentbox.FormattingEnabled = True
        Me.extentbox.Location = New System.Drawing.Point(82, 5)
        Me.extentbox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.extentbox.Name = "extentbox"
        Me.extentbox.Size = New System.Drawing.Size(198, 21)
        Me.extentbox.TabIndex = 37
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Extent Layer:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 65)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Area (Hectares*):"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'areabox
        '
        Me.areabox.Location = New System.Drawing.Point(112, 63)
        Me.areabox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.areabox.Name = "areabox"
        Me.areabox.Size = New System.Drawing.Size(168, 20)
        Me.areabox.TabIndex = 39
        Me.areabox.Text = "100"
        Me.areabox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(39, 89)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Start UNIT ID:"
        '
        'unitidbox
        '
        Me.unitidbox.Location = New System.Drawing.Point(112, 86)
        Me.unitidbox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.unitidbox.Name = "unitidbox"
        Me.unitidbox.Size = New System.Drawing.Size(168, 20)
        Me.unitidbox.TabIndex = 41
        Me.unitidbox.Text = "1"
        Me.unitidbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 164)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(258, 13)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = "*Input Area in units / 10000, e.g. in hectares if meters"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(177, 113)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Step:"
        '
        'stepbox
        '
        Me.stepbox.Location = New System.Drawing.Point(212, 110)
        Me.stepbox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.stepbox.Name = "stepbox"
        Me.stepbox.Size = New System.Drawing.Size(68, 20)
        Me.stepbox.TabIndex = 44
        Me.stepbox.Text = "1"
        Me.stepbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'overlapbox
        '
        Me.overlapbox.AutoSize = True
        Me.overlapbox.Checked = True
        Me.overlapbox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.overlapbox.Location = New System.Drawing.Point(10, 111)
        Me.overlapbox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.overlapbox.Name = "overlapbox"
        Me.overlapbox.Size = New System.Drawing.Size(158, 17)
        Me.overlapbox.TabIndex = 45
        Me.overlapbox.Text = "Include Only Overlap Hexes"
        Me.overlapbox.UseVisualStyleBackColor = True
        '
        'hexnumber
        '
        Me.hexnumber.AutoSize = True
        Me.hexnumber.Location = New System.Drawing.Point(140, 37)
        Me.hexnumber.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.hexnumber.Name = "hexnumber"
        Me.hexnumber.Size = New System.Drawing.Size(145, 17)
        Me.hexnumber.TabIndex = 46
        Me.hexnumber.Text = "Specify Number of Hexes"
        Me.hexnumber.UseVisualStyleBackColor = True
        '
        'hexarea
        '
        Me.hexarea.AutoSize = True
        Me.hexarea.Checked = True
        Me.hexarea.Location = New System.Drawing.Point(24, 37)
        Me.hexarea.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.hexarea.Name = "hexarea"
        Me.hexarea.Size = New System.Drawing.Size(107, 17)
        Me.hexarea.TabIndex = 47
        Me.hexarea.TabStop = True
        Me.hexarea.Text = "Specify Hex Area"
        Me.hexarea.UseVisualStyleBackColor = True
        '
        'HEXGEN_FORM
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(296, 230)
        Me.Controls.Add(Me.hexarea)
        Me.Controls.Add(Me.hexnumber)
        Me.Controls.Add(Me.overlapbox)
        Me.Controls.Add(Me.stepbox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.unitidbox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.areabox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.extentbox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.outputbox)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "HEXGEN_FORM"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HexGen"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents outputbox As System.Windows.Forms.TextBox
    Friend WithEvents extentbox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents areabox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents unitidbox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents stepbox As System.Windows.Forms.TextBox
    Friend WithEvents overlapbox As System.Windows.Forms.CheckBox
    Friend WithEvents hexnumber As System.Windows.Forms.RadioButton
    Friend WithEvents hexarea As System.Windows.Forms.RadioButton

End Class
