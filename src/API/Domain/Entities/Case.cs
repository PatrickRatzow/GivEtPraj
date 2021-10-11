using Commentor.GivEtPraj.Domain.ValueObjects;

namespace Commentor.GivEtPraj.Domain.Entities;

public class Case
{
    public int Id { get; set; }
    public string Description { get; set; }
    public List<Picture> Pictures { get; set; } = new();
    public GeographicLocation GeographicLocation { get; set; }
    public Category Category { get; set; }
    public int Priority {  get; set; }
    public string IpAddress {  get; set; }  
}