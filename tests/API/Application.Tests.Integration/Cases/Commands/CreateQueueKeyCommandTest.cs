using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Cases.Commands;

namespace Commentor.GivEtPraj.Application.Tests.Integration.Cases.Commands;

using static Testing;

public class CreateQueueKeyCommandTest : TestBase
{
    [Test]
    public async Task ShouldCreateQueueKey()
    {
        // Arrange
        var deviceId = Guid.NewGuid();

        var queueKey = new List<QueueKeyDto>();
        var command = new CreateQueueKeyCommand(deviceId);
        // Act
        await Send(command);

        // Assert
        var dbResult = await Count<RecaptchaAuthorization>();
        dbResult.Should().Be(1);
    }
}