using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPMP_Publisher
{
    public class IoTHubConnection
    {
        public static DeviceClient client;

        public static void Initialize(string connString)
        {
            client = DeviceClient.CreateFromConnectionString(connString);
            
            client.OpenAsync();
        }
    }
}
