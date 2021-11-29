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
        var command = new CreateQueueKeyCommand();
        
        // Act
        await Send(command);

        // Assert
        var dbResult = await Count<ReCaptchaAuthorization>();
        dbResult.Should().Be(1);
    }
}