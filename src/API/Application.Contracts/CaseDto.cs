using System.Collections.Generic;

namespace Commentor.GivEtPraj.Application.Contracts;

public class CaseDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public List<CaseImageDto> Pictures { get; set; } = new();
    public CategoryDto Category { get; set; }
}