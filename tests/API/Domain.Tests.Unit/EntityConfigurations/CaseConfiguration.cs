using System.Collections.Generic;
using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.Tests.Unit.PropertyConventions;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.EntityConfigurations;

public class CaseConfiguration : AbstractEntityConfiguration<Case>
{
    public override void Configure()
    {
        Property(x => x.SubCategories)
            .Valid(new List<SubCategory>())
            .Invalid(_ => null!);

        Property(x => x.Comment)
            .Valid(new string('a', 4))
            .Valid(new string('a', 200))
            .Invalid(new string(' ', 3))
            .Invalid(new string(' ', 201));
    }
}