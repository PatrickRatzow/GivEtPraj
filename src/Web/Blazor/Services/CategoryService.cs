using System.Net.Http.Json;
using Commentor.GivEtPraj.Application.Contracts;

namespace Commentor.GivEtPraj.Blazor.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllCategories();
}

public class CategoryService : ICategoryService
{
    private readonly HttpFallbackClient _httpFallbackClient;

    public CategoryService(HttpFallbackClient httpFallbackClient)
    {
        _httpFallbackClient = httpFallbackClient;
    }

    public async Task<List<CategoryDto>> GetAllCategories()
    {
        var categories = await _httpFallbackClient.GetOrFallbackAsync<List<CategoryDto>>("categories", "categories");

        return categories ?? new();
    }
}