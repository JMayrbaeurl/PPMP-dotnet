using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Microsoft.IoT.PPMP.Machine
{
 
    public sealed class Message
    {
        [JsonProperty(PropertyName = "code", Required = Required.Always)]
        public string Code { get; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "hint", NullValueHandling = NullValueHandling.Ignore)]
        public string Hint { get; set; }

        [JsonProperty(PropertyName = "metaData", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Metadata { get; set; }

        [JsonProperty(PropertyName = "origin", NullValueHandling = NullValueHandling.Ignore)]
        public string Origin { get; set; }

        [JsonProperty(PropertyName = "severity", NullValueHandling = NullValueHandling.Ignore)]
        public MessageSeverity Severity { get; set; }

        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "ts", Required = Required.Always)]
        public DateTime Timestamp { get; set; }

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public MessageType Type { get; set; }

        [JsonConstructor]
        public Message(string code)
        {
            this.Code = code;
            this.Timestamp = DateTime.Now;
        }
    }

    public sealed class MessagePayload
    {
        public const string default_Contentspec = "urn:spec://eclipse.org/unide/measurement-message#v2";

        [JsonProperty(PropertyName = "content-spec")]
        public string Contentspec { get; set; }

        [JsonProperty(PropertyName = "device")]
        public Device Device { get; set; }

        public IList<Message> Messages { get; set; }

        public MessagePayload()
        {
            this.Contentspec = default_Contentspec;
        }
    }
}

