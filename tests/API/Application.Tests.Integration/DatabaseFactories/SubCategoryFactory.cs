using System;
using System.Collections.Generic;
using System.Linq;
using Commentor.GivEtPraj.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;

public class SubCategoryFactory : DatabaseFactory
{
    private static readonly object CreationLock = new();
    private static int _created;

    public SubCategoryFactory(IServiceScope serviceScope) : base(serviceScope)
    {
    }

    protected override int Created
    {
        get => _created;
        set => _created = value;
    }

    public SubCategory Create(Guid id, Category category, string? name = null)
    {
        lock (CreationLock)
        {
            return CreateSubcategory(id, category, name);
        }
    }

    public List<SubCategory> CreateMany(Category category, int amount)
    {
        lock (CreationLock)
        {
            return Enumerable.Range(0, amount)
                .Select(x => CreateSubcategory(Guid.NewGuid(), category))
                .ToList();
        }
    }

    private SubCategory CreateSubcategory(Guid id, Category category, string? name = null)
    {
        Created++;

        string str = $"SubCategory #{Created}";
        LocalizedString subName = LocalizedString.From(str, str);

        return Add(new SubCategory(id, subName, category, new List<Case>()));
    }
}