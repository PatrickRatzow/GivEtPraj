using Microsoft.JSInterop;

namespace Blazor.Services
{
    public interface IServiceWorkerService
    {
        void StartWorker();
        void Notify();
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

        public async void Notify()
        {
            await _jsRuntime.InvokeVoidAsync("notify");
        }
    }
}
