namespace Commentor.GivEtPraj.Domain.Entities;

public class Case : BaseCase
{
    public List<SubCategory> SubCategories = new();
    public string Comment { get; set; } = null!;
}