using System;
using Commentor.GivEtPraj.Domain.Exceptions;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.ValueObjects;

public class GeographicLocationTests
{
    [TestCase(90, 0)]
    [TestCase(0, -180)]
    [TestCase(0, 180)]
    public void ShouldCreateCoordsObject(double latitude, double longitude)
    {
        // Act
        Action action = () => GeographicLocation.From(latitude, longitude);

        // Assert
        action.Should().NotThrow();
    }

    [TestCase(-91)]
    [TestCase(91)]
    public void ShouldFailCreatingCoordsObjectDueToLatitude(double latitude)
    {
        // Arrange
        const double longitude = 0;
        
        // Act
        Action action = () => GeographicLocation.From(latitude, longitude);

        // Assert
        action.Should().ThrowExactly<InvalidLatitudeException>();
    }
    
    [TestCase(-181)]
    [TestCase(181)]
    public void ShouldFailCreatingCoordsObjectDueToLongitude(double longitude)
    {
        // Arrange
        const double latitude = 0;
        
        // Act
        Action action = () => GeographicLocation.From(latitude, longitude);

        // Assert
        action.Should().ThrowExactly<InvalidLongitudeException>();
    }
}