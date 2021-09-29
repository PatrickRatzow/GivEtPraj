using Commentor.GivEtPraj.Application.Errors.Interfaces;

namespace Commentor.GivEtPraj.Application.Errors;

public record CaseNotFound(int Id) : INotFoundError
{
    public string ErrorMessage => $"Unable to find any error with the ID {Id}";
}