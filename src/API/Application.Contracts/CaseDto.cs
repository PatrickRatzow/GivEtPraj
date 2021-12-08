using System;
using System.Collections.Generic;
using Commentor.GivEtPraj.Domain.ValueObjects;

namespace Commentor.GivEtPraj.Application.Contracts;

public class CaseDto
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public List<CaseImageDto> Pictures { get; set; } = new();
    public CategoryDto Category { get; set; } = null!;
    public GeographicLocation GeographicLocation { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}