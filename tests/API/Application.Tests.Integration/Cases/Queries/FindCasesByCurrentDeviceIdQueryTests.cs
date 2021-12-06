using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Cases.Queries;

namespace Commentor.GivEtPraj.Application.Tests.Integration.Cases.Queries;

using static Testing;

public class FindCasesByCurrentDeviceIdQueryTests : TestBase
{
    [Test]
    public async Task ShouldFindCasesByDeviceId()
    {
        // Arrange
        var deviceId = SetRandomDeviceId();

        var category = Database.Factory<CategoryFactory>().Create();
        var casesWithDeviceId = Database.Factory<CaseFactory>().CreateMany(category, 2, deviceId);
        var cases = Database.Factory<CaseFactory>().CreateMany(category, 10);

        await Database.Save();

        var query = new FindCasesByCurrentDeviceIdQuery();

        // Act
        var result = await Send(query);

        // Assert
        result.AsT0.Should().BeOfType<List<CaseDto>>()
            .And.Contain(@case => casesWithDeviceId.Any(c => c.Id == @case.Id))
            .And.Contain(@case => cases.All(c => c.Id != @case.Id))
            .And.HaveCount(casesWithDeviceId.Count);
    }

    [Test]
    public async Task ShouldNotFindAnyCasesThatDoesNotHaveTheDeviceId()
    {
        // Arrange
        SetRandomDeviceId();

        var category = Database.Factory<CategoryFactory>().Create();
        Database.Factory<CaseFactory>().CreateMany(category, 10, Guid.NewGuid());

        await Database.Save();

        var query = new FindCasesByCurrentDeviceIdQuery();

        // Act
        var result = await Send(query);

        // Assert
        result.AsT0.Should().BeEmpty();
    }
}