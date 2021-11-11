namespace Commentor.GivEtPraj.Domain.Entities;

public class Case : BaseCase
{
    public List<SubCategory> SubCategories { get; set; } = new();
    public List<int> SubCategoryIds { get; set; } = new();
    public string Comment { get; set; } = null!;
}