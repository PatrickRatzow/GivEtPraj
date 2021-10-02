using Commentor.GivEtPraj.Application.Categories.Queries;
using Commentor.GivEtPraj.Application.Contracts;
using Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;
using FluentAssertions;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Application.Tests.Integration.Cases.Queries;

using static Testing;

public class FindAllCategoriesQueryTests : TestBase
{
    [Test]
    public async Task ShouldFindAllCategories()
    {
        // Arrange
        var categories = Database.Factory<CategoryFactory>().CreateMany(2);

        await Database.Save();

        var query = new FindAllCategoriesQuery();

        // Act
        var result = await Send(query);

        // Assert
        result.Should().AllBeOfType<CategoryDto>();
        result.Should().HaveCount(categories.Count);
    }

    [Test]
    public async Task ShouldReturnEmptyListIfNoCategoriesAreFound()
    {
        // Arrange
        var query = new FindAllCategoriesQuery();

        // Act
        var result = await Send(query);

        // Assert
        result.Should().BeEmpty();
    }
}