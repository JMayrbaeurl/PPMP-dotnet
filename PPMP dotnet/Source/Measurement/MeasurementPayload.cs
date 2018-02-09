// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.IoT.PPMP.Measurement
{
    public enum MeasurementResult { OK, NOK, UNKNOWN }

    public sealed class MeasurementPayload : DeviceMessage
    {
        [JsonProperty(PropertyName = "content-spec")]
        public string Contentspec { get; set; }

        [JsonProperty(PropertyName = "device")]
        public Device Device { get; set; }

        [JsonProperty(PropertyName = "part", NullValueHandling = NullValueHandling.Ignore)]
        public Part Part { get; set; }

        [JsonProperty(PropertyName = "measurements", Required = Required.Always)]
        public List<Measurement> Measurements { get; set; }

        public string DeviceID()
        {
            return this.Device != null ? this.Device.DeviceID : null;
        }
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
        public MeasurementResult Result { get; }
    }

    public sealed class Limits
    {
        [JsonProperty(PropertyName = "lowerError", NullValueHandling = NullValueHandling.Ignore)]
        public float LowerError { get; set; }

        [JsonProperty(PropertyName = "lowerWarn", NullValueHandling = NullValueHandling.Ignore)]
        public float LowerWarn { get; set; }

        [JsonProperty(PropertyName = "target", NullValueHandling = NullValueHandling.Ignore)]
        public float Target { get; set; }

        [JsonProperty(PropertyName = "upperError", NullValueHandling = NullValueHandling.Ignore)]
        public float UpperError { get; set; }

        [JsonProperty(PropertyName = "upperWarn", NullValueHandling = NullValueHandling.Ignore)]
        public float UpperWarn { get; set; }
    }

    [JsonConverter(typeof(SeriesJsonConverter))]
    public sealed class Series
    {
        [JsonProperty(PropertyName = "$_time", Required = Required.Always)]
        public List<Int64> Time { get; set; }

        public string Fieldname { get; set; }

        public bool ShouldSerializeFieldname() { return false;  }

        public List<float> Fieldvalues { get; set; }
    }

    class SeriesJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Series));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties().ToList();

            Series result = new Series();
            foreach(JProperty prop in properties)
            {
                if (prop.Name.Equals("$_time"))
                {
                    result.Time = new List<long>();
                    foreach (var singleValue in prop.Values())
                        result.Time.Add(singleValue.Value<long>());
                } else
                {
                    result.Fieldname = prop.Name;
                    result.Fieldvalues = new List<float>();
                    foreach (var singleValue in prop.Values())
                        result.Fieldvalues.Add(singleValue.Value<float>());
                }
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Series series = value as Series;

            writer.WriteStartObject();

            writer.WritePropertyName("$_time");
            writer.WriteStartArray();
            foreach (long timeEntry in series.Time)
                serializer.Serialize(writer, timeEntry);
            writer.WriteEndArray();

            writer.WritePropertyName(series.Fieldname);
            writer.WriteStartArray();
            foreach (float values in series.Fieldvalues)
                serializer.Serialize(writer, values);
            writer.WriteEndArray();

            writer.WriteEndObject();
        }
    }


    public sealed class Measurement
    {
        [JsonProperty(PropertyName = "code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; }

        [JsonProperty(PropertyName = "limits", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, Limits> Limits { get; set; }

        [JsonProperty(PropertyName = "result", NullValueHandling = NullValueHandling.Ignore)]
        public MeasurementResult Result { get; set; }

        [JsonProperty(PropertyName = "ts", Required = Required.Always)]
        public DateTime Timestamp { get; set; }

        [JsonProperty(PropertyName = "series", Required = Required.Always)]
        public Series Series { get; set; }
    }
}
