namespace Commentor.GivEtPraj.Domain.Entities;

public class CaseImage
{
    public Guid Id { get; set; }
    public int CaseId { get; set; }
    public BaseCase Case { get; set; }
}