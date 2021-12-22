using Commentor.GivEtPraj.Domain.Entities;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.EntityConfigurations;

public class MiscellaneousCaseConfiguration : AbstractEntityConfiguration<MiscellaneousCase>
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