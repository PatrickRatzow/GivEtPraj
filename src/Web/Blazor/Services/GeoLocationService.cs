using BrowserInterop.Extensions;
using BrowserInterop.Geolocation;
using Microsoft.JSInterop;

namespace Commentor.GivEtPraj.Blazor.Services;

public interface IGeoLocationService
{
    Task<GeolocationResult> GetCoords();
}

public class GeoLocationService : IGeoLocationService
{
    private readonly IJSRuntime _jsRuntime;

    public GeoLocationService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<GeolocationResult> GetCoords()
    {
        var window = await _jsRuntime.Window();
        var navigator = await window.Navigator();
        var geoLocationWrapper = navigator.Geolocation;

        return await geoLocationWrapper.GetCurrentPosition();
    }
}