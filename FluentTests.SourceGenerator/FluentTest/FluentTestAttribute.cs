using System;

namespace FluentTests.SourceGenerator;

public class FluentTestAttribute : Attribute
{
    private readonly Type _type;

    public FluentTestAttribute(Type type)
    {
        _type = type;
    }
}