using System;
using Commentor.GivEtPraj.Application.Common.Interfaces;

namespace Commentor.GivEtPraj.Infrastructure;

public class DeviceService : IDeviceService
{
    private Guid? _deviceIdentifier;
    public Guid DeviceIdentifier
    {
        get
        {
            if (_deviceIdentifier is null)
                throw new NullReferenceException(nameof(DeviceIdentifier) + " has not been set");

            return _deviceIdentifier.Value;
        }
        set => _deviceIdentifier = value;
    }
}