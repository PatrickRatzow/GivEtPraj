using System;
using System.Collections.Generic;
using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Entities;

public class SubCategoriesTests
{
    [Test]
    public void SubCategoryInitialization()
    {
        // Arrange
        var id = Guid.NewGuid();
        var localizedString = LocalizedString.From("Something", "Something");
        var category = new Category(Guid.NewGuid(), localizedString, "Icon", false, new List<BaseCase>(), 
            new List<SubCategory>());
   
        // Act
        var subCategory = new SubCategory(id, localizedString, category, new List<Case>());

        // Assert
        subCategory.Should().NotBeNull();
    }
}

