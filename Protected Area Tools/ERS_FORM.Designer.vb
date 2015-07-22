<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ERS_FORM
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
        Me.layerList = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.outputbox = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.outputSize = New System.Windows.Forms.TextBox
        Me.maxVal = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.ScaleYESNO = New System.Windows.Forms.CheckBox
        Me.minscale = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.maxScale = New System.Windows.Forms.TextBox
        Me.outfile = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.comboboxer = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.MaxINDIS = New System.Windows.Forms.TextBox
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(270, 297)
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
        'layerList
        '
        Me.layerList.FormattingEnabled = True
        Me.layerList.Location = New System.Drawing.Point(10, 24)
        Me.layerList.Name = "layerList"
        Me.layerList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.layerList.Size = New System.Drawing.Size(408, 160)
        Me.layerList.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Input Layers:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(213, 247)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Overlay Function:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 197)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Output Raster:"
        '
        'outputbox
        '
        Me.outputbox.BackColor = System.Drawing.SystemColors.Window
        Me.outputbox.Location = New System.Drawing.Point(94, 193)
        Me.outputbox.Name = "outputbox"
        Me.outputbox.ReadOnly = True
        Me.outputbox.Size = New System.Drawing.Size(288, 20)
        Me.outputbox.TabIndex = 10
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(388, 192)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(31, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 246)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Output Cell Size:"
        '
        'outputSize
        '
        Me.outputSize.Location = New System.Drawing.Point(94, 244)
        Me.outputSize.Name = "outputSize"
        Me.outputSize.Size = New System.Drawing.Size(109, 20)
        Me.outputSize.TabIndex = 13
        Me.outputSize.Text = "30"
        Me.outputSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'maxVal
        '
        Me.maxVal.Location = New System.Drawing.Point(337, 221)
        Me.maxVal.Name = "maxVal"
        Me.maxVal.Size = New System.Drawing.Size(83, 20)
        Me.maxVal.TabIndex = 14
        Me.maxVal.Text = "1"
        Me.maxVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(232, 223)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Intensity Scale Max:"
        '
        'ScaleYESNO
        '
        Me.ScaleYESNO.AutoSize = True
        Me.ScaleYESNO.Location = New System.Drawing.Point(94, 269)
        Me.ScaleYESNO.Name = "ScaleYESNO"
        Me.ScaleYESNO.Size = New System.Drawing.Size(88, 17)
        Me.ScaleYESNO.TabIndex = 16
        Me.ScaleYESNO.Text = "Scale Output"
        Me.ScaleYESNO.UseVisualStyleBackColor = True
        '
        'minscale
        '
        Me.minscale.Enabled = False
        Me.minscale.Location = New System.Drawing.Point(212, 270)
        Me.minscale.Name = "minscale"
        Me.minscale.Size = New System.Drawing.Size(75, 20)
        Me.minscale.TabIndex = 17
        Me.minscale.Text = "0"
        Me.minscale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(180, 272)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(27, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Min:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(293, 273)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 13)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Max:"
        '
        'maxScale
        '
        Me.maxScale.Enabled = False
        Me.maxScale.Location = New System.Drawing.Point(327, 270)
        Me.maxScale.Name = "maxScale"
        Me.maxScale.Size = New System.Drawing.Size(92, 20)
        Me.maxScale.TabIndex = 20
        Me.maxScale.Text = "1"
        Me.maxScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'outfile
        '
        Me.outfile.Enabled = False
        Me.outfile.Location = New System.Drawing.Point(114, 219)
        Me.outfile.Margin = New System.Windows.Forms.Padding(2)
        Me.outfile.Name = "outfile"
        Me.outfile.Size = New System.Drawing.Size(108, 20)
        Me.outfile.TabIndex = 21
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 220)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(103, 13)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "Raster Catolog Item:"
        '
        'comboboxer
        '
        Me.comboboxer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboboxer.FormattingEnabled = True
        Me.comboboxer.Location = New System.Drawing.Point(306, 245)
        Me.comboboxer.Margin = New System.Windows.Forms.Padding(2)
        Me.comboboxer.Name = "comboboxer"
        Me.comboboxer.Size = New System.Drawing.Size(114, 21)
        Me.comboboxer.TabIndex = 23
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(57, 305)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(93, 13)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "Expand Extent by:"
        '
        'MaxINDIS
        '
        Me.MaxINDIS.Location = New System.Drawing.Point(154, 302)
        Me.MaxINDIS.Name = "MaxINDIS"
        Me.MaxINDIS.Size = New System.Drawing.Size(104, 20)
        Me.MaxINDIS.TabIndex = 25
        Me.MaxINDIS.Text = "1000"
        Me.MaxINDIS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ERS_FORM
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(428, 339)
        Me.Controls.Add(Me.MaxINDIS)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.comboboxer)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.outfile)
        Me.Controls.Add(Me.maxScale)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.minscale)
        Me.Controls.Add(Me.ScaleYESNO)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.maxVal)
        Me.Controls.Add(Me.outputSize)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.outputbox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.layerList)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ERS_FORM"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Environmental Risk Surface (ERS) Generator"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents layerList As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents outputbox As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents outputSize As System.Windows.Forms.TextBox
    Friend WithEvents maxVal As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ScaleYESNO As System.Windows.Forms.CheckBox
    Friend WithEvents minscale As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents maxScale As System.Windows.Forms.TextBox
    Friend WithEvents outfile As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents comboboxer As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents MaxINDIS As System.Windows.Forms.TextBox

End Class
