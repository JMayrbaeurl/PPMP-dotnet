using System;

namespace Microsoft.IoT.PMPP.Machine
{
    public const string default_Contentspec = "urn:spec://eclipse.org/unide/measurement-message#v2";

    public sealed class MessagePayload
    {
        public string Contentspec { get; set; }

        public MessagePayload()
        {
            this.Contentspec = default_Contentspec;
        }
    }
}

