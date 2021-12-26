using System.Collections.Generic;
using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentTests;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Configurations;

public class CategoryConfiguration : AbstractClassConfiguration<Category>
{
    public override void Configure()
    {
        Property(x => x.Name)
            .Valid(LocalizedString.From("Test", "Test"))
            .Invalid(_ => null!);

        Property(x => x.Icon)
            .Valid("fas fa-road")
            .Invalid("")
            .Invalid(_ => null!);

        Property(x => x.Cases)
            .Valid(new List<BaseCase>())
            .Invalid(_ => null!);

        Property(x => x.SubCategories)
            .Valid(new List<SubCategory>())
            .Invalid(_ => null!);
    }
}