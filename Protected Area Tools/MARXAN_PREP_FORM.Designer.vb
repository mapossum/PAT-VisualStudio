<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MARXAN_PREP_FORM
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.targetbox = New System.Windows.Forms.ComboBox
        Me.TargetNAME = New System.Windows.Forms.ComboBox
        Me.TargetID = New System.Windows.Forms.ComboBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.outputbox = New System.Windows.Forms.TextBox
        Me.waitlab = New System.Windows.Forms.Label
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(192, 134)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(195, 36)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(4, 4)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(89, 28)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(101, 4)
        Me.Cancel_Button.Margin = New System.Windows.Forms.Padding(4)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(89, 28)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(165, 17)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Target Layer to Prepare:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(74, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 17)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Target ID Field:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(50, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(129, 17)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Target Name Field:"
        '
        'targetbox
        '
        Me.targetbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.targetbox.FormattingEnabled = True
        Me.targetbox.Location = New System.Drawing.Point(183, 9)
        Me.targetbox.Name = "targetbox"
        Me.targetbox.Size = New System.Drawing.Size(199, 24)
        Me.targetbox.TabIndex = 26
        '
        'TargetNAME
        '
        Me.TargetNAME.FormattingEnabled = True
        Me.TargetNAME.Location = New System.Drawing.Point(183, 39)
        Me.TargetNAME.Name = "TargetNAME"
        Me.TargetNAME.Size = New System.Drawing.Size(199, 24)
        Me.TargetNAME.TabIndex = 27
        '
        'TargetID
        '
        Me.TargetID.FormattingEnabled = True
        Me.TargetID.Location = New System.Drawing.Point(183, 69)
        Me.TargetID.Name = "TargetID"
        Me.TargetID.Size = New System.Drawing.Size(199, 24)
        Me.TargetID.TabIndex = 28
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(347, 100)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(35, 23)
        Me.Button1.TabIndex = 31
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(23, 100)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(113, 17)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "Output Location:"
        '
        'outputbox
        '
        Me.outputbox.BackColor = System.Drawing.SystemColors.Window
        Me.outputbox.Location = New System.Drawing.Point(142, 100)
        Me.outputbox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.outputbox.Name = "outputbox"
        Me.outputbox.ReadOnly = True
        Me.outputbox.Size = New System.Drawing.Size(199, 22)
        Me.outputbox.TabIndex = 29
        '
        'waitlab
        '
        Me.waitlab.AutoSize = True
        Me.waitlab.Location = New System.Drawing.Point(112, 69)
        Me.waitlab.Name = "waitlab"
        Me.waitlab.Size = New System.Drawing.Size(182, 17)
        Me.waitlab.TabIndex = 32
        Me.waitlab.Text = "Please Wait For Processing"
        Me.waitlab.Visible = False
        '
        'MARXAN_PREP_FORM
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(403, 185)
        Me.Controls.Add(Me.waitlab)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.outputbox)
        Me.Controls.Add(Me.TargetID)
        Me.Controls.Add(Me.TargetNAME)
        Me.Controls.Add(Me.targetbox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MARXAN_PREP_FORM"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Marxan Target Prep"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents targetbox As System.Windows.Forms.ComboBox
    Friend WithEvents TargetNAME As System.Windows.Forms.ComboBox
    Friend WithEvents TargetID As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents outputbox As System.Windows.Forms.TextBox
    Friend WithEvents waitlab As System.Windows.Forms.Label

End Class
