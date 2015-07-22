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
Imports ESRI.ArcGIS.SpatialAnalystTools
Imports ESRI.ArcGIS.SpatialStatisticsTools
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.AnalysisTools
'Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Geometry
'Imports System
Imports ESRI.ArcGIS.Display
Imports system.IO
Imports System.Reflection

Public Class ProcessUpdate

    Public mp_app As IApplication

    <DllImport("user32.dll")> _
    Private Shared Function PostMessage(ByVal wnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
    End Function

    Private Const WM_VSCROLL As UInteger = &H115
    Private Const SB_BOTTOM As UInteger = 7


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ProcessUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OK_Button.Enabled = False
        detailbox.Text = ""
        pb.Minimum = 0
    End Sub

    Public Sub New(ByVal m_app As IApplication, ByVal processtype As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        If processtype = "ERS" Then
            Me.Text = "Processing ERS"
        End If
        If processtype = "RBI" Then
            Me.Text = "Processing RBI"
        End If
        mp_app = m_app
    End Sub

    Public Function ProcessRBI(ByRef layersinfo As RBI_REC) As Boolean

            Dim timestamp As String
            Dim tnow As DateTime

            tnow = Now
            timestamp = Format(Now, "yyyymmddHHMMss")

            mp_app.StatusBar.ProgressBar.Show()
            mp_app.StatusBar.ProgressBar.Position = 0

            Dim tout, sr As Object
            Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
            Dim gpu As IGPUtilities = New ESRI.ArcGIS.Geoprocessing.GPUtilities
            GP.OverwriteOutput = True

            'tde = layersinfo.ERS_LAYERS(i).pgpLayer.DataElement
            'addtext(tde.Extent)

            'layersinfo.outpath
            GP.AddOutputsToMap = False

            Dim srs As CreateSpatialReference = New CreateSpatialReference()
            tout = Nothing


            'Try
            srs.spatial_reference_template = layersinfo.pulayer
            tout = ExTool(GP, srs, Nothing)
            sr = tout
            'Catch ex As Exception
            ''cleanup(GP, "RBI_*")
            'MsgBox("Unable to copy spatial reference from Planning Unit Layer", MsgBoxStyle.Critical, "RBI")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

            'Dim bob As IDataElement
            'bob = GP.GetDataElement(layersinfo.outpath, "")
            'Dim rom As IDEWorkspace
            'rom = bob
            Dim ending, pending As String
            Dim openc, closec As String
            openc = Chr(34)
            closec = Chr(34)
            ending = ""
            pending = ""
            'bob = Nothing

            Dim patscratch As String = ""

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
                Exit Function
            End If

            'If rom.WorkspaceType = esriWorkspaceType.esriFileSystemWorkspace Then

            Directory.CreateDirectory(patscratch & "\RBI_" & timestamp)


            ending = ".shp"
            pending = ".dbf"
            'Dim cnf As CreateFolder = New CreateFolder(layersinfo.outpath, layersinfo.outname)
            ''Try
            'tout = ExTool(GP, cnf, Nothing)
            ''Catch ex As Exception
            ''MsgBox("Unable to create new folder in " & layersinfo.outpath, MsgBoxStyle.Critical, "RBI")
            ''Debug.Print(ex.Message)
            ''Exit Function
            ''End Try
            'End If

            '###############################33
            'tout = layersinfo.outpath
            '3###############################

            '8888If rom.WorkspaceType = esriWorkspaceType.esriLocalDatabaseWorkspace Then
            'openc = "["
            'closec = "]"
            '8888Dim cnf As CreateFeatureDataset = New CreateFeatureDataset(layersinfo.outpath, layersinfo.outname)
            '8888cnf.spatial_reference = tout
            'Try
            '8888tout = ExTool(GP, cnf, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to create new feature dataset in " & layersinfo.outpath, MsgBoxStyle.Critical, "RBI")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try
            '8888End If

            'If rom.WorkspaceType = esriWorkspaceType.esriRemoteDatabaseWorkspace Then
            '    ending = ""
            '    Dim cnf As CreateFeatureDataset = New CreateFeatureDataset(layersinfo.outpath, layersinfo.outname)
            '    cnf.spatial_reference = tout
            '    'Try
            '    tout = ExTool(GP, cnf, Nothing)
            '    'Catch ex As Exception
            '    'MsgBox("Unable to create new feature dataset in " & layersinfo.outpath, MsgBoxStyle.Critical, "RBI")
            '    'Debug.Print(ex.Message)
            '    'Exit Function
            '    'End Try
            'End If

        ''''''''''''''''''GP.SetEnvironmentValue("Workspace", patscratch & "\RBI_" & timestamp)

        Dim psloc As String = patscratch & "\RBI_" & timestamp & "\"

        'Dim timenow As DateTime
            'timenow = Now
            'TimeString = Format(timenow, "YYYYmmddhhmmss")

            'GP.workspace = outputbox.Text & "\" & Outputname.Text
            'Dim dissolvelayer, int1layer As String
            'rai1layer = outp & TimeString & "_rai1" & ".shp"
            'rai2layer = outp & TimeString & "_rai2" & ".shp"
            'rai3layer = outp & TimeString & "_RAI" & ".shp"
        ' MsgBox(patscratch)
        ' MsgBox(psloc)

            'GP.Toolbox = "management"
        Dim cala As CalculateAreas = New CalculateAreas(layersinfo.extentlayer, psloc + "RBI_Extent_1" + ending)
            'Try
            tout = ExTool(GP, cala, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to create new layer from calculate area operation.", MsgBoxStyle.Critical, "RBI")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

            Dim addf As AddField = New AddField(tout, "TAREA", "DOUBLE")
            'Try
            ExTool(GP, addf, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "RBI")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

            addf.field_name = "RAI"
            'Try
            ExTool(GP, addf, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "RBI")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

            Dim calf As CalculateField = New CalculateField(tout, "TAREA", "[F_AREA]")
            'Try
            ExTool(GP, calf, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to calculate field.", MsgBoxStyle.Critical, "RBI")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

        Dim inter As Intersect = New Intersect(layersinfo.pulayer.NameString & ";" & tout, psloc + "RBI_Intersect_1" & ending)
            inter.join_attributes = "ALL"
            inter.output_type = "INPUT"
            'Try
            tout = ExTool(GP, inter, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to execute initial intersect.", MsgBoxStyle.Critical, "RBI")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

            cala.Input_Feature_Class = tout
        cala.Output_Feature_Class = psloc + "RBI_Area_1" + ending
            'Try
            tout = ExTool(GP, cala, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to create new layer from calculate area operation.", MsgBoxStyle.Critical, "RBI")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

            calf.in_table = tout
            calf.field = "RAI"
            'calf.expression = "[TAREA]/[F_AREA]"
            calf.expression = "[F_AREA]/[TAREA]"
            'Try
            ExTool(GP, calf, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to calculate field.", MsgBoxStyle.Critical, "RBI")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

            Dim disl As Dissolve = New Dissolve

            Dim Ln As Integer
            For Ln = 0 To layersinfo.TARGET_LAYERS.Length - 1

                mp_app.StatusBar.Message(0) = "Processing Input: " & CStr(Ln + 1) & " of " & CInt(layersinfo.TARGET_LAYERS.Length)
                Debug.Print((Ln / (layersinfo.TARGET_LAYERS.Length)) * 100)
                mp_app.StatusBar.ProgressBar.Position = CInt((Ln / (layersinfo.TARGET_LAYERS.Length)) * 100)
                '            'timenow = Now
                '            'Timestring = Format(timenow, "YYYYmmddhhmmss")
                '            targetLayer = TH(Ln).Target_NAME  'RBIForm.targetbox.Text
                '            dissolvelayer = outp & TimeString & "dis" & targetLayer & ".shp"
                '            int1layer = outp & TimeString & "puint" & targetLayer & ".shp"
                '            dbasefile = outp & TimeString & "dbase" & targetLayer & ".dbf"
                '            RBITlayer = outp & TimeString & "RBIT" & targetLayer & ".shp"

                '            '''On Error GoTo er:
                '            GP.Toolbox = "management"

            disl.in_features = layersinfo.TARGET_LAYERS(Ln).TARGET_LAYER
            disl.out_feature_class = psloc + "RBI_DIS_T_" & CStr(Ln) + ending
                disl.dissolve_field = layersinfo.TARGET_LAYERS(Ln).fieldname
                'Try
                tout = ExTool(GP, disl, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to dissolve " & layersinfo.TARGET_LAYERS(Ln).TARGET_LAYER.NameString, MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

                addf.in_table = tout
                '            GP.Dissolve(targetLayer, dissolvelayer, TH(Ln).Target_FNAME) 'outp & dissolvelayer

                addf.field_name = "AMOUNT_ALL"
                'Try
                ExTool(GP, addf, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

                '            GP.AddField(dissolvelayer, "AMOUNT_ALL", "DOUBLE")

                addf.field_name = "AMOUNT_INT"
                'Try
                ExTool(GP, addf, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

                '            GP.AddField(dissolvelayer, "AMOUNT_INT", "DOUBLE")

                addf.field_name = "TCOUNT"
                'Try
                ExTool(GP, addf, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

                addf.field_name = "RBI"
                'Try
                ExTool(GP, addf, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

                addf.field_name = "RBN"
                'Try
            ExTool(GP, addf, Nothing)

            addf.field_name = "HNUM"
            'Try
            ExTool(GP, addf, Nothing)

            addf.field_name = "HDEM"
            'Try
            ExTool(GP, addf, Nothing)


            addf.field_name = "HPRIME"
            'Try
            ExTool(GP, addf, Nothing)

            addf.field_name = "RCOUNT"
            'Try
            ExTool(GP, addf, Nothing)

                'Catch ex As Exception
                'MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

                addf.field_name = "RBI_TARGET"
                addf.field_type = "TEXT"
                'Try
                ExTool(GP, addf, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try
                addf.field_type = "DOUBLE"

                '            GP.AddField(dissolvelayer, "TCOUNT", "LONG")
                '            GP.AddField(dissolvelayer, "RBI", "DOUBLE")
                '            GP.AddField(dissolvelayer, "RBN", "DOUBLE")
                '            GP.AddField(dissolvelayer, "RBI_TARGET", "TEXT")


                '            GP.calculatefield(dissolvelayer, "RBI_TARGET", "[" & TH(Ln).Target_FNAME & "]")

                calf.in_table = tout
                calf.field = "RBI_TARGET"
                calf.expression = "[" & layersinfo.TARGET_LAYERS(Ln).fieldname & "]"
                'Try
                ExTool(GP, calf, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to calculate RBI Target field.", MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

                Dim pfclass As IFeatureClass
                pfclass = GP.Open(tout)

                Dim typ As String
                typ = "NON"
                If pfclass.ShapeType = esriGeometryType.esriGeometryPoint Then typ = "!SHAPE.PARTCOUNT!"
                If pfclass.ShapeType = esriGeometryType.esriGeometryPolyline Then typ = "!SHAPE.LENGTH!"
                If pfclass.ShapeType = esriGeometryType.esriGeometryPolygon Then typ = "!SHAPE.AREA!"
                If pfclass.ShapeType = esriGeometryType.esriGeometryMultipoint Then typ = "!SHAPE.PARTCOUNT!"
                If pfclass.ShapeType = esriGeometryType.esriGeometryLine Then typ = "!SHAPE.LENGTH!"

                calf.in_table = tout
                calf.field = "AMOUNT_ALL"
                calf.expression = typ
                calf.expression_type = "PYTHON"
                'Try
                ExTool(GP, calf, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to calculate Amount_ALL field.", MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

                calf.expression_type = "VB"
                'MsgBox(pfclass.ShapeType)
                '            desc = GP.Describe(dissolvelayer)
                '            dcur = GP.UpdateCursor(dissolvelayer)
                '            drow = dcur.Next

                '            Do Until drow Is Nothing
                '                dgeo = drow.GetValue(desc.ShapeFieldName)
                '                tcounter = tcounter + 1

                '                If dgeo.Type = "polygon" Then
                '                    damount = dgeo.Area
                '                    drow.SetValue("AMOUNT_ALL", damount)
                '                End If

                '                If dgeo.Type = "polyline" Then
                '                    damount = dgeo.Length
                '                    drow.SetValue("AMOUNT_ALL", damount)
                '                End If

                '                If dgeo.Type = "multipoint" Then
                '                    damount = dgeo.PartCount
                '                    drow.SetValue("AMOUNT_ALL", damount)
                '                    pointflag = "true"
                '                    'instert buffer code here!!!
                '                End If

                '                drow.SetValue("TCOUNT", 1)
                '                dcur.UpdateRow(drow)
                '                rcount = rcount + 1
                '                drow = dcur.Next
                '            Loop

                '            'GP.RefreshCatalog outputbox.Text & "\" & Outputname.Text
                '            GP.Toolbox = "analysis"
                '            GP.Intersect_analysis(rai3layer & " ; " & dissolvelayer, int1layer, "ALL", "#", "INPUT")



            inter.in_features = tout & ";" & psloc + "RBI_Area_1" & ending
            inter.out_feature_class = psloc + "puint" & CStr(Ln) & ending
                inter.join_attributes = "ALL"
                inter.output_type = "INPUT"

                'Try
                tout = ExTool(GP, inter, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to execute internal intersect.", MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

                calf.in_table = tout
                calf.field = "AMOUNT_INT"
                calf.expression = typ
                calf.expression_type = "PYTHON"
                'Try
                ExTool(GP, calf, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to calculate Amount_INT field.", MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

                calf.expression_type = "VB"

                calf.in_table = tout
                calf.field = "RBI"
                calf.expression = "([AMOUNT_INT] / [AMOUNT_ALL]) * 100"
                'Try
                ExTool(GP, calf, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to calculate RBI field.", MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

            calf.expression_type = "VB"

            calf.in_table = tout
            calf.field = "HNUM"
            calf.expression = "Sqr([AMOUNT_INT] / [AMOUNT_ALL])"
            'Try
            ExTool(GP, calf, Nothing)


                calf.expression_type = "VB"

                calf.in_table = tout
                calf.field = "RBN"
                calf.expression = "([RBI] / [RAI]) * 100"
                'Try
                ExTool(GP, calf, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to calculate RBN field.", MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

                calf.expression_type = "VB"

                '                dcur.UpdateRow(drow)

                '                drow = dcur.Next
                '            Loop

                '            flist = GP.ListFields(int1layer)
                '            dfield = flist.Next
                Try
                pfclass = GP.Open(psloc + "puint" & CStr(Ln) & ending)
                Catch
                    Debug.Print("Thing")
                    Exit Function
                End Try
                Dim fl As Integer
                Dim delfields As String = ""
                Dim pfld As Field
                delfields = ""
                For fl = 0 To pfclass.Fields.FieldCount - 1
                    '            Do Until dfield Is Nothing
                    pfld = pfclass.Fields.Field(fl)
                    If pfld.Type = esriFieldType.esriFieldTypeOID Or pfld.Type = esriFieldType.esriFieldTypeGeometry Then
                    Else
                    If (pfld.Name = "RBI_TARGET") Or (pfld.Name = "TCOUNT") Or (pfld.Name = "AMOUNT_INT") Or (pfld.Name = "AMOUNT_ALL") Or (pfld.Name = "RAI") Or (pfld.Name = "RBN") Or _
                                (pfld.Name = "RBI") Or (pfld.Name = "HNUM") Or (pfld.Name = "HDEM") Or (pfld.Name = "RCOUNT") Or (pfld.Name = "HPRIME") Or (pfld.Name = layersinfo.pufield) Then
                    Else
                        delfields = delfields & pfld.Name & "; "
                    End If
                    End If

                Next fl


                delfields = Microsoft.VisualBasic.Left(delfields, Len(delfields) - 2)
            Dim delf As New DeleteField(psloc + "puint" & CStr(Ln) & ending, delfields)
                'Try
                ExTool(GP, delf, Nothing)
                'Catch ex As Exception
                'MsgBox("Unable to remove excess fields.", MsgBoxStyle.Critical, "RBI")
                'Debug.Print(ex.Message)
                'Exit Function
                'End Try

                If Ln = 0 Then
                Dim cpyr As New CopyRows(psloc + "puint" & CStr(Ln) & ending, psloc + "PRE_DIS_LIST" & pending)
                    'Try
                    ExTool(GP, cpyr, Nothing)
                    'Catch ex As Exception
                    'MsgBox("Unable to copy table.", MsgBoxStyle.Critical, "RBI")
                    'Debug.Print(ex.Message)
                    'Exit Function
                    'End Try
                Else

                Dim cpyr As New CopyRows(psloc + "puint" & CStr(Ln) & ending, psloc + "PRE_DIS_LIST" & CStr(Ln) & pending)
                    'Try
                    ExTool(GP, cpyr, Nothing)
                    'Catch ex As Exception
                    'MsgBox("Unable to copy table.", MsgBoxStyle.Critical, "RBI")
                    'Debug.Print(ex.Message)
                    'Exit Function
                    'End Try

                Dim apper As New Append(psloc + "PRE_DIS_LIST" & CStr(Ln) & pending, psloc + "PRE_DIS_LIST" & pending)
                    'Try
                    ExTool(GP, apper, Nothing)
                    'Catch ex As Exception
                    'MsgBox("Unable to copy table.", MsgBoxStyle.Critical, "RBI")
                    'Debug.Print(ex.Message)
                    'Exit Function
                    'End Try
                End If



                '            GP.DeleteField(int1layer, delfields)
                '            GP.Copyrows(int1layer, dbasefile)

                '            If (Ln = 0) Then
                '                '''having trouble here
                '                '''GP.CreateTable (RBIForm.outputbox & "\"), RBIForm.Outputname, int1layer, "#"
                '                keepfile = dbasefile
                '            Else
                '                ''GP.Append dbasefile, keepfile
                '                Otherfiles = Otherfiles & dbasefile & ";"
                '            End If

                '            'GP.Toolbox = "management"

                '            'insert merge code here

            Next Ln

            mp_app.StatusBar.Message(0) = "Completing RAI Operation"

            GP.AddOutputsToMap = True

            calf.field = "TCOUNT"
            calf.expression = "1"
        calf.in_table = psloc + "PRE_DIS_LIST" & pending
            'Try
            ExTool(GP, calf, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to calculate field.", MsgBoxStyle.Critical, "TCOUNT")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

        Dim stater As New Statistics '"PRE_DIS_LIST" & CStr(Ln) & pending, "PRE_DIS_LIST" & pending)

            'Catch ex As Exception
            'MsgBox("Unable to create final summary table.", MsgBoxStyle.Critical, "Table gen error")
            'Debug.Print(ex.Message)
            'End Try

            '        GP.Toolbox = "analysis"
            '        GP.Statistics(keepfile, summyfile, "RBI SUM;RBN SUM;TCOUNT SUM", RBIForm.pufield.Text)
            '        GP.Statistics(keepfile, listtable, "TCOUNT SUM", "RBI_TARGET")

        stater.in_table = psloc + "PRE_DIS_LIST" & pending
        stater.out_table = psloc + "SUM_TARGET_LIST" & pending
            stater.statistics_fields = "TCOUNT SUM"
            stater.case_field = "RBI_TARGET"
            'Try
            ExTool(GP, stater, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to create final summary table.", MsgBoxStyle.Critical, "Table gen error")
            'Debug.Print(ex.Message)
            'End Try

            Dim rc As Integer
        Dim getcnt As New GetCount(psloc + "SUM_TARGET_LIST" & pending)
            'Try
        rc = ExTool(GP, getcnt, Nothing)

        Dim npl As Integer
        Dim getcnt2 As New GetCount(layersinfo.pulayer)
        'Try
        npl = ExTool(GP, getcnt2, Nothing)


            'Catch ex As Exception
            'MsgBox("Unable to create table view.", MsgBoxStyle.Critical, "Problem")
            'Debug.Print(ex.Message)
            'End Try

            'Dim cfc as CreateFeatureclass = new CreateFeatureclass("  ","RBI_ALL_OUPUTS"

        '        GP.Maketableview(summyfile, "summytab")

        Dim jf As New JoinField(psloc + "PRE_DIS_LIST" & pending, "RBI_TARGET", psloc + "SUM_TARGET_LIST" & pending, "RBI_TARGET")
        jf.fields = "SUM_TCOUNT"

        ExTool(GP, jf, Nothing)


        calf.expression_type = "VB"

        calf.in_table = psloc + "PRE_DIS_LIST" & pending
        calf.field = "HDEM"
        calf.expression = "Sqr([SUM_TCOUNT] / " & CStr(npl) & ")"
        'Try
        ExTool(GP, calf, Nothing)


        calf.expression_type = "VB"

        calf.in_table = psloc + "PRE_DIS_LIST" & pending
        calf.field = "HPRIME"
        calf.expression = "[HNUM] / [HDEM]"
        'Try
        ExTool(GP, calf, Nothing)

        calf.expression_type = "PYTHON"

        calf.in_table = psloc + "PRE_DIS_LIST" & pending
        calf.field = "RCOUNT"
        calf.expression = "getR(!HPRIME!)"
        calf.code_block = "def getR(H):" + vbCrLf + "    if H >= 1:" + vbCrLf + "        return 1" + vbCrLf + "    else:" + vbCrLf + "        return 0"
            'Try
            ExTool(GP, calf, Nothing)

        calf.expression_type = "VB"
        calf.code_block = ""

            stater.in_table = psloc + "PRE_DIS_LIST" & pending
            stater.out_table = psloc + "SUM_PU_LIST" & pending
        stater.statistics_fields = "RBI SUM;RBN SUM;HPRIME SUM;TCOUNT SUM;RCOUNT SUM"
            stater.case_field = layersinfo.pufield
            'Try
            ExTool(GP, stater, Nothing)

            'Catch ex As Exception
            'MsgBox("Unable to Count Rows", MsgBoxStyle.Critical, "Count Rows")
            'Debug.Print(ex.Message)
            'End Try

            '        finaloutput = RBIForm.outputbox & "\" & RBIForm.Outputname & "\RBI_SUMMARY_" & TimeString & ending

            '        GP.Toolbox = "management"
            'GP.AddOutputsToMap = True
            Dim MTV As New MakeTableView  '"PRE_DIS_LIST" & CStr(Ln) & pending, "PRE_DIS_LIST" & pending)
            MTV.in_table = psloc + "SUM_PU_LIST" & pending
            MTV.out_view = "summytab"
            'Try
            ExTool(GP, MTV, Nothing)


            Dim cpf As New CopyFeatures(layersinfo.pulayer, psloc + layersinfo.outname + "_RBI_SUMMARY" + ending)
            'Try
            ExTool(GP, cpf, Nothing)
            'Catch ex As Exception
            '   MsgBox("Unable to copy orginal input PU Layer.", MsgBoxStyle.Critical, "PU layer copy error")
            '  Debug.Print(ex.Message)
            'End Try

            'Try
            addf.field_type = "DOUBLE"
            addf.field_name = "RBIT1"
            addf.in_table = psloc + layersinfo.outname + "_RBI_SUMMARY" + ending
            ExTool(GP, addf, Nothing)
            addf.field_name = "RBIT2"
            ExTool(GP, addf, Nothing)
            addf.field_name = "RBNT1"
            ExTool(GP, addf, Nothing)
            addf.field_name = "RBNT2"
            ExTool(GP, addf, Nothing)
            addf.field_name = "H_PRIME"
        ExTool(GP, addf, Nothing)
        addf.field_name = "RARITY"
        ExTool(GP, addf, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to create new field " & addf.field_name, MsgBoxStyle.Critical, "RBI")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

            'Try
            calf.field = "RBIT1"
            calf.in_table = psloc + layersinfo.outname + "_RBI_SUMMARY" + ending
            calf.expression = "0"
            ExTool(GP, calf, Nothing)
            calf.field = "RBIT2"
            ExTool(GP, calf, Nothing)
            calf.field = "RBNT1"
            ExTool(GP, calf, Nothing)
            calf.field = "RBNT2"
            ExTool(GP, calf, Nothing)
            calf.field = "H_PRIME"
        ExTool(GP, calf, Nothing)
        calf.field = "RARITY"
        ExTool(GP, calf, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to calculate field", MsgBoxStyle.Critical, "Calculate Error")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

            Dim joiner As New AddJoin
            joiner.in_layer_or_view = layersinfo.outname + "_RBI_SUMMARY"
            joiner.in_field = layersinfo.pufield
            joiner.join_table = "SUM_PU_LIST"
            joiner.join_field = layersinfo.pufield
            joiner.join_type = False
            'Try
            ExTool(GP, joiner, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to Join Tables", MsgBoxStyle.Critical, "Join Error")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

            'Try
            calf.in_table = layersinfo.outname + "_RBI_SUMMARY"
            calf.field = "RBIT1"
            calf.expression = "[SUM_PU_LIST.SUM_RBI]/" & CStr(rc)
            ExTool(GP, calf, Nothing)
            calf.field = "RBIT2"
            calf.expression = "[SUM_PU_LIST.SUM_RBI]/([SUM_PU_LIST.SUM_TCOUNT])"
            ExTool(GP, calf, Nothing)
            calf.field = "RBNT1"
            calf.expression = "[SUM_PU_LIST.SUM_RBN]/" & CStr(rc)
            ExTool(GP, calf, Nothing)
            calf.field = "RBNT2"
            calf.expression = "[SUM_PU_LIST.SUM_RBN]/([SUM_PU_LIST.SUM_TCOUNT])"
            ExTool(GP, calf, Nothing)
            calf.field = "H_PRIME"
            calf.expression = "[SUM_PU_LIST.SUM_HPRIME]"
        ExTool(GP, calf, Nothing)
        calf.field = "RARITY"
        calf.expression = "[SUM_PU_LIST.SUM_RCOUNT]"
        ExTool(GP, calf, Nothing)

            'calf.field = "H_PRIME"
            'calf.expression = "[SUM_PU_LIST.SUM_HNUM]/([SUM_PU_LIST.SUM_TCOUNT])"
            'ExTool(GP, calf, Nothing)

            'Catch ex As Exception
            'MsgBox("Unable to calculate field", MsgBoxStyle.Critical, "Calculate Error")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try


            Dim remjoin As New RemoveJoin
            remjoin.in_layer_or_view = layersinfo.outname + "_RBI_SUMMARY"
            remjoin.join_name = "SUM_PU_LIST"
            'Try
            ExTool(GP, remjoin, Nothing)
            'Catch ex As Exception
            'MsgBox("Unable to Remove Join", MsgBoxStyle.Critical, "Remove Join Error")
            'Debug.Print(ex.Message)
            'Exit Function
            'End Try

            Dim pmxmap As IMxDocument
            Dim ptabcol As ITableCollection
            Dim pmap As IMap
            pmxmap = mp_app.Document
            pmap = pmxmap.ActiveView
            ptabcol = pmap
            ptabcol.RemoveAllTables()

            ' ''Dim MTV As New MakeTableView  '"PRE_DIS_LIST" & CStr(Ln) & pending, "PRE_DIS_LIST" & pending)
            ''MTV.in_table = "SUM_PU_LIST" & pending
            ''MTV.out_view = "SUM_PU_LIST"
            ' ''Try
            ''ExTool(GP, MTV, Nothing)
            ' ''Catch ex As Exception
            ' ''MsgBox("Unable to create table view.", MsgBoxStyle.Critical, "Problem")
            ' ''Debug.Print(ex.Message)
            ' ''End Try

            ''MTV.in_table = "SUM_TARGET_LIST" & pending
            ''MTV.out_view = "SUM_TARGET_LIST"
            ' ''Try
            ''ExTool(GP, MTV, Nothing)
            ' ''Catch ex As Exception
            ' ''MsgBox("Unable to create table view.", MsgBoxStyle.Critical, "Problem")
            ' ''Debug.Print(ex.Message)
            ' ''End Try


            Dim cfc As CreateFeatureclass = New CreateFeatureclass(layersinfo.outpath, layersinfo.outname + "_RBI_ALL_TARGETS")
            cfc.spatial_reference = sr
            cfc.geometry_type = "POLYGON"
            cfc.template = psloc + "puint0" & ending
            tout = ExTool(GP, cfc, Nothing)

            'MsgBox(tout)
            'MsgBox(layersinfo.outpath)

            Dim pufeats As IFeatureClass
            Dim targetfeats As IFeatureClass
            Dim targetrecs As ITable

            pufeats = GP.Open(patscratch & "\RBI_" & timestamp & "\" + layersinfo.outname + "_RBI_SUMMARY.shp")
            targetfeats = GP.Open(tout)
            targetrecs = GP.Open(patscratch & "\RBI_" & timestamp & "\PRE_DIS_LIST.dbf")

            'Debug.Print(pufeats.FeatureCount(Nothing))
            'Debug.Print(targetfeats.FeatureCount(Nothing))
            'Debug.Print(targetrecs.RowCount(Nothing))

            Dim recc, frec As Integer
            Dim allfields As IFields
            Dim pfeatin As IFeature
            Dim prowin As IRow
            Dim rowcur As ICursor
            Dim fldinx As Integer
            allfields = targetrecs.Fields
            Dim pqf As IQueryFilter = New QueryFilter
            Dim pfeatcur As IFeatureCursor


            rowcur = targetrecs.Search(Nothing, False)
            prowin = rowcur.NextRow

            mp_app.StatusBar.ProgressBar.Position = 0
            mp_app.StatusBar.Message(0) = "Creating RBI Detail Table"

            Dim cc As Integer
            While Not prowin Is Nothing

                pfeatin = targetfeats.CreateFeature()

                For frec = 0 To allfields.FieldCount - 1
                    fldinx = targetfeats.FindField(allfields.Field(frec).Name)
                    If fldinx > 1 Then
                        pfeatin.Value(fldinx) = prowin.Value(frec)
                    End If
                Next frec

                Debug.Print(Chr(34) & layersinfo.pufield & Chr(34) & " = " & CStr(prowin.Value(prowin.Fields.FindField(layersinfo.pufield))))
                pqf.WhereClause = Chr(34) & layersinfo.pufield & Chr(34) & " = " & CStr(prowin.Value(prowin.Fields.FindField(layersinfo.pufield)))
                pfeatcur = pufeats.Search(pqf, False)
                pfeatin.Shape = pfeatcur.NextFeature.Shape

                pfeatin.Store()
                cc = cc + 1
                mp_app.StatusBar.ProgressBar.Position = CInt((cc / targetrecs.RowCount(Nothing)) * 100)
                prowin = rowcur.NextRow

            End While
            Dim play As IFeatureLayer
            play = GP.Open(layersinfo.outname + "_RBI_SUMMARY")
            pmxmap.ActiveView.FocusMap.DeleteLayer(play)

            Dim cpyf As CopyFeatures = New CopyFeatures(patscratch & "\RBI_" & timestamp & "\" + layersinfo.outname + "_RBI_SUMMARY.shp", layersinfo.outpath + "\" + layersinfo.outname + "_RBI_SUMMARY")
            tout = ExTool(GP, cpyf, Nothing)

            mp_app.StatusBar.ProgressBar.Position = 100
            mp_app.StatusBar.Message(0) = "RAI Complete"
            mp_app.StatusBar.ProgressBar.Hide()

            Dim ienumlay As IEnumLayer
            ienumlay = pmxmap.ActiveView.FocusMap.Layers
            Dim pplay As ILayer
            pplay = ienumlay.Next()

            While Not pplay Is Nothing
                If pplay.Name = layersinfo.outname + "_RBI_SUMMARY" Then
                    play = pplay
                    Exit While
                End If
                pplay = ienumlay.Next()
            End While

        QuantileClassBreaks(play, "RBNT1", 5, mp_app)

            MsgBox("RBI Processing Complete", MsgBoxStyle.OkOnly, "RBI")

            Return True

            '  Catch ex As Exception
            ' MsgBox("RBI Processing Error", MsgBoxStyle.Exclamation, "RBI")
            ' Exit Function
            ' End Try


    End Function

    Public Function ProcessERS(ByRef layersinfo As ERS_REC) As Boolean
        Me.Select()
        addtext(type_defs.erstoptext)
        addtext("Processing Each of " & layersinfo.ERS_LAYERS.Length & " Input Layers...")
        Dim i As Long
        Dim bigi, oidi As Long
        Dim tout As Object
        Dim tfc As IFeatureClass
        Dim tlayer As ILayer
        Dim tgplayer As IGPFeatureLayer
        Dim oidname As String
        Dim pfcur As IFeatureCursor
        Dim pfeat As IFeature
        Dim tinfldis, tweight, tintensity As String
        Dim sufx As String
        Dim term2 As Double
        Dim rater As Double

        mp_app.StatusBar.ProgressBar.Position = 0
        mp_app.StatusBar.ProgressBar.Show()
        mp_app.StatusBar.Message(0) = "Starting ERS Process..."

        Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
        Dim gpu As IGPUtilities = New ESRI.ArcGIS.Geoprocessing.GPUtilities

        Dim timestamp As String
        Dim tnow As DateTime

        tnow = Now
        timestamp = Format(Now, "yyyymmddHHMMss")

        'tde = layersinfo.ERS_LAYERS(i).pgpLayer.DataElement
        'addtext(tde.Extent)

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
            Exit Function
        End If


        'layersinfo.outpath
        GP.OverwriteOutput = True
        GP.AddOutputsToMap = False
        GP.SetEnvironmentValue("workspace", patscratch)
        GP.SetEnvironmentValue("cellsize", layersinfo.cellsize)
        GP.SetEnvironmentValue("extent", CStr(layersinfo.extent.XMin) + " " + CStr(layersinfo.extent.YMin) + " " + CStr(layersinfo.extent.XMax) + " " + CStr(layersinfo.extent.YMax))

        cleanup(GP, "ERS_*")
        addtext("Setting Environment Variables " + CStr(layersinfo.extent.XMin) + " " + CStr(layersinfo.extent.YMin) + " " + CStr(layersinfo.extent.XMax) + " " + CStr(layersinfo.extent.YMax))

        'GP.SetEnvironmentValue("extent", "
        'gpu.Workspace = layersinfo.outpath

        bigi = layersinfo.ERS_LAYERS.Length - 1
        Dim cnt As Long
        cnt = 0

        mp_app.StatusBar.ProgressBar.Show()
        'mp_app.StatusBar.ProgressBar.MinRange = i
        'mp_app.StatusBar.ProgressBar.MaxRange = bigi
        'mp_app.StatusBar.ProgressBar.Position  = 1
        Dim bigcser As CellStatistics = New CellStatistics

        ''Dim bob As IDataElement
        ''bob = GP.GetDataElement(layersinfo.outpath, "")
        ''Dim rom As IDEWorkspace
        ''rom = bob
        Dim ending As String
        Dim openc, closec As String
        openc = Chr(34)
        closec = Chr(34)
        ''ending = ""
        ''If rom.WorkspaceType = esriWorkspaceType.esriFileSystemWorkspace Then
        ending = ".shp"
        ''End If
        ''If rom.WorkspaceType = esriWorkspaceType.esriLocalDatabaseWorkspace Then
        ''    openc = "["
        ''    closec = "]"
        ''End If
        ' ''If rom.WorkspaceType = esriWorkspaceType.esriRemoteDatabaseWorkspace Then
        ' '' ending = ""
        ' ''End If

        ' ''gpu.RefreshView()
        ' ''gpu.ReleaseInternals()

        For i = 0 To bigi
            mp_app.StatusBar.ProgressBar.Show()
            addtext("Processing Layer " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString & " (Layer " & CStr(i) & " of " & CStr(bigi + 1) & ")...")
            mp_app.StatusBar.Message(0) = ("Processing Layer " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString & " (Layer " & CStr(i) & " of " & CStr(bigi + 1) & ")...")

            If layersinfo.ERS_LAYERS(i).layertype = ERS_REC.laytype.feature Then

                Dim cp As CopyFeatures = New CopyFeatures(layersinfo.ERS_LAYERS(i).pgpLayer, "ERS_DIS_CPY_" + CStr(i) + timestamp + ending)
                'Debug.Print(layersinfo.ERS_LAYERS(i).pgpLayer.NameString + " ,  " + layersinfo.outpath + "\" + "ERS_DIS_CPY_" + CStr(i) + timestamp + ending)
                Try
                    tout = ExTool(GP, cp, Nothing)
                Catch ex As Exception
                    MsgBox("Unable to copy features " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString, MsgBoxStyle.Critical, "ERS")
                    Debug.Print(ex.Message)
                    Exit Function
                End Try

                Dim dislay As Dissolve = New Dissolve
                dislay.in_features = tout 'layersinfo.ERS_LAYERS(i).pgpLayer 'layersinfo.outpath + "\" + "ERS_DIS_CPY_" + CStr(i) + timestamp + ending ' layersinfo.ERS_LAYERS(i).pgpLayer
                dislay.out_feature_class = "ERS_DIS_" + CStr(i) + timestamp + ending
                dislay.dissolve_field = layersinfo.ERS_LAYERS(i).infdis & ";" & layersinfo.ERS_LAYERS(i).intensity & ";" & layersinfo.ERS_LAYERS(i).weight
                dislay.multi_part = True
                'dislay.statistics_fields = "#"
                tout = ExTool(GP, dislay, Nothing)

                'MsgBox(tout)
                'tgplayer = gpu.MakeGPLayer("ERS_DIS_" + CStr(i) + ".shp", Nothing)
                'MsgBox(tgplayer.name)

                Try
                    tfc = GP.Open(tout)
                Catch ex As Exception
                    'cleanup(GP, "ERS_*")
                    MsgBox("Unable to complete dissolve on " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString, MsgBoxStyle.Critical, "ERS")
                    Debug.Print(ex.Message)
                    Exit Function
                End Try

                mp_app.StatusBar.Message(0) = ("Processing Layer " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString & " (Layer " & CStr(i) & " of " & CStr(bigi + 1) & ")...")

                oidname = tfc.OIDFieldName
                oidi = tfc.FindField(oidname)
                pfcur = tfc.Search(Nothing, True)
                pfeat = pfcur.NextFeature


                Dim cser As CellStatistics = New CellStatistics

                Dim makelay As MakeFeatureLayer = New MakeFeatureLayer(tout, "ERS_IN_" + CStr(i) + timestamp)
                tout = Nothing

                Try
                    tout = ExTool(GP, makelay, Nothing)
                    tlayer = GP.Open(tout)
                    tgplayer = gpu.MakeGPLayerFromLayer(tlayer)
                Catch ex As Exception
                    cleanup(GP, "ERS_*")
                    MsgBox("Unable to complete make feature layer on " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString, MsgBoxStyle.Critical, "ERS")
                    Debug.Print(ex.Message)
                    Exit Function
                End Try


                Dim sellay As SelectLayerByAttribute = New SelectLayerByAttribute(tgplayer)

                cnt = 0
                While Not (pfeat Is Nothing)
                    sellay.where_clause = openc & oidname & closec & " = " & pfeat.Value(oidi)
                    ExTool(GP, sellay, Nothing)

                    tinfldis = CStr(pfeat.Value(pfeat.Fields.FindField(layersinfo.ERS_LAYERS(i).infdis)))
                    tweight = CStr(pfeat.Value(pfeat.Fields.FindField(layersinfo.ERS_LAYERS(i).weight)))
                    tintensity = CStr(pfeat.Value(pfeat.Fields.FindField(layersinfo.ERS_LAYERS(i).intensity)))
                    sufx = CStr(i) & "_" & CStr(pfeat.Value(oidi))

                    Dim eucdis As EucDistance = New EucDistance(tgplayer, "ERS_DIS_" + sufx)
                    'eucdis.maximum_distance = tinfldis

                    Try
                        tout = ExTool(GP, eucdis, Nothing)
                        fixrastername(tout)
                    Catch ex As Exception
                        cleanup(GP, "ERS_*")
                        MsgBox("Unable to complete Find Distance Operation on Layer " & oidname & " of " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString, MsgBoxStyle.Critical, "ERS")
                        Debug.Print(ex.Message)
                        Exit Function
                    End Try

                    mp_app.StatusBar.Message(0) = ("Processing Layer " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString & " (Layer " & CStr(i) & " of " & CStr(bigi + 1) & ")...")

                    'mastring = 
                    'Dim mapal As SingleOutputMapAlgebra = New SingleOutputMapAlgebra(

                    'Dim mnus As Minus = New Minus(CStr(tinfldis), tout, "ERS_INV_" + sufx)
                    'tout = ExTool(GP, mnus, Nothing)
                    'fixrastername(tout)

                    'Dim mapal As SingleOutputMapAlgebra = New SingleOutputMapAlgebra("con(" & tout & " < 0" & ", 0, " & tout & ")", "ERS_CON" + sufx)
                    'tout = ExTool(GP, mapal, Nothing)
                    'fixrastername(tout)

                    rater = layersinfo.ERS_LAYERS(i).decayrate '1

                    'If layersinfo.ERS_LAYERS(i).decaytype = ERS_REC.decay_type.concave Then rater = 3
                    If layersinfo.ERS_LAYERS(i).decaytype = ERS_REC.decay_type.convex Then rater = 1 / 3

                    'If layersinfo.ERS_LAYERS(i).decaytype = ERS_REC.decay_type.concave Then
                    '    term2 = CDbl(tinfldis) / (CDbl(tinfldis) ^ rater)
                    '    Dim pwr As Power = New Power(tout, rater, "ERS_PWR_" + sufx)
                    '    tout = ExTool(GP, pwr, Nothing)
                    '    fixrastername(tout)

                    '    Dim tms As Times = New Times(tout, term2, "ERS_TMS_" + sufx)
                    '    tout = ExTool(GP, tms, Nothing)
                    '    fixrastername(tout)
                    'End If

                    'If layersinfo.ERS_LAYERS(i).decaytype = ERS_REC.decay_type.convex Then
                    '    term2 = CDbl(tinfldis) / (CDbl(tinfldis) ^ (1 / rater))
                    '    Dim pwr As Power = New Power(tout, CStr(1 / rater), "ERS_PWR_" + sufx)
                    '    tout = ExTool(GP, pwr, Nothing)
                    '    fixrastername(tout)

                    '    Dim tms As Times = New Times(tout, term2, "ERS_TMS_" + sufx)
                    '    tout = ExTool(GP, tms, Nothing)
                    '    fixrastername(tout)
                    'End If
                    'Dim hello As RasterCalculator = New RasterCalculator
                    Dim mapal As RasterCalculator = New RasterCalculator
                    mapal.output_raster = "ERS_OUT" & sufx

                    If (layersinfo.ERS_LAYERS(i).decaytype = ERS_REC.decay_type.constant) Or (tinfldis = 0) Then
                        'Dim coner As Con = New Con(tout, CStr(tinfldis + 0.000001), "ERS_TMS_" + sufx)
                        'coner.in_false_raster_or_constant = 0
                        'tout = ExTool(GP, coner, Nothing)
                        'fixrastername(tout)
                        'term2 = (CDbl(layersinfo.maxval) / CDbl(tinfldis) * (CDbl(tintensity) / 100.0) * CDbl(tweight)) * tinfldis
                        'mapal.expression_string = ("con((" & tinfldis & " - " & tout & ") < 0, 0, " & term2 & ")")

                        term2 = tintensity '234 '((CDbl(tinfldis) / (CDbl(tinfldis))) * (CDbl(layersinfo.maxval) / CDbl(tinfldis) * (CDbl(tintensity) / 100.0) * CDbl(tweight)))
                        mapal.expression = ("Float(Con((" & tinfldis & " - " & Chr(34) & tout & Chr(34) & ") < 0, 0, " & term2 & "))")

                    Else
                        term2 = (CDbl(tinfldis) / (CDbl(tinfldis) ^ rater)) * (CDbl(layersinfo.maxval) / CDbl(tinfldis) * (CDbl(tintensity) / layersinfo.maxval) * CDbl(tweight))
                        mapal.expression = ("Float(Con((" & tinfldis & " - " & Chr(34) & tout & Chr(34) & ") < 0, 0, Power((" & tinfldis & " - " & Chr(34) & tout & Chr(34) & "), " & rater & ") * " & term2 & "))")
                    End If

                    ''Try
                    tout = ExTool(GP, mapal, Nothing)
                    fixrastername(tout)
                    ''Catch ex As Exception
                    ''cleanup(GP, "ERS_*")
                    ''MsgBox("Unable to Calculate Decay Function on Layer " & oidname & " of " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString, MsgBoxStyle.Critical, "ERS")
                    ''Debug.Print(ex.Message)
                    ''Exit Function
                    ''End Try


                    'term2 = CDbl(layersinfo.maxval) / CDbl(tinfldis) * (CDbl(tintensity) / 100.0) * CDbl(tweight)
                    'Dim tms2 As Times = New Times(tout, term2, "ERS_OUT_" + sufx)
                    'tout = ExTool(GP, tms2, Nothing)
                    'fixrastername(tout)

                    cser.in_rasters_or_constants &= tout & ";"

                    pfeat = pfcur.NextFeature

                    'mp_app.StatusBar.ProgressBar.Position = ((CInt(((i) / (bigi + 1)) * 100)) - 2) + CInt((((CInt(((i + 1) / (bigi + 1)) * 100)) - 2) * (((CInt(((cnt + 1) / (tfc.FeatureCount(Nothing)))))))))

                    cnt = cnt + 1
                End While
                mp_app.StatusBar.Message(0) = ("Processing Layer " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString & " (Layer " & CStr(i) & " of " & CStr(bigi + 1) & ")...")

                cser.out_raster = "ERS_OUT_R" + CStr(i)

                cser.statistics_type = layersinfo.ERS_LAYERS(i).combofunk
                Try
                    fixrastername(cser.in_rasters_or_constants)
                    tout = ExTool(GP, cser, Nothing)
                    fixrastername(tout)
                Catch ex As Exception
                    cleanup(GP, "ERS_*")
                    MsgBox("Unable to Calculate Overlay Function on " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString, MsgBoxStyle.Critical, "ERS")
                    Debug.Print(ex.Message)
                    Exit Function
                End Try


                bigcser.in_rasters_or_constants &= tout & ";"

            Else

                Dim rout As Object
                addtext("Processing RASTER Layer " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString & " (Layer " & CStr(i) & " of " & CStr(bigi + 1) & ")...")

                Dim addr As Plus = New Plus(layersinfo.ERS_LAYERS(i).pgpLayer, 1, "ERS_OUT_A" + CStr(i))
                rout = ExTool(GP, addr, Nothing)
                Dim mapalbra As RasterCalculator = New RasterCalculator("Con(IsNull(" & Chr(34) & rout & Chr(34) & "), 0, " & Chr(34) & rout & Chr(34) & ")", "ERS_OUT_F" + CStr(i))
                rout = ExTool(GP, mapalbra, Nothing)


                term2 = CDbl(layersinfo.ERS_LAYERS(i).weight)
                Dim tms3 As Times = New Times(rout, term2, "ERS_OUT_R" + CStr(i))

                Try
                    tout = ExTool(GP, tms3, Nothing)
                    fixrastername(tout)
                Catch ex As Exception
                    cleanup(GP, "ERS_*")
                    MsgBox("Unable to Calculate Weight Function on " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString, MsgBoxStyle.Critical, "ERS")
                    Debug.Print(ex.Message)
                    Exit Function
                End Try


                bigcser.in_rasters_or_constants &= tout & ";"

            End If

            mp_app.StatusBar.ProgressBar.Position = (CInt(((i) / (bigi + 1)) * 100)) '- 2
            mp_app.StatusBar.Message(0) = ("Processing Layer " & layersinfo.ERS_LAYERS(i).pgpLayer.NameString & " (Layer " & CStr(i) & " of " & CStr(bigi + 1) & ")...")

            pb.Value = CInt(((i + 1) / (bigi + 2)) * 100)
        Next i

        If layersinfo.minscale = -9999 Then
            bigcser.out_raster = layersinfo.outname
            bigcser.statistics_type = layersinfo.combofunc
            Try
                fixrastername(bigcser.in_rasters_or_constants)
                tout = ExTool(GP, bigcser, Nothing)
                ''''''fixrastername(tout)
            Catch ex As Exception
                cleanup(GP, "ERS_*")
                MsgBox("Unable to Calculate Final Overlay Function")
                Debug.Print(ex.Message)
                Exit Function
            End Try
        Else
            bigcser.out_raster = "ERS_TEMP_F"
            bigcser.statistics_type = layersinfo.combofunc
            Try
                ''''''fixrastername(bigcser.in_rasters_or_constants)
                tout = ExTool(GP, bigcser, Nothing)
                ''''''fixrastername(tout)
            Catch ex As Exception
                cleanup(GP, "ERS_*")
                MsgBox("Unable to Calculate Final Overlay Function")
                Debug.Print(ex.Message)
                Exit Function
            End Try
            Dim ranger As Double
            ranger = layersinfo.maxscale - layersinfo.minscale
            Dim mapal2 As RasterCalculator = New RasterCalculator("((((" & ranger & ") / ZonalStatistics(Int(" & Chr(34) & tout & Chr(34) & " * 0 + 1), " & Chr(34) & "VALUE" & Chr(34) & ", " & Chr(34) & tout & Chr(34) & ", " & Chr(34) & "MAXIMUM" & Chr(34) & ")) * " & Chr(34) & tout & Chr(34) & ") + " & layersinfo.minscale & ")", "ERS_FINAL")
            Try
                tout = ExTool(GP, mapal2, Nothing)
                ''''fixrastername(tout)
            Catch ex As Exception
                cleanup(GP, "ERS_*")
                MsgBox("Unable to Calculate Final Scale Function")
                Debug.Print(ex.Message)
                Exit Function
            End Try

        End If

        Dim copyfraster As CopyRaster = New CopyRaster(tout, layersinfo.outpath)
        tout = Nothing

        ' Try
        tout = ExTool(GP, copyfraster, Nothing)

        'Catch ex As Exception
        '    MsgBox("Unable to Copy final Output GRID to specified location." + vbCrLf + "You can find a complete version in the PAT scratch directory.")
        '    Debug.Print(ex.Message)
        '    Exit Function
        'End Try

        Dim lp As String()
        lp = layersinfo.outpath.Split("\")
        Dim lplength As Integer

        lplength = lp.Length

        Dim namefordisp As String
        namefordisp = lp(lplength - 1)
        GP.AddOutputsToMap = True

        Dim makerlay As MakeRasterLayer = New MakeRasterLayer(tout, namefordisp) ' & "_")
        tout = Nothing
        Dim trlay As ILayer


        tout = ExTool(GP, makerlay, Nothing)

        GP.AddOutputsToMap = False

        Try
            'trlay = GP.Open(layersinfo.outpath)
            'Dim trlay2 As IDataset
            'trlay2 = trlay
            'trlay.Name = trlay2.BrowseName



            Dim pmdoc As IMxDocument
            Dim mapin As IMap
            pmdoc = mp_app.Document
            mapin = pmdoc.ActiveView

            Dim cnter As Long
            Dim pmlay As ILayer

            For cnter = 0 To mapin.LayerCount - 1
                pmlay = mapin.Layer(cnter)
                If TypeOf pmlay Is IRasterLayer Then
                    If pmlay.Name = namefordisp Then
                        trlay = pmlay
                    End If
                End If
            Next

            'trlay = mapin.Layer(0)

            'type_defs.addrasterlayertomap(mapin, trlay, pmdoc)

            'mapin.AddLayer(trlay)

            Dim tr As IRasterLayer
            tr = trlay
            ''''

            Dim pRen As IRasterStretchColorRampRenderer = New RasterStretchColorRampRenderer
            pRen = tr.Renderer

            Dim pColorRamp1 As IAlgorithmicColorRamp ', pEnumColors As IEnumColors
            Dim pColorRamp2 As IAlgorithmicColorRamp
            Dim paColorRamp As IMultiPartColorRamp
            Dim pColor1 As IRgbColor
            Dim pColor2 As IRgbColor
            Dim pColor3 As IRgbColor

            pColorRamp1 = New AlgorithmicColorRamp
            pColorRamp2 = New AlgorithmicColorRamp
            paColorRamp = New MultiPartColorRamp
            pColorRamp1.Algorithm = esriColorRampAlgorithm.esriCIELabAlgorithm
            pColor1 = New RgbColor
            pColor2 = New RgbColor
            pColor3 = New RgbColor
            pColor1.Red = 255
            pColor1.Green = 0
            pColor1.Blue = 0
            pColor2.Red = 255
            pColor2.Green = 255
            pColor2.Blue = 100
            pColor3.Red = 0
            pColor3.Green = 0
            pColor3.Blue = 255
            pColorRamp1.FromColor = pColor1
            pColorRamp1.ToColor = pColor2
            pColorRamp2.FromColor = pColor2
            pColorRamp2.ToColor = pColor3
            paColorRamp.Ramp(0) = pColorRamp1
            paColorRamp.Ramp(1) = pColorRamp2
            'paColorRamp.CreateRamp True

            pRen.ColorRamp = paColorRamp

            'paColorRamp = GetStyleItem("ESRI.style", "Color Ramps", "Yellow to dark red")
            'pRen.ColorRamp = paColorRamp

            Dim pRStretchType As IRasterStretch
            pRStretchType = pRen
            pRStretchType.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum  'esriRasterStretchTypesEnum.esriRasterStretch_StandardDeviations
            'pRStretchType.StandardDeviationsParam = 3
            pRStretchType.Invert = True
            pRStretchType.Background = True
            pRStretchType.BackgroundValues = 0

            'prend.ColorRamp


            tr.Renderer = pRen
            tr.Renderer.Update()

            pmdoc.ActivatedView.PartialRefresh(esriViewDrawPhase.esriViewGeography, trlay, Nothing)
            pmdoc.ActivatedView.Refresh()
            pmdoc.UpdateContents()


            'mapin.Layer(0).Name = trlay2.BrowseName
            pmdoc.UpdateContents()
            GP.AddOutputsToMap = False

            cleanup(GP, "ERS_*")

        Catch ex As Exception
            'cleanup(GP, "ERS_*")
            MsgBox("Process complete.  Unable to Add final Output Layer to map." + vbCrLf + "Raster Catalog Items must be added manually.", MsgBoxStyle.Exclamation, "ERS Complete")
            Debug.Print(ex.Message)
            mp_app.StatusBar.ProgressBar.Hide()
            cleanup(GP, "ERS_*")
        End Try

        gpu = Nothing
        tlayer = Nothing
        tgplayer = Nothing
        tout = Nothing
        mp_app.StatusBar.ProgressBar.Position = 100

        Beep()
        MsgBox("ERS Processing Complete!", MsgBoxStyle.OkOnly, "ERS")
        addtext("ERS Processing Complete")
        mp_app.StatusBar.Message(0) = ("ERS Processing Complete")
        mp_app.StatusBar.ProgressBar.Hide()
        OK_Button.Enabled = True

        GP.SetEnvironmentValue("extent", "")


        Return True
    End Function

    Public Sub addtext(ByVal textin As String, Optional ByVal cr As Boolean = True)

        detailbox.Text &= textin
        If cr Then detailbox.Text &= vbCrLf

        PostMessage(CType(detailbox.Handle, IntPtr), WM_VSCROLL, CType(SB_BOTTOM, IntPtr), CType(IntPtr.Zero, IntPtr))
    End Sub


    'Private Sub dislayer(ByVal layersinfo As ERS_REC, ByVal indy As Long)

    '    Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
    '    Dim gpu As IGPUtilities
    '    gpu = New ESRI.ArcGIS.Geoprocessing.GPUtilities

    '    ' Set the OverwriteOutput setting to True
    '    GP.OverwriteOutput = True

    '    Dim dislay As Dissolve = New Dissolve(layersinfo.ERS_LAYERS(indy).pgpLayer, "c:\temp\TEST_WHOHA_" + CStr(indy) + ".shp")
    '    dislay.dissolve_field = layersinfo.ERS_LAYERS(indy).infdis & ";" & layersinfo.ERS_LAYERS(indy).intensity & ";" & _
    '                            layersinfo.ERS_LAYERS(indy).weight
    '    ExTool(GP, dislay, Nothing)

    '    GP = Nothing
    '    gpu = Nothing

    'End Sub

    Public Function ExTool(ByVal geop As ESRI.ArcGIS.Geoprocessor.Geoprocessor, ByVal process As IGPProcess, ByVal TC As ITrackCancel) As Object
        ' Set the overwrite output option to true
        Dim thing As IGeoProcessorResult

        geop.OverwriteOutput = True

        Try
            thing = geop.Execute(process, Nothing)
            'ReturnMessages(geop)
            If thing Is Nothing Then
                Return Nothing
                Exit Function
            End If
            Return thing.ReturnValue
        Catch err As Exception
            'addtext(Err.Message)
            'ReturnMessages(geop)
            Return Nothing
            Exit Function
        End Try



    End Function

    Public Sub ReturnMessages(ByVal gp As ESRI.ArcGIS.Geoprocessor.Geoprocessor)
        ' Print out the messages from tool executions
        Dim Count As Integer
        If gp.MessageCount > 0 Then
            For Count = 0 To gp.MessageCount - 1
                addtext(gp.GetMessage(Count))
            Next
        End If
    End Sub

    Public Sub fixrastername(ByRef rasname As String)
        Dim delim As String = "; " & Chr(34)
        rasname = rasname.Trim(delim.ToCharArray())
    End Sub

    Public Sub cleanup(ByVal gp As ESRI.ArcGIS.Geoprocessor.Geoprocessor, ByVal wildcard As String)
        Dim lister As IGpEnumList
        Try
            lister = gp.ListDatasets(wildcard, "")
        Catch err As Exception
            'MsgBox(err.Message)
            gp = Nothing
            Exit Sub
        End Try

        Dim check As String
        Dim deler As Delete = New Delete

        check = lister.Next()
        While check <> ""
            'MsgBox(check)
            deler.in_data = check
            ExTool(gp, deler, Nothing)
            check = lister.Next()
        End While

        gp = Nothing

    End Sub

End Class
