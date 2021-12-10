using System;
using Commentor.GivEtPraj.Domain.Entities;
using Commentor.GivEtPraj.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Application.Tests.Unit.Entities;

public class BaseCaseTests
{
    [Test]
    public void test()
    {
        // Arrange
        var @case = new Case();

        //Act
        @case.Validate();

    }
}