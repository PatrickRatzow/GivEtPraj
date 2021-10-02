using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;

public class DatabaseSetup : IDisposable
{
    private readonly IServiceScope _serviceScope;

    public DatabaseSetup(IServiceScope serviceScope)
    {
        _serviceScope = serviceScope;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public TFactory Factory<TFactory>() where TFactory : DatabaseFactory
    {
        var constructor = typeof(TFactory).GetConstructors()[0].Invoke(new object?[]
        {
            _serviceScope
        });

        return (TFactory)constructor;
    }

    public async Task Save()
    {
        var context = _serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.SaveChangesAsync();
    }
}