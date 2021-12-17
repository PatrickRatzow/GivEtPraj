using System.Diagnostics.CodeAnalysis;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public class SubCategory : BaseEntity
{
    public Guid Id { get; private set; }
    public LocalizedString Name { get; private set; } = null!;
    public Category Category { get; private set; } = null!;
    public IList<Case> Cases { get; private set; } = new List<Case>();

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
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