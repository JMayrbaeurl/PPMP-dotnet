// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.IoT.PPMP.Measurement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PPMP_dotnet_unittests
{
    [TestClass]
    public class MeasurementMessageTests
    {
        [TestMethod]
        public void TestDynamicFieldvalueNamesReading()
        {
            string json = @"{
                ""$_time"": [ 0, 23, 24 ],
                ""temperature"": [ 45.4231, 46.4222, 44.2432 ] }";

            Series aSeries = JsonConvert.DeserializeObject<Series>(json);
            Assert.AreEqual(23, aSeries.Time[1]);
            Assert.AreEqual("temperature", aSeries.Fieldname);
        }

        [TestMethod]
        public void TestDynamicFieldvalueNamesWriting()
        {
            Series series = new Series();
            series.Time = new List<long>() { 1, 2, 3 };
            series.Fieldname = "temperature";
            series.Fieldvalues = new List<float>() { (float)1.5, (float)2.5, (float)3.5 };

            string json = JsonConvert.SerializeObject(series);
            Assert.IsTrue(json.IndexOf("temperature") != -1);
        }

        [TestMethod]
        public void TestSampleMachineMessageFromSpec()
        {
            MeasurementPayload msg = JsonConvert.DeserializeObject<MeasurementPayload>(File.ReadAllText(@".\Testfiles\simplemeasurementmessage.json"));
            Assert.AreEqual("a4927dad-58d4-4580-b460-79cefd56775b", msg.Device.DeviceID);
        }
    }
}
