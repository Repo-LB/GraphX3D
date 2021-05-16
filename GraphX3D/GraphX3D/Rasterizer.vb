Option Strict On
Imports System.Drawing 'Standard .NET Drawing Class - Integrated to improve compatibilty/ease of use of my software for .NET programmers'

Public Class Rasterizer

    'This Class has no constructor or variables, used somewhat like a module it holds functions for Clipping and Rendering triangles.

    'Public Functions
    Public Shared Sub RenderTriangle(ByVal Triangle As MeshObject.TriFace, ByVal Canvas As Bitmap, ByVal T_Color As Color)
        'Scanline Triangle Rasterizer - Sort faces by type, draw edges, fill with scanlines.

        If Triangle.Vertex_B.Y < Triangle.Vertex_A.Y Then
            Dim tmp_Vector As PVector = Triangle.Vertex_A : Triangle.Vertex_A = Triangle.Vertex_B : Triangle.Vertex_B = tmp_Vector : End If
        If Triangle.Vertex_C.Y < Triangle.Vertex_B.Y Then
            Dim tmp_Vector As PVector = Triangle.Vertex_C : Triangle.Vertex_C = Triangle.Vertex_B : Triangle.Vertex_B = tmp_Vector : End If
        If Triangle.Vertex_B.Y < Triangle.Vertex_A.Y Then
            Dim tmp_Vector As PVector = Triangle.Vertex_A : Triangle.Vertex_A = Triangle.Vertex_B : Triangle.Vertex_B = tmp_Vector : End If

        If Triangle.Vertex_A.Y = Triangle.Vertex_B.Y Then
            If Triangle.Vertex_B.X < Triangle.Vertex_A.X Then
                Dim tmp_Vector As PVector = Triangle.Vertex_A : Triangle.Vertex_A = Triangle.Vertex_B : Triangle.Vertex_B = tmp_Vector : End If
            '---------------------------------------- Flat Top ----------------------------------------'
            Dim AC As List(Of Point) = RenderLine_WithData(Canvas, PVector.ToPoint(Triangle.Vertex_A), PVector.ToPoint(Triangle.Vertex_C), T_Color)
            Dim BC As List(Of Point) = RenderLine_WithData(Canvas, PVector.ToPoint(Triangle.Vertex_B), PVector.ToPoint(Triangle.Vertex_C), T_Color)
            SortPointsMin(BC) : SortPointsMax(AC)
            For I = 0 To AC.Count - 1
                RenderLine(Canvas, AC(I), BC(I), T_Color)
            Next

        ElseIf Triangle.Vertex_B.Y = Triangle.Vertex_C.Y Then
            If Triangle.Vertex_C.X < Triangle.Vertex_B.X Then
                Dim tmp_Vector As PVector = Triangle.Vertex_C : Triangle.Vertex_C = Triangle.Vertex_B : Triangle.Vertex_B = tmp_Vector : End If
            '---------------------------------------- Flat Bot ----------------------------------------'
            RenderLine(Canvas, PVector.ToPoint(Triangle.Vertex_B), PVector.ToPoint(Triangle.Vertex_C), T_Color)
            Dim AB As List(Of Point) = RenderLine_WithData(Canvas, PVector.ToPoint(Triangle.Vertex_A), PVector.ToPoint(Triangle.Vertex_B), T_Color)
            Dim AC As List(Of Point) = RenderLine_WithData(Canvas, PVector.ToPoint(Triangle.Vertex_A), PVector.ToPoint(Triangle.Vertex_C), T_Color)
            SortPointsMin(AC) : SortPointsMax(AB)
            For I = 0 To AC.Count - 1
                RenderLine(Canvas, AB(I), AC(I), T_Color)
            Next

        Else
            Dim Alpha As Single = (Triangle.Vertex_B.Y - Triangle.Vertex_A.Y) / (Triangle.Vertex_C.Y - Triangle.Vertex_A.Y)
            Dim Vi As PVector = PVector.Add(Triangle.Vertex_A, PVector.Multiply(PVector.Substract(Triangle.Vertex_C, Triangle.Vertex_A), Alpha))
            If Triangle.Vertex_B.X < Vi.X Then
                '---------------------------------------- Major Right ----------------------------------------'
                Dim AB As List(Of Point) = RenderLine_WithData(Canvas, PVector.ToPoint(Triangle.Vertex_A), PVector.ToPoint(Triangle.Vertex_B), T_Color)
                Dim BC As List(Of Point) = RenderLine_WithData(Canvas, PVector.ToPoint(Triangle.Vertex_B), PVector.ToPoint(Triangle.Vertex_C), T_Color)
                Dim AC As List(Of Point) = RenderLine_WithData(Canvas, PVector.ToPoint(Triangle.Vertex_A), PVector.ToPoint(Triangle.Vertex_C), T_Color)
                AB.AddRange(BC) : SortPointsMin(AC) : SortPointsMax(AB)
                For I = 0 To AC.Count - 1
                    RenderLine(Canvas, AB(I), AC(I), T_Color)
                Next

            Else
                '---------------------------------------- Major Left -----------------------------------------'
                Dim AB As List(Of Point) = RenderLine_WithData(Canvas, PVector.ToPoint(Triangle.Vertex_A), PVector.ToPoint(Triangle.Vertex_B), T_Color)
                Dim BC As List(Of Point) = RenderLine_WithData(Canvas, PVector.ToPoint(Triangle.Vertex_B), PVector.ToPoint(Triangle.Vertex_C), T_Color)
                Dim AC As List(Of Point) = RenderLine_WithData(Canvas, PVector.ToPoint(Triangle.Vertex_A), PVector.ToPoint(Triangle.Vertex_C), T_Color)
                AB.AddRange(BC) : SortPointsMin(AB) : SortPointsMax(AC)
                For I = 0 To AC.Count - 1
                    RenderLine(Canvas, AC(I), AB(I), T_Color)
                Next
            End If
        End If
    End Sub

    Public Shared Function TriClip(ByVal PlanePoint As PVector, ByVal PlaneNormal As PVector, ByVal Triangle As MeshObject.TriFace) As MeshObject.TriFace()
        'Clips a triangle against a plane.

        Dim ValidTriangles(1) As MeshObject.TriFace
        PlaneNormal = PVector.GetUnitVector(PlaneNormal)

        Dim InsidePoints, OutsidePoints As New List(Of PVector)

        Dim D_A = Distance(PlanePoint, PlaneNormal, Triangle.Vertex_A)
        Dim D_B = Distance(PlanePoint, PlaneNormal, Triangle.Vertex_B)
        Dim D_C = Distance(PlanePoint, PlaneNormal, Triangle.Vertex_C)

        If D_A >= 0 Then
            InsidePoints.Add(Triangle.Vertex_A) : Else : OutsidePoints.Add(Triangle.Vertex_A)
        End If
        If D_B >= 0 Then
            InsidePoints.Add(Triangle.Vertex_B) : Else : OutsidePoints.Add(Triangle.Vertex_B)
        End If
        If D_C >= 0 Then
            InsidePoints.Add(Triangle.Vertex_C) : Else : OutsidePoints.Add(Triangle.Vertex_C)
        End If

        Select Case InsidePoints.Count
            Case 0
                ValidTriangles(0) = Nothing : ValidTriangles(1) = Nothing
            Case 1
                '1 New Triangle
                ValidTriangles(0) = New MeshObject.TriFace(InsidePoints(0),
                LineIntersectPlane(PlanePoint, PlaneNormal, InsidePoints(0), OutsidePoints(0)),
                LineIntersectPlane(PlanePoint, PlaneNormal, InsidePoints(0), OutsidePoints(1)))
                ValidTriangles(1) = Nothing
            Case 2
                '2 New Triangles
                Select Case OutsidePoints(0)
                    Case Triangle.Vertex_A
                        Dim A_ As PVector = LineIntersectPlane(PlanePoint, PlaneNormal, Triangle.Vertex_C, Triangle.Vertex_A)
                        Dim B_ As PVector = LineIntersectPlane(PlanePoint, PlaneNormal, Triangle.Vertex_A, Triangle.Vertex_B)
                        ValidTriangles(0) = New MeshObject.TriFace(A_, Triangle.Vertex_B, Triangle.Vertex_C)
                        ValidTriangles(1) = New MeshObject.TriFace(A_, B_, Triangle.Vertex_B)
                    Case Triangle.Vertex_B
                        Dim A_ As PVector = LineIntersectPlane(PlanePoint, PlaneNormal, Triangle.Vertex_B, Triangle.Vertex_A)
                        Dim B_ As PVector = LineIntersectPlane(PlanePoint, PlaneNormal, Triangle.Vertex_C, Triangle.Vertex_B)
                        ValidTriangles(0) = New MeshObject.TriFace(Triangle.Vertex_A, B_, Triangle.Vertex_C)
                        ValidTriangles(1) = New MeshObject.TriFace(Triangle.Vertex_A, A_, B_)
                    Case Triangle.Vertex_C
                        Dim A_ As PVector = LineIntersectPlane(PlanePoint, PlaneNormal, Triangle.Vertex_C, Triangle.Vertex_A)
                        Dim B_ As PVector = LineIntersectPlane(PlanePoint, PlaneNormal, Triangle.Vertex_C, Triangle.Vertex_B)
                        ValidTriangles(0) = New MeshObject.TriFace(Triangle.Vertex_A, Triangle.Vertex_B, B_)
                        ValidTriangles(1) = New MeshObject.TriFace(Triangle.Vertex_A, B_, A_)
                End Select
            Case 3
                'Return Triangle
                ValidTriangles(0) = Triangle
                ValidTriangles(1) = Nothing
        End Select

        Return ValidTriangles
    End Function

    Public Shared Sub RenderLine(ByVal Canvas As Bitmap, ByVal StartPoint As Point, ByVal EndPoint As Point, ByVal T_Color As Color)
        'My messy implementation of Bresenham's Line Algorithm.

        Dim Points As New List(Of Point)

        If StartPoint.X > EndPoint.X Then
            Dim tmp_Point As Point = StartPoint
            StartPoint = EndPoint : EndPoint = tmp_Point
        End If

        Dim Delta_X As Integer = EndPoint.X - StartPoint.X
        Dim Delta_Y As Integer = EndPoint.Y - StartPoint.Y

        If Delta_X = 0 And Delta_Y = 0 Then
            Canvas.SetPixel(StartPoint.X, StartPoint.Y, T_Color)
        ElseIf Delta_X = 0 Then
            If Delta_Y > 0 Then
                For I = StartPoint.Y To EndPoint.Y
                    Canvas.SetPixel(StartPoint.X, I, T_Color)
                Next
            ElseIf Delta_Y < 0 Then
                For I = EndPoint.Y To StartPoint.Y
                    Canvas.SetPixel(StartPoint.X, I, T_Color)
                Next
            End If
        ElseIf Delta_Y = 0 Then
            If Delta_X > 0 Then
                For I = StartPoint.X To EndPoint.X
                    Canvas.SetPixel(I, StartPoint.Y, T_Color)
                Next
            ElseIf Delta_X < 0 Then
                For I = EndPoint.X To StartPoint.X
                    Canvas.SetPixel(I, StartPoint.Y, T_Color)
                Next
            End If
        Else
            If Math.Abs(Delta_Y) > Math.Abs(Delta_X) Then
                Dim Delta_Err As Single = CSng(Delta_X / Delta_Y)
                Dim Err As Single = 0

                If Delta_Err > 0 Then
                    Dim X_Value As Integer = StartPoint.X

                    For I = StartPoint.Y To EndPoint.Y
                        Canvas.SetPixel(X_Value, I, T_Color)
                        Err += Delta_Err
                        If Err > 0.5 Then
                            X_Value += 1
                            Err -= 1
                        End If
                    Next
                Else
                    Dim X_Value As Integer = EndPoint.X

                    For I = EndPoint.Y To StartPoint.Y
                        Canvas.SetPixel(X_Value, I, T_Color)
                        Err += Delta_Err
                        If Err < -0.5 Then
                            X_Value -= 1
                            Err += 1
                        End If
                    Next
                End If
            Else
                Dim Delta_Err As Single = CSng(Delta_Y / Delta_X)
                Dim Err As Single = 0

                If Delta_Err > 0 Then
                    Dim Y_Value As Integer = StartPoint.Y

                    For I = StartPoint.X To EndPoint.X
                        Canvas.SetPixel(I, Y_Value, T_Color)
                        Err += Delta_Err
                        If Err > 0.5 Then
                            Y_Value += 1
                            Err -= 1
                        End If
                    Next
                Else
                    Dim Y_Value As Integer = StartPoint.Y

                    For I = StartPoint.X To EndPoint.X
                        Canvas.SetPixel(I, Y_Value, T_Color)
                        Err += Delta_Err
                        If Err < -0.5 Then
                            Y_Value -= 1
                            Err += 1
                        End If
                    Next
                End If
            End If
        End If

    End Sub

    'Private Functions
    Private Shared Function RenderLine_WithData(ByVal Canvas As Bitmap, ByVal StartPoint As Point, ByVal EndPoint As Point, ByVal T_Color As Color) As List(Of Point)
        'Draws edges while storing points for scanline raster.

        Dim Points As New List(Of Point)

        If StartPoint.X > EndPoint.X Then
            Dim tmp_Point As Point = StartPoint
            StartPoint = EndPoint : EndPoint = tmp_Point
        End If

        Dim Delta_X As Integer = EndPoint.X - StartPoint.X
        Dim Delta_Y As Integer = EndPoint.Y - StartPoint.Y

        If Delta_Y = 0 Then
        ElseIf Delta_X = 0 Then
            If Delta_Y > 0 Then
                For I = StartPoint.Y To EndPoint.Y
                    Canvas.SetPixel(StartPoint.X, I, T_Color)
                    Points.Add(New Point(StartPoint.X, I))
                Next
            ElseIf Delta_Y < 0 Then
                For I = EndPoint.Y To StartPoint.Y
                    Canvas.SetPixel(StartPoint.X, I, T_Color)
                    Points.Add(New Point(StartPoint.X, I))
                Next
            End If
        Else
            If Math.Abs(Delta_Y) > Math.Abs(Delta_X) Then
                Dim Delta_Err As Single = CSng(Delta_X / Delta_Y)
                Dim Err As Single = 0

                If Delta_Err > 0 Then
                    Dim X_Value As Integer = StartPoint.X

                    For I = StartPoint.Y To EndPoint.Y
                        Canvas.SetPixel(X_Value, I, T_Color)
                        Points.Add(New Point(X_Value, I))
                        Err += Delta_Err
                        If Err > 0.5 Then
                            X_Value += 1
                            Err -= 1
                        End If
                    Next
                Else
                    Dim X_Value As Integer = EndPoint.X

                    For I = EndPoint.Y To StartPoint.Y
                        Canvas.SetPixel(X_Value, I, T_Color)
                        Points.Add(New Point(X_Value, I))
                        Err += Delta_Err
                        If Err < -0.5 Then
                            X_Value -= 1
                            Err += 1
                        End If
                    Next
                End If
            Else
                Dim Delta_Err As Single = CSng(Delta_Y / Delta_X)
                Dim Err As Single = 0

                If Delta_Err > 0 Then
                    Dim Y_Value As Integer = StartPoint.Y

                    For I = StartPoint.X To EndPoint.X
                        Canvas.SetPixel(I, Y_Value, T_Color)
                        Points.Add(New Point(I, Y_Value))
                        Err += Delta_Err
                        If Err > 0.5 Then
                            Y_Value += 1
                            Err -= 1
                        End If
                    Next
                Else
                    Dim Y_Value As Integer = StartPoint.Y

                    For I = StartPoint.X To EndPoint.X
                        Canvas.SetPixel(I, Y_Value, T_Color)
                        Points.Add(New Point(I, Y_Value))
                        Err += Delta_Err
                        If Err < -0.5 Then
                            Y_Value -= 1
                            Err += 1
                        End If
                    Next
                End If
            End If
        End If

        Return Points
    End Function

    Private Shared Sub SortPointsMin(ByRef Points As List(Of Point))
        If Points.Count > 0 Then
            Dim EPoints As New List(Of Point)
            Points = Points.OrderBy(Function(P) P.Y).ToList()

            Dim Y_Level As Integer = Points(0).Y
            Dim X_Compare As Integer = Nothing
            For Each P As Point In Points
                If P.Y <> Y_Level Then
                    EPoints.Add(New Point(X_Compare, Y_Level))
                    Y_Level = P.Y
                    X_Compare = Nothing
                End If

                If X_Compare = Nothing Then
                    X_Compare = P.X
                ElseIf P.X < X_Compare Then
                    X_Compare = P.X
                End If
            Next

            Points = EPoints
        End If
    End Sub
    Private Shared Sub SortPointsMax(ByRef Points As List(Of Point))
        If Points.Count > 0 Then
            Dim EPoints As New List(Of Point)
            Points = Points.OrderBy(Function(P) P.Y).ToList()

            Dim Y_Level As Integer = Points(0).Y
            Dim X_Compare As Integer = Nothing
            For Each P As Point In Points
                If P.Y <> Y_Level Then
                    EPoints.Add(New Point(X_Compare, Y_Level))
                    Y_Level = P.Y
                    X_Compare = Nothing
                End If

                If X_Compare = Nothing Then
                    X_Compare = P.X
                ElseIf P.X > X_Compare Then
                    X_Compare = P.X
                End If
            Next

            Points = EPoints
        End If
    End Sub

    Private Shared Function LineIntersectPlane(ByVal PlanePoint As PVector, ByVal PlaneNormal As PVector, ByVal LineStart As PVector, ByVal LineEnd As PVector) As PVector
        'Function called when Clipping to get the interpolated intersection point. 
        Dim PlaneDirection As Single = -PVector.GetDotProduct(PlaneNormal, PlanePoint)
        Dim DP_LineStart As Single = PVector.GetDotProduct(LineStart, PlaneNormal)
        Dim DP_LineEnd As Single = PVector.GetDotProduct(LineEnd, PlaneNormal)
        Dim TestValue As Single = (-PlaneDirection - DP_LineStart) / (DP_LineEnd - DP_LineStart)
        Dim LineSpan As PVector = PVector.Substract(LineEnd, LineStart)
        Dim LineIntersect As PVector = PVector.Multiply(LineSpan, TestValue)
        Return PVector.Add(LineStart, LineIntersect)
    End Function
    Private Shared Function Distance(ByVal PlanePoint As PVector, ByVal PlaneNormal As PVector, ByRef PointToTest As PVector) As Single
        'Function called when Clipping to get the distance from a PVector to a plane. 
        Return PVector.GetDotProduct(PVector.Substract(PointToTest, PlanePoint), PlaneNormal)
    End Function


End Class
