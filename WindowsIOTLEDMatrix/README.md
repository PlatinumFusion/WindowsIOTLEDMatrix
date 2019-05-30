# WindowsIOTLEDMatrix
This Library allows you to use LED Matrix with your Raspberry Pi 3 on Windows IOT Core. It is working with LED matrices 32x16 that can be chained. Currently tested to a chain of 4 matrices of 32x32 => 128x32

I have remapped the pinout to refect the adapter used by https://github.com/hzeller/rpi-rgb-led-matrix
The numbers below are not the header pins on the rpi, but the gpio numbers

The port mapping to make it work is:<br/>
GPIO 18		-->  OE (Output Enabled)<br/>
GPIO 17		-->  CLK (Serial Clock)<br/>
GPIO 4		-->  LAT (Data Latch)<br/>
GPIO 22		-->  A  --|<br/>
GPIO 23		-->  B    |   Row<br/>
GPIO 24		-->  C    | Address<br/>
GPIO 25		-->  D  --|<br/>
GPIO 11		-->  R1 (LED 1: Red)<br/>
GPIO 7		-->  B1 (LED 1: Blue)<br/>
GPIO 27		-->  G1 (LED 1: Green)<br/>
GPIO 8		-->  R2 (LED 2: Red)<br/>
GPIO 10		-->  B2 (LED 2: Blue)<br/>
GPIO 9		-->  G2 (LED 2: Green)<br/>

This is the DEV, and is untested.  As of now the Master is untouched from forking from faicalsaid: https://github.com/faicalsaid/WindowsIOTLEDMatrix

The library is still not very fast and does some flickering. Any help will be apreciated.
This library was based on the work of https://github.com/mattdh666/rpi-led-matrix-panel

This c# library is initiate as a contribution by Highwave Creations http://www.highwave.org
