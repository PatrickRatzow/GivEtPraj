using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;

public class SubCategoryFactory : DatabaseFactory
{
    private static readonly object CreationLock = new();
    private static int _created;
    protected override int Created
    {
        get => _created; 
        set => _created = value;
    }
    
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
        Created++;

        name ??= $"SubCategory #{Created}";

        return Add(new SubCategory
        {
            Name = name,
            Category = category
        });
    }
}