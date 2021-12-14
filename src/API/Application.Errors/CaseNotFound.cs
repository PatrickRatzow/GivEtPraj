using Commentor.GivEtPraj.Application.Errors.Interfaces;

namespace Commentor.GivEtPraj.Application.Errors;

public record CaseNotFound(Guid Id) : INotFoundError
{
    public string ErrorMessage => $"Unable to find any case with the ID {Id}";
}