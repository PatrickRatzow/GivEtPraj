namespace Commentor.GivEtPraj.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public IList<Case> Cases { get; set; } = new List<Case>();
    public IList<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}