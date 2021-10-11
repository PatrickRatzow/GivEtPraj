namespace Commentor.GivEtPraj.Domain.Entities;

public class SubCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Category Category { get; set; } = null!;
}