using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using Restup;
using Restup.Webserver.File;
using Restup.Webserver.Rest;
using Restup.Webserver.Http;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using Restup.WebServer;
using Restup.HttpMessage;
using Restup.HttpMessage.Headers.Response;
using Restup.HttpMessage.Models.Contracts;
using Restup.HttpMessage.Models.Schemas;
using Restup.WebServer.Logging;
using Windows.Networking.Connectivity;
using Windows.ApplicationModel.Background;

namespace DemoAppHeadless
{
    public sealed class ServerClass
    {
        private static  int num = 0;
        //private HttpServer httpServer;
       // private BackgroundTaskDeferral _deferral;
    
        public async void Start()
        {

                //_deferral = taskInstance.GetDeferral;
                num = num + 1;
                Debug.WriteLine("Run Task in ServerClass has been called? " + num.ToString());

                var restRouteHandler = new RestRouteHandler();
                restRouteHandler.RegisterController<ParameterController>();
                var configuration = new HttpServerConfiguration().ListenOnPort(8088).RegisterRoute("api", restRouteHandler).RegisterRoute(new StaticFileRouteHandler("Assets")).EnableCors();

                var httpServer = new HttpServer(configuration);

                await httpServer.StartServerAsync(); //await
                MainTCP.myIPaddr = FindIPAddress();

                
            
        }

        public static string FindIPAddress()
        {
            List<string> ipAddresses = new List<string>();
            var hostnames = NetworkInformation.GetHostNames();

            foreach (var hn in hostnames)
            {
                if (hn.IPInformation != null && ((long)hn.IPInformation.NetworkAdapter.IanaInterfaceType == (long)71 || (long)hn.IPInformation.NetworkAdapter.IanaInterfaceType == (long)6))
                {
                    string ipAddress = hn.DisplayName;
                    ipAddresses.Add(ipAddress);
                }
            }

            if (ipAddresses.Count < 1)
                return null;
            else if (ipAddresses.Count == 1)
                return ipAddresses[0];
            else
                return ipAddresses[ipAddresses.Count - 1];
        }
    }

    public sealed class DataReceived
    {
        public int ID { get; set; }
        public string PropName { get; set; }
    }

    [RestController(InstanceCreationType.Singleton)]
    public sealed class ParameterController
    {
        [UriFormat("/simpleparameter/{id}/property/{propName}")]
        //[UriFormat("/?IPaddress={propName}")]
        public IGetResponse GetWithSimpleParameters(int id, string propName)
        {
            Debug.WriteLine("Get parameters has been called");
            // Await _mainClass.Connect1() 'This with the new mainpage causes issues
            MainTCP.ipadr = propName;
            // MainPage.UpdateUI()
            Debug.WriteLine("ID: " + id.ToString());
            Debug.WriteLine("propname: " + propName.ToString());
            return new GetResponse(GetResponse.ResponseStatus.OK, new DataReceived()
            {
                ID = id,
                PropName = propName
            });
        }
    }
}