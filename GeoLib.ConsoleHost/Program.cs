using GeoLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));

            //string address = "net.tcp://localhost:8111/GeoService";
            //Binding binding = new NetTcpBinding();
            //Type contract = typeof(GeoLib.Contracts.IGeoService);
            //hostGeoManager.AddServiceEndpoint(contract, binding, address);

            hostGeoManager.Open();

            Console.WriteLine("Service Started, Press enter to Close");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
