using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;
using DomainFixture;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Configurations;

public class SubCategoryConfiguration : AbstractClassConfiguration<SubCategory>
{
    public override void Configure()
    {
        Property(x => x.Name)
            .Valid(LocalizedString.From("Test", "Test"))
            .Invalid(_ => null!);
    }
}