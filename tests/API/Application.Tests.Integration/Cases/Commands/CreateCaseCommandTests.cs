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
    [Test]
    public async Task ShouldCreateCase()
    {
        // Arrange
        var category = Database.Factory<CategoryFactory>().Create();
        var subCats = Database.Factory<SubCategoryFactory>().CreateMany(category, 2);

        await Database.Save();

        var comment = "An example Comment";
        var subCategories = subCats.Select(s => s.Id).ToArray();
        var images = new List<string>();
        var longitude = 0;
        var latitude = 0;

        var cases = new List<CaseCreationDto>
        {
            new(images, category.Id, longitude, latitude, comment: comment, subCategories: subCategories)
        };
        var command = new CreateCaseCommand(cases);

        // Act
       await Send(command);

        // Assert
        var dbResult = await Search<BaseCase>(c => c.CategoryId == category.Id);
        dbResult.Should().HaveCount(1)
            .And.AllBeOfType<Case>();
    }

    [Test]
    public async Task ShouldCreateMiscellaneousCase()
    {
        // Arrange
        var category = Database.Factory<CategoryFactory>().Create(miscellaneous: true);

        await Database.Save();

        var description = "An example Description";
        var images = new List<string>();
        var longitude = 0;
        var latitude = 0;

        var cases = new List<CaseCreationDto>
        {
            new(images, category.Id, longitude, latitude, description)
        };
        var command = new CreateCaseCommand(cases);

        // Act
        var result = await Send(command);

        // Assert
        result.Value.Should().BeOfType<Unit>();
        var dbResult = await Search<BaseCase>(c => c.CategoryId == category.Id);
        dbResult.Should().HaveCount(1)
            .And.AllBeOfType<MiscellaneousCase>();
    }
    
    [Test]
    public async Task ShouldNotCreateMiscellaneousCaseIfGivenCategoryIsNotMiscellaneous()
    {
        // Arrange
        var category = Database.Factory<CategoryFactory>().Create(miscellaneous: false);

        await Database.Save();

        var description = "An example Description";
        var images = new List<string>();
        var longitude = 0;
        var latitude = 0;

        var cases = new List<CaseCreationDto>
        {
            new(images, category.Id, longitude, latitude, description)
        };
        var command = new CreateCaseCommand(cases);

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
        var comment = "An example comment";
        var categoryId = int.MaxValue;
        var subCategories = Array.Empty<int>();
        var images = new List<string>();
        var longitude = 0;
        var latitude = 0;

        var cases = new List<CaseCreationDto>
        {
            new(images, categoryId, longitude, latitude, comment: comment, subCategories: subCategories)
        };
        var command = new CreateCaseCommand(cases);

        // Act
        var result = await Send(command);

        // Assert
        result.Value.Should().BeOfType<InvalidCategory>();
        var dbCount = await Count<BaseCase>();
        dbCount.Should().Be(0);
    }
}