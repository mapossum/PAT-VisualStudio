Imports System.Windows.Forms
Imports System.IO
Imports System.Data
Imports System.Data.OleDb
Imports ESRI.ArcGIS.DataSourcesGDB
Imports Ionic.Zip
Imports ESRI.ArcGIS.CartoUI
Imports System.Drawing
Imports System.Reflection
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.SystemUI



Public Class MDS_Form

    Public isdone As Boolean
    Public pile As List(Of Double) = New List(Of Double)
    Public mApp As IMxApplication
    Private m_application As IApplication
    Public GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor
    Public GPU As ESRI.ArcGIS.Geoprocessing.GPUtilities
    Public pFolder As String
    Public crun As String
    Public lrun As String = ""
    Public data As DataSet
    Public outdata As DataSet
    Public nreps As Long
    Public curp As String
    Public ttime As String
    Public cld As Boolean = False
    Public scname As String = "output"
    Public minchart As List(Of Double) = New List(Of Double)
    Public maxchart As List(Of Double) = New List(Of Double)
    Public valmod As String = ""
    Public valchart As List(Of Double) = New List(Of Double)

    'Public loopend As Boolean = False

    Public Delegate Sub MarxanProgressChanged(ByVal progress As Integer)
    Public Delegate Sub inviz(ByVal viz As Boolean)
    Public Delegate Sub AddOutputMessage(ByVal val As String)

    Private Sub MarxanProgressCh(ByVal s As Integer)
        If Me.InvokeRequired Then
            Me.BeginInvoke(New MarxanProgressChanged(AddressOf MarxanProgressCh), New Object() {s})
            Return
        End If
        MarxanProgress.Value = s
    End Sub

    Private Sub isCompleted(ByVal s As Boolean)
        If Me.InvokeRequired Then
            Me.BeginInvoke(New inviz(AddressOf isCompleted), New Object() {s})
            Return
        End If
        ProcessPanel.Visible = s
    End Sub

    Private Sub ChangeProcessLabel(ByVal stringer As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(New AddOutputMessage(AddressOf ChangeProcessLabel), New Object() {stringer})
            Return
        End If
        Label9.Text = stringer
    End Sub

    Private Sub ChangeInputTB(ByVal stringer As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(New AddOutputMessage(AddressOf ChangeInputTB), New Object() {stringer})
            Return
        End If
        inputdatfile.Text = stringer
    End Sub

    Private Sub AddOutMessage(ByVal stringer As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(New AddOutputMessage(AddressOf AddOutMessage), New Object() {stringer})
            Return
        End If
        TextMessages.Text = stringer
    End Sub

    Public Sub New(ByVal inapp As IMxApplication)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        mApp = inapp
        m_application = mApp
        ' Add any initialization after the InitializeComponent() call.

        GP = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
        GP.AddOutputsToMap = False
        GPU = New ESRI.ArcGIS.Geoprocessing.GPUtilities

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim dchooser As New FolderBrowserDialog

        If dchooser.ShowDialog() = Windows.Forms.DialogResult.OK Then

            Me.prFolder.Text = dchooser.SelectedPath()

        End If

    End Sub

    Private Sub CreateProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateProjectToolStripMenuItem.Click
        Me.Panel1.Show()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Dim fchooser As New OpenFileDialog

        fchooser.Filter = "dat files|*.dat|all files|*.*"

        If fchooser.ShowDialog() = Windows.Forms.DialogResult.OK Then
            inputbox.Text = fchooser.FileName
        End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Me.Visible = False
        Dim pGxDialog As IGxDialog, pFilterColl As IGxObjectFilterCollection
        pGxDialog = New GxDialog

        pGxDialog.Title = "Input Planning Unit Feature Class"
        pFilterColl = pGxDialog

        Dim pGxFilter As IGxObjectFilter
        pGxFilter = New GxFilterFeatureClasses
        pFilterColl.AddFilter(pGxFilter, True)

        Dim pego As IEnumGxObject

        'Dim pEnumGxObj As IEnumGxObject
        If Not pGxDialog.DoModalOpen(0, pego) Then
            'featuresBox.Text = ""
            Me.BringToFront()
            Me.Select()
            Exit Sub 'Exit if user press cancel. 
        End If

        Dim peg As IGxObject
        peg = pego.Next()
        featuresBox.Text = peg.FullName

        Me.Visible = True
        Me.BringToFront()
        Me.Select()

        Dim pfc As IFeatureClass
        pfc = GP.Open(featuresBox.Text)

        Dim pfields As IFields

        pfields = pfc.Fields

        Dim i As Integer
        Dim f As IField

        Dim fount As Boolean = False
        For i = 0 To pfields.FieldCount - 1

            f = pfields.Field(i)
            FieldNames.Items.Add(f.Name)
            If f.Name = "PUID" Then fount = True

        Next

        If fount = True Then FieldNames.Text = "PUID"

    End Sub

    Private Sub cancelbut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cancelbut.Click
        Panel1.Visible = False
    End Sub

    Private Sub Okbut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Okbut.Click
        Dim cango As Boolean = True
        If prFolder.Text = "" Then cango = False
        If featuresBox.Text = "" Then cango = False
        If inputbox.Text = "" Then cango = False

        crun = "Original"

        If Not cango Then

            MsgBox("Required information is not specified.", MsgBoxStyle.Exclamation, "Missing Information")

        Else

            pFolder = prFolder.Text
            rCreateFileGDB(featuresBox.Text)
            setupMarxanFolders(inputbox.Text)
            createlayerfiles()
            archivemrun()

            readProject()

            Panel1.Hide()

        End If

    End Sub

    Public Sub createlayerfiles()

        Dim pgroupLay As IGroupLayer = New GroupLayer

        pgroupLay.Name = Path.GetFileName(pFolder)
        pgroupLay.Visible = True

        Dim pMxDoc As IMxDocument
        Dim pMap As IMap

        pMxDoc = mApp.Document
        pMap = pMxDoc.FocusMap

        'pMap.AddLayer(pgroupLay)

        Dim pflay As IFeatureLayer = New FeatureLayer

        Dim pfc As IFeatureClass
        pfc = GP.Open(pFolder + "\GIS_Data.gdb\Planning_Units")
        pflay.FeatureClass = pfc

        Dim pdl As IDataLayer
        pdl = pflay
        pdl.RelativeBase = pFolder + "\GIS_Data.gdb"
        Dim pl As ILayer
        pl = pdl
        pl.Name = "Planning_Units"

        pgroupLay.Add(pl)

        'Create a new LayerFile instance.
        Dim layerFile As ILayerFile = New LayerFileClass()
        'Create a new layer file.
        layerFile.New(pFolder + "\Planning_Units.lyr")

        'Bind the layer file with the layer from the map.
        layerFile.ReplaceContents(pl)
        layerFile.Save()

        'Create a new layer file.
        layerFile.New(pFolder + "\Project.lyr")

        'Bind the layer file with the layer from the map.
        layerFile.ReplaceContents(pgroupLay)
        layerFile.Save()


    End Sub

    Public Sub readProject()
        Try

            mFiles.Items.Clear()
            mFiles.Items.Add("input")

            Dim readin As StreamReader = New StreamReader(pFolder + "\marxan\input.dat")
            ChangeInputTB("") ' inputdatfile.Text = ""
            ChangeInputTB(readin.ReadToEnd()) 'inputdatfile.Text = readin.ReadToEnd()
            readin.Close()

            Dim di As DirectoryInfo = New DirectoryInfo(pFolder + "\marxan\input")
            Dim fi As FileInfo()
            Dim fii As FileInfo
            fi = di.GetFiles()

            'Dim sConnectionString As String = "Driver={Microsoft Text Driver" + "(*.txt; *.csv)};Dbq=" & pFolder + "\marxan\input" & ";Extensions=txt;"
            'Dim objConn As New Odbc.OdbcConnection(sConnectionString)
            Dim objConn As New OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;" & _
                "Data Source=" & pFolder + "\marxan\input" & ";" & _
                "Extended Properties=""Text;HDR=Yes;FMT=Delimited""")
            objConn.Open()

            Dim d As New DataSet("Marxan")

            For Each fii In fi
                Using adapter As New OleDbDataAdapter( _
                "SELECT * FROM " & fii.Name, objConn)
                    adapter.Fill(d, fii.Name.Replace(".txt", ""))
                End Using
                mFiles.Items.Add(fii.Name.Replace(".txt", ""))
            Next

            data = d

            mFiles.Text = "pu"

            objConn.Close()


            Dim pMxDoc As IMxDocument
            Dim pMap As IMap
            Dim pEnumLayer As IEnumLayer
            Dim play As ILayer
            Dim groupylayer As IGroupLayer

            pMxDoc = mApp.Document
            pMap = pMxDoc.FocusMap
            pEnumLayer = pMap.Layers(Nothing, True)
            pEnumLayer.Reset()
            play = pEnumLayer.Next
            Do While Not play Is Nothing
                If Path.GetFileName(pFolder) = play.Name Then
                    If TypeOf play Is IGroupLayer Then
                        pMap.DeleteLayer(play)
                        Exit Do
                    End If
                End If
                play = pEnumLayer.Next
            Loop

            Dim pLayer As ILayer
            pLayer = GP.Open(pFolder + "\Project.lyr")

            pMxDoc = mApp.Document
            pMap = pMxDoc.FocusMap

            pMap.AddLayer(pLayer)

        Catch ex As Exception

            MsgBox("No open project, or previous run.", MsgBoxStyle.Critical)

        End Try

    End Sub

    Public Sub rCreateFileGDB(ByVal feats As String)

        Dim cfgdb As CreateFileGDB = New CreateFileGDB(pFolder, "GIS_Data")
        GP.Execute(cfgdb, Nothing)

        Dim fc2fc As CopyFeatures = New CopyFeatures(feats, pFolder + "\GIS_Data.gdb\Planning_Units")
        GP.Execute(fc2fc, Nothing)

        Dim addf As AddField = New AddField(pFolder + "\GIS_Data.gdb\Planning_Units", "MARXAN_PUID", "LONG")
        GP.Execute(addf, Nothing)

        Dim calv As CalculateField = New CalculateField(pFolder + "\GIS_Data.gdb\Planning_Units", "MARXAN_PUID", "[" + FieldNames.Text + "]")
        GP.Execute(calv, Nothing)

        'Dim createindex As AddIndex = New AddIndex(pFolder + "\GIS_Data.gdb\Planning_Units", "MARXAN_PUID")
        'createindex.unique = True
        'GP.Execute(createindex, Nothing)

        Dim createtab As CreateTable = New CreateTable(pFolder + "\GIS_Data.gdb", "Marxan_Runs")
        GP.Execute(createtab, Nothing)

        Dim ptab As ITable
        ptab = GP.Open(pFolder + "\GIS_Data.gdb\Marxan_Runs")
        AddBfield(ptab)

        ptab = Nothing

    End Sub


    Public Sub AddBfield(ByVal tab As ITable)

        Dim fields As IFields = New FieldsClass()
        Dim fieldsEdit As IFieldsEdit = CType(fields, IFieldsEdit)

        Dim oidField As IField = New FieldClass()
        Dim oidFieldEdit As IFieldEdit = CType(oidField, IFieldEdit)
        oidFieldEdit.Name_2 = "RUNID"
        oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeString
        oidFieldEdit.Length_2 = 25

        Dim oidField2 As IField = New FieldClass()
        Dim oidFieldEdit2 As IFieldEdit = CType(oidField2, IFieldEdit)
        oidFieldEdit2.Name_2 = "Run_Label"
        oidFieldEdit2.Type_2 = esriFieldType.esriFieldTypeString
        oidFieldEdit2.Length_2 = 50

        Dim Costv As IField = New FieldClass()
        Dim costved As IFieldEdit = CType(Costv, IFieldEdit)
        costved.Name_2 = "Best_Cost"
        costved.Type_2 = esriFieldType.esriFieldTypeDouble

        Dim Costv2 As IField = New FieldClass()
        Dim costved2 As IFieldEdit = CType(Costv2, IFieldEdit)
        costved2.Name_2 = "Mean_Cost"
        costved2.Type_2 = esriFieldType.esriFieldTypeDouble

        Dim dataField As IField = New FieldClass()
        Dim dataFieldEdit As IFieldEdit = CType(dataField, IFieldEdit)
        dataFieldEdit.Name_2 = "MARXAN_Data"
        dataFieldEdit.Type_2 = esriFieldType.esriFieldTypeBlob

        Dim inputf As IField = New FieldClass()
        Dim inputfEdit As IFieldEdit = CType(inputf, IFieldEdit)
        inputfEdit.Name_2 = "input_dat"
        inputfEdit.Type_2 = esriFieldType.esriFieldTypeBlob

        Dim gf As IField = New FieldClass()
        Dim gfEdit As IFieldEdit = CType(gf, IFieldEdit)
        gfEdit.Name_2 = "GIS_Layer"
        gfEdit.Type_2 = esriFieldType.esriFieldTypeBlob

        Dim schemaLock As ISchemaLock = CType(tab, ISchemaLock)

        Try
            ' A try block is necessary, as an exclusive lock may not be available.
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock)

            ' Add the field.
            tab.AddField(oidField)
            tab.AddField(oidField2)
            tab.AddField(Costv)
            tab.AddField(Costv2)
            tab.AddField(dataField)
            tab.AddField(inputf)
            tab.AddField(gf)

        Catch exc As Exception
            ' Handle this in a way appropriate to your application.
            Console.WriteLine(exc.Message)
        Finally
            ' Set the lock to shared, whether or not an error occurred.
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock)
        End Try

    End Sub

    Public Sub setupMarxanFolders(ByVal inputdat As String)
        Directory.CreateDirectory(pFolder + "\marxan")
        Directory.CreateDirectory(pFolder + "\marxan\input")
        Directory.CreateDirectory(pFolder + "\marxan\output")
        Directory.CreateDirectory(pFolder + "\temp")

        Dim sr As StreamReader = New StreamReader(inputdat)
        Dim wr As StreamWriter = New StreamWriter(pFolder + "\marxan\input.dat")
        Dim line As String
        Dim ls As String()
        Dim iroot As String = ""
        Dim pathto As String
        Dim fin As String
        Dim fout As String
        Do
            line = sr.ReadLine()
            If Not line Is Nothing Then
                ls = line.Split(" ")
                If ls(0).ToUpper = "INPUTDIR" Then
                    iroot = ls(1)
                    line = "INPUTDIR input"
                End If

                If ls(0).ToUpper = "OUTPUTDIR" Then
                    iroot = ls(1)
                    line = "OUTPUTDIR output"
                End If

                If ls(0).ToUpper = "SCENNAME" Then
                    iroot = ls(1)
                    line = "SCENNAME output"
                End If

                If iroot.Contains(":") Then pathto = iroot Else pathto = Path.GetDirectoryName(inputdat) + "\" + iroot
                Dim di As DirectoryInfo = New DirectoryInfo(pathto)
                fout = ""
                fin = ""

                If ls(0).ToUpper = "SPECNAME" Then
                    fin = ls(1)
                    fout = "spec.txt"
                End If

                If ls(0).ToUpper = "PUNAME" Then
                    fin = ls(1)
                    fout = "pu.txt"
                End If

                If ls(0).ToUpper = "PUVSPRNAME" Then
                    fin = ls(1)
                    fout = "puvspr2.txt"
                End If

                If ls(0).ToUpper = "BOUNDNAME" Then
                    fin = ls(1)
                    fout = "bound.txt"
                End If

                If ls(0).ToUpper = "BLOCKDEFNAME" Then
                    fin = ls(1)
                    fout = "blockdef.txt"
                End If

                If ls(0).ToUpper = "ZONESNAME" Then
                    fin = ls(1)
                    fout = "zones.txt"
                End If

                If ls(0).ToUpper = "COSTSNAME" Then
                    fin = ls(1)
                    fout = "costs.txt"
                End If

                If ls(0).ToUpper = "ZONECOSTNAME" Then
                    fin = ls(1)
                    fout = "zonecost.txt"
                End If

                If ls(0).ToUpper = "ZONECONTRIBNAME" Then
                    fin = ls(1)
                    fout = "zonecontrib.txt"
                End If

                If ls(0).ToUpper = "ZONEBOUNDCOSTNAME" Then
                    fin = ls(1)
                    fout = "zoneboundcost.txt"
                End If


                If Not fout = "" Then
                    line = ls(0).ToUpper + " " + fout
                    System.IO.File.Copy(pathto + "\" + fin, pFolder + "\marxan\input\" + fout)
                End If

                'Debug.Print(line)
                wr.WriteLine(line)
            End If
        Loop Until line Is Nothing

        sr.Close()
        wr.Close()

    End Sub

    Private Sub OpenProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenProjectToolStripMenuItem.Click

        Dim dchooser As New FolderBrowserDialog

        If dchooser.ShowDialog() = Windows.Forms.DialogResult.OK Then
            pFolder = dchooser.SelectedPath()
            readProject()
        End If

    End Sub

    Private Sub mFiles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mFiles.SelectedIndexChanged
        If mFiles.Text <> "input" Then
            inputdatfile.Visible = False
            mcollist.Enabled = True
            Button4.Enabled = True
            TextBox1.Enabled = True
            mainGrid.DataSource = data.Tables(mFiles.Text)
            mainGrid.Columns(0).ReadOnly = True
            mcollist.Items.Clear()
            Dim i As Integer
            For i = 0 To mainGrid.Columns.Count - 1
                mainGrid.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
                If i > 0 Then mcollist.Items.Add(mainGrid.Columns(i).Name)
            Next i
            mcollist.SelectedIndex = i - 2
        Else
            inputdatfile.Visible = True
            mcollist.Enabled = False
            Button4.Enabled = False
            TextBox1.Enabled = False
        End If
    End Sub

    Private Sub RunMarxan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunMarxan.Click

        makeDataReady()
        lrun = crun
        crun = Now.ToString("yyyyMMddHHmmss")
        runMarxanProgram()

    End Sub

    Private Sub makeDataReady()

        Dim dt As DataTable
        Dim dr As DataRow
        Dim dc As DataColumn

        Dim myString As String
        Dim bFirstRecord As Boolean = True

        Dim iWriter As New System.IO.StreamWriter(pFolder + "\marxan\input.dat")
        iWriter.Write(inputdatfile.Text)
        iWriter.Close()

        Dim iRead As New System.IO.StreamReader(pFolder + "\marxan\input.dat")
        Dim line As String
        line = iRead.ReadLine()
        Do While Not line Is Nothing
            If line.ToUpper.Contains("NUMREPS") Then nreps = CLng(line.Split(" ")(1))
            If line.ToUpper.Contains("SCENNAME") Then scname = CStr(line.Split(" ")(1))
            line = iRead.ReadLine()
        Loop

        iRead.Close()

        Try
            For Each dt In data.Tables
                Dim myWriter As New System.IO.StreamWriter(pFolder + "\marxan\input\" + dt.TableName + ".txt")
                Dim firstline As String = ""
                For Each dc In dt.Columns
                    firstline = firstline + "," + dc.ColumnName
                Next
                myWriter.WriteLine(firstline.Trim(","))
                For Each dr In dt.Rows
                    myString = ""
                    bFirstRecord = True
                    For Each field As Object In dr.ItemArray
                        If Not bFirstRecord Then
                            myString = myString + ","
                        End If

                        myString = myString + field.ToString
                        bFirstRecord = False

                    Next
                    myWriter.WriteLine(myString)
                Next
                myWriter.Close()
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub savecurrent()
        'find reason for lag
        Dim pEnumLayer As IEnumLayer
        Dim play As ILayer
        Dim groupylayer As IGroupLayer
        Dim pMxDoc As IMxDocument
        Dim pMap As IMap

        pMxDoc = mApp.Document
        pMap = pMxDoc.FocusMap
        pEnumLayer = pMap.Layers(Nothing, True)
        pEnumLayer.Reset()
        play = pEnumLayer.Next
        Do While Not play Is Nothing
            If Path.GetFileName(pFolder) = play.Name Then
                If TypeOf play Is IGroupLayer Then
                    groupylayer = play
                    Exit Do
                End If
            End If
            play = pEnumLayer.Next
        Loop

        Dim spliter As String()
        Dim flist As SortedList(Of String, ILayer) = New SortedList(Of String, ILayer)
        Dim pfeatl As ITable
        If Not groupylayer Is Nothing Then
            Dim pcomplay As ICompositeLayer
            pcomplay = groupylayer
            Dim curlay As List(Of ILayer) = New List(Of ILayer)
            Dim i As Long = 0
            For i = 0 To pcomplay.Count - 1
                curlay.Add(pcomplay.Layer(i))
                play = pcomplay.Layer(i)
                If TypeOf play Is IFeatureLayer Then
                    pfeatl = play
                    Dim pfl As IFields
                    pfl = pfeatl.Fields
                    For fc As Integer = 0 To pfl.FieldCount - 1
                        spliter = pfl.Field(fc).Name.Split(".")
                        If spliter.Length > 1 Then
                            If spliter(0).Contains("M_") Then
                                If Not flist.Keys.Contains(spliter(0)) Then
                                    flist.Add(spliter(0), play)
                                End If
                            End If
                        End If
                    Next fc
                End If
            Next i

            If flist.Keys.Count > 0 Then

                Dim pFWS As IFeatureWorkspace
                Dim pWorkspaceFactory As IWorkspaceFactory
                pWorkspaceFactory = New FileGDBWorkspaceFactory
                pFWS = pWorkspaceFactory.OpenFromFile(pFolder + "\GIS_Data.gdb", 0)

                Dim pTable As ITable
                Dim pRow As IRow
                Dim val As String
                pTable = pFWS.OpenTable("Marxan_Runs")
                Dim pcur As ICursor
                pcur = pTable.Update(Nothing, True)
                pRow = pcur.NextRow()
                Dim pLayer As ILayer
                Dim pdl As IDataLayer
                Dim layerFile As ILayerFile

                While Not pRow Is Nothing
                    val = pRow.Value(pRow.Fields.FindField("RUNID"))
                    If flist.Keys.Contains("M_" + val) Then
                        pLayer = flist.Item("M_" + val)

                        pdl = pLayer
                        pdl.RelativeBase = pFolder + "\GIS_Data.gdb"

                        'Create a new LayerFile instance.
                        layerFile = New LayerFileClass()
                        'Create a new layer file.
                        layerFile.New(pFolder + "\Planning_Units.lyr")
                        layerFile.ReplaceContents(pdl)
                        layerFile.Save()

                        Dim pMemoryBlobStreamgis As IMemoryBlobStream
                        pMemoryBlobStreamgis = New MemoryBlobStream
                        pMemoryBlobStreamgis.LoadFromFile(pFolder + "\Planning_Units.lyr")

                        pRow.Value(pRow.Fields.FindField("GIS_Layer")) = pMemoryBlobStreamgis
                        pRow.Value(pRow.Fields.FindField("RUN_Label")) = pLayer.Name
                        pRow.Store()
                        pcur.UpdateRow(pRow)

                    End If
                    pRow = pcur.NextRow()
                End While

                pdl = flist.Item(flist.Keys(flist.Keys.Count - 1))
                pdl.RelativeBase = pFolder + "\GIS_Data.gdb"

                'Create a new LayerFile instance.
                layerFile = New LayerFileClass()
                'Create a new layer file.
                layerFile.New(pFolder + "\Planning_Units.lyr")
                layerFile.ReplaceContents(pdl)
                layerFile.Save()

            End If

        End If

    End Sub

    Public Sub runMarxanProgram()

        savecurrent()

        ProcessPanel.Top = 0
        ProcessPanel.Left = 0
        'ProcessPanel.Visible = True
        isCompleted(True)
        MarxanProgressCh(0)
        ChangeProcessLabel("Marxan Progress")

        Directory.Delete(pFolder + "\marxan\output", True)
        Directory.CreateDirectory(pFolder + "\marxan\output")

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

        If marxanplace = "" Then
            MsgBox("You must set the MARXAN file location in options menu.", MsgBoxStyle.Exclamation, "Marxan not found")
            Exit Sub
        End If


        Dim result As String = ""
        Dim p As New Process()

        Environment.CurrentDirectory = Path.GetDirectoryName(pFolder + "\marxan\input.dat")
        p.StartInfo.FileName = marxanplace
        p.StartInfo.CreateNoWindow = True
        p.StartInfo.Arguments = pFolder + "\marxan\input.dat"
        p.StartInfo.RedirectStandardError = True
        p.StartInfo.RedirectStandardOutput = True
        p.StartInfo.UseShellExecute = False

        p.EnableRaisingEvents = True

        ' add an Exited event handler
        AddHandler p.OutputDataReceived, AddressOf ProcessMarxan

        p.Start()
        m_application.StatusBar.ProgressBar.Message = "Running Marxan"
        AddOutMessage("Running Marxan")
        m_application.StatusBar.ProgressBar.Show()

        ' Start the asynchronous read of the sort output stream.
        p.BeginOutputReadLine()

    End Sub

    Public Sub ProcessMarxan(ByVal sendingProcess As Object, ByVal outLine As DataReceivedEventArgs)

        Dim runner As String()

        If Not String.IsNullOrEmpty(outLine.Data) Then

            If outLine.Data.Contains("Run") Then
                runner = outLine.Data.Split(" ")
                Debug.Print(outLine.Data)
                Debug.Print(runner(1))
                AddOutMessage("Processing Marxan" + vbCrLf + vbCrLf + outLine.Data)
                m_application.StatusBar.ProgressBar.Message = outLine.Data
                m_application.StatusBar.ProgressBar.Position = CInt((CDbl(runner(1)) / CDbl(nreps)) * 100)
                MarxanProgressCh(CInt((CDbl(runner(1)) / CDbl(nreps)) * 100))
            End If
            If outLine.Data.Contains("The End") Then
                m_application.StatusBar.ProgressBar.Message = "Marxan Complete, Processing Output"
                m_application.StatusBar.ProgressBar.Position = 0
                AddOutMessage("Marxan Complete, Processing Output")
                'm_application.StatusBar.ProgressBar.Hide()
                readOutput()
            End If


        End If
    End Sub

    Public Sub archivemrun(Optional ByVal costmin As Double = 0, Optional ByVal costmean As Double = 0)

        AddOutMessage("Archiving Marxan Results")
        m_application.StatusBar.ProgressBar.Message = "Archiving Marxan Results"
        MarxanProgressCh(65)
        m_application.StatusBar.ProgressBar.Position = 65

        Using zip As ZipFile = New ZipFile()
            zip.AddDirectory(pFolder + "\marxan")
            zip.Save(pFolder + "\temp\marxan.zip")
        End Using

        AddOutMessage("Archiving Marxan Results")
        m_application.StatusBar.ProgressBar.Message = "Archiving Marxan Results"
        MarxanProgressCh(75)
        m_application.StatusBar.ProgressBar.Position = 75

        Dim pMemoryBlobStreamzip As IMemoryBlobStream
        pMemoryBlobStreamzip = New MemoryBlobStream
        pMemoryBlobStreamzip.LoadFromFile(pFolder + "\temp\marxan.zip")

        Dim pMemoryBlobStreamgis As IMemoryBlobStream
        pMemoryBlobStreamgis = New MemoryBlobStream
        pMemoryBlobStreamgis.LoadFromFile(pFolder + "\Planning_Units.lyr")

        AddOutMessage("Archiving Marxan Results")
        m_application.StatusBar.ProgressBar.Message = "Archiving Marxan Results"
        MarxanProgressCh(90)
        m_application.StatusBar.ProgressBar.Position = 90

        Dim pFWS As IFeatureWorkspace
        Dim pWorkspaceFactory As IWorkspaceFactory
        pWorkspaceFactory = New FileGDBWorkspaceFactory
        pFWS = pWorkspaceFactory.OpenFromFile(pFolder + "\GIS_Data.gdb", 0)

        Dim pTable As ITable
        Dim pRow As IRow
        Dim pMemoryBlobStream As IMemoryBlobStream
        pMemoryBlobStream = New MemoryBlobStream
        pMemoryBlobStream.LoadFromFile(pFolder + "\marxan\input.dat")
        pTable = pFWS.OpenTable("Marxan_Runs")
        pRow = pTable.CreateRow
        pRow.Value(1) = crun
        pRow.Value(2) = crun
        pRow.Value(3) = costmin
        pRow.Value(4) = costmean
        pRow.Value(5) = pMemoryBlobStreamzip 'zip file
        pRow.Value(6) = pMemoryBlobStream 'input.dat file
        pRow.Value(7) = pMemoryBlobStreamgis 'GIS file
        pRow.Store()


        m_application.StatusBar.ProgressBar.Message = "Processing Complete"
        m_application.StatusBar.ProgressBar.Hide()
        MarxanProgressCh(0)
        m_application.StatusBar.ProgressBar.Position = 0

    End Sub

    Public Sub readOutput()

        ChangeProcessLabel("Data Update Progress")
        m_application.StatusBar.ProgressBar.Position = 0
        MarxanProgressCh(0)

        Dim di As DirectoryInfo = New DirectoryInfo(pFolder + "\marxan\output")
        Dim fi As FileInfo()
        Dim fii As FileInfo
        fi = di.GetFiles("*.txt")

        'Dim sConnectionString As String = "Driver={Microsoft Text Driver" + "(*.txt; *.csv)};Dbq=" & pFolder + "\marxan\input" & ";Extensions=txt;"
        'Dim objConn As New Odbc.OdbcConnection(sConnectionString)
        Dim objConn As New OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;" & _
            "Data Source=" & pFolder + "\marxan\output" & ";" & _
            "Extended Properties=""Text;HDR=Yes;FMT=Delimited""")
        objConn.Open()

        Dim d As New DataSet("output")

        For Each fii In fi
            If fii.Name = scname & "_ssoln.txt" Then
                Using adapter As New OleDbDataAdapter( _
                 "SELECT * FROM " & fii.Name & " ORDER BY planning_unit", objConn)
                    adapter.Fill(d, fii.Name.Replace(".txt", ""))
                End Using
            ElseIf Not fii.Name.Contains(scname & "_m") Then
                Using adapter As New OleDbDataAdapter( _
                "SELECT * FROM " & fii.Name, objConn)
                    adapter.Fill(d, fii.Name.Replace(".txt", ""))
                End Using
            End If
        Next

        Dim dt As DataTable = d.Tables(scname & "_ssoln")

        Dim r, p As Long
        Dim sw As StreamWriter = New StreamWriter(pFolder + "\marxan\output\" & scname & "_all.txt")

        Dim firstline As String = ""
        Dim costsum, costmean, costmin, costmax As Double
        costsum = 0
        costmax = Double.MinValue
        costmin = Double.MaxValue
        firstline = "planning_unit,Sum_of_Solutions,Best,"

        For p = 1 To nreps
            Dim cval As Double = CDbl(d.Tables(scname & "_sum").Rows(p - 1).Item(2))
            costsum = costsum + cval
            If costmax < cval Then costmax = cval
            If costmin > cval Then costmin = cval
            firstline = firstline + "Solution_" + CStr(p) + ","
        Next p
        firstline = firstline.Trim(",")
        costmean = costsum / CDbl(nreps)

        'Debug.Print(costmean.ToString + " " + costsum.ToString + " " + costmin.ToString)

        sw.WriteLine(firstline)

        AddOutMessage("Compiling Marxan Output Table")
        m_application.StatusBar.ProgressBar.Message = "Compiling Marxan Output Table"
        MarxanProgressCh(15)
        m_application.StatusBar.ProgressBar.Position = 15

        For r = 0 To dt.Rows.Count - 1
            Dim line As String = ""
            line = dt.Rows(r).Item(0).ToString + "," + dt.Rows(r).Item(1).ToString + "," + d.Tables(scname & "_best").Rows(r).Item(1).ToString + ","
            For p = 1 To nreps
                line = line + d.Tables(scname & "_r" + p.ToString.PadLeft(5, "0"c)).Rows(r).Item(1).ToString + ","
            Next p
            line = line.Trim(",")
            sw.WriteLine(line)
        Next r
        sw.Close()

        'addthis back to get saved output
        ' '' '' '' '' '' ''Using adapter As New OleDbDataAdapter( _
        ' '' '' '' '' '' ''    "SELECT * FROM " & "output_all.txt", objConn)
        ' '' '' '' '' '' ''    adapter.Fill(d, "output_all")
        ' '' '' '' '' '' ''End Using

        ' '' '' '' '' '' ''outdata = d

        'Dim dt As DataTable = outdata.Tables("output_ssoln")
        'Dim tab As ITable = CreateITable(dt)
        'MsgBox(tab.RowCount(Nothing))

        objConn.Close()
        ''''''''''''''''''''''ERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
        AddOutMessage("Reading Processed Outputs into GeoDatabase")
        m_application.StatusBar.ProgressBar.Message = "Reading Processed Outputs into GeoDatabase"
        MarxanProgressCh(35)
        m_application.StatusBar.ProgressBar.Position = 35

        Dim copyr As CopyRows = New CopyRows(pFolder + "\marxan\output\" & scname & "_all.txt", pFolder + "\GIS_Data.gdb\M_" + crun)
        GP.Execute(copyr, Nothing)

        'Dim addi As AddIndex = New AddIndex(pFolder + "\GIS_Data.gdb\M_" + crun, "planning_unit")
        'GP.Execute(addi, Nothing)

        AddOutMessage("Adding Output to Map and Symbolizing")
        m_application.StatusBar.ProgressBar.Message = "Adding Output to Map and Symbolizing"
        MarxanProgressCh(50)
        m_application.StatusBar.ProgressBar.Position = 50

        'JoinTable(GP.Open(pFolder + "\GIS_Data.gdb\M_" + crun))
        JoinGPTable()

        If Clustercheck.CheckState = CheckState.Checked Then

            Dim mfol As String = (pFolder + "\marxan").Replace("\", "\\")

            Dim pWriter As New System.IO.StreamWriter(pFolder + "\marxan\script.R")
            pWriter.WriteLine("sink('" + mfol + "\\output\\" & scname & "_Rlog.txt')")
            pWriter.WriteLine("")
            pWriter.WriteLine("library(rgl)")
            pWriter.WriteLine("library(vegan)")
            pWriter.WriteLine("library(labdsv)")
            pWriter.WriteLine("")
            pWriter.WriteLine("solutions<-read.table('" + mfol + "\\output\\" & scname & "_solutionsmatrix.csv',header=TRUE, row.name=1, sep=',')")
            pWriter.WriteLine("soldist<-vegdist(solutions,distance='bray')")
            pWriter.WriteLine("sol.mds<-nmds(soldist,2)")
            pWriter.WriteLine("")
            pWriter.WriteLine("bmp(file='" + mfol + "\\output\\" & scname & "_2d_plot.bmp',width=1756,height=1065,pointsize=10)")
            pWriter.WriteLine("")
            pWriter.WriteLine("plot(sol.mds$points, type='n', xlab='', ylab='', main='NMDS of solutions')")
            pWriter.WriteLine("text(sol.mds$points, labels=row.names(solutions))")
            pWriter.WriteLine("")
            pWriter.WriteLine("dev.off()")
            pWriter.WriteLine("")
            pWriter.WriteLine("h<-hclust(soldist, method='complete')")
            pWriter.WriteLine("")
            pWriter.WriteLine("bmp(file='" + mfol + "\\output\\" & scname & "_dendogram.bmp',width=1756,height=1065,pointsize=10)")
            pWriter.WriteLine("")
            pWriter.WriteLine("plot(h, xlab='Solutions', ylab='Disimilarity', main='Bray-Curtis dissimilarity of solutions')")
            pWriter.WriteLine("dev.off()")
            pWriter.WriteLine("")
            pWriter.WriteLine("usercut<-cutree(h,k=3)")
            pWriter.WriteLine("")
            pWriter.WriteLine("write('solution,cluster',file='" + mfol + "\\output\\" & scname & "_cluster.csv')")
            pWriter.WriteLine("")
            pWriter.WriteLine("for(i in 1:100)")
            pWriter.WriteLine("{")
            pWriter.WriteLine("   cat(i,file='" + mfol + "\\output\\" & scname & "_cluster.csv',append=TRUE)")
            pWriter.WriteLine("   cat(',',file='" + mfol + "\\output\\" & scname & "_cluster.csv',append=TRUE)")
            pWriter.WriteLine("   write(usercut[i],file='" + mfol + "\\output\\" & scname & "_cluster.csv',append=TRUE)")
            pWriter.WriteLine("}")
            pWriter.WriteLine("")
            pWriter.WriteLine("sol3d.mds<-nmds(soldist,3)")
            pWriter.WriteLine("")
            pWriter.WriteLine("plot3d(sol3d.mds$points, xlab = 'x', ylab = 'y', zlab = 'z', type='n', theta=40, phi=30, ticktype='detailed', main='NMDS of solutions')")
            pWriter.WriteLine("text3d(sol3d.mds$points,texts=row.names(solutions),pretty='TRUE')")
            pWriter.WriteLine("play3d(spin3d(axis=c(1,0,0), rpm=3), duration=10)")
            pWriter.WriteLine("play3d(spin3d(axis=c(0,1,0), rpm=3), duration=10)")
            pWriter.WriteLine("play3d(spin3d(axis=c(0,0,1), rpm=3), duration=10)")
            pWriter.WriteLine("")
            pWriter.WriteLine("")
            pWriter.Flush()
            pWriter.Close()

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

            If rplace <> "" Then

                Dim outrv As String
                outrv = "type  """ + pFolder + "\marxan\script.R""" + " | """ + rplace + """ --slave --vanilla"

                Dim rbat As String = pFolder + "\marxan\scriptR.bat"

                Dim tWriter As New System.IO.StreamWriter(rbat)
                tWriter.WriteLine(outrv)
                tWriter.Flush()
                tWriter.Close()

                Dim psInfo As New System.Diagnostics.ProcessStartInfo(rbat)

                psInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                Dim myProcess As Process = System.Diagnostics.Process.Start(psInfo)

                'System.Diagnostics.Process.Start("cmd /c ""start " + pFolder + "\marxan\output\output_dendogram.bmp" + Chr(34))
            End If

        End If

        'savecurrent()
        archivemrun(costmin, costmean)

        If Not IsNumeric(crun) Then
            'MsgBox("works")

            minchart.Add(costmin)
            maxchart.Add(costmean)

        End If

        'Me.BringToFront()
        'MsgBox("done")

        '''''''''''MsgBox(Clustercheck.

        checkdone()

        'Dim pGxCat As IGxCatalog, pGxLayer As IGxLayer
        'Dim pGxObj As IGxObject, pEnumGxObj As IEnumGxObject
        'Dim num As Long
        'Dim pValue As Object

        'pGxCat = New GxCatalog
        'pValue = pGxCat.GetObjectFromFullName(pFolder + "\Planning_Units.lyr", num)

        'If TypeOf pValue Is IEnumGxObject Then
        '    pEnumGxObj = pValue
        '    pGxObj = pEnumGxObj.Next
        'Else
        '    pGxObj = pValue
        'End If

        'pGxLayer = pGxObj
        'Dim pGFLayer As IGeoFeatureLayer
        'pGFLayer = pGxLayer.Layer

        'Dim pMxDoc As IMxDocument
        'Dim pMap As IMap
        'pMxDoc = mApp.Document
        'pMap = pMxDoc.FocusMap

        'pMap.AddLayer(pGFLayer)

    End Sub

    Public Sub checkdone()

        ''''add overlay with information

        If (pile.Count <> 0) Then
            'MsgBox(pile(0))

            AddOutMessage("Calibration Step Complete, " + CStr(pile.Count) + " Steps Remaining")

            Dim tWriter As New System.IO.StreamWriter(pFolder + "\temp\input.dat")

            Dim iRead As New System.IO.StreamReader(pFolder + "\marxan\input.dat")
            Dim line As String
            line = iRead.ReadLine()
            Do While Not line Is Nothing
                If line.ToUpper.Contains(curp) Then line = curp + " " + CStr(pile(0))
                tWriter.WriteLine(line)
                line = iRead.ReadLine()
            Loop

            iRead.Close()
            tWriter.Close()

            Dim tRead As New System.IO.StreamReader(pFolder + "\temp\input.dat")
            ChangeInputTB(tRead.ReadToEnd()) 'inputdatfile.Text = tRead.ReadToEnd()
            tRead.Close()

            makeDataReady()
            lrun = crun
            crun = ttime + curp + "_" + CStr(pile(0)).Replace(".", "p")
            pile.RemoveAt(0)
            runMarxanProgram()

        Else

            If (valmod <> "") Then
                Dim meanstr As String = ""
                Dim minstr As String = ""
                Dim meanmin As Double = 999999999999999999
                Dim meanmax As Double = -999999999999999999
                Dim minmin As Double = 999999999999999999
                Dim minmax As Double = -999999999999999999

                Dim md As Double
                For Each md In minchart
                    If (md > minmax) Then minmax = md
                    If (md < minmin) Then minmin = md
                Next

                For Each md In maxchart
                    If (md > meanmax) Then meanmax = md
                    If (md < meanmin) Then meanmin = md
                Next

                Dim mmax As Double
                Dim mmin As Double

                If minmin < meanmin Then
                    mmin = minmin
                Else
                    mmin = meanmin
                End If

                If meanmax > minmax Then
                    mmax = meanmax
                Else
                    mmax = minmax
                End If

                For Each md In minchart
                    minstr = minstr + CStr(CInt(((md - mmin) / (mmax - mmin)) * 100)) + ","
                Next

                For Each md In maxchart
                    meanstr = meanstr + CStr(CInt(((md - mmin) / (mmax - mmin)) * 100)) + ","
                Next

                'MsgBox(minstr.TrimEnd(","))
                'MsgBox(meanstr.TrimEnd(","))
                Dim stringtofeed As String = "-1|" + minstr.TrimEnd(",") + "|-1|" + meanstr.TrimEnd(",")

                Dim pathstring As String

                pathstring = pFolder + "\marxan\calibrate.png"

                createGraphImage("http://chart.apis.google.com/chart?cht=lxy&chs=650x450&chd=t:" & stringtofeed & "&chls=3,6,3|5&chxs=0,000055,14,0,lt|1,0000ff,14,1,lt|2,004466,14,0,lt|3,0000ff,14,1,lt&chxr=" & "0," & CStr(valchart(0)) & "," & CStr(valchart(valchart.Count - 1)) + "," & CStr(valchart(1) - valchart(0)) & "|2," & CStr(mmin) & "," & CStr(mmax) & "," & CStr(CInt((mmax - mmin) / 10)) + "&chxt=x,x,y,y&chxl=1:|" & CStr(valmod) & "|3:|Cost&chxp=1,50|3,50&chm=o,0066FF,0,-1,6|o,0066FF,1,-1,6&chco=FF0000,00FF00&chdl=Best%20Cost%20|Average%20Cost", pathstring)

                'System.Diagnostics.Process.Start("cmd /c ""start " + pathstring + Chr(34))

            End If

            isCompleted(False)
            savecurrent()
            valmod = ""
        End If

    End Sub

    Friend Function CreateITable(ByVal dt As DataTable) As ITable
        Dim table As ITable = Nothing
        Dim rowBuffer As IRowBuffer = Nothing
        Dim cursor As ICursor = Nothing
        Dim scratchWorkspaceFact As IScratchWorkspaceFactory = Nothing
        Dim workSpace As IFeatureWorkspace = Nothing
        Dim CLSID As UID = Nothing
        Dim fieldEdit As IFieldEdit = Nothing
        Dim fieldsEdit As IFieldsEdit = Nothing

        table = New StandaloneTableClass()

        'create scratch workspace to store table
        scratchWorkspaceFact = New ScratchWorkspaceFactory()
        workSpace = TryCast(scratchWorkspaceFact.CreateNewScratchWorkspace(), IFeatureWorkspace)

        CLSID = New UID()
        CLSID.Value = "esriGeoDatabase.Object"

        fieldsEdit = New FieldsClass()
        fieldsEdit.FieldCount_2 = dt.Columns.Count

        'add fields to table
        For Each dc As DataColumn In dt.Columns
            fieldEdit = New FieldClass()
            fieldEdit.AliasName_2 = dc.ColumnName
            fieldEdit.Name_2 = dc.ColumnName

            'Added by D. Geasan
            'Determine the data type of the current data table column.
            'Determine the matching ESRI data type for the FieldEdit object.
            Dim strDataType As String = dc.DataType.Name.ToUpper()

            Select Case strDataType
                Case "STRING"
                    fieldEdit.Type_2 = esriFieldType.esriFieldTypeString
                    Exit Select
                Case "SINGLE"
                    fieldEdit.Type_2 = esriFieldType.esriFieldTypeSingle
                    Exit Select
                Case "INT16"
                    fieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger
                    Exit Select
                Case "DATETIME"
                    fieldEdit.Type_2 = esriFieldType.esriFieldTypeDate
                    Exit Select
                Case "INT32"
                    fieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
                    Exit Select
                Case "DOUBLE"
                    fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble
                    Exit Select
                Case Else
                    Exit Select
            End Select


            fieldsEdit.Field_2(dc.Ordinal) = fieldEdit
        Next

        'create table
        table = workSpace.CreateTable(dt.TableName, fieldsEdit, CLSID, Nothing, "")

        'add rows to table
        rowBuffer = table.CreateRowBuffer()
        cursor = table.Insert(True)

        For Each dr As DataRow In dt.Rows
            For x As Integer = 1 To dt.Columns.Count - 1
                'foreach column set values skip OID
                'Debug.WriteLine(dr[x]);
                rowBuffer.Value(x) = dr(x)
            Next
            cursor.InsertRow(rowBuffer)
        Next

        'update table
        cursor.Flush()
        Return table
    End Function

    Public Sub JoinGPTable()

        'Dim pGxCat As IGxCatalog, pGxLayer As IGxLayer
        'Dim pGxObj As IGxObject, pEnumGxObj As IEnumGxObject
        'Dim num As Long
        'Dim pValue As Object

        'pGxCat = New GxCatalog
        'pValue = pGxCat.GetObjectFromFullName(pFolder + "\Planning_Units.lyr", num)

        'If TypeOf pValue Is IEnumGxObject Then
        '    pEnumGxObj = pValue
        '    pGxObj = pEnumGxObj.Next
        'Else
        '    pGxObj = pValue
        'End If

        'pGxLayer = pGxObj
        'Dim pGFLayer As IGeoFeatureLayer
        'pGFLayer = pGxLayer.Layer

        'Dim pfrend As IClassBreaksRenderer

        'pfrend = pGFLayer.Renderer
        'pfrend.Field = "M_" + crun + ".Best"

        'Dim layerFile As ILayerFile = New LayerFileClass()
        ''Create a new layer file.
        'layerFile.New(pFolder + "\Planning_Units.lyr")

        ''Bind the layer file with the layer from the map.
        'layerFile.ReplaceContents(pGFLayer)
        'layerFile.Save()

        'GP.AddOutputsToMap = True

        Dim adlay As MakeFeatureLayer = New MakeFeatureLayer(pFolder + "\GIS_Data.gdb\Planning_Units", "PU_" + scname + " RUNID:" + crun) ' "PU_Run_" + crun
        GP.Execute(adlay, Nothing)

        'GP.AddOutputsToMap = True

        Dim mtv As MakeTableView = New MakeTableView(pFolder + "\GIS_Data.gdb\M_" + crun, "test")
        GP.Execute(mtv, Nothing)

        Dim addj As AddJoin = New AddJoin("PU_" + scname + " RUNID:" + crun, "MARXAN_PUID", "test", "planning_unit")
        GP.Execute(addj, Nothing)

        Dim save2layer As SaveToLayerFile = New SaveToLayerFile("PU_" + scname + " RUNID:" + crun, pFolder + "\PUTEMP.lyr")
        GP.Execute(save2layer, Nothing)

        'Dim applysym As ApplySymbologyFromLayer = New ApplySymbologyFromLayer("PU_Run_" + crun, pFolder + "\Planning_Units.lyr")
        'GP.Execute(applysym, Nothing)

        swaplayers()

    End Sub

    Private Function Dodefaultsymbol() As IUniqueValueRenderer

        Dim pfrend As IUniqueValueRenderer, n As Long
        pfrend = New UniqueValueRenderer

        Dim symd As ISimpleFillSymbol
        symd = New SimpleFillSymbol
        symd.Style = esriSimpleFillStyle.esriSFSSolid
        symd.Outline = Nothing

        '** These properties should be set prior to adding values
        pfrend.FieldCount = 1
        pfrend.Field(0) = "M_" + crun + ".Best"
        pfrend.DefaultSymbol = symd
        pfrend.UseDefaultSymbol = False

        Dim RgbClr1 As ESRI.ArcGIS.Display.RgbColor = New ESRI.ArcGIS.Display.RgbColorClass
        With RgbClr1
            .Red = 100
            .Green = 185
            .Blue = 125
        End With

        Dim symx As ISimpleFillSymbol
        symx = New SimpleFillSymbol
        symx.Style = esriSimpleFillStyle.esriSFSSolid
        symx.Outline = Nothing
        symx.Color = RgbClr1

        pfrend.AddValue("0", "Best", symx)

        Dim RgbClr2 As ESRI.ArcGIS.Display.RgbColor = New ESRI.ArcGIS.Display.RgbColorClass
        With RgbClr2
            .Red = 10
            .Green = 85
            .Blue = 160
        End With

        Dim symx2 As ISimpleFillSymbol
        symx2 = New SimpleFillSymbol
        symx2.Style = esriSimpleFillStyle.esriSFSSolid
        symx2.Outline = Nothing
        symx2.Color = RgbClr2

        pfrend.AddValue("1", "Best", symx2)

        Return pfrend

    End Function

    Public Sub swaplayers()

        Dim title As String
        Dim pgfl1 As IGeoFeatureLayer
        Dim pgfl2 As IGeoFeatureLayer
        'pgfl2 = openlayerfilefromname(pFolder + "\Planning_Units.lyr")
        pgfl2 = GP.Open(pFolder + "\Planning_Units.lyr")
        'pgfl1 = openlayerfilefromname(pFolder + "\PUTEMP.lyr")
        pgfl1 = GP.Open(pFolder + "\PUTEMP.lyr")

        Dim plegi As ILegendInfo

        If TypeOf pgfl2.Renderer Is IClassBreaksRenderer Then

            Dim pfrend As IClassBreaksRenderer
            pfrend = pgfl2.Renderer
            Dim f As String
            f = pfrend.Field.Split(".")(1)
            'MsgBox(f)
            pfrend.Field = "M_" + crun + "." + f
            'MsgBox(pfrend.Field)
            pgfl1.Renderer = pfrend
            plegi = pgfl1
            plegi.LegendGroup(0).Heading = f

        ElseIf TypeOf pgfl2.Renderer Is IUniqueValueRenderer Then

            Dim pfrend As IUniqueValueRenderer
            pfrend = pgfl2.Renderer

            Dim i As Integer
            Dim f As String
            For i = 0 To pfrend.FieldCount - 1
                f = pfrend.Field(i).Split(".")(1)
                pfrend.Field(0) = "M_" + crun + "." + f
            Next
            pgfl1.Renderer = pfrend
            'plegi = pgfl1
            'plegi.LegendGroup(0).Heading = pfrend.Field(i).Split(".")(0)

        ElseIf TypeOf pgfl2.Renderer Is ISimpleRenderer Then

            MsgBox("This tool symbolizes each Marxan run using the current symbology of the previous run. " + vbCrLf + "If you symbolize the output of this run, before running the next run, the output will be pre-symbolized. " + vbCrLf + "Only class breaks and unique value symbol types are supported.", MsgBoxStyle.Exclamation)
            pgfl1.Renderer = Dodefaultsymbol()

        Else

            MsgBox("This tool symbolizes each Marxan run using the current symbology of the previous run. " + vbCrLf + "If you symbolize the output of this run, before running the next run, the output will be pre-symbolized. " + vbCrLf + "Only class breaks and unique value symbol types are supported.", MsgBoxStyle.Exclamation)
            pgfl1.Renderer = Dodefaultsymbol()

        End If

        Dim pdl As IDataLayer
        pdl = pgfl1
        pdl.RelativeBase = pFolder + "\GIS_Data.gdb"
        Dim pl As ILayer
        pl = pdl
        pl.Name = "PU_" + scname + " RUNID:" + crun '"PU_Run_" + crun

        'pl.descripting

        'Create a new LayerFile instance.
        Dim layerFile As ILayerFile = New LayerFileClass()
        'Create a new layer file.
        layerFile.New(pFolder + "\Planning_Units.lyr")

        'Bind the layer file with the layer from the map.
        layerFile.ReplaceContents(pl)
        layerFile.Save()

        Dim player As ILayer
        player = GP.Open(pFolder + "\Planning_Units.lyr")
        Dim pMxDoc As IMxDocument
        Dim pMap As IMap
        pMxDoc = mApp.Document
        pMap = pMxDoc.FocusMap

        Dim pEnumLayer As IEnumLayer
        Dim play As ILayer
        Dim groupylayer As IGroupLayer

        pMxDoc = mApp.Document
        pMap = pMxDoc.FocusMap
        pEnumLayer = pMap.Layers(Nothing, True)
        pEnumLayer.Reset()
        play = pEnumLayer.Next
        Do While Not play Is Nothing
            If Path.GetFileName(pFolder) = play.Name Then
                If TypeOf play Is IGroupLayer Then
                    groupylayer = play
                    Exit Do
                End If
            End If
            play = pEnumLayer.Next
        Loop

        If Not groupylayer Is Nothing Then
            Dim pcomplay As ICompositeLayer
            pcomplay = groupylayer
            Dim curlay As List(Of ILayer) = New List(Of ILayer)
            Dim i As Long = 0
            For i = 0 To pcomplay.Count - 1
                curlay.Add(pcomplay.Layer(i))
            Next
            groupylayer.Clear()
            groupylayer.Add(player)
            Dim clay As ILayer
            For Each clay In curlay
                groupylayer.Add(clay)
            Next

            layerFile.New(pFolder + "\Project.lyr")
            layerFile.ReplaceContents(groupylayer)
            layerFile.Save()
        Else
            pMap.AddLayer(player)
        End If

        pMxDoc.UpdateContents()
        pMxDoc.ActiveView.Refresh()

        System.IO.File.Delete(pFolder + "\PUTEMP.lyr")

    End Sub

    Public Function openlayerfilefromname(ByVal inname As String) As IGeoFeatureLayer

        Dim pGxCat As IGxCatalog, pGxLayer As IGxLayer
        Dim pGxObj As IGxObject, pEnumGxObj As IEnumGxObject
        Dim num As Long
        Dim pValue As Object

        pGxCat = New GxCatalog
        pValue = pGxCat.GetObjectFromFullName(inname, num)

        If TypeOf pValue Is IEnumGxObject Then
            pEnumGxObj = pValue
            pGxObj = pEnumGxObj.Next
        Else
            pGxObj = pValue
        End If

        pGxLayer = pGxObj
        Dim pGFLayer As IGeoFeatureLayer
        pGFLayer = pGxLayer.Layer
        Return pGFLayer

    End Function

    Public Sub JoinTable(ByVal ptable As ITable)

        Dim pMxDoc As IMxDocument
        Dim pMap As IMap
        Dim pLayer As IGeoFeatureLayer
        Dim pDpyRC As IDisplayRelationshipClass
        Dim pMemRCFact As IMemoryRelationshipClassFactory
        Dim pRelClass As IRelationshipClass

        'pMxDoc = mApp.Document
        'pMap = pMxDoc.FocusMap
        '' get a reference to the layer
        'pLayer = pMap.Layer(0)
        'pDpyRC = pLayer

        Dim pEnumLayer As IEnumLayer
        Dim play As ILayer
        Dim groupylayer As IGroupLayer
        Dim pId As New UID

        pMxDoc = mApp.Document
        pMap = pMxDoc.FocusMap
        pEnumLayer = pMap.Layers(Nothing, True)
        pEnumLayer.Reset()
        play = pEnumLayer.Next
        Do While Not play Is Nothing
            If Path.GetFileName(pFolder) = play.Name Then
                groupylayer = play
                Exit Do
            End If
            play = pEnumLayer.Next
        Loop

        Dim pflay As IFeatureLayer = New FeatureLayer

        Dim pfc As IFeatureClass
        pfc = GP.Open(pFolder + "\GIS_Data.gdb\Planning_Units")
        pflay.FeatureClass = pfc

        Dim pdl As IDataLayer
        pdl = pflay
        pdl.RelativeBase = pFolder + "\GIS_Data.gdb"
        Dim pl As ILayer
        pl = pdl
        pl.Name = "PU_" + scname + " RUNID:" + crun '"PU_Run_" + crun

        pLayer = pl

        pDpyRC = pLayer

        ' create a relationship class in memory
        pMemRCFact = New MemoryRelationshipClassFactory
        pRelClass = pMemRCFact.Open("M_" + crun, ptable, "planning_unit", pLayer.FeatureClass, _
                            "MARXAN_PUID", "Forwards", "Backwards", esriRelCardinality.esriRelCardinalityOneToOne)

        ' perform the join

        pDpyRC.DisplayRelationshipClass(pRelClass, esriJoinType.esriLeftInnerJoin)

        'GP.AddOutputsToMap = True

        'Dim adlay As MakeFeatureLayer = New MakeFeatureLayer(pFolder + "\GIS_Data.gdb\Planning_Units", "PU_Run_" + crun)
        'GP.Execute(adlay, Nothing)

        'GP.AddOutputsToMap = False

        'Dim mtv As MakeTableView = New MakeTableView(pFolder + "\GIS_Data.gdb\M_" + crun, "test")
        'GP.Execute(mtv, Nothing)

        'Dim addj As AddJoin = New AddJoin("PU_Run_" + crun, "MARXAN_PUID", "test", "planning_unit")
        'GP.Execute(addj, Nothing)

        'pMxDoc = mApp.Document
        'pMap = pMxDoc.FocusMap
        'pLayer = pMap.Layer(1)

        Dim pGxCat As IGxCatalog, pGxLayer As IGxLayer
        Dim pGxObj As IGxObject, pEnumGxObj As IEnumGxObject
        Dim num As Long
        Dim pValue As Object

        pGxCat = New GxCatalog
        pValue = pGxCat.GetObjectFromFullName(pFolder + "\Planning_Units.lyr", num)

        If TypeOf pValue Is IEnumGxObject Then
            pEnumGxObj = pValue
            pGxObj = pEnumGxObj.Next
        Else
            pGxObj = pValue
        End If

        pGxLayer = pGxObj
        Dim pGFLayer As IGeoFeatureLayer
        pGFLayer = pGxLayer.Layer

        'pMap.AddLayer(pGFLayer)

        'Dim pfeatlayer As IFeatureLayer
        'pfeatlayer = pMap.Layer(1)

        Dim plg As ILegendInfo
        plg = pGFLayer
        Dim title As String
        title = plg.LegendGroup(0).Heading

        'Dim pgflayer As IGeoFeatureLayer
        Dim pgflayer2 As IGeoFeatureLayer
        pgflayer2 = pLayer
        'pgflayer2 = pfeatlayer
        pgflayer2.Renderer = pGFLayer.Renderer

        Dim pfrend As IClassBreaksRenderer

        pfrend = pgflayer2.Renderer
        pfrend.Field = "M_" + crun + ".Best"

        Dim plegi As ILegendInfo
        plegi = pgflayer2

        plegi.LegendGroup(0).Heading = title

        pgflayer2 = plegi

        'Create a new LayerFile instance.
        Dim layerFile As ILayerFile = New LayerFileClass()
        'Create a new layer file.
        layerFile.New(pFolder + "\Planning_Units.lyr")

        'Bind the layer file with the layer from the map.
        layerFile.ReplaceContents(pgflayer2)
        layerFile.Save()

        'pMxDoc.UpdateContents()
        'pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, pgflayer2, Nothing)
        'pMxDoc.ActiveView.Refresh()

        'If Not groupylayer Is Nothing Then
        '''pMap.DeleteLayer(pgflayer2)
        '    groupylayer.Add(pgflayer2)
        'End If

        'pMxDoc.UpdateContents()
        'pMxDoc.ActiveView.Refresh()



        'pgflayer.DisplayField = "Best"

        'Dim prend As IFeatureRenderer


        ''Dim arrayVals As Double()
        ''Dim arrayFreqs As Long()

        'Dim classifier As IClassifyGEN = New NaturalBreaksClass()
        'Dim histogram As ITableHistogram = New TableHistogramClass()
        'Dim hist As IHistogram = TryCast(histogram, IHistogram)
        'histogram.Field = "Best"
        'histogram.Table = pTable 'TryCast(pfeatlayer.FeatureClass, ITable)
        'Dim arrayVals As Object
        'Dim arrayFreqs As Object
        'hist.GetHistogram(arrayVals, arrayFreqs)
        'Dim breaks As Integer = 2
        'classifier.Classify(arrayVals, arrayFreqs, breaks)
        'Dim classBreaks As Double() = DirectCast(classifier.ClassBreaks, Double())

        ''class breaks and the field to perform the class breaks on.
        'Dim pClassBreaksRenderer As IClassBreaksRenderer
        'pClassBreaksRenderer = New ClassBreaksRenderer
        'pClassBreaksRenderer.Field = "Best"

        ''First class break is the minimum value for the class breaks renderer
        'pClassBreaksRenderer.MinimumBreak = classBreaks(0)

        ''Set number of breaks to be number of classes returned from classification
        'pClassBreaksRenderer.BreakCount = 2


        'Dim pFillSymbol As IFillSymbol

        'Dim RgbClr1 As ESRI.ArcGIS.Display.RgbColor = New ESRI.ArcGIS.Display.RgbColorClass
        'With RgbClr1
        '    .Red = 100
        '    .Green = 185
        '    .Blue = 125
        'End With

        'pFillSymbol = New SimpleFillSymbol
        'pFillSymbol.Color = RgbClr1
        'pClassBreaksRenderer.Symbol(0) = pFillSymbol

        'Dim RgbClr2 As ESRI.ArcGIS.Display.RgbColor = New ESRI.ArcGIS.Display.RgbColorClass
        'With RgbClr2
        '    .Red = 10
        '    .Green = 85
        '    .Blue = 160
        'End With

        'pFillSymbol = New SimpleFillSymbol
        'pFillSymbol.Color = RgbClr2
        'pClassBreaksRenderer.Symbol(1) = pFillSymbol

        ''Dim breakindex As Integer
        ''For breakIndex = 0 To pClassBreaksRenderer.BreakCount - 1
        ''    'Retrieve a color and set up a fill symbol,
        ''    'put this in the symbol array corresponding to the class value
        ''    Dim pcolor As IColor = New 
        ''    pColor = pEnumColors.Next


        ' ''Set the break value - note this is the highest value in the class
        ''pClassBreaksRenderer.Break(breakIndex) = ClassBreaksArray(breakIndex + 1)
        ''Next breakIndex

        ''type_defs.QuantileClassBreaks(pLayer, "Best", 2, mApp)

        'Dim pgflayer As IGeoFeatureLayer
        'pgflayer = pLayer

        ''pgflayer.DisplayField = "Best"

        ''Dim prend As IFeatureRenderer

        'pgflayer.Renderer = pClassBreaksRenderer



        ''Dim pFWS As IFeatureWorkspace
        ''Dim pWorkspaceFactory As IWorkspaceFactory
        ''pWorkspaceFactory = New FileGDBWorkspaceFactory
        ''pFWS = pWorkspaceFactory.OpenFromFile(pFolder + "\GIS_Data.gdb", 0)

        ' '' create a relationship class in memory
        ''pMemRCFact = New MemoryRelationshipClassFactory
        ''pRelClass = pMemRCFact.Open("TEST", pTable, "planning_unit", pLayer.FeatureClass, _
        ''                    "MARXAN_PUID", "Forwards", "Backwards", esriRelCardinality.esriRelCardinalityOneToOne)

        ' '' perform the join

        ''pDpyRC.DisplayRelationshipClass(pRelClass, esriJoinType.esriLeftOuterJoin)

        ''Dim layerFile As ILayerFile = New LayerFileClass()
        ' ''Create a new layer file.
        ''layerFile.New("c:\temp\test.lyr")
        ''layerFile.ReplaceContents(pLayer)

        ''layerFile.Save()

        ''Dim pl As IFeatureLayer
        ''pl = GPU.OpenFeatureLayerFromString("c:\temp\test.lyr")
        ''pMap.AddLayer(pl)


    End Sub

    Public Sub JoinWithGDBRelate()
        ' the following code performs a layer join using a predefined relationship in a
        ' geodatabase. if the layer's feature class participates in more than one
        ' relationship class, then the join will be created based on the first
        ' relationship class found in the collection.

        Dim pMxDoc As IMxDocument
        Dim pMap As IMap
        Dim pLayer As IGeoFeatureLayer
        Dim pDpyRC As IDisplayRelationshipClass
        Dim pFeatClass As IFeatureClass
        Dim pEnumRC As IEnumRelationshipClass
        Dim pRelClass As IRelationshipClass

        pMxDoc = mApp.Document
        pMap = pMxDoc.FocusMap

        ' get a reference to the layer
        pLayer = pMap.Layer(0)
        pDpyRC = pLayer

        ' get the relationship class from the layer's feature class
        pFeatClass = pLayer.FeatureClass
        pEnumRC = pFeatClass.RelationshipClasses(esriRelRole.esriRelRoleAny)
        ' get the first relationship class in the collection
        pRelClass = pEnumRC.Next

        ' perform the join
        pDpyRC.DisplayRelationshipClass(pRelClass, esriJoinType.esriLeftOuterJoin)

    End Sub

    Public Function MakeTableQuery(ByVal queryDef As IQueryDef, ByVal keyFields As String, ByVal makeCopy As Boolean, ByVal workspaceName As IWorkspaceName, ByVal tableName As String) As ITable

        ' Make the new TableQueryName.
        Dim queryName2 As IQueryName2 = CType(New TableQueryNameClass(), IQueryName2)
        queryName2.QueryDef = queryDef
        queryName2.PrimaryKey = keyFields
        queryName2.CopyLocally = makeCopy

        ' Set the workspace and name of the new QueryTable.
        Dim datasetName As IDatasetName = CType(queryName2, IDatasetName)
        datasetName.WorkspaceName = workspaceName
        datasetName.Name = tableName

        ' Open and return the table.
        Dim Name As IName = CType(queryName2, IName)
        Dim table As ITable = CType(Name.Open(), ITable)

        Return table

    End Function


    Private Sub PullSelectionFromMapToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PullSelectionFromMapToolStripMenuItem.Click
        '''put code to get data

        Try

            Dim pMxDoc As IMxDocument
            Dim pMap As IMap
            Dim pActiveView As IActiveView

            pMxDoc = mApp.Document
            pActiveView = pMxDoc.ActiveView
            pMap = pMxDoc.FocusMap

            Dim pef As IEnumFeature
            pef = pActiveView.Selection
            Dim pefs As IEnumFeatureSetup
            pefs = pef
            pefs.AllFields = True
            pefs.Recycling = False
            pef = pefs
            pef.Reset()

            Dim pfeat As IFeature
            pfeat = pef.Next()
            Dim sels As List(Of Long) = New List(Of Long)
            Dim indy As Integer

            While Not pfeat Is Nothing
                Dim pDatalayer As IDataset
                pDatalayer = pfeat.Table
                If pDatalayer.Workspace.PathName = pFolder + "\GIS_Data.gdb" Then
                    indy = (pfeat.Fields.FindField("MARXAN_PUID"))
                    sels.Add(CLng(pfeat.Value(indy)))
                End If
                pfeat = pef.Next()
            End While

            mFiles.Text = "pu"

            Dim i As Long
            For i = 0 To mainGrid.RowCount - 1
                If sels.Contains(CLng(mainGrid.Rows(i).Cells(0).Value)) Then mainGrid.Rows(i).Selected = True Else mainGrid.Rows(i).Selected = False
            Next

        Catch

            MsgBox("You must have a valid selection.", MsgBoxStyle.Exclamation)

        End Try


    End Sub

    Private Sub RevertToLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RevertToLToolStripMenuItem.Click
        readProject()
    End Sub

    Private Sub SingleRunSameAsGreenButtonToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SingleRunSameAsGreenButtonToolStripMenuItem.Click

        makeDataReady()
        lrun = crun
        crun = Now.ToString("yyyyMMddHHmmss")
        runMarxanProgram()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If IsNumeric(TextBox1.Text) Then

            Dim i As Long
            For i = 0 To mainGrid.RowCount - 1
                If mainGrid.SelectedRows.Count = 0 Then
                    mainGrid.Rows(i).Cells(mcollist.SelectedIndex + 1).Value = TextBox1.Text
                Else
                    If mainGrid.Rows(i).Selected = True Then mainGrid.Rows(i).Cells(mcollist.SelectedIndex + 1).Value = TextBox1.Text
                End If
            Next

        Else

            MsgBox("Entered value is non-numeric.", MsgBoxStyle.Exclamation)

        End If

    End Sub


    Private Sub CalibrToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalibrToolStripMenuItem.Click
        Panel2.Visible = True
        Panel2.Top = 0
        Panel2.Left = 0

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Panel2.Visible = False
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click


        If IsNumeric(Calmin.Text) And IsNumeric(Calmax.Text) And IsNumeric(CalStep.Text) Then

            Dim loopmin, loopmax, loopstep, ranger As Double

            loopmin = CDbl(Calmin.Text)
            loopmax = CDbl(Calmax.Text)
            loopstep = CDbl(CalStep.Text)
            ranger = loopmax - loopmin
            Dim nsteps As Long
            nsteps = CLng(ranger / loopstep)

            'Dim i As Long

            Dim iWriter As New System.IO.StreamWriter(pFolder + "\marxan\input.dat")
            iWriter.Write(inputdatfile.Text)
            iWriter.Close()

            Dim i As Double
            'loopend = False

            pile = New List(Of Double)
            valchart = New List(Of Double)

            For i = loopmin To loopmax Step loopstep
                'loopend = False
                pile.Add(i)
                valchart.Add(i)
            Next

            curp = ParameterText.Text.ToUpper

            ttime = Now.ToString("yyyyMMddHHmmss_")

            minchart = New List(Of Double)
            maxchart = New List(Of Double)
            valmod = ParameterText.Text

            checkdone()

            'While loopend = False
            ' System.Threading.Thread.Sleep(1000)
            ' End While
            '
            'savecurrent()

            ProcessPanel.Top = 0
            ProcessPanel.Left = 0
            ProcessPanel.Visible = True
            Panel2.Visible = False



        Else

            MsgBox("Calibration Parameters are non-numeric", MsgBoxStyle.Critical)

        End If

    End Sub

    Private Sub SaveProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveProjectToolStripMenuItem.Click
        savecurrent()
    End Sub

    Private Sub MDS_Form_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.cld = True

        Dim pMxDoc As IMxDocument
        Dim pMap As IMap

        pMxDoc = mApp.Document
        pMap = pMxDoc.FocusMap

        Dim pEnumLayer As IEnumLayer
        Dim play As ILayer
        Dim groupylayer As IGroupLayer

        pEnumLayer = pMap.Layers(Nothing, True)
        pEnumLayer.Reset()
        play = pEnumLayer.Next
        Do While Not play Is Nothing
            If Path.GetFileName(pFolder) = play.Name Then
                If TypeOf play Is IGroupLayer Then
                    pMap.DeleteLayer(play)
                    Exit Do
                End If
            End If
            play = pEnumLayer.Next
        Loop

    End Sub

    Private Sub RevertToPreviousMarxanRunToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RevertToPreviousMarxanRunToolStripMenuItem.Click

        ExPanel.Visible = True

        Dim ptab As ITable
        ptab = GP.Open(pFolder + "\GIS_Data.gdb\Marxan_Runs")
        Dim prow As IRow
        Dim runid As String
        Dim runlab As String
        Dim bc As Double
        Dim mc As Double
        Dim pcur As ICursor

        pcur = ptab.Search(Nothing, False)
        prow = pcur.NextRow

        Dim wr As StreamWriter = New StreamWriter(pFolder + "\temp\mruns.txt")
        wr.WriteLine("RUN_ID" + "," + "Label" + "," + "Best_Cost" + "," + "Mean_Cost")

        While Not prow Is Nothing
            runid = prow.Value(prow.Fields.FindField("RUNID"))
            runlab = prow.Value(prow.Fields.FindField("Run_Label"))
            bc = prow.Value(prow.Fields.FindField("Best_Cost"))
            mc = prow.Value(prow.Fields.FindField("Mean_Cost"))

            wr.WriteLine(Chr(34) & runid & Chr(34) & "," & runlab + "," & CStr(bc) & "," & CStr(mc))

            'If flist.Keys.Contains("M_" + Val()) Then
            '    pLayer = flist.Item("M_" + Val())

            '    Dim pMemoryBlobStreamgis As IMemoryBlobStream
            '    pMemoryBlobStreamgis = New MemoryBlobStream
            '    pMemoryBlobStreamgis.LoadFromFile(pFolder + "\Planning_Units.lyr")

            '    pdl = pLayer
            '    pdl.RelativeBase = pFolder + "\GIS_Data.gdb"

            '    'Create a new LayerFile instance.
            '    LayerFile = New LayerFileClass()
            '    'Create a new layer file.
            '    LayerFile.New(pFolder + "\Planning_Units.lyr")
            '    LayerFile.ReplaceContents(pdl)
            '    LayerFile.Save()

            '    prow.Value(prow.Fields.FindField("GIS_Layer")) = pMemoryBlobStreamgis
            '    prow.Value(prow.Fields.FindField("RUN_Label")) = pLayer.Name
            '    prow.Store()
            '    pcur.UpdateRow(prow)

            'End If
            prow = pcur.NextRow()
        End While

        ptab = Nothing

        wr.Flush()
        wr.Close()


        'mainGrid.DataSource = data.Tables(mFiles.Text)


        'Dim di As DirectoryInfo = New DirectoryInfo(pFolder + "\marxan\input")
        'Dim fi As FileInfo()
        'Dim fii As FileInfo
        'fi = di.GetFiles()

        Dim objConn As New OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0;" & _
        "Data Source=" & pFolder + "\temp" & ";" & _
            "Extended Properties=""Text;HDR=Yes;FMT=Delimited""")
        objConn.Open()

        Dim d As New DataSet("MarxanRuns")

        'For Each fii In fi
        Using adapter As New OleDbDataAdapter( _
         "SELECT * FROM " & "mruns.txt", objConn)
            adapter.Fill(d, "mruns")
        End Using
        '    mFiles.Items.Add(fii.Name.Replace(".txt", ""))
        'Next

        'data = d

        'mFiles.Text = "pu"

        objConn.Close()

        ExGrid.DataSource = d.Tables("mruns")



    End Sub

    Private Sub Clustercheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clustercheck.Click

        If Clustercheck.CheckState = CheckState.Checked Then
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

            If rplace = "" Then
                MsgBox("R is either not installed, or the path is not specified in Options --> Settings on the PAT toolbar", MsgBoxStyle.Exclamation)
                Clustercheck.CheckState = CheckState.Unchecked
            End If
        End If
    End Sub


    Public Sub createGraphImage(ByRef url As String, ByVal path As String)
        Try
            Dim request As Net.HttpWebRequest = DirectCast(Net.HttpWebRequest.Create(url), Net.HttpWebRequest)
            Dim response As Net.HttpWebResponse = DirectCast(request.GetResponse, Net.HttpWebResponse)
            Dim img As Image = Image.FromStream(response.GetResponseStream())
            response.Close()
            img.Save(path, System.Drawing.Imaging.ImageFormat.Png)
        Catch

            MsgBox("An internet connection is needed to draw the chart.  Processing is complete, but no chart will be present", MsgBoxStyle.Exclamation, "No Internet Connection Found")

        End Try

        'pb.SizeMode = PictureBoxSizeMode.StretchImage
        'pb.Image = img
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click

        ExPanel.Visible = False


        Dim rv As String
        rv = ExGrid.SelectedRows(0).Cells(0).Value
        'MsgBox(rv)

        Dim ptab As ITable
        ptab = GP.Open(pFolder + "\GIS_Data.gdb\Marxan_Runs")
        Dim prow As IRow
        Dim pcur As ICursor
        Dim queryFilter As IQueryFilter = New QueryFilterClass()
        queryFilter.WhereClause = "RUNID = '" + rv + "'"

        pcur = ptab.Search(queryFilter, False)
        prow = pcur.NextRow

        If File.Exists(pFolder + "\temp\mfiles.zip") Then
            File.Delete(pFolder + "\temp\mfiles.zip")
        End If

        Dim pMemoryBlobStream As IMemoryBlobStream
        pMemoryBlobStream = prow.Value(5)
        pMemoryBlobStream.SaveToFile(pFolder + "\temp\mfiles.zip")

        If File.Exists(pFolder + "\Planning_Units.lyr") Then
            File.Delete(pFolder + "\Planning_Units.lyr")
        End If

        Dim pMemoryBlobStream2 As IMemoryBlobStream
        pMemoryBlobStream2 = prow.Value(7)
        pMemoryBlobStream2.SaveToFile(pFolder + "\Planning_Units.lyr")


        Dim ZipToUnpack As String = pFolder + "\temp\mfiles.zip"
        Dim UnpackDirectory As String = pFolder + "\marxan"
        Using zipy As ZipFile = ZipFile.Read(ZipToUnpack)
            Dim eo As ZipEntry
            ' here, we extract every entry, but we could extract conditionally,
            ' based on entry name, size, date, checkbox status, etc.   
            For Each eo In zipy
                eo.Extract(UnpackDirectory, ExtractExistingFileAction.OverwriteSilently)
            Next
        End Using

        'Dim pMxDoc As IMxDocument
        'Dim pMap As IMap
        'Dim pEnumLayer As IEnumLayer
        'Dim play As ILayer
        'Dim groupylayer As IGroupLayer

        'Dim pLayer As ILayer
        'pLayer = GP.Open(pFolder + "\Planning_Units.lyr")

        'pMxDoc = mApp.Document
        'pMap = pMxDoc.FocusMap

        'pMap.AddLayer(pLayer)

        ''pEnumLayer = pMap.Layers(Nothing, True)
        ''pEnumLayer.Reset()
        ''play = pEnumLayer.Next
        ''Do While Not play Is Nothing
        ''    If Path.GetFileName(pFolder) = play.Name Then
        ''        If TypeOf play Is IGroupLayer Then
        ''            groupylayer = play
        ''            groupylayer.Add(pLayer)
        ''            'pMap.DeleteLayer(play)
        ''            Exit Do
        ''        End If
        ''    End If
        ''    play = pEnumLayer.Next
        ''Loop

        'pMxDoc.ActiveView.Refresh()

        'savecurrent()

        Dim player As ILayer
        player = GP.Open(pFolder + "\Planning_Units.lyr")
        Dim pMxDoc As IMxDocument
        Dim pMap As IMap
        pMxDoc = mApp.Document
        pMap = pMxDoc.FocusMap

        Dim pEnumLayer As IEnumLayer
        Dim play As ILayer
        Dim groupylayer As IGroupLayer

        pMxDoc = mApp.Document
        pMap = pMxDoc.FocusMap
        pEnumLayer = pMap.Layers(Nothing, True)
        pEnumLayer.Reset()
        play = pEnumLayer.Next
        Do While Not play Is Nothing
            If Path.GetFileName(pFolder) = play.Name Then
                If TypeOf play Is IGroupLayer Then
                    groupylayer = play
                    Exit Do
                End If
            End If
            play = pEnumLayer.Next
        Loop

        If Not groupylayer Is Nothing Then
            Dim pcomplay As ICompositeLayer
            pcomplay = groupylayer
            Dim curlay As List(Of ILayer) = New List(Of ILayer)
            Dim i As Long = 0
            For i = 0 To pcomplay.Count - 1
                curlay.Add(pcomplay.Layer(i))
            Next
            groupylayer.Clear()
            groupylayer.Add(player)
            Dim clay As ILayer
            For Each clay In curlay
                groupylayer.Add(clay)
            Next

            Dim layerFile As ILayerFile = New LayerFileClass()
            layerFile.New(pFolder + "\Project.lyr")
            layerFile.ReplaceContents(groupylayer)
            layerFile.Save()
        Else
            pMap.AddLayer(player)
        End If

        pMxDoc.UpdateContents()
        pMxDoc.ActiveView.Refresh()

        readProject()

        pcur = Nothing
        prow = Nothing
        ptab = Nothing

    End Sub


    Private Sub ExGrid_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExGrid.SelectionChanged
        Try
            Dim rv As String
            rv = CStr(ExGrid.SelectedRows(0).Cells(0).Value)
            'MsgBox(rv)

            Dim ptab As ITable
            ptab = GP.Open(pFolder + "\GIS_Data.gdb\Marxan_Runs")
            Dim prow As IRow
            Dim pcur As ICursor
            Dim queryFilter As IQueryFilter = New QueryFilterClass()
            queryFilter.WhereClause = "RUNID = '" + rv + "'"

            pcur = ptab.Search(queryFilter, False)
            prow = pcur.NextRow

            If File.Exists(pFolder + "\temp\tinput.txt") Then
                File.Delete(pFolder + "\temp\tinput.txt")
            End If

            Dim pMemoryBlobStream As IMemoryBlobStream
            pMemoryBlobStream = prow.Value(6)
            pMemoryBlobStream.SaveToFile(pFolder + "\temp\tinput.txt")

            Dim pread As StreamReader = New StreamReader(pFolder + "\temp\tinput.txt")
            TextBox3.Text = pread.ReadToEnd
            pread.Close()

            'MsgBox(prow.Value(2))

            pcur = Nothing
            prow = Nothing
            ptab = Nothing

        Catch
            Dim a As String
            a = "error"
        End Try

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        ExPanel.Visible = False
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim rv As String
        rv = CStr(ExGrid.SelectedRows(0).Cells(0).Value)

        Dim ptab As ITable
        ptab = GP.Open(pFolder + "\GIS_Data.gdb\Marxan_Runs")
        Dim prow As IRow
        Dim pcur As ICursor
        Dim queryFilter As IQueryFilter = New QueryFilterClass()
        queryFilter.WhereClause = "RUNID = '" + rv + "'"

        ptab.DeleteSearchedRows(queryFilter)

        RevertToPreviousMarxanRunToolStripMenuItem_Click(Nothing, Nothing)

        Dim delit As Delete = New Delete(pFolder + "\GIS_Data.gdb\M_" + rv)
        GP.Execute(delit, Nothing)

        'also deleate present layers

    End Sub



    Private Sub ExGrid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ExGrid.CellContentClick

    End Sub
End Class

