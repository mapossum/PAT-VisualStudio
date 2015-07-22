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




Public Class HEXGEN_FORM
    Public mapp As IApplication

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


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        If Not (IsNumeric(areabox.Text) Or IsNumeric(unitidbox.Text) Or IsNumeric(stepbox.Text)) Then
            MsgBox("Area, Step, and Unit ID must be numbers", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Me.Hide()

        Dim hexarea As Double

        If hexnumber.Checked Then

            Dim mxdoc As IMxDocument = mapp.Document
            Dim pmap As IMap = mxdoc.FocusMap
            Dim penumlay As IEnumLayer = pmap.Layers
            Dim pfc As IFeatureClass
            Dim pfl As IFeatureLayer

            Dim play As ILayer = penumlay.Next

            While Not play Is Nothing
                If play.Name = extentbox.Text Then
                    pfl = play
                    pfc = pfl.FeatureClass
                End If
                play = penumlay.Next
            End While

            'MsgBox(FCA(pfc))

            If pfc.ShapeType = esriGeometryType.esriGeometryPolygon Then
                hexarea = FCA(pfc) / (CDbl(areabox.Text))
            Else
                MsgBox("Only polygon features are supported when specifying number of hexagons to create.", MsgBoxStyle.Exclamation, "HEXGEN")
                play = Nothing
                pfc = Nothing
                pfl = Nothing
                Me.Show()
                Exit Sub
            End If

        Else

            hexarea = (CDbl(areabox.Text)) * 10000

        End If
        'output_loc = output_loc[:-1]
        Dim triarea, oneside, halfside, onehi, longway, shortway As Double
        triarea = hexarea / 6
        oneside = Math.Sqrt((triarea * 4) / Math.Sqrt(3))
        halfside = oneside / 2
        onehi = Math.Sqrt((oneside * oneside) - ((halfside) * (halfside)))
        longway = oneside * 2
        shortway = onehi * 2

        Dim pstatus As IStatusBar
        pstatus = mapp.StatusBar

        Dim pfcur As IFeatureCursor
        Dim pdoc As IMxDocument
        pdoc = mapp.Document
        Dim player As ILayer
        player = pdoc.FocusMap.Layer(extentbox.SelectedIndex)
        Dim penv As IEnvelope
        penv = player.AreaOfInterest.Envelope


        Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
        Dim gpu As IGPUtilities = New ESRI.ArcGIS.Geoprocessing.GPUtilities
        GP.OverwriteOutput = True

        GP.AddOutputsToMap = False

        Dim ouputsplit() As String
        ouputsplit = outputbox.Text.Split("\")

        Dim outpath, outname As String
        outpath = ""

        Dim top, i As Integer
        top = ouputsplit.GetUpperBound(0)
        For i = 0 To top
            If i <> top Then
                outpath = outpath + ouputsplit(i) + "\"
            Else
                outname = ouputsplit(i)
            End If
        Next

        outpath = outpath.Substring(0, Len(outpath) - 1)
        Dim gplayer As IGPLayer
        gplayer = gpu.MakeGPLayerFromLayer(player)
        'MsgBox(outpath)
        'MsgBox(outname)

        Dim createsr As New CreateSpatialReference
        createsr.template = gplayer
        createsr.spatial_reference_template = gplayer
        createsr.expand_ratio = 0.05

        Dim outsr As Object
        Try
            outsr = ExTool(GP, createsr, Nothing)
        Catch ex As Exception
            MsgBox("Error creating spatial reference")
        End Try

        Dim tim As DateTime
        tim = Now
        Dim timenow, newcl As String
        timenow = CStr(tim.Year) & CStr(tim.Month) & CStr(tim.Day) & CStr(tim.Hour) & CStr(tim.Minute) & CStr(tim.Second) & CStr(tim.Millisecond)
        newcl = "HEX_TEMP_" & timenow
        Dim createfc As New CreateFeatureclass
        createfc.out_name = newcl '"MARXAN_TEMP" 'outname
        createfc.out_path = outpath
        createfc.spatial_reference = outsr
        createfc.geometry_type = "POLYGON"
        Dim outf As Object
        Try
            outf = ExTool(GP, createfc, Nothing)
        Catch ex As Exception
            MsgBox("Error creating new feature class")
        End Try

        Dim addf As New AddField(outf, "UNIT_ID", "LONG")
        Try
            ExTool(GP, addf, Nothing)
        Catch ex As Exception
            MsgBox("Error creating adding UNIT ID field")
        End Try

        Dim ppointcol As IPointCollection
        Dim ppoint As IPoint
        Dim pfclass As IFeatureClass
        pfclass = GP.Open(outf)

        Dim pfeat As IFeature
        pfcur = pfclass.Insert(False)

        Dim minx, miny, maxx, maxy, xrange, yrange, distancex, distancey, centerx, centery, ystart As Double
        minx = penv.XMin
        miny = penv.YMin
        maxx = penv.XMax
        maxy = penv.YMax
        xrange = maxx - minx
        yrange = maxy - miny

        distancex = oneside + halfside
        distancey = shortway

        Dim counter As Long
        counter = 0
        pstatus.ProgressBar.Show()
        pstatus.ProgressBar.Position = 0
        Dim xc, yc As Long

        '                gp.AddMessage("Generating Hexagons...")
        For xc = 0 To CLng(((xrange + halfside) / distancex))

            '    for xc in range((xrange + longway + oneside + 0.02) / distancex):
            centerx = (minx + (xc * distancex)) - 0.01
            pstatus.Message(0) = ("Processing HEXAGONS - On row " + Str(xc) + " of " + CStr(CInt((xrange + shortway) / distancex)))
            pstatus.ProgressBar.Position = CInt((xc / CInt((xrange + halfside) / distancex)) * 100)
            If (xc Mod 10) = 0 Then
                GP.AddMessage("    On row " + Str(xc) + " of " + CStr(CInt((xrange + halfside) / distancex)))

            End If


            If (xc Mod 2) = 0 Then
                ystart = miny
            Else
                ystart = miny - onehi
            End If
            For yc = 0 To CLng(((yrange + halfside) / distancey))
                centery = ystart + (yc * distancey)


                ppointcol = New Polygon

                ppoint = New Point
                ppoint.PutCoords(centerx + halfside, centery + onehi)
                ppointcol.AddPoint(ppoint)

                '                                pt1 = gp.CreateObject("Point")
                '                                pt1.x = centerx + halfside
                '                                pt1.y = centery + onehi
                '                                gonarray.add(pt1)
                ppoint = New Point
                ppoint.PutCoords(centerx + oneside, centery)
                ppointcol.AddPoint(ppoint)

                '                                pt2 = gp.CreateObject("Point")
                '                                pt2.x = centerx + oneside
                '                                pt2.y = centery
                '                                gonarray.add(pt2)
                ppoint = New Point
                ppoint.PutCoords(centerx + halfside, centery - onehi)
                ppointcol.AddPoint(ppoint)
                '                                pt3 = gp.CreateObject("Point")
                '                                pt3.x = centerx + halfside
                '                                pt3.y = centery - onehi
                '                                gonarray.add(pt3)
                ppoint = New Point
                ppoint.PutCoords(centerx - halfside, centery - onehi)
                ppointcol.AddPoint(ppoint)
                '                                pt4 = gp.CreateObject("Point")
                '                                pt4.x = centerx - halfside
                '                                pt4.y = centery - onehi
                '                                gonarray.add(pt4)
                ppoint = New Point
                ppoint.PutCoords(centerx - oneside, centery)
                ppointcol.AddPoint(ppoint)
                '                                pt5 = gp.CreateObject("Point")
                '                                pt5.x = centerx - oneside
                '                                pt5.y = centery
                '                                gonarray.add(pt5)

                ppoint = New Point
                ppoint.PutCoords(centerx - halfside, centery + onehi)
                ppointcol.AddPoint(ppoint)
                '                                pt6 = gp.CreateObject("Point")
                '                                pt6.x = centerx - halfside
                '                                pt6.y = centery + onehi
                '                                gonarray.add(pt6)
                ppoint = New Point
                ppoint.PutCoords(centerx + halfside, centery + onehi)
                ppointcol.AddPoint(ppoint)

                pfeat = pfclass.CreateFeature
                pfeat.Shape = ppointcol

                pfeat.Value(pfclass.Fields.FindField("UNIT_ID")) = counter + CLng(unitidbox.Text)
                pfeat.Store()
                counter = counter + CInt(stepbox.Text)


            Next
        Next


        Dim mkl As MakeFeatureLayer = New MakeFeatureLayer(outf, newcl)
        ExTool(GP, mkl, Nothing)

        Dim sel As SelectLayerByLocation = New SelectLayerByLocation(newcl)
        If overlapbox.Checked = True Then sel.select_features = gplayer
        sel.selection_type = "NEW_SELECTION"
        ExTool(GP, sel, Nothing)

        GP.AddOutputsToMap = True

        Dim cpy As CopyFeatures = New CopyFeatures(newcl, outputbox.Text)
        ExTool(GP, cpy, Nothing)

        Dim delf As DeleteField = New DeleteField(outputbox.Text, "Id")
        ExTool(GP, delf, Nothing)

        Dim deler As Delete = New Delete(newcl)
        ExTool(GP, deler, Nothing)


        pfcur = pfclass.Update(Nothing, False)

        counter = 0
        If overlapbox.Checked = True Then
            pfeat = pfcur.NextFeature()
            While Not pfeat Is Nothing
                pfeat.Value(pfclass.FindField("UNIT_ID")) = unitidbox.Text + (counter)
                counter = counter + stepbox.Text
                pfcur.UpdateFeature(pfeat)
                pfeat = pfcur.NextFeature
            End While
        End If
        'Dim deler2 As Delete = New Delete(outf)
        'ExTool(GP, deler2, Nothing)



        Dim calf As New CalculateField(outputbox.Text, "UNIT_ID", unitidbox.Text + " + ([FID] * " + stepbox.Text + ")")
        ExTool(GP, calf, Nothing)

        pstatus.Message(0) = "HEXAGONS DONE"
        MsgBox("Processing Complete", MsgBoxStyle.OkOnly, "HEXGEN Complete")
        pstatus.ProgressBar.Hide()

        pfeat = Nothing
        pfclass = Nothing

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub HEXGEN_FORM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mdoc As IMxDocument
        mdoc = mapp.Document
        Dim mlay As ESRI.ArcGIS.Carto.ILayer


        'Dim mDsName As IDatasetName
        Dim i As Long
        For i = 0 To mdoc.ActiveView.FocusMap.LayerCount - 1
            mlay = mdoc.ActiveView.FocusMap.Layer(i)
            extentbox.Items.Add(mlay.Name)
        Next

        Try
            extentbox.SelectedIndex = 0
        Catch ex As Exception

        End Try


    End Sub

    Public Sub New(ByVal app As IApplication)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mapp = app
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

    Private Sub TableLayoutPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub


    Private Sub hexnumber_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hexnumber.CheckedChanged
        If hexnumber.Checked Then
            Label2.Text = " Number of Hexes:"
            Label4.Visible = False
        Else
            Label2.Text = "Area (Hectares*):"
            Label4.Visible = True
        End If
    End Sub

    Public Function FCA(ByVal featureClass As IFeatureClass) As Double
        'In this function we will use a spatial filter combined with an attribute query to perform the search.
        'You do not have to perform both types of filters on a search, either could be used individually.

        'preform the search on the supplied feature class; use a cursor to hold the results
        Dim featureCursor As IFeatureCursor = featureClass.Search(Nothing, False)

        'get the first feature returned
        Dim feature As IFeature = featureCursor.NextFeature()

        'get the "Area" field
        Dim fields As IFields = featureCursor.Fields
        Dim areaIndex As Integer = fields.FindField("Shape")
        Dim ppoly As IArea

        'a variable to hold the total area
        Dim searchedArea As Double = 0

        'loop through all of the features and
        'calculate the sum of all of the areas
        While feature IsNot Nothing
            'searchedArea = searchedArea + CDbl(feature.get_Value(areaIndex))
            ppoly = feature.Value(areaIndex)
            searchedArea = searchedArea + ppoly.Area
            feature = featureCursor.NextFeature()
        End While

        System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor)
        System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass)

        Return searchedArea

    End Function


End Class
