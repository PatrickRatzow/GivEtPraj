using System.Threading.Tasks;

namespace Commentor.GivEtPraj.Application.Tests.Integration;

using static Testing;

public abstract class TestBase
{
    protected DatabaseSetup Database;

    [SetUp]
    public async Task SetUp()
    {
        Database = SetupDatabase();

        await ResetState();
    }

    [TearDown]
    public void TearDown()
    {
        Database.Dispose();
    }
}