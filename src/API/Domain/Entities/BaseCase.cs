using System.Net;
using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.ValueObjects;

namespace Commentor.GivEtPraj.Domain.Entities;

public abstract class BaseCase
{
    public int Id { get; set; }
    public Guid DeviceId { get; set; }
    public Category Category { get; set; } = null!;
    public List<CaseImage> Images { get; set; } = new();
    public GeographicLocation GeographicLocation { get; set; } = null!;
    public Priority Priority { get; set; }
    public IPAddress IpAddress { get; set; } = null!;
    public List<CaseUpdate> CaseUpdates { get; set; } = null!;
}