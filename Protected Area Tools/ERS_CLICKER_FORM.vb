Imports System.Windows.Forms
'Imports System.Runtime.InteropServices
'Imports System.Drawing
'Imports ESRI.ArcGIS.ADF.BaseClasses
'Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
'Imports ESRI.ArcGIS.ArcMapUI
'Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Carto
'Imports ESRI.ArcGIS.Geoprocessing
'Imports ESRI.ArcGIS.Geoprocessor
'Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Geometry

Public Class ERS_CLICKER_FORM

    Private m_application As IApplication
    Private layers() As ILayer
    Private outpath As String
    Private cellsize As Double
    Private maxval As Double
    Private combofunc As String
    Private minscale As Double
    Private maxscale As Double
    Private layersinfo As ERS_REC

    Private Linelabel, intensitylabel, infldislabel, DecayTypeLabel, RateLabel, Overlaylabel, Weightlabel As Integer


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim i As Integer
        Dim cbo As ComboBox
        Dim crl As TextBox

        'thing = Me.Controls(7)

        For i = 0 To layersinfo.ERS_LAYERS.Length - 1

            If layersinfo.ERS_LAYERS(i).layertype = ERS_REC.laytype.raster Then

                crl = Main_Panel.Controls(2 + (i * 7))
                layersinfo.ERS_LAYERS(i).weight = crl.Text
                crl = Main_Panel.Controls(3 + (i * 7))
                layersinfo.ERS_LAYERS(i).intensity = crl.Text

            ElseIf layersinfo.ERS_LAYERS(i).layertype = ERS_REC.laytype.feature Then

                cbo = Main_Panel.Controls(1 + (i * 7))
                If cbo.SelectedIndex = -1 Then
                    If Not IsNumeric(cbo.Text) Then
                        MsgBox("All user input values must be numeric, " + cbo.Text + " is not", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    Try
                        type_defs.IncludeVal(layers(i), "infdis_pro", cbo.Text)
                    Catch
                        MsgBox("Error editing input layer " + layers(i).Name + ". Make sure this layer is editable", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End Try
                    layersinfo.ERS_LAYERS(i).infdis = "infdis_pro"
                Else
                    layersinfo.ERS_LAYERS(i).infdis = cbo.Text
                End If

                cbo = Main_Panel.Controls(2 + (i * 7))
                If cbo.SelectedIndex = -1 Then
                    If Not IsNumeric(cbo.Text) Then
                        MsgBox("All user input values must be numeric, " + cbo.Text + " is not", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    Try
                        type_defs.IncludeVal(layers(i), "weight_pro", cbo.Text)
                    Catch
                        MsgBox("Error editing input layer " + layers(i).Name + ". Make sure this layer is editable", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End Try
                    layersinfo.ERS_LAYERS(i).weight = "weight_pro"
                Else
                    layersinfo.ERS_LAYERS(i).weight = cbo.Text
                End If

                cbo = Main_Panel.Controls(3 + (i * 7))
                If cbo.SelectedIndex = -1 Then
                    If Not IsNumeric(cbo.Text) Then
                        MsgBox("All user input values must be numeric, " + cbo.Text + " is not", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    Try
                        type_defs.IncludeVal(layers(i), "intsty_pro", cbo.Text)
                    Catch
                        MsgBox("Error editing input layer " + layers(i).Name + ". Make sure this layer is editable", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End Try
                    layersinfo.ERS_LAYERS(i).intensity = "intsty_pro"
                Else
                    layersinfo.ERS_LAYERS(i).intensity = cbo.Text
                End If

                cbo = Main_Panel.Controls(4 + (i * 7))
                layersinfo.ERS_LAYERS(i).decaytype = cbo.SelectedIndex

                crl = Main_Panel.Controls(5 + (i * 7))
                If Not IsNumeric(crl.Text) Then
                    MsgBox("All user input values must be numeric, " + cbo.Text + " is not", MsgBoxStyle.Exclamation)
                    Exit Sub
                Else
                    layersinfo.ERS_LAYERS(i).decayrate = CDbl(crl.Text)
                    'If layersinfo.ERS_LAYERS(i).decaytype = ERS_REC.decay_type.convex Then
                    'layersinfo.ERS_LAYERS(i).decayrate = CDbl(crl.Text) * -1
                    'End If

                End If

                cbo = Main_Panel.Controls(6 + (i * 7))
                    layersinfo.ERS_LAYERS(i).combofunk = cbo.Text

            End If

        Next i


        Dim ERSProcess As ProcessUpdate = New ProcessUpdate(m_application, "ERS")

        'ERSProcess.Show()
        'ERSProcess.Hide()
        Me.Hide()
        Dim suc As Boolean
        suc = ERSProcess.ProcessERS(layersinfo)

        'type_defs.ProcessERS(layersinfo, m_application)

        ERSProcess.Close()

        Me.Close()
        'ERSProcess.Dispose()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Dispose()
        'Parent.Dispose()

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ERS_CLICKER_FORM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'MsgBox(layers(0).Name)

        Linelabel = 3
        intensitylabel = 180
        infldislabel = 310
        DecayTypeLabel = 440
        RateLabel = 540
        Overlaylabel = 600
        Weightlabel = 700


        Label1.Location = New System.Drawing.Point(Linelabel, 12)
        Label2.Location = New System.Drawing.Point(infldislabel, 12)
        Label3.Location = New System.Drawing.Point(Weightlabel, 12)
        Label4.Location = New System.Drawing.Point(intensitylabel, 12)
        Label5.Location = New System.Drawing.Point(DecayTypeLabel, 12)
        Label6.Location = New System.Drawing.Point(RateLabel, 12)
        Label7.Location = New System.Drawing.Point(Overlaylabel, 12)

        'MsgBox(layers.Length)
        'MsgBox(layersinfo.Length)

        Dim gpu As ESRI.ArcGIS.Geoprocessing.GPUtilities = New ESRI.ArcGIS.Geoprocessing.GPUtilities

        Dim i As Long
        For i = 0 To layers.Length - 1
            CreateControlLine(i, layers(i))
            'add to info
            layersinfo.ERS_LAYERS(i).pgpLayer = gpu.MakeGPLayerFromLayer(layers(i))
            layersinfo.ERS_LAYERS(i).layertype = type_defs.ltype(layers(i))
        Next

        'gpu = Nothing

    End Sub

    Private Sub CreateControlLine(ByVal Count As Long, ByVal layer As ILayer)
        'Intensity, Influence, Decay Type, Overlay, then Weight?

        Dim lt As ERS_REC.laytype

        lt = type_defs.ltype(layer)

        If lt = ERS_REC.laytype.feature Then

            Dim flist As List(Of String)
            flist = type_defs.getvalfields(layer)

            Dim lbl As New TextBox()
            lbl.Location = New System.Drawing.Point(Linelabel + 2, 3 + (Count * 27))
            lbl.Size = New System.Drawing.Size(160, 20)
            lbl.Text = layer.Name
            lbl.ReadOnly = True
            Main_Panel.Controls.Add(lbl)

            If Count < 10 Then
                Me.Size = (New System.Drawing.Size(850, 150 + (Count * 27)))
                Main_Panel.Size = (New System.Drawing.Size(845, 27 + (Count * 27)))
            Else
                Me.Size = (New System.Drawing.Size(850, 150 + (10 * 27)))
                Main_Panel.Size = (New System.Drawing.Size(845, 27 + (10 * 27)))
            End If


            Dim cbo1 As New ComboBox
            cbo1.Location = New System.Drawing.Point(infldislabel, 2 + (Count * 27))
            cbo1.Size = New System.Drawing.Size(120, 20)
            cbo1.Text = "1000"
            type_defs.addtocbox(flist, cbo1)
            Main_Panel.Controls.Add(cbo1)

            Dim cbo2 As New ComboBox
            cbo2.Location = New System.Drawing.Point(Weightlabel, 2 + (Count * 27))
            cbo2.Size = New System.Drawing.Size(120, 20)
            cbo2.Text = "1"
            type_defs.addtocbox(flist, cbo2)
            Main_Panel.Controls.Add(cbo2)

            Dim cbo3 As New ComboBox
            cbo3.Location = New System.Drawing.Point(intensitylabel, 2 + (Count * 27))
            cbo3.Size = New System.Drawing.Size(120, 20)
            cbo3.Text = "100"
            type_defs.addtocbox(flist, cbo3)
            Main_Panel.Controls.Add(cbo3)

            Dim cbo4 As New ComboBox
            cbo4.Location = New System.Drawing.Point(DecayTypeLabel, 2 + (Count * 27))
            cbo4.Size = New System.Drawing.Size(85, 20)
            cbo4.Items.Add("Linear")
            cbo4.Items.Add("Concave")
            cbo4.Items.Add("Convex")
            cbo4.Items.Add("Constant")
            cbo4.SelectedIndex = 0
            cbo4.DropDownStyle = ComboBoxStyle.DropDownList
            Main_Panel.Controls.Add(cbo4)
            AddHandler cbo4.SelectedIndexChanged, AddressOf Me.ChangeDecay

            Dim txter As New TextBox
            txter.Location = New System.Drawing.Point(RateLabel, 2 + (Count * 27))
            txter.Size = New System.Drawing.Size(50, 20)
            txter.Text = "1"
            txter.Enabled = False
            Main_Panel.Controls.Add(txter)

            Dim cbo5 As New ComboBox
            cbo5.Location = New System.Drawing.Point(Overlaylabel, 2 + (Count * 27))
            cbo5.Size = New System.Drawing.Size(95, 20)
            Dim listofvalid As String
            Dim listoffuncs As String()
            listofvalid = "MEAN|MAJORITY|MAXIMUM|MEDIAN|MINIMUM|MINORITY|RANGE|STD|SUM|VARIETY"
            listoffuncs = Split(listofvalid, "|")
            cbo5.Items.AddRange(listoffuncs)
            cbo5.SelectedItem = "SUM"
            cbo5.DropDownStyle = ComboBoxStyle.DropDownList
            Main_Panel.Controls.Add(cbo5)
        Else

            Dim lbl As New Label()
            lbl.Location = New System.Drawing.Point(Linelabel + 2, 3 + (Count * 27))
            lbl.Size = New System.Drawing.Size(150, 20)
            lbl.Text = layer.Name
            Main_Panel.Controls.Add(lbl)
            Me.Size = (New System.Drawing.Size(835, 150 + (Count * 27)))

            Dim cbo1 As New Label
            cbo1.Location = New System.Drawing.Point(infldislabel, 2 + (Count * 27))
            cbo1.Size = New System.Drawing.Size(120, 20)
            cbo1.Text = "GRID (NA)"
            Main_Panel.Controls.Add(cbo1)

            Dim cbo2 As New TextBox
            cbo2.Location = New System.Drawing.Point(Weightlabel, 2 + (Count * 27))
            cbo2.Size = New System.Drawing.Size(120, 20)
            cbo2.Text = "1"
            Main_Panel.Controls.Add(cbo2)

            Dim cbo3 As New TextBox
            cbo3.Location = New System.Drawing.Point(intensitylabel, 2 + (Count * 27))
            cbo3.Size = New System.Drawing.Size(120, 20)
            cbo3.Text = "100"
            Main_Panel.Controls.Add(cbo3)

            Dim cbo4 As New Label
            cbo4.Location = New System.Drawing.Point(DecayTypeLabel, 2 + (Count * 27))
            cbo4.Size = New System.Drawing.Size(85, 20)
            cbo4.Text = "GRID (NA)"
            'cbo4.Items.Add("Linear")
            'cbo4.Items.Add("Concave")
            'cbo4.Items.Add("Convex")
            'cbo4.Items.Add("Constant")
            'cbo4.SelectedIndex = 0
            'cbo4.DropDownStyle = ComboBoxStyle.DropDownList
            Main_Panel.Controls.Add(cbo4)

            Dim txter As New Label
            txter.Location = New System.Drawing.Point(RateLabel, 2 + (Count * 27))
            txter.Size = New System.Drawing.Size(50, 20)
            txter.Text = "NA"
            Main_Panel.Controls.Add(txter)

            Dim cbo5 As New Label
            cbo5.Location = New System.Drawing.Point(Overlaylabel, 2 + (Count * 27))
            cbo5.Size = New System.Drawing.Size(85, 20)
            cbo5.Text = "GRID (NA)"
            Main_Panel.Controls.Add(cbo5)
        End If

            Main_Panel.AutoScroll = True

    End Sub
    Public Sub New(ByVal m_app As IApplication, ByVal inlayers() As ILayer, ByVal i_outpath As String, ByVal i_cellsize As Double, ByVal i_outmaxval As Double, ByVal i_combofunc As String, ByVal outn As String, ByVal env As IEnvelope, Optional ByVal i_minscale As Double = -9999, Optional ByVal i_maxscale As Double = -9999)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        layersinfo = New ERS_REC(inlayers.Length)

        m_application = m_app
        ' Add any initialization after the InitializeComponent() call.
        ReDim layers(inlayers.Length - 1)
        layers = inlayers

        layersinfo.outpath = i_outpath
        layersinfo.outname = outn
        layersinfo.cellsize = i_cellsize
        layersinfo.maxval = i_outmaxval
        layersinfo.combofunc = i_combofunc
        layersinfo.minscale = i_minscale
        layersinfo.maxscale = i_maxscale
        layersinfo.extent = env

    End Sub



    Private Sub ChangeDecay(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cbo As ComboBox
        cbo = CType(sender, ComboBox)
        'MsgBox(cbo.TabIndex)
        If (cbo.SelectedIndex = 1) Or (cbo.SelectedIndex = 2) Then
            Main_Panel.Controls(cbo.TabIndex + 1).Text = 3
            Main_Panel.Controls(cbo.TabIndex + 1).Enabled = True
        Else
            Main_Panel.Controls(cbo.TabIndex + 1).Text = 1
            Main_Panel.Controls(cbo.TabIndex + 1).Enabled = False
        End If
    End Sub

End Class
