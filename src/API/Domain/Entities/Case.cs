using System.Diagnostics.CodeAnalysis;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public class Case : BaseCase
{
    public List<SubCategory> SubCategories { get; private set; } = new();
    public string? Comment { get; private set; }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private Case()
    {
    }
    
    public Case(Guid id, Guid deviceId, Category category, List<CaseImage> images, 
        GeographicLocation geographicLocation, List<CaseUpdate> caseUpdates, List<SubCategory> subCategories, 
        string comment) : base(id, deviceId, category, images, geographicLocation, caseUpdates)
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

        When(x => x.Comment is not null, () =>
        {
            RuleFor(x => x.Comment)
                .NotEmpty()
                .MinimumLength(0)
                .MaximumLength(200);
        });
    }
}
