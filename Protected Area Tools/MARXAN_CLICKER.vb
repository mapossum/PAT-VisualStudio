Imports System.Windows.Forms
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.esriSystem


Public Class MARXAN_CLICKER

    Public mp_app As IApplication
    Public fclass As IFeatureClass
    Public rcount As Long
    Public targetlab, goallab, typelab, pflab, occlab, cslab, sepnumlab, sepdislab, proplab As Integer

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim i, j As Integer
        Dim tbox As Control
        Dim goal As Double
        Dim typ As Double
        Dim spf As Double
        Dim occ As Double
        Dim clump As Double
        Dim sepnum As Double
        Dim sepdis As Double
        Dim pro As Double
        Me.Hide()

        For i = 0 To rcount - 1
            For j = 1 To 1
                tbox = Main_Panel.Controls(j + (i * 9))
                If Not IsNumeric(tbox.Text) Then
                    MsgBox("You have entered a non-numeric value in one or more fields", MsgBoxStyle.Information)
                    Me.Show()
                    Exit Sub
                End If
            Next
        Next

        Dim pfcur As IFeatureCursor
        pfcur = fclass.Update(Nothing, False)

        Dim pfeat As IFeature

        For i = 0 To rcount - 1
            tbox = Main_Panel.Controls(1 + (i * 9))
            goal = Str(tbox.Text)

            tbox = Main_Panel.Controls(2 + (i * 9))
            typ = Str(tbox.Text)

            tbox = Main_Panel.Controls(3 + (i * 9))
            spf = Str(tbox.Text)

            tbox = Main_Panel.Controls(4 + (i * 9))
            occ = Str(tbox.Text)

            tbox = Main_Panel.Controls(5 + (i * 9))
            clump = Str(tbox.Text)

            tbox = Main_Panel.Controls(6 + (i * 9))
            sepnum = Str(tbox.Text)

            tbox = Main_Panel.Controls(7 + (i * 9))
            sepdis = Str(tbox.Text)

            tbox = Main_Panel.Controls(8 + (i * 9))
            pro = Str(tbox.Text)

            pfeat = pfcur.NextFeature
            pfeat.Value(fclass.FindField("GOAL")) = goal
            pfeat.Value(fclass.FindField("TYPE")) = typ
            pfeat.Value(fclass.FindField("SPF")) = spf
            pfeat.Value(fclass.FindField("OCC")) = occ
            pfeat.Value(fclass.FindField("CLUMP")) = clump
            pfeat.Value(fclass.FindField("SEPNUM")) = sepnum
            pfeat.Value(fclass.FindField("SEPDIS")) = sepdis
            pfeat.Value(fclass.FindField("PROP")) = pro
            pfcur.UpdateFeature(pfeat)
        Next

        Dim pdat As IDataset
        pdat = fclass

        Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
        'Dim gpu As IGPUtilities = New ESRI.ArcGIS.Geoprocessing.GPUtilities

        Dim maklay As MakeFeatureLayer = New MakeFeatureLayer(outputpath, pdat.BrowseName)
        Dim tout As Object
        tout = ExTool(GP, maklay, Nothing)

        Dim flay As IFeatureLayer
        flay = GP.Open(tout)

        Dim pmxdoc As IMxDocument

        pmxdoc = mp_app.Document

        pmxdoc.ActiveView.FocusMap.AddLayer(flay)

        'Dim pdat As IDataset
        'Dim outname, outpath, t, r As String
        'Dim pname As IName
        'pdat = fclass
        'outname = pdat.BrowseName
        'pname = pdat.Workspace.PathName
        't = pname.NameString
        'r = pdat.FullName.NameString
        'outpath = t & "\" & r

        'MsgBox(outpath)
        'System.Runtime.InteropServices.Marshal.ReleaseComObject(pfcur)
        'System.Runtime.InteropServices.Marshal.ReleaseComObject(fclass)
        'System.Runtime.InteropServices.Marshal.ReleaseComObject(pfeat)
        'System.Runtime.InteropServices.Marshal.ReleaseComObject(pdat)
        ''

        'Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
        ''Dim gpu As IGPUtilities = New ESRI.ArcGIS.Geoprocessing.GPUtilities
        ''GP.OverwriteOutput = True

        'GP.AddOutputsToMap = True
        'Dim mkl As MakeFeatureLayer = New MakeFeatureLayer(outpath, pdat)
        'rcount = ExTool(GP, mkl, Nothing)

        ''Catch ex As Exception
        ''MsgBox("Error opening new feature class")
        ''End Try

        MsgBox("Processing Complete", MsgBoxStyle.OkOnly, "Marxan Prep")
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Public outputpath As String
    Public Sub New(ByVal m_app As IApplication, ByVal pather As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        mp_app = m_app

        outputpath = pather

    End Sub

    Private Sub MARXAN_CLICKER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        targetlab = 12
        goallab = 190
        typelab = 265
        pflab = 340
        occlab = 415
        cslab = 490
        sepnumlab = 565
        sepdislab = 640
        proplab = 715

        Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
        Dim gpu As IGPUtilities = New ESRI.ArcGIS.Geoprocessing.GPUtilities
        GP.OverwriteOutput = True
        Dim getc As New GetCount(outputpath)
        Try
            rcount = ExTool(GP, getc, Nothing)
        Catch ex As Exception
            MsgBox("Error opening new feature class")
        End Try

        Dim i As Long
        Dim pfeatureclass As IFeatureClass
        pfeatureclass = GP.Open(outputpath)
        fclass = pfeatureclass
        Dim featcur As IFeatureCursor
        featcur = pfeatureclass.Search(Nothing, False)
        Dim pfeat As IFeature
        Dim tname As String
        Dim tid As Long

        For i = 0 To rcount - 1
            pfeat = featcur.NextFeature
            tname = pfeat.Value(pfeatureclass.Fields.FindField("TNAME"))
            tid = pfeat.Value(pfeatureclass.Fields.FindField("TID"))

            CreateControlLine(i, tname & " (" & CStr(tid) & ")", tid)

        Next

        'Dim targetlab, goallab, typelab, pflab, occlab, cslab, sepnumlab, sepdislab, proplab As Integer



        Label1.Location = New System.Drawing.Point(targetlab, 10)
        Label1.Text = vbCrLf + "Target and ID:"
        Label2.Location = New System.Drawing.Point(goallab, 10)
        Label2.Text = vbCrLf + "Goal:"
        Label3.Location = New System.Drawing.Point(typelab, 10)
        Label3.Text = vbCrLf + "Type:"
        Label4.Location = New System.Drawing.Point(pflab, 10)
        Label4.Text = "Penalty" + vbCrLf + "Factor:"
        Label5.Location = New System.Drawing.Point(occlab, 10)
        Label5.Text = vbCrLf + "Occ.:"
        Label6.Location = New System.Drawing.Point(cslab, 10)
        Label6.Text = "Clump" + vbCrLf + " Size:"
        Label7.Location = New System.Drawing.Point(sepnumlab, 10)
        Label7.Text = "Sep." + vbCrLf + "Number:"
        Label8.Location = New System.Drawing.Point(sepdislab, 10)
        Label8.Text = "Sep." + vbCrLf + "Distance:"
        Label9.Location = New System.Drawing.Point(proplab, 10)
        Label9.Text = vbCrLf + "Prop.:"

    End Sub

    Private Sub CreateControlLine(ByVal count As Long, ByVal namer As String, ByVal tid As Long)

        Dim lbl As New TextBox()
        lbl.Location = New System.Drawing.Point(targetlab, 3 + (count * 27))
        lbl.Size = New System.Drawing.Size(175, 20)
        lbl.ReadOnly = True
        lbl.Text = namer
        Main_Panel.Controls.Add(lbl)

        Me.Size = (New System.Drawing.Size(790, 150 + (count * 27)))

        If count < 15 Then
            Me.Size = (New System.Drawing.Size(800, 165 + (count * 27)))
            Main_Panel.Size = (New System.Drawing.Size(800, 27 + (count * 27)))
        Else
            Me.Size = (New System.Drawing.Size(800, 165 + (10 * 27)))
            Main_Panel.Size = (New System.Drawing.Size(800, 27 + (10 * 27)))
        End If

        Dim cbo1 As New TextBox
        cbo1.Location = New System.Drawing.Point(goallab, 2 + (count * 27))
        cbo1.Size = New System.Drawing.Size(55, 20)
        cbo1.Text = "-1"
        cbo1.TextAlign = HorizontalAlignment.Right
        Main_Panel.Controls.Add(cbo1)

        Dim cbo2 As New TextBox
        cbo2.Location = New System.Drawing.Point(typelab, 2 + (count * 27))
        cbo2.Size = New System.Drawing.Size(55, 20)
        cbo2.Text = CStr(tid)
        cbo2.TextAlign = HorizontalAlignment.Right
        Main_Panel.Controls.Add(cbo2)

        Dim cbo3 As New TextBox
        cbo3.Location = New System.Drawing.Point(pflab, 2 + (count * 27))
        cbo3.Size = New System.Drawing.Size(55, 20)
        cbo3.Text = "-1"
        cbo3.TextAlign = HorizontalAlignment.Right
        Main_Panel.Controls.Add(cbo3)

        Dim cbo4 As New TextBox
        cbo4.Location = New System.Drawing.Point(occlab, 2 + (count * 27))
        cbo4.Size = New System.Drawing.Size(55, 20)
        cbo4.Text = "-1"
        cbo4.TextAlign = HorizontalAlignment.Right
        Main_Panel.Controls.Add(cbo4)

        Dim cbo5 As New TextBox
        cbo5.Location = New System.Drawing.Point(cslab, 2 + (count * 27))
        cbo5.Size = New System.Drawing.Size(55, 20)
        cbo5.Text = "-1"
        cbo5.TextAlign = HorizontalAlignment.Right
        Main_Panel.Controls.Add(cbo5)

        Dim cbo6 As New TextBox
        cbo6.Location = New System.Drawing.Point(sepnumlab, 2 + (count * 27))
        cbo6.Size = New System.Drawing.Size(55, 20)
        cbo6.Text = "-1"
        cbo6.TextAlign = HorizontalAlignment.Right
        Main_Panel.Controls.Add(cbo6)

        Dim cbo7 As New TextBox
        cbo7.Location = New System.Drawing.Point(sepdislab, 2 + (count * 27))
        cbo7.Size = New System.Drawing.Size(55, 20)
        cbo7.Text = "-1"
        cbo7.TextAlign = HorizontalAlignment.Right
        Main_Panel.Controls.Add(cbo7)

        Dim cbo8 As New TextBox
        cbo8.Location = New System.Drawing.Point(proplab, 2 + (count * 27))
        cbo8.Size = New System.Drawing.Size(55, 20)
        cbo8.Text = "-1"
        cbo8.TextAlign = HorizontalAlignment.Right
        Main_Panel.Controls.Add(cbo8)

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
            Return Nothing
        End Try

    End Function

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'MsgBox(Main_Panel.Controls.Count)
        Dim i As Integer = 0
        Dim p As Integer
        Dim cnt As Control
        For Each cnt In Main_Panel.Controls
            'MsgBox(cnt.Text)
            p = i Mod 9
            'MsgBox(p)
            If ((p <> 0) And (p <> 2)) Then
                cnt.Text = Main_Panel.Controls(p).Text
            End If
            i = i + 1
        Next
    End Sub
End Class
