using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using Restup.Webserver.File;
using Restup.Webserver.Rest;
using Restup.Webserver.Http;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using Windows.Networking.Connectivity;

namespace DemoAppHeadless
{
    class MainTCP
    {
        public static System.Timers.Timer Updateuitimer;
        public static System.Timers.Timer ReconnectTimer;
        public static System.Timers.Timer KeepreadingLoopTimer;
  
        public static BackgroundWorker GetLineBGW = new BackgroundWorker();
        public static BackgroundWorker BackgroundWorker1 = new BackgroundWorker();
        public static BackgroundWorker BGwebworker = new BackgroundWorker();
        public static BackgroundWorker BGconnect = new BackgroundWorker();

        public static string myIPaddr;
        public static string ipadr = "172.16.1.127";
        public static Socket client;
        public static NetworkStream stream;
        public static StreamReader reader;
        public static string[] r;
        public static Color bordercolor;
        public static string lapcountString;
        public static Color RaceColor;
        public static Color RaceBrush;
        public static string line = null;
        public async void New(Windows.Foundation.IAsyncAction action)
        {
            ReconnectTimer = new System.Timers.Timer();
            Updateuitimer = new System.Timers.Timer();
            KeepreadingLoopTimer = new System.Timers.Timer();

            ReconnectTimer.Interval = 500;
            Updateuitimer.Interval = 500;
            KeepreadingLoopTimer.Interval = 500;

            ReconnectTimer.Elapsed += new ElapsedEventHandler(ReconnectTimerTick); //Then Calls Connect1()
            ReconnectTimer.Enabled = false;

            GetLineBGW.DoWork +=  new DoWorkEventHandler(GetLineBGW_DoWork);
            BackgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker1_DoWork);


            BackgroundWorker1.WorkerSupportsCancellation = true;
            BackgroundWorker1.WorkerReportsProgress = true;
            GetLineBGW.WorkerSupportsCancellation = true;
            GetLineBGW.WorkerReportsProgress = true;
            //Updateuitimer.Start(); // after loaded check to make sure UI gets updated every 500ms

            //ReconnectTimer.Start();

            Connect1();
            ServerClass servercla = new ServerClass();
            await Task.Run(() =>servercla.Start());
                

        }
        private static void ReconnectTimerTick(Object source, System.Timers.ElapsedEventArgs e)
        {
            Connect1();
        }
        public static void Connect1()
        {
            Debug.WriteLine("Connect1 Triggered");
            //if (!this.Dispatcher.HasThreadAccess)
            //{
            //    Debug.Print("Doesn't have Thread access" + Constants.vbCrLf);
            //    try
            //    {
            //        await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, Connect1);
            //    }
            //    catch
            //    {
            //        Debug.Print("Connect1() cast caught");
            //    }
            //}
            //else
                try
                {
                    RaceColor = Color.Black; // Make sure that the background is black on connection -- incase connection is lost it reverts to black from the <unknonw> flag
                //if (ReconnectTimer.Enabled == false)
                //ReconnectTimer.Enabled = true; // This should already be started  BUT Connect1 is called if the BGReadline fails the read -- thus turning it back on.
                                            // ReconnectTimer get's turned off after a line is read from the socket (I Think..)
                    if (ipadr != "1.1.1.1")
                    {
                    ReconnectTimer.Stop();
                    Debug.WriteLine("Reconnect Timer stopped");
                    try
                    {
                        Debug.WriteLine("Connecting...");
                    Int32 port = 50000;
                        TcpClient client = new TcpClient(ipadr, port); // textBox1.Text
                        client.ReceiveTimeout = 0;
                        Debug.WriteLine("Client Connected");
                        // Dim listener As New TcpListener(IPAddress.Any, 50001)
                        stream = client.GetStream();
                        Debug.WriteLine("Stream got stream");
                        reader = new StreamReader(stream);

                        Debug.WriteLine("stream sent to reader");

                    var Data = new Byte[256];
                        Int32 bytes = 0;
                        String responseData = String.Empty;
                    Debug.WriteLine(@"Reachable \ Please Wait... : " + ipadr); // textBox1.Text
                    ReconnectTimer.Enabled = false;
                        if (stream == null)
                            throw new Exception("Stream is nothing!?!");
                    stream.Flush();

                        Debug.WriteLine("Trying to read data...");
                        line = reader.ReadLine();
                        Debug.WriteLine("Data Below");
                        Debug.WriteLine(line);

                    }

                    //StartKeepReading();
                    catch
                    {
                        Debug.WriteLine("Something Went Wrong");
                        ReconnectTimer.Start();
                    }
                    finally
                    {
                    }
                    // KeepreadingLoopTimer.Start() 'moved this to Backgroundworker1()
                    // ''''  Let's see if we don't call backgroundworker1
                    // all backgroundworker does is call the looptimer
                    // ReconnectTimer.Start()
                    if (BackgroundWorker1.IsBusy == false) //(GetLineBGW.IsBusy == false)//
                    {
                            Debug.WriteLine("Starting Backgroundworker getline");
                            BackgroundWorker1.RunWorkerAsync();
                            Debug.WriteLine("Started Backgroundworker getline");
                        }
                    }
                    else
                    {
                    Debug.Print("IP adr cannot be 1.1.1.1");
                        //LapCount.Text = myIPaddr;
                        lapcountString = myIPaddr;
                    } // If the ip != 1.1.1.1
                }
                catch (SocketException ex)
                {
                Debug.WriteLine("Conncection Failed: " + ex.ToString());
                   // LapCount.Text = myIPaddr;
                    lapcountString = myIPaddr;
                }
                finally
                {
                } // Not Me.Dispatcher.HasThreadAccess
        }
        private async void GetLineBGW_DoWork(object sender, DoWorkEventArgs e) // was private async sub..
        {
            Debug.WriteLine("GetLine Triggered");
            if (stream.CanRead)
            {
                try
                {
                    while (line.Length > 0) // client.Connected 'line = reader.ReadLine() ' was: line.Length > 0
                    {
                        if (BackgroundWorker1.CancellationPending)
                        {
                            BackgroundWorker1.Dispose();
                            GetLineBGW.Dispose();
                            reader.Close();
                            stream.Close();
                        }
                        try
                        {
                            // reader.
                            // If line = reader.ReadLine() Then
                            // If (line = reader.ReadLine()) <> Nothing Then

                            // If reader.Peek >= 0 And (line = reader.ReadLine()) >= 0 Then '' Still erroring
                            // If stream.DataAvailable = True Then
                            if (true)
                            {
                                // If (line = reader.ReadLine()) >= 0 Then
                                line = reader.ReadLine();

                                // Debug.WriteLine(line)
                                string[] r = line.Split(",");
                                if (r[0] == "$G")
                                {
                                    if (r[1] == "1")
                                    {
                                        lapcountString = r[3].ToString();
                                        Debug.WriteLine(line);
                                    }
                                    if (r[1] == "2")
                                    {
                                    }
                                    if (r[1] == "3")
                                    {
                                    }
                                    if (r[1] == "4")
                                    {
                                    }
                                    if (r[1] == "5")
                                    {
                                    }
                                    if (r[1] == "6")
                                    {
                                    }
                                    if (r[1] == "7")
                                    {
                                    }
                                    if (r[1] == "8")
                                    {
                                    }
                                    if (r[1] == "9")
                                    {
                                    }
                                }
                                else if (r[0] == "$F")
                                {
                                    if (r[5] == "\"Green \"")
                                    {
                                        Debug.WriteLine(line);
                                        if (RaceColor != Color.Green)
                                            RaceColor = Color.Green;
                                    }
                                    else if (r[5] == "\"Yellow\"")
                                    {
                                        Debug.WriteLine(line);
                                        if (RaceColor != Color.Yellow)
                                            RaceColor = Color.Yellow;
                                    }
                                    else if (r[5] == "\"Red   \"")
                                    {
                                        Debug.WriteLine(line);
                                        if (RaceColor != Color.Red)
                                            RaceColor = Color.Red;
                                    }
                                    else if (r[5] == "\"      \"")
                                    {
                                        Debug.WriteLine(line);
                                        RaceColor = Color.Black;
                                    }
                                    else
                                        RaceColor = Color.Black;
                                }
                                stream.Flush();
                            }
                            else
                            {
                                BackgroundWorker1.Dispose();
                                GetLineBGW.Dispose();
                                GetLineBGW.CancelAsync();
                                reader.Close();
                                stream.Close();
                                //if (!this.Dispatcher.HasThreadAccess)
                                //{
                                //    try
                                //    {
                                //        //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, KeepreadingLoopTimer.Stop);
                                //        //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, ReconnectTimer.Start);
                                //    }
                                //    catch
                                //    {
                                //    }
                                //}
                                //else
                                //{
                                //    KeepreadingLoopTimer.Stop();
                                //    ReconnectTimer.Start();
                                //}
                            } // for if reader is nothing
                        }
                        catch (Exception al)
                        {
                            GetLineBGW.CancelAsync();
                            Debug.Print(al.ToString());
                        }
                    } // while line is > 0
                }
                catch (Exception Whilee)
                {
                    BackgroundWorker1.Dispose();
                    GetLineBGW.Dispose();
                    GetLineBGW.CancelAsync();
                    reader.Close();
                    stream.Close();
                    Debug.Write(Whilee.ToString());
                    BGconnect.RunWorkerAsync();
                    Connect1();
                }
            }
        }
        public static void Keepreading()
        {
            if (line != null)
            {
                try
                {
                    ReconnectTimer.Stop();
                    long timed = 0;
                    Debug.WriteLine("Connected");
                    if (!GetLineBGW.IsBusy == true)
                        GetLineBGW.RunWorkerAsync();
                    if (lapcountString != null)
                        //LapCount.Text = lapcountString;
                        if (RaceColor != null)
                        {/* TODO Change to default(_) if this is not a reference type */
                         //PlayerFormGrid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(RaceColor.A, RaceColor.R, RaceColor.G, RaceColor.B));
                        }
                    //await Task.Yield();
                }
                catch (Exception e)
                {
                    //ConnectionStatus.Text = "error";
                    BackgroundWorker1.Dispose();
                    GetLineBGW.Dispose();
                    GetLineBGW.CancelAsync();
                    reader.Close();
                    stream.Close();
                    //ConnectionStatus.Text = "Disconnected with Error";
                    //TextBox2.Text = e.ToString();
                    KeepreadingLoopTimer.Stop();
                    ReconnectTimer.Start();
                }
            }
        }
        public async void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Keepreading(); // This is can be called from the keepreadingloop timer
                           // Debug.WriteLine("Backgroundworker started keepreading")
            //if (!this.Dispatcher.HasThreadAccess)
            //{
                //Debug.Print("Doesn't have Thread access" + Constants.vbCrLf);
                try
                {
                    KeepreadingLoopTimer.Start();
                }
                catch
                {
                    Debug.Print("keepreading cast caught");
                }
 
            //else { }
                //KeepreadingLoopTimer.Start();
        }



    }
}
