using System;
using System.Collections.Generic;

namespace Commentor.GivEtPraj.Application.Contracts;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public bool Miscellaneous { get; set; }
    public IList<SubCategoryDto> SubCategories { get; set; } = new List<SubCategoryDto>();
}