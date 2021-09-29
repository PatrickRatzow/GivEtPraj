using System.Reflection;
using Commentor.GivEtPraj.Application.Common.Behaviors;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddFluentValidation();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}