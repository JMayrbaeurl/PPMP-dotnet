using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IoT.PPMP.Machine
{
    public class MessagePayloadFactory
    {
        public MessagePayload CreateSimpleMessage(string deviceID, string code)
        {
            if (String.IsNullOrEmpty(deviceID))
                throw new ArgumentException("Parameter 'deviceID' must not be null or empty");

            if (String.IsNullOrEmpty(code))
                throw new ArgumentException("Parameter 'code' must not be null or empty");

            MessagePayload result = new MessagePayload(deviceID);
            result.Messages.Add(new Message(code));

            return result;
        }

        public MessagePayload CreateSimpleMessage(string deviceID, string code, DateTime timeStamp)
        {
            MessagePayload result = this.CreateSimpleMessage(deviceID, code);
            result.Messages[0].Timestamp = timeStamp;

            return result;
        }
    }
}
