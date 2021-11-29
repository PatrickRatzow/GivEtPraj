using System;
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

        SeedCases(context);
        await context.SaveChangesAsync();
        
        SeedSubCategories(context);
        await context.SaveChangesAsync();
    }
    
    private static void SeedCategories(AppDbContext context)
    {
        var hasAny = context.Categories.Any();
        if (hasAny) return;

        context.Categories.AddRange(new()
        {
            Name = LocalizedString.From("Vejskade", "Road damage"),
            Icon = "fas fa-road"
        }, new()
        {
            Name = LocalizedString.From("Vejskiltning", "Road signs"),
            Icon = "fas fa-sign"
        }, new ()
        {
            Name = LocalizedString.From("Andet", "Other"),
            Icon = "fas fa-comment-alt",
            Miscellaneous = true
        }, new() {
            Name = LocalizedString.From("Toilet", "Toilet"),
            Icon = "fas fa-toilet"
        });
    }

    private static void SeedSubCategories(AppDbContext context)
    {
        var hasAny = context.SubCategories.Any();
        if (hasAny) return;

        var category = context.Categories.First();
        context.SubCategories.AddRange(new()
        {
            Name = LocalizedString.From("Hul", "Hole"),
            Category = category
        }, new()
        {
            Name = LocalizedString.From("Manglende markering", "Missing marking"),
            Category = category
            
        });
    }

    private static void SeedCases(AppDbContext context)
    {
        var hasAny = context.Cases.Any();
        if (hasAny) return;

        var category = context.Categories.First();
        context.Cases.AddRange(new Case
        {
            Comment = "Der er et stor hul i vejen på arbejde",
            Category = category,
            GeographicLocation = GeographicLocation.From(54, 54),
        }, new Case
        {
            Comment = "Hul vejen",
            Category = category,
            GeographicLocation = GeographicLocation.From(53, 53.5),
            Images = new()
            {
                new()
                {
                    Id = Guid.NewGuid()
                },
                new()
                {
                    Id = Guid.NewGuid()
                }
            }
        });
    }
}