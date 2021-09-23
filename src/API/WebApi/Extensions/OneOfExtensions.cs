using System;
using Commentor.GivEtPraj.Domain.Errors.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Commentor.GivEtPraj.WebApi.Extensions
{
   public static partial class OneOfExtensions
    {
        private static IActionResult MatchErrorResponse<TResponse>(TResponse response) =>
            response switch
            {
                INotFoundError notFound =>
                    GetResult(typeof(NotFoundResult), typeof(NotFoundObjectResult), notFound),
                /*
                IValidationError validationError =>
                    GetResult(typeof(BadRequestResult), typeof(BadRequestObjectResult), validationError),
                IAlreadyExistsError alreadyExists =>
                    GetResult(typeof(ConflictResult), typeof(ConflictObjectResult), alreadyExists),
                IPermissionsError permissionsError =>
                    permissionsError.ErrorMessage is null
                        ? new StatusCodeResult(StatusCodes.Status403Forbidden)
                        : new ObjectResult(new GenericError(permissionsError.ErrorMessage))
                        {
                            StatusCode = StatusCodes.Status403Forbidden
                        },
                IAuthorizationError authorizationError =>
                    GetResult(typeof(UnauthorizedResult), typeof(UnauthorizedObjectResult), authorizationError),
                */
                IError error =>
                    throw new ArgumentException($"Unable to find an error handler for {error.GetType().Name}"),
                Unit unit => new NoContentResult(),
                var data =>
                    GetResult(typeof(OkResult), typeof(OkObjectResult), data)
            };

        private static IActionResult GetResult<TData>(Type status, Type @object, TData data)
        {
            object? msg = data;
            if (data is IError err)
            {
                msg = err.ErrorMessage is not null ? new { Error = err.ErrorMessage } : err.ErrorMessage;
            }

            var result = msg is null
                ? Activator.CreateInstance(status) as IActionResult
                : Activator.CreateInstance(@object, msg) as IActionResult;

            return result!;
        }
    }
}