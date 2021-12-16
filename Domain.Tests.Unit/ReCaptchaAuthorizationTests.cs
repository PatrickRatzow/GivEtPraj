using Commentor.GivEtPraj.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Unit;

public class ReCaptchaAuthorizationTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void ReCaptchaAuthorizationInitialization()
    {
        //Arrange


        //Act
        ReCaptchaAuthorization reCaptchaAuthorization = new ReCaptchaAuthorization(Guid.NewGuid(), DateTime.UtcNow);

        //Assert

        Assert.IsNotNull(reCaptchaAuthorization);

    }
}

