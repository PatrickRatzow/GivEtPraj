using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        var subCats = Database.Factory<SubCategoryFactory>().CreateMany(category, 2);

        await Database.Save();

        var deviceId = Guid.NewGuid();
        var comment = "An example Comment";
        var subCategories = subCats.Select(s => s.Id).ToArray();
        var images = new List<Stream>();
        var longitude = 0;
        var latitude = 0;
        var priority = Priority.Low;
        var ipAddress = IPAddress.Parse("127.0.0.1");

        var command = new CreateCaseCommand(deviceId, images, category.Id,
            longitude, latitude, priority, ipAddress, "", comment, subCategories);

        // Act
        var result = await Send(command);

        // Assert
        result.Value.Should().BeOfType<int>();
        var dbResult = await Find<BaseCase>(result.Value.As<int>());
        dbResult.Should().NotBeNull();
    }

    [Test]
    public async Task ShouldCreateMiscellaneousCase()
    {
        // Arrange
        var category = Database.Factory<CategoryFactory>().Create();

        await Database.Save();

        var deviceId = Guid.NewGuid();
        var description = "An example Description";
        var images = new List<Stream>();
        var longitude = 0;
        var latitude = 0;
        var priority = Priority.Low;
        var ipAddress = IPAddress.Parse("127.0.0.1");

        var command = new CreateCaseCommand(deviceId, images, category.Id,
            longitude, latitude, priority, ipAddress, description);

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
        var deviceId = Guid.Empty;
        var description = "An example description";
        var images = new List<Stream>();
        var category = 1;
        var longitude = 0;
        var latitude = 0;
        var priority = Priority.Low;
        var ipAddress = IPAddress.Parse("127.0.0.1");

        var command = new CreateCaseCommand(deviceId, images, category, longitude, latitude, priority, ipAddress,
            description);

        // Act
        var result = await Send(command);

        // Assert
        result.Value.Should().BeOfType<InvalidCategory>();
        var dbCount = await Count<BaseCase>();
        dbCount.Should().Be(0);
    }
}