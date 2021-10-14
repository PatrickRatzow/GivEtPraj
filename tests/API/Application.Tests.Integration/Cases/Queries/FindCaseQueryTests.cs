using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Cases.Queries;

namespace Commentor.GivEtPraj.Application.Tests.Integration.Cases.Queries;

using static Testing;

public class FindCaseQueryTests : TestBase
{
    [Test]
    public async Task ShouldFindCase()
    {
        // Arrange
        var category = Database.Factory<CategoryFactory>().Create();
        var @case = Database.Factory<CaseFactory>().Create(category);

        await Database.Save();

        var query = new FindCaseQuery(@case.Id);

        // Act
        var result = await Send(query);

        // Assert
        result.Value.Should().BeOfType<CaseDto>();
        result.Value.Should().NotBeNull();
    }

    [Test]
    public async Task ShouldNotFindCase()
    {
        // Arrange
        var query = new FindCaseQuery(int.MaxValue);

        // Act
        var result = await Send(query);

        // Assert
        result.Value.Should().BeOfType<CaseNotFound>();
    }


    [Test]
    public async Task ShouldEnsureCategoryIsIncludedInResult()
    {
        // Arrange
        var category = Database.Factory<CategoryFactory>().Create();
        var @case = Database.Factory<CaseFactory>().Create(category);

        await Database.Save();

        var query = new FindCaseQuery(@case.Id);

        // Act
        var result = await Send(query);

        // Assert
        result.Value.As<CaseDto>().Category.Should().NotBeNull();
    }
}