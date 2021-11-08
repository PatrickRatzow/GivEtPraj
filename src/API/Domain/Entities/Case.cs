using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.ValueObjects;
using System.Net;

namespace Commentor.GivEtPraj.Domain.Entities;

public class Case
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public List<CaseImage> Pictures { get; set; } = new();
    public GeographicLocation GeographicLocation { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public Priority Priority {  get; set; }
    public IPAddress IpAddress {  get; set; }  = null!;
    public List<CaseUpdate> CaseUpdates { get; set; } = null!;
}