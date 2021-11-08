using System.Reflection;
using Commentor.GivEtPraj.Application.Common.Behaviors;
using Commentor.GivEtPraj.Application.Common.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.Remove(services.First(sp => sp.ServiceType == typeof(IMapper)));
        services.AddScoped<IMapper>(sp =>
            new LocalizedMapper(
                sp.GetRequiredService<IConfigurationProvider>(),
                sp.GetService!,
                sp.GetRequiredService<ILanguageService>()
            )
        );

        services.AddFluentValidation();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ReCaptchaBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}