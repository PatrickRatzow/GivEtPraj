using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Commentor.GivEtPraj.Application.Common.Options;
using Commentor.GivEtPraj.Application.Common.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Commentor.GivEtPraj.Application.Common.Behaviors;

public class ReCaptchaBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IAppDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ReCaptchaBehavior<TRequest, TResponse>> _logger;
    private readonly ReCaptchaOptions _options;

    public ReCaptchaBehavior(IOptions<ReCaptchaOptions> options, ILogger<ReCaptchaBehavior<TRequest, TResponse>> logger,
        HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IAppDbContext context)
    {
        _options = options.Value;
        _logger = logger;
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var reCaptchaAttribute = request.GetType().GetCustomAttribute<ReCaptchaAttribute>();
        if (reCaptchaAttribute is null) return await next();

        if (reCaptchaAttribute.AllowQueue)
        {
            var result = await VerifyQueue();

            if (result)
                return await next();
        }

        bool? v3Result = null;
        bool? v2Result = null;

        v3Result = await VerifyV3(reCaptchaAttribute.MinimumScore);
        if (v3Result is null || !v3Result.Value)
            v2Result = await VerifyV2();

        if (v3Result is null && v2Result is null)
            throw new UnauthorizedAccessException();

        return await next();
    }

    private async Task<bool?> VerifyV3(float minimumScore)
    {
        var userInput = _httpContextAccessor.HttpContext?.Request.Headers["X-ReCAPTCHA-V3"];
        if (string.IsNullOrEmpty(userInput)) return null;

        var resp = await _httpClient.PostAsync(
            $"https://www.google.com/recaptcha/api/siteverify?secret={_options.V3}&response={userInput}", null);
        var content = await resp.Content.ReadAsStringAsync();
        var captcha = JsonSerializer.Deserialize<ReCaptchaV3Response>(content);

        _logger.LogInformation("ReCAPTCHA V3 score was {CaptchaScore} and it had to be at least {MinimumScore}",
            captcha?.Score ?? 0f, minimumScore);

        const float scoreEpsilon = 0.01f;
        if (captcha?.Score < minimumScore - scoreEpsilon)
            throw new ForbiddenAccessException();

        return true;
    }

    private async Task<bool?> VerifyV2()
    {
        var userInput = _httpContextAccessor.HttpContext?.Request.Headers["X-ReCAPTCHA-V2"];
        if (string.IsNullOrEmpty(userInput)) return null;

        var resp = await _httpClient.PostAsync(
            $"https://www.google.com/recaptcha/api/siteverify?secret={_options.V2}&response={userInput}", null);
        var content = await resp.Content.ReadAsStringAsync();
        var captcha = JsonSerializer.Deserialize<ReCaptchaV2Response>(content);

        _logger.LogInformation("ReCAPTCHA V2 result: {CaptchaResult}", captcha!.Success);

        if (!captcha.Success)
            throw new ForbiddenAccessException();

        return true;
    }

    private async Task<bool> VerifyQueue()
    {
        var userInput = _httpContextAccessor.HttpContext?.Request.Headers["X-QueueKey"];
        if (string.IsNullOrEmpty(userInput)) return false;
        if (!Guid.TryParse(userInput, out var guid)) return false;

        var queueKey = await _context.QueueKeys
            .FirstOrDefaultAsync(qk => qk.Id == guid && qk.ExpiresAt > DateTimeOffset.UtcNow);
        if (queueKey is null) return false;

        _context.QueueKeys.Remove(queueKey);
        await _context.SaveChangesAsync();

        return true;
    }

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