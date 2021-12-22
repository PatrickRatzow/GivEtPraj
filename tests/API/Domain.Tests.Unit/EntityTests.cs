using System;
using System.IO;
using Commentor.GivEtPraj.Domain.Entities;
using NUnit.Framework;

namespace Commentor.GivEtPraj.Domain.Tests.Unit;

public class AutoTestsAttribute : Attribute
{
    public AutoTestsAttribute(params Type[] entityTypes) 
    {
    }
}

[AutoTests(
    typeof(Case), 
    typeof(CaseImage), 
    typeof(CaseUpdate), 
    typeof(Category),
    typeof(MiscellaneousCase), 
    typeof(SubCategory), 
    typeof(ReCaptchaAuthorization)
)]
public partial class EntityTests
{
    [TestCaseSource(
        typeof(EntityTestSource), 
        nameof(EntityTestSource.Test), 
        new object[] { typeof(Case) }
    )]
    public void CaseTests(IEntityTester tester, string propertyName, string value) => tester.Run();
    
    [TestCaseSource(
        typeof(EntityTestSource), 
        nameof(EntityTestSource.Test), 
        new object[] { typeof(CaseImage) }
    )]
    public void CaseImageTests(IEntityTester tester, string propertyName, string value) => tester.Run();

    [TestCaseSource(
        typeof(EntityTestSource), 
        nameof(EntityTestSource.Test), 
        new object[] { typeof(CaseUpdate) }
    )]
    public void CaseUpdateTests(IEntityTester tester, string propertyName, string value) => tester.Run();
    
    [TestCaseSource(
        typeof(EntityTestSource), 
        nameof(EntityTestSource.Test), 
        new object[] { typeof(Category) }
    )]
    public void CategoryTests(IEntityTester tester, string propertyName, string value) => tester.Run();
    
    [TestCaseSource(
        typeof(EntityTestSource), 
        nameof(EntityTestSource.Test), 
        new object[] { typeof(MiscellaneousCase) }
    )]
    public void MiscellaneousCaseTests(IEntityTester tester, string propertyName, string value) => tester.Run();
    
    [TestCaseSource(
        typeof(EntityTestSource), 
        nameof(EntityTestSource.Test), 
        new object[] { typeof(SubCategory) }
    )]
    public void SubCategoryTests(IEntityTester tester, string propertyName, string value) => tester.Run();
    
    [TestCaseSource(
        typeof(EntityTestSource),
        nameof(EntityTestSource.Test), 
        new object[] { typeof(ReCaptchaAuthorization) }
    )]
    public void ReCaptchaAuthorizationTests(IEntityTester tester, string propertyName, string value) => tester.Run();
}