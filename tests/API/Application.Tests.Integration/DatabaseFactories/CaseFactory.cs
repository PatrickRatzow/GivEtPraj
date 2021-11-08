using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;

public class CaseFactory : DatabaseFactory
{
    private static readonly object CreationLock = new();
    private static int _created;

    public CaseFactory(IServiceScope serviceScope) : base(serviceScope)
    {
    }

    protected override int Created
    {
        get => _created;
        set => _created = value;
    }

    public BaseCase Create(Category category, string? description = null, Priority? priority = null,
        IPAddress? ipAddress = null, GeographicLocation? location = null, Guid? deviceId = null)
    {
        lock (CreationLock)
        {
            return CreateCase(category, description, priority, ipAddress, location, deviceId);
        }
    }

    public List<BaseCase> CreateMany(Category category, int amount, Guid? deviceId = null)
    {
        lock (CreationLock)
        {
            return Enumerable.Range(0, amount)
                .Select(x => CreateCase(category, deviceId: deviceId))
                .ToList();
        }
    }

    private BaseCase CreateCase(Category category, string? description = null, Priority? priority = null,
        IPAddress? ipAddress = null, GeographicLocation? location = null, Guid? deviceId = null)
    {
        Created++;

        description ??= $"Description #{Created}";
        ipAddress ??= IPAddress.Parse("127.0.0.1");
        priority ??= Priority.Low;
        location ??= GeographicLocation.From(0, 0);
        deviceId ??= Guid.NewGuid();

        return Add(new Case
        {
            Comment = description,
            Category = category,
            Priority = (Priority)priority,
            IpAddress = ipAddress,
            GeographicLocation = location,
            DeviceId = deviceId.Value
        });
    }
}