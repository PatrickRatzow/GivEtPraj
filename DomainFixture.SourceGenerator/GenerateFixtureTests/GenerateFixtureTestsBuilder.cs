using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Linq;

namespace DomainFixture.SourceGenerator;

public sealed class GenerateFixtureTestsBuilder : CodeBuilder
{
    private readonly INamedTypeSymbol _classSymbol;
    private readonly INamedTypeSymbol _attributeSymbol;

    public GenerateFixtureTestsBuilder(GeneratorExecutionContext context, INamedTypeSymbol classSymbol, string namespaceName, 
        string className, INamedTypeSymbol attributeSymbol) : base(context)
    {
        _classSymbol = classSymbol;
        _attributeSymbol = attributeSymbol;
        NamespaceName = namespaceName;
        ClassName = className;
    }

    public override string NamespaceName { get; protected set; }
    public override string ClassName { get; protected set; }
    private ImmutableArray<TypedConstant> _fixtures;
    
    protected override void BeforeGenerated()
    {
        NamespaceName = _classSymbol.ContainingNamespace.ToDisplayString();
        ClassName = _classSymbol.Name;
        _fixtures = _classSymbol.GetAttributes()
            .Where(ad => ad.AttributeClass!.Name == _attributeSymbol.Name)
            .SelectMany(x => x.ConstructorArguments)
            .Cast<TypedConstant>()
            .ToImmutableArray();

        Use("NUnit.Framework");
        Use("DomainFixture");

        var toUse = new HashSet<string>();
        foreach (var fixture in _fixtures)
        {
            if (fixture.Value is not INamedTypeSymbol namedTypeSymbol) continue;

            var namespaceName = namedTypeSymbol.ContainingNamespace.ToDisplayString();
            toUse.Add(namespaceName);
        }

        foreach (var value in toUse)
        {
            Use(value);
        }
    }

    protected override void WriteCode()
    {
        for (var i = 0; i < _fixtures.Length; i++)
        {
            var fixture = _fixtures[i];
            if (fixture.Value is not INamedTypeSymbol namedTypeSymbol) continue;
            var entityName = namedTypeSymbol.Name;

            AppendLine("[TestCaseSource(");
            Indent();
                AppendLine("typeof(EntityTestSource),");
                AppendLine("nameof(EntityTestSource.Test),");
                AppendLine($"new object[] {{ typeof({entityName}), new [] {{ typeof(BaseEntity) }} }}");
            Outdent();
            AppendLine(")]");
            AppendLine($"public void {entityName}Tests(IEntityTester tester, string propertyName, string value)");
            Indent();
                AppendLine("=> tester.Run();");
            Outdent();

            if (i != _fixtures.Length - 1)
                AppendLine();
        }
    }
}