using Microsoft.IoT.PPMP.Machine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;

namespace PPMP_dotnet_unittests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBasicMachineMessage()
        {
            string json = @"{
                ""device"" : {
                    ""deviceID"" : ""numberOneDevice01""
                }
            }";
            MessagePayload machineMessage = JsonConvert.DeserializeObject<MessagePayload>(json);
            Assert.AreEqual("numberOneDevice01", machineMessage.Device.DeviceID);
            Assert.AreEqual(MessagePayload.default_Contentspec, machineMessage.Contentspec);
        }

        [TestMethod]
        public void TestMachineMessageWithMetadata()
        {
            string json = @"{
                ""device"" : {
                    ""deviceID"" : ""numberOneDevice02"",
                    ""metaData"" : { ""first"" : ""firstvalue"", ""second"" : ""secondvalue"" }
                }
            }";
            MessagePayload machineMessage = JsonConvert.DeserializeObject<MessagePayload>(json);
            Assert.AreEqual("numberOneDevice02", machineMessage.Device.DeviceID);
            Assert.AreEqual("secondvalue", machineMessage.Device.Metadata["second"]);
        }

        [TestMethod]
        public void TestEmptyMachineMessageToJson()
        {
            Message msg = new Message("firstmessage");
            Console.WriteLine(JsonConvert.SerializeObject(msg));
        }

        [TestMethod]
        public void TestSeverityInMachineMessage()
        {
            string json = @"{
                ""code"" : ""anycode"",
                ""ts"" : ""2018-02-08T17:29:51.120Z"",
                ""severity"" : ""HIGH""
            }";

            Message msg = JsonConvert.DeserializeObject<Message>(json);
            Assert.AreEqual(MessageSeverity.HIGH, msg.Severity);
        }

        [TestMethod]
        public void TestSampleMachineMessageFromSpec()
        {
            MessagePayload msg = JsonConvert.DeserializeObject<MessagePayload>(File.ReadAllText(@".\Testfiles\simplemachinemessage.json"));
            Assert.AreEqual("a4927dad-58d4-4580-b460-79cefd56775b", msg.Device.DeviceID);
        }
    }
}
