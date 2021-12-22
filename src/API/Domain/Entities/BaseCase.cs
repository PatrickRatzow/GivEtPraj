using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public abstract class BaseCase : BaseEntity
{
    public Guid Id { get; protected set; }
    public Guid DeviceId { get; protected set; }
    public Category Category { get; protected set; } = null!;
    public List<CaseImage> Images { get; protected set; } = new();
    public GeographicLocation GeographicLocation { get; protected set; } = null!;
    public DateTimeOffset CreatedAt { get; protected set; }
    public List<CaseUpdate> CaseUpdates { get; protected set; } = null!;
    public DateTimeOffset? UpdatedAt { get; protected set; }

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
        RuleFor(x => x.GeographicLocation).NotEmpty();
        RuleFor(x => x.CreatedAt)
            .NotEmpty()
            .LessThanOrEqualTo(_ => DateTimeOffset.UtcNow);
        RuleFor(x => x.CaseUpdates).NotNull();
        When(x => x.UpdatedAt is not null, () =>
        {
            RuleFor(x => x.UpdatedAt).LessThanOrEqualTo(_ => DateTimeOffset.UtcNow);
        });
    }
}