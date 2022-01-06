using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Cases.Commands;
using Commentor.GivEtPraj.Domain.Enums;
using MediatR;

namespace Commentor.GivEtPraj.Application.Tests.Integration.Cases.Commands;

using static Testing;

public class CreateCaseCommandTests : TestBase
{
    private const string TestImage = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAMSURBVBhXY/D09AQAAboA3DKaDXMAAAAASUVORK5CYII=";
    
    [Test]
    public async Task ShouldCreateCase()
    {
        // Arrange
        var category = Database.Factory<CategoryFactory>().Create();
        var subCats = Database.Factory<SubCategoryFactory>().CreateMany(category, 2);

        await Database.Save();

        var id = Guid.NewGuid();
        var comment = "An example comment";
        var subCategories = subCats.Select(s => s.Id).ToArray();
        var images = new List<string> { TestImage };
        var longitude = 0;
        var latitude = 0;

        var cases = new List<CaseCreationDto>
        {
            new(images, category.Id, longitude, latitude, subCategories, null, comment)
        };
        var command = new CreateCaseCommand(id, cases);

        // Act
       await Send(command);

        // Assert
        var dbResult = await Search<BaseCase>(c => c.Category.Id == category.Id);
        dbResult.Should().HaveCount(1)
            .And.AllBeOfType<Case>();
    }

    [Test]
    public async Task ShouldCreateMiscellaneousCase()
    {
        // Arrange
        var category = Database.Factory<CategoryFactory>().Create(miscellaneous: true);

        await Database.Save();

        var id = Guid.NewGuid();
        var description = "An example Description";
        var images = new List<string> { TestImage };
        var longitude = 0;
        var latitude = 0;

        var cases = new List<CaseCreationDto>
        {
            new(images, category.Id, longitude, latitude, null, description)
        };
        var command = new CreateCaseCommand(id, cases);

        // Act
        var result = await Send(command);

        // Assert
        result.Value.Should().BeOfType<Unit>();
        var dbResult = await Search<BaseCase>(c => c.Category.Id == category.Id);
        dbResult.Should().HaveCount(1)
            .And.AllBeOfType<MiscellaneousCase>();
    }
    
    [Test]
    public async Task ShouldNotCreateMiscellaneousCaseIfGivenCategoryIsNotMiscellaneous()
    {
        // Arrange
        var category = Database.Factory<CategoryFactory>().Create(miscellaneous: false);

        await Database.Save();

        var id = Guid.NewGuid();
        var description = "An example Description";
        var images = new List<string> { TestImage };
        var longitude = 0;
        var latitude = 0;

        var cases = new List<CaseCreationDto>
        {
            new(images, category.Id, longitude, latitude, null, description)
        };
        var command = new CreateCaseCommand(id, cases);

        // Act
        var result = await Send(command);

        // Assert
        result.Value.Should().BeOfType<InvalidCategory>();
        var dbCount = await Count<BaseCase>();
        dbCount.Should().Be(0);
    }

    [Test]
    public async Task ShouldNotCreateCaseIfCategoryDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        var comment = "An example comment";
        var categoryId = Guid.NewGuid();
        var subCategories = Array.Empty<Guid>();
        var images = new List<string> { TestImage };
        var longitude = 0;
        var latitude = 0;

        var cases = new List<CaseCreationDto>
        {
            new(images, categoryId, longitude, latitude, comment: comment, subCategories: subCategories)
        };
        var command = new CreateCaseCommand(id, cases);

        // Act
        var result = await Send(command);

        // Assert
        result.Value.Should().BeOfType<InvalidCategory>();
        var dbCount = await Count<BaseCase>();
        dbCount.Should().Be(0);
    }
}