using System;

namespace DomainFixture.SourceGenerator;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class GenerateFixtureTests : Attribute
{
    private readonly Type _type;

    public GenerateFixtureTests(Type type)
    {
        _type = type;
    }
}