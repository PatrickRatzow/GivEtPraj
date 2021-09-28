using Microsoft.JSInterop;
using System.Diagnostics;

namespace Blazor
{
    public interface IGeoLocationService
    {
        Task<string> GetCurrentLocation();
    }

    public class GeoLocationService : IGeoLocationService
    {
        private readonly IJSRuntime _jsRuntime;

        public GeoLocationService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetCurrentLocation()
        {
            var tempLocation = await _jsRuntime.InvokeAsync<string>("geoLocation");
            return tempLocation ;

        }

    }

}