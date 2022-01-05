using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.ValueObjects;

namespace Infrastructure.Persistence;

public static class AppDbContextSeed
{
    public static async Task SeedSampleData(AppDbContext context)
    {
        SeedCategories(context);
        await context.SaveChangesAsync();

        SeedSubCategories(context);
        await context.SaveChangesAsync();

        SeedCases(context);
        await context.SaveChangesAsync();       
    }

    private static void SeedCategories(AppDbContext context)
    {
        var hasAny = context.Categories.Any();
        if (hasAny) return;

        context.Categories.AddRange(new Category(Guid.NewGuid(), LocalizedString.From("Vejskade", "Road damage"), "fas fa-road", false, new List<BaseCase>(), new List<SubCategory>()),
           new Category(Guid.NewGuid(), LocalizedString.From("Vejskiltning", "Road signs"), "fas fa-road", false, new List<BaseCase>(), new List<SubCategory>()),
           new Category(Guid.NewGuid(), LocalizedString.From("Andet", "Other"), "fas fa-comment-alt", true, new List<BaseCase>(), new List<SubCategory>()),
           new Category(Guid.NewGuid(), LocalizedString.From("Toilet", "Toilet"), "fas fa-toilet", false, new List<BaseCase>(), new List<SubCategory>()));
    }

    private static void SeedSubCategories(AppDbContext context)
    {
        var hasAny = context.SubCategories.Any();
        if (hasAny) return;

        var category = context.Categories.First();
        context.SubCategories.AddRange(new SubCategory(Guid.NewGuid(), LocalizedString.From("Hul", "Hole"), category, new List<Case>()),
            new SubCategory(Guid.NewGuid(), LocalizedString.From("Manglende markering", "Missing marking"), category, new List<Case>()));
    }

    private static void SeedCases(AppDbContext context)
    {
        var hasAny = context.Cases.Any();
        if (hasAny) return;

        var category = context.Categories.First();
        var subCategory = context.SubCategories.First();

        context.Cases.AddRange(
            new Case(
                Guid.NewGuid(),
                Guid.NewGuid(),
                category,
                new List<CaseImage> {
                    new CaseImage(Guid.NewGuid()),
                    new CaseImage(Guid.NewGuid())
                },
                GeographicLocation.From(54, 54),
                new List<CaseUpdate>(),
                new List<SubCategory> { subCategory },
                "Der er et stor hul i vejen på arbejde"
            ),
            new Case(
                Guid.NewGuid(),
                Guid.NewGuid(),
                category,
                new List<CaseImage> {
                    new CaseImage(Guid.NewGuid()),
                    new CaseImage(Guid.NewGuid())
                },
                GeographicLocation.From(53, 53.5),
                new List<CaseUpdate>(),
                new List<SubCategory> { subCategory },
                "Hul vejen"
            )
        );
    }
}