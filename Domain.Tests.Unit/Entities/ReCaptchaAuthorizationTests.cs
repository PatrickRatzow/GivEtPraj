using System;
using Commentor.GivEtPraj.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Entities;

public class ReCaptchaAuthorizationTests
{
    [Test]
    public void ReCaptchaAuthorizationInitialization()
    {
        // Arrange
        var deviceId = Guid.NewGuid();

        // Act
        var reCaptchaAuthorization = new ReCaptchaAuthorization(deviceId, DateTimeOffset.UtcNow.AddDays(1));

        // Assert
        reCaptchaAuthorization.Should().NotBeNull();
    }
}

