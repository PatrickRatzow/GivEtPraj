namespace Commentor.GivEtPraj.Domain.Entities;

public class Case : BaseCase
{ 
    public string Comment { get; set; } = null!;
    public List<SubCategory> SubCategories = new();
}