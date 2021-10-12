using System.Collections.Generic;

namespace Commentor.GivEtPraj.Application.Contracts;

public class CategoryDto
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public IList<SubCategoryDto> SubCategories { get; set; } = new List<SubCategoryDto>();
}