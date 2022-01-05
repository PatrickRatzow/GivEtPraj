using System;
using System.Collections.Generic;
using System.Linq;
using Commentor.GivEtPraj.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;

public class CategoryFactory : DatabaseFactory
{
    private static readonly object CreationLock = new();
    private static int _created;

    public CategoryFactory(IServiceScope serviceScope) : base(serviceScope)
    {
    }

    protected override int Created
    {
        get => _created;
        set => _created = value;
    }

    public Category Create(Guid? id = null, string? name = null, string? icon = null, bool miscellaneous = false)
    {
        lock (CreationLock)
        {
            return CreateCategory(id, name, icon, miscellaneous);
        }
    }

    public List<Category> CreateMany(int amount)
    {
        lock (CreationLock)
        {
            return Enumerable.Range(0, amount)
                .Select(x => CreateCategory(Guid.NewGuid()))
                .ToList();
        }
    }

    private Category CreateCategory(Guid? id = null, string? name = null, string? icon = null, bool miscellaneous = false)
    {
        Created++;

        id ??= Guid.NewGuid();
        name ??= $"Category #{Created}";
        icon ??= "fas fa-road";

        return Add(
            new Category(
                id.Value, 
                LocalizedString.From(name, name), 
                icon, 
                miscellaneous, 
                new List<BaseCase>(), 
                new List<SubCategory>()
            )
        );
    }
}