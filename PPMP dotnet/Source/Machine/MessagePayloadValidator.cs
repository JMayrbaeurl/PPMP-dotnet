using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IoT.PPMP.Machine
{
    public class MessagePayloadValidator : AbstractValidator<MessagePayload>
    {
        public MessagePayloadValidator()
        {
            RuleFor(messagePayload => messagePayload.Contentspec).NotNull().NotEmpty();
            RuleFor(messagePayload => messagePayload.Device).NotNull();
            RuleFor(messagePayload => messagePayload.Device).SetValidator(new DeviceValidator());
            RuleFor(messagePayload => messagePayload.Messages).NotNull().NotEmpty();
            RuleFor(messagePayload => messagePayload.Messages).SetCollectionValidator(new MessageValidator());
        }
    }

    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(message => message.Code).NotNull().NotEmpty();
            RuleFor(message => message.Timestamp).NotNull();
        }
    }
}
