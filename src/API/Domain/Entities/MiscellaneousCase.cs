using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

public class MiscellaneousCase : BaseCase
{
    public string Description { get; private set; } = null!;

    private MiscellaneousCase()
    {

    }
    public MiscellaneousCase(Guid id, Guid deviceId, Category category, List<CaseImage> images, GeographicLocation geographicLocation, List<CaseUpdate> caseUpdates, 
        string description) : base(id, deviceId, category, images, geographicLocation, caseUpdates)
    {
        Description = description;
        Validate();
    }
}

public class MiscellaneousCaseValidator : AbstractValidator<MiscellaneousCase>
{
    public MiscellaneousCaseValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
    }
}