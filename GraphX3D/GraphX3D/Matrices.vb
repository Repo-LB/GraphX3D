Public Class Matrices

    'Class Variables
    Private arr_Mat(3, 3) As Single

    'Class Constructor
    Public Sub New()
        arr_Mat(0, 0) = 1 : arr_Mat(1, 0) = 0 : arr_Mat(2, 0) = 0 : arr_Mat(3, 0) = 0
        arr_Mat(0, 1) = 0 : arr_Mat(1, 1) = 1 : arr_Mat(2, 1) = 0 : arr_Mat(3, 1) = 0
        arr_Mat(0, 2) = 0 : arr_Mat(1, 2) = 0 : arr_Mat(2, 2) = 1 : arr_Mat(3, 2) = 0
        arr_Mat(0, 3) = 0 : arr_Mat(1, 3) = 0 : arr_Mat(2, 3) = 0 : arr_Mat(3, 3) = 1
    End Sub

    'Matrices ReadOnly Properties
    Private ReadOnly Property Values As Single(,)
        Get
            Return arr_Mat
        End Get
    End Property

    'Class Functions(Create Matrices)
    Public Shared Function Create_Translation(ByVal XTranslation As Single, ByVal YTranslation As Single, ByVal ZTranslation As Single)

        Dim MAT As New Matrices
        MAT.Values(0, 0) = 1 : MAT.Values(1, 0) = 0 : MAT.Values(2, 0) = 0 : MAT.Values(3, 0) = XTranslation
        MAT.Values(0, 1) = 0 : MAT.Values(1, 1) = 1 : MAT.Values(2, 1) = 0 : MAT.Values(3, 1) = YTranslation
        MAT.Values(0, 2) = 0 : MAT.Values(1, 2) = 0 : MAT.Values(2, 2) = 1 : MAT.Values(3, 2) = ZTranslation
        MAT.Values(0, 3) = 0 : MAT.Values(1, 3) = 0 : MAT.Values(2, 3) = 0 : MAT.Values(3, 3) = 1
        Return MAT

    End Function
    Public Shared Function Create_Scale(ByVal XScale As Single, ByVal YScale As Single, ByVal ZScale As Single)

        Dim MAT As New Matrices
        MAT.Values(0, 0) = XScale : MAT.Values(1, 0) = 0 : MAT.Values(2, 0) = 0 : MAT.Values(3, 0) = 0
        MAT.Values(0, 1) = 0 : MAT.Values(1, 1) = YScale : MAT.Values(2, 1) = 0 : MAT.Values(3, 1) = 0
        MAT.Values(0, 2) = 0 : MAT.Values(1, 2) = 0 : MAT.Values(2, 2) = ZScale : MAT.Values(3, 2) = 0
        MAT.Values(0, 3) = 0 : MAT.Values(1, 3) = 0 : MAT.Values(2, 3) = 0 : MAT.Values(3, 3) = 1
        Return MAT

    End Function
    Public Shared Function Create_RotationX(ByVal Angle As Single) As Matrices

        Dim Sin_T As Double = Math.Sin(Angle * Math.PI / 180)
        Dim Cos_T As Double = Math.Cos(Angle * Math.PI / 180)

        Dim MAT As New Matrices
        MAT.Values(0, 0) = 1 : MAT.Values(1, 0) = 0 : MAT.Values(2, 0) = 0 : MAT.Values(3, 0) = 0
        MAT.Values(0, 1) = 0 : MAT.Values(1, 1) = Cos_T : MAT.Values(2, 1) = Sin_T : MAT.Values(3, 1) = 0
        MAT.Values(0, 2) = 0 : MAT.Values(1, 2) = -Sin_T : MAT.Values(2, 2) = Cos_T : MAT.Values(3, 2) = 0
        MAT.Values(0, 3) = 0 : MAT.Values(1, 3) = 0 : MAT.Values(2, 3) = 0 : MAT.Values(3, 3) = 1
        Return MAT

    End Function
    Public Shared Function Create_RotationY(ByVal Angle As Single) As Matrices

        Dim Sin_T As Double = Math.Sin(Angle * Math.PI / 180)
        Dim Cos_T As Double = Math.Cos(Angle * Math.PI / 180)

        Dim MAT As New Matrices
        MAT.Values(0, 0) = Cos_T : MAT.Values(1, 0) = 0 : MAT.Values(2, 0) = Sin_T : MAT.Values(3, 0) = 0
        MAT.Values(0, 1) = 0 : MAT.Values(1, 1) = 1 : MAT.Values(2, 1) = 0 : MAT.Values(3, 1) = 0
        MAT.Values(0, 2) = -Sin_T : MAT.Values(1, 2) = 0 : MAT.Values(2, 2) = Cos_T : MAT.Values(3, 2) = 0
        MAT.Values(0, 3) = 0 : MAT.Values(1, 3) = 0 : MAT.Values(2, 3) = 0 : MAT.Values(3, 3) = 1
        Return MAT

    End Function
    Public Shared Function Create_RotationZ(ByVal Angle As Single) As Matrices

        Dim Sin_T As Double = Math.Sin(Angle * Math.PI / 180)
        Dim Cos_T As Double = Math.Cos(Angle * Math.PI / 180)

        Dim MAT As New Matrices
        MAT.Values(0, 0) = Cos_T : MAT.Values(1, 0) = Sin_T : MAT.Values(2, 0) = 0 : MAT.Values(3, 0) = 0
        MAT.Values(0, 1) = -Sin_T : MAT.Values(1, 1) = Cos_T : MAT.Values(2, 1) = 0 : MAT.Values(3, 1) = 0
        MAT.Values(0, 2) = 0 : MAT.Values(1, 2) = 0 : MAT.Values(2, 2) = 1 : MAT.Values(3, 2) = 0
        MAT.Values(0, 3) = 0 : MAT.Values(1, 3) = 0 : MAT.Values(2, 3) = 0 : MAT.Values(3, 3) = 1
        Return MAT

    End Function
    Public Shared Function Create_Projection(ByVal AspectRatio As Single, ByVal FOV As Single, ByVal ZNear As Single, ByVal ZFar As Single) As Matrices

        Dim ScFOV As Single = CSng(1 / Math.Tan((FOV * 0.5) * (Math.PI / 180)))

        Dim MAT As New Matrices
        MAT.Values(0, 0) = AspectRatio * ScFOV : MAT.Values(1, 0) = 0 : MAT.Values(2, 0) = 0 : MAT.Values(3, 0) = 0
        MAT.Values(0, 1) = 0 : MAT.Values(1, 1) = ScFOV : MAT.Values(2, 1) = 0 : MAT.Values(3, 1) = 0
        MAT.Values(0, 2) = 0 : MAT.Values(1, 2) = 0 : MAT.Values(2, 2) = ZFar / (ZFar - ZNear) : MAT.Values(3, 2) = -((ZFar * ZNear) / (ZFar - ZNear))
        MAT.Values(0, 3) = 0 : MAT.Values(1, 3) = 0 : MAT.Values(2, 3) = 1 : MAT.Values(3, 3) = 1
        Return MAT

    End Function
    Public Shared Function Create_LookAt(ByVal PositionVector As PVector, ByVal ForwardVector As PVector, ByVal UpVector As PVector) As Matrices

        Dim NewForward As PVector = PVector.GetUnitVector(PVector.Substract(ForwardVector, PositionVector))
        Dim NewRight As PVector = PVector.GetUnitVector(PVector.GetCrossProduct(UpVector, NewForward))
        Dim NewUp As PVector = PVector.GetCrossProduct(NewForward, NewRight)

        Dim MAT As New Matrices
        MAT.Values(0, 0) = NewRight.X : MAT.Values(1, 0) = NewUp.X : MAT.Values(2, 0) = NewForward.X : MAT.Values(3, 0) = PositionVector.X
        MAT.Values(0, 1) = NewRight.Y : MAT.Values(1, 1) = NewUp.Y : MAT.Values(2, 1) = NewForward.Y : MAT.Values(3, 1) = PositionVector.Y
        MAT.Values(0, 2) = NewRight.Z : MAT.Values(1, 2) = NewUp.Z : MAT.Values(2, 2) = NewForward.Z : MAT.Values(3, 2) = PositionVector.Z
        MAT.Values(0, 3) = 0 : MAT.Values(1, 3) = 0 : MAT.Values(2, 3) = 0 : MAT.Values(3, 3) = 1
        Return MAT

    End Function

    'Class Functions(Matrices Operations)
    Public Shared Function Inverse(ByVal Matrix As Matrices) As Matrices
        Dim MAT As New Matrices
        MAT.Values(0, 0) = Matrix.Values(0, 0) : MAT.Values(0, 1) = Matrix.Values(1, 0) : MAT.Values(0, 2) = Matrix.Values(2, 0) : MAT.Values(0, 3) = 0
        MAT.Values(1, 0) = Matrix.Values(0, 1) : MAT.Values(1, 1) = Matrix.Values(1, 1) : MAT.Values(1, 2) = Matrix.Values(2, 1) : MAT.Values(1, 3) = 0
        MAT.Values(2, 0) = Matrix.Values(0, 2) : MAT.Values(2, 1) = Matrix.Values(1, 2) : MAT.Values(2, 2) = Matrix.Values(2, 2) : MAT.Values(2, 3) = 0
        MAT.Values(3, 0) = -(Matrix.Values(3, 0) * MAT.Values(0, 0) + Matrix.Values(3, 1) * MAT.Values(1, 0) + Matrix.Values(3, 2) * MAT.Values(2, 0))
        MAT.Values(3, 1) = -(Matrix.Values(3, 0) * MAT.Values(0, 1) + Matrix.Values(3, 1) * MAT.Values(1, 1) + Matrix.Values(3, 2) * MAT.Values(2, 1))
        MAT.Values(3, 2) = -(Matrix.Values(3, 0) * MAT.Values(0, 2) + Matrix.Values(3, 1) * MAT.Values(1, 2) + Matrix.Values(3, 2) * MAT.Values(2, 2))
        MAT.Values(3, 3) = 1
        Return MAT
    End Function
    Public Shared Function Combine(ByVal Matrix_A As Matrices, ByVal Matrix_B As Matrices) As Matrices
        Dim MAT As New Matrices
        MAT.Values(0, 0) = Matrix_A.Values(0, 0) * Matrix_B.Values(0, 0) + Matrix_A.Values(1, 0) * Matrix_B.Values(0, 1) + Matrix_A.Values(2, 0) * Matrix_B.Values(0, 2) + Matrix_A.Values(3, 0) * Matrix_B.Values(0, 3)
        MAT.Values(0, 1) = Matrix_A.Values(0, 1) * Matrix_B.Values(0, 0) + Matrix_A.Values(1, 1) * Matrix_B.Values(0, 1) + Matrix_A.Values(2, 1) * Matrix_B.Values(0, 2) + Matrix_A.Values(3, 1) * Matrix_B.Values(0, 3)
        MAT.Values(0, 2) = Matrix_A.Values(0, 2) * Matrix_B.Values(0, 0) + Matrix_A.Values(1, 2) * Matrix_B.Values(0, 1) + Matrix_A.Values(2, 2) * Matrix_B.Values(0, 2) + Matrix_A.Values(3, 2) * Matrix_B.Values(0, 3)
        MAT.Values(0, 3) = Matrix_A.Values(0, 3) * Matrix_B.Values(0, 0) + Matrix_A.Values(1, 3) * Matrix_B.Values(0, 1) + Matrix_A.Values(2, 3) * Matrix_B.Values(0, 2) + Matrix_A.Values(3, 3) * Matrix_B.Values(0, 3)
        MAT.Values(1, 0) = Matrix_A.Values(0, 0) * Matrix_B.Values(1, 0) + Matrix_A.Values(1, 0) * Matrix_B.Values(1, 1) + Matrix_A.Values(2, 0) * Matrix_B.Values(1, 2) + Matrix_A.Values(3, 0) * Matrix_B.Values(1, 3)
        MAT.Values(1, 1) = Matrix_A.Values(0, 1) * Matrix_B.Values(1, 0) + Matrix_A.Values(1, 1) * Matrix_B.Values(1, 1) + Matrix_A.Values(2, 1) * Matrix_B.Values(1, 2) + Matrix_A.Values(3, 1) * Matrix_B.Values(1, 3)
        MAT.Values(1, 2) = Matrix_A.Values(0, 2) * Matrix_B.Values(1, 0) + Matrix_A.Values(1, 2) * Matrix_B.Values(1, 1) + Matrix_A.Values(2, 2) * Matrix_B.Values(1, 2) + Matrix_A.Values(3, 2) * Matrix_B.Values(1, 3)
        MAT.Values(1, 3) = Matrix_A.Values(0, 3) * Matrix_B.Values(1, 0) + Matrix_A.Values(1, 3) * Matrix_B.Values(1, 1) + Matrix_A.Values(2, 3) * Matrix_B.Values(1, 2) + Matrix_A.Values(3, 3) * Matrix_B.Values(1, 3)
        MAT.Values(2, 0) = Matrix_A.Values(0, 0) * Matrix_B.Values(2, 0) + Matrix_A.Values(1, 0) * Matrix_B.Values(2, 1) + Matrix_A.Values(2, 0) * Matrix_B.Values(2, 2) + Matrix_A.Values(3, 0) * Matrix_B.Values(2, 3)
        MAT.Values(2, 1) = Matrix_A.Values(0, 1) * Matrix_B.Values(2, 0) + Matrix_A.Values(1, 1) * Matrix_B.Values(2, 1) + Matrix_A.Values(2, 1) * Matrix_B.Values(2, 2) + Matrix_A.Values(3, 1) * Matrix_B.Values(2, 3)
        MAT.Values(2, 2) = Matrix_A.Values(0, 2) * Matrix_B.Values(2, 0) + Matrix_A.Values(1, 2) * Matrix_B.Values(2, 1) + Matrix_A.Values(2, 2) * Matrix_B.Values(2, 2) + Matrix_A.Values(3, 2) * Matrix_B.Values(2, 3)
        MAT.Values(2, 3) = Matrix_A.Values(0, 3) * Matrix_B.Values(2, 0) + Matrix_A.Values(1, 3) * Matrix_B.Values(2, 1) + Matrix_A.Values(2, 3) * Matrix_B.Values(2, 2) + Matrix_A.Values(3, 3) * Matrix_B.Values(2, 3)
        MAT.Values(3, 0) = Matrix_A.Values(0, 0) * Matrix_B.Values(3, 0) + Matrix_A.Values(1, 0) * Matrix_B.Values(3, 1) + Matrix_A.Values(2, 0) * Matrix_B.Values(3, 2) + Matrix_A.Values(3, 0) * Matrix_B.Values(3, 3)
        MAT.Values(3, 1) = Matrix_A.Values(0, 1) * Matrix_B.Values(3, 0) + Matrix_A.Values(1, 1) * Matrix_B.Values(3, 1) + Matrix_A.Values(2, 1) * Matrix_B.Values(3, 2) + Matrix_A.Values(3, 1) * Matrix_B.Values(3, 3)
        MAT.Values(3, 2) = Matrix_A.Values(0, 2) * Matrix_B.Values(3, 0) + Matrix_A.Values(1, 2) * Matrix_B.Values(3, 1) + Matrix_A.Values(2, 2) * Matrix_B.Values(3, 2) + Matrix_A.Values(3, 2) * Matrix_B.Values(3, 3)
        MAT.Values(3, 3) = Matrix_A.Values(0, 3) * Matrix_B.Values(3, 0) + Matrix_A.Values(1, 3) * Matrix_B.Values(3, 1) + Matrix_A.Values(2, 3) * Matrix_B.Values(3, 2) + Matrix_A.Values(3, 3) * Matrix_B.Values(3, 3)
        Return MAT
    End Function

    'Use Matrix
    Public Sub MultiplyVector(ByRef Vector As PVector)
        Dim PV As New PVector(Vector.X, Vector.Y, Vector.Z, Vector.W)
        Vector.X = PV.X * Values(0, 0) + PV.Y * Values(1, 0) + PV.Z * Values(2, 0) + PV.W * Values(3, 0)
        Vector.Y = PV.X * Values(0, 1) + PV.Y * Values(1, 1) + PV.Z * Values(2, 1) + PV.W * Values(3, 1)
        Vector.Z = PV.X * Values(0, 2) + PV.Y * Values(1, 2) + PV.Z * Values(2, 2) + PV.W * Values(3, 2)
        Vector.W = PV.X * Values(0, 3) + PV.Y * Values(1, 3) + PV.Z * Values(2, 3) + PV.W * Values(3, 3)
    End Sub
End Class
