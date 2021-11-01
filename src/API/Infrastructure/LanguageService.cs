using Commentor.GivEtPraj.Application.Common.Interfaces;
using Commentor.GivEtPraj.Application.Contracts;

namespace Commentor.GivEtPraj.Infrastructure;

public class LanguageService : ILanguageService
{
    public Language Language { get; set; }
}