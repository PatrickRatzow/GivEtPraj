using System.Linq;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Categories.Queries;

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

    [Test]
    public async Task ShouldFetchAllSubCategoriesForCategory()
    {
        // Arrange
        var category = Database.Factory<CategoryFactory>().Create();
        var subCategories = Database.Factory<SubCategoryFactory>().CreateMany(category, 4);

        await Database.Save();

        var query = new FindAllCategoriesQuery();

        // Act
        var result = await Send(query);

        // Assert
        result.First().SubCategories.Should().HaveCount(subCategories.Count);
    }
}