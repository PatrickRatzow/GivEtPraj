using System;
using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.Tests.Unit.ConfigurationBuilderExtensions;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.EntityConfigurations;

public class CaseUpdateConfiguration : AbstractEntityConfiguration<CaseUpdate>
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