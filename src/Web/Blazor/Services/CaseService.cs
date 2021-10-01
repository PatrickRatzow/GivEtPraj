using Commentor.GivEtPraj.WebApi.Contracts.Requests;
using System.Net.Http.Json;

namespace Commentor.GivEtPraj.Blazor.Services 
{
    public interface ICaseService
    {
        Task CreateCase(string title, string description, IList<string> images, string category, double longitude, double latitude);
    }

    public class CaseService : ICaseService
    {
        private readonly HttpClient _httpClient;

        public CaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCase(string title, string description, IList<string> images, string category, double longitude, double latitude)
        {
            var request = new CreateCaseRequest(title, description, images, category, longitude, latitude);
            var response = await _httpClient.PostAsJsonAsync("cases", request);
            response.EnsureSuccessStatusCode();
        }
    }
}