using Commentor.GivEtPraj.Domain.ValueObject;

namespace Commentor.GivEtPraj.Domain.Entities;

public class Case
{
    public int Id { get; set; }
    public string Description { get; set; }
    public List<Picture> Pictures { get; set; } = new();
    public GeographicLocation Coords {  get; set; }
    public Category Category { get; set; }
    public int Priority {  get; set; }
    public string IpAddress {  get; set; }  
}