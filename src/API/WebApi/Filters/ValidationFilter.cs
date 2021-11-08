using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using Commentor.GivEtPraj.Application.Common.Security;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Commentor.GivEtPraj.WebApi.Filters;

public static class ValidationFilter
{
    public static void UseFluentValidationExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(x =>
        {
            x.Run(async context =>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = errorFeature?.Error;
                if (exception is null) return;

                if (exception is not ValidationException validationException) throw exception;

                var errors = validationException.Errors.Select(err => new
                {
                    err.PropertyName,
                    err.ErrorMessage
                });

                var errorText = JsonSerializer.Serialize(new
                {
                    Errors = errors
                });
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(errorText, Encoding.UTF8);
            });
        });
    }

    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(x =>
        {
            x.Run(async context =>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = errorFeature?.Error;
                if (exception is null) return;

                context.Response.StatusCode = exception switch
                {
                    ForbiddenAccessException => (int)HttpStatusCode.Forbidden,
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    _ => throw exception
                };
                await context.Response.CompleteAsync();
            });
        });
    }
}