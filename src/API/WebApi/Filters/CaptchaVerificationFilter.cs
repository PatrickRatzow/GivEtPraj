using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Commentor.GivEtPraj.WebApi.Filters;
public class CaptchaVerificationFilter : Attribute, IAsyncAuthorizationFilter
{

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        HttpClient Http = new HttpClient();

        var reCaptchaResponse = context.HttpContext.Request.Headers["X-ReCaptchaResponse"];
        HttpResponseMessage httpResp = await Http.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6LcvD64cAAAAAAqiYQuOfEcWQmrHUxct_B0vldqn&response=" + reCaptchaResponse, null);
        ReCaptchaResponse resp = JsonSerializer.Deserialize<ReCaptchaResponse>(await httpResp.Content.ReadAsStringAsync());

        if (!resp.Success)
            context.Result = new UnauthorizedObjectResult($"No a valid ReCaptcha response");
    }

    public class ReCaptchaResponse
    {
        [JsonPropertyName("success")] public bool Success { get; set; }
        [JsonPropertyName("challenge_ts")] public DateTime ChallengeTs { get; set; }
        [JsonPropertyName("hostName")] public string HostName { get; set; }

    }
}
