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
            var language = context.Request.Headers["X-Language"];
            if (string.IsNullOrEmpty(language))
            {
                languageService.Language = Language.DK;

                await next();

                return;
            }

            languageService.Language = language.ToString() switch
            {
                "da" => Language.DK,
                "en" => Language.EN,
                _ => Language.DK,
            };
            await next();
        });
    }
}