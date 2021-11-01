namespace Commentor.GivEtPraj.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public IList<BaseCase> Cases { get; set; } = new List<BaseCase>();
    public IList<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}