Public Class MDS_Code
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()
        Dim ESRF As New MDS_Form(My.ArcMap.Application)
        ESRF.Show(New Win32HWNDWrapper(My.ArcMap.Application.hWnd))
  End Sub

  Protected Overrides Sub OnUpdate()

  End Sub
End Class
