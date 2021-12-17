using System;
using System.Collections.Generic;
using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Entities;

public class MiscellaneousCaseTests
{
    [Test]
    public void MiscellaneousCaseInitialization()
    {
        // Arrange
        var id = Guid.NewGuid();
        var deviceId = Guid.NewGuid();
        var caseImages = new List<CaseImage>
        {
            new(Guid.NewGuid())
        };
        var localizedString = LocalizedString.From("Something", "Something");
        var category = new Category(Guid.NewGuid(), localizedString, "Icon", false, new List<BaseCase>(), 
            new List<SubCategory>());

        // Act
        var @case = new MiscellaneousCase(id, deviceId, category, caseImages, 
            GeographicLocation.From(0, -180), new(), "Description");

        // Assert
        @case.Should().NotBeNull();
    }
}

