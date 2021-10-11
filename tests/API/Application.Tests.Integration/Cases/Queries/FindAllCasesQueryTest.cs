using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Cases.Queries;

namespace Commentor.GivEtPraj.Application.Tests.Integration.Cases.Queries;

using static Testing;

public class FindAllCasesQueryTest : TestBase
{
    [Test]
    public async Task ShouldFindAllCases()
    {
        // Arrange
        var category = Database.Factory<CategoryFactory>().Create();
        var cases = Database.Factory<CaseFactory>().CreateMany(category, 2);
        
        await Database.Save();

        var query = new FindAllCasesQuery();

        // Act
        var result = await Send(query);

        // Assert
        result.Should().AllBeOfType<CaseSummaryDto>();
        result.Should().HaveCount(cases.Count);
    }
}