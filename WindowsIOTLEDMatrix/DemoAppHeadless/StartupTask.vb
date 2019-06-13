Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Net.Http
Imports System.Threading
Imports System.Diagnostics
Imports Windows.ApplicationModel.Background
Imports LedMatrixEngineSharp
Imports Microsoft.Graphics.Canvas.Text
Imports Windows.UI

Namespace DemoAppHeadless
    Public NotInheritable Class StartupTask
        Implements IBackgroundTask

        Private matrix As RgbMatrix4s
        Private v2 As System.Numerics.Vector2
        Private v1 As System.Numerics.Vector2

        Public Sub Run(ByVal taskInstance As IBackgroundTaskInstance) Implements IBackgroundTask.Run
            matrix = New RgbMatrix4s()
            Windows.System.Threading.ThreadPool.RunAsync(New Windows.System.Threading.WorkItemHandler(AddressOf matrix.updateDisplay), Windows.System.Threading.WorkItemPriority.High)
            drawSomething()
        End Sub

        Private Sub drawSomething()
            Dim ff As CanvasTextFormat = New CanvasTextFormat()
            ff.FontSize = 16
            ff.FontFamily = "Courier New"
            ff.HorizontalAlignment = CanvasHorizontalAlignment.Center
            v1 = New System.Numerics.Vector2()
            v2 = New System.Numerics.Vector2()
            v2.X = 64
            v2.Y = 0
            v1.X = 0
            v1.Y = 0
            Dim epoch As DateTime = DateTime.UtcNow
            Dim millis As Long = CLng(((DateTime.UtcNow - epoch).TotalMilliseconds))
            Dim x As Integer = 0
            Dim y1 As Integer = 0

            While True
                millis = CLng(((DateTime.UtcNow - epoch).TotalMilliseconds))

                If millis > 1 Then

                    If x >= 63 Then
                        matrix.Session.Clear(Color.FromArgb(255, 0, 0, 0))

                        For y As Integer = 0 To x - 1
                        Next

                        x = 0
                        y1 += 1
                    Else

                        If y1 <= 31 Then
                            matrix.drawPixel(x, y1, Color.FromArgb(255, 255, 255, 255))
                        Else
                        End If

                        x += 1
                    End If

                    epoch = DateTime.UtcNow
                End If

                matrix.setTextCursor(2, 0)
                matrix.setFontSize(3)
                matrix.setFontColor(Color.FromArgb(255, 255, 0, 0))
                Dim sentence As String = "Platinum"
                Dim charArr As Char() = sentence.ToCharArray()

                For Each ch As Char In charArr
                    matrix.writeChar(ch)
                Next

                matrix.setTextCursor(2, 10)
                matrix.setFontSize(3)
                matrix.setFontColor(Color.FromArgb(255, 0, 255, 0))
                sentence = "Fusion"
                charArr = sentence.ToCharArray()

                For Each ch As Char In charArr
                    matrix.writeChar(ch)
                Next

                matrix.setTextCursor(5, 20)
                matrix.setFontSize(3)
                matrix.setFontColor(Color.FromArgb(255, 0, 0, 255))
                sentence = "Technology"
                charArr = sentence.ToCharArray()

                For Each ch As Char In charArr
                    matrix.writeChar(ch)
                Next
            End While
        End Sub
    End Class
End Namespace
