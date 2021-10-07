using Commentor.GivEtPraj.Domain.Exceptions;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Application.Tests.Unit.ValueObjects;

public class GeographicLocationTests
{
    [Test]
    public void ShouldCreateGeographicLocationObjectIfLatitudeHasLowestAllowedValue()
    {
        // Arrange
        const double latitude = -90;
        const double longitude = 0;

        // Act
        Action action = () => GeographicLocation.From(latitude, longitude);

        // Assert
        action.Should().NotThrow();
    }

    [Test]
    public void ShouldCreateGeographicLocationObjectIfLatitudeHasHighestAllowedValue()
    {
        // Arrange
        const double latitude = 90;
        const double longitude = 0;

        // Act
        Action action = () => GeographicLocation.From(latitude, longitude);

        // Assert
        action.Should().NotThrow();
    }

    [Test]
    public void ShouldCreateGeographicLocationObjectIfLongitudeHasLowestAllowedValue()
    {
        // Arrange
        const double latitude = 0;
        const double longitude = -180;

        // Act
        Action action = () => GeographicLocation.From(latitude, longitude);

        // Assert
        action.Should().NotThrow();
    }

    [Test]
    public void ShouldCreateGeographicLocationObjectIfLongitudeHasHighestAllowedValue()
    {
        // Arrange
        const double latitude = 0;
        const double longitude = 180;

        // Act
        Action action = () => GeographicLocation.From(latitude, longitude);

        // Assert
        action.Should().NotThrow();
    }

    [Test]
    public void ShouldThrowInvalidLatitudeExceptionIfLatitudeBelowMinus90()
    {
        // Arrange
        const double latitude = -91;
        const double longitude = 0;

        // Act
        Action action = () => GeographicLocation.From(latitude, longitude);

        // Assert
        action.Should().Throw<InvalidLatitudeException>();
    }

    [Test]
    public void ShouldThrowInvalidLatitudeExceptionIfLatitudeAbove90()
    {
        // Arrange
        const double latitude = 91;
        const double longitude = 0;

        // Act
        Action action = () => GeographicLocation.From(latitude, longitude);

        // Assert
        action.Should().Throw<InvalidLatitudeException>();
    }

    [Test]
    public void ShouldThrowInvalidLongitudeExceptionIfLongitudeBelowMinus180()
    {
        // Arrange
        const double latitude = 0;
        const double longitude = -181;

        // Act
        Action action = () => GeographicLocation.From(latitude, longitude);

        // Assert
        action.Should().Throw<InvalidLongitudeException>();
    }

    [Test]
    public void ShouldThrowInvalidLongitudeExceptionIfLongitudeAbove180()
    {
        // Arrange
        const double latitude = 0;
        const double longitude = 181;

        // Act
        Action action = () => GeographicLocation.From(latitude, longitude);

        // Assert
        action.Should().Throw<InvalidLongitudeException>();
    }
}