using System;
using System.Collections.Generic;
using Commentor.GivEtPraj.Domain.Entities;
using DomainFixture;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Configurations;

public class CaseConfiguration : AbstractClassConfiguration<Case>
{
    public override void Configure()
    {
        Property(x => x.SubCategories)
            .Valid(new List<SubCategory>())
            .Invalid(_ => null!);

        Property(x => x.Comment!)
            .Length(4, 200).IsValid()
            .Empty().IsInvalid()
            .Valid(_ => null!);
    }
}