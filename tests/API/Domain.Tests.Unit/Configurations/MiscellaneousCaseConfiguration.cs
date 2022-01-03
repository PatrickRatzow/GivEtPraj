using Commentor.GivEtPraj.Domain.Entities;
using DomainFixture;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Configurations;

public class MiscellaneousCaseConfiguration : AbstractClassConfiguration<MiscellaneousCase>
{
    public override void Configure()
    {
        Property(x => x.Description)
            .Length(4, 4096).IsValid()
            .Empty().IsInvalid();
    }
}