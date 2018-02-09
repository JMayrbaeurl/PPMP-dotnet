using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IoT.PPMP;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PPMP_Publisher.Controllers
{
 
    public abstract class AbstractPPMPController : Controller
    {
        protected void PublishToIoTHub(object messageobject, Dictionary<string, string> props = null)
        {
            this.PublishToIoTHub(messageobject, JsonConvert.SerializeObject(messageobject), props);
        }

        protected void PublishToIoTHub(object messageobject, string json, Dictionary<string, string> props = null)
        {
            Microsoft.Azure.Devices.Client.Message msg = this.CreateMessage(json);

            if (msg != null)
            {
                msg.Properties.Add("payloadformat", "ppmp");
                msg.Properties.Add("payloadformatversion", "v2");

                if (props != null && props.Count > 0)
                {
                    foreach (KeyValuePair<string, string> entry in props)
                    {
                        msg.Properties.Add(entry.Key, entry.Value);
                    }
                }

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

        protected string GetDeviceIDFromMessage(object msgObject)
        {
            return (msgObject as DeviceMessage).DeviceID();
        }
    }
}
