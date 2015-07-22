Public Class HEXGEN_Code
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()

        Dim HGF As New HEXGEN_FORM(My.ArcMap.Application)
        'get handle to make owner
        HGF.Show(New Win32HWNDWrapper(My.ArcMap.Application.hWnd))

  End Sub

  Protected Overrides Sub OnUpdate()

  End Sub
End Class
