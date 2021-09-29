using BrowserInterop.Extensions;
using BrowserInterop.Geolocation;
using Microsoft.JSInterop;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Blazor.Services
{
    public interface IGeoLocationService
    {
        Task ShowAlertWindow();
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

        public async Task ShowAlertWindow()
        {
            await _jsRuntime.InvokeVoidAsync("showAlert", "JS function called!");
        }

    }

}