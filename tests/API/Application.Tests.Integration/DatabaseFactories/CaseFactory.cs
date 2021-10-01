using System.Collections.Generic;
using System.Linq;
using Commentor.GivEtPraj.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;

public class CaseFactory : DatabaseFactory
{
    private static readonly object CreationLock = new();
    private static int _casesCreated = 0;

    public CaseFactory(IServiceScope serviceScope) : base(serviceScope)
    {
    }

    public Case Create(string? title = null, string? description = null)
    {
        lock (CreationLock)
        {
            return CreateCase(title, null);
        }
    }

    public List<Case> CreateMany(int amount)
    {
        lock (CreationLock)
        {
            return Enumerable.Range(0, amount)
                .Select(x => CreateCase())
                .ToList();
        }
    }
    
    private Case CreateCase(string? title = null, string? description = null)
    {
        _casesCreated++;
        
        title ??= $"Case #{_casesCreated}";
        description ??= $"Description #{_casesCreated}";

        return Add(new Case
        {
            Title = title,
            Description = description
        });
    }
}