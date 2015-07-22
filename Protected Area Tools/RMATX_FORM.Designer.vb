<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RMATX_FORM
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.matrix = New System.Windows.Forms.RadioButton()
        Me.relational = New System.Windows.Forms.RadioButton()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.output = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.sfd = New System.Windows.Forms.SaveFileDialog()
        Me.cmdialog = New System.Windows.Forms.OpenFileDialog()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.puvspr = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(331, 141)
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
        'matrix
        '
        Me.matrix.AutoSize = True
        Me.matrix.Checked = True
        Me.matrix.Location = New System.Drawing.Point(12, 11)
        Me.matrix.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.matrix.Name = "matrix"
        Me.matrix.Size = New System.Drawing.Size(421, 17)
        Me.matrix.TabIndex = 1
        Me.matrix.TabStop = True
        Me.matrix.Text = "convert a Marxan sparse (or relational) matrix file from one sorted by Planning U" & _
            "nit ID"
        Me.matrix.UseVisualStyleBackColor = True
        '
        'relational
        '
        Me.relational.AutoSize = True
        Me.relational.Location = New System.Drawing.Point(12, 33)
        Me.relational.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.relational.Name = "relational"
        Me.relational.Size = New System.Drawing.Size(416, 17)
        Me.relational.TabIndex = 2
        Me.relational.Text = "convert a Marxan tabular matrix file to a sparse matrix file sorted by Planning U" & _
            "nit ID"
        Me.relational.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(418, 95)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(23, 19)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'output
        '
        Me.output.BackColor = System.Drawing.SystemColors.Window
        Me.output.Location = New System.Drawing.Point(79, 96)
        Me.output.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.output.Name = "output"
        Me.output.ReadOnly = True
        Me.output.Size = New System.Drawing.Size(336, 20)
        Me.output.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 98)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Output File:"
        '
        'sfd
        '
        Me.sfd.Filter = "Marxan Files(*.dat)|*.dat"
        '
        'cmdialog
        '
        Me.cmdialog.Filter = "Marxan Input Files(*.dat)|*.dat"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(418, 72)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(23, 19)
        Me.Button2.TabIndex = 12
        Me.Button2.Text = "..."
        Me.Button2.UseVisualStyleBackColor = True
        '
        'puvspr
        '
        Me.puvspr.BackColor = System.Drawing.SystemColors.Window
        Me.puvspr.Location = New System.Drawing.Point(79, 73)
        Me.puvspr.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.puvspr.Name = "puvspr"
        Me.puvspr.ReadOnly = True
        Me.puvspr.Size = New System.Drawing.Size(336, 20)
        Me.puvspr.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 75)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "puvspr file:"
        '
        'ofd
        '
        Me.ofd.Filter = "Marxan Files(*.dat)|*.dat"
        '
        'RMATX_FORM
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(489, 182)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.puvspr)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.output)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.relational)
        Me.Controls.Add(Me.matrix)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RMATX_FORM"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Run Convert_mtx.exe"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents matrix As System.Windows.Forms.RadioButton
    Friend WithEvents relational As System.Windows.Forms.RadioButton
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents output As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents sfd As System.Windows.Forms.SaveFileDialog
    Friend WithEvents cmdialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents puvspr As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog

End Class
