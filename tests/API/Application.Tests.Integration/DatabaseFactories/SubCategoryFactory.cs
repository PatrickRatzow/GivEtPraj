using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;

public class SubCategoryFactory : DatabaseFactory
{
    private static readonly object CreationLock = new();
    private static int _subCategoriesCreated = 0;

    public SubCategoryFactory(IServiceScope serviceScope) : base(serviceScope)
    {
    }

    public SubCategory Create(Category category, string? name = null)
    {
        lock (CreationLock)
        {
            return CreateSubcategory(category, name);
        }
    }

    public List<SubCategory> CreateMany(Category category, int amount)
    {
        lock (CreationLock)
        {
            return Enumerable.Range(0, amount)
                .Select(x => CreateSubcategory(category))
                .ToList();
        }
    }

    private SubCategory CreateSubcategory(Category category, string? name = null)
    {
        _subCategoriesCreated++;

        name ??= $"SubCategory #{_subCategoriesCreated}";

        return Add(new SubCategory
        {
            Name = name,
            Category = category
        });
    }
}