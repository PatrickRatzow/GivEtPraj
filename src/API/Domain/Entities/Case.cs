namespace Commentor.GivEtPraj.Domain.Entities;

public class Case
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Picture> Pictures { get; set; } = new();
    public double Longitude { get; set; }
    public double Latitude {  get; set; }
    public Category Category { get; set; }
}