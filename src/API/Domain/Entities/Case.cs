using Commentor.GivEtPraj.Domain.ValueObjects;

namespace Commentor.GivEtPraj.Domain.Entities;

public class Case
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Picture> Pictures { get; set; } = new();
    public Coords Coords { get; set; }
    public Category Category { get; set; }
}