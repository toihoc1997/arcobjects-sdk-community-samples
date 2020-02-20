'Copyright 2019 Esri

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

'See the License for the specific language governing permissions and
'limitations under the License.
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.GeomeTry
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports System.Runtime.InteropServices

<ComClass(PanRight.ClassId, PanRight.InterfaceId, PanRight.EventsId)> _
Public NotInheritable Class PanRight
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "E9CE29CD-CB4C-45AB-A7BE-CC9436A3C222"
    Public Const InterfaceId As String = "7315816A-64B2-46A1-BB1F-8AF35FFF8FFE"
    Public Const EventsId As String = "DDEFED22-8298-4121-9B8B-05D1586B6AB6"
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
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsCommands.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsCommands.Unregister(regKey)

    End Sub

#End Region
#End Region

    Private m_pHookHelper As IHookHelper

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        MyBase.m_category = "Sample_Pan_VBNET/Zoom"
        MyBase.m_caption = "Pan Right"
        MyBase.m_message = "Pan display right by the pan factor percentage"
        MyBase.m_toolTip = "Pan Right"
        MyBase.m_name = "Sample_Pan/Zoom_Pan Right"

        Dim res() As String = GetType(PanRight).Assembly.GetManifestResourceNames()
        If res.GetLength(0) > 0 Then
            MyBase.m_bitmap = New System.Drawing.Bitmap(GetType(PanRight).Assembly.GetManifestResourceStream("PanZoomVBNET.PanRight.bmp"))
        End If
        m_pHookHelper = New HookHelperClass
    End Sub

    Public Overrides Sub OnClick()

        If m_pHookHelper Is Nothing Then
            Return
        End If

        'Get the active view
        Dim pActiveView As IActiveView = CType(m_pHookHelper.FocusMap, IActiveView)

        'Get the extent
        Dim pEnvelope As IEnvelope = CType(pActiveView.Extent, IEnvelope)

        'Create a point to pan to
        Dim pPoint As IPoint
        pPoint = New PointClass
        pPoint.X = ((pEnvelope.XMin + pEnvelope.XMax) / 2) + (pEnvelope.Height / (100 / GetPanFactor()))
        pPoint.Y = ((pEnvelope.YMin + pEnvelope.YMax) / 2)

        'Center the envelope on the point
        pEnvelope.CenterAt(pPoint)

        'Set the new extent
        pActiveView.Extent = pEnvelope

        'Refresh the active view
        pActiveView.Refresh()
    End Sub

    Private Function GetPanFactor() As Long
        Return 50
    End Function

    Public Overrides Sub OnCreate(ByVal hook As Object)
        m_pHookHelper.Hook = hook
    End Sub
End Class


