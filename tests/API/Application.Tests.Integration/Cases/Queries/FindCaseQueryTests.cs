using Commentor.GivEtPraj.Application.Cases.Queries;
using Commentor.GivEtPraj.Application.Categories.Queries;
using Commentor.GivEtPraj.Application.Contracts;
using Commentor.GivEtPraj.Application.Errors;
using Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;
using Commentor.GivEtPraj.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commentor.GivEtPraj.Application.Tests.Integration.Cases.Queries
{
    //Tjek om pic og cat er null

    //Find
    //Ikke find
    //Cat sat

    using static Testing;
    public class FindCaseQueryTests : TestBase
    {
        [Test]
        public async Task ShouldFindCase()
        {
            //Arrange
            var category = Database.Factory<CategoryFactory>().Create();
            var @case = Database.Factory<CaseFactory>().Create(category);

            await Database.Save();

            var query = new FindCaseQuery(@case.Id);

            //Act
            var result = await Send(query);

            //Assert
            result.Value.Should().BeOfType<CaseSummaryDto>();
            result.Value.Should().NotBeNull();
        }

        [Test]
        public async Task ShouldNotFindCase()
        {
            //Arrange

            var query = new FindCaseQuery(int.MaxValue);

            //Act
            var result = await Send(query);

            //Assert
            result.Value.Should().BeOfType<CaseNotFound>();
        }


        [Test]
        public async Task ShouldEnsureCategoryIsIncludedInResult()
        {
            //Arrange
            var category = Database.Factory<CategoryFactory>().Create();
            var @case = Database.Factory<CaseFactory>().Create(category);

            await Database.Save();

            var query = new FindCaseQuery(@case.Id);

            //Act
            var result = await Send(query);

            //Assert
            result.Value.As<CaseSummaryDto>().Category.Should().NotBeNull();
        }
    }
}
