using Microsoft.JSInterop;

namespace Blazor.Services
{
    public interface ICameraService
    {
        Task GetCameraFeed();
    }

    public class CameraService : ICameraService
    {
        private readonly IJSRuntime _jsRuntime;

        public CameraService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task GetCameraFeed()
        {
            await _jsRuntime.InvokeVoidAsync("startVideo");
        }
    }
}
