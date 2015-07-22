Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI

Public Class RBI_FORM
    Public mapp As IApplication
    Public m_application As IMxApplication
    Dim layerdic As New Dictionary(Of String, ILayer)

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        If outputbox.Text = "" Or outputname.Text = "" Then
            MsgBox("You must enter both a output path and output name", MsgBoxStyle.Exclamation, "Incomplete Inputs")
            Exit Sub
        End If

        If targetbox.SelectedItems.Count = 0 Then
            MsgBox("You must select at least one target layer", MsgBoxStyle.Exclamation, "Incomplete Inputs")
            Exit Sub
        End If

        Dim i As Integer

        For i = 0 To targetbox.SelectedItems.Count - 1
            If targetbox.SelectedItems.Item(i) = pubox.Text Then
                MsgBox("Target Layers cannot be used as planning units", MsgBoxStyle.Exclamation, "Improper Inputs")
                Exit Sub
            End If
        Next

        Dim gpu As IGPUtilities = New ESRI.ArcGIS.Geoprocessing.GPUtilities
        Dim tlay As ILayer
        Dim layersinfo As New RBI_REC(targetbox.SelectedItems.Count)
        Dim w As Integer

        For w = 0 To targetbox.SelectedItems.Count - 1
            tlay = layerdic.Item(targetbox.SelectedItems(w))
            layersinfo.TARGET_LAYERS(w).TARGET_LAYER = gpu.MakeGPLayerFromLayer(tlay)
        Next

        tlay = layerdic.Item(pubox.Text)
        layersinfo.pulayer = gpu.MakeGPLayerFromLayer(tlay)

        tlay = layerdic.Item(extentbox.Text)
        layersinfo.extentlayer = gpu.MakeGPLayerFromLayer(tlay)

        layersinfo.outname = outputname.Text
        layersinfo.outpath = outputbox.Text
        layersinfo.pufield = pufield.Text

        Me.Hide()
        Dim that As RBI_CLICKER = New RBI_CLICKER(layersinfo, m_application)
        that.Show(mapp)
        'ERSProcess.Show()
        'ERSProcess.Hide()

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub New(ByVal app As IApplication)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_application = app
    End Sub

    Public Function getvaljfields(ByVal inLayer As IFeatureLayer) As List(Of String)
        Dim that As New List(Of String)
        Dim pfclass As IFeatureClass
        Dim pfld As IField
        Dim i As Integer

        pfclass = inLayer.FeatureClass
        For i = 0 To pfclass.Fields.FieldCount - 1
            pfld = pfclass.Fields.Field(i)
            If pfld.Type = esriFieldType.esriFieldTypeInteger Or pfld.Type = esriFieldType.esriFieldTypeSmallInteger _
                                                   Or pfld.Type = esriFieldType.esriFieldTypeString Or pfld.Type = esriFieldType.esriFieldTypeInteger Then

                that.Add(pfld.Name)
            End If
        Next
        Return that

    End Function

    Private Sub RBI_FORM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim mdoc As IMxDocument
        mdoc = m_application.Document
        Dim mlay As ESRI.ArcGIS.Carto.ILayer
        Dim mlayer As IFeatureLayer
        Dim pclass As IFeatureClass
        layerdic.Clear()

        'Dim mDsName As IDatasetName
        Dim i As Long
        For i = 0 To mdoc.ActiveView.FocusMap.LayerCount - 1
            mlay = mdoc.ActiveView.FocusMap.Layer(i)
            If TypeOf mlay Is IFeatureLayer Then
                Me.targetbox.Items.Add(mlay.Name)
                mlayer = mlay
                pclass = mlayer.FeatureClass
                If pclass.ShapeType = esriGeometryType.esriGeometryPolygon Then
                    Me.pubox.Items.Add(mlay.Name)
                    Me.extentbox.Items.Add(mlay.Name)
                End If
                Try
                    layerdic.Add(mlay.Name, mlay)
                Catch ex As Exception
                    MsgBox("This tool does not support multiple layers with the same name", MsgBoxStyle.Exclamation, "Rename Layer")
                    Me.Close()
                End Try
            End If
        Next i

        Me.pubox.SelectedIndex = 0
        Me.extentbox.SelectedIndex = 0

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Visible = False
        Dim pGxDialog As IGxDialog, pFilterColl As IGxObjectFilterCollection
        pGxDialog = New GxDialog

        pGxDialog.Title = "Folder or Geodatabase"
        pFilterColl = pGxDialog

        Dim pGxFilter As IGxObjectFilter
        'pGxFilter = New GxFilterFGDBFeatureDatasets
        'pFilterColl.AddFilter(pGxFilter, True)
        pGxFilter = New GxFilterFileGeodatabases
        pFilterColl.AddFilter(pGxFilter, True)
        'pGxFilter = New GxFilterPGDBFeatureDatasets
        'pFilterColl.AddFilter(pGxFilter, True)
        pGxFilter = New GxFilterPersonalGeodatabases
        pFilterColl.AddFilter(pGxFilter, True)
        pGxFilter = New GxFilterContainers
        pFilterColl.AddFilter(pGxFilter, True)

        'Dim pEnumGxObj As IEnumGxObject
        If Not pGxDialog.DoModalSave(0) Then ', Nothing) Then
            outputbox.Text = ""
            Me.BringToFront()
            Me.Select()
            Exit Sub 'Exit if user press cancel. 
        End If


        If Not ((My.Computer.FileSystem.FileExists(pGxDialog.FinalLocation.FullName + "\" + pGxDialog.Name) Or _
                My.Computer.FileSystem.DirectoryExists(pGxDialog.FinalLocation.FullName + "\" + pGxDialog.Name))) Then
            MsgBox("Specified folder or geodatabase does not exist", MsgBoxStyle.Exclamation, "Error")
            Me.Visible = False
            Exit Sub
        End If

        'If (My.Computer.FileSystem.DirectoryExists(pGxDialog.FinalLocation.FullName + "\" + pGxDialog.Name)) Then
        '    If Not (pGxDialog.Name.Contains(".gdb")) Then
        '        MsgBox("Specified location is not a folder or local geodatabase", MsgBoxStyle.Exclamation, "Error")
        '        Exit Sub
        '    End If
        'End If

        If (My.Computer.FileSystem.FileExists(pGxDialog.FinalLocation.FullName + "\" + pGxDialog.Name)) Then
            If Not (pGxDialog.Name.Contains(".mdb")) Then
                MsgBox("Specified location is not a folder or local geodatabase", MsgBoxStyle.Exclamation, "Error")
                Exit Sub
            End If
        End If

        outputbox.Text = pGxDialog.FinalLocation.FullName + "\" + pGxDialog.Name

        Me.Visible = True
        Me.BringToFront()
        Me.Select()
    End Sub

    Private Sub pubox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pubox.SelectedIndexChanged

        Dim mdoc As IMxDocument
        mdoc = m_application.Document
        Dim mlay As ESRI.ArcGIS.Carto.ILayer
        Dim flist As List(Of String)
        Dim check As String

        check = pubox.Text

        Dim i As Long
        For i = 0 To mdoc.ActiveView.FocusMap.LayerCount - 1
            mlay = mdoc.ActiveView.FocusMap.Layer(i)
            If mlay.Name = check Then
                pufield.Items.Clear()
                flist = getvaljfields(mlay)
                If flist.Count = 0 Then
                    MsgBox("Layer " & check & " does not have any valid fields, ID field must be of type integer or string.", MsgBoxStyle.Exclamation, "Field Type Error")
                    OK_Button.Enabled = False
                    Exit Sub
                End If
                type_defs.addtocbox(flist, pufield)
                pufield.SelectedIndex = 0
                OK_Button.Enabled = True
                Exit Sub
            End If
        Next i

    End Sub


    Private Sub targetbox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles targetbox.SelectedIndexChanged

    End Sub

    Private Sub pufield_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pufield.SelectedIndexChanged

    End Sub
End Class
