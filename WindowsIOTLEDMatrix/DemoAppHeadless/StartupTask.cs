using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading;
using Windows.ApplicationModel.Background;
using LedMatrixEngineSharp;
using Microsoft.Graphics.Canvas.Text;
using Windows.UI;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace DemoAppHeadless
{
    public sealed class StartupTask : IBackgroundTask
    {
        //This is a headless app for Win IOT

        //Led Matrix
        RgbMatrix matrix;
        System.Numerics.Vector2 v2;
        System.Numerics.Vector2 v1;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            //initialize the led matrix
           
            matrix = new RgbMatrix();
            
            //Don't need await
            Windows.System.Threading.ThreadPool.RunAsync(matrix.updateDisplay, Windows.System.Threading.WorkItemPriority.High);
            //draw on the led matrix
            drawSomething();
        }

        private void drawSomething()
        {
            //System.Threading.Tasks.Task delay = System.Threading.Tasks.Task.Delay(1000);
            //clear the matrix
            matrix.Session.Clear(Color.FromArgb(255, 0, 0, 0));

            //matrix.RowsPer = 4;

            CanvasTextFormat ff = new CanvasTextFormat();
            ff.FontSize = 16;
            ff.FontFamily = "Courier New";
            ff.HorizontalAlignment = CanvasHorizontalAlignment.Center;
            //write hello world
            v1 = new System.Numerics.Vector2();
            v2 = new System.Numerics.Vector2();
            v2.X = 33;
            v2.Y = 0;
            v1.X = 0;
            v1.Y = 0;
            DateTime epoch = DateTime.UtcNow;
            long millis = (long)((DateTime.UtcNow - epoch).TotalMilliseconds);
            int x = 0;
            while (true)
            {
                //TimeSpan ts = DateTime.Now. + TimeSpan.FromSeconds(2);

                // matrix.Session.DrawText("Hello World!", 0, 0, 128, 16, Color.FromArgb(255, 255, 0, 0), ff);

                //draw a circle
                //matrix.Session.DrawCircle(54, 16, 10, Color.FromArgb(255, 0, 255, 0), 1);
                //matrix.Session.FillRectangle(0, 0, 16, 32, Color.FromArgb(255, 255, 255, 255));
                //matrix.Session.DrawLine((0,0),(5,0), Color.FromArgb(255, 255, 255, 255));
                // matrix.Session.DrawLine(v1, v2, Color.FromArgb(255, 255, 255, 255));
                //for (int x = 0; x < 16; x++)
                //{
                //    System.Threading.Tasks.Task.Delay(TimeSpan.FromTicks(2000));
                //    if (8 < x || x > 16)
                //    {
                //        System.Threading.Tasks.Task.Delay(TimeSpan.FromTicks(2000));
                //        System.Threading.Tasks.Task.Delay(1000);
                //        //for (int y = 0; y < 32; y++) {
                //        matrix.drawPixel(x, 0, Color.FromArgb(255, 255, 255, 255));
                //        System.Threading.Tasks.Task.Delay(TimeSpan.FromTicks(2000));
                //        //System.Threading.Tasks.Task.Delay(1000);
                //        //matrix.drawPixel(x, 0, Color.FromArgb(0, 0, 0, 0));
                //    }//}
                //    else if (x >=8 && x <16)
                //    {
                //        matrix.drawPixel(x+1, 0, Color.FromArgb(255, 255, 255, 255));
                //    }

                //}
                // Save this for testing every second
                //millis = (long)((DateTime.UtcNow - epoch).TotalMilliseconds);
                //matrix.drawPixel(0, 0, Color.FromArgb(255, 255, 255, 255));
                
                //    if (millis > 1000)
                //{ 
                //            matrix.drawPixel(x, 0, Color.FromArgb(255, 255, 255, 255));
                //            x++;
                //            epoch = DateTime.UtcNow;
                //        //}
                //    }
                //if (x > 17)
                //{
                //    x = 0;
                //    matrix.Session.Clear(Color.FromArgb(255, 0, 0, 0));
                //}
                // Save until here for testin every second
                    //}
                    //for (int x = 0; x < 16; x++)
                    //{
                    //    matrix.drawPixel(x, 1, Color.FromArgb(255, 255, 255, 0));
                    //    //System.Threading.Tasks.Task.Delay(1000);
                    //    //matrix.drawPixel(x, 0, Color.FromArgb(0, 0, 0, 0));
                    //}
                    //for (int x = 0; x < 24; x++)
                    //{
                    //    matrix.drawPixel(x, 2, Color.FromArgb(255, 0, 255, 255));
                    //    //System.Threading.Tasks.Task.Delay(1000);
                    //    //matrix.drawPixel(x, 0, Color.FromArgb(0, 0, 0, 0));
                    //}
                    //for (int x = 0; x < 32; x++)
                    //{
                    //    matrix.drawPixel(x, 3, Color.FromArgb(255, 255, 0, 0));
                    //    //System.Threading.Tasks.Task.Delay(1000);
                    //    //matrix.drawPixel(x, 0, Color.FromArgb(0, 0, 0, 0));
                    //}
                    //for (int x = 0; x < 64; x++)
                    //{
                    //    matrix.drawPixel(x, 4, Color.FromArgb(255, 255, 0, 0));
                    //    //System.Threading.Tasks.Task.Delay(1000);
                    //    //matrix.drawPixel(x, 0, Color.FromArgb(0, 0, 0, 0));
                    //}
                    //for (int x = 0; x < 1; x++)
                    //{
                    //    matrix.drawPixel(x, 1, Color.FromArgb(255, 0, 255, 0));
                    //    //System.Threading.Tasks.Task.Delay(1000);
                    //    //matrix.drawPixel(x, 0, Color.FromArgb(0, 0, 0, 0));
                    //}
                    //for (int y = 0; y < 8; y++)
                    //{
                    //    matrix.drawPixel(3, y, Color.FromArgb(255, 255, 0, 0));
                    //    //System.Threading.Tasks.Task.Delay(1000);
                    //    //matrix.drawPixel(x, 0, Color.FromArgb(0, 0, 0, 0));
                    //}

                    //matrix.clearDisplay();
                    //System.Threading.Tasks.Task.Delay(1000);
                    //for (int x = 15; x < 0; x--)
                    //{
                    //    matrix.drawPixel(x, 0, Color.FromArgb(255, 255, 255, 255));
                    //    //System.Threading.Tasks.Task.Delay(1000);
                    //    //matrix.drawPixel(x+1, 0, Color.FromArgb(0, 0, 0, 0));
                    //}
                    //matrix.drawPixel(5, 0, Color.FromArgb(255, 255, 255, 255));
                    // matrix.Session.DrawLine(v1, v2, Color.FromArgb(255, 255, 255, 255));
                    //System.Threading.Tasks.Task.Delay(1000);
                    //matrix.clearDisplay();
                }
            //flush the win2d to the led matrix for the specified rectangle 
            //matrix.Flush(0, 0, 128, 32);
        }
    }
}
