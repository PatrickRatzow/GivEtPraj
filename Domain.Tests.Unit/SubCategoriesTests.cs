using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Unit;

public class SubCategoriesTests
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

        //Act
        SubCategory reCaptchaAuthorization = new SubCategory(1, localString, null, null);

        //Assert

        Assert.IsNotNull(reCaptchaAuthorization);

    }
}

