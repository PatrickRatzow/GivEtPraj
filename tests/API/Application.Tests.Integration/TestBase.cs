using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Tests.Integration.DatabaseFactories;
using NUnit.Framework;

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