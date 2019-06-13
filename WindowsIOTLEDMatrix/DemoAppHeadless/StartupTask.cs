using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Diagnostics;
using Windows.ApplicationModel.Background;
using LedMatrixEngineSharp;
using Microsoft.Graphics.Canvas.Text;
using Windows.UI;
using Microsoft.Graphics.Canvas.Brushes;
//using Windows.UI.Xaml.Media;
using System.Drawing;
//Master Branch
// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace DemoAppHeadless
{

    public sealed class StartupTask : IBackgroundTask
    {
        //This is a headless app for Win IOT
        string lapstring = ""; // this is set to the Lapcount
        //Led Matrix
        RgbMatrix4s matrix;
        System.Numerics.Vector2 v2;
        System.Numerics.Vector2 v1;
        ServerClass sclass = new ServerClass();
        MainTCP mTCP = new MainTCP();
        int x = 0;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            //initialize the led matrix
            x = x + 1;
            matrix = new RgbMatrix4s();
             //sclass.Start();
             //mTCP.New();
            Windows.System.Threading.ThreadPool.RunAsync(mTCP.New, Windows.System.Threading.WorkItemPriority.Low);
            //Don't need await
            Windows.System.Threading.ThreadPool.RunAsync(matrix.updateDisplay, Windows.System.Threading.WorkItemPriority.High);
            //draw on the led matrix
            drawSomething();
            Debug.WriteLine(x.ToString());
        }

        private void drawSomething()
        {
            
            //matrix.Session.Clear(Color.FromArgb(255, 0, 0, 0)); //This still does not work
            CanvasTextFormat ff = new CanvasTextFormat();
            ff.FontSize = 16;
            ff.FontFamily = "Courier New";
            ff.HorizontalAlignment = CanvasHorizontalAlignment.Center;
            //write hello world
            v1 = new System.Numerics.Vector2();
            v2 = new System.Numerics.Vector2();
            v2.X = 64; //This is still untested
            v2.Y = 0;
            v1.X = 0;
            v1.Y = 0;
            DateTime epoch = DateTime.UtcNow;
            long millis = (long)((DateTime.UtcNow - epoch).TotalMilliseconds);
            int x = 0;
            int y1 = 0;
            //matrix.Session.DrawText("Hello World!", 0, 0, 128, 16, Color.FromArgb(255, 255, 0, 0), ff);  //Untested
            while (true)
            {
                //matrix.Session.DrawText("Hello World!", 0, 0, 128, 16, Color.FromArgb(255, 255, 0, 0), ff); // Untested

                //draw a circle -- Un Tested -- plus I think I need to make everything go though the drawPixel function
                //matrix.Session.DrawCircle(54, 16, 10, Color.FromArgb(255, 0, 255, 0), 1);
                //matrix.Session.FillRectangle(0, 0, 16, 32, Color.FromArgb(255, 255, 255, 255));
                //matrix.Session.DrawLine((0,0),(5,0), Color.FromArgb(255, 255, 255, 255));
                //matrix.Session.DrawLine(v1, v2, Color.FromArgb(255, 255, 255, 255));




                //This for loop will fill the panel White
                //for (int x = 0; x < 32; x++)
                //{
                //    for (int y = 0; y < 16; y++)
                //    {
                //        matrix.drawPixel(x, y, Color.FromArgb(255, 255, 255, 255));
                //        //matrix.drawPixel(x, 0, Color.FromArgb(0, 0, 0, 0));

                //    }
                //}


                //If you want to draw pixels
                //matrix.drawPixel(0, 5, Color.FromArgb(255, 255, 0, 0));
                //matrix.drawPixel(0, 0, Color.FromArgb(255, 255, 255, 255));


                //To Test the line by line
                //******************************************
                //millis = (long)((DateTime.UtcNow - epoch).TotalMilliseconds);
                //if (millis > 1)
                //{

                //    if (x >= 63)
                //    {

                //        matrix.Session.Clear(Windows.UI.Color.FromArgb(255, 0, 0, 0));
                //        for (int y = 0; y < x; y++)
                //        {
                //            //matrix.drawPixel(y, 4, Color.FromArgb(255, 0, 0, 0));
                //            // matrix.drawPixel(y, 8, Color.FromArgb(255, 0, 0, 0));
                //            //matrix.Translater(y, 0, Color.FromArgb(255, 0, 0, 0));
                //            //matrix.Translater(y, 16, Color.FromArgb(255, 0, 0, 0));
                //        }
                //        x = 0;
                //        y1++;
                //    }
                //    else

                //    {
                //        if (y1 <= 31)
                //        {
                //            matrix.drawPixel(x, y1, Windows.UI.Color.FromArgb(255, 255, 255, 255));
                //            //Debug.Write(x + "," + y1 + " ");
                //            //Debug.WriteLine(x + "," + y1);
                //        }
                //        else
                //        {

                //        }
                //        //matrix.drawPixel(8,4, Color.FromArgb(255, 255, 0, 0));
                //        //matrix.drawPixel(0,0, Color.FromArgb(255, 0, 255, 0));
                //        //matrix.drawPixel(x, 8, Color.FromArgb(255, 255, 255, 255));
                //        //matrix.Translater(x, 0, Color.FromArgb(255, 255, 255, 255));
                //        x++;

                //    }
                //    epoch = DateTime.UtcNow;
                //    //}
                //}//*******************************************************************


                //To TEST the CHAR
                //matrix.settextcursor(5, 8);
                matrix.setTextCursor(10, 10);
                //matrix.setfontsize(3);
                matrix.setFontSize(3);
                matrix.setBackGroundColor(Windows.UI.Color.FromArgb(MainTCP.RaceColor.A, MainTCP.RaceColor.R, MainTCP.RaceColor.G, MainTCP.RaceColor.B));
                //matrix.Session.FillRectangle(0, 0, 64, 32, );
                //matrix.setFontColor(Color.FromArgb(255, 255, 0, 0));
                CanvasSolidColorBrush SolidColorBackground = new CanvasSolidColorBrush(matrix.Session,Windows.UI.Color.FromArgb(MainTCP.RaceColor.A, MainTCP.RaceColor.R, MainTCP.RaceColor.G, MainTCP.RaceColor.B));
                matrix.Session.FillRectangle(0, 0, 64, 32, SolidColorBackground);
                matrix.setFontColor(Windows.UI.Color.FromArgb(MainTCP.RaceColor.A, MainTCP.RaceColor.R, MainTCP.RaceColor.G, MainTCP.RaceColor.B));
                //for (int x1 = 0; x < 64; x++)
                //{
                //    for (int y = 0; y < 32; y++)
                //    {
                //        matrix.drawPixel(x1, y, Windows.UI.Color.FromArgb(255, 0, 0, 0));
                //    }
                //}
                //matrix.Session.DrawLine(0, 0, 32, 64, Windows.UI.Color.FromArgb(255, 255, 255, 255));
                //matrix.Session.DrawText("Hello World!", 0, 0, 128, 16, Windows.UI.Color.FromArgb(255, 255, 0, 0), ff);
                if (MainTCP.lapcountString != null)
                {
                    //matrix.Flush(0,0,64,32);
                    if (lapstring != MainTCP.lapcountString)
                    {
                        lapstring = MainTCP.lapcountString;

                    }
                    matrix.Flush(0, 0, 64, 32);
                    

                    char[] charArr = lapstring.ToCharArray();
                    foreach (char ch in charArr)
                    {
                        matrix.writeChar(ch);
                    }

                }
                else
                {  }

                //
                //matrix.setTextCursor(2, 10);
                ////matrix.setfontsize(3);
                //matrix.setFontSize(3);
                ////matrix.setFontColor(Color.FromArgb(255, 0, 255, 0));
                //sentence = "Fusion";
                //charArr = sentence.ToCharArray();
                //foreach (char ch in charArr)
                //{
                //    matrix.writeChar(ch);
                //}
                ////matrix.setTextCursor(5, 20);
                ////matrix.setfontsize(3);
                //matrix.setFontSize(3);
                //matrix.setFontColor(Color.FromArgb(255, 0, 0, 255));
                //sentence = "Technology";
                //charArr = sentence.ToCharArray();
                //foreach (char ch in charArr)
                //{
                //    matrix.writeChar(ch);
                //}
                //if you want to write each Char
                //matrix.writeChar('H');
                //matrix.writeChar('I');




                //matrix.clearDisplay(); //This does not work

            }
            //flush the win2d to the led matrix for the specified rectangle  //Does not work
            //matrix.Flush(0, 0, 128, 32);
        }
    }
}
