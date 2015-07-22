Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI
Imports System.IO

Public Class ERS_Code
    Inherits ESRI.ArcGIS.Desktop.AddIns.Button

    Public Sub New()

    End Sub

    Protected Overrides Sub OnClick()

        Dim mdoc As IMxDocument
        mdoc = My.ArcMap.Document
        'Dim mlay As ESRI.ArcGIS.Carto.ILayer

        'Dim mDsName As IDatasetName
        'Dim i As Long
        'For i = 0 To mdoc.ActiveView.FocusMap.LayerCount - 1
        'mlay = mdoc.ActiveView.FocusMap.Layer(i)
        'MsgBox(mlay.Name)
        'Next i

        If mdoc.ActiveView.FocusMap.LayerCount = 0 Then
            MsgBox("You must have at least one layer loaded to run this tool", MsgBoxStyle.Exclamation, "Please add data to the map")
            Exit Sub
        Else

            Dim ESRF As New ERS_FORM(My.ArcMap.Application)
            'get handle to make owner
            ESRF.Show(New Win32HWNDWrapper(My.ArcMap.Application.hWnd))

        End If

    End Sub

    Protected Overrides Sub OnUpdate()

    End Sub
End Class
