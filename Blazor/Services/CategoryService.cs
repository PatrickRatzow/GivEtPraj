using System.Net.Http.Json;
using Commentor.GivEtPraj.Application.Contracts;

namespace Blazor.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllCategories();
}

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<CategoryDto>> GetAllCategories()
    {
        var categories = await _httpClient.GetFromJsonAsync<List<CategoryDto>>("categories");
        
        return categories ?? new();
    }
}