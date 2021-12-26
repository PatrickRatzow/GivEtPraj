using System;
using System.Collections.Generic;
using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentTests;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Configurations;

public class BaseCaseConfiguration : AbstractClassConfiguration<BaseCase>
{
    public override void Configure()
    {
        Property(x => x.GeographicLocation)
            .Valid(_ => GeographicLocation.From(0, 0))
            .Invalid(_ => null!);

        Property(x => x.CaseUpdates)
            .Valid(new List<CaseUpdate>())
            .Invalid(_ => null!);
    }
}