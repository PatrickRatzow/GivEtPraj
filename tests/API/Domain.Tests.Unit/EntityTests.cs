using Commentor.GivEtPraj.Domain.Entities;
using DomainFixture.SourceGenerator;

namespace Commentor.GivEtPraj.Domain.Tests.Unit;

[GenerateFixtureTests(typeof(Case))]
[GenerateFixtureTests(typeof(CaseImage))]
[GenerateFixtureTests(typeof(CaseUpdate))]
[GenerateFixtureTests(typeof(Category))]
[GenerateFixtureTests(typeof(MiscellaneousCase))]
[GenerateFixtureTests(typeof(SubCategory))]
[GenerateFixtureTests(typeof(ReCaptchaAuthorization))]
public partial class EntityTests
{
}