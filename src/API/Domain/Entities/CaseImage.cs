
using FluentValidation;

namespace Commentor.GivEtPraj.Domain.Entities;

public class CaseImage : BaseEntity
{
    public Guid Id { get; private set; }

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