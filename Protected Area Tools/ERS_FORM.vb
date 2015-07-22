'Imports System.Windows.Forms
'Imports System.Runtime.InteropServices
'Imports System.Drawing
'Imports ESRI.ArcGIS.ADF.BaseClasses
'Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
'Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI
'Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports System.Runtime.InteropServices


Public Class ERS_FORM

    Private m_application As IApplication

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        'If Not System.IO.Directory.Exists(outputbox.Text) Then
        '    MsgBox("You must specify a folder that exists", MsgBoxStyle.Exclamation, "Incomplete Form Input")
        '    Me.BringToFront()
        '    Me.Select()
        '    Exit Sub
        'End If

        If outfile.Text = "" Then
            MsgBox("You must specify an output name", MsgBoxStyle.Exclamation, "Incomplete Form Input")
            Me.BringToFront()
            Me.Select()
            Exit Sub
        End If

        If IsNumeric(outfile.Text.Substring(0, 1)) Then
            MsgBox("Raster filenames cannont start with a number.", MsgBoxStyle.Exclamation, "Incomplete Form Input")
            Me.BringToFront()
            Me.Select()
            Exit Sub
        End If

        If outfile.Text.Length > 13 Then
            MsgBox("Raster filenames are limited to 8 characters.", MsgBoxStyle.Exclamation, "Incomplete Form Input")
            Me.BringToFront()
            Me.Select()
            Exit Sub
        End If

        If layerList.SelectedItems.Count = 0 Then
            MsgBox("You must select at least one layer to include in the processing.", MsgBoxStyle.Exclamation, "Incomplete Form Input")
            Me.BringToFront()
            Me.Select()
            Exit Sub
        End If

        If Not IsNumeric(MaxINDIS.Text) Then
            MsgBox("Max Influence Distance Must be numeric", MsgBoxStyle.Exclamation, "Incomplete Form Input")
            Me.BringToFront()
            Me.Select()
            Exit Sub
        End If

        If outputbox.Text = "" Then
            MsgBox("You must select an output location.", MsgBoxStyle.Exclamation, "Incomplete Form Input")
            Me.BringToFront()
            Me.Select()
            Exit Sub
        End If

        Dim Outsize As Double
        Dim Maxvalue As Double
        Dim Minsc As Double
        Dim Maxsc As Double

        Try
            Outsize = CDbl(outputSize.Text)
            Maxvalue = CDbl(maxVal.Text)
            If ScaleYESNO.Checked Then
                Minsc = CDbl(minscale.Text)
                Maxsc = CDbl(maxScale.Text)
            End If
        Catch ex As Exception
            MsgBox("Cell Size, Max Value and Scale Ranges (if specified) must all be numarical values.", MsgBoxStyle.Exclamation, "Incorrect Form Input")
            Me.BringToFront()
            Me.Select()
            Exit Sub
        End Try

        Dim combofunc As String

        combofunc = comboboxer.Text

        Dim layers() As ESRI.ArcGIS.Carto.ILayer

        ReDim layers(layerList.SelectedItems.Count - 1)
        Dim mdoc As IMxDocument
        mdoc = m_application.Document
        Dim i As Integer
        'put code to pass layer paths here
        'For i = 0 To layerList.SelectedItems.Count - 1
        '    layers(i) = layerList.SelectedItems.Item(i)
        '    MsgBox(layers(i))
        'Next i
        Dim canuse As ERS_REC.laytype
        Dim env As IEnvelope = New Envelope
        For i = 0 To layerList.SelectedIndices.Count - 1
            layers(i) = mdoc.FocusMap.Layer(layerList.SelectedIndices(i))
            canuse = type_defs.ltype(layers(i))
            env.Union(layers(i).AreaOfInterest)
            If canuse = ERS_REC.laytype.invalid Then
                MsgBox("Layer " + layers(i).Name + " is not a valid layer for this tool, select only feature or raster layers", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        Next i

        env.Expand(MaxINDIS.Text, MaxINDIS.Text, False)

        Dim outn As String
        outn = outfile.Text

        Dim thing As ERS_CLICKER_FORM
        If ScaleYESNO.Checked Then
            thing = New ERS_CLICKER_FORM(m_application, layers, outputbox.Text, Outsize, Maxvalue, combofunc, outn, env, Minsc, Maxsc)
        Else
            thing = New ERS_CLICKER_FORM(m_application, layers, outputbox.Text, Outsize, Maxvalue, combofunc, outn, env)
        End If
        'get reference for window
        thing.Show()


        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub



    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ERS_FORM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim mdoc As IMxDocument
        mdoc = m_application.Document
        Dim mlay As ESRI.ArcGIS.Carto.ILayer

        'Dim mDsName As IDatasetName
        Dim i As Long
        For i = 0 To mdoc.ActiveView.FocusMap.LayerCount - 1
            mlay = mdoc.ActiveView.FocusMap.Layer(i)
            Me.layerList.Items.Add(mlay.Name)
        Next i

        Dim listofvalid As String
        Dim listoffuncs As String()
        listofvalid = "MEAN|MAJORITY|MAXIMUM|MEDIAN|MINIMUM|MINORITY|RANGE|STD|SUM|VARIETY"
        listoffuncs = Split(listofvalid, "|")
        comboboxer.Items.AddRange(listoffuncs)
        comboboxer.SelectedItem = "SUM"

    End Sub

    Public Sub New(ByVal app As IApplication)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_application = app
    End Sub


    Private Sub ScaleYESNO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScaleYESNO.CheckedChanged
        If ScaleYESNO.Checked Then
            minscale.Enabled = True
            maxScale.Enabled = True
        Else
            minscale.Enabled = False
            maxScale.Enabled = False
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Me.Visible = False
        Dim pGxDialog As IGxDialog, pFilterColl As IGxObjectFilterCollection
        pGxDialog = New GxDialog

        pGxDialog.Title = "Specify Output Raster"
        pFilterColl = pGxDialog

        Dim pGxFilter As IGxObjectFilter
        pGxFilter = New GxFilterRasterCatalogDatasets
        pFilterColl.AddFilter(pGxFilter, True)

        pGxFilter = New GxFilterRasterDatasets
        pFilterColl.AddFilter(pGxFilter, True)

        'Dim pGxFilter As IGxObjectFilter
        'pGxFilter = New GxFilterFileGeodatabases
        'pFilterColl.AddFilter(pGxFilter, True)
        'pGxFilter = New GxFilterPersonalGeodatabases
        'pFilterColl.AddFilter(pGxFilter, True)
        'pGxFilter = New GxFilterContainers
        'pFilterColl.AddFilter(pGxFilter, True)
        'pGxFilter = New GxFilterContainers
        'pFilterColl.AddFilter(pGxFilter, True)

        'Dim pEnumGxObj As IEnumGxObject
        If Not pGxDialog.DoModalSave(0) Then
            outputbox.Text = ""
            Me.BringToFront()
            Me.Select()
            Exit Sub 'Exit if user press cancel. 
        End If

        Dim f As String
        f = pGxDialog.FinalLocation.FullName.Replace("\\", "\") + "\" + pGxDialog.Name

        If pGxDialog.ObjectFilter.Description <> "Raster Catalog" Then
            outfile.Text = "NA"
            outfile.Enabled = False
        Else
            outfile.Enabled = True
            outfile.Text = ""
        End If

        outputbox.Text = f.Replace("\\", "\")

        'Marshal.ReleaseComObject(pGxDialog)

        Me.Visible = True
        Me.BringToFront()
        Me.Select()

    End Sub

    Private Sub outputbox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles outputbox.TextChanged

    End Sub

    Private Sub minscale_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles minscale.TextChanged

    End Sub

    Private Sub maxScale_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles maxScale.TextChanged

    End Sub
End Class
