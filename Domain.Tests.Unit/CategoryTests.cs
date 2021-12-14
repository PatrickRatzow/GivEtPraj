using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Unit;

public class CategoryTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void CaseInitialization()
    {
        //Arrange
        LocalizedString localString = LocalizedString.From("Hul i vejen", "Hole in ground");
        List<SubCategory> subCategories = new List<SubCategory>();
        SubCategory subCategory = new SubCategory(1, localString, null, null);
        subCategories.Add(subCategory);
        //Act
        Category category = new Category(1, localString, "Icon", false, null, subCategories);

        //Assert

        Assert.IsNotNull(category);

    }
}

