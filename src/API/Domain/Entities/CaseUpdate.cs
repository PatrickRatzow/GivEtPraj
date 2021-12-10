using Commentor.GivEtPraj.Domain.Enums;

namespace Commentor.GivEtPraj.Domain.Entities;

public class CaseUpdate
{
    public int Id { get; set; }
    public int CaseId { get; set; }
    public BaseCase BaseCase { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Status Status { get; set; }
    public bool SendToReporter { get; set; }
}