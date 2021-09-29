using Microsoft.JSInterop;

namespace Blazor.Services
{
    public interface IServiceWorkerService
    {
        void StartWorker();
    }

    public class ServiceWorkerService : IServiceWorkerService
    {
        private readonly IJSRuntime _jsRuntime;

        public ServiceWorkerService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async void StartWorker()
        {
            await _jsRuntime.InvokeVoidAsync("pingWorker");
        }
    }
}
