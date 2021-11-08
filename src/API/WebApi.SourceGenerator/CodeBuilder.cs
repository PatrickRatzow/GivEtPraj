using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Commentor.GivEtPraj.WebApi.SourceGenerator
{
    public abstract class CodeBuilder
    {
        private readonly GeneratorExecutionContext _context;

        protected readonly List<string> Using = new List<string>();
        private int _currentIndent;
        protected StringBuilder StringBuilder;

        public CodeBuilder(GeneratorExecutionContext context)
        {
            _context = context;
        }

        public virtual bool IsPartial => false;
        public virtual bool IsStatic => true;
        public abstract string NamespaceName { get; }
        public abstract string ClassName { get; }

        protected virtual void BeforeGenerated()
        {
        }

        protected virtual void AfterGenerated()
        {
        }

        protected string PrintIndent()
        {
            return new string(' ', _currentIndent * 4);
        }

        protected void Indent()
        {
            _currentIndent++;
        }

        protected void Outdent()
        {
            _currentIndent--;
        }

        protected void Use(string name)
        {
            Using.Add(name);
        }

        protected void AppendLine(string line = "")
        {
            StringBuilder.AppendLine($"{PrintIndent()}{line}");
        }

        private void WriteUsing()
        {
            if (Using.Count <= 0) return;

            foreach (var name in Using) AppendLine($"using {name};");

            AppendLine();
        }

        protected abstract void WriteCode();

        public override string ToString()
        {
            StringBuilder = new StringBuilder();

            BeforeGenerated();

            WriteUsing();
            AppendLine($"namespace {NamespaceName}");
            AppendLine("{");
            Indent();
            AppendLine($"public {(IsStatic ? "static" : "")} {(IsPartial ? "partial" : "")} class {ClassName}");
            AppendLine("{");
            Indent();
            WriteCode();
            Outdent();
            AppendLine("}");
            Outdent();
            AppendLine("}");

            AfterGenerated();

            return StringBuilder.ToString();
        }
    }
}