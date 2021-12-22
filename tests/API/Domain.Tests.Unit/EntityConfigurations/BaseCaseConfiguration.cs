using System;
using System.Collections.Generic;
using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.EntityConfigurations;

public class BaseCaseConfiguration : AbstractEntityConfiguration<BaseCase>
{
    public override void Configure()
    {
        Property(x => x.GeographicLocation)
            .Valid(_ => GeographicLocation.From(0, 0))
            .Invalid(_ => null!);

        Property(x => x.CaseUpdates)
            .Valid(new List<CaseUpdate>())
            .Invalid(_ => null!);
        
        Property(x => x.UpdatedAt)
            .Valid(_ => DateTimeOffset.UtcNow.AddMinutes(-5))
            .Valid(new DateTimeOffset?())
            .Invalid(_ => DateTimeOffset.UtcNow.AddMinutes(5));
    }
}