using System;
using Commentor.GivEtPraj.Application.Common.Interfaces;
using Commentor.GivEtPraj.Infrastructure.Compression;
using Commentor.GivEtPraj.Infrastructure.Storage;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Commentor.GivEtPraj.Infrastructure;

public static class DependencyInjection
{
    private static readonly ILoggerFactory EfLoggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            if (env.IsDevelopment())
            {
                options.UseLoggerFactory(EfLoggerFactory);
            }
        });
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddSingleton<IFileStorage, AzureBlobFileStorage>();
        services.AddSingleton<IImageStorage, ImageStorage>();
        services.AddSingleton<IImageCompression, BitmapImageCompression>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<IDeviceService, DeviceService>();

        return services;
    }
}

public class DiceRollFilter
{
    public string Name { get; }
    public int Amount { get; }
    public bool Inclusive { get; }

    public static DiceRollFilter CreateKeep(int amount) => new("Keep", amount, true);
    public static DiceRollFilter CreateDrop(int amount) => new("Drop", amount, false);
    
    private DiceRollFilter(string name, int amount, bool inclusive)
    {
        Name = name;
        Amount = amount;
        Inclusive = inclusive;
    }
}

public class DiceRoll
{
    private int Rolls { get; }
    private int Sides { get; }
    private DiceRollFilter? Filter { get; }
    
    private DiceRoll(int rolls, int sides, DiceRollFilter? filter = null)
    {
        Rolls = rolls;
        Sides = sides;
        Filter = filter;
    }

    private static DiceRoll Create(int rolls, int sides, DiceRollFilter? filter = null)
    {
        if (rolls <= 0) throw new ArgumentOutOfRangeException(nameof(rolls), $"You need at least 1 roll");
        if (sides <= 0) throw new ArgumentOutOfRangeException(nameof(sides), $"You need at least 1 side");
        if (filter is not null) ValidateFiltering(filter, sides);

        return new(rolls, sides, filter);
    }

    private static void ValidateFiltering(DiceRollFilter filter, int sides)
    {
        switch (filter.Inclusive)
        {
            case true when filter.Amount >= sides:
            case false when filter.Amount > sides:
                break;
            default:
                throw new ArgumentException(nameof(filter.Amount), $"{filter.Amount} should be higher than {sides}");
        }
    }
}