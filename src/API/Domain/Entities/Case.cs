using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

public class Case : BaseCase
{
    public List<SubCategory> SubCategories { get; private set; } = new();
    public string? Comment { get; private set; }

    private Case() : base()
    {

    }
    public Case(Guid id, Guid deviceId, Category category, List<CaseImage> images, GeographicLocation geographicLocation, List<CaseUpdate> caseUpdates
        , List<SubCategory> subCategories, string comment) : base(id, deviceId, category, images, geographicLocation, caseUpdates)
    {
        SubCategories = subCategories;
        Comment = comment;
        Validate();
    }
}

public class CaseValidator : AbstractValidator<Case>
{
    public CaseValidator()
    {
        RuleFor(x => x.SubCategories).NotNull();
    }
}
