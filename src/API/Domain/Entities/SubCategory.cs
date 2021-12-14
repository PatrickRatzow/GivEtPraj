using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

public class SubCategory : BaseEntity
{
    public Guid Id { get; set; }
    public LocalizedString Name { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public IList<Case> Cases { get; set; } = new List<Case>();

    private SubCategory()
    {

    }
    public SubCategory(Guid id, LocalizedString name, Category category, IList<Case> cases)
    {
        Id = id;
        Name = name;
        Category = category;
        Cases = cases;
        Validate();
    }
}
public class SubCategoryValidator : AbstractValidator<SubCategory>
{
    public SubCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}