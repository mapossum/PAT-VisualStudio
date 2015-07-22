Imports ESRI.ArcGIS.Geometry

Public Class ERS_REC
    Public ERS_LAYERS() As ERS_LAYER
    Public outpath As String
    Public outname As String
    Public cellsize As Double
    Public maxval As Double
    Public combofunc As String
    Public minscale As Double
    Public maxscale As Double
    Public extent As IEnvelope

    Public Structure ERS_LAYER
        Dim pgpLayer As ESRI.ArcGIS.DataSourcesFile.IGPLayer
        Dim layertype As laytype
        Dim geotype As String
        Dim infdis As String
        Dim weight As String
        Dim intensity As String
        Dim decaytype As decay_type
        Dim decayrate As Double
        Dim combofunk As String
    End Structure

    Public Enum decay_type
        linear = 0
        concave = 1
        convex = 2
        constant = 3
    End Enum

    Public Enum laytype
        feature = 1
        raster = 2
        invalid = 3
    End Enum

    Public Sub New(ByVal num_recs As Integer)
        ReDim ERS_LAYERS(num_recs - 1)
    End Sub
End Class


