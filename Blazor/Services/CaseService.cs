using System.Net.Http.Json;
using Commentor.GivEtPraj.WebApi.Contracts.Requests;

namespace Blazor.Services 
{
    public interface ICaseService
    {
        Task CreateCase(string title, string description, IList<string> images, string category);
    }

    public class CaseService : ICaseService
    {
        private readonly HttpClient _httpClient;

        public CaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCase(string title, string description, IList<string> images, string category)
        {
            var request = new CreateCaseRequest(title, description, images, category);
            var response = await _httpClient.PostAsJsonAsync("cases", request);
            response.EnsureSuccessStatusCode();
        }
    }
}