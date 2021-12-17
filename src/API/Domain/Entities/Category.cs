using System.Diagnostics.CodeAnalysis;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public class Category : BaseEntity
{
    public Guid Id { get; private set; }
    public LocalizedString Name { get; private set; } = null!;
    public string Icon { get; private set; } = null!;
    public bool Miscellaneous { get; private set; }
    public IList<BaseCase> Cases { get; private set; } = new List<BaseCase>();
    public IList<SubCategory> SubCategories { get; private set; } = new List<SubCategory>();

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private Category()
    {
    }

    public Category(Guid id, LocalizedString name, string icon, bool miscellaneous, IList<BaseCase> cases, IList<SubCategory> subCategories)
    {
        Id = id;
        Name = name;
        Icon = icon;
        Miscellaneous = miscellaneous;
        Cases = cases;
        SubCategories = subCategories;
        
        Validate();
    }
}

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Icon).NotEmpty();
        RuleFor(x => x.Cases).NotNull();
        RuleFor(x => x.SubCategories).NotNull();
    }
}