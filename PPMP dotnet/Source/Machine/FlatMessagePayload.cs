using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IoT.PPMP.Machine
{
    public class FlatMessagePayload : DeviceMessage
    {
        [JsonProperty(PropertyName = "content-spec")]
        public string Contentspec { get; set; }

        [JsonProperty(PropertyName = "device")]
        public Device Device { get; set; }

        [JsonProperty(PropertyName = "message")]
        public Message Message { get; set; }

        public FlatMessagePayload()
        {
            this.Contentspec = MessagePayload.default_Contentspec;
        }

        public FlatMessagePayload(string deviceID) : this()
        {
            this.Device = new Device(deviceID);
        }

        public FlatMessagePayload(MessagePayload fromMessage, int messageindex = 0)
        {
            this.Contentspec = fromMessage.Contentspec;
            this.Device = fromMessage.Device;
            this.Message = (fromMessage.Messages != null && fromMessage.Messages.Count > messageindex) 
                ? fromMessage.Messages[messageindex] : null;
        }

        public string DeviceID()
        {
            return this.Device != null ? this.Device.DeviceID : null;
        }

        public static IList<FlatMessagePayload> ToFlatMessagePayloads(MessagePayload msgPayload)
        {
            List<FlatMessagePayload> result = new List<FlatMessagePayload>();

            if (msgPayload != null && msgPayload.Messages != null && msgPayload.Messages.Count > 0)
            {
                for (int i = 0; i < msgPayload.Messages.Count; i++)
                    result.Add(new FlatMessagePayload(msgPayload, i));
            }

            return result;
        }
    }
}
