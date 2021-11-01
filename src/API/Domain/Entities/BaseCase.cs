using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.ValueObjects;
using System.Net;

namespace Commentor.GivEtPraj.Domain.Entities;

public abstract class BaseCase
{
    public int Id { get; set; }        
    public Category Category { get; set; } = null!;
    public List<Picture> Pictures { get; set; } = new();
    public GeographicLocation GeographicLocation { get; set; } = null!; 
    public Priority Priority {  get; set; }
    public IPAddress IpAddress {  get; set; }  = null!;
    public List<CaseUpdate> CaseUpdates { get; set; } = null!;
}