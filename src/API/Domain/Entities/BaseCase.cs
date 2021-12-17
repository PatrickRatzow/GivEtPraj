using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public abstract class BaseCase : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid DeviceId { get; private set; }
    public Category Category { get; private set; } = null!;
    public List<CaseImage> Images { get; private set; } = new();
    public GeographicLocation GeographicLocation { get; private set; } = null!;
    public DateTimeOffset CreatedAt { get; private set; }
    public List<CaseUpdate> CaseUpdates { get; private set; } = null!;
    public DateTimeOffset? UpdatedAt { get; private set; }

    protected BaseCase()
    {
    }

    protected BaseCase(Guid id, Guid deviceId, Category category, List<CaseImage> images, GeographicLocation geographicLocation, List<CaseUpdate> caseUpdates)
    {
        Id = id;
        DeviceId = deviceId;
        Category = category;
        Images = images;
        GeographicLocation = geographicLocation;
        CreatedAt = DateTime.UtcNow;
        CaseUpdates = caseUpdates;
        UpdatedAt = CaseUpdates.LastOrDefault()?.CreatedAt;
    }

    public void AddCaseUpdate(CaseUpdate caseUpdate)
    {
        CaseUpdates.Add(caseUpdate);
        Validate();
    }
}

public class BaseCaseValidator : AbstractValidator<BaseCase>
{
    public BaseCaseValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.DeviceId).NotEmpty();
        RuleFor(x => x.Category).NotEmpty();
        RuleFor(x => x.Images).NotEmpty();
        RuleForEach(x => x.Images).SetValidator(new CaseImageValidator());
        RuleFor(x => x.GeographicLocation).NotEmpty();
        RuleFor(x => x.CreatedAt).NotEmpty();
        RuleFor(x => x.CaseUpdates).NotNull();
        RuleForEach(x => x.CaseUpdates).SetValidator(new CaseUpdateValidator());
    }
}