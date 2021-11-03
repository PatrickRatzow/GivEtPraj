using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Common.Behaviors;
using Commentor.GivEtPraj.Application.Common.Security;
using Commentor.GivEtPraj.WebApi;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace Commentor.GivEtPraj.Application.Tests.Integration;

[SetUpFixture]
public class Testing
{
    private static IConfigurationRoot _configuration;
    private static IServiceScopeFactory _scopeFactory;
    private static Checkpoint _checkpoint;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        var path = Directory.GetCurrentDirectory();
        var builder = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", false, true)
            .AddEnvironmentVariables();

        _configuration = builder.Build();

        var hostEnvironment = Mock.Of<IWebHostEnvironment>(w =>
            w.EnvironmentName == "Development" &&
            w.ApplicationName == "Commentor.GivEtPraj.WebApi");
        var startup = new Startup(_configuration, hostEnvironment);
        var services = new ServiceCollection();

        services.AddSingleton<IConfiguration>(_configuration);
        services.AddSingleton(hostEnvironment);
        
        services.AddLogging();

        startup.ConfigureServices(services);

        var reCaptchaBehavior = services.First(sp => sp.ImplementationType == typeof(ReCaptchaBehavior<,>));
        services.Remove(reCaptchaBehavior);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TestReCaptchaBehavior<,>));
        
        _scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

        _checkpoint = new()
        {
            SchemasToInclude = new[]
            {
                "dbo"
            },
            TablesToIgnore = new[]
            {
                "__EFMigrationsHistory"
            },
            DbAdapter = DbAdapter.SqlServer
        };

        EnsureDatabase();
    }

    private static void EnsureDatabase()
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.Migrate();
    }

    public static IServiceScope CreateScope() => _scopeFactory.CreateScope();

    public static async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task ResetState()
    {
        await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));
    }

    public static async Task<TEntity?> Find<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static DatabaseSetup SetupDatabase()
    {
        return new(CreateScope());
    }

    public static async Task<TEntity> Add<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var entityEntry = await context.AddAsync(entity);

        await context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public static async Task<int> Update<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Update(entity);

        return await context.SaveChangesAsync();
    }

    public static async Task AddRange(params object[] entities)
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.AddRangeAsync(entities);

        await context.SaveChangesAsync();
    }

    public static async Task<int> Count<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.Set<TEntity>()
            .CountAsync();
    }

    public static async Task<int> Count<TEntity>(Expression<Func<TEntity, bool>> predicate)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.Set<TEntity>()
            .CountAsync(predicate);
    }


    public static async Task<List<TEntity>> Search<TEntity>(Expression<Func<TEntity, bool>> predicate)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.Set<TEntity>()
            .Where(predicate)
            .ToListAsync();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
    
    private class TestReCaptchaBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            return await next();
        }
    }
}