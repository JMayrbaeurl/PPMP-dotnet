using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IoT.PPMP;
using Microsoft.IoT.PPMP.Machine;
using Microsoft.IoT.PPMP.Measurement;
using Microsoft.IoT.PPMP.Process;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PPMP_Publisher.Controllers
{

    public class PPMPController : Controller
    {
        [HttpPost("/rest/v2/message")]
        public void Post([FromBody]MessagePayload message)
        {
            Console.WriteLine(message);

            this.PublishToIoTHub(message);
        }

        [HttpPost("/rest/v2/measurement")]
        public void Post([FromBody]MeasurementPayload message)
        {
            Console.WriteLine(message);

            this.PublishToIoTHub(message);
        }

        [HttpPost("/rest/v2/process")]
        public void Post([FromBody]ProcessPayload message)
        {
            Console.WriteLine(message);

            this.PublishToIoTHub(message);
        }

        [Route("/rest/v2")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Welcome message", "PPMP REST server" };
        }

        private void PublishToIoTHub(object messageobject)
        {
            this.PublishToIoTHub(messageobject, JsonConvert.SerializeObject(messageobject));
        }

        private void PublishToIoTHub(object messageobject, string json)
        {
            Microsoft.Azure.Devices.Client.Message msg = this.CreateMessage(json);

            if (msg != null)
            {
                msg.Properties.Add("payloadformat", "ppmp");
                msg.Properties.Add("payloadformatversion", "v2");

                if (messageobject is DeviceMessage)
                    msg.Properties.Add("deviceID", this.GetDeviceIDFromMessage(messageobject));

                IoTHubConnection.client.SendEventAsync(msg);
            }
        }

        protected Microsoft.Azure.Devices.Client.Message CreateMessage(string json)
        {
            Microsoft.Azure.Devices.Client.Message msg = 
                new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(json));

            return msg;
        }

        private string GetDeviceIDFromMessage(object msgObject)
        {
            return (msgObject as DeviceMessage).DeviceID();
        }
    }
}
