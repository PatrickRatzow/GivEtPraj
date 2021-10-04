using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.JSInterop;

namespace Commentor.GivEtPraj.Blazor.Services;

public class HttpFallbackClient
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly IJSRuntime _jsRuntime;

    public HttpFallbackClient(HttpClient httpClient, ILocalStorageService localStorage, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _jsRuntime = jsRuntime;
    }

    public async ValueTask<T?> GetOrFallbackAsync<T>(string url, string key)
    {
        var offline = await _jsRuntime.InvokeAsync<bool>("isOffline");
        if (offline)
            return await _localStorage.GetItemAsync<T>(key) ?? default(T);
        
        try
        {
            var result = await _httpClient.GetFromJsonAsync<T>(url);
            if (result is null) return await _localStorage.GetItemAsync<T>(key) ?? default(T);

            await _localStorage.SetItemAsync(key, result);

            return result;
        }
        catch
        {
            return await _localStorage.GetItemAsync<T>(key) ?? default(T);
        }
    }
}