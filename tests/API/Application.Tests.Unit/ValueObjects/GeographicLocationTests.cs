using Commentor.GivEtPraj.Domain.Exceptions;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commentor.GivEtPraj.Application.Tests.Unit.ValueObjects
{
    public class GeographicLocationTests
    {
        [Test]
        public void ShouldCreateCoordsObjectIfLatitudeHasLowestAllowedValue()
        {
            //Arrange
            double latitude = -90;
            double longitude = 0;


            //Act
            Action action = () => GeographicLocation.From(latitude, longitude);

            //Assert
            action.Should().NotThrow();

        }

        [Test]
        public void ShouldCreateCoordsObjectIfLatitudeHasHighestAllowedValue()
        {
            //Arrange
            double latitude = 90;
            double longitude = 0;


            //Act
            Action action = () => GeographicLocation.From(latitude, longitude);

            //Assert
            action.Should().NotThrow();

        }

        [Test]
        public void ShouldCreateCoordsObjectIfLongitudeHasLowestAllowedValue()
        {
            //Arrange
            double latitude = 0;
            double longitude = -180;


            //Act
            Action action = () => GeographicLocation.From(latitude, longitude);

            //Assert
            action.Should().NotThrow();

        }

        [Test]
        public void ShouldCreateCoordsObjectIfLongitudeHasHighestAllowedValue()
        {
            //Arrange
            double latitude = 0;
            double longitude = 180;


            //Act
            Action action = () => GeographicLocation.From(latitude, longitude);

            //Assert
            action.Should().NotThrow();

        }

        [Test]
        public void ShouldThrowInvalidLatitudeExceptionIfLatitudeBelowMinus90()
        {
            //Arrange
            double latitude = -91;
            double longitude = 0;


            //Act
            Action action = () => GeographicLocation.From(latitude, longitude);

            //Assert
            action.Should().Throw<InvalidLatitudeException>();

        }

        [Test]
        public void ShouldThrowInvalidLatitudeExceptionIfLatitudeAbove90()
        {
            //Arrange
            double latitude = 91;
            double longitude = 0;


            //Act
            Action action = () => GeographicLocation.From(latitude, longitude);

            //Assert
            action.Should().Throw<InvalidLatitudeException>();

        }

        [Test]
        public void ShouldThrowInvalidLongitudeExceptionIfLongitudeBelowMinus180()
        {
            //Arrange
            double latitude = 0;
            double longitude = -181;


            //Act
            Action action = () => GeographicLocation.From(latitude, longitude);

            //Assert
            action.Should().Throw<InvalidLongitudeException>();

        }

        [Test]
        public void ShouldThrowInvalidLongitudeExceptionIfLongitudeAbove180()
        {
            //Arrange
            double latitude = 0;
            double longitude = 181;


            //Act
            Action action = () => GeographicLocation.From(latitude, longitude);

            //Assert
            action.Should().Throw<InvalidLongitudeException>();

        }
    }
}
