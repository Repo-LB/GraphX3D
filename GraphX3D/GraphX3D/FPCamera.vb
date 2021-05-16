Option Strict On

Public Class FPCamera

    'Class Variables
    Private c_location, c_forward, c_right, c_upward, c_target As PVector
    Private c_yaw, c_pitch As Single
    Private c_viewMatrices As Matrices

    'Class Constructor
    Public Sub New()
        c_location = New PVector(0, 0, 0, 0)
        c_forward = New PVector(0, 0, 1, 0)
        c_upward = New PVector(0, 1, 0, 0)
        c_right = New PVector(1, 0, 0, 0)
        c_yaw = 0 : c_pitch = 0
        c_viewMatrices = Matrices.Inverse(Matrices.Create_LookAt(c_location, PVector.Add(c_location, c_forward), c_upward))
    End Sub

    'Camera ReadOnly Properties
    Public ReadOnly Property ForwardVector As PVector
        Get
            Return c_forward
        End Get
    End Property
    Public ReadOnly Property UpwardVector As PVector
        Get
            Return c_upward
        End Get
    End Property
    Public ReadOnly Property RightVector As PVector
        Get
            Return c_right
        End Get
    End Property
    Public ReadOnly Property ViewMatrices As Matrices
        Get
            Return c_viewMatrices
        End Get
    End Property

    'Camera Modifiable Properties
    Public Property Location As PVector
        Get
            Return c_location
        End Get
        Set(Cam_Location As PVector)
            c_location = Cam_Location
        End Set
    End Property
    Public Property Yaw As Single
        Get
            Return c_yaw
        End Get
        Set(Cam_Yaw As Single)
            c_yaw = Cam_Yaw
        End Set
    End Property
    Public Property Pitch As Single
        Get
            Return c_pitch
        End Get
        Set(Cam_Pitch As Single)
            c_pitch = Cam_Pitch
        End Set
    End Property

    'Camera Functions
    Public Sub Update()
        'Updates the view of the Camera based on Yaw, Pitch and Location 
        Dim c_top As New PVector(0, 1, 0, 0)
        Dim MYaw As Matrices = Matrices.Create_RotationY(c_yaw)
        Dim MPitch As Matrices = Matrices.Create_RotationX(c_pitch)
        Dim MRotation As Matrices = Matrices.Combine(MYaw, MPitch)
        c_target = New PVector(0, 0, 1, 0) : MYaw.MultiplyVector(c_target)
        c_right = New PVector(1, 0, 0, 0) : MYaw.MultiplyVector(c_right)
        c_forward = c_target : MPitch.MultiplyVector(c_target) : c_target = PVector.Add(c_location, c_target)
        c_viewMatrices = Matrices.Inverse(Matrices.Create_LookAt(c_location, c_target, c_top))
    End Sub

End Class
