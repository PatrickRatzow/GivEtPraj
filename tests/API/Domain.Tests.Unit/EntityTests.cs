using Commentor.GivEtPraj.Domain.Entities;
using DomainFixture;
using DomainFixture.SourceGenerator;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Domain.Tests.Unit;

public partial class EntityTests
{
    [TestCaseSource(
        typeof(EntityTestSource), 
        nameof(EntityTestSource.Test), 
        new object[] { typeof(Case), new [] { typeof(BaseEntity) } }
    )]
    public void CaseTests(IEntityTester tester, string propertyName, string value) => tester.Run();
    
    [TestCaseSource(
        typeof(EntityTestSource), 
        nameof(EntityTestSource.Test), 
        new object[] { typeof(CaseImage), new [] { typeof(BaseEntity) } }
    )]
    public void CaseImageTests(IEntityTester tester, string propertyName, string value) => tester.Run();
    
    [TestCaseSource(
        typeof(EntityTestSource), 
        nameof(EntityTestSource.Test), 
        new object[] { typeof(CaseUpdate), new [] { typeof(BaseEntity) } }
    )]
    public void CaseUpdateTests(IEntityTester tester, string propertyName, string value) => tester.Run();
    
    [TestCaseSource(
        typeof(EntityTestSource), 
        nameof(EntityTestSource.Test), 
        new object[] { typeof(Category), new [] { typeof(BaseEntity) } }
    )]
    public void CategoryTests(IEntityTester tester, string propertyName, string value) => tester.Run();
    
    [TestCaseSource(
        typeof(EntityTestSource), 
        nameof(EntityTestSource.Test), 
        new object[] { typeof(MiscellaneousCase), new [] { typeof(BaseEntity) } }
    )]
    public void MiscellaneousCaseTests(IEntityTester tester, string propertyName, string value) => tester.Run();
    
    [TestCaseSource(
        typeof(EntityTestSource), 
        nameof(EntityTestSource.Test), 
        new object[] { typeof(SubCategory), new [] { typeof(BaseEntity) } }
    )]
    public void SubCategoryTests(IEntityTester tester, string propertyName, string value) => tester.Run();
    
    [TestCaseSource(
        typeof(EntityTestSource),
        nameof(EntityTestSource.Test), 
        new object[] { typeof(ReCaptchaAuthorization), new [] { typeof(BaseEntity) } }
    )]
    public void ReCaptchaAuthorizationTests(IEntityTester tester, string propertyName, string value) => tester.Run();
}