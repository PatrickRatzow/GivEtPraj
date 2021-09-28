using Microsoft.JSInterop;

namespace Blazor
{
    public interface IGeoLocationService
    {
        string GetCurrentLocation();
    }

    public class GeoLocationService : IGeoLocationService
    {
        private readonly IJSRuntime _jsRuntime;

        public GeoLocationService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public string GetCurrentLocation()
        {
            throw new NotImplementedException();
        }
    }

}