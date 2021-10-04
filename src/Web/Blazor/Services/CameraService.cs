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
        var stringToRemove = "data:image/png;base64,";
        var photo = await _jsRuntime.InvokeAsync<string>("takePhoto");
        return photo.Replace(stringToRemove, string.Empty);
    }
}