using Microsoft.AspNetCore.Http;

namespace Commentor.GivEtPraj.WebApi.Extensions;

public static class HttpRequestExtensions
{
    public static bool IsRegularMethod(this HttpRequest request)
        => HttpMethods.IsDelete(request.Method)
           || HttpMethods.IsGet(request.Method)
           || HttpMethods.IsPatch(request.Method)
           || HttpMethods.IsPost(request.Method)
           || HttpMethods.IsPut(request.Method);
}