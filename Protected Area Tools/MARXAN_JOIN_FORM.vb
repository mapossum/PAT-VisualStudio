Imports System.Windows.Forms
Imports System.IO
Imports System.Text.Encoding
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geometry


Public Class MARXAN_JOIN_FORM
    Private m_application As IApplication
    Dim layerdic As New Dictionary(Of String, ILayer)
    Private fname As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        fname = System.IO.Path.GetFileName(OutputBOX.Text)
        'Dim GP
        ''CODE FOR JOIN
        'GP = CreateObject("esriGeoprocessing.GpDispatch.1")

        'Dim tout As Object
        Dim GP As ESRI.ArcGIS.Geoprocessor.Geoprocessor = New ESRI.ArcGIS.Geoprocessor.Geoprocessor
        GP.OverwriteOutput = True
        GP.AddOutputsToMap = False

        Dim tout As Object

        Dim fadder As AddField = New AddField(PUBOX.Text, TextBox2.Text, "Double")
        tout = GP.Execute(fadder, Nothing)

        Dim mout As String = "MOUT"
        ''GP.MakeTableView_management OUTPUTBOX.Text, "newtab"

        Dim mtv As MakeTableView = New MakeTableView(OutputBOX.Text, mout)
        tout = GP.Execute(mtv, Nothing)

        'GP.AddField(PUBOX.Text, TextBox2.Text, "long")

        'GP.AddJoin_management(PUBOX.Text, PUFIELD.Text, OutputBOX.Text, Replace(MARXANFIELD.List(0), Chr(34), ""), "KEEP_COMMON")

        Dim adj As AddJoin = New AddJoin(PUBOX.Text, PUFIELD.Text, mout, MARXANPUFIELD.Text)
        adj.join_type = "KEEP_COMMON"
        GP.Execute(adj, Nothing)

        'GP.calculatefield(PUBOX.Text, PUBOX.Text + "." + Left(TextBox2.Text, 8), "[" + Replace(MARXANFIELD.List(1), Chr(34), "") + "]")

        Dim calcf As CalculateField = New CalculateField(PUBOX.Text, PUBOX.Text + "." + TextBox2.Text, "[" + Replace(MARXANFIELD.Text, Chr(34), "") + "]")
        calcf.expression_type = "VB"
        tout = GP.Execute(calcf, Nothing)

        'filetext = Right(OutputBOX.Text, (Len(OutputBOX.Text) - Len(TextBox1.Text)))
        'GP.RemoveJoin(PUBOX.Text, filetext)

        Dim rmj As RemoveJoin = New RemoveJoin
        rmj.in_layer_or_view = PUBOX.Text
        rmj.join_name = fname

        tout = GP.Execute(rmj, Nothing)

        Dim play As IFeatureLayer

        play = GP.Open(PUBOX.Text)

        type_defs.QuantileClassBreaks(play, TextBox2.Text, 5, m_application)

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
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

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub MARXAN_JOIN_FORM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        checkok()

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

                mlayer = mlay
                pclass = mlayer.FeatureClass
                If pclass.ShapeType = esriGeometryType.esriGeometryPolygon Then
                    Me.PUBOX.Items.Add(mlay.Name)

                End If
                Try
                    layerdic.Add(mlay.Name, mlay)
                Catch ex As Exception
                    MsgBox("This tool does not support multiple layers with the same name", MsgBoxStyle.Exclamation, "Rename Layer")
                    Me.Close()
                End Try
            End If
        Next i

        'Me.statusbox.SelectedIndex = 0
        Me.PUBOX.SelectedIndex = 0

        Me.PUFIELD.SelectedIndex = 0
        'Me.StatusValueBox.SelectedIndex = 2


    End Sub

    Public Sub New(ByVal app As IApplication)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_application = app
    End Sub
    Private Sub checkok()
        If PUFIELD.Text <> "" And PUBOX.Text <> "" And OutputBOX.Text <> "" And TextBox2.Text <> "" Then
            OK_Button.Enabled = True
        Else
            OK_Button.Enabled = False
        End If

    End Sub

    Private Sub PUBOX_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PUBOX.SelectedIndexChanged
        checkok()
        Dim mdoc As IMxDocument
        mdoc = m_application.Document
        Dim mlay As ESRI.ArcGIS.Carto.ILayer
        Dim flist As List(Of String)
        Dim check As String

        check = PUBOX.Text

        Dim i As Long
        For i = 0 To mdoc.ActiveView.FocusMap.LayerCount - 1
            mlay = mdoc.ActiveView.FocusMap.Layer(i)
            If mlay.Name = check Then
                PUFIELD.Items.Clear()
                flist = getvaljfields(mlay)
                If flist.Count = 0 Then
                    MsgBox("Layer " & check & " does not have any valid fields, ID field must be of type integer or string.", MsgBoxStyle.Exclamation, "Field Type Error")
                    OK_Button.Enabled = False
                    Exit Sub
                End If
                type_defs.addtocbox(flist, PUFIELD)
                PUFIELD.SelectedIndex = 0
                OK_Button.Enabled = True
                Exit Sub
            End If
        Next i

    End Sub



    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        checkok()
    End Sub

    Private Sub PUFIELD_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PUFIELD.SelectedIndexChanged
        checkok()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Visible = False

        ofd.Multiselect = False
        ofd.Title = "Choose a output file from a previous MARXAN run."

        Dim rs As System.Windows.Forms.DialogResult
        rs = ofd.ShowDialog()
        If rs = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        Else
            OutputBOX.Text = ofd.FileName
        End If



        Me.Visible = True
        Me.BringToFront()
        Me.Select()
    End Sub

    Private Sub OutputBOX_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputBOX.TextChanged
        checkok()
        MARXANFIELD.Items.Clear()
        MARXANPUFIELD.Items.Clear()
        If OutputBOX.Text <> "" Then

            Dim lines As String
            Dim rest As String
            Dim infields As String()
            Dim bb As Boolean = False
            Dim newtop As String
            Dim a As TextReader = New StreamReader(OutputBOX.Text)

            lines = a.ReadLine
            rest = a.ReadToEnd

            Try
                infields = Split(lines, ",")
                'Dim reslt As DialogResult

                'If UBound(infields) <> 1 Then
                ' reslt = MsgBox("This file does not appear to be valid MARXAN file. " + vbCrLf + "Continue?" + vbCrLf + "Only the first two fields will be used.  Additional fields will be ignored and the program may not run correctly.", vbYesNo)
                'End If


                Dim i As Long

                'If ((reslt = Windows.Forms.DialogResult.Yes) Or (reslt = Windows.Forms.DialogResult.OK)) Then
                newtop = ""
                For i = 0 To UBound(infields)
                    'infields(i) = Replace(infields(i), Chr(34), "")
                    infields(i) = Replace(Replace(infields(i), " ", "_"), Chr(34), "") ', 8
                    MARXANFIELD.Items.Add(infields(i))
                    MARXANPUFIELD.Items.Add(infields(i))
                    newtop = newtop + infields(i) + ","
                Next i

                newtop = newtop.Trim(",") 'Left(newtop, Len(newtop) - 1)
                'MsgBox(newtop)

                MARXANPUFIELD.SelectedIndex = 0
                MARXANPUFIELD.Enabled = True
                TextBox2.Text = Replace(infields(1), Chr(34), "")

                MARXANFIELD.SelectedIndex = 1
                MARXANFIELD.Enabled = True
                TextBox2.Text = Replace(infields(1), Chr(34), "")
                'Else
                '    OutputBOX.Text = ""
                'End If
                bb = True
            Catch
                MsgBox("This file does not appear to be valid MARXAN file.", MsgBoxStyle.Critical, "MARXAN Join and Display")
            End Try

            a.Close()

            If bb Then
                Dim b As TextWriter = New StreamWriter(OutputBOX.Text, False)
                b.WriteLine(newtop)
                b.Write(rest)
                b.Close()
            End If

        End If
    End Sub



End Class

