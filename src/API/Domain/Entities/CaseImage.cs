
using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
public class CaseImage : BaseEntity
{
    public Guid Id { get; private set; }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private CaseImage()
    {
    }
    
    public CaseImage(Guid id)
    {
        Id = id;
        
        Validate();
    }
}

public class CaseImageValidator : AbstractValidator<CaseImage>
{
    public CaseImageValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}