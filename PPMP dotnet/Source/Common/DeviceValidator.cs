using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.IoT.PPMP
{
    public class DeviceValidator : AbstractValidator<Device>
    {
        public DeviceValidator()
        {
            RuleFor(device => device.DeviceID).NotNull().NotEmpty();
        }
    }
}
