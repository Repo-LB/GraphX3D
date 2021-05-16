Option Strict On
Imports System.IO 'Standard .NET Class for File Access

Public Class MeshObject

    'SubClass TriFace
    Public Class TriFace

        'Class Variables
        Private arr_TVertices(2) As PVector

        'Class Constructor
        Public Sub New(ByRef Vertex_A As PVector, ByRef Vertex_B As PVector, ByRef Vertex_C As PVector)
            arr_TVertices(0) = Vertex_A : arr_TVertices(1) = Vertex_B : arr_TVertices(2) = Vertex_C
        End Sub

        'TriFace ReadOnly Properties
        Public ReadOnly Property Vertices As PVector()
            Get
                Return arr_TVertices
            End Get
        End Property
        Public ReadOnly Property UnitVectorNormal As PVector
            Get
                Dim Line1 As PVector = PVector.Substract(Vertex_B, Vertex_A)
                Dim Line2 As PVector = PVector.Substract(Vertex_C, Vertex_A)
                Dim NormalUV As PVector = PVector.GetUnitVector(PVector.GetCrossProduct(Line1, Line2))
                NormalUV.W = 0
                Return NormalUV
            End Get
        End Property

        'TriFace Modifiable Properties
        Public Property Vertex_A As PVector
            Get
                Return arr_TVertices(0)
            End Get
            Set(ByVal new_Vertex0 As PVector)
                arr_TVertices(0) = new_Vertex0
            End Set
        End Property
        Public Property Vertex_B As PVector
            Get
                Return arr_TVertices(1)
            End Get
            Set(ByVal new_Vertex1 As PVector)
                arr_TVertices(1) = new_Vertex1
            End Set
        End Property
        Public Property Vertex_C As PVector
            Get
                Return arr_TVertices(2)
            End Get
            Set(ByVal new_Vertex2 As PVector)
                arr_TVertices(2) = new_Vertex2
            End Set
        End Property

        'Clone Function
        Public Function Clone() As TriFace
            Dim TF As New TriFace(Vertex_A.Clone, Vertex_B.Clone, Vertex_C.Clone)
            Return TF
        End Function

    End Class


    '----- START OF MESHOBJECT CLASS -----'

    'Note: Originally designed this class in C++. VB.NET Arrays have a weird behavior, more suitable "List(Of Object)" may be preferable.

    'Class Variables
    Private arr_Vertices() As PVector
    Private arr_Faces() As TriFace

    'MeshObject ReadOnly Properties
    Public ReadOnly Property Vertices As PVector()
        Get
            Return arr_Vertices
        End Get
    End Property
    Public ReadOnly Property Faces As TriFace()
        Get
            Return arr_Faces
        End Get
    End Property

    'MeshObject Functions
    Public Sub AddVertex(ByVal Vertex As PVector, ByVal Optional Position As Integer = -1)

        If Vertex IsNot Nothing Then
            If arr_Vertices IsNot Nothing Then
                If Position = -1 Or Position = arr_Vertices.Length Then
                    Dim new_Vertices(arr_Vertices.Length) As PVector
                    For i = 0 To arr_Vertices.Length - 1
                        new_Vertices(i) = arr_Vertices(i)
                    Next
                    new_Vertices(arr_Vertices.Length) = Vertex
                    arr_Vertices = new_Vertices
                ElseIf Position >= 0 And Position < arr_Vertices.Length Then
                    Dim new_Vertices(arr_Vertices.Length) As PVector
                    For i = 0 To Position - 1
                        new_Vertices(i) = arr_Vertices(i)
                    Next
                    new_Vertices(Position) = Vertex
                    For i = Position + 1 To arr_Vertices.Length
                        new_Vertices(i) = arr_Vertices(i - 1)
                    Next
                    arr_Vertices = new_Vertices
                Else
                    Throw New ArgumentOutOfRangeException("The vertex to be added must figure within bounds of the array.")
                End If
            Else
                If Position = 0 Or Position = -1 Then
                    Dim new_Vertices() As PVector = {Vertex}
                    arr_Vertices = new_Vertices
                Else
                    Throw New ArgumentOutOfRangeException("The vertex to be added must figure within bounds of the array.")
                End If
            End If
        Else
            Throw New Exception("The new vertex can not be equal to nothing.")
        End If

    End Sub
    Public Sub RemoveVertex(ByVal Position As Integer)

        If arr_Vertices IsNot Nothing Then
            If Position >= 0 And Position < arr_Vertices.Length - 1 Then
                Dim new_Vertices(arr_Vertices.Length - 2) As PVector
                For i = 0 To Position - 1
                    new_Vertices(i) = arr_Vertices(i)
                Next
                For i = Position To arr_Vertices.Length - 2
                    new_Vertices(i) = arr_Vertices(i + 1)
                Next
                arr_Vertices = new_Vertices
            Else
                Throw New ArgumentOutOfRangeException("The vertex to be removed must figure within bounds of the array.")
            End If
        Else
            Throw New Exception("An empty mesh has no vertex to delete.")
        End If

    End Sub
    Public Sub ReplaceVertex(ByVal Vertex As PVector, ByVal Position As Integer)

        If arr_Vertices IsNot Nothing Then
            If Vertex IsNot Nothing Then
                If Position >= 0 And Position < arr_Vertices.Length - 1 Then
                    arr_Vertices(Position) = Vertex
                Else
                    Throw New ArgumentOutOfRangeException("The vertex to be replaced must figure within bounds of the array.")
                End If
            Else
                Throw New Exception("The new vertex can not be equal to nothing.")
            End If
        Else
            Throw New Exception("An empty mesh has no vertex to replace.")
        End If

    End Sub

    Public Sub AddFace(ByVal Face As TriFace, ByVal Optional Position As Integer = -1)

        If Face IsNot Nothing Then
            If arr_Faces IsNot Nothing Then
                If Position = -1 Or Position = arr_Faces.Length Then
                    Dim new_Faces(arr_Faces.Length) As TriFace
                    For i = 0 To arr_Faces.Length - 1
                        new_Faces(i) = arr_Faces(i)
                    Next
                    new_Faces(arr_Faces.Length) = Face
                    arr_Faces = new_Faces
                ElseIf Position >= 0 And Position < arr_Faces.Length Then
                    Dim new_Faces(arr_Faces.Length) As TriFace
                    For i = 0 To Position - 1
                        new_Faces(i) = arr_Faces(i)
                    Next
                    new_Faces(Position) = Face
                    For i = Position + 1 To arr_Faces.Length
                        new_Faces(i) = arr_Faces(i - 1)
                    Next
                    arr_Faces = new_Faces
                Else
                    Throw New ArgumentOutOfRangeException("The face to be added must figure within bounds of the array.")
                End If
            Else
                If Position = 0 Or Position = -1 Then

                    Dim new_Faces() As TriFace = {Face}
                    arr_Faces = new_Faces
                Else
                    Throw New ArgumentOutOfRangeException("The face to be added must figure within bounds of the array.")
                End If
            End If
        Else
            Throw New Exception("The new face can not be equal to nothing.")
        End If

    End Sub
    Public Sub RemoveFace(ByVal Position As Integer)

        If arr_Faces IsNot Nothing Then
            If Position >= 0 And Position < arr_Faces.Length - 1 Then
                Dim new_Faces(arr_Faces.Length - 2) As TriFace
                For i = 0 To Position - 1
                    new_Faces(i) = arr_Faces(i)
                Next
                For i = Position To arr_Faces.Length - 2
                    new_Faces(i) = arr_Faces(i + 1)
                Next
                arr_Faces = new_Faces
            Else
                Throw New ArgumentOutOfRangeException("The face to be removed must figure within bounds of the array.")
            End If
        Else
            Throw New Exception("An empty mesh has no face to delete.")
        End If

    End Sub
    Public Sub ReplaceFace(ByVal Face As TriFace, ByVal Position As Integer)

        If arr_Faces IsNot Nothing Then
            If Face IsNot Nothing Then
                If Position >= 0 And Position < arr_Faces.Length - 1 Then
                    arr_Faces(Position) = Face
                Else
                    Throw New ArgumentOutOfRangeException("The face to be replaced must figure within bounds of the array.")
                End If
            Else
                Throw New Exception("The new face can not be equal to nothing.")
            End If
        Else
            Throw New Exception("An empty mesh has no face to replace.")
        End If

    End Sub

    'Class Functions
    Public Shared Function FromObjectFile(ByVal PathToObjectFile As String) As MeshObject
        'Parses values from very simple .obj files(vertices and faces only, triangulated) and returns a MeshObject 

        Dim MSH As New MeshObject
        Dim ObjFile As New StreamReader(PathToObjectFile)
        Dim str As String = ObjFile.ReadLine

        Do Until str Is Nothing
            Dim LineBlocks() As String = str.Split(CChar(" "))
            If LineBlocks(0) = "v" Then
                MSH.AddVertex(New PVector(CSng(LineBlocks(1)), CSng(LineBlocks(2)), CSng(LineBlocks(3))))
            ElseIf LineBlocks(0) = "f" Then
                MSH.AddFace(New TriFace(MSH.Vertices(CInt(LineBlocks(1)) - 1), MSH.Vertices(CInt(LineBlocks(2)) - 1), MSH.Vertices(CInt(LineBlocks(3)) - 1)))
            End If
            str = ObjFile.ReadLine
        Loop

        ObjFile.Close()
        Return MSH
    End Function

    'Deprecated Functions To Rotate MeshObjects Directly (Still Working) -> Rotate MeshObjects using a rotation Matrix for better performance.
    Public Sub Rotate_X(ByVal Angle As Single)
        Dim Sin_T As Double = Math.Sin(Angle * Math.PI / 180)
        Dim Cos_T As Double = Math.Cos(Angle * Math.PI / 180)
        For Each Vertex As PVector In arr_Vertices
            Dim Y As Double = Vertex.Y
            Dim Z As Double = Vertex.Z
            Vertex.Y = CSng(Y * Cos_T - Z * Sin_T)
            Vertex.Z = CSng(Z * Cos_T + Y * Sin_T)
        Next
    End Sub
    Public Sub Rotate_Y(ByVal Angle As Single)
        Dim Sin_T As Double = Math.Sin(Angle * Math.PI / 180)
        Dim Cos_T As Double = Math.Cos(Angle * Math.PI / 180)
        For Each Vertex As PVector In arr_Vertices
            Dim X As Double = Vertex.X
            Dim Z As Double = Vertex.Z
            Vertex.X = CSng(X * Cos_T - Z * Sin_T)
            Vertex.Z = CSng(Z * Cos_T + X * Sin_T)
        Next
    End Sub
    Public Sub Rotate_Z(ByVal Angle As Single)
        Dim Sin_T As Double = Math.Sin(Angle * Math.PI / 180)
        Dim Cos_T As Double = Math.Cos(Angle * Math.PI / 180)
        For Each Vertex As PVector In arr_Vertices
            Dim X As Double = Vertex.X
            Dim Y As Double = Vertex.Y
            Vertex.X = CSng(X * Cos_T - Y * Sin_T)
            Vertex.Y = CSng(Y * Cos_T + X * Sin_T)
        Next
    End Sub

End Class


