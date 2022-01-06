using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Commentor.GivEtPraj.WebApi.SourceGenerator.MatchResponse
{
    [Generator]
    public class MatchResponseGenerator : ISourceGenerator
    {
        private string _code;
        private GeneratorExecutionContext _context;

        public void Execute(GeneratorExecutionContext context)
        {
            _context = context;

            GenerateCode();
            Save();
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
            {
                //Debugger.Launch();
            }
#endif
        }

        private void GenerateCode()
        {
            var builder = new MatchResponseBuilder(_context);
            _code = builder.ToString();
        }

        private void Save()
        {
           /* var descriptor = new DiagnosticDescriptor(nameof(MatchResponseGenerator), "Result",
                "Finished MatchResponseGenerator", "Compilation", DiagnosticSeverity.Warning,
                true);
            _context.ReportDiagnostic(Diagnostic.Create(descriptor, null));
           */

            // inject the created source into the users compilation
            _context.AddSource("MatchResponse", SourceText.From(_code, Encoding.UTF8));
        }
    }
}