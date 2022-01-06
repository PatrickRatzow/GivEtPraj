using System.CodeDom.Compiler;
using OneOf;

namespace Commentor.GivEtPraj.WebApi.Extensions;

public static class OneOfExtensions
{
    private static IActionResult MatchErrorResponse<TResponse>(TResponse response) =>
        response switch
        {
            INotFoundError => GetResult<NotFoundResult, NotFoundObjectResult>(response),
            IValidationError => GetResult<BadRequestResult, BadRequestObjectResult>(response),
            IAlreadyExistsError => GetResult<ConflictResult, ConflictObjectResult>(response),
            IError => throw new ArgumentException($"Unable to find an error handler for {response.GetType().Name}"),
            Unit => new NoContentResult(),
            _ => GetResult<OkResult, OkObjectResult>(response)
        };

    private static IActionResult GetResult<TCodeResult, TObjectResult>(object? data) 
        where TCodeResult : StatusCodeResult 
        where TObjectResult : ObjectResult
    {
        var msg = data;
        if (data is IError err)
            msg = err.ErrorMessage is not null
                ? new { Error = err.ErrorMessage }
                : err.ErrorMessage;

        var result = msg is null
            ? Activator.CreateInstance(typeof(TCodeResult)) as IActionResult
            : Activator.CreateInstance(typeof(TObjectResult), msg) as IActionResult;

        return result!;
    }

    // These were automatically generated, just boilerplate overloads
    [GeneratedCode("Commentor.GivEtPraj.WebApi.SourceGenerator", "1.0.0")]
    public static IActionResult MatchResponse<T0>(this OneOf<T0> oneOf, Func<T0, IActionResult>? t0 = null)
    {
        return oneOf.Match(
            t0 ?? MatchErrorResponse
        );
    }

    [GeneratedCode("Commentor.GivEtPraj.WebApi.SourceGenerator", "1.0.0")]
    public static IActionResult MatchResponse<T0, T1>(this OneOf<T0, T1> oneOf, Func<T0, IActionResult>? t0 = null,
        Func<T1, IActionResult>? t1 = null)
    {
        return oneOf.Match(
            t0 ?? MatchErrorResponse,
            t1 ?? MatchErrorResponse
        );
    }

    [GeneratedCode("Commentor.GivEtPraj.WebApi.SourceGenerator", "1.0.0")]
    public static IActionResult MatchResponse<T0, T1, T2>(this OneOf<T0, T1, T2> oneOf,
        Func<T0, IActionResult>? t0 = null, Func<T1, IActionResult>? t1 = null, Func<T2, IActionResult>? t2 = null)
    {
        return oneOf.Match(
            t0 ?? MatchErrorResponse,
            t1 ?? MatchErrorResponse,
            t2 ?? MatchErrorResponse
        );
    }

    [GeneratedCode("Commentor.GivEtPraj.WebApi.SourceGenerator", "1.0.0")]
    public static IActionResult MatchResponse<T0, T1, T2, T3>(this OneOf<T0, T1, T2, T3> oneOf,
        Func<T0, IActionResult>? t0 = null, Func<T1, IActionResult>? t1 = null, Func<T2, IActionResult>? t2 = null,
        Func<T3, IActionResult>? t3 = null)
    {
        return oneOf.Match(
            t0 ?? MatchErrorResponse,
            t1 ?? MatchErrorResponse,
            t2 ?? MatchErrorResponse,
            t3 ?? MatchErrorResponse
        );
    }

    [GeneratedCode("Commentor.GivEtPraj.WebApi.SourceGenerator", "1.0.0")]
    public static IActionResult MatchResponse<T0, T1, T2, T3, T4>(this OneOf<T0, T1, T2, T3, T4> oneOf,
        Func<T0, IActionResult>? t0 = null, Func<T1, IActionResult>? t1 = null, Func<T2, IActionResult>? t2 = null,
        Func<T3, IActionResult>? t3 = null, Func<T4, IActionResult>? t4 = null)
    {
        return oneOf.Match(
            t0 ?? MatchErrorResponse,
            t1 ?? MatchErrorResponse,
            t2 ?? MatchErrorResponse,
            t3 ?? MatchErrorResponse,
            t4 ?? MatchErrorResponse
        );
    }

    [GeneratedCode("Commentor.GivEtPraj.WebApi.SourceGenerator", "1.0.0")]
    public static IActionResult MatchResponse<T0, T1, T2, T3, T4, T5>(this OneOf<T0, T1, T2, T3, T4, T5> oneOf,
        Func<T0, IActionResult>? t0 = null, Func<T1, IActionResult>? t1 = null, Func<T2, IActionResult>? t2 = null,
        Func<T3, IActionResult>? t3 = null, Func<T4, IActionResult>? t4 = null, Func<T5, IActionResult>? t5 = null)
    {
        return oneOf.Match(
            t0 ?? MatchErrorResponse,
            t1 ?? MatchErrorResponse,
            t2 ?? MatchErrorResponse,
            t3 ?? MatchErrorResponse,
            t4 ?? MatchErrorResponse,
            t5 ?? MatchErrorResponse
        );
    }

    [GeneratedCode("Commentor.GivEtPraj.WebApi.SourceGenerator", "1.0.0")]
    public static IActionResult MatchResponse<T0, T1, T2, T3, T4, T5, T6>(this OneOf<T0, T1, T2, T3, T4, T5, T6> oneOf,
        Func<T0, IActionResult>? t0 = null, Func<T1, IActionResult>? t1 = null, Func<T2, IActionResult>? t2 = null,
        Func<T3, IActionResult>? t3 = null, Func<T4, IActionResult>? t4 = null, Func<T5, IActionResult>? t5 = null,
        Func<T6, IActionResult>? t6 = null)
    {
        return oneOf.Match(
            t0 ?? MatchErrorResponse,
            t1 ?? MatchErrorResponse,
            t2 ?? MatchErrorResponse,
            t3 ?? MatchErrorResponse,
            t4 ?? MatchErrorResponse,
            t5 ?? MatchErrorResponse,
            t6 ?? MatchErrorResponse
        );
    }

    [GeneratedCode("Commentor.GivEtPraj.WebApi.SourceGenerator", "1.0.0")]
    public static IActionResult MatchResponse<T0, T1, T2, T3, T4, T5, T6, T7>(
        this OneOf<T0, T1, T2, T3, T4, T5, T6, T7> oneOf, Func<T0, IActionResult>? t0 = null,
        Func<T1, IActionResult>? t1 = null, Func<T2, IActionResult>? t2 = null, Func<T3, IActionResult>? t3 = null,
        Func<T4, IActionResult>? t4 = null, Func<T5, IActionResult>? t5 = null, Func<T6, IActionResult>? t6 = null,
        Func<T7, IActionResult>? t7 = null)
    {
        return oneOf.Match(
            t0 ?? MatchErrorResponse,
            t1 ?? MatchErrorResponse,
            t2 ?? MatchErrorResponse,
            t3 ?? MatchErrorResponse,
            t4 ?? MatchErrorResponse,
            t5 ?? MatchErrorResponse,
            t6 ?? MatchErrorResponse,
            t7 ?? MatchErrorResponse
        );
    }

    [GeneratedCode("Commentor.GivEtPraj.WebApi.SourceGenerator", "1.0.0")]
    public static IActionResult MatchResponse<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
        this OneOf<T0, T1, T2, T3, T4, T5, T6, T7, T8> oneOf, Func<T0, IActionResult>? t0 = null,
        Func<T1, IActionResult>? t1 = null, Func<T2, IActionResult>? t2 = null, Func<T3, IActionResult>? t3 = null,
        Func<T4, IActionResult>? t4 = null, Func<T5, IActionResult>? t5 = null, Func<T6, IActionResult>? t6 = null,
        Func<T7, IActionResult>? t7 = null, Func<T8, IActionResult>? t8 = null)
    {
        return oneOf.Match(
            t0 ?? MatchErrorResponse,
            t1 ?? MatchErrorResponse,
            t2 ?? MatchErrorResponse,
            t3 ?? MatchErrorResponse,
            t4 ?? MatchErrorResponse,
            t5 ?? MatchErrorResponse,
            t6 ?? MatchErrorResponse,
            t7 ?? MatchErrorResponse,
            t8 ?? MatchErrorResponse
        );
    }
}