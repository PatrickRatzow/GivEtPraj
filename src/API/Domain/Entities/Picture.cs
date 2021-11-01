namespace Commentor.GivEtPraj.Domain.Entities;

public class Picture
{
    public Guid Id { get; set; } 
    public int CaseId { get; set; }
    public BaseCase BaseCase { get; set; }
}