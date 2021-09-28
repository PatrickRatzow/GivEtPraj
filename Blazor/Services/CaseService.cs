using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Commentor.GivEtPraj.WebApi.Contracts.Requests;

namespace Blazor.Services 
{
    public interface ICaseService
    {
        Task CreateCase(string title, string description, IList<string> images);
    }

    public class CaseService : ICaseService
    {
        private readonly HttpClient _httpClient;

        public CaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCase(string title, string description, IList<string> images)
        {
            var request = new CreateCaseRequest(title, description, images);
            var response = await _httpClient.PostAsJsonAsync("cases", request);
            response.EnsureSuccessStatusCode();
        }
    }
}