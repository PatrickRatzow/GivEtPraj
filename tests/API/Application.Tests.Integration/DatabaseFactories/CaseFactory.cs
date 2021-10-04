using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;

public class CaseFactory : DatabaseFactory
{
    private static readonly object CreationLock = new();
    private static int _casesCreated = 0;

    public CaseFactory(IServiceScope serviceScope) : base(serviceScope)
    {
    }

    public Case Create(Category category, string? title = null, string? description = null)
    {
        lock (CreationLock)
        {
            return CreateCase(category, title, null);
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

    private Case CreateCase(Category category, string? title = null, string? description = null)
    {
        _casesCreated++;

        title ??= $"Case #{_casesCreated}";
        description ??= $"Description #{_casesCreated}";

        return Add(new Case
        {
            Title = title,
            Description = description,
            Category = category
        });
    }
}