using FluentValidation.Results;
using Microsoft.IoT.PPMP.Machine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;

namespace PPMP_dotnet_unittestss
{
    [TestClass]
    public class MachineMessageValidationTests
    {

        [TestMethod]
        public void TestBasicMessagePayloadValidation()
        {
            MessagePayload msg = new MessagePayload();
            MessagePayloadValidator validator = new MessagePayloadValidator();

            ValidationResult results = validator.Validate(msg);


            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }

            Assert.IsFalse(results.IsValid);
        }

        [TestMethod]
        public void TestEmptyMessagePayloadValidation()
        {
            MessagePayload msg = new MessagePayload("firstDevice");
            MessagePayloadValidator validator = new MessagePayloadValidator();

            ValidationResult results = validator.Validate(msg);


            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }

            Assert.IsFalse(results.IsValid);
        }

        [TestMethod]
        public void TestSimpleMessagePayloadValidation()
        {
            MessagePayloadValidator validator = new MessagePayloadValidator();

            ValidationResult results = validator.Validate(
                new MessagePayloadFactory().CreateSimpleMessage("firstDevice", "errorCode001"));


            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }

            Assert.IsTrue(results.IsValid);
        }

        [TestMethod]
        public void TestSimpleMessageFromSpec()
        {
            MessagePayload msg = JsonConvert.DeserializeObject<MessagePayload>(File.ReadAllText(@".\Testfiles\simplemachinemessage.json"));

            MessagePayloadValidator validator = new MessagePayloadValidator();
            ValidationResult results = validator.Validate(msg);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }

            Assert.IsTrue(results.IsValid);
        }
    }
}
