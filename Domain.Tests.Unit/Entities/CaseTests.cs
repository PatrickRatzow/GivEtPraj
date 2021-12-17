using System;
using System.Collections.Generic;
using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Entities;

public class CaseTests
{
    [Test]
    public void CaseInitialization()
    {
        //Arrange
        var id = Guid.NewGuid();
        var deviceId = Guid.NewGuid();
        var caseImages = new List<CaseImage>
        {
            new(Guid.NewGuid())
        };
        var localizedString = LocalizedString.From("Something", "Something");
        var category = new Category(Guid.NewGuid(), localizedString, "Icon", false, new List<BaseCase>(), 
            new List<SubCategory>());
        var subCategories = new List<SubCategory>
        {
            new(Guid.NewGuid(), localizedString, category, new List<Case>())
        };

        // Act
        var @case = new Case(id, deviceId, category, caseImages, GeographicLocation.From(0, -180), new(), subCategories, 
            "Comment");
        
        // Assert
        @case.Should().NotBeNull();
    }
}
