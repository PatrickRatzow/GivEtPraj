using Commentor.GivEtPraj.Domain.Enums;

namespace Commentor.GivEtPraj.Domain.Entities;

public class CaseUpdate
{
    public int Id { get; set; }
    public int CaseId { get; set; }
    public BaseCase BaseCase { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Status CurrentStatus { get; set; }
    public Employee Employee { get; set; } = null!;
    public bool SendToReporter { get; set; }
}