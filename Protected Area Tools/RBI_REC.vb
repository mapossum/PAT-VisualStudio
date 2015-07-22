Public Class RBI_REC
    Public TARGET_LAYERS() As TAR_REC
    Public pulayer As ESRI.ArcGIS.DataSourcesFile.IGPLayer
    Public extentlayer As ESRI.ArcGIS.DataSourcesFile.IGPLayer
    Public outpath As String
    Public outname As String
    Public pufield As String

    Public Sub New(ByVal num_recs As Integer)
        ReDim TARGET_LAYERS(num_recs - 1)
    End Sub

    Public Structure TAR_REC
        Dim TARGET_LAYER As ESRI.ArcGIS.DataSourcesFile.IGPLayer
        Dim fieldname As String
    End Structure
End Class
