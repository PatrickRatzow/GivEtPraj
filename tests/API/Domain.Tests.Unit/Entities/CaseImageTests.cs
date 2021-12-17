using System;
using Commentor.GivEtPraj.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Domain.Tests.Unit.Entities;

public class CaseImageTests
{
    [Test]
    public void CaseImageInitialization()
    {
        // Act
        var caseImage = new CaseImage(Guid.NewGuid());
        
        // Assert
        caseImage.Should().NotBeNull();
    }
}
