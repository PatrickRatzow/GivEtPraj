using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Tests.Unit;

public class CaseTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void CaseInitialization()
    {
        //Arrange
        //CaseImage
        List<CaseImage> caseImages = new List<CaseImage> { new CaseImage(Guid.NewGuid()) };

        //SubcategoryList
        LocalizedString localString = LocalizedString.From("Hul i vejen", "Hole in ground");
        SubCategory subCat = new SubCategory(1, localString, null, null);
        List<SubCategory> subCategories = new List<SubCategory>();
        subCategories.Add(subCat);
        Category category = new Category(1, localString, "Icon", false, null, subCategories);

        //Act
        Case @case = new Case(Guid.NewGuid(), Guid.NewGuid(), category, caseImages, new GeographicLocation(0, -180),
           new List<CaseUpdate>(), subCategories, "Comment");
        
        //Assert

        Assert.IsNotNull(@case);

    }
}
