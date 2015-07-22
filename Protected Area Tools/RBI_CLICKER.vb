Imports System.Windows.Forms
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geoprocessing
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.DataSourcesFile
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.DataManagementTools
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI


Public Class RBI_CLICKER
    Public m_application As IMxApplication
    Private layersinfo As RBI_REC

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim i As Integer

        For i = 0 To layersinfo.TARGET_LAYERS.Length - 1
            layersinfo.TARGET_LAYERS(i).fieldname = Main_Panel.Controls((2 * i) + 1).Text
        Next i

        Me.Hide()
        Dim RBIProcess As ProcessUpdate = New ProcessUpdate(m_application, "RBI")
        Dim suc As Boolean
        suc = RBIProcess.ProcessRBI(layersinfo)

        RBIProcess.Close()

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub RBI_CLICKER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Location = New System.Drawing.Point(12, 12)
        Label2.Location = New System.Drawing.Point(180, 12)
        Dim gpu As IGPUtilities = New ESRI.ArcGIS.Geoprocessing.GPUtilities
        Dim pfc As IFeatureClass

        Dim i, j As Long
        For i = 0 To layersinfo.TARGET_LAYERS.Length - 1
            'CreateControlLine(i, layersinfo.TARGET_LAYERS(i).TARGET_LAYER.NameString)
            Dim lbl As New TextBox()
            lbl.ReadOnly = True
            lbl.Location = New System.Drawing.Point(12, 4 + (i * 27))
            lbl.Size = New System.Drawing.Size(150, 20)
            lbl.Text = layersinfo.TARGET_LAYERS(i).TARGET_LAYER.NameString
            Main_Panel.Controls.Add(lbl)

            If i < 10 Then
                Me.Size = (New System.Drawing.Size(390, 150 + (i * 27)))
                Main_Panel.Size = (New System.Drawing.Size(380, 27 + (i * 27)))
            Else
                Me.Size = (New System.Drawing.Size(390, 150 + (10 * 27)))
                Main_Panel.Size = (New System.Drawing.Size(380, 27 + (10 * 27)))
            End If

            Dim cbo1 As New ComboBox
            cbo1.Location = New System.Drawing.Point(180, 2 + (i * 27))
            cbo1.Size = New System.Drawing.Size(180, 20)
            cbo1.DropDownStyle = ComboBoxStyle.DropDownList
            'cbo1.SelectedIndex = 0
            Main_Panel.Controls.Add(cbo1)

            pfc = gpu.OpenFeatureClassFromString(layersinfo.TARGET_LAYERS(i).TARGET_LAYER.DataElement.GetPath & "\" & layersinfo.TARGET_LAYERS(i).TARGET_LAYER.DataElement.Name)
            For j = 0 To pfc.Fields.FieldCount - 1
                If (pfc.Fields.Field(j).Type = esriFieldType.esriFieldTypeInteger Or pfc.Fields.Field(j).Type = esriFieldType.esriFieldTypeSingle Or pfc.Fields.Field(j).Type = esriFieldType.esriFieldTypeDouble Or pfc.Fields.Field(j).Type = esriFieldType.esriFieldTypeSmallInteger Or pfc.Fields.Field(j).Type = esriFieldType.esriFieldTypeString) Then
                    cbo1.Items.Add(pfc.Fields.Field(j).Name)
                    'MsgBox(pfc.Fields.FieldCount)
                    'MsgBox(layersinfo.TARGET_LAYERS(i).TARGET_LAYER.DataElement.GetPath)
                    'add to info
                End If
            Next j
            cbo1.SelectedIndex = 0
        Next i


    End Sub


    Public Sub New(ByRef inlayersinfo As RBI_REC, ByVal m_app As IMxApplication)

        ' This call is required by the Windows Form Designer.
        m_application = m_app
        InitializeComponent()
        layersinfo = inlayersinfo
        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Public Function getvaljfields(ByVal inLayer As IFeatureLayer) As List(Of String)
        Dim that As New List(Of String)
        Dim pfclass As IFeatureClass
        Dim pfld As IField
        Dim i As Integer

        pfclass = inLayer.FeatureClass
        For i = 0 To pfclass.Fields.FieldCount - 1
            pfld = pfclass.Fields.Field(i)
            '   If pfld.Type = esriFieldType.esriFieldTypeInteger Or pfld.Type = esriFieldType.esriFieldTypeSmallInteger Or pfld.Type = esriFieldType.esriFieldTypeDouble _
            '                                         Or pfld.Type = esriFieldType.esriFieldTypeString Or pfld.Type = esriFieldType.esriFieldTypeInteger Then

            that.Add(pfld.Name)
            ' End If
        Next
        Return that

    End Function

End Class
