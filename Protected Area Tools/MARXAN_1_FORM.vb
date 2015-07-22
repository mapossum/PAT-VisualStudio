Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto
Imports System.Runtime.InteropServices
'Imports System.Drawing
'Imports ESRI.ArcGIS.ADF.BaseClasses
'Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.ConversionTools
Imports ESRI.ArcGIS.SpatialAnalystTools
Imports ESRI.ArcGIS.SpatialStatisticsTools
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.AnalysisTools
Imports System.Reflection
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI


Public Class MARXAN_1_FORM
    Dim layerdic As New Dictionary(Of String, ILayer)
    Public m_application As IApplication

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim BIGDIC As New List(Of preFile)
        Dim PUDIC As New List(Of preFile)

        If statusbox.Text <> "" Then
            useLayer.Checked = True
        End If

        If outputbox.Text = "" Then
            MsgBox("You must enter an output folder", MsgBoxStyle.Exclamation, "Incomplete Inputs")
            Exit Sub
        End If

        If targetbox.SelectedItems.Count = 0 Then
            MsgBox("You must select at least one input layer", MsgBoxStyle.Exclamation, "Incomplete Inputs")
            Exit Sub
        End If

        Dim i As Integer

        For i = 0 To targetbox.SelectedItems.Count - 1
            If targetbox.SelectedItems.Item(i) = pubox.Text Then
                MsgBox("Target Layers cannot be used as planning units", MsgBoxStyle.Exclamation, "Improper Inputs")
                Exit Sub
            End If
        Next

        If statusbox.Text = pubox.Text Then
            MsgBox("Status Layers cannot also be used as planning units", MsgBoxStyle.Exclamation, "Improper Inputs")
            Exit Sub
        End If

        Dim pMxDoc As IMxDocument
        pMxDoc = m_application.Document
        Dim pMap As IMap
        Dim fname1, fname2 As String
        pMap = pMxDoc.FocusMap
        Dim papplication As IApplication
        papplication = m_application
        Dim pstatus As IStatusBar
        pstatus = papplication.StatusBar
        Dim Tcurve As ICurve
        Dim qs As String

        Dim targetLayer, pulayer, outpaths, copyname, statfname, statuslayer, sumfield As String
        statuslayer = ""
        pulayer = pubox.Text  'MARXANForm.PUBOX.Text

        If useLayer.Checked = False Then
            statfname = Me.statusbox.Text 'MARXANForm.STATUSBOX.Text
        Else
            statuslayer = Me.statusbox.Text 'MARXANForm.STATUSBOX.Text
        End If

        Me.Hide()

        m_application.StatusBar.ProgressBar.Show()
        m_application.StatusBar.ProgressBar.Position = 0


        Dim tout, t2 As Object
        Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
        Dim gpu As IGPUtilities = New ESRI.ArcGIS.Geoprocessing.GPUtilities
        GP.OverwriteOutput = True
        GP.AddOutputsToMap = False

        Dim player As IFeatureLayer
        player = GP.Open(pulayer)
        Dim pdset As IDataset
        Dim toolong As Boolean

        pdset = player.FeatureClass
        If (Len(pdset.Name) > 12 Or IsNumeric(pdset.Name.Substring(0, 1))) Then toolong = True


        Dim timestamp, timestamp0 As String
        Dim tnow As DateTime

        tnow = Now
        timestamp0 = Format(Now, "yyyymmddHhNnSs")

        'On Error GoTo E1
        If meanbut.Checked = True Then sumfield = "MEAN"
        If sumbut.Checked = True Then sumfield = "SUM"
        If maxbut.Checked = True Then sumfield = "MAX"

        'Dim fs As Object
        'fs = CreateObject("Scripting.FileSystemObject")
        'fs.CreateFolder(outputbox.Text & "\tempfiles")

        fname2 = pufield.Text

        Dim pFeatClass As IFeatureClass
        Dim pFeatLayer As IFeatureLayer
        Dim pDataSet As IDataset
        Dim pucopy As String

        'GP.CreateFolder_management(outputbox.Text, "tempfiles" & timestamp)

        Dim patscratch As String = ""
        'rkey = "HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Control\Session Manager\Environment"
        'Dim regKey As Microsoft.Win32.RegistryKey
        'regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\ControlSet001\Control\Session Manager\Environment", False)
        'patscratch = regKey.GetValue("PAT_SCRATCH")
        'regKey.Close()

        Dim matrixplace As String = ""
        Dim marxanplace As String = ""
        Dim patplace As String = ""
        Dim ineditplace As String = ""
        Dim rplace As String = ""

        Dim readpaths As New System.IO.StreamReader(IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) & "\PATInfo\paths.txt")

        Dim line As String

        ' Read first line.
        line = readpaths.ReadLine

        ' Loop over each line in file, While list is Not Nothing.
        Do While (Not line Is Nothing)
            Dim vals As String()
            vals = line.Split("|")
            If vals.Length = 2 Then
                If vals(0) = "MATRIXPLACE" Then matrixplace = vals(1)
                If vals(0) = "MARXANPLACE" Then marxanplace = vals(1)
                If vals(0) = "PATSCRATCH" Then patplace = vals(1)
                If vals(0) = "RPLACE" Then rplace = vals(1)
                If vals(0) = "INEDITPLACE" Then ineditplace = vals(1)
                'patplace = regKey.GetValue("PATSCRATCH")
                'marxanplace = regKey.GetValue("MARXANHOME")
                'ineditplace = regKey.GetValue("INEDITHOME")
                'rplace = regKey.GetValue("RHOME")
                patscratch = patplace
            End If
            line = readpaths.ReadLine
        Loop

        readpaths.Close()

        If patscratch = "" Then
            MsgBox("You must set the PAT SCRATCH folder in options menu.", MsgBoxStyle.Exclamation, "PAT SCRATCH not found")
            Exit Sub
        End If


        'If rom.WorkspaceType = esriWorkspaceType.esriFileSystemWorkspace Then

        'Directory.CreateDirectory(patscratch & "\RBI_" & timestamp)

        Dim cf As New CreateFolder(patscratch, "MIG_tempfiles" & timestamp0)
        ExTool(GP, cf, Nothing)

        GP.SetEnvironmentValue("workspace", (patscratch & "\MIG_tempfiles" & timestamp0))

        Dim boundcov, intersectlayer, dissolvelayer, linelayer, polypu, statusint, ratiolayer As String
        boundcov = "bound_cov"

        '        '  'Set the toolbox
        Dim abundancelayer As String
        intersectlayer = "target_pu_int"
        dissolvelayer = "dis_target_pu"
        abundancelayer = "abundance"
        linelayer = "bound_lines"
        polypu = "pu_poly"
        pucopy = "pu_copy"
        statusint = "Status_int"
        Dim statusints As String = "Status_ints"
        ratiolayer = "status_ratio"
        Dim jtabname As String


        '        'Create abundance file
        Dim addf As AddField = New AddField()
        Dim calf As New CalculateField()
        Dim inter As Intersect = New Intersect()
        Dim aj2 As AddJoin = New AddJoin()
        Dim dess As Dissolve = New Dissolve()

        m_application.StatusBar.ProgressBar.Position = 15

        Dim selitem As Integer
        For Each selitem In targetbox.SelectedIndices

            tnow = Now
            timestamp = Format(Now, "yyyymmddHhNnSs")

            'Dim cf As New CreateFolder(outputbox.Text, "tempfiles" & timestamp)
            cf.out_folder_path = patscratch
            cf.out_name = "MIG_tempfiles" & timestamp
            ExTool(GP, cf, Nothing)

            GP.SetEnvironmentValue("workspace", (patscratch & "\MIG_tempfiles" & timestamp))

            'MsgBox(selitem)
            targetLayer = targetbox.Items.Item(selitem)
            fname1 = "TID"

            pstatus.Message(0) = "Creating puvspr file"

            inter.in_features = pulayer & " ; " & targetLayer
            inter.out_feature_class = "target_pu_int.shp"
            inter.join_attributes = "ALL"
            inter.output_type = "INPUT"
            tout = ExTool(GP, inter, Nothing)

            addf.in_table = "target_pu_int.shp"
            addf.field_name = "Combine"
            addf.field_type = "TEXT"
            ExTool(GP, addf, Nothing)
            addf.field_type = "DOUBLE"
            addf.field_name = "F_AREA"
            ExTool(GP, addf, Nothing)

            '        GP.Toolbox = "management"
            '        GP.AddField(intersectlayer, "Combine", "text")
            '        Dim qs As String
            qs = "[" & fname1 & "] & " & Chr(34) & "," & Chr(34) & " & [" & fname2 & "]"

            calf.field = "Combine"
            calf.in_table = "target_pu_int.shp"
            calf.expression_type = "VB"
            calf.expression = qs
            ExTool(GP, calf, Nothing)

            dess.in_features = "target_pu_int.shp"
            dess.out_feature_class = "target_pu_ds.shp"
            dess.dissolve_field = "Combine;" + pufield.Text

            t2 = ExTool(GP, dess, Nothing)

            '        GP.calculatefield(intersectlayer, "Combine", qs)
            '        GP.Dissolve(intersectlayer, dissolvelayer, "Combine")

            Dim pfclass As IFeatureClass
            pfclass = GP.Open(t2)

            Dim typ As String
            typ = "NON"
            If pfclass.ShapeType = esriGeometryType.esriGeometryPoint Then typ = "!SHAPE.PARTCOUNT!"
            If pfclass.ShapeType = esriGeometryType.esriGeometryPolyline Then typ = "!SHAPE.LENGTH!"
            If pfclass.ShapeType = esriGeometryType.esriGeometryPolygon Then typ = "!SHAPE.AREA!"
            If pfclass.ShapeType = esriGeometryType.esriGeometryMultipoint Then typ = "!SHAPE.PARTCOUNT!"
            If pfclass.ShapeType = esriGeometryType.esriGeometryLine Then typ = "!SHAPE.LENGTH!"

            calf.in_table = tout
            calf.field = "F_AREA"
            calf.expression = typ
            calf.expression_type = "PYTHON"
            Try
                ExTool(GP, calf, Nothing)
            Catch ex As Exception
                MsgBox("Unable to calculate Amount.", MsgBoxStyle.Critical, "MARXAN")
                Debug.Print(ex.Message)
                Exit Sub
            End Try

            writeABfile(GP, tout, typ, pufield.Text, fname1, fname2, PUDIC)

            Dim pinfclass As IFeatureLayer

            pstatus.Message(0) = "Creating spec file"
            pinfclass = GP.Open(targetLayer)


            Dim MyTable As ESRI.ArcGIS.Geodatabase.ITable = CType(pinfclass, ESRI.ArcGIS.Geodatabase.ITable)

            'create table sort 
            Dim MyTableSort As New ESRI.ArcGIS.Geodatabase.TableSort
            MyTableSort.Fields = "TID"
            MyTableSort.Ascending("TID") = True
            MyTableSort.Table = MyTable
            'sort the table
            MyTableSort.Sort(Nothing)

            'output the returned results of the sort
            Dim pcur As ESRI.ArcGIS.Geodatabase.ICursor = MyTableSort.Rows
            Dim pfeat As ESRI.ArcGIS.Geodatabase.IRow = pcur.NextRow

            'Dim pcur As IFeatureCursor
            Dim idin, typein, target, SPF, target2, sepdistance, sepnum, namein, targetocc, prop As String
            'pcur = pinfclass.Search(Nothing, False)

            Dim writestart2 As Boolean
            If My.Computer.FileSystem.FileExists(Me.outputbox.Text & "\block.dat") Then
                writestart2 = False
            Else
                writestart2 = True
            End If

            Dim b As Awriter
            If CreateBlock.Checked = True Then
                b = New Awriter(Me.outputbox.Text & "\block.dat")
                If writestart2 Then b.WriteLine("type,target,target2,targetocc,sepnum,sepdistance,prop,spf")
            End If

            Try
                Do Until pfeat Is Nothing
                    idin = CStr(pfeat.Value(pfeat.Fields.FindField("TID")))
                    If CreateBlock.Checked = True Then
                        typein = CStr(pfeat.Value(pfeat.Fields.FindField("TYPE")))
                    Else
                        typein = CStr(-1)
                    End If
                    namein = CStr(pfeat.Value(pfeat.Fields.FindField("TNAME")))
                    SPF = CStr(pfeat.Value(pfeat.Fields.FindField("SPF")))
                    target = CStr(pfeat.Value(pfeat.Fields.FindField("GOAL")))
                    targetocc = CStr(pfeat.Value(pfeat.Fields.FindField("OCC")))
                    target2 = CStr(pfeat.Value(pfeat.Fields.FindField("CLUMP")))
                    sepnum = CStr(pfeat.Value(pfeat.Fields.FindField("SEPNUM")))
                    sepdistance = CStr(pfeat.Value(pfeat.Fields.FindField("SEPDIS")))
                    prop = CStr(pfeat.Value(pfeat.Fields.FindField("PROP")))

                    Dim nextl As New preFile
                    nextl.zIndex = CLng(idin)
                    If newspec.Checked = True Then
                        'a.WriteLine( )
                        nextl.line = idin & "," & prop & "," & target & "," & SPF & "," & namein
                    Else
                        'a.WriteLine(idin & "," & typein & "," & target & "," & SPF & "," & target2 & "," & sepdistance & "," & _sepnum & "," & namein & "," & targetocc)
                        nextl.line = idin & "," & typein & "," & target & "," & SPF & "," & target2 & "," & sepdistance & "," & sepnum & "," & namein & "," & targetocc
                    End If

                    BIGDIC.Add(nextl)

                    If CreateBlock.Checked = True Then b.WriteLine(typein & "," & target & "," & target2 & "," & targetocc & _
                                                                "," & sepnum & "," & sepdistance & "," & prop & "," & SPF)
                    pfeat = pcur.NextRow
                Loop
            Catch ex As Exception
                MsgBox(ex.Message)
                '''' modify code to instert special field helper here

                MsgBox("Target input file is invalid. Run Marxan Target Prep on " & targetLayer & " and restart M.I.G.  Also make sure Target IDs are unique across all input data layers.", MsgBoxStyle.Exclamation, "")
                m_application.StatusBar.ProgressBar.Hide()
                Exit Sub

            End Try


            If CreateBlock.Checked = True Then
                b.Close()
            End If

        Next selitem


        Dim writestart As Boolean
        If My.Computer.FileSystem.FileExists(Me.outputbox.Text & "\puvspr.dat") Then
            writestart = False
        Else
            writestart = True
        End If
        Dim pva As Awriter = New Awriter(Me.outputbox.Text & "\puvspr.dat")

        If writestart Then pva.WriteLine("species,pu,amount")

        PUDIC.Sort(Function(x, y) x.zIndex.CompareTo(y.zIndex))
        
        For Each its As preFile In PUDIC
            pva.WriteLine(its.line)
        Next

        pva.Close()


        m_application.StatusBar.ProgressBar.Position = 50


        If My.Computer.FileSystem.FileExists(Me.outputbox.Text & "\spec.dat") Then
            writestart = False
        Else
            writestart = True
        End If

        Dim a As Awriter = New Awriter(Me.outputbox.Text & "\spec.dat")
        If newspec.Checked = True Then
            If writestart Then a.WriteLine("id,prop,target,spf,name")
        Else
            If writestart Then a.WriteLine("id,type,target,spf,target2,sepdistance,sepnum,name,targetocc")
        End If

        BIGDIC.Sort(Function(x, y) x.zIndex.CompareTo(y.zIndex))

        For Each it As preFile In BIGDIC
            a.WriteLine(it.line)
            Console.WriteLine(it.line)
        Next

        a.Close()

        m_application.StatusBar.ProgressBar.Position = 55

        GP.SetEnvironmentValue("workspace", (patscratch & "\MIG_tempfiles" & timestamp0))


        Dim cpyf As CopyFeatures = New CopyFeatures(pulayer, "pu_short.shp")
        '        'Create boundary file

        '        GP.Toolbox = "conversion"
        Dim covin As Object
        'If toolong Then
        covin = ExTool(GP, cpyf, Nothing)
        'Else
        'covin = pulayer
        'End If
        Dim pupoly, puarc As String

        addf.in_table = covin
        addf.field_name = "STAT"
        addf.field_type = "DOUBLE"
        ExTool(GP, addf, Nothing)
        addf.field_name = "OA_RATIO"
        addf.field_type = "DOUBLE"
        ExTool(GP, addf, Nothing)
        addf.field_name = "F_AREA"
        addf.field_type = "DOUBLE"
        ExTool(GP, addf, Nothing)
        addf.field_name = "CENTROIDXY"
        addf.field_type = "TEXT"
        addf.field_length = 100
        ExTool(GP, addf, Nothing)
        addf.field_name = "STATUS_CAL"
        addf.field_type = "LONG"
        ExTool(GP, addf, Nothing)
        addf.field_name = "OV_SL"
        addf.field_type = "LONG"
        ExTool(GP, addf, Nothing)

        pupoly = "PUPOLY" & CStr(selitem)
        puarc = "PUARC" & CStr(selitem)

        calf.in_table = covin
        calf.field = "F_AREA"
        calf.expression = "!SHAPE.AREA!"
        calf.expression_type = "PYTHON"
        ExTool(GP, calf, Nothing)

        If CreateBound.Checked = True Then
            pstatus.Message(0) = "Creating boundary file"
            ' & " POLYGON"
            '*************PUTCODE TO MEAUSRE LENTGH OF FCTOOCV

            Dim fcc As FeatureclassToCoverage = New FeatureclassToCoverage(covin & " POLYGON", boundcov)
            ExTool(GP, fcc, Nothing)
            'Exit Sub
            'dim bld as New

            '        GP.featureclasstocoverage(pulayer & " POLYGON", boundcov)
            '        ercode = "Arc Info with ArcGIS Workstation is NOT present"
            '        GP.Toolbox = "arc"
            '        GP.Build(boundcov, "line")
            '        ercode = ""
            '        GP.Toolbox = "conversion"

            Dim createGDB As CreateFileGDB = New CreateFileGDB(patscratch & "\MIG_tempfiles" & timestamp0, "temp.gdb")
            ExTool(GP, createGDB, Nothing)

            Dim copyfeats As CopyFeatures = New CopyFeatures(patscratch & "\MIG_tempfiles" & timestamp0 + "\bound_cov\arc", patscratch & "\MIG_tempfiles" & timestamp0 & "\temp.gdb\" & "bound_cov_arc")
            ExTool(GP, copyfeats, Nothing)

            Dim copyfeats2 As CopyFeatures = New CopyFeatures(patscratch & "\MIG_tempfiles" & timestamp0 + "\bound_cov\polygon", patscratch & "\MIG_tempfiles" & timestamp0 & "\temp.gdb\" & "bound_cov_polygon")
            ExTool(GP, copyfeats2, Nothing)

            'Dim aout As Object
            'Dim fcsf As FeatureClassToShapefile = New FeatureClassToShapefile(boundcov & "\arc", patscratch & "\MIG_tempfiles" & timestamp0)
            'ExTool(GP, fcsf, Nothing)
            'fcsf.Input_Features = (boundcov & "\polygon")
            'ExTool(GP, fcsf, Nothing)

            ''        GP.FeatureClassToShapefile(outputbox.Text & "\tempfiles" & timestamp & "\" & boundcov & "\arc", outputbox.Text & "\tempfiles" & timestamp)
            ''        GP.FeatureClassToShapefile(outputbox.Text & "\tempfiles" & timestamp & "\" & boundcov & "\polygon", outputbox.Text & "\tempfiles" & timestamp)

            ''        Addlay("bound_cov_polygon.shp", timestamp)
            ''        Addlay("bound_cov_arc.shp", timestamp)

            ''        GP.Toolbox = "management"

            addf.in_table = patscratch & "\MIG_tempfiles" & timestamp0 & "\temp.gdb\" & "bound_cov_arc"
            addf.field_type = "LONG"
            addf.field_name = "LEFTID"
            ExTool(GP, addf, Nothing)
            '        GP.AddField("bound_cov_arc", "LEFTID", "LONG")
            addf.field_name = "RIGHTID"
            ExTool(GP, addf, Nothing)
            '        GP.AddField("bound_cov_arc", "RIGHTID", "LONG")
            addf.field_name = "LEN"
            ExTool(GP, addf, Nothing)
            '        GP.AddField("bound_cov_arc", "LEN", "LONG")



            'Dim cpyf As CopyFeatures = New CopyFeatures(pulayer, "pu_short.shp")
            cpyf.in_features = patscratch & "\MIG_tempfiles" & timestamp0 & "\temp.gdb\" & "bound_cov_arc"
            cpyf.out_feature_class = patscratch & "\MIG_tempfiles" & timestamp0 & "\temp.gdb\" & "bound_cov_arc_f"
            ExTool(GP, cpyf, Nothing)

            Dim mfl As MakeFeatureLayer = New MakeFeatureLayer(patscratch & "\MIG_tempfiles" & timestamp0 & "\temp.gdb\" & "bound_cov_polygon", pupoly)
            ExTool(GP, mfl, Nothing)
            Dim mfl2 As MakeFeatureLayer = New MakeFeatureLayer(patscratch & "\MIG_tempfiles" & timestamp0 & "\temp.gdb\" & "bound_cov_arc_f", puarc)
            ExTool(GP, mfl2, Nothing)

            calf.in_table = puarc
            calf.field = "LEN"
            calf.expression = "!SHAPE.LENGTH!"
            ExTool(GP, calf, Nothing)
            '        GP.calculatefield("bound_cov_arc", "LEN", "[LENGTH]")

            'Dim aj As AddJoin = New AddJoin(puarc, "LEFTPOLYG", pupoly, "BOUND_COV_")
            Dim aj As AddJoin = New AddJoin(puarc, "F_LEFTPOLYGON", pupoly, "BOUND_COV_")

            aj.join_type = False '"KEEP_COMMON"
            ExTool(GP, aj, Nothing)

            calf.in_table = puarc
            calf.field = "bound_cov_arc_f.LEFTID"
            calf.expression_type = "VB"
            calf.expression = "[bound_cov_polygon." & pufield.Text & "]"
            ExTool(GP, calf, Nothing)

            '        GP.AddJoin("bound_cov_arc", "LPOLY_", "bound_cov_polygon", "BOUND_COV_", "KEEP_COMMON")
            '        GP.calculatefield("bound_cov_arc", "bound_cov_arc.LEFTID", "[bound_cov_polygon." & pufield.Text & "]")
            Dim rj As RemoveJoin = New RemoveJoin(puarc)
            rj.join_name = "bound_cov_polygon"
            ExTool(GP, rj, Nothing)

            '        GP.RemoveJoin("bound_cov_arc", "bound_cov_polygon")
            '        GP.AddJoin("bound_cov_arc", "RPOLY_", "bound_cov_polygon", "BOUND_COV_", "KEEP_COMMON")

            'aj.in_field = "RIGHTPOLY"
            aj.in_field = "F_RIGHTPOLYGON"
            ExTool(GP, aj, Nothing)

            calf.field = "bound_cov_arc_f.RIGHTID"
            ExTool(GP, calf, Nothing)
            '        GP.calculatefield("bound_cov_arc", "bound_cov_arc.RIGHTID", "[bound_cov_polygon." & pufield.Text & "]")
            '        GP.RemoveJoin("bound_cov_arc", "bound_cov_polygon")
            ExTool(GP, rj, Nothing)

            '        ercode = "Selection 1"
            Dim sellay As SelectLayerByAttribute = New SelectLayerByAttribute()
            sellay.in_layer_or_view = puarc
            sellay.where_clause = "LEFTID = 0"
            ExTool(GP, sellay, Nothing)
            '        SelectMapFeatures("LEFTID = 0", "bound_cov_arc")
            '        GP.calculatefield("bound_cov_arc", "LEFTID", "[RIGHTID]")
            calf.in_table = puarc
            calf.field = "LEFTID"
            calf.expression = "[RIGHTID]"
            calf.expression_type = "VB"
            ExTool(GP, calf, Nothing)
            '        ercode = "Selection 2"
            '        SelectMapFeatures("RIGHTID = 0", "bound_cov_arc")
            '        GP.calculatefield("bound_cov_arc", "RIGHTID", "[LEFTID]")
            '        ercode = "unSelection 1"
            '        unSelectMapFeatures("bound_cov_arc")
            '        ercode = ""
            sellay.in_layer_or_view = puarc
            sellay.where_clause = "RIGHTID = 0"
            ExTool(GP, sellay, Nothing)
            calf.in_table = puarc
            calf.field = "RIGHTID"
            calf.expression = "[LEFTID]"
            calf.expression_type = "VB"
            ExTool(GP, calf, Nothing)

            sellay.in_layer_or_view = puarc
            sellay.selection_type = "CLEAR_SELECTION"
            sellay.where_clause = ""
            ExTool(GP, sellay, Nothing)

            '        GP.AddField("bound_cov_arc", "Combine", "TEXT")
            Dim addf2 As AddField = New AddField(patscratch & "\MIG_tempfiles" & timestamp0 & "\temp.gdb\" & "bound_cov_arc_f", "Combine", "TEXT")
            ExTool(GP, addf2, Nothing)

            qs = "[LEFTID] & " & Chr(34) & "," & Chr(34) & " & [RIGHTID]"
            calf.in_table = patscratch & "\MIG_tempfiles" & timestamp0 & "\temp.gdb\" & "bound_cov_arc_f"
            calf.field = "Combine"
            calf.expression = qs
            calf.expression_type = "VB"
            ExTool(GP, calf, Nothing)
            '        GP.calculatefield("bound_cov_arc", "Combine", qs)

            Dim dis As Dissolve = New Dissolve(patscratch & "\MIG_tempfiles" & timestamp0 & "\temp.gdb\" & "bound_cov_arc_f", "bounds_f.shp")
            dis.dissolve_field = "Combine"
            dis.statistics_fields = "LEN SUM;LEFTID MIN;RIGHTID MIN"
            ExTool(GP, dis, Nothing)

            '        GP.Dissolve("bound_cov_arc", "bounds_f", "Combine", "LEN SUM")

            Dim pfc As IFeatureClass
            pfc = GP.Open("bounds_f.shp")

            writeboundfile(pfc)
            pfc = Nothing
            jtabname = "bound_cov_polygon"
        Else
            Dim mfl As MakeFeatureLayer = New MakeFeatureLayer("pu_short.shp", pupoly)
            ExTool(GP, mfl, Nothing)
            pupoly = "PUPOLY" & CStr(selitem)
            jtabname = "pu_short"
        End If

        m_application.StatusBar.ProgressBar.Position = 70



        If costbox.Text = "Planning Unit Field" Then

            'GP.calculatefield(pucopy, "STAT", "[" & MARXANForm.COSTFIELD.Text & "]")
            calf.in_table = pupoly
            calf.field = "STAT"
            calf.expression = "[" & costfield.Text & "]"
            calf.expression_type = "VB"
            ExTool(GP, calf, Nothing)

        ElseIf costbox.Text = "Planning Unit Area" Then

            '            GP.calculatefield(pucopy, "STAT", "[AREA]")
            calf.in_table = pupoly
            calf.field = "STAT"
            calf.expression = "!SHAPE.AREA!"
            calf.expression_type = "PYTHON"
            ExTool(GP, calf, Nothing)

        ElseIf IsNumeric(costbox.Text) Then

            '            GP.calculatefield(pucopy, "STAT", MARXANForm.COSTBOX.Text)
            calf.in_table = pupoly
            calf.field = "STAT"
            calf.expression_type = "VB"
            calf.expression = CDbl(costbox.Text)
            ExTool(GP, calf, Nothing)

        Else

            '            GP.RefreshCatalog(outputbox.Text & "\tempfiles" & timestamp)
            '            ercode = "If your Cost Selection was a valid layer, Spatial Analyst is not avalible, if you have it installed make sure you turn it on."
            '            'GP.CheckOutExtension "Spatial"
            '            GP.Toolbox = "sa"
            Dim flt As Float = New Float
            flt.in_raster_or_constant = costbox.Text
            flt.out_raster = "temp_ras"
            ExTool(GP, flt, Nothing)
            '            GP.Float(MARXANForm.COSTBOX.Text, "temp_ras")
            '            ercode = ""
            player = Nothing
            Dim zsat As ZonalStatisticsAsTable = New ZonalStatisticsAsTable(pulayer, pufield.Text, "temp_ras", "zone_sum")
            zsat.in_zone_data = pulayer
            zsat.zone_field = pufield.Text
            zsat.in_value_raster = patscratch & "\MIG_tempfiles" & timestamp0 & "\temp_ras"
            zsat.out_table = "zone_sum"
            'zsat.ignore_nodata =
            ExTool(GP, zsat, Nothing)
            '            GP.ZonalStatisticsAsTable(pulayer, pufield.Text, "temp_ras", "zone_sum", "DATA")
            '            AddDBASEFile("zone_sum.dbf", timestamp)
            '            GP.Toolbox = "management"

            Dim jfield As New JoinField(pupoly, pufield.Text, "zone_sum", pufield.Text)
            jfield.fields = sumfield
            ExTool(GP, jfield, Nothing)

            calf.in_table = pupoly
            calf.field = "STAT"
            calf.expression_type = "VB"
            calf.expression = "[" & sumfield & "]"
            ExTool(GP, calf, Nothing)

            'aj2.in_layer_or_view = pupoly
            'aj2.in_field = pufield.Text
            'aj2.join_table = "zone_sum"
            'aj2.join_field = pufield.Text '"VALUE"  '''''''''ARCGIS10 Change
            'aj2.join_type = False
            'ExTool(GP, aj2, Nothing)
            ''            GP.AddJoin(pucopy, pufield.Text, "zone_sum", "VALUE", "KEEP_COMMON")
            'calf.in_table = pupoly
            'calf.field = jtabname + ".STAT" ' bound_cov_polygon.
            'calf.expression = "[zone_sum:" & sumfield & "]"
            'ExTool(GP, calf, Nothing)

            'Dim rj As RemoveJoin = New RemoveJoin(pupoly, "zone_sum")
            'ExTool(GP, rj, Nothing)

        End If



        calf.expression = "!SHAPE.CENTROID!"
        calf.in_table = pupoly
        calf.field = "CENTROIDXY"
        calf.expression_type = "PYTHON"
        ExTool(GP, calf, Nothing)


        '        GP.AddField(pucopy, "XLOC", "DOUBLE")
        '        GP.AddField(pucopy, "YLOC", "DOUBLE")

        '        desc = GP.Describe(pucopy)

        '        dcur = GP.UpdateCursor(pucopy)
        '        drow = dcur.Next

        '        Do Until drow Is Nothing
        '            dgeo = drow.GetValue(desc.ShapeFieldName)
        '            tcounter = tcounter + 1

        '            pts = Split(CStr(dgeo.Centroid), " ")
        '            'MsgBox centpoint.Type
        '            ex = pts(0)
        '            why = pts(1)
        '            drow.SetValue("XLOC", ex)
        '            drow.SetValue("YLOC", why)

        '            dcur.UpdateRow(drow)
        '            drow = dcur.Next
        '        Loop

        '        desc = Nothing
        '        dcur = Nothing
        '        drow = Nothing


        '        GP.AddField(pucopy, "STATUS_CAL", "LONG")
        '        GP.calculatefield(pucopy, "STATUS_CAL", "0")


        calf.field = "STATUS_CAL"
        calf.expression = "0"
        calf.expression_type = "VB"
        ExTool(GP, calf, Nothing)

        m_application.StatusBar.ProgressBar.Position = 85

        If ((useLayer.Checked = True) And (statusbox.Text <> "") And (StatusValueBox.SelectedIndex <> 0)) Then
            'GP.Toolbox = "stats"

            'addf.in_table = pupoly
            'addf.field_name = "PUA"
            'addf.field_type = "DOUBLE"
            'ExTool(GP, addf, Nothing)
            'addf.field_name = "A_RATIO"
            'addf.field_type = "DOUBLE"
            'ExTool(GP, addf, Nothing)


            Dim puout As Object
            '''Dim cala As CalculateAreas = New CalculateAreas()
            '''cala.Input_Feature_Class = pupoly
            '''cala.Output_Feature_Class = "pupoly_wArea.shp"
            '''puout = ExTool(GP, cala, Nothing)



            'GP.CalculateAreas_stats(pulayer, "puarea")
            'GP.Toolbox = "management"
            'GP.AddField("puarea", "PUA", "LONG")
            'GP.calculatefield("puarea", "PUA", "[F_AREA]")

            'calf.in_table = puout
            'calf.field = "PUA"
            'calf.expression = "[F_AREA]"
            'calf.expression_type = "VB"
            'ExTool(GP, calf, Nothing)

            'GP.Toolbox = "analysis"
            'GP.Intersect("puarea" & " ; " & statuslayer, statusint & ".shp", "ALL", "#", "INPUT")          
            inter.in_features = pupoly & " ; " & statuslayer
            inter.out_feature_class = statusint & ".shp"
            inter.join_attributes = "ALL"
            inter.output_type = "INPUT"
            puout = ExTool(GP, inter, Nothing)

            Dim seller As [Select] = New [Select](puout, statusints)

            seller.where_clause = Chr(34) + "F_AREA" + Chr(34) + " <> 0"
            puout = ExTool(GP, seller, Nothing)

            'GP.Toolbox = "stats"
            'GP.CalculateAreas_stats(statusint, "status_ratio")
            'GP.Toolbox = "management"
            'GP.AddField(pucopy, "RATIO", "DOUBLE")
            calf.in_table = puout
            calf.field = "OA_RATIO"
            calf.expression = "!SHAPE.AREA! / !F_AREA!"
            calf.expression_type = "PYTHON"
            ExTool(GP, calf, Nothing)
            ' Exit Sub

            'Dim mfl2 As MakeFeatureLayer = New MakeFeatureLayer(puout, "stat_ratio")
            'ExTool(GP, mfl2, Nothing)
            aj2.in_layer_or_view = pupoly
            aj2.in_field = fname2
            aj2.join_table = puout
            aj2.join_field = fname2
            aj2.join_type = False
            ExTool(GP, aj2, Nothing)

            'GP.AddJoin(pucopy, fname2, "status_ratio", fname2, "KEEP_COMMON")
            calf.expression = "[Status_ints.OA_RATIO]"
            calf.expression_type = "VB"
            calf.in_table = pupoly
            calf.field = jtabname & ".OA_RATIO"
            ExTool(GP, calf, Nothing)


            If StatusValueBox.SelectedIndex < 4 Then

                Dim rj2 As RemoveJoin = New RemoveJoin(pupoly)
                rj2.join_name = "Status_ints"
                ExTool(GP, rj2, Nothing)

                calf.in_table = pupoly
                calf.field = "STATUS_CAL"
                calf.expression = "getstat(!OA_RATIO!," & CStr(CDbl(ThresholdBox.Text) / 100) & ")"
                calf.expression_type = "PYTHON"
                calf.code_block = "def getstat(fthres,jthres):" & Microsoft.VisualBasic.ControlChars.CrLf & _
                                "     if fthres > jthres:" & Microsoft.VisualBasic.ControlChars.CrLf & _
                                "          return " & CStr(StatusValueBox.SelectedIndex) & Microsoft.VisualBasic.ControlChars.CrLf & _
                                "     else:" & Microsoft.VisualBasic.ControlChars.CrLf & _
                                "          return 0"
                ExTool(GP, calf, Nothing)

            Else

                'GP.AddJoin(pucopy, fname2, "status_ratio", fname2, "KEEP_COMMON")
                calf.expression = "[Status_ints." & StatusValueBox.Text & "]"
                calf.expression_type = "VB"
                calf.in_table = pupoly
                calf.field = jtabname & ".OV_SL"
                ExTool(GP, calf, Nothing)

                Dim rj2 As RemoveJoin = New RemoveJoin(pupoly)
                rj2.join_name = "Status_ints"
                ExTool(GP, rj2, Nothing)

                calf.in_table = pupoly
                calf.field = "STATUS_CAL"
                calf.expression = "getstat(!OA_RATIO!," & CStr(CDbl(ThresholdBox.Text) / 100) & ",!OV_SL!)"
                calf.expression_type = "PYTHON"
                calf.code_block = "def getstat(fthres,jthres,statval):" & Microsoft.VisualBasic.ControlChars.CrLf & _
                                "     if fthres > jthres:" & Microsoft.VisualBasic.ControlChars.CrLf & _
                                "          return statval" & Microsoft.VisualBasic.ControlChars.CrLf & _
                                "     else:" & Microsoft.VisualBasic.ControlChars.CrLf & _
                                "          return 0"
                ExTool(GP, calf, Nothing)

            End If

            'GP.calculatefield(pucopy, "bound_cov_polygon.RATIO", "[status_ratio.F_AREA] / [status_ratio.PUA]")
            'GP.RemoveJoin(pucopy, ratiolayer)
            'Dim ratio As Double
            'ratio = (CDbl(STATUSFIELD.Text) / 100)
            'SelectMapFeatures "Ratio > 0" & CStr(ratio), pucopy
            'ercode = "Selection 3"
            'GP.SelectlayerbyAttribute(pucopy, "NEW_SELECTION", Chr(34) & "Ratio" & Chr(34) & " > 0" & CStr(ratio))
            ''MsgBox Chr(34) & "Ratio" & Chr(34) & " > 0" & CStr(ratio)
            'GP.calculatefield(pucopy, "STATUS_CAL", "2")
            'ercode = "Selection 4"
            'GP.SelectlayerbyAttribute(pucopy, "CLEAR_SELECTION")
            'ercode = ""
            ''unSelectMapFeatures pucopy
        End If

        If ((useLayer.Checked = False) And (statusbox.Text <> "")) Then

            calf.field = "STATUS_CAL"
            calf.expression = "[" & statusbox.Text & "]"
            calf.expression_type = "VB"
            ExTool(GP, calf, Nothing)

        End If

        Dim puc As IFeatureLayer
        puc = GP.Open(pupoly)
        Debug.Print("********************PU**********************")
        Debug.Print(pupoly)
        writePufile(puc)

        m_application.StatusBar.ProgressBar.Position = 100

        '        Dim pact As IActiveView
        '        pact = pMap
        '        pact.Refresh()

        '        MsgBox("Processing is Complete")
        '        MARXANForm.Hide()
        '        Exit Sub

        'E1:
        '        MsgBox("Processing Error " & ercode & " " & GP.GetMessages(2))

        '        RemoveLayerorTable("zone_sum.dbf")
        '        RemoveLayerorTable("zone_sum")

        '        penumLay = pMap.Layers
        '        play = penumLay.Next

        '        While Not play Is Nothing
        '            If play.Name = "target_pu_int" Then pMap.DeleteLayer(play)
        '            If play.Name = "targ_dis" Then pMap.DeleteLayer(play)
        '            If play.Name = "dis_target_pu" Then pMap.DeleteLayer(play)
        '            If play.Name = "bounds_f" Then pMap.DeleteLayer(play)
        '            If play.Name = "bound_cov_arc" Then pMap.DeleteLayer(play)
        '            If play.Name = "bound_cov_polygon" Then pMap.DeleteLayer(play)
        '            If play.Name = "abundance" Then pMap.DeleteLayer(play)
        '            If play.Name = "dis_target_pu" Then pMap.DeleteLayer(play)
        '            If play.Name = "target_pu_int" Then pMap.DeleteLayer(play)
        '            If play.Name = "targ_dis" Then pMap.DeleteLayer(play)
        '            If play.Name = "temp_ras" Then pMap.DeleteLayer(play)
        '            If play.Name = "puarea" Then pMap.DeleteLayer(play)
        '            If play.Name = "Status_int" Then pMap.DeleteLayer(play)
        '            If play.Name = "status_ratio" Then pMap.DeleteLayer(play)
        '            play = penumLay.Next
        '        End While
        MsgBox("Processing Complete", MsgBoxStyle.OkOnly, "MARXAN")
        m_application.StatusBar.ProgressBar.Hide()

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

    Private Sub MARXAN_1_FORM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mdoc As IMxDocument
        mdoc = m_application.Document
        Dim mlay As ESRI.ArcGIS.Carto.ILayer
        Dim mlayer As IFeatureLayer
        Dim pclass As IFeatureClass
        layerdic.Clear()

        costbox.Items.Add("Planning Unit Area")
        costbox.Items.Add("Planning Unit Field")
        'statusbox.Items.Add("")

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
                    Me.statusbox.Items.Add(mlay.Name)
                End If
                Try
                    layerdic.Add(mlay.Name, mlay)
                Catch ex As Exception
                    MsgBox("This tool does not support multiple layers with the same name", MsgBoxStyle.Exclamation, "Rename Layer")
                    Me.Close()
                End Try
            ElseIf TypeOf mlay Is IRasterLayer Then
                Me.costbox.Items.Add(mlay.Name)
            End If
        Next i

        'Me.statusbox.SelectedIndex = 0
        Me.pubox.SelectedIndex = 0
        'Me.statusbox.Text = 0
        Me.costbox.SelectedIndex = 0
        Me.pufield.SelectedIndex = 0
        'Me.StatusValueBox.SelectedIndex = 2
        ThresholdBox.Enabled = False
        StatusValueBox.Enabled = False

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Visible = False
        Dim pGxDialog As IGxDialog, pFilterColl As IGxObjectFilterCollection
        pGxDialog = New GxDialog

        pGxDialog.Title = "Choose a Folder"
        pFilterColl = pGxDialog

        Dim pGxFilter As IGxObjectFilter
        pGxFilter = New GxFilterContainers
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
        Dim overwrite As Microsoft.VisualBasic.MsgBoxResult
        If My.Computer.FileSystem.DirectoryExists(outputbox.Text) Then
            If My.Computer.FileSystem.FileExists(outputbox.Text & "\pu.dat") Or My.Computer.FileSystem.FileExists(outputbox.Text & "\bound.dat") Or _
            My.Computer.FileSystem.FileExists(outputbox.Text & "\spec.dat") Or _
            My.Computer.FileSystem.FileExists(outputbox.Text & "\puvspr.dat") Or _
            My.Computer.FileSystem.FileExists(outputbox.Text & "\block.dat") Then
                overwrite = MsgBox("MARXAN output files exist the specified directory." & Microsoft.VisualBasic.ControlChars.CrLf & "If you continue, the pu.dat & bound.dat files will be overwritten, and all other ouptut files will be appended." _
                     & Microsoft.VisualBasic.ControlChars.CrLf & "Do you wish to continue?", MsgBoxStyle.YesNo, "")
                If overwrite = MsgBoxResult.No Then
                    outputbox.Text = ""
                End If
            End If
        Else
            MsgBox("Choose an existing directory", MsgBoxStyle.Exclamation, "")
            outputbox.Text = ""
        End If
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

    Public Function getvaljfields(ByVal inLayer As IFeatureLayer) As List(Of String)
        Dim that As New List(Of String)
        Dim pfclass As IFeatureClass
        Dim pfld As IField
        Dim i As Integer

        pfclass = inLayer.FeatureClass
        For i = 0 To pfclass.Fields.FieldCount - 1
            pfld = pfclass.Fields.Field(i)
            If pfld.Type = esriFieldType.esriFieldTypeDouble Or pfld.Type = esriFieldType.esriFieldTypeSmallInteger _
                Or pfld.Type = esriFieldType.esriFieldTypeString Or pfld.Type = esriFieldType.esriFieldTypeInteger Or pfld.Type = esriFieldType.esriFieldTypeSingle Then
                that.Add(pfld.Name)
            End If
        Next
        Return that

    End Function

    Private Sub useLayer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles useLayer.CheckedChanged ', pufield.SelectedIndexChanged
        statusbox.Items.Clear()
        StatusValueBox.Items.Clear()
        statusbox.Items.Add("")

        Dim j As Integer
        If useLayer.Checked = True Then

            'Me.StatusValueBox.SelectedIndex = 2
            For j = 0 To pubox.Items.Count - 1
                statusbox.Items.Add(pubox.Items(j))
            Next
            'statusbox.SelectedIndex = 0
            'For i = 0 To pufield.Items.Count - 1
            ' StatusValueBox.Items.Add(pufield.Items(i))
            ' Next

            'ThresholdBox.Enabled = True
            'StatusValueBox.Enabled = True

        Else
            For j = 0 To pufield.Items.Count - 1
                statusbox.Items.Add(pufield.Items(j))
            Next
            ThresholdBox.Enabled = False
            StatusValueBox.Enabled = False
        End If
        'Try
        ' Me.statusbox.SelectedIndex = 0
        'Catch
        'useLayer.Checked = True
        'MsgBox("Error")
        'End Try
    End Sub

    Private Sub costbox_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles costbox.Leave


        If Me.costbox.SelectedIndex = -1 Then
            If Not IsNumeric(costbox.Text) Then
                MsgBox("Cost Value must be Numeric")
                Me.costbox.SelectedIndex = 0
            End If
        End If

    End Sub


    Private Sub costbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles costbox.TextChanged



    End Sub

    Private Sub costbox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles costbox.SelectedIndexChanged

        If Me.costbox.SelectedIndex = 0 Then
            sumbut.Enabled = False
            meanbut.Enabled = False
            maxbut.Enabled = False
            costfield.Enabled = False
        End If
        If Me.costbox.SelectedIndex = 1 Then
            costfield.Items.Clear()
            sumbut.Enabled = False
            meanbut.Enabled = False
            maxbut.Enabled = False
            costfield.Enabled = True
            Dim i As Integer
            For i = 0 To pufield.Items.Count - 1
                costfield.Items.Add(pufield.Items(i))
            Next
            Try
                Me.costfield.SelectedIndex = 0
            Catch
                costbox.SelectedIndex = 0
                'MsgBox("Error")
            End Try
        End If
        If Me.costbox.SelectedIndex = -1 Then
            If Not IsNumeric(costbox.Text) Then
                MsgBox("Cost Value must be Numeric")
                Me.costbox.SelectedIndex = 0
                sumbut.Enabled = False
                meanbut.Enabled = False
                maxbut.Enabled = False
                costfield.Enabled = False
            End If
        End If

        If Me.costbox.SelectedIndex > 1 Then
            sumbut.Enabled = True
            meanbut.Enabled = True
            maxbut.Enabled = True
            costfield.Enabled = False
        End If

    End Sub


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
            MsgBox(("Faild while excecuting " & process.ToolName & " "))
            Return Nothing
        End Try

    End Function

    Private Sub pufield_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pufield.SelectedIndexChanged
        useLayer.Checked = True
        costbox.SelectedIndex = 0
    End Sub

    Private Sub writeABfile(ByVal gp As ESRI.ArcGIS.Geoprocessor.Geoprocessor, ByRef tout As Object, ByVal instat As String, ByVal IDFname As String, ByVal specField As String, ByVal puidField As String, ByRef inPUDIC As List(Of preFile))

        Dim pcur As IFeatureCursor
        Dim pfeat As IRow
        Dim species, pu, Amount As String

        Dim pfc As IFeatureClass
        pfc = gp.Open(tout)

        Dim MyTable As ESRI.ArcGIS.Geodatabase.ITable = CType(pfc, ESRI.ArcGIS.Geodatabase.ITable)

        Dim MyTableSort As New ESRI.ArcGIS.Geodatabase.TableSort
        MyTableSort.Fields = puidField + ", " + specField
        MyTableSort.Ascending(puidField) = True
        MyTableSort.Table = MyTable
        'sort the table
        MyTableSort.Sort(Nothing)


        Dim MyCursor As ESRI.ArcGIS.Geodatabase.ICursor = MyTableSort.Rows
        pfeat = MyCursor.NextRow

        'Dim am As String
        Do Until pfeat Is Nothing

            species = CStr(pfeat.Value(pfeat.Fields.FindField("Combine")))


            If instat = "!SHAPE.AREA!" Then
                If ACF.Text = "" Or ACF.Text = "0" Then
                    Amount = Format((pfeat.Value(pfc.Fields.FindField("F_AREA"))), "0.0000")
                Else
                    Amount = Format((pfeat.Value(pfc.Fields.FindField("F_AREA")) / CDbl(ACF.Text)), "0.0000")
                    'MsgBox Amount
                End If
            End If

            If instat = "!SHAPE.LENGTH!" Then
                If LCF.Text = "" Or LCF.Text = "0" Then
                    Amount = Format((pfeat.Value(pfc.Fields.FindField("F_AREA"))), "0.0000")
                Else
                    Amount = Format((pfeat.Value(pfc.Fields.FindField("F_AREA")) / CDbl(LCF.Text)), "0.0000")
                End If
            End If

            If instat = "!SHAPE.PARTCOUNT!" Then
                Amount = Format((pfeat.Value(pfc.Fields.FindField("F_AREA"))), "#")
            End If

            '
            Dim nextl As New preFile
            nextl.zIndex = CLng(CLng(pfeat.Value(pfc.Fields.FindField(puidField))))

            nextl.line = species + "," + Amount

            inPUDIC.Add(nextl)


            pfeat = MyCursor.NextRow
        Loop

    End Sub



    Private Sub writeboundfile(ByVal pfc As IFeatureClass)

        Dim MyTable As ESRI.ArcGIS.Geodatabase.ITable = CType(pfc, ESRI.ArcGIS.Geodatabase.ITable)

        'create table sort 
        Dim MyTableSort As New ESRI.ArcGIS.Geodatabase.TableSort
        MyTableSort.Fields = "MIN_LEFTID, MIN_RIGHTI"
        MyTableSort.Ascending("MIN_LEFTID") = True
        MyTableSort.Table = MyTable
        'sort the table
        MyTableSort.Sort(Nothing)

        'output the returned results of the sort
        Dim pcur As ESRI.ArcGIS.Geodatabase.ICursor = MyTableSort.Rows
        Dim pfeat As ESRI.ArcGIS.Geodatabase.IRow = pcur.NextRow

        Dim ids, boundary, outWrite As String
        pcur = pfc.Search(Nothing, False)

        If My.Computer.FileSystem.FileExists(outputbox.Text & "\bound.dat") Then
            My.Computer.FileSystem.DeleteFile(outputbox.Text & "\bound.dat")
        End If

        Dim a As Awriter = New Awriter(outputbox.Text & "\bound.dat")

        a.WriteLine("id1,id2,boundary")

        Do Until pfeat Is Nothing
            ids = CStr(pfeat.Value(pfeat.Fields.FindField("Combine")))
            boundary = Format((pfeat.Value(pfeat.Fields.FindField("SUM_LEN")) / CDbl(LCF.Text)), "0.00")
            outWrite = ids & "," & boundary
            If Not (outWrite.Contains(",,")) Then
                a.WriteLine(outWrite)
            End If
            pfeat = pcur.NextRow
        Loop

        a.Close()

    End Sub

    Private Sub writePufile(ByVal pfc As IFeatureLayer)

        Dim pcur As IFeatureCursor
        Dim pfeat As IFeature
        Dim idin, cost, Status, xin, yin As String
        pcur = pfc.Search(Nothing, False)
        Dim cent As String()

        Dim MYDIC As New Dictionary(Of Long, String)

        pfeat = pcur.NextFeature

        If My.Computer.FileSystem.FileExists(outputbox.Text & "\pu.dat") Then
            My.Computer.FileSystem.DeleteFile(outputbox.Text & "\pu.dat")
        End If

        'system.Text.Encoding.Convert(system.Text.Encoding.UTF8,system.Text.Encoding.ASCII,

        Dim a As Awriter = New Awriter(outputbox.Text & "\pu.dat")
        'a.Encoding = System.Text.Encoding.ASCII
        'Encoding = System.Text.Encoding.ASCII
        'a = My.Computer.FileSystem.OpenTextFileWriter(outputbox.Text & "\pu.dat", True)
        'My.Computer.FileSystem.

        'dim a as new

        If includeXYcheck.Checked = True Then
            a.WriteLine("id,cost,status,xloc,yloc")
        Else
            a.WriteLine("id,cost,status")
        End If

        Dim olist As New List(Of String)
        Dim min As Long
        Dim max As Long
        min = 999999999999999999
        max = -999999999999999999
        Dim testnum As Long

        Do Until pfeat Is Nothing
            Try
                idin = CStr(pfeat.Value(pfeat.Fields.FindField(pufield.Text)))
                testnum = CLng(idin)
                If testnum < min Then
                    min = testnum
                End If
                If testnum > max Then
                    max = testnum
                End If
                cost = CStr(pfeat.Value(pfeat.Fields.FindField("STAT")))
                If useField.Checked = True Then
                    Status = CStr(pfeat.Value(pfeat.Fields.FindField(statusbox.Text)))
                Else
                    Status = CStr(pfeat.Value(pfeat.Fields.FindField("STATUS_CAL")))
                End If

                Debug.Print(idin)
                If (idin <> "0") Then
                    If includeXYcheck.Checked = True Then
                        cent = CStr(pfeat.Value(pfeat.Fields.FindField("CENTROIDXY"))).Split(" ")
                        xin = cent(0) / LCF.Text
                        yin = cent(1) / LCF.Text
                        'a.WriteLine(idin & "," & cost & "," & Status & "," & xin & "," & yin)
                        olist.Add(idin & "," & cost & "," & Status & "," & xin & "," & yin)
                        MYDIC.Add(CLng(idin), idin & "," & cost & "," & Status & "," & xin & "," & yin)
                    Else
                        'a.WriteLine(idin & "," & cost & "," & Status)
                        olist.Add(idin & "," & cost & "," & Status)
                        MYDIC.Add(CLng(idin), idin & "," & cost & "," & Status)
                    End If

                End If

            Catch ex As Exception
                Debug.Print("Liney no Printy")
            End Try

            pfeat = pcur.NextFeature
        Loop
        'olist.Sort()

        Dim keys As ICollection = MYDIC.Keys
        Dim keysArray(MYDIC.Count - 1) As Long
        keys.CopyTo(keysArray, 0)
        System.Array.Sort(keysArray)
        For Each key As String In keysArray
            a.WriteLine(MYDIC(key))
            Console.WriteLine(MYDIC(key))
        Next

        a.Close()

    End Sub



    Private Sub statusbox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles statusbox.SelectedIndexChanged
        If useLayer.Checked = True Then
            Dim mdoc As IMxDocument
            mdoc = m_application.Document
            Dim mlay As ESRI.ArcGIS.Carto.ILayer
            'Dim mlayer As IFeatureLayer
            'Dim pclass As IFeatureClass
            Dim flist As List(Of String)

            StatusValueBox.Items.Clear()
            StatusValueBox.Items.Add("Status 0")
            StatusValueBox.Items.Add("Status 1")
            StatusValueBox.Items.Add("Status 2")
            StatusValueBox.Items.Add("Status 3")
            StatusValueBox.SelectedIndex = 2

            If statusbox.SelectedIndex = 0 Then
                ThresholdBox.Enabled = False
                StatusValueBox.Enabled = False
                Exit Sub
            End If

            Dim i As Long
            For i = 0 To mdoc.ActiveView.FocusMap.LayerCount - 1
                mlay = mdoc.ActiveView.FocusMap.Layer(i)
                If mlay.Name = statusbox.Text Then
                    'pufield.Items.Clear()
                    flist = getvaljfields(mlay)
                    'If flist.Count = 0 Then
                    'MsgBox("Layer " & statusbox.Text & " does not have any valid fields, ID field must be of type integer or string.", MsgBoxStyle.Exclamation, "Field Type Error")
                    'OK_Button.Enabled = False
                    'Exit Sub
                    'End If
                    type_defs.addtocbox(flist, StatusValueBox)
                    'pufield.SelectedIndex = 0
                    'OK_Button.Enabled = True

                    ThresholdBox.Enabled = True
                    StatusValueBox.Enabled = True

                    Exit Sub
                End If
            Next i
            'Me.StatusValueBox.SelectedIndex = 2
        End If
    End Sub


    Private Sub CreateBlock_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateBlock.CheckedChanged
        If CreateBlock.Checked = True Then
            newspec.Checked = False
        End If
    End Sub

    Private Sub newspec_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles newspec.CheckedChanged
        If newspec.Checked = True Then
            CreateBlock.Checked = False
        End If
    End Sub
End Class

Public Class preFile
    Public Property zIndex As Long
    Public Property line As String
End Class