Imports ESRI.ArcGIS.Carto
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.CartoUI
Imports System.Windows.Forms
Imports System.IO
Imports System.Text.Encoding


Module type_defs

    Public Const erstoptext As String = "**********************************" & vbCrLf & _
                                     "* Enviormental Risk Surface Tool *" & vbCrLf & _
                                     "*     The Nature Conservancy     *" & vbCrLf & _
                                     "*  ----------------------------  *" & vbCrLf & _
                                     "*  Created by: George Raber and  *" & vbCrLf & _
                                     "*              Steve Schill      *" & vbCrLf & _
                                     "**********************************" & vbCrLf

    Public Function ltype(ByVal inlay As ILayer) As ERS_REC.laytype

        Dim pDataLayer As IDataLayer
        Dim pDatasetName As IDatasetName
        Dim pWSName As IWorkspaceName
        'Dim sFDS As String
        'Dim pFCName As IFeatureClassName

        'Dim pRDname As IRasterDatasetName

        If TypeOf inlay Is IDataLayer Then
            'sFDS = ""
            pDataLayer = inlay
            pDatasetName = pDataLayer.DataSourceName
            pWSName = pDatasetName.WorkspaceName
            If TypeOf pDatasetName Is IFeatureClassName Then
                'pFCName = pDatasetName
                Return ERS_REC.laytype.feature
                'If Not pFCName.FeatureDatasetName Is Nothing Then
                '    sFDS = pFCName.FeatureDatasetName.Name
                'End If
                'Me.layerList.Items.Add(inlay.Name)
                'Me.ListBox1.Items.Add("Feature," & pWSName.PathName & sFDS & pDatasetName.Name)
            ElseIf TypeOf pDatasetName Is IRasterDatasetName Then
                Return ERS_REC.laytype.raster
                'pRDname = pDatasetName
                'Me.layerList.Items.Add(inlay.Name)
                'Me.ListBox1.Items.Add("Raster,") ' & pWSName.PathName & sFDS & pDatasetName.Name)
                'If Not pRDname.RasterBandNames.Next Is Nothing Then
                ' sFDS = pRDname.RasterBandNames.Next.Name
                'End If
            Else
                Return ERS_REC.laytype.invalid
            End If

        Else
            Return ERS_REC.laytype.invalid
        End If
    End Function

    Public Function getvalfields(ByVal inLayer As IFeatureLayer) As List(Of String)
        Dim that As New List(Of String)
        Dim pfclass As IFeatureClass
        Dim pfld As IField
        Dim i As Integer

        pfclass = inLayer.FeatureClass
        For i = 0 To pfclass.Fields.FieldCount - 1
            pfld = pfclass.Fields.Field(i)
            If pfld.Type = esriFieldType.esriFieldTypeSingle Or pfld.Type = esriFieldType.esriFieldTypeDouble Or pfld.Type = esriFieldType.esriFieldTypeInteger Or pfld.Type = esriFieldType.esriFieldTypeSmallInteger Then
                that.Add(pfld.Name)
            End If
        Next
        Return that

    End Function

    Public Sub IncludeVal(ByRef inlayer As IFeatureLayer, ByVal fname As String, ByVal val As Double)
        addf(inlayer, fname)
        calf(inlayer, fname, val)
        '''inlayer = Nothing
    End Sub

    Private Sub addf(ByRef inlayer As IFeatureLayer, ByVal fname As String)

        Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
        Dim gpu As IGPUtilities
        gpu = New ESRI.ArcGIS.Geoprocessing.GPUtilities
        Dim inGPlayer As IGPLayer

        Dim pfclass As IFeatureClass
        Dim i As Integer
        Dim delit As Boolean
        delit = False
        pfclass = inlayer.FeatureClass
        For i = 0 To pfclass.Fields.FieldCount - 1
            If fname = pfclass.Fields.Field(i).Name Then
                delit = True
            End If
        Next

        'If delit Then
        'Dim dfld As DeleteField = New DeleteField(inGPlayer, fname)
        'RunTool(GP, dfld, Nothing)
        'End If

        inGPlayer = gpu.MakeGPLayerFromLayer(inlayer)
        pfclass = Nothing

        ' Set the OverwriteOutput setting to True
        GP.OverwriteOutput = True

        If Not delit Then
            Dim addfld As AddField = New AddField(inGPlayer, fname, "DOUBLE")
            RunTool(GP, addfld, Nothing)
        End If

        Dim n As Integer
        n = GP.MessageCount
        Dim hh As Integer
        For hh = 0 To n - 1
            Debug.Print(GP.GetMessage(hh))
        Next

        Marshal.ReleaseComObject(gpu)
        'Marshal.ReleaseComObject(pfclass)
        Marshal.ReleaseComObject(inGPlayer)
        'pfclass = Nothing
        'Marshal.ReleaseComObject(GP)
        'GP = Nothing
        'gpu = Nothing

    End Sub


    Private Sub calf(ByRef inlayer As IFeatureLayer, ByVal fname As String, ByVal val As Double)

        Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
        Dim gpu As IGPUtilities
        gpu = New ESRI.ArcGIS.Geoprocessing.GPUtilities
        Dim inGPlayer As IGPLayer
        'Dim i As Integer

        inGPlayer = gpu.MakeGPLayerFromLayer(inlayer)
        GP.OverwriteOutput = True

        'Dim pfields As IFields
        'pfields = gpu.GetFields(inGPlayer)

        'For i = 0 To pfields.FieldCount - 1
        '    Debug.Print(pfields.Field(i).Name)
        'Next

        'pfields = Nothing

        Dim calval As CalculateField = New CalculateField(inGPlayer, fname, val)
        RunTool(GP, calval, Nothing)

        'GP = Nothing
        'gpu = Nothing

        Dim n As Integer
        n = GP.MessageCount
        Dim hh As Integer
        For hh = 0 To n - 1
            Debug.Print(GP.GetMessage(hh))
        Next

        Marshal.ReleaseComObject(gpu)
        Marshal.ReleaseComObject(inGPlayer)
        'pfclass = Nothing
        'Marshal.ReleaseComObject(GP)

        'inlayer = Nothing

    End Sub

    Public Sub RunTool(ByVal geop As ESRI.ArcGIS.Geoprocessor.Geoprocessor, ByVal process As IGPProcess, ByVal TC As ITrackCancel)

        ' Set the overwrite output option to true
        geop.OverwriteOutput = True

        Try
            geop.Execute(process, Nothing)
            ReturnMessages(geop)

        Catch err As Exception
            Debug.WriteLine(err.Message)
            ReturnMessages(geop)
        End Try
    End Sub

    ' Function for returning the tool messages.


    Public Sub addrasterlayertomap(ByVal mapa As IMap, ByVal raslay As IRasterLayer, ByVal pmxdoc As IMxDocument)
        Dim pRen As IRasterStretchColorRampRenderer = New RasterStretchColorRampRenderer
        Dim prasren As IRasterRenderer

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
        prasren = pRen
        prasren.Update()
        pRen.ResetLabels()

        raslay.Renderer = prasren

        prasren.Update()
        raslay.Renderer = pRen
        prasren.Update()
        pmxdoc.ActivatedView.PartialRefresh(esriViewDrawPhase.esriViewGeography, raslay, Nothing)
        pmxdoc.ActivatedView.Refresh()
        pmxdoc.UpdateContents()


        mapa.AddLayer(raslay)
        Dim pactiveview As IActiveView
        pactiveview = mapa
        pactiveview.Refresh()
        'mapa.docmymapdoc.UpdateContents()
        'mapa.ContentsChanged()
        'mapa.Refresh()

        'UserForm2.Hide
        'UserForm1.Hide

    End Sub

    Public Sub addtocbox(ByRef fl As List(Of String), ByRef cb As ComboBox)
        Dim stri As String
        For Each stri In fl
            cb.Items.Add(stri)
        Next
    End Sub

    Public Sub QuantileClassBreaks(ByVal pflayer As IGeoFeatureLayer, ByVal Fieldname As String, ByVal clsnum As Long, ByRef mp_app As IApplication)

        Dim pMxDoc As IMxDocument
        Dim pFclass As IFeatureClass
        Dim pFeature As IFeature
        Dim pFCursor As IFeatureCursor
        Dim prender As IClassBreaksRenderer
        Dim pColorEnum As IEnumColors
        Dim pCRamp As IColorRamp
        Dim pSym As ISimpleFillSymbol
        Dim pTable As ITable
        Dim pGeoLayer As IGeoFeatureLayer
        Dim pClassifyGEN As IClassifyGEN
        Dim pTableHistogram As ITableHistogram
        Dim pHistogram As IHistogram
        Dim frqs As Object, xVals As Object
        Dim cb As Object
        Dim pColorRamp1 As IAlgorithmicColorRamp, pEnumColors As IEnumColors
        Dim pColorRamp2 As IAlgorithmicColorRamp
        Dim paColorRamp As IMultiPartColorRamp
        Dim pColor1 As IRgbColor
        Dim pColor2 As IRgbColor
        Dim pColor3 As IRgbColor
        Dim i As Integer
        Dim pUIProperties As IClassBreaksUIProperties
        Dim pSimpleFillSym As ISimpleFillSymbol
        Dim pSimplelinesym As ISimpleLineSymbol


        pMxDoc = mp_app.Document
        'Set pFLayer = pMxDoc.ActiveView.FocusMap.layer(0)
        pFclass = pflayer.FeatureClass
        pFCursor = pFclass.Search(Nothing, False)
        pFeature = pFCursor.NextFeature
        pTable = pFclass

        pGeoLayer = pflayer
        prender = New ClassBreaksRenderer
        pClassifyGEN = New EqualInterval

        pTableHistogram = New TableHistogram
        pHistogram = pTableHistogram

        pTableHistogram.Field = Fieldname        ' matches renderer field
        pTableHistogram.Table = pTable

        pHistogram.GetHistogram(xVals, frqs)
        pClassifyGEN.Classify(xVals, frqs, clsnum)      ' use five classes

        prender = New ClassBreaksRenderer
        cb = pClassifyGEN.ClassBreaks

        prender.Field = Fieldname
        prender.BreakCount = clsnum
        prender.MinimumBreak = 0.00001
        ' note: minimum break is NOT the first break, it is actually the lowest value
        '   in the data set, the minimum value.

        ' create our color ramp
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
        pColor2.Blue = 0
        pColor3.Red = 0
        pColor3.Green = 255
        pColor3.Blue = 0
        pColorRamp1.FromColor = pColor1
        pColorRamp1.ToColor = pColor2
        pColorRamp2.FromColor = pColor2
        pColorRamp2.ToColor = pColor3
        paColorRamp.Ramp(0) = pColorRamp1
        paColorRamp.Ramp(1) = pColorRamp2
        paColorRamp.Size = clsnum
        paColorRamp.CreateRamp(True)
        pEnumColors = paColorRamp.Colors
        pEnumColors.Reset()
        ' use this interface to set dialog properties
        pUIProperties = prender
        pUIProperties.ColorRamp = "Custom"

        Dim ccolor As IColor
        ' be careful, indices are different for the diff lists
        For i = 0 To clsnum - 1
            prender.Break(i) = cb(i + 1)
            ' a better label
            prender.Label(i) = cb(i) & " - " & cb(i + 1)
            pUIProperties.LowBreak(i) = cb(i)
            pSimpleFillSym = New SimpleFillSymbol
            pSimplelinesym = New SimpleLineSymbol
            ccolor = pEnumColors.Next
            pSimplelinesym.Color = ccolor
            pSimplelinesym.Width = 0
            pSimpleFillSym.Outline = pSimplelinesym
            pSimpleFillSym.Color = ccolor
            prender.Symbol(i) = pSimpleFillSym
        Next i

        ' assign renderer to layer
        pGeoLayer.Renderer = prender
        pMxDoc.UpdateContents()
        pMxDoc.ActiveView.Refresh()
    End Sub



    Public Function ExTool(ByVal geop As ESRI.ArcGIS.Geoprocessor.Geoprocessor, ByVal process As IGPProcess, ByVal TC As ITrackCancel) As Object
        ' Set the overwrite output option to true
        Dim thing As IGeoProcessorResult

        geop.OverwriteOutput = True
        'Try
        thing = geop.Execute(process, Nothing)

        Dim n As Integer
        n = geop.MessageCount
        Dim hh As Integer
        For hh = 0 To n - 1
            Debug.Print("Right here")
            Debug.Print(geop.GetMessage(hh))
        Next

        ReturnMessages(geop)
        Return thing.ReturnValue
        'Catch err As Exception
        'addtext(Err.Message)
        'ReturnMessages(geop)
        'Return Nothing
        'End Try

    End Function

    Public Sub ReturnMessages(ByVal gp As ESRI.ArcGIS.Geoprocessor.Geoprocessor)
        ' Print out the messages from tool executions
        Dim Count As Integer
        If gp.MessageCount > 0 Then
            For Count = 0 To gp.MessageCount - 1
                Debug.Print(gp.GetMessage(Count))
            Next
        End If
    End Sub

    Public Sub fixrastername(ByRef rasname As String)
        Dim tt As String
        tt = rasname
        Debug.Print(tt)
        Dim delim As String = "; " & Chr(34)
        rasname = rasname.Trim(delim.ToCharArray())
    End Sub

    Public Sub cleanup(ByVal gp As ESRI.ArcGIS.Geoprocessor.Geoprocessor, ByVal wildcard As String)
        Dim lister As IGpEnumList
        Try
            lister = gp.ListDatasets(wildcard, "")
        Catch err As Exception
            'MsgBox(err.Message)
            'gp = Nothing
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

        'gp = Nothing

    End Sub


    Function GetStyleItem(ByVal strStyleFile As String, ByVal strStyleClass As String, ByVal strName As String) As IColorRamp
        Dim pSG As IStyleGallery
        pSG = New StyleGallery
        pSG.LoadStyle(strStyleFile, strStyleClass)

        Dim pEnumSGI As IEnumStyleGalleryItem
        pEnumSGI = pSG.Items(strStyleClass, strStyleFile, "")
        pEnumSGI.Reset()
        Dim pSGI As IStyleGalleryItem
        pSGI = pEnumSGI.Next
        Do While Not pSGI Is Nothing
            If LCase(pSGI.Name) = LCase(strName) Then
                GetStyleItem = pSGI.Item
                Exit Function
            End If
            pSGI = pEnumSGI.Next
        Loop
        GetStyleItem = Nothing
    End Function

    Public Class Win32HWNDWrapper
        Implements System.Windows.Forms.IWin32Window
        Private _hwnd As System.IntPtr

        Public ReadOnly Property Handle() As System.IntPtr Implements System.Windows.Forms.IWin32Window.Handle
            Get
                Return _hwnd
            End Get
        End Property

        Public Sub New(ByVal Handle As System.IntPtr)
            _hwnd = Handle
        End Sub
    End Class

    Public Class Awriter
        '
        'Inherits TextWriter
        Inherits StreamWriter
        'Private Thing As TextWriter

        Public Overrides ReadOnly Property Encoding() As System.Text.Encoding
            Get
                Encoding = ASCII
            End Get
        End Property

        Public Sub New(ByVal textfilename As String)
            MyBase.New(textfilename, True)
        End Sub
    End Class

End Module
