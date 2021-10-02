using Microsoft.JSInterop;

namespace Commentor.GivEtPraj.Blazor.Services;

public interface ICameraService
{
    Task GetCameraFeed();
    Task<string> TakePhoto();
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

    public async Task<string> TakePhoto()
    {
        return await _jsRuntime.InvokeAsync<string>("takePhoto");
    }
}