using System.Net;
using System.Text;
using System.Text.Json;
using Commentor.GivEtPraj.Application.Common.Interfaces;
using Commentor.GivEtPraj.Application.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
            languageService.Language = language.ToString() switch
            {
                "da" => Language.DK,
                "en" => Language.EN,
                _ => Language.EN
            };
            
            await next();
        });
    }

    public static void UseDeviceService(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            if (!context.Request.IsRegularMethod())
            {
                await next();

                return;
            }

            var deviceService = context.RequestServices.GetRequiredService<IDeviceService>();
            var deviceId = context.Request.Headers["X-DeviceId"];
            if (Guid.TryParse(deviceId, out var guid))
            {
                deviceService.DeviceIdentifier = guid;

                await next();

                return;
            }

            var errorText = JsonSerializer.Serialize(new
            {
                Errors = new object[]
                {
                    new { ErrorMessage = "Invalid/missing GUID in the X-DeviceId header" }
                }
            });
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(errorText, Encoding.UTF8);
        });
    }
}