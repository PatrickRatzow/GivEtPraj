using Commentor.GivEtPraj.Domain.ValueObjects;

namespace Commentor.GivEtPraj.Domain.Entities;

public class SubCategory
{
    public int Id { get; set; }
    public LocalizedString Name { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public IList<Case> Cases { get; set; } = new List<Case>();
}