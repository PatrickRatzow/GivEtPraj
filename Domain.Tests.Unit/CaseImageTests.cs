using Commentor.GivEtPraj.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tests.Unit;

public class CaseImageTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void CaseImageInitialization()
    {
        //Arrange
        
        //Act
        CaseImage caseImage = new CaseImage(Guid.NewGuid());
        //Assert

        Assert.IsNotNull(caseImage);

    }
}
