<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PATSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PATSettings))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.MarxanButton = New System.Windows.Forms.Button
        Me.Marxanbox = New System.Windows.Forms.TextBox
        Me.IneditButton = New System.Windows.Forms.Button
        Me.Ineditbox = New System.Windows.Forms.TextBox
        Me.MatrixButton = New System.Windows.Forms.Button
        Me.MatrixBox = New System.Windows.Forms.TextBox
        Me.PatScratchButton = New System.Windows.Forms.Button
        Me.PatScratchBox = New System.Windows.Forms.TextBox
        Me.ofd = New System.Windows.Forms.OpenFileDialog
        Me.fbd = New System.Windows.Forms.FolderBrowserDialog
        Me.rbutton = New System.Windows.Forms.Button
        Me.rtext = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 304)
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
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(9, 10)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(411, 74)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = resources.GetString("TextBox1.Text")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 89)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Scratch Directory:"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(9, 115)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox2.Size = New System.Drawing.Size(411, 81)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.Text = resources.GetString("TextBox2.Text")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 202)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Marxan Executable:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 223)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "InEdit Executable:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 244)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(137, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Convert2Matrix Executable:"
        '
        'MarxanButton
        '
        Me.MarxanButton.Location = New System.Drawing.Point(396, 200)
        Me.MarxanButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MarxanButton.Name = "MarxanButton"
        Me.MarxanButton.Size = New System.Drawing.Size(23, 19)
        Me.MarxanButton.TabIndex = 8
        Me.MarxanButton.Text = "..."
        Me.MarxanButton.UseVisualStyleBackColor = True
        '
        'Marxanbox
        '
        Me.Marxanbox.BackColor = System.Drawing.SystemColors.Window
        Me.Marxanbox.Location = New System.Drawing.Point(112, 200)
        Me.Marxanbox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Marxanbox.Name = "Marxanbox"
        Me.Marxanbox.ReadOnly = True
        Me.Marxanbox.Size = New System.Drawing.Size(281, 20)
        Me.Marxanbox.TabIndex = 7
        '
        'IneditButton
        '
        Me.IneditButton.Location = New System.Drawing.Point(396, 220)
        Me.IneditButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.IneditButton.Name = "IneditButton"
        Me.IneditButton.Size = New System.Drawing.Size(23, 19)
        Me.IneditButton.TabIndex = 10
        Me.IneditButton.Text = "..."
        Me.IneditButton.UseVisualStyleBackColor = True
        '
        'Ineditbox
        '
        Me.Ineditbox.BackColor = System.Drawing.SystemColors.Window
        Me.Ineditbox.Location = New System.Drawing.Point(106, 221)
        Me.Ineditbox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Ineditbox.Name = "Ineditbox"
        Me.Ineditbox.ReadOnly = True
        Me.Ineditbox.Size = New System.Drawing.Size(286, 20)
        Me.Ineditbox.TabIndex = 9
        '
        'MatrixButton
        '
        Me.MatrixButton.Location = New System.Drawing.Point(396, 240)
        Me.MatrixButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MatrixButton.Name = "MatrixButton"
        Me.MatrixButton.Size = New System.Drawing.Size(23, 19)
        Me.MatrixButton.TabIndex = 12
        Me.MatrixButton.Text = "..."
        Me.MatrixButton.UseVisualStyleBackColor = True
        '
        'MatrixBox
        '
        Me.MatrixBox.BackColor = System.Drawing.SystemColors.Window
        Me.MatrixBox.Location = New System.Drawing.Point(148, 241)
        Me.MatrixBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MatrixBox.Name = "MatrixBox"
        Me.MatrixBox.ReadOnly = True
        Me.MatrixBox.Size = New System.Drawing.Size(245, 20)
        Me.MatrixBox.TabIndex = 11
        '
        'PatScratchButton
        '
        Me.PatScratchButton.Location = New System.Drawing.Point(396, 85)
        Me.PatScratchButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PatScratchButton.Name = "PatScratchButton"
        Me.PatScratchButton.Size = New System.Drawing.Size(23, 19)
        Me.PatScratchButton.TabIndex = 14
        Me.PatScratchButton.Text = "..."
        Me.PatScratchButton.UseVisualStyleBackColor = True
        '
        'PatScratchBox
        '
        Me.PatScratchBox.BackColor = System.Drawing.SystemColors.Window
        Me.PatScratchBox.Location = New System.Drawing.Point(103, 86)
        Me.PatScratchBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PatScratchBox.Name = "PatScratchBox"
        Me.PatScratchBox.ReadOnly = True
        Me.PatScratchBox.Size = New System.Drawing.Size(290, 20)
        Me.PatScratchBox.TabIndex = 13
        '
        'ofd
        '
        Me.ofd.Filter = "Executables(*.exe)|*.exe"
        '
        'rbutton
        '
        Me.rbutton.Location = New System.Drawing.Point(395, 264)
        Me.rbutton.Margin = New System.Windows.Forms.Padding(2)
        Me.rbutton.Name = "rbutton"
        Me.rbutton.Size = New System.Drawing.Size(23, 19)
        Me.rbutton.TabIndex = 17
        Me.rbutton.Text = "..."
        Me.rbutton.UseVisualStyleBackColor = True
        '
        'rtext
        '
        Me.rtext.BackColor = System.Drawing.SystemColors.Window
        Me.rtext.Location = New System.Drawing.Point(86, 265)
        Me.rtext.Margin = New System.Windows.Forms.Padding(2)
        Me.rtext.Name = "rtext"
        Me.rtext.ReadOnly = True
        Me.rtext.Size = New System.Drawing.Size(306, 20)
        Me.rtext.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 268)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "R Executable:"
        '
        'Options
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 346)
        Me.Controls.Add(Me.rbutton)
        Me.Controls.Add(Me.rtext)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.PatScratchButton)
        Me.Controls.Add(Me.PatScratchBox)
        Me.Controls.Add(Me.MatrixButton)
        Me.Controls.Add(Me.MatrixBox)
        Me.Controls.Add(Me.IneditButton)
        Me.Controls.Add(Me.Ineditbox)
        Me.Controls.Add(Me.MarxanButton)
        Me.Controls.Add(Me.Marxanbox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Options"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents MarxanButton As System.Windows.Forms.Button
    Friend WithEvents Marxanbox As System.Windows.Forms.TextBox
    Friend WithEvents IneditButton As System.Windows.Forms.Button
    Friend WithEvents Ineditbox As System.Windows.Forms.TextBox
    Friend WithEvents MatrixButton As System.Windows.Forms.Button
    Friend WithEvents MatrixBox As System.Windows.Forms.TextBox
    Friend WithEvents PatScratchButton As System.Windows.Forms.Button
    Friend WithEvents PatScratchBox As System.Windows.Forms.TextBox
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents fbd As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents rbutton As System.Windows.Forms.Button
    Friend WithEvents rtext As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label

End Class
