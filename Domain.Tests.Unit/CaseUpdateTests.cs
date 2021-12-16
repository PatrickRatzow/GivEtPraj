using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Unit;

public class CaseUpdateTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void CaseUpdateInitialization()
    {
        //Arrange
        //CaseImage
        List<CaseImage> caseImages = new List<CaseImage> { new CaseImage(Guid.NewGuid()) };

        //SubcategoryList
        LocalizedString localString = LocalizedString.From("Hul i vejen", "Hole in ground");
        SubCategory subCat = new SubCategory(Guid.NewGuid(), localString, null, null);
        List<SubCategory> subCategories = new List<SubCategory>();
        subCategories.Add(subCat);
        Category category = new Category(Guid.NewGuid(), localString, "Icon", false, null, subCategories);
        Case @case = new Case(Guid.NewGuid(), Guid.NewGuid(), category, caseImages, new GeographicLocation(0, -180),
           new List<CaseUpdate>(), subCategories, "Comment");


        //Act
        CaseUpdate caseUpdate = new CaseUpdate(Guid.NewGuid(), @case, DateTime.UtcNow, Status.InProgress, false);

        //Assert

        Assert.IsNotNull(caseUpdate);

    }
}

