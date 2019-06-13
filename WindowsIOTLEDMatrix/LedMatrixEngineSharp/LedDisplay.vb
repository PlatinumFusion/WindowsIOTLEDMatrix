Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Windows.UI

Namespace LedMatrixEngineSharp
    Friend Class LedDisplay
        Private Width As Integer
        Public planes As Plane()

        Public Sub New(ByVal pwmbits As Integer, ByVal width As Integer)
            Width = width
            planes = New Plane(pwmbits - 1) {}

            For j As Integer = 0 To pwmbits - 1
                planes(j) = New Plane()

                For i As Integer = 0 To 16 - 1
                    planes(j).colormatrix(i) = New DisplayRow(i, width)
                Next
            Next
        End Sub
    End Class

    Public Class DisplayRow
        Private raddress As RowAddress
        Public Property color1 As Color()
        Public Property color2 As Color()
        Public Property color3 As Color()
        Public Property color4 As Color()

        Public Sub New(ByVal address As Integer, ByVal width As Integer)
            raddress = New RowAddress(address)
            color1 = New Color(width - 1) {}
            color2 = New Color(width - 1) {}
            color3 = New Color(width - 1) {}
            color4 = New Color(width - 1) {}
        End Sub
    End Class

    Public Class Plane
        Public Property colormatrix As DisplayRow()

        Public Sub New()
            colormatrix = New DisplayRow(31) {}
        End Sub
    End Class

    Public Class RowAddress
        Public Property A As Boolean
        Public Property B As Boolean
        Public Property C As Boolean
        Public Property D As Boolean

        Public Sub New(ByVal address As Integer)
            A = (address And 1) = 1
            B = (address And 2) = 2
            C = (address And 4) = 4
        End Sub
    End Class
End Namespace
