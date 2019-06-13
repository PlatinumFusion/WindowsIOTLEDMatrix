Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Windows.Devices.Gpio
Imports Windows.UI
Imports Windows.UI.Xaml.Shapes
Imports System.Threading
Imports System.Diagnostics
Imports Microsoft.Graphics.Canvas

Namespace LedMatrixEngineSharp
    Public Class RgbMatrix4s
        Public proxy As GpioProxy
        Public Property _fontColor As Color
        Public Property _fontHeight As Integer
        Public Property _fontSize As Integer
        Public Property _fontWidth As Integer
        Public Property _textCursorX As Integer
        Public Property _textCursorY As Integer
        Public Property _wordWrap As Boolean
        Public Property RowsPerSubPanel As Integer
        Private display As LedDisplay
        Private pwmbits As Integer = 1
        Public Property RowClockTime As Integer
        Private device As CanvasDevice
        Private target As CanvasRenderTarget
        Public Property Session As CanvasDrawingSession
        Private RowSleepNanos As Long()
        Private Font5x7 As Byte()
        Private Font4x6 As Byte()
        Private Font3x5 As Byte()
        Private Width As Integer = 128
        Private Height As Integer = 16

        Public Sub New()
            Font3x5 = New Byte() {&H00, &H00, &H00, &H17, &H00, &H00, &H03, &H00, &H03, &H0A, &H1F, &H0A, &H16, &H13, &H1A, &H09, &H04, &H0A, &H0A, &H15, &H1A, &H03, &H00, &H00, &H00, &H0E, &H11, &H11, &H0E, &H00, &H06, &H06, &H00, &H04, &H0E, &H04, &H0C, &H1C, &H00, &H04, &H04, &H04, &H10, &H00, &H00, &H18, &H04, &H03, &H1F, &H11, &H1F, &H02, &H1F, &H00, &H1D, &H15, &H17, &H15, &H15, &H1F, &H0F, &H08, &H1E, &H17, &H15, &H1D, &H1F, &H15, &H1D, &H01, &H01, &H1F, &H1F, &H15, &H1F, &H17, &H15, &H1F, &H00, &H0A, &H00, &H00, &H1A, &H00, &H04, &H0A, &H11, &H0A, &H0A, &H0A, &H11, &H0A, &H04, &H00, &H15, &H07, &H1F, &H15, &H17, &H1F, &H05, &H1F, &H1F, &H15, &H1B, &H1F, &H11, &H11, &H1F, &H11, &H0E, &H1F, &H15, &H15, &H1F, &H05, &H01, &H1F, &H11, &H1D, &H1F, &H04, &H1F, &H11, &H1F, &H11, &H08, &H10, &H0F, &H1F, &H04, &H1B, &H1F, &H10, &H10, &H1F, &H06, &H1F, &H1C, &H04, &H1C, &H1F, &H11, &H1F, &H1F, &H05, &H07, &H0E, &H19, &H1E, &H1F, &H05, &H1B, &H17, &H15, &H1D, &H01, &H1F, &H01, &H1F, &H10, &H1F, &H0F, &H10, &H0F, &H1F, &H0C, &H1F, &H1B, &H04, &H1B, &H17, &H14, &H1F, &H19, &H15, &H13, &H00, &H1F, &H11, &H03, &H04, &H18, &H11, &H1F, &H00, &H06, &H01, &H06, &H10, &H10, &H10, &H01, &H01, &H02, &H18, &H14, &H1C, &H1F, &H14, &H1C, &H1C, &H14, &H14, &H1C, &H14, &H1F, &H0C, &H1A, &H14, &H04, &H1E, &H05, &H17, &H15, &H1E, &H1F, &H04, &H1C, &H00, &H1D, &H00, &H08, &H10, &H0D, &H1F, &H0C, &H1A, &H00, &H1F, &H00, &H18, &H0C, &H18, &H18, &H04, &H18, &H1E, &H12, &H1E, &H1F, &H05, &H07, &H07, &H05, &H1F, &H1E, &H04, &H04, &H12, &H15, &H09, &H02, &H1F, &H02, &H1C, &H10, &H1C, &H0C, &H10, &H0C, &H0C, &H18, &H0C, &H14, &H08, &H14, &H16, &H18, &H06, &H04, &H1C, &H10, &H04, &H0E, &H11, &H00, &H1F, &H00, &H11, &H0E, &H04, &H02, &H04, &H02, &H1F, &H1F, &H1F}
            Font4x6 = New Byte() {&H00, &H00, &H00, &H00, &H00, &H2F, &H00, &H00, &H03, &H00, &H03, &H00, &H3F, &H0A, &H3F, &H0A, &H03, &H02, &H07, &H00, &H33, &H0B, &H34, &H33, &H1A, &H25, &H2A, &H10, &H00, &H03, &H00, &H00, &H00, &H1E, &H21, &H00, &H00, &H21, &H1E, &H00, &H0A, &H04, &H0A, &H00, &H04, &H0E, &H04, &H00, &H20, &H10, &H00, &H00, &H04, &H04, &H04, &H00, &H00, &H20, &H00, &H00, &H30, &H08, &H04, &H03, &H1E, &H29, &H25, &H1E, &H22, &H3F, &H20, &H00, &H32, &H29, &H25, &H22, &H12, &H21, &H25, &H1A, &H0C, &H0A, &H3F, &H08, &H27, &H25, &H25, &H19, &H1E, &H25, &H25, &H19, &H01, &H39, &H05, &H03, &H1A, &H25, &H25, &H1A, &H06, &H29, &H29, &H1E, &H00, &H14, &H00, &H00, &H20, &H14, &H00, &H00, &H08, &H14, &H22, &H22, &H0A, &H0A, &H0A, &H0A, &H22, &H22, &H14, &H08, &H02, &H01, &H2D, &H02, &H1E, &H21, &H2D, &H2E, &H3E, &H09, &H09, &H3E, &H3F, &H25, &H25, &H1A, &H1E, &H21, &H21, &H12, &H3F, &H21, &H21, &H1E, &H3F, &H25, &H25, &H21, &H3F, &H05, &H05, &H01, &H1E, &H21, &H29, &H1A, &H3F, &H04, &H04, &H3F, &H21, &H3F, &H21, &H00, &H10, &H21, &H21, &H1F, &H3F, &H0C, &H12, &H21, &H3F, &H20, &H20, &H20, &H3F, &H02, &H06, &H3F, &H3F, &H04, &H08, &H3F, &H1E, &H21, &H21, &H1E, &H3F, &H09, &H09, &H06, &H1E, &H21, &H11, &H2E, &H3F, &H09, &H09, &H36, &H22, &H25, &H25, &H19, &H01, &H3F, &H01, &H01, &H1F, &H20, &H20, &H1F, &H0F, &H30, &H10, &H0F, &H3F, &H10, &H18, &H3F, &H33, &H0C, &H0C, &H33, &H07, &H38, &H04, &H03, &H31, &H29, &H25, &H23, &H00, &H3F, &H21, &H00, &H03, &H04, &H08, &H30, &H00, &H21, &H3F, &H00, &H02, &H01, &H02, &H00, &H20, &H20, &H20, &H20, &H00, &H01, &H02, &H00, &H10, &H2A, &H2A, &H3C, &H3F, &H24, &H24, &H18, &H1C, &H22, &H22, &H14, &H18, &H24, &H24, &H3F, &H1C, &H2A, &H2A, &H0C, &H3E, &H09, &H01, &H02, &H24, &H2A, &H2A, &H1E, &H3F, &H08, &H04, &H38, &H24, &H3D, &H20, &H00, &H10, &H20, &H20, &H1D, &H3F, &H08, &H14, &H22, &H21, &H3F, &H20, &H00, &H3E, &H02, &H1C, &H3E, &H3E, &H02, &H02, &H3C, &H1C, &H22, &H22, &H1C, &H3E, &H0A, &H0A, &H04, &H04, &H0A, &H0A, &H3E, &H3E, &H02, &H02, &H04, &H24, &H2A, &H2A, &H12, &H04, &H1E, &H24, &H20, &H1E, &H20, &H20, &H3E, &H1E, &H20, &H10, &H0E, &H1E, &H38, &H20, &H1E, &H36, &H08, &H08, &H36, &H26, &H28, &H28, &H1E, &H32, &H2A, &H2A, &H26, &H04, &H1B, &H21, &H00, &H00, &H3F, &H00, &H00, &H00, &H21, &H1B, &H04, &H04, &H02, &H04, &H02, &H3F, &H35, &H35, &H3F}
            Font5x7 = New Byte() {&H00, &H00, &H00, &H00, &H00, &H00, &H00, &H5F, &H00, &H00, &H00, &H07, &H00, &H07, &H00, &H14, &H7F, &H14, &H7F, &H14, &H24, &H2A, &H7F, &H2A, &H12, &H23, &H13, &H08, &H64, &H62, &H36, &H49, &H55, &H22, &H50, &H00, &H05, &H03, &H00, &H00, &H00, &H1C, &H22, &H41, &H00, &H00, &H41, &H22, &H1C, &H00, &H08, &H2A, &H1C, &H2A, &H08, &H08, &H08, &H3E, &H08, &H08, &H00, &H50, &H30, &H00, &H00, &H08, &H08, &H08, &H08, &H08, &H00, &H60, &H60, &H00, &H00, &H20, &H10, &H08, &H04, &H02, &H3E, &H51, &H49, &H45, &H3E, &H00, &H42, &H7F, &H40, &H00, &H42, &H61, &H51, &H49, &H46, &H21, &H41, &H45, &H4B, &H31, &H18, &H14, &H12, &H7F, &H10, &H27, &H45, &H45, &H45, &H39, &H3C, &H4A, &H49, &H49, &H30, &H01, &H71, &H09, &H05, &H03, &H36, &H49, &H49, &H49, &H36, &H06, &H49, &H49, &H29, &H1E, &H00, &H36, &H36, &H00, &H00, &H00, &H56, &H36, &H00, &H00, &H00, &H08, &H14, &H22, &H41, &H14, &H14, &H14, &H14, &H14, &H41, &H22, &H14, &H08, &H00, &H02, &H01, &H51, &H09, &H06, &H32, &H49, &H79, &H41, &H3E, &H7E, &H11, &H11, &H11, &H7E, &H7F, &H49, &H49, &H49, &H36, &H3E, &H41, &H41, &H41, &H22, &H7F, &H41, &H41, &H22, &H1C, &H7F, &H49, &H49, &H49, &H41, &H7F, &H09, &H09, &H01, &H01, &H3E, &H41, &H41, &H51, &H32, &H7F, &H08, &H08, &H08, &H7F, &H00, &H41, &H7F, &H41, &H00, &H20, &H40, &H41, &H3F, &H01, &H7F, &H08, &H14, &H22, &H41, &H7F, &H40, &H40, &H40, &H40, &H7F, &H02, &H04, &H02, &H7F, &H7F, &H04, &H08, &H10, &H7F, &H3E, &H41, &H41, &H41, &H3E, &H7F, &H09, &H09, &H09, &H06, &H3E, &H41, &H51, &H21, &H5E, &H7F, &H09, &H19, &H29, &H46, &H46, &H49, &H49, &H49, &H31, &H01, &H01, &H7F, &H01, &H01, &H3F, &H40, &H40, &H40, &H3F, &H1F, &H20, &H40, &H20, &H1F, &H7F, &H20, &H18, &H20, &H7F, &H63, &H14, &H08, &H14, &H63, &H03, &H04, &H78, &H04, &H03, &H61, &H51, &H49, &H45, &H43, &H00, &H00, &H7F, &H41, &H41, &H02, &H04, &H08, &H10, &H20, &H41, &H41, &H7F, &H00, &H00, &H04, &H02, &H01, &H02, &H04, &H40, &H40, &H40, &H40, &H40, &H00, &H01, &H02, &H04, &H00, &H20, &H54, &H54, &H54, &H78, &H7F, &H48, &H44, &H44, &H38, &H38, &H44, &H44, &H44, &H20, &H38, &H44, &H44, &H48, &H7F, &H38, &H54, &H54, &H54, &H18, &H08, &H7E, &H09, &H01, &H02, &H08, &H14, &H54, &H54, &H3C, &H7F, &H08, &H04, &H04, &H78, &H00, &H44, &H7D, &H40, &H00, &H20, &H40, &H44, &H3D, &H00, &H00, &H7F, &H10, &H28, &H44, &H00, &H41, &H7F, &H40, &H00, &H7C, &H04, &H18, &H04, &H78, &H7C, &H08, &H04, &H04, &H78, &H38, &H44, &H44, &H44, &H38, &H7C, &H14, &H14, &H14, &H08, &H08, &H14, &H14, &H18, &H7C, &H7C, &H08, &H04, &H04, &H08, &H48, &H54, &H54, &H54, &H20, &H04, &H3F, &H44, &H40, &H20, &H3C, &H40, &H40, &H20, &H7C, &H1C, &H20, &H40, &H20, &H1C, &H3C, &H40, &H30, &H40, &H3C, &H44, &H28, &H10, &H28, &H44, &H0C, &H50, &H50, &H50, &H3C, &H44, &H64, &H54, &H4C, &H44, &H00, &H08, &H36, &H41, &H00, &H00, &H00, &H7F, &H00, &H00, &H00, &H41, &H36, &H08, &H00, &H08, &H08, &H2A, &H1C, &H08, &H08, &H1C, &H2A, &H08, &H08}
            _textCursorX = 0
            _textCursorY = 0
            Dim white As Windows.UI.Color = Windows.UI.Color.FromArgb(255, 255, 255, 255)
            proxy = New GpioProxy()
            proxy.setupOutputBits()
            device = New CanvasDevice()
            target = New CanvasRenderTarget(device, Width, Height, 64)
            Session = target.CreateDrawingSession()
            _fontColor = white
            _fontSize = 1
            _fontWidth = 3
            _fontHeight = 5
            _wordWrap = True
            RowsPerSubPanel = 8
            RowClockTime = 50
            RowSleepNanos = New Long() {(1 * RowClockTime) - RowClockTime, (2 * RowClockTime) - RowClockTime, (4 * RowClockTime) - RowClockTime, (8 * RowClockTime) - RowClockTime, (16 * RowClockTime) - RowClockTime, (32 * RowClockTime) - RowClockTime, (64 * RowClockTime) - RowClockTime, (128 * RowClockTime) - RowClockTime}
            display = New LedDisplay(pwmbits, Width)
            clearDisplay()
        End Sub

        Public Sub Flush(ByVal x As Integer, ByVal y As Integer, ByVal wx As Integer, ByVal wy As Integer)
            Session.Flush()
            Dim lc As Color() = target.GetPixelColors()

            For i As Integer = 0 To lc.Length - 1
                Dim posx As Integer = i Mod Width
                Dim posy As Integer = i / Width

                If posx >= x AndAlso posx <= x + wx Then

                    If posy >= y AndAlso posy <= y + wy Then
                        drawPixel(posx, posy, lc(i))
                    End If
                End If
            Next
        End Sub

        Public Sub clearDisplay()
        End Sub

        Public Sub drawPixel(ByVal x As Integer, ByVal y As Integer, ByVal c As Color)
            If (y < 4) OrElse (y > 7 AndAlso y < 12) OrElse (y > 15 AndAlso y < 20) OrElse (y > 23 AndAlso y < 28) Then
                If y > 7 AndAlso y < 12 Then y = y - 4
                If y > 15 AndAlso y < 20 Then y = y - 8
                If y > 23 AndAlso y < 28 Then y = y - 12

                If x <= 7 Then
                    x = x + 8
                ElseIf x >= 8 AndAlso x <= 15 Then
                    x = x + 16
                ElseIf x >= 16 AndAlso x <= 23 Then
                    x = x + 24
                ElseIf x >= 24 AndAlso x <= 31 Then
                    x = x + 32
                ElseIf x >= 32 AndAlso x <= 39 Then
                    x = x + 40
                ElseIf x >= 40 AndAlso x <= 47 Then
                    x = x + 48
                ElseIf x >= 48 AndAlso x <= 55 Then
                    x = x + 56
                ElseIf x >= 56 AndAlso x <= 63 Then
                    x = x + 64
                Else
                End If
            Else
                If y > 3 AndAlso y < 8 Then y = y - 4
                If y > 11 AndAlso y < 16 Then y = y - 8
                If y > 19 AndAlso y < 24 Then y = y - 12
                If y > 27 AndAlso y < 32 Then y = y - 16

                If x <= 7 Then
                ElseIf x >= 8 AndAlso x <= 15 Then
                    x = x + 8
                ElseIf x >= 16 AndAlso x <= 23 Then
                    x = x + 16
                ElseIf x >= 24 AndAlso x <= 31 Then
                    x = x + 24
                ElseIf x >= 32 AndAlso x <= 39 Then
                    x = x + 32
                ElseIf x >= 40 AndAlso x <= 47 Then
                    x = x + 40
                ElseIf x >= 48 AndAlso x <= 55 Then
                    x = x + 48
                ElseIf x >= 56 AndAlso x <= 63 Then
                    x = x + 56
                Else
                End If
            End If

            Dim red As Integer = c.R
            Dim green As Integer = c.G
            Dim blue As Integer = c.B
                        ''' Cannot convert AssignmentExpressionSyntax, System.NotSupportedException: RightShiftAssignmentExpression is not supported!
'''    at ICSharpCode.CodeConverter.VB.SyntaxKindExtensions.ConvertToken(SyntaxKind t, TokenContext context)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.MakeAssignmentStatement(AssignmentExpressionSyntax node)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
''' 
''' Input: 
'''             red >>= 8 - pwmbits
''' 
                        ''' Cannot convert AssignmentExpressionSyntax, System.NotSupportedException: RightShiftAssignmentExpression is not supported!
'''    at ICSharpCode.CodeConverter.VB.SyntaxKindExtensions.ConvertToken(SyntaxKind t, TokenContext context)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.MakeAssignmentStatement(AssignmentExpressionSyntax node)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
''' 
''' Input: 
'''             green >>= 8 - pwmbits
''' 
                        ''' Cannot convert AssignmentExpressionSyntax, System.NotSupportedException: RightShiftAssignmentExpression is not supported!
'''    at ICSharpCode.CodeConverter.VB.SyntaxKindExtensions.ConvertToken(SyntaxKind t, TokenContext context)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.MakeAssignmentStatement(AssignmentExpressionSyntax node)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
''' 
''' Input: 
'''             blue >>= 8 - pwmbits
''' 
            For b As Integer = 0 To pwmbits - 1
                                ''' Cannot convert LocalDeclarationStatementSyntax, System.InvalidCastException: Unable to cast object of type 'Microsoft.CodeAnalysis.VisualBasic.Syntax.EmptyStatementSyntax' to type 'Microsoft.CodeAnalysis.VisualBasic.Syntax.ExpressionSyntax'.
'''    at ICSharpCode.CodeConverter.VB.CommonConversions.RemodelVariableDeclaration(VariableDeclarationSyntax declaration)
'''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.LocalDeclarationStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
''' 
''' Input: 
'''                 int mask = 1 << b;
''' 
''' 
                If y < 4 Then
                    display.planes(b).colormatrix(y).color1(x).A = c.A
                    display.planes(b).colormatrix(y).color1(x).R = CByte((If((red And mask) = mask, 255, 0)))
                    display.planes(b).colormatrix(y).color1(x).G = CByte((If((green And mask) = mask, 255, 0)))
                    display.planes(b).colormatrix(y).color1(x).B = CByte((If((blue And mask) = mask, 255, 0)))
                ElseIf y > 3 AndAlso y < 8 Then
                    display.planes(b).colormatrix(y - 4).color2(x).A = c.A
                    display.planes(b).colormatrix(y - 4).color2(x).R = CByte((If((red And mask) = mask, 255, 0)))
                    display.planes(b).colormatrix(y - 4).color2(x).G = CByte((If((green And mask) = mask, 255, 0)))
                    display.planes(b).colormatrix(y - 4).color2(x).B = CByte((If((blue And mask) = mask, 255, 0)))
                ElseIf y > 7 AndAlso y < 12 Then
                    display.planes(b).colormatrix(y - 8).color3(x).A = c.A
                    display.planes(b).colormatrix(y - 8).color3(x).R = CByte((If((red And mask) = mask, 255, 0)))
                    display.planes(b).colormatrix(y - 8).color3(x).G = CByte((If((green And mask) = mask, 255, 0)))
                    display.planes(b).colormatrix(y - 8).color3(x).B = CByte((If((blue And mask) = mask, 255, 0)))
                ElseIf y > 11 AndAlso y < 16 Then
                    display.planes(b).colormatrix(y - 12).color4(x).A = c.A
                    display.planes(b).colormatrix(y - 12).color4(x).R = CByte((If((red And mask) = mask, 255, 0)))
                    display.planes(b).colormatrix(y - 12).color4(x).G = CByte((If((green And mask) = mask, 255, 0)))
                    display.planes(b).colormatrix(y - 12).color4(x).B = CByte((If((blue And mask) = mask, 255, 0)))
                End If
            Next
        End Sub

        Private pwmi As Integer = 0
        Private row As Integer = 0

        Public Sub updateDisplay(ByVal action As Windows.Foundation.IAsyncAction)
            While True
                Dim lastr1 As GpioPinValue = GpioPinValue.Low
                Dim lastg1 As GpioPinValue = GpioPinValue.Low
                Dim lastb1 As GpioPinValue = GpioPinValue.Low
                Dim lastr2 As GpioPinValue = GpioPinValue.Low
                Dim lastg2 As GpioPinValue = GpioPinValue.Low
                Dim lastb2 As GpioPinValue = GpioPinValue.Low
                Dim lastP2r1 As GpioPinValue = GpioPinValue.Low
                Dim lastP2g1 As GpioPinValue = GpioPinValue.Low
                Dim lastP2b1 As GpioPinValue = GpioPinValue.Low
                Dim lastP2r2 As GpioPinValue = GpioPinValue.Low
                Dim lastP2g2 As GpioPinValue = GpioPinValue.Low
                Dim lastP2b2 As GpioPinValue = GpioPinValue.Low

                For a As Integer = 0 To 2 - 1

                    For row As Integer = a To RowsPerSubPanel - 1 Step 2

                        For pwmi As Integer = 0 To pwmbits - 1
                            Dim myrow As DisplayRow = display.planes(pwmi).colormatrix(row)

                            For col As Integer = 0 To (Width) - 1
                                Dim _r1 As GpioPinValue = If(myrow.color1(col).R = 0, GpioPinValue.Low, GpioPinValue.High)
                                Dim _g1 As GpioPinValue = If(myrow.color1(col).G = 0, GpioPinValue.Low, GpioPinValue.High)
                                Dim _b1 As GpioPinValue = If(myrow.color1(col).B = 0, GpioPinValue.Low, GpioPinValue.High)
                                Dim _r2 As GpioPinValue = If(myrow.color2(col).R = 0, GpioPinValue.Low, GpioPinValue.High)
                                Dim _g2 As GpioPinValue = If(myrow.color2(col).G = 0, GpioPinValue.Low, GpioPinValue.High)
                                Dim _b2 As GpioPinValue = If(myrow.color2(col).B = 0, GpioPinValue.Low, GpioPinValue.High)
                                Dim P2_r1 As GpioPinValue = If(myrow.color3(col).R = 0, GpioPinValue.Low, GpioPinValue.High)
                                Dim P2_g1 As GpioPinValue = If(myrow.color3(col).G = 0, GpioPinValue.Low, GpioPinValue.High)
                                Dim P2_b1 As GpioPinValue = If(myrow.color3(col).B = 0, GpioPinValue.Low, GpioPinValue.High)
                                Dim P2_r2 As GpioPinValue = If(myrow.color4(col).R = 0, GpioPinValue.Low, GpioPinValue.High)
                                Dim P2_g2 As GpioPinValue = If(myrow.color4(col).G = 0, GpioPinValue.Low, GpioPinValue.High)
                                Dim P2_b2 As GpioPinValue = If(myrow.color4(col).B = 0, GpioPinValue.Low, GpioPinValue.High)

                                If lastr1 <> _r1 Then
                                    proxy.r1.Write(_r1)
                                    lastr1 = _r1
                                End If

                                If lastg1 <> _g1 Then
                                    proxy.g1.Write(_g1)
                                    lastg1 = _g1
                                End If

                                If lastb1 <> _b1 Then
                                    proxy.b1.Write(_b1)
                                    lastb1 = _b1
                                End If

                                If lastr2 <> _r2 Then
                                    proxy.r2.Write(_r2)
                                    lastr2 = _r2
                                End If

                                If lastg2 <> _g2 Then
                                    proxy.g2.Write(_g2)
                                    lastg2 = _g2
                                End If

                                If lastb2 <> _b2 Then
                                    proxy.b2.Write(_b2)
                                    lastb2 = _b2
                                End If

                                If lastP2r1 <> P2_r1 Then
                                    proxy.P2r1.Write(P2_r1)
                                    lastP2r1 = P2_r1
                                End If

                                If lastP2g1 <> P2_g1 Then
                                    proxy.P2g1.Write(P2_g1)
                                    lastP2g1 = P2_g1
                                End If

                                If lastP2b1 <> P2_b1 Then
                                    proxy.P2b1.Write(P2_b1)
                                    lastP2b1 = P2_b1
                                End If

                                If lastP2r2 <> P2_r2 Then
                                    proxy.P2r2.Write(P2_r2)
                                    lastP2r2 = P2_r2
                                End If

                                If lastP2g2 <> P2_g2 Then
                                    proxy.P2g2.Write(P2_g2)
                                    lastP2g2 = P2_g2
                                End If

                                If lastP2b2 <> P2_b2 Then
                                    proxy.P2b2.Write(P2_b2)
                                    lastP2b2 = P2_b2
                                End If

                                proxy.clock.Write(GpioPinValue.High)
                                proxy.clock.Write(GpioPinValue.Low)
                            Next

                            proxy.setRowAddress(row)
                            proxy.latch.Write(GpioPinValue.High)
                            proxy.latch.Write(GpioPinValue.Low)
                            proxy.outputEnabled.Write(GpioPinValue.High)
                            proxy.outputEnabled.Write(GpioPinValue.Low)
                        Next
                    Next
                Next
            End While
        End Sub

        Public Sub setTextCursor(ByVal x As Integer, ByVal y As Integer)
            _textCursorX = x
            _textCursorY = y
        End Sub

        Public Sub setFontColor(ByVal color As Color)
            _fontColor = color
        End Sub

        Public Sub setFontSize(ByVal size As Integer)
            _fontSize = If((size >= 3), 3, size)

            If _fontSize = 1 Then
                _fontWidth = 3
                _fontHeight = 5
            ElseIf _fontSize = 2 Then
                _fontWidth = 4
                _fontHeight = 6
            ElseIf _fontSize = 3 Then
                _fontWidth = 5
                _fontHeight = 7
            End If
        End Sub

        Public Sub setWordWrap(ByVal wrap As Boolean)
            _wordWrap = wrap
        End Sub

        Public Sub writeChar(ByVal c As Char)
            If c = vbLf Then
                _textCursorX = 0
                _textCursorY += _fontHeight
            ElseIf c = vbCr Then
            Else
                putChar(_textCursorX, _textCursorY, c, _fontSize, _fontColor)
                _textCursorX += _fontWidth + 1

                If _wordWrap AndAlso (_textCursorX > (Width - _fontWidth)) Then
                    _textCursorX = 0
                    _textCursorY += _fontHeight + 1
                End If
            End If
        End Sub

        Public Sub putChar(ByVal x As Integer, ByVal y As Integer, ByVal c As Char, ByVal size As Integer, ByVal color As Color)
            Dim font As Byte() = Font5x7
            Dim fontWidth As Integer = 5
            Dim fontHeight As Integer = 7

            If size = 1 Then
                font = Font3x5
                fontWidth = 3
                fontHeight = 5
            ElseIf size = 2 Then
                font = Font4x6
                fontWidth = 4
                fontHeight = 6
            ElseIf size = 3 Then
            End If

            For i As Integer = 0 To fontWidth + 1 - 1
                Dim line As Integer

                If i = fontWidth Then
                    line = &H0
                Else
                    line = font((CInt(c) - &H20) * fontWidth + i)
                End If

                For j As Integer = 0 To fontHeight + 1 - 1

                    If (line And &H1) = 1 Then
                        drawPixel(x + i, y + j, color)
                    End If

                                        ''' Cannot convert AssignmentExpressionSyntax, System.NotSupportedException: RightShiftAssignmentExpression is not supported!
'''    at ICSharpCode.CodeConverter.VB.SyntaxKindExtensions.ConvertToken(SyntaxKind t, TokenContext context)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.MakeAssignmentStatement(AssignmentExpressionSyntax node)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
''' 
''' Input: 
''' 
'''                     line >>= 1
''' 
                Next
            Next
        End Sub

        Public Sub clearRect(ByVal fx As Integer, ByVal fy As Integer, ByVal fw As Integer, ByVal fh As Integer)
            Dim maxX, maxY As Integer
            maxX = If((fx + fw) > Width, Width, (fx + fw))
            maxY = If((fy + fh) > Height, Height, (fy + fh))

            For b As Integer = pwmbits - 1 To 0

                For x As Integer = fx To maxX - 1

                    For y As Integer = fy To maxY - 1

                        If y < 8 Then
                            display.planes(b).colormatrix(y).color1(x).R = 0
                            display.planes(b).colormatrix(y).color1(x).G = 0
                            display.planes(b).colormatrix(y).color1(x).B = 0
                        Else
                            display.planes(b).colormatrix(y - 8).color2(x).R = 0
                            display.planes(b).colormatrix(y - 8).color2(x).G = 0
                            display.planes(b).colormatrix(y - 8).color2(x).B = 0
                        End If
                    Next
                Next
            Next
        End Sub
    End Class
End Namespace
