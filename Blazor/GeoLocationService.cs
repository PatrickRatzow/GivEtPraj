using Microsoft.JSInterop;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Blazor
{
    public interface IGeoLocationService
    {
        void GetCurrentLocation();
        Task ShowAlertWindow();
        Task<GeoLocationResponse> GetCoords();
    }

    public class GeoLocationSuccessEventArgs : EventArgs
    {
        public string Longtitude {  get; set;}
        public string Latitude {  get; set;}
    }


    public class CallBackInteropWrapper
    {
        [JsonPropertyName("__isCallBackWrapper")]
        public string IsCallBackWrapper { get; set; } = "";

        private CallBackInteropWrapper()
        {

        }
        public static CallBackInteropWrapper Create<T>(Func<T, Task> callback)
        {
            var res = new CallBackInteropWrapper
            {
                CallbackRef = DotNetObjectReference.Create(new JSInteropActionWrapper<T>(callback))
            };
            return res;
        }

        public static CallBackInteropWrapper Create(Func<Task> callback)
        {
            var res = new CallBackInteropWrapper
            {
                CallbackRef = DotNetObjectReference.Create(new JSInteropActionWrapper(callback))
            };
            return res;
        }

        public object CallbackRef { get; set; }


        private class JSInteropActionWrapper
        {
            private readonly Func<Task> toDo;

            internal JSInteropActionWrapper(Func<Task> toDo)
            {
                this.toDo = toDo;
            }
            [JSInvokable]
            public async Task Invoke()
            {
                await toDo.Invoke();
            }
        }

        private class JSInteropActionWrapper<T>
        {
            private readonly Func<T, Task> toDo;

            internal JSInteropActionWrapper(Func<T, Task> toDo)
            {
                this.toDo = toDo;
            }
            [JSInvokable]
            public async Task Invoke(T arg1)
            {
                await toDo.Invoke(arg1);
            }
        }

    }

    public class GeoLocationResponse
    {
        public event EventHandler<GeoLocationSuccessEventArgs> OnSuccess;
        public event EventHandler<GeoLocationSuccessEventArgs> OnFailure;

        public void PublishSuccess(string longtitude, string latitude)
        {
            OnSuccess.Invoke(this, new GeoLocationSuccessEventArgs
            {
                Latitude = latitude,
                Longtitude = longtitude
            });
        }

        public void PublishFailure()
        {
            OnSuccess.Invoke(this, new GeoLocationSuccessEventArgs());
        }

    }
    public class GeoLocationService : IGeoLocationService
    {
        private readonly IJSRuntime _jsRuntime;

        public GeoLocationService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async void GetCurrentLocation()
        {
            await _jsRuntime.InvokeVoidAsync("getLocation");
        }

        public async Task<GeoLocationResponse> GetCoords()
        {
            var response = new GeoLocationResponse();

            await _jsRuntime.InvokeAsync<string>("getCoords", CallBackInteropWrapper.Create<string>(async s =>
            {
                var coords = s.Split("/");
                var longitude = coords[0];
                var latitude = coords[1];

                response.PublishSuccess(longitude, latitude);
            }), CallBackInteropWrapper.Create<string>(async s =>
            {
                response.PublishFailure();
            }));

            return response;
        }

        public async Task ShowAlertWindow()
        {
            await _jsRuntime.InvokeVoidAsync("showAlert", "JS function called!");
        }

    }

}