using System.Diagnostics.CodeAnalysis;
using Commentor.GivEtPraj.Domain.Enums;
using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public class CaseUpdate : BaseEntity
{
    public Guid Id { get; private set; }
    public BaseCase BaseCase { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public Status Status { get; private set; }
    public bool SendToReporter { get; private set; }
    
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private CaseUpdate()
    {
    }
    
    public CaseUpdate(Guid id, BaseCase baseCase, DateTime createdAt, Status status, bool sendToReporter)
    {
        Id = id;
        BaseCase = baseCase;
        CreatedAt = createdAt;
        Status = status;
        SendToReporter = sendToReporter;
        
        Validate();
    }
}

public class CaseUpdateValidator : AbstractValidator<CaseUpdate>
{
    public CaseUpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.BaseCase).NotEmpty();
        RuleFor(x => x.CreatedAt)
            .NotEmpty()
            .LessThanOrEqualTo(_ => DateTime.UtcNow);
        RuleFor(x => x.Status).IsInEnum();
    }
}
           