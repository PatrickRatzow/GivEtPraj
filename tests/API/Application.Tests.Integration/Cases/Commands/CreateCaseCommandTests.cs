using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Cases.Commands;
using Commentor.GivEtPraj.Domain.Enums;

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

        var description = "An example description";
        var images = new List<Stream>();
        var longitude = 0;
        var latitude = 0;
        var priority = Priority.Low;
        var ipAddress = IPAddress.Parse("127.0.0.1");
        var command = new CreateCaseCommand(description, images, category.Name.English, longitude, latitude, priority, ipAddress);

        // Act
        var result = await Send(command);

        // Assert
        result.Value.Should().BeOfType<int>();
        var dbResult = await Find<BaseCase>(result.Value.As<int>());
        dbResult.Should().NotBeNull();
    }

    [Test]
    public async Task ShouldNotCreateCaseIfCategoryDoesNotExist()
    {
        // Arrange
        var description = "An example description";
        var images = new List<Stream>();
        var categoryName = "Some category";
        var longitude = 0;
        var latitude = 0;
        var priority = Priority.Low;
        var ipAddress = IPAddress.Parse("127.0.0.1");
        var command = new CreateCaseCommand(description, images, categoryName, longitude, latitude, priority, ipAddress);

        // Act
        var result = await Send(command);

        // Assert
        result.Value.Should().BeOfType<InvalidCategory>();
        var dbCount = await Count<BaseCase>();
        dbCount.Should().Be(0);
    }
}