Imports ESRI.ArcGIS.ArcMapUI

Public Class JoinDiplayOutput
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()

        Dim mdoc As IMxDocument
        mdoc = My.ArcMap.Document

        If mdoc.ActiveView.FocusMap.LayerCount = 0 Then
            MsgBox("You must have at least one layer loaded to run this tool", MsgBoxStyle.Exclamation, "Please add data to the map")
            Exit Sub
        Else

            Dim MJF As New MARXAN_JOIN_FORM(My.ArcMap.Application)
            'get handle to make owner
            MJF.Show(New Win32HWNDWrapper(My.ArcMap.Application.hWnd))

        End If

    End Sub

    Protected Overrides Sub OnUpdate()

    End Sub
End Class
