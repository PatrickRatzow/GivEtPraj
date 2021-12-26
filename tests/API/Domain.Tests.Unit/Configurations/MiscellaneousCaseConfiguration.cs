using System;
using System.Collections.Generic;
using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentTests;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Configurations;

public class MiscellaneousCaseConfiguration : AbstractClassConfiguration<MiscellaneousCase>
{
    public override void Configure()
    {
        Property(x => x.Description)
            .Valid(new string('a', 4))
            .Valid(new string('a', 4096))
            .Invalid(new string('a', 3))
            .Invalid(new string('a', 4097))
            .Invalid(new string(' ', 4));
    }
}