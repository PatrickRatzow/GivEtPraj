using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Commentor.GivEtPraj.WebApi.Filters;

public class ReCaptcha : Attribute, IAsyncAuthorizationFilter
{
    private readonly float _minimumScore;
    private ILogger<ReCaptcha>? _logger;
    private ReCaptchaSecrets? _secrets;

    public ReCaptcha(float minimumScore = 0.6f)
    {
        _minimumScore = minimumScore;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            FetchSecretsFromConfig(context);

            using var httpClient = new HttpClient();

            var hadV3 = false;
            var hadV2 = false;

            hadV3 = await VerifyV3(httpClient, context);
            if (!hadV3)
                hadV2 = await VerifyV2(httpClient, context);


            if (context.Result is not null || hadV3 || hadV2) return;

            // If neither V3 or V2 could find data to work with, clearly it's not included in the response
            context.Result = new UnauthorizedResult();
        }
        catch
        {
            context.Result = new ForbidResult();
        }
    }

    private void FetchSecretsFromConfig(AuthorizationFilterContext context)
    {
        _logger ??= context.HttpContext.RequestServices.GetRequiredService<ILogger<ReCaptcha>>();
        
        if (_secrets is not null) return;

        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

        _secrets = new(configuration["ReCaptcha:V3"], configuration["ReCaptcha:V2"]);
    }

    private async Task<bool> VerifyV3(HttpClient httpClient, AuthorizationFilterContext context)
    {
        var userInput = context.HttpContext.Request.Headers["X-ReCAPTCHA-V3"];
        if (string.IsNullOrEmpty(userInput)) return false;

        var resp = await httpClient.PostAsync(
            $"https://www.google.com/recaptcha/api/siteverify?secret={_secrets!.V3}&response={userInput}", null);
        var content = await resp.Content.ReadAsStringAsync();
        var captcha = JsonSerializer.Deserialize<ReCaptchaV3Response>(content);

        _logger?.LogInformation("ReCAPTCHA V3 score was {CaptchaScore} and it had to be at least {MinimumScore}", 
            captcha?.Score ?? 0f, _minimumScore);
        
        const float scoreEpsilon = 0.01f;
        if (captcha?.Score < _minimumScore - scoreEpsilon)
            context.Result = new ForbidResult();

        return true;
    }

    private async Task<bool> VerifyV2(HttpClient httpClient, AuthorizationFilterContext context)
    {
        var userInput = context.HttpContext.Request.Headers["X-ReCAPTCHA-V2"];
        if (string.IsNullOrEmpty(userInput)) return false;

        var resp = await httpClient.PostAsync(
            $"https://www.google.com/recaptcha/api/siteverify?secret={_secrets!.V2}&response={userInput}", null);
        var content = await resp.Content.ReadAsStringAsync();
        var captcha = JsonSerializer.Deserialize<ReCaptchaV2Response>(content);

        _logger?.LogInformation("ReCAPTCHA V2 result: {CaptchaResult}", captcha!.Success);
        
        if (!captcha!.Success)
            context.Result = new ForbidResult();

        return true;
    }

    private record ReCaptchaSecrets(string V3, string V2);

    private class ReCaptchaV3Response
    {
        [JsonPropertyName("success")] public bool Success { get; set; }

        [JsonPropertyName("score")] public float Score { get; set; }

        [JsonPropertyName("action")] public string Action { get; set; } = null!;

        [JsonPropertyName("challenge_ts")] public DateTime ChallengeDate { get; set; }

        [JsonPropertyName("hostname")] public string Hostname { get; set; } = null!;
    }

    private class ReCaptchaV2Response
    {
        [JsonPropertyName("success")] public bool Success { get; set; }

        [JsonPropertyName("challenge_ts")] public DateTime ChallengeTs { get; set; }

        [JsonPropertyName("hostname")] public string Hostname { get; set; } = null!;
    }
}