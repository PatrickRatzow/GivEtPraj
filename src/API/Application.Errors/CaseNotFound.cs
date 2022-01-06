using Commentor.GivEtPraj.Application.Errors.Interfaces;

namespace Commentor.GivEtPraj.Application.Errors;

public readonly record struct CaseNotFound(Guid Id) : INotFoundError
{
    public string ErrorMessage => $"Unable to find any case with the ID {Id}";
}