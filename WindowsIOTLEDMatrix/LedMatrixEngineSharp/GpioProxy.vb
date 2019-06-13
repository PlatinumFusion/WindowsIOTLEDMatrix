Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Windows.Devices.Gpio

Namespace LedMatrixEngineSharp
    Public Class GpioProxy
        Public Property gpio As GpioController
        Const OE_PIN As Integer = 18
        Const CLOCK_PIN As Integer = 17
        Const LATCH_PIN As Integer = 4
        Const ROWADDRA_PIN As Integer = 22
        Const ROWADDRB_PIN As Integer = 23
        Const ROWADDRC_PIN As Integer = 24
        Const ROWADDRD_PIN As Integer = 25
        Const R1_PIN As Integer = 11
        Const G1_PIN As Integer = 27
        Const B1_PIN As Integer = 7
        Const R2_PIN As Integer = 8
        Const G2_PIN As Integer = 9
        Const B2_PIN As Integer = 10
        Const P2R1_PIN As Integer = 12
        Const P2G1_PIN As Integer = 5
        Const P2B1_PIN As Integer = 6
        Const P2R2_PIN As Integer = 19
        Const P2G2_PIN As Integer = 13
        Const P2B2_PIN As Integer = 20
        Public outputEnabled As GpioPin
        Public clock As GpioPin
        Public latch As GpioPin
        Public rowAddressA As GpioPin
        Public rowAddressB As GpioPin
        Public rowAddressC As GpioPin
        Public rowAddressD As GpioPin
        Public r1 As GpioPin
        Public g1 As GpioPin
        Public b1 As GpioPin
        Public r2 As GpioPin
        Public g2 As GpioPin
        Public b2 As GpioPin
        Public P2r1 As GpioPin
        Public P2g1 As GpioPin
        Public P2b1 As GpioPin
        Public P2r2 As GpioPin
        Public P2g2 As GpioPin
        Public P2b2 As GpioPin

        Public Sub New()
            gpio = GpioController.GetDefault()
        End Sub

        Public Sub setupOutputBits()
            outputEnabled = gpio.OpenPin(OE_PIN)
            outputEnabled.SetDriveMode(GpioPinDriveMode.Output)
            clock = gpio.OpenPin(CLOCK_PIN)
            clock.SetDriveMode(GpioPinDriveMode.Output)
            latch = gpio.OpenPin(LATCH_PIN)
            latch.SetDriveMode(GpioPinDriveMode.Output)
            rowAddressA = gpio.OpenPin(ROWADDRA_PIN)
            rowAddressA.SetDriveMode(GpioPinDriveMode.Output)
            rowAddressB = gpio.OpenPin(ROWADDRB_PIN)
            rowAddressB.SetDriveMode(GpioPinDriveMode.Output)
            rowAddressC = gpio.OpenPin(ROWADDRC_PIN)
            rowAddressC.SetDriveMode(GpioPinDriveMode.Output)
            rowAddressD = gpio.OpenPin(ROWADDRD_PIN)
            rowAddressD.SetDriveMode(GpioPinDriveMode.Output)
            r1 = gpio.OpenPin(R1_PIN)
            r1.Write(GpioPinValue.Low)
            r1.SetDriveMode(GpioPinDriveMode.Output)
            b1 = gpio.OpenPin(B1_PIN)
            b1.Write(GpioPinValue.Low)
            b1.SetDriveMode(GpioPinDriveMode.Output)
            g1 = gpio.OpenPin(G1_PIN)
            g1.Write(GpioPinValue.Low)
            g1.SetDriveMode(GpioPinDriveMode.Output)
            r2 = gpio.OpenPin(R2_PIN)
            r2.Write(GpioPinValue.Low)
            r2.SetDriveMode(GpioPinDriveMode.Output)
            b2 = gpio.OpenPin(B2_PIN)
            b2.Write(GpioPinValue.Low)
            b2.SetDriveMode(GpioPinDriveMode.Output)
            g2 = gpio.OpenPin(G2_PIN)
            g2.Write(GpioPinValue.Low)
            g2.SetDriveMode(GpioPinDriveMode.Output)
            P2r1 = gpio.OpenPin(P2R1_PIN)
            P2r1.Write(GpioPinValue.Low)
            P2r1.SetDriveMode(GpioPinDriveMode.Output)
            P2b1 = gpio.OpenPin(P2B1_PIN)
            P2b1.Write(GpioPinValue.Low)
            P2b1.SetDriveMode(GpioPinDriveMode.Output)
            P2g1 = gpio.OpenPin(P2G1_PIN)
            P2g1.Write(GpioPinValue.Low)
            P2g1.SetDriveMode(GpioPinDriveMode.Output)
            P2r2 = gpio.OpenPin(P2R2_PIN)
            P2r2.Write(GpioPinValue.Low)
            P2r2.SetDriveMode(GpioPinDriveMode.Output)
            P2b2 = gpio.OpenPin(P2B2_PIN)
            P2b2.Write(GpioPinValue.Low)
            P2b2.SetDriveMode(GpioPinDriveMode.Output)
            P2g2 = gpio.OpenPin(P2G2_PIN)
            P2g2.Write(GpioPinValue.Low)
            P2g2.SetDriveMode(GpioPinDriveMode.Output)
        End Sub

        Private lastr1 As GpioPinValue
        Private lastg1 As GpioPinValue
        Private lastb1 As GpioPinValue
        Private lastr2 As GpioPinValue
        Private lastg2 As GpioPinValue
        Private lastb2 As GpioPinValue
        Private lastP2r1 As GpioPinValue
        Private lastP2g1 As GpioPinValue
        Private lastP2b1 As GpioPinValue
        Private lastP2r2 As GpioPinValue
        Private lastP2g2 As GpioPinValue
        Private lastP2b2 As GpioPinValue

        Public Sub setRGB(ByVal _r1 As GpioPinValue, ByVal _g1 As GpioPinValue, ByVal _b1 As GpioPinValue, ByVal _r2 As GpioPinValue, ByVal _g2 As GpioPinValue, ByVal _b2 As GpioPinValue)
            If lastr1 <> _r1 Then
                r1.Write(_r1)
                lastr1 = _r1
            End If

            If lastg1 <> _g1 Then
                g1.Write(_g1)
                lastg1 = _g1
            End If

            If lastb1 <> _b1 Then
                b1.Write(_b1)
                lastb1 = _b1
            End If

            If lastr2 <> _r2 Then
                r2.Write(_r2)
                lastr2 = _r2
            End If

            If lastg2 <> _g2 Then
                g2.Write(_g2)
                lastg2 = _g2
            End If

            If lastb2 <> _b2 Then
                b2.Write(_b2)
                lastb2 = _b2
            End If
        End Sub

        Public Sub setRowAddress(ByVal row As Integer)
            rowAddressA.Write(If((row And 1) = 1, GpioPinValue.High, GpioPinValue.Low))
            rowAddressB.Write(If((row And 2) = 2, GpioPinValue.High, GpioPinValue.Low))
            rowAddressC.Write(If((row And 4) = 4, GpioPinValue.High, GpioPinValue.Low))
        End Sub
    End Class
End Namespace
