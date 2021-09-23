using Commentor.GivEtPraj.Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Commentor.GivEtPraj.Infrastructure
{
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
                options.UseSqlServer();
                options.UseLoggerFactory(EfLoggerFactory);
            });
            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>()!);

            return services;
        }
    }
}