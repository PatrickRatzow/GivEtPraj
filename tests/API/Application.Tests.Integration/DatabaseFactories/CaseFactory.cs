using System.Collections.Generic;
using System.Linq;
using System.Net;
using Commentor.GivEtPraj.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;

public class CaseFactory : DatabaseFactory
{
    private static readonly object CreationLock = new();
    private static int _casesCreated = 0;

    public CaseFactory(IServiceScope serviceScope) : base(serviceScope)
    {
    }

    public Case Create(Category category,  string? description = null, Priority? priority = null, IPAddress? ipAddress = null )
    {
        lock (CreationLock)
        {
            return CreateCase(category, description, priority, ipAddress);
        }
    }

    public List<Case> CreateMany(Category category, int amount)
    {
        lock (CreationLock)
        {
            return Enumerable.Range(0, amount)
                .Select(x => CreateCase(category))
                .ToList();
        }
    }

    private Case CreateCase(Category category, string? description = null, Priority? priority = null, IPAddress? ipAddress = null)
    {
        _casesCreated++;

        description ??= $"Description #{_casesCreated}";
        ipAddress ??= IPAddress.Parse("127.0.0.1");
        priority ??= Priority.Low;

        return Add(new Case
        {           
            Description = description,
            Category = category,
            Priority = (Priority)priority,
            IpAddress = ipAddress
        });
    }
}