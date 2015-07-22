<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RBI_FORM
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
        Me.targetbox = New System.Windows.Forms.ListBox
        Me.pubox = New System.Windows.Forms.ComboBox
        Me.pufield = New System.Windows.Forms.ComboBox
        Me.extentbox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.outputbox = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.outputname = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(487, 216)
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
        'targetbox
        '
        Me.targetbox.FormattingEnabled = True
        Me.targetbox.ItemHeight = 16
        Me.targetbox.Location = New System.Drawing.Point(12, 34)
        Me.targetbox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.targetbox.Name = "targetbox"
        Me.targetbox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.targetbox.Size = New System.Drawing.Size(301, 212)
        Me.targetbox.TabIndex = 1
        '
        'pubox
        '
        Me.pubox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.pubox.FormattingEnabled = True
        Me.pubox.Location = New System.Drawing.Point(447, 15)
        Me.pubox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.pubox.Name = "pubox"
        Me.pubox.Size = New System.Drawing.Size(239, 24)
        Me.pubox.TabIndex = 2
        '
        'pufield
        '
        Me.pufield.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.pufield.FormattingEnabled = True
        Me.pufield.Location = New System.Drawing.Point(447, 55)
        Me.pufield.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.pufield.Name = "pufield"
        Me.pufield.Size = New System.Drawing.Size(239, 24)
        Me.pufield.TabIndex = 3
        '
        'extentbox
        '
        Me.extentbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.extentbox.FormattingEnabled = True
        Me.extentbox.Location = New System.Drawing.Point(447, 98)
        Me.extentbox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.extentbox.Name = "extentbox"
        Me.extentbox.Size = New System.Drawing.Size(239, 24)
        Me.extentbox.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(344, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 17)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = " Analysis Unit:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(322, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = " Analysis ID Field:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(334, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 17)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Analysis Extent:"
        '
        'outputbox
        '
        Me.outputbox.BackColor = System.Drawing.SystemColors.Window
        Me.outputbox.Location = New System.Drawing.Point(447, 140)
        Me.outputbox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.outputbox.Name = "outputbox"
        Me.outputbox.ReadOnly = True
        Me.outputbox.Size = New System.Drawing.Size(199, 22)
        Me.outputbox.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(328, 143)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(113, 17)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Output Location:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(651, 140)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(35, 23)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(342, 182)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 17)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Output Name:"
        '
        'outputname
        '
        Me.outputname.Location = New System.Drawing.Point(447, 178)
        Me.outputname.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.outputname.Name = "outputname"
        Me.outputname.Size = New System.Drawing.Size(239, 22)
        Me.outputname.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 17)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Targets:"
        '
        'RBI_FORM
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 266)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.outputname)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.outputbox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.extentbox)
        Me.Controls.Add(Me.pufield)
        Me.Controls.Add(Me.pubox)
        Me.Controls.Add(Me.targetbox)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RBI_FORM"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Relative Biodiversity Index (RBI) Calculator"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents targetbox As System.Windows.Forms.ListBox
    Friend WithEvents pubox As System.Windows.Forms.ComboBox
    Friend WithEvents pufield As System.Windows.Forms.ComboBox
    Friend WithEvents extentbox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents outputbox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents outputname As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
