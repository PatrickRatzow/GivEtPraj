using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;

public class CategoryFactory : DatabaseFactory
{
    private static readonly object CreationLock = new();
    private static int _categoriesCreated = 0;

    public CategoryFactory(IServiceScope serviceScope) : base(serviceScope)
    {
    }

    public Category Create(string? name = null)
    {
        lock (CreationLock)
        {
            return CreateCategory(name);
        }
    }

    public List<Category> CreateMany(int amount)
    {
        lock (CreationLock)
        {
            return Enumerable.Range(0, amount)
                .Select(x => CreateCategory())
                .ToList();
        }
    }

    private Category CreateCategory(string? name = null)
    {
        _categoriesCreated++;

        name ??= $"Category #{_categoriesCreated}";

        return Add(new Category
        {
            Name = name
        });
    }
}