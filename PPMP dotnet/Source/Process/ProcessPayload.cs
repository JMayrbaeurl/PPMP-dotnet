using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IoT.PPMP.Process
{
    public enum ProcessResult { OK, NOK, UNKNOWN }

    public enum PartType { SINGLE, BATCH }

    public sealed class ProcessPayload
    {
        [JsonProperty(PropertyName = "content-spec")]
        public string Contentspec { get; set; }

        [JsonProperty(PropertyName = "device")]
        public Device Device { get; set; }

        [JsonProperty(PropertyName = "part", NullValueHandling = NullValueHandling.Ignore)]
        public Part Part { get; set; }
    }

    public sealed class Part
    {
        [JsonProperty(PropertyName = "code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; }

        [JsonProperty(PropertyName = "metaData", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Metadata { get; set; }

        [JsonProperty(PropertyName = "partID", NullValueHandling = NullValueHandling.Ignore)]
        public string PartID { get; }

        [JsonProperty(PropertyName = "partTypeID", NullValueHandling = NullValueHandling.Ignore)]
        public string PartTypeID { get; }

        [JsonProperty(PropertyName = "result", NullValueHandling = NullValueHandling.Ignore)]
        public ProcessResult Result { get; }

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public PartType PartType { get; }
    }

    public sealed class Process
    {

    }
}
