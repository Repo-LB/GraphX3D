Imports GraphX3D

Public Class Form1

    'Set projection/canvas size
    Dim int_View_Width As Integer = 1024
    Dim int_View_Height As Integer = 768
    Dim mat_Projection As Matrices = Matrices.Create_Projection(int_View_Height / int_View_Width, 90, 1, 1000)

    'Setup varibles for very basic shader
    Dim pv_LightVector As New PVector(0, 0, -1, 0)
    Dim col_MainColor As Color = Color.FromArgb(255, 255, 255)

    'Add a camera
    Dim cam_MainCam As FPCamera = New FPCamera

    'Setup mouse control variables
    Dim pt_WindowLocation As Point = Me.PointToScreen(New Point(0, 0))
    Dim pt_WindowCenterPoint = New Point(pt_WindowLocation.X + (Size.Width / 2), pt_WindowLocation.Y + (Me.Size.Height / 2))
    Dim pt_Mouse_Position As Point
    Dim sng_MouseSensitivity As Single = 5.0
    Dim bool_MouseLocked As Boolean = True

    'Setup additional variables
    Dim sng_MovementSpeedModifier As Single = 0.9
    Dim bool_ShowEdges As Boolean = False

    'Create or import objects for your world
    Dim msh_Cube As MeshObject = MeshObject.FromObjectFile(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\Cube.obj")
    Dim msh_Cone As MeshObject = MeshObject.FromObjectFile(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\Cone.obj")
    Dim msh_Sphere As MeshObject = MeshObject.FromObjectFile(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\Sphere.obj")
    Dim list_WorldMeshes As New List(Of MeshObject)

    '___APPLICATION_INIT

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Set the cursor position to the center of the window at startup
        Cursor.Position = pt_WindowCenterPoint : pt_Mouse_Position = pt_WindowCenterPoint

        'Position your objects with scaling/rotation/translation
        msh_Cube.PositionMatrix = Matrices.Combine(Matrices.Create_Scale(1, 1, 1), Matrices.Create_Translation(3, 0.5, -5))
        msh_Cone.PositionMatrix = Matrices.Combine(Matrices.Create_Scale(9, 9, 9), Matrices.Create_Translation(-3, 0.5, 5))
        msh_Sphere.PositionMatrix = Matrices.Combine(Matrices.Create_Scale(7, 7, 7), Matrices.Create_Translation(0, 0.5, 12))

        'Add all objects to world list
        list_WorldMeshes.Add(msh_Cube)
        list_WorldMeshes.Add(msh_Cone)
        list_WorldMeshes.Add(msh_Sphere)

    End Sub

    '___MOUSE_CONTROLS

    Private Sub tmr_MouseLock_Tick(sender As Object, e As EventArgs) Handles tmr_MouseLock.Tick

        'Timer for continuous mouse movement
        bool_MouseLocked = False
        MouseUpdate()

    End Sub
    Private Sub MouseUpdate()

        'Update windows location and centerpoint
        pt_WindowLocation = Me.PointToScreen(New Point(0, 0))
        pt_WindowCenterPoint = New Point(pt_WindowLocation.X + (Size.Width / 2), pt_WindowLocation.Y + (Me.Size.Height / 2))

        'Reset mouse location to centerpoint
        Cursor.Position = pt_WindowCenterPoint : pt_Mouse_Position = Nothing

    End Sub
    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove

        'Check for difference in mouse position and adjust camera Yaw accordingly
        If tmr_MouseLock.Enabled = True And bool_MouseLocked = True Then

            If pt_Mouse_Position <> Nothing Then
                cam_MainCam.Yaw += (e.X - pt_Mouse_Position.X) / sng_MouseSensitivity
                'Camera Pitch could go here
            End If
            pt_Mouse_Position = e.Location
            Me.Invalidate()

        Else
            bool_MouseLocked = True
        End If

    End Sub

    '___KEYBOARD_CONTROLS

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        'Select appropriate action based on keycode
        Select Case e.KeyCode
            Case Keys.W
                tmr_Forward.Enabled = True
            Case Keys.S
                tmr_Backward.Enabled = True
            Case Keys.A
                tmr_Left.Enabled = True
            Case Keys.D
                tmr_Right.Enabled = True
            Case Keys.Space
                tmr_Upward.Enabled = True
            Case Keys.ShiftKey
                tmr_Downward.Enabled = True
            Case Keys.Back
                'Show/Hide Edges
                If bool_ShowEdges = True Then
                    bool_ShowEdges = False
                Else
                    bool_ShowEdges = True
                End If
                Me.Invalidate()
            Case Keys.Enter
                'Reposition Light Source
                pv_LightVector = PVector.Substract(cam_MainCam.Location, cam_MainCam.ForwardVector)
                pv_LightVector = PVector.Substract(pv_LightVector, cam_MainCam.Location)
                Me.Invalidate()
            Case Keys.Escape
                'Lock/Unlock Mouse
                If tmr_MouseLock.Enabled = True Then
                    tmr_MouseLock.Enabled = False
                Else
                    tmr_MouseLock.Enabled = True
                End If
        End Select

    End Sub
    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        'Disable movement timers uppon key release
        Select Case e.KeyCode
            Case Keys.W
                tmr_Forward.Enabled = False
            Case Keys.S
                tmr_Backward.Enabled = False
            Case Keys.A
                tmr_Left.Enabled = False
            Case Keys.D
                tmr_Right.Enabled = False
            Case Keys.Space
                tmr_Upward.Enabled = False
            Case Keys.ShiftKey
                tmr_Downward.Enabled = False
        End Select
    End Sub

    'Keyboard movement timers, toggled by keyup/keydown
    Private Sub tmr_Forward_Tick(sender As Object, e As EventArgs) Handles tmr_Forward.Tick
        cam_MainCam.Location = PVector.Add(cam_MainCam.Location, PVector.Multiply(cam_MainCam.ForwardVector, sng_MovementSpeedModifier))
        Me.Invalidate()
    End Sub
    Private Sub tmr_Backward_Tick(sender As Object, e As EventArgs) Handles tmr_Backward.Tick
        cam_MainCam.Location = PVector.Substract(cam_MainCam.Location, PVector.Multiply(cam_MainCam.ForwardVector, sng_MovementSpeedModifier))
        Me.Invalidate()
    End Sub
    Private Sub tmr_Left_Tick(sender As Object, e As EventArgs) Handles tmr_Left.Tick
        cam_MainCam.Location = PVector.Substract(cam_MainCam.Location, PVector.Multiply(cam_MainCam.RightVector, sng_MovementSpeedModifier))
        Me.Invalidate()
    End Sub
    Private Sub tmr_Right_Tick(sender As Object, e As EventArgs) Handles tmr_Right.Tick
        cam_MainCam.Location = PVector.Add(cam_MainCam.Location, PVector.Multiply(cam_MainCam.RightVector, sng_MovementSpeedModifier))
        Me.Invalidate()
    End Sub
    Private Sub tmr_Upward_Tick(sender As Object, e As EventArgs) Handles tmr_Upward.Tick
        cam_MainCam.Location = PVector.Substract(cam_MainCam.Location, PVector.Multiply(cam_MainCam.UpwardVector, sng_MovementSpeedModifier))
        Me.Invalidate()
    End Sub
    Private Sub tmr_Downward_Tick(sender As Object, e As EventArgs) Handles tmr_Downward.Tick
        cam_MainCam.Location = PVector.Add(cam_MainCam.Location, PVector.Multiply(cam_MainCam.UpwardVector, sng_MovementSpeedModifier))
        Me.Invalidate()
    End Sub

    '___APPLICATION_REPAINT

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint

        'Update the camera and set the projection matrix accordingly
        cam_MainCam.Update()
        Dim MProjectView As Matrices = Matrices.Combine(mat_Projection, cam_MainCam.ViewMatrix)

        'Create Canvas(Paint Buffer)
        Dim CNV As New Bitmap(1024, 768)
        Dim G As Graphics = e.Graphics

        'Loop trough objects in your world
        For Each WorldObject As MeshObject In list_WorldMeshes

            'Clone the original object and apply Position/Offset matrix
            For Each Face As MeshObject.TriFace In WorldObject.Faces
                Dim PreProjectionTriangle As MeshObject.TriFace = Face.Clone
                For Each Vertex In PreProjectionTriangle.Vertices
                    WorldObject.PositionMatrix.MultiplyVector(Vertex)
                Next

                'Check if object faces point toward the camera(Backface culling)
                Dim Camera_Ray As PVector = PVector.Substract(PreProjectionTriangle.Vertex_A, cam_MainCam.Location)
                If PVector.GetDotProduct(PreProjectionTriangle.UnitVectorNormal, Camera_Ray) < 0 Then

                    'Apply basic shader effect (To Include In Library)
                    Dim Light_Ray As Single = PVector.GetDotProduct(PVector.GetUnitVector(pv_LightVector), PreProjectionTriangle.UnitVectorNormal)
                    Light_Ray = PVector.GetDotProduct(PVector.GetUnitVector(pv_LightVector), PreProjectionTriangle.UnitVectorNormal)
                    If Light_Ray > 0 Then
                        col_MainColor = Color.FromArgb(255 * Light_Ray / 2 + 127, 255 * Light_Ray / 2 + 127, 255 * Light_Ray / 2 + 127)
                    Else
                        Dim Inv_Light_Ray = 1 + Light_Ray
                        col_MainColor = Color.FromArgb(255 * Inv_Light_Ray / 2, 255 * Inv_Light_Ray / 2, 255 * Inv_Light_Ray / 2)
                    End If

                    'Clip triangles to the near plane
                    Dim ZClippedTriangles As MeshObject.TriFace() = Rasterizer.TriClip(PVector.Add(cam_MainCam.Location, cam_MainCam.ForwardVector), cam_MainCam.ForwardVector, PreProjectionTriangle)
                    For Each ZValidatedTriangle As MeshObject.TriFace In ZClippedTriangles

                        If ZValidatedTriangle IsNot Nothing Then

                            'Project triangles and convert to screen space
                            Dim PreRasterTriangle As MeshObject.TriFace = ZValidatedTriangle.Clone
                            MProjectView.MultiplyVector(PreRasterTriangle.Vertex_A) : MProjectView.MultiplyVector(PreRasterTriangle.Vertex_B) : MProjectView.MultiplyVector(PreRasterTriangle.Vertex_C)
                            PreRasterTriangle.Vertex_A = PVector.ScreenTransform(PVector.PerspectiveDivide(PreRasterTriangle.Vertex_A), int_View_Width, int_View_Height)
                            PreRasterTriangle.Vertex_B = PVector.ScreenTransform(PVector.PerspectiveDivide(PreRasterTriangle.Vertex_B), int_View_Width, int_View_Height)
                            PreRasterTriangle.Vertex_C = PVector.ScreenTransform(PVector.PerspectiveDivide(PreRasterTriangle.Vertex_C), int_View_Width, int_View_Height)

                            Dim TrianglesToRaster As New List(Of MeshObject.TriFace)
                            TrianglesToRaster.Add(PreRasterTriangle)

                            'Clip triangles with canvas size
                            For TestedPlanes = 0 To 3
                                Dim ValidatedTriangles As New List(Of MeshObject.TriFace)
                                For Each TriangleToClip As MeshObject.TriFace In TrianglesToRaster
                                    Select Case TestedPlanes
                                        Case 0
                                            Dim ClippedTriangles As MeshObject.TriFace() = Rasterizer.TriClip(New PVector(0, 0, 0), New PVector(0, 1, 0), TriangleToClip)
                                            If ClippedTriangles(0) IsNot Nothing Then
                                                ValidatedTriangles.Add(ClippedTriangles(0)) : End If
                                            If ClippedTriangles(1) IsNot Nothing Then
                                                ValidatedTriangles.Add(ClippedTriangles(1)) : End If
                                        Case 1
                                            Dim ClippedTriangles As MeshObject.TriFace() = Rasterizer.TriClip(New PVector(int_View_Width - 1, 0, 0), New PVector(-1, 0, 0), TriangleToClip)
                                            If ClippedTriangles(0) IsNot Nothing Then
                                                ValidatedTriangles.Add(ClippedTriangles(0)) : End If
                                            If ClippedTriangles(1) IsNot Nothing Then
                                                ValidatedTriangles.Add(ClippedTriangles(1)) : End If
                                        Case 2
                                            Dim ClippedTriangles As MeshObject.TriFace() = Rasterizer.TriClip(New PVector(0, int_View_Height - 1, 0), New PVector(0, -1, 0), TriangleToClip)
                                            If ClippedTriangles(0) IsNot Nothing Then
                                                ValidatedTriangles.Add(ClippedTriangles(0)) : End If
                                            If ClippedTriangles(1) IsNot Nothing Then
                                                ValidatedTriangles.Add(ClippedTriangles(1)) : End If
                                        Case 3
                                            Dim ClippedTriangles As MeshObject.TriFace() = Rasterizer.TriClip(New PVector(0, 0, 0), New PVector(1, 0, 0), TriangleToClip)
                                            If ClippedTriangles(0) IsNot Nothing Then
                                                ValidatedTriangles.Add(ClippedTriangles(0)) : End If
                                            If ClippedTriangles(1) IsNot Nothing Then
                                                ValidatedTriangles.Add(ClippedTriangles(1)) : End If
                                    End Select
                                Next
                                TrianglesToRaster = ValidatedTriangles
                            Next

                            'Draw validated triangle list to canvas
                            If TrianglesToRaster.Count > 0 Then
                                For Each Triangle As MeshObject.TriFace In TrianglesToRaster

                                    If Triangle IsNot Nothing Then
                                        Dim P0 As New Point(Triangle.Vertex_A.X, Triangle.Vertex_A.Y)
                                        Dim P1 As New Point(Triangle.Vertex_B.X, Triangle.Vertex_B.Y)
                                        Dim P2 As New Point(Triangle.Vertex_C.X, Triangle.Vertex_C.Y)

                                        Rasterizer.RenderTriangle(Triangle, CNV, col_MainColor)

                                        'Draw edges if option is turned on
                                        If bool_ShowEdges = True Then
                                            Rasterizer.RenderLine(CNV, P0, P1, Color.Red)
                                            Rasterizer.RenderLine(CNV, P1, P2, Color.Red)
                                            Rasterizer.RenderLine(CNV, P0, P2, Color.Red)
                                        End If

                                    End If
                                Next
                            End If

                        End If
                    Next

                End If
            Next

        Next

        'Paint canvas to screen
        G.DrawImage(CNV, 0, 0)
        'Paint instructions to screen
        G.DrawString("Press (ENTER) to change the light source to your current location" & vbCrLf & "Press (BACKSPACE) to toggle wireframe drawing" & vbCrLf & "Press (ESCAPE) to lock/unlock mouse control", DefaultFont, Brushes.Black, New Point(15, 15))

    End Sub
End Class