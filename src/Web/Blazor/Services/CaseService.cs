using System.Net.Http.Json;
using Commentor.GivEtPraj.WebApi.Contracts.Requests;

namespace Commentor.GivEtPraj.Blazor.Services;

public interface ICaseService
{
    Task CreateCase(CreateCaseRequest caseRequest, string reCaptchaResp);
}

public class CaseService : ICaseService
{
    private readonly HttpClient _httpClient;

    public CaseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateCase(CreateCaseRequest caseRequest, string reCaptchaResp)
    {
        _httpClient.DefaultRequestHeaders.Add("X-ReCaptchaResponse", reCaptchaResp);
        var response = await _httpClient.PostAsJsonAsync("cases", caseRequest);
        response.EnsureSuccessStatusCode();
    }
}