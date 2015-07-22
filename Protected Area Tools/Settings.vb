Imports System.Runtime.InteropServices

Public Class Settings
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()
        Dim op As New PATSettings(My.ArcMap.Application)


        op.Show(New Win32HWNDWrapper(My.ArcMap.Application.hWnd))

    End Sub

    Protected Overrides Sub OnUpdate()

    End Sub
End Class
