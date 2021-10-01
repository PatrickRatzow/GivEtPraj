using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;

public abstract class DatabaseFactory
{
    protected readonly IServiceScope ServiceScope;
        
    protected DatabaseFactory(IServiceScope serviceScope)
    {
        ServiceScope = serviceScope;
    }

    private AppDbContext Context
        => ServiceScope.ServiceProvider.GetRequiredService<AppDbContext>();

    protected TEntity Add<TEntity>(TEntity entity) where TEntity : class
    {
        return Context.Add(entity).Entity;
    }
}