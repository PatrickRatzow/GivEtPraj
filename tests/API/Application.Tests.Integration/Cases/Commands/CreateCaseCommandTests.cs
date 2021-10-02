using Commentor.GivEtPraj.Application.Cases.Commands;
using Commentor.GivEtPraj.Application.Contracts;
using Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;
using Commentor.GivEtPraj.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Application.Tests.Integration.Cases.Commands;

using static Testing;

public class CreateCaseCommandTests : TestBase
{
    [Test]
    public async Task ShouldCreateCase()
    {
        // Arrange
        var category = Database.Factory<CategoryFactory>().Create();

        await Database.Save();

        var title = "Test Case";
        var description = "An example description";
        var images = new List<string>();
        var longitude = 0;
        var latitude = 0;
        var command = new CreateCaseCommand(title, description, images, category.Name, longitude, latitude);

        // Act
        var result = await Send(command);

        // Assert
        result.Value.Should().BeOfType<CaseSummaryDto>();
        var dbResult = await Find<Case>(result.Value.As<CaseSummaryDto>().Id);
        dbResult.Should().NotBeNull();
    }
}