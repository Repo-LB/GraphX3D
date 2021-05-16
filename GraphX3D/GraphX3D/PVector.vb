Option Strict On
Imports System.Drawing

Public Class PVector

    'Class Variables
    Private pv_X, pv_Y, pv_Z, pv_W As Single

    'Class Constructor
    Public Sub New(ByVal X As Single, ByVal Y As Single, ByVal Z As Single, ByVal Optional W As Single = 1)
        pv_X = X : pv_Y = Y : pv_Z = Z : pv_W = W
    End Sub

    'PVector ReadOnly Properties 
    Public ReadOnly Property Length As Single
        Get
            Return CSng(Math.Sqrt(GetDotProduct(Me, Me)))
        End Get
    End Property

    'PVector Modifiable Properties 
    Public Property W As Single
        Get
            Return pv_W
        End Get
        Set(ByVal new_W As Single)
            pv_W = new_W
        End Set
    End Property
    Public Property X As Single
        Get
            Return pv_X
        End Get
        Set(ByVal new_X As Single)
            pv_X = new_X
        End Set
    End Property
    Public Property Y As Single
        Get
            Return pv_Y
        End Get
        Set(ByVal new_Y As Single)
            pv_Y = new_Y
        End Set
    End Property
    Public Property Z As Single
        Get
            Return pv_Z
        End Get
        Set(ByVal new_Z As Single)
            pv_Z = new_Z
        End Set
    End Property

    'Define Equality/Inequality Operators for PVectors (Same should be done for TriFace -> To Update)
    Public Shared Operator =(ByVal PV_0 As PVector, ByVal PV_1 As PVector) As Boolean
        Dim TestEquality As PVector = PVector.Substract(PV_0, PV_1)
        If TestEquality.X = 0 And TestEquality.Y = 0 And TestEquality.Z = 0 Then
            Return True
        Else
            Return False
        End If
    End Operator
    Public Shared Operator <>(ByVal PV_0 As PVector, ByVal PV_1 As PVector) As Boolean
        Dim TestEquality As PVector = PVector.Substract(PV_0, PV_1)
        If TestEquality.X = 0 And TestEquality.Y = 0 And TestEquality.Z = 0 Then
            Return False
        Else
            Return True
        End If
    End Operator

    'Clone Function
    Public Function Clone() As PVector
        Dim PV As New PVector(X, Y, Z, W)
        Return PV
    End Function

    'Class Functions(Operations on PVector)
    Public Shared Function PerspectiveDivide(ByVal Vector As PVector) As PVector
        Dim PV As New PVector(Vector.X, Vector.Y, Vector.Z, Vector.W)
        If PV.W > 0 Then
            PV.X /= PV.W
            PV.Y /= PV.W
            PV.Z /= PV.W
        End If
        Return PV
    End Function
    Public Shared Function ScreenTransform(ByVal Vector As PVector, ByVal ScreenWidth As Integer, ByVal ScreenHeight As Integer) As PVector
        Dim PV As New PVector(Vector.X, Vector.Y, Vector.Z, Vector.W)
        PV.X = (PV.X + 1) * CSng(0.5 * ScreenWidth) : PV.Y = (PV.Y + 1) * CSng(0.5 * ScreenHeight)
        Return PV
    End Function
    Public Shared Function GetUnitVector(ByVal Vector As PVector) As PVector
        Dim PV As New PVector(Vector.X, Vector.Y, Vector.Z, 0)
        Dim L As Single = Vector.Length
        If L > 0 Then
            Dim InvL As Single = 1 / L
            PV.X *= InvL : PV.Y *= InvL : PV.Z *= InvL
        End If
        Return PV
    End Function
    Public Shared Function GetDotProduct(ByVal Vector_A As PVector, ByVal Vector_B As PVector) As Single
        Return Vector_A.X * Vector_B.X + Vector_A.Y * Vector_B.Y + Vector_A.Z * Vector_B.Z
    End Function
    Public Shared Function GetCrossProduct(ByVal Vector_A As PVector, ByVal Vector_B As PVector) As PVector
        Dim PV As New PVector(0, 0, 0, 0)
        PV.X = Vector_A.Y * Vector_B.Z - Vector_A.Z * Vector_B.Y
        PV.Y = Vector_A.Z * Vector_B.X - Vector_A.X * Vector_B.Z
        PV.Z = Vector_A.X * Vector_B.Y - Vector_A.Y * Vector_B.X
        Return PV
    End Function
    Public Shared Function Add(ByVal Vector_A As PVector, ByVal Vector_B As PVector) As PVector
        Dim PV As New PVector(Vector_A.X + Vector_B.X, Vector_A.Y + Vector_B.Y, Vector_A.Z + Vector_B.Z, Vector_A.W)
        Return PV
    End Function
    Public Shared Function Add(ByVal Vector_A As PVector, ByVal Scalar As Single) As PVector
        Dim PV As New PVector(Vector_A.X + Scalar, Vector_A.Y + Scalar, Vector_A.Z + Scalar, Vector_A.W)
        Return PV
    End Function
    Public Shared Function Substract(ByVal Vector_A As PVector, ByVal Vector_B As PVector) As PVector
        Dim PV As New PVector(Vector_A.X - Vector_B.X, Vector_A.Y - Vector_B.Y, Vector_A.Z - Vector_B.Z, Vector_A.W)
        Return PV
    End Function
    Public Shared Function Substract(ByVal Vector_A As PVector, ByVal Scalar As Single) As PVector
        Dim PV As New PVector(Vector_A.X - Scalar, Vector_A.Y - Scalar, Vector_A.Z - Scalar, Vector_A.W)
        Return PV
    End Function
    Public Shared Function Multiply(ByVal Vector_A As PVector, ByVal Vector_B As PVector) As PVector
        Dim PV As New PVector(Vector_A.X * Vector_B.X, Vector_A.Y * Vector_B.Y, Vector_A.Z * Vector_B.Z, Vector_A.W)
        Return PV
    End Function
    Public Shared Function Multiply(ByVal Vector_A As PVector, ByVal Scalar As Single) As PVector
        Dim PV As New PVector(Vector_A.X * Scalar, Vector_A.Y * Scalar, Vector_A.Z * Scalar, Vector_A.W)
        Return PV
    End Function
    Public Shared Function Divide(ByVal Vector_A As PVector, ByVal Vector_B As PVector) As PVector
        Dim PV As New PVector(Vector_A.X / Vector_B.X, Vector_A.Y / Vector_B.Y, Vector_A.Z / Vector_B.Z, Vector_A.W)
        Return PV
    End Function
    Public Shared Function Divide(ByVal Vector_A As PVector, ByVal Scalar As Single) As PVector
        Dim PV As New PVector(Vector_A.X / Scalar, Vector_A.Y / Scalar, Vector_A.Z / Scalar, Vector_A.W)
        Return PV
    End Function
    Public Shared Function Negate(ByVal Vector As PVector) As PVector
        Dim PV As New PVector(Vector.X * -1, Vector.Y * -1, Vector.Z * -1, Vector.W)
        Return PV
    End Function
    'Convert to point function used by Rasterizer for compatibility with standard .NET 2D Drawing.
    Public Shared Function ToPoint(ByVal Vector As PVector) As Point
        Dim PV As New Point(CInt(Vector.X), CInt(Vector.Y))
        Return PV
    End Function

End Class