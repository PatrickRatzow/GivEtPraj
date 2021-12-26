using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.Enums;
using FluentTests;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Configurations;

public class CaseUpdateConfiguration : AbstractClassConfiguration<CaseUpdate>
{
    public override void Configure()
    {
        Property(x => x.Status)
            .Valid(x => Status.Done)
            .Valid(x => 0)
            .Invalid(x => Status.Done + 1)
            .Invalid(x => (Status)(-1));
    }
}