using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Microsoft.IoT.PPMP
{

    public sealed class Device
    {
        [JsonProperty(PropertyName = "deviceID", Required = Required.Always)]
        public string DeviceID { get; }

        [JsonProperty(PropertyName = "metaData", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Metadata { get; set; }

        [JsonProperty(PropertyName = "operationalStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string OperationalStatus { get; set; }

        [JsonConstructor]
        public Device(string deviceID)
        {
            this.DeviceID = deviceID;

            if (String.IsNullOrEmpty(deviceID))
            {
                throw new ArgumentException("Device id must not be null or empty");
            }

            this.Metadata = new Dictionary<string, string>();
        }
    }

    

}

