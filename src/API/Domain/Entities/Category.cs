using Commentor.GivEtPraj.Domain.ValueObjects;

namespace Commentor.GivEtPraj.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public LocalizedString Name { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public IList<Case> Cases { get; set; } = new List<Case>();
    public IList<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}