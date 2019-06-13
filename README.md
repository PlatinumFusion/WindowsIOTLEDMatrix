# WindowsIOTLEDMatrix
This Library allows you to use LED Matrix with your Raspberry Pi 3 on Windows IOT Core.
This currently works with 2x2 panels of 1/4 scan 16x32 RGB matrix

# Finally The Master Release

The pinout is mapped to be used with the adapter used here: https://github.com/hzeller/rpi-rgb-led-matrix
The numbers below are not the header pins on the rpi, but the gpio numbers

The port mapping to make it work is:<br/>
GPIO 18		-->  OE (Output Enabled)<br/>
GPIO 17		-->  CLK (Serial Clock)<br/>
GPIO 4		-->  LAT (Data Latch)<br/>
GPIO 22		-->  A  --|<br/>
GPIO 23		-->  B    |   Row<br/>
GPIO 24		-->  C    | Address<br/>
GPIO 25		-->  D  --|<br/>
GPIO 11		-->  [1]R1 (LED 1: Red)<br/>
GPIO 7		-->  [1]B1 (LED 1: Blue)<br/>
GPIO 27		-->  [1]G1 (LED 1: Green)<br/>
GPIO 8		-->  [1]R2 (LED 2: Red)<br/>
GPIO 10		-->  [1]B2 (LED 2: Blue)<br/>
GPIO 9		-->  [1]G2 (LED 2: Green)<br/>
# Second (bottom) Panels
GPIO 12		-->  [2]R1 (LED 1: Red)<br/>
GPIO 6		-->  [2]B1 (LED 1: Blue)<br/>
GPIO 5		-->  [2]G1 (LED 1: Green)<br/>
GPIO 19		-->  [2]R2 (LED 2: Red)<br/>
GPIO 20		-->  [2]B2 (LED 2: Blue)<br/>
GPIO 13		-->  [2]G2 (LED 2: Green)<br/>

# What it looks like
![Display](https://github.com/PlatinumFusion/WindowsIOTLEDMatrix/raw/Master/img/IMG_1466.JPG)

# The Adapter:
![Adapter](https://github.com/PlatinumFusion/WindowsIOTLEDMatrix/raw/Master/img/IMG_1470.JPG)
Check out these adapters: https://github.com/hzeller/rpi-rgb-led-matrix/tree/master/adapter
The 3rd panel output is not (yet) supported.


The library is still not very fast and does some flickering. Any help will be apreciated.
This library was based on the work of https://github.com/mattdh666/rpi-led-matrix-panel

This c# library is initiate as a contribution by Highwave Creations http://www.highwave.org
