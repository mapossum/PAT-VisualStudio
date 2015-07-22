Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.esriSystem


Public Class MARXAN_PREP_FORM
    Public mapp As IApplication
    Public m_application As IMxApplication

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim i As Integer
        For i = 0 To Me.Controls.Count - 1
            Me.Controls(i).Visible = False
        Next

        waitlab.Visible = True

        Dim gpu As IGPUtilities = New ESRI.ArcGIS.Geoprocessing.GPUtilities
        Dim tlay As ILayer
        Dim w As Integer
        Dim tlayer As IGPLayer

        Dim pmap As IMap
        Dim pdoc As IMxDocument
        pdoc = mapp.Document
        pmap = pdoc.FocusMap
        For w = 0 To pmap.LayerCount - 1
            'MsgBox(pmap.Layer(w).Name & " " & targetbox.Text)
            If pmap.Layer(w).Name = targetbox.Text Then
                tlay = pmap.Layer(w)
            End If
        Next

        'MsgBox(tlay.Name)

        tlayer = gpu.MakeGPLayerFromLayer(tlay)

        'Dim tout As Object
        Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
        GP.OverwriteOutput = True
        GP.AddOutputsToMap = False

        'Dim GP As Object
        'GP = CreateObject("esriGeoprocessing.GpDispatch.1")

        'GP.Toolbox = "management"
        ''Dim qs As String
        ''qs = "[" & fname1 & "] & " & Chr(34) & "," & Chr(34) & " & [" & fname2 & "]"
        ''GP.calculatefield intersectlayer, "Combine", qs

        Dim disser As New Dissolve
        disser.in_features = tlayer
        disser.out_feature_class = outputbox.Text
        disser.dissolve_field = TargetNAME.Text & ";" & TargetID.Text
        'GP.Dissolve(ComboBox3.Text, TextBox1.Text, ComboBox4.Text & ";" & ComboBox5.Text, "#", "MULTI_PART")
        ''On Error GoTo jjj

        Try
            ExTool(GP, disser, Nothing)
        Catch ex As Exception
            MsgBox("Unable to execute inital dissolve - Invalid Data.", MsgBoxStyle.Critical, "Cannot start Dissolve")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        GP.AddOutputsToMap = False


        Dim addf As AddField = New AddField(outputbox.Text, "GOAL", "DOUBLE")
        Try
            ExTool(GP, addf, Nothing)
        Catch ex As Exception
            MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "MARXAN")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        addf.field_name = "TYPE"
        Try
            ExTool(GP, addf, Nothing)
        Catch ex As Exception
            MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "MARXAN")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        addf.field_name = "SPF"
        Try
            ExTool(GP, addf, Nothing)
        Catch ex As Exception
            MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "MARXAN")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        addf.field_name = "OCC"
        Try
            ExTool(GP, addf, Nothing)
        Catch ex As Exception
            MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "MARXAN")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        addf.field_name = "CLUMP"
        Try
            ExTool(GP, addf, Nothing)
        Catch ex As Exception
            MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "MARXAN")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        addf.field_name = "SEPNUM"
        Try
            ExTool(GP, addf, Nothing)
        Catch ex As Exception
            MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "MARXAN")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        addf.field_name = "SEPDIS"
        Try
            ExTool(GP, addf, Nothing)
        Catch ex As Exception
            MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "MARXAN")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        addf.field_name = "PROP"
        Try
            ExTool(GP, addf, Nothing)
        Catch ex As Exception
            MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "MARXAN")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        addf.field_name = "TNAME"
        addf.field_type = "TEXT"
        Try
            ExTool(GP, addf, Nothing)
        Catch ex As Exception
            MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "MARXAN")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        addf.field_name = "TID"
        addf.field_type = "LONG"
        Try
            ExTool(GP, addf, Nothing)
        Catch ex As Exception
            MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "MARXAN")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        Dim calf As New CalculateField(outputbox.Text, "TID", "[" & TargetID.Text & "]")
        Try
            ExTool(GP, calf, Nothing)
        Catch ex As Exception
            MsgBox("Unable to create new field ", MsgBoxStyle.Critical, "MARXAN")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        calf.field = "TNAME"
        calf.expression = "[" & TargetNAME.Text & "]"
        Try
            ExTool(GP, calf, Nothing)
        Catch ex As Exception
            MsgBox("Unable to create new field ", MsgBoxStyle.Critical, "MARXAN")
            Debug.Print(ex.Message)
            Exit Sub
        End Try

        Dim t_prep As New MARXAN_CLICKER(mapp, outputbox.Text)
        t_prep.Show(New Win32HWNDWrapper(mapp.hWnd))
        Me.Hide()

        'GP.AddField(TextBox1.Text, "GOAL", "Double")
        'GP.AddField(TextBox1.Text, "TYPE", "DOUBLE")
        'GP.AddField(TextBox1.Text, "SPF", "Double")
        'GP.AddField(TextBox1.Text, "OCC", "Double")
        'GP.AddField(TextBox1.Text, "CLUMP", "Double")
        'GP.AddField(TextBox1.Text, "SEPNUM", "Double")
        'GP.AddField(TextBox1.Text, "SEPDIS", "Double")
        'GP.AddField(TextBox1.Text, "PROP", "Double")
        ''Dissolve tnc_mxn_terr_targets C:\temp\asdfasd.shp ID;DESCRIPTIO # MULTI_PART

        'Dim pDoc As IMxDocument
        'Dim pMap As IMap
        'pDoc = ThisDocument
        'pMap = pDoc.FocusMap
        'Dim pfeaturelayer As IFeatureLayer
        ''Set pfeaturelayer = pMap.layer(0)

        'Dim ply As ILayer
        'For r = 0 To pMap.LayerCount - 1
        '    ply = pMap.Layer(r)
        '    If (Right(Left(TextBox1.Text, Len(TextBox1.Text)), Len(Left(TextBox1.Text, Len(TextBox1.Text))) - InStrRev(Left(TextBox1.Text, Len(TextBox1.Text)), "\")) = ply.Name) Then pfeaturelayer = ply
        '    If (Right(Left(TextBox1.Text, Len(TextBox1.Text) - 4), Len(Left(TextBox1.Text, Len(TextBox1.Text) - 4)) - InStrRev(Left(TextBox1.Text, Len(TextBox1.Text) - 4), "\")) = ply.Name) Then pfeaturelayer = ply
        'Next r

        'Dim pFclass As IFeatureClass
        'pFclass = pfeaturelayer.FeatureClass
        'Dim pfeatcur As IFeatureCursor
        'Dim pfeat As IFeature
        'Dim fcount As Long
        'fcount = pFclass.FeatureCount(Nothing)
        'ReDim RH(fcount - 1)
        'counter = 0

        'pfeatcur = pFclass.Search(Nothing, False)

        'pfeat = pfeatcur.NextFeature
        'count = 0
        'Do Until pfeat Is Nothing

        '    RH(count).Target_NAME = pfeat.Value(pfeat.Fields.FindField(ComboBox4.Text))
        '    RH(count).Target_ID = pfeat.Value(pfeat.Fields.FindField(ComboBox5.Text))
        '    RH(count).Amount = 0
        '    RH(count).Clump = -1
        '    RH(count).Occurance = -1
        '    RH(count).proportion = -1
        '    RH(count).Sep_Dis = -1
        '    RH(count).Sep_Num = -1
        '    RH(count).SPF = -1
        '    RH(count).Target_Type = -1

        '    pfeat = pfeatcur.NextFeature
        '    count = count + 1
        'Loop



        'MARXAN_clicker.redisplayinfo()


        'MARXAN_clicker.Show()

        ''jjj:
        ''MsgBox GP.GetMessages(0)


        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Visible = False
        Dim pGxDialog As IGxDialog, pFilterColl As IGxObjectFilterCollection
        pGxDialog = New GxDialog

        pGxDialog.Title = "Choose a Output Feature Class"
        pFilterColl = pGxDialog

        Dim pGxFilter As IGxObjectFilter
        pGxFilter = New GxFilterSDEFeatureClasses
        pFilterColl.AddFilter(pGxFilter, True)
        pGxFilter = New GxFilterFGDBFeatureClasses
        pFilterColl.AddFilter(pGxFilter, True)
        pGxFilter = New GxFilterPGDBFeatureClasses
        pFilterColl.AddFilter(pGxFilter, True)
        pGxFilter = New GxFilterShapefiles
        pFilterColl.AddFilter(pGxFilter, True)

        'Dim pEnumGxObj As IEnumGxObject
        If Not pGxDialog.DoModalSave(0) Then
            outputbox.Text = ""
            Me.BringToFront()
            Me.Select()
            Exit Sub 'Exit if user press cancel. 
        End If

        outputbox.Text = pGxDialog.FinalLocation.FullName + "\" + pGxDialog.Name

        Me.Visible = True
        Me.BringToFront()
        Me.Select()
    End Sub

    Public Sub New(ByVal app As IApplication)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_application = app
        mapp = m_application
    End Sub

    Private Sub MARXAN_PREP_FORM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mdoc As IMxDocument
        mdoc = m_application.Document
        Dim mlay As ESRI.ArcGIS.Carto.ILayer


        'Dim mDsName As IDatasetName
        Dim i As Long
        For i = 0 To mdoc.ActiveView.FocusMap.LayerCount - 1
            mlay = mdoc.ActiveView.FocusMap.Layer(i)
            If TypeOf mlay Is IFeatureLayer Then
                Me.targetbox.Items.Add(mlay.Name)

            End If
        Next i

        Me.targetbox.SelectedIndex = 0
    End Sub


    Private Sub targetbox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles targetbox.SelectedIndexChanged

        Dim mdoc As IMxDocument
        mdoc = m_application.Document
        Dim mlay As ESRI.ArcGIS.Carto.ILayer
        Dim flist As List(Of String)
        Dim check As String

        check = targetbox.Text

        Dim i As Long
        For i = 0 To mdoc.ActiveView.FocusMap.LayerCount - 1
            mlay = mdoc.ActiveView.FocusMap.Layer(i)
            If mlay.Name = check Then
                TargetID.Items.Clear()
                TargetNAME.Items.Clear()
                flist = getvaljfields(mlay)
                If flist.Count = 0 Then
                    MsgBox("Layer " & check & " does not have any valid fields, fields must be of type number or string.", MsgBoxStyle.Exclamation, "Field Type Error")
                    OK_Button.Enabled = False
                    Exit Sub
                End If
                type_defs.addtocbox(flist, TargetID)
                type_defs.addtocbox(flist, TargetNAME)
                Try
                    TargetNAME.SelectedIndex = 0
                    TargetID.SelectedIndex = 1
                Catch

                End Try
                OK_Button.Enabled = True
                Exit Sub
            End If
        Next i
    End Sub

    Public Function getvaljfields(ByVal inLayer As IFeatureLayer) As List(Of String)
        Dim that As New List(Of String)
        Dim pfclass As IFeatureClass
        Dim pfld As IField
        Dim i As Integer

        pfclass = inLayer.FeatureClass
        For i = 0 To pfclass.Fields.FieldCount - 1
            pfld = pfclass.Fields.Field(i)
            If pfld.Type = esriFieldType.esriFieldTypeSmallInteger Or pfld.Type = esriFieldType.esriFieldTypeDouble Or pfld.Type = esriFieldType.esriFieldTypeSingle _
                Or pfld.Type = esriFieldType.esriFieldTypeString Or pfld.Type = esriFieldType.esriFieldTypeInteger Then
                that.Add(pfld.Name)
            End If
        Next
        Return that

    End Function

    Public Function ExTool(ByVal geop As ESRI.ArcGIS.Geoprocessor.Geoprocessor, ByVal process As IGPProcess, ByVal TC As ITrackCancel) As Object
        ' Set the overwrite output option to true
        Dim thing As IGeoProcessorResult

        geop.OverwriteOutput = True

        Try
            thing = geop.Execute(process, Nothing)
            ReturnMessages(geop)
            Return thing.ReturnValue
        Catch err As Exception
            ReturnMessages(geop)
            Return Nothing
        End Try

    End Function



    Private Sub outputbox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles outputbox.TextChanged

    End Sub
End Class
