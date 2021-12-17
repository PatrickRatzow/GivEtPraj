using System;
using System.Collections.Generic;
using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Entities;

public class CategoryTests
{
    [Test]
    public void CategoryInitialization()
    {
        // Arrange
        var localizedString = LocalizedString.From("Something", "Something");
     
        // Act
        var category = new Category(Guid.NewGuid(), localizedString, "Icon", false, new List<BaseCase>(), 
            new List<SubCategory>());

        // Assert
        category.Should().NotBeNull();
    }
}

