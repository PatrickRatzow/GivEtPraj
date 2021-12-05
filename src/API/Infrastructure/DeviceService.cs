using System;
using Commentor.GivEtPraj.Application.Common.Interfaces;

namespace Commentor.GivEtPraj.Infrastructure;

public class DeviceService : IDeviceService
{
    public Guid DeviceIdentifier { get; set; }
}