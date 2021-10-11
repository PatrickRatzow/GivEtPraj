using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.ValueObjects;
using System.Net;

namespace Commentor.GivEtPraj.Domain.Entities;

public class Case
{
    public int Id { get; set; }
    public string Description { get; set; }
    public List<Picture> Pictures { get; set; } = new();
    public GeographicLocation GeographicLocation { get; set; }
    public Category Category { get; set; }
    public Priority Priority {  get; set; }
    public IPAddress IpAddress {  get; set; }  
}