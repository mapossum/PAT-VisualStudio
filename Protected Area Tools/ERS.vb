Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI

<ComClass(ERS.ClassId, ERS.InterfaceId, ERS.EventsId), _
 ProgId("TNC_ERS_RBI_MARXAN.ERS")> _
Public NotInheritable Class ERS
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "183fae2e-97d0-4b8d-aa0c-e06cc0c56c02"
    Public Const InterfaceId As String = "9fbe7c17-c2a4-47c6-94da-e5b4e99fd280"
    Public Const EventsId As String = "2f90d90d-c7d7-4fb5-b65e-bc22c351e56b"
#End Region

#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryRegistration(registerType)

        'Add any COM registration code after the ArcGISCategoryRegistration() call

    End Sub

    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryUnregistration(registerType)

        'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Register(regKey)

    End Sub
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

    Private m_application As IApplication

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        MyBase.m_category = "TNC Tool"  'localizable text 
        MyBase.m_caption = "ERS"   'localizable text 
        MyBase.m_message = "This Tool Executes the Enviornmental Risk Surface Generator"   'localizable text 
        MyBase.m_toolTip = "This Tool Executes the Enviornmental Risk Surface Generator" 'localizable text 
        MyBase.m_name = "ERS_TOOL"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")


        Try
            'TODO: change bitmap name if necessary
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try



    End Sub


    Public Overrides Sub OnCreate(ByVal hook As Object)

        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)

            'Disable if it is not ArcMap
            If TypeOf hook Is IMxApplication Then
                MyBase.m_enabled = True
            Else
                MyBase.m_enabled = False
            End If
        End If

        'Dim randy As New Random()
        'Dim tell As Integer
        'tell = randy.Next(1, 101)
        'Dim splasher As New splash
        'If tell < 50 Then
        '    splasher.PictureBox1.Visible = True
        '    splasher.PictureBox2.Visible = False
        'Else
        '    splasher.PictureBox1.Visible = False
        '    splasher.PictureBox2.Visible = True
        'End If
        'splasher.Show()
        'm_application.Visible = True


    End Sub


    Public Overrides Sub OnClick()
        'TODO: Add ERS.OnClick implementation
        Dim mdoc As IMxDocument
        mdoc = m_application.Document

        If mdoc.ActiveView.FocusMap.LayerCount = 0 Then
            MsgBox("You must have at least one layer loaded to run this tool", MsgBoxStyle.Exclamation, "Please add data to the map")
            Exit Sub
        Else
            Dim thing As ERS_FORM
            thing = New ERS_FORM(m_application)
            thing.Show(New Win32HWNDWrapper(m_application.hWnd))
        End If

    End Sub
End Class



