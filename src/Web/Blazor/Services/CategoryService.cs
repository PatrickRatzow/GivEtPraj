using System.Net.Http.Json;
using Commentor.GivEtPraj.Application.Contracts;

namespace Commentor.GivEtPraj.Blazor.Services;

public interface ICategoryService
{
    public event Action? OnChange;
    List<CategoryDto> Categories { get; }
    Task<List<CategoryDto>> GetAllCategories();
}

public class CategoryService : ICategoryService
{
    private readonly HttpFallbackClient _httpFallbackClient;
    public event Action? OnChange; 
    public List<CategoryDto> Categories { get; private set; } = new();

    public CategoryService(HttpFallbackClient httpFallbackClient)
    {
        _httpFallbackClient = httpFallbackClient;
    }

    public async Task<List<CategoryDto>> GetAllCategories()
    {
        var categories = await _httpFallbackClient.GetOrFallbackAsync<List<CategoryDto>>("categories", "categories");
        if (categories is not null)
        {
            Categories = categories;
            OnChange?.Invoke();
        }

        return Categories;
    }
}