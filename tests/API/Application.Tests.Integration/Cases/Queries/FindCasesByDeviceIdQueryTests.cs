using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Cases.Queries;

namespace Commentor.GivEtPraj.Application.Tests.Integration.Cases.Queries;

using static Testing;

public class FindCasesByDeviceIdQueryTests : TestBase
{
    [Test]
    public async Task ShouldFindCasesByDeviceId()
    {
        // Arrange
        var deviceId = Guid.NewGuid();

        var category = Database.Factory<CategoryFactory>().Create();
        var casesWithDeviceId = Database.Factory<CaseFactory>().CreateMany(category, 2, deviceId);
        var cases = Database.Factory<CaseFactory>().CreateMany(category, 10);

        await Database.Save();

        var query = new FindCasesByDeviceIdQuery(deviceId);

        // Act
        var result = await Send(query);

        // Assert
        var value = result.Value.As<List<CaseDto>>();
        value.Should().Contain(@case => casesWithDeviceId.Any(c => c.Id == @case.Id));
        value.Should().Contain(@case => cases.All(c => c.Id != @case.Id));
        value.Should().HaveCount(casesWithDeviceId.Count);
    }

    [Test]
    public async Task ShouldNotFindAnyCasesThatDoesNotHaveTheDeviceId()
    {
        // Arrange
        var deviceId = Guid.NewGuid();

        var category = Database.Factory<CategoryFactory>().Create();
        Database.Factory<CaseFactory>().CreateMany(category, 10, Guid.NewGuid());

        await Database.Save();

        var query = new FindCasesByDeviceIdQuery(deviceId);

        // Act
        var result = await Send(query);

        // Assert
        var value = result.Value.As<List<CaseDto>>();
        value.Should().BeEmpty();
    }
}