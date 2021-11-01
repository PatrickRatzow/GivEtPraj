using Commentor.GivEtPraj.Application.Common.Interfaces;
using Commentor.GivEtPraj.Application.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Commentor.GivEtPraj.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseLanguageService(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            var languageService = context.RequestServices.GetRequiredService<ILanguageService>();
            languageService.Language = Language.EN;

            await next();
        });
    }
}