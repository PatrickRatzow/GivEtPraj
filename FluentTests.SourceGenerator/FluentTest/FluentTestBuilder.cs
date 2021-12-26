using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace FluentTests.SourceGenerator;

public sealed class FluentTestBuilder : CodeBuilder
{
    private readonly INamedTypeSymbol _classSymbol;

    public FluentTestBuilder(GeneratorExecutionContext context, INamedTypeSymbol classSymbol, string namespaceName, 
        string className) : base(context)
    {
        _classSymbol = classSymbol;
        NamespaceName = namespaceName;
        ClassName = className;
    }

    public override string NamespaceName { get; protected set; }
    public override string ClassName { get; protected set; }
    
    protected override void WriteCode()
    {
       
    }
}