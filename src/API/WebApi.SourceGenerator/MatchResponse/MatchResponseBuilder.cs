using Microsoft.CodeAnalysis;

namespace Commentor.GivEtPraj.WebApi.SourceGenerator.MatchResponse
{
    public class MatchResponseBuilder : CodeBuilder
    {
        public MatchResponseBuilder(GeneratorExecutionContext context) : base(context)
        {
        }

        public override bool IsPartial => true;
        public override bool IsStatic => true;
        public override string NamespaceName => "Commentor.GivEtPraj.WebApi.Extensions";
        public override string ClassName => "OneOfExtensions";

        protected override void BeforeGenerated()
        {
            Use("System");
            Use("System.CodeDom.Compiler");
            Use("OneOf");
            Use("Microsoft.AspNetCore.Mvc");
        }

        protected override void WriteCode()
        {
            const int amountOfMethods = 8;
            for (var i = 0; i <= amountOfMethods; i++)
            {
                var genericArr = new string[i + 1];
                var parametersArr = new string[i + 1];
                for (var j = 0; j <= i; j++)
                {
                    genericArr[j] = $"T{j}";
                    parametersArr[j] = $"Func<{genericArr[j]}, IActionResult>? t{j} = null";
                }
                var generics = string.Join(", ", genericArr);
                var parameters = string.Join(", ", parametersArr);

                AppendLine("[GeneratedCode(\"Commentor.GivEtPraj.WebApi.SourceGenerator\", \"1.0.0\")]");
                AppendLine(
                    $"public static IActionResult MatchResponse<{generics}>(this OneOf<{generics}> oneOf, {parameters})");
                AppendLine("{");
                Indent();
                AppendLine("return oneOf.Match(");
                Indent();
                for (var j = 0; j <= i; j++)
                {
                    var comma = j == i ? "" : ",";
                    AppendLine($"t{j} ?? MatchErrorResponse{comma}");
                }
                Outdent();
                AppendLine(");");
                Outdent();
                AppendLine("}");
                AppendLine();
            }
        }
    }
}