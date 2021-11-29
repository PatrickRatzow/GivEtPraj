using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Cases.Commands;

namespace Commentor.GivEtPraj.Application.Tests.Integration.Cases.Commands;

using static Testing;

public class PreAuthorizeDeviceIdCommandTests : TestBase
{
    [Test]
    public async Task ShouldCreateReCaptchaAuthorization()
    {
        // Arrange
        var command = new PreAuthorizeDeviceCommand();
        
        // Act
        await Send(command);

        // Assert
        var dbResult = await Count<ReCaptchaAuthorization>();
        dbResult.Should().Be(1);
    }
}